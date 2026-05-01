namespace iE.Core.Reports.Services;

public class InlineAssistantResponse
{
    public List<InlineAssistantSuggestion> Suggestions { get; set; } = [];
    public string Severity { get; set; } = "none";
    public bool WasEvaluated { get; set; }
    public string Mode { get; set; } = "Disabled";
}
