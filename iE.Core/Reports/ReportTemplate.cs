namespace iE.Core.Reports;

public class ReportTemplate
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Standard { get; set; } = string.Empty;
    public string EquipmentType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ReportTemplateSection> Sections { get; set; } = new();
}
