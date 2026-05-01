namespace iE.Core.Reports.Review;

public class ReportWorkflowService
{
    public bool TryStartReview(InspectionReport report) =>
        string.Equals(report.Status, InspectionReportStatuses.ReadyForReview, StringComparison.Ordinal);

    public bool TryApprove(InspectionReport report) =>
        string.Equals(report.Status, InspectionReportStatuses.InReview, StringComparison.Ordinal);

    public bool TryReturnForRevision(InspectionReport report) =>
        string.Equals(report.Status, InspectionReportStatuses.InReview, StringComparison.Ordinal);

    public void SubmitForReview(InspectionReport report)
    {
        report.Status = InspectionReportStatuses.ReadyForReview;
        report.UpdatedAt = DateTime.UtcNow;
        AddHistory(report, ReviewAction.SubmittedForReview);
    }

    public void StartReview(InspectionReport report)
    {
        report.Status = InspectionReportStatuses.InReview;
        report.UpdatedAt = DateTime.UtcNow;
    }

    public void Approve(InspectionReport report)
    {
        report.Status = InspectionReportStatuses.Final;
        report.UpdatedAt = DateTime.UtcNow;
        AddHistory(report, ReviewAction.Approved);
    }

    public void ReturnForRevision(InspectionReport report, string reviewerComments)
    {
        report.Status = InspectionReportStatuses.ReturnedForRevision;
        report.UpdatedAt = DateTime.UtcNow;
        AddHistory(report, ReviewAction.ReturnedForRevision, reviewerComments.Trim());
    }

    private static void AddHistory(InspectionReport report, ReviewAction action, string? comments = null)
    {
        report.ReviewHistory.Add(new ReportReviewHistory
        {
            Id = Guid.NewGuid().ToString("N"),
            ReportId = report.Id,
            Action = action,
            Comments = comments,
            PerformedByUserId = report.UpdatedByUserId ?? report.CreatedByUserId ?? "system",
            PerformedAt = DateTime.UtcNow
        });
    }
}
