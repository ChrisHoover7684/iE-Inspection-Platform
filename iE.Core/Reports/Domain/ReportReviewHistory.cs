namespace iE.Core.Reports.Domain;

public enum ReviewAction
{
    SubmittedForReview = 1,
    Approved = 2,
    ReturnedForRevision = 3
}

public class ReportReviewHistory
{
    public string Id { get; set; } = string.Empty;
    public string ReportId { get; set; } = string.Empty;
    public ReviewAction Action { get; set; }
    public string? Comments { get; set; }
    public string? PerformedByUserId { get; set; }
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
}
