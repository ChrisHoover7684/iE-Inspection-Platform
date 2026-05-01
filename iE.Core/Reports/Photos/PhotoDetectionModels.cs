namespace iE.Core.Reports.Photos;

public enum PhotoDetectionType
{
    Corrosion,
    CoatingDamage,
    LeakStaining,
    MechanicalDamage,
    MissingBolting,
    InsulationDamage,
    CuiIndicator,
    Unknown
}

public enum PhotoDetectionSeverity
{
    Low,
    Medium,
    High
}

public class PhotoDetection
{
    public PhotoDetectionType Type { get; set; } = PhotoDetectionType.Unknown;
    public PhotoDetectionSeverity Severity { get; set; } = PhotoDetectionSeverity.Low;
    public decimal Confidence { get; set; }
    public string Rationale { get; set; } = string.Empty;
}

public class PhotoDetectionRequest
{
    public string PhotoId { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public string? FileUrl { get; set; }
    public string? Base64Image { get; set; }
    public string? Description { get; set; }
    public string? RelatedComponent { get; set; }
    public string? RelatedChecklistItem { get; set; }
}

public class PhotoDetectionResult
{
    public string PhotoId { get; set; } = string.Empty;
    public List<PhotoDetection> Detections { get; set; } = [];
}
