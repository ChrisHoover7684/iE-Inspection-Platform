using iE.Api.Auth;
using iE.Api.Controllers;
using iE.Api.Contracts;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class PhotoMarkupsControllerAuthorizationTests
{
    [Fact]
    public void AuthDisabled_AllowsReadAndWriteForKnownPhoto()
    {
        var controller = BuildController(false, out var db, out _, out _);
        SeedReportWithPhoto(db, reportId: "r-auth-off", photoId: "p-auth-off");

        var create = controller.Create("p-auth-off", ValidRequest());
        var read = controller.Get("p-auth-off");

        Assert.IsType<CreatedResult>(create.Result);
        Assert.IsType<OkObjectResult>(read.Result);
    }

    [Fact]
    public void InScope_WithPhotosRead_CanReadMarkup()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-read", photoId: "p-read");
        SeedMarkup(db, "p-read");
        SetTenant(accessor, AuthCapabilities.PhotosRead);

        var read = controller.Get("p-read");

        Assert.IsType<OkObjectResult>(read.Result);
    }

    [Fact]
    public void InScope_WithPhotosWrite_CanCreateMarkup()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-write", photoId: "p-write");
        SetTenant(accessor, AuthCapabilities.PhotosWrite);

        var create = controller.Create("p-write", ValidRequest());

        Assert.IsType<CreatedResult>(create.Result);
    }

    [Fact]
    public void CrossTenantOwningReport_ReturnsNotFound()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-tenant", photoId: "p-tenant", orgId: "22222222-2222-2222-2222-222222222222");
        SetTenant(accessor, AuthCapabilities.PhotosRead, AuthCapabilities.PhotosWrite);

        var read = controller.Get("p-tenant");

        Assert.IsType<NotFoundObjectResult>(read.Result);
    }

    [Fact]
    public void CrossFacilityOwningReport_ReturnsNotFound()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-facility", photoId: "p-facility", facilityId: "facility-b");
        SetTenant(accessor, AuthCapabilities.PhotosRead, AuthCapabilities.PhotosWrite);

        var read = controller.Get("p-facility");

        Assert.IsType<NotFoundObjectResult>(read.Result);
    }

    [Fact]
    public void MissingReadCapability_ReturnsForbidden()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-no-read", photoId: "p-no-read");
        SeedMarkup(db, "p-no-read");
        SetTenant(accessor, AuthCapabilities.PhotosWrite);

        var read = controller.Get("p-no-read");

        Assert.Equal(403, ((ObjectResult)read.Result!).StatusCode);
    }

    [Fact]
    public void MissingWriteCapability_ReturnsForbidden()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SeedReportWithPhoto(db, reportId: "r-no-write", photoId: "p-no-write");
        SetTenant(accessor, AuthCapabilities.PhotosRead);

        var create = controller.Create("p-no-write", ValidRequest());

        Assert.Equal(403, ((ObjectResult)create.Result!).StatusCode);
    }

    [Fact]
    public void UnknownOrOrphanPhoto_ReturnsNotFound()
    {
        var controller = BuildController(true, out var db, out var accessor, out _);
        SeedFacilityAccess(db, "facility-a");
        SetTenant(accessor, AuthCapabilities.PhotosRead, AuthCapabilities.PhotosWrite);

        var read = controller.Get("missing-photo");
        var create = controller.Create("missing-photo", ValidRequest());

        Assert.IsType<NotFoundObjectResult>(read.Result);
        Assert.IsType<NotFoundObjectResult>(create.Result);
    }

    private static CreatePhotoMarkupRequest ValidRequest() => new() { MarkupJson = "{\"annotations\":[{\"type\":\"circle\"}]}" };

    private static PhotoMarkupsController BuildController(bool authEnabled, out InspectionReportsDbContext db, out TenantContextAccessor accessor, out PhotoMarkupRepository repo)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        var guard = new ReportAccessGuard(config, accessor, db);
        repo = new PhotoMarkupRepository(db);

        var controller = new PhotoMarkupsController(repo, guard);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        return controller;
    }

    private static void SetTenant(TenantContextAccessor accessor, params string[] capabilities)
    {
        accessor.Current = new TenantContext
        {
            IsAuthenticated = true,
            ExternalSubject = "subject-1",
            ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Capabilities = [.. capabilities]
        };
    }

    private static void SeedFacilityAccess(InspectionReportsDbContext db, string facilityId)
    {
        db.UserFacilityAccesses.Add(new UserFacilityAccess
        {
            Id = Guid.NewGuid().ToString("N"),
            UserId = "subject-1",
            ClientOrganizationId = "11111111-1111-1111-1111-111111111111",
            FacilityId = facilityId,
            IsActive = true
        });
        db.SaveChanges();
    }

    private static void SeedReportWithPhoto(InspectionReportsDbContext db, string reportId, string photoId, string orgId = "11111111-1111-1111-1111-111111111111", string facilityId = "facility-a")
    {
        db.InspectionReports.Add(new InspectionReport
        {
            Id = reportId,
            TemplateId = "api-570-piping-inspection",
            ClientOrganizationId = orgId,
            FacilityId = facilityId,
            Status = InspectionReportStatuses.Draft,
            CreatedAt = DateTime.UtcNow,
            Photos = [new InspectionPhoto { Id = photoId, PhotoNumber = "1", Description = "d" }]
        });
        db.SaveChanges();
    }

    private static void SeedMarkup(InspectionReportsDbContext db, string photoId)
    {
        db.PhotoMarkups.Add(new PhotoMarkup
        {
            Id = Guid.NewGuid().ToString("N"),
            PhotoId = photoId,
            MarkupJson = "{\"annotations\":[{\"type\":\"circle\"}]}",
            CreatedAt = DateTime.UtcNow
        });
        db.SaveChanges();
    }
}
