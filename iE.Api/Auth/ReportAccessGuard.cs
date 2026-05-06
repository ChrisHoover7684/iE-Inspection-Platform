using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public enum ReportAccessDecision
{
    Allowed,
    NotFound,
    Forbidden,
    Unauthorized
}

public sealed class ReportAccessGuard(IConfiguration configuration, ITenantContextAccessor tenantContextAccessor, InspectionReportsDbContext dbContext)
{
    private readonly bool _authenticationEnabled = StartupConfiguration.IsAuthenticationEnabled(configuration);

    public bool IsAuthenticationEnabled => _authenticationEnabled;

    public ReportAccessDecision CanAccessReport(InspectionReport report, string requiredCapability)
    {
        if (!_authenticationEnabled)
        {
            return ReportAccessDecision.Allowed;
        }

        var tenant = tenantContextAccessor.Current;
        if (!tenant.IsAuthenticated || string.IsNullOrWhiteSpace(tenant.ExternalSubject) || tenant.ClientOrganizationId is null)
        {
            return ReportAccessDecision.Unauthorized;
        }

        if (!string.Equals(report.ClientOrganizationId, tenant.ClientOrganizationId.Value.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            return ReportAccessDecision.NotFound;
        }

        if (!HasFacilityAccess(report.FacilityId, tenant.ExternalSubject, report.ClientOrganizationId))
        {
            return ReportAccessDecision.NotFound;
        }

        return tenant.Capabilities.Contains(requiredCapability, StringComparer.OrdinalIgnoreCase)
            ? ReportAccessDecision.Allowed
            : ReportAccessDecision.Forbidden;
    }

    public List<InspectionReport> ScopeReports(IEnumerable<InspectionReport> reports, string requiredCapability)
    {
        if (!_authenticationEnabled)
        {
            return reports.ToList();
        }

        var tenant = tenantContextAccessor.Current;
        if (!tenant.IsAuthenticated || string.IsNullOrWhiteSpace(tenant.ExternalSubject) || tenant.ClientOrganizationId is null)
        {
            return [];
        }

        if (!tenant.Capabilities.Contains(requiredCapability, StringComparer.OrdinalIgnoreCase))
        {
            return [];
        }

        var tenantOrgId = tenant.ClientOrganizationId.Value.ToString();
        var allowedFacilities = GetAllowedFacilities(tenant.ExternalSubject, tenantOrgId);
        return reports.Where(r =>
                string.Equals(r.ClientOrganizationId, tenantOrgId, StringComparison.OrdinalIgnoreCase)
                && allowedFacilities.Contains(r.FacilityId, StringComparer.OrdinalIgnoreCase))
            .ToList();
    }

    public void ApplyCreateOwnership(InspectionReport report)
    {
        if (!_authenticationEnabled)
        {
            return;
        }

        var tenant = tenantContextAccessor.Current;
        if (tenant.ClientOrganizationId is null)
        {
            return;
        }

        report.ClientOrganizationId = tenant.ClientOrganizationId.Value.ToString();
        report.CreatedByUserId = tenant.ExternalSubject;
        report.UpdatedByUserId = tenant.ExternalSubject;
    }

    public void ApplyUpdateOwnership(InspectionReport report)
    {
        if (!_authenticationEnabled)
        {
            return;
        }

        var tenant = tenantContextAccessor.Current;
        report.UpdatedByUserId = tenant.ExternalSubject;
    }

    public InspectionReport? GetOwningReportByPhotoId(string photoId)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return null;
        }

        return dbContext.InspectionReports
            .AsNoTracking()
            .FirstOrDefault(report => report.Photos.Any(photo => photo.Id == photoId));
    }

    private bool HasFacilityAccess(string facilityId, string userId, string clientOrganizationId)
    {
        if (string.IsNullOrWhiteSpace(facilityId))
        {
            return false;
        }

        return dbContext.UserFacilityAccesses.Any(ufa =>
            ufa.IsActive
            && ufa.UserId == userId
            && ufa.ClientOrganizationId == clientOrganizationId
            && (ufa.FacilityId == null || ufa.FacilityId == facilityId));
    }

    private HashSet<string> GetAllowedFacilities(string userId, string clientOrganizationId)
    {
        var accesses = dbContext.UserFacilityAccesses
            .Where(ufa => ufa.IsActive && ufa.UserId == userId && ufa.ClientOrganizationId == clientOrganizationId)
            .Select(ufa => ufa.FacilityId)
            .ToList();

        if (accesses.Any(a => a is null))
        {
            return dbContext.Facilities.Where(f => f.ClientOrganizationId == clientOrganizationId).Select(f => f.Id)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        return accesses.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a!).ToHashSet(StringComparer.OrdinalIgnoreCase);
    }
}
