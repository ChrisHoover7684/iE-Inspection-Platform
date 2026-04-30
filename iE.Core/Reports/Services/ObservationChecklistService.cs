namespace iE.Core.Reports.Services;

public class ObservationChecklistService
{
    private const string Api570PipingTemplateId = "api-570-piping-inspection";

    private static readonly List<ObservationChecklistItem> Api570ChecklistItems = new()
    {
        CreateItem("external-condition", "External Condition", "External condition acceptable"),
        CreateItem("active-leakage", "Leakage", "No active leakage observed"),
        CreateItem("supports", "Supports", "Supports visually acceptable"),
        CreateItem("brackets-attachments", "Brackets", "Brackets/attachments acceptable"),
        CreateItem("gaskets", "Gaskets", "Gaskets acceptable"),
        CreateItem("bolting", "Bolting", "Bolting acceptable"),
        CreateItem("flanges", "Flanges", "Flanges acceptable"),
        CreateItem("coating", "Coating", "Coating acceptable"),
        CreateItem("insulation-jacketing", "Insulation", "Insulation/jacketing acceptable"),
        CreateItem("mechanical-damage", "Mechanical Damage", "No mechanical damage observed"),
        CreateItem("external-corrosion", "Corrosion", "No reportable external corrosion observed")
    };

    public ObservationChecklistTemplate GetChecklistForTemplate(string templateId)
    {
        if (!string.Equals(templateId?.Trim(), Api570PipingTemplateId, StringComparison.OrdinalIgnoreCase))
        {
            return new ObservationChecklistTemplate
            {
                TemplateId = templateId,
                Items = new List<ObservationChecklistItem>()
            };
        }

        return new ObservationChecklistTemplate
        {
            TemplateId = Api570PipingTemplateId,
            Items = Api570ChecklistItems.Select(CloneItem).ToList()
        };
    }

    public ObservationChecklistBuildResult BuildObservationsAndFindingsFromChecklist(
        string templateId,
        IReadOnlyList<ObservationChecklistItemResponse> checklistResponses,
        InspectionReport? report = null)
    {
        var checklistTemplate = GetChecklistForTemplate(templateId);
        var itemMap = checklistTemplate.Items.ToDictionary(x => x.Id, StringComparer.OrdinalIgnoreCase);

        var result = new ObservationChecklistBuildResult();

        foreach (var response in checklistResponses ?? Array.Empty<ObservationChecklistItemResponse>())
        {
            if (string.IsNullOrWhiteSpace(response.ItemId) || !itemMap.TryGetValue(response.ItemId, out var item))
            {
                continue;
            }

            switch (response.Status)
            {
                case ObservationStatus.Acceptable:
                    result.Observations.Add(CreateObservation(item, response, ObservationStatus.Acceptable));
                    break;
                case ObservationStatus.Finding:
                    result.Findings.Add(CreateFinding(item, response, report));
                    break;
                case ObservationStatus.NotInspected:
                    result.Observations.Add(CreateObservation(item, response, ObservationStatus.NotInspected));
                    break;
                case ObservationStatus.NotApplicable:
                    result.Observations.Add(CreateObservation(item, response, ObservationStatus.NotApplicable));
                    break;
                default:
                    result.Observations.Add(CreateObservation(item, response, ObservationStatus.Acceptable));
                    break;
            }
        }

        return result;
    }

    private static InspectionObservation CreateObservation(
        ObservationChecklistItem item,
        ObservationChecklistItemResponse response,
        ObservationStatus status)
    {
        return new InspectionObservation
        {
            Id = Guid.NewGuid().ToString("N"),
            Category = item.Category,
            Status = status,
            Notes = string.IsNullOrWhiteSpace(response.Notes) ? item.Label : response.Notes,
            PhotoIds = (response.PhotoIds ?? new List<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList()
        };
    }

    private static InspectionFinding CreateFinding(ObservationChecklistItem item, ObservationChecklistItemResponse response, InspectionReport? report)
    {
        var provided = response.Finding;
        var resolvedLineNumber = ResolveLineNumber(response, report);

        if (provided is null)
        {
            return CreateMinimalFinding(item, response, resolvedLineNumber);
        }

        if (string.IsNullOrWhiteSpace(provided.Id))
        {
            provided.Id = Guid.NewGuid().ToString("N");
        }

        provided.AssociatedChecklistItem = item.Id;
        provided.PhotoIds = response.PhotoIds?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>();

        if (string.IsNullOrWhiteSpace(provided.Description))
        {
            provided.Description = response.Notes ?? item.Label;
        }

        if (provided.FindingType == FindingType.None)
        {
            provided.FindingType = FindingType.Other;
        }

        if (provided.Severity == FindingSeverity.None)
        {
            provided.Severity = FindingSeverity.Medium;
        }

        if (string.IsNullOrWhiteSpace(provided.LineNumber))
        {
            provided.LineNumber = resolvedLineNumber;
        }

        if (!provided.ApproximateFeetOfFindings.HasValue && response.ApproximateFeetOfFindings.HasValue)
        {
            provided.ApproximateFeetOfFindings = response.ApproximateFeetOfFindings.Value;
        }

        return provided;
    }

    private static InspectionFinding CreateMinimalFinding(
        ObservationChecklistItem item,
        ObservationChecklistItemResponse response,
        string? lineNumber)
    {
        return new InspectionFinding
        {
            Id = Guid.NewGuid().ToString("N"),
            AssociatedChecklistItem = item.Id,
            Description = string.IsNullOrWhiteSpace(response.Notes) ? item.Label : response.Notes,
            FindingType = FindingType.Other,
            Severity = FindingSeverity.Medium,
            DamageMechanism = DamageMechanismType.None,
            Location = item.Category,
            LineNumber = lineNumber,
            ApproximateFeetOfFindings = response.ApproximateFeetOfFindings,
            RepairRequired = false,
            PhotoIds = response.PhotoIds?.Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>()
        };
    }

    private static string? ResolveLineNumber(ObservationChecklistItemResponse response, InspectionReport? report)
    {
        if (!string.IsNullOrWhiteSpace(response.LineNumber))
        {
            return response.LineNumber.Trim();
        }

        var validLineNumbers = report?.PipingProfile?.LineNumbers
            ?.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList()
            ?? new List<string>();

        return validLineNumbers.Count == 1 ? validLineNumbers[0] : null;
    }

    private static ObservationChecklistItem CreateItem(string id, string category, string label)
    {
        return new ObservationChecklistItem
        {
            Id = id,
            Category = category,
            Label = label,
            DefaultStatus = ObservationStatus.Acceptable,
            RequiresPhoto = false,
            AppliesToTemplateIds = new List<string> { Api570PipingTemplateId }
        };
    }

    private static ObservationChecklistItem CloneItem(ObservationChecklistItem source)
    {
        return new ObservationChecklistItem
        {
            Id = source.Id,
            Category = source.Category,
            Label = source.Label,
            DefaultStatus = source.DefaultStatus,
            RequiresPhoto = source.RequiresPhoto,
            AppliesToTemplateIds = source.AppliesToTemplateIds.ToList()
        };
    }
}
