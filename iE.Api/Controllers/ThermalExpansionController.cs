using Microsoft.AspNetCore.Mvc;
using iE.Core.Mechanical.ThermalExpansion;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/mechanical/thermal-expansion")]
    public class ThermalExpansionController : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<ThermalExpansionResult> Calculate([FromBody] ThermalExpansionInput input)
        {
            try
            {
                var service = new ThermalExpansionService();
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