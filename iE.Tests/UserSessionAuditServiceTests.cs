using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using iE.Tests.TestDoubles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace iE.Tests;

public class UserSessionAuditServiceTests
{
    [Fact]
    public async Task RecordRequestSeen_HashesSensitiveValues()
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        await using var db = new InspectionReportsDbContext(options);
        db.ClientOrganizationUsers.Add(new ClientOrganizationUser { Id = "u1", ClientOrganizationId = "t1", ExternalSubject = "sub1", Status = ClientOrganizationUserStatuses.Active });
        await db.SaveChangesAsync();

        var accessor = new TenantContextAccessor { Current = TenantContext.Unauthenticated("test") };
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = "false" }).Build();
        var service = new UserSessionAuditService(db, accessor, new NoopAuditEventWriter(), Options.Create(new AccountSharingAuditOptions { Enabled = true }), cfg);

        await service.RecordRequestSeenAsync("t1", "sub1", "session-token", "device-fingerprint", "1.2.3.4", "ua", default);

        var session = db.ClientOrganizationUserSessions.Single();
        Assert.DoesNotContain("1.2.3.4", session.IpHash!);
        Assert.DoesNotContain("session-token", session.SessionKeyHash!);
    }
}
