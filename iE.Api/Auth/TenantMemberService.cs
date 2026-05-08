using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public enum TenantMemberOperationDecision { Allowed, Denied }

public sealed record TenantMemberOperationResult(
    TenantMemberOperationDecision Decision,
    string ReasonCode,
    string? MemberId = null,
    int? ConsumedSeatCount = null,
    int? LimitValue = null)
{
    public bool Allowed => Decision == TenantMemberOperationDecision.Allowed;
    public static TenantMemberOperationResult Allow(string reasonCode, string? memberId = null, int? consumedSeatCount = null, int? limitValue = null)
        => new(TenantMemberOperationDecision.Allowed, reasonCode, memberId, consumedSeatCount, limitValue);
    public static TenantMemberOperationResult Deny(string reasonCode, string? memberId = null, int? consumedSeatCount = null, int? limitValue = null)
        => new(TenantMemberOperationDecision.Denied, reasonCode, memberId, consumedSeatCount, limitValue);
}

public interface ITenantMemberService
{
    Task<TenantMemberOperationResult> InviteMemberAsync(string? email, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
    Task<TenantMemberOperationResult> ActivateMemberAsync(string memberId, string? externalSubject, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
    Task<TenantMemberOperationResult> DisableMemberAsync(string memberId, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
    Task<TenantMemberOperationResult> RemoveMemberAsync(string memberId, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientOrganizationUser>> GetTenantMembersAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
}

public sealed class TenantMemberService(IConfiguration configuration, ITenantContextAccessor tenantContextAccessor, InspectionReportsDbContext dbContext, EntitlementSeatLimitGuard seatLimitGuard, IAuditEventWriter auditEventWriter) : ITenantMemberService
{
    public async Task<TenantMemberOperationResult> InviteMemberAsync(string? email, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        var tenantId = ResolveTenantId(clientOrganizationId, trustedInternalRequest);
        if (tenantId is null) return TenantMemberOperationResult.Deny("tenant_context_missing");

        var normalizedEmail = NormalizeEmail(email);
        if (normalizedEmail is null) return TenantMemberOperationResult.Deny("invalid_email");

        var existing = await dbContext.ClientOrganizationUsers.FirstOrDefaultAsync(u => u.ClientOrganizationId == tenantId && u.NormalizedEmail == normalizedEmail, cancellationToken);
        if (existing is not null && IsSeatConsuming(existing.Status))
        {
            return TenantMemberOperationResult.Deny("duplicate_member", existing.Id);
        }

        var seatResult = await seatLimitGuard.CheckMaxUsersAsync(tenantId, cancellationToken);
        if (!seatResult.Allowed)
        {
            await auditEventWriter.WriteAsync(AuditActions.TenantMemberSeatLimitDenied, "TenantMember", null, AuditResults.Denied, metadata: BuildMetadata("invite", seatResult.ReasonCode, null, true, false, seatResult.ActiveSeatCount, seatResult.LimitValue));
            return TenantMemberOperationResult.Deny(seatResult.ReasonCode, consumedSeatCount: seatResult.ActiveSeatCount, limitValue: seatResult.LimitValue);
        }

        if (existing is not null)
        {
            existing.Status = ClientOrganizationUserStatuses.Invited;
            existing.UpdatedAtUtc = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(cancellationToken);
            await auditEventWriter.WriteAsync(AuditActions.TenantMemberInvited, "TenantMember", existing.Id, AuditResults.Success, metadata: BuildMetadata("invite", seatResult.ReasonCode, existing.Id, true, false, seatResult.ActiveSeatCount, seatResult.LimitValue));
            return TenantMemberOperationResult.Allow(seatResult.ReasonCode, existing.Id, seatResult.ActiveSeatCount, seatResult.LimitValue);
        }

        var row = new ClientOrganizationUser
        {
            Id = Guid.NewGuid().ToString("N"),
            ClientOrganizationId = tenantId,
            Email = normalizedEmail,
            NormalizedEmail = normalizedEmail,
            Status = ClientOrganizationUserStatuses.Invited,
            CreatedAtUtc = DateTime.UtcNow
        };
        dbContext.ClientOrganizationUsers.Add(row);
        await dbContext.SaveChangesAsync(cancellationToken);
        await auditEventWriter.WriteAsync(AuditActions.TenantMemberInvited, "TenantMember", row.Id, AuditResults.Success, metadata: BuildMetadata("invite", seatResult.ReasonCode, row.Id, true, false, seatResult.ActiveSeatCount, seatResult.LimitValue));
        return TenantMemberOperationResult.Allow(seatResult.ReasonCode, row.Id, seatResult.ActiveSeatCount, seatResult.LimitValue);
    }

    public async Task<TenantMemberOperationResult> ActivateMemberAsync(string memberId, string? externalSubject, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(externalSubject)) return TenantMemberOperationResult.Deny("invalid_external_subject");
        var tenantId = ResolveTenantId(clientOrganizationId, trustedInternalRequest);
        if (tenantId is null) return TenantMemberOperationResult.Deny("tenant_context_missing");
        var row = await dbContext.ClientOrganizationUsers.FirstOrDefaultAsync(u => u.Id == memberId && u.ClientOrganizationId == tenantId, cancellationToken);
        if (row is null) return TenantMemberOperationResult.Deny("tenant_context_missing");

        var duplicateSubject = await dbContext.ClientOrganizationUsers.AnyAsync(u => u.ClientOrganizationId == tenantId && u.Id != memberId && u.ExternalSubject == externalSubject && u.Status == ClientOrganizationUserStatuses.Active, cancellationToken);
        if (duplicateSubject) return TenantMemberOperationResult.Deny("duplicate_member", row.Id);

        row.ExternalSubject = externalSubject.Trim();
        row.Status = ClientOrganizationUserStatuses.Active;
        row.UpdatedAtUtc = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        await auditEventWriter.WriteAsync(AuditActions.TenantMemberActivated, "TenantMember", row.Id, AuditResults.Success, metadata: BuildMetadata("activate", "entitlement_allowed", row.Id, row.NormalizedEmail is not null, true, null, null));
        return TenantMemberOperationResult.Allow("entitlement_allowed", row.Id);
    }

    public async Task<TenantMemberOperationResult> DisableMemberAsync(string memberId, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
        => await TransitionStatus(memberId, ClientOrganizationUserStatuses.Disabled, AuditActions.TenantMemberDisabled, "disable", clientOrganizationId, trustedInternalRequest, cancellationToken);

    public async Task<TenantMemberOperationResult> RemoveMemberAsync(string memberId, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
        => await TransitionStatus(memberId, ClientOrganizationUserStatuses.Removed, AuditActions.TenantMemberRemoved, "remove", clientOrganizationId, trustedInternalRequest, cancellationToken);

    public async Task<IReadOnlyList<ClientOrganizationUser>> GetTenantMembersAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        var tenantId = ResolveTenantId(clientOrganizationId, trustedInternalRequest);
        if (tenantId is null) return [];
        return await dbContext.ClientOrganizationUsers.AsNoTracking().Where(u => u.ClientOrganizationId == tenantId).OrderBy(u => u.CreatedAtUtc).ToListAsync(cancellationToken);
    }

    private async Task<TenantMemberOperationResult> TransitionStatus(string memberId, string status, string auditAction, string operation, string? clientOrganizationId, bool trustedInternalRequest, CancellationToken cancellationToken)
    {
        var tenantId = ResolveTenantId(clientOrganizationId, trustedInternalRequest);
        if (tenantId is null) return TenantMemberOperationResult.Deny("tenant_context_missing");
        var row = await dbContext.ClientOrganizationUsers.FirstOrDefaultAsync(u => u.Id == memberId && u.ClientOrganizationId == tenantId, cancellationToken);
        if (row is null) return TenantMemberOperationResult.Deny("tenant_context_missing");
        row.Status = status;
        row.UpdatedAtUtc = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        await auditEventWriter.WriteAsync(auditAction, "TenantMember", row.Id, AuditResults.Success, metadata: BuildMetadata(operation, "entitlement_allowed", row.Id, row.NormalizedEmail is not null, row.ExternalSubject is not null, null, null));
        return TenantMemberOperationResult.Allow("entitlement_allowed", row.Id);
    }

    private string? ResolveTenantId(string? explicitTenantId, bool trustedInternalRequest)
    {
        var authEnabled = configuration.GetValue<bool>("Authentication:Enabled");
        if (!authEnabled) return explicitTenantId ?? tenantContextAccessor.Current?.ClientOrganizationId?.ToString();
        var current = tenantContextAccessor.Current?.ClientOrganizationId?.ToString();
        if (string.IsNullOrWhiteSpace(current)) return null;
        if (string.IsNullOrWhiteSpace(explicitTenantId)) return current;
        return trustedInternalRequest && explicitTenantId == current ? current : current;
    }

    private static bool IsSeatConsuming(string status) => status is ClientOrganizationUserStatuses.Active or ClientOrganizationUserStatuses.Invited;
    private static string? NormalizeEmail(string? email) => string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();
    private static Dictionary<string, object?> BuildMetadata(string operation, string reasonCode, string? targetUserId, bool hasEmail, bool hasExternalSubject, int? consumedSeatCount, int? limitValue)
        => new() { ["operation"] = operation, ["reasonCode"] = reasonCode, ["targetUserId"] = targetUserId, ["hasEmail"] = hasEmail, ["hasExternalSubject"] = hasExternalSubject, ["consumedSeatCount"] = consumedSeatCount, ["limitValue"] = limitValue };
}
