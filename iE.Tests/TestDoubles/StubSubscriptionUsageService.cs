using iE.Api.Auth;

namespace iE.Tests.TestDoubles;

public sealed class StubSubscriptionUsageService(SubscriptionUsageSnapshot? usage = null) : ISubscriptionUsageService
{
    public SubscriptionUsageSnapshot? Usage { get; set; } = usage ?? new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 0);

    public Task<SubscriptionUsageSnapshot?> GetUsageAsync(string? clientOrganizationId = null, bool trustedInternalRequest = false, CancellationToken cancellationToken = default)
        => Task.FromResult(Usage);
}
