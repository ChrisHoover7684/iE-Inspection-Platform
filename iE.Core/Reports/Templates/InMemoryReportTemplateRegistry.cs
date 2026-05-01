using iE.Core.Reports;

namespace iE.Core.Reports.Templates;

public class InMemoryReportTemplateRegistry : IReportTemplateRegistry
{
    private static readonly List<ReportTemplate> Templates =
    [
        CreateApi510VesselInternal(),
        CreateApi510VesselExternal(),
        CreateApi570PipingCui(),
        CreateStiSp001TankExternal()
    ];

    public IReadOnlyList<ReportTemplate> GetTemplates() => Templates;

    public ReportTemplate? GetTemplateById(string templateId) =>
        Templates.FirstOrDefault(template =>
            string.Equals(template.Id, templateId, StringComparison.OrdinalIgnoreCase));

    private static ReportTemplate CreateApi510VesselInternal() =>
        CreateTemplate(
            id: "api-510-vessel-internal",
            name: "API 510 Vessel Internal Inspection",
            standard: "API 510",
            equipmentType: "Pressure Vessel",
            description: "Internal pressure vessel inspection template for API 510 compliance.");

    private static ReportTemplate CreateApi510VesselExternal() =>
        CreateTemplate(
            id: "api-510-vessel-external",
            name: "API 510 Vessel External Inspection",
            standard: "API 510",
            equipmentType: "Pressure Vessel",
            description: "External pressure vessel inspection template for API 510 compliance.");

    private static ReportTemplate CreateApi570PipingCui() =>
        CreateTemplate(
            id: "api-570-piping-cui",
            name: "API 570 Piping CUI Inspection",
            standard: "API 570",
            equipmentType: "Piping",
            description: "Piping corrosion under insulation inspection template for API 570 inspections.");

    private static ReportTemplate CreateStiSp001TankExternal() =>
        CreateTemplate(
            id: "sti-sp001-tank-external",
            name: "STI SP001 Tank External Inspection",
            standard: "STI SP001",
            equipmentType: "Storage Tank",
            description: "External aboveground storage tank inspection template aligned to STI SP001.");

    private static ReportTemplate CreateTemplate(string id, string name, string standard, string equipmentType, string description)
    {
        return new ReportTemplate
        {
            Id = id,
            Name = name,
            Standard = standard,
            EquipmentType = equipmentType,
            Description = description,
            Sections =
            [
                CreateSection("summary", "Summary", 1, false, [
                    CreateField("inspection-date", "Inspection Date", "date", true),
                    CreateField("inspector", "Inspector", "text", true),
                    CreateField("asset-tag", "Asset Tag", "text", true),
                    CreateField("overall-summary", "Overall Summary", "textarea")
                ]),
                CreateSection("scope-preparation", "Scope / Preparation", 2, false, [
                    CreateField("inspection-scope", "Inspection Scope", "textarea", true),
                    CreateField("isolation-cleaning", "Isolation / Cleaning Complete", "boolean"),
                    CreateField("permits-safety", "Permits and Safety Controls", "textarea")
                ]),
                CreateSection("inspection-results", "Inspection Results", 3, false, [
                    CreateField("condition-summary", "Condition Summary", "textarea"),
                    CreateField("thickness-trends", "Thickness / Degradation Trends", "textarea"),
                    CreateField("service-impact", "Service Impact", "textarea")
                ]),
                CreateSection("findings", "Findings", 4, true, [
                    CreateField("finding-title", "Finding", "text", true),
                    CreateField("finding-location", "Location", "text"),
                    CreateField("finding-severity", "Severity", "select", options: ["Low", "Medium", "High", "Critical"]),
                    CreateField("finding-description", "Description", "textarea")
                ]),
                CreateSection("nde-testing", "NDE / Testing", 5, true, [
                    CreateField("test-method", "Method", "select", options: ["UT", "RT", "PT", "MT", "VT", "Hydrotest", "Other"]),
                    CreateField("test-area", "Area / Component", "text"),
                    CreateField("test-result", "Result", "textarea"),
                    CreateField("test-acceptance", "Acceptance Criteria Met", "boolean")
                ]),
                CreateSection("repairs", "Repairs", 6, true, [
                    CreateField("repair-description", "Repair Description", "textarea"),
                    CreateField("repair-date", "Repair Date", "date"),
                    CreateField("repair-work-order", "Work Order", "text"),
                    CreateField("repair-verified", "Repair Verified", "boolean")
                ]),
                CreateSection("recommendations", "Recommendations", 7, true, [
                    CreateField("recommendation", "Recommendation", "textarea", true),
                    CreateField("recommended-by", "Recommended By", "text"),
                    CreateField("target-date", "Target Date", "date"),
                    CreateField("priority", "Priority", "select", options: ["Routine", "Priority", "Immediate"])
                ]),
                CreateSection("photos", "Photos", 8, true, [
                    CreateField("photo-reference", "Photo Reference", "text"),
                    CreateField("photo-caption", "Caption", "textarea"),
                    CreateField("photo-location", "Location", "text"),
                    CreateField("photo-linked-finding", "Linked Finding", "text")
                ])
            ]
        };
    }

    private static ReportTemplateSection CreateSection(
        string id,
        string title,
        int order,
        bool isRepeatable,
        List<ReportTemplateField> fields) =>
        new()
        {
            Id = id,
            Title = title,
            Order = order,
            IsRepeatable = isRepeatable,
            Fields = fields
        };

    private static ReportTemplateField CreateField(
        string id,
        string label,
        string dataType,
        bool isRequired = false,
        List<string>? options = null) =>
        new()
        {
            Id = id,
            Label = label,
            DataType = dataType,
            IsRequired = isRequired,
            Options = options ?? []
        };
}
