using iE.Api.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/nde-reports")]
public sealed class NdeReportsController(INdeReportDraftService ndeReportDraftService, INdeLogReadModelService ndeLogReadModelService) : ControllerBase
{
    [HttpGet("{ndeRequestId}/draft")]
    public ActionResult<NdeReportDraftReadModel> GetDraft(string ndeRequestId)
    {
        if (!ndeLogReadModelService.Exists(ndeRequestId)) return NotFound();
        var draft = ndeReportDraftService.GetDraft(ndeRequestId);
        return draft is null ? NotFound() : Ok(draft);
    }

    [HttpGet("drafts")]
    public ActionResult<IReadOnlyList<NdeReportDraftReadModel>> GetDrafts() => Ok(ndeReportDraftService.GetDrafts());

    [HttpPut("{ndeRequestId}/draft")]
    public ActionResult<NdeReportDraftReadModel> SaveDraft(string ndeRequestId, [FromBody] NdeReportDraftSaveRequest request)
    {
        var result = ndeReportDraftService.SaveDraft(ndeRequestId, request);
        if (!result.Success || result.Draft is null)
        {
            return result.ReasonCode == "not_found" ? NotFound() : BadRequest(new { reasonCode = result.ReasonCode });
        }

        return Ok(result.Draft);
    }

    [HttpPost("{ndeRequestId}/complete")]
    public ActionResult<NdeReportDraftReadModel> CompleteDraft(string ndeRequestId, [FromBody] NdeReportDraftSaveRequest request)
    {
        var result = ndeReportDraftService.CompleteDraft(ndeRequestId, request);
        if (!result.Success || result.Draft is null)
        {
            return result.ReasonCode == "not_found" ? NotFound() : BadRequest(new { reasonCode = result.ReasonCode });
        }

        return Ok(result.Draft);
    }
}
