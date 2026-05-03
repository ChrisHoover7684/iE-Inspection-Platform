using iE.Core.Calculators;
using iE.Core.Calculators.Models;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers.Calculators;

[ApiController]
[Route("api/calculators/corrosion-rate")]
public class CorrosionRateCalculatorController : ControllerBase
{
    [HttpPost("calculate")]
    public ActionResult<CorrosionRateCalculationResult> Calculate([FromBody] CorrosionRateCalculationInput input)
    {
        try
        {
            var result = new CorrosionRateService().Calculate(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
