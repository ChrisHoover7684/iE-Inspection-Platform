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
    Task<IReadOnlyList<NdeRequest>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct=default);
    Task<IReadOnlyList<NdeRequest>> QueryForAssetAsync(string tenantId, string facilityId, string assetId, CancellationToken ct=default);
}
public sealed class NdeRequestService(InspectionReportsDbContext db,IReportLogService reportLog, IAuditEventWriter audit, INdeRequestTypeService typeService):INdeRequestService
{
    static bool Allowed(string from,string to)=> (from,to) switch { ("draft","requested") or ("requested","scheduled") or ("requested","in_progress") or ("scheduled","in_progress") or ("in_progress","results_received") or ("results_received","closed")=>true, ("draft","canceled") or ("requested","canceled") or ("scheduled","canceled") or ("in_progress","canceled")=>true,_=>false};

    public async Task<WorkflowOperationResult> CreateDraftAsync(string tenantId,string facilityId,string? reportId,string? assetId,string? processUnitId,string ndeType,string priority,string reason,CancellationToken ct=default){
        if(!await typeService.IsValidTypeAsync(tenantId, ndeType, ct)) return new(false,WorkflowReasonCodes.InvalidNdeType);
        if(!NdeRequestPriorities.All.Contains(priority)) return new(false,WorkflowReasonCodes.InvalidPriority);
        if(string.IsNullOrWhiteSpace(reason)) return new(false,WorkflowReasonCodes.InvalidReason);
        if(reportId is not null && !await db.InspectionReports.AnyAsync(x=>x.Id==reportId && x.ClientOrganizationId==tenantId && x.FacilityId==facilityId,ct)) return new(false,WorkflowReasonCodes.InvalidReference);
        if(assetId is not null && !await db.Assets.AnyAsync(x=>x.Id==assetId && x.FacilityId==facilityId,ct)) return new(false,WorkflowReasonCodes.InvalidReference);
        if(processUnitId is not null && !await db.ProcessUnits.AnyAsync(x=>x.Id==processUnitId && x.FacilityId==facilityId,ct)) return new(false,WorkflowReasonCodes.InvalidReference);
        var x=new NdeRequest{ClientOrganizationId=tenantId,FacilityId=facilityId,ReportId=reportId,AssetId=assetId,ProcessUnitId=processUnitId,NdeType=ndeType,Priority=priority,Reason=reason[..Math.Min(reason.Length,256)],Status=NdeRequestStatuses.Draft};
        db.NdeRequests.Add(x); await db.SaveChangesAsync(ct);
        if(reportId is not null) await reportLog.AddNdeLinkedEventAsync(tenantId,facilityId,reportId,x.Id,ReportLogEventTypes.NdeRequestCreated,"NDE request draft created",x.Status,ct);
        await audit.WriteAsync("NdeRequestCreated","NdeRequest",x.Id,"Success",facilityId,new Dictionary<string, object?>{{"operation","create"},{"reasonCode",WorkflowReasonCodes.Created},{"reportId",reportId},{"ndeRequestId",x.Id},{"status",x.Status},{"hasAsset",assetId is not null},{"hasProcessUnit",processUnitId is not null}},ct);
        return new(true,WorkflowReasonCodes.Created,x.Id,x.Status);
    }

    async Task<WorkflowOperationResult> Move(string id,string to,string? summary=null,CancellationToken ct=default){
        var x=await db.NdeRequests.FirstOrDefaultAsync(x=>x.Id==id,ct);
        if(x is null) return new(false,WorkflowReasonCodes.NotFound);
        if(!Allowed(x.Status,to)) return new(false,WorkflowReasonCodes.InvalidStatusTransition,x.Id,x.Status);

        x.Status=to;
        x.UpdatedAtUtc=DateTime.UtcNow;

        if(to==NdeRequestStatuses.ResultsReceived){x.ResultsReceivedAtUtc=DateTime.UtcNow; x.ResultsSummary=summary?.Length>1000?summary[..1000]:summary;}
        if(to==NdeRequestStatuses.Closed) x.ClosedAtUtc=DateTime.UtcNow;
        if(to==NdeRequestStatuses.Canceled){x.CanceledAtUtc=DateTime.UtcNow; x.CancellationReason=summary?.Length>500?summary[..500]:summary;}

        await db.SaveChangesAsync(ct);

        var eventType = to switch
        {
            var t when t==NdeRequestStatuses.ResultsReceived => ReportLogEventTypes.NdeResultsReceived,
            var t when t==NdeRequestStatuses.Canceled => ReportLogEventTypes.NdeRequestCancelled,
            _ => ReportLogEventTypes.NdeRequestStatusChanged
        };

        if(x.ReportId is not null)
        {
            var message = to==NdeRequestStatuses.Canceled ? "NDE request cancelled" : $"NDE request moved to {to}";
            await reportLog.AddNdeLinkedEventAsync(x.ClientOrganizationId,x.FacilityId,x.ReportId,x.Id,eventType,message,to,ct);
        }

        var action = to==NdeRequestStatuses.ResultsReceived ? "NdeResultsReceived" : to==NdeRequestStatuses.Canceled ? "NdeRequestCancelled" : "NdeRequestStatusChanged";
        await audit.WriteAsync(action,"NdeRequest",x.Id,"Success",x.FacilityId,new Dictionary<string, object?>{{"operation",to==NdeRequestStatuses.Canceled?"cancel":"status_change"},{"reasonCode",WorkflowReasonCodes.Updated},{"reportId",x.ReportId},{"ndeRequestId",x.Id},{"status",x.Status},{"eventType",eventType},{"hasAsset",x.AssetId is not null},{"hasProcessUnit",x.ProcessUnitId is not null}},ct);
        return new(true,WorkflowReasonCodes.Updated,x.Id,x.Status);}

    public Task<WorkflowOperationResult> RequestAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Requested,ct:ct);
    public Task<WorkflowOperationResult> ScheduleAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Scheduled,ct:ct);
    public Task<WorkflowOperationResult> MarkInProgressAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.InProgress,ct:ct);
    public Task<WorkflowOperationResult> RecordResultsReceivedAsync(string id,string summary,CancellationToken ct=default)=>Move(id,NdeRequestStatuses.ResultsReceived,summary,ct);
    public Task<WorkflowOperationResult> CloseAsync(string id, CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Closed,ct:ct);
    public Task<WorkflowOperationResult> CancelAsync(string id,string reason,CancellationToken ct=default)=>Move(id,NdeRequestStatuses.Canceled,reason,ct);
    public async Task<IReadOnlyList<NdeRequest>> QueryForReportAsync(string tenantId, string facilityId, string reportId, CancellationToken ct=default)=>await db.NdeRequests.Where(x=>x.ClientOrganizationId==tenantId&&x.FacilityId==facilityId&&x.ReportId==reportId).OrderBy(x=>x.CreatedAtUtc).ToListAsync(ct);
    public async Task<IReadOnlyList<NdeRequest>> QueryForAssetAsync(string tenantId, string facilityId, string assetId, CancellationToken ct=default)=>await db.NdeRequests.Where(x=>x.ClientOrganizationId==tenantId&&x.FacilityId==facilityId&&x.AssetId==assetId).OrderBy(x=>x.CreatedAtUtc).ToListAsync(ct);
}
