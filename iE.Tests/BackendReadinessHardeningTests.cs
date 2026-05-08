using Microsoft.AspNetCore.Hosting;
using iE.Api;
using iE.Api.Auth;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace iE.Tests;

public class BackendReadinessHardeningTests
{
    [Fact]
    public void AuthenticationEnabled_RequiresAuthorityAndAudience_WhenEnabled()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Authentication:Enabled"] = "true"
        });

        var options = StartupConfiguration.GetAuthOptions(configuration);
        var ex = Assert.Throws<InvalidOperationException>(() => AuthOptions.ValidateWhenEnabled(true, options));

        Assert.Contains("Authentication:JwtBearer:Authority", ex.Message);
    }

    [Fact]
    public void AccountSharingAudit_DefaultsToDisabled_AuditOnly()
    {
        var configuration = BuildConfiguration();

        var options = AccountSharingAuditOptions.FromConfiguration(configuration);

        Assert.False(options.Enabled);
    }

    [Theory]
    [InlineData("not-a-bool")]
    [InlineData("1")]
    [InlineData("")]
    public void AccountSharingAudit_InvalidEnabledValue_FailsSafeToFalse(string value)
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["AccountSharingAudit:Enabled"] = value
        });

        var options = AccountSharingAuditOptions.FromConfiguration(configuration);

        Assert.False(options.Enabled);
    }

    [Theory]
    [InlineData("no")]
    [InlineData("123")]
    [InlineData("")]
    public void ApplyMigrationsOnStartup_InvalidValue_FailsSafeToFalse(string value)
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Database:ApplyMigrationsOnStartup"] = value
        });

        Assert.False(StartupConfiguration.ShouldApplyMigrationsOnStartup(configuration));
    }

    [Fact]
    public void SubscriptionEnforcementEnabled_DoesNotRequireBillingProviderConfig()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Subscriptions:EnforcementEnabled"] = "true"
        });

        Assert.True(StartupConfiguration.IsSubscriptionEnforcementEnabled(configuration));
    }

    [Fact]
    public void ReadinessSnapshot_RedactsSecrets_AndPublishesSafeFlags()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["ConnectionStrings:InspectionReports"] = "Host=localhost;Database=test;Username=u;Password=super-secret",
            ["Authentication:Enabled"] = "true",
            ["Subscriptions:EnforcementEnabled"] = "true",
            ["Database:ApplyMigrationsOnStartup"] = "true",
            ["AccountSharingAudit:Enabled"] = "true"
        });
        var db = BuildDb();
        var env = new FakeHostEnvironment("Staging");

        var snapshot = new BackendReadinessSnapshotService(configuration, env, db).GetSnapshot();

        Assert.True(snapshot.AuthenticationEnabled);
        Assert.True(snapshot.SubscriptionEnforcementEnabled);
        Assert.True(snapshot.AccountSharingAuditEnabled);
        Assert.True(snapshot.ApplyMigrationsOnStartup);
        Assert.True(snapshot.HasRequiredConnectionString);
        Assert.True(snapshot.ConnectionStringLooksSafe);
        Assert.Equal("Staging", snapshot.EnvironmentName);
        Assert.NotNull(snapshot.DatabaseProviderName);
        Assert.DoesNotContain("Password", snapshot.ToString(), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("super-secret", snapshot.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void HealthEndpoints_RemainPublic_AndUngatedInProgramDefinition()
    {
        var programText = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "iE.Api", "Program.cs"));

        Assert.Contains("app.MapHealthChecks(\"/health/live\"", programText);
        Assert.Contains("app.MapHealthChecks(\"/health/ready\"", programText);
        Assert.DoesNotContain("/health/live\").RequireAuthorization", programText);
        Assert.DoesNotContain("/health/ready\").RequireAuthorization", programText);
    }

    [Fact]
    public void Migrations_AreTimestampOrdered_AndIncludeTenantSubscriptionSessionDeviceUpdates()
    {
        var migrationFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "iE.Core", "Reports", "Migrations"), "*.cs")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(name => name is not null && char.IsDigit(name![0]) && !name.EndsWith(".Designer", StringComparison.OrdinalIgnoreCase))
            .Select(name => name!)
            .OrderBy(x => x, StringComparer.Ordinal)
            .ToList();

        for (var i = 1; i < migrationFiles.Count; i++)
        {
            Assert.True(string.CompareOrdinal(migrationFiles[i - 1], migrationFiles[i]) < 0);
        }
        Assert.Contains(migrationFiles, x => x.Contains("AddClientOrganizationUsers", StringComparison.Ordinal));
        Assert.Contains(migrationFiles, x => x.Contains("AddSubscriptionEntitlementFoundation", StringComparison.Ordinal));
        Assert.Contains(migrationFiles, x => x.Contains("AddUserSessionDeviceAuditFoundation", StringComparison.Ordinal));
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?>? values = null)
        => new ConfigurationBuilder().AddInMemoryCollection(values ?? new Dictionary<string, string?>()).Build();

    private static InspectionReportsDbContext BuildDb()
        => new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    private sealed class FakeHostEnvironment(string environmentName) : IHostEnvironment, IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = environmentName;
        public string ApplicationName { get; set; } = "iE.Api";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public Microsoft.Extensions.FileProviders.IFileProvider ContentRootFileProvider { get; set; } = null!;
        public string WebRootPath { get; set; } = Directory.GetCurrentDirectory();
        public Microsoft.Extensions.FileProviders.IFileProvider WebRootFileProvider { get; set; } = null!;
    }
}
