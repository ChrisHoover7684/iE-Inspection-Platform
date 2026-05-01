namespace iE.Core.Reports.Templates;

public class InMemoryReportTemplateRegistry : IReportTemplateRegistry
{
    private static readonly IReadOnlyList<ReportTemplate> Templates = BuildTemplates();

    public IReadOnlyList<ReportTemplate> GetTemplates() => Templates;

    public ReportTemplate? GetTemplateById(string templateId) =>
        Templates.FirstOrDefault(template =>
            string.Equals(template.Id, templateId, StringComparison.OrdinalIgnoreCase));

    private static IReadOnlyList<ReportTemplate> BuildTemplates() =>
        ExternalInspectionTemplates.Build()
            .Concat(RepairRecommendationTemplates.Build())
            .ToList();
}
