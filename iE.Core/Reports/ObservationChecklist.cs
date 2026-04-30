namespace iE.Core.Reports;

public class ObservationChecklistTemplate
{
    public string TemplateId { get; set; } = string.Empty;
    public List<ObservationChecklistItem> Items { get; set; } = new();
}

public class ObservationChecklistItem
{
    public string Id { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public ObservationStatus DefaultStatus { get; set; } = ObservationStatus.Acceptable;
    public bool RequiresPhoto { get; set; }
    public List<string> AppliesToTemplateIds { get; set; } = new();
}

public class ObservationChecklistItemResponse
{
    public string ItemId { get; set; } = string.Empty;
    public ObservationStatus Status { get; set; } = ObservationStatus.Acceptable;
    public string? Notes { get; set; }
    public List<string> PhotoIds { get; set; } = new();
    public InspectionFinding? Finding { get; set; }
}

public class ObservationChecklistBuildResult
{
    public List<InspectionObservation> Observations { get; set; } = new();
    public List<InspectionFinding> Findings { get; set; } = new();
}
