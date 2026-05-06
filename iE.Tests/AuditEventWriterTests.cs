using System.Text.Json;
using iE.Api.Auth;
using iE.Api.Tenancy;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace iE.Tests;

public class AuditEventWriterTests
{
    [Fact]
    public async Task Writes_Audit_Row_With_Context()
    {
        var db = BuildDb();
        var accessor = new TenantContextAccessor
        {
            Current = new TenantContext
            {
                IsAuthenticated = true,
                ExternalSubject = "user-1",
                ClientOrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Capabilities = []
            }
        };
        var http = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
        http.HttpContext.TraceIdentifier = "trace-123";
        http.HttpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("127.0.0.1");
        http.HttpContext.Request.Headers.UserAgent = "test-agent";
        var writer = new AuditEventWriter(db, accessor, http, NullLogger<AuditEventWriter>.Instance);

        await writer.WriteAsync("ReportCreated", "Report", "r1", "Success", "facility-a", new Dictionary<string, object?> { ["route"] = "CreateInstance" });

        var evt = db.AuditEvents.Single();
        Assert.Equal("ReportCreated", evt.Action);
        Assert.Equal("trace-123", evt.CorrelationId);
        Assert.Equal("user-1", evt.ActorUserId);
        Assert.Contains("route", evt.MetadataJson);
    }

    [Fact]
    public async Task Sanitizer_Removes_Sensitive_Keys()
    {
        var db = BuildDb();
        var writer = new AuditEventWriter(db, new TenantContextAccessor(), new HttpContextAccessor(), NullLogger<AuditEventWriter>.Instance);
        await writer.WriteAsync("A", "R", "1", "Success", metadata: new Dictionary<string, object?> { ["token"] = "x", ["markupJson"] = "{}", ["ok"] = true });
        var json = db.AuditEvents.Single().MetadataJson!;
        Assert.DoesNotContain("token", json, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("markupJson", json, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ok", json);
    }

    private static InspectionReportsDbContext BuildDb()
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        return new InspectionReportsDbContext(options);
    }
}
