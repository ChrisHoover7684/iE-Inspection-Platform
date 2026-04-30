namespace iE.Core.Reports;

public class RepairRecommendation
{
    public string Id { get; set; } = string.Empty;
    public string FindingId { get; set; } = string.Empty;
    public string EquipmentTag { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public FindingType FindingType { get; set; } = FindingType.Other;
    public DamageMechanismType DamageMechanism { get; set; } = DamageMechanismType.None;
    public string DefectDescription { get; set; } = string.Empty;
    public string RecommendationText { get; set; } = string.Empty;
    public RepairRecommendationPriority Priority { get; set; } = RepairRecommendationPriority.Medium;
    public string Basis { get; set; } = string.Empty;
    public List<string> RelatedPhotoIds { get; set; } = new();
    public RepairRecommendationStatus Status { get; set; } = RepairRecommendationStatus.Draft;
}

public enum RepairRecommendationStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Completed = 3,
    Closed = 4
}

public enum RepairRecommendationPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}
