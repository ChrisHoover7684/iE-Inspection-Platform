namespace iE.Core.Reports.Templates;

internal static class ExternalInspectionTemplates
{
    public static IReadOnlyList<ReportTemplate> Build() =>
    [
        CreateApi510VesselExternal(),
        CreateApi570PipingExternal(),
        CreateApi570PipingCuiExternal(),
        CreateStiSp001TankExternal()
    ];

    private static ReportTemplate CreateApi510VesselExternal() => new()
    {
        Id = "api-510-vessel-external",
        Name = "API 510 Vessel External Inspection",
        Standard = "API 510",
        EquipmentType = "Pressure Vessel",
        Description = "External visual inspection template for pressure vessels.",
        Sections =
        [
            CommonTemplateSections.ReportHeader(1, "Equipment Tag"),
            CommonTemplateSections.InspectionContext(2),
            CommonTemplateSections.ScopePreparation(3),
            CommonTemplateSections.CreateSection("external-inspection", "External Inspection", 4, false,
                CommonTemplateSections.Field("external-corrosion", "External Corrosion Observed", "boolean"),
                CommonTemplateSections.Field("leaks-staining", "Leaks or Staining Observed", "boolean"),
                CommonTemplateSections.Field("external-summary", "External Visual Summary", "textarea", true)),
            CommonTemplateSections.CreateSection("component-condition", "Component Condition", 5, true,
                CommonTemplateSections.Field("shell-condition", "Shell External Condition", "select", true, null, "Good", "Monitor", "Repair Needed"),
                CommonTemplateSections.Field("head-condition", "Head External Condition", "select", true, null, "Good", "Monitor", "Repair Needed"),
                CommonTemplateSections.Field("nozzle-condition", "Nozzle Condition", "select", false, null, "Good", "Monitor", "Repair Needed"),
                CommonTemplateSections.Field("support-condition", "Support/Skirt/Saddle/Leg Condition", "select", false, null, "Good", "Monitor", "Repair Needed"),
                CommonTemplateSections.Field("platform-ladder-condition", "Platform/Ladder/Attachment Condition", "select", false, null, "Good", "Monitor", "Repair Needed")),
            CommonTemplateSections.CreateSection("coating-insulation-condition", "Coating / Insulation Condition", 6, false,
                CommonTemplateSections.Field("insulation-coating-condition", "Insulation/Coating Condition", "textarea"),
                CommonTemplateSections.Field("grounding-condition", "Grounding Condition", "textarea", false, "Use for assets requiring grounding verification.")),
            CommonTemplateSections.CreateSection("thickness-cml-review", "Thickness / CML Review", 7, false,
                CommonTemplateSections.Field("cml-review-summary", "CML/Thickness Review Summary", "textarea")),
            CommonTemplateSections.Findings(8),
            CommonTemplateSections.NdeTesting(9),
            CommonTemplateSections.RepairsPerformed(10),
            CommonTemplateSections.Recommendations(11),
            CommonTemplateSections.Photos(12),
            CommonTemplateSections.ReturnToService(13)
        ]
    };

    private static ReportTemplate CreateApi570PipingExternal() => new()
    {
        Id = "api-570-piping-external",
        Name = "API 570 Piping External Inspection",
        Standard = "API 570",
        EquipmentType = "Piping",
        Description = "External visual piping inspection template.",
        Sections = BuildApi570Sections(false)
    };

    private static ReportTemplate CreateApi570PipingCuiExternal() => new()
    {
        Id = "api-570-piping-cui-external",
        Name = "API 570 Piping CUI External Inspection",
        Standard = "API 570",
        EquipmentType = "Piping",
        Description = "External piping inspection template with CUI-focused prompts.",
        Sections = BuildApi570Sections(true)
    };

