using System;
using System.Collections.Generic;

namespace iE.Core.Inspection.CorrosionRate
{
    public class CorrosionRateResult
    {
        public double ThicknessLossInches { get; set; }
        public double ExposureTimeYears { get; set; }
        public double CorrosionRateInchesPerYear { get; set; }
        public double CorrosionRateMpy { get; set; }
        public double CorrosionRateMmPerYear { get; set; }
        public double? RemainingLifeYears { get; set; }
        public double? NextInspectionYears { get; set; }
        public DateTime? NextInspectionDate { get; set; }
        public List<string> Warnings { get; set; } = new();
        public string Display { get; set; } = "";
    }
}
