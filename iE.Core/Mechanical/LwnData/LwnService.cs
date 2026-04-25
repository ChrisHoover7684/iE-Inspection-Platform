using System;
using System.Linq;

namespace iE.Core.Mechanical.LwnData
{
    public class LwnService
    {
        public LwnResult Calculate(LwnInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Size))
                throw new Exception("Size is required.");

            if (string.IsNullOrWhiteSpace(input.Schedule))
                throw new Exception("Schedule is required.");

            string size = input.Size.Trim();
            string schedule = input.Schedule.Trim();

            if (!LwnRepository.ThicknessData.TryGetValue((size, schedule), out double thickness))
                throw new Exception($"Schedule '{schedule}' was not found for size {size}.");

            if (!double.TryParse(size, out double insideDiameter))
                throw new Exception($"Invalid size '{size}'.");

            double outsideDiameter = insideDiameter + (2.0 * thickness);

            double minThickness;
            double maxThickness;

            if (thickness <= 0.709)
            {
                minThickness = thickness - 0.012;
                maxThickness = thickness + 0.060;
            }
            else
            {
                minThickness = thickness - 0.020;
                maxThickness = thickness + 0.060;
            }

            minThickness = Math.Max(minThickness, 0);

            return new LwnResult
            {
                Size = size,
                Schedule = schedule,
                NominalThickness = Math.Round(thickness, 3),
                OutsideDiameter = Math.Round(outsideDiameter, 3),
                InsideDiameter = Math.Round(insideDiameter, 3),
                MinThickness = Math.Round(minThickness, 3),
                MaxThickness = Math.Round(maxThickness, 3),
                Display =
                    $"Size: {size}\n" +
                    $"Schedule: {schedule}\n" +
                    $"Nominal Thickness: {thickness:0.###} in\n" +
                    $"Outside Diameter: {outsideDiameter:0.###} in\n" +
                    $"Inside Diameter: {insideDiameter:0.###} in\n" +
                    $"Min Thickness: {minThickness:0.###} in\n" +
                    $"Max Thickness: {maxThickness:0.###} in"
            };
        }

        public List<string> GetSizes()
        {
            return LwnRepository.ThicknessData.Keys
                .Select(k => k.size)
                .Distinct()
                .OrderBy(s => double.TryParse(s, out double n) ? n : double.MaxValue)
                .ToList();
        }

        public List<string> GetSchedulesForSize(string size)
        {
            string normalized = size.Trim();

            return LwnRepository.ThicknessData.Keys
                .Where(k => k.size == normalized)
                .Select(k => k.schedule)
                .Distinct()
                .ToList();
        }
    }
}