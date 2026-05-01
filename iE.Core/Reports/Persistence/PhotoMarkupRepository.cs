using Microsoft.EntityFrameworkCore;

namespace iE.Core.Reports.Persistence;

public class PhotoMarkupRepository(InspectionReportsDbContext dbContext)
{
    private static readonly HashSet<string> AllowedShapeTypes =
    [
        "arrow",
        "circle",
        "rectangle",
        "text"
    ];

    public PhotoMarkup Create(string photoId, string markupJson)
    {
        ValidateMarkup(markupJson);

        var markup = new PhotoMarkup
        {
            Id = Guid.NewGuid().ToString("N"),
            PhotoId = photoId,
            MarkupJson = markupJson,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.PhotoMarkups.Add(markup);
        dbContext.SaveChanges();
        return markup;
    }

    public List<PhotoMarkup> GetByPhotoId(string photoId)
    {
        return dbContext.PhotoMarkups
            .AsNoTracking()
            .Where(pm => pm.PhotoId == photoId)
            .OrderBy(pm => pm.CreatedAt)
            .ToList();
    }

    private static void ValidateMarkup(string markupJson)
    {
        if (string.IsNullOrWhiteSpace(markupJson))
        {
            throw new ArgumentException("MarkupJson is required.");
        }

        using var doc = System.Text.Json.JsonDocument.Parse(markupJson);
        var root = doc.RootElement;
        if (root.ValueKind != System.Text.Json.JsonValueKind.Object)
        {
            throw new ArgumentException("MarkupJson must be a JSON object.");
        }

        if (!root.TryGetProperty("annotations", out var annotations)
            || annotations.ValueKind != System.Text.Json.JsonValueKind.Array)
        {
            throw new ArgumentException("MarkupJson must contain an 'annotations' array.");
        }

        foreach (var annotation in annotations.EnumerateArray())
        {
            if (!annotation.TryGetProperty("type", out var typeElement)
                || typeElement.ValueKind != System.Text.Json.JsonValueKind.String)
            {
                throw new ArgumentException("Each annotation must include a string 'type'.");
            }

            var type = typeElement.GetString();
            if (string.IsNullOrWhiteSpace(type) || !AllowedShapeTypes.Contains(type))
            {
                throw new ArgumentException("Annotation type must be one of: arrow, circle, rectangle, text.");
            }
        }
    }
}
