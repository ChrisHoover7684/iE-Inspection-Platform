using iE.Core.Reports;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportingController : ControllerBase
{
    [HttpGet("templates")]
    public ActionResult<IReadOnlyList<ReportTemplate>> GetTemplates()
    {
        return Ok(ReportingSeedData.GetTemplates());
    }

    [HttpGet("templates/{id}")]
    public ActionResult<ReportTemplate> GetTemplateById(string id)
    {
        var template = ReportingSeedData.GetTemplateById(id);
        if (template is null)
        {
            return NotFound(new { error = $"Report template '{id}' was not found." });
        }

        return Ok(template);
    }
}
