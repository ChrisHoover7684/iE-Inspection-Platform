namespace iE.Core.Reports.Services;

public class InlineAssistantResponse
{
    public List<InlineAssistantSuggestion> Suggestions { get; set; } = [];
    public string Severity { get; set; } = "none";
}
