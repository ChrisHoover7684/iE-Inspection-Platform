import { pressureVesselApi } from '../api';
import type { EngineeringCalculationWarning, InspectionCalculationSnapshot } from '../engineering/types';
import type { AttachmentLocation, NozzleCalculationRequest } from '../types';
import type { ComponentCalculationPrefill } from './componentCalculationPrefill';

export type Api510CalculationType = 'ug-27-shell-tmin' | 'ug-32-formed-head-tmin' | 'ug-45-nozzle-neck-tmin' | 'review-only';
type Ug27DiameterBasis = 'inside-diameter-basis' | 'outside-diameter-basis' | 'both-known';

export type ComponentCalculationExecutionInput = {
  prefill: ComponentCalculationPrefill;
  calculationType: Api510CalculationType;
  manualInputs?: Record<string, unknown>;
  snapshotContext?: { linkedSectionId?: string; linkedFieldId?: string; linkedFindingId?: string };
};

export type ComponentCalculationExecutionResult = {
  success: boolean;
  calculationType: Api510CalculationType;
  componentKey: string;
  componentLabel: string;
  pressureSide: string;
  designPressureUsed?: number;
  designTemperatureUsed?: number;
  inputsUsed: Record<string, unknown>;
  resultSummary: string;
  warnings: string[];
  snapshotReadyPayload?: InspectionCalculationSnapshot;
};

const toNumber = (v: unknown): number | undefined => {
  if (typeof v === 'number' && Number.isFinite(v)) return v;
  if (typeof v === 'string' && v.trim() !== '') {
    const parsed = Number(v);
    if (Number.isFinite(parsed)) return parsed;
  }
  return undefined;
};

const hasValue = (v: unknown) => v !== undefined && v !== null && !(typeof v === 'string' && v.trim() === '');
const toSnapshotWarnings = (warningMessages: string[]): EngineeringCalculationWarning[] =>
  warningMessages.map((message, index) => ({ code: `API510_BRIDGE_${index + 1}`, message, severity: 'warning' }));

const buildApiFailure = (base: Omit<ComponentCalculationExecutionResult, 'success' | 'resultSummary' | 'warnings'>, warnings: string[], message = 'API calculation failed'): ComponentCalculationExecutionResult => ({
  ...base,
  success: false,
  resultSummary: message,
  warnings
});

const toApiErrorMessage = (error: unknown): string => {
  if (!error || typeof error !== 'object') return 'Unknown API error.';
  const e = error as { status?: number; message?: string; body?: { message?: string } };
  const status = typeof e.status === 'number' ? `status ${e.status}` : undefined;
  const msg = typeof e.message === 'string' ? e.message : undefined;
  const bodyMsg = typeof e.body?.message === 'string' ? e.body.message : undefined;
  return [status, msg, bodyMsg].filter(Boolean).join(': ') || 'Unknown API error.';
};

const resolveAttachmentLocation = (parent?: string, location?: string): { value?: AttachmentLocation; warning?: string } => {
  const key = location ?? parent;
  if (key === 'shell') return { value: 'Shell' };
  if (key === 'shell-cover' || key === 'bonnet-head') return { value: 'Head' };
  if (key === 'channel-channel-head') return { warning: 'Nozzle attachment location for channel/channel-head is not supported by current backend attachment enum.' };
  if (key === 'tubesheet-area') return { warning: 'Tubesheet-area nozzle attachment is review-only and not executable in this bridge.' };
  return { warning: `Nozzle attachment location is unsupported for parent/location "${key ?? 'unspecified'}".` };
};

