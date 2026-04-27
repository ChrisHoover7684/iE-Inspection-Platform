import { useEffect, useMemo, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InspectionFinding, InspectionReport, InspectionReportAnswer } from './types';

const INSPECTION_STATUS_OPTIONS = ['', 'No Issue', 'Issue', 'N/A'];

function toLocalDate(value?: string | null) {
  if (!value) return '';
  return value.slice(0, 10);
}

function getErrorMessage(error: unknown, fallback: string): string {
  if (error instanceof ApiError) return error.message;
  if (error instanceof Error) return error.message;
  return fallback;
}

function AnswerEditor({
  answer,
  onChange
}: {
  answer: InspectionReportAnswer;
  onChange: (value: Partial<InspectionReportAnswer>) => void;
}) {
  const type = answer.dataType.toLowerCase();

  if (type === 'inspection-status') {
    return (
      <div className="answer-fields">
        <select value={answer.value || ''} onChange={(event) => onChange({ value: event.target.value })}>
          {INSPECTION_STATUS_OPTIONS.map((option) => (
            <option key={option} value={option}>
              {option || '(blank)'}
            </option>
          ))}
        </select>
        <textarea
          placeholder="Comment"
          value={answer.comment || ''}
          onChange={(event) => onChange({ comment: event.target.value })}
        />
        <label>
          <input
            type="checkbox"
            checked={answer.photoRequired ?? false}
            onChange={(event) => onChange({ photoRequired: event.target.checked })}
          />
          Photo Required
        </label>
        <label>
          <input
            type="checkbox"
            checked={answer.transferToComponentSection ?? false}
            onChange={(event) => onChange({ transferToComponentSection: event.target.checked })}
          />
          Transfer To Component Section
        </label>
        <label>
          <input
            type="checkbox"
            checked={answer.recommendationRequired ?? false}
            onChange={(event) => onChange({ recommendationRequired: event.target.checked })}
          />
          Recommendation Required
        </label>
      </div>
    );
  }

  if (type === 'textarea') {
    return (
      <textarea value={answer.value || ''} onChange={(event) => onChange({ value: event.target.value })} />
    );
  }

  if (type === 'boolean') {
    return (
      <input
        type="checkbox"
        checked={(answer.value || '').toLowerCase() === 'true'}
        onChange={(event) => onChange({ value: event.target.checked ? 'true' : 'false' })}
      />
    );
  }

  if (type === 'date') {
    return (
      <input
        type="date"
        value={toLocalDate(answer.value)}
        onChange={(event) => onChange({ value: event.target.value })}
      />
    );
  }

  if (type === 'number') {
    return (
      <input
        type="number"
        value={answer.value || ''}
        onChange={(event) => onChange({ value: event.target.value })}
      />
    );
  }

  if (type === 'multiselect') {
    return (
      <input
        type="text"
        value={(answer.values || []).join(', ')}
        onChange={(event) =>
          onChange({
            value: null,
            values: event.target.value
              .split(',')
              .map((part) => part.trim())
              .filter(Boolean)
          })
        }
      />
    );
  }

  return (
    <input
      type="text"
      value={answer.value || ''}
      onChange={(event) => onChange({ value: event.target.value })}
    />
  );
}

