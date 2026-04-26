namespace iE.Core.Equipment.Tanks.Models
{
    public class TankBottomInput
    {
        public double NominalBottomThickness { get; set; } // inches
        public double CorrosionAllowance { get; set; } // inches
        public double ActualBottomThickness { get; set; } // inches

        public bool HasAnnularRing { get; set; }
        public double AnnularRingNominalThickness { get; set; } // inches
        public double ActualAnnularRingThickness { get; set; } // inches
    }
}
