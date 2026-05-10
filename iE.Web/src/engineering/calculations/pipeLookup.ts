import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type PipeLookupInputs = { nps: string; schedule: string; outsideDiameter: number; nominalThickness: number; };
export type PipeLookupOutputs = { insideDiameter: number; lowerLimitMinus12_5: number; upperLimitPlus12_5: number; };

export function calculatePipeDimensions(inputs: PipeLookupInputs): EngineeringCalculationResult<PipeLookupInputs, PipeLookupOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const insideDiameter = inputs.outsideDiameter - (2 * inputs.nominalThickness);
  if (inputs.outsideDiameter <= 0) warnings.push({ code: 'OUTSIDE_DIAMETER_NON_POSITIVE', message: 'Outside diameter must be greater than zero.', severity: 'error' });
  if (inputs.nominalThickness <= 0) warnings.push({ code: 'NOMINAL_THICKNESS_NON_POSITIVE', message: 'Nominal thickness must be greater than zero.', severity: 'error' });
  if (insideDiameter <= 0) warnings.push({ code: 'INSIDE_DIAMETER_NON_POSITIVE', message: 'Computed inside diameter is non-positive and not physically valid.', severity: 'error' });

  const hasInvalidDimensions = insideDiameter <= 0;

  return {
    id: `pipe-lookup-${calculatedAt}`,
    calculationType: 'pipe-lookup',
    displayName: 'Pipe Dimension Foundation Helper',
    formulaVersion: 'asme-b36-foundation-v1',
    standardReferences: [{ standard: 'ASME B36', note: 'Lookup foundation helper only; verify dimensions against governing owner tables.' }],
    inputs,
    outputs: {
      insideDiameter,
      lowerLimitMinus12_5: inputs.nominalThickness * 0.875,
      upperLimitPlus12_5: inputs.nominalThickness * 1.125
    },
    warnings,
    calculatedAt,
    insertLabel: hasInvalidDimensions
      ? `${inputs.nps}" Sch ${inputs.schedule} pipe dimensions (invalid dimensions; ID ${insideDiameter.toFixed(4)} in)`
      : `${inputs.nps}" Sch ${inputs.schedule} pipe dimensions (ID ${insideDiameter.toFixed(4)} in)`
  };
}
