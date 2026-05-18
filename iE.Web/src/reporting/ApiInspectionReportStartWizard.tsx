import { useMemo, useState } from 'react';
import { Api510ExternalReportShell } from './Api510ExternalReportShell';
import { allApiInspectionReportOptions, apiInspectionReportHierarchy } from './apiInspectionReportHierarchy';

const commonFields = ['Inspection Date','Inspector','Client / Facility','Unit / Area','Equipment Tag / Line Number / Tank ID','Service','Inspection Reason','Operating Status','Inspection Scope'];

const designFieldsByType: Record<string, string[]> = {
  'api570.piping-external': ['Design Pressure','Design Temperature','Circuit ID','Line Number(s)','Piping Class','Pipe size/material rows'],
  'api570.piping-cui-external': ['Design Pressure','Design Temperature','Circuit ID','Line Number(s)','Piping Class','Pipe size/material rows'],
  'api510.external.exchanger.shell-tube': ['Shell Side Design Pressure','Shell Side Design Temperature','Tube Side Design Pressure','Tube Side Design Temperature','Shell Material','Tube/Channel Side Material'],
};

const componentsByType: Record<string, { minimum: string[]; optional: string[] }> = {
  'api510.external.exchanger.shell-tube': {
    minimum: ['Shell', 'Channel / Channel Head', 'Nozzles'],
    optional: ['Shell Cover','Channel Cover','Channel Head / Dollar Plate','Bonnet Head','Tubesheet Area','Saddles / Supports','Expansion Joint','Coating / Insulation Area']
  }
};

export function ApiInspectionReportStartWizard() {
  const [selectedTypeId, setSelectedTypeId] = useState('api570.piping-external');
  const selected = useMemo(() => allApiInspectionReportOptions.find((option) => option.id === selectedTypeId), [selectedTypeId]);

  const designFields = useMemo(() => {
    if (selectedTypeId.includes('api510.external.drum-vessel.')) return ['Vessel Design Pressure', 'Vessel Design Temperature', 'Shell Material', 'Head Material', 'Head Type'];
    if (selectedTypeId.includes('api510.external.tower-column.')) return ['Vessel Design Pressure', 'Vessel Design Temperature', 'Shell Course Material'];
    return designFieldsByType[selectedTypeId] || [];
  }, [selectedTypeId]);

  const componentPreview = useMemo(() => {
    if (selectedTypeId.includes('api510.external.drum-vessel.')) return { minimum: ['Shell', 'Heads', 'Nozzles', 'Supports', 'Manway where applicable'], optional: [] };
    if (selectedTypeId.includes('api510.external.tower-column.')) return { minimum: ['Shell Courses', 'Nozzles', 'Manways', 'Skirt', 'Base Ring / Anchor Bolts', 'Platforms / Ladders / Handrails'], optional: [] };
    return componentsByType[selectedTypeId];
  }, [selectedTypeId]);

  return <section className="card" aria-label="Start New API Inspection Report">
    <h3>Start New API Inspection Report</h3>
    <div>
      <h4>{apiInspectionReportHierarchy.api570.label}</h4>
      {apiInspectionReportHierarchy.api570.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}
      <h4>{apiInspectionReportHierarchy.api510External.label}</h4>
      {apiInspectionReportHierarchy.api510External.groups.map((group) => <div key={group.label}><h5>{group.label}</h5>{group.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}</div>)}
      <h4>{apiInspectionReportHierarchy.sti653External.label}</h4>
      {apiInspectionReportHierarchy.sti653External.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}
    </div>
    <p><strong>Selected Report:</strong> {selected?.label}</p>
    <h4>Start Form (Common Fields)</h4>
    <ul>{commonFields.map((field) => <li key={field}>{field}</li>)}</ul>
    <h4>Design Conditions</h4>
    <ul>{designFields.map((field) => <li key={field}>{field}</li>)}</ul>
    {componentPreview && <>
      <h4>Component Selection Preview (Minimum/Default)</h4>
      <ul>{componentPreview.minimum.map((c) => <li key={c}>{c}</li>)}</ul>
      {componentPreview.optional.length > 0 && <><h5>Optional</h5><ul>{componentPreview.optional.map((c) => <li key={c}>{c}</li>)}</ul></>}
    </>}
    <Api510ExternalReportShell reportTypeId={selectedTypeId} />
  </section>;
}
