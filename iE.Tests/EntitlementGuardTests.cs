using iE.Api.Auth;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class EntitlementGuardTests
{
    [Fact]
    public async Task Enforcement_Disabled_Allows()
    {
        var guard = Build(false, new EntitlementCheckResult(false, null, "subscription_unavailable"));
        var result = await guard.CheckAsync("reports.create");
        Assert.True(result.Allowed);
        Assert.Equal("subscription_enforcement_disabled", result.ReasonCode);
    }

    [Fact]
    public async Task Enforcement_Enabled_MissingSubscription_Denies()
    {
        var guard = Build(true, new EntitlementCheckResult(false, null, "subscription_unavailable"));
        var result = await guard.CheckAsync("reports.create");
        Assert.False(result.Allowed);
        Assert.Equal("subscription_unavailable", result.ReasonCode);
    }

    [Fact]
    public async Task Enforcement_Enabled_AllowedEntitlement_Allows()
    {
        var guard = Build(true, new EntitlementCheckResult(true, null, "allowed"));
        var result = await guard.CheckAsync("reports.create");
        Assert.True(result.Allowed);
        Assert.Equal("entitlement_allowed", result.ReasonCode);
    }

    [Fact]
    public async Task Enforcement_Enabled_UnknownReasonCode_MapsToEntitlementUnavailable()
    {
        var guard = Build(true, EntitlementCheckResult.Denied("provider_raw_json:plan_123"));
        var result = await guard.CheckAsync("reports.create");

        Assert.False(result.Allowed);
        Assert.Equal("entitlement_unavailable", result.ReasonCode);
        Assert.DoesNotContain("plan_", result.ReasonCode, StringComparison.OrdinalIgnoreCase);
    }

    private static EntitlementGuard Build(bool enforcement, EntitlementCheckResult serviceResult)
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Subscriptions:EnforcementEnabled"] = enforcement.ToString().ToLowerInvariant()
        }).Build();
        return new EntitlementGuard(cfg, new StubEntitlementService(serviceResult));
    }

    private sealed class StubEntitlementService(EntitlementCheckResult result) : IEntitlementService
    {
        public Task<EntitlementCheckResult> CheckAsync(string entitlementKey, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
            => Task.FromResult(result);
    }
}
