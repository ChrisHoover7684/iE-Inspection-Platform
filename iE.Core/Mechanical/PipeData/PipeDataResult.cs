namespace iE.Core.Mechanical.PipeData
{
    public class PipeDataResult
    {
        public string Nps { get; set; } = "";
        public string Schedule { get; set; } = "";
        public double OutsideDiameter { get; set; }
        public double NominalThickness { get; set; }
        public double InsideDiameter { get; set; }
        public double LowerLimitMinus12_5 { get; set; }
        public double UpperLimitPlus12_5 { get; set; }
        public string Display { get; set; } = "";
    }
}