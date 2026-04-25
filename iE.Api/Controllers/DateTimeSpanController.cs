using Microsoft.AspNetCore.Mvc;
using iE.Core.Common.DateTimeSpan;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/common/date-time-span")]
    public class DateTimeSpanController : ControllerBase
    {
        [HttpPost("calculate")]
        public ActionResult<DateTimeSpanResult> Calculate([FromBody] DateTimeSpanInput input)
        {
            try
            {
                var service = new DateTimeSpanService();
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