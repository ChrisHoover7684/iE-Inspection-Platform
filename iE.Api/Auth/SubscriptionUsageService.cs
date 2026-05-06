using iE.Api.Tenancy;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public sealed record SubscriptionUsageSnapshot(string TenantId, int ActiveReportCount);

public interface ISubscriptionUsageService
{
    Task<SubscriptionUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default);
}

public sealed class SubscriptionUsageService(IConfiguration configuration, ITenantContextAccessor tenantContextAccessor, InspectionReportsDbContext dbContext) : ISubscriptionUsageService
{
    public async Task<SubscriptionUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
    {
        var authEnabled = configuration.GetValue<bool>("Authentication:Enabled");
        var resolvedTenantId = ResolveTenantId(authEnabled, clientOrganizationId, trustedInternalRequest);
        if (string.IsNullOrWhiteSpace(resolvedTenantId))
        {
            return null;
        }

        // MVP: Active reports are all non-Final statuses.
        var activeCount = await dbContext.InspectionReports
            .AsNoTracking()
            .Where(r => r.ClientOrganizationId == resolvedTenantId)
            .Where(r => r.Status != InspectionReportStatuses.Final)
            .CountAsync(cancellationToken);

        return new SubscriptionUsageSnapshot(resolvedTenantId, activeCount);
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
