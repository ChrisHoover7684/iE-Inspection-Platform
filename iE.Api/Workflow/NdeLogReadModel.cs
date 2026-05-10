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

public interface INdeLogReadModelService
{
    IReadOnlyList<NdeLogItemReadModel> GetLogItems();
}

public sealed class DemoNdeLogReadModelService : INdeLogReadModelService
{
    public IReadOnlyList<NdeLogItemReadModel> GetLogItems() =>
    [
        new("nde-001", "NDE-24-001", "P-102A", null, null, "UT Thickness", NdeLogStatuses.Draft, "Normal", "J. Rivera", "L. Tran", "2026-05-20", null, NdeReportStatuses.NotStarted, null, null, null),
        new("nde-002", "NDE-24-002", null, "CIR-4A-220", null, "RT", NdeLogStatuses.Requested, "High", "M. Patel", "S. Owens", "2026-05-17", null, NdeReportStatuses.InProgress, "RPT-24-002", null, null),
        new("nde-006", "NDE-24-006", null, null, "TK-804", "PAUT", NdeLogStatuses.Reviewed, "Normal", "P. Singh", "N. Brooks", "2026-05-09", "2026-05-08", NdeReportStatuses.ReportReady, "RPT-24-006", "NDE-24-006-report.pdf", "/demo-downloads/NDE-24-006-report.pdf"),
        new("nde-007", "NDE-24-007", "L-5507", null, null, "VT", NdeLogStatuses.Closed, "Low", "R. Scott", "H. Diaz", "2026-05-06", "2026-05-05", NdeReportStatuses.Downloaded, "RPT-24-007", "NDE-24-007-report.pdf", "/demo-downloads/NDE-24-007-report.pdf")
    ];
}
