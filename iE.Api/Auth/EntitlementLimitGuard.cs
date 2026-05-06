using iE.Core.Reports;

namespace iE.Api.Auth;

public enum EntitlementLimitDecision
{
    Allowed,
    Denied
}

public sealed record EntitlementLimitResult(EntitlementLimitDecision Decision, string ReasonCode, int? ActiveCount = null, int? LimitValue = null)
{
    public bool Allowed => Decision == EntitlementLimitDecision.Allowed;
    public static EntitlementLimitResult Allow(string reasonCode, int? activeCount = null, int? limitValue = null) => new(EntitlementLimitDecision.Allowed, reasonCode, activeCount, limitValue);
    public static EntitlementLimitResult Deny(string reasonCode, int? activeCount = null, int? limitValue = null) => new(EntitlementLimitDecision.Denied, reasonCode, activeCount, limitValue);
}

public sealed class EntitlementLimitGuard(IConfiguration configuration, EntitlementGuard entitlementGuard, ISubscriptionUsageService subscriptionUsageService)
{
    public async Task<EntitlementLimitResult> CheckMaxActiveReportsAsync(string? clientOrganizationId = null, CancellationToken cancellationToken = default)
    {
        if (!StartupConfiguration.IsSubscriptionEnforcementEnabled(configuration))
        {
            return EntitlementLimitResult.Allow("subscription_enforcement_disabled");
        }

        var entitlement = await entitlementGuard.CheckAsync(EntitlementKeys.MaxActiveReports, clientOrganizationId, cancellationToken: cancellationToken);
        if (!entitlement.Allowed)
        {
            return entitlement.ReasonCode switch
            {
                "tenant_context_missing" => EntitlementLimitResult.Deny("tenant_context_missing"),
                "subscription_unavailable" => EntitlementLimitResult.Deny("subscription_unavailable"),
                "entitlement_unavailable" => EntitlementLimitResult.Deny("entitlement_unavailable"),
                "entitlement_disabled" => EntitlementLimitResult.Deny("entitlement_disabled"),
                _ => EntitlementLimitResult.Deny("limit_unavailable")
            };
        }

        var usage = await subscriptionUsageService.GetUsageAsync(clientOrganizationId, cancellationToken: cancellationToken);
        if (usage is null) return EntitlementLimitResult.Deny("tenant_context_missing");

        if (!entitlement.LimitValue.HasValue)
        {
            return EntitlementLimitResult.Allow("entitlement_allowed", usage.ActiveReportCount, null);
        }

        return usage.ActiveReportCount >= entitlement.LimitValue.Value
            ? EntitlementLimitResult.Deny("limit_exceeded", usage.ActiveReportCount, entitlement.LimitValue)
            : EntitlementLimitResult.Allow("entitlement_allowed", usage.ActiveReportCount, entitlement.LimitValue);
    }
}
