using Microsoft.AspNetCore.Mvc;
using iE.Core.Inspection.CorrosionRate;
using iE.Api.Extensions;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/inspection/corrosion-rate")]
    public class CorrosionRateController(ILogger<CorrosionRateController> logger) : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<CorrosionRateResult> Calculate([FromBody] CorrosionRateInput input)
        {
            try
            {
                var service = new CorrosionRateService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                logger.LogInformation(ex, "Corrosion rate domain failure. TraceId={TraceId}", HttpContext.TraceIdentifier);
                return DomainError(ex.Message);
            }
        }
    }
}