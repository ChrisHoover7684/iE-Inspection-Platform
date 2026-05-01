namespace iE.Core.Reports.Services;

public interface IPhotoMarkupRenderer
{
    Task<byte[]?> RenderAsync(byte[] originalImage, string markupJson, CancellationToken cancellationToken = default);
}
