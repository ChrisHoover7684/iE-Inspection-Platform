using iE.Core.Reports.Services;

namespace iE.Core.Reports.Models;

public class ReportDraft
{
    public ReportData ReportData { get; set; } = new();
    public ReportValidationResult ValidationResult { get; set; } = new(new List<ReportValidationMessage>());
    public List<RepairRecommendation> RepairRecommendations { get; set; } = new();
    public bool CanSubmitForReview { get; set; }
}
