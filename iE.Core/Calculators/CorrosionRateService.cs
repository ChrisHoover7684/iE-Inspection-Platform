using iE.Core.Calculators.Models;

namespace iE.Core.Calculators;

public class CorrosionRateService
{
    public CorrosionRateCalculationResult Calculate(CorrosionRateCalculationInput input)
    {
        var warnings = new List<string>();

        if (input.PreviousThicknessInches <= 0 || input.CurrentThicknessInches <= 0)
            throw new ArgumentException("Thickness values must be greater than zero.");

        if (input.CurrentDate <= input.PreviousDate)
            throw new ArgumentException("Current date must be after previous date.");

        if (input.RequiredThicknessTminInches.HasValue && input.RequiredThicknessTminInches <= 0)
            throw new ArgumentException("Required thickness (Tmin) must be greater than zero when provided.");

        if (input.CurrentThicknessInches > input.PreviousThicknessInches)
            warnings.Add("Current thickness is greater than previous thickness.");

        var years = (input.CurrentDate - input.PreviousDate).TotalDays / 365.25;
        if (years <= 0)
            throw new ArgumentException("Years between readings must be greater than zero.");

        var metalLoss = input.PreviousThicknessInches - input.CurrentThicknessInches;
        var corrosionRateInPerYear = metalLoss / years;
        var corrosionRateMpy = corrosionRateInPerYear * 1000.0;

        if (corrosionRateInPerYear < 0)
            warnings.Add("Calculated corrosion rate is negative.");

        double? remainingLife = null;
        if (input.RequiredThicknessTminInches.HasValue)
        {
            if (corrosionRateInPerYear <= 0)
            {
                warnings.Add("Remaining life cannot be calculated when corrosion rate is zero or negative.");
            }
            else
            {
                remainingLife = (input.CurrentThicknessInches - input.RequiredThicknessTminInches.Value) / corrosionRateInPerYear;
            }
        }

        return new CorrosionRateCalculationResult
        {
            MetalLossInches = Math.Round(metalLoss, 3),
            YearsBetweenReadings = Math.Round(years, 2),
            CorrosionRateInchesPerYear = Math.Round(corrosionRateInPerYear, 6),
            CorrosionRateMpy = Math.Round(corrosionRateMpy, 2),
            RemainingLifeYears = remainingLife.HasValue ? Math.Round(remainingLife.Value, 2) : null,
            Warnings = warnings
        };
    }
}
