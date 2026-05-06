using iE.Api.Auth;

namespace iE.Tests.TestDoubles;

public class NoopAuditEventWriter : IAuditEventWriter
{
    public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
