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

        var roles = RoleCapabilityMapper.ExtractRoles(principal, _authOptions.RequiredRolesClaimName);
        var capabilities = RoleCapabilityMapper.MapCapabilities(roles);

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

}
