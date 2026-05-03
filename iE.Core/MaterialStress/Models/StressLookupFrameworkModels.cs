namespace iE.Core.MaterialStress.Models;

public enum CalculationFamily
{
    PressureVessel,
    Piping,
    Tanks
}

public sealed record SharedStressLookupRequest(
    CalculationFamily CalculationFamily,
    string DesignCode,
    StressEra Era,
    string SpecNo,
    string TypeGrade,
    string ProductForm,
    string AlloyUNS,
    string ClassConditionTemper,
    double TemperatureF,
    bool AllowPartialMatch = false,
    bool StrictMode = true,
    bool AllowExtrapolation = false,
    double MaxExtrapolationDegrees = 0);
