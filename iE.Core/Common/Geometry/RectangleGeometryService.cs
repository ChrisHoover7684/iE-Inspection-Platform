using System;

namespace iE.Core.Common.Geometry
{
    public class RectangleGeometryService
    {
        public RectangleGeometryResult Calculate(RectangleGeometryInput input)
        {
            if (input.Length <= 0 || input.Width <= 0)
                throw new Exception("Length and width must be greater than zero.");

            if (input.Height.HasValue && input.Height.Value <= 0)
                throw new Exception("Height must be greater than zero when provided.");

            double area = input.Length * input.Width;
            double perimeter = 2.0 * (input.Length + input.Width);
            double diagonal = Math.Sqrt(input.Length * input.Length + input.Width * input.Width);
            double? volume = input.Height.HasValue ? area * input.Height.Value : null;

            string display =
                $"Area: {area:0.######}\n" +
                $"Perimeter: {perimeter:0.######}\n" +
                $"Diagonal: {diagonal:0.######}";

            if (volume.HasValue)
                display += $"\nVolume: {volume.Value:0.######}";

            return new RectangleGeometryResult
            {
                Length = Math.Round(input.Length, 6),
                Width = Math.Round(input.Width, 6),
                Height = input.Height.HasValue ? Math.Round(input.Height.Value, 6) : null,
                Area = Math.Round(area, 6),
                Perimeter = Math.Round(perimeter, 6),
                Diagonal = Math.Round(diagonal, 6),
                Volume = volume.HasValue ? Math.Round(volume.Value, 6) : null,
                Display = display
            };
        }
    }
}