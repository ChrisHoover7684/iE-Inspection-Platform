namespace iE.Core.Reports.Templates;

public static class InspectionFieldCatalog
{
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
        },
        Api510Set("api-510-external-exchangers-fields", "API 510 External Exchangers", "Exchanger", "api510.external.exchanger", ["Shell","Shell Cover","Channel Head","Channel Cover","Bonnet Head","Nozzles","Tubesheet Area","Flanges / Gaskets / Bolting","Saddles / Supports","Expansion Joint","Coating / Insulation Area","Platform / Ladder / Access","Nameplate / Markings","CML Locations","Other Component"],
            [
                Field("api510.external.exchanger.shell-tube.shell.condition", "Shell Condition (Shell & Tube)", "API 510", "External", "Pressure Equipment", "Exchanger", "Component Condition", "Shell", "select", false, 120, options: ["Acceptable","Monitor","Issue"], supportsFinding: true),
                Field("api510.external.exchanger.shell-tube.channel-head.condition", "Channel Head Condition (Shell & Tube)", "API 510", "External", "Pressure Equipment", "Exchanger", "Component Condition", "Channel Head", "select", false, 130, options: ["Acceptable","Monitor","Issue"], supportsFinding: true)
            ]),
        Api510Set("api-510-external-drum-vessel-fields", "API 510 External Drums / Pressure Vessels", "Drum / Vessel", "api510.external.drum-vessel", ["Shell","Heads","Nozzles","Manways","Flanges / Gaskets / Bolting","Saddle / Skirt / Legs","Supports / Foundation","Platforms / Ladders / Handrails","Coating","Insulation","Nameplate / Markings","CML Locations","Other Component"],
            [Field("api510.external.drum-vessel.shell.condition", "Shell Condition", "API 510", "External", "Pressure Equipment", "Drum / Vessel", "Component Condition", "Shell", "select", false, 120, options: ["Acceptable","Monitor","Issue"], supportsFinding: true)]),
        Api510Set("api-510-external-tower-column-fields", "API 510 External Towers / Columns", "Tower / Column", "api510.external.tower-column", ["Shell Courses","Heads","Nozzles","Manways","Skirt","Base Ring / Anchor Bolts","Platforms / Ladders / Handrails","Insulation / Jacketing","Coating","Nameplate / Markings","CML Locations","Other Component"],
            [Field("api510.external.tower-column.shell-courses.condition", "Shell Courses Condition", "API 510", "External", "Pressure Equipment", "Tower / Column", "Component Condition", "Shell Courses", "select", false, 120, options: ["Acceptable","Monitor","Issue"], supportsFinding: true)])
    ];

    public static IReadOnlyList<InspectionFieldDefinition> FutureOnlyApi510InternalFields { get; } =
    [
        Field("api510.internal.future.shell.condition", "Internal Shell Condition", "API 510", "Internal", "Pressure Equipment", "Vessel", "Internal Inspection", "Internal Shell", "textarea", false, 1, isFutureOnly: true, reviewNotes: "Future-only. Do not include in MVP export.")
    ];

    private static InspectionFieldSet Api510Set(string id, string name, string subtype, string prefix, IReadOnlyList<string> presets, IReadOnlyList<InspectionFieldDefinition> extraFields)
    {
        var fields = Api510CommonFields(subtype, prefix).Concat(extraFields).ToArray();
        return new InspectionFieldSet { Id = id, Name = name, Standard = "API 510", InspectionScope = "External", EquipmentFamily = "Pressure Equipment", EquipmentSubtype = subtype, ComponentPresets = presets, Fields = fields };
    }

    private static IReadOnlyList<InspectionFieldDefinition> Api510CommonFields(string subtype, string prefix) =>
    [
        Field($"{prefix}.external-condition.summary", "External Condition", "API 510", "External", "Pressure Equipment", subtype, "External Condition", "Equipment", "textarea", true, 10, supportsSummary: true),
        Field($"{prefix}.leakage-staining.summary", "Leakage / Staining", "API 510", "External", "Pressure Equipment", subtype, "Leakage / Staining", "Equipment", "textarea", false, 20, supportsFinding: true, supportsPhotoTag: true, supportsSummary: true),
        Field($"{prefix}.coating.condition", "Coating Condition", "API 510", "External", "Pressure Equipment", subtype, "Coating / Insulation", "Equipment", "select", false, 30, options: ["Acceptable", "Monitor", "Finding"], supportsFinding: true),
        Field($"{prefix}.insulation.condition", "Insulation Condition", "API 510", "External", "Pressure Equipment", subtype, "Coating / Insulation", "Equipment", "select", false, 40, options: ["Acceptable", "Monitor", "Finding", "Not Applicable"], supportsFinding: true),
        Field($"{prefix}.supports-foundation.condition", "Supports / Foundation Condition", "API 510", "External", "Pressure Equipment", subtype, "Supports / Attachments", "Equipment", "textarea", false, 50, supportsFinding: true, supportsSummary: true),
        Field($"{prefix}.cml-thickness.review-summary", "CML / Thickness Review Summary", "API 510", "External", "Pressure Equipment", subtype, "CML / Thickness Review", "Equipment", "textarea", false, 60, supportsFinding: true, supportsNdeRequest: true, supportsSummary: true),
        Field($"{prefix}.component.condition", "Component Condition", "API 510", "External", "Pressure Equipment", subtype, "Component Condition", "Component", "select", false, 70, options: ["Acceptable", "Monitor", "Issue"], supportsFinding: true),
        Field($"{prefix}.finding.notes", "Finding Notes", "API 510", "External", "Pressure Equipment", subtype, "Findings", "Component", "textarea", false, 80, supportsFinding: true, supportsSummary: true),
        Field($"{prefix}.recommendation.text", "Recommendation Text", "API 510", "External", "Pressure Equipment", subtype, "Recommendations", "Component", "textarea", false, 90, supportsRecommendation: true),
        Field($"{prefix}.photo.reference-tag", "Photo Reference / Picture Tag", "API 510", "External", "Pressure Equipment", subtype, "Photos", "Component", "text", false, 100, supportsPhotoTag: true),
        Field($"{prefix}.external.summary", "External Summary", "API 510", "External", "Pressure Equipment", subtype, "Return to Service", "Equipment", "textarea", true, 110, supportsRecommendation: true, supportsSummary: true)
    ];

    private static InspectionFieldDefinition Field(string tag, string label, string standard, string scope, string family, string subtype, string group, string component, string type, bool required, int order, IReadOnlyList<string>? options = null, bool supportsFinding = false, bool supportsRecommendation = false, bool supportsRepairRequired = false, bool supportsPhotoTag = false, bool supportsSummary = false, bool supportsNdeRequest = false, bool isFutureOnly = false, string? reviewNotes = null)
        => new() { FieldTag = tag, Label = label, Standard = standard, InspectionScope = scope, EquipmentFamily = family, EquipmentSubtype = subtype, SectionGroup = group, ComponentType = component, DataType = type, Required = required, DefaultLayoutOrder = order, Options = options ?? [], SupportsFinding = supportsFinding, SupportsRecommendation = supportsRecommendation, SupportsRepairRequired = supportsRepairRequired, SupportsPhotoTag = supportsPhotoTag, SupportsSummary = supportsSummary, SupportsNdeRequest = supportsNdeRequest, WordExportGroup = $"{standard} | {scope} | {family} | {subtype}", ReviewNotes = reviewNotes, IsFutureOnly = isFutureOnly };
}
