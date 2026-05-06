using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class EntitlementServiceTests
{
    [Fact]
    public async Task ActiveSubscription_EnabledEntitlement_Allows() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.True(r.Allowed); }

    [Fact]
    public async Task ActiveSubscription_DisabledEntitlement_Denies() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, false); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Fact]
    public async Task MissingSubscription_Denies() { var svc = Build(true, out _, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Theory]
    [InlineData(SubscriptionStatuses.Suspended)]
    [InlineData(SubscriptionStatuses.Canceled)]
    [InlineData(SubscriptionStatuses.PastDue)]
    public async Task InactiveStatuses_Deny(string status) { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", status, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Fact]
    public async Task TrialingWithValidTrialEnd_Allows() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Trialing, EntitlementKeys.ReportsCreate, true, DateTime.UtcNow.AddDays(1)); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.True(r.Allowed); }

    [Fact]
    public async Task TrialingExpired_Denies() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Trialing, EntitlementKeys.ReportsCreate, true, DateTime.UtcNow.AddDays(-1)); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Fact]
    public async Task LimitValue_Returned() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.MaxUsers, true, null, 15); var r = await svc.CheckAsync(EntitlementKeys.MaxUsers); Assert.Equal(15, r.LimitValue); }

    [Fact]
    public async Task AuthEnabled_ScopesToCurrentTenant() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "22222222-2222-2222-2222-222222222222", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Fact]
    public async Task AuthEnabled_MissingTenant_Denies() { var svc = Build(true, out var db, out _); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.False(r.Allowed); }

    [Fact]
    public async Task AuthDisabled_ExplicitTenant_Allowed() { var svc = Build(false, out var db, out _); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate, "11111111-1111-1111-1111-111111111111"); Assert.True(r.Allowed); }

    [Fact]
    public async Task Result_DoesNotExposeMetadataOrSecrets() { var svc = Build(true, out var db, out var tenant); tenant.Current = Tenant("11111111-1111-1111-1111-111111111111"); Seed(db, "11111111-1111-1111-1111-111111111111", SubscriptionStatuses.Active, EntitlementKeys.ReportsCreate, true); var r = await svc.CheckAsync(EntitlementKeys.ReportsCreate); Assert.DoesNotContain("token", r.ReasonCode, StringComparison.OrdinalIgnoreCase); }

    private static EntitlementService Build(bool authEnabled, out InspectionReportsDbContext db, out ITenantContextAccessor accessor)
    {
        db = new InspectionReportsDbContext(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        accessor = new TenantContextAccessor();
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        return new EntitlementService(cfg, accessor, db);
    }

    private static TenantContext Tenant(string id) => new() { IsAuthenticated = true, ClientOrganizationId = Guid.Parse(id) };

    private static void Seed(InspectionReportsDbContext db, string orgId, string status, string key, bool enabled, DateTime? trialEndsAt = null, int? limit = null)
    {
        var planId = Guid.NewGuid().ToString("N");
        db.SubscriptionPlans.Add(new SubscriptionPlan { Id = planId, Name = "Dev", PlanCode = $"dev-{planId[..8]}", MetadataJson = "{\"note\":\"non-sensitive\"}" });
        db.ClientSubscriptions.Add(new ClientSubscription { Id = Guid.NewGuid().ToString("N"), ClientOrganizationId = orgId, PlanId = planId, Status = status, StartsAtUtc = DateTime.UtcNow.AddDays(-2), TrialEndsAtUtc = trialEndsAt });
        db.SubscriptionEntitlements.Add(new SubscriptionEntitlement { Id = Guid.NewGuid().ToString("N"), PlanId = planId, EntitlementKey = key, Enabled = enabled, LimitValue = limit });
        db.SaveChanges();
    }
}
