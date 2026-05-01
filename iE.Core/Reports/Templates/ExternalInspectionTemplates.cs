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
                CommonTemplateSections.Field("external-corrosion", "External Corrosion Observed", "boolean", false, "If yes, describe area, extent, and estimated metal loss in findings."),
                CommonTemplateSections.Field("leaks-staining", "Leaks or Staining Observed", "boolean", false, "Capture weeps, drips, active leaks, or dried staining at nozzles and attachments."),
                CommonTemplateSections.Field("hot-spots-distortion", "Hot Spots / Bulging / Distortion", "textarea", false, "Note discoloration, bulges, flat spots, dents, or vibration-related movement."),
                CommonTemplateSections.Field("nameplate-marking-condition", "Nameplate / Marking Condition", "textarea", false, "Confirm tag/nameplate legibility and any discrepancies with records."),
                CommonTemplateSections.Field("external-summary", "External Visual Summary", "textarea", true, "Summarize vessel external condition, reportable damage locations, and immediate actions taken.")),
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

        var insulationConditionHelpText = cuiFocused
            ? "Capture jacketing breaches, seal failures, wet insulation indicators, and degraded insulation condition."
            : "Capture insulation/jacketing condition and any missing, damaged, or degraded areas.";

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
                CommonTemplateSections.Field("external-condition", "External Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("external-condition-notes", "External Condition Notes", "textarea", false, "Describe general condition concerns and notable areas inspected."),
                CommonTemplateSections.Field("active-leakage", "Active Leakage Observed", "boolean", true, "Capture active leaks, weeps, drips, or process staining at components and joints."),
                CommonTemplateSections.Field("active-leakage-notes", "Active Leakage Notes", "textarea"),
                CommonTemplateSections.Field("supports", "Supports Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("supports-notes", "Supports Notes", "textarea", false, "Include supports, hangers, shoes, guides, and anchors."),
                CommonTemplateSections.Field("brackets-attachments", "Brackets / Attachments Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("brackets-attachments-notes", "Brackets / Attachments Notes", "textarea"),
                CommonTemplateSections.Field("gaskets", "Gaskets Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("gaskets-notes", "Gaskets Notes", "textarea", false, "Document gasket extrusion, seepage, or visible degradation."),
                CommonTemplateSections.Field("bolting", "Bolting Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("bolting-notes", "Bolting Notes", "textarea", false, "Document loose, missing, damaged, or corroded bolting."),
                CommonTemplateSections.Field("flanges", "Flanges Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("flanges-notes", "Flanges Notes", "textarea"),
                CommonTemplateSections.Field("coating", "Coating Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("coating-notes", "Coating Notes", "textarea", false, "Record coating breakdown, blistering, peeling, or underfilm corrosion evidence."),
                CommonTemplateSections.Field("insulation-jacketing", "Insulation / Jacketing Condition", "select", true, null, "Acceptable", "Monitor", "Finding", "Not Applicable"),
                CommonTemplateSections.Field("insulation-jacketing-notes", "Insulation / Jacketing Notes", "textarea", false, insulationConditionHelpText),
                CommonTemplateSections.Field("mechanical-damage", "Mechanical Damage Observed", "boolean", true, "Capture dents, gouges, distortion, vibration damage, or impact marks."),
                CommonTemplateSections.Field("mechanical-damage-notes", "Mechanical Damage Notes", "textarea"),
                CommonTemplateSections.Field("external-corrosion", "External Corrosion Condition", "select", true, null, "Acceptable", "Monitor", "Finding"),
                CommonTemplateSections.Field("external-corrosion-notes", "External Corrosion Notes", "textarea", false, "Describe corrosion locations, extent, and affected components."),
                CommonTemplateSections.Field("deadlegs-injection-points", "Deadlegs/Injection Points Observed Externally", "textarea"),
                CommonTemplateSections.Field("small-bore-branch-condition", "Small Bore / Branch Connection Condition", "textarea", false, "Capture vibration cracking, corrosion at weldolets, and support adequacy."),
                CommonTemplateSections.Field("temporary-repairs-observed", "Temporary Repairs Observed", "textarea", false, "Identify clamps, wraps, or other temporary repairs and remaining serviceability concerns."),
                CommonTemplateSections.Field("approximate-feet-findings", "Approximate Feet of Findings", "number"),
                CommonTemplateSections.Field("external-summary", "External Summary", "textarea", true, externalSummary)),
            CommonTemplateSections.CreateSection("coating-insulation-condition", "Coating / Insulation Condition", 5, false,
                CommonTemplateSections.Field("coating-condition", "Coating Detail", "textarea"),
                CommonTemplateSections.Field("insulation-condition", "Insulation Detail", "textarea"),
                CommonTemplateSections.Field("insulation-jacketing-seal-condition", "Insulation Jacketing / Seal Condition", "textarea", false, "Record broken jacketing, open seams, wet insulation indicators, and damaged weather seals."),
                CommonTemplateSections.Field("wet-insulation-indicators", "Wet Insulation Indicators", "boolean", false, "Mark yes when moisture intrusion indicators are present (staining, corrosion products, dampness, ice)."),
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
                CommonTemplateSections.Field("shell-to-bottom-area-condition", "Shell-to-Bottom / Chime Area Condition", "textarea", false, "Document corrosion, coating breakdown, settlement gaps, and moisture hold-up areas."),
                CommonTemplateSections.Field("appurtenances", "Appurtenances", "textarea"),
                CommonTemplateSections.Field("vents", "Vents", "textarea"),
                CommonTemplateSections.Field("coating-condition", "Coating Condition", "textarea"),
                CommonTemplateSections.Field("containment-release-prevention", "Containment/Release Prevention", "textarea"),
                CommonTemplateSections.Field("overfill-spill-prevention", "Overfill / Spill Prevention", "textarea", false, "Verify alarms, gauges, shutoff devices, and spill controls are present and in serviceable condition."),
                CommonTemplateSections.Field("anchor-bolts-grounding", "Anchor Bolts/Grounding", "textarea"),
                CommonTemplateSections.Field("stairs-platforms-ladders", "Stairs/Platforms/Ladders", "textarea"),
                CommonTemplateSections.Field("external-leaks-staining", "External Leaks/Staining", "textarea"),
                CommonTemplateSections.Field("external-summary", "External Summary", "textarea", true, "Summarize tank external condition, observed deficiencies, and follow-up actions.")),
            CommonTemplateSections.Findings(5),
            CommonTemplateSections.NdeTesting(6),
            CommonTemplateSections.RepairsPerformed(7),
            CommonTemplateSections.Recommendations(8),
            CommonTemplateSections.Photos(9),
            CommonTemplateSections.ReturnToService(10)
        ]
    };
}
