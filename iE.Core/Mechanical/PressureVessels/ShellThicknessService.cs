namespace iE.Core.Mechanical.PressureVessels;

public sealed class ShellThicknessService
{
    public CylindricalShellResult CalculateCylindrical(CylindricalShellInput input)
    {
        var tCirc = (input.DesignPressurePsi * input.InsideRadiusIn) /
                    ((input.AllowableStressPsi * input.JointEfficiency) - (0.6 * input.DesignPressurePsi));

        var tLong = (input.DesignPressurePsi * input.InsideRadiusIn) /
                    ((2.0 * input.AllowableStressPsi * input.JointEfficiency) + (0.4 * input.DesignPressurePsi));

        var governing = Math.Max(tCirc, tLong);
        var requiredWithCa = governing + input.CorrosionAllowanceIn;
        var margin = input.ProvidedThicknessIn - requiredWithCa;

        return new CylindricalShellResult(tCirc, tLong, governing, requiredWithCa, margin);
    }

    public SphericalShellResult CalculateSpherical(SphericalShellInput input)
    {
        var t = (input.DesignPressurePsi * input.InsideRadiusIn) /
                ((2.0 * input.AllowableStressPsi * input.JointEfficiency) - (0.2 * input.DesignPressurePsi));

        var requiredWithCa = t + input.CorrosionAllowanceIn;
        var margin = input.ProvidedThicknessIn - requiredWithCa;

        return new SphericalShellResult(t, requiredWithCa, margin);
    }

    public HeadThicknessResult CalculateConicalShell(
        double designPressurePsi,
        double diameterIn,
        double halfApexAngleDeg,
        double allowableStressPsi,
        double jointEfficiency,
        double corrosionAllowanceIn,
        double providedThicknessIn)
    {
        var radians = Math.PI * halfApexAngleDeg / 180.0;
        var t = (designPressurePsi * diameterIn) /
                (2.0 * Math.Cos(radians) * ((allowableStressPsi * jointEfficiency) - (0.6 * designPressurePsi)));

        var requiredWithCa = t + corrosionAllowanceIn;
        var margin = providedThicknessIn - requiredWithCa;
        return new HeadThicknessResult(t, requiredWithCa, margin);
    }
}
