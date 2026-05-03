namespace iE.Core.Mechanical.PressureVessels;

public sealed class HeadThicknessService
{
    public static double ResolveEffectiveInsideDiameter(double innerDiameterIn, double outerDiameterIn, double originalThicknessIn)
    {
        if (innerDiameterIn > 0) return innerDiameterIn;
        if (outerDiameterIn > 0 && originalThicknessIn > 0) return outerDiameterIn - (2.0 * originalThicknessIn);
        return 0;
    }

    public static bool AreGeometryConsistent(double innerDiameterIn, double outerDiameterIn, double originalThicknessIn, double toleranceIn = 0.001)
    {
        if (innerDiameterIn > 0 && outerDiameterIn > 0 && originalThicknessIn > 0)
            return Math.Abs(outerDiameterIn - (innerDiameterIn + (2.0 * originalThicknessIn))) <= toleranceIn;
        return true;
    }

    public static double GetDefaultFlatCFactor(string edgeCondition) => edgeCondition switch
    {
        "Integral welded flat head" => 0.30,
        "Bolted flat cover" => 0.33,
        "Loose / unrestrained flat plate" => 0.50,
        _ => 0.30
    };

    public static (double crownRadiusIn, double knuckleRadiusIn) GetAsmeFdTorisphericalDefaultsFromOd(double outerDiameterIn)
    {
        if (outerDiameterIn <= 0) return (0, 0);
        return (outerDiameterIn, 0.06 * outerDiameterIn);
    }

    public HeadThicknessResult Calculate(HeadThicknessInput input)
    {
        var t = input.HeadType switch
        {
            HeadType.Ellipsoidal2To1 => Ellipsoidal(input.DesignPressurePsi, input.EffectiveInsideDiameterIn, input.AllowableStressPsi, input.JointEfficiency),
            HeadType.Hemispherical => Hemispherical(input.DesignPressurePsi, input.EffectiveInsideRadiusIn, input.AllowableStressPsi, input.JointEfficiency),
            HeadType.TorisphericalAsmeFd => Torispherical(input.DesignPressurePsi, input.CrownRadiusIn, input.AllowableStressPsi, input.JointEfficiency),
            HeadType.Conical => Conical(input.DesignPressurePsi, input.EffectiveInsideDiameterIn, input.HalfApexAngleDeg, input.AllowableStressPsi, input.JointEfficiency),
            HeadType.Toriconical => Toriconical(input.DesignPressurePsi, input.EffectiveInsideDiameterIn, input.HalfApexAngleDeg, input.AllowableStressPsi, input.JointEfficiency),
            HeadType.FlatUg34 => Flat(input.DesignPressurePsi, input.EffectiveInsideDiameterIn, input.AllowableStressPsi, input.JointEfficiency, input.FlatHeadCFactor),
            _ => throw new ArgumentOutOfRangeException(nameof(input.HeadType), input.HeadType, "Unsupported head type")
        };

        return new HeadThicknessResult(t, t + input.CorrosionAllowanceIn, input.ProvidedThicknessIn - (t + input.CorrosionAllowanceIn));
    }

    public static double Ellipsoidal(double pressure, double diameter, double allowableStress, double jointEfficiency)
    {
        ValidateInputs(pressure, diameter, allowableStress, jointEfficiency, "Diameter must be greater than 0");
        var denominator = 2 * allowableStress * jointEfficiency - 0.2 * pressure;
        if (denominator <= 0) throw new ArgumentException("Invalid input: denominator must be greater than zero. Check P, S, and E.");
        return (pressure * diameter) / denominator;
    }

    public static double Hemispherical(double pressure, double insideRadius, double allowableStress, double jointEfficiency)
    {
        ValidateInputs(pressure, insideRadius, allowableStress, jointEfficiency, "Inside radius must be greater than 0");
        var denominator = 2 * allowableStress * jointEfficiency - 0.2 * pressure;
        if (denominator <= 0) throw new ArgumentException("Invalid input: denominator must be greater than zero. Check P, S, and E.");
        return (pressure * insideRadius) / denominator;
    }

    public static double Torispherical(double pressure, double crownRadius, double allowableStress, double jointEfficiency)
    {
        ValidateInputs(pressure, crownRadius, allowableStress, jointEfficiency, "Crown radius must be greater than 0");
        var denominator = allowableStress * jointEfficiency - 0.1 * pressure;
        if (denominator <= 0) throw new ArgumentException("Invalid input: denominator must be greater than zero. Check P, S, and E.");
        return (0.885 * pressure * crownRadius) / denominator;
    }

    public static double Conical(double pressure, double diameter, double halfApexAngleDeg, double allowableStress, double jointEfficiency)
    {
        ValidateInputs(pressure, diameter, allowableStress, jointEfficiency, "Diameter must be greater than 0");
        if (halfApexAngleDeg <= 0 || halfApexAngleDeg >= 90) throw new ArgumentException("Half-apex angle must be between 0 and 90 degrees");
        var alphaRad = halfApexAngleDeg * Math.PI / 180.0;
        var denominator = 2 * Math.Cos(alphaRad) * (allowableStress * jointEfficiency - 0.6 * pressure);
        if (denominator <= 0) throw new ArgumentException("Invalid input: denominator must be greater than zero. Check P, S, and E.");
        return (pressure * diameter) / denominator;
    }

    public static double Toriconical(double pressure, double diameter, double halfApexAngleDeg, double allowableStress, double jointEfficiency) =>
        Conical(pressure, diameter, halfApexAngleDeg, allowableStress, jointEfficiency);

    public static double Flat(double pressure, double diameter, double allowableStress, double jointEfficiency, double cFactor)
    {
        ValidateInputs(pressure, diameter, allowableStress, jointEfficiency, "Diameter must be greater than 0");
        if (cFactor <= 0) throw new ArgumentException("C factor must be greater than 0");
        var radicand = (pressure * cFactor) / (allowableStress * jointEfficiency);
        if (radicand <= 0) throw new ArgumentException("Invalid input: radicand must be greater than zero");
        return diameter * Math.Sqrt(radicand);
    }

    public static bool IsHemisphericalFormulaValid(double requiredThickness, double insideRadius, double pressure, double allowableStress, double jointEfficiency)
        => requiredThickness < 0.356 * insideRadius || pressure < 0.665 * allowableStress * jointEfficiency;

    public static bool IsTorisphericalFormulaValid(double requiredThickness, double crownRadius)
        => crownRadius > 0 && requiredThickness / crownRadius <= 0.002;

    private static void ValidateInputs(double pressure, double sizeTerm, double allowableStress, double jointEfficiency, string sizeError)
    {
        if (pressure <= 0) throw new ArgumentException("Pressure must be greater than 0");
        if (sizeTerm <= 0) throw new ArgumentException(sizeError);
        if (allowableStress <= 0) throw new ArgumentException("Allowable stress must be greater than 0");
        if (double.IsNaN(jointEfficiency) || double.IsInfinity(jointEfficiency)) throw new ArgumentException("Joint efficiency must be a finite value");
        if (jointEfficiency <= 0 || jointEfficiency > 1) throw new ArgumentException("Joint efficiency must be between 0 and 1");
    }
}
