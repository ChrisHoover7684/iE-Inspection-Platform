using Microsoft.AspNetCore.Mvc;
using iE.Core.Inspection.StiSp001;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/inspection/sti-sp001")]
    public class StiSp001Controller : ControllerBase
    {
        private readonly StiSp001Service _service = new();

        [HttpPost("schedule")]
        public ActionResult<StiSp001ScheduleResult> CalculateSchedule([FromBody] StiSp001ScheduleInput input)
        {
            try
            {
                return Ok(_service.CalculateSchedule(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("thickness")]
        public ActionResult<StiSp001ThicknessResult> EvaluateThickness([FromBody] StiSp001ThicknessInput input)
        {
            try
            {
                return Ok(_service.EvaluateThickness(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("containment")]
        public ActionResult<StiSp001ContainmentResult> CalculateContainment([FromBody] StiSp001ContainmentInput input)
        {
            try
            {
                return Ok(_service.CalculateContainment(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