export function ReportEditPage() {
  const { id } = useParams<{ id: string }>();
  const [report, setReport] = useState<InspectionReport | null>(null);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [isLoadingReport, setIsLoadingReport] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [isSyncingFindings, setIsSyncingFindings] = useState(false);
  const [isExportingDocx, setIsExportingDocx] = useState(false);

  const loadReport = async () => {
    if (!id) return;
    setIsLoadingReport(true);
    setError('');
    try {
      const nextReport = await reportingApi.getInstanceById(id);
      setReport(nextReport);
    } catch (error) {
      setError(`API not reachable: ${getErrorMessage(error, 'Load failed')}`);
    } finally {
      setIsLoadingReport(false);
    }
  };

  useEffect(() => {
    loadReport();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const sortedSections = useMemo(
    () =>
      (report?.sections || [])
        .map((section, index) => ({ section, index }))
        .sort((a, b) => (a.section.order ?? 0) - (b.section.order ?? 0)),
    [report]
  );

  const checklistIssueCount = useMemo(() => {
    if (!report) return 0;
    return report.sections.reduce(
      (count, section) =>
        count +
        section.answers.filter(
          (answer) => answer.dataType.toLowerCase() === 'inspection-status' && answer.value === 'Issue'
        ).length,
      0
    );
  }, [report]);

  const updateAnswer = (
    sectionIndex: number,
    answerIndex: number,
    partial: Partial<InspectionReportAnswer>
  ) => {
    setReport((current) => {
      if (!current) return current;
      const next = structuredClone(current);
      Object.assign(next.sections[sectionIndex].answers[answerIndex], partial);
      return next;
    });
  };

  const updateFinding = (findingIndex: number, partial: Partial<InspectionFinding>) => {
    setReport((current) => {
      if (!current) return current;
      const next = structuredClone(current);
      Object.assign(next.findings[findingIndex], partial);
      return next;
    });
  };

  const deleteFinding = (findingIndex: number) => {
    setReport((current) => {
      if (!current) return current;
      const next = structuredClone(current);
      next.findings.splice(findingIndex, 1);
      return next;
    });
  };

  const onSave = async () => {
    if (!report) return;
    setIsSaving(true);
    setError('');
    setMessage('');
    try {
      const updated = await reportingApi.saveInstance(report.id, report);
      setReport(updated);
      setMessage('Report saved successfully.');
    } catch (error) {
      setError(`Save failed: ${getErrorMessage(error, 'Unable to save report.')}`);
    } finally {
      setIsSaving(false);
    }
  };

  const onSyncFindings = async () => {
    if (!report) return;
    setIsSyncingFindings(true);
    setError('');
    setMessage('');
    try {
      const updated = await reportingApi.syncFindings(report.id);
      setReport(updated);
      setMessage(`Findings synced. Total findings: ${updated.findings.length}.`);
    } catch (error) {
      setError(`Sync failed: ${getErrorMessage(error, 'Unable to sync findings.')}`);
    } finally {
      setIsSyncingFindings(false);
    }
  };

  const onExportDocx = async () => {
    if (!report) return;
    setIsExportingDocx(true);
    setError('');
    setMessage('');
    try {
      const { blob, fileName } = await reportingApi.exportDocx(report.id);
      const url = URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = url;
      anchor.download = fileName;
      anchor.click();
      URL.revokeObjectURL(url);
      setMessage(`Exported ${fileName}.`);
    } catch (error) {
      setError(`Export failed: ${getErrorMessage(error, 'Unable to export DOCX.')}`);
    } finally {
      setIsExportingDocx(false);
    }
  };

  if (!report) {
    return (
      <div className="page">
        <p>{isLoadingReport ? 'Loading report by ID...' : 'No report loaded.'}</p>
        {error && <p className="error">{error}</p>}
        <Link to="/reports-test">Back</Link>
      </div>
    );
  }

  return (
    <div className="page">
      <h1>Report Editor</h1>
      <Link to="/reports-test">Back to dashboard</Link>

      {error && <p className="error">{error}</p>}
      {message && <p className="success">{message}</p>}

      <div className="card">
        <h2>Header</h2>
        <p><strong>Report ID:</strong> {report.id}</p>
        <p><strong>Template ID:</strong> {report.templateId}</p>
        <p><strong>Status:</strong> {report.status}</p>
        <p><strong>ClientOrganizationId:</strong> {report.clientOrganizationId}</p>
        <p><strong>FacilityId:</strong> {report.facilityId}</p>
        <p><strong>ProcessUnitId:</strong> {report.processUnitId || '-'}</p>
        <p><strong>AssetId:</strong> {report.assetId || '-'}</p>
        <p><strong>Finding Count:</strong> <span className="badge">{report.findings.length}</span></p>
        <p><strong>Checklist Issues:</strong> <span className="badge issue-badge">{checklistIssueCount}</span></p>
      </div>

      <div className="card">
        <h2>Sections</h2>
        {sortedSections.map(({ section, index: sectionIndex }) => (
          <div key={`${section.sectionId}-${section.instanceNumber ?? 0}`} className="section">
            <h3>
              {section.sectionTitle}
              {section.instanceNumber ? ` #${section.instanceNumber}` : ''}
            </h3>
            {section.answers.map((answer, answerIndex) => {
              const isIssue =
                answer.dataType.toLowerCase() === 'inspection-status' && answer.value === 'Issue';
              return (
                <div key={answer.fieldId} className={`answer-row ${isIssue ? 'issue-row' : ''}`}>
                  <label>
                    <strong>{answer.label}</strong>
                    <span className="data-type">({answer.dataType})</span>
                  </label>
                  <AnswerEditor
                    answer={answer}
                    onChange={(partial) => updateAnswer(sectionIndex, answerIndex, partial)}
                  />
                </div>
              );
            })}
          </div>
        ))}
      </div>

      <div className="card">
        <h2>Findings ({report.findings.length})</h2>
        {report.findings.length === 0 && <p>No findings yet.</p>}
        {report.findings.map((finding, findingIndex) => (
          <div key={finding.id || findingIndex} className="finding-grid">
            <label>
              ComponentLocation
              <input
                value={finding.componentLocation}
                onChange={(event) => updateFinding(findingIndex, { componentLocation: event.target.value })}
              />
            </label>
            <label>
              ComponentType
              <input
                value={finding.componentType}
                onChange={(event) => updateFinding(findingIndex, { componentType: event.target.value })}
              />
            </label>
            <label>
              FindingType
              <input
                value={finding.findingType}
                onChange={(event) => updateFinding(findingIndex, { findingType: event.target.value })}
              />
            </label>
            <label>
              AssociatedChecklistItem
              <input
                value={finding.associatedChecklistItem}
                onChange={(event) =>
                  updateFinding(findingIndex, { associatedChecklistItem: event.target.value })
                }
              />
            </label>
            <label>
              DetailedDescription
              <textarea
                value={finding.detailedDescription}
                onChange={(event) =>
                  updateFinding(findingIndex, { detailedDescription: event.target.value })
                }
              />
            </label>
            <label>
              Severity
              <input
                value={finding.severity}
                onChange={(event) => updateFinding(findingIndex, { severity: event.target.value })}
              />
            </label>
            <label>
              RecommendationRequired
              <input
                type="checkbox"
                checked={finding.recommendationRequired}
                onChange={(event) =>
                  updateFinding(findingIndex, { recommendationRequired: event.target.checked })
                }
              />
            </label>
            <label>
              RecommendationText
              <textarea
                value={finding.recommendationText || ''}
                onChange={(event) =>
                  updateFinding(findingIndex, { recommendationText: event.target.value })
                }
              />
            </label>
            <div>
              <button type="button" onClick={() => deleteFinding(findingIndex)}>Delete Finding</button>
            </div>
          </div>
        ))}
      </div>

      <div className="row">
        <button onClick={onSave} disabled={isSaving || isLoadingReport}>
          {isSaving ? 'Saving...' : 'Save Report'}
        </button>
        <button onClick={onSyncFindings} disabled={isSyncingFindings || isLoadingReport}>
          {isSyncingFindings ? 'Syncing Findings...' : 'Sync Findings'}
        </button>
        <button onClick={onExportDocx} disabled={isExportingDocx || isLoadingReport}>
          {isExportingDocx ? 'Exporting DOCX...' : 'Export DOCX'}
        </button>
        <button onClick={loadReport} disabled={isLoadingReport}>
          {isLoadingReport ? 'Loading...' : 'Reload Report'}
        </button>
      </div>
    </div>
  );
}
