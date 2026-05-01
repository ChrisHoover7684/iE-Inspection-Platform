using iE.Core.Reports.Domain;
using iE.Core.Reports.Services;

namespace iE.Tests.Reports;

public class ReportNarrativeGeneratorTests
{
    private readonly ReportNarrativeGenerator _generator = new();

    [Fact]
    public void CleanExternalInspection_GeneratesNoFindingsSummary()
    {
        var report = NewReport();

        var result = _generator.Generate(report);

        Assert.Contains("No reportable findings", result.Sections.Summary, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("No reportable findings were recorded.", result.Sections.Findings);
    }

    [Fact]
    public void ReportWithRepairFinding_GeneratesRepairRecommendationText()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding
        {
            Id = "F-1",
            FindingType = FindingType.Corrosion,
            Location = "6 o'clock elbow",
            Description = "Localized wall loss observed",
            RepairRequired = true,
            RepairRecommendation = "Replace affected spool and re-inspect after replacement"
        });

        var result = _generator.Generate(report);

        Assert.Contains("requiring repair action", result.Sections.Summary, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Replace affected spool", result.Sections.Repairs, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Recommended action", result.Sections.Recommendations, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void MissingCriticalData_ReturnsWarnings()
    {
        var report = new InspectionReport();

        var result = _generator.Generate(report);

        Assert.NotEmpty(result.MissingDataWarnings);
        Assert.Contains(result.MissingDataWarnings, w => w.Contains("template", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(result.MissingDataWarnings, w => w.Contains("unit", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(result.MissingDataWarnings, w => w.Contains("service", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void ActiveLeak_IncludesRepairAndVerificationLanguage_WhenPresent()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding
        {
            Id = "F-LEAK-1",
            FindingType = FindingType.Leak,
            Location = "Flange at pump discharge",
            Description = "Active weeping leak observed",
            RepairRequired = true,
            RepairRecommendation = "Renew gasket and torque flange per spec",
            NdeMethod = "UT",
            NdeResult = "No additional thinning at leak interface"
        });

        var result = _generator.Generate(report);

        Assert.Contains("leak-related repairs", result.Sections.Recommendations, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("verification", result.Sections.ReturnToService, StringComparison.OrdinalIgnoreCase);
    }

    private static InspectionReport NewReport() => new()
    {
        TemplateId = "api-570-piping-external",
        EquipmentTag = "12" + "-P-101",
        Unit = "Crude Unit",
        Service = "Hydrocarbon"
    };
}
