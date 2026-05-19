import { Fragment, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { Api510ShellTubeCalculationWorkspace } from './Api510ShellTubeCalculationWorkspace';
import { InlineReportAssistPanel } from './InlineReportAssistPanel';
import { evaluateInlineAssistRules } from './ieAssistRules';
import {
  API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY,
  API510_SHELL_TUBE_EXTERNAL_REPORT_TYPE_ID,
  buildApi510ShellTubeExternalDraftPayload,
  readApi510ShellTubeExternalDraftPayload
} from './api510ShellTubeReportDraft';
import {
  API_INSPECTION_DRAFT_SETUP_STORAGE_KEY,
  readApiInspectionDraftSetup,
  type CalculationFieldValuesMap
} from './componentCalculationPrefill';
import type { Api510FindingDraft } from './api510CalculationFindings';
import type { InspectionCalculationSnapshot } from '../engineering/types';
import type {
  Api510ShellTubeExternalChecklistField,
  Api510ShellTubeExternalComponentState,
  Api510ShellTubeExternalDraftPayload,
  Api510ShellTubeExternalSummaryFieldsDraft
} from '../types';

const reportTypeId = API510_SHELL_TUBE_EXTERNAL_REPORT_TYPE_ID;
const defaultComponents = ['Shell', 'Channel / Channel Head', 'Nozzles'];
export { API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY };

type ChecklistField = Api510ShellTubeExternalChecklistField;
type ComponentReportState = Api510ShellTubeExternalComponentState;

const reportSections = [
  'Report Header',
  'Design Conditions',
  'Materials',
  'Components',
  'External Condition',
  'Coating / Insulation',
  'Leakage / Staining',
  'Supports / Attachments',
  'CML / Thickness Review',
  'Calculations',
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

const designFields = [
  ['shellSideDesignPressure', 'Shell Side Design Pressure'],
  ['shellSideDesignTemperature', 'Shell Side Design Temperature'],
  ['tubeSideDesignPressure', 'Tube Side Design Pressure'],
  ['tubeSideDesignTemperature', 'Tube Side Design Temperature']
] as const;

const materialFields = [
  ['shellMaterialSpec', 'Shell Material Spec'],
  ['shellMaterialGrade', 'Shell Material Grade'],
  ['shellProductForm', 'Shell Product Form'],
  ['tubeChannelMaterialSpec', 'Tube/Channel Material Spec'],
  ['tubeChannelMaterialGrade', 'Tube/Channel Material Grade'],
  ['tubeChannelProductForm', 'Tube/Channel Product Form']
] as const;

const conditionFields = [
  ['generalExternalCondition', 'General External Condition'],
  ['coatingInsulationNotes', 'Coating / Insulation Notes'],
  ['leakageStainingNotes', 'Leakage / Staining Notes'],
  ['supportsAttachmentsNotes', 'Supports / Attachments Notes'],
  ['cmlThicknessReviewNotes', 'CML / Thickness Review Notes']
] as const;

const checklistFields = [
  'Create Finding',
  'Recommendation Required',
  'Repair Required',
  'Photo Required',
  'NDE Required',
  'Add to Summary'
] as const;

const summaryChecklistFields = checklistFields.filter((field) => field !== 'Add to Summary');
const emptyChecklist = (): Record<ChecklistField, boolean> => Object.fromEntries(checklistFields.map((field) => [field, false])) as Record<ChecklistField, boolean>;
const emptyComponentState = (): ComponentReportState => ({ condition: '', location: '', findingNotes: '', recommendationText: '', photoTag: '', checklist: emptyChecklist() });
const safeDraft = () => readApiInspectionDraftSetup();
const fieldValue = (values: Record<string, string> | undefined, key: string) => values?.[key] || '';
const sectionId = (section: string) => section.toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/^-|-$/g, '');
const componentAnchor = (component: string) => `component-${sectionId(component)}`;

function ReadOnlyField({ label, value }: { label: string; value: string }) {
  return (
    <div className="report-review-field api510-compact-field">
      <dt>{label}</dt>
      <dd>{value || '—'}</dd>
    </div>
  );
}

function DraftFieldList({ fields, values }: { fields: readonly (readonly [string, string])[]; values: Record<string, string> | undefined }) {
  return (
    <dl className="api510-field-card-grid">
      {fields.map(([key, label]) => <ReadOnlyField key={key} label={label} value={fieldValue(values, key)} />)}
    </dl>
  );
}

function TextAreaField({ label, value, onChange }: { label: string; value: string; onChange: (value: string) => void }) {
  return (
    <label className="wizard-field">
      <span>{label}</span>
      <textarea rows={3} value={value} onChange={(event) => onChange(event.target.value)} placeholder={`Enter ${label.toLowerCase()}`} />
    </label>
  );
}

function ComponentSection({
  component,
  variant,
  state,
  onChange
}: {
  component: string;
  variant: 'default' | 'optional';
  state: ComponentReportState;
  onChange: (next: ComponentReportState) => void;
}) {
  const setField = (key: keyof Omit<ComponentReportState, 'checklist'>, value: string) => onChange({ ...state, [key]: value });
  const setChecklist = (field: ChecklistField, checked: boolean) => onChange({ ...state, checklist: { ...state.checklist, [field]: checked } });

  return (
    <details id={componentAnchor(component)} className={`card wizard-setup-card api510-component-card api510-component-card-${variant}`} aria-label={`${component} Component Section`} open>
      <summary>
        <h4>{component}</h4>
        <span className="badge">{variant === 'default' ? 'Default' : 'Optional'}</span>
      </summary>
      <div className="wizard-field-grid">
        <label className="wizard-field">
          <span>Condition</span>
          <select aria-label={`${component} Condition`} value={state.condition} onChange={(event) => setField('condition', event.target.value)}>
            <option value="" disabled>Select condition</option>
            <option>Acceptable</option>
            <option>Monitor</option>
            <option>Requires Evaluation</option>
            <option>Requires Repair</option>
          </select>
        </label>
        <label className="wizard-field">
          <span>Location</span>
          <input aria-label={`${component} Location`} value={state.location} onChange={(event) => setField('location', event.target.value)} placeholder="Clock position, elevation, CML, or area" />
        </label>
        <label className="wizard-field">
          <span>{component} Finding Notes</span>
          <textarea aria-label={`${component} Finding Notes`} rows={3} value={state.findingNotes} onChange={(event) => setField('findingNotes', event.target.value)} placeholder={`Enter ${component.toLowerCase()} finding notes`} />
        </label>
        <label className="wizard-field">
          <span>{component} Recommendation Text</span>
          <textarea aria-label={`${component} Recommendation Text`} rows={3} value={state.recommendationText} onChange={(event) => setField('recommendationText', event.target.value)} placeholder={`Enter ${component.toLowerCase()} recommendation text`} />
        </label>
        <label className="wizard-field">
          <span>Photo Tag</span>
          <input aria-label={`${component} Photo Tag`} value={state.photoTag} onChange={(event) => setField('photoTag', event.target.value)} placeholder="Photo reference" />
        </label>
      </div>
      <div className="wizard-component-list" aria-label={`${component} Checklist`}>
        {checklistFields.map((field) => (
          <label key={field}>
            <input type="checkbox" aria-label={`${component} ${field}`} checked={state.checklist[field]} onChange={(event) => setChecklist(field, event.target.checked)} />
            {field}
          </label>
        ))}
      </div>
      <InlineReportAssistPanel component={component} state={state} onChange={onChange} />
    </details>
  );
}

const calculationFieldValues = (header: Record<string, string> | undefined): CalculationFieldValuesMap => ({
  'api510.external.exchanger.shell-tube.shell-side.design-pressure': header?.shellSideDesignPressure,
  'api510.external.exchanger.shell-tube.shell-side.design-temperature': header?.shellSideDesignTemperature,
  'api510.external.exchanger.shell-tube.tube-side.design-pressure': header?.tubeSideDesignPressure,
  'api510.external.exchanger.shell-tube.tube-side.design-temperature': header?.tubeSideDesignTemperature
});

export function Api510ShellTubeExternalReportPage() {
  const draft = useMemo(safeDraft, []);
  const [calculations, setCalculations] = useState<InspectionCalculationSnapshot[]>([]);
  const [findingDrafts, setFindingDrafts] = useState<Api510FindingDraft[]>([]);
  const [savedDraftPayload, setSavedDraftPayload] = useState<Api510ShellTubeExternalDraftPayload | undefined>(() => readApi510ShellTubeExternalDraftPayload());
  const [lastSavedAt, setLastSavedAt] = useState(() => savedDraftPayload?.savedAt ?? '');

  const components = useMemo(() => {
    const selected = draft?.reportTypeId === reportTypeId ? draft.components : [];
    return Array.from(new Set([...defaultComponents, ...selected]));
  }, [draft]);

  const [componentStates, setComponentStates] = useState<Record<string, ComponentReportState>>(() => Object.fromEntries(components.map((component) => [component, savedDraftPayload?.componentStates[component] ?? emptyComponentState()])));
  const [summaryFields, setSummaryFields] = useState<Api510ShellTubeExternalSummaryFieldsDraft>(() => savedDraftPayload?.summaryFields ?? {
    generalExternalCondition: '',
    coatingInsulationNotes: '',
    leakageStainingNotes: '',
    supportsAttachmentsNotes: '',
    cmlThicknessReviewNotes: '',
    coatingInsulationDetails: '',
    leakageStainingDetails: '',
    supportsAttachmentsDetails: '',
    cmlThicknessReviewDetails: '',
    findingSummary: ''
  });
  const [photoLog, setPhotoLog] = useState(() => savedDraftPayload?.photos.photoLog ?? '');
  const [ndeTesting, setNdeTesting] = useState(() => savedDraftPayload?.ndeTesting.requirementsAndResults ?? '');
  const [recommendationSummary, setRecommendationSummary] = useState(() => savedDraftPayload?.recommendations.summary ?? '');
  const [returnToServiceStatus, setReturnToServiceStatus] = useState(() => savedDraftPayload?.returnToService.status ?? '');
  const [returnToServiceNotes, setReturnToServiceNotes] = useState(() => savedDraftPayload?.returnToService.notes ?? '');
  const fieldValues = useMemo(() => calculationFieldValues(draft?.header), [draft]);

  const checklistCounts = useMemo(() => Object.fromEntries(summaryChecklistFields.map((field) => [
    field,
    components.filter((component) => componentStates[component]?.checklist[field]).length
  ])) as Record<(typeof summaryChecklistFields)[number], number>, [componentStates, components]);

  const selectedSummaryComponents = components;


  const assistByComponent = useMemo(() => Object.fromEntries(components.map((component) => [component, evaluateInlineAssistRules(componentStates[component] ?? emptyComponentState())])), [componentStates, components]);

  const assistSummary = useMemo(() => ({
    totalAssistWarnings: components.reduce((count, component) => count + assistByComponent[component].suggestions.filter((suggestion) => suggestion.severity !== 'info').length, 0),
    componentsNeedingAttention: components.filter((component) => assistByComponent[component].suggestions.some((suggestion) => suggestion.severity !== 'info')).length,
    missingPhotoTags: components.filter((component) => (componentStates[component]?.checklist['Photo Required']) && !componentStates[component]?.photoTag.trim()).length,
    missingRecommendations: components.filter((component) => (componentStates[component]?.checklist['Recommendation Required']) && !componentStates[component]?.recommendationText.trim()).length,
    ndeNeedingMethodLocation: components.filter((component) => (componentStates[component]?.checklist['NDE Required']) && !componentStates[component]?.location.trim()).length
  }), [assistByComponent, componentStates, components]);

  const draftPayloadPreview = useMemo(() => buildApi510ShellTubeExternalDraftPayload({
    sourceStartWizardDraft: draft,
    sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY,
    components,
    componentStates,
    checklistFields,
    calculationSnapshots: calculations,
    findingDrafts,
    summaryFields,
    photos: { photoLog },
    ndeTesting: { requirementsAndResults: ndeTesting },
    recommendations: { summary: recommendationSummary },
    returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes },
    savedAt: lastSavedAt || 'Not saved locally'
  }), [calculations, componentStates, components, draft, findingDrafts, lastSavedAt, ndeTesting, photoLog, recommendationSummary, returnToServiceNotes, returnToServiceStatus, summaryFields]);

  const handleComponentChange = (component: string, next: ComponentReportState) => setComponentStates((current) => ({ ...current, [component]: next }));

  const handleSaveLocalDraft = () => {
    const savedAt = new Date().toISOString();
    const payload = buildApi510ShellTubeExternalDraftPayload({
      sourceStartWizardDraft: draft,
      sourceStartWizardDraftStorageKey: API_INSPECTION_DRAFT_SETUP_STORAGE_KEY,
      components,
      componentStates,
      checklistFields,
      calculationSnapshots: calculations,
      findingDrafts,
      summaryFields,
      photos: { photoLog },
      ndeTesting: { requirementsAndResults: ndeTesting },
      recommendations: { summary: recommendationSummary },
      returnToService: { status: returnToServiceStatus, notes: returnToServiceNotes },
      savedAt
    });
    window.localStorage.setItem(API510_SHELL_TUBE_EXTERNAL_REPORT_LOCAL_STORAGE_KEY, JSON.stringify(payload));
    setSavedDraftPayload(payload);
    setLastSavedAt(savedAt);
  };

  if (draft && draft.reportTypeId !== reportTypeId) {
    return (
      <section className="page api510-shell-tube-external-report">
        <h2>API 510 Shell-and-Tube External Report</h2>
        <p role="alert">The saved draft setup is not for an API 510 External Shell-and-Tube Exchanger report.</p>
        <Link to="/reports">Return to Start Wizard</Link>
      </section>
    );
  }

  return (
    <section className="page report-page api510-shell-tube-external-report">
      <header className="api510-report-header-sticky" aria-label="Sticky report header">
        <div>
          <h2>API 510 Shell-and-Tube External Report</h2>
          <p>External inspection report-entry page for Shell-and-Tube Exchanger equipment.</p>
        </div>
        <div className="api510-header-actions">
          <Link to="/reports/api-510-shell-tube-workspace">Open calculation workspace route</Link>
          <button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button>
          <button type="button" disabled title="Word export will be connected after report persistence is finalized.">Export Word Report</button>
        </div>
      </header>

      {!draft && <p role="status" className="wizard-note">No Start Wizard draft setup was found in {API_INSPECTION_DRAFT_SETUP_STORAGE_KEY}; blank report fields are shown.</p>}

      <div className="api510-report-layout">
        <main className="api510-report-main">
          <nav className="card api510-section-nav" aria-label="Report sections">
            <strong>Section navigation</strong>
            <ul>
              {reportSections.map((section) => <li key={section}><a href={`#${sectionId(section)}`}>{section}</a></li>)}
            </ul>
          </nav>

          <section id="report-header" className="card wizard-setup-card api510-compact-card" aria-label="Report Header">
            <h3>Report Header</h3>
            <DraftFieldList fields={headerFields} values={draft?.header} />
          </section>

          <section id="design-conditions" className="card wizard-setup-card api510-compact-card" aria-label="Design Conditions">
            <h3>Design Conditions</h3>
            <DraftFieldList fields={designFields} values={draft?.header} />
          </section>

          <section id="materials" className="card wizard-setup-card api510-compact-card" aria-label="Materials">
            <h3>Materials</h3>
            <DraftFieldList fields={materialFields} values={draft?.header} />
          </section>

          <section id="components" aria-label="Components" className="card wizard-setup-card">
            <div className="api510-section-heading-row">
              <div>
                <h3>Components</h3>
                <p className="muted">{components.length} component(s) selected for this external report.</p>
              </div>
              <nav aria-label="Component quick links">
                <ul className="api510-component-links">
                  {components.map((component) => <li key={component}><a href={`#${componentAnchor(component)}`}>{component}</a></li>)}
                </ul>
              </nav>
            </div>
            <div className="wizard-card-grid api510-component-grid">
              {components.map((component) => (
                <ComponentSection
                  key={component}
                  component={component}
                  variant={defaultComponents.includes(component) ? 'default' : 'optional'}
                  state={componentStates[component] ?? emptyComponentState()}
                  onChange={(next) => handleComponentChange(component, next)}
                />
              ))}
            </div>
          </section>

          <section id="external-condition" className="card wizard-setup-card" aria-label="External Condition">
            <h3>External Condition</h3>
            <div className="wizard-field-grid">{conditionFields.map(([key, label]) => <TextAreaField key={key} label={label} value={summaryFields[key]} onChange={(value) => setSummaryFields((current) => ({ ...current, [key]: value }))} />)}</div>
          </section>

          <section id="coating-insulation" className="card wizard-setup-card" aria-label="Coating / Insulation"><h3>Coating / Insulation</h3><TextAreaField label="Coating / Insulation Details" value={summaryFields.coatingInsulationDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, coatingInsulationDetails: value }))} /></section>
          <section id="leakage-staining" className="card wizard-setup-card" aria-label="Leakage / Staining"><h3>Leakage / Staining</h3><TextAreaField label="Leakage / Staining Details" value={summaryFields.leakageStainingDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, leakageStainingDetails: value }))} /></section>
          <section id="supports-attachments" className="card wizard-setup-card" aria-label="Supports / Attachments"><h3>Supports / Attachments</h3><TextAreaField label="Supports / Attachments Details" value={summaryFields.supportsAttachmentsDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, supportsAttachmentsDetails: value }))} /></section>
          <section id="cml-thickness-review" className="card wizard-setup-card" aria-label="CML / Thickness Review"><h3>CML / Thickness Review</h3><TextAreaField label="CML / Thickness Review Details" value={summaryFields.cmlThicknessReviewDetails} onChange={(value) => setSummaryFields((current) => ({ ...current, cmlThicknessReviewDetails: value }))} /></section>

          <section id="calculations" className="card wizard-setup-card" aria-label="Calculations">
            <h3>Calculations</h3>
            <p>Shell-and-Tube calculation workspace is embedded below using draft design field values from this report.</p>
            <Api510ShellTubeCalculationWorkspace
              hasReportContext
              reportId="api510-shell-tube-external-draft"
              fieldValues={fieldValues}
              reportCalculations={calculations}
              onSaveSnapshot={(snapshot) => setCalculations((current) => [...current, snapshot])}
              onCreateFindingDraft={(finding) => setFindingDrafts((current) => [...current, finding])}
            />
          </section>

          <section id="findings" className="card wizard-setup-card" aria-label="Findings">
            <h3>Findings</h3>
            <TextAreaField label="Finding Summary" value={summaryFields.findingSummary} onChange={(value) => setSummaryFields((current) => ({ ...current, findingSummary: value }))} />
            {findingDrafts.length > 0 && <p>{findingDrafts.length} calculation finding draft(s) created.</p>}
          </section>

          <section id="recommendations" className="card wizard-setup-card" aria-label="Recommendations"><h3>Recommendations</h3><TextAreaField label="Recommendation Summary" value={recommendationSummary} onChange={setRecommendationSummary} /></section>
          <section id="photos" className="card wizard-setup-card" aria-label="Photos"><h3>Photos</h3><label className="wizard-field"><span>Photo Log</span><textarea rows={3} value={photoLog} onChange={(event) => setPhotoLog(event.target.value)} placeholder="Photo tags and descriptions" /></label></section>
          <section id="nde-testing" className="card wizard-setup-card" aria-label="NDE / Testing"><h3>NDE / Testing</h3><TextAreaField label="NDE / Testing Requirements and Results" value={ndeTesting} onChange={setNdeTesting} /></section>
          <section id="return-to-service" className="card wizard-setup-card" aria-label="Return to Service">
            <h3>Return to Service</h3>
            <div className="wizard-field-grid">
              <label className="wizard-field"><span>Return to Service Status</span><select aria-label="Return to Service Status" value={returnToServiceStatus} onChange={(event) => setReturnToServiceStatus(event.target.value)}><option value="" disabled>Select status</option><option>Acceptable for continued service</option><option>Acceptable with recommendations</option><option>Hold for repair / engineering review</option></select></label>
              <TextAreaField label="Return to Service Notes" value={returnToServiceNotes} onChange={setReturnToServiceNotes} />
            </div>
          </section>
        </main>

        <aside className="api510-report-sidebar" aria-label="Report sidebar">
          <section className="card" aria-label="Report Actions">
            <h3>Report Actions</h3>
            <button type="button" onClick={handleSaveLocalDraft}>Save Draft Locally</button>
            <button type="button" disabled>Export Word Report</button>
            <p className="wizard-note">Word export will be connected after report persistence is finalized.</p>
            <p role="status">Last Saved: {lastSavedAt ? new Date(lastSavedAt).toLocaleString() : 'Not saved locally'}</p>
          </section>

          <section className="card" aria-label="iE Assist">
            <h3>iE Assist</h3>
            <p className="wizard-note">Assistant review hooks use local deterministic rules and do not call external AI services.</p>
            <dl className="api510-summary-list">
              <dt>Total assist warnings</dt><dd data-testid="total-assist-warnings-count">{assistSummary.totalAssistWarnings}</dd>
              <dt>Components needing attention</dt><dd data-testid="components-needing-attention-count">{assistSummary.componentsNeedingAttention}</dd>
              <dt>Missing photo tags</dt><dd data-testid="missing-photo-tags-count">{assistSummary.missingPhotoTags}</dd>
              <dt>Missing recommendations</dt><dd data-testid="missing-recommendations-count">{assistSummary.missingRecommendations}</dd>
              <dt>NDE flags needing method/location</dt><dd data-testid="nde-needing-method-location-count">{assistSummary.ndeNeedingMethodLocation}</dd>
            </dl>
          </section>

          <section className="card" aria-label="Report Summary Preview">
            <h3>Report Summary Preview</h3>
            <dl className="api510-summary-list">
              <dt>Selected Components</dt><dd>{selectedSummaryComponents.length}</dd>
              <dt>Calculation Snapshots</dt><dd>{calculations.length}</dd>
              <dt>Finding Drafts</dt><dd>{findingDrafts.length}</dd>
              {summaryChecklistFields.map((field) => <Fragment key={field}><dt>{field}</dt><dd>{checklistCounts[field]}</dd></Fragment>)}
            </dl>
            <ul aria-label="Selected component preview">
              {selectedSummaryComponents.map((component) => <li key={component}>{component}</li>)}
            </ul>
          </section>

          <section className="card" aria-label="Draft Payload Debug">
            <details>
              <summary>View Draft Payload</summary>
              <pre aria-label="API 510 Shell-and-Tube draft payload">{JSON.stringify(draftPayloadPreview, null, 2)}</pre>
            </details>
          </section>

          <section className="card" aria-label="Summary warnings">
            <h3>Summary Warnings</h3>
            <ul>
              {calculations.length === 0 && <li>No calculation snapshots saved yet.</li>}
              {findingDrafts.length === 0 && <li>No calculation finding drafts created yet.</li>}
              {!lastSavedAt && <li>Local report draft has not been saved.</li>}
            </ul>
          </section>
        </aside>
      </div>
    </section>
  );
}
