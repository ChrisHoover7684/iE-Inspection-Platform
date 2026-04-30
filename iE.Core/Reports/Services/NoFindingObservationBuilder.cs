namespace iE.Core.Reports.Services;

public class NoFindingObservationBuilder
{
    private const string Api570PipingTemplateId = "api-570-piping-inspection";

    public List<InspectionObservation> BuildDefaultObservations(string templateId, PipingInspectionProfile? pipingProfile = null)
    {
        if (string.IsNullOrWhiteSpace(templateId))
        {
            return new List<InspectionObservation>();
        }

        if (!string.Equals(templateId.Trim(), Api570PipingTemplateId, StringComparison.OrdinalIgnoreCase))
        {
            return new List<InspectionObservation>();
        }

        var observations = new List<InspectionObservation>
        {
            CreateObservation("External Condition", "External condition acceptable"),
            CreateObservation("Leakage", "No active leakage observed"),
            CreateObservation("Supports", "Pipe supports visually acceptable"),
            CreateObservation("Corrosion / Damage", "No reportable external corrosion or mechanical damage observed")
        };

        if (IsInsulated(pipingProfile?.InsulatedStatus))
        {
            observations.Add(CreateObservation("Insulation / Jacketing", "Insulation/jacketing acceptable"));
        }

        return observations;
    }

    private static InspectionObservation CreateObservation(string category, string notes)
    {
        return new InspectionObservation
        {
            Id = Guid.NewGuid().ToString("N"),
            Category = category,
            Status = ObservationStatus.Acceptable,
            Notes = notes,
            PhotoIds = new List<string>()
        };
    }

    private static bool IsInsulated(string? insulatedStatus)
    {
        if (string.IsNullOrWhiteSpace(insulatedStatus))
        {
            return false;
        }

        var normalized = insulatedStatus.Trim().ToLowerInvariant();
        return normalized is "insulated" or "yes" or "true";
    }
}
