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
                .Select(x => BuildFindingLine(x, report.PipingProfile))
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

    private static string BuildFindingLine(InspectionFinding finding, PipingInspectionProfile? pipingProfile)
    {
        var location = string.IsNullOrWhiteSpace(finding.Location) ? "Unknown location" : finding.Location.Trim();
        var description = string.IsNullOrWhiteSpace(finding.Description) ? "No description" : finding.Description.Trim();
        var lineNumber = finding.LineNumber?.Trim();
        if (string.IsNullOrWhiteSpace(lineNumber))
        {
            lineNumber = pipingProfile?.LineNumber?.Trim();
        }

        var approxFeet = finding.ApproximateFeetOfFindings ?? pipingProfile?.ApproximateFeetOfFindings;

        var parts = new List<string> { $"{location}: {description}" };
        if (!string.IsNullOrWhiteSpace(lineNumber))
        {
            parts.Add($"on Line {lineNumber}");
        }

        if (approxFeet.HasValue)
        {
            parts.Add($"for approximately {approxFeet.Value:0.##} ft");
        }

        if (!string.IsNullOrWhiteSpace(pipingProfile?.UpstreamEquipment)
            && !string.IsNullOrWhiteSpace(pipingProfile?.DownstreamEquipment))
        {
            parts.Add($"between {pipingProfile.UpstreamEquipment.Trim()} and {pipingProfile.DownstreamEquipment.Trim()}");
        }

        return string.Join(" ", parts).Trim();
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
