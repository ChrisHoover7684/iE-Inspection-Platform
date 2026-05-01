using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Rules;

public interface IInspectionTagRuleEngine
{
    InspectionRuleResult Evaluate(InspectionReport report);
}

public class InspectionTagRuleEngine : IInspectionTagRuleEngine
{
    private const string Api570PipingTemplateId = "api-570-piping-inspection";

    public InspectionRuleResult Evaluate(InspectionReport report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var result = new InspectionRuleResult();
        var answers = (report.Sections ?? new List<InspectionReportSection>())
            .SelectMany(x => x.Answers ?? new List<InspectionReportAnswer>())
            .ToList();

        var answerLookup = answers
            .Where(x => !string.IsNullOrWhiteSpace(x.FieldId))
            .GroupBy(x => x.FieldId.Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Last(), StringComparer.OrdinalIgnoreCase);

        bool isApi570 = string.Equals(report.TemplateId, Api570PipingTemplateId, StringComparison.OrdinalIgnoreCase);

        if (isApi570)
        {
            EvaluateApi570Checklist(report, result, answerLookup);
        }

        foreach (var finding in report.Findings ?? new List<InspectionFinding>())
        {
            EvaluateFindingRules(finding, result);
        }

        EvaluateRepairRecommendationTemplateRules(report, result, answerLookup);
        return result;
    }

    private static void EvaluateApi570Checklist(InspectionReport report, InspectionRuleResult result, Dictionary<string, InspectionReportAnswer> answerLookup)
    {
        var activeLeak = IsTrue(answerLookup, "active-leakage");
        if (activeLeak)
        {
            AddTag(result, "active-leak", "Active Leak", "api570", InspectionRuleSeverity.Error, "active-leakage", null, "Active leakage observed.", true);
            if (!(report.Findings ?? []).Any(f => !string.IsNullOrWhiteSpace(f.RepairRecommendation)))
            {
                AddTag(result, "repair-recommendation-missing", "Repair Recommendation Missing", "repair", InspectionRuleSeverity.Error, "active-leakage", null, "Active leakage requires a repair recommendation.", true);
            }

            if (!(report.Findings ?? []).Any(f => (f.PhotoIds ?? []).Any(x => !string.IsNullOrWhiteSpace(x))))
            {
                AddTag(result, "photo-required", "Photo Required", "photo", InspectionRuleSeverity.Warning, "active-leakage", null, "Active leakage requires at least one photo.", true);
            }
        }

        CheckFindingAnswerRule(report, result, answerLookup, "external-corrosion", "external-corrosion", "External Corrosion", "Finding requires finding description/location.");
        CheckFindingAnswerRule(report, result, answerLookup, "mechanical-damage", "mechanical-damage", "Mechanical Damage", "Finding requires finding description/location.");
        CheckFindingAnswerRule(report, result, answerLookup, "supports", "support-deficiency", "Support Deficiency", "Finding requires notes.");
        CheckFindingAnswerRule(report, result, answerLookup, "brackets-attachments", "support-deficiency", "Support Deficiency", "Finding requires notes.");
        CheckFindingAnswerRule(report, result, answerLookup, "gaskets", "gasket-deficiency", "Gasket Deficiency", "Finding requires notes.");
        CheckFindingAnswerRule(report, result, answerLookup, "bolting", "bolting-deficiency", "Bolting Deficiency", "Finding requires notes.");
        CheckFindingAnswerRule(report, result, answerLookup, "flanges", "flange-deficiency", "Flange Deficiency", "Finding requires notes.");

        if (IsFinding(answerLookup, "insulation-jacketing") && string.IsNullOrWhiteSpace(GetValue(answerLookup, "cui-concern")) && string.IsNullOrWhiteSpace(GetValue(answerLookup, "insulation-notes")))
        {
            AddTag(result, "cui-concern", "CUI Concern", "api570", InspectionRuleSeverity.Warning, "insulation-jacketing", null, "Insulation/jacketing finding requires CUI concern or insulation notes.", true);
        }

        if (IsTrue(answerLookup, "wet-insulation-indicators"))
        {
            AddTag(result, "wet-insulation", "Wet Insulation", "api570", InspectionRuleSeverity.Warning, "wet-insulation-indicators", null, "Wet insulation indicators observed.", true);
            if (string.IsNullOrWhiteSpace(GetValue(answerLookup, "cui-concern")))
            {
                AddTag(result, "cui-concern", "CUI Concern", "api570", InspectionRuleSeverity.Warning, "wet-insulation-indicators", null, "Wet insulation indicators require CUI concern documentation.", true);
            }

            if (!(report.Findings ?? []).Any(f => (f.PhotoIds ?? []).Any(x => !string.IsNullOrWhiteSpace(x))))
            {
                AddTag(result, "photo-required", "Photo Required", "photo", InspectionRuleSeverity.Warning, "wet-insulation-indicators", null, "Wet insulation indicators require photo evidence.", true);
            }
        }

        if (!string.IsNullOrWhiteSpace(GetValue(answerLookup, "temporary-repairs-observed"))
            && !(report.Findings ?? []).Any(f => !string.IsNullOrWhiteSpace(f.RepairRecommendation)))
        {
            AddTag(result, "temporary-repair", "Temporary Repair", "api570", InspectionRuleSeverity.Warning, "temporary-repairs-observed", null, "Temporary repairs observed requires repair recommendation.", true);
        }

        var approxFeet = ParseDouble(GetValue(answerLookup, "approximate-feet-findings")) ?? report.PipingProfile?.ApproximateFeetOfFindings;
        if (approxFeet.HasValue && approxFeet.Value > 0 && (report.Findings?.Count ?? 0) == 0)
        {
            AddTag(result, "repair-required", "Related Finding Required", "api570", InspectionRuleSeverity.Warning, "approximate-feet-findings", null, "Approximate feet of findings > 0 requires at least one related finding.", true);
        }
    }

