using iE.Api.Auth;
using iE.Api.Controllers;
using iE.Api.Tenancy;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using iE.Core.Reports.Templates;
using iE.Core.Reports.Services;
using iE.Tests.TestDoubles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class ReportingControllerReferenceValidationTests
{
    [Fact]
    public async Task CreateFromTemplate_ValidReferences_Succeeds()
    {
        var c = BuildController(true, out var db, out var a, new NoopAuditEventWriter());
        SeedTenantData(db);
        SeedAccess(db, "facility-a");
        SetTenant(a);

        var result = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", "unit-a", "asset-a", null);
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task CreateFromTemplate_ProcessUnitAnotherFacility_ReturnsNotFound()
    {
        var c = BuildController(true, out var db, out var a, new NoopAuditEventWriter());
        SeedTenantData(db);
        SeedAccess(db, "facility-a");
        SetTenant(a);

        var result = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", "unit-b", "asset-a", null);
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFromTemplate_AssetAnotherFacility_ReturnsNotFound()
    {
        var c = BuildController(true, out var db, out var a, new NoopAuditEventWriter());
        SeedTenantData(db);
        SeedAccess(db, "facility-a");
        SetTenant(a);

        var result = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", "unit-a", "asset-c", null);
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFromTemplate_AssetProcessUnitMismatch_ReturnsBadRequest()
    {
        var c = BuildController(true, out var db, out var a, new NoopAuditEventWriter());
        SeedTenantData(db);
        SeedAccess(db, "facility-a");
        SetTenant(a);

        var result = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", "unit-a", "asset-b", null);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFromTemplate_ValidationFailure_EmitsSafeAuditEvent()
    {
        var writer = new CapturingAuditEventWriter();
        var c = BuildController(true, out var db, out var a, writer);
        SeedTenantData(db);
        SeedAccess(db, "facility-a");
        SetTenant(a);

        var result = await c.CreateInstanceFromTemplate("api-570-piping-external", null, "facility-a", "unit-a", "asset-b", null);
        Assert.IsType<BadRequestObjectResult>(result.Result);

        var evt = Assert.Single(writer.Events, e => e.Action == AuditActions.ReportReferenceValidationFailed);
        Assert.Equal(AuditResourceTypes.Report, evt.ResourceType);
        Assert.Equal(AuditResults.Denied, evt.Result);
        Assert.True(evt.Metadata?.ContainsKey("route"));
        Assert.Equal("CreateInstanceFromTemplate", evt.Metadata!["route"]?.ToString());
        Assert.True(evt.Metadata.ContainsKey("reasonCode"));
        Assert.False(evt.Metadata.ContainsKey("rawPayload"));
        Assert.False(evt.Metadata.ContainsKey("requestBody"));
    }

    [Fact]
    public async Task AuthDisabled_CreateInstance_RemainsCompatible()
    {
        var c = BuildController(false, out _, out _, new NoopAuditEventWriter());
        var result = await c.CreateInstance(Base("client-any", "facility-any"));
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    private static InspectionReport Base(string org = "11111111-1111-1111-1111-111111111111", string facility = "facility-a") => new() { Id = "r-1", TemplateId = "api-570-piping-inspection", ClientOrganizationId = org, FacilityId = facility, Status = InspectionReportStatuses.Draft, CreatedAt = DateTime.UtcNow };
    private static void SetTenant(TenantContextAccessor a) => a.Current = new TenantContext { IsAuthenticated = true, ExternalSubject = "subject-1", ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Capabilities = [AuthCapabilities.ReportsWrite] };

    private static void SeedTenantData(InspectionReportsDbContext db)
    {
        db.ClientOrganizations.AddRange(new ClientOrganization { Id = "11111111-1111-1111-1111-111111111111", Name = "A", IsActive = true }, new ClientOrganization { Id = "22222222-2222-2222-2222-222222222222", Name = "B", IsActive = true });
        db.Facilities.AddRange(new Facility { Id = "facility-a", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", Name = "A", IsActive = true }, new Facility { Id = "facility-c", ClientOrganizationId = "22222222-2222-2222-2222-222222222222", Name = "B", IsActive = true });
        db.ProcessUnits.AddRange(new ProcessUnit { Id = "unit-a", FacilityId = "facility-a", Name = "u", UnitCode = "1", IsActive = true }, new ProcessUnit { Id = "unit-b", FacilityId = "facility-c", Name = "u", UnitCode = "1", IsActive = true });
        db.Assets.AddRange(new Asset { Id = "asset-a", FacilityId = "facility-a", ProcessUnitId = "unit-a", EquipmentTag = "t", EquipmentType = "p", Service = "s", IsActive = true }, new Asset { Id = "asset-b", FacilityId = "facility-a", ProcessUnitId = "unit-b", EquipmentTag = "t", EquipmentType = "p", Service = "s", IsActive = true }, new Asset { Id = "asset-c", FacilityId = "facility-c", ProcessUnitId = "unit-b", EquipmentTag = "t", EquipmentType = "p", Service = "s", IsActive = true });
        db.SaveChanges();
    }

    private static void SeedAccess(InspectionReportsDbContext db, string facilityId)
    {
        db.UserFacilityAccesses.Add(new UserFacilityAccess { Id = Guid.NewGuid().ToString("N"), UserId = "subject-1", ClientOrganizationId = "11111111-1111-1111-1111-111111111111", FacilityId = facilityId, IsActive = true });
        db.SaveChanges();
    }

    private static ReportingController BuildController(bool authEnabled, out InspectionReportsDbContext db, out TenantContextAccessor accessor, IAuditEventWriter auditWriter)
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        db = new InspectionReportsDbContext(options);
        accessor = new TenantContextAccessor();
        var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled.ToString() }).Build();
        var guard = new ReportAccessGuard(config, accessor, db);
        var referenceGuard = new ReportReferenceGuard(config, db, auditWriter);
        var controller = new ReportingController(new InspectionReportRepository(db), new InspectionReportFactory(), null!, null!, null!, null!, null!, null!, null!, new InMemoryReportTemplateRegistry(), null!, null!, null!, null!, guard, referenceGuard, auditWriter);
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
