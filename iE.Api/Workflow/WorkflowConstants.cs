namespace iE.Api.Workflow;

public static class ReportLogEventTypes
{
    public const string NdeRequestCreated = "nde_request_created";
    public const string NdeRequestStatusChanged = "nde_request_status_changed";
    public const string NdeResultsReceived = "nde_results_received";
}
public static class NdeRequestStatuses { public const string Draft="draft"; public const string Requested="requested"; public const string Scheduled="scheduled"; public const string InProgress="in_progress"; public const string ResultsReceived="results_received"; public const string Closed="closed"; public const string Canceled="canceled"; public static readonly HashSet<string> All=[Draft,Requested,Scheduled,InProgress,ResultsReceived,Closed,Canceled]; }
public static class NdeRequestTypes { public static readonly HashSet<string> All=["ut_thickness","pt","mt","rt","pmi","hardness","boroscope","visual_followup","other"]; }
public static class NdeRequestPriorities { public static readonly HashSet<string> All=["low","normal","high","urgent"]; }
public static class WorkflowReasonCodes { public const string Allowed="allowed"; public const string Created="created"; public const string Updated="updated"; public const string NotFound="not_found"; public const string InvalidStatusTransition="invalid_status_transition"; public const string InvalidNdeType="invalid_nde_type"; public const string InvalidPriority="invalid_priority"; public const string InvalidReference="invalid_reference"; public const string CrossRef="cross_tenant_or_facility_reference"; }
public record WorkflowOperationResult(bool Success,string ReasonCode,string? Id=null,string? Status=null);
