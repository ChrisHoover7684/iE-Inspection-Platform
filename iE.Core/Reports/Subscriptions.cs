namespace iE.Core.Reports;

public static class SubscriptionStatuses
{
    public const string Trialing = "trialing";
    public const string Active = "active";
    public const string PastDue = "past_due";
    public const string Suspended = "suspended";
    public const string Canceled = "canceled";
}

public class SubscriptionPlan
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PlanCode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? MetadataJson { get; set; }
    public List<ClientSubscription> ClientSubscriptions { get; set; } = new();
    public List<SubscriptionEntitlement> Entitlements { get; set; } = new();
}

public class ClientSubscription
{
    public string Id { get; set; } = string.Empty;
    public string ClientOrganizationId { get; set; } = string.Empty;
    public string PlanId { get; set; } = string.Empty;
    public string Status { get; set; } = SubscriptionStatuses.Trialing;
    public DateTime StartsAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? EndsAtUtc { get; set; }
    public DateTime? TrialEndsAtUtc { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public SubscriptionPlan? Plan { get; set; }
}

public class SubscriptionEntitlement
{
    public string Id { get; set; } = string.Empty;
    public string PlanId { get; set; } = string.Empty;
    public string EntitlementKey { get; set; } = string.Empty;
    public int? LimitValue { get; set; }
    public bool Enabled { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public SubscriptionPlan? Plan { get; set; }
}

public static class EntitlementKeys
{
    public const string ReportsCreate = "reports.create";
    public const string ReportsExport = "reports.export";
    public const string PhotosMarkup = "photos.markup";
    public const string AuditQuery = "audit.query";
    public const string MaxActiveReports = "max.activeReports";
    public const string MaxFacilities = "max.facilities";
    public const string MaxUsers = "max.users";
}
