using System;

namespace iE.Core.Common.Geometry
{
    public class SphereGeometryService
    {
        public SphereGeometryResult Calculate(SphereGeometryInput input)
        {
            if (string.IsNullOrWhiteSpace(input.SourceType))
                throw new Exception("SourceType is required.");

            if (input.Value <= 0)
                throw new Exception("Sphere input must be greater than zero.");

            double radius = input.SourceType switch
            {
                "Radius" => input.Value,
                "Volume" => Math.Pow((input.Value * 3.0) / (4.0 * Math.PI), 1.0 / 3.0),
                "Surface Area" => Math.Sqrt(input.Value / (4.0 * Math.PI)),
                _ => throw new Exception("Unknown sphere source type.")
            };

            double diameter = radius * 2.0;
            double volume = (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);
            double surfaceArea = 4.0 * Math.PI * radius * radius;

            return new SphereGeometryResult
            {
                Radius = Math.Round(radius, 6),
                Diameter = Math.Round(diameter, 6),
                Volume = Math.Round(volume, 6),
                SurfaceArea = Math.Round(surfaceArea, 6),
                Display =
                    $"Radius: {radius:0.######}\n" +
                    $"Diameter: {diameter:0.######}\n" +
                    $"Volume: {volume:0.######}\n" +
                    $"Surface Area: {surfaceArea:0.######}"
            };
        }
    }
}