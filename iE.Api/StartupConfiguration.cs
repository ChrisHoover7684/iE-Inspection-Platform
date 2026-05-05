namespace iE.Api;

internal static class StartupConfiguration
{
    internal const string InspectionReportsConnectionStringName = "InspectionReports";

    internal static string GetRequiredConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(InspectionReportsConnectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Missing required configuration: ConnectionStrings:InspectionReports. " +
                "Set the ConnectionStrings__InspectionReports environment variable or configure " +
                "ConnectionStrings:InspectionReports via dotnet user-secrets for local development.");
        }

        return connectionString;
    }

    internal static bool ShouldApplyMigrationsOnStartup(IConfiguration configuration)
    {
        return configuration.GetValue<bool>("Database:ApplyMigrationsOnStartup");
    }
}
