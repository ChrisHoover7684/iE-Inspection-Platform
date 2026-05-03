namespace iE.Core.Calculators.Corrosion
{
    public class CorrosionRateInput
    {
        public double InitialThicknessInches { get; set; }
        public double FinalThicknessInches { get; set; }

        public bool UseDates { get; set; }
        public double? ExposureTimeYears { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }

        public double? TminRequiredThicknessInches { get; set; }
    }
}
