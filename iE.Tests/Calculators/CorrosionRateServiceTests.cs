using iE.Core.Calculators;
using iE.Core.Calculators.Models;

namespace iE.Tests.Calculators;

public class CorrosionRateServiceTests
{
    [Fact]
    public void Calculate_ReturnsExpectedValues_ForKnownInput()
    {
        var service = new CorrosionRateService();
        var input = new CorrosionRateCalculationInput
        {
            PreviousThicknessInches = 0.280,
            CurrentThicknessInches = 0.250,
            PreviousDate = new DateTime(2020, 1, 1),
            CurrentDate = new DateTime(2025, 1, 1),
            RequiredThicknessTminInches = 0.180
        };

        var result = service.Calculate(input);

        Assert.Equal(0.030, result.MetalLossInches, 3);
        Assert.Equal(5.00, result.YearsBetweenReadings, 2);
        Assert.Equal(0.006, result.CorrosionRateInchesPerYear, 3);
        Assert.Equal(6.0, result.CorrosionRateMpy, 1);
        Assert.Equal(11.67, result.RemainingLifeYears, 2);
    }
}
