namespace iE.Core.Reports.Domain;

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
    public List<ReportReviewHistory> ReviewHistory { get; set; } = new();
    public PipingInspectionProfile? PipingProfile { get; set; }
}
