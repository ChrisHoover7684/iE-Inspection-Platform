using iE.Api.Auth;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Workflow;

public interface IReportLogService
{
    Task<WorkflowOperationResult> AddAsync(string tenantId, string facilityId, string reportId, string eventType, string message, string? status = null, string? fromStatus = null, string? toStatus = null, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddStatusChangeAsync(string tenantId, string facilityId, string reportId, string fromStatus, string toStatus, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReportCreatedAsync(string tenantId, string facilityId, string reportId, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReportUpdatedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReportSubmittedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReviewStartedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReviewApprovedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddReviewReturnedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default);
    Task<WorkflowOperationResult> AddNdeLinkedEventAsync(string tenantId, string facilityId, string reportId, string ndeRequestId, string eventType, string message, string? status = null, CancellationToken ct = default);
    Task<IReadOnlyList<ReportLogEntry>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct = default);
}

public sealed class ReportLogService(InspectionReportsDbContext db, IAuditEventWriter audit) : IReportLogService
{
    public Task<WorkflowOperationResult> AddAsync(string tenantId, string facilityId, string reportId, string eventType, string message, string? status = null, string? fromStatus = null, string? toStatus = null, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
    {
        var entry = new ReportLogEntry
        {
            ClientOrganizationId = tenantId,
            FacilityId = facilityId,
            ReportId = reportId,
            EventType = eventType,
            EventStatus = status,
            FromStatus = fromStatus,
            ToStatus = toStatus,
            ActorUserId = actorUserId,
            ActorExternalSubject = actorExternalSubject,
            Message = message.Length > 1000 ? message[..1000] : message
        };
        db.ReportLogEntries.Add(entry);
        return SaveAndAuditAsync(entry, reportId, status, eventType, ct);
    }

    public Task<WorkflowOperationResult> AddStatusChangeAsync(string tenantId, string facilityId, string reportId, string fromStatus, string toStatus, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.StatusChanged, message, toStatus, fromStatus, toStatus, actorUserId, actorExternalSubject, ct);
    public Task<WorkflowOperationResult> AddReportCreatedAsync(string tenantId, string facilityId, string reportId, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReportCreated, message, InspectionReportStatuses.Draft, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);
    public Task<WorkflowOperationResult> AddReportUpdatedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReportUpdated, message, status, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);
    public Task<WorkflowOperationResult> AddReportSubmittedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReportSubmitted, message, status, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);
    public Task<WorkflowOperationResult> AddReviewStartedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReviewStarted, message, status, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);
    public Task<WorkflowOperationResult> AddReviewApprovedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReviewApproved, message, status, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);
    public Task<WorkflowOperationResult> AddReviewReturnedAsync(string tenantId, string facilityId, string reportId, string status, string message, string? actorUserId = null, string? actorExternalSubject = null, CancellationToken ct = default)
        => AddAsync(tenantId, facilityId, reportId, ReportLogEventTypes.ReviewReturned, message, status, actorUserId: actorUserId, actorExternalSubject: actorExternalSubject, ct: ct);

    public async Task<WorkflowOperationResult> AddNdeLinkedEventAsync(string tenantId, string facilityId, string reportId, string ndeRequestId, string eventType, string message, string? status = null, CancellationToken ct = default)
    {
        var entry = new ReportLogEntry { ClientOrganizationId = tenantId, FacilityId = facilityId, ReportId = reportId, RelatedNdeRequestId = ndeRequestId, EventType = eventType, EventStatus = status, Message = message.Length > 1000 ? message[..1000] : message };
        db.ReportLogEntries.Add(entry);
        return await SaveAndAuditAsync(entry, reportId, status, eventType, ct, ndeRequestId);
    }
    private async Task<WorkflowOperationResult> SaveAndAuditAsync(ReportLogEntry entry, string reportId, string? status, string eventType, CancellationToken ct, string? ndeRequestId = null)
    {
        await db.SaveChangesAsync(ct);
        await audit.WriteAsync("ReportLogEntryAdded","ReportLogEntry",entry.Id,"Success",entry.FacilityId,new Dictionary<string, object?>{{"operation","add"},{"reasonCode",WorkflowReasonCodes.Created},{"reportId",reportId},{"status",status},{"eventType",eventType},{"hasNdeRequest",ndeRequestId is not null}},ct);
        return new(true, WorkflowReasonCodes.Created, entry.Id, status);
    }

    public async Task<IReadOnlyList<ReportLogEntry>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct = default)
        => await db.ReportLogEntries.Where(x => x.ClientOrganizationId == tenantId && x.FacilityId == facilityId && x.ReportId == reportId).OrderBy(x => x.CreatedAtUtc).ToListAsync(ct);
}
