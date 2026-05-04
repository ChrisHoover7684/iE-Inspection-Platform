namespace iE.Core.Mechanical.B313.Materials;

public sealed record B313ThicknessRequest(
    double PressurePsi,
    double TemperatureF,
    double OutsideDiameterIn,
    string Spec,
    string Grade,
    string ProductForm,
    string UnsNo,
    string ClassConditionTemper,
    string MaterialCategory,
    string JointType,
    string JointQualityKey,
    double WFactor = 1.0,
    double? YOverride = null,
    double? EOverride = null
);

public sealed record B313ThicknessResult(
    bool Success,
    string Message,
    double? AllowableStressPsi,
    double? EFactor,
    double? YCoefficient,
    double? WFactor,
    double? RequiredThicknessIn
);
