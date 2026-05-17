namespace iE.Core.Reports.Templates;

public static class InspectionFieldCatalog
{
    // Frontend field catalog is the temporary source of truth for external field review export.
    // Keep backend MVP field sets limited to currently active API 570 behavior.
    public static IReadOnlyList<InspectionFieldSet> ExternalMvpFieldSets { get; } =
    [
        new()
        {
            Id = "api-570-external-piping-fields",
            Name = "API 570 External Piping",
            Standard = "API 570",
            InspectionScope = "External",
            EquipmentFamily = "Piping",
            EquipmentSubtype = "General",
            Fields =
            [
                Field("api570.external.piping.component.type", "Component Type", "API 570", "External", "Piping", "General", "Component Section", "Component", "select", true, 10, options: ["Valve", "Other Component"]),
                Field("api570.external.piping.component.finding-notes", "Finding Notes", "API 570", "External", "Piping", "General", "Findings", "Component", "textarea", false, 20, supportsFinding: true, supportsSummary: true),
                Field("api570.external.piping.component.recommendation-text", "Recommendation Text", "API 570", "External", "Piping", "General", "Recommendations", "Component", "textarea", false, 30, supportsRecommendation: true),
                Field("api570.external.piping.component.photo-tag", "Photo Reference / Picture Tag", "API 570", "External", "Piping", "General", "Photos", "Component", "text", false, 40, supportsPhotoTag: true)
            ]
        }
    ];

    public static IReadOnlyList<InspectionFieldDefinition> FutureOnlyApi510InternalFields { get; } =
    [
        Field("api510.internal.future.shell.condition", "Internal Shell Condition", "API 510", "Internal", "Pressure Equipment", "Vessel", "Internal Inspection", "Internal Shell", "textarea", false, 1, isFutureOnly: true, reviewNotes: "Future-only. Do not include in MVP export.")
    ];

    private static InspectionFieldDefinition Field(string tag, string label, string standard, string scope, string family, string subtype, string group, string component, string type, bool required, int order, IReadOnlyList<string>? options = null, bool supportsFinding = false, bool supportsRecommendation = false, bool supportsRepairRequired = false, bool supportsPhotoTag = false, bool supportsSummary = false, bool supportsNdeRequest = false, bool isFutureOnly = false, string? reviewNotes = null)
        => new() { FieldTag = tag, Label = label, Standard = standard, InspectionScope = scope, EquipmentFamily = family, EquipmentSubtype = subtype, SectionGroup = group, ComponentType = component, DataType = type, Required = required, DefaultLayoutOrder = order, Options = options ?? [], SupportsFinding = supportsFinding, SupportsRecommendation = supportsRecommendation, SupportsRepairRequired = supportsRepairRequired, SupportsPhotoTag = supportsPhotoTag, SupportsSummary = supportsSummary, SupportsNdeRequest = supportsNdeRequest, WordExportGroup = $"{standard} | {scope} | {family} | {subtype}", ReviewNotes = reviewNotes, IsFutureOnly = isFutureOnly };
}
