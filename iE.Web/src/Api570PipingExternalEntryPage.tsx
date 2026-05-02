import { useEffect, useMemo, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { ApiError, reportingApi } from './api';
import type { InlineSuggestion, InspectionReport, InspectionReportAnswer, NarrativeResult, ReportTemplate, UiAlert } from './types';

const TEMPLATE_ID = 'api-570-piping-external';
const ACTIVE_REPORT_ID_STORAGE_KEY = 'ie_api570_active_report_id';
const IE_ASSIST_STORAGE_KEY = 'ie_dashboard_ie_assist_enabled';

const HEADER_FIELDS = [
  { key: 'clientOrganizationId', label: 'Client' },
  { key: 'facilityId', label: 'Facility' },
  { key: 'templateId', label: 'Inspection Report Type' },
  { key: 'systemId', label: 'System ID' },
  { key: 'circuitId', label: 'Circuit ID' },
  { key: 'service', label: 'Service' },
  { key: 'unit', label: 'Line Numbers' },
  { key: 'equipmentTag', label: 'Pipe Class' },
  { key: 'inspectionDate', label: 'Inspection Date' },
  { key: 'inspectorName', label: 'Inspector' },
  { key: 'unit', label: 'Unit' }
] as const;

const NARRATIVE_SECTION_ORDER = ['Summary', 'Inspection', 'Findings', 'NDE/Testing', 'Repairs', 'Recommendations', 'Return to Service'] as const;

const SECTION_GROUPS: Record<string, string[]> = {
  'General Information': ['general', 'service', 'tag', 'line', 'system', 'circuit'],
  'External Inspection': ['external', 'visual', 'surface', 'shell'],
  Supports: ['support', 'hanger', 'guide', 'anchor'],
  'Insulation / CUI': ['insulation', 'cui', 'jacketing', 'moisture'],
  'Corrosion / Damage': ['corrosion', 'damage', 'pitting', 'thinning', 'deformation'],
  'NDE / Testing': ['nde', 'ut', 'pt', 'mt', 'rt', 'test'],
  Recommendations: ['recommend', 'repair', 'action'],
  'Photos / Attachments': ['photo', 'attachment', 'image']
};

const getErrorMessage = (error: unknown, fallback: string) => (error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback);
const findNarrativeSection = (narrative: NarrativeResult, title: string) => narrative.sections.find((section) => section.title.trim().toLowerCase() === title.toLowerCase());

const toCondition = (answer: InspectionReportAnswer): 'acceptable' | 'issue' | 'na' => {
  const value = `${answer.value ?? ''} ${answer.comment ?? ''}`.toLowerCase();
  if (value.includes('n/a') || value.includes('na')) return 'na';
  if (answer.recommendationRequired || answer.repairRequired || answer.photoRequired || value.includes('issue') || value.includes('defect')) return 'issue';
  return 'acceptable';
};

export function Api570PipingExternalEntryPage() {
  const location = useLocation();
  const [report, setReport] = useState<InspectionReport | null>(null);
  const [template, setTemplate] = useState<ReportTemplate | null>(null);
  const [ieAssistEnabled, setIeAssistEnabled] = useState(() => localStorage.getItem(IE_ASSIST_STORAGE_KEY) !== 'false');
  const [alerts, setAlerts] = useState<UiAlert[]>([]);
  const [narrative, setNarrative] = useState<NarrativeResult | null>(null);
  const [fieldSuggestions, setFieldSuggestions] = useState<Record<string, InlineSuggestion[]>>({});
  const [collapsedSections, setCollapsedSections] = useState<Record<string, boolean>>({});
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [isDirty, setIsDirty] = useState(false);
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => { void (async () => { /* unchanged init */
    try {
      const loadedTemplate = await reportingApi.getTemplateById(TEMPLATE_ID); setTemplate(loadedTemplate);
      const routeState = (location.state as { reportId?: string; report?: InspectionReport; ieAssistEnabled?: boolean } | null) ?? null;
      const routeReport = routeState?.report; const routeReportId = routeState?.reportId; const storedId = localStorage.getItem(ACTIVE_REPORT_ID_STORAGE_KEY);
      if (typeof routeState?.ieAssistEnabled === 'boolean') { setIeAssistEnabled(routeState.ieAssistEnabled); localStorage.setItem(IE_ASSIST_STORAGE_KEY, String(routeState.ieAssistEnabled)); }
      if (routeReport?.id) { setReport(routeReport); localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, routeReport.id); return; }
      const existingId = routeReportId || storedId;
      if (existingId) { try { const existing = await reportingApi.getInstanceById(existingId); setReport(existing); localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, existing.id); return; } catch { localStorage.removeItem(ACTIVE_REPORT_ID_STORAGE_KEY); } }
      const created = await reportingApi.createInstanceFromTemplate(TEMPLATE_ID); setReport(created); localStorage.setItem(ACTIVE_REPORT_ID_STORAGE_KEY, created.id);
    } catch (e) { setError(getErrorMessage(e, 'Failed to initialize report.')); }
  })(); }, [location.state]);

  useEffect(() => { localStorage.setItem(IE_ASSIST_STORAGE_KEY, String(ieAssistEnabled)); }, [ieAssistEnabled]);

  const sortedSections = useMemo(() => (report?.sections || []).map((section, i) => ({ section, i })).sort((a, b) => a.section.order - b.section.order), [report]);

  const findingsCount = useMemo(() => sortedSections.reduce((sum, { section }) => sum + section.answers.filter((a) => toCondition(a) === 'issue').length, 0), [sortedSections]);
  const recommendationsCount = useMemo(() => sortedSections.reduce((sum, { section }) => sum + section.answers.filter((a) => a.recommendationRequired || a.repairRequired).length, 0), [sortedSections]);

  const updateAnswer = async (sectionIndex: number, answerIndex: number, partial: Partial<InspectionReportAnswer>) => {
    if (!report) return;
    const latest: InspectionReport = structuredClone(report); Object.assign(latest.sections[sectionIndex].answers[answerIndex], partial);
    setReport(latest); setIsDirty(true);
    const answer = latest.sections[sectionIndex].answers[answerIndex];
    const text = answer.comment || answer.value || '';
    if (!text.trim() || !ieAssistEnabled) return;
    try {
      const res = await reportingApi.inlineSuggestions(latest, answer.fieldId, text, ieAssistEnabled, false, latest.sections[sectionIndex].sectionId);
      setFieldSuggestions((current) => ({ ...current, [answer.fieldId]: res.suggestions }));
    } catch {}
  };

  const saveReport = async () => {
    if (!report) return;
    setIsSaving(true);
    setError('');
    try {
      const saved = await reportingApi.saveInstance(report.id, report);
      setReport(saved);
      setIsDirty(false);
      setMessage('Report saved.');
    } catch (e) {
      setError(getErrorMessage(e, 'Failed to save report.'));
    } finally {
      setIsSaving(false);
    }
  };

  const localSuggestionsFor = (answer: InspectionReportAnswer): InlineSuggestion[] => {
    const condition = toCondition(answer); const notes = (answer.comment || answer.value || '').toLowerCase(); const suggestions: InlineSuggestion[] = [];
    if (notes.includes('pitting')) suggestions.push({ promptType: 'keyword', severity: 'warning', suggestion: 'Add pit depth and whether pitting is general or localized.' });
    if (notes.includes('repair')) suggestions.push({ promptType: 'keyword', severity: 'info', suggestion: 'Confirm a repair photo is tagged.' });
    if (condition === 'issue' && !notes.trim()) suggestions.push({ promptType: 'required', severity: 'warning', suggestion: 'Add detail describing the issue.' });
    return suggestions;
  };

  const sectionBuckets = useMemo(() => {
    const buckets = Object.keys(SECTION_GROUPS).map((name) => ({ name, entries: [] as typeof sortedSections }));
    sortedSections.forEach((entry) => {
      const lower = entry.section.sectionTitle.toLowerCase();
      const bucket = buckets.find((b) => SECTION_GROUPS[b.name].some((k) => lower.includes(k))) ?? buckets[0];
      bucket.entries.push(entry);
    });
    return buckets.filter((b) => b.entries.length > 0);
  }, [sortedSections]);

  if (!report) return <div className="page">{error || 'Loading API 570 Piping External report...'}</div>;

  return <div className="page report-page">
    <h1>API 570 Piping External - Report Entry</h1>
    <div className="report-sticky-toolbar card">
      <div className="toolbar-title"><strong>API 570 Piping External - Report Entry</strong></div>
      <div className="toolbar-metrics">
        <span>Findings: <strong>{findingsCount}</strong></span>
        <span>Recommendations: <strong>{recommendationsCount}</strong></span>
      </div>
      <div className="toolbar-actions">
        <span className="muted">{isSaving ? 'Saving…' : isDirty ? 'Unsaved changes' : 'Saved'}</span>
        <button type="button" onClick={() => void saveReport()} disabled={isSaving || !isDirty}>{isSaving ? 'Saving…' : 'Save'}</button>
      </div>
    </div>

    <div className="workflow-layout report-content-layout">
      <div>
        <div className="card accordion-card" key="report-header">
          <button className="accordion-toggle" onClick={() => setCollapsedSections((s) => ({ ...s, reportHeader: !s.reportHeader }))}>Report Header <span>{collapsedSections.reportHeader ? '+' : '−'}</span></button>
          {!collapsedSections.reportHeader && <div className="report-header-grid">
            {HEADER_FIELDS.map((field) => <div key={`${field.key}-${field.label}`} className="header-chip"><span>{field.label}</span><strong>{(report[field.key as keyof InspectionReport] as string) || '—'}</strong></div>)}
          </div>}
        </div>
        {sectionBuckets.map((bucket) => <div className="card accordion-card" key={bucket.name}>
          <button className="accordion-toggle" onClick={() => setCollapsedSections((s) => ({ ...s, [bucket.name]: !s[bucket.name] }))}>{bucket.name} <span>{collapsedSections[bucket.name] ? '+' : '−'}</span></button>
          {!collapsedSections[bucket.name] && bucket.entries.map(({ section, i: sectionIndex }) => <div className="section-card" key={`${section.sectionId}-${section.instanceNumber ?? 0}`}>
            <h3>{section.sectionTitle}</h3>
            {section.answers.map((answer, answerIndex) => {
              const suggestions = [...localSuggestionsFor(answer), ...(fieldSuggestions[answer.fieldId] || [])];
              return <div className="inspection-row" key={`${section.sectionId}-${answer.fieldId}-${answerIndex}`}>
                <div><label><strong>{answer.label}</strong></label></div>
                <div><select value={toCondition(answer)} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value === 'na' ? 'N/A' : answer.value, recommendationRequired: e.target.value === 'issue' ? answer.recommendationRequired ?? false : false })}><option value="acceptable">Acceptable</option><option value="issue">Issue</option><option value="na">N/A</option></select></div>
                <div><textarea placeholder="Enter inspection notes..." value={answer.comment ?? ''} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { comment: e.target.value })} />
                {suggestions.length > 0 && <ul className="suggestions">{suggestions.map((s, idx) => <li key={`${s.promptType}-${idx}`}><strong>{s.severity.toUpperCase()}:</strong> {s.suggestion}</li>)}</ul>}</div>
              </div>;
            })}
          </div>)}
        </div>)}
      </div>
      <aside className="right-sidebar sticky-panel-group">
        <div className="card assist-panel"><h3>iE Assist</h3><label className="toggle-row"><input type="checkbox" checked={ieAssistEnabled} onChange={(e) => setIeAssistEnabled(e.target.checked)} /><span>Enabled</span></label><p className="muted">Active inline suggestions are shown within note fields.</p></div>
        <div className="card alerts-panel"><h3>Alerts</h3><button type="button" onClick={async () => { if (!report) return; const next = await reportingApi.getAlerts(report); setAlerts(next); }}>Refresh Alerts</button>{alerts.length === 0 ? <p className="muted">No alerts loaded.</p> : <ul>{alerts.map((a) => <li key={a.id}><strong>{a.severity}:</strong> {a.title}</li>)}</ul>}</div>
      </aside>
    </div>
  </div>;
}
