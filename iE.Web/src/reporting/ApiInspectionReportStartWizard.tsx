import { useEffect, useMemo, useState } from 'react';
import { Api510ExternalReportShell } from './Api510ExternalReportShell';
import { allApiInspectionReportOptions, apiInspectionReportHierarchy } from './apiInspectionReportHierarchy';
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, type ApiInspectionDraftSetup } from './componentCalculationPrefill';

type FieldConfig = {
  key: string;
  label: string;
  type?: 'text' | 'date' | 'number' | 'textarea';
  placeholder?: string;
};

type ComponentConfig = { minimum: string[]; optional: string[] };

const commonFields: FieldConfig[] = [
  { key: 'inspectionDate', label: 'Inspection Date', type: 'date' },
  { key: 'inspector', label: 'Inspector' },
  { key: 'clientFacility', label: 'Client / Facility' },
  { key: 'unitArea', label: 'Unit / Area' },
  { key: 'equipmentIdentifier', label: 'Equipment Tag / Line Number / Tank ID' },
  { key: 'service', label: 'Service' },
  { key: 'inspectionReason', label: 'Inspection Reason' },
  { key: 'operatingStatus', label: 'Operating Status' },
  { key: 'inspectionScope', label: 'Inspection Scope', type: 'textarea' }
];

const api570DesignFields: FieldConfig[] = [
  { key: 'designPressure', label: 'Design Pressure', type: 'number' },
  { key: 'designTemperature', label: 'Design Temperature', type: 'number' },
  { key: 'circuitId', label: 'Circuit ID' },
  { key: 'lineNumbers', label: 'Line Number(s)' },
  { key: 'pipingClass', label: 'Piping Class' },
  { key: 'pipeSizeMaterialRows', label: 'Pipe size/material rows', type: 'textarea', placeholder: 'Add pipe size and material rows for the selected circuit.' }
];

const shellTubeDesignFields: FieldConfig[] = [
  { key: 'shellSideDesignPressure', label: 'Shell Side Design Pressure', type: 'number' },
  { key: 'shellSideDesignTemperature', label: 'Shell Side Design Temperature', type: 'number' },
  { key: 'tubeSideDesignPressure', label: 'Tube Side Design Pressure', type: 'number' },
  { key: 'tubeSideDesignTemperature', label: 'Tube Side Design Temperature', type: 'number' }
];

const shellTubeMaterialFields: FieldConfig[] = [
  { key: 'shellMaterialSpec', label: 'Shell Material Spec' },
  { key: 'shellMaterialGrade', label: 'Shell Material Grade' },
  { key: 'shellProductForm', label: 'Shell Product Form' },
  { key: 'tubeChannelMaterialSpec', label: 'Tube/Channel Material Spec' },
  { key: 'tubeChannelMaterialGrade', label: 'Tube/Channel Material Grade' },
  { key: 'tubeChannelProductForm', label: 'Tube/Channel Product Form' }
];

const plateFrameDesignFields: FieldConfig[] = [
  { key: 'hotSideDesignPressure', label: 'Hot Side Design Pressure', type: 'number' },
  { key: 'hotSideDesignTemperature', label: 'Hot Side Design Temperature', type: 'number' },
  { key: 'coldSideDesignPressure', label: 'Cold Side Design Pressure', type: 'number' },
  { key: 'coldSideDesignTemperature', label: 'Cold Side Design Temperature', type: 'number' }
];

const doublePipeDesignFields: FieldConfig[] = [
  { key: 'innerPipeSideDesignPressure', label: 'Inner Pipe Side Design Pressure', type: 'number' },
  { key: 'innerPipeSideDesignTemperature', label: 'Inner Pipe Side Design Temperature', type: 'number' },
  { key: 'annulusOuterPipeSideDesignPressure', label: 'Annulus / Outer Pipe Side Design Pressure', type: 'number' },
  { key: 'annulusOuterPipeSideDesignTemperature', label: 'Annulus / Outer Pipe Side Design Temperature', type: 'number' }
];

