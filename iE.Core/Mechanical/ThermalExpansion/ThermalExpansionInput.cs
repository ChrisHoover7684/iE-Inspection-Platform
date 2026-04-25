namespace iE.Core.Mechanical.ThermalExpansion
{
    public class ThermalExpansionInput
    {
        public string Material { get; set; } = "Carbon Steel";
        public double LengthInches { get; set; }
        public double DeltaT { get; set; }
        public bool IsCelsiusDeltaT { get; set; }
    }
}