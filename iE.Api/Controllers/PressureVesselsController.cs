using iE.Core.Mechanical.PressureVessels;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/mechanical/pressure-vessels")]
public sealed class PressureVesselsController : ControllerBase
{
    private readonly PressureVesselAllowableStressResolver _stressResolver;

    public PressureVesselsController(PressureVesselAllowableStressResolver stressResolver)
    {
        _stressResolver = stressResolver;
    }

    public sealed record CylindricalShellCalculationRequest(CylindricalShellInput Input, PressureVesselMaterialStressInput MaterialStress);
    public sealed record SphericalShellCalculationRequest(SphericalShellInput Input, PressureVesselMaterialStressInput MaterialStress);
    public sealed record ConicalShellCalculationRequest(ConicalShellInput Input, PressureVesselMaterialStressInput? MaterialStress);
    public sealed record HeadCalculationRequest(HeadThicknessInput Input, PressureVesselMaterialStressInput? MaterialStress);
    public sealed record NozzleCalculationRequest(NozzleThicknessInput Input);

    public sealed record CalculationEnvelope<T>(double ResolvedAllowableStressPsi, string? MaterialMatched, double TemperatureUsed, bool WasInterpolated, bool WasExtrapolated, string StressSourceMessage, T Result, IReadOnlyList<string> Warnings);

    [HttpPost("shells/cylindrical/calculate")]
    public ActionResult<CalculationEnvelope<CylindricalShellResult>> CalculateCylindrical([FromBody] CylindricalShellCalculationRequest request)
        => Execute(() =>
        {
            var resolved = _stressResolver.Resolve(request.MaterialStress);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var input = request.Input with { AllowableStressPsi = resolved.AllowableStressPsi.Value };
            var result = new ShellThicknessService().CalculateCylindrical(input);
            return new CalculationEnvelope<CylindricalShellResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("shells/spherical/calculate")]
    public ActionResult<CalculationEnvelope<SphericalShellResult>> CalculateSpherical([FromBody] SphericalShellCalculationRequest request)
        => Execute(() =>
        {
            var resolved = _stressResolver.Resolve(request.MaterialStress);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var input = request.Input with { AllowableStressPsi = resolved.AllowableStressPsi.Value };
            var result = new ShellThicknessService().CalculateSpherical(input);
            return new CalculationEnvelope<SphericalShellResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("shells/conical/calculate")]
    public ActionResult<CalculationEnvelope<ConicalShellResult>> CalculateConical([FromBody] ConicalShellCalculationRequest request)
        => Execute(() =>
        {
            var stressInput = request.MaterialStress ?? new PressureVesselMaterialStressInput("ASME_VIII_DIV1", "From1999Onward", 70, "", "", "", "", "", true, request.Input.AllowableStressPsi);
            var resolved = _stressResolver.Resolve(stressInput);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var result = new ShellThicknessService().CalculateConicalShell(request.Input with { AllowableStressPsi = resolved.AllowableStressPsi.Value });
            return new CalculationEnvelope<ConicalShellResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("heads/calculate")]
    public ActionResult<CalculationEnvelope<HeadThicknessResult>> CalculateHead([FromBody] HeadCalculationRequest request)
        => Execute(() =>
        {
            var stressInput = request.MaterialStress ?? new PressureVesselMaterialStressInput("ASME_VIII_DIV1", "From1999Onward", 70, "", "", "", "", "", true, request.Input.AllowableStressPsi);
            var resolved = _stressResolver.Resolve(stressInput);
            if (!resolved.IsValid || !resolved.AllowableStressPsi.HasValue) throw new ArgumentException(resolved.Message);
            var result = new HeadThicknessService().Calculate(request.Input with { AllowableStressPsi = resolved.AllowableStressPsi.Value });
            return new CalculationEnvelope<HeadThicknessResult>(resolved.AllowableStressPsi.Value, resolved.MaterialMatched, resolved.TemperatureUsed, resolved.WasInterpolated, resolved.WasExtrapolated, resolved.Message, result, resolved.Warnings);
        });

    [HttpPost("nozzles/ug45/calculate")]
    public ActionResult<CalculationEnvelope<NozzleThicknessResult>> CalculateNozzle([FromBody] NozzleCalculationRequest request)
        => Execute(() =>
        {
            var result = new NozzleThicknessService().Calculate(request.Input);
            return new CalculationEnvelope<NozzleThicknessResult>(request.Input.AllowableStressPsi, null, request.Input.DesignTemperatureF, false, false, "Nozzle UG-45 uses direct NozzleThicknessInput values.", result, result.Warnings);
        });

    [HttpGet("heads/types")] public ActionResult<IReadOnlyList<string>> GetHeadTypes() => Ok(Enum.GetNames<HeadType>());
    [HttpGet("nozzles/types")] public ActionResult<IReadOnlyList<string>> GetNozzleTypes() => Ok(Enum.GetNames<NozzleType>());
    [HttpGet("ug45-table")] public ActionResult<IReadOnlyList<Ug45TableEntry>> GetUg45Table() => Ok(Ug45Table.GetAll());

    private ActionResult<T> Execute<T>(Func<T> action)
    {
        try { return Ok(action()); }
        catch (ArgumentOutOfRangeException ex) { return BadRequest(new { error = ex.Message }); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
    }
}