const airCoolerDesignFields: FieldConfig[] = [
  { key: 'tubeSideDesignPressure', label: 'Tube Side Design Pressure', type: 'number' },
  { key: 'tubeSideDesignTemperature', label: 'Tube Side Design Temperature', type: 'number' },
  { key: 'headerBoxDesignPressure', label: 'Header Box Design Pressure', type: 'number' },
  { key: 'headerBoxDesignTemperature', label: 'Header Box Design Temperature', type: 'number' }
];

const remainingExchangerMaterialFields: FieldConfig[] = [
  { key: 'primaryMaterialSpec', label: 'Primary Material Spec' },
  { key: 'primaryMaterialGrade', label: 'Primary Material Grade' },
  { key: 'primaryProductForm', label: 'Primary Product Form' },
  { key: 'secondaryMaterialSpec', label: 'Secondary Material Spec' },
  { key: 'secondaryMaterialGrade', label: 'Secondary Material Grade' },
  { key: 'secondaryProductForm', label: 'Secondary Product Form' }
];

const drumVesselDesignFields: FieldConfig[] = [
  { key: 'vesselDesignPressure', label: 'Vessel Design Pressure', type: 'number' },
  { key: 'vesselDesignTemperature', label: 'Vessel Design Temperature', type: 'number' }
];

const drumVesselMaterialFields: FieldConfig[] = [
  { key: 'shellMaterialSpec', label: 'Shell Material Spec' },
  { key: 'shellMaterialGrade', label: 'Shell Material Grade' },
  { key: 'shellProductForm', label: 'Shell Product Form' },
  { key: 'headMaterialSpec', label: 'Head Material Spec' },
  { key: 'headMaterialGrade', label: 'Head Material Grade' },
  { key: 'headProductForm', label: 'Head Product Form' },
  { key: 'headType', label: 'Head Type' }
];

const towerDesignFields: FieldConfig[] = [
  { key: 'vesselDesignPressure', label: 'Vessel Design Pressure', type: 'number' },
  { key: 'vesselDesignTemperature', label: 'Vessel Design Temperature', type: 'number' }
];

const towerMaterialFields: FieldConfig[] = [
  { key: 'shellCourseMaterialSpec', label: 'Shell Course Material Spec' },
  { key: 'shellCourseMaterialGrade', label: 'Shell Course Material Grade' },
  { key: 'shellCourseProductForm', label: 'Shell Course Product Form' }
];

const sti653DesignFields: FieldConfig[] = [
  { key: 'tankId', label: 'Tank ID' },
  { key: 'tankService', label: 'Tank Service' },
  { key: 'tankConstruction', label: 'Tank Construction Standard' }
];

