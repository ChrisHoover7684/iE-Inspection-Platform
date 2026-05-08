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


    internal static StressTestSubscriptionSeedOptions GetStressTestSeedOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("StressTestSeed");
        var enabled = bool.TryParse(section["Enabled"], out var parsedEnabled) && parsedEnabled;
        var clientOrganizationId = section["ClientOrganizationId"];
        var startsAtRaw = section["StartsAtUtc"];
        DateTime? startsAtUtc = null;
        if (!string.IsNullOrWhiteSpace(startsAtRaw) && DateTime.TryParse(startsAtRaw, out var parsedStart))
        {
            startsAtUtc = parsedStart.Kind == DateTimeKind.Utc ? parsedStart : DateTime.SpecifyKind(parsedStart, DateTimeKind.Utc);
        }

        var durationDays = int.TryParse(section["DurationDays"], out var parsedDays) ? parsedDays : StressTestPlanConstants.IntendedDurationDays;
        return new StressTestSubscriptionSeedOptions(enabled, clientOrganizationId, startsAtUtc, durationDays);
    }

    internal static bool IsSubscriptionEnforcementEnabled(IConfiguration configuration)
    {
        var value = configuration["Subscriptions:EnforcementEnabled"];
        return bool.TryParse(value, out var enabled) && enabled;
    }
}
