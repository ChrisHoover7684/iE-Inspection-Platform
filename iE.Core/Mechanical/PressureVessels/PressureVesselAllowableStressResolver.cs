using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.Mechanical.PressureVessels;

public sealed class PressureVesselAllowableStressResolver
{
    private readonly MaterialStressService _materialStressService;

    public PressureVesselAllowableStressResolver(MaterialStressService? materialStressService = null)
    {
        _materialStressService = materialStressService ?? new MaterialStressService();
    }

    public ResolvedAllowableStress Resolve(PressureVesselMaterialStressInput input)
    {
        if (input.ManualAllowableStress)
        {
            if (!input.AllowableStressPsi.HasValue || input.AllowableStressPsi.Value <= 0)
                return new ResolvedAllowableStress(false, null, null, input.DesignTemperatureF, false, false, "Manual allowable stress override is enabled, but AllowableStressPsi is missing or invalid.", new[] { "Provide a valid manual allowable stress in psi." });
            return new ResolvedAllowableStress(true, input.AllowableStressPsi.Value, null, input.DesignTemperatureF, false, false, "Manual allowable stress override (psi).", Array.Empty<string>());
        }

        if (!Enum.TryParse<StressEra>(input.StressEra, out var era))
            era = StressEra.From1999Onward;

        var lookup = _materialStressService.GetAllowableStress(new StressLookupRequest
        {
            CalculationFamily = "PressureVessel",
            DesignCode = string.IsNullOrWhiteSpace(input.DesignCode) ? "ASME_VIII_DIV1" : input.DesignCode,
            Era = era,
            SpecNo = input.MaterialSpec,
            TypeGrade = input.MaterialGrade,
            ProductForm = input.ProductForm,
            AlloyUNS = input.AlloyUNS,
            ClassConditionTemper = input.ClassConditionTemper,
            Temperature = input.DesignTemperatureF,
            AllowPartialMatch = false,
            StrictMode = true,
            AllowExtrapolation = false,
            MaxExtrapolationDegrees = 0
        });

        if (!lookup.Found || !lookup.AllowableStressPsi.HasValue)
            return new ResolvedAllowableStress(false, null, null, input.DesignTemperatureF, lookup.WasInterpolated, lookup.WasExtrapolated, $"No material allowable stress found: {lookup.Message}", new[] { "Use manual override to continue when stress table data is unavailable." });

        var material = lookup.Record?.Material is null ? null : $"{lookup.Record.Material.SpecNo} {lookup.Record.Material.TypeGrade} {lookup.Record.Material.ProductForm}".Trim();
        return new ResolvedAllowableStress(true, lookup.AllowableStressPsi.Value, material, lookup.TemperatureUsed, lookup.WasInterpolated, lookup.WasExtrapolated, lookup.Message, Array.Empty<string>());
    }
}
