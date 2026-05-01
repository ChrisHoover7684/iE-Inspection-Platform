namespace iE.Core.Reports.Models;

public class AnnotatedPhotoExport
{
    public string PhotoId { get; set; } = string.Empty;
    public string OriginalFileUrl { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public string MarkupJson { get; set; } = string.Empty;
    public string? RenderedFilePath { get; set; }
    public byte[]? RenderedBytes { get; set; }
}
