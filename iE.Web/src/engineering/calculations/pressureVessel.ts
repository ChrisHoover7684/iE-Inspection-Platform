import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type PressureVesselMarginInputs = { requiredWithCorrosionAllowanceIn: number; providedThicknessIn: number; };
export type PressureVesselMarginOutputs = { marginIn: number; isAcceptable: boolean; };

export function calculateRequiredThicknessMargin(inputs: PressureVesselMarginInputs): EngineeringCalculationResult<PressureVesselMarginInputs, PressureVesselMarginOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const marginIn = inputs.providedThicknessIn - inputs.requiredWithCorrosionAllowanceIn;
  if (inputs.requiredWithCorrosionAllowanceIn <= 0) warnings.push({ code: 'REQUIRED_THICKNESS_NON_POSITIVE', message: 'Required thickness input should be greater than zero.', severity: 'warning' });
  if (inputs.providedThicknessIn <= 0) warnings.push({ code: 'PROVIDED_THICKNESS_NON_POSITIVE', message: 'Provided thickness input should be greater than zero.', severity: 'error' });
  if (marginIn < 0) warnings.push({ code: 'NEGATIVE_MARGIN', message: 'Provided thickness is below required thickness plus corrosion allowance.', severity: 'error' });
  return {
    id: `pressure-vessel-margin-${calculatedAt}`,
    calculationType: 'pressure-vessel-margin',
    displayName: 'Pressure Vessel Thickness Margin Helper',
    formulaVersion: 'asme-viii-div1-foundation-v1',
    standardReferences: [{ standard: 'ASME Section VIII Div 1', note: 'Foundation margin helper only; not a full code compliance check.' }],
    inputs,
    outputs: { marginIn, isAcceptable: marginIn >= 0 },
    warnings,
    calculatedAt,
    insertLabel: `Pressure vessel margin helper result (${marginIn.toFixed(4)} in margin)`
  };
}
