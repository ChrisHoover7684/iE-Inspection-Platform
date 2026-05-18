import { useMemo, useState } from 'react';
import type { Dispatch, SetStateAction } from 'react';
import {
  buildComponentCalculationPrefill,
  buildShellTubeDraftWorkspaceContext,
  readApiInspectionDraftSetup,
  type CalculationFieldValuesMap
} from './componentCalculationPrefill';
import { executeApi510ComponentCalculation, type Api510CalculationType, type ComponentCalculationExecutionResult } from './componentCalculationExecution';
import { buildApi510FindingDraft, type Api510FindingDraft } from './api510CalculationFindings';
import type { InspectionCalculationSnapshot } from '../engineering/types';

type Props = {
  fieldValues?: CalculationFieldValuesMap;
  hasReportContext?: boolean;
  reportId?: string;
  reportCalculations?: InspectionCalculationSnapshot[];
  onSaveSnapshot?: (snapshot: InspectionCalculationSnapshot) => void;
  onCreateFindingDraft?: (draft: Api510FindingDraft) => void;
};

const shellTubeComponents = [
  { key: 'shell', label: 'Shell' },
  { key: 'nozzles', label: 'Nozzles' }
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
  diameterBasis: 'Diameter Basis',
  parentThicknessSource: 'Parent Thickness Source',
  shellOrHeadRequiredThicknessIn: 'Shell/Head Required Thickness (in.)',
  shellOrHeadExternalRequiredThicknessIn: 'Shell/Head External Pressure Required Thickness (in.)',
  nominalThicknessIn: 'Nominal Nozzle Neck Thickness (in.)',
  nominalPipeSize: 'Nominal Pipe Size',
  externalPressurePsi: 'External Pressure (psi)',
  ug16MinimumThicknessIn: 'UG-16 Minimum Thickness (in.)',
  ug45TableMinimumThicknessIn: 'UG-45 Table Minimum Thickness (in.)'
};

const materialFields = ['designCode', 'stressEra', 'materialSpec', 'materialGrade', 'productForm', 'alloyUNS', 'classConditionTemper', 'resolvedAllowableStressPsi'];
const ug27Fields = ['jointEfficiency', 'insideDiameterIn', 'outsideDiameterIn', 'originalThicknessIn', 'providedThicknessIn', 'corrosionAllowanceIn'];
const ug45Fields = ['shellOrHeadRequiredThicknessIn', 'shellOrHeadExternalRequiredThicknessIn', 'outsideDiameterIn', 'insideDiameterIn', 'nominalThicknessIn', 'originalThicknessIn', 'nominalPipeSize', 'corrosionAllowanceIn', 'jointEfficiency', 'externalPressurePsi', 'ug16MinimumThicknessIn', 'ug45TableMinimumThicknessIn'];

const diameterBasisOptions = [
  { value: 'both-known', label: 'Both ID and OD known' },
  { value: 'inside-diameter-basis', label: 'Inside Diameter basis' },
  { value: 'outside-diameter-basis', label: 'Outside Diameter basis' }
] as const;

const parentThicknessSourceOptions = [
  { value: 'selected-parent', label: 'Selected Parent' },
  { value: 'manual-entry', label: 'Manual Entry' }
] as const;

function inputLabel(key: string, inputs: Record<string, unknown>, setInputs: Dispatch<SetStateAction<Record<string, unknown>>>) {
  const label = fieldLabels[key] ?? key;
  return <label key={key}>{label}<input aria-label={label} value={String(inputs[key] ?? '')} onChange={(e) => setInputs((p) => ({ ...p, [key]: e.target.value }))} /></label>;
}

