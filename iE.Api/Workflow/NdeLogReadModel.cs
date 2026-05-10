namespace iE.Api.Workflow;

public static class NdeLogStatuses
{
    public const string Draft = "Draft";
    public const string Requested = "Requested";
    public const string Scheduled = "Scheduled";
    public const string InProgress = "In Progress";
    public const string ResultsReceived = "Results Received";
    public const string Reviewed = "Reviewed";
    public const string Closed = "Closed";
    public const string Cancelled = "Cancelled";
    public const string Overdue = "Overdue";
}

public static class NdeReportStatuses
{
    public const string NotStarted = "Not Started";
    public const string InProgress = "In Progress";
    public const string ResultsReceived = "Results Received";
    public const string ReportReady = "Report Ready";
    public const string Downloaded = "Downloaded";
    public const string NotAvailable = "Not Available";
}

public sealed record NdeLogItemReadModel(
    string Id,
    string RequestNumber,
    string? AssetTag,
    string? CircuitId,
    string? EquipmentTag,
    string Method,
    string Status,
    string Priority,
    string? RequestedBy,
    string? AssignedTo,
    string? DueDate,
    string? ResultReceivedDate,
    string ReportStatus,
    string? ReportNumber,
    string? ReportFileName,
    string? ReportDownloadUrl);

public sealed record NdeLogTransitionRequest(string NextStatus, string? Comment, string Actor);
public sealed record NdeLogTransitionEventReadModel(string Id,string NdeRequestId,string FromStatus,string ToStatus,string Actor,DateTime TimestampUtc,string? Comment);
public sealed record NdeLogTransitionResult(bool Success, string ReasonCode, NdeLogItemReadModel? Item = null, NdeLogTransitionEventReadModel? Event = null);

public interface INdeLogReadModelService
{
    IReadOnlyList<NdeLogItemReadModel> GetLogItems();
    NdeLogTransitionResult Transition(string id, NdeLogTransitionRequest request);
    IReadOnlyList<NdeLogTransitionEventReadModel> GetEvents(string id);
    bool Exists(string id);
}

public sealed class DemoNdeLogReadModelService : INdeLogReadModelService
{
    private readonly List<NdeLogItemReadModel> _items =
    [
        new("nde-001", "NDE-24-001", "P-102A", null, null, "UT Thickness", NdeLogStatuses.Draft, "Normal", "J. Rivera", "L. Tran", "2026-05-20", null, NdeReportStatuses.NotStarted, null, null, null),
        new("nde-002", "NDE-24-002", null, "CIR-4A-220", null, "RT", NdeLogStatuses.Requested, "High", "M. Patel", "S. Owens", "2026-05-17", null, NdeReportStatuses.InProgress, "RPT-24-002", null, null),
        new("nde-006", "NDE-24-006", null, null, "TK-804", "PAUT", NdeLogStatuses.Reviewed, "Normal", "P. Singh", "N. Brooks", "2026-05-09", "2026-05-08", NdeReportStatuses.ReportReady, "RPT-24-006", "NDE-24-006-report.pdf", "/demo-downloads/NDE-24-006-report.pdf"),
        new("nde-007", "NDE-24-007", "L-5507", null, null, "VT", NdeLogStatuses.Closed, "Low", "R. Scott", "H. Diaz", "2026-05-06", "2026-05-05", NdeReportStatuses.Downloaded, "RPT-24-007", "NDE-24-007-report.pdf", "/demo-downloads/NDE-24-007-report.pdf")
    ];
    private readonly List<NdeLogTransitionEventReadModel> _events = [];

    public IReadOnlyList<NdeLogItemReadModel> GetLogItems() => _items.ToList();
    public IReadOnlyList<NdeLogTransitionEventReadModel> GetEvents(string id) => _events.Where(x => x.NdeRequestId == id).OrderBy(x => x.TimestampUtc).ToList();
    public bool Exists(string id) => _items.Any(x => x.Id == id);

    public NdeLogTransitionResult Transition(string id, NdeLogTransitionRequest request)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index < 0) return new(false, "not_found");
        var current = _items[index];
        if (!Allowed(current.Status).Contains(request.NextStatus, StringComparer.Ordinal)) return new(false, "invalid_transition");

        var timestampUtc = DateTime.UtcNow;
        var updated = current with { Status = request.NextStatus, ResultReceivedDate = request.NextStatus == NdeLogStatuses.ResultsReceived ? (current.ResultReceivedDate ?? timestampUtc.ToString("yyyy-MM-dd")) : current.ResultReceivedDate };
        _items[index] = updated;
        var evt = new NdeLogTransitionEventReadModel(Guid.NewGuid().ToString("n"), id, current.Status, request.NextStatus, request.Actor, timestampUtc, request.Comment);
        _events.Add(evt);
        return new(true, "allowed", updated, evt);
    }

    private static string[] Allowed(string status) => status switch
    {
        NdeLogStatuses.Draft => [NdeLogStatuses.Requested, NdeLogStatuses.Cancelled],
        NdeLogStatuses.Requested => [NdeLogStatuses.Scheduled, NdeLogStatuses.Cancelled],
        NdeLogStatuses.Scheduled => [NdeLogStatuses.InProgress, NdeLogStatuses.Cancelled],
        NdeLogStatuses.InProgress => [NdeLogStatuses.ResultsReceived, NdeLogStatuses.Cancelled],
        NdeLogStatuses.ResultsReceived => [NdeLogStatuses.Reviewed, NdeLogStatuses.Cancelled],
        NdeLogStatuses.Reviewed => [NdeLogStatuses.Closed],
        NdeLogStatuses.Overdue => [NdeLogStatuses.Scheduled, NdeLogStatuses.Cancelled],
        _ => []
    };
}
