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

        Assert.True(rows.Count >= 10);
        Assert.Contains(rows, r => r.ReportStatus is NdeReportStatuses.ReportReady or NdeReportStatuses.Downloaded);
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportNumber));
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportFileName));
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportDownloadUrl));
        Assert.All(rows, r => Assert.StartsWith("NDE-26-", r.RequestNumber));
        Assert.All(rows.Where(r => !string.IsNullOrWhiteSpace(r.ReportNumber)), r => Assert.StartsWith("RPT-26-", r.ReportNumber));
        Assert.Contains(rows, r => string.IsNullOrWhiteSpace(r.ReportNumber));
        Assert.Contains(rows, r => !string.IsNullOrWhiteSpace(r.ReportNumber) && string.IsNullOrWhiteSpace(r.ReportDownloadUrl));
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Draft);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Requested);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Scheduled);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.InProgress);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.ResultsReceived);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Reviewed);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Closed);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Cancelled);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Overdue);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.NotStarted);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.InProgress);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.ResultsReceived);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.ReportReady);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Downloaded);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.NotAvailable);
        Assert.All(rows.Where(r => r.Status is NdeLogStatuses.Draft or NdeLogStatuses.Requested or NdeLogStatuses.Scheduled or NdeLogStatuses.InProgress or NdeLogStatuses.Cancelled),
            r => Assert.DoesNotContain(r.ReportStatus, [NdeReportStatuses.ReportReady, NdeReportStatuses.Downloaded]));
        Assert.All(rows.Where(r => r.ReportStatus is NdeReportStatuses.ReportReady or NdeReportStatuses.Downloaded), r =>
        {
            Assert.Contains(r.Status, [NdeLogStatuses.Reviewed, NdeLogStatuses.Closed]);
            Assert.False(string.IsNullOrWhiteSpace(r.ReportNumber));
            Assert.False(string.IsNullOrWhiteSpace(r.ReportFileName));
            Assert.False(string.IsNullOrWhiteSpace(r.ReportDownloadUrl));
        });
        Assert.All(rows.Where(r => r.ReportStatus is NdeReportStatuses.ReportReady or NdeReportStatuses.Downloaded), r =>
        {
            Assert.False(string.IsNullOrWhiteSpace(r.ReportNumber));
            Assert.Equal($"{r.ReportNumber}.pdf", r.ReportFileName);
            Assert.Equal($"/demo-downloads/{r.ReportFileName}", r.ReportDownloadUrl);
        });
        Assert.All(rows.Where(r => !string.IsNullOrWhiteSpace(r.ReportNumber)), r =>
        {
            var methodCode = NdeNumbering.GetMethodAbbreviation(r.Method);
            Assert.Contains($"-26-{methodCode}-", r.ReportNumber!, StringComparison.Ordinal);
        });
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.ReportReady && r.ReportNumber == "RPT-26-PAUT-006");
        Assert.Contains(rows, r => r.RequestNumber == "NDE-26-010" && r.Status == NdeLogStatuses.Reviewed && r.ReportStatus == NdeReportStatuses.ReportReady);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Downloaded && r.ReportNumber == "RPT-26-VT-007");
        Assert.DoesNotContain(rows, r => r.Status == NdeLogStatuses.Scheduled && r.ReportStatus is NdeReportStatuses.ReportReady or NdeReportStatuses.Downloaded);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Scheduled);
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
