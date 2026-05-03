using iE.Core.Mechanical.PressureVessels;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/mechanical/pressure-vessels")]
public sealed class PressureVesselsController : ControllerBase
{
    [HttpPost("shells/cylindrical/calculate")]
    public ActionResult<CylindricalShellResult> CalculateCylindrical([FromBody] CylindricalShellInput input)
        => Ok(new ShellThicknessService().CalculateCylindrical(input));

    [HttpPost("shells/spherical/calculate")]
    public ActionResult<SphericalShellResult> CalculateSpherical([FromBody] SphericalShellInput input)
        => Ok(new ShellThicknessService().CalculateSpherical(input));

    [HttpPost("shells/conical/calculate")]
    public ActionResult<ConicalShellResult> CalculateConical([FromBody] ConicalShellInput input)
        => Ok(new ShellThicknessService().CalculateConicalShell(input));

    [HttpPost("heads/calculate")]
    public ActionResult<HeadThicknessResult> CalculateHead([FromBody] HeadThicknessInput input)
        => Ok(new HeadThicknessService().Calculate(input));

    [HttpPost("nozzles/ug45/calculate")]
    public ActionResult<NozzleThicknessResult> CalculateNozzle([FromBody] NozzleThicknessInput input)
        => Ok(new NozzleThicknessService().Calculate(input));

    [HttpGet("heads/types")]
    public ActionResult<IReadOnlyList<string>> GetHeadTypes() => Ok(Enum.GetNames<HeadType>());

    [HttpGet("nozzles/types")]
    public ActionResult<IReadOnlyList<string>> GetNozzleTypes() => Ok(Enum.GetNames<NozzleType>());

    [HttpGet("ug45-table")]
    public ActionResult<IReadOnlyList<Ug45TableEntry>> GetUg45Table() => Ok(Ug45Table.GetAll());
}
