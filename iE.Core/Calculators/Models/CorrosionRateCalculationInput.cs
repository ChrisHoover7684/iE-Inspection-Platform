namespace iE.Core.Calculators.Models;

public class CorrosionRateCalculationInput
{
    public double PreviousThicknessInches { get; set; }
    public double CurrentThicknessInches { get; set; }
    public DateTime PreviousDate { get; set; }
    public DateTime CurrentDate { get; set; }
    public double? RequiredThicknessTminInches { get; set; }
}
