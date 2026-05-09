import type { EngineeringCalculationResult } from '../types';

export type TankShellCorrosionInputs = { currentThicknessIn: number; requiredThicknessIn: number; corrosionAllowanceIn: number; };
export type TankShellCorrosionOutputs = { requiredWithCorrosionAllowanceIn: number; remainingMarginIn: number; };

export function calculateTankShellCorrosionMargin(inputs: TankShellCorrosionInputs): EngineeringCalculationResult<TankShellCorrosionInputs, TankShellCorrosionOutputs> {
  const requiredWithCorrosionAllowanceIn = inputs.requiredThicknessIn + inputs.corrosionAllowanceIn;
  return {
    calculationType: 'tank-shell-corrosion-margin',
    formulaVersion: 'api-653-foundation-v1',
    inputs,
    outputs: {
      requiredWithCorrosionAllowanceIn,
      remainingMarginIn: inputs.currentThicknessIn - requiredWithCorrosionAllowanceIn
    },
    warnings: []
  };
}
