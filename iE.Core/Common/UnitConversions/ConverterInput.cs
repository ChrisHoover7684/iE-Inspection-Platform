namespace iE.Core.Common.UnitConversions
{
    public class ConverterInput
    {
        public string Category { get; set; } = "";
        public double Value { get; set; }
        public string FromUnit { get; set; } = "";
        public string ToUnit { get; set; } = "";
    }
}