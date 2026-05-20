import { Fragment, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import {
  API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY,
  buildApi510RemainingExchangerExternalDraftPayload,
  isApi510RemainingExchangerExternalReportTypeId,
  readApi510RemainingExchangerExternalDraftPayload
} from './api510RemainingExchangerReportDraft';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, readApiInspectionDraftSetup } from './componentCalculationPrefill'
import { buildChecklistCounts } from './externalReportChecklistUtils';
import type {
  Api510RemainingExchangerExternalChecklistField,
  Api510RemainingExchangerExternalComponentState,
  Api510RemainingExchangerExternalDraftPayload,
  Api510RemainingExchangerExternalReportTypeId,
  Api510RemainingExchangerExternalSummaryFieldsDraft
} from '../types';

export { API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY };

type ChecklistField = Api510RemainingExchangerExternalChecklistField;
type ComponentReportState = Api510RemainingExchangerExternalComponentState;

const exchangerConfigs: Record<Api510RemainingExchangerExternalReportTypeId, {
  title: string;
  description: string;
  defaultComponents: string[];
  designFields: readonly (readonly [string, string])[];
}> = {
  'api510.external.exchanger.plate-frame': {
    title: 'API 510 Plate and Frame Exchanger External Report',
    description: 'External inspection report-entry page for Plate and Frame Exchanger equipment.',
    defaultComponents: ['Frame Head', 'Pressure Plate', 'Plate Pack External', 'Ports / Nozzles', 'Tie Bolts'],
    designFields: [
      ['hotSideDesignPressure', 'Hot Side Design Pressure'],
      ['hotSideDesignTemperature', 'Hot Side Design Temperature'],
      ['coldSideDesignPressure', 'Cold Side Design Pressure'],
      ['coldSideDesignTemperature', 'Cold Side Design Temperature']
    ]
  },
  'api510.external.exchanger.double-pipe': {
    title: 'API 510 Double Pipe Exchanger External Report',
    description: 'External inspection report-entry page for Double Pipe Exchanger equipment.',
    defaultComponents: ['Inner Pipe External', 'Outer Pipe', 'Return Bends', 'Nozzles'],
    designFields: [
      ['innerPipeSideDesignPressure', 'Inner Pipe Side Design Pressure'],
      ['innerPipeSideDesignTemperature', 'Inner Pipe Side Design Temperature'],
      ['annulusOuterPipeSideDesignPressure', 'Annulus / Outer Pipe Side Design Pressure'],
      ['annulusOuterPipeSideDesignTemperature', 'Annulus / Outer Pipe Side Design Temperature']
    ]
  },
  'api510.external.exchanger.air-cooler': {
    title: 'API 510 Air Cooler / Fin Fan External Report',
    description: 'External inspection report-entry page for Air Cooler / Fin Fan equipment.',
    defaultComponents: ['Header Box', 'Tube Bundle External', 'Nozzles', 'Frame / Supports'],
    designFields: [
      ['tubeSideDesignPressure', 'Tube Side Design Pressure'],
      ['tubeSideDesignTemperature', 'Tube Side Design Temperature'],
      ['headerBoxDesignPressure', 'Header Box Design Pressure'],
      ['headerBoxDesignTemperature', 'Header Box Design Temperature']
    ]
  }
};

const reportSections = [
  'Report Header',
  'Design Conditions',
  'Materials',
  'Components',
  'External Condition',
  'Coating / Corrosion',
  'Leakage / Staining',
  'Supports / Attachments',
  'CML / Thickness Review',
  'Calculations / Review Notes',
  'Findings',
  'Recommendations',
  'Photos',
  'NDE / Testing',
  'Return to Service'
] as const;

const headerFields = [
  ['inspectionDate', 'Inspection Date'],
  ['inspector', 'Inspector'],
  ['clientFacility', 'Client / Facility'],
  ['unitArea', 'Unit / Area'],
  ['equipmentIdentifier', 'Equipment Tag'],
  ['service', 'Service'],
  ['inspectionReason', 'Inspection Reason'],
  ['operatingStatus', 'Operating Status'],
  ['inspectionScope', 'Inspection Scope']
] as const;

