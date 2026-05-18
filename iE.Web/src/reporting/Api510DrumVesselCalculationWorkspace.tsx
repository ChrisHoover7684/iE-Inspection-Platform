import { useMemo, useState } from 'react';
import type { Dispatch, SetStateAction } from 'react';
import {
  buildComponentCalculationPrefill,
  buildDrumVesselDraftWorkspaceContext,
  readApiInspectionDraftSetup,
  type CalculationFieldValuesMap
} from './componentCalculationPrefill';
import { executeApi510ComponentCalculation, type Api510CalculationType, type ComponentCalculationExecutionResult } from './componentCalculationExecution';
import { buildApi510FindingDraft, type Api510FindingDraft } from './api510CalculationFindings';
import type { InspectionCalculationSnapshot } from '../engineering/types';

type Props = {
  fieldValues?: CalculationFieldValuesMap;
  hasReportContext?: boolean;
  reportCalculations?: InspectionCalculationSnapshot[];
  onSaveSnapshot?: (snapshot: InspectionCalculationSnapshot) => void;
  onCreateFindingDraft?: (draft: Api510FindingDraft) => void;
};

const equipmentSubtypes = ['Horizontal Drum', 'Vertical Drum', 'Separator / KO Drum', 'Accumulator / Receiver', 'Generic Pressure Vessel'] as const;
const components = [
  { key: 'shell', label: 'Shell', defaultCalculationType: 'ug-27-shell-tmin' as const },
  { key: 'heads', label: 'Heads', defaultCalculationType: 'ug-32-formed-head-tmin' as const },
  { key: 'nozzles', label: 'Nozzles', defaultCalculationType: 'ug-45-nozzle-neck-tmin' as const },
  { key: 'supports', label: 'Supports / Saddles / Skirt / Manway', defaultCalculationType: 'review-only' as const }
] as const;

const headTypeOptions = [
  { value: 'Ellipsoidal2To1', label: '2:1 Ellipsoidal' },
  { value: 'Hemispherical', label: 'Hemispherical' },
  { value: 'TorisphericalAsmeFd', label: 'ASME Flanged & Dished / Torispherical' },
  { value: 'Conical', label: 'Conical' },
  { value: 'Toriconical', label: 'Toriconical' },
  { value: 'FlatUg34', label: 'Flat Head / Flat Cover' }
] as const;

const parentThicknessSourceOptions = [
  { value: 'selected-parent', label: 'Selected Parent' },
  { value: 'manual-entry', label: 'Manual Entry' }
] as const;

const fieldLabels: Record<string, string> = {
  designCode: 'Design Code',
  stressEra: 'Stress Table Era',
  materialSpec: 'Material Specification',
  materialGrade: 'Grade',
  productForm: 'Product Form',
  alloyUNS: 'Alloy / UNS Number',
  classConditionTemper: 'Class / Condition / Temper',
  resolvedAllowableStressPsi: 'Resolved Allowable Stress (psi)',
  allowableStressPsi: 'Manual Allowable Stress (psi)',
  overrideReason: 'Manual Override Reason',
  jointEfficiency: 'Joint Efficiency',
  insideDiameterIn: 'Inside Diameter (in.)',
  outsideDiameterIn: 'Outside Diameter (in.)',
  originalThicknessIn: 'Original/Nominal Thickness (in.)',
  providedThicknessIn: 'Current / Provided Thickness (in.)',
  corrosionAllowanceIn: 'Corrosion Allowance (in.)',
  parentThicknessSource: 'Parent Thickness Source',
  shellOrHeadRequiredThicknessIn: 'Shell/Head Required Thickness (in.)',
  shellOrHeadExternalRequiredThicknessIn: 'Shell/Head External Pressure Required Thickness (in.)',
  nominalThicknessIn: 'Nominal Nozzle Neck Thickness (in.)',
  nominalPipeSize: 'Nominal Pipe Size',
  externalPressurePsi: 'External Pressure (psi)',
  ug16MinimumThicknessIn: 'UG-16 Minimum Thickness (in.)',
  ug45TableMinimumThicknessIn: 'UG-45 Table Minimum Thickness (in.)',
  effectiveInsideDiameterIn: 'Effective Inside Diameter (in.)',
  effectiveInsideRadiusIn: 'Effective Inside Radius (in.)',
  crownRadiusIn: 'Crown Radius (in.)',
  halfApexAngleDeg: 'Half Apex Angle (deg.)',
  flatHeadCFactor: 'Flat Head C Factor'
};

