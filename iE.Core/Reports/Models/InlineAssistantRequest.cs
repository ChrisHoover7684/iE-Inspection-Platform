using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Services;

public class InlineAssistantRequest
{
    public InspectionReport Report { get; set; } = new();
    public string CurrentFieldId { get; set; } = string.Empty;
    public string CurrentText { get; set; } = string.Empty;
    public string? SectionId { get; set; }
    public string? FindingId { get; set; }
    public int? CursorPosition { get; set; }
}
