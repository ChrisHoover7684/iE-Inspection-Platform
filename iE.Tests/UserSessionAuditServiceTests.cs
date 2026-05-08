using System.Text.Json;
using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace iE.Tests;

public class UserSessionAuditServiceTests
{
    [Fact]
    public async Task AuthDisabled_Allows_ExplicitTenantAndSubject()
    {
        await using var db = BuildDb();
        SeedUser(db, "t1", "sub1");
        var writer = new CapturingAuditEventWriter();
        var service = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync("t1", "sub1", "sess", "dev", "1.2.3.4", "ua", default);

        Assert.Single(db.ClientOrganizationUserSessions);
        Assert.Single(db.ClientOrganizationUserDevices);
    }

    [Theory]
    [InlineData(null, "sub1")]
    [InlineData("t1", null)]
    public async Task AuthDisabled_MissingExplicitContext_IsSafeNoOp(string? tenantId, string? subject)
    {
        await using var db = BuildDb();
        var writer = new CapturingAuditEventWriter();
        var service = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync(tenantId, subject, "sess", "dev", "1.2.3.4", "ua", default);

        Assert.Empty(db.ClientOrganizationUserSessions);
        Assert.Empty(db.ClientOrganizationUserDevices);
        Assert.Empty(writer.Events);
    }

    [Fact]
    public async Task AuthEnabled_Resolves_FromTenantContext_And_Ignores_ExplicitCrossTenant()
    {
        await using var db = BuildDb();
        SeedUser(db, "tenant-from-context", "subject-from-context");
        var writer = new CapturingAuditEventWriter();
        var service = BuildService(db, writer, authEnabled: true, tenantContext: new TenantContext
        {
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            ExternalSubject = "subject-from-context"
        }, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync("other-tenant", "other-subject", "sess", "dev", "1.2.3.4", "ua", default);

        var row = db.ClientOrganizationUserSessions.Single();
        Assert.Equal("11111111-1111-1111-1111-111111111111", row.ClientOrganizationId);
        Assert.Equal("subject-from-context", row.ExternalSubject);
    }

    [Fact]
    public async Task AuthEnabled_MissingTenant_Returns_TenantContextMissing()
    {
        await using var db = BuildDb();
        var service = BuildService(db, new CapturingAuditEventWriter(), authEnabled: true, tenantContext: new TenantContext { ExternalSubject = "sub1" }, options: new AccountSharingAuditOptions { Enabled = true });

        var result = await service.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);

        Assert.False(result.Success);
        Assert.Equal("tenant_context_missing", result.ReasonCode);
    }

    [Fact]
    public async Task AuthEnabled_MissingSubject_Returns_UserUnresolved()
    {
        await using var db = BuildDb();
        var service = BuildService(db, new CapturingAuditEventWriter(), authEnabled: true, tenantContext: new TenantContext { ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111") }, options: new AccountSharingAuditOptions { Enabled = true });

        var result = await service.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);

        Assert.False(result.Success);
        Assert.Equal("user_unresolved", result.ReasonCode);
    }

    [Fact]
    public async Task RecordRequestSeen_StoresOnlyHashedSensitiveValues()
    {
        await using var db = BuildDb();
        SeedUser(db, "t1", "sub1");
        var writer = new CapturingAuditEventWriter();
        var service = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true });

        const string session = "session-token";
        const string device = "device-fingerprint";
        const string ip = "10.20.30.40";
        const string ua = "Mozilla/5.0 TestUA";
        await service.RecordRequestSeenAsync("t1", "sub1", session, device, ip, ua, default);

        var storedSession = db.ClientOrganizationUserSessions.Single();
        var storedDevice = db.ClientOrganizationUserDevices.Single();
        Assert.NotEqual(session, storedSession.SessionKeyHash);
        Assert.NotEqual(ip, storedSession.IpHash);
        Assert.NotEqual(ua, storedSession.UserAgentHash);
        Assert.NotEqual(device, storedDevice.DeviceFingerprintHash);
        Assert.Equal(64, storedSession.SessionKeyHash!.Length);
        Assert.Equal(64, storedSession.IpHash!.Length);
        Assert.Equal(64, storedSession.UserAgentHash!.Length);
        Assert.Equal(64, storedDevice.DeviceFingerprintHash.Length);

        await service.RecordRequestSeenAsync("t1", "sub1", session, device, ip, ua, default);
        var repeated = db.ClientOrganizationUserSessions.Single();
        Assert.Equal(storedSession.SessionKeyHash, repeated.SessionKeyHash);

