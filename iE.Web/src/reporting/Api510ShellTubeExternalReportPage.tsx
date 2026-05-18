import { useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { Api510ShellTubeCalculationWorkspace } from './Api510ShellTubeCalculationWorkspace';
import {
  API_INSPECTION_DRAFT_SETUP_STORAGE_KEY,
  readApiInspectionDraftSetup,
  type CalculationFieldValuesMap
} from './componentCalculationPrefill';
import type { Api510FindingDraft } from './api510CalculationFindings';
import type { InspectionCalculationSnapshot } from '../engineering/types';

const reportTypeId = 'api510.external.exchanger.shell-tube';
const defaultComponents = ['Shell', 'Channel / Channel Head', 'Nozzles'];

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

const safeDraft = () => readApiInspectionDraftSetup();

const fieldValue = (values: Record<string, string> | undefined, key: string) => values?.[key] || '';

function ReadOnlyField({ label, value }: { label: string; value: string }) {
  return (
    <div className="report-review-field">
      <dt>{label}</dt>
      <dd>{value || '—'}</dd>
    </div>
  );
}

function DraftFieldList({ fields, values }: { fields: readonly (readonly [string, string])[]; values: Record<string, string> | undefined }) {
  return (
    <dl className="wizard-field-grid">
      {fields.map(([key, label]) => <ReadOnlyField key={key} label={label} value={fieldValue(values, key)} />)}
    </dl>
  );
}

function TextAreaField({ label }: { label: string }) {
  return (
    <label className="wizard-field">
      <span>{label}</span>
      <textarea rows={3} placeholder={`Enter ${label.toLowerCase()}`} />
    </label>
  );
}

function ComponentSection({ component }: { component: string }) {
  return (
    <section className="card wizard-setup-card" aria-label={`${component} Component Section`}>
      <h4>{component}</h4>
      <div className="wizard-field-grid">
        <label className="wizard-field">
          <span>Condition</span>
          <select aria-label={`${component} Condition`} defaultValue="">
            <option value="" disabled>Select condition</option>
            <option>Acceptable</option>
            <option>Monitor</option>
            <option>Requires Evaluation</option>
            <option>Requires Repair</option>
          </select>
        </label>
        <label className="wizard-field">
          <span>Location</span>
          <input aria-label={`${component} Location`} placeholder="Clock position, elevation, CML, or area" />
        </label>
        <TextAreaField label={`${component} Finding Notes`} />
        <TextAreaField label={`${component} Recommendation Text`} />
        <label className="wizard-field">
          <span>Photo Tag</span>
          <input aria-label={`${component} Photo Tag`} placeholder="Photo reference" />
        </label>
      </div>
      <div className="wizard-component-list" aria-label={`${component} Checklist`}>
        {checklistFields.map((field) => (
          <label key={field}>
            <input type="checkbox" aria-label={`${component} ${field}`} />
            {field}
          </label>
        ))}
      </div>
    </section>
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

  const components = useMemo(() => {
    const selected = draft?.reportTypeId === reportTypeId ? draft.components : [];
    return Array.from(new Set([...defaultComponents, ...selected]));
  }, [draft]);

  const fieldValues = useMemo(() => calculationFieldValues(draft?.header), [draft]);

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
    <section className="page api510-shell-tube-external-report">
      <header className="page-header">
        <div>
          <h2>API 510 Shell-and-Tube External Report</h2>
          <p>External inspection report-entry page for Shell-and-Tube Exchanger equipment.</p>
        </div>
        <Link to="/reports/api-510-shell-tube-workspace">Open calculation workspace route</Link>
      </header>

      {!draft && <p role="status" className="wizard-note">No Start Wizard draft setup was found in {API_INSPECTION_DRAFT_SETUP_STORAGE_KEY}; blank report fields are shown.</p>}

      <nav aria-label="Report sections">
        <ul className="wizard-component-list">
          {reportSections.map((section) => <li key={section}><a href={`#${section.toLowerCase().replace(/[^a-z0-9]+/g, '-')}`}>{section}</a></li>)}
        </ul>
      </nav>

      <section id="report-header" className="card wizard-setup-card" aria-label="Report Header">
        <h3>Report Header</h3>
        <DraftFieldList fields={headerFields} values={draft?.header} />
      </section>

      <section id="design-conditions" className="card wizard-setup-card" aria-label="Design Conditions">
        <h3>Design Conditions</h3>
        <DraftFieldList fields={designFields} values={draft?.header} />
      </section>

      <section id="materials" className="card wizard-setup-card" aria-label="Materials">
        <h3>Materials</h3>
        <DraftFieldList fields={materialFields} values={draft?.header} />
      </section>

      <section id="components" aria-label="Components">
        <h3>Components</h3>
        <div className="wizard-card-grid">
          {components.map((component) => <ComponentSection key={component} component={component} />)}
        </div>
      </section>

      <section id="external-condition" className="card wizard-setup-card" aria-label="External Condition">
        <h3>External Condition</h3>
        <div className="wizard-field-grid">{conditionFields.map(([, label]) => <TextAreaField key={label} label={label} />)}</div>
      </section>

      <section id="coating-insulation" className="card wizard-setup-card" aria-label="Coating / Insulation">
        <h3>Coating / Insulation</h3>
        <TextAreaField label="Coating / Insulation Details" />
      </section>

      <section id="leakage-staining" className="card wizard-setup-card" aria-label="Leakage / Staining">
        <h3>Leakage / Staining</h3>
        <TextAreaField label="Leakage / Staining Details" />
      </section>

      <section id="supports-attachments" className="card wizard-setup-card" aria-label="Supports / Attachments">
        <h3>Supports / Attachments</h3>
        <TextAreaField label="Supports / Attachments Details" />
      </section>

      <section id="cml-thickness-review" className="card wizard-setup-card" aria-label="CML / Thickness Review">
        <h3>CML / Thickness Review</h3>
        <TextAreaField label="CML / Thickness Review Details" />
      </section>

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
        <TextAreaField label="Finding Summary" />
        {findingDrafts.length > 0 && <p>{findingDrafts.length} calculation finding draft(s) created.</p>}
      </section>

      <section id="recommendations" className="card wizard-setup-card" aria-label="Recommendations">
        <h3>Recommendations</h3>
        <TextAreaField label="Recommendation Summary" />
      </section>

      <section id="photos" className="card wizard-setup-card" aria-label="Photos">
        <h3>Photos</h3>
        <label className="wizard-field"><span>Photo Log</span><textarea rows={3} placeholder="Photo tags and descriptions" /></label>
      </section>

      <section id="nde-testing" className="card wizard-setup-card" aria-label="NDE / Testing">
        <h3>NDE / Testing</h3>
        <TextAreaField label="NDE / Testing Requirements and Results" />
      </section>

      <section id="return-to-service" className="card wizard-setup-card" aria-label="Return to Service">
        <h3>Return to Service</h3>
        <div className="wizard-field-grid">
          <label className="wizard-field"><span>Return to Service Status</span><select aria-label="Return to Service Status" defaultValue=""><option value="" disabled>Select status</option><option>Acceptable for continued service</option><option>Acceptable with recommendations</option><option>Hold for repair / engineering review</option></select></label>
          <TextAreaField label="Return to Service Notes" />
        </div>
      </section>
    </section>
  );
}
