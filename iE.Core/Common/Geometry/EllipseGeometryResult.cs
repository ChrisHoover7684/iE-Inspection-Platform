namespace iE.Core.Common.Geometry
{
    public class EllipseGeometryResult
    {
        public double MajorAxis { get; set; }
        public double MinorAxis { get; set; }
        public double SemiMajor { get; set; }
        public double SemiMinor { get; set; }
        public double Area { get; set; }
        public double PerimeterApprox { get; set; }
        public string Display { get; set; } = "";
    }
}