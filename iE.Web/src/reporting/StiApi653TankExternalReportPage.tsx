import { Fragment, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import {
  STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY,
  STI_API653_TANK_EXTERNAL_REPORT_TYPE_ID,
  buildStiApi653TankExternalDraftPayload,
  readStiApi653TankExternalDraftPayload
} from './stiApi653TankReportDraft';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, readApiInspectionDraftSetup } from './componentCalculationPrefill';
import type {
  StiApi653TankExternalChecklistField,
  StiApi653TankExternalComponentState,
  StiApi653TankExternalDraftPayload,
  StiApi653TankExternalSummaryFieldsDraft
} from '../types';

export { STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY };

type ChecklistField = StiApi653TankExternalChecklistField;
type ComponentReportState = StiApi653TankExternalComponentState;

const reportSections = [
  'Report Header',
  'Tank Details',
  'Foundation',
  'Shell',
  'Roof',
  'Shell-to-Bottom / Chime Area',
  'Nozzles / Appurtenances',
  'Vents',
  'Coating',
  'Containment / Release Prevention',
  'Overfill / Spill Prevention',
  'Anchor Bolts / Grounding',
  'Stairs / Platforms / Ladders',
  'External Leaks / Staining',
  'Findings',
  'Recommendations',
  'Photos',
  'NDE / Testing',
  'Return to Service'
] as const;

const checklistSections = reportSections.filter((section) => !['Report Header', 'Tank Details', 'Findings', 'Recommendations', 'Photos', 'NDE / Testing', 'Return to Service'].includes(section));

const headerFields = [
  ['inspectionDate', 'Inspection Date'],
  ['inspector', 'Inspector'],
  ['clientFacility', 'Client / Facility'],
  ['unitArea', 'Unit / Area'],
  ['equipmentIdentifier', 'Equipment Tag / Tank ID'],
  ['service', 'Service'],
  ['inspectionReason', 'Inspection Reason'],
  ['operatingStatus', 'Operating Status'],
  ['inspectionScope', 'Inspection Scope']
] as const;

const tankDetailFields = [
  ['tankId', 'Tank ID'],
  ['tankService', 'Tank Service'],
  ['tankConstruction', 'Tank Construction Standard']
] as const;

const notesFieldBySection: Record<string, keyof StiApi653TankExternalSummaryFieldsDraft> = {
  Foundation: 'foundationNotes',
  Shell: 'shellNotes',
  Roof: 'roofNotes',
  'Shell-to-Bottom / Chime Area': 'chimeAreaNotes',
  'Nozzles / Appurtenances': 'nozzlesAppurtenancesNotes',
  Vents: 'ventsNotes',
  Coating: 'coatingNotes',
  'Containment / Release Prevention': 'containmentReleasePreventionNotes',
  'Overfill / Spill Prevention': 'overfillSpillPreventionNotes',
  'Anchor Bolts / Grounding': 'anchorBoltsGroundingNotes',
  'Stairs / Platforms / Ladders': 'stairsPlatformsLaddersNotes',
  'External Leaks / Staining': 'externalLeaksStainingNotes'
};

const checklistFields = ['Create Finding', 'Recommendation Required', 'Repair Required', 'Photo Required', 'NDE Required', 'Add to Summary'] as const;
const summaryChecklistFields = checklistFields.filter((field) => field !== 'Add to Summary');
const emptyChecklist = (): Record<ChecklistField, boolean> => Object.fromEntries(checklistFields.map((field) => [field, false])) as Record<ChecklistField, boolean>;
const emptyComponentState = (): ComponentReportState => ({ condition: '', location: '', findingNotes: '', recommendationText: '', photoTag: '', checklist: emptyChecklist() });
const fieldValue = (values: Record<string, string> | undefined, key: string) => values?.[key] || '';
const sectionId = (section: string) => section.toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/^-|-$/g, '');

