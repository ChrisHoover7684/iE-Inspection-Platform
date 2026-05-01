namespace iE.Api.Contracts;

public class ImproveTextRequest
{
    public string Text { get; set; } = string.Empty;
    public string? Context { get; set; }
}
