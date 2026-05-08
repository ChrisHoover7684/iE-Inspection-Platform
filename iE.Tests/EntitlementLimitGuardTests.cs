using iE.Api.Auth;
using iE.Tests.TestDoubles;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class EntitlementLimitGuardTests
{
    [Fact]
    public async Task EnforcementDisabled_Allows()
    {
        var g = Build(false, EntitlementCheckResult.Denied("entitlement_disabled"), new SubscriptionUsageSnapshot("t1", 99));
        var r = await g.CheckMaxActiveReportsAsync("t1");
        Assert.True(r.Allowed);
    }

    [Theory]
    [InlineData(null, 100, true)]
    [InlineData(5, 4, true)]
    [InlineData(5, 5, false)]
    [InlineData(5, 6, false)]
    public async Task LimitBehaviors(int? limit, int count, bool allowed)
    {
        var g = Build(true, EntitlementCheckResult.AllowedWithLimit(limit), new SubscriptionUsageSnapshot("t1", count));
        var r = await g.CheckMaxActiveReportsAsync("t1");
        Assert.Equal(allowed, r.Allowed);
    }

    [Fact]
    public async Task MissingEntitlement_Denies()
    {
        var g = Build(true, EntitlementCheckResult.Denied("entitlement_disabled"), new SubscriptionUsageSnapshot("t1", 1));
        var r = await g.CheckMaxActiveReportsAsync("t1");
        Assert.False(r.Allowed);
    }

    [Fact]
    public async Task UnknownEntitlementReasonCode_MapsToLimitUnavailable()
    {
        var g = Build(true, EntitlementCheckResult.Denied("provider_payload.subscription.plan"), new SubscriptionUsageSnapshot("t1", 1));
        var r = await g.CheckMaxActiveReportsAsync("t1");

        Assert.False(r.Allowed);
        Assert.Equal("entitlement_unavailable", r.ReasonCode);
        Assert.DoesNotContain("plan", r.ReasonCode, StringComparison.OrdinalIgnoreCase);
    }

    static EntitlementLimitGuard Build(bool enabled, EntitlementCheckResult result, SubscriptionUsageSnapshot? usage)
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = enabled.ToString().ToLowerInvariant() }).Build();
        var e = new EntitlementGuard(cfg, new StubEntitlementService(result));
        return new EntitlementLimitGuard(cfg, e, new StubSubscriptionUsageService(usage));
    }
}