    private static List<ReportTemplateSection> BuildApi570Sections(bool cuiFocused)
    {
        var externalSummary = cuiFocused
            ? "CUI-focused external inspection summary"
            : "External inspection summary";

        return
        [
            CommonTemplateSections.ReportHeader(1, "Line Number(s)"),
            CommonTemplateSections.InspectionContext(2),
            CommonTemplateSections.ScopePreparation(3),
            CommonTemplateSections.CreateSection("external-inspection", "External Inspection", 4, false,
                CommonTemplateSections.Field("circuit", "Circuit", "text", true),
                CommonTemplateSections.Field("piping-class", "Piping Class", "text"),
                CommonTemplateSections.Field("nps", "NPS", "text"),
                CommonTemplateSections.Field("insulated", "Insulated Status", "select", true, null, "Insulated", "Uninsulated", "Partially Insulated"),
                CommonTemplateSections.Field("external-corrosion", "External Corrosion", "textarea"),
                CommonTemplateSections.Field("supports-hangers-condition", "Supports/Hangers Condition", "textarea"),
                CommonTemplateSections.Field("deadlegs-injection-points", "Deadlegs/Injection Points Observed Externally", "textarea"),
                CommonTemplateSections.Field("approximate-feet-findings", "Approximate Feet of Findings", "number"),
                CommonTemplateSections.Field("external-summary", "External Summary", "textarea", true, externalSummary)),
            CommonTemplateSections.CreateSection("coating-insulation-condition", "Coating / Insulation Condition", 5, false,
                CommonTemplateSections.Field("coating-condition", "Coating Condition", "textarea"),
                CommonTemplateSections.Field("insulation-condition", "Insulation Condition", "textarea"),
                CommonTemplateSections.Field("cui-concern", "CUI Concern", "select", false, null, "Low", "Moderate", "High")),
            CommonTemplateSections.CreateSection("thickness-cml-review", "Thickness / CML Review", 6, false,
                CommonTemplateSections.Field("thickness-review-summary", "Thickness Review Summary", "textarea")),
            CommonTemplateSections.Findings(7),
            CommonTemplateSections.NdeTesting(8),
            CommonTemplateSections.RepairsPerformed(9),
            CommonTemplateSections.Recommendations(10),
            CommonTemplateSections.Photos(11),
            CommonTemplateSections.ReturnToService(12)
        ];
    }

    private static ReportTemplate CreateStiSp001TankExternal() => new()
    {
        Id = "sti-sp001-tank-external",
        Name = "STI SP001 Tank External Inspection",
        Standard = "STI SP001",
        EquipmentType = "Storage Tank",
        Description = "External visual inspection template for aboveground storage tanks.",
        Sections =
        [
            CommonTemplateSections.ReportHeader(1, "Tank ID"),
            CommonTemplateSections.InspectionContext(2),
            CommonTemplateSections.ScopePreparation(3),
            CommonTemplateSections.CreateSection("external-inspection", "External Inspection", 4, false,
                CommonTemplateSections.Field("tank-type", "Tank Type", "text"),
                CommonTemplateSections.Field("capacity", "Capacity", "number"),
                CommonTemplateSections.Field("foundation-condition", "Foundation Condition", "textarea"),
                CommonTemplateSections.Field("shell-condition", "Shell Condition", "textarea"),
                CommonTemplateSections.Field("roof-condition", "Roof Condition", "textarea"),
                CommonTemplateSections.Field("appurtenances", "Appurtenances", "textarea"),
                CommonTemplateSections.Field("vents", "Vents", "textarea"),
                CommonTemplateSections.Field("coating-condition", "Coating Condition", "textarea"),
                CommonTemplateSections.Field("containment-release-prevention", "Containment/Release Prevention", "textarea"),
                CommonTemplateSections.Field("anchor-bolts-grounding", "Anchor Bolts/Grounding", "textarea"),
                CommonTemplateSections.Field("stairs-platforms-ladders", "Stairs/Platforms/Ladders", "textarea"),
                CommonTemplateSections.Field("external-leaks-staining", "External Leaks/Staining", "textarea")),
            CommonTemplateSections.Findings(5),
            CommonTemplateSections.NdeTesting(6),
            CommonTemplateSections.RepairsPerformed(7),
            CommonTemplateSections.Recommendations(8),
            CommonTemplateSections.Photos(9),
            CommonTemplateSections.ReturnToService(10)
        ]
    };
}
