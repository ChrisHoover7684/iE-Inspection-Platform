namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressService
{
    private readonly B313MaterialStressRepository _repository;
    public B313MaterialStressService(B313MaterialStressRepository? repository = null) => _repository = repository ?? new();

    private static string Normalize(string? value)
        => string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToUpperInvariant();

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
        if (request.PressurePsi <= 0)
            return new(false, "PressurePsi must be greater than 0.", null, null, null, null, null);
        if (request.OutsideDiameterIn <= 0)
            return new(false, "OutsideDiameterIn must be greater than 0.", null, null, null, null, null);
        if (request.WFactor <= 0 || request.WFactor > 1)
            return new(false, "WFactor must be greater than 0 and less than or equal to 1.", null, null, null, null, null);
        if (request.EOverride.HasValue && (request.EOverride.Value <= 0 || request.EOverride.Value > 1))
            return new(false, "EOverride must be greater than 0 and less than or equal to 1.", null, null, null, null, null);
        if (request.YOverride.HasValue && (request.YOverride.Value < 0 || request.YOverride.Value > 0.7))
            return new(false, "YOverride must be between 0 and 0.7.", null, null, null, null, null);

        var stress = GetAllowableStressPsi(request.Spec, request.Grade, request.ProductForm, request.UnsNo, request.ClassConditionTemper, request.TemperatureF);
        if (!stress.HasValue)
        {
            var spec = Normalize(request.Spec);
            var grade = Normalize(request.Grade);
            var productForm = Normalize(request.ProductForm);
            var unsNo = Normalize(request.UnsNo);
            var classConditionTemper = Normalize(request.ClassConditionTemper);

            var suggestions = _repository.GetB313Records()
                .Select(r => new
                {
                    Spec = Normalize(r.Material.SpecNo),
                    Grade = Normalize(r.Material.TypeGrade),
                    ProductForm = Normalize(r.Material.ProductForm),
                    UnsNo = Normalize(r.Material.AlloyUNS),
                    ClassConditionTemper = Normalize(r.Material.ClassConditionTemper)
                })
                .Where(r => r.Spec == spec || (!string.IsNullOrWhiteSpace(grade) && r.Grade == grade))
                .Distinct()
                .Take(8)
                .Select(r => $"{r.Spec} / {r.Grade} / {r.ProductForm} / {r.UnsNo} / {r.ClassConditionTemper}")
                .ToArray();

            var message = $"Allowable stress not found. normalized: spec='{spec}', grade='{grade}', productForm='{productForm}', unsNo='{unsNo}', classConditionTemper='{classConditionTemper}'.";
            if (suggestions.Length > 0)
                message += $" closeMatches: {string.Join("; ", suggestions)}";

            return new(false, message, null, null, null, null, null);
        }
        if (stress.Value <= 0)
            return new(false, "Allowable stress must be greater than 0.", null, null, null, null, null);

        var e = request.EOverride ?? GetEFactor(request.JointType, request.JointQualityKey);
        var y = request.YOverride ?? GetYCoefficient(request.TemperatureF, request.MaterialCategory);
        var t = CalculateRequiredThickness(request.PressurePsi, request.OutsideDiameterIn, stress.Value, e, y, request.WFactor);

        return new(true, "Calculated using B31.3 equation with imported allowable stress.", stress.Value, e, y, request.WFactor, t);
    }

    public IReadOnlyList<B313MaterialStressRecord> ListMaterials(string? spec)
    {
        var normalizedSpec = Normalize(spec);
        return _repository.GetB313Records()
            .Where(r => string.IsNullOrWhiteSpace(normalizedSpec) || Normalize(r.Material.SpecNo) == normalizedSpec)
            .Select(r => new B313MaterialStressRecord
            {
                Spec = r.Material.SpecNo,
                Grade = r.Material.TypeGrade,
                ProductForm = r.Material.ProductForm,
                UNSNo = r.Material.AlloyUNS,
                ClassConditionTemper = r.Material.ClassConditionTemper
            })
            .OrderBy(r => r.Spec)
            .ThenBy(r => r.Grade)
            .ToList();
    }
}
