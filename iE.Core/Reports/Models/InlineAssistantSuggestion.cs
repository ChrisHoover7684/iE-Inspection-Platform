namespace iE.Core.Reports.Services;

public class InlineAssistantSuggestion(string promptType, string suggestion, string severity, string? replacementText = null)
{
    public string PromptType { get; set; } = promptType;
    public string Suggestion { get; set; } = suggestion;
    public string Severity { get; set; } = severity;
    public string? ReplacementText { get; set; } = replacementText;
}
