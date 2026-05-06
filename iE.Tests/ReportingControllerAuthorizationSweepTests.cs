using iE.Tests.TestDoubles;
using iE.Api.Contracts;
using iE.Api.Auth;
using iE.Api.Controllers;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Checklists;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Drafting;
using iE.Core.Reports.Persistence;
using iE.Core.Reports.Review;
using iE.Core.Reports.Rules;
using iE.Core.Reports.Services;
using iE.Core.Reports.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace iE.Tests;

public class ReportingControllerAuthorizationSweepTests
{
    [Fact]
    public async Task CreateInstance_MissingEntitlement_EmitsSafeEntitlementDeniedAudit()
    {
        var writer = new CapturingAuditEventWriter();
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: true, entitlementAllowed: false, auditWriter: writer);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);
        var r = await c.CreateInstance(Base("r-ent"));
        Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode);
        var evt = Assert.Single(writer.Events, e => e.Action == AuditActions.EntitlementDenied);
        Assert.Equal(AuditResults.Denied, evt.Result);
        Assert.Equal("CreateInstance", evt.Metadata?["route"]?.ToString());
        Assert.Equal(EntitlementKeys.ReportsCreate, evt.Metadata?["entitlementKey"]?.ToString());
        Assert.False(evt.Metadata?.ContainsKey("requestBody") ?? false);
    }

    [Fact]
    public async Task CreateInstance_LimitExceeded_Returns403_AndAuditsSafeMetadata()
    {
        var writer = new CapturingAuditEventWriter();
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: true, entitlementAllowed: true, usage: new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 5), limitValue: 5, auditWriter: writer);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);
        var r = await c.CreateInstance(Base("r-limit"));
        Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode);
        var evt = Assert.Single(writer.Events, e => e.Action == AuditActions.EntitlementLimitDenied);
        Assert.Equal(EntitlementKeys.MaxActiveReports, evt.Metadata?["entitlementKey"]?.ToString());
        Assert.False(evt.Metadata?.ContainsKey("metadataJson") ?? false);
    }


    [Fact]
    public async Task CreateInstanceFromTemplate_LimitExceeded_Returns403_SubscriptionLimitExceeded()
    {
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: true, entitlementAllowed: true, usage: new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 5), limitValue: 5);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);
        SeedDemoProcessUnitAndAsset(db);

        var r = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", null, null, null);
        var obj = Assert.IsType<ObjectResult>(r.Result);
        Assert.Equal(403, obj.StatusCode);
        var json = JsonSerializer.Serialize(obj.Value);
        Assert.Contains("subscription_limit_exceeded", json);
    }

    [Fact]
    public async Task BuildDraftFromChecklist_CreateBranch_LimitExceeded_Returns403_SubscriptionLimitExceeded()
    {
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: true, entitlementAllowed: true, usage: new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 2), limitValue: 2);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);

        var request = new ApplyObservationChecklistRequest
        {
            TemplateId = "api-570-piping-external",
            ChecklistResponses = [new ObservationChecklistItemResponse { ItemId = "visual-external", IsApplicable = true, HasFinding = false }],
            Report = Base("r-checklist-limit")
        };

        var r = await c.BuildDraftFromChecklist(request);
        var obj = Assert.IsType<ObjectResult>(r.Result);
        Assert.Equal(403, obj.StatusCode);
        var json = JsonSerializer.Serialize(obj.Value);
        Assert.Contains("subscription_limit_exceeded", json);
    }

    [Fact]
    public async Task CreateInstance_EnforcementDisabled_Allows_OverLimit()
    {
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: false, entitlementAllowed: true, usage: new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 1000), limitValue: 1);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);

        var r = await c.CreateInstance(Base("r-over-limit-disabled"));
        Assert.IsType<CreatedAtActionResult>(r.Result);
    }

    [Fact]
    public async Task CreateInstance_NullMaxActiveReports_Allows_Unlimited()
    {
        var c = BuildController(true, out var db, out var a, out _, entitlementEnabled: true, entitlementAllowed: true, usage: new SubscriptionUsageSnapshot("11111111-1111-1111-1111-111111111111", 1000), limitValue: null);
        SetTenant(a, AuthCapabilities.ReportsWrite);
        SeedAccess(db);
        SeedClientOrgAndFacility(db);

        var r = await c.CreateInstance(Base("r-unlimited"));
        Assert.IsType<CreatedAtActionResult>(r.Result);
    }
    [Fact]
    public async Task CreateFromTemplate_MissingWrite_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsRead); SeedAccess(db); var r = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", null, null, null); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public async Task CreateFromTemplate_OutOfScopeFacility_ReturnsNotFound() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); var r = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-b", null, null, null); Assert.IsType<NotFoundObjectResult>(r.Result); }
    [Fact]
    public async Task CreateFromTemplate_Allowed_StampsOwnership() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); SeedClientOrgAndFacility(db); SeedDemoProcessUnitAndAsset(db); var r = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", null, null, null); var created = Assert.IsType<InspectionReport>(((CreatedAtActionResult)r.Result!).Value); Assert.Equal("11111111-1111-1111-1111-111111111111", created.ClientOrganizationId); Assert.Equal("subject-1", created.CreatedByUserId); }
    [Fact]
    public async Task SyncFindings_MissingWrite_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsRead); SeedAccess(db); repo.Create(Base("r1")); var r = await c.SyncFindingsFromChecklistTransfers("r1"); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public async Task SubmitForReview_MissingSubmit_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); repo.Create(Base("r2")); var r = await c.SubmitForReview("r2"); var objectResult = Assert.IsType<ObjectResult>(r); Assert.Equal(403, objectResult.StatusCode); }
    [Fact]
    public async Task ReviewActions_MissingReview_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsSubmit); SeedAccess(db); repo.Create(Base("r3", InspectionReportStatuses.ReadyForReview)); var result = await c.StartReview("r3"); var objectResult = Assert.IsType<ObjectResult>(result); Assert.Equal(403, objectResult.StatusCode); }
    [Fact]
    public async Task ReviewHistory_MissingRead_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); repo.Create(Base("r4")); var r = await c.GetReviewHistory("r4"); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public async Task AuthDisabled_PathsRemainAccessible() { var c = BuildController(false, out _, out _, out var repo); repo.Create(Base("r5", InspectionReportStatuses.ReadyForReview, "client-a", "facility-a")); Assert.IsType<OkObjectResult>((await c.GetReviewHistory("r5")).Result); Assert.IsType<OkObjectResult>(await c.SubmitForReview("r5")); }

    private static InspectionReport Base(string id, string status = InspectionReportStatuses.Draft, string org = "11111111-1111-1111-1111-111111111111", string fac = "facility-a") => new() { Id = id, TemplateId = "api-570-piping-inspection", ClientOrganizationId = org, FacilityId = fac, Status = status, CreatedAt = DateTime.UtcNow.AddDays(-1), Observations = [new InspectionObservation { Id = "o", Status = ObservationStatus.Acceptable, Notes = "ok", Category = "ext" }] };
    private static void SetTenant(TenantContextAccessor a, params string[] caps) => a.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "subject-1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [..caps] };
    private static void SeedAccess(InspectionReportsDbContext db) { db.UserFacilityAccesses.Add(new UserFacilityAccess { Id = Guid.NewGuid().ToString("N"), UserId = "subject-1", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "facility-a", IsActive = true }); db.SaveChanges(); }
    private static void SeedClientOrgAndFacility(InspectionReportsDbContext db) { db.ClientOrganizations.Add(new ClientOrganization { Id = "11111111-1111-1111-1111-111111111111", Name = "Test Org", IsActive = true }); db.Facilities.Add(new Facility { Id = "facility-a", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", Name = "Test Facility", IsActive = true }); db.SaveChanges(); }
    private static void SeedDemoProcessUnitAndAsset(InspectionReportsDbContext db) { db.ProcessUnits.Add(new ProcessUnit { Id = "unit-demo-crude", FacilityId = "facility-a", Name = "Crude Unit", UnitCode = "001", IsActive = true }); db.Assets.Add(new Asset { Id = "asset-demo-piping-system", FacilityId = "facility-a", ProcessUnitId = "unit-demo-crude", EquipmentTag = "TAG-1", EquipmentType = "Piping", Service = "Service", IsActive = true }); db.SaveChanges(); }

    private static ReportingController BuildController(bool authEnabled, out InspectionReportsDbContext db, out TenantContextAccessor accessor, out InspectionReportRepository repo, bool entitlementEnabled = false, bool entitlementAllowed = true, SubscriptionUsageSnapshot? usage = null, int? limitValue = null, IAuditEventWriter? auditWriter = null)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        var guard = new ReportAccessGuard(config, accessor, db);
        var entitlementConfig = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = entitlementEnabled.ToString().ToLowerInvariant() }).Build();
        var entitlementGuard = new EntitlementGuard(entitlementConfig, new StubEntitlementService(entitlementAllowed ? EntitlementCheckResult.AllowedWithLimit(limitValue) : EntitlementCheckResult.Denied("entitlement_disabled")));
        var limitGuard = new EntitlementLimitGuard(entitlementConfig, entitlementGuard, new StubSubscriptionUsageService(usage));
        var referenceGuard = new ReportReferenceGuard(config, db, new NoopAuditEventWriter());
        repo = new InspectionReportRepository(db);

        var controller = new ReportingController(repo, new InspectionReportFactory(), null!, null!, new ReportDraftBuilder(new SummaryBuilder(), new ReportValidationService(new InspectionTagRuleEngine()), new RepairRecommendationBuilder()), null!, new ObservationChecklistService(), new ChecklistMergeService(), new ReportWorkflowService(), new InMemoryReportTemplateRegistry(), new InspectionTagRuleEngine(), null!, null!, null!, guard, referenceGuard, entitlementGuard, limitGuard, auditWriter ?? new NoopAuditEventWriter());
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        return controller;
    }

    private sealed class CapturingAuditEventWriter : IAuditEventWriter
    {
        public List<(string Action, string ResourceType, string? ResourceId, string Result, string? FacilityId, IDictionary<string, object?>? Metadata)> Events { get; } = [];
        public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
        {
            Events.Add((action, resourceType, resourceId, result, facilityId, metadata));
            return Task.CompletedTask;
        }
    }
}
