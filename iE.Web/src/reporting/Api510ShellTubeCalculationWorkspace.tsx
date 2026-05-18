import { useMemo, useState } from 'react';
import { buildComponentCalculationPrefill, type CalculationFieldValuesMap } from './componentCalculationPrefill';
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

export function Api510ShellTubeCalculationWorkspace({ fieldValues = {}, hasReportContext = false, reportCalculations = [], onSaveSnapshot, onCreateFindingDraft }: Props) {
  const [componentKey, setComponentKey] = useState('shell');
  const [pressureSide, setPressureSide] = useState<'shell-side'|'tube-side'>('shell-side');
  const [parentComponent, setParentComponent] = useState('shell');
  const [nozzleLocation, setNozzleLocation] = useState('shell');
  const [calculationType, setCalculationType] = useState<Api510CalculationType>('ug-27-shell-tmin');
  const [designConditions, setDesignConditions] = useState<CalculationFieldValuesMap>({});
  const [inputs, setInputs] = useState<Record<string, unknown>>({
    designCode: 'ASME_VIII_DIV1',
    stressEra: 'From1999Onward'
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
      ...fieldValues,
      ...(hasReportContext ? {} : {
        'api510.external.exchanger.shell-tube.shell-side.design-pressure': designConditions.shellSideDesignPressure,
        'api510.external.exchanger.shell-tube.shell-side.design-temperature': designConditions.shellSideDesignTemperature,
        'api510.external.exchanger.shell-tube.tube-side.design-pressure': designConditions.tubeSideDesignPressure,
        'api510.external.exchanger.shell-tube.tube-side.design-temperature': designConditions.tubeSideDesignTemperature
      })
    }
  ), [componentKey, pressureSide, parentComponent, nozzleLocation, fieldValues, hasReportContext, designConditions]);

  const onRun = async () => {
    if (!prefill) return;
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
    <label>Component
      <select aria-label="Component" value={componentKey} onChange={(e) => {
        const next = e.target.value;
        setComponentKey(next);
        setCalculationType(next === 'nozzles' ? 'ug-45-nozzle-neck-tmin' : 'ug-27-shell-tmin');
      }}>
        {shellTubeComponents.map((c) => <option key={c.key} value={c.key}>{c.label}</option>)}
      </select>
    </label>
    <label>Pressure Side<select aria-label="Pressure Side" value={pressureSide} onChange={(e) => setPressureSide(e.target.value as 'shell-side'|'tube-side')}><option value="shell-side">shell-side</option><option value="tube-side">tube-side</option></select></label>
    {componentKey === 'nozzles' && <>
      <label>Parent Component<select aria-label="Parent Component" value={parentComponent} onChange={(e) => setParentComponent(e.target.value)}><option value="shell">shell</option><option value="channel-channel-head">channel-channel-head</option><option value="shell-cover">shell-cover</option><option value="bonnet-head">bonnet-head</option></select></label>
      <label>Nozzle Location<select aria-label="Nozzle Location" value={nozzleLocation} onChange={(e) => setNozzleLocation(e.target.value)}><option value="shell">shell</option><option value="channel-channel-head">channel-channel-head</option><option value="bonnet-head">bonnet-head</option><option value="tubesheet-area">tubesheet-area</option></select></label>
    </>}
    <label>Calculation Type<select aria-label="Calculation Type" value={calculationType} onChange={(e) => setCalculationType(e.target.value as Api510CalculationType)}>
      <option value="ug-27-shell-tmin">UG-27 Shell Tmin</option>
      <option value="ug-45-nozzle-neck-tmin">UG-45 Nozzle Neck Tmin</option>
      <option value="review-only">Review Only</option>
    </select></label>

    <div>Resolved Pressure Side: {prefill?.resolvedPressureSide ?? 'n/a'}</div>
    <div>Design Pressure: {prefill?.designPressureFieldTag ?? 'n/a'} = {String(prefill?.designPressureValue ?? 'n/a')}</div>
    <div>Design Temperature: {prefill?.designTemperatureFieldTag ?? 'n/a'} = {String(prefill?.designTemperatureValue ?? 'n/a')}</div>
    <div>Tmin supported: {String(prefill?.supportsTminCalculation ?? false)}</div>
    <div>UG-45 supported: {String(prefill?.supportsUg45 ?? false)}</div>
    {[...(prefill?.missingRequiredInputWarnings ?? []), ...(prefill?.designPressureValue === undefined ? ['Design pressure value is not available from selected source.'] : []), ...(prefill?.designTemperatureValue === undefined ? ['Design temperature value is not available from selected source.'] : [])].map((w) => <div key={w}>warning: {w}</div>)}
    {prefill?.notes.map((n) => <div key={n}>note: {n}</div>)}

    {!hasReportContext && <fieldset><legend>Design Conditions</legend>
      <label>Shell-Side Design Pressure<input aria-label="Shell-Side Design Pressure" value={String(designConditions.shellSideDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,shellSideDesignPressure:e.target.value}))} /></label>
      <label>Shell-Side Design Temperature<input aria-label="Shell-Side Design Temperature" value={String(designConditions.shellSideDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,shellSideDesignTemperature:e.target.value}))} /></label>
      <label>Tube-Side Design Pressure<input aria-label="Tube-Side Design Pressure" value={String(designConditions.tubeSideDesignPressure ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,tubeSideDesignPressure:e.target.value}))} /></label>
      <label>Tube-Side Design Temperature<input aria-label="Tube-Side Design Temperature" value={String(designConditions.tubeSideDesignTemperature ?? '')} onChange={(e)=>setDesignConditions((p)=>({...p,tubeSideDesignTemperature:e.target.value}))} /></label>
    </fieldset>}

    <fieldset><legend>Material Allowable Stress Resolution</legend>
      {['designCode','stressEra','materialSpec','materialGrade','productForm','alloyUNS','classConditionTemper','resolvedAllowableStressPsi'].map((k) => <label key={k}>{k === 'materialSpec' ? 'Material Specification' : k === 'materialGrade' ? 'Grade' : k === 'productForm' ? 'Product Form' : k === 'resolvedAllowableStressPsi' ? 'Resolved Allowable Stress' : k}<input aria-label={k === 'resolvedAllowableStressPsi' ? 'Resolved Allowable Stress' : k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
      <div>Source/Warnings: {String(inputs.materialResolutionMessage ?? '')}</div>
      <label><input type="checkbox" aria-label="Use manual allowable stress override" checked={Boolean(inputs.useManualAllowableStressOverride)} onChange={(e)=>setInputs((p)=>({...p,useManualAllowableStressOverride:e.target.checked}))} />Use manual allowable stress override</label>
      {Boolean(inputs.useManualAllowableStressOverride) && <>
        <label>allowableStressPsi<input aria-label="allowableStressPsi" value={String(inputs.allowableStressPsi ?? '')} onChange={(e)=>setInputs((p)=>({...p,allowableStressPsi:e.target.value}))} /></label>
        <label>overrideReason<input aria-label="overrideReason" value={String(inputs.overrideReason ?? '')} onChange={(e)=>setInputs((p)=>({...p,overrideReason:e.target.value}))} /></label>
      </>}
    </fieldset>

    {showUg27 && <>
      {['jointEfficiency','insideDiameterIn','outsideDiameterIn','originalThicknessIn','providedThicknessIn','corrosionAllowanceIn','diameterBasis'].map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
    </>}
    {showUg45 && <>
      {['parentThicknessSource','shellOrHeadRequiredThicknessIn','shellOrHeadExternalRequiredThicknessIn','outsideDiameterIn','insideDiameterIn','nominalThicknessIn','originalThicknessIn','nominalPipeSize','corrosionAllowanceIn','jointEfficiency','externalPressurePsi','ug16MinimumThicknessIn','ug45TableMinimumThicknessIn'].map((k) => <label key={k}>{k}<input aria-label={k} value={String(inputs[k] ?? '')} onChange={(e) => setInputs((p) => ({...p,[k]: e.target.value}))} /></label>)}
      <label><input aria-label="externalPressureNotApplicable" type="checkbox" onChange={(e) => setInputs((p) => ({...p,externalPressureApplicable: !e.target.checked}))} />External pressure not applicable</label>
      <label><input aria-label="ug16NotApplicable" type="checkbox" onChange={(e) => setInputs((p) => ({...p,ug16MinimumThicknessApplicable: !e.target.checked}))} />UG-16 minimum not applicable</label>
    </>}

    <button onClick={onRun}>Run Calculation</button>
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
  </section>;
}
