using Microsoft.AspNetCore.Mvc;

using iE.Core.Common.UnitConversions;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/common/converter")]
    public class ConverterController : ControllerBase
    {
        [HttpPost("convert")]
        public ActionResult<ConverterResult> Convert([FromBody] ConverterInput input)
        {
            try
            {
                var service = new ConverterService();
                var result = service.Convert(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}