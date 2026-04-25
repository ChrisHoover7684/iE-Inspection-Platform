namespace iE.Core.Common.Geometry
{
    public class CylinderGeometryResult
    {
        public double Radius { get; set; }
        public double Diameter { get; set; }
        public double Height { get; set; }
        public double BaseArea { get; set; }
        public double LateralSurfaceArea { get; set; }
        public double TotalSurfaceArea { get; set; }
        public double Volume { get; set; }
        public string Display { get; set; } = "";
    }
}