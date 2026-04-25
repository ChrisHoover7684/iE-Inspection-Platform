using Microsoft.AspNetCore.Mvc;
using iE.Core.Mechanical.LwnData;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/mechanical/lwn")]
    public class LwnController : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<LwnResult> Calculate([FromBody] LwnInput input)
        {
            try
            {
                var service = new LwnService();
                return Ok(service.Calculate(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("sizes")]
        public ActionResult<List<string>> GetSizes()
        {
            var service = new LwnService();
            return Ok(service.GetSizes());
        }

        [HttpGet("schedules/{size}")]
        public ActionResult<List<string>> GetSchedulesForSize(string size)
        {
            var service = new LwnService();
            return Ok(service.GetSchedulesForSize(size));
        }
    }
}