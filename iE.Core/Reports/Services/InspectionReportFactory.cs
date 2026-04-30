namespace iE.Core.Reports.Services;

public class InspectionReportFactory
{
    public InspectionReport CreateFromTemplate(ReportTemplate template)
    {
        var report = new InspectionReport
        {
            Id = Guid.NewGuid().ToString("N"),
            TemplateId = template.Id,
            Status = InspectionReportStatuses.Draft,
            CreatedAt = DateTime.UtcNow,
            Sections = template.Sections
                .OrderBy(section => section.Order)
                .Select(section => new InspectionReportSection
                {
                    SectionId = section.Id,
                    SectionTitle = section.Title,
                    Order = section.Order,
                    IsRepeatable = section.IsRepeatable,
                    Answers = section.Fields.Select(MapTemplateFieldToAnswer).ToList()
                })
                .ToList()
        };

        HydrateTopLevelFields(report);

        return report;
    }

    public InspectionReport CreateFindingDraftsFromChecklistTransfers(InspectionReport report)
    {
        var existingChecklistItems = new HashSet<string>(
            report.Findings
                .Where(finding => !string.IsNullOrWhiteSpace(finding.AssociatedChecklistItem))
                .Select(finding => finding.AssociatedChecklistItem),
            StringComparer.OrdinalIgnoreCase);

        var checklistAnswersForTransfer = report.Sections
            .SelectMany(section => section.Answers)
            .Where(answer =>
                string.Equals(answer.DataType, "inspection-status", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(answer.Value, "Issue", StringComparison.OrdinalIgnoreCase) &&
                answer.TransferToComponentSection == true &&
                !string.IsNullOrWhiteSpace(answer.FieldId))
            .ToList();

        foreach (var answer in checklistAnswersForTransfer)
        {
            if (!existingChecklistItems.Add(answer.FieldId))
            {
                continue;
            }

            report.Findings.Add(new InspectionFinding
            {
                Id = Guid.NewGuid().ToString("N"),
                AssociatedChecklistItem = answer.FieldId,
                FindingType = ParseFindingType(answer.Label),
                Description = answer.Comment ?? string.Empty,
                RepairRequired = answer.RecommendationRequired == true,
                RepairRecommendation = string.Empty,
                Severity = FindingSeverity.None,
                Location = string.Empty,
                ComponentType = string.Empty,
                PhotoIds = new List<string>()
            });
        }

        return report;
    }

    private static InspectionReportAnswer MapTemplateFieldToAnswer(ReportTemplateField field)
    {
        var isChecklist = string.Equals(field.DataType, "inspection-status", StringComparison.OrdinalIgnoreCase) || field.IsChecklistItem;

        return new InspectionReportAnswer
        {
            FieldId = field.Id,
            Label = field.Label,
            DataType = field.DataType,
            Value = field.DefaultValue,
            Values = string.Equals(field.DataType, "multiselect", StringComparison.OrdinalIgnoreCase)
                ? new List<string>()
                : new List<string>(),
            Comment = isChecklist ? string.Empty : null,
            PhotoRequired = isChecklist ? false : null,
            TransferToComponentSection = isChecklist ? false : null,
            RecommendationRequired = isChecklist ? false : null
        };
    }

    private static FindingType ParseFindingType(string? value)
    {
        if (Enum.TryParse<FindingType>(value, true, out var parsed))
        {
            return parsed;
        }

        return FindingType.Other;
    }

    private static void HydrateTopLevelFields(InspectionReport report)
    {
        var answerByFieldId = report.Sections
            .SelectMany(section => section.Answers)
            .ToDictionary(answer => answer.FieldId, StringComparer.OrdinalIgnoreCase);

        report.Unit = answerByFieldId.GetValueOrDefault("unit")?.Value ?? string.Empty;
        report.SystemId = answerByFieldId.GetValueOrDefault("system-id")?.Value ?? string.Empty;
        report.CircuitId = answerByFieldId.GetValueOrDefault("circuit-id")?.Value ?? string.Empty;
        report.Service = answerByFieldId.GetValueOrDefault("service")?.Value ?? string.Empty;
    }
}
