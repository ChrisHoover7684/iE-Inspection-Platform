using System;

namespace iE.Core.Common.Geometry
{
    public class EllipseGeometryService
    {
        public EllipseGeometryResult Calculate(EllipseGeometryInput input)
        {
            if (input.MajorAxis <= 0 || input.MinorAxis <= 0)
                throw new Exception("Major and minor axis must be greater than zero.");

            double a = input.MajorAxis / 2.0;
            double b = input.MinorAxis / 2.0;

            double area = Math.PI * a * b;

            // Ramanujan approximation for ellipse perimeter
            double h = Math.Pow((a - b), 2) / Math.Pow((a + b), 2);
            double perimeter =
                Math.PI * (a + b) *
                (1 + (3 * h) / (10 + Math.Sqrt(4 - 3 * h)));

            return new EllipseGeometryResult
            {
                MajorAxis = Math.Round(input.MajorAxis, 6),
                MinorAxis = Math.Round(input.MinorAxis, 6),
                SemiMajor = Math.Round(a, 6),
                SemiMinor = Math.Round(b, 6),
                Area = Math.Round(area, 6),
                PerimeterApprox = Math.Round(perimeter, 6),
                Display =
                    $"Major Axis: {input.MajorAxis:0.######}\n" +
                    $"Minor Axis: {input.MinorAxis:0.######}\n" +
                    $"Area: {area:0.######}\n" +
                    $"Perimeter (approx): {perimeter:0.######}"
            };
        }
    }
}