using System;
using System.Collections.Generic;
using System.Linq;

namespace iE.Core.Mechanical.PipeData
{
    public class PipeDataService
    {
        private readonly List<PipeRow> _rows = new()
        {
            new("0.125", 0.405, new() { ["40"] = 0.068, ["80"] = 0.095 }),
            new("0.25", 0.540, new() { ["40"] = 0.088, ["80"] = 0.119 }),
            new("0.375", 0.675, new() { ["40"] = 0.091, ["80"] = 0.126 }),
            new("0.5", 0.840, new() { ["10"] = 0.083, ["20"] = 0.109, ["40"] = 0.109, ["80"] = 0.147, ["160"] = 0.188, ["5S"] = 0.065, ["10S"] = 0.083, ["40S"] = 0.109, ["80S"] = 0.147, ["STD"] = 0.109, ["XS"] = 0.147, ["XXS"] = 0.294 }),
            new("0.75", 1.050, new() { ["10"] = 0.083, ["20"] = 0.113, ["40"] = 0.113, ["80"] = 0.154, ["160"] = 0.219, ["5S"] = 0.065, ["10S"] = 0.083, ["40S"] = 0.113, ["80S"] = 0.154, ["STD"] = 0.113, ["XS"] = 0.154, ["XXS"] = 0.308 }),
            new("1", 1.315, new() { ["10"] = 0.109, ["20"] = 0.133, ["40"] = 0.133, ["80"] = 0.179, ["160"] = 0.250, ["5S"] = 0.065, ["10S"] = 0.109, ["40S"] = 0.133, ["80S"] = 0.179, ["STD"] = 0.133, ["XS"] = 0.179, ["XXS"] = 0.358 }),
            new("1.25", 1.660, new() { ["40"] = 0.140, ["80"] = 0.191, ["160"] = 0.250 }),
            new("1.5", 1.900, new() { ["10"] = 0.109, ["20"] = 0.145, ["40"] = 0.145, ["80"] = 0.200, ["160"] = 0.281, ["5S"] = 0.065, ["10S"] = 0.109, ["40S"] = 0.145, ["80S"] = 0.200, ["STD"] = 0.145, ["XS"] = 0.200, ["XXS"] = 0.400 }),
            new("2", 2.375, new() { ["10"] = 0.109, ["20"] = 0.154, ["40"] = 0.154, ["80"] = 0.218, ["160"] = 0.344, ["5S"] = 0.065, ["10S"] = 0.109, ["40S"] = 0.154, ["80S"] = 0.218, ["STD"] = 0.154, ["XS"] = 0.218, ["XXS"] = 0.436 }),
            new("2.5", 2.875, new() { ["40"] = 0.203, ["80"] = 0.276, ["160"] = 0.375 }),
            new("3", 3.500, new() { ["10"] = 0.120, ["20"] = 0.188, ["40"] = 0.216, ["80"] = 0.300, ["160"] = 0.438, ["5S"] = 0.083, ["10S"] = 0.120, ["40S"] = 0.216, ["80S"] = 0.300, ["STD"] = 0.216, ["XS"] = 0.300, ["XXS"] = 0.600 }),
            new("3.5", 4.000, new() { ["40"] = 0.226, ["80"] = 0.318 }),
            new("4", 4.500, new() { ["10"] = 0.120, ["20"] = 0.237, ["40"] = 0.237, ["80"] = 0.337, ["160"] = 0.531, ["5S"] = 0.083, ["10S"] = 0.120, ["40S"] = 0.237, ["80S"] = 0.337, ["STD"] = 0.237, ["XS"] = 0.337, ["XXS"] = 0.674 }),
            new("5", 5.563, new() { ["10"] = 0.134, ["20"] = 0.258, ["40"] = 0.258, ["80"] = 0.375, ["160"] = 0.625, ["5S"] = 0.109, ["10S"] = 0.134, ["40S"] = 0.258, ["80S"] = 0.375, ["STD"] = 0.258, ["XS"] = 0.375, ["XXS"] = 0.750 }),
            new("6", 6.625, new() { ["10"] = 0.134, ["20"] = 0.280, ["40"] = 0.280, ["80"] = 0.432, ["160"] = 0.719, ["5S"] = 0.109, ["10S"] = 0.134, ["40S"] = 0.280, ["80S"] = 0.432, ["STD"] = 0.280, ["XS"] = 0.432, ["XXS"] = 0.864 }),
            new("8", 8.625, new() { ["10"] = 0.148, ["20"] = 0.277, ["40"] = 0.322, ["80"] = 0.500, ["160"] = 0.906, ["5S"] = 0.109, ["10S"] = 0.148, ["40S"] = 0.322, ["80S"] = 0.500, ["STD"] = 0.322, ["XS"] = 0.500, ["XXS"] = 0.875 }),
            new("10", 10.750, new() { ["10"] = 0.165, ["20"] = 0.307, ["40"] = 0.365, ["80"] = 0.594, ["160"] = 1.125, ["5S"] = 0.134, ["10S"] = 0.165, ["40S"] = 0.365, ["80S"] = 0.500, ["STD"] = 0.365, ["XS"] = 0.500, ["XXS"] = 1.000 }),
            new("12", 12.750, new() { ["10"] = 0.180, ["20"] = 0.330, ["40"] = 0.406, ["80"] = 0.688, ["160"] = 1.312, ["5S"] = 0.156, ["10S"] = 0.180, ["40S"] = 0.406, ["80S"] = 0.500, ["STD"] = 0.375, ["XS"] = 0.500, ["XXS"] = 1.000 }),
            new("14", 14.000, new() { ["40"] = 0.438, ["60"] = 0.594, ["80"] = 0.750, ["100"] = 0.938, ["120"] = 1.094, ["140"] = 1.250, ["160"] = 1.406 }),
            new("16", 16.000, new() { ["40"] = 0.500, ["60"] = 0.656, ["80"] = 0.844, ["100"] = 1.031, ["120"] = 1.219, ["140"] = 1.438, ["160"] = 1.594 }),
            new("18", 18.000, new() { ["40"] = 0.562, ["60"] = 0.750, ["80"] = 0.938, ["100"] = 1.156, ["120"] = 1.375, ["140"] = 1.562, ["160"] = 1.781 }),
            new("20", 20.000, new() { ["40"] = 0.594, ["60"] = 0.812, ["80"] = 1.031, ["100"] = 1.281, ["120"] = 1.500, ["140"] = 1.750, ["160"] = 1.969 }),
            new("22", 22.000, new() { ["60"] = 0.875, ["80"] = 1.125, ["100"] = 1.375, ["120"] = 1.625, ["140"] = 1.875, ["160"] = 2.125 }),
            new("24", 24.000, new() { ["40"] = 0.688, ["60"] = 0.969, ["80"] = 1.218, ["100"] = 1.531, ["120"] = 1.812, ["140"] = 2.062, ["160"] = 2.344 })
        };

