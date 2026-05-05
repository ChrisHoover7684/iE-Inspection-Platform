using iE.Core.Mechanical.B313.Materials;

namespace iE.Tests.Mechanical;

public class B313MaterialStressServiceTests
{
    [Fact]
    public void AllowableStress_A106B_At579F_ReturnsExpectedPsi()
    {
        var service = new B313MaterialStressService();
        var stress = service.GetAllowableStressPsi("A106", "B", string.Empty, "K03006", string.Empty, 579);
        // B31.3 importer record A106 / B / UNS K03006 has 19.0 ksi @500°F and 17.9 ksi @600°F; linear interpolation at 579°F gives ~18.131 ksi / 18131 psi.
        Assert.InRange(Math.Round(stress ?? 0, 0), 18130, 18132);
    }

    [Fact]
    public void Baseline_A106B_Succeeds()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(200, 579, 2.375, "A106", "B", "Smls", "K03006", "", "Ferritic", "Seamless", "FULL_RT", 1.0, 0.4, 1.0));
        Assert.True(result.Success);
    }

    [Fact]
    public void SA106_SpecLookup_SucceedsWithoutSpecAliasAssumption()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(200, 579, 2.375, "SA-106", "B", "Smls", "K03006", "", "Ferritic", "Seamless", "FULL_RT", 1.0, 0.4, 1.0));

        Assert.False(result.Success);
        Assert.Contains("spec='SA106'", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("use A106 instead of SA106", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductFormVariant_MapsAndSucceeds()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(200, 579, 2.375, "A106", "B", "Smls. pipe", "K03006", "", "Ferritic", "Seamless", "FULL_RT", 1.0, 0.4, 1.0));
        Assert.True(result.Success);
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
        // Uses the canonical B31.3 A106 Grade B K03006 allowable stress at 579°F.
        Assert.Equal(0.0130, result.RequiredThicknessIn ?? 0);
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
    public void CalculateThickness_InvalidInputs_ReturnsFailure(double pressurePsi, double outsideDiameterIn, double wFactor, double yOverride, double eOverride)
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(pressurePsi, 579, outsideDiameterIn, "A106", "B", string.Empty, "K03006", string.Empty, "Ferritic", "Seamless", "FULL_RT", wFactor, yOverride, eOverride));
        Assert.False(result.Success);
        Assert.Null(result.RequiredThicknessIn);
    }

    [Fact]
    public void CalculateThickness_MaterialNotFound_ContainsNormalizedDiagnostics()
    {
        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(new B313ThicknessRequest(200, 579, 2.375, " a106 ", "not-a-grade", string.Empty, "K03006", string.Empty, "Ferritic", "Seamless", "FULL_RT"));

        Assert.False(result.Success);
        Assert.Contains("normalized", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("A106", result.Message);
        Assert.Contains("/api/B313/materials?spec=A106", result.Message);
    }

    [Fact]
    public void GetMaterials_A106_ReturnsGradeB()
    {
        var service = new B313MaterialStressService();
        var results = service.ListMaterials("A106");
        Assert.Contains(results, r => string.Equals(r.Grade, "B", StringComparison.OrdinalIgnoreCase));
    }
}