const componentsByType: Record<string, ComponentConfig> = {
  'api570.piping-external': {
    minimum: ['Piping Circuit', 'Injection / Deadlegs where applicable', 'Supports'],
    optional: ['CUI Areas', 'Small Bore Connections', 'Valves / Flanges', 'Expansion Joints']
  },
  'api570.piping-cui-external': {
    minimum: ['Piping Circuit', 'CUI Areas', 'Supports'],
    optional: ['Small Bore Connections', 'Valves / Flanges', 'Expansion Joints']
  },
  'api510.external.exchanger.shell-tube': {
    minimum: ['Shell', 'Channel / Channel Head', 'Nozzles'],
    optional: ['Shell Cover', 'Channel Cover', 'Channel Head / Dollar Plate', 'Bonnet Head', 'Tubesheet Area', 'Saddles / Supports', 'Expansion Joint', 'Coating / Insulation Area']
  },
  'api510.external.exchanger.plate-frame': {
    minimum: ['Frame Head', 'Pressure Plate', 'Plate Pack External', 'Ports / Nozzles', 'Tie Bolts'],
    optional: ['Carrying Bar', 'Guide Bar', 'Gaskets / Seals', 'Supports', 'Leakage / Staining Areas', 'Coating / Corrosion Areas', 'Nameplate / Markings', 'CML Locations', 'Other Component']
  },
  'api510.external.exchanger.double-pipe': {
    minimum: ['Inner Pipe External', 'Outer Pipe', 'Return Bends', 'Nozzles'],
    optional: ['Flanges / Gaskets / Bolting', 'Supports', 'Expansion Joint', 'Insulation / Coating Area', 'Vents / Drains', 'CML Locations', 'Other Component']
  },
  'api510.external.exchanger.air-cooler': {
    minimum: ['Header Box', 'Tube Bundle External', 'Nozzles', 'Frame / Supports'],
    optional: ['Tubes', 'Fins', 'Tube Supports', 'Plugs / Covers', 'Fan / Guard Area', 'Louvers', 'Access Platforms', 'Coating / Corrosion Areas', 'Nameplate / Markings', 'CML Locations', 'Other Component']
  },
  'sti653.external.tank': {
    minimum: ['Tank Shell', 'Roof', 'Nozzles / Appurtenances'],
    optional: ['Foundation', 'Stairs / Platforms', 'External Coating', 'Insulation']
  }
};

const drumVesselComponents: ComponentConfig = {
  minimum: ['Shell', 'Heads', 'Nozzles', 'Supports', 'Manway where applicable'],
  optional: ['Saddles', 'Skirt', 'Lifting Lugs', 'External Coating', 'Insulation']
};

const commonTowerComponents = ['Shell Courses', 'Nozzles', 'Manways', 'Skirt', 'Base Ring / Anchor Bolts', 'Platforms / Ladders / Handrails'];
const sharedTowerOptionalComponents = ['Heads', 'Insulation / Jacketing', 'Coating', 'Supports / Bracing', 'Davits / Lifting Attachments', 'External Piping Attachments', 'Vents / Drains', 'Nameplate / Markings', 'CML Locations'];

const towerComponentsByType: Record<string, ComponentConfig> = {
  'api510.external.tower-column.distillation': {
    minimum: commonTowerComponents,
    optional: [...sharedTowerOptionalComponents, 'Tray Manways', 'Draw Nozzles', 'Reboiler / Condenser Connections', 'Other Component']
  },
  'api510.external.tower-column.absorber-stripper': {
    minimum: commonTowerComponents,
    optional: [...sharedTowerOptionalComponents, 'Packing Access Manways', 'Distributor Nozzles', 'Reboiler / Overhead Connections', 'Other Component']
  },
  'api510.external.tower-column.packed-column': {
    minimum: commonTowerComponents,
    optional: [...sharedTowerOptionalComponents, 'Packing Support Access', 'Distributor Access', 'Mist Eliminator Access', 'Other Component']
  },
  'api510.external.tower-column.tray-column': {
    minimum: commonTowerComponents,
    optional: [...sharedTowerOptionalComponents, 'Tray Access Manways', 'Downcomer / Tray Access Locations', 'Draw Pan Connections', 'Other Component']
  }
};

const isDrumVesselReport = (typeId: string) => typeId.includes('api510.external.drum-vessel.');
const isTowerReport = (typeId: string) => typeId.includes('api510.external.tower-column.');
const isApi570Report = (typeId: string) => typeId.startsWith('api570.');
const isSti653Report = (typeId: string) => typeId.startsWith('sti653.');

function getDesignFields(typeId: string) {
  if (isApi570Report(typeId)) return api570DesignFields;
  if (typeId === 'api510.external.exchanger.shell-tube') return shellTubeDesignFields;
  if (typeId === 'api510.external.exchanger.plate-frame') return plateFrameDesignFields;
  if (typeId === 'api510.external.exchanger.double-pipe') return doublePipeDesignFields;
  if (typeId === 'api510.external.exchanger.air-cooler') return airCoolerDesignFields;
  if (isDrumVesselReport(typeId)) return drumVesselDesignFields;
  if (isTowerReport(typeId)) return towerDesignFields;
  if (isSti653Report(typeId)) return sti653DesignFields;
  return [];
}

