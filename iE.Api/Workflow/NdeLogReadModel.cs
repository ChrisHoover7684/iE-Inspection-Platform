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
    public const string NotAvailable = "Not Available";
    public const string InProgress = "In Progress";
    public const string Complete = "Complete";
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
    string? ReportDownloadUrl,
    string? InspectionDetails,
    IReadOnlyList<NdeRequestScopeItemReadModel>? ScopeItems = null);

public sealed record NdeRequestScopeItemReadModel(
    string Id,
    string Method,
    string Stage,
    string DisplayName,
    string? WeldId,
    string? Location,
    string? Notes);

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
    private const int DemoYear = 2026;

    private readonly List<NdeLogItemReadModel> _items =
    [
        new("nde-001", NdeNumbering.FormatRequestNumber(DemoYear, 1), "P-102A", null, null, "UT Thickness", NdeLogStatuses.Draft, "Normal", "J. Rivera", "L. Tran", "2026-05-20", null, NdeReportStatuses.NotStarted, null, null, null),
        new("nde-002", NdeNumbering.FormatRequestNumber(DemoYear, 2), null, "CIR-4A-220", null, "RT", NdeLogStatuses.Requested, "High", "M. Patel", "S. Owens", "2026-05-17", null, NdeReportStatuses.InProgress, NdeNumbering.FormatReportNumber(DemoYear, "RT", 2), null, null),
        new("nde-003", NdeNumbering.FormatRequestNumber(DemoYear, 3), null, null, "E-4401", "MT", NdeLogStatuses.Scheduled, "Normal", "T. Nguyen", "R. Hall", "2026-05-13", null, NdeReportStatuses.NotAvailable, null, null, null),
        new("nde-004", NdeNumbering.FormatRequestNumber(DemoYear, 4), "HX-22B", null, null, "PT", NdeLogStatuses.InProgress, "Critical", "A. Lopez", "D. Kim", "2026-05-12", null, NdeReportStatuses.InProgress, NdeNumbering.FormatReportNumber(DemoYear, "PT", 4), null, null, "Nozzle N2 root and cap PT verification before hydrotest.",
        [
            new("scope-004-pt-prep", "PT", "Prep", "PT Prep", "W-22B-01", "Nozzle N2 Root", "Surface prep and cleaning"),
            new("scope-004-pt-root", "PT", "Root", "PT Root", "W-22B-01", "Nozzle N2 Root", "Root pass examination"),
            new("scope-004-pt-final", "PT", "Final", "PT Final", "W-22B-01", "Nozzle N2 Cap", "Final cap examination")
        ]),
        new("nde-005", NdeNumbering.FormatRequestNumber(DemoYear, 5), null, "CIR-3C-118", null, "PMI", NdeLogStatuses.ResultsReceived, "High", "G. Martin", "V. Chen", "2026-05-10", "2026-05-09", NdeReportStatuses.InProgress, NdeNumbering.FormatReportNumber(DemoYear, "PMI", 5), null, null),
        new("nde-006", NdeNumbering.FormatRequestNumber(DemoYear, 6), null, null, "TK-804", "PAUT", NdeLogStatuses.Reviewed, "Normal", "P. Singh", "N. Brooks", "2026-05-09", "2026-05-08", NdeReportStatuses.Complete, NdeNumbering.FormatReportNumber(DemoYear, "PAUT", 6), "RPT-26-PAUT-006.pdf", "/demo-downloads/RPT-26-PAUT-006.pdf"),
        new("nde-007", NdeNumbering.FormatRequestNumber(DemoYear, 7), "L-5507", null, null, "VT", NdeLogStatuses.Closed, "Low", "R. Scott", "H. Diaz", "2026-05-06", "2026-05-05", NdeReportStatuses.Complete, NdeNumbering.FormatReportNumber(DemoYear, "VT", 7), "RPT-26-VT-007.pdf", "/demo-downloads/RPT-26-VT-007.pdf"),
        new("nde-008", NdeNumbering.FormatRequestNumber(DemoYear, 8), null, null, "PSV-91", "UT Thickness", NdeLogStatuses.Cancelled, "Low", "C. White", "B. Young", "2026-05-04", null, NdeReportStatuses.NotStarted, null, null, null),
        new("nde-009", NdeNumbering.FormatRequestNumber(DemoYear, 9), null, "CIR-9D-032", null, "RT", NdeLogStatuses.Overdue, "Critical", "D. Reed", "M. Gray", "2026-05-01", null, NdeReportStatuses.InProgress, NdeNumbering.FormatReportNumber(DemoYear, "RT", 9), null, null),
        new("nde-010", NdeNumbering.FormatRequestNumber(DemoYear, 10), "P-300C", null, null, "PAUT", NdeLogStatuses.Reviewed, "High", "L. Ward", "K. Adams", "2026-05-14", null, NdeReportStatuses.Complete, NdeNumbering.FormatReportNumber(DemoYear, "PAUT", 10), "RPT-26-PAUT-010.pdf", "/demo-downloads/RPT-26-PAUT-010.pdf")
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
