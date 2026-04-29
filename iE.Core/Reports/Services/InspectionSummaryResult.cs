namespace iE.Core.Reports.Services;

public class InspectionSummaryResult
{
    public string Summary { get; set; } = string.Empty;
    public string Repairs { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public string NdeAndTesting { get; set; } = string.Empty;
    public string ReturnToService { get; set; } = string.Empty;
}
