using iE.Api.Auth;

namespace iE.Tests.TestDoubles;

public sealed class StubEntitlementService(EntitlementCheckResult? result = null) : IEntitlementService
{
    public EntitlementCheckResult Result { get; set; } = result ?? EntitlementCheckResult.AllowedWithLimit(null);

    public Task<EntitlementCheckResult> CheckAsync(string entitlementKey, string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
        => Task.FromResult(Result);
}
