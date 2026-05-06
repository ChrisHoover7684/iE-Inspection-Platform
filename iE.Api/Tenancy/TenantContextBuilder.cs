using System.Security.Claims;
using iE.Api.Auth;

namespace iE.Api.Tenancy;

public interface ITenantContextBuilder
{
    TenantContext Build(ClaimsPrincipal principal, string traceId);
}

public sealed class TenantContextBuilder(AuthOptions authOptions) : ITenantContextBuilder
{
    private readonly AuthOptions _authOptions = authOptions;

    public TenantContext Build(ClaimsPrincipal principal, string traceId)
    {
        var identity = principal.Identity;
        if (identity?.IsAuthenticated != true)
        {
            return TenantContext.Unauthenticated(traceId);
        }

        var sub = principal.FindFirstValue("sub");
        if (string.IsNullOrWhiteSpace(sub))
        {
            throw new InvalidOperationException("Authenticated principal is missing required claim: sub.");
        }

        var orgIdValue = principal.FindFirstValue(_authOptions.RequiredTenantClaimName);
        if (string.IsNullOrWhiteSpace(orgIdValue))
        {
            throw new InvalidOperationException($"Authenticated principal is missing required claim: {_authOptions.RequiredTenantClaimName}.");
        }

        if (!Guid.TryParse(orgIdValue, out var clientOrganizationId))
        {
            throw new InvalidOperationException($"Claim {_authOptions.RequiredTenantClaimName} must be a valid GUID.");
        }

        var roles = principal.Claims
            .Where(c => string.Equals(c.Type, _authOptions.RequiredRolesClaimName, StringComparison.OrdinalIgnoreCase))
            .SelectMany(c => c.Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var capabilities = MapCapabilities(roles);

        return new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = sub,
            IdentityTenantId = principal.FindFirstValue("tid"),
            ClientOrganizationId = clientOrganizationId,
            Roles = roles,
            Capabilities = capabilities,
            TraceId = traceId
        };
    }

    private static string[] MapCapabilities(IEnumerable<string> roles)
    {
        var capabilities = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var role in roles)
        {
            switch (role.ToLowerInvariant())
            {
                case "ie_owner":
                case "ie_admin":
                    Add(capabilities, AuthCapabilities.ReportsRead, AuthCapabilities.ReportsWrite, AuthCapabilities.ReportsSubmit,
                        AuthCapabilities.ReportsReview, AuthCapabilities.PhotosRead, AuthCapabilities.PhotosWrite,
                        AuthCapabilities.ExportsRead, AuthCapabilities.AdminUsersManage);
                    break;
                case "ie_inspector":
                    Add(capabilities, AuthCapabilities.ReportsRead, AuthCapabilities.ReportsWrite, AuthCapabilities.ReportsSubmit,
                        AuthCapabilities.PhotosRead, AuthCapabilities.PhotosWrite, AuthCapabilities.ExportsRead);
                    break;
                case "ie_reviewer":
                    Add(capabilities, AuthCapabilities.ReportsRead, AuthCapabilities.ReportsReview, AuthCapabilities.PhotosRead,
                        AuthCapabilities.ExportsRead);
                    break;
                case "ie_readonly":
                    Add(capabilities, AuthCapabilities.ReportsRead, AuthCapabilities.PhotosRead, AuthCapabilities.ExportsRead);
                    break;
            }
        }

        return capabilities.ToArray();
    }

    private static void Add(HashSet<string> capabilities, params string[] values)
    {
        foreach (var value in values)
        {
            capabilities.Add(value);
        }
    }
}
