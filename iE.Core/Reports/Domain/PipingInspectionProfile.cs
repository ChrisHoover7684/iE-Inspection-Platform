namespace iE.Core.Reports.Domain;

public class PipingInspectionProfile
{
    public List<string> LineNumbers { get; set; } = new();
    // TODO: Remove after API clients migrate to LineNumbers/finding-level LineNumber.
    public string? LineNumber { get; set; }
    // TODO: Remove after API clients migrate to finding-level ApproximateFeetOfFindings.
    public double? ApproximateFeetOfFindings { get; set; }
    public string? UpstreamEquipment { get; set; }
    public string? DownstreamEquipment { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public string? NominalPipeSize { get; set; }
    public string? PipingClass { get; set; }
    public string? InsulatedStatus { get; set; }
}
