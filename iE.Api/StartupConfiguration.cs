using iE.Api.Auth;

namespace iE.Api;

internal static class StartupConfiguration
{
    internal const string InspectionReportsConnectionStringName = "InspectionReports";

    internal static string GetRequiredConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(InspectionReportsConnectionStringName);

        if (!ConnectionStringLooksSafe(connectionString))
        {
            throw new InvalidOperationException(
                "Missing required configuration: ConnectionStrings:InspectionReports. " +
                "Set the ConnectionStrings__InspectionReports environment variable or configure " +
                "ConnectionStrings:InspectionReports via dotnet user-secrets for local development.");
        }

        return connectionString!;
    }

    internal static bool IsAuthenticationEnabled(IConfiguration configuration)
    {
        return configuration.GetValue<bool>("Authentication:Enabled");
    }

    internal static AuthOptions GetAuthOptions(IConfiguration configuration)
    {
        var options = new AuthOptions();
        configuration.GetSection("Authentication:JwtBearer").Bind(options);
        return options;
    }

    internal static bool ConnectionStringLooksSafe(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return false;
        }

        return !IsPlaceholderConnectionString(connectionString);
    }

    private static bool IsPlaceholderConnectionString(string connectionString)
    {
        var normalized = connectionString.Trim();

        return normalized.Equals("SET_IN_ENV_OR_USER_SECRETS", StringComparison.OrdinalIgnoreCase)
            || normalized.Equals("<set-in-user-secrets-or-env>", StringComparison.OrdinalIgnoreCase)
            || normalized.Equals("CHANGEME", StringComparison.OrdinalIgnoreCase)
            || normalized.Equals("TODO", StringComparison.OrdinalIgnoreCase);
    }

    internal static bool ShouldApplyMigrationsOnStartup(IConfiguration configuration)
    {
        var value = configuration["Database:ApplyMigrationsOnStartup"];
        return bool.TryParse(value, out var enabled) && enabled;
    }

    internal static bool IsSubscriptionEnforcementEnabled(IConfiguration configuration)
    {
        var value = configuration["Subscriptions:EnforcementEnabled"];
        return bool.TryParse(value, out var enabled) && enabled;
    }
}
