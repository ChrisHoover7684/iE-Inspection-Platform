using iE.Api.Controllers;
using iE.Api.Workflow;
using Microsoft.AspNetCore.Mvc;

public class NdeLogReadModelFoundationTests
{
    [Fact]
    public void GetLogItems_ReturnsSeededRows_WithReportMetadata()
    {
        var service = new DemoNdeLogReadModelService();
        var controller = new NdeLogController(service);

        var result = controller.GetLogItems();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var rows = Assert.IsAssignableFrom<IReadOnlyList<NdeLogItemReadModel>>(ok.Value);

        Assert.NotEmpty(rows);
        Assert.Contains(rows, r => r.ReportStatus is NdeReportStatuses.ReportReady or NdeReportStatuses.Downloaded);
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportNumber));
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportFileName));
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportDownloadUrl));
    }

    [Fact]
    public void Endpoints_Remain_Separated_For_NdeLog_And_ApiInspectionReports()
    {
        var ndeRoute = typeof(NdeLogController).GetCustomAttributes(typeof(RouteAttribute), false)
            .Cast<RouteAttribute>()
            .Single()
            .Template;
        var reportsRoute = typeof(ReportingController).GetCustomAttributes(typeof(RouteAttribute), false)
            .Cast<RouteAttribute>()
            .Single()
            .Template;

        Assert.Equal("api/nde/log", ndeRoute);
        Assert.Equal("api/reports", reportsRoute);
    }
}
