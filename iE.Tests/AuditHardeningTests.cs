using iE.Api;
using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports.Persistence;
using iE.Tests.TestDoubles;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace iE.Tests;

public class AuditHardeningTests
{
    [Fact] public async Task Writer_Trims_And_Sanitizes_And_Bounds_Metadata() { var db=Db(); var w=Writer(db); await w.WriteAsync(new string('a',200),new string('b',100),new string('c',200),new string('d',50),new string('e',100), new Dictionary<string,object?>{["Authorization"]="x",["nested"]=new Dictionary<string,object?>{{"password","y"}},["ok"]=new string('z',6000)}); var e=db.AuditEvents.Single(); Assert.True(e.Action.Length<=128); Assert.DoesNotContain("Authorization",e.MetadataJson!,StringComparison.OrdinalIgnoreCase); Assert.DoesNotContain("password",e.MetadataJson!,StringComparison.OrdinalIgnoreCase); Assert.True((e.MetadataJson?.Length ?? 0)<=4000); }

    [Fact] public async Task Writer_Does_Not_Throw_On_Bad_Metadata() { var db=Db(); var w=Writer(db); await w.WriteAsync("a","r",null,"s",metadata:new Dictionary<string,object?>{{"bad",new BadMeta()}}); Assert.Single(db.AuditEvents); }

    [Fact] public async Task AuditEvent_AppendOnly_Guard_Rejects_Modify_Delete() { var db=Db(); db.AuditEvents.Add(new AuditEvent{AuditEventId="1",Action="a",ResourceType="r",Result="s",OccurredAtUtc=DateTime.UtcNow}); await db.SaveChangesAsync(); var e=db.AuditEvents.Single(); e.Action="x"; await Assert.ThrowsAsync<InvalidOperationException>(()=>db.SaveChangesAsync()); db.Entry(e).State=EntityState.Unchanged; db.AuditEvents.Remove(e); await Assert.ThrowsAsync<InvalidOperationException>(()=>db.SaveChangesAsync()); }

    [Fact] public async Task Query_Service_Tenant_Scopes_When_Enabled() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","true"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor{Current=new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111")}},cfg,BuildEntitlementGuard()); var rows=await svc.QueryAsync(new AuditEventQuery{Limit=500}); Assert.All(rows,r=>Assert.Equal("11111111-1111-1111-1111-111111111111",r.TenantId)); }

    [Fact] public async Task Query_Service_Empty_When_Enabled_Without_Tenant() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","true"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor(),cfg,BuildEntitlementGuard()); var rows=await svc.QueryAsync(new AuditEventQuery()); Assert.Empty(rows); }

    [Fact] public async Task Query_Service_Local_Mode_Uses_Filters_And_Limits() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","false"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor(),cfg,BuildEntitlementGuard()); var rows=await svc.QueryAsync(new AuditEventQuery{TenantId="t2",Action="A2",Limit=9999}); Assert.Single(rows); Assert.Null(rows[0].MetadataJson); }

    [Fact] public async Task Query_Service_EnforcementEnabled_MissingAuditQuery_ReturnsEmpty() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","true"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor{Current=new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111")}},cfg,BuildEntitlementGuard(enforcementEnabled:true,entitlementAllowed:false)); var rows=await svc.QueryAsync(new AuditEventQuery()); Assert.Empty(rows); }
    [Fact] public async Task Query_Service_EnforcementEnabled_AuditQueryAllowed_ReturnsTenantScopedRows() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","true"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor{Current=new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111")}},cfg,BuildEntitlementGuard(enforcementEnabled:true,entitlementAllowed:true)); var rows=await svc.QueryAsync(new AuditEventQuery()); Assert.Single(rows); Assert.All(rows,r=>Assert.Equal("11111111-1111-1111-1111-111111111111",r.TenantId)); }
    [Fact] public async Task Query_Service_EnforcementDisabled_AllowsRows_EvenIfEntitlementWouldDeny() { var db=Db(); Seed(db); var cfg=new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","true"}}).Build(); var svc=new AuditEventQueryService(db,new TenantContextAccessor{Current=new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111")}},cfg,BuildEntitlementGuard(enforcementEnabled:false,entitlementAllowed:false)); var rows=await svc.QueryAsync(new AuditEventQuery()); Assert.Single(rows); }
    
    [Fact]
    public async Task Writer_Sanitizer_Removes_Sensitive_Keys_Case_Insensitively()
    {
        var db = Db();
        var writer = Writer(db);
        await writer.WriteAsync("a", "r", "1", "s", metadata: new Dictionary<string, object?>
        {
            ["Authorization"] = "x",
            ["AUTHORIZATION"] = "x",
            ["accessToken"] = "x",
            ["AccessToken"] = "x",
            ["APIKEY"] = "x",
            ["ConnectionString"] = "x",
            ["StackTrace"] = "x",
            ["ok"] = true
        });

        var json = db.AuditEvents.Single().MetadataJson!;
        Assert.DoesNotContain("Authorization", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("accessToken", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("APIKEY", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("StackTrace", json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ok", json, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Writer_Sanitizer_Removes_Nested_Mixed_Case_Sensitive_Keys()
    {
        var db = Db();
        var writer = Writer(db);
        await writer.WriteAsync("a", "r", "1", "s", metadata: new Dictionary<string, object?>
        {
            ["safe"] = new Dictionary<string, object?> { ["Authorization"] = "secret", ["ok"] = "value" }
        });

        var json = db.AuditEvents.Single().MetadataJson!;
        Assert.DoesNotContain("Authorization", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("secret", json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ok", json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("value", json, StringComparison.OrdinalIgnoreCase);
    }

    static InspectionReportsDbContext Db()=>new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
    static AuditEventWriter Writer(InspectionReportsDbContext db){var http=new HttpContextAccessor{HttpContext=new DefaultHttpContext()}; return new AuditEventWriter(db,new TenantContextAccessor{Current=new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111"),ExternalSubject="u1"}},http,NullLogger<AuditEventWriter>.Instance);}    
    static void Seed(InspectionReportsDbContext db){db.AuditEvents.AddRange(new AuditEvent{AuditEventId="1",TenantId="11111111-1111-1111-1111-111111111111",Action="A1",ResourceType="R",ResourceId="1",Result="Success",OccurredAtUtc=DateTime.UtcNow.AddMinutes(-1)}, new AuditEvent{AuditEventId="2",TenantId="t2",Action="A2",ResourceType="R",ResourceId="2",Result="Denied",OccurredAtUtc=DateTime.UtcNow}); db.SaveChanges();}
    static EntitlementGuard BuildEntitlementGuard(bool enforcementEnabled = false, bool entitlementAllowed = true)
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Subscriptions:EnforcementEnabled"] = enforcementEnabled.ToString().ToLowerInvariant()
        }).Build();

        return new EntitlementGuard(cfg, new StubEntitlementService(entitlementAllowed ? EntitlementCheckResult.AllowedWithLimit(null) : EntitlementCheckResult.Denied("entitlement_disabled")));
    }
    class BadMeta{public override string ToString()=>throw new Exception("bad");}
}
