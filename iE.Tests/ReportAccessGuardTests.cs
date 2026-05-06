using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class ReportAccessGuardTests
{
    [Fact]
    public void AuthDisabled_AllowsAccess()
    {
        var guard = BuildGuard(false, out _, out _);
        var decision = guard.CanAccessReport(new InspectionReport { ClientOrganizationId = "other", FacilityId = "f1" }, AuthCapabilities.ReportsRead);
        Assert.Equal(ReportAccessDecision.Allowed, decision);
    }

    [Fact]
    public void AuthEnabled_MatchingTenantCapabilityAndFacility_AllowsAccess()
    {
        var guard = BuildGuard(true, out var db, out var accessor);
        accessor.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "u1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [AuthCapabilities.ReportsRead] };
        SeedAccess(db, "u1", "11111111-1111-1111-1111-111111111111", "f1");
        var decision = guard.CanAccessReport(new InspectionReport { ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "f1" }, AuthCapabilities.ReportsRead);
        Assert.Equal(ReportAccessDecision.Allowed, decision);
    }

    [Fact]
    public void AuthEnabled_TenantMismatch_NotFound() { var guard = BuildGuard(true, out var db, out var accessor); accessor.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "u1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [AuthCapabilities.ReportsRead] }; SeedAccess(db, "u1", "11111111-1111-1111-1111-111111111111", "f1"); Assert.Equal(ReportAccessDecision.NotFound, guard.CanAccessReport(new InspectionReport { ClientOrganizationId = "22222222-2222-2222-2222-222222222222", FacilityId = "f1" }, AuthCapabilities.ReportsRead)); }

    [Fact]
    public void AuthEnabled_MissingCapability_Forbidden() { var guard = BuildGuard(true, out var db, out var accessor); accessor.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "u1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111") }; SeedAccess(db, "u1", "11111111-1111-1111-1111-111111111111", "f1"); Assert.Equal(ReportAccessDecision.Forbidden, guard.CanAccessReport(new InspectionReport { ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "f1" }, AuthCapabilities.ReportsRead)); }

    [Fact]
    public void AuthEnabled_FacilityNotAllowed_NotFound() { var guard = BuildGuard(true, out var db, out var accessor); accessor.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "u1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [AuthCapabilities.ReportsRead] }; SeedAccess(db, "u1", "11111111-1111-1111-1111-111111111111", "f1"); Assert.Equal(ReportAccessDecision.NotFound, guard.CanAccessReport(new InspectionReport { ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = "f2" }, AuthCapabilities.ReportsRead)); }

    private static ReportAccessGuard BuildGuard(bool enabled, out InspectionReportsDbContext db, out ITenantContextAccessor accessor)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = enabled.ToString() }).Build();
        return new ReportAccessGuard(config, accessor, db);
    }

    private static void SeedAccess(InspectionReportsDbContext db, string userId, string orgId, string facilityId)
    {
        db.UserFacilityAccesses.Add(new UserFacilityAccess { Id = Guid.NewGuid().ToString("N"), UserId = userId, ClientOrganizationId = orgId, FacilityId = facilityId, IsActive = true });
        db.SaveChanges();
    }
}
