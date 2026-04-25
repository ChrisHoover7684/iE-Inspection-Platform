using Microsoft.AspNetCore.Mvc;
using iE.Core.Mechanical.BoltData;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/mechanical/bolt-data")]
    public class BoltDataController : ControllerBase
    {
        [HttpPost("lookup")]
        public ActionResult<BoltDataResult> Lookup([FromBody] BoltDataInput input)
        {
            try
            {
                var service = new BoltDataService();
                var result = service.Lookup(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("sizes")]
        public ActionResult<List<string>> GetFlangeSizes()
        {
            var service = new BoltDataService();
            return Ok(service.GetFlangeSizes());
        }

        [HttpGet("ratings/{flangeSize}")]
        public ActionResult<List<string>> GetRatingsForSize(string flangeSize)
        {
            try
            {
                var service = new BoltDataService();
                return Ok(service.GetRatingsForSize(flangeSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}