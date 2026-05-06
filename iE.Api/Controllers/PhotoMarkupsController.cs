using iE.Api.Contracts;
using iE.Api.Auth;
using iE.Api.Extensions;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/photos/{photoId}/markups")]
public class PhotoMarkupsController(PhotoMarkupRepository photoMarkupRepository, ReportAccessGuard reportAccessGuard, IAuditEventWriter auditEventWriter) : ControllerBase
{
    private ActionResult? EnforcePhotoAccess(string photoId, string capability, string forbiddenMessage)
    {
        var owningReport = reportAccessGuard.GetOwningReportByPhotoId(photoId);
        if (owningReport is null)
        {
            return this.NotFoundError($"Photo '{photoId}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(owningReport, capability);
        if (access == ReportAccessDecision.NotFound) { _ = auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope" }); return this.NotFoundError($"Photo '{photoId}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { _ = auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["requiredCapability"] = capability, ["denialReason"] = "missing_capability" }); return this.ForbiddenError(forbiddenMessage); }
        if (access == ReportAccessDecision.Unauthorized) { _ = auditEventWriter.WriteAsync("AuthorizationDenied", "Photo", photoId, "Denied", metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated" }); return Unauthorized(); }
        return null;
    }

    [HttpPost]
    public ActionResult<PhotoMarkup> Create(string photoId, [FromBody] CreatePhotoMarkupRequest request)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        var accessResult = EnforcePhotoAccess(photoId, AuthCapabilities.PhotosWrite, "Insufficient capability to update photo markup.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        try
        {
            var created = photoMarkupRepository.Create(photoId, request.MarkupJson);
            _ = auditEventWriter.WriteAsync("PhotoMarkupCreated", "PhotoMarkup", created.Id, "Success", metadata: new Dictionary<string, object?> { ["photoId"] = photoId });
            return Created($"/api/photos/{photoId}/markups/{created.Id}", created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public ActionResult<List<PhotoMarkup>> Get(string photoId)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        var accessResult = EnforcePhotoAccess(photoId, AuthCapabilities.PhotosRead, "Insufficient capability to read photo markup.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        return Ok(photoMarkupRepository.GetByPhotoId(photoId));
    }
}
