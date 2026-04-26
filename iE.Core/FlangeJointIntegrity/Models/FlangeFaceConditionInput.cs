namespace iE.Core.FlangeJointIntegrity.Models
{
    public class FlangeFaceConditionInput
    {
        public FacingType FacingType { get; set; }
        public SerrationsCondition SerrationsCondition { get; set; }
        public bool CorrosionObserved { get; set; }
        public bool PittingObserved { get; set; }
        public bool MechanicalDamageObserved { get; set; }
        public bool WarpageSuspected { get; set; }
        public string? Notes { get; set; }
    }
}
