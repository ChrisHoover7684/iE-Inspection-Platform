namespace iE.Core.FlangeJointIntegrity.Models
{
    public class BoltAssemblyConditionInput
    {
        public bool BoltMaterialKnown { get; set; }
        public AssemblyCondition BoltCondition { get; set; }
        public AssemblyCondition NutCondition { get; set; }
        public bool WasherUsed { get; set; }
        public bool LubricantUsed { get; set; }
        public bool LubricantKnown { get; set; }
        public bool StudsProtrudeBeyondNuts { get; set; }
        public bool ThreadDamageObserved { get; set; }
        public string? Notes { get; set; }
    }
}
