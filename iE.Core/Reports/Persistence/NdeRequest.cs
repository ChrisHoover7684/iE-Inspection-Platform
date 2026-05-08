namespace iE.Core.Reports.Persistence;

public class NdeRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string FacilityId { get; set; } = string.Empty;
    public string? ProcessUnitId { get; set; }
    public string? AssetId { get; set; }
    public string? ReportId { get; set; }
    public string? RequestNumber { get; set; }
    public string NdeType { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string? RequestedByUserId { get; set; }
    public string? RequestedByExternalSubject { get; set; }
    public string? AssignedToName { get; set; }
    public string? VendorName { get; set; }
    public DateTime? DueDateUtc { get; set; }
    public DateTime? ScheduledAtUtc { get; set; }
    public DateTime? ResultsReceivedAtUtc { get; set; }
    public DateTime? ClosedAtUtc { get; set; }
    public DateTime? CanceledAtUtc { get; set; }
    public string? ResultsSummary { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
