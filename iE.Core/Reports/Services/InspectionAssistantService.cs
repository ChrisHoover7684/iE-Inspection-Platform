using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Services;

public interface IInspectionAssistantService
{
    List<string> GetSuggestions(InspectionReport report, InspectionRuleResult rules);
    string ImproveText(string input, string? context);
}

public class InspectionAssistantService : IInspectionAssistantService
{
    public List<string> GetSuggestions(InspectionReport report, InspectionRuleResult rules)
    {
        ArgumentNullException.ThrowIfNull(report);
        ArgumentNullException.ThrowIfNull(rules);

        var suggestions = new List<string>();

        foreach (var tag in rules.Tags.Where(x => x.RequiresAction))
        {
            if (tag.Id == "repair-recommendation-missing")
                suggestions.Add("Add a clear repair recommendation for each flagged finding.");

            if (tag.Id == "photo-required")
                suggestions.Add("Attach at least one supporting photo for each flagged finding.");

            if (tag.Id == "nde-required")
                suggestions.Add("Add an NDE method and result for leak-related findings.");

            if (tag.Id == "repair-required")
                suggestions.Add("Complete missing finding details and required repair fields.");
        }

        foreach (var finding in report.Findings)
        {
            if (string.IsNullOrWhiteSpace(finding.Location) || string.IsNullOrWhiteSpace(finding.Description))
                suggestions.Add($"Finding {finding.Id}: include both location and description.");

            if ((finding.FindingType == FindingType.Leak || finding.FindingType == FindingType.Cracking)
                && string.IsNullOrWhiteSpace(finding.NdeMethod)
                && string.IsNullOrWhiteSpace(finding.NdeResult))
            {
                suggestions.Add($"Finding {finding.Id}: consider adding NDE (UT/MT/PT) to characterize the {finding.FindingType.ToString().ToLowerInvariant()}.");
            }

            if (finding.RepairRequired && string.IsNullOrWhiteSpace(finding.RepairRecommendation))
                suggestions.Add($"Finding {finding.Id}: repair is required; provide a recommendation and target priority.");
        }

        return suggestions
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public string ImproveText(string input, string? context)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        var cleaned = input.Trim();
        cleaned = char.ToUpperInvariant(cleaned[0]) + cleaned[1..];

        if (!cleaned.EndsWith('.') && !cleaned.EndsWith('!') && !cleaned.EndsWith('?'))
            cleaned += ".";

        if (string.IsNullOrWhiteSpace(context))
            return cleaned;

        return $"{cleaned} ({context.Trim()})";
    }
}
