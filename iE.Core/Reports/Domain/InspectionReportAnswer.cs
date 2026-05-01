namespace iE.Core.Reports.Domain;

public class InspectionReportAnswer
{
    public string FieldId { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string DataType { get; set; } = "text";
    public string? Value { get; set; }
    public List<string> Values { get; set; } = new();
    public string? Comment { get; set; }
    public bool? PhotoRequired { get; set; }
    public bool? TransferToComponentSection { get; set; }
    public bool? RecommendationRequired { get; set; }
}
