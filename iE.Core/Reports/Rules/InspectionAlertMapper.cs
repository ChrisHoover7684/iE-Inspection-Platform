using iE.Core.Reports.Domain;

namespace iE.Core.Reports.Rules;

public class InspectionAlertMapper
{
    public List<UiAlert> Map(InspectionRuleResult ruleResult)
    {
        ArgumentNullException.ThrowIfNull(ruleResult);

        return ruleResult.Tags
            .Select(tag => new UiAlert
            {
                Severity = MapSeverity(tag.Severity),
                Message = tag.Message,
                SourceFieldId = tag.SourceFieldId,
                SourceFindingId = tag.SourceFindingId,
                RequiresAction = tag.RequiresAction
            })
            .ToList();
    }

    private static UiAlertSeverity MapSeverity(InspectionRuleSeverity severity) => severity switch
    {
        InspectionRuleSeverity.Error => UiAlertSeverity.Error,
        InspectionRuleSeverity.Warning => UiAlertSeverity.Warning,
        _ => UiAlertSeverity.Info
    };
}
