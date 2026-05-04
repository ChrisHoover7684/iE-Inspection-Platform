using iE.Core.Mechanical.B313.Materials;

namespace iE.Tests.Mechanical;

public class B313MaterialStressServiceTests
{
    [Fact]
    public void AllowableStress_A106B_At579F_ReturnsExpectedPsi()
    {
        var service = new B313MaterialStressService();
        var stress = service.GetAllowableStressPsi("A106", "B", string.Empty, "K03006", string.Empty, 579);
        Assert.Equal(15447, Math.Round(stress ?? 0, 0));
    }

    [Fact]
    public void B313_ThicknessCase_Nps2_MatchesExpectedValue()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(
            PressurePsi: 200,
            TemperatureF: 579,
            OutsideDiameterIn: 2.375,
            Spec: "A106",
            Grade: "B",
            ProductForm: string.Empty,
            UnsNo: "K03006",
            ClassConditionTemper: string.Empty,
            MaterialCategory: "Ferritic",
            JointType: "Seamless",
            JointQualityKey: "FULL_RT",
            WFactor: 1.00,
            YOverride: 0.4,
            EOverride: 1.00));

        Assert.True(result.Success);
        Assert.Equal(0.0153, Math.Round(result.RequiredThicknessIn ?? 0, 4));
    }

    [Theory]
    [InlineData(0, 2.375, 1.0, 0.4, 1.0)]
    [InlineData(-1, 2.375, 1.0, 0.4, 1.0)]
    [InlineData(200, 0, 1.0, 0.4, 1.0)]
    [InlineData(200, 2.375, 0.0, 0.4, 1.0)]
    [InlineData(200, 2.375, 1.1, 0.4, 1.0)]
    [InlineData(200, 2.375, 1.0, -0.1, 1.0)]
    [InlineData(200, 2.375, 1.0, 0.8, 1.0)]
    [InlineData(200, 2.375, 1.0, 0.4, 0.0)]
    [InlineData(200, 2.375, 1.0, 0.4, 1.2)]
    public void CalculateThickness_InvalidInputs_ReturnsFailure(
        double pressurePsi,
        double outsideDiameterIn,
        double wFactor,
        double yOverride,
        double eOverride)
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(
            PressurePsi: pressurePsi,
            TemperatureF: 579,
            OutsideDiameterIn: outsideDiameterIn,
            Spec: "A106",
            Grade: "B",
            ProductForm: string.Empty,
            UnsNo: "K03006",
            ClassConditionTemper: string.Empty,
            MaterialCategory: "Ferritic",
            JointType: "Seamless",
            JointQualityKey: "FULL_RT",
            WFactor: wFactor,
            YOverride: yOverride,
            EOverride: eOverride));

        Assert.False(result.Success);
        Assert.Null(result.RequiredThicknessIn);
    }

    [Fact]
    public void CalculateThickness_MaterialNotFound_ContainsNormalizedDiagnostics()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(
            PressurePsi: 200,
            TemperatureF: 579,
            OutsideDiameterIn: 2.375,
            Spec: " a106 ",
            Grade: "not-a-grade",
            ProductForm: string.Empty,
            UnsNo: "K03006",
            ClassConditionTemper: string.Empty,
            MaterialCategory: "Ferritic",
            JointType: "Seamless",
            JointQualityKey: "FULL_RT"));

        Assert.False(result.Success);
        Assert.Contains("normalized", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("A106", result.Message);
        Assert.Contains("closeMatches", result.Message);
    }
}
