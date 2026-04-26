namespace iE.Core.FlangeJointIntegrity.Models
{
    public class FlangeJointInput
    {
        public GasketSeatingDefectInput? GasketDefect { get; set; }
        public FlangeFaceConditionInput? FlangeFace { get; set; }
        public BoltAssemblyConditionInput? BoltAssembly { get; set; }
        public FlangeAssemblyChecklistInput? Checklist { get; set; }

        // Basic torque / bolt stress support
        public double? TargetTorqueFtLbs { get; set; }
        public double? NutFactorK { get; set; }
        public double? BoltNominalDiameterInches { get; set; }
        public double? TensileStressAreaIn2 { get; set; }
        public string? Notes { get; set; }
    }
}
