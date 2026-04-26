namespace iE.Core.Reports;

public class ReportingHierarchyNode
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? ParentId { get; set; }
    public string? TemplateId { get; set; }
    public List<ReportingHierarchyNode> Children { get; set; } = new();
}