function ReadOnlyField({ label, value }: { label: string; value: string }) {
  return <div className="report-review-field api510-compact-field"><dt>{label}</dt><dd>{value || '—'}</dd></div>;
}

function DraftFieldList({ fields, values }: { fields: readonly (readonly [string, string])[]; values: Record<string, string> | undefined }) {
  return <dl className="api510-field-card-grid">{fields.map(([key, label]) => <ReadOnlyField key={key} label={label} value={fieldValue(values, key)} />)}</dl>;
}

function TextAreaField({ label, value, onChange }: { label: string; value: string; onChange: (value: string) => void }) {
  return <label className="wizard-field"><span>{label}</span><textarea rows={3} value={value} onChange={(event) => onChange(event.target.value)} placeholder={`Enter ${label.toLowerCase()}`} /></label>;
}

function ChecklistSection({ section, state, notes, onNotesChange, onChange }: { section: string; state: ComponentReportState; notes: string; onNotesChange: (value: string) => void; onChange: (next: ComponentReportState) => void }) {
  const setField = (key: keyof Omit<ComponentReportState, 'checklist'>, value: string) => onChange({ ...state, [key]: value });
  const setChecklist = (field: ChecklistField, checked: boolean) => onChange({ ...state, checklist: { ...state.checklist, [field]: checked } });

  return (
    <section id={sectionId(section)} className="card wizard-setup-card api510-component-card" aria-label={section}>
      <h3>{section}</h3>
      <div className="wizard-field-grid">
        <label className="wizard-field"><span>Condition</span><select aria-label={`${section} Condition`} value={state.condition} onChange={(event) => setField('condition', event.target.value)}><option value="">Select condition</option><option>Acceptable</option><option>Monitor</option><option>Repair Required</option><option>NDE Required</option></select></label>
        <label className="wizard-field"><span>Location</span><input aria-label={`${section} Location`} value={state.location} onChange={(event) => setField('location', event.target.value)} placeholder="Location / orientation" /></label>
        <TextAreaField label="Finding Notes" value={state.findingNotes} onChange={(value) => setField('findingNotes', value)} />
        <TextAreaField label="Recommendation Text" value={state.recommendationText} onChange={(value) => setField('recommendationText', value)} />
        <label className="wizard-field"><span>Photo Tag</span><input aria-label={`${section} Photo Tag`} value={state.photoTag} onChange={(event) => setField('photoTag', event.target.value)} placeholder="Photo reference" /></label>
        <TextAreaField label={`${section} Notes`} value={notes} onChange={onNotesChange} />
      </div>
      <div className="wizard-component-list" aria-label={`${section} Checklist`}>
        {checklistFields.map((field) => <label key={field}><input type="checkbox" aria-label={`${section} ${field}`} checked={state.checklist[field]} onChange={(event) => setChecklist(field, event.target.checked)} />{field}</label>)}
      </div>
    </section>
  );
}

