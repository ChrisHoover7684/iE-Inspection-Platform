import type { EngineeringCalculationResult } from '../types';

export type PipeLookupInputs = { nps: string; schedule: string; outsideDiameter: number; nominalThickness: number; };
export type PipeLookupOutputs = { insideDiameter: number; lowerLimitMinus12_5: number; upperLimitPlus12_5: number; };

export function calculatePipeDimensions(inputs: PipeLookupInputs): EngineeringCalculationResult<PipeLookupInputs, PipeLookupOutputs> {
  const insideDiameter = inputs.outsideDiameter - (2 * inputs.nominalThickness);
  return {
    calculationType: 'pipe-lookup',
    formulaVersion: 'asme-b36-foundation-v1',
    inputs,
    outputs: {
      insideDiameter,
      lowerLimitMinus12_5: inputs.nominalThickness * 0.875,
      upperLimitPlus12_5: inputs.nominalThickness * 1.125
    },
    warnings: []
  };
}
