namespace iE.Core.Reports;

public enum FindingType
{
    None = 0,
    Pitting = 1,
    Cracking = 2,
    Corrosion = 3,
    MetalLoss = 4,
    Deformation = 5,
    CoatingFailure = 6,
    Leak = 7,
    Other = 99
}

public enum DamageMechanismType
{
    None = 0,
    CUI = 1,
    Hic = 2,
    Ssc = 3,
    ErosionCorrosion = 4,
    Fatigue = 5,
    ThermalFatigue = 6,
    ExternalCorrosion = 7,
    InternalCorrosion = 8,
    Other = 99
}

public enum FindingSeverity
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}
