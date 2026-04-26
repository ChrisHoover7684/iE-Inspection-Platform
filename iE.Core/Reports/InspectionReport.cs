namespace iE.Core.Reports;

public static class InspectionReportStatuses
{
    public const string Draft = "Draft";
    public const string InReview = "InReview";
    public const string Final = "Final";
}

public class InspectionReport
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string FacilityId { get; set; } = string.Empty;
    public string? ProcessUnitId { get; set; }
    public string? AssetId { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? UpdatedByUserId { get; set; }
    public string TemplateId { get; set; } = string.Empty;
    public string ReportNumber { get; set; } = string.Empty;
    public string EquipmentTag { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string SystemId { get; set; } = string.Empty;
    public string CircuitId { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public string Status { get; set; } = InspectionReportStatuses.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public List<InspectionReportSection> Sections { get; set; } = new();
    public List<InspectionFinding> Findings { get; set; } = new();
    public List<InspectionPhoto> Photos { get; set; } = new();
}

public class InspectionReportSection
{
    public string SectionId { get; set; } = string.Empty;
    public string SectionTitle { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsRepeatable { get; set; }
    public int? InstanceNumber { get; set; }
    public List<InspectionReportAnswer> Answers { get; set; } = new();
}

public class InspectionReportAnswer
{
    public string FieldId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string DataType { get; set; } = "text";
    public string? Value { get; set; }
    public List<string> Values { get; set; } = new();
    public string? Comment { get; set; }
    public bool? PhotoRequired { get; set; }
    public bool? TransferToComponentSection { get; set; }
    public bool? RecommendationRequired { get; set; }
}

public class InspectionFinding
{
    public string Id { get; set; } = string.Empty;
    public string ComponentLocation { get; set; } = string.Empty;
    public string ComponentType { get; set; } = string.Empty;
    public string FindingType { get; set; } = string.Empty;
    public string AssociatedChecklistItem { get; set; } = string.Empty;
    public string DetailedDescription { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool RecommendationRequired { get; set; }
    public string? RecommendationText { get; set; }
    public List<string> PhotoIds { get; set; } = new();
}

public class InspectionPhoto
{
    public string Id { get; set; } = string.Empty;
    public string PhotoNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RelatedComponent { get; set; } = string.Empty;
    public string RelatedChecklistItem { get; set; } = string.Empty;
    public bool PhotoRequired { get; set; }
    public bool PhotoAttached { get; set; }
    public string? FileName { get; set; }
    public string? FileUrl { get; set; }
}
