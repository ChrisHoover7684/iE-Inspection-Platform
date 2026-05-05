using System.Text.Json;

namespace iE.Tests.GoldenCases;

internal sealed record GoldenCaseTolerance(
    double? AllowableStressPsi,
    double? ThicknessIn,
    double? RequiredThicknessIn,
    double? RemainingLifeYears);

internal sealed record GoldenCaseMeta(
    string CaseId,
    string CalculatorModule,
    JsonElement Units,
    string SourceProvenanceNote,
    GoldenCaseTolerance Tolerance,
    string ReviewerApprovalStatus,
    string CodeStandardEdition);

internal sealed record GoldenCaseFileSet(
    string BasePath,
    string MetaPath,
    string InputPath,
    string ExpectedPath,
    GoldenCaseMeta Meta,
    JsonDocument Input,
    JsonDocument Expected);

internal static class GoldenCaseLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static IReadOnlyList<GoldenCaseFileSet> LoadAll(string repositoryRoot)
    {
        var goldenCasesRoot = Path.Combine(repositoryRoot, "iE.Tests", "GoldenCases");
        if (!Directory.Exists(goldenCasesRoot))
        {
            throw new InvalidOperationException($"Golden cases directory was not found: '{goldenCasesRoot}'.");
        }

        var metaFiles = Directory.GetFiles(goldenCasesRoot, "*.meta.json", SearchOption.AllDirectories)
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (metaFiles.Count == 0)
        {
            throw new InvalidOperationException($"No '*.meta.json' files were found under '{goldenCasesRoot}'.");
        }

        var cases = new List<GoldenCaseFileSet>();
        foreach (var metaPath in metaFiles)
        {
            var basePath = metaPath[..^".meta.json".Length];
            var inputPath = basePath + ".input.json";
            var expectedPath = basePath + ".expected.json";

            if (!File.Exists(inputPath))
            {
                throw new InvalidOperationException($"Missing matching input file for '{metaPath}'. Expected '{inputPath}'.");
            }

            if (!File.Exists(expectedPath))
            {
                throw new InvalidOperationException($"Missing matching expected file for '{metaPath}'. Expected '{expectedPath}'.");
            }

            var metaText = File.ReadAllText(metaPath);
            var metaDoc = JsonDocument.Parse(metaText);
            ValidateRequiredMeta(metaPath, metaDoc.RootElement);

            var meta = JsonSerializer.Deserialize<GoldenCaseMeta>(metaText, JsonOptions)
                       ?? throw new InvalidOperationException($"Could not deserialize meta file '{metaPath}'.");

            var input = JsonDocument.Parse(File.ReadAllText(inputPath));
            var expected = JsonDocument.Parse(File.ReadAllText(expectedPath));
            cases.Add(new GoldenCaseFileSet(basePath, metaPath, inputPath, expectedPath, meta, input, expected));
        }

        return cases;
    }

    private static void ValidateRequiredMeta(string metaPath, JsonElement root)
    {
        ValidateRequired(root, "caseId", metaPath);
        ValidateRequired(root, "calculatorModule", metaPath);
        ValidateRequired(root, "units", metaPath);
        ValidateRequired(root, "sourceProvenanceNote", metaPath);
        ValidateRequired(root, "tolerance", metaPath);
        ValidateRequired(root, "reviewerApprovalStatus", metaPath);
        ValidateRequired(root, "codeStandardEdition", metaPath);
    }

    private static void ValidateRequired(JsonElement root, string fieldName, string metaPath)
    {
        if (!root.TryGetProperty(fieldName, out var field))
        {
            throw new InvalidOperationException($"Missing required metadata field '{fieldName}' in '{metaPath}'.");
        }

        if (field.ValueKind == JsonValueKind.Null)
        {
            throw new InvalidOperationException($"Metadata field '{fieldName}' is null in '{metaPath}'.");
        }

        if (field.ValueKind == JsonValueKind.String && string.IsNullOrWhiteSpace(field.GetString()))
        {
            throw new InvalidOperationException($"Metadata field '{fieldName}' is empty in '{metaPath}'.");
        }
    }
}
