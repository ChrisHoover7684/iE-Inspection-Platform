using System.Globalization;

namespace iE.Core.Reports.Builders;

public class RepairRecommendationBuilder
{
    public List<RepairRecommendation> Build(InspectionReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var recommendations = new List<RepairRecommendation>();

        foreach (var finding in report.Findings)
        {
            if (!ShouldCreateRecommendation(finding))
            {
                continue;
            }

            recommendations.Add(new RepairRecommendation
            {
                Id = Guid.NewGuid().ToString("N"),
                FindingId = finding.Id,
                EquipmentTag = report.EquipmentTag,
                Unit = report.Unit,
                Location = finding.Location,
                FindingType = finding.FindingType,
                DamageMechanism = finding.DamageMechanism,
                DefectDescription = BuildDefectDescription(finding),
                RecommendationText = BuildRecommendationText(finding),
                Priority = MapPriority(finding.Severity),
                Basis = BuildBasis(finding),
                RelatedPhotoIds = finding.PhotoIds?.ToList() ?? new List<string>(),
                Status = RepairRecommendationStatus.Draft
            });
        }

        return recommendations;
    }

    private static bool ShouldCreateRecommendation(InspectionFinding finding)
    {
        return finding.RepairRequired
            || IsThicknessBelowMinimum(finding)
            || IsLeakageFound(finding);
    }

    private static bool IsThicknessBelowMinimum(InspectionFinding finding)
    {
        return finding.ThicknessResult.HasValue
            && finding.MinimumRequiredThickness.HasValue
            && finding.ThicknessResult.Value < finding.MinimumRequiredThickness.Value;
    }

    private static bool IsLeakageFound(InspectionFinding finding)
    {
        if (finding.FindingType == FindingType.Leak)
        {
            return true;
        }

        return (finding.Description ?? string.Empty).Contains("leak", StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildDefectDescription(InspectionFinding finding)
    {
        if (finding.PitDepth.HasValue && finding.PitDepth.Value > 0.030)
        {
            return $"Measurable pitting observed with pit depth {FormatMeasurement(finding.PitDepth.Value)}. {finding.Description ?? string.Empty}".Trim();
        }

        return finding.Description ?? string.Empty;
    }

    private static string BuildRecommendationText(InspectionFinding finding)
    {
        var recommendationParts = new List<string>();

        if (finding.RepairRequired)
        {
            recommendationParts.Add("Repair is required based on inspection findings.");
        }

        if (IsThicknessBelowMinimum(finding))
        {
            recommendationParts.Add(
                $"Measured thickness {FormatMeasurement(finding.ThicknessResult!.Value)} is below t-min {FormatMeasurement(finding.MinimumRequiredThickness!.Value)}.");
        }

        if (IsLeakageFound(finding))
        {
            recommendationParts.Add("Leakage identified; isolate, mitigate, and repair the affected area.");
        }

        if (finding.PitDepth.HasValue && finding.PitDepth.Value > 0.030)
        {
            recommendationParts.Add(
                $"Measurable pitting exceeds screening threshold with pit depth {FormatMeasurement(finding.PitDepth.Value)}.");
        }

        if (!string.IsNullOrWhiteSpace(finding.RepairRecommendation))
        {
            recommendationParts.Add(finding.RepairRecommendation.Trim());
        }

        if (recommendationParts.Count == 0)
        {
            recommendationParts.Add("Evaluate the finding and plan appropriate corrective action.");
        }

        return string.Join(" ", recommendationParts);
    }

    private static string BuildBasis(InspectionFinding finding)
    {
        var basisParts = new List<string>();

        if (finding.RepairRequired)
        {
            basisParts.Add("RepairRequired=true");
        }

        if (IsThicknessBelowMinimum(finding))
        {
            basisParts.Add($"Thickness {FormatMeasurement(finding.ThicknessResult!.Value)} < t-min {FormatMeasurement(finding.MinimumRequiredThickness!.Value)}");
        }

        if (IsLeakageFound(finding))
        {
            basisParts.Add("Leakage found");
        }

        if (finding.PitDepth.HasValue && finding.PitDepth.Value > 0.030)
        {
            basisParts.Add($"Pit depth {FormatMeasurement(finding.PitDepth.Value)} > 0.030\"");
        }

        return basisParts.Count > 0 ? string.Join("; ", basisParts) : "Inspection finding";
    }

    private static RepairRecommendationPriority MapPriority(FindingSeverity severity)
    {
        return severity switch
        {
            FindingSeverity.Critical => RepairRecommendationPriority.Critical,
            FindingSeverity.High => RepairRecommendationPriority.High,
            FindingSeverity.Medium => RepairRecommendationPriority.Medium,
            FindingSeverity.Low => RepairRecommendationPriority.Low,
            _ => RepairRecommendationPriority.Medium
        };
    }

    private static string FormatMeasurement(double value)
    {
        return string.Create(CultureInfo.InvariantCulture, $"{value:0.000}\"");
    }
}
