using System.Text;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace iE.Core.Reports.Services;

public class PhotoAppendixExportService
{
    private const string PhotoBinaryTag = "{{PhotoBinary}}";
    private const long EmusPerInch = 914400;
    private static readonly long MaxImageWidthEmus = (long)(3.0 * EmusPerInch);
    private static readonly long MaxImageHeightEmus = (long)(2.2d * EmusPerInch);

    public byte[] Export(InspectionReport report)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Reports", "Templates", "API570_External_PhotoAppendix.docx");
        if (!File.Exists(templatePath))
        {
            throw new InvalidOperationException(
                $"Photo appendix template file was not found at '{templatePath}'. Ensure 'API570_External_PhotoAppendix.docx' exists in Reports/Templates.");
        }

        using var templateStream = File.OpenRead(templatePath);
        using var outputStream = new MemoryStream();
        templateStream.CopyTo(outputStream);
        outputStream.Position = 0;

        using var document = WordprocessingDocument.Open(outputStream, true);
        var mainPart = document.MainDocumentPart
            ?? throw new InvalidOperationException("Invalid DOCX template: missing main document part.");

        var body = mainPart.Document.Body
            ?? throw new InvalidOperationException("Invalid DOCX template: missing document body.");

        var templateRow = body.Descendants<TableRow>()
            .FirstOrDefault(r => r.InnerText.Contains(PhotoBinaryTag, StringComparison.Ordinal));

        if (templateRow is null)
        {
            throw new InvalidOperationException(
                $"Template row containing '{PhotoBinaryTag}' was not found in API570_External_PhotoAppendix.docx.");
        }

        if (report.Photos.Count == 0)
        {
            ReplaceTextTags(templateRow, BuildEmptyPhotoTagMap());
            mainPart.Document.Save();
            return outputStream.ToArray();
        }

        foreach (var photo in SortPhotos(report.Photos))
        {
            var clonedRow = (TableRow)templateRow.CloneNode(true);
            ReplaceTextTags(clonedRow, BuildPhotoTagMap(report, photo));
            InsertOrReplacePhoto(mainPart, clonedRow, photo);
            templateRow.Parent?.InsertBefore(clonedRow, templateRow);
        }

        templateRow.Remove();
        mainPart.Document.Save();

