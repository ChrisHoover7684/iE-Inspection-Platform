namespace iE.Core.Reports.Rules;

public class UiAlert
{
    public UiAlertSeverity Severity { get; set; } = UiAlertSeverity.Info;
    public string Message { get; set; } = string.Empty;
    public string? SourceFieldId { get; set; }
    public string? SourceFindingId { get; set; }
    public bool RequiresAction { get; set; }
}

public enum UiAlertSeverity
{
    Info = 1,
    Warning = 2,
    Error = 3
}