function getMaterialFields(typeId: string) {
  if (typeId === 'api510.external.exchanger.shell-tube') return shellTubeMaterialFields;
  if (['api510.external.exchanger.plate-frame', 'api510.external.exchanger.double-pipe', 'api510.external.exchanger.air-cooler'].includes(typeId)) return remainingExchangerMaterialFields;
  if (isDrumVesselReport(typeId)) return drumVesselMaterialFields;
  if (isTowerReport(typeId)) return towerMaterialFields;
  return [];
}

function getComponentConfig(typeId: string): ComponentConfig {
  if (isDrumVesselReport(typeId)) return drumVesselComponents;
  if (isTowerReport(typeId)) return towerComponentsByType[typeId] ?? { minimum: commonTowerComponents, optional: sharedTowerOptionalComponents };
  return componentsByType[typeId] || { minimum: ['External visual inspection'], optional: ['Insulation / coating areas', 'Supports'] };
}

function EditableField({ field, value, onChange }: { field: FieldConfig; value: string; onChange: (key: string, value: string) => void }) {
  if (field.type === 'textarea') {
    return (
      <label className="wizard-field">
        <span>{field.label}</span>
        <textarea value={value} placeholder={field.placeholder} onChange={(event) => onChange(field.key, event.target.value)} />
      </label>
    );
  }

  return (
    <label className="wizard-field">
      <span>{field.label}</span>
      <input type={field.type || 'text'} value={value} placeholder={field.placeholder} onChange={(event) => onChange(field.key, event.target.value)} />
    </label>
  );
}

