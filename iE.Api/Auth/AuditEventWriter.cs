using System.Text.Json;
using iE.Api.Tenancy;
using iE.Core.Reports.Persistence;

namespace iE.Api.Auth;

public interface IAuditEventWriter
{
    Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default);
}

public sealed class AuditEventWriter(InspectionReportsDbContext dbContext, ITenantContextAccessor tenantContextAccessor, IHttpContextAccessor httpContextAccessor, ILogger<AuditEventWriter> logger) : IAuditEventWriter
{
    private static readonly HashSet<string> SensitiveKeys = new(StringComparer.OrdinalIgnoreCase) { "token", "authorization", "password", "connectionString", "rawPayload", "markupJson" };

    public async Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var tenant = tenantContextAccessor.Current;
            var http = httpContextAccessor.HttpContext;
            var sanitized = SanitizeMetadata(metadata);
            var evt = new AuditEvent
            {
                AuditEventId = Guid.NewGuid().ToString("N"),
                OccurredAtUtc = DateTime.UtcNow,
                TenantId = tenant.ClientOrganizationId?.ToString(),
                FacilityId = facilityId,
                ActorUserId = string.IsNullOrWhiteSpace(tenant.ExternalSubject) ? null : tenant.ExternalSubject,
                Action = action,
                ResourceType = resourceType,
                ResourceId = resourceId,
                Result = result,
                CorrelationId = http?.TraceIdentifier,
                ClientIp = http?.Connection.RemoteIpAddress?.ToString(),
                UserAgent = http?.Request.Headers.UserAgent.ToString(),
                MetadataJson = sanitized.Count == 0 ? null : JsonSerializer.Serialize(sanitized)
            };

            dbContext.AuditEvents.Add(evt);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to persist audit event for action {Action} and resource {ResourceType}/{ResourceId}", action, resourceType, resourceId);
        }
    }

    internal static Dictionary<string, object?> SanitizeMetadata(IDictionary<string, object?>? metadata)
    {
        var result = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        if (metadata is null) return result;
        foreach (var kvp in metadata)
        {
            if (SensitiveKeys.Contains(kvp.Key)) continue;
            result[kvp.Key] = kvp.Value;
        }

        return result;
    }
}
