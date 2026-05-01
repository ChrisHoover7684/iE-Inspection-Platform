import { useEffect, useMemo, useState } from 'react';
import { ApiError, reportingApi } from './api';
import type { InlineSuggestion, InspectionReport, InspectionReportAnswer, NarrativeResult, UiAlert } from './types';

const TEMPLATE_ID = 'api-570-piping-external';

const getErrorMessage = (error: unknown, fallback: string) =>
  error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback;

export function Api570PipingExternalEntryPage() {
  const [report, setReport] = useState<InspectionReport | null>(null);
  const [ieAssistEnabled, setIeAssistEnabled] = useState(true);
  const [alerts, setAlerts] = useState<UiAlert[]>([]);
  const [narrative, setNarrative] = useState<NarrativeResult | null>(null);
  const [fieldSuggestions, setFieldSuggestions] = useState<Record<string, InlineSuggestion[]>>({});
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  useEffect(() => {
    (async () => {
      try {
        await reportingApi.getTemplateById(TEMPLATE_ID);
        const created = await reportingApi.createInstanceFromTemplate(TEMPLATE_ID);
        setReport(created);
      } catch (error) {
        setError(getErrorMessage(error, 'Failed to initialize report.'));
      }
    })();
  }, []);

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

  const refreshAlerts = async () => {
    if (!report) return;
    try {
      const nextAlerts = await reportingApi.getAlerts(report);
      setAlerts(nextAlerts);
      setMessage(`Loaded ${nextAlerts.length} alerts.`);
    } catch (error) {
      setError(getErrorMessage(error, 'Unable to load alerts.'));
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
                  <textarea
                    value={answer.value || ''}
                    onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })}
                  />
                  <div className="row">
                    <button
                      type="button"
                      onClick={() => void updateAnswer(sectionIndex, answerIndex, {}, true)}>
                      Verify This Note
                    </button>
                  </div>
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
          <button type="button" onClick={refreshAlerts}>Refresh Alerts</button>
          {alerts.length === 0 ? <p className="muted">No alerts loaded.</p> : (
            <ul>
              {alerts.map((alert) => (
                <li key={alert.id}><strong>{alert.severity}:</strong> {alert.title} - {alert.message}</li>
              ))}
            </ul>
          )}
          <hr />
          <button type="button" onClick={onGeneratePreview}>Generate Preview</button>
          {narrative && <pre className="narrative-preview">{narrative.summary}</pre>}
        </aside>
      </div>
    </div>
  );
}
