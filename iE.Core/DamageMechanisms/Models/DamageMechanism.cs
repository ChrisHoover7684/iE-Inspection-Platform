using System.Collections.Generic;

namespace iE.Core.DamageMechanisms.Models
{
    public class DamageMechanism
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string AffectedMaterials { get; set; } = "";
        public string AffectedUnits { get; set; } = "";
        public string CriticalFactors { get; set; } = "";
        public string Prevention { get; set; } = "";
        public string Inspection { get; set; } = "";
        public string RiskLevel { get; set; } = "";
        public List<string> RelatedMechanisms { get; set; } = new();
    }
}