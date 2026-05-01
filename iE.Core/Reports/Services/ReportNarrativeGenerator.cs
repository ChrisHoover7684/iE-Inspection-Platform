using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Services;

public interface IReportNarrativeGenerator
{
    ReportNarrativeResult Generate(InspectionReport report);
}

public class ReportNarrativeGenerator : IReportNarrativeGenerator
{
    public ReportNarrativeResult Generate(InspectionReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var warnings = ValidateRequiredFields(report);

        var summary = BuildSummary(report);
        var inspection = BuildInspection(report);
        var findings = BuildFindings(report);
        var nde = BuildNde(report);
        var repairs = BuildRepairs(report);
        var recommendations = BuildRecommendations(report);
        var returnToService = BuildReturnToService(report);

        return new ReportNarrativeResult
        {
            Sections = new ReportNarrativeSections
            {
                Summary = summary,
                Inspection = inspection,
                Findings = findings,
                NdeTesting = nde,
                Repairs = repairs,
                Recommendations = recommendations,
                ReturnToService = returnToService
            },
            MissingDataWarnings = warnings
        };
    }

    private static List<string> ValidateRequiredFields(InspectionReport report)
    {
        var warnings = new List<string>();

        if (string.IsNullOrWhiteSpace(report.TemplateId)) warnings.Add("Missing template ID.");
        if (string.IsNullOrWhiteSpace(report.EquipmentTag) && string.IsNullOrWhiteSpace(report.AssetId)) warnings.Add("Missing equipment tag or tank/asset ID.");
        if (string.IsNullOrWhiteSpace(report.Unit)) warnings.Add("Missing unit.");
        if (string.IsNullOrWhiteSpace(report.Service)) warnings.Add("Missing service.");

        return warnings;
    }

    private static string BuildSummary(InspectionReport report)
    {
        var lineId = FirstNonEmpty(report.EquipmentTag, report.PipingProfile?.LineNumber, report.AssetId, "the inspected equipment");
        var hasObservations = report.Findings.Count > 0;
        var noObservations = !hasObservations;

        if (noObservations)
            return $"External visual inspection completed for {lineId}. No reportable conditions were identified during this inspection.";

        var repairCount = report.Findings.Count(f => f.RepairRequired || !string.IsNullOrWhiteSpace(f.RepairRecommendation));
        var findingCountText = CountNoun(report.Findings.Count, "reportable finding");
        return repairCount > 0
            ? $"External visual inspection completed for {lineId}. {findingCountText} were documented, with {repairCount} requiring repair action."
            : $"External visual inspection completed for {lineId}. {findingCountText} were documented.";
    }

    private static string BuildInspection(InspectionReport report)
    {
        var id = FirstNonEmpty(report.EquipmentTag, report.PipingProfile?.LineNumber, report.AssetId, "unspecified equipment");
        return $"Inspection performed on {id} in {DefaultText(report.Unit)} unit, {DefaultText(report.Service)} service.";
    }

    private static string BuildFindings(InspectionReport report)
    {
        if (report.Findings.Count == 0)
            return "No reportable conditions were identified during this inspection.";

        var lines = report.Findings.Select(f =>
        {
            var location = DefaultText(f.Location, "location not provided");
            var description = DefaultText(f.Description, "description not provided");
            var type = ToPhrase(f.FindingType);
            return $"{type} was documented at {location}. {EnsureSentence(description)}";
        });

        return string.Join(" ", lines);
    }

    private static string BuildNde(InspectionReport report)
    {
        var ndeFindings = report.Findings
            .Where(f => !string.IsNullOrWhiteSpace(f.NdeMethod) || !string.IsNullOrWhiteSpace(f.NdeResult))
            .ToList();

        if (ndeFindings.Count == 0)
            return "No NDE or additional testing data was documented.";

        return string.Join(" ", ndeFindings.Select(f =>
            $"Item {DefaultText(f.Id, "N/A")}: NDE method {DefaultText(f.NdeMethod, "not specified")}, result {DefaultText(f.NdeResult, "not specified")}."));
    }

    private static string BuildRepairs(InspectionReport report)
    {
        var repairFindings = report.Findings
            .Where(f => f.RepairRequired || !string.IsNullOrWhiteSpace(f.RepairRecommendation))
            .ToList();

        if (repairFindings.Count == 0)
            return "No repairs were identified in this report.";

        return string.Join(" ", repairFindings.Select(f =>
            $"Item {DefaultText(f.Id, "N/A")}: {DefaultText(f.RepairRecommendation, "Repair required; recommendation not provided")}."));
    }

    private static string BuildRecommendations(InspectionReport report)
    {
        if (report.Findings.Count == 0)
            return "Continue routine monitoring and inspection per applicable program requirements.";

        var activeLeak = report.Findings.Any(f => f.FindingType == FindingType.Leak);
        if (activeLeak)
        {
            return "Prioritize leak-related repairs and verify leak resolution after corrective work, when verification data is documented.";
        }

        var recs = report.Findings
            .Where(f => !string.IsNullOrWhiteSpace(f.RepairRecommendation))
            .Select(f => f.RepairRecommendation!.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return recs.Count > 0
            ? string.Join(" ", recs.Select(EnsureSentence))
            : "Address documented findings based on severity and applicable integrity requirements.";
    }

    private static string BuildReturnToService(InspectionReport report)
    {
        var hasLeak = report.Findings.Any(f => f.FindingType == FindingType.Leak);
        var hasRepairRequired = report.Findings.Any(f => f.RepairRequired);

        if (!hasRepairRequired && !hasLeak)
            return "No reportable conditions were identified that would prevent continued service.";

        if (hasLeak)
            return "Return to service should follow completion of leak-related repairs and documented verification, where required.";

        return "Return to service should follow completion of required repairs and standard verification.";
    }

    private static string DefaultText(string? value, string fallback = "not provided") =>
        string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();

    private static string FirstNonEmpty(params string?[] values)
    {
        var found = values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
        return string.IsNullOrWhiteSpace(found) ? "not provided" : found.Trim();
    }

    private static string CountNoun(int count, string singularNoun) =>
        count == 1 ? $"1 {singularNoun}" : $"{count} {singularNoun}s";

    private static string EnsureSentence(string value)
    {
        var trimmed = value.Trim();
        if (trimmed.EndsWith('.') || trimmed.EndsWith('!') || trimmed.EndsWith('?'))
            return trimmed;

        return $"{trimmed}.";
    }

    private static string ToPhrase(FindingType findingType) => findingType switch
    {
        FindingType.Corrosion => "Localized corrosion",
        FindingType.Cracking => "Cracking",
        FindingType.Leak => "Leak indication",
        FindingType.Deformation => "Deformation",
        FindingType.Other => "Anomaly",
        _ => "Finding"
    };
}
