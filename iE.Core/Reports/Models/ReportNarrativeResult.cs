namespace iE.Core.Reports.Services;

public class ReportNarrativeResult
{
    public ReportNarrativeSections Sections { get; set; } = new();
    public List<string> MissingDataWarnings { get; set; } = [];
}
