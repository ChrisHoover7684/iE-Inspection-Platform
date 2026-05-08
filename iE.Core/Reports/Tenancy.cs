namespace iE.Core.Reports;

public static class UserFacilityRoles
{
    public const string Admin = "Admin";
    public const string Inspector = "Inspector";
    public const string Reviewer = "Reviewer";
    public const string Viewer = "Viewer";
}

public class ClientOrganization
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Facility> Facilities { get; set; } = new();
    public List<UserFacilityAccess> UserFacilityAccesses { get; set; } = new();
    public List<InspectionReport> InspectionReports { get; set; } = new();
}

public class Facility
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ClientOrganization? ClientOrganization { get; set; }
    public List<ProcessUnit> ProcessUnits { get; set; } = new();
    public List<Asset> Assets { get; set; } = new();
    public List<InspectionReport> InspectionReports { get; set; } = new();
}

public class ProcessUnit
{
    public string Id { get; set; } = string.Empty;
    public string FacilityId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UnitCode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Facility? Facility { get; set; }
    public List<Asset> Assets { get; set; } = new();
}

public class Asset
{
    public string Id { get; set; } = string.Empty;
    public string FacilityId { get; set; } = string.Empty;
    public string? ProcessUnitId { get; set; }
    public string EquipmentTag { get; set; } = string.Empty;
    public string EquipmentType { get; set; } = string.Empty;
    public string Service { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public Facility? Facility { get; set; }
    public ProcessUnit? ProcessUnit { get; set; }
}

public class UserFacilityAccess
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string? FacilityId { get; set; }
    public string Role { get; set; } = UserFacilityRoles.Inspector;
    public bool IsActive { get; set; } = true;
    public ClientOrganization? ClientOrganization { get; set; }
    public Facility? Facility { get; set; }
}

public static class ClientOrganizationUserStatuses
{
    public const string Active = "active";
    public const string Invited = "invited";
    public const string Disabled = "disabled";
    public const string Removed = "removed";
}

public class ClientOrganizationUser
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string? ExternalSubject { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public string? DisplayName { get; set; }
    public string Status { get; set; } = ClientOrganizationUserStatuses.Invited;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
    public DateTime? LastSeenAtUtc { get; set; }
    public ClientOrganization? ClientOrganization { get; set; }
}

public class ClientOrganizationUserSession
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string? ClientOrganizationUserId { get; set; }
    public string ExternalSubject { get; set; } = string.Empty;
    public string? SessionKeyHash { get; set; }
    public DateTime StartedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAtUtc { get; set; }
    public string? IpHash { get; set; }
    public string? UserAgentHash { get; set; }
    public string? DeviceId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

public class ClientOrganizationUserDevice
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string? ClientOrganizationUserId { get; set; }
    public string ExternalSubject { get; set; } = string.Empty;
    public string DeviceFingerprintHash { get; set; } = string.Empty;
    public DateTime FirstSeenAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenAtUtc { get; set; } = DateTime.UtcNow;
    public string? LastIpHash { get; set; }
    public string? LastUserAgentHash { get; set; }
    public bool IsTrusted { get; set; }
    public bool IsDisabled { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
