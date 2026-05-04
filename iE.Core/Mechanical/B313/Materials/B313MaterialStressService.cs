namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressService
{
    private readonly B313MaterialStressRepository _repository;
    public B313MaterialStressService(B313MaterialStressRepository? repository = null) => _repository = repository ?? new();

    public double? GetAllowableStressPsi(string spec, string grade, string productForm, string unsNo, string classConditionTemper, double temperatureF)
    {
        var result = _repository.FindAllowableStress(spec, grade, productForm, unsNo, classConditionTemper, temperatureF);
        return result.Found ? result.AllowableStressPsi : null;
    }

    public static double Interpolate(double t, double t1, double s1, double t2, double s2)
    {
        if (Math.Abs(t2 - t1) < double.Epsilon) return s1;
        return s1 + (t - t1) * (s2 - s1) / (t2 - t1);
    }

    public static double GetYCoefficient(double designTemperatureF)
        => designTemperatureF <= 900 ? 0.4 : 0.7;

    public static double GetEFactor(bool seamless) => seamless ? 1.0 : 0.85;

    public static string ClassifyMaterial(string spec, string productForm)
        => $"{spec.Trim().ToUpperInvariant()}|{productForm.Trim().ToUpperInvariant()}";

    public static double CalculateRequiredThickness(double pressurePsi, double outsideDiameterIn, double allowableStressPsi, double qualityFactorE, double yCoefficient, double wFactor)
        => (pressurePsi * outsideDiameterIn) / (2.0 * ((allowableStressPsi * qualityFactorE * wFactor) + (pressurePsi * yCoefficient)));
}
