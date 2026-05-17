namespace iE.Core.Reports.Templates;

public sealed class ComponentCalculationContext
{
    public string ComponentKey { get; init; } = string.Empty;
    public string ComponentLabel { get; init; } = string.Empty;
    public string EquipmentSubtype { get; init; } = string.Empty;
    public bool ParentComponentRequired { get; init; }
    public IReadOnlyList<string> AllowedParentComponentKeys { get; init; } = [];
    public string? SelectedParentComponent { get; init; }
    public bool NozzleLocationRequired { get; init; }
    public IReadOnlyList<string> AllowedNozzleLocationOptions { get; init; } = [];
    public string? SelectedNozzleLocation { get; init; }
    public string PressureBoundarySide { get; init; } = "unknown";
    public string? DesignPressureFieldTag { get; init; }
    public string? DesignTemperatureFieldTag { get; init; }
    public bool TminCalculationSupported { get; init; }
    public string? TminCalculationMethod { get; init; }
    public bool Ug45Supported { get; init; }
    public IReadOnlyList<string> ParentThicknessSourceOptions { get; init; } = [];
    public IReadOnlyList<string> ValidationWarnings { get; init; } = [];
}
