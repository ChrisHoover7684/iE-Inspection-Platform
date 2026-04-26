using iE.Core.Reports;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportingController(InspectionReportRepository inspectionReportRepository) : ControllerBase
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

        var report = new InspectionReport
        {
            Id = Guid.NewGuid().ToString("N"),
            TemplateId = template.Id,
            Status = InspectionReportStatuses.Draft,
            CreatedAt = DateTime.UtcNow,
            Sections = template.Sections
                .OrderBy(section => section.Order)
                .Select(section => new InspectionReportSection
                {
                    SectionId = section.Id,
                    SectionTitle = section.Title,
                    Order = section.Order,
                    IsRepeatable = section.IsRepeatable,
                    Answers = section.Fields.Select(MapTemplateFieldToAnswer).ToList()
                })
                .ToList()
        };

        HydrateTopLevelFields(report);

        var created = inspectionReportRepository.Create(report);
        return CreatedAtAction(nameof(GetInstanceById), new { id = created.Id }, created);
    }

    private static InspectionReportAnswer MapTemplateFieldToAnswer(ReportTemplateField field)
    {
        var isMultiSelect = string.Equals(field.DataType, "multiselect", StringComparison.OrdinalIgnoreCase);
        var isChecklist = string.Equals(field.DataType, "inspection-status", StringComparison.OrdinalIgnoreCase) || field.IsChecklistItem;

        return new InspectionReportAnswer
        {
            FieldId = field.Id,
            Label = field.Label,
            DataType = field.DataType,
            Value = isChecklist ? string.Empty : field.DefaultValue,
            Values = isMultiSelect ? new List<string>() : new List<string>(),
            Comment = isChecklist ? string.Empty : null,
            PhotoRequired = isChecklist ? false : null,
            TransferToComponentSection = isChecklist ? false : null,
            RecommendationRequired = isChecklist ? false : null
        };
    }

    private static void HydrateTopLevelFields(InspectionReport report)
    {
        var answerByFieldId = report.Sections
            .SelectMany(section => section.Answers)
            .ToDictionary(answer => answer.FieldId, StringComparer.OrdinalIgnoreCase);

        report.Unit = answerByFieldId.GetValueOrDefault("unit")?.Value ?? string.Empty;
        report.SystemId = answerByFieldId.GetValueOrDefault("system-id")?.Value ?? string.Empty;
        report.CircuitId = answerByFieldId.GetValueOrDefault("circuit-id")?.Value ?? string.Empty;
        report.Service = answerByFieldId.GetValueOrDefault("service")?.Value ?? string.Empty;
    }
}
