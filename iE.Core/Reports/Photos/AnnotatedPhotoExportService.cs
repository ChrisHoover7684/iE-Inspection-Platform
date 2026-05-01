using iE.Core.Reports.Models;

namespace iE.Core.Reports.Photos;

public class AnnotatedPhotoExportService(IPhotoMarkupRenderer photoMarkupRenderer)
{
    public async Task<IReadOnlyList<AnnotatedPhotoExport>> BuildAsync(
        InspectionReport report,
        IReadOnlyCollection<PhotoMarkup> markups,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(report);
        ArgumentNullException.ThrowIfNull(markups);

        var exports = new List<AnnotatedPhotoExport>(report.Photos.Count);

        foreach (var photo in report.Photos)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var latestMarkup = markups
                .Where(markup => string.Equals(markup.PhotoId, photo.Id, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(markup => markup.CreatedAt)
                .FirstOrDefault();

            var export = new AnnotatedPhotoExport
            {
                PhotoId = photo.Id,
                OriginalFileUrl = photo.FileUrl ?? string.Empty,
                Caption = photo.Description,
                MarkupJson = latestMarkup?.MarkupJson ?? string.Empty
            };

            var originalBytes = TryResolvePhotoBytes(photo);
            if (originalBytes is not null)
            {
                export.RenderedBytes = await photoMarkupRenderer
                    .RenderAsync(originalBytes, export.MarkupJson, cancellationToken)
                    .ConfigureAwait(false);
            }

            exports.Add(export);
        }

        return exports;
    }

    private static byte[]? TryResolvePhotoBytes(InspectionPhoto photo)
    {
        if (!photo.PhotoAttached)
        {
            return null;
        }

        foreach (var candidate in new[] { photo.FileUrl, photo.FileName })
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                continue;
            }

            var trimmed = candidate.Trim();

            if (trimmed.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                var commaIndex = trimmed.IndexOf(',');
                if (commaIndex > -1 && commaIndex < trimmed.Length - 1)
                {
                    trimmed = trimmed[(commaIndex + 1)..];
                }
            }

            try
            {
                return Convert.FromBase64String(trimmed);
            }
            catch (FormatException)
            {
                // Ignore invalid candidate and try the next value.
            }
        }

        return null;
    }
}
