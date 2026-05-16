using iE.Core.Reports.Domain;

namespace iE.Tests.Reports;

public class InspectionReportCalculationSnapshotTests
{
    [Fact]
    public void NewReport_InitializesCalculationsAsEmptyList()
    {
        var report = new InspectionReport();

        Assert.NotNull(report.Calculations);
        Assert.Empty(report.Calculations);
    }

    [Fact]
    public void ReportWithCalculations_PreservesSnapshotData()
    {
        var report = new InspectionReport();
        var snapshot = new InspectionCalculationSnapshot
        {
            Id = "calc-1",
            CalculationType = "corrosion-rate",
            DisplayName = "Corrosion Rate Foundation Helper",
            FormulaVersion = "api-570-foundation-v1",
            Inputs = "{\"initialThicknessInches\":0.5}",
            Outputs = "{\"corrosionRateInchesPerYear\":0.05}",
            InsertLabel = "Corrosion Rate (0.0500 in/yr)",
            StandardReferences = [new InspectionCalculationReference { Standard = "API 570", Note = "Validated" }],
            Warnings = [new InspectionCalculationWarning { Code = "USER_INPUT_VALIDATION_REQUIRED", Message = "Validate", Severity = "warning" }]
        };

        report.Calculations.Add(snapshot);

        Assert.Single(report.Calculations);
        Assert.Equal("corrosion-rate", report.Calculations[0].CalculationType);
        Assert.Equal("API 570", report.Calculations[0].StandardReferences[0].Standard);
        Assert.Equal("USER_INPUT_VALIDATION_REQUIRED", report.Calculations[0].Warnings[0].Code);
    }
}
