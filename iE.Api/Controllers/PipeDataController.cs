using Microsoft.AspNetCore.Mvc;
using iE.Core.Mechanical.PipeData;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/mechanical/pipe-data")]
    public class PipeDataController : ControllerBase
    {
        [HttpPost("lookup")]
        public ActionResult<PipeDataResult> Lookup([FromBody] PipeDataInput input)
        {
            try
            {
                var service = new PipeDataService();
                var result = service.Lookup(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("nps")]
        public ActionResult<List<string>> GetNpsList()
        {
            var service = new PipeDataService();
            return Ok(service.GetNpsList());
        }

        [HttpGet("schedules/{nps}")]
        public ActionResult<List<string>> GetSchedulesForNps(string nps)
        {
            try
            {
                var service = new PipeDataService();
                return Ok(service.GetSchedulesForNps(nps));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}