using System;

namespace iE.Core.Common.Geometry
{
    public class RightTriangleGeometryService
    {
        public RightTriangleGeometryResult Calculate(RightTriangleGeometryInput input)
        {
            if (input.SideA <= 0 || input.SideB <= 0)
                throw new Exception("SideA and SideB must be greater than zero.");

            double hypotenuse = Math.Sqrt(input.SideA * input.SideA + input.SideB * input.SideB);
            double area = 0.5 * input.SideA * input.SideB;

            return new RightTriangleGeometryResult
            {
                SideA = Math.Round(input.SideA, 6),
                SideB = Math.Round(input.SideB, 6),
                Hypotenuse = Math.Round(hypotenuse, 6),
                Area = Math.Round(area, 6),
                Display =
                    $"Side A: {input.SideA:0.######}\n" +
                    $"Side B: {input.SideB:0.######}\n" +
                    $"Hypotenuse: {hypotenuse:0.######}\n" +
                    $"Area: {area:0.######}"
            };
        }
    }
}