using iE.Api.Auth;
using iE.Core.Reports.Persistence;

namespace iE.Api;

public sealed record BackendReadinessSnapshot(
    bool AuthenticationEnabled,
    bool SubscriptionEnforcementEnabled,
    bool AccountSharingAuditEnabled,
    bool ApplyMigrationsOnStartup,
    bool HasRequiredConnectionString,
    bool ConnectionStringLooksSafe,
    string? DatabaseProviderName,
    string EnvironmentName);

public interface IBackendReadinessSnapshotService
{
    BackendReadinessSnapshot GetSnapshot();
}

public sealed class BackendReadinessSnapshotService(
    IConfiguration configuration,
    IWebHostEnvironment environment,
    InspectionReportsDbContext dbContext) : IBackendReadinessSnapshotService
{
    public BackendReadinessSnapshot GetSnapshot()
    {
        var connectionString = configuration.GetConnectionString(StartupConfiguration.InspectionReportsConnectionStringName);
        var hasRequiredConnectionString = !string.IsNullOrWhiteSpace(connectionString);

        return new BackendReadinessSnapshot(
            AuthenticationEnabled: StartupConfiguration.IsAuthenticationEnabled(configuration),
            SubscriptionEnforcementEnabled: StartupConfiguration.IsSubscriptionEnforcementEnabled(configuration),
            AccountSharingAuditEnabled: AccountSharingAuditOptions.FromConfiguration(configuration).Enabled,
            ApplyMigrationsOnStartup: StartupConfiguration.ShouldApplyMigrationsOnStartup(configuration),
            HasRequiredConnectionString: hasRequiredConnectionString,
            ConnectionStringLooksSafe: hasRequiredConnectionString && StartupConfiguration.ConnectionStringLooksSafe(connectionString!),
            DatabaseProviderName: dbContext.Database.ProviderName,
            EnvironmentName: environment.EnvironmentName);
    }
}
