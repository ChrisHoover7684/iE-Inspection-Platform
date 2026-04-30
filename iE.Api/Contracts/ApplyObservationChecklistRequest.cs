using iE.Core.Reports;
using System.Text.Json.Serialization;

namespace iE.Api.Contracts;

public class ApplyObservationChecklistRequest
{
    public string TemplateId { get; set; } = string.Empty;
    [JsonPropertyName("checklistResponses")]
    public List<ObservationChecklistItemResponse> ChecklistResponses { get; set; } = new();

    [JsonPropertyName("responses")]
    public List<ObservationChecklistItemResponse> LegacyResponses
    {
        set => ChecklistResponses = value ?? new List<ObservationChecklistItemResponse>();
    }

    public InspectionReport? Report { get; set; }
}