export async function executeApi510ComponentCalculation({ prefill, calculationType, manualInputs, snapshotContext }: ComponentCalculationExecutionInput): Promise<ComponentCalculationExecutionResult> {
  const warnings = [...prefill.missingRequiredInputWarnings];
  const pressure = toNumber(prefill.designPressureValue);
  const temperature = toNumber(prefill.designTemperatureValue);

  if (!prefill.supportsTminCalculation) {
    return { success: false, calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, inputsUsed: manualInputs ?? {}, resultSummary: 'Component is not Tmin-calculation eligible.', warnings: [...warnings, 'Component is not Tmin-calculation eligible.'] };
  }

  if (pressure === undefined) warnings.push('Design pressure is missing.');
  if (temperature === undefined) warnings.push('Design temperature is missing.');

  const fail = (message: string): ComponentCalculationExecutionResult => ({
    success: false,
    calculationType,
    componentKey: prefill.componentKey,
    componentLabel: prefill.componentLabel,
    pressureSide: prefill.resolvedPressureSide,
    designPressureUsed: pressure,
    designTemperatureUsed: temperature,
    inputsUsed: manualInputs ?? {},
    resultSummary: message,
    warnings
  });

  const materialStress = toNumber(manualInputs?.allowableStressPsi);
  if (materialStress === undefined) warnings.push('Material allowable stress input is missing.');
  if (pressure === undefined || temperature === undefined) return fail('Calculation blocked: missing required design condition inputs.');

  if (calculationType === 'ug-27-shell-tmin') {
    const jointEfficiency = toNumber(manualInputs?.jointEfficiency);
    const insideDiameter = toNumber(manualInputs?.insideDiameterIn);
    const outsideDiameter = toNumber(manualInputs?.outsideDiameterIn);
    const originalThickness = toNumber(manualInputs?.originalThicknessIn);
    const providedThickness = toNumber(manualInputs?.providedThicknessIn);
    const corrosionAllowanceProvided = hasValue(manualInputs?.corrosionAllowanceIn);
    const corrosionAllowance = toNumber(manualInputs?.corrosionAllowanceIn);
    const diameterBasis = manualInputs?.diameterBasis as Ug27DiameterBasis | undefined;

    if (jointEfficiency === undefined) warnings.push('Joint efficiency is required for UG-27 shell calculation.');
    if (insideDiameter === undefined || outsideDiameter === undefined) warnings.push('UG-27 requires both inside diameter and outside diameter for this bridge.');
    if (diameterBasis && !['inside-diameter-basis', 'outside-diameter-basis', 'both-known'].includes(diameterBasis)) warnings.push('UG-27 diameter basis must be inside-diameter-basis, outside-diameter-basis, or both-known when provided.');
    if (originalThickness === undefined) warnings.push('Original thickness is required for UG-27 shell calculation.');
    if (providedThickness === undefined) warnings.push('Provided/current thickness is required for UG-27 shell calculation.');
    if (!corrosionAllowanceProvided || corrosionAllowance === undefined) warnings.push('Corrosion allowance is required for UG-27 shell calculation (zero allowed when explicitly provided).');
    if (warnings.some((w) => w.includes('required') || w.includes('missing'))) return fail('UG-27 execution blocked by missing required inputs.');

    const input = {
      designPressurePsi: pressure,
      allowableStressPsi: materialStress!,
      insideDiameterIn: insideDiameter!,
      outsideDiameterIn: outsideDiameter!,
      originalThicknessIn: originalThickness!,
      providedThicknessIn: providedThickness!,
      corrosionAllowanceIn: corrosionAllowance!,
      jointEfficiency: jointEfficiency!
    };

    try {
      const response = await pressureVesselApi.calculateCylindrical({ input, materialStress: {
        designCode: 'ASME_VIII_DIV1',
        stressEra: 'From1999Onward',
        designTemperatureF: temperature,
        materialSpec: '', materialGrade: '', productForm: '', alloyUNS: '', classConditionTemper: '', manualAllowableStress: true, allowableStressPsi: materialStress!
      }});
      warnings.push(...(response.warnings ?? []));
      const snapshotReadyPayload: InspectionCalculationSnapshot = {
        id: crypto.randomUUID(), calculationType, displayName: 'API 510 External Tmin Bridge', formulaVersion: 'api510-bridge-v1', calculatedAt: new Date().toISOString(), insertLabel: `${prefill.componentLabel} UG-27 Tmin`,
        standardReferences: [{ standard: 'ASME Section VIII Div 1', paragraph: 'UG-27', note: 'API 510 external bridge execution' }],
        warnings: toSnapshotWarnings(warnings),
        inputs: { prefill, manualInputs, sourceDesignFieldTags: { pressure: prefill.designPressureFieldTag, temperature: prefill.designTemperatureFieldTag } },
        outputs: { response, metadata: { equipmentSubtype: prefill.equipmentSubtype, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, calculationType, diameterBasisUsed: diameterBasis ?? 'both-known', diameterRequirement: 'both-required', manualInputsUsed: input } },
        ...snapshotContext
      };
      return { success: true, calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: input, resultSummary: 'UG-27 calculation executed.', warnings, snapshotReadyPayload };
    } catch (error) {
      const warningList = [...warnings, `API calculation failed: ${toApiErrorMessage(error)}`];
      return buildApiFailure({ calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: input }, warningList);
    }
  }

  if (calculationType === 'ug-45-nozzle-neck-tmin') {
    const parentThicknessSource = manualInputs?.parentThicknessSource;
    const shellOrHeadRequiredThickness = toNumber(manualInputs?.shellOrHeadRequiredThicknessIn);
    const shellOrHeadExternalRequiredThickness = toNumber(manualInputs?.shellOrHeadExternalRequiredThicknessIn);
    const nozzleOutsideDiameter = toNumber(manualInputs?.outsideDiameterIn);
    const nozzleInsideDiameter = toNumber(manualInputs?.insideDiameterIn);
    const nominalThickness = toNumber(manualInputs?.nominalThicknessIn);
    const originalThickness = toNumber(manualInputs?.originalThicknessIn);
    const nominalPipeSize = manualInputs?.nominalPipeSize;
    const corrosionAllowanceProvided = hasValue(manualInputs?.corrosionAllowanceIn);
    const corrosionAllowance = toNumber(manualInputs?.corrosionAllowanceIn);
    const jointEfficiency = toNumber(manualInputs?.jointEfficiency);
    const externalPressureProvided = hasValue(manualInputs?.externalPressurePsi);
    const externalPressureValue = toNumber(manualInputs?.externalPressurePsi);
    const externalPressureApplicable = manualInputs?.externalPressureApplicable;
    const externalPressureMarkedNotApplicable = externalPressureApplicable === false;
    const ug16MinimumThicknessProvided = hasValue(manualInputs?.ug16MinimumThicknessIn);
    const ug16MinimumThickness = toNumber(manualInputs?.ug16MinimumThicknessIn);
    const ug16MinimumThicknessNotApplicable = manualInputs?.ug16MinimumThicknessApplicable === false;
    const ug45TableMinimumThicknessProvided = hasValue(manualInputs?.ug45TableMinimumThicknessIn);
    const ug45TableMinimumThickness = toNumber(manualInputs?.ug45TableMinimumThicknessIn);

    if (!parentThicknessSource) warnings.push('Parent thickness source is required for UG-45 nozzle calculation.');
    if (!prefill.selectedParentComponent) warnings.push('Parent component is required but missing.');
    if (!prefill.selectedNozzleLocation) warnings.push('Nozzle location is required but missing.');
    if (shellOrHeadRequiredThickness === undefined) warnings.push('Shell/head required thickness is required for UG-45 nozzle calculation.');
    if (shellOrHeadExternalRequiredThickness === undefined) warnings.push('Shell/head external required thickness is required for UG-45 nozzle calculation.');
    if (nozzleOutsideDiameter === undefined || nozzleInsideDiameter === undefined) warnings.push('Nozzle outside and inside diameter inputs are required for UG-45 nozzle calculation.');
    if (nominalThickness === undefined || originalThickness === undefined) warnings.push('Nozzle nominal/original thickness inputs are required for UG-45 nozzle calculation.');
    if (!hasValue(nominalPipeSize)) warnings.push('Nozzle nominal pipe size (or equivalent size) is required for UG-45 nozzle calculation.');
    if (!corrosionAllowanceProvided || corrosionAllowance === undefined) warnings.push('Corrosion allowance is required for UG-45 nozzle calculation (zero allowed when explicitly provided).');
    if (jointEfficiency === undefined) warnings.push('Joint efficiency is required for UG-45 nozzle calculation.');
    if ((!externalPressureProvided && !externalPressureMarkedNotApplicable) || (externalPressureProvided && externalPressureValue === undefined)) warnings.push('External pressure is required for UG-45 unless explicitly marked not applicable.');
    if ((!ug16MinimumThicknessProvided && !ug16MinimumThicknessNotApplicable) || (ug16MinimumThicknessProvided && ug16MinimumThickness === undefined)) warnings.push('UG-16 minimum thickness is required for UG-45 unless explicitly marked not applicable.');

    const attachment = resolveAttachmentLocation(prefill.selectedParentComponent, prefill.selectedNozzleLocation);
    if (!attachment.value && attachment.warning) warnings.push(attachment.warning);

    if (warnings.some((w) => w.includes('required') || w.includes('missing') || w.includes('not supported') || w.includes('unsupported') || w.includes('review-only'))) {
      return fail('UG-45 execution blocked by missing or unsupported required inputs.');
    }

    const resolvedExternalPressure = externalPressureMarkedNotApplicable ? 0 : externalPressureValue;
    const resolvedUg16MinimumThickness = ug16MinimumThicknessNotApplicable ? 0 : ug16MinimumThickness;
    const input: NozzleCalculationRequest['input'] = {
      designCode: 'ASME_VIII_DIV1', designPressurePsi: pressure, externalPressurePsi: resolvedExternalPressure!, designTemperatureF: temperature,
      jointEfficiency: jointEfficiency!, corrosionAllowanceIn: corrosionAllowance!,
      manualAllowableStress: true, allowableStressPsi: materialStress!, materialSpec: '', materialGrade: '', materialProductForm: '', codeEra: 'Post1999',
      attachmentLocation: attachment.value!, shellOrHeadRequiredThicknessIn: shellOrHeadRequiredThickness!, shellOrHeadExternalRequiredThicknessIn: shellOrHeadExternalRequiredThickness!,
      ug16MinimumThicknessIn: resolvedUg16MinimumThickness!, nozzleType: 'PipeNozzle', useOdForTa: false, useIdForTa: true,
      outsideDiameterIn: nozzleOutsideDiameter!, insideDiameterIn: nozzleInsideDiameter!, nominalThicknessIn: nominalThickness!,
      originalThicknessIn: originalThickness!, nominalPipeSize: String(nominalPipeSize), ug45TableMinimumThicknessIn: ug45TableMinimumThicknessProvided ? ug45TableMinimumThickness ?? null : null
    };

    try {
      const response = await pressureVesselApi.calculateNozzle({ input });
      warnings.push(...(response.warnings ?? []), ...(response.result?.warnings ?? []));
      if (response.result?.isValid === false || response.result?.errorMessage) {
        const failureWarnings = [...warnings];
        if (response.result?.errorMessage) failureWarnings.push(`API result error: ${response.result.errorMessage}`);
        return {
          success: false,
          calculationType,
          componentKey: prefill.componentKey,
          componentLabel: prefill.componentLabel,
          pressureSide: prefill.resolvedPressureSide,
          designPressureUsed: pressure,
          designTemperatureUsed: temperature,
          inputsUsed: input as unknown as Record<string, unknown>,
          resultSummary: 'API calculation failed',
          warnings: failureWarnings,
          snapshotReadyPayload: {
            id: crypto.randomUUID(), calculationType, displayName: 'API 510 External Tmin Bridge', formulaVersion: 'api510-bridge-v1', calculatedAt: new Date().toISOString(), insertLabel: `${prefill.componentLabel} UG-45 Tmin`,
            standardReferences: [{ standard: 'ASME Section VIII Div 1', paragraph: 'UG-45', note: 'API 510 external bridge execution' }], warnings: toSnapshotWarnings(failureWarnings),
            inputs: { prefill, manualInputs, metadata: { equipmentSubtype: prefill.equipmentSubtype, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, parentComponent: prefill.selectedParentComponent, nozzleLocation: prefill.selectedNozzleLocation, pressureSide: prefill.resolvedPressureSide, calculationType, sourceDesignFieldTags: { pressure: prefill.designPressureFieldTag, temperature: prefill.designTemperatureFieldTag }, defaultDecisions: { externalPressure: externalPressureMarkedNotApplicable ? 'explicit-not-applicable' : 'explicit-value', ug16MinimumThickness: ug16MinimumThicknessNotApplicable ? 'explicit-not-applicable' : 'explicit-value', ug45TableMinimumThickness: ug45TableMinimumThicknessProvided ? 'explicit-value-or-null' : 'omitted-as-null' }, manualInputsUsed: input } },
            outputs: { response }, ...snapshotContext
          }
        };
      }
      const snapshotReadyPayload: InspectionCalculationSnapshot = {
        id: crypto.randomUUID(), calculationType, displayName: 'API 510 External Tmin Bridge', formulaVersion: 'api510-bridge-v1', calculatedAt: new Date().toISOString(), insertLabel: `${prefill.componentLabel} UG-45 Tmin`,
        standardReferences: [{ standard: 'ASME Section VIII Div 1', paragraph: 'UG-45', note: 'API 510 external bridge execution' }], warnings: toSnapshotWarnings(warnings),
        inputs: { prefill, manualInputs, metadata: { equipmentSubtype: prefill.equipmentSubtype, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, parentComponent: prefill.selectedParentComponent, nozzleLocation: prefill.selectedNozzleLocation, pressureSide: prefill.resolvedPressureSide, calculationType, sourceDesignFieldTags: { pressure: prefill.designPressureFieldTag, temperature: prefill.designTemperatureFieldTag }, defaultDecisions: { externalPressure: externalPressureMarkedNotApplicable ? 'explicit-not-applicable' : 'explicit-value', ug16MinimumThickness: ug16MinimumThicknessNotApplicable ? 'explicit-not-applicable' : 'explicit-value', ug45TableMinimumThickness: ug45TableMinimumThicknessProvided ? 'explicit-value-or-null' : 'omitted-as-null' }, manualInputsUsed: input } },
        outputs: { response }, ...snapshotContext
      };
      return { success: true, calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: input as unknown as Record<string, unknown>, resultSummary: 'UG-45 calculation executed.', warnings, snapshotReadyPayload };
    } catch (error) {
      const warningList = [...warnings, `API calculation failed: ${toApiErrorMessage(error)}`];
      return buildApiFailure({ calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: manualInputs ?? {} }, warningList);
    }
  }

  return fail('Calculation type is currently non-executable/review-only for this first-pass bridge.');
}
