using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public static class StressTestPlanConstants
{
    public const string PlanCode = "company_stress_test_90d";
    public const string DisplayName = "Company Stress Test - 90 Day";
    public const int IntendedDurationDays = 90;
}

public sealed record StressTestSubscriptionSeedOptions(
    bool Enabled,
    string? ClientOrganizationId,
    DateTime? StartsAtUtc,
    int DurationDays = StressTestPlanConstants.IntendedDurationDays);

public sealed record StressTestSubscriptionSeedResult(bool Seeded, bool Success, string ReasonCode);

public interface IStressTestSubscriptionSeedService
{
    Task<StressTestSubscriptionSeedResult> SeedAsync(StressTestSubscriptionSeedOptions options, CancellationToken cancellationToken = default);
}

public sealed class StressTestSubscriptionSeedService(InspectionReportsDbContext dbContext) : IStressTestSubscriptionSeedService
{
    private static readonly string[] RequiredEntitlements =
    [
        EntitlementKeys.ReportsCreate,
        EntitlementKeys.ReportsExport,
        EntitlementKeys.PhotosMarkup,
        EntitlementKeys.AuditQuery,
        EntitlementKeys.MaxActiveReports,
        EntitlementKeys.MaxUsers
    ];

    public async Task<StressTestSubscriptionSeedResult> SeedAsync(StressTestSubscriptionSeedOptions options, CancellationToken cancellationToken = default)
    {
        if (!options.Enabled) return new(false, true, "disabled");
        if (string.IsNullOrWhiteSpace(options.ClientOrganizationId)) return new(false, false, "invalid_client_organization_id");
        if (options.DurationDays <= 0) return new(false, false, "invalid_duration_days");

        var startsAtUtc = options.StartsAtUtc ?? DateTime.UtcNow;
        if (startsAtUtc.Kind != DateTimeKind.Utc) startsAtUtc = DateTime.SpecifyKind(startsAtUtc, DateTimeKind.Utc);
        var endsAtUtc = startsAtUtc.AddDays(options.DurationDays);

        var now = DateTime.UtcNow;
        var plan = await dbContext.SubscriptionPlans.FirstOrDefaultAsync(p => p.PlanCode == StressTestPlanConstants.PlanCode, cancellationToken);
        if (plan is null)
        {
            plan = new SubscriptionPlan
            {
                Id = Guid.NewGuid().ToString("N"),
                PlanCode = StressTestPlanConstants.PlanCode,
                Name = StressTestPlanConstants.DisplayName,
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            };
            dbContext.SubscriptionPlans.Add(plan);
        }
        else
        {
            plan.Name = StressTestPlanConstants.DisplayName;
            plan.IsActive = true;
            plan.UpdatedAt = now;
        }

        var subscription = await dbContext.ClientSubscriptions
            .Where(s => s.ClientOrganizationId == options.ClientOrganizationId)
            .Where(s => s.PlanId == plan.Id)
            .OrderByDescending(s => s.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription is null)
        {
            subscription = new ClientSubscription
            {
                Id = Guid.NewGuid().ToString("N"),
                ClientOrganizationId = options.ClientOrganizationId,
                PlanId = plan.Id,
                Status = SubscriptionStatuses.Trialing,
                StartsAtUtc = startsAtUtc,
                EndsAtUtc = endsAtUtc,
                TrialEndsAtUtc = endsAtUtc,
                CreatedAt = now,
                UpdatedAt = now
            };
            dbContext.ClientSubscriptions.Add(subscription);
        }
        else
        {
            subscription.Status = SubscriptionStatuses.Trialing;
            subscription.StartsAtUtc = startsAtUtc;
            subscription.EndsAtUtc = endsAtUtc;
            subscription.TrialEndsAtUtc = endsAtUtc;
            subscription.UpdatedAt = now;
        }

        foreach (var key in RequiredEntitlements)
        {
            var entitlement = await dbContext.SubscriptionEntitlements
                .FirstOrDefaultAsync(e => e.PlanId == plan.Id && e.EntitlementKey == key, cancellationToken);
            if (entitlement is null)
            {
                entitlement = new SubscriptionEntitlement
                {
                    Id = Guid.NewGuid().ToString("N"),
                    PlanId = plan.Id,
                    EntitlementKey = key,
                    Enabled = true,
                    LimitValue = null,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                dbContext.SubscriptionEntitlements.Add(entitlement);
            }
            else
            {
                entitlement.Enabled = true;
                entitlement.LimitValue = null;
                entitlement.UpdatedAt = now;
            }
        }

        var facilitiesEntitlement = await dbContext.SubscriptionEntitlements
            .FirstOrDefaultAsync(e => e.PlanId == plan.Id && e.EntitlementKey == EntitlementKeys.MaxFacilities, cancellationToken);
        if (facilitiesEntitlement is not null)
        {
            facilitiesEntitlement.Enabled = true;
            facilitiesEntitlement.LimitValue = null;
            facilitiesEntitlement.UpdatedAt = now;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return new(true, true, "seeded");
    }
}
