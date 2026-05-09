import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type TankShellCorrosionInputs = { currentThicknessIn: number; requiredThicknessIn: number; corrosionAllowanceIn: number; };
export type TankShellCorrosionOutputs = { requiredWithCorrosionAllowanceIn: number; remainingMarginIn: number; };

export function calculateTankShellCorrosionMargin(inputs: TankShellCorrosionInputs): EngineeringCalculationResult<TankShellCorrosionInputs, TankShellCorrosionOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();
  const requiredWithCorrosionAllowanceIn = inputs.requiredThicknessIn + inputs.corrosionAllowanceIn;
  const remainingMarginIn = inputs.currentThicknessIn - requiredWithCorrosionAllowanceIn;
  if (inputs.currentThicknessIn <= 0) warnings.push({ code: 'CURRENT_THICKNESS_NON_POSITIVE', message: 'Current thickness input should be greater than zero.', severity: 'error' });
  if (inputs.requiredThicknessIn <= 0) warnings.push({ code: 'REQUIRED_THICKNESS_NON_POSITIVE', message: 'Required thickness input should be greater than zero.', severity: 'warning' });
  if (remainingMarginIn < 0) warnings.push({ code: 'NEGATIVE_MARGIN', message: 'Current thickness is below required thickness plus corrosion allowance.', severity: 'error' });
  return {
    id: `tank-shell-corrosion-margin-${calculatedAt}`,
    calculationType: 'tank-shell-corrosion-margin',
    displayName: 'Tank Shell Corrosion Margin Helper',
    formulaVersion: 'api-653-foundation-v1',
    standardReferences: [{ standard: 'API 653', note: 'Foundation margin helper only; not a full code compliance check.' }],
    inputs,
    outputs: {
      requiredWithCorrosionAllowanceIn,
      remainingMarginIn
    },
    warnings,
    calculatedAt,
    insertLabel: `Tank shell margin helper result (${remainingMarginIn.toFixed(4)} in margin)`
  };
}