export function ApiInspectionReportStartWizard() {
  const [selectedTypeId, setSelectedTypeId] = useState('api570.piping-external');
  const [formValues, setFormValues] = useState<Record<string, string>>({});
  const [selectedOptionalComponents, setSelectedOptionalComponents] = useState<string[]>([]);
  const [draftMessage, setDraftMessage] = useState('');
  const [draftSetup, setDraftSetup] = useState<ApiInspectionDraftSetup | null>(null);
  const selected = useMemo(() => allApiInspectionReportOptions.find((option) => option.id === selectedTypeId), [selectedTypeId]);

  const designFields = useMemo(() => getDesignFields(selectedTypeId), [selectedTypeId]);
  const materialFields = useMemo(() => getMaterialFields(selectedTypeId), [selectedTypeId]);
  const componentConfig = useMemo(() => getComponentConfig(selectedTypeId), [selectedTypeId]);
  const selectedComponentCount = componentConfig.minimum.length + selectedOptionalComponents.length;

  useEffect(() => {
    setSelectedOptionalComponents([]);
    setDraftMessage('');
    setDraftSetup(null);
  }, [selectedTypeId]);

  const updateField = (key: string, value: string) => {
    setFormValues((current) => ({ ...current, [key]: value }));
  };

  const toggleOptionalComponent = (component: string, checked: boolean) => {
    setSelectedOptionalComponents((current) => {
      if (checked) return current.includes(component) ? current : [...current, component];
      return current.filter((item) => item !== component);
    });
  };

  const startDraftReport = () => {
    const draftSetup: ApiInspectionDraftSetup = {
      reportTypeId: selectedTypeId,
      reportTypeLabel: selected?.label,
      header: formValues,
      components: [...componentConfig.minimum, ...selectedOptionalComponents]
    };
    setDraftSetup(draftSetup);
    window.localStorage.setItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY, JSON.stringify(draftSetup));
    setDraftMessage('Draft setup prepared. Full report creation will be connected next.');
  };

  return <section className="card start-wizard" aria-label="API Inspection Report Start Wizard">
    <h3>Step 1: Select Report Type</h3>
    <div className="wizard-hierarchy wizard-step-layout" aria-label="Report type hierarchy">
      <div className="wizard-hierarchy-group">
        <h4>{apiInspectionReportHierarchy.api570.label}</h4>
        {apiInspectionReportHierarchy.api570.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}
      </div>
      <div className="wizard-hierarchy-group">
        <h4>{apiInspectionReportHierarchy.api510External.label}</h4>
        {apiInspectionReportHierarchy.api510External.groups.map((group) => <div key={group.label}><h5>{group.label}</h5>{group.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}</div>)}
      </div>
      <div className="wizard-hierarchy-group">
        <h4>{apiInspectionReportHierarchy.sti653External.label}</h4>
        {apiInspectionReportHierarchy.sti653External.options.map((option) => <label key={option.id}><input type="radio" name="report-type" checked={selectedTypeId === option.id} onChange={() => setSelectedTypeId(option.id)} />{option.label}</label>)}
      </div>
    </div>

    <p><strong>Selected Report:</strong> {selected?.label}</p>

    <h3>Step 2: Report Setup</h3>
    <div className="wizard-card-grid wizard-step-layout">
      <section className="card wizard-setup-card" aria-label="Report Header">
        <h4>Report Header</h4>
        <div className="wizard-field-grid">
          {commonFields.map((field) => <EditableField key={field.key} field={field} value={formValues[field.key] || ''} onChange={updateField} />)}
        </div>
      </section>

      <section className="card wizard-setup-card" aria-label="Design Conditions">
        <h4>Design Conditions</h4>
        {designFields.length > 0 ? <div className="wizard-field-grid">
          {designFields.map((field) => <EditableField key={field.key} field={field} value={formValues[field.key] || ''} onChange={updateField} />)}
        </div> : <p>Design setup fields will be added for this report type.</p>}
        <p className="wizard-note">Allowable stress will be resolved from material tables during calculations.</p>
      </section>

      <section className="card wizard-setup-card" aria-label="Materials">
        <h4>Materials</h4>
        {materialFields.length > 0 ? <div className="wizard-field-grid">
          {materialFields.map((field) => <EditableField key={field.key} field={field} value={formValues[field.key] || ''} onChange={updateField} />)}
        </div> : <p>Material details will be captured in the report body or calculation workspace for this report type.</p>}
      </section>

      <section className="card wizard-setup-card" aria-label="Components">
        <h4>Components</h4>
        <p>{selectedComponentCount} components selected.</p>
        <div className="wizard-component-list">
          <h5>Default / Minimum</h5>
          {componentConfig.minimum.map((component) => (
            <label key={component}>
              <input type="checkbox" checked disabled />
              {component}
            </label>
          ))}
          {componentConfig.optional.length > 0 && <>
            <h5>Optional</h5>
            {componentConfig.optional.map((component) => (
              <label key={component}>
                <input
                  type="checkbox"
                  checked={selectedOptionalComponents.includes(component)}
                  onChange={(event) => toggleOptionalComponent(component, event.target.checked)}
                />
                {component}
              </label>
            ))}
          </>}
        </div>
      </section>

      <section className="card wizard-setup-card wizard-actions-card" aria-label="Actions">
        <h4>Step 3: Actions</h4>
        <button type="button" onClick={startDraftReport}>Start Draft Report</button>
        {draftMessage && <p role="status" className="wizard-success-message">{draftMessage}</p>}
        {draftSetup && <p className="wizard-note">Prepared draft: {draftSetup.reportTypeLabel} with {draftSetup.components.length} components selected.</p>}
        <Api510ExternalReportShell reportTypeId={selectedTypeId} draftPrepared={Boolean(draftSetup)} />
      </section>
    </div>
  </section>;
}
