import { useMemo, useState } from 'react';
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
  const [inputs, setInputs] = useState<Record<string, unknown>>({ headType: draftContext?.headType || 'Ellipsoidal2To1', designCode: 'ASME_VIII_DIV1', stressEra: 'From1999Onward' });
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
    {draftContext && <section aria-label="Draft Selected Components">
      <h3>Draft Selected Components</h3>
      <ul>{draftContext.selectedComponents.map((component) => <li key={component}>{component}</li>)}</ul>
    </section>}
    <label>Equipment Subtype<select aria-label="Equipment Subtype" value={equipmentSubtype} onChange={(e) => setEquipmentSubtype(e.target.value as (typeof equipmentSubtypes)[number])}>{equipmentSubtypes.map((s) => <option key={s} value={s}>{s}</option>)}</select></label>
    <label>Component<select aria-label="Component" value={componentKey} onChange={(e) => {
      const next = e.target.value;
      setComponentKey(next);
      setCalculationType(components.find((c) => c.key === next)?.defaultCalculationType ?? 'review-only');
    }}>{components.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}</select></label>

    {componentKey === 'nozzles' && <>
      <label>Parent Component<select aria-label="Parent Component" value={parentComponent} onChange={(e) => setParentComponent(e.target.value)}><option value="shell">shell</option><option value="heads">heads</option></select></label>
      <label>Nozzle Location<select aria-label="Nozzle Location" value={nozzleLocation} onChange={(e) => setNozzleLocation(e.target.value)}><option value="shell">shell</option><option value="heads">heads</option></select></label>
    </>}

    <label>Calculation Type<select aria-label="Calculation Type" value={calculationType} onChange={(e) => setCalculationType(e.target.value as Api510CalculationType)}>
      <option value="ug-27-shell-tmin">UG-27 Shell Tmin</option>
      <option value="ug-32-formed-head-tmin">UG-32 Formed Head Tmin</option>
      <option value="ug-45-nozzle-neck-tmin">UG-45 Nozzle Neck Tmin</option>
      <option value="review-only">Review Only</option>
    </select></label>

    <div>Design Pressure: {prefill?.designPressureFieldTag ?? 'n/a'} = {String(prefill?.designPressureValue ?? 'n/a')}</div>
    <div>Design Temperature: {prefill?.designTemperatureFieldTag ?? 'n/a'} = {String(prefill?.designTemperatureValue ?? 'n/a')}</div>

    {!hasReportContext && <fieldset><legend>Design Conditions</legend><label>Vessel Design Pressure<input aria-label="Vessel Design Pressure" value={String(designConditions.vesselDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,vesselDesignPressure:e.target.value}))} /></label><label>Vessel Design Temperature<input aria-label="Vessel Design Temperature" value={String(designConditions.vesselDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,vesselDesignTemperature:e.target.value}))} /></label></fieldset>}

    <fieldset><legend>Material Allowable Stress Resolution</legend>
      {['designCode','stressEra','materialSpec','materialGrade','productForm','alloyUNS','classConditionTemper','resolvedAllowableStressPsi'].map((k) => <label key={k}>{k === 'materialSpec' ? 'Material Specification' : k === 'materialGrade' ? 'Grade' : k === 'productForm' ? 'Product Form' : k === 'resolvedAllowableStressPsi' ? 'Resolved Allowable Stress' : k}<input aria-label={k === 'resolvedAllowableStressPsi' ? 'Resolved Allowable Stress' : k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
      <label><input type="checkbox" aria-label="Use manual allowable stress override" checked={Boolean(inputs.useManualAllowableStressOverride)} onChange={(e)=>setInputs((p)=>({...p,useManualAllowableStressOverride:e.target.checked}))} />Use manual allowable stress override</label>
      {Boolean(inputs.useManualAllowableStressOverride) && <>
        <label>allowableStressPsi<input aria-label="allowableStressPsi" value={String(inputs.allowableStressPsi ?? '')} onChange={(e)=>setInputs((p)=>({...p,allowableStressPsi:e.target.value}))} /></label>
        <label>overrideReason<input aria-label="overrideReason" value={String(inputs.overrideReason ?? '')} onChange={(e)=>setInputs((p)=>({...p,overrideReason:e.target.value}))} /></label>
      </>}
    </fieldset>

    {calculationType === 'ug-27-shell-tmin' && ['jointEfficiency','insideDiameterIn','outsideDiameterIn','originalThicknessIn','providedThicknessIn','corrosionAllowanceIn'].map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}

    {calculationType === 'ug-32-formed-head-tmin' && <>
      <label>Head Type<select aria-label="Head Type" value={String(inputs.headType ?? 'Ellipsoidal2To1')} onChange={(e) => setInputs((p) => ({ ...p, headType: e.target.value }))}>{headTypeOptions.map((h) => <option key={h.value} value={h.value}>{h.label}</option>)}</select></label>
      {['jointEfficiency','originalThicknessIn','providedThicknessIn','corrosionAllowanceIn'].map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
      {visibleHeadGeometryFields.map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
    </>}

    {calculationType === 'ug-45-nozzle-neck-tmin' && <>
      {['parentThicknessSource','shellOrHeadRequiredThicknessIn','shellOrHeadExternalRequiredThicknessIn','outsideDiameterIn','insideDiameterIn','nominalThicknessIn','originalThicknessIn','nominalPipeSize','corrosionAllowanceIn','jointEfficiency','externalPressurePsi','ug16MinimumThicknessIn','ug45TableMinimumThicknessIn'].map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
    </>}

    {!canExecute && <div>Review-only component. Calculation execution is disabled.</div>}
    <button disabled={!canExecute} onClick={onRun}>Run Calculation</button>
    <button disabled={!canSaveSnapshot} onClick={handleSaveSnapshot}>Save Calculation Snapshot</button>
    <button disabled={!execution} onClick={() => execution && onCreateFindingDraft?.(buildApi510FindingDraft(execution, undefined, false))}>Create Finding from Calculation</button>
    <button disabled={!execution} onClick={() => execution && onCreateFindingDraft?.(buildApi510FindingDraft(execution, undefined, true))}>Create Recommendation</button>

    {actionMessage && <div>{actionMessage}</div>}

    {execution && <>
      <div>Execution: {execution.success ? 'success' : 'failure'}</div>
      <div>Snapshot Preview: {execution.snapshotReadyPayload ? 'available' : 'none'}</div>
    </>}
  </section>;
}