export function Api510ShellTubeCalculationWorkspace({ fieldValues = {}, hasReportContext = false, reportCalculations = [], onSaveSnapshot, onCreateFindingDraft }: Props) {
  const draftContext = useMemo(() => {
    if (hasReportContext || Object.keys(fieldValues).length > 0) return undefined;
    return buildShellTubeDraftWorkspaceContext(readApiInspectionDraftSetup());
  }, [fieldValues, hasReportContext]);
  const workspaceFieldValues = useMemo(() => ({ ...draftContext?.fieldValues, ...fieldValues }), [draftContext, fieldValues]);
  const [componentKey, setComponentKey] = useState('shell');
  const [pressureSide, setPressureSide] = useState<'shell-side'|'tube-side'>('shell-side');
  const [parentComponent, setParentComponent] = useState('shell');
  const [nozzleLocation, setNozzleLocation] = useState('shell');
  const [calculationType, setCalculationType] = useState<Api510CalculationType>('ug-27-shell-tmin');
  const [designConditions, setDesignConditions] = useState<CalculationFieldValuesMap>({});
  const [inputs, setInputs] = useState<Record<string, unknown>>({
    designCode: 'ASME_VIII_DIV1',
    stressEra: 'From1999Onward',
    diameterBasis: 'both-known',
    parentThicknessSource: 'selected-parent'
  });
  const [execution, setExecution] = useState<ComponentCalculationExecutionResult | null>(null);
  const [savedSnapshotIds, setSavedSnapshotIds] = useState<Set<string>>(new Set());
  const [actionMessage, setActionMessage] = useState('');

  const prefill = useMemo(() => buildComponentCalculationPrefill(
    'Shell and Tube Exchanger',
    componentKey,
    pressureSide,
    componentKey === 'nozzles' ? parentComponent : undefined,
    componentKey === 'nozzles' ? nozzleLocation : undefined,
    {
      ...(hasReportContext ? {} : {
        'api510.external.exchanger.shell-tube.shell-side.design-pressure': designConditions.shellSideDesignPressure,
        'api510.external.exchanger.shell-tube.shell-side.design-temperature': designConditions.shellSideDesignTemperature,
        'api510.external.exchanger.shell-tube.tube-side.design-pressure': designConditions.tubeSideDesignPressure,
        'api510.external.exchanger.shell-tube.tube-side.design-temperature': designConditions.tubeSideDesignTemperature
      }),
      ...workspaceFieldValues
    }
  ), [componentKey, pressureSide, parentComponent, nozzleLocation, workspaceFieldValues, hasReportContext, designConditions]);

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

  const handleCreateFinding = (forceRecommendation: boolean) => {
    if (!execution) return;
    const snapshot = execution.snapshotReadyPayload;
    const hasSavedSnapshot = Boolean(
      snapshot && (savedSnapshotIds.has(snapshot.id) || reportCalculations.some((calculation) => calculation.id === snapshot.id))
    );
    const linkedSnapshot = hasSavedSnapshot ? snapshot : undefined;
    const draft = buildApi510FindingDraft(execution, linkedSnapshot, forceRecommendation);
    onCreateFindingDraft?.(draft);
    setActionMessage(forceRecommendation ? 'Finding and recommendation draft created.' : 'Finding draft created.');
  };

  const showUg27 = calculationType === 'ug-27-shell-tmin';
  const showUg45 = calculationType === 'ug-45-nozzle-neck-tmin';

  return <section>
    <h2>API 510 Shell-and-Tube Calculation Workspace</h2>
    {draftContext && <div role="status">Using draft setup from Start Wizard</div>}

    {draftContext && <section aria-label="Draft Context">
      <h3>Draft Context</h3>
      <p>Design pressure and temperature are pulled from the Start Wizard draft when available.</p>
      <h4>Draft Selected Components</h4>
      <ul>{draftContext.selectedComponents.map((component) => <li key={component}>{component}</li>)}</ul>
    </section>}

    <fieldset>
      <legend>Component Selection</legend>
      <label>Component
        <select aria-label="Component" value={componentKey} onChange={(e) => {
          const next = e.target.value;
          setComponentKey(next);
          setCalculationType(next === 'nozzles' ? 'ug-45-nozzle-neck-tmin' : 'ug-27-shell-tmin');
        }}>
          {shellTubeComponents.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
        </select>
      </label>
      <label>Pressure Side<select aria-label="Pressure Side" value={pressureSide} onChange={(e) => setPressureSide(e.target.value as 'shell-side'|'tube-side')}><option value="shell-side">Shell Side</option><option value="tube-side">Tube Side</option></select></label>
      {componentKey === 'nozzles' && <>
        <label>Parent Component<select aria-label="Parent Component" value={parentComponent} onChange={(e) => setParentComponent(e.target.value)}><option value="shell">Shell</option><option value="channel-channel-head">Channel / Channel Head</option><option value="shell-cover">Shell Cover</option><option value="bonnet-head">Bonnet Head</option></select></label>
        <label>Nozzle Location<select aria-label="Nozzle Location" value={nozzleLocation} onChange={(e) => setNozzleLocation(e.target.value)}><option value="shell">Shell</option><option value="channel-channel-head">Channel / Channel Head</option><option value="bonnet-head">Bonnet Head</option><option value="tubesheet-area">Tubesheet Area</option></select></label>
      </>}
      <label>Calculation Type<select aria-label="Calculation Type" value={calculationType} onChange={(e) => setCalculationType(e.target.value as Api510CalculationType)}>
        <option value="ug-27-shell-tmin">UG-27 Shell Minimum Thickness</option>
        <option value="ug-45-nozzle-neck-tmin">UG-45 Nozzle Neck Minimum Thickness</option>
        <option value="review-only">Review Only</option>
      </select></label>
    </fieldset>

    <section aria-label="Resolved Design Conditions">
      <h3>Resolved Design Conditions</h3>
      <p>Design pressure and temperature are pulled from the Start Wizard draft when available.</p>
      <div>Resolved Pressure Side: {prefill?.resolvedPressureSide ?? 'n/a'}</div>
      <div>Design Pressure: {prefill?.designPressureFieldTag ?? 'n/a'} = {String(prefill?.designPressureValue ?? 'n/a')}</div>
      <div>Design Temperature: {prefill?.designTemperatureFieldTag ?? 'n/a'} = {String(prefill?.designTemperatureValue ?? 'n/a')}</div>
      <div>Tmin supported: {String(prefill?.supportsTminCalculation ?? false)}</div>
      <div>UG-45 supported: {String(prefill?.supportsUg45 ?? false)}</div>
      {[...(prefill?.missingRequiredInputWarnings ?? []), ...(prefill?.designPressureValue === undefined ? ['Design pressure value is not available from selected source.'] : []), ...(prefill?.designTemperatureValue === undefined ? ['Design temperature value is not available from selected source.'] : [])].map((w) => <div key={w}>warning: {w}</div>)}
      {prefill?.notes.map((n) => <div key={n}>note: {n}</div>)}
      {!hasReportContext && <fieldset><legend>Manual Design Conditions</legend>
        <label>Shell-Side Design Pressure<input aria-label="Shell-Side Design Pressure" value={String(designConditions.shellSideDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,shellSideDesignPressure:e.target.value}))} /></label>
        <label>Shell-Side Design Temperature<input aria-label="Shell-Side Design Temperature" value={String(designConditions.shellSideDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,shellSideDesignTemperature:e.target.value}))} /></label>
        <label>Tube-Side Design Pressure<input aria-label="Tube-Side Design Pressure" value={String(designConditions.tubeSideDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,tubeSideDesignPressure:e.target.value}))} /></label>
        <label>Tube-Side Design Temperature<input aria-label="Tube-Side Design Temperature" value={String(designConditions.tubeSideDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,tubeSideDesignTemperature:e.target.value}))} /></label>
      </fieldset>}
    </section>

    <fieldset>
      <legend>Material / Allowable Stress</legend>
      <p>Allowable stress should be resolved from material tables. Manual override requires a reason.</p>
      {materialFields.map((k) => inputLabel(k, inputs, setInputs))}
      <div>Source/Warnings: {String(inputs.materialResolutionMessage ?? '')}</div>
      <label><input type="checkbox" aria-label="Use manual allowable stress override" checked={Boolean(inputs.useManualAllowableStressOverride)} onChange={(e)=>setInputs((p)=>({...p,useManualAllowableStressOverride:e.target.checked}))} />Use manual allowable stress override</label>
      {Boolean(inputs.useManualAllowableStressOverride) && <>
        {inputLabel('allowableStressPsi', inputs, setInputs)}
        {inputLabel('overrideReason', inputs, setInputs)}
      </>}
    </fieldset>

    <fieldset>
      <legend>Calculation Inputs</legend>
      {showUg27 && <>
        {ug27Fields.map((k) => inputLabel(k, inputs, setInputs))}
        <label>Diameter Basis<select aria-label="Diameter Basis" value={String(inputs.diameterBasis ?? 'both-known')} onChange={(e) => setInputs((p) => ({ ...p, diameterBasis: e.target.value }))}>{diameterBasisOptions.map((o) => <option key={o.value} value={o.value}>{o.label}</option>)}</select></label>
      </>}
      {showUg45 && <>
        <label>Parent Thickness Source<select aria-label="Parent Thickness Source" value={String(inputs.parentThicknessSource ?? 'selected-parent')} onChange={(e) => setInputs((p) => ({ ...p, parentThicknessSource: e.target.value }))}>{parentThicknessSourceOptions.map((o) => <option key={o.value} value={o.value}>{o.label}</option>)}</select></label>
        {ug45Fields.map((k) => inputLabel(k, inputs, setInputs))}
        <label><input aria-label="External pressure not applicable" type="checkbox" onChange={(e) => setInputs((p) => ({...p,externalPressureApplicable: !e.target.checked}))} />External pressure not applicable</label>
        <label><input aria-label="UG-16 minimum not applicable" type="checkbox" onChange={(e) => setInputs((p) => ({...p,ug16MinimumThicknessApplicable: !e.target.checked}))} />UG-16 minimum not applicable</label>
      </>}
    </fieldset>

    <section aria-label="Results / Snapshot">
      <h3>Results / Snapshot</h3>
      <p>Save Snapshot is enabled only when this workspace is opened from a report context.</p>
      <button disabled={!canExecute} onClick={onRun}>Run Calculation</button>
      {!canExecute && <div>Review-only component. Calculation execution is disabled.</div>}
      <button disabled={!canSaveSnapshot} onClick={handleSaveSnapshot}>Save Calculation Snapshot</button>
      <button disabled={!execution?.snapshotReadyPayload && !execution} onClick={() => handleCreateFinding(false)}>Create Finding from Calculation</button>
      <button disabled={!execution} onClick={() => handleCreateFinding(true)}>Create Recommendation</button>
      {actionMessage && <div>{actionMessage}</div>}
      {execution && <>
        <div>Execution: {execution.success ? 'success' : 'failure'}</div>
        <div>Summary: {execution.resultSummary}</div>
        {execution.warnings.map((w) => <div key={w}>exec warning: {w}</div>)}
        <div>Snapshot Preview: {execution.snapshotReadyPayload ? 'available' : 'none'}</div>
        <div>Source Tags: {prefill?.designPressureFieldTag} / {prefill?.designTemperatureFieldTag}</div>
      </>}
    </section>
  </section>;
}
