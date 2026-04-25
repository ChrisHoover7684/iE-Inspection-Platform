namespace iE.Core.Common.Geometry
{
    public class RectangleGeometryResult
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double? Height { get; set; }
        public double Area { get; set; }
        public double Perimeter { get; set; }
        public double Diagonal { get; set; }
        public double? Volume { get; set; }
        public string Display { get; set; } = "";
    }
}