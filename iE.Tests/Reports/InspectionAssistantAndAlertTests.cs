using iE.Core.Reports;
using FindingType = iE.Core.Reports.FindingType;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Rules;
using iE.Core.Reports.Services;

namespace iE.Tests.Reports;

public class InspectionAssistantAndAlertTests
{
    private readonly InspectionTagRuleEngine _engine = new();
    private readonly InspectionAlertMapper _mapper = new();
    private readonly InspectionAssistantService _assistant = new();

    [Fact]
    public void ActiveLeak_GeneratesUiAlert()
    {
        var report = Api570ReportWithAnswer("active-leakage", "true");
        var alerts = _mapper.Map(_engine.Evaluate(report));

        Assert.Contains(alerts, a => a.Message.Contains("Active leakage", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void MissingRepairRecommendation_GeneratesUiAlert()
    {
        var report = new InspectionReport { TemplateId = "api-570-piping-inspection" };
        report.Findings.Add(new InspectionFinding { Id = "f1", RepairRequired = true, PhotoIds = ["p1"] });

        var alerts = _mapper.Map(_engine.Evaluate(report));

        Assert.Contains(alerts, a => a.Message.Contains("RepairRecommendation", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Assistant_SuggestsMissingData()
    {
        var report = new InspectionReport { TemplateId = "api-570-piping-inspection" };
        report.Findings.Add(new InspectionFinding { Id = "f1", Description = "", Location = "" });
        var rules = _engine.Evaluate(report);

        var suggestions = _assistant.GetSuggestions(report, rules);

        Assert.Contains(suggestions, s => s.Contains("location and description", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Assistant_SuggestsNde_ForCrackAndLeak()
    {
        var report = new InspectionReport { TemplateId = "api-570-piping-inspection" };
        report.Findings.Add(new InspectionFinding { Id = "f1", FindingType = FindingType.Cracking });
        report.Findings.Add(new InspectionFinding { Id = "f2", FindingType = FindingType.Leak });

        var rules = _engine.Evaluate(report);
        var suggestions = _assistant.GetSuggestions(report, rules);

        Assert.Contains(suggestions, s => s.Contains("NDE", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(suggestions, s => s.Contains("crack", StringComparison.OrdinalIgnoreCase)
            || s.Contains("leak", StringComparison.OrdinalIgnoreCase));

        var inlineResponse = _assistant.GetInlineSuggestions(new InlineAssistantRequest
        {
            CurrentFieldId = "finding-description",
            CurrentText = "Possible crack with active leak"
        });

        Assert.Contains(inlineResponse.Suggestions, s => s.PromptType == "NdeSuggestion");
        Assert.True(inlineResponse.Severity is "warning" or "critical");
    }

    [Fact]
    public void CleanReport_NoFalsePositiveSuggestions()
    {
        var report = new InspectionReport { TemplateId = "api-570-piping-inspection" };
        report.Findings.Add(new InspectionFinding
        {
            Id = "f1",
            FindingType = FindingType.Corrosion,
            Description = "External corrosion",
            Location = "Line 12",
            RepairRequired = false,
            RepairRecommendation = "Monitor",
            PhotoIds = ["p1"],
            NdeMethod = "UT",
            NdeResult = "No critical loss"
        });

        var suggestions = _assistant.GetSuggestions(report, _engine.Evaluate(report));

        Assert.Empty(suggestions);
    }

    [Fact]
    public void InlineAssistant_Pitting_ReturnsDepthAndLocalizedSuggestion()
    {
        var response = _assistant.GetInlineSuggestions(new InlineAssistantRequest
        {
            CurrentFieldId = "finding-description",
            CurrentText = "Observed pitting on the elbow"
        });

        Assert.Contains(response.Suggestions, s => s.PromptType == "MissingData" && s.Suggestion.Contains("pit depth", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(response.Suggestions, s => s.Suggestion.Contains("localized or general", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void InlineAssistant_Leak_ReturnsRepairPhotoLocationSuggestions()
    {
        var response = _assistant.GetInlineSuggestions(new InlineAssistantRequest
        {
            CurrentFieldId = "finding-description",
            CurrentText = "Small leak noted"
        });

        Assert.Contains(response.Suggestions, s => s.PromptType == "PhotoRequired");
        Assert.Contains(response.Suggestions, s => s.PromptType == "RepairSuggestion");
        Assert.Contains(response.Suggestions, s => s.Suggestion.Contains("location", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void InlineAssistant_Crack_ReturnsNdeSuggestion()
    {
        var response = _assistant.GetInlineSuggestions(new InlineAssistantRequest
        {
            CurrentFieldId = "finding-description",
            CurrentText = "Possible crack at weld toe"
        });

        Assert.Contains(response.Suggestions, s => s.PromptType == "NdeSuggestion" && s.Suggestion.Contains("PT/MT", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void InlineAssistant_CleanText_ReturnsNoRequiredActionAlert()
    {
        var response = _assistant.GetInlineSuggestions(new InlineAssistantRequest
        {
            CurrentFieldId = "finding-description",
            CurrentText = "No visible damage was observed at this time."
        });

        Assert.DoesNotContain(response.Suggestions, s => s.Severity.Equals("critical", StringComparison.OrdinalIgnoreCase));
        Assert.DoesNotContain(response.Suggestions, s => s.PromptType is "MissingData" or "PhotoRequired" or "RepairSuggestion" or "NdeSuggestion");
    }

    private static InspectionReport Api570ReportWithAnswer(string fieldId, string value)
    {
        var report = new InspectionReport { TemplateId = "api-570-piping-external" };
        report.Sections.Add(new InspectionReportSection
        {
            Answers = [new InspectionReportAnswer { FieldId = fieldId, Value = value }]
        });

        return report;
    }
}
