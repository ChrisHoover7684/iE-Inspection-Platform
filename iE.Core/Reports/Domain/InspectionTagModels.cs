namespace iE.Core.Reports.Domain;

public class InspectionRuleResult
{
    public List<InspectionTag> Tags { get; set; } = new();
    public List<InspectionRuleMessage> Messages { get; set; } = new();
    public bool HasErrors => Tags.Any(t => t.Severity == InspectionRuleSeverity.Error);
}

public class InspectionTag
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public InspectionRuleSeverity Severity { get; set; } = InspectionRuleSeverity.Info;
    public string? SourceFieldId { get; set; }
    public string? SourceFindingId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool RequiresAction { get; set; }
}

public class InspectionRuleMessage
{
    public string TagId { get; set; } = string.Empty;
    public InspectionRuleSeverity Severity { get; set; } = InspectionRuleSeverity.Info;
    public string Message { get; set; } = string.Empty;
    public string? SourceFieldId { get; set; }
    public string? SourceFindingId { get; set; }
}

public enum InspectionTagType
{
    Finding = 1,
    Observation = 2,
    RepairRecommendation = 3,
    Report = 4
}

public enum InspectionRuleSeverity
{
    Info = 1,
    Warning = 2,
    Error = 3
}
