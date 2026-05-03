using iE.Core.Calculators.Models;

namespace iE.Core.Calculators;

public class PipeLookupService
{
    private static readonly Dictionary<string, double> OutsideDiameters = new()
    {
        ["1/2\""] = 0.84,
        ["3/4\""] = 1.05,
        ["1\""] = 1.315,
        ["1.5\""] = 1.9,
        ["2\""] = 2.375,
        ["3\""] = 3.5,
        ["4\""] = 4.5,
        ["6\""] = 6.625,
        ["8\""] = 8.625,
        ["10\""] = 10.75,
        ["12\""] = 12.75
    };

    private static readonly Dictionary<string, Dictionary<string, double>> WallThickness = new()
    {
        ["1/2\""] = new() { ["10"] = 0.083, ["20"] = 0.090, ["40"] = 0.109, ["80"] = 0.147, ["160"] = 0.188 },
        ["3/4\""] = new() { ["10"] = 0.083, ["20"] = 0.091, ["40"] = 0.113, ["80"] = 0.154, ["160"] = 0.219 },
        ["1\""] = new() { ["10"] = 0.109, ["20"] = 0.133, ["40"] = 0.133, ["80"] = 0.179, ["160"] = 0.25 },
        ["1.5\""] = new() { ["10"] = 0.109, ["20"] = 0.145, ["40"] = 0.145, ["80"] = 0.2, ["160"] = 0.281 },
        ["2\""] = new() { ["10"] = 0.109, ["20"] = 0.154, ["40"] = 0.154, ["80"] = 0.218, ["160"] = 0.344 },
        ["3\""] = new() { ["10"] = 0.12, ["20"] = 0.188, ["40"] = 0.216, ["80"] = 0.3, ["160"] = 0.438 },
        ["4\""] = new() { ["10"] = 0.12, ["20"] = 0.237, ["40"] = 0.237, ["80"] = 0.337, ["160"] = 0.531 },
        ["6\""] = new() { ["10"] = 0.134, ["20"] = 0.25, ["40"] = 0.28, ["80"] = 0.432, ["160"] = 0.719 },
        ["8\""] = new() { ["10"] = 0.148, ["20"] = 0.277, ["40"] = 0.322, ["80"] = 0.5, ["160"] = 0.906 },
        ["10\""] = new() { ["10"] = 0.165, ["20"] = 0.307, ["40"] = 0.365, ["80"] = 0.5, ["160"] = 1.0 },
        ["12\""] = new() { ["10"] = 0.18, ["20"] = 0.33, ["40"] = 0.406, ["80"] = 0.687, ["160"] = 1.125 }
    };

    public IReadOnlyList<string> GetNpsOptions() => OutsideDiameters.Keys.ToList();
    public IReadOnlyList<string> GetScheduleOptions() => ["10", "20", "40", "80", "160"];

    public PipeLookupResult Lookup(string nps, string schedule)
    {
        if (!OutsideDiameters.TryGetValue(nps, out var od))
            throw new ArgumentException("Unsupported NPS value.");

        if (!WallThickness.TryGetValue(nps, out var scheduleMap) || !scheduleMap.TryGetValue(schedule, out var wall))
            throw new ArgumentException("Unsupported schedule for the selected NPS.");

        return new PipeLookupResult
        {
            Nps = nps,
            Schedule = schedule,
            OutsideDiameterInches = od,
            NominalWallThicknessInches = wall,
            InsideDiameterInches = Math.Round(od - (2 * wall), 3)
        };
    }
}