const materialFields = [
  ['primaryMaterialSpec', 'Primary Material Spec'],
  ['primaryMaterialGrade', 'Primary Material Grade'],
  ['primaryProductForm', 'Primary Product Form'],
  ['secondaryMaterialSpec', 'Secondary Material Spec'],
  ['secondaryMaterialGrade', 'Secondary Material Grade'],
  ['secondaryProductForm', 'Secondary Product Form']
] as const;

const conditionFields = [
  ['generalExternalCondition', 'General External Condition'],
  ['coatingCorrosionNotes', 'Coating / Corrosion Notes'],
  ['leakageStainingNotes', 'Leakage / Staining Notes'],
  ['supportsAttachmentsNotes', 'Supports / Attachments Notes'],
  ['cmlThicknessReviewNotes', 'CML / Thickness Review Notes']
] as const;

const checklistFields = ['Create Finding', 'Recommendation Required', 'Repair Required', 'Photo Required', 'NDE Required', 'Add to Summary'] as const;
const summaryChecklistFields = checklistFields.filter((field) => field !== 'Add to Summary');
const emptyChecklist = (): Record<ChecklistField, boolean> => Object.fromEntries(checklistFields.map((field) => [field, false])) as Record<ChecklistField, boolean>;
const emptyComponentState = (): ComponentReportState => ({ condition: '', location: '', findingNotes: '', recommendationText: '', photoTag: '', checklist: emptyChecklist() });
const fieldValue = (values: Record<string, string> | undefined, key: string) => values?.[key] || '';
const sectionId = (section: string) => section.toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/^-|-$/g, '');
const componentAnchor = (component: string) => `component-${sectionId(component)}`;

function ReadOnlyField({ label, value }: { label: string; value: string }) {
  return <div className="report-review-field api510-compact-field"><dt>{label}</dt><dd>{value || '—'}</dd></div>;
}

function DraftFieldList({ fields, values }: { fields: readonly (readonly [string, string])[]; values: Record<string, string> | undefined }) {
  return <dl className="api510-field-card-grid">{fields.map(([key, label]) => <ReadOnlyField key={key} label={label} value={fieldValue(values, key)} />)}</dl>;
}

function TextAreaField({ label, value, onChange }: { label: string; value: string; onChange: (value: string) => void }) {
  return <label className="wizard-field"><span>{label}</span><textarea rows={3} value={value} onChange={(event) => onChange(event.target.value)} placeholder={`Enter ${label.toLowerCase()}`} /></label>;
}

function ComponentSection({ component, variant, state, onChange }: { component: string; variant: 'default' | 'optional'; state: ComponentReportState; onChange: (next: ComponentReportState) => void }) {
  const setField = (key: keyof Omit<ComponentReportState, 'checklist'>, value: string) => onChange({ ...state, [key]: value });
  const setChecklist = (field: ChecklistField, checked: boolean) => onChange({ ...state, checklist: { ...state.checklist, [field]: checked } });

  return (
    <details id={componentAnchor(component)} className={`card wizard-setup-card api510-component-card api510-component-card-${variant}`} aria-label={`${component} Component Section`} open>
      <summary><h4>{component}</h4><span className="badge">{variant === 'default' ? 'Default' : 'Optional'}</span></summary>
      <div className="wizard-field-grid">
        <label className="wizard-field"><span>Condition</span><select aria-label={`${component} Condition`} value={state.condition} onChange={(event) => setField('condition', event.target.value)}><option value="">Select condition</option><option>Acceptable</option><option>Monitor</option><option>Repair Required</option><option>NDE Required</option></select></label>
        <label className="wizard-field"><span>Location</span><input aria-label={`${component} Location`} value={state.location} onChange={(event) => setField('location', event.target.value)} placeholder="Location / orientation" /></label>
        <TextAreaField label="Finding Notes" value={state.findingNotes} onChange={(value) => setField('findingNotes', value)} />
        <TextAreaField label="Recommendation Text" value={state.recommendationText} onChange={(value) => setField('recommendationText', value)} />
        <label className="wizard-field"><span>Photo Tag</span><input aria-label={`${component} Photo Tag`} value={state.photoTag} onChange={(event) => setField('photoTag', event.target.value)} placeholder="Photo reference" /></label>
      </div>
      <div className="wizard-component-list" aria-label={`${component} Checklist`}>
        {checklistFields.map((field) => <label key={field}><input type="checkbox" aria-label={`${component} ${field}`} checked={state.checklist[field]} onChange={(event) => setChecklist(field, event.target.checked)} />{field}</label>)}
      </div>
    </details>
  );
}

