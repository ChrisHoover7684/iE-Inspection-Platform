namespace iE.Core.Reports.Models;

public class ReportData
{
    public string? ReportId { get; set; }
    public string? EquipmentTag { get; set; }
    public string? CircuitId { get; set; }
    public List<ThicknessEvaluationResult> ThicknessEvaluations { get; set; } = new();
}

public class ThicknessEvaluationResult
{
    public string? CmlName { get; set; }
    public double? MeasuredThickness { get; set; }
    public double? RequiredThickness { get; set; }
    public double? CorrosionRate { get; set; }
    public double? RemainingLifeYears { get; set; }
    public bool IsAcceptable { get; set; }
    public string? Notes { get; set; }
}
