using iE.Api.Auth;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Api.Workflow;
public interface INdeRequestService
{
    Task<WorkflowOperationResult> CreateDraftAsync(string tenantId,string facilityId,string? reportId,string? assetId,string? processUnitId,string ndeType,string priority,string reason,CancellationToken ct=default);
    Task<WorkflowOperationResult> RequestAsync(string id, CancellationToken ct=default);
    Task<WorkflowOperationResult> ScheduleAsync(string id, CancellationToken ct=default);
    Task<WorkflowOperationResult> MarkInProgressAsync(string id, CancellationToken ct=default);
    Task<WorkflowOperationResult> RecordResultsReceivedAsync(string id,string summary,CancellationToken ct=default);
    Task<WorkflowOperationResult> CloseAsync(string id, CancellationToken ct=default);
    Task<WorkflowOperationResult> CancelAsync(string id,string reason,CancellationToken ct=default);
}
public sealed class NdeRequestService(InspectionReportsDbContext db,IReportLogService reportLog, IAuditEventWriter audit):INdeRequestService
{
    static bool Allowed(string from,string to)=> (from,to) switch { ("draft","requested") or ("requested","scheduled") or ("requested","in_progress") or ("scheduled","in_progress") or ("in_progress","results_received") or ("results_received","closed")=>true, ("draft","canceled") or ("requested","canceled") or ("scheduled","canceled") or ("in_progress","canceled")=>true,_=>false};
    public async Task<WorkflowOperationResult> CreateDraftAsync(string tenantId,string facilityId,string? reportId,string? assetId,string? processUnitId,string ndeType,string priority,string reason,CancellationToken ct=default){ if(!NdeRequestTypes.All.Contains(ndeType)) return new(false,WorkflowReasonCodes.InvalidNdeType); if(!NdeRequestPriorities.All.Contains(priority)) return new(false,WorkflowReasonCodes.InvalidPriority); var x=new NdeRequest{ClientOrganizationId=tenantId,FacilityId=facilityId,ReportId=reportId,AssetId=assetId,ProcessUnitId=processUnitId,NdeType=ndeType,Priority=priority,Reason=reason,Status=NdeRequestStatuses.Draft}; db.NdeRequests.Add(x); await db.SaveChangesAsync(ct); if(reportId is not null) await reportLog.AddNdeLinkedEventAsync(tenantId,facilityId,reportId,x.Id,ReportLogEventTypes.NdeRequestCreated,"NDE request draft created",x.Status,ct); return new(true,WorkflowReasonCodes.Created,x.Id,x.Status);}    
    async Task<WorkflowOperationResult> Move(string id,string to,string? summary=null,CancellationToken ct=default){var x=await db.NdeRequests.FirstOrDefaultAsync(x=>x.Id==id,ct); if(x is null) return new(false,WorkflowReasonCodes.NotFound); if(!Allowed(x.Status,to)) return new(false,WorkflowReasonCodes.InvalidStatusTransition,x.Id,x.Status); x.Status=to; x.UpdatedAtUtc=DateTime.UtcNow; if(to==NdeRequestStatuses.ResultsReceived){x.ResultsReceivedAtUtc=DateTime.UtcNow; x.ResultsSummary=summary?.Length>1000?summary[..1000]:summary;} if(to==NdeRequestStatuses.Closed) x.ClosedAtUtc=DateTime.UtcNow; if(to==NdeRequestStatuses.Canceled){x.CanceledAtUtc=DateTime.UtcNow; x.CancellationReason=summary?.Length>500?summary[..500]:summary;} await db.SaveChangesAsync(ct); if(x.ReportId is not null){var et=to==NdeRequestStatuses.ResultsReceived?ReportLogEventTypes.NdeResultsReceived:ReportLogEventTypes.NdeRequestStatusChanged; await reportLog.AddNdeLinkedEventAsync(x.ClientOrganizationId,x.FacilityId,x.ReportId,x.Id,et,$"NDE request moved to {to}",to,ct);} return new(true,WorkflowReasonCodes.Updated,x.Id,x.Status);}    
    public Task<WorkflowOperationResult> RequestAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Requested,ct:ct);
    public Task<WorkflowOperationResult> ScheduleAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Scheduled,ct:ct);
    public Task<WorkflowOperationResult> MarkInProgressAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.InProgress,ct:ct);
    public Task<WorkflowOperationResult> RecordResultsReceivedAsync(string id,string summary,CancellationToken ct=default)=>Move(id,NdeRequestStatuses.ResultsReceived,summary,ct);
    public Task<WorkflowOperationResult> CloseAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Closed,ct:ct);
    public Task<WorkflowOperationResult> CancelAsync(string id,string reason,CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Canceled,reason,ct);
}