const materialFields = ['designCode', 'stressEra', 'materialSpec', 'materialGrade', 'productForm', 'alloyUNS', 'classConditionTemper', 'resolvedAllowableStressPsi'];
const ug27Fields = ['jointEfficiency', 'insideDiameterIn', 'outsideDiameterIn', 'originalThicknessIn', 'providedThicknessIn', 'corrosionAllowanceIn'];
const ug32BaseFields = ['jointEfficiency', 'originalThicknessIn', 'providedThicknessIn', 'corrosionAllowanceIn'];
const ug45Fields = ['shellOrHeadRequiredThicknessIn', 'shellOrHeadExternalRequiredThicknessIn', 'outsideDiameterIn', 'insideDiameterIn', 'nominalThicknessIn', 'originalThicknessIn', 'nominalPipeSize', 'corrosionAllowanceIn', 'jointEfficiency', 'externalPressurePsi', 'ug16MinimumThicknessIn', 'ug45TableMinimumThicknessIn'];

function inputLabel(key: string, inputs: Record<string, unknown>, setInputs: Dispatch<SetStateAction<Record<string, unknown>>>) {
  const label = fieldLabels[key] ?? key;
  return <label key={key}>{label}<input aria-label={label} value={String(inputs[key] ?? '')} onChange={(e) => setInputs((p) => ({ ...p, [key]: e.target.value }))} /></label>;
}

