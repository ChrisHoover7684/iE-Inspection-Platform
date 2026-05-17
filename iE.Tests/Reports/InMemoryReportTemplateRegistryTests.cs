using iE.Core.Reports.Templates;

namespace iE.Tests.Reports;

public class InMemoryReportTemplateRegistryTests
{
    private static readonly string[] RequiredExternalInspectionTemplates =
    [
        "api-510-vessel-external",
        "api-570-piping-external",
        "api-570-piping-cui-external",
        "sti-sp001-tank-external",
        "api-510-exchanger-shell-tube-external",
        "api-510-exchanger-plate-frame-external",
        "api-510-exchanger-double-pipe-external",
        "api-510-exchanger-air-cooler-external",
        "api-510-drum-horizontal-external",
        "api-510-drum-vertical-external",
        "api-510-drum-separator-ko-external",
        "api-510-drum-accumulator-receiver-external",
        "api-510-vessel-generic-external",
        "api-510-tower-distillation-external",
        "api-510-tower-absorber-stripper-external",
        "api-510-tower-packed-column-external",
        "api-510-tower-tray-column-external"
    ];

    private static readonly string[] RequiredRepairRecommendationTemplates =
    [
        "repair-recommendation-general",
        "repair-recommendation-api-510-vessel",
        "repair-recommendation-api-570-piping",
        "repair-recommendation-sti-sp001-tank"
    ];

    private readonly InMemoryReportTemplateRegistry registry = new();

    [Fact]
    public void GetTemplates_ReturnsAllRequiredTemplates()
    {
        var templates = registry.GetTemplates();

        Assert.Equal(21, templates.Count);

        foreach (var templateId in RequiredExternalInspectionTemplates.Concat(RequiredRepairRecommendationTemplates))
        {
            Assert.Contains(templates, template => template.Id == templateId);
        }
    }

    [Fact]
    public void GetTemplates_UsesUniqueTemplateIds()
    {
        var templates = registry.GetTemplates();

        var uniqueCount = templates.Select(template => template.Id).Distinct(StringComparer.OrdinalIgnoreCase).Count();

        Assert.Equal(templates.Count, uniqueCount);
    }

    [Fact]
    public void EachTemplate_HasUniqueSectionIds()
    {
        var templates = registry.GetTemplates();

        foreach (var template in templates)
        {
            var uniqueSections = template.Sections.Select(section => section.Id).Distinct(StringComparer.OrdinalIgnoreCase).Count();
            Assert.Equal(template.Sections.Count, uniqueSections);
        }
    }

    [Fact]
    public void EachSection_HasUniqueFieldIds()
    {
        var templates = registry.GetTemplates();

        foreach (var template in templates)
        {
            foreach (var section in template.Sections)
            {
                var uniqueFields = section.Fields.Select(field => field.Id).Distinct(StringComparer.OrdinalIgnoreCase).Count();
                Assert.Equal(section.Fields.Count, uniqueFields);
            }
        }
    }

    [Theory]
    [InlineData("api-570-piping-external")]
    [InlineData("api-570-piping-cui-external")]
    public void Api570PipingTemplates_ContainChecklistCriteriaFields(string templateId)
    {
        var template = registry.GetTemplateById(templateId);

        Assert.NotNull(template);

        var fieldIds = template!.Sections
            .SelectMany(section => section.Fields)
            .Select(field => field.Id)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var requiredChecklistFieldIds = new[]
        {
            "external-condition",
            "active-leakage",
            "supports",
            "brackets-attachments",
            "gaskets",
            "bolting",
            "flanges",
            "coating",
            "insulation-jacketing",
            "mechanical-damage",
            "external-corrosion"
        };

        foreach (var fieldId in requiredChecklistFieldIds)
        {
            Assert.Contains(fieldId, fieldIds);
        }
    }

    [Fact]
    public void GetTemplateById_IsCaseInsensitive()
    {
        var template = registry.GetTemplateById("API-510-VESSEL-EXTERNAL");

        Assert.NotNull(template);
        Assert.Equal("api-510-vessel-external", template!.Id);
    }

    [Fact]
    public void Api510ExternalCatalog_DoesNotExposeInternalTemplates()
    {
        var catalog = ReportTemplateCatalog.BuildExternalMvpCatalog(registry.GetTemplates());
        Assert.Contains(catalog, c => c.TemplateId == "api-510-exchanger-shell-tube-external");
        Assert.DoesNotContain(catalog, c => c.InspectionScope == "Internal" && c.Standard == "API 510");
    }
}
