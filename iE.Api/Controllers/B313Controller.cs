using iE.Core.Mechanical.B313.Materials;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class B313Controller : ControllerBase
{
    [HttpPost("thickness")]
    public ActionResult<B313ThicknessResult> Thickness([FromBody] B313ThicknessRequest request)
    {
        var svc = new B313MaterialStressService();
        var result = svc.CalculateThickness(request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
