using iE.Core.Mechanical.PipeData;

namespace iE.Tests.Mechanical;

public class PipeDataServiceTests
{
    [Theory]
    [InlineData("0.5", "10", 0.083)]
    [InlineData("2", "STD", 0.154)]
    [InlineData("6", "XS", 0.432)]
    [InlineData("10", "80S", 0.500)]
    [InlineData("12", "XXS", 1.000)]
    public void Lookup_ReturnsExpectedFields_ForCommonSizesAndSchedules(string nps, string schedule, double expectedThickness)
    {
        var service = new PipeDataService();

        var result = service.Lookup(new PipeDataInput { Nps = nps, Schedule = schedule });

        Assert.Equal(nps, result.Nps);
        Assert.Equal(schedule, result.Schedule);
        Assert.True(result.OutsideDiameter > 0);
        Assert.Equal(expectedThickness, result.NominalThickness, 3);
        Assert.Equal(Math.Round(result.OutsideDiameter - (2 * result.NominalThickness), 3), result.InsideDiameter, 3);
        Assert.Equal(Math.Round(result.NominalThickness * 0.875, 3), result.LowerLimitMinus12_5, 3);
        Assert.Equal(Math.Round(result.NominalThickness * 1.125, 3), result.UpperLimitPlus12_5, 3);
    }

    [Fact]
    public void GetNpsList_IncludesHalfInchThroughTwelveInch()
    {
        var service = new PipeDataService();
        var list = service.GetNpsList();

        Assert.Contains("0.5", list);
        Assert.Contains("12", list);
    }
}
