using iE.Api.Auth;
using iE.Tests.TestDoubles;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class EntitlementSeatLimitGuardTests
{
    [Fact]
    public async Task EntitlementKey_MaxUsers_Exists()
    {
        Assert.Equal("max.users", iE.Core.Reports.EntitlementKeys.MaxUsers);
        await Task.CompletedTask;
    }

    [Fact]
    public async Task EnforcementDisabled_Allows()
    {
        var g = Build(false, EntitlementCheckResult.Denied("entitlement_disabled"), new SubscriptionSeatUsageSnapshot("t1", 999, 999, 0));
        var r = await g.CheckMaxUsersAsync("t1");
        Assert.True(r.Allowed);
        Assert.Equal("subscription_enforcement_disabled", r.ReasonCode);
    }

    [Theory]
    [InlineData(3, 5, true)]
    [InlineData(5, 5, false)]
    [InlineData(6, 5, false)]
    public async Task LimitChecks_Work(int seatCount, int limit, bool allowed)
    {
        var g = Build(true, EntitlementCheckResult.AllowedWithLimit(limit), new SubscriptionSeatUsageSnapshot("t1", seatCount, seatCount, 0));
        var r = await g.CheckMaxUsersAsync("t1");
        Assert.Equal(allowed, r.Allowed);
        Assert.Equal(allowed ? "entitlement_allowed" : "seat_limit_exceeded", r.ReasonCode);
    }

    [Fact]
    public async Task NullLimit_AllowsUnlimited()
    {
        var g = Build(true, EntitlementCheckResult.AllowedWithLimit(null), new SubscriptionSeatUsageSnapshot("t1", 999, 999, 0));
        var r = await g.CheckMaxUsersAsync("t1");
        Assert.True(r.Allowed);
        Assert.Equal("entitlement_allowed", r.ReasonCode);
    }

    [Fact]
    public async Task MissingEntitlement_Denies()
    {
        var g = Build(true, EntitlementCheckResult.Denied("entitlement_disabled"), new SubscriptionSeatUsageSnapshot("t1", 1, 1, 0));
        var r = await g.CheckMaxUsersAsync("t1");
        Assert.False(r.Allowed);
        Assert.Equal("entitlement_disabled", r.ReasonCode);
    }

    [Fact]
    public async Task MissingTenantContext_Denies()
    {
        var g = Build(true, EntitlementCheckResult.AllowedWithLimit(10), null);
        var r = await g.CheckMaxUsersAsync();
        Assert.False(r.Allowed);
        Assert.Equal("tenant_context_missing", r.ReasonCode);
    }

    [Fact]
    public async Task UnknownReasonCode_NormalizesSafely()
    {
        var g = Build(true, EntitlementCheckResult.Denied("provider_raw.plan.token"), new SubscriptionSeatUsageSnapshot("t1", 1, 1, 0));
        var r = await g.CheckMaxUsersAsync("t1");
        Assert.False(r.Allowed);
        Assert.Equal("entitlement_unavailable", r.ReasonCode);
        Assert.DoesNotContain("token", r.ReasonCode, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("plan", r.ReasonCode, StringComparison.OrdinalIgnoreCase);
    }

    static EntitlementSeatLimitGuard Build(bool enabled, EntitlementCheckResult result, SubscriptionSeatUsageSnapshot? usage)
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = enabled.ToString().ToLowerInvariant() }).Build();
        var e = new EntitlementGuard(cfg, new StubEntitlementService(result));
        return new EntitlementSeatLimitGuard(cfg, e, new StubSubscriptionSeatUsageService(usage));
    }

    private sealed class StubSubscriptionSeatUsageService(SubscriptionSeatUsageSnapshot? usage) : ISubscriptionSeatUsageService
    {
        public SubscriptionSeatUsageSnapshot? Usage { get; set; } = usage;
        public Task<SubscriptionSeatUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default) => Task.FromResult(Usage);
    }
}
