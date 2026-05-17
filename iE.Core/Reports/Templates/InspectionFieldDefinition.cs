namespace iE.Core.Reports.Templates;

public class InspectionFieldDefinition
{
    public string FieldTag { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public string Standard { get; init; } = string.Empty;
    public string InspectionScope { get; init; } = string.Empty;
    public string EquipmentFamily { get; init; } = string.Empty;
    public string EquipmentSubtype { get; init; } = string.Empty;
    public string SectionGroup { get; init; } = string.Empty;
    public string ComponentType { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public IReadOnlyList<string> Options { get; init; } = [];
    public bool Required { get; init; }
    public string? DefaultValue { get; init; }
    public string? HelpText { get; init; }
    public bool SupportsFinding { get; init; }
    public bool SupportsRecommendation { get; init; }
    public bool SupportsRepairRequired { get; init; }
    public bool SupportsPhotoTag { get; init; }
    public bool SupportsSummary { get; init; }
    public bool SupportsNdeRequest { get; init; }
    public string WordExportGroup { get; init; } = string.Empty;
    public int DefaultLayoutOrder { get; init; }
    public string? ReviewNotes { get; init; }
    public bool IsFutureOnly { get; init; }
}
