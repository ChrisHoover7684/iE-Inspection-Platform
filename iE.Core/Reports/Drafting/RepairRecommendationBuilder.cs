using System.Globalization;

namespace iE.Core.Reports.Drafting;

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

        var description = finding.Description ?? string.Empty;
        return description.Contains("leak", StringComparison.OrdinalIgnoreCase)
            || description.Contains("leakage", StringComparison.OrdinalIgnoreCase)
            || description.Contains("seepage", StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildDefectDescription(InspectionFinding finding)
    {
        if (finding.PitDepth.HasValue && finding.PitDepth.Value > 0.030)
        {
            return $"Localized measurable pitting up to {FormatMeasurement(finding.PitDepth.Value)} deep was observed. {finding.Description ?? string.Empty}".Trim();
        }

        return finding.Description ?? string.Empty;
    }

    private static string BuildRecommendationText(InspectionFinding finding)
    {
        var recommendationParts = new List<string>();
        var specificRepairRecommendation = finding.RepairRecommendation?.Trim();

        if (finding.RepairRequired)
        {
            recommendationParts.Add("Inspection findings indicate repair is required to restore component integrity.");
        }

        if (IsThicknessBelowMinimum(finding))
        {
            recommendationParts.Add(
                $"The lowest measured thickness recorded was {FormatMeasurement(finding.ThicknessResult!.Value)}, which is below the required minimum thickness of {FormatMeasurement(finding.MinimumRequiredThickness!.Value)}.");
        }

        if (!string.IsNullOrWhiteSpace(specificRepairRecommendation))
        {
            recommendationParts.Add(specificRepairRecommendation);
        }
        else if (IsLeakageFound(finding))
        {
            recommendationParts.Add("Repair the leaking component at the identified location and verify integrity following reassembly.");
        }

        if (finding.PitDepth.HasValue && finding.PitDepth.Value > 0.030)
        {
            recommendationParts.Add(
                $"Localized measurable pitting up to {FormatMeasurement(finding.PitDepth.Value)} deep was observed and should be evaluated for repair.");
        }

        recommendationParts = recommendationParts
            .Where(part => !string.IsNullOrWhiteSpace(part))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

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
