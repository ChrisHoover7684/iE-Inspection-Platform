namespace iE.Core.Reports.Photos;

public class PhotoDetectionService : IPhotoDetectionService
{
    public PhotoDetectionResult Detect(PhotoDetectionRequest request)
    {
        // Scaffold implementation:
        // This service classifies photo context from text metadata only (name/url/description/checklist fields).
        // It does NOT perform image-content analysis or call any AI/computer-vision model.
        var result = new PhotoDetectionResult
        {
            PhotoId = request.PhotoId
        };

        var searchableText = string.Join(' ',
            request.FileName,
            request.FileUrl,
            request.Description,
            request.RelatedComponent,
            request.RelatedChecklistItem).ToLowerInvariant();

        AddIfMatch(result.Detections, searchableText, new[] { "corrosion", "corroded", "rust" }, PhotoDetectionType.Corrosion, "Metadata indicates corrosion-related condition.");
        AddIfMatch(result.Detections, searchableText, new[] { "leak", "stain", "staining", "wet" }, PhotoDetectionType.LeakStaining, "Metadata indicates potential leak or staining.");
        AddIfMatch(result.Detections, searchableText, new[] { "cui" }, PhotoDetectionType.CuiIndicator, "Metadata explicitly references CUI.");
        AddIfMatch(result.Detections, searchableText, new[] { "coating", "paint", "coated" }, PhotoDetectionType.CoatingDamage, "Metadata indicates coating damage context.");
        AddIfMatch(result.Detections, searchableText, new[] { "damage", "dent", "deformed" }, PhotoDetectionType.MechanicalDamage, "Metadata indicates potential mechanical damage.");
        AddIfMatch(result.Detections, searchableText, new[] { "bolting", "bolt missing", "missing bolt", "no bolt" }, PhotoDetectionType.MissingBolting, "Metadata indicates missing bolting.");
        AddIfMatch(result.Detections, searchableText, new[] { "insulation", "lagging" }, PhotoDetectionType.InsulationDamage, "Metadata indicates insulation-related issue.");

        return result;
    }

    private static void AddIfMatch(
        ICollection<PhotoDetection> detections,
        string searchableText,
        IEnumerable<string> keywords,
        PhotoDetectionType type,
        string rationale)
    {
        // Keyword matching is intentionally simple to keep this as a deterministic metadata scaffold.
        if (!keywords.Any(searchableText.Contains))
        {
            return;
        }

        detections.Add(new PhotoDetection
        {
            Type = type,
            Severity = PhotoDetectionSeverity.Low,
            Confidence = 0.35m,
            Rationale = rationale
        });
    }
}
