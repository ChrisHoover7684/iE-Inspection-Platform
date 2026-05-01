using iE.Core.Reports;

namespace iE.Core.Reports.Templates;

public interface IReportTemplateRegistry
{
    IReadOnlyList<ReportTemplate> GetTemplates();
    ReportTemplate? GetTemplateById(string templateId);
}
