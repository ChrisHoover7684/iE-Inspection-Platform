using System.Globalization;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using W = DocumentFormat.OpenXml.Wordprocessing;

namespace iE.Core.Reports.Services;

public class InspectionReportDocxExportService(InspectionSummaryService inspectionSummaryService)
{
    private static readonly Regex AnyTemplateTagRegex = new("{{[^{}]+}}", RegexOptions.Compiled);
    private static readonly Regex EmptyChecklistLabelLineRegex =
        new(@"^\s*(Photos|Recommendation|Repair):\s*$\r?\n?", RegexOptions.Multiline | RegexOptions.Compiled);

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

            var tagMap = BuildTagMap(report);
            EnsureTemplateCoverage(body, tagMap);
            ReplaceTextTags(body, tagMap);
            mainPart.Document.Save();
        }

        result = outputStream.ToArray();
        return result;
    }

    private static readonly IReadOnlyDictionary<string, string> ChecklistFieldToTemplatePrefixMap =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["active-process-leaks"] = "A1",
            ["leak-repair-devices-clamps"] = "A2",
            ["pipe-cracks-or-corrosion"] = "C1",
            ["dead-legs"] = "C2",
            ["long-horizontal-runs-over-100-ft"] = "C3",
            ["abnormal-thermal-expansion-deformation"] = "C4",
            ["vibration-overhung-weight"] = "C5",
            ["cantilevered-branches-vents-drains"] = "C6",
            ["piping-misalignment-restricted-movement"] = "C7",
            ["bolting-inadequate-thread-engagement"] = "D1",
            ["counterbalance-condition"] = "D2",
            ["soil-to-air-interface"] = "D3",
            ["weld-haz-corrosion-damage"] = "D4",
            ["flange-corrosion-damage"] = "D5",
            ["other-general-finding"] = "E1",
            ["insulated-piping-condition"] = "E2",
            ["approximate-percent-insulated"] = "E3",
            ["insulation-type-identified"] = "F1",
            ["cui-temperature-range"] = "F2",
            ["sweating-service"] = "F3",
            ["steam-traced"] = "F4",
            ["evidence-corrosion-under-insulation"] = "F5",
            ["challenge-need-for-insulation"] = "F6",
            ["fireproofing-condition"] = "F7",
            ["other-insulation-fireproofing-finding"] = "F8",
            ["coating-condition"] = "F9",
            ["coating-damage"] = "F10",
            ["coating-failure"] = "F11",
            ["lead-containing-coating-identified"] = "F12",
            ["other-coating-finding"] = "G1",
            ["loose-support-fretting-wear"] = "G2",
            ["pipe-or-shoe-off-supports"] = "G3",
            ["support-hanger-brace-condition"] = "G4",
            ["corrosion-at-supports"] = "G5",
            ["bottomed-out-spring-hangers"] = "G6",
            ["support-bolting-condition"] = "G7",
            ["missing-u-bolts-restraints"] = "H1",
            ["other-support-finding"] = "H2"
        };

    private static readonly IReadOnlyCollection<string> AllChecklistPrefixes =
        Enumerable.Range(1, 9).Select(i => $"A{i}")
            .Concat(Enumerable.Range(1, 5).Select(i => $"B{i}"))
            .Concat(Enumerable.Range(1, 7).Select(i => $"C{i}"))
            .Concat(Enumerable.Range(1, 5).Select(i => $"D{i}"))
            .Concat(Enumerable.Range(1, 3).Select(i => $"E{i}"))
            .Concat(Enumerable.Range(1, 12).Select(i => $"F{i}"))
            .Concat(Enumerable.Range(1, 7).Select(i => $"G{i}"))
            .Concat(Enumerable.Range(1, 8).Select(i => $"H{i}"))
            .Concat(Enumerable.Range(1, 6).Select(i => $"I{i}"))
            .ToArray();

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
            ["{{TypeOfService}}"] = string.Empty,
            ["{{Status}}"] = Clean(report.Status),
            ["{{Report_Start_Date}}"] = FormatDate(report.CreatedAt),
            ["{{Report_End_Date}}"] = report.UpdatedAt.HasValue ? FormatDate(report.UpdatedAt.Value) : string.Empty,
            ["{{OperatingState}}"] = string.Empty,
            ["{{DesignPress}}"] = string.Empty,
            ["{{OperatingPress}}"] = string.Empty,
            ["{{DesignTemp}}"] = string.Empty,
            ["{{OperatingTemp}}"] = string.Empty,
            ["{{Pipe_Spec}}"] = string.Empty,
            ["{{Mtl_Grade}}"] = string.Empty,
            ["{{IsInsulated}}"] = string.Empty,
            ["{{InsulationType}}"] = string.Empty,
            ["{{FormID}}"] = Clean(report.TemplateId),
            ["{{Reviewed_Date}}"] = report.UpdatedAt.HasValue ? FormatDate(report.UpdatedAt.Value) : string.Empty,
            ["{{CreatedAt}}"] = FormatDateTime(report.CreatedAt),
            ["{{UpdatedAt}}"] = report.UpdatedAt.HasValue ? FormatDateTime(report.UpdatedAt.Value) : "Not updated",
            ["{{FindingsNarrative}}"] = CleanMultiline(summary.Summary),
            ["{{Summary_A}}"] = string.Empty,
            ["{{Summary_B}}"] = string.Empty,
            ["{{Summary_C}}"] = CleanMultiline(summary.Summary),
            ["{{Summary_D}}"] = string.Empty,
            ["{{Summary_E}}"] = string.Empty,
            ["{{Summary_F}}"] = string.Empty,
            ["{{Summary_G}}"] = string.Empty,
            ["{{Summary_H}}"] = string.Empty,
            ["{{Summary_I}}"] = string.Empty,
            ["{{Recommendations}}"] = CleanMultiline(summary.Recommendations),
            ["{{SuitabilityStatement}}"] = CleanMultiline(summary.ReturnToService),
            ["{{SuitabilityBasis}}"] = string.Empty,
            ["{{GoverningCML}}"] = string.Empty,
            ["{{MeasuredThickness}}"] = string.Empty,
            ["{{RequiredThickness_B313}}"] = string.Empty,
            ["{{CorrosionRate}}"] = string.Empty,
            ["{{RemainingLife}}"] = string.Empty,
            ["{{NextInspectionDue}}"] = string.Empty,
            ["{{ThicknessEvaluationSummary}}"] = string.Empty,
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
        var findings = report.Findings ?? new List<InspectionFinding>();

        foreach (var (fieldId, prefix) in ChecklistFieldToTemplatePrefixMap)
        {
            var answer = answers.FirstOrDefault(a => string.Equals(a.FieldId, fieldId, StringComparison.OrdinalIgnoreCase));
            var finding = findings.FirstOrDefault(f =>
                string.Equals(f.AssociatedChecklistItem, answer?.FieldId, StringComparison.OrdinalIgnoreCase));

            tagMap[$"{{{{{prefix}_Status}}}}"] = answer?.Value ?? string.Empty;
            tagMap[$"{{{{{prefix}_Comment}}}}"] = answer?.Comment ?? string.Empty;
            tagMap[$"{{{{{prefix}_PhotoRefs}}}}"] = string.Empty;
            tagMap[$"{{{{{prefix}_RecommendationText}}}}"] = finding?.RecommendationText ?? string.Empty;
            tagMap[$"{{{{{prefix}_RepairText}}}}"] = string.Empty;
        }

        foreach (var prefix in AllChecklistPrefixes)
        {
            tagMap.TryAdd($"{{{{{prefix}_Status}}}}", string.Empty);
            tagMap.TryAdd($"{{{{{prefix}_Comment}}}}", string.Empty);
            tagMap.TryAdd($"{{{{{prefix}_PhotoRefs}}}}", string.Empty);
            tagMap.TryAdd($"{{{{{prefix}_RecommendationText}}}}", string.Empty);
            tagMap.TryAdd($"{{{{{prefix}_RepairText}}}}", string.Empty);
            tagMap.TryAdd($"{{{{{prefix}_Other_Description}}}}", string.Empty);
        }
    }

    private static void EnsureTemplateCoverage(OpenXmlElement scope, Dictionary<string, string> tagMap)
    {
        var templateTags = ExtractTemplateTags(scope);
        foreach (var tag in templateTags)
        {
            if (tag.StartsWith("{{#if ", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(tag, "{{/if}}", StringComparison.OrdinalIgnoreCase))
            {
                tagMap.TryAdd(tag, string.Empty);
                continue;
            }

            tagMap.TryAdd(tag, string.Empty);
        }
    }

    private static IReadOnlyCollection<string> ExtractTemplateTags(OpenXmlElement scope)
    {
        var tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var paragraph in scope.Descendants<W.Paragraph>())
        {
            var textNodes = paragraph.Descendants<W.Text>().ToList();
            if (textNodes.Count == 0)
            {
                continue;
            }

            var text = string.Concat(textNodes.Select(t => t.Text));
            foreach (Match match in AnyTemplateTagRegex.Matches(text))
            {
                var rawTag = match.Value.Trim();
                if (rawTag.StartsWith("{{#if ", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(rawTag, "{{/if}}", StringComparison.OrdinalIgnoreCase) ||
                    rawTag.StartsWith("{{", StringComparison.Ordinal) && rawTag.EndsWith("}}", StringComparison.Ordinal))
                {
                    tags.Add(rawTag);
                }
            }
        }

        return tags.ToArray();
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

            updated = AnyTemplateTagRegex.Replace(updated, string.Empty);
            updated = RemoveEmptyChecklistLabelLines(updated);

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

    private static string RemoveEmptyChecklistLabelLines(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return EmptyChecklistLabelLineRegex.Replace(value, string.Empty);
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
