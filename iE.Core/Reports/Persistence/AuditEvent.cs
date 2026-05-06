namespace iE.Core.Reports.Persistence;

public class AuditEvent
{
    public string AuditEventId { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
    public string? TenantId { get; set; }
    public string? FacilityId { get; set; }
    public string? ActorUserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public string? ResourceId { get; set; }
    public string Result { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }
    public string? ClientIp { get; set; }
    public string? UserAgent { get; set; }
    public string? MetadataJson { get; set; }
}
