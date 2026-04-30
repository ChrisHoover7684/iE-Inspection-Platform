namespace iE.Core.Reports.Models;

public class ReportData
{
    public string SummaryText { get; set; } = string.Empty;
    public List<string> Findings { get; set; } = new();
    public List<string> Repairs { get; set; } = new();
    public List<string> FutureRecommendations { get; set; } = new();
}

public class ThicknessEvaluationResult
{
    public double? LowestThickness { get; set; }
    public double? RequiredThickness { get; set; }
    public double? RemainingLifeYears { get; set; }
}
