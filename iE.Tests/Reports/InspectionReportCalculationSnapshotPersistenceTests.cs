using System.Text.Json;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Persistence;
using Microsoft.EntityFrameworkCore;

namespace iE.Tests.Reports;

public class InspectionReportCalculationSnapshotPersistenceTests
{
    [Fact]
    public void DeserializeReport_WithSerializedCalculationInputsOutputs_BindsAsStrings()
    {
        const string json = """
        {
          "id": "r-1",
          "clientOrganizationId": "client-demo-refining",
          "facilityId": "facility-demo-gulf-coast",
          "templateId": "api-570-piping-external",
          "status": "Draft",
          "createdAt": "2026-05-16T00:00:00Z",
          "sections": [],
          "findings": [],
          "photos": [],
          "calculations": [
            {
              "id": "calc-1",
              "calculationType": "corrosion-rate",
              "displayName": "Corrosion Rate Foundation Helper",
              "formulaVersion": "api-570-foundation-v1",
              "standardReferences": [],
              "inputs": "{\"initialThicknessInches\":0.5}",
              "outputs": "{\"corrosionRateInchesPerYear\":0.05}",
              "warnings": [{ "code": "W1", "message": "msg", "severity": "warning" }],
              "calculatedAt": "2026-05-16T00:00:00Z",
              "insertLabel": "Corrosion Rate"
            }
          ]
        }
        """;

        var report = JsonSerializer.Deserialize<InspectionReport>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        Assert.NotNull(report);
        Assert.Single(report!.Calculations);
        Assert.Contains("initialThicknessInches", report.Calculations[0].Inputs);
    }

    [Fact]
    public void RepositoryUpdate_RoundTripsCalculationSnapshotWithWarningsAndJsonPayload()
    {
        var options = new DbContextOptionsBuilder<InspectionReportsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        using var db = new InspectionReportsDbContext(options);
        var repo = new InspectionReportRepository(db);

        var report = new InspectionReport
        {
            Id = "report-1",
            ClientOrganizationId = "client-demo-refining",
            FacilityId = "facility-demo-gulf-coast",
            TemplateId = "api-570-piping-external",
            Status = "Draft",
            CreatedAt = DateTime.UtcNow
        };

        repo.Create(report);

        report.Calculations.Add(new InspectionCalculationSnapshot
        {
            Id = "calc-1",
            CalculationType = "corrosion-rate",
            DisplayName = "Corrosion Rate Foundation Helper",
            FormulaVersion = "api-570-foundation-v1",
            Inputs = "{\"initialThicknessInches\":0.5}",
            Outputs = "{\"corrosionRateInchesPerYear\":0.05}",
            InsertLabel = "Corrosion Rate",
            CalculatedAt = DateTime.UtcNow,
            StandardReferences = [new InspectionCalculationReference { Standard = "API 570", Paragraph = "7.1" }],
            Warnings = [new InspectionCalculationWarning { Code = "W1", Message = "Validate", Severity = "warning" }]
        });

        repo.Update(report.Id, report);

        var loaded = repo.GetById(report.Id);
        Assert.NotNull(loaded);
        Assert.Single(loaded!.Calculations);
        Assert.Equal("{\"initialThicknessInches\":0.5}", loaded.Calculations[0].Inputs);
        Assert.Equal("W1", loaded.Calculations[0].Warnings[0].Code);
    }
}
