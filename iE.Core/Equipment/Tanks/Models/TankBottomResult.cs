namespace iE.Core.Equipment.Tanks.Models
{
    public class TankBottomResult
    {
        public double RequiredBottomThickness { get; set; }
        public double RequiredAnnularRingThickness { get; set; }

        public bool BottomPasses { get; set; }
        public bool AnnularRingPasses { get; set; }

        public string BottomStatus { get; set; } = string.Empty;
        public string AnnularRingStatus { get; set; } = string.Empty;
    }
}
