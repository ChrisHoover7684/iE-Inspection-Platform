namespace iE.Api.Contracts;

public class CreateReportInstanceRequest
{
    public string? ClientOrganizationId { get; set; }
    public string? FacilityId { get; set; }
    public string? ProcessUnitId { get; set; }
    public string? AssetId { get; set; }
}