    private static void EvaluateFindingRules(InspectionFinding finding, InspectionRuleResult result)
    {
        if (finding.RepairRequired)
        {
            AddTag(result, "repair-required", "Repair Required", "finding", InspectionRuleSeverity.Error, null, finding.Id, "Finding marked RepairRequired.", true);
            if (string.IsNullOrWhiteSpace(finding.RepairRecommendation))
                AddTag(result, "repair-recommendation-missing", "Repair Recommendation Missing", "finding", InspectionRuleSeverity.Error, null, finding.Id, "RepairRequired findings require RepairRecommendation.", true);
            if ((finding.PhotoIds ?? []).All(string.IsNullOrWhiteSpace))
                AddTag(result, "photo-required", "Photo Required", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "RepairRequired findings require at least one photo.", true);
        }

        if (finding.FindingType == FindingType.Leak && string.IsNullOrWhiteSpace(finding.NdeMethod) && string.IsNullOrWhiteSpace(finding.NdeResult))
            AddTag(result, "nde-required", "NDE Required", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "Leak findings require NDE method or result when available.", true);

        if (finding.PitDepth.HasValue)
        {
            AddTag(result, "pitting", "Pitting", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "Pit depth populated.", false);
            if (!finding.IsLocalized && !finding.IsGeneral)
                AddTag(result, "pitting", "Pitting Classification Missing", "finding", InspectionRuleSeverity.Error, null, finding.Id, "PitDepth requires IsLocalized or IsGeneral.", true);
        }

        if (finding.ThicknessResult.HasValue && finding.MinimumRequiredThickness.HasValue && finding.ThicknessResult.Value < finding.MinimumRequiredThickness.Value)
        {
            AddTag(result, "below-tmin", "Below Tmin", "finding", InspectionRuleSeverity.Error, null, finding.Id, "Thickness result is below minimum required thickness.", true);
            if (string.IsNullOrWhiteSpace(finding.RepairRecommendation))
                AddTag(result, "repair-recommendation-missing", "Repair Recommendation Missing", "finding", InspectionRuleSeverity.Error, null, finding.Id, "Below Tmin requires repair recommendation.", true);
        }

        if (finding.DamageMechanism == DamageMechanismType.CUI)
        {
            AddTag(result, "cui-concern", "CUI Concern", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "CUI-related finding detected.", true);
            if (string.IsNullOrWhiteSpace(finding.InsulationCondition))
                AddTag(result, "cui-concern", "Insulation Condition Missing", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "CUI-related finding requires insulation condition.", true);
            if ((finding.PhotoIds ?? []).All(string.IsNullOrWhiteSpace))
                AddTag(result, "photo-required", "Photo Required", "finding", InspectionRuleSeverity.Warning, null, finding.Id, "CUI-related finding requires at least one photo.", true);
        }
    }

