namespace iE.Core.Equipment.Tanks.Models
{
    public class TankSettlementInput
    {
        public double TankDiameter { get; set; } // ft
        public List<double> SettlementReadings { get; set; } = new(); // inches
        public double OutOfPlaneLimitPercent { get; set; } = 1.0;
    }
}
