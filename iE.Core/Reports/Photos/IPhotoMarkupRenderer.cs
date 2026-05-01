namespace iE.Core.Reports.Photos;

public interface IPhotoMarkupRenderer
{
    Task<byte[]?> RenderAsync(byte[] originalImage, string markupJson, CancellationToken cancellationToken = default);
}
