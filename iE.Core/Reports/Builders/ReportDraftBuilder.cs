using iE.Core.Reports.Models;
using iE.Core.Reports.Services;

namespace iE.Core.Reports.Builders;

public class ReportDraftBuilder(
    SummaryBuilder summaryBuilder,
    ReportValidationService reportValidationService,
    RepairRecommendationBuilder repairRecommendationBuilder)
{
    public ReportDraft Build(InspectionReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var validationResult = reportValidationService.Validate(report);
        var repairRecommendations = repairRecommendationBuilder.Build(report);
        var reportData = BuildReportData(report, repairRecommendations);

        return new ReportDraft
        {
            ReportData = reportData,
            ValidationResult = validationResult,
            RepairRecommendations = repairRecommendations,
            CanSubmitForReview = !validationResult.HasErrors
        };
    }

    private ReportData BuildReportData(InspectionReport report, List<RepairRecommendation> repairRecommendations)
    {
        var thicknessEvaluationResult = BuildThicknessEvaluationResult(report);
        var findingDescriptions = report.Findings.Select(x => x.Description);

        var summaryText = summaryBuilder.BuildInspectionSummary(
            thicknessEvaluationResult,
            findingDescriptions,
            report.PipingProfile);

        return new ReportData
        {
            SummaryText = summaryText,
            Findings = report.Findings
                .Select(BuildFindingLine)
                .ToList(),
            Repairs = report.Findings
                .Where(x => x.RepairRequired)
                .Select(x => string.IsNullOrWhiteSpace(x.RepairRecommendation)
                    ? x.Description
                    : x.RepairRecommendation!)
                .ToList(),
            FutureRecommendations = RepairRecommendationsToLines(repairRecommendations)
        };
    }

    private static ThicknessEvaluationResult BuildThicknessEvaluationResult(InspectionReport report)
    {
        var lowestThickness = report.Findings
            .Where(x => x.ThicknessResult.HasValue)
            .Select(x => x.ThicknessResult!.Value)
            .DefaultIfEmpty()
            .Min();

        var requiredThickness = report.Findings
            .Where(x => x.MinimumRequiredThickness.HasValue)
            .Select(x => x.MinimumRequiredThickness!.Value)
            .DefaultIfEmpty()
            .Max();

        return new ThicknessEvaluationResult
        {
            LowestThickness = lowestThickness == 0 ? null : lowestThickness,
            RequiredThickness = requiredThickness == 0 ? null : requiredThickness,
            RemainingLifeYears = null
        };
    }

    private static string BuildFindingLine(InspectionFinding finding)
    {
        var location = string.IsNullOrWhiteSpace(finding.Location) ? "Unknown location" : finding.Location.Trim();
        var description = string.IsNullOrWhiteSpace(finding.Description) ? "No description" : finding.Description.Trim();
        return $"{location}: {description}";
    }

    private static List<string> RepairRecommendationsToLines(List<RepairRecommendation> recommendations)
    {
        return recommendations
            .Select(x => x.RecommendationText)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .ToList();
    }
}
