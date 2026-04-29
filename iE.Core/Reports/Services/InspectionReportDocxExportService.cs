using System.IO.Compression;
using System.Text;
using System.Xml;

namespace iE.Core.Reports.Services;

public class InspectionReportDocxExportService(InspectionSummaryService inspectionSummaryService)
{
    public byte[] Export(InspectionReport report)
    {
        using var memoryStream = new MemoryStream();

        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            WriteEntry(archive, "[Content_Types].xml", BuildContentTypesXml());
            WriteEntry(archive, "_rels/.rels", BuildRootRelsXml());
            WriteEntry(archive, "word/document.xml", BuildDocumentXml(report));
            WriteEntry(archive, "word/_rels/document.xml.rels", BuildDocumentRelsXml());
        }

        return memoryStream.ToArray();
    }

    private static void WriteEntry(ZipArchive archive, string entryName, string content)
    {
        var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
        using var writer = new StreamWriter(entry.Open(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        writer.Write(content);
    }

    private static string BuildContentTypesXml() =>
        """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
          <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
          <Default Extension="xml" ContentType="application/xml"/>
          <Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/>
        </Types>
        """;

    private static string BuildRootRelsXml() =>
        """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
          <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
        </Relationships>
        """;

    private static string BuildDocumentRelsXml() =>
        """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships" />
        """;

    private string BuildDocumentXml(InspectionReport report)
    {
        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = false,
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
            Indent = true
        };

        using var stringWriter = new StringWriter();
        using var writer = XmlWriter.Create(stringWriter, settings);

        writer.WriteStartDocument();
        var summary = inspectionSummaryService.Build(report);

        var tagValues = new Dictionary<string, string>
        {
            ["{{InspectionSummary}}"] = summary.Summary,
            ["{{RepairSummary}}"] = summary.Repairs,
            ["{{RecommendationsSummary}}"] = summary.Recommendations,
            ["{{NdeTestingSummary}}"] = summary.NdeAndTesting,
            ["{{ReturnToServiceSummary}}"] = summary.ReturnToService
        };

        writer.WriteStartElement("w", "document", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
        writer.WriteStartElement("w", "body", null);

        WriteHeading(writer, "API 570 Inspection Report");
        WriteParagraph(writer, $"Report Number: {ValueOrNotProvided(report.ReportNumber)}");
        WriteParagraph(writer, $"Equipment Tag: {ValueOrNotProvided(report.EquipmentTag)}");
        WriteParagraph(writer, $"Unit: {ValueOrNotProvided(report.Unit)}");
        WriteParagraph(writer, $"System ID: {ValueOrNotProvided(report.SystemId)}");
        WriteParagraph(writer, $"Circuit ID: {ValueOrNotProvided(report.CircuitId)}");
        WriteParagraph(writer, $"Service: {ValueOrNotProvided(report.Service)}");
        WriteParagraph(writer, $"Status: {ValueOrNotProvided(report.Status)}");
        WriteParagraph(writer, $"Created At (UTC): {report.CreatedAt:yyyy-MM-dd HH:mm:ss}");
        WriteParagraph(writer, $"Updated At (UTC): {(report.UpdatedAt.HasValue ? report.UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Not updated")}");

        WriteHeading(writer, "Checklist Sections");
        if (report.Sections.Count == 0)
        {
            WriteParagraph(writer, "No checklist sections available.");
        }
        else
        {
            foreach (var section in report.Sections.OrderBy(s => s.Order).ThenBy(s => s.InstanceNumber ?? 0))
            {
                var instanceSuffix = section.InstanceNumber.HasValue ? $" (Instance {section.InstanceNumber.Value})" : string.Empty;
                WriteParagraph(writer, $"Section: {ValueOrNotProvided(section.SectionTitle)}{instanceSuffix}");

                if (section.Answers.Count == 0)
                {
                    WriteParagraph(writer, "- No answers captured.");
                    continue;
                }

                foreach (var answer in section.Answers)
                {
                    var answerValue = answer.Values.Count > 0
                        ? string.Join(", ", answer.Values)
                        : ValueOrNotProvided(answer.Value);
                    WriteParagraph(writer, $"- {ValueOrNotProvided(answer.Label)}: {answerValue}");
                    if (!string.IsNullOrWhiteSpace(answer.Comment))
                    {
                        WriteParagraph(writer, $"Comment: {answer.Comment}");
                    }
                }
            }
        }

        WriteHeading(writer, "Inspection Summary");
        WriteParagraph(writer, ResolveTag("{{InspectionSummary}}", tagValues));
        WriteParagraph(writer, ResolveTag("{{RepairSummary}}", tagValues));
        WriteParagraph(writer, ResolveTag("{{RecommendationsSummary}}", tagValues));
        WriteParagraph(writer, ResolveTag("{{NdeTestingSummary}}", tagValues));
        WriteParagraph(writer, ResolveTag("{{ReturnToServiceSummary}}", tagValues));

        WriteHeading(writer, "Findings");
        if (report.Findings.Count == 0)
        {
            WriteParagraph(writer, "No findings recorded.");
        }
        else
        {
            foreach (var finding in report.Findings)
            {
                WriteParagraph(writer, $"Finding: {ValueOrNotProvided(finding.FindingType)} @ {ValueOrNotProvided(finding.ComponentLocation)}");
                WriteParagraph(writer, $"Severity: {ValueOrNotProvided(finding.Severity)}");
                WriteParagraph(writer, $"Component Type: {ValueOrNotProvided(finding.ComponentType)}");
                WriteParagraph(writer, $"Checklist Item: {ValueOrNotProvided(finding.AssociatedChecklistItem)}");
                WriteParagraph(writer, $"Description: {ValueOrNotProvided(finding.DetailedDescription)}");
                if (finding.RecommendationRequired)
                {
                    WriteParagraph(writer, $"Recommendation: {ValueOrNotProvided(finding.RecommendationText)}");
                }
            }
        }

        WriteHeading(writer, "Photos");
        if (report.Photos.Count == 0)
        {
            WriteParagraph(writer, "No photo references recorded.");
        }
        else
        {
            foreach (var photo in report.Photos)
            {
                WriteParagraph(writer, $"Photo {ValueOrNotProvided(photo.PhotoNumber)}: {ValueOrNotProvided(photo.Description)}");
                WriteParagraph(writer, $"Related Component: {ValueOrNotProvided(photo.RelatedComponent)}");
                WriteParagraph(writer, $"Related Checklist Item: {ValueOrNotProvided(photo.RelatedChecklistItem)}");
                WriteParagraph(writer, $"File: {ValueOrNotProvided(photo.FileName ?? photo.FileUrl)}");
                WriteParagraph(writer, $"Attached: {(photo.PhotoAttached ? "Yes" : "No")}");
            }
        }

        writer.WriteStartElement("w", "sectPr", null);
        writer.WriteStartElement("w", "pgSz", null);
        writer.WriteAttributeString("w", "w", null, "12240");
        writer.WriteAttributeString("w", "h", null, "15840");
        writer.WriteEndElement();
        writer.WriteEndElement();

        writer.WriteEndElement();
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();

        return stringWriter.ToString();
    }

    private static string ResolveTag(string value, IReadOnlyDictionary<string, string> tagValues)
    {
        if (tagValues.TryGetValue(value, out var replacement))
        {
            return replacement;
        }

        return value;
    }

    private static void WriteHeading(XmlWriter writer, string text)
    {
        writer.WriteStartElement("w", "p", null);
        writer.WriteStartElement("w", "r", null);
        writer.WriteStartElement("w", "rPr", null);
        writer.WriteElementString("w", "b", null, string.Empty);
        writer.WriteEndElement();
        writer.WriteElementString("w", "t", null, text);
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static void WriteParagraph(XmlWriter writer, string text)
    {
        writer.WriteStartElement("w", "p", null);
        writer.WriteStartElement("w", "r", null);
        writer.WriteElementString("w", "t", null, text);
        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static string ValueOrNotProvided(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "Not provided" : value.Trim();
}
