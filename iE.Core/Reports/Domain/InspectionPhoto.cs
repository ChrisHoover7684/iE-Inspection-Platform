namespace iE.Core.Reports.Domain;

public class InspectionPhoto
{
    public string Id { get; set; } = string.Empty;
    public string PhotoNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RelatedComponent { get; set; } = string.Empty;
    public string RelatedChecklistItem { get; set; } = string.Empty;
    public bool PhotoRequired { get; set; }
    public bool PhotoAttached { get; set; }
    public string? FileName { get; set; }
    public string? FileUrl { get; set; }
}
