namespace iE.Core.Reports.Templates;

public class ReportTemplateSection
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsRepeatable { get; set; }
    public List<ReportTemplateField> Fields { get; set; } = new();
}
