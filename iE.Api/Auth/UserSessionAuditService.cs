using System.Security.Cryptography;
using System.Text;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace iE.Api.Auth;

public sealed class AccountSharingAuditOptions
{
    public bool Enabled { get; init; }
    public int DeviceWindowHours { get; init; } = 24;
    public int MaxDistinctDevicesPerWindow { get; init; } = 5;
    public int MaxDistinctIpHashesPerWindow { get; init; } = 5;

    public static AccountSharingAuditOptions FromConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection("AccountSharingAudit");
        return new AccountSharingAuditOptions
        {
            Enabled = bool.TryParse(section["Enabled"], out var enabled) && enabled,
            DeviceWindowHours = TryPositiveInt(section["DeviceWindowHours"], 24),
            MaxDistinctDevicesPerWindow = TryPositiveInt(section["MaxDistinctDevicesPerWindow"], 5),
            MaxDistinctIpHashesPerWindow = TryPositiveInt(section["MaxDistinctIpHashesPerWindow"], 5)
        };
    }

    private static int TryPositiveInt(string? value, int fallback)
        => int.TryParse(value, out var parsed) && parsed > 0 ? parsed : fallback;
}

public sealed record UserSessionAuditResult(bool Success, string ReasonCode, int DistinctDeviceCount = 0, int DistinctIpHashCount = 0);

public interface IUserSessionAuditService
{
    Task RecordRequestSeenAsync(string? explicitTenantId, string? explicitExternalSubject, string? sessionKey, string? deviceFingerprint, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default);
    Task RecordSessionEndedAsync(string? sessionKey, CancellationToken cancellationToken = default);
    Task<UserSessionAuditResult> DetectSuspiciousAccountSharingAsync(string? explicitTenantId, string? explicitExternalSubject, CancellationToken cancellationToken = default);
}

public sealed class UserSessionAuditService(InspectionReportsDbContext dbContext, ITenantContextAccessor tenantContextAccessor, IAuditEventWriter auditEventWriter, IOptions<AccountSharingAuditOptions> options, IConfiguration configuration) : IUserSessionAuditService
{
    private readonly AccountSharingAuditOptions _options = options.Value;
    private readonly bool _authEnabled = configuration.GetValue<bool>("Authentication:Enabled");

    public async Task RecordRequestSeenAsync(string? explicitTenantId, string? explicitExternalSubject, string? sessionKey, string? deviceFingerprint, string? ipAddress, string? userAgent, CancellationToken cancellationToken = default)
    {
        var resolved = ResolveContext(explicitTenantId, explicitExternalSubject);
        if (!resolved.Success || !_options.Enabled) return;

        var now = DateTime.UtcNow;
        var sessionHash = Hash(sessionKey);
        var deviceHash = Hash(deviceFingerprint);
        var ipHash = Hash(ipAddress);
        var userAgentHash = Hash(userAgent);

        var user = await dbContext.ClientOrganizationUsers.FirstOrDefaultAsync(x => x.ClientOrganizationId == resolved.TenantId && x.ExternalSubject == resolved.ExternalSubject, cancellationToken);

        if (sessionHash is not null)
        {
            var session = await dbContext.ClientOrganizationUserSessions.FirstOrDefaultAsync(x => x.ClientOrganizationId == resolved.TenantId && x.ExternalSubject == resolved.ExternalSubject && x.SessionKeyHash == sessionHash, cancellationToken);
            if (session is null)
            {
                session = new ClientOrganizationUserSession
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ClientOrganizationId = resolved.TenantId,
                    ClientOrganizationUserId = user?.Id,
                    ExternalSubject = resolved.ExternalSubject,
                    SessionKeyHash = sessionHash,
                    StartedAtUtc = now,
                    LastSeenAtUtc = now,
                    IpHash = ipHash,
                    UserAgentHash = userAgentHash,
                    DeviceId = deviceHash,
                    CreatedAtUtc = now
                };
                dbContext.ClientOrganizationUserSessions.Add(session);
            }
            else
            {
                session.LastSeenAtUtc = now;
                session.IpHash = ipHash;
                session.UserAgentHash = userAgentHash;
                session.DeviceId = deviceHash;
            }
        }

