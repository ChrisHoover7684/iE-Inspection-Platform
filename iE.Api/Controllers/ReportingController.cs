using iE.Core.Reports;
using iE.Core.Reports.Services;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportingController(
    InspectionReportRepository inspectionReportRepository,
    InspectionReportFactory inspectionReportFactory,
    InspectionReportDocxExportService inspectionReportDocxExportService) : ControllerBase
{
    [HttpGet("templates")]
    public ActionResult<IReadOnlyList<ReportTemplate>> GetTemplates()
    {
        return Ok(ReportingSeedData.GetTemplates());
    }

    [HttpGet("templates/{id}")]
    public ActionResult<ReportTemplate> GetTemplateById(string id)
    {
        var template = ReportingSeedData.GetTemplateById(id);
        if (template is null)
        {
            return NotFound(new { error = $"Report template '{id}' was not found." });
        }

        return Ok(template);
    }

    [HttpGet("instances")]
    public ActionResult<List<InspectionReport>> GetInstances()
    {
        return Ok(inspectionReportRepository.GetAll());
    }

    [HttpGet("instances/{id}")]
    public ActionResult<InspectionReport> GetInstanceById(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        return Ok(report);
    }

    [HttpGet("instances/{id}/export/docx")]
    public ActionResult ExportInstanceDocx(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        var fileBytes = inspectionReportDocxExportService.Export(report);
        var safeReportNumber = string.IsNullOrWhiteSpace(report.ReportNumber)
            ? id
            : report.ReportNumber.Trim().Replace(' ', '-');
        var fileName = $"api570-report-{safeReportNumber}.docx";

        return File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            fileName);
    }

    [HttpPost("instances")]
    public ActionResult<InspectionReport> CreateInstance([FromBody] InspectionReport report)
    {
        if (string.IsNullOrWhiteSpace(report.Id))
        {
            report.Id = Guid.NewGuid().ToString("N");
        }

        if (string.IsNullOrWhiteSpace(report.Status))
        {
            report.Status = InspectionReportStatuses.Draft;
        }

        report.CreatedAt = DateTime.UtcNow;
        report.UpdatedAt = null;

        var created = inspectionReportRepository.Create(report);
        return CreatedAtAction(nameof(GetInstanceById), new { id = created.Id }, created);
    }

    [HttpPut("instances/{id}")]
    public ActionResult<InspectionReport> UpdateInstance(string id, [FromBody] InspectionReport report)
    {
        var existing = inspectionReportRepository.GetById(id);
        if (existing is null)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        report.Id = id;
        if (string.IsNullOrWhiteSpace(report.Status))
        {
            report.Status = existing.Status;
        }

        report.CreatedAt = existing.CreatedAt;
        report.UpdatedAt = DateTime.UtcNow;

        var updated = inspectionReportRepository.Update(id, report);
        return Ok(updated);
    }

    [HttpDelete("instances/{id}")]
    public ActionResult DeleteInstance(string id)
    {
        var deleted = inspectionReportRepository.Delete(id);
        if (!deleted)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        return NoContent();
    }

    [HttpPost("templates/{templateId}/instances")]
    public ActionResult<InspectionReport> CreateInstanceFromTemplate(string templateId)
    {
        var template = ReportingSeedData.GetTemplateById(templateId);
        if (template is null)
        {
            return NotFound(new { error = $"Report template '{templateId}' was not found." });
        }

        var report = inspectionReportFactory.CreateFromTemplate(template);

        var created = inspectionReportRepository.Create(report);
        return CreatedAtAction(nameof(GetInstanceById), new { id = created.Id }, created);
    }

    [HttpPost("instances/{id}/sync-findings")]
    public ActionResult<InspectionReport> SyncFindingsFromChecklistTransfers(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        var updatedReport = inspectionReportFactory.CreateFindingDraftsFromChecklistTransfers(report);
        updatedReport.UpdatedAt = DateTime.UtcNow;
        inspectionReportRepository.Update(id, updatedReport);
        return Ok(updatedReport);
    }
}
