using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Rules;

namespace iE.Tests.Reports;

public class InspectionTagRuleEngineTests
{
    private readonly InspectionTagRuleEngine _engine = new();

    [Fact]
    public void ActiveLeakage_CreatesActiveLeak_AndRequiresRepairAndPhoto()
    {
        var report = Api570ReportWithAnswer("api-570-piping-external", "active-leakage", "true");
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "active-leak");
        Assert.Contains(result.Tags, t => t.Id == "repair-recommendation-missing");
        Assert.Contains(result.Tags, t => t.Id == "photo-required");
    }

    [Fact]
    public void RepairRequiredWithoutRecommendation_ReturnsMissingRecommendation()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding { Id = "f1", RepairRequired = true, PhotoIds = ["p1"] });
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "repair-recommendation-missing" && t.SourceFindingId == "f1");
    }

    [Fact]
    public void RepairRequiredWithoutPhoto_ReturnsPhotoRequired()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding { Id = "f1", RepairRequired = true, RepairRecommendation = "fix" });
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "photo-required" && t.SourceFindingId == "f1");
    }

    [Fact]
    public void WetInsulation_CreatesCuiAndWetInsulationAndPhotoRequirement()
    {
        var report = Api570ReportWithAnswer("api-570-piping-cui-external", "wet-insulation-indicators", "true");
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "wet-insulation");
        Assert.Contains(result.Tags, t => t.Id == "cui-concern");
        Assert.Contains(result.Tags, t => t.Id == "photo-required");
    }

    [Fact]
    public void ThicknessBelowTmin_CreatesBelowTminAndRequiresRecommendation()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding { Id = "f1", ThicknessResult = 0.1, MinimumRequiredThickness = 0.2 });
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "below-tmin" && t.SourceFindingId == "f1");
        Assert.Contains(result.Tags, t => t.Id == "repair-recommendation-missing" && t.SourceFindingId == "f1");
    }

    [Fact]
    public void PitDepth_CreatesPittingTag_AndRequiresClassification()
    {
        var report = NewReport();
        report.Findings.Add(new InspectionFinding { Id = "f1", PitDepth = 0.05 });
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Id == "pitting" && t.SourceFindingId == "f1");
        Assert.Contains(result.Tags, t => t.Message.Contains("IsLocalized or IsGeneral"));
    }

    [Fact]
    public void RepairRecommendationTemplate_RequiresActionAndPriority()
    {
        var report = NewReport("repair-recommendation-api-570-piping");
        var result = _engine.Evaluate(report);
        Assert.Contains(result.Tags, t => t.Message.Contains("repair action", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(result.Tags, t => t.Message.Contains("priority", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(result.Tags, t => t.SourceFieldId == "repair-priority");
    }

    [Fact]
    public void CompleteReport_HasNoFalsePositives()
    {
        var report = Api570ReportWithAnswer("api-570-piping-external", "active-leakage", "false");
        report.Findings.Add(new InspectionFinding
        {
            Id = "f1",
            FindingType = FindingType.Corrosion,
            Description = "external corrosion",
            Location = "line 10",
            RepairRequired = false,
            PhotoIds = ["p1"],
            RepairRecommendation = "monitor"
        });

        var result = _engine.Evaluate(report);
        Assert.DoesNotContain(result.Tags, t => t.Id == "active-leak");
        Assert.DoesNotContain(result.Tags, t => t.Id == "repair-recommendation-missing");
    }

    private static InspectionReport Api570ReportWithAnswer(string templateId, string fieldId, string value)
    {
        var report = NewReport(templateId);
        report.Sections.Add(new InspectionReportSection
        {
            Id = "s1",
            Answers =
            [
                new InspectionReportAnswer { FieldId = fieldId, Value = value }
            ]
        });
        return report;
    }

    private static InspectionReport NewReport(string templateId = "api-570-piping-inspection") => new()
    {
        Id = "r1",
        TemplateId = templateId
    };
}
