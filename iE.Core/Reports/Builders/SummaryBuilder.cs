using System.Globalization;
using System.Text;
using iE.Core.Reports.Models;

namespace iE.Core.Reports.Builders;

public class SummaryBuilder
{
    public string BuildInspectionSummary(ReportData reportData)
    {
        ArgumentNullException.ThrowIfNull(reportData);

        var lines = new List<string>();
        lines.Add(BuildHeader(reportData));

        if (reportData.ThicknessEvaluations.Count == 0)
        {
            lines.Add("No thickness evaluation results were provided.");
            return string.Join("\n", lines);
        }

        var acceptableCount = reportData.ThicknessEvaluations.Count(result => result.IsAcceptable);
        var nonAcceptableCount = reportData.ThicknessEvaluations.Count - acceptableCount;

        lines.Add($"Thickness evaluations completed: {reportData.ThicknessEvaluations.Count}.");
        lines.Add($"Acceptable locations: {acceptableCount}. Locations requiring review/action: {nonAcceptableCount}.");

        var governing = reportData.ThicknessEvaluations
            .Where(r => r.RemainingLifeYears.HasValue)
            .OrderBy(r => r.RemainingLifeYears)
            .FirstOrDefault();

        if (governing is not null)
        {
            lines.Add(
                $"Governing location: {Clean(governing.CmlName)} with estimated remaining life of {FormatNumber(governing.RemainingLifeYears)} years.");
        }

        lines.Add("Thickness evaluation details:");

        foreach (var result in reportData.ThicknessEvaluations)
        {
            lines.Add(BuildResultLine(result));
        }

        return string.Join("\n", lines);
    }

    private static string BuildHeader(ReportData reportData)
    {
        var parts = new List<string>
        {
            "Inspection thickness summary"
        };

        if (!string.IsNullOrWhiteSpace(reportData.ReportId))
        {
            parts.Add($"Report {reportData.ReportId.Trim()}");
        }

        if (!string.IsNullOrWhiteSpace(reportData.EquipmentTag))
        {
            parts.Add($"Equipment {reportData.EquipmentTag.Trim()}");
        }

        if (!string.IsNullOrWhiteSpace(reportData.CircuitId))
        {
            parts.Add($"Circuit {reportData.CircuitId.Trim()}");
        }

        return string.Join(" | ", parts) + ".";
    }

    private static string BuildResultLine(ThicknessEvaluationResult result)
    {
        var builder = new StringBuilder();

        builder.Append("- ");
        builder.Append(Clean(result.CmlName));
        builder.Append(": ");
        builder.Append(result.IsAcceptable ? "Acceptable" : "Requires action");

        builder.Append("; Measured=");
        builder.Append(FormatNumber(result.MeasuredThickness));
        builder.Append(" in");

        builder.Append(", Required=");
        builder.Append(FormatNumber(result.RequiredThickness));
        builder.Append(" in");

        builder.Append(", CR=");
        builder.Append(FormatNumber(result.CorrosionRate));
        builder.Append(" in/yr");

        builder.Append(", Remaining life=");
        builder.Append(FormatNumber(result.RemainingLifeYears));
        builder.Append(" yr");

        if (!string.IsNullOrWhiteSpace(result.Notes))
        {
            builder.Append(".");
            builder.Append(" Notes: ");
            builder.Append(result.Notes.Trim());
        }

        return builder.ToString();
    }

    private static string FormatNumber(double? value) =>
        value.HasValue ? value.Value.ToString("0.###", CultureInfo.InvariantCulture) : "N/A";

    private static string Clean(string? value) => string.IsNullOrWhiteSpace(value) ? "Not documented" : value.Trim();
}
