using iE.Api;
using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class StressTestSubscriptionSeedServiceTests
{
    [Fact]
    public async Task Seed_CreatesPlanSubscriptionAndRequiredEntitlements()
    {
        var db = Db();
        var service = new StressTestSubscriptionSeedService(db);
        var startsAt = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc);

        var result = await service.SeedAsync(new StressTestSubscriptionSeedOptions(true, "tenant-1", startsAt));

        Assert.True(result.Success);
        var plan = Assert.Single(db.SubscriptionPlans.Where(p => p.PlanCode == StressTestPlanConstants.PlanCode));
        Assert.Equal(StressTestPlanConstants.DisplayName, plan.Name);
        var subscription = Assert.Single(db.ClientSubscriptions.Where(s => s.ClientOrganizationId == "tenant-1"));
        Assert.Equal(SubscriptionStatuses.Trialing, subscription.Status);
        Assert.Equal(startsAt, subscription.StartsAtUtc);
        Assert.Equal(startsAt.AddDays(90), subscription.EndsAtUtc);
        Assert.Equal(startsAt.AddDays(90), subscription.TrialEndsAtUtc);

        var keys = db.SubscriptionEntitlements.Where(e => e.PlanId == plan.Id).Select(e => e.EntitlementKey).ToHashSet();
        Assert.Contains(EntitlementKeys.ReportsCreate, keys);
        Assert.Contains(EntitlementKeys.ReportsExport, keys);
        Assert.Contains(EntitlementKeys.PhotosMarkup, keys);
        Assert.Contains(EntitlementKeys.AuditQuery, keys);
        Assert.Contains(EntitlementKeys.MaxActiveReports, keys);
        Assert.Contains(EntitlementKeys.MaxUsers, keys);
        Assert.All(db.SubscriptionEntitlements.Where(e => e.PlanId == plan.Id), e => Assert.Null(e.LimitValue));
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        var db = Db();
        var service = new StressTestSubscriptionSeedService(db);
        var options = new StressTestSubscriptionSeedOptions(true, "tenant-1", new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc));

        await service.SeedAsync(options);
        await service.SeedAsync(options);

        Assert.Single(db.SubscriptionPlans.Where(p => p.PlanCode == StressTestPlanConstants.PlanCode));
        Assert.Single(db.ClientSubscriptions.Where(s => s.ClientOrganizationId == "tenant-1"));
        Assert.Equal(6, db.SubscriptionEntitlements.Count());
    }

    [Fact]
    public async Task Seed_DoesNotStoreBillingProviderIdentifiersOrSecrets()
    {
        var db = Db();
        var service = new StressTestSubscriptionSeedService(db);

        await service.SeedAsync(new StressTestSubscriptionSeedOptions(true, "tenant-1", new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc)));

        var plan = Assert.Single(db.SubscriptionPlans);
        Assert.True(string.IsNullOrWhiteSpace(plan.MetadataJson));

        Assert.All(db.SubscriptionEntitlements, e =>
        {
            Assert.DoesNotContain("stripe", e.EntitlementKey, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("marketplace", e.EntitlementKey, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("token", e.EntitlementKey, StringComparison.OrdinalIgnoreCase);
        });
    }

    [Fact]
    public async Task InvalidConfig_DoesNotSeed()
    {
        var db = Db();
        var service = new StressTestSubscriptionSeedService(db);

        Assert.False((await service.SeedAsync(new StressTestSubscriptionSeedOptions(true, null, null))).Success);
        Assert.False((await service.SeedAsync(new StressTestSubscriptionSeedOptions(true, "tenant-1", null, 0))).Success);
        Assert.False((await service.SeedAsync(new StressTestSubscriptionSeedOptions(false, "tenant-1", null))).Seeded);
        Assert.Empty(db.SubscriptionPlans);
    }

    [Fact]
    public async Task EnforcementEnabled_WithUnlimitedEntitlements_AllowsGuards()
    {
        var db = Db();
        await new StressTestSubscriptionSeedService(db).SeedAsync(new StressTestSubscriptionSeedOptions(true, "tenant-1", DateTime.UtcNow.AddDays(-1)));
        db.ClientOrganizationUsers.AddRange(
            new ClientOrganizationUser { Id = "u1", ClientOrganizationId = "tenant-1", Status = ClientOrganizationUserStatuses.Active, ExternalSubject = "sub-u1", CreatedAtUtc = DateTime.UtcNow },
            new ClientOrganizationUser { Id = "u2", ClientOrganizationId = "tenant-1", Status = ClientOrganizationUserStatuses.Invited, ExternalSubject = "sub-u2", CreatedAtUtc = DateTime.UtcNow });
        db.InspectionReports.Add(new InspectionReport { Id = "r1", TemplateId = "api-570-piping-external", ClientOrganizationId = "tenant-1", FacilityId = "f1", Status = InspectionReportStatuses.InReview, ReportNumber = "R-STRESS-1", EquipmentTag = "stress-test-asset", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        await db.SaveChangesAsync();

        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = "true", ["Authentication:Enabled"] = "false" }).Build();
        var tenant = new TenantContextAccessor();
        var entSvc = new EntitlementService(cfg, tenant, db);
        var entGuard = new EntitlementGuard(cfg, entSvc);
        var seat = new EntitlementSeatLimitGuard(cfg, entGuard, new SubscriptionSeatUsageService(cfg, tenant, db));
        var active = new EntitlementLimitGuard(cfg, entGuard, new SubscriptionUsageService(cfg, tenant, db));

        Assert.True((await seat.CheckMaxUsersAsync("tenant-1", default)).Allowed);
        Assert.True((await active.CheckMaxActiveReportsAsync("tenant-1", default)).Allowed);
    }

    [Fact]
    public void StartupStressConfig_FailsSafeParsing()
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["StressTestSeed:Enabled"] = "true",
            ["StressTestSeed:ClientOrganizationId"] = "tenant-1",
            ["StressTestSeed:DurationDays"] = "bad"
        }).Build();

        var opts = StartupConfiguration.GetStressTestSeedOptions(cfg);
        Assert.True(opts.Enabled);
        Assert.Equal("tenant-1", opts.ClientOrganizationId);
        Assert.Equal(StressTestPlanConstants.IntendedDurationDays, opts.DurationDays);
    }

    [Fact]
    public void StartupStressConfig_InvalidEnabledValue_FailsSafeToDisabled()
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["StressTestSeed:Enabled"] = "not-a-bool"
        }).Build();

        var opts = StartupConfiguration.GetStressTestSeedOptions(cfg);
        Assert.False(opts.Enabled);
    }

    static InspectionReportsDbContext Db() => new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
}
