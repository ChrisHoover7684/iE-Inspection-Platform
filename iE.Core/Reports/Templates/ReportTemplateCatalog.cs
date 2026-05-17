namespace iE.Core.Reports.Templates;

public record ReportTemplateCatalogEntry(string Standard, string InspectionScope, string EquipmentFamily, string EquipmentSubtype, string TemplateId);

public static class ReportTemplateCatalog
{
    public static IReadOnlyList<ReportTemplateCatalogEntry> BuildExternalMvpCatalog(IEnumerable<ReportTemplate> templates) =>
        templates
            .Where(t => !string.Equals(t.InspectionScope, "Internal", StringComparison.OrdinalIgnoreCase))
            .Select(t => new ReportTemplateCatalogEntry(
                t.Standard,
                t.InspectionScope,
                t.EquipmentFamily ?? t.EquipmentType,
                t.EquipmentSubtype ?? t.Name,
                t.Id))
            .ToList();
}
