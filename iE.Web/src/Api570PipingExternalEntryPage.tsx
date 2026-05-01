import { useEffect, useMemo, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InlineSuggestion, InspectionReport, InspectionReportAnswer, NarrativeResult, ReportTemplate, UiAlert } from './types';

const TEMPLATE_ID = 'api-570-piping-external';
const ACTIVE_REPORT_ID_STORAGE_KEY = 'ie_api570_active_report_id';

const NARRATIVE_SECTION_ORDER = [
  'Summary',
  'Inspection',
  'Findings',
  'NDE/Testing',
  'Repairs',
  'Recommendations',
  'Return to Service'
] as const;

const getErrorMessage = (error: unknown, fallback: string) =>
  error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback;

const findNarrativeSection = (narrative: NarrativeResult, title: string) =>
  narrative.sections.find((section) => section.title.trim().toLowerCase() === title.toLowerCase());

export function Api570PipingExternalEntryPage() {
  const location = useLocation();
  const [report, setReport] = useState<InspectionReport | null>(null);
  const [template, setTemplate] = useState<ReportTemplate | null>(null);
  const [ieAssistEnabled, setIeAssistEnabled] = useState(true);
  const [alerts, setAlerts] = useState<UiAlert[]>([]);
  const [narrative, setNarrative] = useState<NarrativeResult | null>(null);
  const [fieldSuggestions, setFieldSuggestions] = useState<Record<string, InlineSuggestion[]>>({});
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [isDirty, setIsDirty] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        const loadedTemplate = await reportingApi.getTemplateById(TEMPLATE_ID);
        setTemplate(loadedTemplate);

        const routeState = (location.state as { reportId?: string; report?: InspectionReport } | null) ?? null;
        const routeReport = routeState?.report;
        const routeReportId = routeState?.reportId;
        const storedId = localStorage.getItem(ACTIVE_REPORT_ID_STORAGE_KEY);

        if (routeReport?.id) {
          setReport(routeReport);
          localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, routeReport.id);
          return;
        }

        const existingId = routeReportId || storedId;
        if (existingId) {
          try {
            const existing = await reportingApi.getInstanceById(existingId);
            setReport(existing);
            localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, existing.id);
            return;
          } catch {
            localStorage.removeItem(ACTIVE_REPORT_ID_STORAGE_KEY);
          }
        }

        const created = await reportingApi.createInstanceFromTemplate(TEMPLATE_ID);
        setReport(created);
        localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, created.id);
      } catch (error) {
        setError(getErrorMessage(error, 'Failed to initialize report.'));
      }
    })();
  }, [location.state]);

  const sortedSections = useMemo(
    () => (report?.sections || []).map((section, i) => ({ section, i })).sort((a, b) => a.section.order - b.section.order),
    [report]
  );

  const updateAnswer = async (
    sectionIndex: number,
    answerIndex: number,
    partial: Partial<InspectionReportAnswer>,
    manualVerificationRequested = false
  ) => {
    if (!report) return;
    const latest: InspectionReport = structuredClone(report);
    Object.assign(latest.sections[sectionIndex].answers[answerIndex], partial);
    setReport(latest);
    setIsDirty(true);

    const answer = latest.sections[sectionIndex].answers[answerIndex];
    const text = answer.value || answer.comment || '';
    if (!text.trim()) return;
    if (!ieAssistEnabled && !manualVerificationRequested) return;

    try {
      const res = await reportingApi.inlineSuggestions(
        latest,
        answer.fieldId,
        text,
        ieAssistEnabled,
        manualVerificationRequested,
        latest.sections[sectionIndex].sectionId
      );
      setFieldSuggestions((current) => ({ ...current, [answer.fieldId]: res.suggestions }));
    } catch {
      // optional UX call; do not block typing
    }
  };

  const saveReport = async () => {
    if (!report) return;
    setIsSaving(true);
    setError('');
    try {
      const saved = await reportingApi.saveInstance(report.id, report);
      setReport(saved);
      setIsDirty(false);
      setMessage(`Report ${saved.id} saved.`);
      localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, saved.id);
      await refreshAlerts(saved);
    } catch (error) {
      setError(getErrorMessage(error, 'Unable to save report.'));
    } finally {
      setIsSaving(false);
    }
  };

  const renderAnswerInput = (sectionIndex: number, answerIndex: number, answer: InspectionReportAnswer) => {
    const templateField = template?.sections
      .find((section) => section.id === report?.sections[sectionIndex]?.sectionId)
      ?.fields?.find((field) => field.id === answer.fieldId);

    const metadataOptions = templateField?.options ?? [];
    const answerValues = answer.values ?? [];
    const optionValues = answerValues.length > 0 ? answerValues : metadataOptions;
    const currentValue = answer.value || '';
    const currentMultiValues = answer.values ?? [];

    switch (answer.dataType.toLowerCase()) {
      case 'textarea':
        return <textarea value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })} />;
      case 'boolean':
        return (
          <select value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })}>
            <option value="">Select...</option>
            <option value="yes">Yes</option>
            <option value="no">No</option>
          </select>
        );
      case 'date':
        return <input type="date" value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })} />;
      case 'number':
        return <input type="number" value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })} />;
      case 'select':
        return (
          <select value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })}>
            <option value="">Select...</option>
            {optionValues.map((option) => (
              <option key={option} value={option}>{option}</option>
            ))}
          </select>
        );
      case 'multiselect':
        return (
          <div className="checkbox-group">
            {optionValues.map((option) => {
              const checked = currentMultiValues.includes(option);
              return (
                <label key={option} className="toggle-row">
                  <input
                    type="checkbox"
                    checked={checked}
                    onChange={(e) => {
                      const nextValues = e.target.checked
                        ? [...currentMultiValues, option]
                        : currentMultiValues.filter((value) => value !== option);
                      void updateAnswer(sectionIndex, answerIndex, { values: nextValues, value: nextValues.join(', ') });
                    }}
                  />
                  <span>{option}</span>
                </label>
              );
            })}
          </div>
        );
      case 'text':
      default:
        return <input type="text" value={currentValue} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })} />;
    }
  };

  const refreshAlerts = async (reportToUse?: InspectionReport) => {
    const targetReport = reportToUse ?? report;
    if (!targetReport) return;
    try {
      const nextAlerts = await reportingApi.getAlerts(targetReport);
      setAlerts(nextAlerts);
      setMessage(`Loaded ${nextAlerts.length} alerts.`);
    } catch (error) {
      setError(getErrorMessage(error, 'Unable to load alerts.'));
    }
  };

  const createNewReport = async () => {
    try {
      setError('');
      const created = await reportingApi.createInstanceFromTemplate(TEMPLATE_ID);
      localStorage.removeItem(ACTIVE_REPORT_ID_STORAGE_KEY);
      localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, created.id);
      setReport(created);
      setAlerts([]);
      setNarrative(null);
      setFieldSuggestions({});
      setIsDirty(false);
      setMessage(`Created new report ${created.id}.`);
    } catch (error) {
      setError(getErrorMessage(error, 'Unable to create new report.'));
    }
  };

  const onGeneratePreview = async () => {
    if (!report) return;
    try {
      const preview = await reportingApi.generateNarrative(report);
      setNarrative(preview);
      setMessage('Narrative preview generated.');
    } catch (error) {
      setError(getErrorMessage(error, 'Unable to generate preview.'));
    }
  };

  if (!report) return <div className="page">{error || 'Loading API 570 Piping External report...'}</div>;

  return (
    <div className="page">
      <h1>API 570 Piping External - Report Entry</h1>
      <div className="card">
        <label className="toggle-row">
          <input type="checkbox" checked={ieAssistEnabled} onChange={(e) => setIeAssistEnabled(e.target.checked)} />
          <span>iE Assist</span>
        </label>
        <p className="muted">Live inline suggestions while typing when enabled.</p>
        <div className="row">
          <button type="button" onClick={() => void saveReport()} disabled={isSaving || !isDirty}>
            {isSaving ? 'Saving...' : 'Save Report'}
          </button>
          <button type="button" onClick={() => void createNewReport()}>New Report</button>
          <span className="muted">Status: {isDirty ? 'Dirty (unsaved changes)' : 'Saved'}</span>
        </div>
      </div>

      {error && <p className="error">{error}</p>}
      {message && <p className="success">{message}</p>}

      <div className="workflow-layout">
        <div>
          {sortedSections.map(({ section, i: sectionIndex }) => (
            <div className="card section" key={`${section.sectionId}-${section.instanceNumber ?? 0}`}>
              <h2>{section.sectionTitle}</h2>
              {section.answers.map((answer, answerIndex) => (
                <div className="answer-row" key={`${section.sectionId}-${answer.fieldId}-${answerIndex}`}>
                  <label><strong>{answer.label}</strong> <span className="data-type">({answer.dataType})</span></label>
                  {renderAnswerInput(sectionIndex, answerIndex, answer)}
                  {(answer.dataType.toLowerCase() === 'text' || answer.dataType.toLowerCase() === 'textarea') && (
                    <div className="row">
                      <button
                        type="button"
                        onClick={() => void updateAnswer(sectionIndex, answerIndex, {}, true)}>
                        Verify This Note
                      </button>
                    </div>
                  )}
                  {(fieldSuggestions[answer.fieldId] || []).length > 0 && (
                    <ul className="suggestions">
                      {fieldSuggestions[answer.fieldId].map((suggestion, idx) => (
                        <li key={`${suggestion.promptType}-${idx}`}>
                          <strong>{suggestion.severity.toUpperCase()}:</strong> {suggestion.suggestion}
                        </li>
                      ))}
                    </ul>
                  )}
                </div>
              ))}
            </div>
          ))}
        </div>

        <aside className="card alerts-panel">
          <h3>Alerts</h3>
          <button type="button" onClick={() => void refreshAlerts()}>Refresh Alerts</button>
          {alerts.length === 0 ? <p className="muted">No alerts loaded.</p> : (
            <ul>
              {alerts.map((alert) => (
                <li key={alert.id}><strong>{alert.severity}:</strong> {alert.title} - {alert.message}</li>
              ))}
            </ul>
          )}
          <hr />
          <button type="button" onClick={onGeneratePreview}>Generate Preview</button>
          {narrative && (
            <div className="narrative-preview">
              {NARRATIVE_SECTION_ORDER.map((sectionTitle) => {
                const section = findNarrativeSection(narrative, sectionTitle);
                const text = sectionTitle === 'Summary' ? narrative.summary : section?.narrative || '(No content generated yet)';
                return (
                  <div key={sectionTitle}>
                    <h4>{sectionTitle}</h4>
                    <pre>{text}</pre>
                  </div>
                );
              })}
            </div>
          )}
        </aside>
      </div>
    </div>
  );
}
