namespace iE.Core.Reports;

public static class ReportingSeedData
{
    private static readonly string[] InspectionStatusOptions = ["No Issue", "Issue", "N/A"];

    private static readonly List<ReportTemplate> Templates =
    [
        CreateApi510Template(),
        CreateApi570Template(),
        CreateApi653Template()
    ];

    public static IReadOnlyList<ReportTemplate> GetTemplates() => Templates;

    public static ReportTemplate? GetTemplateById(string id) =>
        Templates.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase));

    private static ReportTemplate CreateApi510Template()
    {
        return new ReportTemplate
        {
            Id = "std-api-510-external-pressure-vessel-inspection",
            Name = "Standard API 510 External Pressure Vessel Inspection",
            Standard = "API 510",
            EquipmentType = "Pressure Vessel",
            Description = "Checklist-driven external pressure vessel inspection template for API 510 inspection reporting.",
            Sections =
            [
                new ReportTemplateSection
                {
                    Id = "general-information",
                    Title = "General Information",
                    Order = 1,
                    Fields =
                    [
                        new ReportTemplateField { Id = "asset-id", Label = "Asset ID", DataType = "text", IsRequired = true },
                        new ReportTemplateField { Id = "inspection-date", Label = "Inspection Date", DataType = "date", IsRequired = true },
                        new ReportTemplateField { Id = "inspector-name", Label = "Inspector Name", DataType = "text", IsRequired = true },
                        new ReportTemplateField
                        {
                            Id = "inspection-type",
                            Label = "Inspection Type",
                            DataType = "select",
                            DefaultValue = "External Visual",
                            Options = ["External Visual", "On-Stream", "Follow-up Inspection", "Repair Verification"]
                        },
                        new ReportTemplateField { Id = "summary", Label = "Inspection Summary", DataType = "textarea" }
                    ]
                }
            ]
        };
    }

    private static ReportTemplate CreateApi570Template()
    {
        return new ReportTemplate
        {
            Id = "std-api-570-external-piping-inspection",
            Name = "Standard API 570 External Piping Inspection",
            Standard = "API 570",
            EquipmentType = "Piping",
            Description = "Checklist-driven external piping inspection template for API 570 inspection reporting.",
            Sections =
            [
                new ReportTemplateSection
                {
                    Id = "general-information",
                    Title = "General Information",
                    Order = 1,
                    Fields =
                    [
                        new ReportTemplateField { Id = "unit", Label = "Unit", DataType = "text" },
                        new ReportTemplateField { Id = "system-id", Label = "System ID", DataType = "text" },
                        new ReportTemplateField { Id = "circuit-id", Label = "Circuit ID", DataType = "text" },
                        new ReportTemplateField { Id = "service", Label = "Service", DataType = "text" },
                        new ReportTemplateField { Id = "inspection-date", Label = "Inspection Date", DataType = "date" },
                        new ReportTemplateField { Id = "inspector-name", Label = "Inspector Name", DataType = "text" },
                        new ReportTemplateField { Id = "inspector-api-number", Label = "Inspector API Number", DataType = "text" },
                        new ReportTemplateField { Id = "reviewer-name", Label = "Reviewer Name", DataType = "text" },
                        new ReportTemplateField
                        {
                            Id = "inspection-type",
                            Label = "Inspection Type",
                            DataType = "select",
                            Options = ["External Visual", "CUI Screening", "Follow-up Inspection", "Repair Verification", "Baseline Inspection"]
                        },
                        new ReportTemplateField
                        {
                            Id = "inspection-standard",
                            Label = "Inspection Standard",
                            DataType = "text",
                            DefaultValue = "API 570"
                        }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "inspection-scope",
                    Title = "Inspection Scope",
                    Order = 2,
                    Fields =
                    [
                        new ReportTemplateField
                        {
                            Id = "inspection-scope-selection",
                            Label = "Inspection Scope Selection",
                            DataType = "multiselect",
                            Options =
                            [
                                "External Visual Inspection",
                                "CUI Screening",
                                "Support Inspection",
                                "Coating Inspection",
                                "Insulation Inspection",
                                "Follow-up Inspection",
                                "Repair Verification"
                            ],
                            HelpText = "Select all scope areas that apply for this inspection."
                        }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "summary-input",
                    Title = "Summary Input",
                    Order = 3,
                    Fields =
                    [
                        new ReportTemplateField { Id = "system-description", Label = "System Description", DataType = "textarea" },
                        new ReportTemplateField { Id = "circuit-count", Label = "Circuit Count", DataType = "number" },
                        new ReportTemplateField { Id = "material-metallurgy-summary", Label = "Material / Metallurgy Summary", DataType = "textarea" },
                        new ReportTemplateField { Id = "approximate-system-length", Label = "Approximate System Length", DataType = "text" },
                        new ReportTemplateField { Id = "insulated-system-description", Label = "Insulated System Description", DataType = "textarea" },
                        new ReportTemplateField { Id = "operating-temperature-range", Label = "Operating Temperature Range", DataType = "text" },
                        new ReportTemplateField { Id = "inspection-limitations", Label = "Inspection Limitations", DataType = "textarea" },
                        new ReportTemplateField { Id = "overall-inspection-summary", Label = "Overall Inspection Summary", DataType = "textarea" }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "recommendations",
                    Title = "Recommendations",
                    Order = 4,
                    Fields =
                    [
                        new ReportTemplateField { Id = "no-recommendations-required", Label = "No Recommendations Required", DataType = "boolean" },
                        new ReportTemplateField { Id = "recommendation-summary", Label = "Recommendation Summary", DataType = "textarea" },
                        new ReportTemplateField { Id = "recommended-actions", Label = "Recommended Actions", DataType = "textarea" },
                        new ReportTemplateField { Id = "repair-work-order", Label = "Repair Work Order / IWR / WSA Number", DataType = "text" },
                        new ReportTemplateField { Id = "follow-up-nde-required", Label = "Follow-up NDE Required", DataType = "boolean" },
                        new ReportTemplateField { Id = "next-inspection-consideration", Label = "Next Inspection Consideration", DataType = "textarea" }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "general-checklist",
                    Title = "General Checklist",
                    Order = 5,
                    Fields =
                    [
                        CreateChecklistField("active-process-leaks", "Active Process Leaks"),
                        CreateChecklistField("leak-repair-devices-clamps", "Leak Repair Devices / Clamps"),
                        CreateChecklistField("pipe-cracks-or-corrosion", "Pipe Cracks or Corrosion"),
                        CreateChecklistField("dead-legs", "Dead Legs", "Sections of piping with little or no flow"),
                        CreateChecklistField("long-horizontal-runs-over-100-ft", "Long Horizontal Runs Over 100 ft"),
                        CreateChecklistField("abnormal-thermal-expansion-deformation", "Abnormal Thermal Expansion / Deformation"),
                        CreateChecklistField("vibration-overhung-weight", "Vibration / Overhung Weight"),
                        CreateChecklistField("cantilevered-branches-vents-drains", "Cantilevered Branches / Vents / Drains"),
                        CreateChecklistField("piping-misalignment-restricted-movement", "Piping Misalignment / Restricted Movement"),
                        CreateChecklistField("bolting-inadequate-thread-engagement", "Bolting with Inadequate Thread Engagement"),
                        CreateChecklistField("counterbalance-condition", "Counterbalance Condition"),
                        CreateChecklistField("soil-to-air-interface", "Soil-to-Air Interface"),
                        CreateChecklistField("weld-haz-corrosion-damage", "Weld or Heat-Affected Zone Corrosion or Damage"),
                        CreateChecklistField("flange-corrosion-damage", "Flange Corrosion or Damage"),
                        CreateChecklistField("other-general-finding", "Other General Finding")
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "insulation-fireproofing-checklist",
                    Title = "Insulation / Fireproofing Checklist",
                    Order = 6,
                    Fields =
                    [
                        CreateChecklistField("insulated-piping-condition", "Insulated Piping Condition"),
                        CreateChecklistField("approximate-percent-insulated", "Approximate Percent Insulated"),
                        CreateChecklistField("insulation-type-identified", "Insulation Type Identified"),
                        CreateChecklistField("cui-temperature-range", "Circuit Operates Within CUI Temperature Range", "Corrosion Under Insulation risk area"),
                        CreateChecklistField("sweating-service", "Sweating Service"),
                        CreateChecklistField("steam-traced", "Steam Traced"),
                        CreateChecklistField("evidence-corrosion-under-insulation", "Evidence of Corrosion Under Insulation"),
                        CreateChecklistField("challenge-need-for-insulation", "Challenge Need for Insulation"),
                        CreateChecklistField("fireproofing-condition", "Fireproofing Condition"),
                        CreateChecklistField("other-insulation-fireproofing-finding", "Other Insulation / Fireproofing Finding")
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "coatings-checklist",
                    Title = "Coatings Checklist",
                    Order = 7,
                    Fields =
                    [
                        CreateChecklistField("coating-condition", "Coating Condition"),
                        CreateChecklistField("coating-damage", "Coating Damage"),
                        CreateChecklistField("coating-failure", "Coating Failure"),
                        CreateChecklistField("lead-containing-coating-identified", "Lead-Containing Coating Identified"),
                        CreateChecklistField("other-coating-finding", "Other Coating Finding")
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "supports-checklist",
                    Title = "Supports Checklist",
                    Order = 8,
                    Fields =
                    [
                        CreateChecklistField("loose-support-fretting-wear", "Loose Support Causing Fretting / Metal Wear"),
                        CreateChecklistField("pipe-or-shoe-off-supports", "Pipe or Shoe Off Supports"),
                        CreateChecklistField("support-hanger-brace-condition", "Support / Hanger / Brace Condition"),
                        CreateChecklistField("corrosion-at-supports", "Corrosion at Supports / Hangers / Braces"),
                        CreateChecklistField("bottomed-out-spring-hangers", "Bottomed-Out Spring Hangers"),
                        CreateChecklistField("support-bolting-condition", "Support Bolting Condition"),
                        CreateChecklistField("missing-u-bolts-restraints", "Missing U-Bolts / Restraints"),
                        CreateChecklistField("other-support-finding", "Other Support Finding")
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "component-finding-details",
                    Title = "Component / Finding Details",
                    Order = 9,
                    IsRepeatable = true,
                    Fields =
                    [
                        new ReportTemplateField { Id = "component-location", Label = "Component / Location", DataType = "text" },
                        new ReportTemplateField { Id = "component-circuit-location", Label = "Component / Circuit / Location (Legacy)", DataType = "text", HelpText = "Legacy field retained for backward compatibility." },
                        new ReportTemplateField
                        {
                            Id = "component-type",
                            Label = "Component Type",
                            DataType = "select",
                            Options = ["Pipe", "Elbow", "Flange", "Valve", "Support", "Branch", "Dead Leg", "Other"],
                            HelpText = "Dead Leg: Sections of piping with little or no flow."
                        },
                        new ReportTemplateField { Id = "finding-type", Label = "Finding Type", DataType = "text" },
                        new ReportTemplateField { Id = "associated-checklist-item", Label = "Associated Checklist Item", DataType = "text" },
                        new ReportTemplateField { Id = "detailed-description", Label = "Detailed Description", DataType = "textarea" },
                        new ReportTemplateField { Id = "detailed-finding-description", Label = "Detailed Finding Description (Legacy)", DataType = "textarea", HelpText = "Legacy field retained for backward compatibility." },
                        new ReportTemplateField
                        {
                            Id = "severity",
                            Label = "Severity",
                            DataType = "select",
                            Options = ["Low", "Moderate", "Severe"]
                        },
                        new ReportTemplateField { Id = "photo-numbers", Label = "Photo Numbers (Legacy)", DataType = "text", HelpText = "Legacy field retained for backward compatibility." },
                        new ReportTemplateField { Id = "recommendation-required", Label = "Recommendation Required", DataType = "boolean" },
                        new ReportTemplateField { Id = "recommendation-text", Label = "Recommendation Text", DataType = "textarea" }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "photos",
                    Title = "Photos",
                    Order = 10,
                    IsRepeatable = true,
                    Fields =
                    [
                        new ReportTemplateField { Id = "photo-number", Label = "Photo Number", DataType = "text" },
                        new ReportTemplateField { Id = "photo-description", Label = "Photo Description", DataType = "textarea" },
                        new ReportTemplateField { Id = "related-component", Label = "Related Component", DataType = "text" },
                        new ReportTemplateField { Id = "related-component-location", Label = "Related Component / Location (Legacy)", DataType = "text", HelpText = "Legacy field retained for backward compatibility." },
                        new ReportTemplateField { Id = "related-checklist-item", Label = "Related Checklist Item", DataType = "text" },
                        new ReportTemplateField { Id = "photo-required", Label = "Photo Required", DataType = "boolean" },
                        new ReportTemplateField { Id = "photo-attached", Label = "Photo Attached", DataType = "boolean" }
                    ]
                }
            ]
        };
    }

    private static ReportTemplateField CreateChecklistField(string id, string label, string? helpText = null)
    {
        return new ReportTemplateField
        {
            Id = id,
            Label = label,
            DataType = "inspection-status",
            IsChecklistItem = true,
            AllowsComment = true,
            AllowsPhotoFlag = true,
            AllowsTransferToComponentSection = true,
            AllowsRecommendationFlag = true,
            HelpText = helpText,
            Options = [..InspectionStatusOptions]
        };
    }

    private static ReportTemplate CreateApi653Template()
    {
        return new ReportTemplate
        {
            Id = "std-api-653-external-tank-inspection",
            Name = "Standard API 653 External Tank Inspection",
            Standard = "API 653",
            EquipmentType = "Storage Tank",
            Description = "Checklist-driven external storage tank inspection template for API 653 inspection reporting.",
            Sections =
            [
                new ReportTemplateSection
                {
                    Id = "general-information",
                    Title = "General Information",
                    Order = 1,
                    Fields =
                    [
                        new ReportTemplateField { Id = "tank-id", Label = "Tank ID", DataType = "text", IsRequired = true },
                        new ReportTemplateField { Id = "inspection-date", Label = "Inspection Date", DataType = "date", IsRequired = true },
                        new ReportTemplateField { Id = "inspector-name", Label = "Inspector Name", DataType = "text", IsRequired = true },
                        new ReportTemplateField
                        {
                            Id = "inspection-type",
                            Label = "Inspection Type",
                            DataType = "select",
                            DefaultValue = "External Visual",
                            Options = ["External Visual", "CML Review", "Follow-up Inspection", "Repair Verification"]
                        },
                        new ReportTemplateField { Id = "summary", Label = "Inspection Summary", DataType = "textarea" }
                    ]
                },
                new ReportTemplateSection
                {
                    Id = "external-checklist",
                    Title = "External Checklist",
                    Order = 2,
                    Fields =
                    [
                        CreateChecklistField("shell-condition", "Shell Condition"),
                        CreateChecklistField("roof-condition", "Roof Condition"),
                        CreateChecklistField("bottom-edge-corrosion", "Bottom Edge Corrosion"),
                        CreateChecklistField("foundation-ringwall-condition", "Foundation / Ringwall Condition"),
                        CreateChecklistField("nozzle-manway-condition", "Nozzle / Manway Condition"),
                        CreateChecklistField("coating-condition", "Coating Condition"),
                        CreateChecklistField("leaks-emissions", "Leaks / Emissions"),
                        CreateChecklistField("external-appurtenances", "External Appurtenances (Ladders, Platforms, Stairs)")
                    ]
                }
            ]
        };
    }
}
