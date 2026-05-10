import type { B313ThicknessInput, B313ThicknessResult } from '../../types';
import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type B31_3PipingOutputs = {
  allowableStressPsi: number | null;
  eFactor: number | null;
  yCoefficient: number | null;
  wFactor: number | null;
  requiredThicknessIn: number | null;
};

export function toB31_3EngineeringSnapshot(
  inputs: B313ThicknessInput,
  apiResult: B313ThicknessResult,
): EngineeringCalculationResult<B313ThicknessInput, B31_3PipingOutputs> {
  const calculatedAt = new Date().toISOString();
  const warnings: EngineeringCalculationWarning[] = [];

  if (!apiResult.success) {
    warnings.push({ code: 'B313_CALCULATION_FAILED', message: apiResult.message, severity: 'error' });
  }

  warnings.push({
    code: 'USER_INPUT_VALIDATION_REQUIRED',
    message: 'Reviewer validation required for allowable stress, weld factors, Y coefficient, and other user-entered code values.',
    severity: 'warning'
  });

  return {
    id: `b31-3-piping-${calculatedAt}`,
    calculationType: 'b31-3-piping',
    displayName: 'ASME B31.3 Piping Wall Thickness Foundation Helper',
    formulaVersion: 'asme-b31.3-service-v1',
    standardReferences: [
      { standard: 'ASME B31.3', note: 'Calculated from backend B31.3 engine/material stress source-of-truth service.' },
      { standard: 'API 570', note: 'Insertable engineering snapshot foundation for piping inspection workflows.' }
    ],
    inputs,
    outputs: {
      allowableStressPsi: apiResult.allowableStressPsi,
      eFactor: apiResult.eFactor,
      yCoefficient: apiResult.yCoefficient,
      wFactor: apiResult.wFactor,
      requiredThicknessIn: apiResult.requiredThicknessIn,
    },
    warnings,
    calculatedAt,
    insertLabel: `B31.3 piping check (${apiResult.success ? `required thickness ${apiResult.requiredThicknessIn?.toFixed(4) ?? 'N/A'} in` : 'requires review'})`
  };
}
