namespace iE.Core.Reports.Templates;

public class ReportTemplate
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Standard { get; set; } = string.Empty;
    public string EquipmentType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string InspectionScope { get; set; } = "External";
    public string? EquipmentFamily { get; set; }
    public string? EquipmentSubtype { get; set; }
    public List<ReportTemplateSection> Sections { get; set; } = new();
}
