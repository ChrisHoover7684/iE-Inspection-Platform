using System.Text.Json;
using System.Text.Json.Serialization;
using iE.Api.Tenancy;
using iE.Core.Reports.Persistence;

namespace iE.Api.Auth;

public interface IAuditEventWriter
{
    Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default);
}

public sealed class AuditEventWriter(InspectionReportsDbContext dbContext, ITenantContextAccessor tenantContextAccessor, IHttpContextAccessor httpContextAccessor, ILogger<AuditEventWriter> logger) : IAuditEventWriter
{
    private const int MaxMetadataLength = 4000;
    private const int MaxStringValueLength = 256;

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
                TenantId = TrimToLength(tenant.ClientOrganizationId?.ToString(), 64),
                FacilityId = TrimToLength(facilityId, 64),
                ActorUserId = TrimToLength(string.IsNullOrWhiteSpace(tenant.ExternalSubject) ? null : tenant.ExternalSubject, 128),
                Action = TrimToLength(action, 128) ?? string.Empty,
                ResourceType = TrimToLength(resourceType, 64) ?? string.Empty,
                ResourceId = TrimToLength(resourceId, 128),
                Result = TrimToLength(result, 32) ?? string.Empty,
                CorrelationId = TrimToLength(http?.TraceIdentifier, 128),
                ClientIp = TrimToLength(http?.Connection.RemoteIpAddress?.ToString(), 64),
                UserAgent = TrimToLength(http?.Request.Headers.UserAgent.ToString(), 512),
                MetadataJson = SerializeMetadataSafely(sanitized)
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
            if (AuditMetadataKeys.Sensitive.Contains(kvp.Key)) continue;
            result[kvp.Key] = SanitizeValue(kvp.Value);
        }

        return result;
    }

    private static object? SanitizeValue(object? value)
    {
        return value switch
        {
            null => null,
            string s => TrimToLength(s, MaxStringValueLength),
            IDictionary<string, object?> dict => SanitizeMetadata(dict),
            IDictionary<string, string> dictStr => SanitizeMetadata(dictStr.ToDictionary(k => k.Key, v => (object?)v.Value)),
            IEnumerable<object?> list => list.Select(SanitizeValue).ToList(),
            _ => value
        };
    }

    private static string? SerializeMetadataSafely(Dictionary<string, object?> sanitized)
    {
        if (sanitized.Count == 0) return null;
        try
        {
            var json = JsonSerializer.Serialize(sanitized, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            if (json.Length <= MaxMetadataLength) return json;
            var fallback = JsonSerializer.Serialize(new Dictionary<string, object?> { [AuditMetadataKeys.MetadataTruncated] = true });
            return fallback.Length <= MaxMetadataLength ? fallback : fallback[..MaxMetadataLength];
        }
        catch
        {
            return "{\"metadataTruncated\":true}";
        }
    }

    private static string? TrimToLength(string? value, int max)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        return value.Length <= max ? value : value[..max];
    }
}
