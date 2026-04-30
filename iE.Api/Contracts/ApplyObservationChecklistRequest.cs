using iE.Core.Reports;

namespace iE.Api.Contracts;

public class ApplyObservationChecklistRequest
{
    public string TemplateId { get; set; } = string.Empty;
    public List<ObservationChecklistItemResponse> Responses { get; set; } = new();
    public InspectionReport? Report { get; set; }
}
