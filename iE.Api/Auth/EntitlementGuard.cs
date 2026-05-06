using iE.Core.Reports;

namespace iE.Api.Auth;

public enum EntitlementAccessDecision
{
    Allowed,
    Denied
}

public sealed record EntitlementAccessResult(EntitlementAccessDecision Decision, string ReasonCode, int? LimitValue = null)
{
    public bool Allowed => Decision == EntitlementAccessDecision.Allowed;

    public static EntitlementAccessResult Allow(string reasonCode = "entitlement_allowed", int? limitValue = null) => new(EntitlementAccessDecision.Allowed, reasonCode, limitValue);
    public static EntitlementAccessResult Deny(string reasonCode) => new(EntitlementAccessDecision.Denied, reasonCode);
}

public sealed class EntitlementGuard(IConfiguration configuration, IEntitlementService entitlementService)
{
    public async Task<EntitlementAccessResult> CheckAsync(string entitlementKey, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        if (!StartupConfiguration.IsSubscriptionEnforcementEnabled(configuration))
        {
            return EntitlementAccessResult.Allow("subscription_enforcement_disabled");
        }

        if (string.IsNullOrWhiteSpace(entitlementKey))
        {
            return EntitlementAccessResult.Deny("invalid_entitlement_key");
        }

        var result = await entitlementService.CheckAsync(entitlementKey, clientOrganizationId, trustedInternalRequest, cancellationToken);
        if (result.Allowed)
        {
            return EntitlementAccessResult.Allow("entitlement_allowed", result.LimitValue);
        }

        return result.ReasonCode switch
        {
            "tenant_context_missing" => EntitlementAccessResult.Deny("tenant_context_missing"),
            "invalid_entitlement_key" => EntitlementAccessResult.Deny("invalid_entitlement_key"),
            "subscription_unavailable" => EntitlementAccessResult.Deny("subscription_unavailable"),
            "entitlement_unavailable" => EntitlementAccessResult.Deny("entitlement_unavailable"),
            "entitlement_disabled" => EntitlementAccessResult.Deny("entitlement_disabled"),
            _ => EntitlementAccessResult.Deny("entitlement_unavailable")
        };
    }
}
