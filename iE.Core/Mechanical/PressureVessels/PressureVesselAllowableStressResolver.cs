using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.Mechanical.PressureVessels;

public sealed class PressureVesselAllowableStressResolver
{
    private readonly SharedStressLookupService _sharedStressLookupService;

    public PressureVesselAllowableStressResolver(MaterialStressService materialStressService)
    {
        _sharedStressLookupService = new SharedStressLookupService(materialStressService);
    }

    public ResolvedAllowableStress Resolve(PressureVesselMaterialStressInput input)
    {
        if (input.ManualAllowableStress)
        {
            if (!input.AllowableStressPsi.HasValue || input.AllowableStressPsi.Value <= 0)
                return new ResolvedAllowableStress(false, null, null, input.DesignTemperatureF, false, false, "Manual allowable stress override is enabled, but AllowableStressPsi is missing or invalid.", new[] { "Provide a valid manual allowable stress in psi." });
            return new ResolvedAllowableStress(true, input.AllowableStressPsi.Value, null, input.DesignTemperatureF, false, false, "Manual allowable stress override (psi).", Array.Empty<string>());
        }

        if (!Enum.TryParse<StressEra>(input.StressEra, ignoreCase: true, out var era))
            return new ResolvedAllowableStress(false, null, null, input.DesignTemperatureF, false, false, "Invalid StressEra. Expected Pre1950 / From1950To1998 / From1999Onward.", new[] { "Provide a valid StressEra or enable manual allowable stress override." });

        var lookup = _sharedStressLookupService.GetAllowableStress(new SharedStressLookupRequest(
            CalculationFamily.PressureVessel,
            string.IsNullOrWhiteSpace(input.DesignCode) ? "ASME_VIII_DIV1" : input.DesignCode,
            era,
            input.MaterialSpec,
            input.MaterialGrade,
            input.ProductForm,
            input.AlloyUNS,
            input.ClassConditionTemper,
            input.DesignTemperatureF,
            AllowPartialMatch: false,
            StrictMode: true,
            AllowExtrapolation: false,
            MaxExtrapolationDegrees: 0));

        if (!lookup.Found || !lookup.AllowableStressPsi.HasValue)
            return new ResolvedAllowableStress(false, null, null, input.DesignTemperatureF, lookup.WasInterpolated, lookup.WasExtrapolated, $"No material allowable stress found: {lookup.Message}", new[] { "Use manual override to continue when stress table data is unavailable." });

        var material = lookup.Record?.Material is null ? null : $"{lookup.Record.Material.SpecNo} {lookup.Record.Material.TypeGrade} {lookup.Record.Material.ProductForm}".Trim();
        return new ResolvedAllowableStress(true, lookup.AllowableStressPsi.Value, material, lookup.TemperatureUsed, lookup.WasInterpolated, lookup.WasExtrapolated, lookup.Message, Array.Empty<string>());
    }
}
