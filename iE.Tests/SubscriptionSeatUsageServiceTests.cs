using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class SubscriptionSeatUsageServiceTests
{
    [Fact]
    public async Task Counts_Active_And_Invited_Seats_Only_For_CurrentTenant()
    {
        var db = Db();
        db.ClientOrganizationUsers.AddRange(
            User("u1", "11111111-1111-1111-1111-111111111111", ClientOrganizationUserStatuses.Active),
            User("u2", "11111111-1111-1111-1111-111111111111", ClientOrganizationUserStatuses.Invited),
            User("u3", "11111111-1111-1111-1111-111111111111", ClientOrganizationUserStatuses.Disabled),
            User("u4", "22222222-2222-2222-2222-222222222222", ClientOrganizationUserStatuses.Active));
        await db.SaveChangesAsync();

        var tenant = new TenantContextAccessor { Current = new TenantContext { ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111") } };
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = "true" }).Build();
        var svc = new SubscriptionSeatUsageService(cfg, tenant, db);

        var usage = await svc.GetUsageAsync();

        Assert.NotNull(usage);
        Assert.Equal(2, usage!.ConsumedSeatCount);
        Assert.Equal(1, usage.ActiveSeatCount);
        Assert.Equal(1, usage.InvitedSeatCount);
    }

    [Fact]
    public async Task AuthDisabled_Uses_ExplicitTenant()
    {
        var db = Db();
        db.ClientOrganizationUsers.Add(User("u1", "11111111-1111-1111-1111-111111111111", ClientOrganizationUserStatuses.Active));
        await db.SaveChangesAsync();
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = "false" }).Build();

        var svc = new SubscriptionSeatUsageService(cfg, new TenantContextAccessor(), db);
        var usage = await svc.GetUsageAsync("11111111-1111-1111-1111-111111111111");

        Assert.NotNull(usage);
        Assert.Equal(1, usage!.ConsumedSeatCount);
    }

    static InspectionReportsDbContext Db() => new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    static ClientOrganizationUser User(string id, string tenantId, string status) => new() { Id = id, ClientOrganizationId = tenantId, ExternalSubject = $"subject-{id}", Status = status, CreatedAtUtc = DateTime.UtcNow };
}
