using iE.Core.Reports;

namespace iE.Api.Auth;

public enum EntitlementSeatLimitDecision
{
    Allowed,
    Denied
}

public sealed record EntitlementSeatLimitResult(EntitlementSeatLimitDecision Decision, string ReasonCode, int? ActiveSeatCount = null, int? LimitValue = null)
{
    public bool Allowed => Decision == EntitlementSeatLimitDecision.Allowed;
    public static EntitlementSeatLimitResult Allow(string reasonCode, int? activeSeatCount = null, int? limitValue = null) => new(EntitlementSeatLimitDecision.Allowed, reasonCode, activeSeatCount, limitValue);
    public static EntitlementSeatLimitResult Deny(string reasonCode, int? activeSeatCount = null, int? limitValue = null) => new(EntitlementSeatLimitDecision.Denied, reasonCode, activeSeatCount, limitValue);
}

public sealed class EntitlementSeatLimitGuard(IConfiguration configuration, EntitlementGuard entitlementGuard, ISubscriptionSeatUsageService subscriptionSeatUsageService)
{
    public async Task<EntitlementSeatLimitResult> CheckMaxUsersAsync(string? clientOrganizationId = null, CancellationToken cancellationToken = default)
    {
        if (!StartupConfiguration.IsSubscriptionEnforcementEnabled(configuration))
        {
            return EntitlementSeatLimitResult.Allow("subscription_enforcement_disabled");
        }

        var entitlement = await entitlementGuard.CheckAsync(EntitlementKeys.MaxUsers, clientOrganizationId, cancellationToken: cancellationToken);
        if (!entitlement.Allowed)
        {
            return entitlement.ReasonCode switch
            {
                "tenant_context_missing" => EntitlementSeatLimitResult.Deny("tenant_context_missing"),
                "subscription_unavailable" => EntitlementSeatLimitResult.Deny("subscription_unavailable"),
                "entitlement_unavailable" => EntitlementSeatLimitResult.Deny("entitlement_unavailable"),
                "entitlement_disabled" => EntitlementSeatLimitResult.Deny("entitlement_disabled"),
                _ => EntitlementSeatLimitResult.Deny("seat_limit_unavailable")
            };
        }

        var usage = await subscriptionSeatUsageService.GetUsageAsync(clientOrganizationId, cancellationToken: cancellationToken);
        if (usage is null) return EntitlementSeatLimitResult.Deny("tenant_context_missing");

        if (!entitlement.LimitValue.HasValue)
        {
            return EntitlementSeatLimitResult.Allow("entitlement_allowed", usage.ConsumedSeatCount, null);
        }

        return usage.ConsumedSeatCount >= entitlement.LimitValue.Value
            ? EntitlementSeatLimitResult.Deny("seat_limit_exceeded", usage.ConsumedSeatCount, entitlement.LimitValue)
            : EntitlementSeatLimitResult.Allow("entitlement_allowed", usage.ConsumedSeatCount, entitlement.LimitValue);
    }
}
