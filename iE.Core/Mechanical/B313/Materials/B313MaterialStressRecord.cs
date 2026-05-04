namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressRecord
{
    public string MaterialGroup { get; init; } = string.Empty;
    public string Material { get; init; } = string.Empty;
    public string NominalComposition { get; init; } = string.Empty;
    public string ProductForm { get; init; } = string.Empty;
    public string Spec { get; init; } = string.Empty;
    public string Grade { get; init; } = string.Empty;
    public string UNSNo { get; init; } = string.Empty;
    public string ClassConditionTemper { get; init; } = string.Empty;
    public string Size { get; init; } = string.Empty;
    public string PNo { get; init; } = string.Empty;
    public double? MinTemp { get; init; }
    public double? Tensile { get; init; }
    public double? Yield { get; init; }
    public double? MaxTemp { get; init; }
    public string Notes { get; init; } = string.Empty;
    public IReadOnlyList<B313MaterialStressPoint> StressValues { get; init; } = Array.Empty<B313MaterialStressPoint>();
}
