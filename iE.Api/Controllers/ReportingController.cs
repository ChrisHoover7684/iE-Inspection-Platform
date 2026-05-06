using iE.Api.Contracts;
using iE.Core.Reports;
using iE.Core.Reports.Domain;
using iE.Core.Reports.Drafting;
using iE.Core.Reports.Models;
using iE.Core.Reports.Persistence;
using iE.Core.Reports.Photos;
using iE.Core.Reports.Checklists;
using iE.Core.Reports.Exports;
using iE.Core.Reports.Review;
using iE.Core.Reports.Services;
using iE.Core.Reports.Templates;
using iE.Core.Reports.Rules;
using Microsoft.AspNetCore.Mvc;
using iE.Api.Extensions;
using iE.Api.Auth;

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
    ChecklistMergeService checklistMergeService,
    ReportWorkflowService reportWorkflowService,
    IReportTemplateRegistry reportTemplateRegistry,
    IInspectionTagRuleEngine inspectionTagRuleEngine,
    InspectionAlertMapper inspectionAlertMapper,
    IInspectionAssistantService inspectionAssistantService,
    IReportNarrativeGenerator reportNarrativeGenerator,
    ReportAccessGuard reportAccessGuard,
    ReportReferenceGuard reportReferenceGuard,
    IAuditEventWriter auditEventWriter) : ControllerBase
{
    private async Task<ActionResult?> EnforceReportAccessAsync(InspectionReport report, string reportIdForMessage, string capability, string forbiddenMessage)
    {
        var access = reportAccessGuard.CanAccessReport(report, capability);
        if (access == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, reportIdForMessage, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = capability }); return this.NotFoundError($"Inspection report instance '{reportIdForMessage}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, reportIdForMessage, AuditResults.Denied, report.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = capability }); return this.ForbiddenError(forbiddenMessage); }
        if (access == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, reportIdForMessage, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = capability }); return Unauthorized(); }

        return null;
    }

    [HttpGet]
    public ActionResult<List<ReportListItemDto>> GetReports(
        [FromQuery] string? status,
        [FromQuery] string? facilityId,
        [FromQuery] string? unit)
    {
        var reports = reportAccessGuard.ScopeReports(inspectionReportRepository.GetReports(status, facilityId, unit), AuthCapabilities.ReportsRead)
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
            return this.NotFoundError($"Report template '{templateId}' was not found.");
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
        var reports = reportAccessGuard.ScopeReports(inspectionReportRepository.GetAll(clientOrganizationId, facilityId, templateId, status), AuthCapabilities.ReportsRead);
        return Ok(reports);
    }

    [HttpGet("instances/{id}")]
    public ActionResult<InspectionReport> GetInstanceById(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(report, AuthCapabilities.ReportsRead);
        if (access == ReportAccessDecision.NotFound) return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        if (access == ReportAccessDecision.Forbidden) return this.ForbiddenError("Insufficient capability to read report.");
        if (access == ReportAccessDecision.Unauthorized) return Unauthorized();

        return Ok(report);
    }

    [HttpGet("instances/{id}/export/docx")]
    public async Task<ActionResult> ExportInstanceDocx(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(report, AuthCapabilities.ExportsRead);
        if (access == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return this.NotFoundError($"Inspection report instance '{id}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, report.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return this.ForbiddenError("Insufficient capability to export report."); }
        if (access == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return Unauthorized(); }

        var fileBytes = inspectionReportDocxExportService.Export(report);
        var safeReportNumber = string.IsNullOrWhiteSpace(report.ReportNumber)
            ? id
            : report.ReportNumber.Trim().Replace(' ', '-');
        var fileName = $"api570-report-{safeReportNumber}.docx";

        await auditEventWriter.WriteAsync(AuditActions.ReportExported, AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId, new Dictionary<string, object?> { ["exportType"] = "report-docx" });
        return File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            fileName);
    }


    [HttpGet("instances/{id}/export/photo-appendix/docx")]
    public async Task<ActionResult> ExportPhotoAppendixDocx(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(report, AuthCapabilities.ExportsRead);
        if (access == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return this.NotFoundError($"Inspection report instance '{id}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, report.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return this.ForbiddenError("Insufficient capability to export report."); }
        if (access == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = AuthCapabilities.ExportsRead }); return Unauthorized(); }

        var fileBytes = photoAppendixExportService.Export(report);
        await auditEventWriter.WriteAsync(AuditActions.ReportExported, AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId, new Dictionary<string, object?> { ["exportType"] = "photo-appendix-docx" });
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
    public async Task<ActionResult<ReportDraft>> BuildDraftFromChecklist([FromBody] ApplyObservationChecklistRequest request)
    {
        if (request.Report is null)
        {
            return this.ValidationError("Report payload is required.");
        }

        request.Report.Observations ??= new List<InspectionObservation>();
        request.Report.Findings ??= new List<InspectionFinding>();

        if ((request.ChecklistResponses is null || request.ChecklistResponses.Count == 0)
            && request.Report.Observations.Count == 0
            && request.Report.Findings.Count == 0)
        {
            return this.ValidationError("checklistResponses are required when report has no observations or findings.");
        }

        var templateId = string.IsNullOrWhiteSpace(request.TemplateId)
            ? request.Report.TemplateId
            : request.TemplateId;

        var buildResult = observationChecklistService.BuildObservationsAndFindingsFromChecklist(
            templateId,
            request.ChecklistResponses,
            request.Report);

        checklistMergeService.Merge(request.Report, buildResult);

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

            reportAccessGuard.ApplyCreateOwnership(request.Report);
            var createAccessResult = await EnforceReportAccessAsync(request.Report, request.Report.Id, AuthCapabilities.ReportsWrite, "Insufficient capability to create report.");
            if (createAccessResult is not null)
            {
                return createAccessResult;
            }

            request.Report.CreatedAt = DateTime.UtcNow;
            request.Report.UpdatedAt = null;
            var createReferenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, request.Report, "BuildDraftFromChecklist");
            if (createReferenceValidation is not null)
            {
                return createReferenceValidation;
            }

            inspectionReportRepository.Create(request.Report);
            await auditEventWriter.WriteAsync(AuditActions.ReportCreated, AuditResourceTypes.Report, request.Report.Id, AuditResults.Success, request.Report.FacilityId, new Dictionary<string, object?> { ["route"] = "BuildDraftFromChecklist", ["path"] = "create" });
        }
        else
        {
            var updateAccessResult = await EnforceReportAccessAsync(existingReport, existingReport.Id, AuthCapabilities.ReportsWrite, "Insufficient capability to update report.");
            if (updateAccessResult is not null)
            {
                return updateAccessResult;
            }

            request.Report.Id = existingReport.Id;
            request.Report.ClientOrganizationId = existingReport.ClientOrganizationId;
            request.Report.FacilityId = existingReport.FacilityId;
            request.Report.CreatedAt = existingReport.CreatedAt;
            request.Report.CreatedByUserId = existingReport.CreatedByUserId;
            request.Report.UpdatedAt = DateTime.UtcNow;
            reportAccessGuard.ApplyUpdateOwnership(request.Report);

            if (string.IsNullOrWhiteSpace(request.Report.Status))
            {
                request.Report.Status = existingReport.Status;
            }

            var checklistUpdateReferenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, request.Report, "BuildDraftFromChecklist");
            if (checklistUpdateReferenceValidation is not null)
            {
                return checklistUpdateReferenceValidation;
            }

            inspectionReportRepository.Update(existingReport.Id, request.Report);
            await auditEventWriter.WriteAsync(AuditActions.ReportUpdated, AuditResourceTypes.Report, request.Report.Id, AuditResults.Success, request.Report.FacilityId, new Dictionary<string, object?> { ["route"] = "BuildDraftFromChecklist", ["path"] = "update" });
        }

        var draft = reportDraftBuilder.Build(request.Report);
        return Ok(draft);
    }

    [HttpPost("quick-complete-no-findings")]
    public ActionResult<ReportDraft> QuickCompleteNoFindings([FromBody] InspectionReport report)
    {
        if (report.Findings.Count > 0)
        {
            return this.DomainError("Quick complete cannot be used when reportable findings exist. Remove findings before using this action.");
        }

        report.Observations = noFindingObservationBuilder.BuildDefaultObservations(report.TemplateId, report.PipingProfile);

        var draft = reportDraftBuilder.Build(report);
        return Ok(draft);
    }

    [HttpPost("instances")]
    public async Task<ActionResult<InspectionReport>> CreateInstance([FromBody] InspectionReport report)
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

        reportAccessGuard.ApplyCreateOwnership(report);
        var createAccess = reportAccessGuard.CanAccessReport(report, AuthCapabilities.ReportsWrite);
        if (createAccess == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, report.Id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.NotFoundError("Inspection report instance was not found."); }
        if (createAccess == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, report.Id, AuditResults.Denied, report.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.ForbiddenError("Insufficient capability to create report."); }
        if (createAccess == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, report.Id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return Unauthorized(); }
        report.UpdatedAt = null;

        var referenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, report, "CreateInstance");
        if (referenceValidation is not null)
        {
            return referenceValidation;
        }



        var created = inspectionReportRepository.Create(report);
        await auditEventWriter.WriteAsync(AuditActions.ReportCreated, AuditResourceTypes.Report, created.Id, AuditResults.Success, created.FacilityId);
        return CreatedAtAction(nameof(GetInstanceById), new { id = created.Id }, created);
    }

    [HttpPut("instances/{id}")]
    public async Task<ActionResult<InspectionReport>> UpdateInstance(string id, [FromBody] InspectionReport report)
    {
        var existing = inspectionReportRepository.GetById(id);
        if (existing is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        report.Id = existing.Id;
        if (string.IsNullOrWhiteSpace(report.Status))
        {
            report.Status = existing.Status;
        }

        report.ClientOrganizationId = existing.ClientOrganizationId;
        report.FacilityId = existing.FacilityId; // TODO: support facility moves via dedicated endpoint with source + target facility authorization.
        report.CreatedAt = existing.CreatedAt;
        report.CreatedByUserId = existing.CreatedByUserId;
        report.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(report);

        var updateAccess = reportAccessGuard.CanAccessReport(existing, AuthCapabilities.ReportsWrite);
        if (updateAccess == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.NotFoundError($"Inspection report instance '{id}' was not found."); }
        if (updateAccess == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, existing.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.ForbiddenError("Insufficient capability to update report."); }
        if (updateAccess == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return Unauthorized(); }

        var updateReferenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, report, "UpdateInstance");
        if (updateReferenceValidation is not null)
        {
            return updateReferenceValidation;
        }

        var updated = inspectionReportRepository.Update(id, report);
        await auditEventWriter.WriteAsync(AuditActions.ReportUpdated, AuditResourceTypes.Report, updated.Id, AuditResults.Success, updated.FacilityId);
        return Ok(updated);
    }

    [HttpDelete("instances/{id}")]
    public async Task<ActionResult> DeleteInstance(string id)
    {
        var existing = inspectionReportRepository.GetById(id);
        if (existing is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var access = reportAccessGuard.CanAccessReport(existing, AuthCapabilities.ReportsWrite);
        if (access == ReportAccessDecision.NotFound) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "out_of_scope", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.NotFoundError($"Inspection report instance '{id}' was not found."); }
        if (access == ReportAccessDecision.Forbidden) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, existing.FacilityId, new Dictionary<string, object?> { ["denialReason"] = "missing_capability", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return this.ForbiddenError("Insufficient capability to delete report."); }
        if (access == ReportAccessDecision.Unauthorized) { await auditEventWriter.WriteAsync(AuditActions.AuthorizationDenied, AuditResourceTypes.Report, id, AuditResults.Denied, metadata: new Dictionary<string, object?> { ["denialReason"] = "unauthenticated", ["requiredCapability"] = AuthCapabilities.ReportsWrite }); return Unauthorized(); }

        var deleted = inspectionReportRepository.Delete(id);
        if (!deleted)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        await auditEventWriter.WriteAsync(AuditActions.ReportDeleted, AuditResourceTypes.Report, id, AuditResults.Success, existing.FacilityId);
        return NoContent();
    }

    [HttpPost("templates/{templateId}/instances")]
    public async Task<ActionResult<InspectionReport>> CreateInstanceFromTemplate(
        string templateId,
        [FromQuery] string? clientOrganizationId,
        [FromQuery] string? facilityId,
        [FromQuery] string? processUnitId,
        [FromQuery] string? assetId,
        [FromBody] CreateReportInstanceRequest? request)
    {
        var template = reportTemplateRegistry.GetTemplateById(templateId);
        if (template is null)
        {
            return this.NotFoundError($"Report template '{templateId}' was not found.");
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

        reportAccessGuard.ApplyCreateOwnership(report);
        var createAccessResult = await EnforceReportAccessAsync(report, report.Id, AuthCapabilities.ReportsWrite, "Insufficient capability to create report.");
        if (createAccessResult is not null)
        {
            return createAccessResult;
        }

        var referenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, report, "CreateInstanceFromTemplate");
        if (referenceValidation is not null)
        {
            return referenceValidation;
        }

        var created = inspectionReportRepository.Create(report);
        await auditEventWriter.WriteAsync(AuditActions.ReportCreated, AuditResourceTypes.Report, created.Id, AuditResults.Success, created.FacilityId, new Dictionary<string, object?> { ["templateId"] = templateId, ["route"] = "CreateInstanceFromTemplate" });
        return CreatedAtAction(nameof(GetInstanceById), new { id = created.Id }, created);
    }

    [HttpPost("instances/{id}/sync-findings")]
    public async Task<ActionResult<InspectionReport>> SyncFindingsFromChecklistTransfers(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsWrite, "Insufficient capability to update report.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        var updatedReport = inspectionReportFactory.CreateFindingDraftsFromChecklistTransfers(report);
        updatedReport.Id = report.Id;
        updatedReport.ClientOrganizationId = report.ClientOrganizationId;
        updatedReport.FacilityId = report.FacilityId;
        updatedReport.CreatedAt = report.CreatedAt;
        updatedReport.CreatedByUserId = report.CreatedByUserId;
        updatedReport.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(updatedReport);
        var syncReferenceValidation = await reportReferenceGuard.ValidateForPersistedWriteAsync(this, updatedReport, "SyncFindingsFromChecklistTransfers");
        if (syncReferenceValidation is not null)
        {
            return syncReferenceValidation;
        }

        inspectionReportRepository.Update(id, updatedReport);
        await auditEventWriter.WriteAsync(AuditActions.ReportUpdated, AuditResourceTypes.Report, id, AuditResults.Success, updatedReport.FacilityId, new Dictionary<string, object?> { ["route"] = "SyncFindingsFromChecklistTransfers" });
        return Ok(updatedReport);
    }



    [HttpPost("alerts")]
    public ActionResult<List<UiAlert>> GetAlerts([FromBody] InspectionReport report)
    {
        var rules = inspectionTagRuleEngine.Evaluate(report);
        return Ok(inspectionAlertMapper.Map(rules));
    }

    [HttpPost("assistant/suggestions")]
    public ActionResult<List<string>> GetAssistantSuggestions([FromBody] InspectionReport report)
    {
        var rules = inspectionTagRuleEngine.Evaluate(report);
        return Ok(inspectionAssistantService.GetSuggestions(report, rules));
    }


    [HttpPost("generate-narrative")]
    public ActionResult<ReportNarrativeResult> GenerateNarrative([FromBody] InspectionReport report)
    {
        return Ok(reportNarrativeGenerator.Generate(report));
    }

    [HttpPost("assistant/improve-text")]
    public ActionResult<object> ImproveText([FromBody] ImproveTextRequest request)
    {
        var improvedText = inspectionAssistantService.ImproveText(request.Text, request.Context);
        return Ok(new { improvedText });
    }

    [HttpPost("assistant/inline-suggestions")]
    public ActionResult<InlineAssistantResponse> GetInlineSuggestions([FromBody] InlineAssistantRequest request)
    {
        return Ok(inspectionAssistantService.GetInlineSuggestions(request));
    }

    [HttpPost("rules/evaluate")]
    public ActionResult<InspectionRuleResult> EvaluateRules([FromBody] InspectionReport report)
    {
        return Ok(inspectionTagRuleEngine.Evaluate(report));
    }

    [HttpPost("{id}/submit-for-review")]
    public async Task<ActionResult> SubmitForReview(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsSubmit, "Insufficient capability to submit report.");
        if (accessResult is not null)
        {
            return accessResult;
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

        reportWorkflowService.SubmitForReview(report);
        report.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(report);
        inspectionReportRepository.Update(id, report);

        await auditEventWriter.WriteAsync("ReportSubmittedForReview", AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId);
        return Ok(new
        {
            message = "Report submitted for review",
            status = report.Status
        });
    }

    [HttpPost("{id}/start-review")]
    public async Task<ActionResult> StartReview(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsReview, "Insufficient capability to review report.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        if (!reportWorkflowService.TryStartReview(report))
        {
            return BadRequest(new
            {
                message = "Invalid report status for this action"
            });
        }

        reportWorkflowService.StartReview(report);
        report.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(report);
        inspectionReportRepository.Update(id, report);

        await auditEventWriter.WriteAsync("ReviewStarted", AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId);
        return Ok(new
        {
            message = "Review started",
            status = report.Status
        });
    }

    [HttpPost("{id}/approve")]
    public async Task<ActionResult> ApproveReport(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsReview, "Insufficient capability to review report.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        if (!reportWorkflowService.TryApprove(report))
        {
            return BadRequest(new
            {
                message = "Invalid report status for this action"
            });
        }

        reportWorkflowService.Approve(report);
        report.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(report);
        inspectionReportRepository.Update(id, report);

        await auditEventWriter.WriteAsync("ReportApproved", AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId);
        return Ok(new
        {
            message = "Report approved",
            status = report.Status
        });
    }

    [HttpPost("{id}/return-for-revision")]
    public async Task<ActionResult> ReturnForRevision(string id, [FromBody] ReturnForRevisionRequest request)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsReview, "Insufficient capability to review report.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        if (!reportWorkflowService.TryReturnForRevision(report))
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

        reportWorkflowService.ReturnForRevision(report, request.ReviewerComments);
        report.UpdatedAt = DateTime.UtcNow;
        reportAccessGuard.ApplyUpdateOwnership(report);
        inspectionReportRepository.Update(id, report);

        await auditEventWriter.WriteAsync("ReportReturnedForRevision", AuditResourceTypes.Report, id, AuditResults.Success, report.FacilityId);
        return Ok(new
        {
            message = "Report returned for revision",
            status = report.Status,
            reviewerComments = request.ReviewerComments.Trim()
        });
    }

    [HttpGet("{id}/review-history")]
    public async Task<ActionResult<IReadOnlyList<ReportReviewHistory>>> GetReviewHistory(string id)
    {
        var report = inspectionReportRepository.GetById(id);
        if (report is null)
        {
            return this.NotFoundError($"Inspection report instance '{id}' was not found.");
        }

        var accessResult = await EnforceReportAccessAsync(report, id, AuthCapabilities.ReportsRead, "Insufficient capability to read report.");
        if (accessResult is not null)
        {
            return accessResult;
        }

        var history = report.ReviewHistory
            .OrderByDescending(x => x.PerformedAt)
            .ToList();

        return Ok(history);
    }

}
