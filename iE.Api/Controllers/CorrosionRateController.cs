using Microsoft.AspNetCore.Mvc;
using iE.Core.Inspection.CorrosionRate;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/inspection/corrosion-rate")]
    public class CorrosionRateController : ControllerBase
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
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}