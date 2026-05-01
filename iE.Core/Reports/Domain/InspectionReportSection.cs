namespace iE.Core.Reports.Domain;

public class InspectionReportSection
{
    public string SectionId { get; set; } = string.Empty;
    public string SectionTitle { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsRepeatable { get; set; }
    public int? InstanceNumber { get; set; }
    public List<InspectionReportAnswer> Answers { get; set; } = new();
}
