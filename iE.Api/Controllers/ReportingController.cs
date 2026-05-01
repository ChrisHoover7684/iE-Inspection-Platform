using iE.Api.Contracts;
using iE.Core.Reports;
using iE.Core.Reports.Models;
using iE.Core.Reports.Builders;
using iE.Core.Reports.Services;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportingController(
    InspectionReportRepository inspectionReportRepository,
    InspectionReportFactory inspectionReportFactory,
    InspectionReportDocxExportService inspectionReportDocxExportService,
    PhotoAppendixExportService photoAppendixExportService,
    ReportDraftBuilder reportDraftBuilder,
    NoFindingObservationBuilder noFindingObservationBuilder,
    ObservationChecklistService observationChecklistService,
    IReportTemplateRegistry reportTemplateRegistry) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ReportListItemDto>> GetReports(
        [FromQuery] string? status,
        [FromQuery] string? facilityId,
        [FromQuery] string? unit)
    {
        var reports = inspectionReportRepository.GetReports(status, facilityId, unit)
            .Select(report => new ReportListItemDto
            {
                Id = report.Id,
                ReportNumber = report.ReportNumber,
                EquipmentTag = report.EquipmentTag,
                Unit = report.Unit,
                Status = report.Status,
                CreatedAt = report.CreatedAt,
                UpdatedAt = report.UpdatedAt
            })
            .ToList();

        return Ok(reports);
    }

    [HttpGet("templates")]
    public ActionResult<IReadOnlyList<ReportTemplate>> GetTemplates()
    {
        return Ok(reportTemplateRegistry.GetTemplates());
    }

    [HttpGet("templates/{templateId}")]
    public ActionResult<ReportTemplate> GetTemplateById(string templateId)
    {
        var template = reportTemplateRegistry.GetTemplateById(templateId);
        if (template is null)
        {
            return NotFound(new { error = $"Report template '{templateId}' was not found." });
        }

        return Ok(template);
    }

    [HttpGet("instances")]
    public ActionResult<List<InspectionReport>> GetInstances(
        [FromQuery] string? clientOrganizationId,
        [FromQuery] string? facilityId,
        [FromQuery] string? templateId,
        [FromQuery] string? status)
    {
        return Ok(inspectionReportRepository.GetAll(clientOrganizationId, facilityId, templateId, status));
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


    [HttpGet("instances/{id}/export/photo-appendix/docx")]
    public ActionResult ExportPhotoAppendixDocx(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound(new { error = $"Inspection report instance '{id}' was not found." });
        }

        var fileBytes = photoAppendixExportService.Export(report);
        var fileName = string.IsNullOrWhiteSpace(report.ReportNumber)
            ? $"InspectionReport_{id}_PhotoAppendix.docx"
            : $"{report.ReportNumber.Trim()}_PhotoAppendix.docx";

        return File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            fileName);
    }


    [HttpPost("draft")]
    public ActionResult<ReportDraft> BuildDraft([FromBody] InspectionReport report)
    {
        var draft = reportDraftBuilder.Build(report);
        return Ok(draft);
    }


    [HttpGet("templates/{templateId}/checklist")]
    public ActionResult<ObservationChecklistTemplate> GetObservationChecklist(string templateId)
    {
        var checklist = observationChecklistService.GetChecklistForTemplate(templateId);
        return Ok(checklist);
    }

    [HttpPost("checklist/build-draft")]
    public ActionResult<ReportDraft> BuildDraftFromChecklist([FromBody] ApplyObservationChecklistRequest request)
    {
        if (request.Report is null)
        {
            return BadRequest(new { error = "Report payload is required." });
        }

        request.Report.Observations ??= new List<InspectionObservation>();
        request.Report.Findings ??= new List<InspectionFinding>();

        if ((request.ChecklistResponses is null || request.ChecklistResponses.Count == 0)
            && request.Report.Observations.Count == 0
            && request.Report.Findings.Count == 0)
        {
            return BadRequest(new
            {
                error = "checklistResponses are required when report has no observations or findings."
            });
        }

        var templateId = string.IsNullOrWhiteSpace(request.TemplateId)
            ? request.Report.TemplateId
            : request.TemplateId;

        var buildResult = observationChecklistService.BuildObservationsAndFindingsFromChecklist(
            templateId,
            request.ChecklistResponses,
            request.Report);

        MergeChecklistObservations(request.Report.Observations, buildResult.Observations);
        MergeChecklistFindings(request.Report.Findings, buildResult.Findings);

        var existingReport = !string.IsNullOrWhiteSpace(request.Report.Id)
            ? inspectionReportRepository.GetById(request.Report.Id)
            : null;

        if (existingReport is null)
        {
            if (string.IsNullOrWhiteSpace(request.Report.Id))
            {
                request.Report.Id = Guid.NewGuid().ToString("N");
            }

            if (string.IsNullOrWhiteSpace(request.Report.Status))
            {
                request.Report.Status = InspectionReportStatuses.Draft;
            }

            request.Report.CreatedAt = DateTime.UtcNow;
            request.Report.UpdatedAt = null;
            inspectionReportRepository.Create(request.Report);
        }
        else
        {
            request.Report.Id = existingReport.Id;
            request.Report.CreatedAt = existingReport.CreatedAt;
            request.Report.UpdatedAt = DateTime.UtcNow;

            if (string.IsNullOrWhiteSpace(request.Report.Status))
            {
                request.Report.Status = existingReport.Status;
            }

            inspectionReportRepository.Update(existingReport.Id, request.Report);
        }

        var draft = reportDraftBuilder.Build(request.Report);
        return Ok(draft);
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

    [HttpPost("quick-complete-no-findings")]
    public ActionResult<ReportDraft> QuickCompleteNoFindings([FromBody] InspectionReport report)
    {
        if (report.Findings.Count > 0)
        {
            return BadRequest(new
            {
                error = "Quick complete cannot be used when reportable findings exist. Remove findings before using this action."
            });
        }

        report.Observations = noFindingObservationBuilder.BuildDefaultObservations(report.TemplateId, report.PipingProfile);

        var draft = reportDraftBuilder.Build(report);
        return Ok(draft);
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
    public ActionResult<InspectionReport> CreateInstanceFromTemplate(
        string templateId,
        [FromQuery] string? clientOrganizationId,
        [FromQuery] string? facilityId,
        [FromQuery] string? processUnitId,
        [FromQuery] string? assetId,
        [FromBody] CreateReportInstanceRequest? request)
    {
        var template = ReportingSeedData.GetTemplateById(templateId);
        if (template is null)
        {
            return NotFound(new { error = $"Report template '{templateId}' was not found." });
        }

        var report = inspectionReportFactory.CreateFromTemplate(template);

        report.ClientOrganizationId = request?.ClientOrganizationId
            ?? clientOrganizationId
            ?? "client-demo-refining";
        report.FacilityId = request?.FacilityId
            ?? facilityId
            ?? "facility-demo-gulf-coast";
        report.ProcessUnitId = request?.ProcessUnitId
            ?? processUnitId
            ?? "unit-demo-crude";
        report.AssetId = request?.AssetId
            ?? assetId
            ?? "asset-demo-piping-system";

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

    [HttpPost("{id}/submit-for-review")]
    public ActionResult SubmitForReview(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound();
        }

        var draft = reportDraftBuilder.Build(report);
        if (!draft.CanSubmitForReview)
        {
            return BadRequest(new
            {
                message = "Report cannot be submitted",
                validationMessages = draft.ValidationResult.Messages
            });
        }

        report.Status = InspectionReportStatuses.ReadyForReview;
        report.UpdatedAt = DateTime.UtcNow;
        report.ReviewHistory.Add(new ReportReviewHistory
        {
            Id = Guid.NewGuid().ToString("N"),
            ReportId = report.Id,
            Action = ReviewAction.SubmittedForReview,
            PerformedByUserId = ResolvePerformedByUserId(report),
            PerformedAt = DateTime.UtcNow
        });
        inspectionReportRepository.Update(id, report);

        return Ok(new
        {
            message = "Report submitted for review",
            status = report.Status
        });
    }

    [HttpPost("{id}/start-review")]
    public ActionResult StartReview(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, InspectionReportStatuses.ReadyForReview, StringComparison.Ordinal))
        {
            return BadRequest(new
            {
                message = "Invalid report status for this action"
            });
        }

        report.Status = InspectionReportStatuses.InReview;
        report.UpdatedAt = DateTime.UtcNow;
        inspectionReportRepository.Update(id, report);

        return Ok(new
        {
            message = "Review started",
            status = report.Status
        });
    }

    [HttpPost("{id}/approve")]
    public ActionResult ApproveReport(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, InspectionReportStatuses.InReview, StringComparison.Ordinal))
        {
            return BadRequest(new
            {
                message = "Invalid report status for this action"
            });
        }

        report.Status = InspectionReportStatuses.Final;
        report.UpdatedAt = DateTime.UtcNow;
        report.ReviewHistory.Add(new ReportReviewHistory
        {
            Id = Guid.NewGuid().ToString("N"),
            ReportId = report.Id,
            Action = ReviewAction.Approved,
            PerformedByUserId = ResolvePerformedByUserId(report),
            PerformedAt = DateTime.UtcNow
        });
        inspectionReportRepository.Update(id, report);

        return Ok(new
        {
            message = "Report approved",
            status = report.Status
        });
    }

    [HttpPost("{id}/return-for-revision")]
    public ActionResult ReturnForRevision(string id, [FromBody] ReturnForRevisionRequest request)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound();
        }

        if (!string.Equals(report.Status, InspectionReportStatuses.InReview, StringComparison.Ordinal))
        {
            return BadRequest(new
            {
                message = "Invalid report status for this action"
            });
        }

        if (string.IsNullOrWhiteSpace(request.ReviewerComments))
        {
            return BadRequest(new
            {
                message = "reviewerComments is required"
            });
        }

        report.Status = InspectionReportStatuses.ReturnedForRevision;
        report.UpdatedAt = DateTime.UtcNow;
        report.ReviewHistory.Add(new ReportReviewHistory
        {
            Id = Guid.NewGuid().ToString("N"),
            ReportId = report.Id,
            Action = ReviewAction.ReturnedForRevision,
            Comments = request.ReviewerComments.Trim(),
            PerformedByUserId = ResolvePerformedByUserId(report),
            PerformedAt = DateTime.UtcNow
        });
        inspectionReportRepository.Update(id, report);

        return Ok(new
        {
            message = "Report returned for revision",
            status = report.Status,
            reviewerComments = request.ReviewerComments.Trim()
        });
    }

    [HttpGet("{id}/review-history")]
    public ActionResult<IReadOnlyList<ReportReviewHistory>> GetReviewHistory(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return NotFound();
        }

        var history = report.ReviewHistory
            .OrderByDescending(x => x.PerformedAt)
            .ToList();

        return Ok(history);
    }

    private static string ResolvePerformedByUserId(InspectionReport report)
    {
        return report.UpdatedByUserId
            ?? report.CreatedByUserId
            ?? "system";
    }
}
