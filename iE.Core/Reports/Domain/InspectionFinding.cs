namespace iE.Core.Reports.Domain;

public class InspectionFinding
{
    public string Id { get; set; } = string.Empty;
    public FindingType FindingType { get; set; } = FindingType.Other;
    public DamageMechanismType DamageMechanism { get; set; } = DamageMechanismType.None;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public FindingSeverity Severity { get; set; } = FindingSeverity.None;
    public bool RepairRequired { get; set; }
    public string? RepairRecommendation { get; set; }
    public string? ComponentType { get; set; }
    public string? AssociatedChecklistItem { get; set; }
    public double? PitDepth { get; set; }
    public bool IsLocalized { get; set; }
    public bool IsGeneral { get; set; }
    public List<string> PhotoIds { get; set; } = new();
    public string? NdeMethod { get; set; }
    public string? NdeResult { get; set; }
    public string? InsulationCondition { get; set; }
    public double? ThicknessResult { get; set; }
    public double? MinimumRequiredThickness { get; set; }
    public double? CorrosionAllowance { get; set; }
    public string? LineNumber { get; set; }
    public double? ApproximateFeetOfFindings { get; set; }
}
