namespace iE.Core.Inspection.CorrosionRate
{
    public class CorrosionRateInput
    {
        public double InitialThicknessInches { get; set; }
        public double FinalThicknessInches { get; set; }

        public bool UseDates { get; set; }

        public double? ExposureTimeYears { get; set; }

        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }

        public double InspectionFactor { get; set; } = 0.5;
        public double CurrentThicknessInches { get; set; }
        public double TminInches { get; set; }
    }
}
