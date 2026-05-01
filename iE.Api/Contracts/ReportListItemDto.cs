namespace iE.Api.Contracts;

public class ReportListItemDto
{
    public string Id { get; set; } = string.Empty;
    public string ReportNumber { get; set; } = string.Empty;
    public string EquipmentTag { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
