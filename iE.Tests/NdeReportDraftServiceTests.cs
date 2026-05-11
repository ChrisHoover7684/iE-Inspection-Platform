using iE.Api.Workflow;

namespace iE.Tests;

public class NdeReportDraftServiceTests
{
    [Fact]
    public void SaveDraft_ForExistingRequest_PersistsAndUpdatesMetadata()
    {
        var log = new DemoNdeLogReadModelService();
        var service = new DemoNdeReportDraftService(log);
        var result = service.SaveDraft("nde-001", new NdeReportDraftSaveRequest(null, "nde-001", null, null, null, null, "I", "2026-05-11", "PROC", null, null, null, false, NdeReportTestResults.Passed, null, null, null, null, null, null, null));
        Assert.True(result.Success);
        Assert.NotNull(service.GetDraft("nde-001"));
        Assert.Equal(NdeReportStatuses.InProgress, log.GetLogItems().First(x => x.Id == "nde-001").ReportStatus);
    }

    [Fact]
    public void CompleteDraft_RequiresFields_AndDoesNotCreateFakeDownloadUrl()
    {
        var log = new DemoNdeLogReadModelService();
        var service = new DemoNdeReportDraftService(log);
        var invalid = service.CompleteDraft("nde-001", new NdeReportDraftSaveRequest(null, "nde-001", null, null, null, null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, null));
        Assert.False(invalid.Success);
        var completed = service.CompleteDraft("nde-001", new NdeReportDraftSaveRequest(null, "nde-001", null, null, null, null, "Tech", "2026-05-11", "PROC", null, null, null, false, NdeReportTestResults.Passed, null, null, null, null, null, null, null));
        Assert.True(completed.Success);
        Assert.Equal(NdeReportStatuses.Complete, log.GetLogItems().First(x => x.Id == "nde-001").ReportStatus);
        Assert.Null(completed.Draft!.ReportDownloadUrl);
    }
}
