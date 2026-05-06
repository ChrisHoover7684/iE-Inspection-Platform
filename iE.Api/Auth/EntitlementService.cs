using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public interface IEntitlementService
{
    Task<EntitlementCheckResult> CheckAsync(string entitlementKey, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
}

public sealed record EntitlementCheckResult(bool Allowed, int? LimitValue, string ReasonCode)
{
    public static EntitlementCheckResult Denied(string reasonCode) => new(false, null, reasonCode);
    public static EntitlementCheckResult AllowedWithLimit(int? limitValue) => new(true, limitValue, "allowed");
}

public sealed class EntitlementService(IConfiguration configuration, ITenantContextAccessor tenantContextAccessor, InspectionReportsDbContext dbContext) : IEntitlementService
{
    public async Task<EntitlementCheckResult> CheckAsync(string entitlementKey, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(entitlementKey)) return EntitlementCheckResult.Denied("invalid_entitlement_key");

        var authEnabled = configuration.GetValue<bool>("Authentication:Enabled");
        var resolvedTenantId = ResolveTenantId(authEnabled, clientOrganizationId, trustedInternalRequest);
        if (resolvedTenantId is null)
        {
            return EntitlementCheckResult.Denied("tenant_context_missing");
        }

        var now = DateTime.UtcNow;
        var subscription = await dbContext.ClientSubscriptions
            .AsNoTracking()
            .Where(s => s.ClientOrganizationId == resolvedTenantId)
            .Where(s => s.StartsAtUtc <= now)
            .Where(s => !s.EndsAtUtc.HasValue || s.EndsAtUtc > now)
            .OrderByDescending(s => s.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription is null) return EntitlementCheckResult.Denied("subscription_unavailable");
        if (!IsSubscriptionEntitlementEligible(subscription, now)) return EntitlementCheckResult.Denied("entitlement_unavailable");

        var entitlement = await dbContext.SubscriptionEntitlements
            .AsNoTracking()
            .Where(e => e.PlanId == subscription.PlanId)
            .Where(e => e.EntitlementKey == entitlementKey)
            .OrderByDescending(e => e.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (entitlement is null || !entitlement.Enabled) return EntitlementCheckResult.Denied("entitlement_disabled");

        return EntitlementCheckResult.AllowedWithLimit(entitlement.LimitValue);
    }

    private string? ResolveTenantId(bool authEnabled, string? explicitClientOrganizationId, bool trustedInternalRequest)
    {
        if (!authEnabled) return explicitClientOrganizationId ?? tenantContextAccessor.Current?.ClientOrganizationId?.ToString();

        var currentTenant = tenantContextAccessor.Current?.ClientOrganizationId?.ToString();
        if (string.IsNullOrWhiteSpace(currentTenant)) return null;

        if (string.IsNullOrWhiteSpace(explicitClientOrganizationId)) return currentTenant;
        if (trustedInternalRequest && explicitClientOrganizationId == currentTenant) return currentTenant;

        return currentTenant;
    }

    private static bool IsSubscriptionEntitlementEligible(ClientSubscription subscription, DateTime now)
    {
        if (subscription.Status == SubscriptionStatuses.Active) return true;
        if (subscription.Status != SubscriptionStatuses.Trialing) return false;
        return subscription.TrialEndsAtUtc.HasValue && subscription.TrialEndsAtUtc.Value > now;
    }
}
