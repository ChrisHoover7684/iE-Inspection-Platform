namespace iE.Core.Mechanical.PressureVessels;

public sealed record CylindricalShellInput(
    double DesignPressurePsi,
    double InsideRadiusIn,
    double AllowableStressPsi,
    double JointEfficiency,
    double CorrosionAllowanceIn,
    double ProvidedThicknessIn);

public sealed record CylindricalShellResult(
    double CircumferentialRequiredThicknessIn,
    double LongitudinalRequiredThicknessIn,
    double GoverningRequiredThicknessIn,
    double RequiredWithCorrosionAllowanceIn,
    double MarginIn);

public sealed record SphericalShellInput(
    double DesignPressurePsi,
    double InsideRadiusIn,
    double AllowableStressPsi,
    double JointEfficiency,
    double CorrosionAllowanceIn,
    double ProvidedThicknessIn);

public sealed record SphericalShellResult(
    double GoverningRequiredThicknessIn,
    double RequiredWithCorrosionAllowanceIn,
    double MarginIn);

public enum HeadType
{
    Ellipsoidal2To1,
    Hemispherical,
    TorisphericalAsmeFd,
    Conical,
    Toriconical,
    FlatUg34,
}

public sealed record HeadThicknessInput(
    HeadType HeadType,
    double DesignPressurePsi,
    double AllowableStressPsi,
    double JointEfficiency,
    double EffectiveInsideDiameterIn,
    double EffectiveInsideRadiusIn,
    double CrownRadiusIn,
    double HalfApexAngleDeg,
    double FlatHeadCFactor,
    double CorrosionAllowanceIn,
    double ProvidedThicknessIn);

public sealed record HeadThicknessResult(
    double GoverningRequiredThicknessIn,
    double RequiredWithCorrosionAllowanceIn,
    double MarginIn);
