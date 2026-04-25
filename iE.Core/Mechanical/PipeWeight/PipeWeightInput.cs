namespace iE.Core.Mechanical.PipeWeight
{
    public class PipeWeightInput
    {
        public double OuterDiameter { get; set; }   // inches or mm
        public double WallThickness { get; set; }   // inches or mm
        public double Length { get; set; }          // ft or m
        public double Density { get; set; }         // lb/in³ or g/cm³
        public bool IsMetric { get; set; }          // true = metric
    }
}