using iE.Core.Mechanical.PressureVessels;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/mechanical/pressure-vessels")]
public sealed class PressureVesselsController : ControllerBase
{
    private readonly PressureVesselAllowableStressResolver _stressResolver = new();

    public sealed record ShellCalculationRequest(CylindricalShellInput Input, PressureVesselMaterialStressInput MaterialStress);
    public sealed record HeadCalculationRequest(HeadThicknessInput Input, PressureVesselMaterialStressInput MaterialStress);

    public sealed record CalculationEnvelope<T>(
        double ResolvedAllowableStressPsi,
        string? MaterialMatched,
        double TemperatureUsed,
        bool WasInterpolated,
        bool WasExtrapolated,
        string StressSourceMessage,
        T Result,
        IReadOnlyList<string> Warnings);

    [HttpPost("shells/cylindrical/calculate")]
    public ActionResult<CalculationEnvelope<CylindricalShellResult>> CalculateCylindrical([FromBody] ShellCalculationRequest request)
        => Execute(() =>
        {
            var resolved = _stressResolver.Resolve(request.MaterialStress);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var input = request.Input with { AllowableStressPsi = resolved.AllowableStressPsi.Value };
            var result = new ShellThicknessService().CalculateCylindrical(input);
            return new CalculationEnvelope<CylindricalShellResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("shells/spherical/calculate")]
    public ActionResult<CalculationEnvelope<SphericalShellResult>> CalculateSpherical([FromBody] ShellCalculationRequest request)
        => Execute(() =>
        {
            var resolved = _stressResolver.Resolve(request.MaterialStress);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var i = request.Input;
            var input = new SphericalShellInput(i.DesignPressurePsi, resolved.AllowableStressPsi.Value, i.InsideDiameterIn, i.OutsideDiameterIn, i.OriginalThicknessIn, i.JointEfficiency, i.CorrosionAllowanceIn, i.ProvidedThicknessIn);
            var result = new ShellThicknessService().CalculateSpherical(input);
            return new CalculationEnvelope<SphericalShellResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("shells/conical/calculate")]
    public ActionResult<ConicalShellResult> CalculateConical([FromBody] ConicalShellInput input)
        => Execute(() => new ShellThicknessService().CalculateConicalShell(input));

    [HttpPost("heads/calculate")]
    public ActionResult<HeadThicknessResult> CalculateHead([FromBody] HeadThicknessInput input)
        => Execute(() => new HeadThicknessService().Calculate(input));

    [HttpPost("nozzles/ug45/calculate")]
    public ActionResult<NozzleThicknessResult> CalculateNozzle([FromBody] NozzleThicknessInput input)
        => Execute(() => new NozzleThicknessService().Calculate(input));

    [HttpGet("heads/types")]
    public ActionResult<IReadOnlyList<string>> GetHeadTypes() => Ok(Enum.GetNames<HeadType>());

    [HttpGet("nozzles/types")]
    public ActionResult<IReadOnlyList<string>> GetNozzleTypes() => Ok(Enum.GetNames<NozzleType>());

    [HttpGet("ug45-table")]
    public ActionResult<IReadOnlyList<Ug45TableEntry>> GetUg45Table() => Ok(Ug45Table.GetAll());

    private ActionResult<T> Execute<T>(Func<T> action)
    {
        try
        {
            return Ok(action());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
