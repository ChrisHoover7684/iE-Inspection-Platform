using iE.Api.Auth;
using iE.Api.Workflow;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

public class NdeRequestWorkflowTests
{
    [Fact]
    public async Task BuiltIn_Nde_Types_Are_Accepted()
    {
        using var db = Db();
        var typeService = new NdeRequestTypeService(db);

        foreach (var code in new[]
                 {
                     "ut_thickness", "utt_grid", "utsw", "paut", "pt", "wfpt", "mt", "wfmt", "rt", "pmi", "hardness", "boroscope", "etc", "iris", "guided_wave_ut", "visual_followup", "other", "custom"
                 })
        {
            Assert.True(await typeService.IsValidTypeAsync("tenant-a", code));
        }
    }

    [Fact]
    public async Task Invalid_Nde_Type_Is_Denied()
    {
        using var db = Db();
        var svc = CreateService(db);

        var result = await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "invalid_nde_type", "normal", "reason");

        Assert.False(result.Success);
        Assert.Equal(WorkflowReasonCodes.InvalidNdeType, result.ReasonCode);
    }

    [Fact]
    public async Task Custom_Tenant_Type_Rules_Enforced()
    {
        using var db = Db();
        db.NdeRequestTypeDefinitions.AddRange(
            new NdeRequestTypeDefinition { ClientOrganizationId = "tenant-a", Code = "tenant_custom", DisplayName = "Tenant Custom", IsBuiltIn = false, IsActive = true },
            new NdeRequestTypeDefinition { ClientOrganizationId = "tenant-a", Code = "inactive_custom", DisplayName = "Inactive", IsBuiltIn = false, IsActive = false },
            new NdeRequestTypeDefinition { ClientOrganizationId = "tenant-b", Code = "other_tenant_only", DisplayName = "Other Tenant", IsBuiltIn = false, IsActive = true },
            new NdeRequestTypeDefinition { ClientOrganizationId = "tenant-a", Code = "shared_code", DisplayName = "Shared A", IsBuiltIn = false, IsActive = true },
            new NdeRequestTypeDefinition { ClientOrganizationId = "tenant-b", Code = "shared_code", DisplayName = "Shared B", IsBuiltIn = false, IsActive = true });
        await db.SaveChangesAsync();

        var svc = CreateService(db);

        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "tenant_custom", "normal", "reason")).Success);
        Assert.False((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "inactive_custom", "normal", "reason")).Success);
        Assert.False((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "other_tenant_only", "normal", "reason")).Success);
        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "shared_code", "normal", "reason")).Success);
        Assert.True((await svc.CreateDraftAsync("tenant-b", "facility-a", null, null, null, "shared_code", "normal", "reason")).Success);
        Assert.False((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, " ", "normal", "reason")).Success);
        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "paut", "normal", "reason")).Success);
    }

    [Fact]
    public async Task Draft_Creation_And_Bounds_Are_Enforced()
    {
        using var db = Db();
        SeedReferences(db);
        var svc = CreateService(db);

        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", "report-a", null, null, "pt", "normal", "reason")).Success);
        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", null, "asset-a", null, "pt", "normal", "reason")).Success);
        Assert.True((await svc.CreateDraftAsync("tenant-a", "facility-a", "report-a", "asset-a", null, "pt", "normal", "reason")).Success);

        Assert.False((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "pt", "bad", "reason")).Success);
        Assert.False((await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "pt", "normal", "")).Success);

        var longReason = new string('r', 600);
        var created = await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "pt", "normal", longReason);
        var saved = await db.NdeRequests.SingleAsync(x => x.Id == created.Id);
        Assert.Equal(256, saved.Reason.Length);

        var route = await svc.RequestAsync(created.Id!);
        Assert.True(route.Success);
        var inProgress = await svc.MarkInProgressAsync(created.Id!);
        Assert.True(inProgress.Success);

        var longSummary = new string('s', 1200);
        var results = await svc.RecordResultsReceivedAsync(created.Id!, longSummary);
        Assert.True(results.Success);
        saved = await db.NdeRequests.SingleAsync(x => x.Id == created.Id);
        Assert.Equal(1000, saved.ResultsSummary!.Length);

        var close = await svc.CloseAsync(created.Id!);
        Assert.True(close.Success);

        var cancelDraft = await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "pt", "normal", "cancel me");
        var canceled = await svc.CancelAsync(cancelDraft.Id!, new string('c', 900));
        Assert.True(canceled.Success);
        var canceledSaved = await db.NdeRequests.SingleAsync(x => x.Id == cancelDraft.Id);
        Assert.Equal(500, canceledSaved.CancellationReason!.Length);
    }

    [Fact]
    public async Task Status_Transitions_Valid_And_Invalid_Are_Enforced()
    {
        using var db = Db();
        var svc = CreateService(db);

        async Task<string> NewDraft()
        {
            var created = await svc.CreateDraftAsync("tenant-a", "facility-a", null, null, null, "pt", "normal", "reason");
            Assert.True(created.Success);
            return created.Id!;
        }

        var id = await NewDraft();
        Assert.True((await svc.RequestAsync(id)).Success);

        id = await NewDraft();
        await svc.RequestAsync(id);
        Assert.True((await svc.ScheduleAsync(id)).Success);

        id = await NewDraft();
        await svc.RequestAsync(id);
        Assert.True((await svc.MarkInProgressAsync(id)).Success);

        id = await NewDraft();
        await svc.RequestAsync(id);
        await svc.ScheduleAsync(id);
        Assert.True((await svc.MarkInProgressAsync(id)).Success);

        id = await NewDraft();
        await svc.RequestAsync(id);
        await svc.MarkInProgressAsync(id);
        Assert.True((await svc.RecordResultsReceivedAsync(id, "ok")).Success);

        id = await NewDraft();
        await svc.RequestAsync(id);
        await svc.MarkInProgressAsync(id);
        await svc.RecordResultsReceivedAsync(id, "ok");
        Assert.True((await svc.CloseAsync(id)).Success);

        var c1 = await NewDraft();
        Assert.True((await svc.CancelAsync(c1, "reason")).Success);
        var c2 = await NewDraft();
        await svc.RequestAsync(c2); Assert.True((await svc.CancelAsync(c2, "reason")).Success);
        var c3 = await NewDraft();
        await svc.RequestAsync(c3); await svc.ScheduleAsync(c3); Assert.True((await svc.CancelAsync(c3, "reason")).Success);
        var c4 = await NewDraft();
        await svc.RequestAsync(c4); await svc.MarkInProgressAsync(c4); Assert.True((await svc.CancelAsync(c4, "reason")).Success);

        var bad1 = await NewDraft();
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, (await svc.CloseAsync(bad1)).ReasonCode);
        var bad2 = await NewDraft();
        await svc.RequestAsync(bad2); await svc.MarkInProgressAsync(bad2); await svc.RecordResultsReceivedAsync(bad2, "ok"); await svc.CloseAsync(bad2);
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, (await svc.RequestAsync(bad2)).ReasonCode);
        var bad3 = await NewDraft();
        await svc.CancelAsync(bad3, "cancel");
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, (await svc.RequestAsync(bad3)).ReasonCode);
        var bad4 = await NewDraft();
        await svc.RequestAsync(bad4); await svc.MarkInProgressAsync(bad4); await svc.RecordResultsReceivedAsync(bad4, "ok");
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, (await svc.RequestAsync(bad4)).ReasonCode);
        var bad5 = await NewDraft();
        await svc.RequestAsync(bad5); await svc.ScheduleAsync(bad5);
        Assert.Equal(WorkflowReasonCodes.InvalidStatusTransition, (await svc.CloseAsync(bad5)).ReasonCode);
    }

    [Fact]
    public async Task Report_Log_And_Audit_Metadata_Are_Validated()
    {
        using var db = Db();
        SeedReferences(db);
        var audit = new RecordingAuditWriter();
        var svc = CreateService(db, audit);
        var reportLog = new ReportLogService(db, audit);

        var created = await svc.CreateDraftAsync("tenant-a", "facility-a", "report-a", null, null, "pt", "normal", "reason");
        await svc.RequestAsync(created.Id!);
        await svc.MarkInProgressAsync(created.Id!);
        await svc.RecordResultsReceivedAsync(created.Id!, "summary");

        var logs = await reportLog.QueryForReportAsync("tenant-a", "facility-a", "report-a");
        Assert.NotEmpty(logs);
        Assert.Contains(logs, x => x.EventType == ReportLogEventTypes.NdeRequestCreated);
        Assert.Contains(logs, x => x.EventType == ReportLogEventTypes.NdeRequestStatusChanged);
        Assert.Contains(logs, x => x.EventType == ReportLogEventTypes.NdeResultsReceived);
        Assert.True(logs.SequenceEqual(logs.OrderBy(x => x.CreatedAtUtc)));

        db.ReportLogEntries.Add(new ReportLogEntry { ClientOrganizationId = "tenant-a", FacilityId = "facility-a", ReportId = "report-b", EventType = "other", Message = "other" });
        await db.SaveChangesAsync();
        var filtered = await reportLog.QueryForReportAsync("tenant-a", "facility-a", "report-a");
        Assert.All(filtered, x => Assert.Equal("report-a", x.ReportId));

        var entry = logs.First();
        entry.Message = "mutate";
        Assert.Throws<InvalidOperationException>(() => db.SaveChanges());

        db.Entry(entry).State = EntityState.Unchanged;
        db.ReportLogEntries.Remove(entry);
        Assert.Throws<InvalidOperationException>(() => db.SaveChanges());

        var allow = new HashSet<string> { "operation", "reasonCode", "reportId", "ndeRequestId", "status", "eventType", "hasAsset", "hasProcessUnit" };
        var deny = new[] { "raw request body", "raw email", "tokens", "authorization headers", "provider payloads", "raw subscription", "raw plan", "metadataJson", "connection strings", "secrets" };

        Assert.NotEmpty(audit.Events);
        foreach (var evt in audit.Events)
        {
            if (evt.Metadata is null) continue;
            Assert.All(evt.Metadata.Keys, key => Assert.Contains(key, allow));
            foreach (var marker in deny)
            {
                Assert.DoesNotContain(evt.Metadata.Keys, key => key.Contains(marker, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    private static NdeRequestService CreateService(InspectionReportsDbContext db, IAuditEventWriter? audit = null)
    {
        audit ??= new RecordingAuditWriter();
        return new NdeRequestService(db, new ReportLogService(db, audit), audit, new NdeRequestTypeService(db));
    }

    private static InspectionReportsDbContext Db() => new(new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

    private static void SeedReferences(InspectionReportsDbContext db)
    {
        db.Assets.Add(new Asset { Id = "asset-a", FacilityId = "facility-a", EquipmentTag = "tag", EquipmentType = "type", Service = "svc" });
        db.InspectionReports.Add(new InspectionReport { Id = "report-a", ClientOrganizationId = "tenant-a", FacilityId = "facility-a", ReportNumber = "R-1", ReportType = ReportType.Internal, InspectionDate = DateTime.UtcNow, InspectorName = "inspector", AssetId = "asset-a" });
        db.SaveChanges();
    }

    private sealed class RecordingAuditWriter : IAuditEventWriter
    {
        public List<(string Action, IDictionary<string, object?>? Metadata)> Events { get; } = [];

        public Task WriteAsync(string action, string resourceType, string? resourceId, string result, string? facilityId = null, IDictionary<string, object?>? metadata = null, CancellationToken cancellationToken = default)
        {
            Events.Add((action, metadata));
            return Task.CompletedTask;
        }
    }
}
