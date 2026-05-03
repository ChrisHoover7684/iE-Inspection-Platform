using iE.Core.Calculators;
using iE.Core.Calculators.Models;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers.Calculators;

[ApiController]
[Route("api/calculators/pipe-lookup")]
public class PipeLookupCalculatorController : ControllerBase
{
    [HttpGet("nps")]
    public ActionResult<IReadOnlyList<string>> GetNps()
    {
        return Ok(new PipeLookupService().GetNpsOptions());
    }

    [HttpGet("schedules")]
    public ActionResult<IReadOnlyList<string>> GetSchedules()
    {
        return Ok(new PipeLookupService().GetScheduleOptions());
    }

    [HttpGet("lookup")]
    public ActionResult<PipeLookupResult> Lookup([FromQuery] string nps, [FromQuery] string schedule)
    {
        try
        {
            return Ok(new PipeLookupService().Lookup(nps, schedule));
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