        if (deviceHash is not null)
        {
            var device = await dbContext.ClientOrganizationUserDevices.FirstOrDefaultAsync(x => x.ClientOrganizationId == resolved.TenantId && x.ExternalSubject == resolved.ExternalSubject && x.DeviceFingerprintHash == deviceHash, cancellationToken);
            if (device is null)
            {
                dbContext.ClientOrganizationUserDevices.Add(new ClientOrganizationUserDevice
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ClientOrganizationId = resolved.TenantId,
                    ClientOrganizationUserId = user?.Id,
                    ExternalSubject = resolved.ExternalSubject,
                    DeviceFingerprintHash = deviceHash,
                    FirstSeenAtUtc = now,
                    LastSeenAtUtc = now,
                    LastIpHash = ipHash,
                    LastUserAgentHash = userAgentHash,
                    CreatedAtUtc = now
                });
            }
            else
            {
                device.LastSeenAtUtc = now;
                device.LastIpHash = ipHash;
                device.LastUserAgentHash = userAgentHash;
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        await auditEventWriter.WriteAsync(AuditActions.UserSessionSeen, "UserSession", null, AuditResults.Success, metadata: new Dictionary<string, object?> { ["operation"] = "request_seen", ["hasDeviceFingerprint"] = deviceHash is not null, ["userResolved"] = user is not null }, cancellationToken: cancellationToken);
    }

    public async Task RecordSessionEndedAsync(string? sessionKey, CancellationToken cancellationToken = default)
    {
        var sessionHash = Hash(sessionKey);
        if (sessionHash is null) return;
        var tenantId = tenantContextAccessor.Current.ClientOrganizationId?.ToString();
        if (string.IsNullOrWhiteSpace(tenantId)) return;
        var subject = tenantContextAccessor.Current.ExternalSubject;
        if (string.IsNullOrWhiteSpace(subject)) return;
        var session = await dbContext.ClientOrganizationUserSessions.FirstOrDefaultAsync(x => x.ClientOrganizationId == tenantId && x.ExternalSubject == subject && x.SessionKeyHash == sessionHash, cancellationToken);
        if (session is null) return;
        session.EndedAtUtc = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        await auditEventWriter.WriteAsync(AuditActions.UserSessionEnded, "UserSession", session.Id, AuditResults.Success, metadata: new Dictionary<string, object?> { ["operation"] = "session_end" }, cancellationToken: cancellationToken);
    }

    public async Task<UserSessionAuditResult> DetectSuspiciousAccountSharingAsync(string? explicitTenantId, string? explicitExternalSubject, CancellationToken cancellationToken = default)
    {
        if (!_options.Enabled) return new(true, "audit_unavailable");
        var resolved = ResolveContext(explicitTenantId, explicitExternalSubject);
        if (!resolved.Success) return new(false, resolved.ReasonCode);
        var since = DateTime.UtcNow.AddHours(-Math.Max(1, _options.DeviceWindowHours));
        var devices = await dbContext.ClientOrganizationUserDevices.Where(x => x.ClientOrganizationId == resolved.TenantId && x.ExternalSubject == resolved.ExternalSubject && x.LastSeenAtUtc >= since).Select(x => x.DeviceFingerprintHash).Distinct().CountAsync(cancellationToken);
        var ips = await dbContext.ClientOrganizationUserSessions.Where(x => x.ClientOrganizationId == resolved.TenantId && x.ExternalSubject == resolved.ExternalSubject && x.LastSeenAtUtc >= since && x.IpHash != null).Select(x => x.IpHash!).Distinct().CountAsync(cancellationToken);
        var reason = "no_signal";
        if (devices > _options.MaxDistinctDevicesPerWindow) reason = "excessive_device_count";
        else if (ips > _options.MaxDistinctIpHashesPerWindow) reason = "excessive_location_count";
        else if (devices > 1 && ips > 1) reason = "possible_shared_account";
        if (reason != "no_signal")
        {
            await auditEventWriter.WriteAsync(AuditActions.SuspiciousAccountSharingDetected, "UserSession", null, AuditResults.Success, metadata: new Dictionary<string, object?> { ["operation"] = "detect", ["reasonCode"] = reason, ["distinctDeviceCount"] = devices, ["distinctIpHashCount"] = ips, ["windowHours"] = _options.DeviceWindowHours, ["userResolved"] = true }, cancellationToken: cancellationToken);
        }
        return new(true, reason, devices, ips);
    }

    private (bool Success, string TenantId, string ExternalSubject, string ReasonCode) ResolveContext(string? explicitTenantId, string? explicitExternalSubject)
    {
        var tenant = tenantContextAccessor.Current;
        if (_authEnabled)
        {
            var tenantId = tenant.ClientOrganizationId?.ToString();
            if (string.IsNullOrWhiteSpace(tenantId)) return (false, string.Empty, string.Empty, "tenant_context_missing");
            var subject = tenant.ExternalSubject;
            if (string.IsNullOrWhiteSpace(subject)) return (false, string.Empty, string.Empty, "user_unresolved");
            return (true, tenantId, subject, "no_signal");
        }

        if (string.IsNullOrWhiteSpace(explicitTenantId) || string.IsNullOrWhiteSpace(explicitExternalSubject)) return (false, string.Empty, string.Empty, "tenant_context_missing");
        return (true, explicitTenantId, explicitExternalSubject, "no_signal");
    }

    private static string? Hash(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();
    }
}
