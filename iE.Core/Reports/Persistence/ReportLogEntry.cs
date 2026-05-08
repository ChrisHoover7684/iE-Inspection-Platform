namespace iE.Core.Reports.Persistence;

public class ReportLogEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string FacilityId { get; set; } = string.Empty;
    public string ReportId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string? EventStatus { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ActorUserId { get; set; }
    public string? ActorExternalSubject { get; set; }
    public string? FromStatus { get; set; }
    public string? ToStatus { get; set; }
    public string? RelatedNdeRequestId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string? MetadataJson { get; set; }
}