export function Api510RemainingExchangerExternalReportPage() {
  const draft = useMemo(readApiInspectionDraftSetup, []);
  const currentReportTypeId = isApi510RemainingExchangerExternalReportTypeId(draft?.reportTypeId) ? draft.reportTypeId : 'api510.external.exchanger.plate-frame';
  const config = exchangerConfigs[currentReportTypeId];
  const [savedDraftPayload, setSavedDraftPayload] = useState<Api510RemainingExchangerExternalDraftPayload | undefined>(() => readApi510RemainingExchangerExternalDraftPayload());
  const [lastSavedAt, setLastSavedAt] = useState(() => savedDraftPayload?.savedAt ?? '');
  const components = useMemo(() => Array.from(new Set([...config.defaultComponents, ...(draft?.reportTypeId === currentReportTypeId ? draft.components : [])])), [config.defaultComponents, currentReportTypeId, draft]);
  const [componentStates, setComponentStates] = useState<Record<string, ComponentReportState>>(() => Object.fromEntries(components.map((component) => [component, savedDraftPayload?.componentStates[component] ?? emptyComponentState()])));
  const [summaryFields, setSummaryFields] = useState<Api510RemainingExchangerExternalSummaryFieldsDraft>(() => savedDraftPayload?.summaryFields ?? {
    generalExternalCondition: '', coatingCorrosionNotes: '', leakageStainingNotes: '', supportsAttachmentsNotes: '', cmlThicknessReviewNotes: '', coatingCorrosionDetails: '', leakageStainingDetails: '', supportsAttachmentsDetails: '', cmlThicknessReviewDetails: '', calculationReviewNotes: '', findingSummary: ''
  });
  const [photoLog, setPhotoLog] = useState(() => savedDraftPayload?.photos.photoLog ?? '');
  const [ndeTesting, setNdeTesting] = useState(() => savedDraftPayload?.ndeTesting.requirementsAndResults ?? '');
  const [recommendationSummary, setRecommendationSummary] = useState(() => savedDraftPayload?.recommendations.summary ?? '');
  const [returnToServiceStatus, setReturnToServiceStatus] = useState(() => savedDraftPayload?.returnToService.status ?? '');
  const [returnToServiceNotes, setReturnToServiceNotes] = useState(() => savedDraftPayload?.returnToService.notes ?? '');

  const checklistCounts = useMemo(() => buildChecklistCounts(summaryChecklistFields, components, (component, field) => Boolean(componentStates[component]?.checklist[field])), [componentStates, components]);
  const draftPayloadPreview = useMemo(() => buildApi510RemainingExchangerExternalDraftPayload({ reportTypeId: currentReportTypeId, sourceStartWizardDraft: draft, sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, components, componentStates, checklistFields, summaryFields, photos: { photoLog }, ndeTesting: { requirementsAndResults: ndeTesting }, recommendations: { summary: recommendationSummary }, returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes }, savedAt: lastSavedAt || 'Not saved locally' }), [componentStates, components, currentReportTypeId, draft, lastSavedAt, ndeTesting, photoLog, recommendationSummary, returnToServiceNotes, returnToServiceStatus, summaryFields]);

  const handleComponentChange = (component: string, next: ComponentReportState) => setComponentStates((current) => ({ ...current, [component]: next }));
  const handleSaveLocalDraft = () => {
    const savedAt = new Date().toISOString();
    const payload = buildApi510RemainingExchangerExternalDraftPayload({ reportTypeId: currentReportTypeId, sourceStartWizardDraft: draft, sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, components, componentStates, checklistFields, summaryFields, photos: { photoLog }, ndeTesting: { requirementsAndResults: ndeTesting }, recommendations: { summary: recommendationSummary }, returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes }, savedAt });
    window.localStorage.setItem(API510_REMAINING_EXCHANGER_EXTERNAL_REPORT_LOCAL_STORAGE_KEY, JSON.stringify(payload));
    setSavedDraftPayload(payload);
    setLastSavedAt(savedAt);
  };

  if (draft && !isApi510RemainingExchangerExternalReportTypeId(draft.reportTypeId)) {
    return <section className="page report-page"><h2>API 510 Remaining Exchanger External Report</h2><p role="alert">The saved draft setup is not for a supported remaining API 510 External Exchanger report.</p><Link to="/reports">Return to Start Wizard</Link></section>;
  }

  return (
    <section className="page report-page api510-shell-tube-external-report">
      <header className="api510-report-header-sticky" aria-label="Sticky report header"><div><h2>{config.title}</h2><p>{config.description}</p></div><div className="api510-header-actions"><button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button><button type="button" disabled title="Word export will be connected after report persistence is finalized.">Export Word Report</button></div></header>
      {!draft && <p role="status" className="wizard-note">No Start Wizard draft setup was found in {API_INSPECTION_DRAFT_SETUP_STORAGE_KEY}; Plate and Frame blank report fields are shown.</p>}
      <div className="api510-report-layout">
        <main className="api510-report-main">
          <nav className="card api510-section-nav" aria-label="Report sections"><strong>Section navigation</strong><ul>{reportSections.map((section) => <li key={section}><a href={`#${sectionId(section)}`}>{section}</a></li>)}</ul></nav>
          <section id="report-header" className="card wizard-setup-card api510-compact-card" aria-label="Report Header"><h3>Report Header</h3><DraftFieldList fields={headerFields} values={draft?.header} /></section>
          <section id="design-conditions" className="card wizard-setup-card api510-compact-card" aria-label="Design Conditions"><h3>Design Conditions</h3><DraftFieldList fields={config.designFields} values={draft?.header} /></section>
          <section id="materials" className="card wizard-setup-card api510-compact-card" aria-label="Materials"><h3>Materials</h3><DraftFieldList fields={materialFields} values={draft?.header} /></section>
          <section id="components" aria-label="Components" className="card wizard-setup-card"><div className="api510-section-heading-row"><div><h3>Components</h3><p className="muted">{components.length} component(s) selected for this external report.</p></div><nav aria-label="Component quick links"><ul className="api510-component-links">{components.map((component) => <li key={component}><a href={`#${componentAnchor(component)}`}>{component}</a></li>)}</ul></nav></div><div className="wizard-card-grid api510-component-grid">{components.map((component) => <ComponentSection key={component} component={component} variant={config.defaultComponents.includes(component) ? 'default' : 'optional'} state={componentStates[component] ?? emptyComponentState()} onChange={(next) => handleComponentChange(component, next)} />)}</div></section>
          <section id="external-condition" className="card wizard-setup-card" aria-label="External Condition"><h3>External Condition</h3><div className="wizard-field-grid">{conditionFields.map(([key, label]) => <TextAreaField key={key} label={label} value={summaryFields[key]} onChange={(value) => setSummaryFields((current) => ({ ...current, [key]: value }))} />)}</div></section>
          <section id="coating-corrosion" className="card wizard-setup-card" aria-label="Coating / Corrosion"><h3>Coating / Corrosion</h3><TextAreaField label="Coating / Corrosion Details" value={summaryFields.coatingCorrosionDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, coatingCorrosionDetails: value }))} /></section>
          <section id="leakage-staining" className="card wizard-setup-card" aria-label="Leakage / Staining"><h3>Leakage / Staining</h3><TextAreaField label="Leakage / Staining Details" value={summaryFields.leakageStainingDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, leakageStainingDetails: value }))} /></section>
          <section id="supports-attachments" className="card wizard-setup-card" aria-label="Supports / Attachments"><h3>Supports / Attachments</h3><TextAreaField label="Supports / Attachments Details" value={summaryFields.supportsAttachmentsDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, supportsAttachmentsDetails: value }))} /></section>
          <section id="cml-thickness-review" className="card wizard-setup-card" aria-label="CML / Thickness Review"><h3>CML / Thickness Review</h3><TextAreaField label="CML / Thickness Review Details" value={summaryFields.cmlThicknessReviewDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, cmlThicknessReviewDetails: value }))} /></section>
          <section id="calculations-review-notes" className="card wizard-setup-card" aria-label="Calculations / Review Notes"><h3>Calculations / Review Notes</h3><p>Calculation workspace coming soon for this exchanger type.</p><TextAreaField label="Calculation Review Notes" value={summaryFields.calculationReviewNotes} onChange={(value) => setSummaryFields((current) => ({ ...current, calculationReviewNotes: value }))} /></section>
          <section id="findings" className="card wizard-setup-card" aria-label="Findings"><h3>Findings</h3><TextAreaField label="Finding Summary" value={summaryFields.findingSummary} onChange={(value) => setSummaryFields((current) => ({ ...current, findingSummary: value }))} /></section>
          <section id="recommendations" className="card wizard-setup-card" aria-label="Recommendations"><h3>Recommendations</h3><TextAreaField label="Recommendation Summary" value={recommendationSummary} onChange={setRecommendationSummary} /></section>
          <section id="photos" className="card wizard-setup-card" aria-label="Photos"><h3>Photos</h3><label className="wizard-field"><span>Photo Log</span><textarea rows={3} value={photoLog} onChange={(event) => setPhotoLog(event.target.value)} placeholder="Photo tags and descriptions" /></label></section>
          <section id="nde-testing" className="card wizard-setup-card" aria-label="NDE / Testing"><h3>NDE / Testing</h3><TextAreaField label="NDE / Testing Requirements and Results" value={ndeTesting} onChange={setNdeTesting} /></section>
          <section id="return-to-service" className="card wizard-setup-card" aria-label="Return to Service"><h3>Return to Service</h3><div className="wizard-field-grid"><label className="wizard-field"><span>Return to Service Status</span><select aria-label="Return to Service Status" value={returnToServiceStatus} onChange={(event) => setReturnToServiceStatus(event.target.value)}><option value="">Select status</option><option>Acceptable for continued service</option><option>Acceptable with recommendations</option><option>Hold for repair / engineering review</option></select></label><TextAreaField label="Return to Service Notes" value={returnToServiceNotes} onChange={setReturnToServiceNotes} /></div></section>
        </main>
        <aside className="api510-report-sidebar" aria-label="Report sidebar">
          <section className="card" aria-label="Report Actions"><h3>Report Actions</h3><button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button><button type="button" disabled>Export Word Report</button><p className="wizard-note">Word export will be connected after report persistence is finalized.</p><p role="status">Last Saved: {lastSavedAt ? new Date(lastSavedAt).toLocaleString() : 'Not saved locally'}</p></section>
          <section className="card" aria-label="iE Assist"><h3>iE Assist</h3><p className="wizard-note">Assistant review hooks will use the local report draft until backend report persistence is finalized.</p></section>
          <section className="card" aria-label="Report Summary Preview"><h3>Report Summary Preview</h3><dl className="api510-summary-list"><dt>Selected Components</dt><dd>{components.length}</dd><dt>Calculation Status</dt><dd>Review-only</dd>{summaryChecklistFields.map((field) => <Fragment key={field}><dt>{field}</dt><dd>{checklistCounts[field]}</dd></Fragment>)}</dl><ul aria-label="Selected component preview">{components.map((component) => <li key={component}>{component}</li>)}</ul></section>
          <section className="card" aria-label="Draft Payload Debug"><details><summary>View Draft Payload</summary><pre aria-label="API 510 remaining exchanger draft payload">{JSON.stringify(draftPayloadPreview, null, 2)}</pre></details></section>
          <section className="card" aria-label="Summary warnings"><h3>Summary Warnings</h3><ul><li>Calculation workspace coming soon for this exchanger type.</li>{!lastSavedAt && <li>Local report draft has not been saved.</li>}</ul></section>
        </aside>
      </div>
    </section>
  );
}
