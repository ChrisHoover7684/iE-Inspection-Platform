namespace iE.Api.Auth;

public sealed class AuthOptions
{
    public string? Authority { get; init; }

    public string? Audience { get; init; }

    public bool RequireHttpsMetadata { get; init; } = true;

    public string RequiredTenantClaimName { get; init; } = "org_id";

    public string RequiredRolesClaimName { get; init; } = "roles";

    public static void ValidateWhenEnabled(bool authenticationEnabled, AuthOptions options)
    {
        if (!authenticationEnabled)
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.Authority))
        {
            throw new InvalidOperationException("Authentication is enabled but Authentication:JwtBearer:Authority is missing.");
        }

        if (string.IsNullOrWhiteSpace(options.Audience))
        {
            throw new InvalidOperationException("Authentication is enabled but Authentication:JwtBearer:Audience is missing.");
        }
    }
}
