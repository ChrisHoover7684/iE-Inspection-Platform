using System.Text.Json;
using iE.Core.Mechanical.B313.Materials;

namespace iE.Tests.GoldenCases;

public class GoldenCaseRegressionTests
{
    [Fact]
    public void GoldenCases_AllCases_HaveRequiredMetadata()
    {
        var cases = GoldenCaseLoader.LoadAll(FindRepositoryRoot());

        Assert.NotEmpty(cases);
        Assert.All(cases, c =>
        {
            Assert.False(string.IsNullOrWhiteSpace(c.Meta.CaseId));
            Assert.False(string.IsNullOrWhiteSpace(c.Meta.CalculatorModule));
            Assert.False(string.IsNullOrWhiteSpace(c.Meta.SourceProvenanceNote));
            Assert.False(string.IsNullOrWhiteSpace(c.Meta.ReviewerApprovalStatus));
            Assert.False(string.IsNullOrWhiteSpace(c.Meta.CodeStandardEdition));
        });
    }

    [Fact]
    public void GoldenCases_B313_A106B_K03006_579F_MatchesExpectedBaseline()
    {
        var goldenCase = GetCaseById("B313/A106_B_K03006_579F");
        var tolerance = goldenCase.Meta.Tolerance;

        var request = JsonSerializer.Deserialize<B313ThicknessRequest>(goldenCase.Input.RootElement.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException($"Failed to deserialize input for case '{goldenCase.Meta.CaseId}'.");

        var service = new B313MaterialStressService();
        var result = service.CalculateThickness(request);

        var expected = goldenCase.Expected.RootElement;
        Assert.Equal(expected.GetProperty("success").GetBoolean(), result.Success);
        Assert.Equal(expected.GetProperty("allowableStressPsi").GetDouble(), result.AllowableStressPsi ?? double.NaN, tolerance.AllowableStressPsi ?? 0);
        Assert.Equal(expected.GetProperty("requiredThicknessIn").GetDouble(), result.RequiredThicknessIn ?? double.NaN, tolerance.ThicknessIn ?? tolerance.RequiredThicknessIn ?? 0);
    }

    [Fact]
    public void GoldenCases_PendingCases_AreMarkedPendingAndNotAuthoritative()
    {
        var cases = GoldenCaseLoader.LoadAll(FindRepositoryRoot());

        var pendingCases = cases.Where(c => IsPendingOrReferencePending(c.Meta)).ToList();
        Assert.NotEmpty(pendingCases);

        Assert.All(pendingCases, c =>
        {
            Assert.Equal("pending", c.Meta.ReviewerApprovalStatus, ignoreCase: true);
            Assert.Contains("reference pending review", c.Meta.SourceProvenanceNote, StringComparison.OrdinalIgnoreCase);
        });
    }

    private static GoldenCaseFileSet GetCaseById(string caseId)
    {
        return GoldenCaseLoader.LoadAll(FindRepositoryRoot())
            .Single(c => string.Equals(c.Meta.CaseId, caseId, StringComparison.Ordinal));
    }

    private static bool IsPendingOrReferencePending(GoldenCaseMeta meta)
    {
        return string.Equals(meta.ReviewerApprovalStatus, "pending", StringComparison.OrdinalIgnoreCase)
               || meta.SourceProvenanceNote.Contains("reference pending review", StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRepositoryRoot()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null)
        {
            if (Directory.Exists(Path.Combine(current.FullName, "iE.Tests"))
                && Directory.Exists(Path.Combine(current.FullName, "iE.Api")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new InvalidOperationException($"Could not determine repository root from '{AppContext.BaseDirectory}'.");
    }
}
