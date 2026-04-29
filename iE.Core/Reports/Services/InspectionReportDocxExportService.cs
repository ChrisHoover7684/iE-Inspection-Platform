using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using W = DocumentFormat.OpenXml.Wordprocessing;

namespace iE.Core.Reports.Services;

public class InspectionReportDocxExportService(InspectionSummaryService inspectionSummaryService)
{
    public byte[] Export(InspectionReport report)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, "Reports", "Templates", "API570_External_Main.docx");
        if (!File.Exists(templatePath))
        {
            throw new InvalidOperationException(
                $"Main report template file was not found at '{templatePath}'. Ensure 'API570_External_Main.docx' exists in Reports/Templates.");
        }

        using var templateStream = File.OpenRead(templatePath);
        using var outputStream = new MemoryStream();
        templateStream.CopyTo(outputStream);
        outputStream.Position = 0;
        byte[] result;

        using (var document = WordprocessingDocument.Open(outputStream, true))
        {
            var mainPart = document.MainDocumentPart
                ?? throw new InvalidOperationException("Invalid DOCX template: missing main document part.");

            var body = mainPart.Document.Body
                ?? throw new InvalidOperationException("Invalid DOCX template: missing document body.");

            ReplaceTextTags(body, BuildTagMap(report));
            mainPart.Document.Save();
        }

        result = outputStream.ToArray();
        return result;
    }

    private static readonly IReadOnlyDictionary<string, string> ChecklistFieldToTemplatePrefixMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Known mapping from API 570 report template seed data.
            ["pipe-cracks-or-corrosion"] = "C1"

            // TODO: Add remaining API 570 external checklist mappings (A1-A9, B1-B5, C2-C7, D1-D5, E1-E3, F1-F12, G1-G7, H1-H8, I1-I6)
            // when their exact fieldId-to-tag assignments are confirmed from template/seed data.
        };

    private Dictionary<string, string> BuildTagMap(InspectionReport report)
    {
        var summary = inspectionSummaryService.Build(report);
        var clientTagValue = Clean(report.ClientOrganizationId);
        var facilityTagValue = Clean(report.FacilityId);

        var tagMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["{{Client}}"] = clientTagValue,
            ["{{Facility}}"] = facilityTagValue,
            ["{{SystemID}}"] = Clean(report.SystemId),
            ["{{ReportID}}"] = Clean(report.Id),
            ["{{InspectionDate}}"] = FormatDate(report.CreatedAt),
            ["{{Insp_Name}}"] = Clean(report.CreatedByUserId),
            ["{{Reviewer_Name}}"] = Clean(report.UpdatedByUserId),
            ["{{ReportNumber}}"] = Clean(report.ReportNumber),
            ["{{EquipmentTag}}"] = Clean(report.EquipmentTag),
            ["{{Unit}}"] = Clean(report.Unit),
            ["{{SystemId}}"] = Clean(report.SystemId),
            ["{{Circuit}}"] = Clean(report.CircuitId),
            ["{{CircuitId}}"] = Clean(report.CircuitId),
            ["{{Service}}"] = Clean(report.Service),
            ["{{Status}}"] = Clean(report.Status),
            ["{{CreatedAt}}"] = FormatDateTime(report.CreatedAt),
            ["{{UpdatedAt}}"] = report.UpdatedAt.HasValue ? FormatDateTime(report.UpdatedAt.Value) : "Not updated",
            ["{{InspectionSummary}}"] = CleanMultiline(summary.Summary),
            ["{{RepairSummary}}"] = CleanMultiline(summary.Repairs),
            ["{{RecommendationsSummary}}"] = CleanMultiline(summary.Recommendations),
            ["{{NdeTestingSummary}}"] = CleanMultiline(summary.NdeAndTesting),
            ["{{ReturnToServiceSummary}}"] = CleanMultiline(summary.ReturnToService)
        };

        AddChecklistAnswerMappings(tagMap, report);
        return tagMap;
    }

    private static void AddChecklistAnswerMappings(
        Dictionary<string, string> tagMap,
        InspectionReport report)
    {
        var answers = report.Sections
            .SelectMany(section => section.Answers)
            .ToList();

        foreach (var (fieldId, prefix) in ChecklistFieldToTemplatePrefixMap)
        {
            var answer = answers.FirstOrDefault(a => string.Equals(a.FieldId, fieldId, StringComparison.OrdinalIgnoreCase));

            tagMap[$"{{{{{prefix}_Status}}}}"] = answer?.Value ?? string.Empty;
            tagMap[$"{{{{{prefix}_Comment}}}}"] = answer?.Comment ?? string.Empty;
            tagMap[$"{{{{{prefix}_PhotoRefs}}}}"] = string.Empty;
            tagMap[$"{{{{{prefix}_RecommendationText}}}}"] = answer?.RecommendationRequired == true
                ? answer.Comment ?? string.Empty
                : string.Empty;
            tagMap[$"{{{{{prefix}_RepairText}}}}"] = string.Empty;
        }
    }

    private static void ReplaceTextTags(OpenXmlElement scope, IReadOnlyDictionary<string, string> tagMap)
    {
        foreach (var paragraph in scope.Descendants<W.Paragraph>())
        {
            var textNodes = paragraph.Descendants<W.Text>().ToList();
            if (textNodes.Count == 0)
            {
                continue;
            }

            var fullText = string.Concat(textNodes.Select(t => t.Text));
            var updated = fullText;
            foreach (var (tag, value) in tagMap)
            {
                updated = updated.Replace(tag, value, StringComparison.OrdinalIgnoreCase);
            }

            if (string.Equals(fullText, updated, StringComparison.Ordinal))
            {
                continue;
            }

            paragraph.RemoveAllChildren<W.Run>();
            var replacementRun = new W.Run();
            replacementRun.AppendChild(new W.Text(updated) { Space = SpaceProcessingModeValues.Preserve });
            paragraph.AppendChild(replacementRun);
        }
    }

    private static string Clean(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "Not provided" : value.Trim();

    private static string CleanMultiline(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Not provided";
        }

        return value.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Trim();
    }

    private static string FormatDateTime(DateTime value) =>
        value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

    private static string FormatDate(DateTime value) =>
        value == default ? "Not provided" : value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
}
