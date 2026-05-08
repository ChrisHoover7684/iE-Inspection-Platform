namespace iE.Core.Reports.Persistence;

public class NdeRequestTypeDefinition
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string? ClientOrganizationId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsBuiltIn { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
