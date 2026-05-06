using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace iE.Api.Auth;

public sealed class CapabilityClaimsTransformation(AuthOptions authOptions) : IClaimsTransformation
{
    private readonly AuthOptions _authOptions = authOptions;

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return Task.FromResult(principal);
        }

        if (principal.Identity is not ClaimsIdentity identity)
        {
            return Task.FromResult(principal);
        }

        var roles = RoleCapabilityMapper.ExtractRoles(principal, _authOptions.RequiredRolesClaimName);
        var capabilities = RoleCapabilityMapper.MapCapabilities(roles);
        var existing = new HashSet<string>(principal.Claims
            .Where(c => c.Type == "capability")
            .Select(c => c.Value), StringComparer.OrdinalIgnoreCase);

        foreach (var capability in capabilities)
        {
            if (existing.Add(capability))
            {
                identity.AddClaim(new Claim("capability", capability));
            }
        }

        return Task.FromResult(principal);
    }
}
