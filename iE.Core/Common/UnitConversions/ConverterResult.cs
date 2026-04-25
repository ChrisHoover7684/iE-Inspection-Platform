namespace iE.Core.Common.UnitConversions
{
    public class ConverterResult
    {
        public string Category { get; set; } = "";
        public double InputValue { get; set; }
        public double ConvertedValue { get; set; }
        public string FromUnit { get; set; } = "";
        public string ToUnit { get; set; } = "";
        public string Display { get; set; } = "";
    }
}