        await service.RecordRequestSeenAsync("t1", "sub1", "different-session", "different-device", "10.20.30.41", "Another-UA", default);
        Assert.NotEqual(repeated.SessionKeyHash, db.ClientOrganizationUserSessions.OrderBy(x => x.CreatedAtUtc).Last().SessionKeyHash);
    }

    [Fact]
    public async Task RecordRequestSeen_CreatesAndUpdates_SessionAndDeviceRows()
    {
        await using var db = BuildDb();
        SeedUser(db, "t1", "sub1");
        var service = BuildService(db, new CapturingAuditEventWriter(), authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync("t1", "sub1", "sess1", "dev1", "1.1.1.1", "ua", default);
        var firstSessionSeen = db.ClientOrganizationUserSessions.Single().LastSeenAtUtc;
        var firstDeviceSeen = db.ClientOrganizationUserDevices.Single().LastSeenAtUtc;
        await Task.Delay(10);
        await service.RecordRequestSeenAsync("t1", "sub1", "sess1", "dev1", "1.1.1.2", "ua2", default);

        Assert.Single(db.ClientOrganizationUserSessions);
        Assert.Single(db.ClientOrganizationUserDevices);
        Assert.True(db.ClientOrganizationUserSessions.Single().LastSeenAtUtc > firstSessionSeen);
        Assert.True(db.ClientOrganizationUserDevices.Single().LastSeenAtUtc > firstDeviceSeen);

        await service.RecordRequestSeenAsync("t1", "sub1", "sess2", "dev2", "2.2.2.2", "ua3", default);
        Assert.Equal(2, db.ClientOrganizationUserDevices.Count());
    }

    [Fact]
    public async Task RecordRequestSeen_Isolates_ByTenant_And_Subject()
    {
        await using var db = BuildDb();
        var service = BuildService(db, new CapturingAuditEventWriter(), authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync("t1", "sub1", "sess", "dev", "1.1.1.1", "ua", default);
        await service.RecordRequestSeenAsync("t2", "sub1", "sess", "dev", "1.1.1.1", "ua", default);
        await service.RecordRequestSeenAsync("t1", "sub2", "sess", "dev", "1.1.1.1", "ua", default);

        Assert.Equal(3, db.ClientOrganizationUserSessions.Count());
        Assert.Equal(3, db.ClientOrganizationUserDevices.Count());
    }

    [Fact]
    public async Task RecordSessionEnded_UpdatesOnlyMatchingTenantSubjectSession()
    {
        await using var db = BuildDb();
        const string contextTenantId = "11111111-1111-1111-1111-111111111111";
        var ctx = new TenantContextAccessor
        {
            Current = new TenantContext { ClientOrganizationId = Guid.Parse(contextTenantId), ExternalSubject = "sub1" }
        };
        var service = BuildService(db, new CapturingAuditEventWriter(), authEnabled: true, tenantAccessor: ctx, options: new AccountSharingAuditOptions { Enabled = true });

        await service.RecordRequestSeenAsync(null, null, "target-session", "dev", "1.1.1.1", "ua", default);
        db.ClientOrganizationUserSessions.Add(new ClientOrganizationUserSession
        {
            Id = "other-session",
            ClientOrganizationId = "t2",
            ExternalSubject = "sub1",
            SessionKeyHash = "not-the-target-hash",
            StartedAtUtc = DateTime.UtcNow,
            LastSeenAtUtc = DateTime.UtcNow,
            CreatedAtUtc = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        await service.RecordSessionEndedAsync("target-session", default);

        Assert.NotNull(db.ClientOrganizationUserSessions.Single(x => x.ClientOrganizationId == contextTenantId).EndedAtUtc);
        Assert.Null(db.ClientOrganizationUserSessions.Single(x => x.ClientOrganizationId == "t2").EndedAtUtc);
    }

    [Fact]
    public async Task SuspiciousDetection_RespectsOptions_And_AuditOnlyBehavior()
    {
        await using var db = BuildDb();
        var writerDisabled = new CapturingAuditEventWriter();
        var disabled = BuildService(db, writerDisabled, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = false });
        var disabledResult = await disabled.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);
        Assert.Equal("audit_unavailable", disabledResult.ReasonCode);
        Assert.DoesNotContain(writerDisabled.Events, x => x.Action == AuditActions.SuspiciousAccountSharingDetected);

        var writer = new CapturingAuditEventWriter();
        var enabled = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true, DeviceWindowHours = 24, MaxDistinctDevicesPerWindow = 2, MaxDistinctIpHashesPerWindow = 2 });

        await enabled.RecordRequestSeenAsync("t1", "sub1", "s1", "d1", "1.1.1.1", "ua", default);
        var noSignal = await enabled.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);
        Assert.Equal("no_signal", noSignal.ReasonCode);

        await enabled.RecordRequestSeenAsync("t1", "sub1", "s2", "d2", "1.1.1.2", "ua", default);
        var possible = await enabled.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);
        Assert.Contains(possible.ReasonCode, new[] { "possible_shared_account", "no_signal" });

        await enabled.RecordRequestSeenAsync("t1", "sub1", "s3", "d3", "1.1.1.3", "ua", default);
        var excessiveDevices = await enabled.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);
        Assert.Equal("excessive_device_count", excessiveDevices.ReasonCode);

        var ipOnly = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true, DeviceWindowHours = 24, MaxDistinctDevicesPerWindow = 10, MaxDistinctIpHashesPerWindow = 1 });
        var excessiveIp = await ipOnly.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);
        Assert.Equal("excessive_location_count", excessiveIp.ReasonCode);

        Assert.All(db.ClientOrganizationUserSessions, row => Assert.False(row.IsRevoked));
        Assert.Contains(writer.Events, x => x.Action == AuditActions.SuspiciousAccountSharingDetected);
    }

    [Fact]
    public async Task AuditMetadata_Contains_Only_AllowedKeys_ForSessionDeviceSuspiciousEvents()
    {
        await using var db = BuildDb();
        var writer = new CapturingAuditEventWriter();
        var service = BuildService(db, writer, authEnabled: false, options: new AccountSharingAuditOptions { Enabled = true, MaxDistinctDevicesPerWindow = 1, MaxDistinctIpHashesPerWindow = 1 });

        await service.RecordRequestSeenAsync("t1", "sub1", "authorization: bearer abc", "dev1", "9.9.9.9", "agent", default);
        await service.RecordRequestSeenAsync("t1", "sub1", "s2", "dev2", "9.9.9.10", "agent2", default);
        await service.DetectSuspiciousAccountSharingAsync("t1", "sub1", default);

        var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "operation", "reasonCode", "hasDeviceFingerprint", "distinctDeviceCount", "distinctIpHashCount", "windowHours", "userResolved" };
        var forbidden = new[] { "raw email", "raw ip address", "raw user agent", "session key", "token", "authorization header", "request body", "provider payload", "raw subscription", "raw plan", "metadataJson" };

        foreach (var evt in writer.Events.Where(e => e.Action is AuditActions.UserSessionSeen or AuditActions.UserSessionEnded or AuditActions.SuspiciousAccountSharingDetected))
        {
            foreach (var key in evt.Metadata.Keys)
            {
                Assert.Contains(key, allowed);
                Assert.DoesNotContain(forbidden, f => key.Contains(f, StringComparison.OrdinalIgnoreCase));
            }

            var serialized = JsonSerializer.Serialize(evt.Metadata).ToLowerInvariant();
            Assert.DoesNotContain("authorization", serialized);
            Assert.DoesNotContain("token", serialized);
            Assert.DoesNotContain("requestbody", serialized);
        }
    }

    [Fact]
    public void AccountSharingOptions_Defaults_And_InvalidConfig_AreSafe()
    {
        var missing = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>()).Build();
        var missingParsed = AccountSharingAuditOptions.FromConfiguration(missing);
        Assert.False(missingParsed.Enabled);
        Assert.Equal(24, missingParsed.DeviceWindowHours);
        Assert.Equal(5, missingParsed.MaxDistinctDevicesPerWindow);
        Assert.Equal(5, missingParsed.MaxDistinctIpHashesPerWindow);

        var invalid = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["AccountSharingAudit:Enabled"] = "notabool",
            ["AccountSharingAudit:DeviceWindowHours"] = "NaN",
            ["AccountSharingAudit:MaxDistinctDevicesPerWindow"] = "0",
            ["AccountSharingAudit:MaxDistinctIpHashesPerWindow"] = "-1"
        }).Build();

        AccountSharingAuditOptions? parsed = null;
        var ex = Record.Exception(() => parsed = AccountSharingAuditOptions.FromConfiguration(invalid));
        Assert.Null(ex);
        Assert.NotNull(parsed);
        Assert.False(parsed!.Enabled);
        Assert.Equal(24, parsed.DeviceWindowHours);
        Assert.Equal(5, parsed.MaxDistinctDevicesPerWindow);
        Assert.Equal(5, parsed.MaxDistinctIpHashesPerWindow);
    }

    private static InspectionReportsDbContext BuildDb()
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new InspectionReportsDbContext(options);
    }

    private static void SeedUser(InspectionReportsDbContext db, string tenantId, string subject)
    {
        db.ClientOrganizationUsers.Add(new ClientOrganizationUser { Id = Guid.NewGuid().ToString("N"), ClientOrganizationId = tenantId, ExternalSubject = subject, Status = ClientOrganizationUserStatuses.Active });
        db.SaveChanges();
    }

    private static UserSessionAuditService BuildService(InspectionReportsDbContext db, CapturingAuditEventWriter writer, bool authEnabled, TenantContext? tenantContext = null, TenantContextAccessor? tenantAccessor = null, AccountSharingAuditOptions? options = null)
    {
        var accessor = tenantAccessor ?? new TenantContextAccessor { Current = tenantContext ?? TenantContext.Unauthenticated("test") };
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString().ToLowerInvariant() }).Build();
        return new UserSessionAuditService(db, accessor, writer, Options.Create(options ?? new AccountSharingAuditOptions { Enabled = true }), cfg);
    }

    private sealed class CapturingAuditEventWriter : IAuditEventWriter
    {
        public List<CapturedAuditEvent> Events { get; } = [];

        public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
        {
            Events.Add(new CapturedAuditEvent(action, resourceType, resourceId, result, metadata is null ? new Dictionary<string, object?>() : new Dictionary<string, object?>(metadata)));
            return Task.CompletedTask;
        }
    }

    private sealed record CapturedAuditEvent(string Action, string ResourceType, string? ResourceId, string Result, Dictionary<string, object?> Metadata);
}
