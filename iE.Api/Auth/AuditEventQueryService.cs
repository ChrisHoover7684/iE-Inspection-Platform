using iE.Api.Tenancy;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Auth;

public sealed class AuditEventQuery
{
    public string? TenantId { get; init; }
    public string? FacilityId { get; init; }
    public string? ActorUserId { get; init; }
    public string? Action { get; init; }
    public string? ResourceType { get; init; }
    public string? ResourceId { get; init; }
    public string? Result { get; init; }
    public DateTime? OccurredFromUtc { get; init; }
    public DateTime? OccurredToUtc { get; init; }
    public int? Limit { get; init; }
}

public interface IAuditEventQueryService
{
    Task<IReadOnlyList<AuditEvent>> QueryAsync(AuditEventQuery query, CancellationToken cancellationToken = default);
}

public sealed class AuditEventQueryService(InspectionReportsDbContext dbContext, ITenantContextAccessor tenantContextAccessor, IConfiguration configuration) : IAuditEventQueryService
{
    private const int DefaultLimit = 100;
    private const int MaxLimit = 500;

    public async Task<IReadOnlyList<AuditEvent>> QueryAsync(AuditEventQuery query, CancellationToken cancellationToken = default)
    {
        var authEnabled = StartupConfiguration.IsAuthenticationEnabled(configuration);
        IQueryable<AuditEvent> rows = dbContext.AuditEvents.AsNoTracking();
        if (authEnabled)
        {
            var tenantId = tenantContextAccessor.Current.ClientOrganizationId?.ToString();
            if (string.IsNullOrWhiteSpace(tenantId)) return [];
            rows = rows.Where(x => x.TenantId == tenantId);
        }
        else if (!string.IsNullOrWhiteSpace(query.TenantId))
        {
            rows = rows.Where(x => x.TenantId == query.TenantId);
        }

        if (!string.IsNullOrWhiteSpace(query.FacilityId)) rows = rows.Where(x => x.FacilityId == query.FacilityId);
        if (!string.IsNullOrWhiteSpace(query.ActorUserId)) rows = rows.Where(x => x.ActorUserId == query.ActorUserId);
        if (!string.IsNullOrWhiteSpace(query.Action)) rows = rows.Where(x => x.Action == query.Action);
        if (!string.IsNullOrWhiteSpace(query.ResourceType)) rows = rows.Where(x => x.ResourceType == query.ResourceType);
        if (!string.IsNullOrWhiteSpace(query.ResourceId)) rows = rows.Where(x => x.ResourceId == query.ResourceId);
        if (!string.IsNullOrWhiteSpace(query.Result)) rows = rows.Where(x => x.Result == query.Result);
        if (query.OccurredFromUtc.HasValue) rows = rows.Where(x => x.OccurredAtUtc >= query.OccurredFromUtc.Value);
        if (query.OccurredToUtc.HasValue) rows = rows.Where(x => x.OccurredAtUtc <= query.OccurredToUtc.Value);

        var limit = Math.Clamp(query.Limit ?? DefaultLimit, 1, MaxLimit);
        var results = await rows.OrderByDescending(x => x.OccurredAtUtc).Take(limit).ToListAsync(cancellationToken);
        foreach (var item in results) item.MetadataJson = null;
        return results;
    }
}
