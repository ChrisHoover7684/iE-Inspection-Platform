namespace iE.Core.Reports.Templates;

internal static class RepairRecommendationTemplates
{
    public static IReadOnlyList<ReportTemplate> Build() =>
    [
        Create("repair-recommendation-general", "General Repair Recommendation", "General", "Asset", "General repair recommendation template."),
        Create("repair-recommendation-api-510-vessel", "API 510 Vessel Repair Recommendation", "API 510", "Equipment Tag", "Repair recommendation template for pressure vessels."),
        Create("repair-recommendation-api-570-piping", "API 570 Piping Repair Recommendation", "API 570", "Line Number", "Repair recommendation template for piping."),
        Create("repair-recommendation-sti-sp001-tank", "STI SP001 Tank Repair Recommendation", "STI SP001", "Tank ID", "Repair recommendation template for aboveground storage tanks.")
    ];

    private static ReportTemplate Create(string id, string name, string standard, string assetLabel, string description) => new()
    {
        Id = id,
        Name = name,
        Standard = standard,
        EquipmentType = "Repair Recommendation",
        Description = description,
        Sections =
        [
            CommonTemplateSections.RepairRecommendationHeader(1, assetLabel),
            CommonTemplateSections.CreateSection("asset-location", "Asset / Location", 2, false,
                CommonTemplateSections.Field("location", "Location", "text", true),
                CommonTemplateSections.Field("system-boundary", "System Boundary/Description", "textarea")),
            CommonTemplateSections.CreateSection("condition-requiring-repair", "Condition Requiring Repair", 3, false,
                CommonTemplateSections.Field("description-condition", "Description of Condition", "textarea", true),
                CommonTemplateSections.Field("damage-mechanism", "Damage Mechanism (if known)", "text")),
            CommonTemplateSections.CreateSection("recommended-repair", "Recommended Repair", 4, false,
                CommonTemplateSections.Field("recommended-repair-action", "Recommended Repair Action", "textarea", true)),
            CommonTemplateSections.RepairBasis(5),
            CommonTemplateSections.CreateSection("nde-verification", "NDE / Verification Requirements", 6, false,
                CommonTemplateSections.Field("required-nde", "Required NDE", "multiselect", false, null, "VT", "UT", "RT", "PT", "MT", "Leak Test", "Pressure Test"),
                CommonTemplateSections.Field("pressure-leak-test-required", "Pressure/Leak Test Required", "boolean"),
                CommonTemplateSections.Field("verification-notes", "Verification Notes", "textarea")),
            CommonTemplateSections.CreateSection("priority-timing", "Priority / Timing", 7, false,
                CommonTemplateSections.Field("repair-priority", "Repair Priority", "select", true, null, "Immediate", "Priority", "Routine"),
                CommonTemplateSections.Field("target-completion-date", "Target Completion Date", "date")),
            CommonTemplateSections.CreateSection("work-order-references", "Work Order / WSA / IWR References", 8, false,
                CommonTemplateSections.Field("work-order-reference", "WSA/IWR/Work Order Reference", "text")),
            CommonTemplateSections.CreateSection("photos-attachments", "Photos / Attachments", 9, true,
                CommonTemplateSections.Field("photo-reference", "Photo Reference", "text"),
                CommonTemplateSections.Field("attachment-note", "Attachment Note", "textarea")),
            CommonTemplateSections.CloseoutRequirements(10)
        ]
    };
}
