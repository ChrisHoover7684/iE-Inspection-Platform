using iE.Api.Extensions;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Auth;

public sealed class ReportReferenceGuard(IConfiguration configuration, InspectionReportsDbContext dbContext, IAuditEventWriter auditEventWriter)
{
    private readonly bool _authenticationEnabled = StartupConfiguration.IsAuthenticationEnabled(configuration);

    public async Task<ActionResult?> ValidateForPersistedWriteAsync(ControllerBase controller, InspectionReport report, string route)
    {
        if (!_authenticationEnabled)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(report.ClientOrganizationId))
        {
            return await FailAsync(controller, report, route, "missing_client_organization", 400);
        }

        if (string.IsNullOrWhiteSpace(report.FacilityId))
        {
            return await FailAsync(controller, report, route, "missing_facility", 400);
        }

        var clientOrganization = dbContext.ClientOrganizations.FirstOrDefault(c => c.Id == report.ClientOrganizationId);
        if (clientOrganization is null)
        {
            return await FailAsync(controller, report, route, "client_organization_not_found", 404);
        }
        if (!clientOrganization.IsActive)
        {
            return await FailAsync(controller, report, route, "client_organization_inactive", 400);
        }

        var facility = dbContext.Facilities.FirstOrDefault(f => f.Id == report.FacilityId);
        if (facility is null)
        {
            return await FailAsync(controller, report, route, "facility_not_found", 404);
        }
        if (!string.Equals(facility.ClientOrganizationId, report.ClientOrganizationId, StringComparison.OrdinalIgnoreCase))
        {
            return await FailAsync(controller, report, route, "facility_client_mismatch", 404);
        }
        if (!facility.IsActive)
        {
            return await FailAsync(controller, report, route, "facility_inactive", 400);
        }

        ProcessUnit? processUnit = null;
        if (!string.IsNullOrWhiteSpace(report.ProcessUnitId))
        {
            processUnit = dbContext.ProcessUnits.FirstOrDefault(pu => pu.Id == report.ProcessUnitId);
            if (processUnit is null)
            {
                return await FailAsync(controller, report, route, "process_unit_not_found", 404);
            }
            if (!string.Equals(processUnit.FacilityId, report.FacilityId, StringComparison.OrdinalIgnoreCase))
            {
                return await FailAsync(controller, report, route, "process_unit_facility_mismatch", 404);
            }
            if (!processUnit.IsActive)
            {
                return await FailAsync(controller, report, route, "process_unit_inactive", 400);
            }
        }

        if (!string.IsNullOrWhiteSpace(report.AssetId))
        {
            var asset = dbContext.Assets.FirstOrDefault(a => a.Id == report.AssetId);
            if (asset is null)
            {
                return await FailAsync(controller, report, route, "asset_not_found", 404);
            }
            if (!string.Equals(asset.FacilityId, report.FacilityId, StringComparison.OrdinalIgnoreCase))
            {
                return await FailAsync(controller, report, route, "asset_facility_mismatch", 404);
            }
            if (!asset.IsActive)
            {
                return await FailAsync(controller, report, route, "asset_inactive", 400);
            }
            if (processUnit is not null && !string.IsNullOrWhiteSpace(asset.ProcessUnitId) && !string.Equals(asset.ProcessUnitId, processUnit.Id, StringComparison.OrdinalIgnoreCase))
            {
                return await FailAsync(controller, report, route, "asset_process_unit_mismatch", 400);
            }
        }

        return null;
    }

    private async Task<ActionResult> FailAsync(ControllerBase controller, InspectionReport report, string route, string reasonCode, int statusCode)
    {
        await auditEventWriter.WriteAsync(AuditActions.ReportReferenceValidationFailed, AuditResourceTypes.Report, report.Id, AuditResults.Denied, report.FacilityId, new Dictionary<string, object?> { ["route"] = route, ["reasonCode"] = reasonCode });
        return statusCode == 404
            ? controller.NotFoundError("Inspection report instance was not found.")
            : controller.ValidationError("Report ownership references are invalid.");
    }
}