export function StiApi653TankExternalReportPage() {
  const draft = useMemo(readApiInspectionDraftSetup, []);
  const [savedDraftPayload, setSavedDraftPayload] = useState<StiApi653TankExternalDraftPayload | undefined>(() => readStiApi653TankExternalDraftPayload());
  const [lastSavedAt, setLastSavedAt] = useState(() => savedDraftPayload?.savedAt ?? '');
  const [componentStates, setComponentStates] = useState<Record<string, ComponentReportState>>(() => Object.fromEntries(checklistSections.map((section) => [section, savedDraftPayload?.componentStates[section] ?? emptyComponentState()])));
  const [summaryFields, setSummaryFields] = useState<StiApi653TankExternalSummaryFieldsDraft>(() => savedDraftPayload?.summaryFields ?? {
    foundationNotes: '', shellNotes: '', roofNotes: '', chimeAreaNotes: '', nozzlesAppurtenancesNotes: '', ventsNotes: '', coatingNotes: '', containmentReleasePreventionNotes: '', overfillSpillPreventionNotes: '', anchorBoltsGroundingNotes: '', stairsPlatformsLaddersNotes: '', externalLeaksStainingNotes: '', findingSummary: ''
  });
  const [photoLog, setPhotoLog] = useState(() => savedDraftPayload?.photos.photoLog ?? '');
  const [ndeTesting, setNdeTesting] = useState(() => savedDraftPayload?.ndeTesting.requirementsAndResults ?? '');
  const [recommendationSummary, setRecommendationSummary] = useState(() => savedDraftPayload?.recommendations.summary ?? '');
  const [returnToServiceStatus, setReturnToServiceStatus] = useState(() => savedDraftPayload?.returnToService.status ?? '');
  const [returnToServiceNotes, setReturnToServiceNotes] = useState(() => savedDraftPayload?.returnToService.notes ?? '');
  const checklistCounts = useMemo(() => Object.fromEntries(summaryChecklistFields.map((field) => [field, checklistSections.filter((section) => componentStates[section]?.checklist[field]).length])) as Record<(typeof summaryChecklistFields)[number], number>, [componentStates]);
  const draftPayloadPreview = useMemo(() => buildStiApi653TankExternalDraftPayload({ sourceStartWizardDraft: draft, sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, sections: [...checklistSections], componentStates, checklistFields, summaryFields, photos: { photoLog }, ndeTesting: { requirementsAndResults: ndeTesting }, recommendations: { summary: recommendationSummary }, returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes }, savedAt: lastSavedAt || 'Not saved locally' }), [componentStates, draft, lastSavedAt, ndeTesting, photoLog, recommendationSummary, returnToServiceNotes, returnToServiceStatus, summaryFields]);

  const handleSaveLocalDraft = () => {
    const savedAt = new Date().toISOString();
    const payload = buildStiApi653TankExternalDraftPayload({ sourceStartWizardDraft: draft, sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, sections: [...checklistSections], componentStates, checklistFields, summaryFields, photos: { photoLog }, ndeTesting: { requirementsAndResults: ndeTesting }, recommendations: { summary: recommendationSummary }, returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes }, savedAt });
    window.localStorage.setItem(STI_API653_TANK_EXTERNAL_REPORT_LOCAL_STORAGE_KEY, JSON.stringify(payload));
    setSavedDraftPayload(payload);
    setLastSavedAt(savedAt);
  };

  if (draft && draft.reportTypeId !== STI_API653_TANK_EXTERNAL_REPORT_TYPE_ID) {
    return <section className="page report-page"><h2>STI / API 653 Tank External Report</h2><p role="alert">The saved draft setup is not for an STI / API 653 Tank External report.</p><Link to="/reports">Return to Start Wizard</Link></section>;
  }

  return (
    <section className="page report-page api510-shell-tube-external-report">
      <header className="api510-report-header-sticky" aria-label="Sticky report header"><div><h2>STI / API 653 Tank External Report</h2><p>External inspection report-entry page for storage tank equipment.</p></div><div className="api510-header-actions"><button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button><button type="button" disabled title="Word export will be connected after report persistence is finalized.">Export Word Report</button></div></header>
      {!draft && <p role="status" className="wizard-note">No Start Wizard draft setup was found in {API_INSPECTION_DRAFT_SETUP_STORAGE_KEY}; blank tank report fields are shown.</p>}
      <div className="api510-report-layout">
        <main className="api510-report-main">
          <nav className="card api510-section-nav" aria-label="Report sections"><strong>Section navigation</strong><ul>{reportSections.map((section) => <li key={section}><a href={`#${sectionId(section)}`}>{section}</a></li>)}</ul></nav>
          <section id="report-header" className="card wizard-setup-card api510-compact-card" aria-label="Report Header"><h3>Report Header</h3><DraftFieldList fields={headerFields} values={draft?.header} /></section>
          <section id="tank-details" className="card wizard-setup-card api510-compact-card" aria-label="Tank Details"><h3>Tank Details</h3><DraftFieldList fields={tankDetailFields} values={draft?.header} /></section>
          {checklistSections.map((section) => {
            const field = notesFieldBySection[section];
            return <ChecklistSection key={section} section={section} state={componentStates[section] ?? emptyComponentState()} notes={summaryFields[field] as string} onNotesChange={(value) => setSummaryFields((current) => ({ ...current, [field]: value }))} onChange={(next) => setComponentStates((current) => ({ ...current, [section]: next }))} />;
          })}
          <section id="findings" className="card wizard-setup-card" aria-label="Findings"><h3>Findings</h3><TextAreaField label="Finding Summary" value={summaryFields.findingSummary} onChange={(value) => setSummaryFields((current) => ({ ...current, findingSummary: value }))} /></section>
          <section id="recommendations" className="card wizard-setup-card" aria-label="Recommendations"><h3>Recommendations</h3><TextAreaField label="Recommendation Summary" value={recommendationSummary} onChange={setRecommendationSummary} /></section>
          <section id="photos" className="card wizard-setup-card" aria-label="Photos"><h3>Photos</h3><label className="wizard-field"><span>Photo Log</span><textarea rows={3} value={photoLog} onChange={(event) => setPhotoLog(event.target.value)} placeholder="Photo tags and descriptions" /></label></section>
          <section id="nde-testing" className="card wizard-setup-card" aria-label="NDE / Testing"><h3>NDE / Testing</h3><TextAreaField label="NDE / Testing Requirements and Results" value={ndeTesting} onChange={setNdeTesting} /></section>
          <section id="return-to-service" className="card wizard-setup-card" aria-label="Return to Service"><h3>Return to Service</h3><div className="wizard-field-grid"><label className="wizard-field"><span>Return to Service Status</span><select aria-label="Return to Service Status" value={returnToServiceStatus} onChange={(event) => setReturnToServiceStatus(event.target.value)}><option value="">Select status</option><option>Acceptable for continued service</option><option>Acceptable with recommendations</option><option>Hold for repair / engineering review</option></select></label><TextAreaField label="Return to Service Notes" value={returnToServiceNotes} onChange={setReturnToServiceNotes} /></div></section>
        </main>
        <aside className="api510-report-sidebar" aria-label="Report sidebar">
          <section className="card" aria-label="Report Actions"><h3>Report Actions</h3><button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button><button type="button" disabled>Export Word Report</button><p className="wizard-note">Word export will be connected after report persistence is finalized.</p><p role="status">Last Saved: {lastSavedAt ? new Date(lastSavedAt).toLocaleString() : 'Not saved locally'}</p></section>
          <section className="card" aria-label="iE Assist"><h3>iE Assist</h3><p className="wizard-note">Assistant review hooks will use the local report draft until backend report persistence is finalized.</p></section>
          <section className="card" aria-label="Report Summary Preview"><h3>Report Summary Preview</h3><dl className="api510-summary-list"><dt>Tank Checklist Sections</dt><dd>{checklistSections.length}</dd>{summaryChecklistFields.map((field) => <Fragment key={field}><dt>{field}</dt><dd>{checklistCounts[field]}</dd></Fragment>)}</dl><ul aria-label="Selected component preview">{checklistSections.map((section) => <li key={section}>{section}</li>)}</ul></section>
          <section className="card" aria-label="Draft Payload Debug"><details><summary>View Draft Payload</summary><pre aria-label="STI API 653 tank draft payload">{JSON.stringify(draftPayloadPreview, null, 2)}</pre></details></section>
          <section className="card" aria-label="Summary warnings"><h3>Summary Warnings</h3><ul>{!lastSavedAt && <li>Local report draft has not been saved.</li>}</ul></section>
        </aside>
      </div>
    </section>
  );
}
