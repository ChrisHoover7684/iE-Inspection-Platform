import { pressureVesselApi } from '../api';
import type { InspectionCalculationSnapshot } from '../engineering/types';
import type { ComponentCalculationPrefill } from './componentCalculationPrefill';

export type Api510CalculationType = 'ug-27-shell-tmin' | 'ug-32-formed-head-tmin' | 'ug-45-nozzle-neck-tmin' | 'review-only';

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
    if (jointEfficiency === undefined) warnings.push('Joint efficiency is required for UG-27 shell calculation.');
    if (warnings.some((w) => w.includes('required') || w.includes('missing'))) return fail('UG-27 execution blocked by missing required inputs.');
    const input = {
      designPressurePsi: pressure,
      allowableStressPsi: materialStress!,
      insideDiameterIn: toNumber(manualInputs?.insideDiameterIn) ?? 1,
      outsideDiameterIn: toNumber(manualInputs?.outsideDiameterIn) ?? 1.25,
      originalThicknessIn: toNumber(manualInputs?.originalThicknessIn) ?? 0.5,
      providedThicknessIn: toNumber(manualInputs?.providedThicknessIn) ?? 0.5,
      corrosionAllowanceIn: toNumber(manualInputs?.corrosionAllowanceIn) ?? 0,
      jointEfficiency: jointEfficiency!
    };
    const response = await pressureVesselApi.calculateCylindrical({ input, materialStress: {
      designCode: 'ASME_VIII_DIV1',
      stressEra: 'From1999Onward',
      designTemperatureF: temperature,
      materialSpec: '', materialGrade: '', productForm: '', alloyUNS: '', classConditionTemper: '', manualAllowableStress: true, allowableStressPsi: materialStress!
    }});
    const snapshotReadyPayload: InspectionCalculationSnapshot = {
      id: crypto.randomUUID(), calculationType, displayName: 'API 510 External Tmin Bridge', formulaVersion: 'api510-bridge-v1', calculatedAt: new Date().toISOString(), insertLabel: `${prefill.componentLabel} UG-27 Tmin`,
      standardReferences: [{ standard: 'ASME Section VIII Div 1', paragraph: 'UG-27', note: 'API 510 external bridge execution' }],
      warnings: [],
      inputs: { prefill, manualInputs, sourceDesignFieldTags: { pressure: prefill.designPressureFieldTag, temperature: prefill.designTemperatureFieldTag } },
      outputs: { response, metadata: { equipmentSubtype: prefill.equipmentSubtype, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide } },
      ...snapshotContext
    };
    return { success: true, calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: input, resultSummary: 'UG-27 calculation executed.', warnings, snapshotReadyPayload };
  }

  if (calculationType === 'ug-45-nozzle-neck-tmin') {
    const parentThicknessSource = manualInputs?.parentThicknessSource;
    if (!parentThicknessSource) warnings.push('Parent thickness source is required for UG-45 nozzle calculation.');
    if (!prefill.selectedParentComponent) warnings.push('Parent component is required but missing.');
    if (!prefill.selectedNozzleLocation) warnings.push('Nozzle location is required but missing.');
    if (warnings.some((w) => w.includes('required') || w.includes('missing'))) return fail('UG-45 execution blocked by missing required inputs.');

    const input = {
      designCode: 'ASME_VIII_DIV1', designPressurePsi: pressure, externalPressurePsi: toNumber(manualInputs?.externalPressurePsi) ?? 0, designTemperatureF: temperature,
      jointEfficiency: toNumber(manualInputs?.jointEfficiency) ?? 1, corrosionAllowanceIn: toNumber(manualInputs?.corrosionAllowanceIn) ?? 0,
      manualAllowableStress: true, allowableStressPsi: materialStress!, materialSpec: '', materialGrade: '', materialProductForm: '', codeEra: 'From1999Onward',
      attachmentLocation: 'Shell', shellOrHeadRequiredThicknessIn: toNumber(manualInputs?.shellOrHeadRequiredThicknessIn) ?? 0.25, shellOrHeadExternalRequiredThicknessIn: toNumber(manualInputs?.shellOrHeadExternalRequiredThicknessIn) ?? 0.25,
      ug16MinimumThicknessIn: toNumber(manualInputs?.ug16MinimumThicknessIn) ?? 0, nozzleType: 'set_on', useOdForTa: false, useIdForTa: true,
      outsideDiameterIn: toNumber(manualInputs?.outsideDiameterIn) ?? 2.375, insideDiameterIn: toNumber(manualInputs?.insideDiameterIn) ?? 2, nominalThicknessIn: toNumber(manualInputs?.nominalThicknessIn) ?? 0.154,
      originalThicknessIn: toNumber(manualInputs?.originalThicknessIn) ?? 0.154, nominalPipeSize: String(manualInputs?.nominalPipeSize ?? 'NPS 2'), ug45TableMinimumThicknessIn: toNumber(manualInputs?.ug45TableMinimumThicknessIn) ?? null
    } as const;
    const response = await pressureVesselApi.calculateNozzle({ input: input as never });
    const snapshotReadyPayload: InspectionCalculationSnapshot = {
      id: crypto.randomUUID(), calculationType, displayName: 'API 510 External Tmin Bridge', formulaVersion: 'api510-bridge-v1', calculatedAt: new Date().toISOString(), insertLabel: `${prefill.componentLabel} UG-45 Tmin`,
      standardReferences: [{ standard: 'ASME Section VIII Div 1', paragraph: 'UG-45', note: 'API 510 external bridge execution' }], warnings: [],
      inputs: { prefill, manualInputs, metadata: { equipmentSubtype: prefill.equipmentSubtype, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, parentComponent: prefill.selectedParentComponent, nozzleLocation: prefill.selectedNozzleLocation, pressureSide: prefill.resolvedPressureSide, sourceDesignFieldTags: { pressure: prefill.designPressureFieldTag, temperature: prefill.designTemperatureFieldTag } } },
      outputs: { response }, ...snapshotContext
    };
    return { success: true, calculationType, componentKey: prefill.componentKey, componentLabel: prefill.componentLabel, pressureSide: prefill.resolvedPressureSide, designPressureUsed: pressure, designTemperatureUsed: temperature, inputsUsed: input as unknown as Record<string, unknown>, resultSummary: 'UG-45 calculation executed.', warnings, snapshotReadyPayload };
  }

  return fail('Calculation type is currently non-executable/review-only for this first-pass bridge.');
}
