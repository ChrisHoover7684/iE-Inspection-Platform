using iE.Core.Mechanical.B313.Materials;

namespace iE.Tests.Mechanical;

public class B313MaterialStressServiceTests
{
    [Fact]
    public void AllowableStress_IsReturnedInPsi()
    {
        var service = new B313MaterialStressService();
        var stress = service.GetAllowableStressPsi("A106", "B", "Smls", "K03006", "...", 579);
        Assert.NotNull(stress);
        Assert.True(stress > 10000);
    }

    [Fact]
    public void B313_ThicknessCase_MatchesPortedLogic()
    {
        var service = new B313MaterialStressService();
        var stress = service.GetAllowableStressPsi("A106", "B", "Smls", "K03006", "...", 579);
        Assert.NotNull(stress);

        const double nps2OutsideDiameter = 2.375;
        double thickness = B313MaterialStressService.CalculateRequiredThickness(
            pressurePsi: 200,
            outsideDiameterIn: nps2OutsideDiameter,
            allowableStressPsi: stress!.Value,
            qualityFactorE: 1.00,
            yCoefficient: 0.4,
            wFactor: 1.00);

        Assert.InRange(thickness, 0.01, 0.2);
    }
}
