namespace iE.Core.Reports.Templates;

internal static class CommonTemplateSections
{
    internal static ReportTemplateSection CreateSection(string id, string title, int order, bool isRepeatable, params ReportTemplateField[] fields) =>
        new()
        {
            Id = id,
            Title = title,
            Order = order,
            IsRepeatable = isRepeatable,
            Fields = fields.ToList()
        };

    internal static ReportTemplateField Field(
        string id,
        string label,
        string dataType,
        bool isRequired = false,
        string? helpText = null,
        params string[] options) =>
        new()
        {
            Id = id,
            Label = label,
            DataType = dataType,
            IsRequired = isRequired,
            HelpText = helpText,
            Options = options.ToList()
        };

    internal static ReportTemplateSection ReportHeader(int order, string assetLabel) =>
        CreateSection("report-header", "Report Header", order, false,
            Field("inspection-date", "Inspection Date", "date", true),
            Field("inspector", "Inspector", "text", true),
            Field("unit", "Unit", "text", true),
            Field("service", "Service", "text"),
            Field("asset-identifier", assetLabel, "text", true));

    internal static ReportTemplateSection InspectionContext(int order) =>
        CreateSection("inspection-context", "Inspection Context", order, false,
            Field("inspection-reason", "Inspection Reason", "select", true, null, "Scheduled", "On-stream concern", "Post-repair", "Pre-startup", "Other"),
            Field("operating-status", "Operating Status", "select", false, null, "In service", "Out of service", "Standby"),
            Field("process-conditions", "Observed Process Conditions", "textarea"));

    internal static ReportTemplateSection ScopePreparation(int order) =>
        CreateSection("scope-preparation", "Scope / Preparation", order, false,
            Field("inspection-scope", "Inspection Scope", "textarea", true),
            Field("accessing-scaffolding", "Access/Scaffolding Adequate", "boolean"),
            Field("surface-prep", "Surface Preparation Completed", "boolean"),
            Field("permits-controls", "Permits / Safety Controls", "textarea"));

    internal static ReportTemplateSection Findings(int order) =>
        CreateSection("findings", "Findings", order, true,
            Field("finding-type", "Finding Type", "select", true, null, "Corrosion", "Leak", "Coating", "Insulation", "Mechanical Damage", "Other"),
            Field("location", "Location", "text", true),
            Field("description", "Description", "textarea", true),
            Field("severity", "Severity", "select", false, null, "Low", "Medium", "High", "Critical"));

    internal static ReportTemplateSection NdeTesting(int order) =>
        CreateSection("nde-testing", "NDE / Testing", order, true,
            Field("nde-method", "NDE Method", "multiselect", false, null, "VT", "UT", "PAUT", "RT", "PT", "MT", "Other"),
            Field("test-location", "Test Location", "text"),
            Field("result-summary", "Result Summary", "textarea"),
            Field("acceptance-met", "Acceptance Criteria Met", "boolean"));

    internal static ReportTemplateSection RepairsPerformed(int order) =>
        CreateSection("repairs-performed", "Repairs Performed", order, true,
            Field("repair-action", "Repair Action", "textarea", true),
            Field("repair-reference", "Work Order / Package", "text"),
            Field("repair-date", "Repair Date", "date"),
            Field("repair-verified", "Repair Verified", "boolean"));

    internal static ReportTemplateSection Recommendations(int order) =>
        CreateSection("recommendations", "Recommendations", order, true,
            Field("recommendation", "Recommendation", "textarea", true),
            Field("priority", "Priority", "select", true, null, "Routine", "Priority", "Immediate"),
            Field("target-date", "Target Date", "date"));

    internal static ReportTemplateSection Photos(int order) =>
        CreateSection("photos", "Photos", order, true,
            Field("photo-reference", "Photo Reference", "text", true),
            Field("photo-caption", "Caption", "textarea"),
            Field("linked-location", "Linked Location", "text"));

    internal static ReportTemplateSection ReturnToService(int order) =>
        CreateSection("return-to-service", "Return to Service", order, false,
            Field("rts-approved", "Approved for Return to Service", "boolean"),
            Field("rts-conditions", "Return to Service Conditions", "textarea"),
            Field("rts-approved-by", "Approved By", "text"));

    internal static ReportTemplateSection RepairRecommendationHeader(int order, string assetLabel) =>
        CreateSection("recommendation-header", "Recommendation Header", order, false,
            Field("recommendation-number", "Recommendation Number", "text", true),
            Field("date-identified", "Date Identified", "date", true),
            Field("identified-by", "Identified By", "text", true),
            Field("asset-identifier", assetLabel, "text", true),
            Field("unit", "Unit", "text", true),
            Field("service", "Service", "text"));

    internal static ReportTemplateSection RepairBasis(int order) =>
        CreateSection("basis-justification", "Basis / Justification", order, false,
            Field("finding-type", "Finding Type", "select", true, null, "Corrosion", "Leak", "Mechanical Damage", "Coating Failure", "Deformation", "Other"),
            Field("damage-mechanism", "Damage Mechanism", "text"),
            Field("justification", "Justification", "textarea", true));

    internal static ReportTemplateSection CloseoutRequirements(int order) =>
        CreateSection("closeout-requirements", "Closeout Requirements", order, false,
            Field("engineering-review-required", "Engineering Review Required", "boolean"),
            Field("inspector-closeout-required", "Inspector Closeout Required", "boolean", true),
            Field("closeout-criteria", "Closeout Criteria", "textarea"));
}
