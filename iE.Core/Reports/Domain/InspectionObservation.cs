namespace iE.Core.Reports.Domain;

public enum ObservationStatus
{
    Acceptable = 1,
    Finding = 2,
    NotInspected = 3,
    NotApplicable = 4
}

public class InspectionObservation
{
    public string Id { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public ObservationStatus Status { get; set; } = ObservationStatus.Acceptable;
    public string? Notes { get; set; }
    public List<string> PhotoIds { get; set; } = new();
}
