namespace iE.Core.Mechanical.PressureVessels;

public sealed class HeadThicknessService
{
    public HeadThicknessResult Calculate(HeadThicknessInput input)
    {
        var t = input.HeadType switch
        {
            HeadType.Ellipsoidal2To1 => (input.DesignPressurePsi * input.EffectiveInsideDiameterIn) /
                                        ((2.0 * input.AllowableStressPsi * input.JointEfficiency) - (0.2 * input.DesignPressurePsi)),

            HeadType.Hemispherical => (input.DesignPressurePsi * input.EffectiveInsideRadiusIn) /
                                      ((2.0 * input.AllowableStressPsi * input.JointEfficiency) - (0.2 * input.DesignPressurePsi)),

            HeadType.TorisphericalAsmeFd => (0.885 * input.DesignPressurePsi * input.CrownRadiusIn) /
                                            ((input.AllowableStressPsi * input.JointEfficiency) - (0.1 * input.DesignPressurePsi)),

            HeadType.Conical or HeadType.Toriconical => CalculateConicalThickness(input),

            HeadType.FlatUg34 => input.EffectiveInsideDiameterIn * Math.Sqrt((input.FlatHeadCFactor * input.DesignPressurePsi) /
                                                                              (input.AllowableStressPsi * input.JointEfficiency)),
            _ => throw new ArgumentOutOfRangeException(nameof(input.HeadType), input.HeadType, "Unsupported head type")
        };

        var requiredWithCa = t + input.CorrosionAllowanceIn;
        var margin = input.ProvidedThicknessIn - requiredWithCa;

        return new HeadThicknessResult(t, requiredWithCa, margin);
    }

    private static double CalculateConicalThickness(HeadThicknessInput input)
    {
        var radians = Math.PI * input.HalfApexAngleDeg / 180.0;
        return (input.DesignPressurePsi * input.EffectiveInsideDiameterIn) /
               (2.0 * Math.Cos(radians) * ((input.AllowableStressPsi * input.JointEfficiency) - (0.6 * input.DesignPressurePsi)));
    }
}
