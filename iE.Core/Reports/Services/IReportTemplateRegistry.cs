using iE.Core.Reports;

namespace iE.Core.Reports.Services;

public interface IReportTemplateRegistry
{
    IReadOnlyList<ReportTemplate> GetTemplates();
    ReportTemplate? GetTemplateById(string templateId);
}
