using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iE.Tests;

public class SubscriptionUsageServiceTests
{
    [Fact]
    public async Task Counts_Only_CurrentTenant_NonFinal()
    {
        var db = Db();
        db.InspectionReports.AddRange(
            new InspectionReport{Id="1",ClientOrganizationId="t1",FacilityId="f",Status=InspectionReportStatuses.Draft},
            new InspectionReport{Id="2",ClientOrganizationId="t1",FacilityId="f",Status=InspectionReportStatuses.Final},
            new InspectionReport{Id="3",ClientOrganizationId="t2",FacilityId="f",Status=InspectionReportStatuses.Draft});
        db.SaveChanges();
        var tenant = new TenantContextAccessor{ Current = new TenantContext{ClientOrganizationId=Guid.Parse("11111111-1111-1111-1111-111111111111")}};
        var cfg = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?>{{"Authentication:Enabled","false"}}).Build();
        var svc = new SubscriptionUsageService(cfg, tenant, db);
        var usage = await svc.GetUsageAsync("t1");
        Assert.NotNull(usage);
        Assert.Equal(1, usage!.ActiveReportCount);
    }
}
