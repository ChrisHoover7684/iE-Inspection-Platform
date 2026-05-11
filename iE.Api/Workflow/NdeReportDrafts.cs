namespace iE.Api.Workflow;

public static class NdeReportDraftStatuses
{
    public const string Draft = "Draft";
    public const string Complete = "Complete";
}

public static class NdeReportTestResults
{
    public const string Passed = "Passed";
    public const string Failed = "Failed";
    public const string InfoOnly = "Info Only";
}

public sealed record NdeReportDraftReadModel(
    string Id,
    string NdeRequestId,
    string RequestNumber,
    string ReportNumber,
    string Method,
    string? Stage,
    string? Inspector,
    string? ExaminationDate,
    string? Procedure,
    string? AcceptanceCriteria,
    string? SurfaceCondition,
    string? ExaminedArea,
    bool IndicationsFound,
    string? TestResult,
    string? Findings,
    string? Recommendations,
    string? Notes,
    string Status,
    string? GeneratedAtUtc,
    string? ReportFileName,
    string? ReportDownloadUrl);

public sealed record NdeReportDraftSaveRequest(
    string? Id,
    string NdeRequestId,
    string? RequestNumber,
    string? ReportNumber,
    string? Method,
    string? Stage,
    string? Inspector,
    string? ExaminationDate,
    string? Procedure,
    string? AcceptanceCriteria,
    string? SurfaceCondition,
    string? ExaminedArea,
    bool IndicationsFound,
    string? TestResult,
    string? Findings,
    string? Recommendations,
    string? Notes,
    string? Status,
    string? GeneratedAtUtc,
    string? ReportFileName,
    string? ReportDownloadUrl);

public sealed record NdeReportDraftOperationResult(bool Success, string ReasonCode, NdeReportDraftReadModel? Draft = null);

public interface INdeReportDraftService
{
    NdeReportDraftReadModel? GetDraft(string ndeRequestId);
    IReadOnlyList<NdeReportDraftReadModel> GetDrafts();
    NdeReportDraftOperationResult SaveDraft(string ndeRequestId, NdeReportDraftSaveRequest request);
    NdeReportDraftOperationResult CompleteDraft(string ndeRequestId, NdeReportDraftSaveRequest request);
}

public sealed class DemoNdeReportDraftService(INdeLogReadModelService ndeLogReadModelService) : INdeReportDraftService
{
    private readonly Dictionary<string, NdeReportDraftReadModel> _drafts = new(StringComparer.Ordinal);

    public NdeReportDraftReadModel? GetDraft(string ndeRequestId) => _drafts.TryGetValue(ndeRequestId, out var draft) ? draft : null;
    public IReadOnlyList<NdeReportDraftReadModel> GetDrafts() => _drafts.Values.OrderBy(x => x.RequestNumber).ToList();

    public NdeReportDraftOperationResult SaveDraft(string ndeRequestId, NdeReportDraftSaveRequest request)
    {
        if (!ndeLogReadModelService.TryGetLogItem(ndeRequestId, out var item) || item is null) return new(false, "not_found");
        var reportNumber = request.ReportNumber ?? item.ReportNumber ?? NdeNumbering.FormatReportNumber(2026, item.Method, ExtractSequence(item.RequestNumber));
        var saved = new NdeReportDraftReadModel(
            request.Id ?? $"draft-{ndeRequestId}", ndeRequestId, item.RequestNumber, reportNumber, request.Method ?? item.Method, request.Stage ?? item.NdeStage,
            request.Inspector, request.ExaminationDate, request.Procedure, request.AcceptanceCriteria, request.SurfaceCondition,
            request.ExaminedArea, request.IndicationsFound, request.TestResult, request.Findings, request.Recommendations, request.Notes,
            NdeReportDraftStatuses.Draft, null, null, null);
        _drafts[ndeRequestId] = saved;
        ndeLogReadModelService.SyncReportMetadata(ndeRequestId, reportNumber, NdeReportStatuses.InProgress, null, null);
        return new(true, "saved", saved);
    }

    public NdeReportDraftOperationResult CompleteDraft(string ndeRequestId, NdeReportDraftSaveRequest request)
    {
        if (!ndeLogReadModelService.TryGetLogItem(ndeRequestId, out var item) || item is null) return new(false, "not_found");
        if (string.IsNullOrWhiteSpace(request.Inspector) || string.IsNullOrWhiteSpace(request.ExaminationDate) || string.IsNullOrWhiteSpace(request.Procedure) || string.IsNullOrWhiteSpace(request.TestResult)) return new(false, "required_fields_missing");
        var reportNumber = request.ReportNumber ?? item.ReportNumber ?? NdeNumbering.FormatReportNumber(2026, item.Method, ExtractSequence(item.RequestNumber));
        var completed = new NdeReportDraftReadModel(
            request.Id ?? $"draft-{ndeRequestId}", ndeRequestId, item.RequestNumber, reportNumber, request.Method ?? item.Method, request.Stage ?? item.NdeStage,
            request.Inspector, request.ExaminationDate, request.Procedure, request.AcceptanceCriteria, request.SurfaceCondition,
            request.ExaminedArea, request.IndicationsFound, request.TestResult, request.Findings, request.Recommendations, request.Notes,
            NdeReportDraftStatuses.Complete, DateTime.UtcNow.ToString("O"), $"{reportNumber}.pdf", null);
        _drafts[ndeRequestId] = completed;
        ndeLogReadModelService.SyncReportMetadata(ndeRequestId, reportNumber, NdeReportStatuses.Complete, completed.ReportFileName, null);
        return new(true, "completed", completed);
    }

    private static int ExtractSequence(string requestNumber) => int.TryParse(requestNumber.Split('-').LastOrDefault(), out var seq) ? seq : 1;
}
