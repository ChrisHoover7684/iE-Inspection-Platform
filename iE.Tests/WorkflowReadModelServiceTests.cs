using iE.Api.Tenancy;
using iE.Api.Workflow;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class WorkflowReadModelServiceTests
{
    [Fact]
    public async Task Tenant_And_Facility_Scopes_Are_Enforced_For_Nde_And_Timeline()
    {
        using var db = Db();
        Seed(db);
        var svc = Build(db, true, "11111111-1111-1111-1111-111111111111");

        var nde = await svc.QueryNdeRequestsAsync(new NdeRequestQueryFilter(FacilityId: "f1", IncludeCancelled: true));
        Assert.All(nde.Items, x => Assert.Equal("f1", x.FacilityId));
        Assert.DoesNotContain(nde.Items, x => x.Id == "n3");

        var timeline = await svc.QueryReportTimelineAsync(new WorkflowQueryFilter(FacilityId: "f1", ReportId: "r1"));
        Assert.Equal("allowed", timeline.ReasonCode);
        Assert.True(timeline.Items.SequenceEqual(timeline.Items.OrderBy(x => x.CreatedAtUtc)));

        var cross = await svc.QueryReportTimelineAsync(new WorkflowQueryFilter(FacilityId: "f1", ReportId: "r3"));
        Assert.Empty(cross.Items);
    }

    [Fact]
    public async Task Filters_And_Client_Visibility_Are_Applied()
    {
        using var db = Db(); Seed(db);
        var svc = Build(db, false, null);

        var overdue = await svc.QueryNdeRequestsAsync(new NdeRequestQueryFilter(TenantId: "t1", Overdue: true));
        Assert.Contains(overdue.Items, x => x.IsOverdue);

        var high = await svc.QueryOwnerApprovalQueueAsync(new WorkflowQueryFilter(TenantId: "t1"));
        Assert.Contains(high.Items, x => x.Priority is "high" or "urgent");

        var clientLib = await svc.QueryReportLibraryAsync(new ReportLibraryQueryFilter(TenantId: "t1"), clientView: true);
        Assert.All(clientLib.Items, x => Assert.Equal(InspectionReportStatuses.Final, x.Status));

        var clientNde = await svc.QueryClientNdeLogAsync(new NdeRequestQueryFilter(TenantId: "t1", IncludeCancelled: true));
        Assert.All(clientNde.Items, x => Assert.NotNull(x.DisplayStatus));
    }

    static WorkflowReadModelService Build(InspectionReportsDbContext db, bool authEnabled, string? tenant)
    {
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?> { ["Authentication:Enabled"] = authEnabled ? "true" : "false" }).Build();
        var accessor = new TenantContextAccessor();
        if (tenant is not null) accessor.Current = new TenantContext { ClientOrganizationId = Guid.Parse(tenant) };
        return new WorkflowReadModelService(db, accessor, cfg);
    }

    static InspectionReportsDbContext Db()
    {
        var opts = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new InspectionReportsDbContext(opts);
    }

    static void Seed(InspectionReportsDbContext db)
    {
        db.ClientOrganizations.AddRange(new ClientOrganization { Id = "t1", Name = "T1" }, new ClientOrganization { Id = "t2", Name = "T2" });
        db.Facilities.AddRange(new Facility { Id = "f1", ClientOrganizationId = "t1", Name = "F1" }, new Facility { Id = "f2", ClientOrganizationId = "t2", Name = "F2" });
        db.InspectionReports.AddRange(
            new InspectionReport { Id = "r1", ClientOrganizationId = "t1", FacilityId = "f1", TemplateId = "api570", ReportNumber = "R-1", EquipmentTag = "E1", Status = InspectionReportStatuses.Draft },
            new InspectionReport { Id = "r2", ClientOrganizationId = "t1", FacilityId = "f1", TemplateId = "api510", ReportNumber = "R-2", EquipmentTag = "E2", Status = InspectionReportStatuses.Final },
            new InspectionReport { Id = "r3", ClientOrganizationId = "t2", FacilityId = "f2", TemplateId = "api570", ReportNumber = "R-3", EquipmentTag = "E3", Status = InspectionReportStatuses.Final });
        db.NdeRequests.AddRange(
            new NdeRequest { Id = "n1", ClientOrganizationId = "t1", FacilityId = "f1", ReportId = "r1", AssetId = "a1", NdeType = "pt", Priority = "high", Status = NdeRequestStatuses.Requested, Reason = "x", DueDateUtc = DateTime.UtcNow.AddDays(-1) },
            new NdeRequest { Id = "n2", ClientOrganizationId = "t1", FacilityId = "f1", ReportId = "r2", AssetId = "a1", NdeType = "ut_thickness", Priority = "normal", Status = NdeRequestStatuses.Closed, Reason = "x", ResultsReceivedAtUtc = DateTime.UtcNow.AddDays(-2) },
            new NdeRequest { Id = "n3", ClientOrganizationId = "t2", FacilityId = "f2", ReportId = "r3", AssetId = "a2", NdeType = "mt", Priority = "urgent", Status = NdeRequestStatuses.Canceled, Reason = "x" });
        db.ReportLogEntries.AddRange(
            new ReportLogEntry { Id = "l1", ClientOrganizationId = "t1", FacilityId = "f1", ReportId = "r1", EventType = "evt", Message = "m1", CreatedAtUtc = DateTime.UtcNow.AddMinutes(-2) },
            new ReportLogEntry { Id = "l2", ClientOrganizationId = "t1", FacilityId = "f1", ReportId = "r1", EventType = "evt2", Message = "m2", CreatedAtUtc = DateTime.UtcNow.AddMinutes(-1) });
        db.SaveChanges();
    }
}
