using System.Text;

namespace iE.Core.Reports.Services;

public class InspectionSummaryService
{
    public InspectionSummaryResult Build(InspectionReport report)
    {
        var findings = report.Findings ?? new List<InspectionFinding>();
        var answers = (report.Sections ?? new List<InspectionReportSection>())
            .SelectMany(section => section.Answers ?? new List<InspectionReportAnswer>())
            .ToList();

        var repairFindings = findings
            .Where(RequiresRepair)
            .ToList();

        var recommendationFindings = findings
            .Where(f => f.RepairRequired)
            .ToList();

        var recommendationAnswers = answers
            .Where(a => a.RecommendationRequired == true)
            .ToList();

        var ndeAnswers = answers
            .Where(IsNdeOrTestingAnswer)
            .ToList();

        return new InspectionSummaryResult
        {
            Summary = BuildSummary(findings),
            Repairs = BuildRepairSummary(repairFindings),
            Recommendations = BuildRecommendationSummary(recommendationFindings, recommendationAnswers),
            NdeAndTesting = BuildNdeSummary(ndeAnswers),
            ReturnToService = BuildReturnToServiceSummary(repairFindings, recommendationFindings, recommendationAnswers)
        };
    }

    private static string BuildSummary(List<InspectionFinding> findings) =>
        findings.Count == 0
            ? "Based on the inspection scope completed, no conditions requiring immediate repair were identified at the time of inspection."
            : "Inspection findings were identified during execution and are summarized in the findings section of this report.";

    private static string BuildRepairSummary(List<InspectionFinding> repairFindings)
    {
        if (repairFindings.Count == 0)
        {
            return "No repairs or corrective actions were identified as required at this time.";
        }

        var lines = repairFindings
            .Select(f => $"- {Clean(f.Location)} ({Clean(f.ComponentType)}): {Clean(f.Description)}")
            .ToList();

        return "Repairs/corrective actions identified:\n" + string.Join("\n", lines);
    }

    private static string BuildRecommendationSummary(
        List<InspectionFinding> recommendationFindings,
        List<InspectionReportAnswer> recommendationAnswers)
    {
        var lines = new List<string>();

        lines.AddRange(recommendationFindings.Select(f =>
        {
            var recommendation = string.IsNullOrWhiteSpace(f.RepairRecommendation)
                ? Clean(f.Description)
                : Clean(f.RepairRecommendation);
            return $"- Finding ({Clean(f.Location)}): {recommendation}";
        }));

        lines.AddRange(recommendationAnswers.Select(a =>
        {
            var basis = string.IsNullOrWhiteSpace(a.Comment) ? Clean(a.Value) : Clean(a.Comment);
            return $"- Checklist ({Clean(a.Label)}): {basis}";
        }));

        if (lines.Count == 0)
        {
            return "No future recommendations were generated at this time.";
        }

        return "Recommendations for follow-up:\n" + string.Join("\n", lines);
    }

    private static string BuildNdeSummary(List<InspectionReportAnswer> ndeAnswers)
    {
        if (ndeAnswers.Count == 0)
        {
            return "NDE/testing was not performed during this inspection unless otherwise documented in the checklist.";
        }

        var lines = ndeAnswers.Select(a =>
        {
            var value = a.Values.Count > 0 ? string.Join(", ", a.Values) : Clean(a.Value);
            var comment = string.IsNullOrWhiteSpace(a.Comment) ? string.Empty : $" ({Clean(a.Comment)})";
            return $"- {Clean(a.Label)}: {value}{comment}";
        });

        return "NDE/testing documented during this inspection:\n" + string.Join("\n", lines);
    }

    private static string BuildReturnToServiceSummary(
        List<InspectionFinding> repairFindings,
        List<InspectionFinding> recommendationFindings,
        List<InspectionReportAnswer> recommendationAnswers)
    {
        var hasRestrictions = repairFindings.Count > 0 || recommendationFindings.Count > 0 || recommendationAnswers.Count > 0;

        return hasRestrictions
            ? "Return to service is subject to closure of identified repairs/recommendations and owner-user risk review."
            : "Based on conditions observed within the completed inspection scope, the asset/circuit was acceptable for continued service.";
    }

    private static bool RequiresRepair(InspectionFinding finding)
    {
        var text = $"{finding.FindingType} {finding.Severity} {finding.Description}".ToLowerInvariant();
        return text.Contains("repair")
               || text.Contains("corrective")
               || text.Contains("replace")
               || text.Contains("leak")
               || text.Contains("severe")
               || text.Contains("critical");
    }

    private static bool IsNdeOrTestingAnswer(InspectionReportAnswer answer)
    {
        var text = $"{answer.Label} {answer.Value} {answer.Comment}".ToLowerInvariant();
        return text.Contains("nde")
               || text.Contains("nondestructive")
               || text.Contains("non-destructive")
               || text.Contains("ut")
               || text.Contains("mpi")
               || text.Contains("pt")
               || text.Contains("rt")
               || text.Contains("testing")
               || text.Contains("thickness")
               || text.Contains("hardness")
               || text.Contains("hydrotest")
               || text.Contains("pmi");
    }

    private static string Clean(string? value) => string.IsNullOrWhiteSpace(value) ? "Not documented" : value.Trim();
}
