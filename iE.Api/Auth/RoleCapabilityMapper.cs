using System.Security.Claims;
namespace iE.Api.Auth;

public static class RoleCapabilityMapper
{
    public static IReadOnlyCollection<string> MapCapabilities(IEnumerable<string> roles)
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

    public static IReadOnlyCollection<string> ExtractRoles(ClaimsPrincipal principal, string rolesClaimName)
    {
        return principal.Claims
            .Where(c => string.Equals(c.Type, rolesClaimName, StringComparison.OrdinalIgnoreCase))
            .SelectMany(c => c.Value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static void Add(HashSet<string> capabilities, params string[] values)
    {
        foreach (var value in values)
        {
            capabilities.Add(value);
        }
    }
}
