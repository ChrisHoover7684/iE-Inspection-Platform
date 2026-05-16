namespace iE.Core.Reports.Domain;

public class InspectionCalculationSnapshot
{
    public string Id { get; set; } = string.Empty;
    public string CalculationType { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string FormulaVersion { get; set; } = string.Empty;
    public List<InspectionCalculationReference> StandardReferences { get; set; } = new();
    public string Inputs { get; set; } = "{}";
    public string Outputs { get; set; } = "{}";
    public List<InspectionCalculationWarning> Warnings { get; set; } = new();
    public DateTime CalculatedAt { get; set; }
    public string InsertLabel { get; set; } = string.Empty;
    public string? LinkedSectionId { get; set; }
    public string? LinkedFieldId { get; set; }
    public string? LinkedFindingId { get; set; }
}

public class InspectionCalculationReference
{
    public string Standard { get; set; } = string.Empty;
    public string? Edition { get; set; }
    public string? Paragraph { get; set; }
    public string? Note { get; set; }
}

public class InspectionCalculationWarning
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
}
