using System.Security.Claims;
using iE.Api;
using iE.Api.Auth;
using iE.Api.Tenancy;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class AuthFoundationTests
{
    [Fact]
    public void ValidateWhenEnabled_MissingAuthority_Throws()
    {
        var options = new AuthOptions { Audience = "api://aud" };
        var ex = Assert.Throws<InvalidOperationException>(() => AuthOptions.ValidateWhenEnabled(true, options));
        Assert.Contains("Authority", ex.Message);
    }

    [Fact]
    public void ValidateWhenEnabled_MissingAudience_Throws()
    {
        var options = new AuthOptions { Authority = "https://issuer" };
        var ex = Assert.Throws<InvalidOperationException>(() => AuthOptions.ValidateWhenEnabled(true, options));
        Assert.Contains("Audience", ex.Message);
    }

    [Fact]
    public void ValidateWhenEnabled_WhenDisabled_DoesNotRequireAuthorityAudience()
    {
        AuthOptions.ValidateWhenEnabled(false, new AuthOptions());
    }

    [Fact]
    public void StartupConfiguration_AuthenticationDisabled_DefaultFalse()
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection().Build();
        Assert.False(StartupConfiguration.IsAuthenticationEnabled(config));
    }

    [Fact]
    public void ClaimsParser_RequiresSub()
    {
        var principal = AuthenticatedPrincipal(("org_id", Guid.NewGuid().ToString()));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build(principal, "trace"));
        Assert.Contains("sub", ex.Message);
    }

    [Fact]
    public void ClaimsParser_RequiresOrgId()
    {
        var principal = AuthenticatedPrincipal(("sub", "user-1"));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build(principal, "trace"));
        Assert.Contains("org_id", ex.Message);
    }

    [Fact]
    public void ClaimsParser_InvalidOrgIdFailsClearly()
    {
        var principal = AuthenticatedPrincipal(("sub", "user-1"), ("org_id", "not-a-guid"));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build(principal, "trace"));
        Assert.Contains("valid GUID", ex.Message);
    }

    [Fact]
    public void RolesMapToExpectedCapabilities()
    {
        var principal = AuthenticatedPrincipal(("sub", "user-1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "ie_reviewer"));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ctx = builder.Build(principal, "trace");
        Assert.Contains(AuthCapabilities.ReportsRead, ctx.Capabilities);
        Assert.Contains(AuthCapabilities.ReportsReview, ctx.Capabilities);
        Assert.DoesNotContain(AuthCapabilities.ReportsWrite, ctx.Capabilities);
    }

    [Fact]
    public void MultipleRoleClaims_CombineCapabilities()
    {
        var principal = AuthenticatedPrincipal(("sub", "user-1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "ie_readonly"), ("roles", "ie_inspector"));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ctx = builder.Build(principal, "trace");
        Assert.Contains(AuthCapabilities.ReportsSubmit, ctx.Capabilities);
        Assert.Contains(AuthCapabilities.ExportsRead, ctx.Capabilities);
    }

    [Fact]
    public void UnknownRoles_DoNotGrantCapabilities()
    {
        var principal = AuthenticatedPrincipal(("sub", "user-1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "unknown_role"));
        var builder = new TenantContextBuilder(new AuthOptions());
        var ctx = builder.Build(principal, "trace");
        Assert.Empty(ctx.Capabilities);
    }

    [Fact]
    public void TenantContextAccessor_StoresAndReturnsContext()
    {
        var accessor = new TenantContextAccessor();
        var context = new TenantContext { IsAuthenticated = true, ExternalSubject = "sub", ClientOrganizationId = Guid.NewGuid(), TraceId = "t" };
        accessor.Current = context;
        Assert.Same(context, accessor.Current);
    }

    private static ClaimsPrincipal AuthenticatedPrincipal(params (string Type, string Value)[] claims)
    {
        var identity = new ClaimsIdentity(claims.Select(c => new Claim(c.Type, c.Value)), "test-auth");
        return new ClaimsPrincipal(identity);
    }
}
