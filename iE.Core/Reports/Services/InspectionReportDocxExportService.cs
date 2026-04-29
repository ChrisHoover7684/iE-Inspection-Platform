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

    private Dictionary<string, string> BuildTagMap(InspectionReport report)
    {
        var summary = inspectionSummaryService.Build(report);

        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["{{Client}}"] = Clean(report.ClientOrganizationId),
            ["{{Facility}}"] = Clean(report.FacilityId),
            ["{{SystemID}}"] = Clean(report.SystemId),
            ["{{ReportID}}"] = Clean(report.ReportNumber),
            ["{{InspectionDate}}"] = FormatDate(report.CreatedAt),
            ["{{Insp_Name}}"] = Clean(report.CreatedByUserId),
            ["{{ReportNumber}}"] = Clean(report.ReportNumber),
            ["{{EquipmentTag}}"] = Clean(report.EquipmentTag),
            ["{{Unit}}"] = Clean(report.Unit),
            ["{{SystemId}}"] = Clean(report.SystemId),
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
    }

    private static void ReplaceTextTags(OpenXmlElement scope, IReadOnlyDictionary<string, string> tagMap)
    {
        foreach (var textNode in scope.Descendants<W.Text>())
        {
            var updated = textNode.Text;
            foreach (var (tag, value) in tagMap)
            {
                updated = updated.Replace(tag, value, StringComparison.OrdinalIgnoreCase);
            }

            textNode.Text = updated;
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
