using System.Globalization;
using System.Text;
using iE.Core.Reports.Models;

namespace iE.Core.Reports.Builders;

public class SummaryBuilder
{
    public string BuildInspectionSummary(
        ThicknessEvaluationResult thicknessEvaluationResult,
        IEnumerable<string>? findings)
    {
        ArgumentNullException.ThrowIfNull(thicknessEvaluationResult);

        var summary = new StringBuilder();

        var lowest = thicknessEvaluationResult.LowestThickness;
        var required = thicknessEvaluationResult.RequiredThickness;

        if (lowest.HasValue && required.HasValue)
        {
            if (lowest.Value < required.Value)
            {
                summary.Append(
                    $"The lowest measured thickness recorded was {FormatMeasurement(lowest)}. The calculated required thickness is {FormatMeasurement(required)}; therefore, the component is below minimum allowable thickness and requires evaluation for repair.");
            }
            else
            {
                summary.Append(
                    $"The lowest measured thickness recorded was {FormatMeasurement(lowest)}, which is above the calculated required thickness of {FormatMeasurement(required)}.");
            }
        }
        else
        {
            summary.Append("Thickness evaluation values were incomplete for summary generation.");
        }

        if (thicknessEvaluationResult.RemainingLifeYears.HasValue)
        {
            summary.Append($" Estimated remaining life is {FormatYears(thicknessEvaluationResult.RemainingLifeYears.Value)} years.");
        }

        var findingList = findings?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .ToList() ?? new List<string>();

        var hasMeasurablePitting = findingList.Any(f => ContainsPittingAboveThreshold(f, 0.030));
        var hasMinorPitting = !hasMeasurablePitting && findingList.Any(f => ContainsPittingValue(f));

        if (hasMeasurablePitting)
        {
            summary.Append(" Findings indicate measurable pitting.");
        }
        else if (hasMinorPitting)
        {
            summary.Append(" Findings indicate minor pitting.");
        }

        return summary.ToString().Trim();
    }

    private static string FormatMeasurement(double? value) =>
        value.HasValue
            ? $"{value.Value.ToString("0.000", CultureInfo.InvariantCulture)}\""
            : "N/A";

    private static string FormatYears(double value) => value.ToString("0.##", CultureInfo.InvariantCulture);

    private static bool ContainsPittingValue(string finding)
    {
        if (!finding.Contains("pitting", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return TryExtractFirstDecimal(finding, out _);
    }

    private static bool ContainsPittingAboveThreshold(string finding, double threshold)
    {
        if (!finding.Contains("pitting", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return TryExtractFirstDecimal(finding, out var value) && value > threshold;
    }

    private static bool TryExtractFirstDecimal(string input, out double value)
    {
        value = 0;

        var chars = input
            .Where(c => char.IsDigit(c) || c == '.')
            .ToArray();

        if (chars.Length == 0)
        {
            return false;
        }

        var numeric = new string(chars);
        return double.TryParse(numeric, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
    }
}
