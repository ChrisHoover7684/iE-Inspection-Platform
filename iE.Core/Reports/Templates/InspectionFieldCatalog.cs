namespace iE.Core.Reports.Templates;

public static class InspectionFieldCatalog
{
    public static IReadOnlyList<string> Api570ExternalComponentPresets { get; } =
    [
        "Valve", "Control Valve / Control Loop", "Flange Pair", "Bolting / Gasket", "Support", "Spring Can / Hanger", "Guide / Anchor", "Shoe / Saddle", "Bellows / Expansion Joint", "Small Bore Connection", "Branch Connection", "Deadleg", "Injection Point / Mix Point", "Insulation / Jacketing Area", "CUI Area", "Other Component"
    ];

    // Frontend field catalog is the temporary source of truth for #276 external field review export.
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
            ComponentPresets = Api570ExternalComponentPresets,
            Fields =
            [
                Field("api570.external.piping.component.type", "Component Type", "API 570", "External", "Piping", "General", "Component Section", "Component", "select", true, 10, options: Api570ExternalComponentPresets),
                Field("api570.external.piping.component.tag-name", "Component Tag / Name", "API 570", "External", "Piping", "General", "Component Section", "Component", "text", false, 20),
                Field("api570.external.piping.component.location", "Location", "API 570", "External", "Piping", "General", "Component Section", "Component", "text", false, 30),
                Field("api570.external.piping.component.condition", "Condition", "API 570", "External", "Piping", "General", "Component Section", "Component", "select", false, 40, options: ["Acceptable", "Monitor", "Issue"], supportsFinding: true),
                Field("api570.external.piping.component.finding-notes", "Finding Notes", "API 570", "External", "Piping", "General", "Findings", "Component", "textarea", false, 50, supportsFinding: true, supportsSummary: true),
                Field("api570.external.piping.component.recommendation-text", "Recommendation Text", "API 570", "External", "Piping", "General", "Recommendations", "Component", "textarea", false, 60, supportsRecommendation: true),
                Field("api570.external.piping.component.photo-tag", "Photo Reference / Picture Tag", "API 570", "External", "Piping", "General", "Photos", "Component", "text", false, 70, supportsPhotoTag: true),
                Field("api570.external.piping.component.create-finding", "Create Finding", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 80, supportsFinding: true),
                Field("api570.external.piping.component.recommendation-required", "Recommendation Required", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 90, supportsRecommendation: true),
                Field("api570.external.piping.component.repair-required", "Repair Required", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 100, supportsRepairRequired: true),
                Field("api570.external.piping.component.photo-required", "Photo Required", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 110, supportsPhotoTag: true),
                Field("api570.external.piping.component.nde-required", "NDE Required", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 120, supportsNdeRequest: true),
                Field("api570.external.piping.component.add-to-summary", "Add to Summary", "API 570", "External", "Piping", "General", "Actions", "Component", "boolean", false, 130, supportsSummary: true)
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
