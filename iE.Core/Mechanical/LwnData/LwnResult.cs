namespace iE.Core.Mechanical.LwnData
{
    public class LwnResult
    {
        public string Size { get; set; } = "";
        public string Schedule { get; set; } = "";

        public double NominalThickness { get; set; }
        public double OutsideDiameter { get; set; }
        public double InsideDiameter { get; set; }

        public double MinThickness { get; set; }
        public double MaxThickness { get; set; }

        public string Display { get; set; } = "";
    }
}