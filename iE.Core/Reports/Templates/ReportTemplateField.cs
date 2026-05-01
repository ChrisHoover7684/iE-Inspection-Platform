namespace iE.Core.Reports.Templates;

public class ReportTemplateField
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string DataType { get; set; } = "text";
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }

    public bool IsChecklistItem { get; set; }
    public bool AllowsComment { get; set; }
    public bool AllowsPhotoFlag { get; set; }
    public bool AllowsTransferToComponentSection { get; set; }
    public bool AllowsRecommendationFlag { get; set; }
    public string? HelpText { get; set; }
    public List<string> Options { get; set; } = new();
}
