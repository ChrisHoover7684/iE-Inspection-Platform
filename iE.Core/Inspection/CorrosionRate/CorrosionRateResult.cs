namespace iE.Core.Inspection.CorrosionRate
{
    public class CorrosionRateResult
    {
        public double ThicknessLossInches { get; set; }
        public double ExposureTimeYears { get; set; }
        public double CorrosionRateInchesPerYear { get; set; }
        public double CorrosionRateMpy { get; set; }
        public double CorrosionRateMmPerYear { get; set; }
        public string Display { get; set; } = "";
    }
}