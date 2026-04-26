namespace iE.Core.Inspection.StiSp001
{
    public class StiSp001ContainmentResult
    {
        public bool Pass { get; set; }
        public double AvailableCapacityGallons { get; set; }
        public double RequiredCapacityGallons { get; set; }
        public string Result { get; set; } = string.Empty;
        public string ResultColor { get; set; } = "Black";
    }
}
