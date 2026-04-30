namespace iE.Core.Reports.Services;

public class ReportValidationService
{
    private const double MeasurablePittingThresholdInches = 0.030;

    public ReportValidationResult Validate(InspectionReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var messages = new List<ReportValidationMessage>();

        foreach (var finding in report.Findings)
        {
            if (finding.PhotoIds is null)
            {
                messages.Add(ReportValidationMessage.Error(
                    finding.Id,
                    "PHOTO_IDS_NULL",
                    "PhotoIds must not be null."));
            }

            if (finding.FindingType == FindingType.Pitting && !finding.PitDepth.HasValue)
            {
                messages.Add(ReportValidationMessage.Error(
                    finding.Id,
                    "PIT_DEPTH_MISSING",
                    "Pit depth is required when finding type is Pitting."));
            }

            if (finding.FindingType == FindingType.Pitting)
            {
                if (!finding.IsLocalized && !finding.IsGeneral)
                {
                    messages.Add(ReportValidationMessage.Error(
                        finding.Id,
                        "PITTING_CLASSIFICATION_MISSING",
                        "Pitting findings must be marked as either localized or general."));
                }

                if (finding.IsLocalized && finding.IsGeneral)
                {
                    messages.Add(ReportValidationMessage.Error(
                        finding.Id,
                        "PITTING_CLASSIFICATION_CONFLICT",
                        "Pitting findings cannot be marked as both localized and general."));
                }
            }

            if (finding.PitDepth.HasValue && finding.PitDepth.Value > MeasurablePittingThresholdInches)
            {
                messages.Add(ReportValidationMessage.Warning(
                    finding.Id,
                    "MEASURABLE_PITTING",
                    $"Pit depth ({FormatInches(finding.PitDepth.Value)}) exceeds measurable pitting threshold of {FormatInches(MeasurablePittingThresholdInches)}."));
            }

            if (finding.RepairRequired && string.IsNullOrWhiteSpace(finding.RepairRecommendation))
            {
                messages.Add(ReportValidationMessage.Error(
                    finding.Id,
                    "REPAIR_RECOMMENDATION_REQUIRED",
                    "Repair recommendation is required when repair is required."));
            }

            if (finding.RepairRequired && (finding.PhotoIds is null || finding.PhotoIds.Count == 0))
            {
                messages.Add(ReportValidationMessage.Warning(
                    finding.Id,
                    "REPAIR_PHOTO_MISSING",
                    "Repair is required, but no photo is attached."));
            }

            if (finding.DamageMechanism == DamageMechanismType.CUI && string.IsNullOrWhiteSpace(finding.InsulationCondition))
            {
                messages.Add(ReportValidationMessage.Warning(
                    finding.Id,
                    "CUI_INSULATION_CONDITION_MISSING",
                    "Damage mechanism is CUI, but insulation condition is missing."));
            }

            if (!string.IsNullOrWhiteSpace(finding.NdeMethod) && string.IsNullOrWhiteSpace(finding.NdeResult))
            {
                messages.Add(ReportValidationMessage.Warning(
                    finding.Id,
                    "NDE_RESULT_MISSING",
                    "NDE method is provided, but NDE result is missing."));
            }

            var belowMinimumThickness = finding.ThicknessResult.HasValue
                && finding.MinimumRequiredThickness.HasValue
                && finding.ThicknessResult.Value < finding.MinimumRequiredThickness.Value;

            if (belowMinimumThickness && string.IsNullOrWhiteSpace(finding.RepairRecommendation))
            {
                messages.Add(ReportValidationMessage.Error(
                    finding.Id,
                    "REPAIR_RECOMMENDATION_MISSING",
                    $"Thickness result ({FormatInches(finding.ThicknessResult!.Value)}) is below t-min ({FormatInches(finding.MinimumRequiredThickness!.Value)}), but no repair recommendation exists."));
            }
        }

        return new ReportValidationResult(messages);
    }

    private static string FormatInches(double value) => $"{value:0.000}\"";
}

public class ReportValidationResult
{
    public ReportValidationResult(List<ReportValidationMessage> messages)
    {
        Messages = messages;
    }

    public List<ReportValidationMessage> Messages { get; }
    public bool HasErrors => Messages.Any(x => x.Level == ReportValidationLevel.Error);
    public bool HasWarnings => Messages.Any(x => x.Level == ReportValidationLevel.Warning);
}

public class ReportValidationMessage
{
    public string? FindingId { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public ReportValidationLevel Level { get; init; }

    public static ReportValidationMessage Error(string? findingId, string code, string message) =>
        new()
        {
            FindingId = findingId,
            Code = code,
            Message = message,
            Level = ReportValidationLevel.Error
        };

    public static ReportValidationMessage Warning(string? findingId, string code, string message) =>
        new()
        {
            FindingId = findingId,
            Code = code,
            Message = message,
            Level = ReportValidationLevel.Warning
        };
}

public enum ReportValidationLevel
{
    Warning = 1,
    Error = 2
}
