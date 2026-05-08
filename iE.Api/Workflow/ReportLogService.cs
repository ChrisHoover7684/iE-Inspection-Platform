using iE.Api.Auth;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Workflow;

public interface IReportLogService
{
    Task<WorkflowOperationResult> AddNdeLinkedEventAsync(string tenantId, string facilityId, string reportId, string ndeRequestId, string eventType, string message, string? status = null, CancellationToken ct = default);
    Task<IReadOnlyList<ReportLogEntry>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct = default);
}

public sealed class ReportLogService(InspectionReportsDbContext db, IAuditEventWriter audit) : IReportLogService
{
    public async Task<WorkflowOperationResult> AddNdeLinkedEventAsync(string tenantId, string facilityId, string reportId, string ndeRequestId, string eventType, string message, string? status = null, CancellationToken ct = default)
    {
        var entry = new ReportLogEntry { ClientOrganizationId = tenantId, FacilityId = facilityId, ReportId = reportId, RelatedNdeRequestId = ndeRequestId, EventType = eventType, EventStatus = status, Message = message.Length > 1000 ? message[..1000] : message };
        db.ReportLogEntries.Add(entry);
        await db.SaveChangesAsync(ct);
        await audit.WriteAsync("ReportLogEntryAdded","ReportLogEntry",entry.Id,"Success",facilityId,new Dictionary<string, object?>{{"operation","add"},{"reasonCode",WorkflowReasonCodes.Created},{"reportId",reportId},{"ndeRequestId",ndeRequestId},{"status",status},{"eventType",eventType}},ct);
        return new(true, WorkflowReasonCodes.Created, entry.Id, status);
    }

    public async Task<IReadOnlyList<ReportLogEntry>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct = default)
        => await db.ReportLogEntries.Where(x => x.ClientOrganizationId == tenantId && x.FacilityId == facilityId && x.ReportId == reportId).OrderBy(x => x.CreatedAtUtc).ToListAsync(ct);
}
