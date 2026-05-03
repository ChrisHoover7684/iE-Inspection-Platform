namespace iE.Core.Mechanical.PressureVessels;

public sealed class ShellThicknessService
{
    public double ResolveCylindricalRadius(double insideDiameterIn, double outsideDiameterIn, double originalThicknessIn)
    {
        if (insideDiameterIn > 0) return insideDiameterIn / 2.0;
        if (outsideDiameterIn > 0 && originalThicknessIn > 0) return (outsideDiameterIn - (2.0 * originalThicknessIn)) / 2.0;
        return 0;
    }

    public double NormalizeCylindricalJointEfficiency(double jointEfficiency) => NormalizeJointEfficiency(jointEfficiency);
    public double NormalizeSphericalJointEfficiency(double jointEfficiency) => NormalizeJointEfficiency(jointEfficiency);

    public double ResolveSphericalInsideRadius(double insideDiameterIn, double outsideDiameterIn, double originalThicknessIn)
    {
        if (insideDiameterIn > 0) return insideDiameterIn / 2.0;
        if (outsideDiameterIn > 0 && originalThicknessIn > 0) return (outsideDiameterIn - (2.0 * originalThicknessIn)) / 2.0;
        return 0;
    }

    public CylindricalShellResult CalculateCylindrical(CylindricalShellInput input)
    {
        var radiusIn = ResolveCylindricalRadius(input.InsideDiameterIn, input.OutsideDiameterIn, input.OriginalThicknessIn);
        var jointEfficiency = NormalizeCylindricalJointEfficiency(input.JointEfficiency);

        var tCirc = CylindricalCircumferential(input.DesignPressurePsi, radiusIn, input.AllowableStressPsi, jointEfficiency);
        var tLong = CylindricalLongitudinal(input.DesignPressurePsi, radiusIn, input.AllowableStressPsi, jointEfficiency);

        var governing = Math.Max(tCirc, tLong);
        return new CylindricalShellResult(radiusIn, tCirc, tLong, governing, RequiredThicknessWithCa(governing, input.CorrosionAllowanceIn), CalculateMargin(input.ProvidedThicknessIn, governing, input.CorrosionAllowanceIn));
    }

    public SphericalShellResult CalculateSpherical(SphericalShellInput input)
    {
        var insideRadiusIn = ResolveSphericalInsideRadius(input.InsideDiameterIn, input.OutsideDiameterIn, input.OriginalThicknessIn);
        var jointEfficiency = NormalizeSphericalJointEfficiency(input.JointEfficiency);

        var t = Spherical(input.DesignPressurePsi, insideRadiusIn, input.AllowableStressPsi, jointEfficiency);
        return new SphericalShellResult(insideRadiusIn, t, RequiredThicknessWithCa(t, input.CorrosionAllowanceIn), CalculateMargin(input.ProvidedThicknessIn, t, input.CorrosionAllowanceIn));
    }

    public HeadThicknessResult CalculateConicalShell(double designPressurePsi, double diameterIn, double halfApexAngleDeg, double allowableStressPsi, double jointEfficiency, double corrosionAllowanceIn, double providedThicknessIn)
    {
        var t = Conical(designPressurePsi, diameterIn, halfApexAngleDeg, allowableStressPsi, jointEfficiency);
        return new HeadThicknessResult(t, RequiredThicknessWithCa(t, corrosionAllowanceIn), CalculateMargin(providedThicknessIn, t, corrosionAllowanceIn));
    }

    public static double CylindricalCircumferential(double pressure, double radius, double allowableStress, double jointEfficiency)
    {
        ValidatePositive(pressure, "Pressure must be greater than 0");
        ValidatePositive(radius, "Radius must be greater than 0");
        ValidatePositive(allowableStress, "Allowable stress must be greater than 0");
        ValidateEfficiency(jointEfficiency);

        var denominator = allowableStress * jointEfficiency - 0.6 * pressure;
        if (denominator <= 0) throw new ArgumentException("Denominator must be greater than zero. Check P, S, and E.");
        return (pressure * radius) / denominator;
    }

    public static double CylindricalLongitudinal(double pressure, double radius, double allowableStress, double jointEfficiency)
    {
        ValidatePositive(pressure, "Pressure must be greater than 0");
        ValidatePositive(radius, "Radius must be greater than 0");
        ValidatePositive(allowableStress, "Allowable stress must be greater than 0");
        ValidateEfficiency(jointEfficiency);

        var denominator = 2 * allowableStress * jointEfficiency + 0.4 * pressure;
        if (denominator <= 0) throw new ArgumentException("Denominator must be greater than zero. Check P, S, and E.");
        return (pressure * radius) / denominator;
    }

    public static double Spherical(double pressure, double radius, double allowableStress, double jointEfficiency)
    {
        ValidatePositive(pressure, "Pressure must be greater than 0");
        ValidatePositive(radius, "Inside radius must be greater than 0");
        ValidatePositive(allowableStress, "Allowable stress must be greater than 0");
        ValidateEfficiency(jointEfficiency);

        var denominator = 2 * allowableStress * jointEfficiency - 0.2 * pressure;
        if (denominator <= 0) throw new ArgumentException("Denominator must be greater than zero. Check P, S, and E.");
        return (pressure * radius) / denominator;
    }

    public static double Conical(double pressure, double diameter, double halfApexAngleDeg, double allowableStress, double jointEfficiency)
    {
        ValidatePositive(pressure, "Pressure must be greater than 0");
        ValidatePositive(diameter, "Diameter must be greater than 0");
        if (halfApexAngleDeg <= 0 || halfApexAngleDeg >= 90) throw new ArgumentException("Half-apex angle must be between 0 and 90 degrees");
        ValidatePositive(allowableStress, "Allowable stress must be greater than 0");
        ValidateEfficiency(jointEfficiency);

        var alphaRad = halfApexAngleDeg * Math.PI / 180.0;
        var denominator = 2 * Math.Cos(alphaRad) * (allowableStress * jointEfficiency - 0.6 * pressure);
        if (denominator <= 0) throw new ArgumentException("Denominator must be greater than zero. Check P, S, and E.");
        return (pressure * diameter) / denominator;
    }

    public static double RequiredThicknessWithCa(double requiredThickness, double corrosionAllowance) => requiredThickness + corrosionAllowance;
    public static double CalculateMargin(double providedThickness, double requiredThickness, double corrosionAllowance) => providedThickness - (requiredThickness + corrosionAllowance);

    private static void ValidatePositive(double value, string message)
    {
        if (value <= 0) throw new ArgumentException(message);
    }

    private static void ValidateEfficiency(double jointEfficiency)
    {
        if (jointEfficiency <= 0 || jointEfficiency > 1) throw new ArgumentException("Joint efficiency must be between 0 and 1");
    }

    private static double NormalizeJointEfficiency(double jointEfficiency)
    {
        ValidateEfficiency(jointEfficiency);
        return jointEfficiency;
    }
}