    private static void EvaluateRepairRecommendationTemplateRules(InspectionReport report, InspectionRuleResult result, Dictionary<string, InspectionReportAnswer> answerLookup)
    {
        if (!report.TemplateId.Contains("repair-recommendation", StringComparison.OrdinalIgnoreCase)) return;

        if (string.IsNullOrWhiteSpace(GetValue(answerLookup, "recommended-repair-action")))
            AddTag(result, "repair-recommendation-missing", "Recommended Repair Action Missing", "repair", InspectionRuleSeverity.Error, "recommended-repair-action", null, "Repair recommendation template requires recommended repair action.", true);

        if (string.IsNullOrWhiteSpace(GetValue(answerLookup, "priority")))
            AddTag(result, "repair-required", "Priority Missing", "repair", InspectionRuleSeverity.Error, "priority", null, "Repair recommendation template requires priority.", true);

        if (IsTrue(answerLookup, "engineering-review-required"))
            AddTag(result, "engineering-review", "Engineering Review", "repair", InspectionRuleSeverity.Warning, "engineering-review-required", null, "Engineering review required.", true);

        if (IsTrue(answerLookup, "pressure-leak-test-required"))
            AddTag(result, "pressure-test-required", "Pressure Test Required", "repair", InspectionRuleSeverity.Warning, "pressure-leak-test-required", null, "Pressure/leak test required.", true);
    }

    private static void CheckFindingAnswerRule(InspectionReport report, InspectionRuleResult result, Dictionary<string, InspectionReportAnswer> answerLookup, string fieldId, string tagId, string tagName, string requirement)
    {
        if (!IsFinding(answerLookup, fieldId)) return;
        AddTag(result, tagId, tagName, "api570", InspectionRuleSeverity.Warning, fieldId, null, $"{tagName} identified.", true);
        var hasDescriptiveFinding = (report.Findings ?? []).Any(f => !string.IsNullOrWhiteSpace(f.Description) && !string.IsNullOrWhiteSpace(f.Location));
        if (!hasDescriptiveFinding)
            AddTag(result, "repair-required", "Finding Details Required", "api570", InspectionRuleSeverity.Warning, fieldId, null, requirement, true);
    }

    private static void AddTag(InspectionRuleResult result, string id, string name, string category, InspectionRuleSeverity severity, string? sourceFieldId, string? sourceFindingId, string message, bool requiresAction)
    {
        result.Tags.Add(new InspectionTag
        {
            Id = id,
            Name = name,
            Category = category,
            Severity = severity,
            SourceFieldId = sourceFieldId,
            SourceFindingId = sourceFindingId,
            Message = message,
            RequiresAction = requiresAction
        });

        result.Messages.Add(new InspectionRuleMessage
        {
            TagId = id,
            Severity = severity,
            Message = message,
            SourceFieldId = sourceFieldId,
            SourceFindingId = sourceFindingId
        });
    }

    private static bool IsFinding(Dictionary<string, InspectionReportAnswer> lookup, string fieldId) =>
        string.Equals(GetValue(lookup, fieldId), "Finding", StringComparison.OrdinalIgnoreCase);

    private static bool IsTrue(Dictionary<string, InspectionReportAnswer> lookup, string fieldId)
    {
        var val = GetValue(lookup, fieldId);
        return string.Equals(val, "true", StringComparison.OrdinalIgnoreCase)
               || string.Equals(val, "yes", StringComparison.OrdinalIgnoreCase)
               || string.Equals(val, "finding", StringComparison.OrdinalIgnoreCase);
    }

    private static string? GetValue(Dictionary<string, InspectionReportAnswer> lookup, string fieldId)
        => lookup.TryGetValue(fieldId, out var answer) ? answer.Value?.Trim() : null;

    private static double? ParseDouble(string? input) => double.TryParse(input, out var d) ? d : null;
}
