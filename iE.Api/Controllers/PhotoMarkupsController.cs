using iE.Api.Contracts;
using iE.Api.Auth;
using iE.Api.Extensions;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/photos/{photoId}/markups")]
public class PhotoMarkupsController(PhotoMarkupRepository photoMarkupRepository, ReportAccessGuard reportAccessGuard, EntitlementGuard entitlementGuard, IAuditEventWriter auditEventWriter) : ControllerBase
{
    private async Task<ActionResult?> EnforcePhotoAccessAsync(string photoId, string capability, string forbiddenMessage)
    {
        var owningReport = reportAccessGuard.GetOwningReportByPhotoId(photoId);
        if (owningReport is null)
        {
            return this.NotFoundError($"Photo '{photoId}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(owningReport, capability);
        if (access == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope" }); return this.NotFoundError($"Photo '{photoId}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["requiredCapability"] = capability, ["denialReason"] = "missing_capability" }); return this.ForbiddenError(forbiddenMessage); }
        if (access == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated" }); return Unauthorized(); }
        return null;
    }

    

    private async Task<ActionResult?> EnforceEntitlementAsync(string entitlementKey, string route, string? tenantId = null)
    {
        var entitlement = await entitlementGuard.CheckAsync(entitlementKey, tenantId);
        if (entitlement.Allowed) return null;

        await auditEventWriter.WriteAsync(AuditActions.EntitlementDenied, AuditResourceTypes.PhotoMarkup, metadata: new Dictionary<string, object?>
        {
            ["route"] = route,
            ["entitlementKey"] = entitlementKey,
            ["reasonCode"] = entitlement.ReasonCode
        });

        return new ObjectResult(this.CreateError("entitlement_required", "This subscription does not include this operation.")) { StatusCode = StatusCodes.Status403Forbidden };
    }
[HttpPost]
    public async Task<ActionResult<PhotoMarkup>> Create(string photoId, [FromBody] CreatePhotoMarkupRequest request)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        var accessResult = await EnforcePhotoAccessAsync(photoId, AuthCapabilities.PhotosWrite, "Insufficient capability to update photo markup.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        var tenantId = reportAccessGuard.GetOwningReportByPhotoId(photoId)?.ClientOrganizationId;
        var entitlementResult = await EnforceEntitlementAsync(EntitlementKeys.PhotosMarkup, "CreatePhotoMarkup", tenantId);
        if (entitlementResult is not null)
        {
            return entitlementResult;
        }

        try
        {
            var created = photoMarkupRepository.Create(photoId, request.MarkupJson);
            await auditEventWriter.WriteAsync("PhotoMarkupCreated", "PhotoMarkup", created.Id, "Success", metadata: new Dictionary<string, object?> { ["photoId"] = photoId });
            return Created($"/api/photos/{photoId}/markups/{created.Id}", created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotoMarkup>>> Get(string photoId)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        var accessResult = await EnforcePhotoAccessAsync(photoId, AuthCapabilities.PhotosRead, "Insufficient capability to read photo markup.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        return Ok(photoMarkupRepository.GetByPhotoId(photoId));
    }
}