export function Api510DrumVesselCalculationWorkspace({ fieldValues = {}, hasReportContext = false, reportCalculations = [], onSaveSnapshot, onCreateFindingDraft }: Props) {
  const draftContext = useMemo(() => {
    if (hasReportContext || Object.keys(fieldValues).length > 0) return undefined;
    return buildDrumVesselDraftWorkspaceContext(readApiInspectionDraftSetup());
  }, [fieldValues, hasReportContext]);
  const workspaceFieldValues = useMemo(() => ({ ...draftContext?.fieldValues, ...fieldValues }), [draftContext, fieldValues]);
  const draftEquipmentSubtype = equipmentSubtypes.find((subtype) => subtype === draftContext?.equipmentSubtype) ?? 'Horizontal Drum';
  const [equipmentSubtype, setEquipmentSubtype] = useState<(typeof equipmentSubtypes)[number]>(draftEquipmentSubtype);
  const [componentKey, setComponentKey] = useState('shell');
  const [parentComponent, setParentComponent] = useState('shell');
  const [nozzleLocation, setNozzleLocation] = useState('shell');
  const [calculationType, setCalculationType] = useState<Api510CalculationType>('ug-27-shell-tmin');
  const [designConditions, setDesignConditions] = useState<CalculationFieldValuesMap>({});
  const [inputs, setInputs] = useState<Record<string, unknown>>({ headType: draftContext?.headType || 'Ellipsoidal2To1', designCode: 'ASME_VIII_DIV1', stressEra: 'From1999Onward', parentThicknessSource: 'selected-parent' });
  const [execution, setExecution] = useState<ComponentCalculationExecutionResult | null>(null);
  const [savedSnapshotIds, setSavedSnapshotIds] = useState<Set<string>>(new Set());
  const [actionMessage, setActionMessage] = useState('');

  const prefill = useMemo(() => buildComponentCalculationPrefill(equipmentSubtype, componentKey, 'shared', componentKey === 'nozzles' ? parentComponent : undefined, componentKey === 'nozzles' ? nozzleLocation : undefined, { ...(hasReportContext ? {} : { 'api510.external.drum-vessel.design-pressure': designConditions.vesselDesignPressure, 'api510.external.drum-vessel.design-temperature': designConditions.vesselDesignTemperature }), ...workspaceFieldValues }), [equipmentSubtype, componentKey, parentComponent, nozzleLocation, workspaceFieldValues, hasReportContext, designConditions]);
  const canExecute = calculationType !== 'review-only';
  const onRun = async () => {
    if (!prefill || !canExecute) return;
    setActionMessage('');
    const result = await executeApi510ComponentCalculation({ prefill, calculationType, manualInputs: inputs });
    setExecution(result);
  };
  const canSaveSnapshot = hasReportContext && Boolean(execution?.snapshotReadyPayload);

  const handleSaveSnapshot = () => {
    if (!execution?.snapshotReadyPayload || !canSaveSnapshot) return;
    const snapshot = execution.snapshotReadyPayload;
    const alreadySaved = savedSnapshotIds.has(snapshot.id) || reportCalculations.some((c) => c.id === snapshot.id);
    if (alreadySaved) {
      setActionMessage('Snapshot already saved for this calculation result.');
      return;
    }
    onSaveSnapshot?.(snapshot);
    setSavedSnapshotIds((prev) => new Set([...prev, snapshot.id]));
    setActionMessage('Calculation snapshot saved.');
  };

  const visibleHeadGeometryFields = (() => {
    const t = inputs.headType;
    if (t === 'Ellipsoidal2To1') return ['effectiveInsideDiameterIn'];
    if (t === 'Hemispherical') return ['effectiveInsideRadiusIn'];
    if (t === 'TorisphericalAsmeFd') return ['crownRadiusIn'];
    if (t === 'Conical' || t === 'Toriconical') return ['effectiveInsideDiameterIn', 'halfApexAngleDeg'];
    if (t === 'FlatUg34') return ['effectiveInsideDiameterIn', 'flatHeadCFactor'];
    return [];
  })();

  return <section>
    <h2>API 510 Drum/Vessel Calculation Workspace</h2>
    {draftContext && <div role="status">Using draft setup from Start Wizard</div>}

    {draftContext && <section aria-label="Draft Context">
      <h3>Draft Context</h3>
      <p>Design pressure and temperature are pulled from the Start Wizard draft when available.</p>
      <h4>Draft Selected Components</h4>
      <ul>{draftContext.selectedComponents.map((component) => <li key={component}>{component}</li>)}</ul>
    </section>}

    <fieldset>
      <legend>Component Selection</legend>
      <label>Equipment Subtype<select aria-label="Equipment Subtype" value={equipmentSubtype} onChange={(e) => setEquipmentSubtype(e.target.value as (typeof equipmentSubtypes)[number])}>{equipmentSubtypes.map((s) => <option key={s} value={s}>{s}</option>)}</select></label>
      <label>Component<select aria-label="Component" value={componentKey} onChange={(e) => {
        const next = e.target.value;
        setComponentKey(next);
        setCalculationType(components.find((c) => c.key === next)?.defaultCalculationType ?? 'review-only');
      }}>{components.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}</select></label>

      {componentKey === 'nozzles' && <>
        <label>Parent Component<select aria-label="Parent Component" value={parentComponent} onChange={(e) => setParentComponent(e.target.value)}><option value="shell">Shell</option><option value="heads">Heads</option></select></label>
        <label>Nozzle Location<select aria-label="Nozzle Location" value={nozzleLocation} onChange={(e) => setNozzleLocation(e.target.value)}><option value="shell">Shell</option><option value="heads">Heads</option></select></label>
      </>}

      <label>Calculation Type<select aria-label="Calculation Type" value={calculationType} onChange={(e) => setCalculationType(e.target.value as Api510CalculationType)}>
        <option value="ug-27-shell-tmin">UG-27 Shell Minimum Thickness</option>
        <option value="ug-32-formed-head-tmin">UG-32 Formed Head Minimum Thickness</option>
        <option value="ug-45-nozzle-neck-tmin">UG-45 Nozzle Neck Minimum Thickness</option>
        <option value="review-only">Review Only</option>
      </select></label>
    </fieldset>

    <section aria-label="Resolved Design Conditions">
      <h3>Resolved Design Conditions</h3>
      <p>Design pressure and temperature are pulled from the Start Wizard draft when available.</p>
      <div>Design Pressure: {prefill?.designPressureFieldTag ?? 'n/a'} = {String(prefill?.designPressureValue ?? 'n/a')}</div>
      <div>Design Temperature: {prefill?.designTemperatureFieldTag ?? 'n/a'} = {String(prefill?.designTemperatureValue ?? 'n/a')}</div>
      {!hasReportContext && <fieldset><legend>Manual Design Conditions</legend><label>Vessel Design Pressure<input aria-label="Vessel Design Pressure" value={String(designConditions.vesselDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,vesselDesignPressure:e.target.value}))} /></label><label>Vessel Design Temperature<input aria-label="Vessel Design Temperature" value={String(designConditions.vesselDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,vesselDesignTemperature:e.target.value}))} /></label></fieldset>}
    </section>

    <fieldset>
      <legend>Material / Allowable Stress</legend>
      <p>Allowable stress should be resolved from material tables. Manual override requires a reason.</p>
      {materialFields.map((k) => inputLabel(k, inputs, setInputs))}
      <label><input type="checkbox" aria-label="Use manual allowable stress override" checked={Boolean(inputs.useManualAllowableStressOverride)} onChange={(e)=>setInputs((p)=>({...p,useManualAllowableStressOverride:e.target.checked}))} />Use manual allowable stress override</label>
      {Boolean(inputs.useManualAllowableStressOverride) && <>
        {inputLabel('allowableStressPsi', inputs, setInputs)}
        {inputLabel('overrideReason', inputs, setInputs)}
      </>}
    </fieldset>

    <fieldset>
      <legend>Calculation Inputs</legend>
      {calculationType === 'ug-27-shell-tmin' && ug27Fields.map((k) => inputLabel(k, inputs, setInputs))}

      {calculationType === 'ug-32-formed-head-tmin' && <>
        <label>Head Type<select aria-label="Head Type" value={String(inputs.headType ?? 'Ellipsoidal2To1')} onChange={(e) => setInputs((p) => ({ ...p, headType: e.target.value }))}>{headTypeOptions.map((h) => <option key={h.value} value={h.value}>{h.label}</option>)}</select></label>
        {ug32BaseFields.map((k) => inputLabel(k, inputs, setInputs))}
        {visibleHeadGeometryFields.map((k) => inputLabel(k, inputs, setInputs))}
      </>}

      {calculationType === 'ug-45-nozzle-neck-tmin' && <>
        <label>Parent Thickness Source<select aria-label="Parent Thickness Source" value={String(inputs.parentThicknessSource ?? 'selected-parent')} onChange={(e) => setInputs((p) => ({ ...p, parentThicknessSource: e.target.value }))}>{parentThicknessSourceOptions.map((o) => <option key={o.value} value={o.value}>{o.label}</option>)}</select></label>
        {ug45Fields.map((k) => inputLabel(k, inputs, setInputs))}
      </>}

      {!canExecute && <div>Review-only component. Calculation execution is disabled.</div>}
    </fieldset>

    <section aria-label="Results / Snapshot">
      <h3>Results / Snapshot</h3>
      <p>Save Snapshot is enabled only when this workspace is opened from a report context.</p>
      <button disabled={!canExecute} onClick={onRun}>Run Calculation</button>
      <button disabled={!canSaveSnapshot} onClick={handleSaveSnapshot}>Save Calculation Snapshot</button>
      <button disabled={!execution} onClick={() => execution && onCreateFindingDraft?.(buildApi510FindingDraft(execution, undefined, false))}>Create Finding from Calculation</button>
      <button disabled={!execution} onClick={() => execution && onCreateFindingDraft?.(buildApi510FindingDraft(execution, undefined, true))}>Create Recommendation</button>
      {actionMessage && <div>{actionMessage}</div>}
      {execution && <>
        <div>Execution: {execution.success ? 'success' : 'failure'}</div>
        <div>Snapshot Preview: {execution.snapshotReadyPayload ? 'available' : 'none'}</div>
      </>}
    </section>
  </section>;
}
