namespace iE.Core.Calculators.Models;

public class CorrosionRateCalculationResult
{
    public double MetalLossInches { get; set; }
    public double YearsBetweenReadings { get; set; }
    public double CorrosionRateInchesPerYear { get; set; }
    public double CorrosionRateMpy { get; set; }
    public double? RemainingLifeYears { get; set; }
    public List<string> Warnings { get; set; } = [];
}
