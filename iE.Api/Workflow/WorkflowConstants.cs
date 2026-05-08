namespace iE.Api.Workflow;

public static class ReportLogEventTypes
{
    public const string NdeRequestCreated = "nde_request_created";
    public const string NdeRequestStatusChanged = "nde_request_status_changed";
    public const string NdeResultsReceived = "nde_results_received";
    public const string NdeRequestCancelled = "nde_request_cancelled";
}

public static class NdeRequestStatuses { public const string Draft="draft"; public const string Requested="requested"; public const string Scheduled="scheduled"; public const string InProgress="in_progress"; public const string ResultsReceived="results_received"; public const string Closed="closed"; public const string Canceled="canceled"; }
public static class NdeRequestPriorities { public static readonly HashSet<string> All=["low","normal","high","urgent"]; }
public static class WorkflowReasonCodes { public const string Created="created"; public const string Updated="updated"; public const string NotFound="not_found"; public const string InvalidStatusTransition="invalid_status_transition"; public const string InvalidNdeType="invalid_nde_type"; public const string InvalidPriority="invalid_priority"; public const string InvalidReason="invalid_reason"; public const string InvalidReference="invalid_reference"; }
public record WorkflowOperationResult(bool Success,string ReasonCode,string? Id=null,string? Status=null);

public sealed record NdeTypeDefinition(string Code, string DisplayName);
public static class NdeBuiltInTypes
{
    public static readonly NdeTypeDefinition[] All = [
        new("ut_thickness","UT Thickness"),new("utt_grid","UTT Grid"),new("utsw","UTSW"),new("paut","PAUT"),new("pt","PT"),new("wfpt","WFPT"),new("mt","MT"),new("wfmt","WFMT"),new("rt","RT"),new("pmi","PMI"),new("hardness","Hardness"),new("boroscope","Boroscope"),new("etc","ETC"),new("iris","IRIS"),new("guided_wave_ut","Guided Wave UT"),new("visual_followup","Visual Follow-up"),new("other","Other"),new("custom","Custom")
    ];
    public static readonly HashSet<string> Codes = All.Select(x=>x.Code).ToHashSet(StringComparer.OrdinalIgnoreCase);
}
