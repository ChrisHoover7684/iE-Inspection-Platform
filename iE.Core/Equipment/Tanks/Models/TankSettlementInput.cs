namespace iE.Core.Equipment.Tanks.Models
{
    public class TankSettlementInput
    {
        public double TankDiameter { get; set; } // ft
        public List<double> SettlementElevationPoints { get; set; } = new(); // inches
        public double ReferenceElevation { get; set; } // inches
        public double CenterElevation { get; set; } // inches
        public double BottomThickness { get; set; } // inches
    }
}
