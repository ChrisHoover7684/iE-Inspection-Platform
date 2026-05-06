namespace iE.Api.Tenancy;

public interface ITenantContext
{
    bool IsAuthenticated { get; }
    string? ExternalSubject { get; }
    string? IdentityTenantId { get; }
    Guid? ClientOrganizationId { get; }
    IReadOnlyCollection<string> Roles { get; }
    IReadOnlyCollection<string> Capabilities { get; }
    string TraceId { get; }
}

public sealed class TenantContext : ITenantContext
{
    public static TenantContext Unauthenticated(string traceId) => new()
    {
        IsAuthenticated = false,
        TraceId = traceId
    };

    public bool IsAuthenticated { get; init; }
    public string? ExternalSubject { get; init; }
    public string? IdentityTenantId { get; init; }
    public Guid? ClientOrganizationId { get; init; }
    public IReadOnlyCollection<string> Roles { get; init; } = Array.Empty<string>();
    public IReadOnlyCollection<string> Capabilities { get; init; } = Array.Empty<string>();
    public string TraceId { get; init; } = string.Empty;
}

public interface ITenantContextAccessor
{
    ITenantContext Current { get; set; }
}

public sealed class TenantContextAccessor : ITenantContextAccessor
{
    public ITenantContext Current { get; set; } = TenantContext.Unauthenticated(string.Empty);
}
