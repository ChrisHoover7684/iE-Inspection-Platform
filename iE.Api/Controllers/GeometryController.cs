using Microsoft.AspNetCore.Mvc;
using iE.Core.Common.Geometry;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/common/geometry")]
    public class GeometryController : ControllerBase
    {
        [HttpPost("circle")]
        public ActionResult<CircleGeometryResult> CalculateCircle([FromBody] CircleGeometryInput input)
        {
            try
            {
                var service = new CircleGeometryService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("right-triangle")]
        public IActionResult CalculateRightTriangle([FromBody] RightTriangleGeometryInput input)
        {
            try
            {
                var service = new RightTriangleGeometryService();
                var result = service.Calculate(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("rectangle")]
        public ActionResult<RectangleGeometryResult> CalculateRectangle([FromBody] RectangleGeometryInput input)
        {
            try
            {
                var service = new RectangleGeometryService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("sphere")]
        public ActionResult<SphereGeometryResult> CalculateSphere([FromBody] SphereGeometryInput input)
        {
            try
            {
                var service = new SphereGeometryService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("cylinder")]
        public ActionResult<CylinderGeometryResult> CalculateCylinder([FromBody] CylinderGeometryInput input)
        {
            try
            {
                var service = new CylinderGeometryService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("cone")]
        public ActionResult<ConeGeometryResult> CalculateCone([FromBody] ConeGeometryInput input)
        {
            try
            {
                var service = new ConeGeometryService();
                var result = service.Calculate(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("ellipse")]
        public ActionResult<EllipseGeometryResult> CalculateEllipse([FromBody] EllipseGeometryInput input)
        {
            try
            {
                var service = new EllipseGeometryService();
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