        return outputStream.ToArray();
    }

    private static Dictionary<string, string> BuildPhotoTagMap(InspectionReport report, InspectionPhoto photo)
    {
        var findingType = ResolveFindingType(report, photo);
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["{{ReportNumber}}"] = Clean(report.ReportNumber),
            ["{{EquipmentTag}}"] = Clean(report.EquipmentTag),
            ["{{PhotoId}}"] = Clean(photo.Id),
            ["{{PhotoNumber}}"] = Clean(photo.PhotoNumber),
            ["{{ProfileId}}"] = Clean(photo.RelatedChecklistItem),
            ["{{ComponentLocation}}"] = Clean(photo.RelatedComponent),
            ["{{FindingType}}"] = Clean(findingType),
            ["{{Description}}"] = Clean(photo.Description),
            ["{{PhotoDescription}}"] = Clean(photo.Description),
            ["{{RelatedComponent}}"] = Clean(photo.RelatedComponent),
            ["{{RelatedChecklistItem}}"] = Clean(photo.RelatedChecklistItem),
            ["{{PhotoAttached}}"] = photo.PhotoAttached ? "Yes" : "No",
            ["{{FileName}}"] = Clean(photo.FileName),
            ["{{FileUrl}}"] = Clean(photo.FileUrl),
            [PhotoBinaryTag] = string.Empty
        };
    }

    private static Dictionary<string, string> BuildEmptyPhotoTagMap()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [PhotoBinaryTag] = "[No photos attached]",
            ["{{PhotoNumber}}"] = string.Empty,
            ["{{ProfileId}}"] = string.Empty,
            ["{{ComponentLocation}}"] = string.Empty,
            ["{{FindingType}}"] = string.Empty,
            ["{{Description}}"] = "No photos attached.",
            ["{{PhotoDescription}}"] = string.Empty,
            ["{{RelatedComponent}}"] = string.Empty,
            ["{{RelatedChecklistItem}}"] = string.Empty
        };
    }

    private static string ResolveFindingType(InspectionReport report, InspectionPhoto photo)
    {
        var linkedFinding = report.Findings.FirstOrDefault(f =>
            f.PhotoIds.Any(photoId => string.Equals(photoId, photo.Id, StringComparison.OrdinalIgnoreCase)));

        if (!string.IsNullOrWhiteSpace(linkedFinding?.FindingType))
        {
            return linkedFinding.FindingType;
        }

        var checklistFinding = report.Findings.FirstOrDefault(f =>
            string.Equals(f.AssociatedChecklistItem, photo.RelatedChecklistItem, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(checklistFinding?.FindingType))
        {
            return checklistFinding.FindingType;
        }

        return string.Empty;
    }

    private static IEnumerable<InspectionPhoto> SortPhotos(IEnumerable<InspectionPhoto> photos)
    {
        return photos.OrderBy(photo => TryParsePhotoNumber(photo.PhotoNumber, out _) ? 0 : 1)
            .ThenBy(photo => TryParsePhotoNumber(photo.PhotoNumber, out var number) ? number : int.MaxValue)
            .ThenBy(photo => Clean(photo.PhotoNumber), StringComparer.OrdinalIgnoreCase);
    }

    private static bool TryParsePhotoNumber(string? value, out int number)
    {
        return int.TryParse(Clean(value), NumberStyles.Integer, CultureInfo.InvariantCulture, out number);
    }

    private static void ReplaceTextTags(OpenXmlElement scope, IReadOnlyDictionary<string, string> tagMap)
    {
        foreach (var textNode in scope.Descendants<Text>())
        {
            var updated = textNode.Text;
            foreach (var (tag, value) in tagMap)
            {
                updated = updated.Replace(tag, value, StringComparison.OrdinalIgnoreCase);
            }

            textNode.Text = updated;
        }
    }

    private static void InsertOrReplacePhoto(MainDocumentPart mainPart, TableRow row, InspectionPhoto photo)
    {
        var targetCell = row.Descendants<TableCell>()
            .FirstOrDefault(c => c.InnerText.Contains(PhotoBinaryTag, StringComparison.Ordinal));

        if (targetCell is null)
        {
            return;
        }

        foreach (var paragraph in targetCell.Elements<Paragraph>().ToList())
        {
            paragraph.Remove();
        }

        var imageBytes = TryResolvePhotoBytes(photo);
        if (imageBytes is null)
        {
            targetCell.AppendChild(new Paragraph(new Run(new Text("Photo not available"))));
            return;
        }

        var imageType = DetectImagePartType(imageBytes, out var extension);
        if (imageType is null)
        {
            targetCell.AppendChild(new Paragraph(new Run(new Text("Unsupported image format"))));
            return;
        }

        var imagePart = mainPart.AddImagePart(imageType.Value);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            imagePart.FeedData(imageStream);
        }

        var relationshipId = mainPart.GetIdOfPart(imagePart);

        var (pixelWidth, pixelHeight) = TryReadImageSize(imageBytes);
        var (cx, cy) = ResizeToFit(pixelWidth, pixelHeight, MaxImageWidthEmus, MaxImageHeightEmus);

        var drawing = BuildDrawing(relationshipId, cx, cy, extension);
        targetCell.AppendChild(new Paragraph(new Run(drawing)));
    }

    private static byte[]? TryResolvePhotoBytes(InspectionPhoto photo)
    {
        if (!photo.PhotoAttached)
        {
            return null;
        }

        foreach (var candidate in new[] { photo.FileUrl, photo.FileName })
        {
            var bytes = TryParseImageCandidate(candidate);
            if (bytes is not null)
            {
                return bytes;
            }
        }

        return null;
    }

    private static byte[]? TryParseImageCandidate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var candidate = value.Trim();

        if (candidate.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            var commaIndex = candidate.IndexOf(',');
            if (commaIndex > -1 && commaIndex < candidate.Length - 1)
            {
                var payload = candidate[(commaIndex + 1)..];
                if (TryDecodeBase64(payload, out var dataUriBytes))
                {
                    return dataUriBytes;
                }
            }
        }

        if (TryDecodeBase64(candidate, out var rawBase64Bytes))
        {
            return rawBase64Bytes;
        }

        if (File.Exists(candidate))
        {
            return File.ReadAllBytes(candidate);
        }

        var appBasePath = Path.Combine(AppContext.BaseDirectory, candidate);
        if (File.Exists(appBasePath))
        {
            return File.ReadAllBytes(appBasePath);
        }

        return null;
    }

    private static bool TryDecodeBase64(string input, out byte[] bytes)
    {
        bytes = Array.Empty<byte>();
        try
        {
            bytes = Convert.FromBase64String(input);
            return bytes.Length > 0;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static ImagePartType? DetectImagePartType(byte[] bytes, out string extension)
    {
        extension = "bin";
        if (bytes.Length >= 8 && bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
        {
            extension = "png";
            return ImagePartType.Png;
        }

        if (bytes.Length >= 3 && bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
        {
            extension = "jpg";
            return ImagePartType.Jpeg;
        }

        if (bytes.Length >= 6)
        {
            var header = Encoding.ASCII.GetString(bytes, 0, 6);
            if (header is "GIF87a" or "GIF89a")
            {
                extension = "gif";
                return ImagePartType.Gif;
            }
        }

        if (bytes.Length >= 2 && bytes[0] == 0x42 && bytes[1] == 0x4D)
        {
            extension = "bmp";
            return ImagePartType.Bmp;
        }

        return null;
    }

    private static (int width, int height) TryReadImageSize(byte[] bytes)
    {
        if (TryReadPngSize(bytes, out var pngSize))
        {
            return pngSize;
        }

        if (TryReadJpegSize(bytes, out var jpegSize))
        {
            return jpegSize;
        }

        if (TryReadGifSize(bytes, out var gifSize))
        {
            return gifSize;
        }

        return (1024, 768);
    }

    private static bool TryReadPngSize(byte[] data, out (int width, int height) size)
    {
        size = default;
        if (data.Length < 24 || data[0] != 0x89 || data[1] != 0x50 || data[2] != 0x4E || data[3] != 0x47)
        {
            return false;
        }

        var width = ReadInt32BigEndian(data, 16);
        var height = ReadInt32BigEndian(data, 20);
        if (width <= 0 || height <= 0)
        {
            return false;
        }

        size = (width, height);
        return true;
    }

    private static bool TryReadGifSize(byte[] data, out (int width, int height) size)
    {
        size = default;
        if (data.Length < 10)
        {
            return false;
        }

        var sig = Encoding.ASCII.GetString(data, 0, 6);
        if (sig is not ("GIF87a" or "GIF89a"))
        {
            return false;
        }

        var width = data[6] | (data[7] << 8);
        var height = data[8] | (data[9] << 8);
        if (width <= 0 || height <= 0)
        {
            return false;
        }

        size = (width, height);
        return true;
    }

    private static bool TryReadJpegSize(byte[] data, out (int width, int height) size)
    {
        size = default;
        if (data.Length < 4 || data[0] != 0xFF || data[1] != 0xD8)
        {
            return false;
        }

        var i = 2;
        while (i + 8 < data.Length)
        {
            if (data[i] != 0xFF)
            {
                i++;
                continue;
            }

            var marker = data[i + 1];
            i += 2;

            if (marker == 0xD8 || marker == 0xD9)
            {
                continue;
            }

            if (i + 1 >= data.Length)
            {
                return false;
            }

            var length = (data[i] << 8) + data[i + 1];
            if (length < 2 || i + length > data.Length)
            {
                return false;
            }

            var isSofMarker = marker is >= 0xC0 and <= 0xC3 or >= 0xC5 and <= 0xC7 or >= 0xC9 and <= 0xCB or >= 0xCD and <= 0xCF;
            if (isSofMarker)
            {
                if (i + 7 >= data.Length)
                {
                    return false;
                }

                var height = (data[i + 3] << 8) + data[i + 4];
                var width = (data[i + 5] << 8) + data[i + 6];
                if (width <= 0 || height <= 0)
                {
                    return false;
                }

                size = (width, height);
                return true;
            }

            i += length;
        }

        return false;
    }

    private static int ReadInt32BigEndian(byte[] bytes, int offset)
    {
        return (bytes[offset] << 24)
               | (bytes[offset + 1] << 16)
               | (bytes[offset + 2] << 8)
               | bytes[offset + 3];
    }

    private static (long cx, long cy) ResizeToFit(int widthPx, int heightPx, long maxCx, long maxCy)
    {
        if (widthPx <= 0 || heightPx <= 0)
        {
            return (maxCx, maxCy);
        }

        var widthEmus = widthPx * 9525L;
        var heightEmus = heightPx * 9525L;

        var widthScale = (double)maxCx / widthEmus;
        var heightScale = (double)maxCy / heightEmus;
        var scale = Math.Min(1d, Math.Min(widthScale, heightScale));

        var resizedCx = Math.Max(1L, (long)Math.Round(widthEmus * scale, MidpointRounding.AwayFromZero));
        var resizedCy = Math.Max(1L, (long)Math.Round(heightEmus * scale, MidpointRounding.AwayFromZero));

        return (resizedCx, resizedCy);
    }

    private static Drawing BuildDrawing(string relationshipId, long cx, long cy, string extension)
    {
        var elementId = (uint)Random.Shared.Next(1, int.MaxValue);
        var elementName = $"Photo_{Guid.NewGuid():N}.{extension}";

        return new Drawing(
            new DW.Inline(
                new DW.Extent { Cx = cx, Cy = cy },
                new DW.EffectExtent
                {
                    LeftEdge = 0L,
                    TopEdge = 0L,
                    RightEdge = 0L,
                    BottomEdge = 0L
                },
                new DW.DocProperties { Id = elementId, Name = elementName },
                new DW.NonVisualGraphicFrameDrawingProperties(
                    new GraphicFrameLocks { NoChangeAspect = true }),
                new Graphic(
                    new GraphicData(
                        new PIC.Picture(
                            new PIC.NonVisualPictureProperties(
                                new PIC.NonVisualDrawingProperties { Id = elementId, Name = elementName },
                                new PIC.NonVisualPictureDrawingProperties()),
                            new PIC.BlipFill(
                                new Blip { Embed = relationshipId },
                                new Stretch(new FillRectangle())),
                            new PIC.ShapeProperties(
                                new Transform2D(
                                    new Offset { X = 0L, Y = 0L },
                                    new Extents { Cx = cx, Cy = cy }),
                                new PresetGeometry(new AdjustValueList()) { Preset = ShapeTypeValues.Rectangle })))
                    { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
            {
                DistanceFromTop = 0U,
                DistanceFromBottom = 0U,
                DistanceFromLeft = 0U,
                DistanceFromRight = 0U
            });
    }

    private static string Clean(string? value) =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
}
