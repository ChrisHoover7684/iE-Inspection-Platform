using System;

namespace iE.Core.Common.Geometry
{
    public class ConeGeometryService
    {
        public ConeGeometryResult Calculate(ConeGeometryInput input)
        {
            if (input.Radius <= 0)
                throw new Exception("Radius must be greater than zero.");

            if (input.Height <= 0)
                throw new Exception("Height must be greater than zero.");

            double diameter = input.Radius * 2.0;
            double slantHeight = Math.Sqrt(input.Radius * input.Radius + input.Height * input.Height);
            double baseArea = Math.PI * input.Radius * input.Radius;
            double lateralSurfaceArea = Math.PI * input.Radius * slantHeight;
            double totalSurfaceArea = baseArea + lateralSurfaceArea;
            double volume = (Math.PI * input.Radius * input.Radius * input.Height) / 3.0;

            return new ConeGeometryResult
            {
                Radius = Math.Round(input.Radius, 6),
                Diameter = Math.Round(diameter, 6),
                Height = Math.Round(input.Height, 6),
                SlantHeight = Math.Round(slantHeight, 6),
                BaseArea = Math.Round(baseArea, 6),
                LateralSurfaceArea = Math.Round(lateralSurfaceArea, 6),
                TotalSurfaceArea = Math.Round(totalSurfaceArea, 6),
                Volume = Math.Round(volume, 6),
                Display =
                    $"Radius: {input.Radius:0.######}\n" +
                    $"Diameter: {diameter:0.######}\n" +
                    $"Height: {input.Height:0.######}\n" +
                    $"Slant Height: {slantHeight:0.######}\n" +
                    $"Base Area: {baseArea:0.######}\n" +
                    $"Lateral Surface Area: {lateralSurfaceArea:0.######}\n" +
                    $"Total Surface Area: {totalSurfaceArea:0.######}\n" +
                    $"Volume: {volume:0.######}"
            };
        }
    }
}