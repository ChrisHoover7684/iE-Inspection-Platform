namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressService
{
    private readonly B313MaterialStressRepository _repository;
    public B313MaterialStressService(B313MaterialStressRepository? repository = null) => _repository = repository ?? new();

    public double? GetAllowableStressPsi(string spec, string grade, string? productForm, string? unsNo, string? classConditionTemper, double temperatureF)
    {
        var result = _repository.FindAllowableStress(spec, grade, productForm ?? string.Empty, unsNo ?? string.Empty, classConditionTemper ?? string.Empty, temperatureF);
        return result.Found ? result.AllowableStressPsi : null;
    }

    public static double Interpolate(double t, double t1, double s1, double t2, double s2)
    {
        if (Math.Abs(t2 - t1) < double.Epsilon) return s1;
        return s1 + (t - t1) * (s2 - s1) / (t2 - t1);
    }

    public static double GetYCoefficient(double designTemperatureF, string materialCategory)
    {
        // Ported style: piecewise by material class; exact old add-in breakpoints unavailable in this repo.
        return materialCategory.Trim().ToUpperInvariant() switch
        {
            "AUSTENITIC" or "NICKEL" => designTemperatureF <= 1000 ? 0.4 : 0.5,
            _ => designTemperatureF <= 900 ? 0.4 : 0.7
        };
    }

    public static double GetEFactor(string jointType, string qualityKey)
    {
        var jt = jointType.Trim().ToUpperInvariant();
        var q = qualityKey.Trim().ToUpperInvariant();
        if (jt == "SEAMLESS") return 1.0;
        if (jt == "ERW" && (q == "FULL_RT" || q == "RT1")) return 1.0;
        if (jt == "ERW" && (q == "SPOT_RT" || q == "RT3")) return 0.85;
        if (jt == "FURNACE_BUTT_WELD") return 0.6;
        return 0.8;
    }

    public static string ClassifyMaterial(string spec, string productForm)
        => $"{spec.Trim().ToUpperInvariant()}|{productForm.Trim().ToUpperInvariant()}";

    public static double CalculateRequiredThickness(double pressurePsi, double outsideDiameterIn, double allowableStressPsi, double qualityFactorE, double yCoefficient, double wFactor)
        => (pressurePsi * outsideDiameterIn) / (2.0 * ((allowableStressPsi * qualityFactorE * wFactor) + (pressurePsi * yCoefficient)));

    public B313ThicknessResult CalculateThickness(B313ThicknessRequest request)
    {
        var stress = GetAllowableStressPsi(request.Spec, request.Grade, request.ProductForm, request.UnsNo, request.ClassConditionTemper, request.TemperatureF);
        if (!stress.HasValue) return new(false, "Allowable stress not found for specified material inputs.", null, null, null, null, null);

        var e = request.EOverride ?? GetEFactor(request.JointType, request.JointQualityKey);
        var y = request.YOverride ?? GetYCoefficient(request.TemperatureF, request.MaterialCategory);
        var t = CalculateRequiredThickness(request.PressurePsi, request.OutsideDiameterIn, stress.Value, e, y, request.WFactor);

        return new(true, "Calculated using B31.3 equation with imported allowable stress.", stress.Value, e, y, request.WFactor, t);
    }
}
