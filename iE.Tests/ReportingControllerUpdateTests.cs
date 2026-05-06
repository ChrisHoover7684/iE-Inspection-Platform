using iE.Tests.TestDoubles;
using iE.Api.Auth;
using iE.Api.Controllers;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class ReportingControllerUpdateTests
{
    [Fact]
    public async Task UpdateInstance_PreservesOwnershipFields_AndStampsUpdater_WhenAuthEnabled()
    {
        var controller = BuildController(true, out var db, out var accessor, out var repo);
        accessor.Current = new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = "subject-1",
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Capabilities = [AuthCapabilities.ReportsWrite]
        };

        SeedAccess(db, "subject-1", "11111111-1111-1111-1111-111111111111", "facility-a");
        SeedClientOrgAndFacility(db);
        var createdAt = DateTime.UtcNow.AddDays(-3);
        repo.Create(new InspectionReport
        {
            Id = "r-1",
            ClientOrganizationId = "11111111-1111-1111-1111-111111111111",
            FacilityId = "facility-a",
            CreatedAt = createdAt,
            CreatedByUserId = "creator-original",
            Status = InspectionReportStatuses.Draft
        });

        var submitted = new InspectionReport
        {
            Id = "different-id",
            ClientOrganizationId = "22222222-2222-2222-2222-222222222222",
            FacilityId = "facility-b",
            CreatedAt = DateTime.UtcNow,
            CreatedByUserId = "attacker",
            Status = InspectionReportStatuses.ReadyForReview
        };

        var result = await controller.UpdateInstance("r-1", submitted);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var updated = Assert.IsType<InspectionReport>(ok.Value);

        Assert.Equal("r-1", updated.Id);
        Assert.Equal("11111111-1111-1111-1111-111111111111", updated.ClientOrganizationId);
        Assert.Equal("facility-a", updated.FacilityId);
        Assert.Equal(createdAt, updated.CreatedAt);
        Assert.Equal("creator-original", updated.CreatedByUserId);
        Assert.Equal("subject-1", updated.UpdatedByUserId);
        Assert.True(updated.UpdatedAt.HasValue);
    }

    [Fact]
    public async Task UpdateInstance_AuthDisabled_DoesNotOverwriteOwnershipFromExistingUserContext()
    {
        var controller = BuildController(false, out var db, out _, out var repo);
        repo.Create(new InspectionReport
        {
            Id = "r-2",
            ClientOrganizationId = "client-a",
            FacilityId = "facility-a",
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            CreatedByUserId = "creator-original",
            Status = InspectionReportStatuses.Draft
        });

        var submitted = new InspectionReport
        {
            ClientOrganizationId = "client-b",
            FacilityId = "facility-b",
            CreatedByUserId = "attacker"
        };

        var result = await controller.UpdateInstance("r-2", submitted);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var updated = Assert.IsType<InspectionReport>(ok.Value);

        Assert.Equal("client-a", updated.ClientOrganizationId);
        Assert.Equal("facility-a", updated.FacilityId);
        Assert.Equal("creator-original", updated.CreatedByUserId);
        Assert.Null(updated.UpdatedByUserId);
    }

    [Fact]
    public async Task UpdateInstance_CrossTenant_RemainsNotFound()
    {
        var controller = BuildController(true, out var db, out var accessor, out var repo);
        accessor.Current = new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = "subject-1",
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Capabilities = [AuthCapabilities.ReportsWrite]
        };

        SeedAccess(db, "subject-1", "11111111-1111-1111-1111-111111111111", "facility-a");
        repo.Create(new InspectionReport { Id = "r-3", ClientOrganizationId = "22222222-2222-2222-2222-222222222222", FacilityId = "facility-a", CreatedAt = DateTime.UtcNow });

        var result = await controller.UpdateInstance("r-3", new InspectionReport());
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateInstance_CrossFacility_RemainsNotFound()
    {
        var controller = BuildController(true, out var db, out var accessor, out var repo);
        accessor.Current = new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = "subject-1",
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Capabilities = [AuthCapabilities.ReportsWrite]
        };

        SeedAccess(db, "subject-1", "11111111-1111-1111-1111-111111111111", "facility-a");
        repo.Create(new InspectionReport { Id = "r-4", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "facility-b", CreatedAt = DateTime.UtcNow });

        var result = await controller.UpdateInstance("r-4", new InspectionReport());
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateInstance_MissingReportsWrite_RemainsForbidden()
    {
        var controller = BuildController(true, out var db, out var accessor, out var repo);
        accessor.Current = new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = "subject-1",
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Capabilities = [AuthCapabilities.ReportsRead]
        };

        SeedAccess(db, "subject-1", "11111111-1111-1111-1111-111111111111", "facility-a");
        repo.Create(new InspectionReport { Id = "r-5", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "facility-a", CreatedAt = DateTime.UtcNow });

        var result = await controller.UpdateInstance("r-5", new InspectionReport());
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(403, objectResult.StatusCode);
    }

    private static ReportingController BuildController(bool authEnabled, out InspectionReportsDbContext db, out TenantContextAccessor accessor, out InspectionReportRepository repo)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        var guard = new ReportAccessGuard(config, accessor, db);
        var entitlementConfig = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Subscriptions:EnforcementEnabled"] = "false" }).Build();
        var entitlementGuard = new EntitlementGuard(entitlementConfig, new StubEntitlementService());
        var referenceGuard = new ReportReferenceGuard(config, db, new NoopAuditEventWriter());
        repo = new InspectionReportRepository(db);

        var controller = new ReportingController(repo, null!, null!, null!, null!, null!, null!, null!, null!, null!, null!, null!, null!, null!, guard, referenceGuard, entitlementGuard, new NoopAuditEventWriter());
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        return controller;
    }

    private static void SeedAccess(InspectionReportsDbContext db, string userId, string orgId, string facilityId)
    {
        db.UserFacilityAccesses.Add(new UserFacilityAccess { Id = Guid.NewGuid().ToString("N"), UserId = userId, ClientOrganizationId = orgId, FacilityId = facilityId, IsActive = true });
        db.SaveChanges();
    }

    private static void SeedClientOrgAndFacility(InspectionReportsDbContext db)
    {
        db.ClientOrganizations.Add(new ClientOrganization
        {
            Id = "11111111-1111-1111-1111-111111111111",
            Name = "Test Org",
            IsActive = true
        });
        db.Facilities.Add(new Facility
        {
            Id = "facility-a",
            ClientOrganizationId = "11111111-1111-1111-1111-111111111111",
            Name = "Test Facility",
            IsActive = true
        });
        db.SaveChanges();
    }
}
