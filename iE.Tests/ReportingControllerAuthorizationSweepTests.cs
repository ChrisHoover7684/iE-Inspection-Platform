using iE.Tests.TestDoubles;
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

namespace iE.Tests;

public class ReportingControllerAuthorizationSweepTests
{
    [Fact]
    public void CreateFromTemplate_MissingWrite_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsRead); SeedAccess(db); var r = c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", null, null, null); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public void CreateFromTemplate_OutOfScopeFacility_ReturnsNotFound() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); var r = c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-b", null, null, null); Assert.IsType<NotFoundObjectResult>(r.Result); }
    [Fact]
    public void CreateFromTemplate_Allowed_StampsOwnership() { var c = BuildController(true, out var db, out var a, out _); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); var r = c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", null, null, null); var created = Assert.IsType<InspectionReport>(((CreatedAtActionResult)r.Result!).Value); Assert.Equal("11111111-1111-1111-1111-111111111111", created.ClientOrganizationId); Assert.Equal("subject-1", created.CreatedByUserId); }
    [Fact]
    public void SyncFindings_MissingWrite_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsRead); SeedAccess(db); repo.Create(Base("r1")); var r = c.SyncFindingsFromChecklistTransfers("r1"); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public void SubmitForReview_MissingSubmit_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); repo.Create(Base("r2")); var r = c.SubmitForReview("r2"); var objectResult = Assert.IsType<ObjectResult>(r); Assert.Equal(403, objectResult.StatusCode); }
    [Fact]
    public void ReviewActions_MissingReview_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsSubmit); SeedAccess(db); repo.Create(Base("r3", InspectionReportStatuses.ReadyForReview)); var result = c.StartReview("r3"); var objectResult = Assert.IsType<ObjectResult>(result); Assert.Equal(403, objectResult.StatusCode); }
    [Fact]
    public void ReviewHistory_MissingRead_ReturnsForbidden() { var c = BuildController(true, out var db, out var a, out var repo); SetTenant(a, AuthCapabilities.ReportsWrite); SeedAccess(db); repo.Create(Base("r4")); var r = c.GetReviewHistory("r4"); Assert.Equal(403, ((ObjectResult)r.Result!).StatusCode); }
    [Fact]
    public void AuthDisabled_PathsRemainAccessible() { var c = BuildController(false, out _, out _, out var repo); repo.Create(Base("r5", InspectionReportStatuses.ReadyForReview, "client-a", "facility-a")); Assert.IsType<OkObjectResult>(c.GetReviewHistory("r5").Result); Assert.IsType<OkObjectResult>(c.SubmitForReview("r5")); }

    private static InspectionReport Base(string id, string status = InspectionReportStatuses.Draft, string org = "11111111-1111-1111-1111-111111111111", string fac = "facility-a") => new() { Id = id, TemplateId = "api-570-piping-inspection", ClientOrganizationId = org, FacilityId = fac, Status = status, CreatedAt = DateTime.UtcNow.AddDays(-1), Observations = [new InspectionObservation { Id = "o", Status = ObservationStatus.Acceptable, Notes = "ok", Category = "ext" }] };
    private static void SetTenant(TenantContextAccessor a, params string[] caps) => a.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "subject-1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [..caps] };
    private static void SeedAccess(InspectionReportsDbContext db) { db.UserFacilityAccesses.Add(new UserFacilityAccess { Id = Guid.NewGuid().ToString("N"), UserId = "subject-1", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "facility-a", IsActive = true }); db.SaveChanges(); }

    private static ReportingController BuildController(bool authEnabled, out InspectionReportsDbContext db, out TenantContextAccessor accessor, out InspectionReportRepository repo)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        var guard = new ReportAccessGuard(config, accessor, db);
        repo = new InspectionReportRepository(db);

        var controller = new ReportingController(repo, new InspectionReportFactory(), null!, null!, new ReportDraftBuilder(new SummaryBuilder(), new ReportValidationService(new InspectionTagRuleEngine()), new RepairRecommendationBuilder()), null!, new ObservationChecklistService(), new ChecklistMergeService(), new ReportWorkflowService(), new InMemoryReportTemplateRegistry(), new InspectionTagRuleEngine(), null!, null!, null!, guard, new NoopAuditEventWriter());
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        return controller;
    }
}
