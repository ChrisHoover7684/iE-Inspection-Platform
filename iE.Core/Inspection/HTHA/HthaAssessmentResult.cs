namespace iE.Core.Inspection.HTHA
{
    public class HthaAssessmentResult
    {
        public string Material { get; set; } = string.Empty;
        public double TemperatureF { get; set; }
        public double TemperatureC { get; set; }
        public double HydrogenPartialPressurePsia { get; set; }
        public double HydrogenPartialPressureMpa { get; set; }

        public double? MaxAllowableTemperatureF { get; set; }
        public double? TemperatureRatio { get; set; }

        public string RiskCategory { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public bool CurveDataAvailable { get; set; }

        public List<string> GeneralRecommendations { get; set; } = new();
        public List<string> MaterialSpecificRecommendations { get; set; } = new();

        public string Display { get; set; } = string.Empty;
    }
}
