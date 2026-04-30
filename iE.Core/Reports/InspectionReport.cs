namespace iE.Core.Reports;

public static class InspectionReportStatuses
{
    public const string Draft = "Draft";
    public const string InReview = "InReview";
    public const string Final = "Final";
}


public class PipingInspectionProfile
{
    public List<string> LineNumbers { get; set; } = new();
    // TODO: Remove after API clients migrate to LineNumbers/finding-level LineNumber.
    public string? LineNumber { get; set; }
    // TODO: Remove after API clients migrate to finding-level ApproximateFeetOfFindings.
    public double? ApproximateFeetOfFindings { get; set; }
    public string? UpstreamEquipment { get; set; }
    public string? DownstreamEquipment { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public string? NominalPipeSize { get; set; }
    public string? PipingClass { get; set; }
    public string? InsulatedStatus { get; set; }
}


public enum ObservationStatus
{
    Acceptable = 1,
    NoIssues = 2,
    NotInspected = 3
}

public class InspectionObservation
{
    public string Id { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public ObservationStatus Status { get; set; } = ObservationStatus.Acceptable;
    public string? Notes { get; set; }
    public List<string> PhotoIds { get; set; } = new();
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
    public List<InspectionObservation> Observations { get; set; } = new();
    public List<InspectionPhoto> Photos { get; set; } = new();
    public PipingInspectionProfile? PipingProfile { get; set; }
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
    public FindingType FindingType { get; set; } = FindingType.Other;
    public DamageMechanismType DamageMechanism { get; set; } = DamageMechanismType.None;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public FindingSeverity Severity { get; set; } = FindingSeverity.None;
    public bool RepairRequired { get; set; }
    public string? RepairRecommendation { get; set; }
    public string? ComponentType { get; set; }
    public string? AssociatedChecklistItem { get; set; }
    public double? PitDepth { get; set; }
    public bool IsLocalized { get; set; }
    public bool IsGeneral { get; set; }
    public List<string> PhotoIds { get; set; } = new();
    public string? NdeMethod { get; set; }
    public string? NdeResult { get; set; }
    public string? InsulationCondition { get; set; }
    public double? ThicknessResult { get; set; }
    public double? MinimumRequiredThickness { get; set; }
    public double? CorrosionAllowance { get; set; }
    public string? LineNumber { get; set; }
    public double? ApproximateFeetOfFindings { get; set; }
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
