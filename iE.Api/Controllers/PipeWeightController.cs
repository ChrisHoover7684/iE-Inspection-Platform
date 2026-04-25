using Microsoft.AspNetCore.Mvc;
using iE.Core.Mechanical.PipeWeight;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/mechanical/pipe-weight")]
    public class PipeWeightController : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<PipeWeightResult> Calculate([FromBody] PipeWeightInput input)
        {
            try
            {
                var service = new PipeWeightService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
    }
}