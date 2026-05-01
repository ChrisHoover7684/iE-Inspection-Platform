namespace iE.Core.Reports.Checklists;

public class ChecklistMergeService
{
    public void Merge(InspectionReport report, ObservationChecklistBuildResult buildResult)
    {
        MergeChecklistObservations(report.Observations, buildResult.Observations);
        MergeChecklistFindings(report.Findings, buildResult.Findings);
    }

    private static void MergeChecklistObservations(
        List<InspectionObservation> existingObservations,
        IReadOnlyList<InspectionObservation> generatedObservations)
    {
        foreach (var generated in generatedObservations)
        {
            var hasDuplicate = existingObservations.Any(existing =>
                (!string.IsNullOrWhiteSpace(existing.Id) &&
                 string.Equals(existing.Id, generated.Id, StringComparison.OrdinalIgnoreCase))
                || (string.Equals(existing.Category, generated.Category, StringComparison.OrdinalIgnoreCase)
                    && existing.Status == generated.Status
                    && string.Equals(existing.Notes?.Trim(), generated.Notes?.Trim(), StringComparison.OrdinalIgnoreCase)
                    && HaveEquivalentPhotoIds(existing.PhotoIds, generated.PhotoIds)));

            if (hasDuplicate)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(generated.Id)
                || existingObservations.Any(o => string.Equals(o.Id, generated.Id, StringComparison.OrdinalIgnoreCase)))
            {
                generated.Id = Guid.NewGuid().ToString("N");
            }

            existingObservations.Add(generated);
        }
    }

    private static void MergeChecklistFindings(
        List<InspectionFinding> existingFindings,
        IReadOnlyList<InspectionFinding> generatedFindings)
    {
        foreach (var generated in generatedFindings)
        {
            var hasDuplicate = existingFindings.Any(existing =>
                (!string.IsNullOrWhiteSpace(existing.Id) &&
                 string.Equals(existing.Id, generated.Id, StringComparison.OrdinalIgnoreCase))
                || (string.Equals(existing.AssociatedChecklistItem, generated.AssociatedChecklistItem, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(existing.Description?.Trim(), generated.Description?.Trim(), StringComparison.OrdinalIgnoreCase)
                    && existing.FindingType == generated.FindingType
                    && existing.Severity == generated.Severity));

            if (hasDuplicate)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(generated.Id)
                || existingFindings.Any(f => string.Equals(f.Id, generated.Id, StringComparison.OrdinalIgnoreCase)))
            {
                generated.Id = Guid.NewGuid().ToString("N");
            }

            existingFindings.Add(generated);
        }
    }

    private static bool HaveEquivalentPhotoIds(IReadOnlyList<string>? left, IReadOnlyList<string>? right)
    {
        var leftNormalized = (left ?? Array.Empty<string>())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var rightNormalized = (right ?? Array.Empty<string>())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        if (leftNormalized.Length != rightNormalized.Length)
        {
            return false;
        }

        for (var i = 0; i < leftNormalized.Length; i++)
        {
            if (!string.Equals(leftNormalized[i], rightNormalized[i], StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }
}
