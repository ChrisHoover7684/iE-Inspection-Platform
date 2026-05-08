using iE.Api.Auth;
using iE.Api.Workflow;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

public class NdeRequestWorkflowTests
{
    [Fact]
    public async Task Invalid_Transition_Denied()
    {
        var db = Db();
        var typeService = new NdeRequestTypeService(db);
        var svc = new NdeRequestService(db, new ReportLogService(db, new NullAuditWriter()), new NullAuditWriter(), typeService);
        var created = await svc.CreateDraftAsync("t1", "f1", null, null, null, "pt", "normal", "reason");
        var res = await svc.CloseAsync(created.Id!);
        Assert.False(res.Success);
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, res.ReasonCode);
    }

    [Fact]
    public async Task Custom_Active_Type_Allowed_In_Tenant()
    {
        var db = Db();
        db.NdeRequestTypeDefinitions.Add(new NdeRequestTypeDefinition { ClientOrganizationId = "t1", Code = "my_custom", DisplayName = "My Custom", IsBuiltIn = false, IsActive = true });
        db.SaveChanges();
        var svc = new NdeRequestService(db, new ReportLogService(db, new NullAuditWriter()), new NullAuditWriter(), new NdeRequestTypeService(db));
        var res = await svc.CreateDraftAsync("t1", "f1", null, null, null, "my_custom", "normal", "reason");
        Assert.True(res.Success);
    }

    private static InspectionReportsDbContext Db() => new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    private sealed class NullAuditWriter : IAuditEventWriter
    {
        public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
