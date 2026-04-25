namespace iE.Core.Mechanical.ThermalExpansion
{
    public class ThermalExpansionResult
    {
        public string Material { get; set; } = "";
        public double CoefficientInPerInF { get; set; }
        public double DeltaTFahrenheit { get; set; }
        public double LengthInches { get; set; }
        public double ExpansionInches { get; set; }
        public string Display { get; set; } = "";
    }
}