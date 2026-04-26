namespace iE.Core.FlangeJointIntegrity.Models
{
    public enum GasketType
    {
        Soft,
        Hard,
        SpiralWound,
        RTJ,
        Other
    }

    public enum DefectLocation
    {
        InnerEdge,
        OuterEdge,
        AcrossSealingFace,
        Isolated,
        Unknown
    }

    public enum FacingType
    {
        RaisedFace,
        FlatFace,
        RTJ,
        TongueGroove,
        Other
    }

    public enum SerrationsCondition
    {
        Good,
        LightDamage,
        HeavyDamage,
        Missing,
        Unknown
    }

    public enum AssemblyCondition
    {
        New,
        ReusedGood,
        Corroded,
        Damaged,
        Unknown
    }

    public enum FlangeJointOverallStatus
    {
        Acceptable,
        Monitor,
        RepairRequired,
        EngineeringReviewRequired
    }

    public enum ReviewSeverity
    {
        Low,
        Medium,
        High
    }

    public enum RecommendationPriority
    {
        Info,
        Warning,
        Action,
        Critical
    }
}