        public PipeDataResult Lookup(PipeDataInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Nps))
                throw new Exception("NPS is required.");

            if (string.IsNullOrWhiteSpace(input.Schedule))
                throw new Exception("Schedule is required.");

            string nps = NormalizeNps(input.Nps);
            string schedule = NormalizeSchedule(input.Schedule);

            PipeRow? row = _rows.FirstOrDefault(r => r.Nps == nps);

            if (row == null)
                throw new Exception($"NPS '{input.Nps}' was not found.");

            if (!row.ThicknessBySchedule.TryGetValue(schedule, out double thickness))
                throw new Exception($"Schedule '{input.Schedule}' was not found for NPS {input.Nps}.");

            double id = row.OutsideDiameter - (2.0 * thickness);
            double lower = thickness * 0.875;
            double upper = thickness * 1.125;

            return new PipeDataResult
            {
                Nps = row.Nps,
                Schedule = schedule,
                OutsideDiameter = Math.Round(row.OutsideDiameter, 3),
                NominalThickness = Math.Round(thickness, 3),
                InsideDiameter = Math.Round(id, 3),
                LowerLimitMinus12_5 = Math.Round(lower, 3),
                UpperLimitPlus12_5 = Math.Round(upper, 3),
                Display =
                    $"NPS: {row.Nps}\n" +
                    $"Schedule: {schedule}\n" +
                    $"OD: {row.OutsideDiameter:0.###} in\n" +
                    $"Nominal Thickness: {thickness:0.###} in\n" +
                    $"ID: {id:0.###} in\n" +
                    $"Lower Limit (-12.5%): {lower:0.###} in\n" +
                    $"Upper Limit (+12.5%): {upper:0.###} in"
            };
        }

        public List<string> GetNpsList()
        {
            return _rows.Select(r => r.Nps).ToList();
        }

        public List<string> GetSchedulesForNps(string nps)
        {
            string normalized = NormalizeNps(nps);
            PipeRow? row = _rows.FirstOrDefault(r => r.Nps == normalized);

            if (row == null)
                throw new Exception($"NPS '{nps}' was not found.");

            return row.ThicknessBySchedule.Keys
                .OrderBy(x => x, StringComparer.Ordinal)
                .ToList();
        }

        private string NormalizeNps(string value)
        {
            return value.Trim()
                .Replace("\"", "")
                .Replace("NPS", "", StringComparison.OrdinalIgnoreCase)
                .Trim();
        }

        private string NormalizeSchedule(string value)
        {
            string normalized = value.Trim()
                .Replace("Sch", "", StringComparison.OrdinalIgnoreCase)
                .Replace("Schedule", "", StringComparison.OrdinalIgnoreCase)
                .Trim()
                .ToUpperInvariant();

            return normalized;
        }

        private class PipeRow
        {
            public string Nps { get; }
            public double OutsideDiameter { get; }
            public Dictionary<string, double> ThicknessBySchedule { get; }

            public PipeRow(string nps, double outsideDiameter, Dictionary<string, double> thicknessBySchedule)
            {
                Nps = nps;
                OutsideDiameter = outsideDiameter;
                ThicknessBySchedule = thicknessBySchedule;
            }
        }
    }
}
