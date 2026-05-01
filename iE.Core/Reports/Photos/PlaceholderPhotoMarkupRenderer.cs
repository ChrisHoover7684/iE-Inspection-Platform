namespace iE.Core.Reports.Photos;

public class PlaceholderPhotoMarkupRenderer : IPhotoMarkupRenderer
{
    public Task<byte[]?> RenderAsync(byte[] originalImage, string markupJson, CancellationToken cancellationToken = default)
    {
        // TODO: Replace this placeholder implementation with an image rendering engine
        // that applies arrows/circles/rectangles/text from markupJson onto a temporary
        // export copy without mutating the original source photo.
        return Task.FromResult<byte[]?>(originalImage.ToArray());
    }
}
