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
}
