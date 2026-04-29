using DocumentFormat.OpenXml.Packaging;
using W = DocumentFormat.OpenXml.Wordprocessing;

namespace iE.Core.Reports.Services;

public class InspectionReportDocxExportService(InspectionSummaryService inspectionSummaryService)
{
    public byte[] Export(InspectionReport report)
    {
        using var outputStream = new MemoryStream();
        byte[] result;

        using (var document = WordprocessingDocument.Create(
                   outputStream,
                   DocumentFormat.OpenXml.WordprocessingDocumentType.Document,
                   autoSave: true))
        {
            var mainPart = document.AddMainDocumentPart();
            mainPart.Document = BuildDocument(report);
            mainPart.Document.Save();
        }

        result = outputStream.ToArray();
        return result;
    }

    private W.Document BuildDocument(InspectionReport report)
    {
        var summary = inspectionSummaryService.Build(report);

        var tagValues = new Dictionary<string, string>
        {
            ["{{InspectionSummary}}"] = summary.Summary,
            ["{{RepairSummary}}"] = summary.Repairs,
            ["{{RecommendationsSummary}}"] = summary.Recommendations,
            ["{{NdeTestingSummary}}"] = summary.NdeAndTesting,
            ["{{ReturnToServiceSummary}}"] = summary.ReturnToService
        };

        var body = new W.Body();

        body.Append(Heading("API 570 Inspection Report"));
        body.Append(Paragraph($"Report Number: {ValueOrNotProvided(report.ReportNumber)}"));
        body.Append(Paragraph($"Equipment Tag: {ValueOrNotProvided(report.EquipmentTag)}"));
        body.Append(Paragraph($"Unit: {ValueOrNotProvided(report.Unit)}"));
        body.Append(Paragraph($"System ID: {ValueOrNotProvided(report.SystemId)}"));
        body.Append(Paragraph($"Circuit ID: {ValueOrNotProvided(report.CircuitId)}"));
        body.Append(Paragraph($"Service: {ValueOrNotProvided(report.Service)}"));
        body.Append(Paragraph($"Status: {ValueOrNotProvided(report.Status)}"));
        body.Append(Paragraph($"Created At (UTC): {report.CreatedAt:yyyy-MM-dd HH:mm:ss}"));
        body.Append(Paragraph($"Updated At (UTC): {(report.UpdatedAt.HasValue ? report.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Not updated")}"));

        body.Append(Heading("Checklist Sections"));
        if (report.Sections.Count == 0)
        {
            body.Append(Paragraph("No checklist sections available."));
        }
        else
        {
            foreach (var section in report.Sections.OrderBy(s => s.Order).ThenBy(s => s.InstanceNumber ?? 0))
            {
                var instanceSuffix = section.InstanceNumber.HasValue ? $" (Instance {section.InstanceNumber.Value})" : string.Empty;
                body.Append(Paragraph($"Section: {ValueOrNotProvided(section.SectionTitle)}{instanceSuffix}"));

                if (section.Answers.Count == 0)
                {
                    body.Append(Paragraph("- No answers captured."));
                    continue;
                }

                foreach (var answer in section.Answers)
                {
                    var answerValue = answer.Values.Count > 0
                        ? string.Join(", ", answer.Values)
                        : ValueOrNotProvided(answer.Value);
                    body.Append(Paragraph($"- {ValueOrNotProvided(answer.Label)}: {answerValue}"));
                    if (!string.IsNullOrWhiteSpace(answer.Comment))
                    {
                        body.Append(Paragraph($"Comment: {answer.Comment}"));
                    }
                }
            }
        }

        body.Append(Heading("Inspection Summary"));
        body.Append(Paragraph(ResolveTag("{{InspectionSummary}}", tagValues)));
        body.Append(Paragraph(ResolveTag("{{RepairSummary}}", tagValues)));
        body.Append(Paragraph(ResolveTag("{{RecommendationsSummary}}", tagValues)));
        body.Append(Paragraph(ResolveTag("{{NdeTestingSummary}}", tagValues)));
        body.Append(Paragraph(ResolveTag("{{ReturnToServiceSummary}}", tagValues)));

        body.Append(Heading("Findings"));
        if (report.Findings.Count == 0)
        {
            body.Append(Paragraph("No findings recorded."));
        }
        else
        {
            foreach (var finding in report.Findings)
            {
                body.Append(Paragraph($"Finding: {ValueOrNotProvided(finding.FindingType)} @ {ValueOrNotProvided(finding.ComponentLocation)}"));
                body.Append(Paragraph($"Severity: {ValueOrNotProvided(finding.Severity)}"));
                body.Append(Paragraph($"Component Type: {ValueOrNotProvided(finding.ComponentType)}"));
                body.Append(Paragraph($"Checklist Item: {ValueOrNotProvided(finding.AssociatedChecklistItem)}"));
                body.Append(Paragraph($"Description: {ValueOrNotProvided(finding.DetailedDescription)}"));
                if (finding.RecommendationRequired)
                {
                    body.Append(Paragraph($"Recommendation: {ValueOrNotProvided(finding.RecommendationText)}"));
                }
            }
        }

        body.Append(Heading("Photos"));
        if (report.Photos.Count == 0)
        {
            body.Append(Paragraph("No photo references recorded."));
        }
        else
        {
            foreach (var photo in report.Photos)
            {
                body.Append(Paragraph($"Photo {ValueOrNotProvided(photo.PhotoNumber)}: {ValueOrNotProvided(photo.Description)}"));
                body.Append(Paragraph($"Related Component: {ValueOrNotProvided(photo.RelatedComponent)}"));
                body.Append(Paragraph($"Related Checklist Item: {ValueOrNotProvided(photo.RelatedChecklistItem)}"));
                body.Append(Paragraph($"File: {ValueOrNotProvided(photo.FileName ?? photo.FileUrl)}"));
                body.Append(Paragraph($"Attached: {(photo.PhotoAttached ? "Yes" : "No")}"));
            }
        }

        body.Append(new W.SectionProperties(
            new W.PageSize { Width = 12240U, Height = 15840U }));

        return new W.Document(body);
    }

    private static string ResolveTag(string value, IReadOnlyDictionary<string, string> tagValues)
    {
        if (tagValues.TryGetValue(value, out var replacement))
        {
            return replacement;
        }

        return value;
    }

    private static W.Paragraph Heading(string text) =>
        new(
            new W.Run(
                new W.RunProperties(new W.Bold()),
                new W.Text(text) { Space = W.SpaceProcessingModeValues.Preserve }));

    private static W.Paragraph Paragraph(string text) =>
        new(new W.Run(new W.Text(text) { Space = W.SpaceProcessingModeValues.Preserve }));

    private static string ValueOrNotProvided(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "Not provided" : value.Trim();
}
