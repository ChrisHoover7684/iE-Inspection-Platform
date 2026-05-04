using iE.Core.Mechanical.B313.Materials;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class B313Controller : ControllerBase
{
    private readonly B313MaterialStressService _svc = new();

    [HttpPost("thickness")]
    public ActionResult<B313ThicknessResult> Thickness([FromBody] B313ThicknessRequest request)
    {
        var result = _svc.CalculateThickness(request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("materials")]
    public ActionResult<IReadOnlyList<B313MaterialStressRecord>> Materials([FromQuery] string? spec = null)
    {
        var results = _svc.ListMaterials(spec);
        return Ok(results);
    }
}
