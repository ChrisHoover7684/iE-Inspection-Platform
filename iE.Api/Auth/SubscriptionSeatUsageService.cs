using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public sealed record SubscriptionSeatUsageSnapshot(string TenantId, int ConsumedSeatCount, int ActiveSeatCount, int InvitedSeatCount);

public interface ISubscriptionSeatUsageService
{
    Task<SubscriptionSeatUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
}

public sealed class SubscriptionSeatUsageService(IConfiguration configuration, ITenantContextAccessor tenantContextAccessor, InspectionReportsDbContext dbContext) : ISubscriptionSeatUsageService
{
    public async Task<SubscriptionSeatUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        var authEnabled = configuration.GetValue<bool>("Authentication:Enabled");
        var resolvedTenantId = ResolveTenantId(authEnabled, clientOrganizationId, trustedInternalRequest);
        if (string.IsNullOrWhiteSpace(resolvedTenantId))
        {
            return null;
        }

        // MVP seat counting rule: active + invited consume seats; disabled + removed do not.
        var seatRows = await dbContext.ClientOrganizationUsers
            .AsNoTracking()
            .Where(u => u.ClientOrganizationId == resolvedTenantId)
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Active = g.Count(u => u.Status == ClientOrganizationUserStatuses.Active),
                Invited = g.Count(u => u.Status == ClientOrganizationUserStatuses.Invited)
            })
            .FirstOrDefaultAsync(cancellationToken);

        var activeCount = seatRows?.Active ?? 0;
        var invitedCount = seatRows?.Invited ?? 0;

        return new SubscriptionSeatUsageSnapshot(resolvedTenantId, activeCount + invitedCount, activeCount, invitedCount);
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
}
