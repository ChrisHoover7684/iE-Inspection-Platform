namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001ThicknessInput
    {
        public double OriginalThickness { get; set; }
        public double MeasuredThickness { get; set; }
        public double? AffectedAreaSquareInches { get; set; }
        public int Category { get; set; } = 1;
        public bool IsDoubleWall { get; set; }
    }
}
