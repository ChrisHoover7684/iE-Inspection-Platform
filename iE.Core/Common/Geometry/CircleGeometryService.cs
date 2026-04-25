using System;

namespace iE.Core.Common.Geometry
{
    public class CircleGeometryService
    {
        public CircleGeometryResult Calculate(CircleGeometryInput input)
        {
            Validate(input);

            double radius = input.SourceType switch
            {
                "Circumference" => input.Value / (2.0 * Math.PI),
                "Diameter" => input.Value / 2.0,
                "Radius" => input.Value,
                "Area" => Math.Sqrt(input.Value / Math.PI),
                _ => throw new Exception("Unknown circle source type.")
            };

            double diameter = radius * 2.0;
            double circumference = 2.0 * Math.PI * radius;
            double area = Math.PI * radius * radius;

            return new CircleGeometryResult
            {
                Radius = Math.Round(radius, 6),
                Diameter = Math.Round(diameter, 6),
                Circumference = Math.Round(circumference, 6),
                Area = Math.Round(area, 6),
                Display =
                    $"Radius: {radius:0.######}\n" +
                    $"Diameter: {diameter:0.######}\n" +
                    $"Circumference: {circumference:0.######}\n" +
                    $"Area: {area:0.######}"
            };
        }

        private void Validate(CircleGeometryInput input)
        {
            if (string.IsNullOrWhiteSpace(input.SourceType))
                throw new Exception("SourceType is required.");

            if (input.Value < 0)
                throw new Exception("Circle input cannot be negative.");

            if (input.Value == 0)
                throw new Exception("Circle input must be greater than zero.");
        }
    }
}