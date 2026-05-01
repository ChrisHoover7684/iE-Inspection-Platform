using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Services;

public interface IInspectionAssistantService
{
    List<string> GetSuggestions(InspectionReport report, InspectionRuleResult rules);
    string ImproveText(string input, string? context);
    InlineAssistantResponse GetInlineSuggestions(InlineAssistantRequest request);
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

    public InlineAssistantResponse GetInlineSuggestions(InlineAssistantRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = new InlineAssistantResponse();
        var text = request.CurrentText?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(text))
            return response;

        var suggestions = new List<InlineAssistantSuggestion>();
        var lowered = text.ToLowerInvariant();

        if (lowered.Contains("pitting"))
            suggestions.Add(new InlineAssistantSuggestion("MissingData", "Include pit depth and indicate if pitting is localized or general.", "warning"));

        if (lowered.Contains("leak"))
        {
            suggestions.Add(new InlineAssistantSuggestion("MissingData", "Add precise leak location in the component/system.", "warning"));
            suggestions.Add(new InlineAssistantSuggestion("PhotoRequired", "Attach at least one supporting leak photo.", "critical"));
            suggestions.Add(new InlineAssistantSuggestion("RepairSuggestion", "Add a repair recommendation for the leak condition.", "critical"));
        }

        if (lowered.Contains("crack") || lowered.Contains("cracking"))
            suggestions.Add(new InlineAssistantSuggestion("NdeSuggestion", "Consider PT/MT (or equivalent NDE) and include crack location.", "warning"));

        if (lowered.Contains("cui"))
        {
            suggestions.Add(new InlineAssistantSuggestion("MissingData", "Document insulation condition and whether CUI is suspected.", "warning"));
            suggestions.Add(new InlineAssistantSuggestion("PhotoRequired", "Attach a photo showing insulation/jacketing condition for CUI context.", "warning"));
        }

        if (lowered.Contains("repair"))
            suggestions.Add(new InlineAssistantSuggestion("RepairSuggestion", "Reference WSA, IWR, or work order number for the repair action.", "warning"));

        if (LooksIncompleteSentence(text))
        {
            var improved = ImproveText(text, null);
            suggestions.Add(new InlineAssistantSuggestion("WordingImprovement", "Improve wording for a complete inspection statement.", "info", improved));
        }

        response.Suggestions = suggestions;
        response.Severity = suggestions.Any(s => s.Severity.Equals("critical", StringComparison.OrdinalIgnoreCase))
            ? "critical"
            : suggestions.Any(s => s.Severity.Equals("warning", StringComparison.OrdinalIgnoreCase)) ? "warning" : "info";

        return response;
    }

    private static bool LooksIncompleteSentence(string input)
    {
        if (input.Length < 12)
            return true;

        var hasPunctuation = input.EndsWith('.') || input.EndsWith('!') || input.EndsWith('?');
        var wordCount = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        return !hasPunctuation && wordCount < 6;
    }
}
