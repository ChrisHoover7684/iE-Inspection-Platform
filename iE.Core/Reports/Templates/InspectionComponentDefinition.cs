namespace iE.Core.Reports.Templates;

public sealed class InspectionComponentDefinition
{
    public string ComponentKey { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public string Standard { get; init; } = string.Empty;
    public string InspectionScope { get; init; } = string.Empty;
    public string EquipmentFamily { get; init; } = string.Empty;
    public string EquipmentSubtype { get; init; } = string.Empty;
    public string RequirementLevel { get; init; } = "minimum";
    public bool DefaultSelected { get; init; }
    public string FieldTagPrefix { get; init; } = string.Empty;
    public bool ParentComponentRequired { get; init; }
    public IReadOnlyList<string> AllowedParentComponentKeys { get; init; } = [];
    public string PressureBoundarySide { get; init; } = "unknown";
    public string? DesignPressureFieldTag { get; init; }
    public string? DesignTemperatureFieldTag { get; init; }
    public IReadOnlyDictionary<string, string> DesignPressureFieldTagsByPressureBoundarySide { get; init; } = new Dictionary<string, string>();
    public IReadOnlyDictionary<string, string> DesignTemperatureFieldTagsByPressureBoundarySide { get; init; } = new Dictionary<string, string>();
    public IReadOnlyList<string> DesignConditionSourceOptions { get; init; } = [];
    public IReadOnlyList<string> ParentThicknessSourceOptions { get; init; } = [];
    public bool NozzleLocationRequired { get; init; }
    public IReadOnlyList<string> NozzleLocationOptions { get; init; } = [];
    public bool SupportsTminCalculation { get; init; }
    public string? TminCalculationMethod { get; init; }
    public bool SupportsNozzleUg45 { get; init; }
    public bool SupportsFinding { get; init; }
    public bool SupportsRecommendation { get; init; }
    public bool SupportsRepairRequired { get; init; }
    public bool SupportsPhotoTag { get; init; }
    public bool SupportsSummary { get; init; }
    public bool SupportsNdeRequest { get; init; }
    public string? ReviewNotes { get; init; }
}
