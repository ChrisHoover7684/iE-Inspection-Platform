using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using iE.Tests.TestDoubles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class TenantMemberServiceTests
{
    [Fact] public async Task Invite_Creates_Invited_And_Normalizes_Email() { var svc = Build(false, false, out var db, out _); var r = await svc.InviteMemberAsync("  User@Example.com  ", "t1"); Assert.True(r.Allowed); var row = Assert.Single(db.ClientOrganizationUsers); Assert.Equal("invited", row.Status); Assert.Equal("user@example.com", row.NormalizedEmail); }
    [Fact] public async Task Invite_EnforcementEnabled_EqualLimit_Denies() { var svc = Build(true, true, out var db, out var audit, limit:1); db.ClientOrganizationUsers.Add(User("u1","t1",ClientOrganizationUserStatuses.Active)); await db.SaveChangesAsync(); var r = await svc.InviteMemberAsync("x@y.com","t1"); Assert.False(r.Allowed); Assert.Equal("seat_limit_exceeded", r.ReasonCode); Assert.Contains(audit.Events, e=>e.Action==AuditActions.TenantMemberSeatLimitDenied); }
    [Fact] public async Task Invite_EnforcementDisabled_Allows_OverLimit() { var svc = Build(false, true, out var db, out _, limit:1); db.ClientOrganizationUsers.AddRange(User("u1","t1",ClientOrganizationUserStatuses.Active),User("u2","t1",ClientOrganizationUserStatuses.Invited)); await db.SaveChangesAsync(); var r = await svc.InviteMemberAsync("x@y.com","t1"); Assert.True(r.Allowed); }
    [Fact] public async Task Invite_MissingEntitlement_Denies_WhenEnabled() { var svc = Build(true, true, out _, out _, includeEntitlement:false); var r = await svc.InviteMemberAsync("x@y.com","t1"); Assert.False(r.Allowed); Assert.Equal("entitlement_unavailable", r.ReasonCode); }
    [Fact] public async Task Invite_Duplicate_Email_Denied_For_Invited_Or_Active() { var svc = Build(false, false, out var db, out _); db.ClientOrganizationUsers.Add(User("u1","t1",ClientOrganizationUserStatuses.Invited,email:"a@b.com")); await db.SaveChangesAsync(); var r = await svc.InviteMemberAsync("a@b.com","t1"); Assert.False(r.Allowed); Assert.Equal("duplicate_member", r.ReasonCode); }
    [Fact] public async Task AuthEnabled_MissingTenant_Denies() { var svc = Build(false, true, out _, out _, setTenant:false); var r = await svc.InviteMemberAsync("x@y.com"); Assert.False(r.Allowed); Assert.Equal("tenant_context_missing", r.ReasonCode); }
    [Fact] public async Task Activate_Disable_Remove_Flow() { var svc = Build(false, false, out var db, out var audit); var invite = await svc.InviteMemberAsync("u@e.com","t1"); var act = await svc.ActivateMemberAsync(invite.MemberId!,"sub-1","t1"); Assert.True(act.Allowed); var dis = await svc.DisableMemberAsync(invite.MemberId!,"t1"); Assert.True(dis.Allowed); var rem = await svc.RemoveMemberAsync(invite.MemberId!,"t1"); Assert.True(rem.Allowed); var row = await db.ClientOrganizationUsers.FirstAsync(); Assert.Equal(ClientOrganizationUserStatuses.Removed,row.Status); Assert.Contains(audit.Events,e=>e.Action==AuditActions.TenantMemberActivated); Assert.Contains(audit.Events,e=>e.Action==AuditActions.TenantMemberDisabled); Assert.Contains(audit.Events,e=>e.Action==AuditActions.TenantMemberRemoved); }

    static TenantMemberService Build(bool enforcementEnabled, bool authEnabled, out InspectionReportsDbContext db, out CapturingAuditWriter audit, int? limit = 2, bool includeEntitlement = true, bool setTenant = true)
    {
        db = new InspectionReportsDbContext(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        SeedSubscription(db, "t1", limit, includeEntitlement);
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = enforcementEnabled.ToString().ToLowerInvariant(), ["Authentication:Enabled"] = authEnabled.ToString().ToLowerInvariant() }).Build();
        var tenant = new TenantContextAccessor(); if (setTenant) tenant.Current = new TenantContext { ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111") };
        var entitlementService = new EntitlementService(cfg, tenant, db);
        var entitlementGuard = new EntitlementGuard(cfg, entitlementService);
        var usageService = new SubscriptionSeatUsageService(cfg, tenant, db);
        var guard = new EntitlementSeatLimitGuard(cfg, entitlementGuard, usageService);
        audit = new CapturingAuditWriter();
        return new TenantMemberService(cfg, tenant, db, guard, audit);
    }

    static void SeedSubscription(InspectionReportsDbContext db, string tenantId, int? limit, bool includeEntitlement)
    {
        db.SubscriptionPlans.Add(new SubscriptionPlan { Id = "p1", Name = "Plan", PlanCode = "p" });
        db.ClientSubscriptions.Add(new ClientSubscription { Id = "s1", ClientOrganizationId = tenantId, PlanId = "p1", Status = SubscriptionStatuses.Active, StartsAtUtc = DateTime.UtcNow.AddDays(-1) });
        if (includeEntitlement) db.SubscriptionEntitlements.Add(new SubscriptionEntitlement { Id = "e1", PlanId = "p1", EntitlementKey = EntitlementKeys.MaxUsers, Enabled = true, LimitValue = limit });
        db.SaveChanges();
    }

    static ClientOrganizationUser User(string id, string tenantId, string status, string? email = null) => new() { Id = id, ClientOrganizationId = tenantId, Status = status, Email = email, NormalizedEmail = email?.Trim().ToLowerInvariant(), ExternalSubject = $"subject-{id}", CreatedAtUtc = DateTime.UtcNow };

    sealed class CapturingAuditWriter : IAuditEventWriter
    {
        public List<(string Action, IDictionary<string, object?>? Metadata)> Events { get; } = [];
        public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default) { Events.Add((action, metadata)); return Task.CompletedTask; }
    }
}
