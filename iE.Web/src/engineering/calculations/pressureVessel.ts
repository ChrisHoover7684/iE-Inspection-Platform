import type { EngineeringCalculationResult } from '../types';

export type PressureVesselMarginInputs = { requiredWithCorrosionAllowanceIn: number; providedThicknessIn: number; };
export type PressureVesselMarginOutputs = { marginIn: number; isAcceptable: boolean; };

export function calculateRequiredThicknessMargin(inputs: PressureVesselMarginInputs): EngineeringCalculationResult<PressureVesselMarginInputs, PressureVesselMarginOutputs> {
  const marginIn = inputs.providedThicknessIn - inputs.requiredWithCorrosionAllowanceIn;
  return {
    calculationType: 'pressure-vessel-margin',
    formulaVersion: 'asme-viii-div1-foundation-v1',
    inputs,
    outputs: { marginIn, isAcceptable: marginIn >= 0 },
    warnings: []
  };
}
