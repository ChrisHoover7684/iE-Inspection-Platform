using iE.Core.Reports.Services;

namespace iE.Tests.Reports;

public class InMemoryReportTemplateRegistryTests
{
    private readonly InMemoryReportTemplateRegistry registry = new();

    [Fact]
    public void GetTemplates_ReturnsExpectedStarterTemplates()
    {
        var templates = registry.GetTemplates();

        Assert.Equal(4, templates.Count);
        Assert.Contains(templates, t => t.Id == "api-510-vessel-internal");
        Assert.Contains(templates, t => t.Id == "api-510-vessel-external");
        Assert.Contains(templates, t => t.Id == "api-570-piping-cui");
        Assert.Contains(templates, t => t.Id == "sti-sp001-tank-external");
    }

    [Fact]
    public void GetTemplateById_ReturnsTemplateWithSectionsAndFields()
    {
        var template = registry.GetTemplateById("api-570-piping-cui");

        Assert.NotNull(template);
        Assert.NotEmpty(template!.Sections);
        Assert.Contains(template.Sections, s => s.Title == "Summary");
        Assert.Contains(template.Sections, s => s.Title == "Scope / Preparation");
        Assert.Contains(template.Sections, s => s.Title == "Inspection Results");
        Assert.Contains(template.Sections, s => s.Title == "Findings");
        Assert.Contains(template.Sections, s => s.Title == "NDE / Testing");
        Assert.Contains(template.Sections, s => s.Title == "Repairs");
        Assert.Contains(template.Sections, s => s.Title == "Recommendations");
        Assert.Contains(template.Sections, s => s.Title == "Photos");
        Assert.All(template.Sections, section => Assert.NotEmpty(section.Fields));
    }

    [Fact]
    public void GetTemplateById_IsCaseInsensitive()
    {
        var template = registry.GetTemplateById("API-510-VESSEL-EXTERNAL");

        Assert.NotNull(template);
        Assert.Equal("api-510-vessel-external", template!.Id);
    }
}
