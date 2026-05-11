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
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Complete);
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
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Complete);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.NotAvailable);
        Assert.Contains(rows, r => r.AccessType == "Ground");
        Assert.Contains(rows, r => r.AccessType == "Scaffold");
        Assert.Contains(rows, r => r.AccessType == "Rope Access");
        Assert.Contains(rows, r => r.AccessType == "Aerial Lift");
        Assert.Contains(rows, r => r.AccessType == "Ladder");
        Assert.Contains(rows, r => r.AccessType == "Platform");
        Assert.Contains(rows, r => r.AccessType == "Confined Space");
        Assert.All(
            rows.Where(r =>
                r.Status is NdeLogStatuses.Draft
                    or NdeLogStatuses.Requested
                    or NdeLogStatuses.Scheduled
                    or NdeLogStatuses.InProgress
                    or NdeLogStatuses.Cancelled),
            r => Assert.False(
                r.ReportStatus == NdeReportStatuses.Complete,
                $"{r.RequestNumber} should not be downloadable while status is {r.Status}."));
        Assert.All(rows.Where(r => r.ReportStatus == NdeReportStatuses.Complete), r =>
        {
            Assert.True(
                r.Status is NdeLogStatuses.Reviewed or NdeLogStatuses.Closed,
                $"{r.RequestNumber} has downloadable report status but request status is {r.Status}.");
            Assert.False(string.IsNullOrWhiteSpace(r.ReportNumber));
            Assert.False(string.IsNullOrWhiteSpace(r.ReportFileName));
            Assert.False(string.IsNullOrWhiteSpace(r.ReportDownloadUrl));
        });
        Assert.All(rows.Where(r => r.ReportStatus == NdeReportStatuses.Complete), r =>
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
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Complete && r.ReportNumber == "RPT-26-PAUT-0006");
        Assert.Contains(rows, r => r.RequestNumber == "NDE-26-0010" && r.Status == NdeLogStatuses.Reviewed && r.ReportStatus == NdeReportStatuses.Complete);
        Assert.Contains(rows, r => r.ReportStatus == NdeReportStatuses.Complete && r.ReportNumber == "RPT-26-VT-0007");
        Assert.DoesNotContain(rows, r => r.Status == NdeLogStatuses.Scheduled && r.ReportStatus == NdeReportStatuses.Complete);
        Assert.Contains(rows, r => r.Status == NdeLogStatuses.Scheduled);
        var stagedWeldRequests = rows.Where(r => r.RelatedRequestGroupId == "weld-w-22b-01-nozzle-n11-weld-213").ToList();
        Assert.Equal(3, stagedWeldRequests.Count);
        Assert.Contains(stagedWeldRequests, r => r.RequestNumber == "NDE-26-0004" && r.NdeStage == "Prep");
        Assert.Contains(stagedWeldRequests, r => r.RequestNumber == "NDE-26-0011" && r.NdeStage == "Root");
        Assert.Contains(stagedWeldRequests, r => r.RequestNumber == "NDE-26-0012" && r.NdeStage == "Final");
        Assert.All(stagedWeldRequests, r =>
        {
            Assert.Equal("W-22B-01", r.WeldId);
            Assert.Equal("Nozzle N11 Weld 213", r.Location);
        });
        Assert.Contains(rows, r => r.Project == "Demo Turnaround 2026" && r.OwningGroup == "Inspection" && r.Code == "API 570" && r.Unit == "Unit 73");
        Assert.Contains(rows, r => r.Project == "Unit 73 Maintenance" && r.OwningGroup == "Mechanical Integrity" && r.Code == "NBIC" && r.Unit == "Unit 73");
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
