using iE.Api;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class StartupConfigurationTests
{
    [Fact]
    public void GetRequiredConnectionString_WhenMissing_ThrowsInvalidOperationException()
    {
        var configuration = BuildConfiguration();

        var exception = Assert.Throws<InvalidOperationException>(() => StartupConfiguration.GetRequiredConnectionString(configuration));

        Assert.Contains("ConnectionStrings:InspectionReports", exception.Message);
    }

    [Fact]
    public void GetRequiredConnectionString_WhenBlank_ThrowsInvalidOperationException()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["ConnectionStrings:InspectionReports"] = "   "
        });

        Assert.Throws<InvalidOperationException>(() => StartupConfiguration.GetRequiredConnectionString(configuration));
    }


    [Theory]
    [InlineData("SET_IN_ENV_OR_USER_SECRETS")]
    [InlineData("<set-in-user-secrets-or-env>")]
    [InlineData("CHANGEME")]
    [InlineData("TODO")]
    public void GetRequiredConnectionString_WhenPlaceholder_ThrowsInvalidOperationException(string placeholderValue)
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["ConnectionStrings:InspectionReports"] = placeholderValue
        });

        Assert.Throws<InvalidOperationException>(() => StartupConfiguration.GetRequiredConnectionString(configuration));
    }

    [Fact]
    public void GetRequiredConnectionString_WhenValid_ReturnsValue()
    {
        const string connectionString = "Host=localhost;Port=5432;Database=inspection_reports_dev;Username=postgres;Password=test";
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["ConnectionStrings:InspectionReports"] = connectionString
        });

        var resolved = StartupConfiguration.GetRequiredConnectionString(configuration);

        Assert.Equal(connectionString, resolved);
    }

    [Fact]
    public void ShouldApplyMigrationsOnStartup_DefaultsToFalse()
    {
        var configuration = BuildConfiguration();

        var shouldApply = StartupConfiguration.ShouldApplyMigrationsOnStartup(configuration);

        Assert.False(shouldApply);
    }

    [Fact]
    public void ShouldApplyMigrationsOnStartup_WhenTrue_ReturnsTrue()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["Database:ApplyMigrationsOnStartup"] = "true"
        });

        var shouldApply = StartupConfiguration.ShouldApplyMigrationsOnStartup(configuration);

        Assert.True(shouldApply);
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?>? values = null)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(values ?? new Dictionary<string, string?>())
            .Build();
    }
}
