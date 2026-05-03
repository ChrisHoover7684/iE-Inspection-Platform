using iE.Core.Mechanical.PressureVessels;

namespace iE.Tests.Mechanical;

public class PressureVesselServicesTests
{
    [Fact]
    public void CylindricalShell_CalculatesCircAndLongAndGoverning()
    {
        var service = new ShellThicknessService();
        var input = new CylindricalShellInput(150, 20000, 48, 0, 0.25, 1.0, 0.125, 0.5);

        var result = service.CalculateCylindrical(input);

        Assert.Equal(24, result.RadiusIn, 6);
        Assert.Equal(0.1808136614766449, result.CircumferentialRequiredThicknessIn, 12);
        Assert.Equal(0.08986520224694163, result.LongitudinalRequiredThicknessIn, 12);
        Assert.Equal(result.CircumferentialRequiredThicknessIn, result.GoverningRequiredThicknessIn, 4);
    }

    [Fact]
    public void SphericalShell_CalculatesThickness()
    {
        var service = new ShellThicknessService();
        var input = new SphericalShellInput(150, 20000, 48, 0, 0.25, 1.0, 0.125, 0.5);

        var result = service.CalculateSpherical(input);

        Assert.Equal(24, result.InsideRadiusIn, 6);
        Assert.Equal(0.09006755066299724, result.GoverningRequiredThicknessIn, 12);
    }

    [Fact]
    public void HeadThickness_EllipsoidalFormulaMatches()
    {
        var service = new HeadThicknessService();
        var input = new HeadThicknessInput(HeadType.Ellipsoidal2To1, 150, 20000, 1.0, 48, 24, 0, 0, 0, 0.125, 0.5);

        var result = service.Calculate(input);

        Assert.Equal(0.18013510132599448, result.GoverningRequiredThicknessIn, 12);
    }

    [Fact]
    public void CylindricalShell_ThicknessIncreasesWhenJointEfficiencyDecreases()
    {
        var service = new ShellThicknessService();

        var highEfficiency = new CylindricalShellInput(150, 20000, 48, 0, 0.25, 1.0, 0.125, 0.5);
        var lowEfficiency = new CylindricalShellInput(150, 20000, 48, 0, 0.25, 0.85, 0.125, 0.5);

        var highResult = service.CalculateCylindrical(highEfficiency);
        var lowResult = service.CalculateCylindrical(lowEfficiency);

        Assert.True(lowResult.CircumferentialRequiredThicknessIn > highResult.CircumferentialRequiredThicknessIn);
        Assert.True(lowResult.LongitudinalRequiredThicknessIn > highResult.LongitudinalRequiredThicknessIn);
    }

    [Fact]
    public void Nozzle_Ug45_GoverningThicknessCalculated()
    {
        var service = new NozzleThicknessService();
        var input = new NozzleThicknessInput("ASME Section VIII 1999", 150, 0, 100, 1.0, 0.0625, false, 20000, "SA-106", "B", "Seamless Pipe", CodeEra.Post1999, AttachmentLocation.Shell, 0.1, 0.08, 0.0625, NozzleType.PipeNozzle, false, true, 4.5, 4.0, 0.25, 0.25, "4", 0.109);

        var result = service.Calculate(input);

        Assert.True(result.GoverningRequiredThicknessIn > 0);
        Assert.Equal(Math.Max(result.TaRequiredThicknessIn + input.CorrosionAllowanceIn, result.TbRequiredThicknessIn), result.GoverningRequiredThicknessIn, 6);
    }
}
