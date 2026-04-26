using Microsoft.AspNetCore.Mvc;
using iE.Core.FlangeJointIntegrity.Models;
using iE.Core.FlangeJointIntegrity.Services;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/inspection/flange-joint-integrity")]
    public class FlangeJointIntegrityController : ControllerBase
    {
        [HttpPost("gasket-defect")]
        public ActionResult<FlangeJointReviewResult> ReviewGasketDefect([FromBody] GasketSeatingDefectInput input)
        {
            try
            {
                var service = new FlangeJointIntegrityService();
                return Ok(service.ReviewGasketDefect(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("flange-face")]
        public ActionResult<FlangeJointReviewResult> ReviewFlangeFace([FromBody] FlangeFaceConditionInput input)
        {
            try
            {
                var service = new FlangeJointIntegrityService();
                return Ok(service.ReviewFlangeFace(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("bolt-assembly")]
        public ActionResult<FlangeJointReviewResult> ReviewBoltAssembly([FromBody] BoltAssemblyConditionInput input)
        {
            try
            {
                var service = new FlangeJointIntegrityService();
                return Ok(service.ReviewBoltAssembly(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("checklist")]
        public ActionResult<FlangeJointReviewResult> ReviewChecklist([FromBody] FlangeAssemblyChecklistInput input)
        {
            try
            {
                var service = new FlangeJointIntegrityService();
                return Ok(service.ReviewAssemblyChecklist(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("review-joint")]
        public ActionResult<FlangeJointReviewResult> ReviewJoint([FromBody] FlangeJointInput input)
        {
            try
            {
                var service = new FlangeJointIntegrityService();
                return Ok(service.ReviewJoint(input));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
