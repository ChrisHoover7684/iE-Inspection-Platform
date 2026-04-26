namespace iE.Core.Equipment.Tanks.Models
{
    public class TankLocalizedCorrosionInput
    {
        // API 653 localized shell corrosion variables
        public double Diameter { get; set; }          // D, inches
        public double RemainingThickness { get; set; } // t2, inches
        public double MinimumThickness { get; set; }   // t_min, inches
        public double ActualMeasuredLength { get; set; } // inches
    }
}
