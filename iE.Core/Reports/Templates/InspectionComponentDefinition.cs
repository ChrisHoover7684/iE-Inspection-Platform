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
    public bool SupportsFinding { get; init; }
    public bool SupportsRecommendation { get; init; }
    public bool SupportsRepairRequired { get; init; }
    public bool SupportsPhotoTag { get; init; }
    public bool SupportsSummary { get; init; }
    public bool SupportsNdeRequest { get; init; }
    public string? ReviewNotes { get; init; }
}
