using System.Security.Claims;
using System.Text.Json;
using iE.Api;
using iE.Api.Auth;
using iE.Api.Contracts;
using iE.Api.Middleware;
using iE.Api.Tenancy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

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
    public async Task CapabilityClaimsTransformation_MapsInspectorCapabilities()
    {
        var principal = AuthenticatedPrincipal(("sub", "u1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "ie_inspector"));
        var transformer = new CapabilityClaimsTransformation(new AuthOptions());
        var transformed = await transformer.TransformAsync(principal);
        var capabilities = transformed.Claims.Where(c => c.Type == "capability").Select(c => c.Value).ToArray();
        Assert.Contains(AuthCapabilities.ReportsWrite, capabilities);
        Assert.Contains(AuthCapabilities.PhotosWrite, capabilities);
    }

    [Fact]
    public async Task CapabilityClaimsTransformation_UnknownRolesAddNothing()
    {
        var principal = AuthenticatedPrincipal(("sub", "u1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "unknown"));
        var transformer = new CapabilityClaimsTransformation(new AuthOptions());
        var transformed = await transformer.TransformAsync(principal);
        Assert.Empty(transformed.Claims.Where(c => c.Type == "capability"));
    }

    [Fact]
    public async Task CapabilityClaimsTransformation_DeduplicatesCapabilities()
    {
        var principal = AuthenticatedPrincipal(("sub", "u1"), ("org_id", Guid.NewGuid().ToString()), ("roles", "ie_owner"), ("roles", "ie_admin"), ("roles", "ie_owner,ie_admin"));
        var transformer = new CapabilityClaimsTransformation(new AuthOptions());
        var transformed = await transformer.TransformAsync(principal);
        var claims = transformed.Claims.Where(c => c.Type == "capability").Select(c => c.Value).ToArray();
        Assert.Equal(claims.Length, claims.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Fact]
    public async Task TenantContextMiddleware_MissingOrgId_ReturnsSafeForbiddenResponse()
    {
        var middleware = new TenantContextMiddleware(_ => Task.CompletedTask, NullLogger<TenantContextMiddleware>.Instance);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.User = AuthenticatedPrincipal(("sub", "u1"));
        context.TraceIdentifier = "trace-123";

        await middleware.InvokeAsync(context, new TenantContextAccessor(), new TenantContextBuilder(new AuthOptions()));

        Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
        context.Response.Body.Position = 0;
        var response = await JsonSerializer.DeserializeAsync<ApiErrorResponse>(context.Response.Body);
        Assert.NotNull(response);
        Assert.Equal("tenant_context_invalid", response.Code);
        Assert.Equal("trace-123", response.TraceId);
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
