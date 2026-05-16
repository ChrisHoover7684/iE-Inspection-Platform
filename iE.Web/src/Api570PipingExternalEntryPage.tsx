import { useEffect, useMemo, useRef, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { ApiError, b313Api, pipeLookupApi, reportingApi } from './api';
import type { InlineSuggestion, InspectionReport, InspectionReportAnswer, NarrativeResult, ReportTemplate, UiAlert } from './types';
import { calculateCorrosionRate } from './engineering/calculations/corrosionRate';
import { calculatePipeDimensions } from './engineering/calculations/pipeLookup';
import type { InspectionCalculationSnapshot } from './engineering/types';
import { applyMaterialPreset, B31_MATERIAL_PRESETS, buildCircuitBatchSnapshot, formatCircuitBatchSummary, mapB313RowResult, NPS_OD_LOOKUP, resolveCircuitConditionsFromReport, toResultDisplayRows, type B31CircuitRowInput, type B31CircuitSourceMetadata } from './engineering/calculations/b31_3CircuitBatch';

const TEMPLATE_ID = 'api-570-piping-external';
const ACTIVE_REPORT_ID_STORAGE_KEY = 'ie_api570_active_report_id';
const IE_ASSIST_STORAGE_KEY = 'ie_dashboard_ie_assist_enabled';

const HEADER_GRID_COLUMNS = [
  [
    { key: 'clientOrganizationId', label: 'Client' },
    { key: 'facilityId', label: 'Facility' }
  ],
  [
    { key: 'unit', label: 'Unit' },
    { key: 'systemId', label: 'System ID' },
    { key: 'circuitId', label: 'Circuit ID' },
    { key: 'service', label: 'Service' }
  ],
  [
    { key: 'lineNumbers', label: 'Line Numbers' },
    { key: 'equipmentTag', label: 'Pipe Class' },
    { key: 'inspectorName', label: 'Inspector Name' },
    { key: 'inspectionDate', label: 'Inspection Date' },
    { key: 'operatingState', label: 'Operating State' },
    { key: 'insulated', label: 'Insulated' }
  ]
] as const;

const NARRATIVE_SECTION_ORDER = ['Summary', 'Inspection', 'Findings', 'NDE/Testing', 'Repairs', 'Recommendations', 'Return to Service'] as const;

const REPORT_FLOW = [
  { name: 'Inspection Context', keywords: ['general', 'context', 'scope', 'access', 'method', 'reference', 'history'], defaultCollapsed: false },
  { name: 'Pressure Boundary', keywords: ['corrosion', 'pitting', 'weld', 'haz', 'damage', 'distortion', 'leak', 'vibration', 'wear', 'cml', 'thickness', 'injection', 'mix point'], defaultCollapsed: false },
  { name: 'Corrosion Protection & Environment', keywords: ['coating', 'insulation', 'jacketing', 'cui', 'soil-to-air', 'environment', 'drainage'], defaultCollapsed: true },
  { name: 'Supports & Structural Integrity', keywords: ['support', 'shoe', 'guide', 'anchor', 'spring', 'hanger', 'rack', 'structural', 'settlement', 'misalignment', 'movement', 'sliding'], defaultCollapsed: true },
  { name: 'Connections & Components', keywords: ['flange', 'bolt', 'gasket', 'valve', 'prd', 'psv', 'vent', 'drain', 'small-bore', 'instrument', 'tracing', 'flex hose'], defaultCollapsed: true },
  { name: 'Findings', keywords: ['finding', 'issue', 'defect', 'severity'], defaultCollapsed: true },
  { name: 'Recommendations', keywords: ['recommend', 'repair', 'action'], defaultCollapsed: true },
  { name: 'Summary / Final Review', keywords: ['summary', 'final', 'review', 'suitability', 'nde', 'thickness'], defaultCollapsed: true }
] as const;

const DUPLICATE_HEADER_LABELS = new Set([
  'inspector',
  'inspector name',
  'line number(s)',
  'line numbers',
  'circuit',
  'circuit id',
  'piping class',
  'pipe class',
  'service',
  'unit',
  'inspection date'
]);

const getErrorMessage = (error: unknown, fallback: string) => (error instanceof ApiError ? error.message : error instanceof Error ? error.message : fallback);
const findNarrativeSection = (narrative: NarrativeResult, title: string) => narrative.sections.find((section) => section.title.trim().toLowerCase() === title.toLowerCase());

const defaultB31Rows = (): B31CircuitRowInput[] => ([
  { id: crypto.randomUUID(), nps: '2', materialPreset: 'A106_GR_B_SEAMLESS', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' },
  { id: crypto.randomUUID(), nps: '4', materialPreset: 'A106_GR_B_SEAMLESS', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' },
  { id: crypto.randomUUID(), nps: '6', materialPreset: 'A106_GR_B_SEAMLESS', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' },
  { id: crypto.randomUUID(), nps: '0.75', materialPreset: 'A53_GR_B_ERW_EB', spec: 'A53', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'ERW', jointQualityKey: 'E/B' }
]);

const toCondition = (answer: InspectionReportAnswer): 'acceptable' | 'issue' | 'na' | 'monitor' => {
  const value = `${answer.value ?? ''} ${answer.comment ?? ''}`.toLowerCase();
  if (value.includes('n/a') || value.includes('na')) return 'na';
  if (value.includes('monitor')) return 'monitor';
  if (value.includes('issue') || value.includes('defect')) return 'issue';
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
  const [activeField, setActiveField] = useState<{ sectionIndex: number; answerIndex: number } | null>(null);
  const [selectedTool, setSelectedTool] = useState('corrosion-rate');
  const [isToolWorkspaceOpen, setIsToolWorkspaceOpen] = useState(false);
  const [calcInput, setCalcInput] = useState({ initialThicknessInches: 0.5, finalThicknessInches: 0.45, exposureTimeYears: 1, currentThicknessInches: 0.45, tminInches: 0.35, inspectionFactor: 0.5 });
  const [calcResult, setCalcResult] = useState<InspectionCalculationSnapshot | null>(null);
  const [savedCalculationIds, setSavedCalculationIds] = useState<Set<string>>(new Set());

  const [pipeLookupNpsOptions, setPipeLookupNpsOptions] = useState<string[]>([]);
  const [pipeLookupScheduleOptions, setPipeLookupScheduleOptions] = useState<string[]>([]);
  const [pipeLookupInput, setPipeLookupInput] = useState({ nps: '', schedule: '' });
  const [isPipeLookupLoading, setIsPipeLookupLoading] = useState(false);

  const [b31Rows, setB31Rows] = useState<B31CircuitRowInput[]>(() => defaultB31Rows());
  const [b31SourceMeta, setB31SourceMeta] = useState<B31CircuitSourceMetadata>(() => resolveCircuitConditionsFromReport(null));
  const [b31ManualPressure, setB31ManualPressure] = useState<number | ''>('');
  const [b31ManualTemperature, setB31ManualTemperature] = useState<number | ''>('');
  const [b31Shared, setB31Shared] = useState({ wFactor: 1, yOverride: '', eOverride: '', defaultJointType: 'Seamless', defaultJointQualityKey: 'Seamless' });
  const [isB31BatchRunning, setIsB31BatchRunning] = useState(false);

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

  useEffect(() => {
    setB31SourceMeta(resolveCircuitConditionsFromReport(report));
  }, [report]);


  useEffect(() => {
    if (selectedTool !== 'pipe-lookup') return;
    void (async () => {
      try {
        const nps = await pipeLookupApi.getNps();
        setPipeLookupNpsOptions(nps);
        setPipeLookupInput((current) => ({
          nps: current.nps || nps[0] || '',
          schedule: ''
        }));
      } catch (e) {
        setError(getErrorMessage(e, 'Failed to load pipe lookup NPS options.'));
      }
    })();
  }, [selectedTool]);

  useEffect(() => {
    if (selectedTool !== 'pipe-lookup' || !pipeLookupInput.nps) {
      setPipeLookupScheduleOptions([]);
      return;
    }
    void (async () => {
      try {
        const schedules = await pipeLookupApi.getSchedules(pipeLookupInput.nps);
        setPipeLookupScheduleOptions(schedules);
        setPipeLookupInput((current) => ({
          ...current,
          schedule: schedules.includes(current.schedule) ? current.schedule : (schedules[0] || '')
        }));
      } catch (e) {
        setError(getErrorMessage(e, 'Failed to load pipe lookup schedule options.'));
      }
    })();
  }, [pipeLookupInput.nps, selectedTool]);

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
    const buckets = REPORT_FLOW.map((group) => ({ ...group, entries: [] as typeof sortedSections }));
    sortedSections.forEach((entry) => {
      const lower = entry.section.sectionTitle.toLowerCase();
      const bucket = buckets.find((b) => b.keywords.some((k) => lower.includes(k))) ?? buckets[0];
      bucket.entries.push(entry);
    });
    return buckets.filter((b) => b.entries.length > 0);
  }, [sortedSections]);

  useEffect(() => {
    setCollapsedSections((current) => {
      const next = { ...current };
      sectionBuckets.forEach((bucket) => {
        if (!(bucket.name in next)) next[bucket.name] = bucket.defaultCollapsed;
      });
      return next;
    });
  }, [sectionBuckets]);


  const toolbarRef = useRef<HTMLDivElement | null>(null);
  const headerRef = useRef<HTMLDivElement | null>(null);
  const workspaceCloseButtonRef = useRef<HTMLButtonElement | null>(null);

  useEffect(() => {
    const updateStickyOffsets = () => {
      const root = document.documentElement;
      const toolbarHeight = toolbarRef.current?.getBoundingClientRect().height ?? 0;
      const headerHeight = headerRef.current?.getBoundingClientRect().height ?? 0;
      root.style.setProperty('--toolbar-height', `${Math.ceil(toolbarHeight)}px`);
      root.style.setProperty('--report-header-height', `${Math.ceil(headerHeight)}px`);
    };

    updateStickyOffsets();

    const observer = typeof ResizeObserver !== 'undefined'
      ? new ResizeObserver(() => updateStickyOffsets())
      : null;

    if (toolbarRef.current && observer) observer.observe(toolbarRef.current);
    if (headerRef.current && observer) observer.observe(headerRef.current);

    window.addEventListener('resize', updateStickyOffsets);

    return () => {
      window.removeEventListener('resize', updateStickyOffsets);
      observer?.disconnect();
    };
  }, [report, template, ieAssistEnabled, isDirty, isSaving, findingsCount, recommendationsCount]);

  useEffect(() => {
    if (!isToolWorkspaceOpen) return;
    workspaceCloseButtonRef.current?.focus();
    const onKeyDown = (event: KeyboardEvent) => {
      if (event.key === 'Escape') {
        setIsToolWorkspaceOpen(false);
      }
    };
    window.addEventListener('keydown', onKeyDown);
    return () => window.removeEventListener('keydown', onKeyDown);
  }, [isToolWorkspaceOpen]);

  const updateHeaderField = (key: keyof InspectionReport, value: string) => {
    if (!report) return;
    const latest = structuredClone(report);
    (latest[key] as string | undefined) = value;
    setReport(latest);
    setIsDirty(true);
  };
  const toolRegistry = [{ id: 'corrosion-rate', label: 'Corrosion Rate' }, { id: 'pipe-lookup', label: 'Pipe Lookup' }, { id: 'b31-3-piping', label: 'B31.3 Piping' }];
  const appendSummaryToActiveField = () => {
    if (!report || !activeField || !calcResult) return;
    const latest = structuredClone(report);
    const answer = latest.sections[activeField.sectionIndex]?.answers?.[activeField.answerIndex];
    if (!answer) return;
    const summary = calcResult.calculationType === 'b31-3-piping-circuit-batch' ? formatCircuitBatchSummary(calcResult as never) : calcResult.insertLabel;
    answer.comment = `${answer.comment ?? ''}${answer.comment ? '\n' : ''}${summary}`.trim();
    setReport(latest);
    setIsDirty(true);
  };

  const saveCalculationSnapshot = () => {
    if (!report || !calcResult) return;
    if (savedCalculationIds.has(calcResult.id)) return;
    const latest = structuredClone(report);
    const activeAnswer = activeField ? latest.sections[activeField.sectionIndex]?.answers?.[activeField.answerIndex] : null;
    const activeSection = activeField ? latest.sections[activeField.sectionIndex] : null;
    const snapshotToSave: InspectionCalculationSnapshot = {
      ...calcResult,
      id: `${calcResult.calculationType}-${new Date().toISOString()}-${Math.random().toString(36).slice(2, 8)}`,
      linkedSectionId: activeSection?.sectionId,
      linkedFieldId: activeAnswer?.fieldId
    };
    latest.calculations = [...(latest.calculations ?? []), snapshotToSave];
    setReport(latest);
    setSavedCalculationIds((current) => new Set(current).add(calcResult.id));
    setIsDirty(true);
  };

  if (!report) return <div className="page">{error || 'Loading API 570 Piping External report...'}</div>;

  return <div className="page report-page api570-report">
    <h1 className="report-page-title">API 570 Piping External - Report Entry</h1>
    <div className="api570-toolbar" ref={toolbarRef}>
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

    <div className="report-header-sticky" key="report-header" ref={headerRef}>
      <div className="report-header-shell">
          <div className="report-header-title">Report Header</div>
          <div className="report-header-grid">
            {HEADER_GRID_COLUMNS.map((column, columnIndex) => <div key={`header-col-${columnIndex}`} className="report-header-column">
              {column.map((field) => <div key={`${field.key}-${field.label}`} className="report-header-field"><span className="report-header-label">{field.label}</span><input className="report-header-input" type={field.key === 'inspectionDate' ? 'date' : 'text'} value={(report[field.key as keyof InspectionReport] as string) || ''} onChange={(e) => updateHeaderField(field.key as keyof InspectionReport, e.target.value)} placeholder={`Enter ${field.label.toLowerCase()}...`} /></div>)}
            </div>)}
          </div>
      </div>
    </div>

    <div className="report-page-body">
      <div className="report-main-column">
        {sectionBuckets.map((bucket) => <div className="api570-accordion" key={bucket.name}>
          <button className="accordion-toggle" onClick={() => setCollapsedSections((s) => ({ ...s, [bucket.name]: !s[bucket.name] }))}>{bucket.name} <span>{collapsedSections[bucket.name] ? '+' : '−'}</span></button>
          {!collapsedSections[bucket.name] && bucket.entries.map(({ section, i: sectionIndex }) => <div className="section-card" key={`${section.sectionId}-${section.instanceNumber ?? 0}`}>
            <h3>{section.sectionTitle}</h3>
            {section.answers.map((answer, answerIndex) => {
              if (DUPLICATE_HEADER_LABELS.has(answer.label.trim().toLowerCase())) return null;
              const suggestions = [...localSuggestionsFor(answer), ...(fieldSuggestions[answer.fieldId] || [])];
              const condition = toCondition(answer);
              const showIssueDetails = condition === 'issue';
              const showMonitorDetails = condition === 'monitor';
              return <div className="inspection-row" key={`${section.sectionId}-${answer.fieldId}-${answerIndex}`}>
                <div><label><strong>{answer.label}</strong></label></div>
                <div><select value={toCondition(answer)} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value === 'na' ? 'N/A' : e.target.value, recommendationRequired: e.target.value === 'issue' ? answer.recommendationRequired ?? false : false, photoRequired: e.target.value === 'issue' ? answer.photoRequired ?? false : false })}><option value="na">N/A</option><option value="acceptable">Acceptable</option><option value="monitor">Monitor</option><option value="issue">Issue</option></select></div>
                <div><textarea placeholder="Notes" value={answer.comment ?? ''} onFocus={() => setActiveField({ sectionIndex, answerIndex })} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { comment: e.target.value })} />
                {suggestions.length > 0 && <ul className="suggestions">{suggestions.map((s, idx) => <li key={`${s.promptType}-${idx}`}><strong>{s.severity.toUpperCase()}:</strong> {s.suggestion}</li>)}</ul>}</div>
                {(showIssueDetails || showMonitorDetails) && <div className="inspection-row-details">
                  {showIssueDetails && <select value={answer.values?.[0] || ''} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { values: [e.target.value] })}><option value="">Severity</option><option value="Low">Low</option><option value="Medium">Medium</option><option value="High">High</option><option value="Critical">Critical</option></select>}
                  <input type="text" value={answer.value ?? ''} placeholder="Location / Component" onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { value: e.target.value })} />
                  {showMonitorDetails && <input type="text" value={answer.values?.[1] || ''} placeholder="Follow-up Note" onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { values: [answer.values?.[0] || '', e.target.value] })} />}
                  {showIssueDetails && <div className="inspection-row-flags"><label><input type="checkbox" checked={!!answer.photoRequired} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { photoRequired: e.target.checked })} /> Photo Required</label><label><input type="checkbox" checked={!!answer.recommendationRequired} onChange={(e) => void updateAnswer(sectionIndex, answerIndex, { recommendationRequired: e.target.checked })} /> Recommendation Required</label></div>}
                </div>}
              </div>;
            })}
          </div>)}
        </div>)}
      </div>
      <aside className="report-sidebar api570-sidebar">
        <div className="assist-alerts-panel">
          <h3>iE Assist &amp; Alerts</h3>
          <section className="sidebar-section">
            <h4>iE Assist</h4>
            <label className="toggle-row assist-toggle-row">
              <span className="assist-toggle-label">iE Assist</span>
              <span className="assist-toggle-control">
                <input type="checkbox" checked={ieAssistEnabled} onChange={(e) => setIeAssistEnabled(e.target.checked)} />
                <span>{ieAssistEnabled ? 'Enabled' : 'Disabled'}</span>
              </span>
            </label>
            <p className="muted">Active inline suggestions are shown within note fields.</p>
          </section>
          <section className="sidebar-section">
            <h4>Active Suggestions</h4>
            <p className="muted">Inline recommendations appear next to inspection note fields as you type.</p>
          </section>
          <section className="sidebar-section">
            <h4>Alerts</h4>
            <button className="refresh-alerts-btn" type="button" onClick={async () => { if (!report) return; const next = await reportingApi.getAlerts(report); setAlerts(next); }}>Refresh Alerts</button>
            {alerts.length === 0 ? <p className="muted">No alerts loaded.</p> : <ul>{alerts.map((a) => <li key={a.id}><strong>{a.severity}:</strong> {a.title}</li>)}</ul>}
          </section>
        </div>
        <div className="sidebar-section">
          <h4>Engineering Tools</h4>
          <select value={selectedTool} onChange={(e) => setSelectedTool(e.target.value)}>
            {toolRegistry.map((tool) => <option key={tool.id} value={tool.id}>{tool.label}</option>)}
          </select>
          <button type="button" onClick={() => setIsToolWorkspaceOpen(true)} disabled={selectedTool !== 'b31-3-piping'}>
            Open Tool Workspace
          </button>
          {selectedTool === 'corrosion-rate' ? <>
            <div className="compact-grid">
              {Object.entries(calcInput).map(([k, v]) => <input key={k} type="number" step="any" value={v} placeholder={k} onChange={(e) => setCalcInput((c) => ({ ...c, [k]: Number(e.target.value) }))} />)}
            </div>
            <button type="button" onClick={() => setCalcResult(calculateCorrosionRate(calcInput))}>Calculate</button>
            {calcResult && <>
              <p className="muted">{calcResult.insertLabel}</p>
              <p className="muted">Warnings: {calcResult.warnings.length}</p>
              <button type="button" onClick={saveCalculationSnapshot} disabled={savedCalculationIds.has(calcResult.id)}>{savedCalculationIds.has(calcResult.id) ? 'Saved to Report' : 'Save to Report Calculations'}</button>
              <button type="button" onClick={appendSummaryToActiveField}>Insert Summary into Active Notes Field</button>
            </>}
          </> : selectedTool === 'pipe-lookup' ? <>
            <div className="compact-grid">
              <select value={pipeLookupInput.nps} onChange={(e) => setPipeLookupInput({ nps: e.target.value, schedule: '' })}>
                <option value="">Select NPS</option>
                {pipeLookupNpsOptions.map((nps) => <option key={nps} value={nps}>{nps}</option>)}
              </select>
              <select value={pipeLookupInput.schedule} onChange={(e) => setPipeLookupInput((current) => ({ ...current, schedule: e.target.value }))} disabled={!pipeLookupInput.nps}>
                <option value="">Select Schedule</option>
                {pipeLookupScheduleOptions.map((schedule) => <option key={schedule} value={schedule}>{schedule}</option>)}
              </select>
            </div>
            <button type="button" disabled={!pipeLookupInput.nps || !pipeLookupInput.schedule || isPipeLookupLoading} onClick={async () => {
              setIsPipeLookupLoading(true);
              try {
                const lookup = await pipeLookupApi.lookup({ nps: pipeLookupInput.nps, schedule: pipeLookupInput.schedule });
                setCalcResult(calculatePipeDimensions({ nps: lookup.nps, schedule: lookup.schedule, outsideDiameter: lookup.outsideDiameter, nominalThickness: lookup.nominalThickness }));
              } catch (e) {
                setError(getErrorMessage(e, 'Pipe lookup failed.'));
              } finally {
                setIsPipeLookupLoading(false);
              }
            }}>{isPipeLookupLoading ? 'Running…' : 'Run Lookup'}</button>
            {calcResult?.calculationType === 'pipe-lookup' && <>
              <p className="muted">OD: {(calcResult.inputs as { outsideDiameter: number }).outsideDiameter.toFixed(4)} in</p>
              <p className="muted">Nominal Thickness: {(calcResult.inputs as { nominalThickness: number }).nominalThickness.toFixed(4)} in</p>
              <p className="muted">Calculated ID: {(calcResult.outputs as { insideDiameter: number }).insideDiameter.toFixed(4)} in</p>
              <p className="muted">Lower wall tolerance (-12.5%): {(calcResult.outputs as { lowerLimitMinus12_5: number }).lowerLimitMinus12_5.toFixed(4)} in</p>
              <p className="muted">Upper wall tolerance (+12.5%): {(calcResult.outputs as { upperLimitPlus12_5: number }).upperLimitPlus12_5.toFixed(4)} in</p>
              <p className="muted">Warnings: {calcResult.warnings.length}</p>
              <button type="button" onClick={saveCalculationSnapshot} disabled={savedCalculationIds.has(calcResult.id)}>{savedCalculationIds.has(calcResult.id) ? 'Saved to Report' : 'Save to Report Calculations'}</button>
              <button type="button" onClick={appendSummaryToActiveField}>Insert Summary into Active Notes Field</button>
            </>}
          </> : <>
            <p className="muted">Use the workspace to run B31.3 circuit Tmin batch tools and save to report calculations.</p>
            {calcResult?.calculationType === 'b31-3-piping-circuit-batch' && <p className="muted">{calcResult.insertLabel}</p>}
          </>}
          <h4>Saved Calculations</h4>
          {(report.calculations ?? []).length === 0 ? <p className="muted">No saved calculations.</p> : <ul>{(report.calculations ?? []).map((c) => <li key={c.id}><strong>{c.displayName}</strong><br />{new Date(c.calculatedAt).toLocaleString()} · {c.insertLabel} · warnings: {c.warnings.length}</li>)}</ul>}
        </div>
      </aside>
    </div>
    {isToolWorkspaceOpen && selectedTool === 'b31-3-piping' && <div className="tool-workspace-backdrop" onClick={() => setIsToolWorkspaceOpen(false)}>
      <div className="tool-workspace-dialog" role="dialog" aria-label="Engineering Tool Workspace" onClick={(e) => e.stopPropagation()}>
        <div className="tool-workspace-header">
          <h3>Engineering Tool Workspace — B31.3 Circuit Tmin Batch</h3>
          <button type="button" ref={workspaceCloseButtonRef} onClick={() => setIsToolWorkspaceOpen(false)}>Close</button>
        </div>
        <div className="sidebar-section"><h4>Circuit Conditions</h4>
          <p className="muted">Design Pressure: {b31SourceMeta.pressure.value ?? 'Not found'} psig — from {b31SourceMeta.pressure.source}</p>
          <p className="muted">Design Temperature: {b31SourceMeta.temperature.value ?? 'Not found'} °F — from {b31SourceMeta.temperature.source}</p>
          {(b31SourceMeta.pressure.value == null || b31SourceMeta.temperature.value == null) && <div>
            <p className="muted">Warning: Circuit pressure/temperature not found; provide one-time manual overrides.</p>
            <input type="number" placeholder="Manual pressure psig" value={b31ManualPressure} onChange={(e)=>setB31ManualPressure(e.target.value===''?'':Number(e.target.value))} />
            <input type="number" placeholder="Manual temperature °F" value={b31ManualTemperature} onChange={(e)=>setB31ManualTemperature(e.target.value===''?'':Number(e.target.value))} />
          </div>}
          <h4>Shared Defaults</h4>
          <div className="compact-grid">
            <input type="text" value={b31Shared.defaultJointType} onChange={(e)=>setB31Shared((c)=>({...c,defaultJointType:e.target.value}))} placeholder="Default Joint Type" />
            <input type="text" value={b31Shared.defaultJointQualityKey} onChange={(e)=>setB31Shared((c)=>({...c,defaultJointQualityKey:e.target.value}))} placeholder="Default Joint Quality Key" />
            <input type="number" step="any" value={b31Shared.wFactor} onChange={(e)=>setB31Shared((c)=>({...c,wFactor:Number(e.target.value)}))} placeholder="W factor" />
            <input type="number" step="any" value={b31Shared.yOverride} onChange={(e)=>setB31Shared((c)=>({...c,yOverride:e.target.value}))} placeholder="Y override (optional)" />
            <input type="number" step="any" value={b31Shared.eOverride} onChange={(e)=>setB31Shared((c)=>({...c,eOverride:e.target.value}))} placeholder="E override (optional)" />
          </div>
        </div>
        <h4>Editable Pipe Rows</h4>
        {b31Rows.map((row, idx)=><div key={row.id} className="compact-grid">
          <input value={row.nps} placeholder="NPS" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,nps:e.target.value}:r))} />
          <input value={row.schedule ?? ''} placeholder="Schedule" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,schedule:e.target.value}:r))} />
          <select value={row.materialPreset ?? 'CUSTOM'} onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?applyMaterialPreset(r, e.target.value as 'A106_GR_B_SEAMLESS'|'A53_GR_B_ERW_EB'|'CUSTOM'):r))}>
            {B31_MATERIAL_PRESETS.map((preset)=><option key={preset.key} value={preset.key}>{preset.label}</option>)}
          </select>
          <input value={row.spec} placeholder="Spec" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,spec:e.target.value}:r))} />
          <input value={row.grade} placeholder="Grade" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,grade:e.target.value}:r))} />
          <input value={row.productForm} placeholder="Product Form" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,productForm:e.target.value}:r))} />
          <input value={row.materialCategory} placeholder="Material Category" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,materialCategory:e.target.value}:r))} />
          <input value={row.jointType ?? ''} placeholder="Joint Type" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,jointType:e.target.value}:r))} />
          <input value={row.jointQualityKey ?? ''} placeholder="Joint Quality Key" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,jointQualityKey:e.target.value}:r))} />
          <input value={row.note ?? ''} placeholder="Note / Location (optional)" onChange={(e)=>setB31Rows((rows)=>rows.map((r)=>r.id===row.id?{...r,note:e.target.value}:r))} />
          <button type="button" onClick={()=>setB31Rows((rows)=>rows.filter((r)=>r.id!==row.id))}>Remove</button>
          <button type="button" onClick={()=>setB31Rows((rows)=>{const target=rows[idx]; return [...rows.slice(0,idx+1),{...target,id:crypto.randomUUID()},...rows.slice(idx+1)]})}>Duplicate</button>
        </div>)}
        <button type="button" onClick={()=>setB31Rows((rows)=>[...rows,{ id: crypto.randomUUID(), nps: '', materialPreset: 'A106_GR_B_SEAMLESS', schedule: '', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: b31Shared.defaultJointType, jointQualityKey: b31Shared.defaultJointQualityKey, note: '' }])}>Add Row</button>
            <button type="button" disabled={isB31BatchRunning} onClick={async ()=>{
              setIsB31BatchRunning(true);
              try {
                const pressure = b31SourceMeta.pressure.value ?? (b31ManualPressure === '' ? null : b31ManualPressure);
                const temperature = b31SourceMeta.temperature.value ?? (b31ManualTemperature === '' ? null : b31ManualTemperature);
                if (pressure == null || temperature == null) { setError('Circuit pressure and temperature are required.'); return; }
                const sourceMeta = {
                  pressure: { ...b31SourceMeta.pressure, value: pressure, isManual: b31SourceMeta.pressure.value == null },
                  temperature: { ...b31SourceMeta.temperature, value: temperature, isManual: b31SourceMeta.temperature.value == null }
                };
                const rowResults = await Promise.all(b31Rows.map(async (row) => {
                  const warnings:string[] = [];
                  let od = NPS_OD_LOOKUP[row.nps] ?? null;
                  if (row.schedule) {
                    try { const lu = await pipeLookupApi.lookup({ nps: row.nps, schedule: row.schedule }); od = lu.outsideDiameter; } catch { warnings.push('Pipe lookup failed; fallback OD used.'); }
                  }
                  if (!od) return mapB313RowResult(row.id, row, null, null, [...warnings, 'Unable to resolve outside diameter.']);
                  const input = { pressurePsi: pressure, temperatureF: temperature, outsideDiameterIn: od, spec: row.spec, grade: row.grade, productForm: row.productForm, unsNo: row.unsNo ?? '', classConditionTemper: row.classConditionTemper ?? '', materialCategory: row.materialCategory, jointType: row.jointType ?? b31Shared.defaultJointType, jointQualityKey: row.jointQualityKey ?? b31Shared.defaultJointQualityKey, wFactor: b31Shared.wFactor, yOverride: b31Shared.yOverride === '' ? null : Number(b31Shared.yOverride), eOverride: b31Shared.eOverride === '' ? null : Number(b31Shared.eOverride) };
                  try { const res = await b313Api.calculateThickness(input); return mapB313RowResult(row.id, {...row, outsideDiameterIn: od}, input, res, warnings); } catch (e) { return mapB313RowResult(row.id, {...row, outsideDiameterIn: od}, input, { success:false, message:getErrorMessage(e,'Calculation failed.'), allowableStressPsi:null,eFactor:null,yCoefficient:null,wFactor:null,requiredThicknessIn:null }, warnings); }
                }));
                setCalcResult(buildCircuitBatchSnapshot({ pressurePsi: pressure, temperatureF: temperature, sourceMetadata: sourceMeta, wFactor: b31Shared.wFactor, yOverride: b31Shared.yOverride===''?null:Number(b31Shared.yOverride), eOverride: b31Shared.eOverride===''?null:Number(b31Shared.eOverride), defaultJointType: b31Shared.defaultJointType, defaultJointQualityKey: b31Shared.defaultJointQualityKey }, rowResults));
              } finally { setIsB31BatchRunning(false); }
            }}>{isB31BatchRunning ? 'Running…' : 'Run Batch Calculation'}</button>
        {calcResult?.calculationType === 'b31-3-piping-circuit-batch' && <>
              <p className="muted">{calcResult.insertLabel}</p>
              <div className="tool-results-table-wrap"><table className="tool-results-table">
                <thead><tr><th>NPS</th><th>Material</th><th>OD</th><th>Allowable Stress</th><th>E</th><th>Y</th><th>W</th><th>Required Tmin</th><th>Status/Message</th></tr></thead>
                <tbody>{toResultDisplayRows((calcResult.outputs as { rows: never[] }).rows as never[]).map((row, index)=><tr key={`${row.nps}-${index}`}><td>{row.nps}</td><td>{row.material}</td><td>{row.outsideDiameterIn?.toFixed?.(4) ?? 'N/A'}</td><td>{row.allowableStressPsi ?? 'N/A'}</td><td>{row.eFactor ?? 'N/A'}</td><td>{row.yCoefficient ?? 'N/A'}</td><td>{row.wFactor ?? 'N/A'}</td><td>{row.requiredThicknessIn?.toFixed?.(4) ?? 'N/A'}</td><td>{row.statusMessage}</td></tr>)}</tbody>
              </table></div>
              <button type="button" onClick={saveCalculationSnapshot} disabled={savedCalculationIds.has(calcResult.id)}>{savedCalculationIds.has(calcResult.id) ? 'Saved to Report' : 'Save to Report Calculations'}</button>
              <button type="button" onClick={appendSummaryToActiveField}>Insert Summary into Active Notes Field</button>
            </>}
      </div>
    </div>}
  </div>;
}
