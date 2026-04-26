namespace iE.Core.FlangeJointIntegrity.Models
{
    public class FlangeAssemblyChecklistInput
    {
        public bool FlangeFacesCleaned { get; set; }
        public bool GasketNew { get; set; }
        public bool GasketCentered { get; set; }
        public bool BoltsCleanedOrNew { get; set; }
        public bool LubricantAppliedToThreadsAndNutFace { get; set; }
        public bool CrossPatternUsed { get; set; }
        public bool MultiPassTighteningUsed { get; set; }
        public bool FinalTorqueCheckComplete { get; set; }
        public bool LeakTestRequired { get; set; }
        public bool LeakTestComplete { get; set; }
        public string? Notes { get; set; }
    }
}
