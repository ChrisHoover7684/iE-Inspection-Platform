using iE.Api.Workflow;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

public class NdeRequestWorkflowTests
{
    [Fact]
    public async Task Invalid_Transition_Denied()
    {
        var db = new InspectionReportsDbContext(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        var log = new ReportLogService(db, new NullAuditWriter());
        var svc = new NdeRequestService(db, log, new NullAuditWriter());
        var created = await svc.CreateDraftAsync("t1", "f1", "r1", null, null, "pt", "normal", "reason");
        var res = await svc.CloseAsync(created.Id!);
        Assert.False(res.Success);
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, res.ReasonCode);
    }

    private sealed class NullAuditWriter : iE.Api.Auth.IAuditEventWriter
    {
        public Task WriteAsync(iE.Api.Auth.AuditEventWriteRequest request, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
