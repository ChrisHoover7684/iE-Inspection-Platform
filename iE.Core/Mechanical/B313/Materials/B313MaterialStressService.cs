namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressService
{
    private readonly B313MaterialStressRepository _repository;
    public B313MaterialStressService(B313MaterialStressRepository? repository = null) => _repository = repository ?? new();

    private static string Normalize(string? value)
        => string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToUpperInvariant();

    private static string NormalizeSpec(string? value)
    {
        var spec = Normalize(value)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty)
            .Replace(" ", string.Empty);
        return spec;
    }

    private static string NormalizeProductForm(string? value)
    {
        var form = Normalize(value).Replace(".", string.Empty);
        return form switch
        {
            "SMLS" or "SMLS PIPE" => "SMLS. PIPE",
            "WLD" or "WLD PIPE" => "WLD. PIPE",
            "WLD & SMLS PIPE" or "WLD. & SMLS. PIPE" => "WLD. & SMLS. PIPE",
            _ => Normalize(value)
        };
    }

    private static string NormalizeClassConditionTemper(string? value)
    {
        var normalized = Normalize(value);
        return normalized == "..." ? string.Empty : normalized;
    }
    private static string NormalizeOptionalField(string? value)
    {
        var normalized = Normalize(value);
        return normalized == "..." ? string.Empty : normalized;
    }

    private static double RoundFactor(double value) => Math.Round(value, 3);

    private List<iE.Core.MaterialStress.Models.MaterialStressRecord> GetB313Candidates(string spec, string grade, string? productForm, string? unsNo, string? classConditionTemper)
    {
        var normalizedSpec = NormalizeSpec(spec);
        var normalizedGrade = Normalize(grade);
        var normalizedForm = NormalizeProductForm(productForm);
        var normalizedUns = Normalize(unsNo);
        var normalizedClass = NormalizeClassConditionTemper(classConditionTemper);

        var scoped = _repository.GetB313Records()
            .Where(r => NormalizeSpec(r.Material.SpecNo) == normalizedSpec
                && Normalize(r.Material.TypeGrade) == normalizedGrade
                && (string.IsNullOrWhiteSpace(normalizedForm)
                    || NormalizeProductForm(r.Material.ProductForm) == normalizedForm
                    || string.IsNullOrWhiteSpace(NormalizeOptionalField(r.Material.ProductForm))))
            .ToList();

        if (!string.IsNullOrWhiteSpace(normalizedUns) && !string.IsNullOrWhiteSpace(normalizedClass))
        {
            var withUnsAndClass = scoped
                .Where(r => Normalize(r.Material.AlloyUNS) == normalizedUns
                    && NormalizeClassConditionTemper(r.Material.ClassConditionTemper) == normalizedClass)
                .ToList();
            if (withUnsAndClass.Count > 0) return withUnsAndClass;
        }

        if (!string.IsNullOrWhiteSpace(normalizedUns))
        {
            var withUns = scoped
                .Where(r => Normalize(r.Material.AlloyUNS) == normalizedUns)
                .ToList();
            if (withUns.Count > 0) return withUns;
        }

        if (!string.IsNullOrWhiteSpace(normalizedForm))
        {
            var withForm = scoped
                .Where(r => NormalizeProductForm(r.Material.ProductForm) == normalizedForm
                    || string.IsNullOrWhiteSpace(NormalizeOptionalField(r.Material.ProductForm)))
                .ToList();
            if (withForm.Count > 0) return withForm;
        }

        if (!string.IsNullOrWhiteSpace(normalizedClass))
        {
            var withClass = scoped
                .Where(r => NormalizeClassConditionTemper(r.Material.ClassConditionTemper) == normalizedClass)
                .ToList();
            if (withClass.Count > 0) return withClass;
        }

        return scoped;
    }

    private double? GetAllowableStressPsiCore(string spec, string grade, string? productForm, string? unsNo, string? classConditionTemper, double temperatureF)
    {
        var normalizedUns = Normalize(unsNo);
        var normalizedClass = NormalizeClassConditionTemper(classConditionTemper);
        var candidates = GetB313Candidates(spec, grade, productForm, unsNo, classConditionTemper);
        if (candidates.Count != 1) return null;

        var match = candidates[0];
        var result = _repository.FindAllowableStress(
            match.Material.SpecNo,
            match.Material.TypeGrade,
            match.Material.ProductForm,
            string.IsNullOrWhiteSpace(normalizedUns) ? match.Material.AlloyUNS : normalizedUns,
            string.IsNullOrWhiteSpace(normalizedClass) ? match.Material.ClassConditionTemper : normalizedClass,
            temperatureF);
        return result.Found ? result.AllowableStressPsi : null;
    }

    private List<iE.Core.MaterialStress.Models.MaterialStressRecord> FindCandidateRecords(string spec, string grade, string? productForm, string? classConditionTemper)
    {
        var normalizedSpec = NormalizeSpec(spec);
        var normalizedGrade = Normalize(grade);
        var normalizedForm = NormalizeProductForm(productForm);
        var normalizedClass = NormalizeClassConditionTemper(classConditionTemper);

        return GetB313Candidates(spec, grade, productForm, null, classConditionTemper)
            .Where(r => string.IsNullOrWhiteSpace(normalizedClass) || NormalizeClassConditionTemper(r.Material.ClassConditionTemper) == normalizedClass)
            .ToList();
    }

    public double? GetAllowableStressPsi(string spec, string grade, string? productForm, string? unsNo, string? classConditionTemper, double temperatureF)
        => GetAllowableStressPsiCore(spec, grade, productForm, unsNo, classConditionTemper, temperatureF);

    public static double Interpolate(double t, double t1, double s1, double t2, double s2)
    {
        if (Math.Abs(t2 - t1) < double.Epsilon) return s1;
        return s1 + (t - t1) * (s2 - s1) / (t2 - t1);
    }

    public static double GetYCoefficient(double designTemperatureF, string materialCategory)
    {
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

    public static double CalculateRequiredThickness(double pressurePsi, double outsideDiameterIn, double allowableStressPsi, double qualityFactorE, double yCoefficient, double wFactor)
        => (pressurePsi * outsideDiameterIn) / (2.0 * ((allowableStressPsi * qualityFactorE * wFactor) + (pressurePsi * yCoefficient)));

    public B313ThicknessResult CalculateThickness(B313ThicknessRequest request)
    {
        if (request.PressurePsi <= 0) return new(false, "PressurePsi must be greater than 0.", null, null, null, null, null);
        if (request.OutsideDiameterIn <= 0) return new(false, "OutsideDiameterIn must be greater than 0.", null, null, null, null, null);
        if (request.WFactor <= 0 || request.WFactor > 1) return new(false, "WFactor must be greater than 0 and less than or equal to 1.", null, null, null, null, null);
        if (request.EOverride.HasValue && (request.EOverride.Value <= 0 || request.EOverride.Value > 1)) return new(false, "EOverride must be greater than 0 and less than or equal to 1.", null, null, null, null, null);
        if (request.YOverride.HasValue && (request.YOverride.Value < 0 || request.YOverride.Value > 0.7)) return new(false, "YOverride must be between 0 and 0.7.", null, null, null, null, null);
        if (string.IsNullOrWhiteSpace(request.Spec) && string.IsNullOrWhiteSpace(request.Grade) && string.IsNullOrWhiteSpace(request.ProductForm))
            return new(false, "Spec, Grade, and ProductForm cannot all be blank.", null, null, null, null, null);

        var candidates = FindCandidateRecords(request.Spec, request.Grade, request.ProductForm, request.ClassConditionTemper);
        if (candidates.Count > 0)
        {
            var minTemp = candidates.Min(c => c.MinTemperature ?? double.MaxValue);
            var maxTemp = candidates.Max(c => c.MaxTemperature ?? double.MinValue);
            if (request.TemperatureF < minTemp || request.TemperatureF > maxTemp)
                return new(false, $"TemperatureF must be within available stress range ({minTemp}F to {maxTemp}F).", null, null, null, null, null);
        }

        var normalizedSpec = NormalizeSpec(request.Spec);
        var normalizedGrade = Normalize(request.Grade);
        var normalizedProductForm = NormalizeProductForm(request.ProductForm);
        var normalizedUnsNo = Normalize(request.UnsNo);
        var normalizedClassConditionTemper = NormalizeClassConditionTemper(request.ClassConditionTemper);

        if (candidates.Count == 0)
        {
            var notFoundMessage = $"Allowable stress not found. normalized: spec='{normalizedSpec}', grade='{normalizedGrade}', productForm='{normalizedProductForm}', unsNo='{normalizedUnsNo}', classConditionTemper='{normalizedClassConditionTemper}'. Try GET /api/B313/materials?spec={normalizedSpec}.";
            if (normalizedSpec.StartsWith("SA", StringComparison.Ordinal))
            {
                notFoundMessage += " B31.3 piping lookups typically use A-spec naming (for example, use A106 instead of SA106) unless an explicit SA-spec B31.3 record exists.";
            }

            return new(false, notFoundMessage, null, null, null, null, null);
        }
        if (candidates.Count > 1)
        {
            return new(false, $"Ambiguous B31.3 material match ({candidates.Count} records). Provide ProductForm/UNS/ClassConditionTemper to disambiguate.", null, null, null, null, null);
        }

        var stress = GetAllowableStressPsiCore(request.Spec, request.Grade, request.ProductForm, request.UnsNo, request.ClassConditionTemper, request.TemperatureF);
        if (!stress.HasValue)
        {
            var message = $"Allowable stress not found. normalized: spec='{normalizedSpec}', grade='{normalizedGrade}', productForm='{normalizedProductForm}', unsNo='{normalizedUnsNo}', classConditionTemper='{normalizedClassConditionTemper}'. Try GET /api/B313/materials?spec={normalizedSpec}.";
            if (normalizedSpec.StartsWith("SA", StringComparison.Ordinal))
            {
                message += " B31.3 piping lookups typically use A-spec naming (for example, use A106 instead of SA106) unless an explicit SA-spec B31.3 record exists.";
            }
            return new(false, message, null, null, null, null, null);
        }

        var e = request.EOverride ?? GetEFactor(request.JointType, request.JointQualityKey);
        var y = request.YOverride ?? GetYCoefficient(request.TemperatureF, request.MaterialCategory);
        var t = CalculateRequiredThickness(request.PressurePsi, request.OutsideDiameterIn, stress.Value, e, y, request.WFactor);

        if (t <= 0) return new(false, "Required thickness must be greater than 0.", null, null, null, null, null);

        return new(true,
            "Calculated using B31.3 equation with imported allowable stress.",
            Math.Round(stress.Value, 0),
            RoundFactor(e),
            RoundFactor(y),
            RoundFactor(request.WFactor),
            Math.Round(t, 4));
    }

    public IReadOnlyList<B313MaterialStressRecord> ListMaterials(string? spec)
    {
        var normalizedSpec = NormalizeSpec(spec);
        return _repository.GetB313Records()
            .Where(r => string.IsNullOrWhiteSpace(normalizedSpec) || NormalizeSpec(r.Material.SpecNo) == normalizedSpec)
            .Select(r => new B313MaterialStressRecord
            {
                Spec = r.Material.SpecNo,
                Grade = r.Material.TypeGrade,
                ProductForm = r.Material.ProductForm,
                UNSNo = r.Material.AlloyUNS,
                ClassConditionTemper = r.Material.ClassConditionTemper,
                Notes = r.Material.Notes
            })
            .OrderBy(r => r.Spec)
            .ThenBy(r => r.Grade)
            .ThenBy(r => r.ProductForm)
            .ToList();
    }
}
