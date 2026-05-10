import type { EngineeringCalculationResult, EngineeringCalculationWarning } from '../types';

export type B31_3PipingInputs = {
  designPressurePsi: number;
  outsideDiameterIn: number;
  allowableStressPsi: number;
  weldJointQualityFactor: number;
  coefficientY: number;
  mechanicalAllowanceIn: number;
  corrosionAllowanceIn: number;
  millToleranceFraction: number;
  providedThicknessIn: number;
  standardEdition?: string;
};

export type B31_3PipingOutputs = {
  pressureDesignThicknessIn: number;
  requiredThicknessWithAllowancesIn: number;
  requiredNominalThicknessIn: number | null;
  availableThicknessForPressureIn: number;
  thicknessMarginIn: number;
  isAcceptable: boolean;
};

export function calculateB31_3PipingWallThickness(inputs: B31_3PipingInputs): EngineeringCalculationResult<B31_3PipingInputs, B31_3PipingOutputs> {
  const warnings: EngineeringCalculationWarning[] = [];
  const calculatedAt = new Date().toISOString();

  if (inputs.designPressurePsi <= 0) warnings.push({ code: 'DESIGN_PRESSURE_NON_POSITIVE', message: 'Design pressure must be greater than zero.', severity: 'error' });
  if (inputs.outsideDiameterIn <= 0) warnings.push({ code: 'OUTSIDE_DIAMETER_NON_POSITIVE', message: 'Outside diameter must be greater than zero.', severity: 'error' });
  if (inputs.allowableStressPsi <= 0) warnings.push({ code: 'ALLOWABLE_STRESS_NON_POSITIVE', message: 'Allowable stress must be greater than zero.', severity: 'error' });
  if (inputs.weldJointQualityFactor <= 0 || inputs.weldJointQualityFactor > 1) warnings.push({ code: 'WELD_FACTOR_RANGE', message: 'Weld joint quality factor should typically be within (0, 1].', severity: 'warning' });
  if (inputs.coefficientY < 0 || inputs.coefficientY > 1) warnings.push({ code: 'Y_FACTOR_RANGE', message: 'Y coefficient should typically be within [0, 1].', severity: 'warning' });
  if (inputs.mechanicalAllowanceIn < 0) warnings.push({ code: 'MECHANICAL_ALLOWANCE_NEGATIVE', message: 'Mechanical allowance cannot be negative.', severity: 'error' });
  if (inputs.corrosionAllowanceIn < 0) warnings.push({ code: 'CORROSION_ALLOWANCE_NEGATIVE', message: 'Corrosion allowance cannot be negative.', severity: 'error' });
  if (inputs.millToleranceFraction < 0 || inputs.millToleranceFraction >= 1) warnings.push({ code: 'MILL_TOLERANCE_RANGE', message: 'Mill tolerance fraction must be in [0, 1).', severity: 'error' });
  if (inputs.providedThicknessIn <= 0) warnings.push({ code: 'PROVIDED_THICKNESS_NON_POSITIVE', message: 'Provided thickness must be greater than zero.', severity: 'error' });

  const denominator = (2 * inputs.allowableStressPsi * inputs.weldJointQualityFactor) + (inputs.designPressurePsi * inputs.coefficientY);
  const canCalculatePressureThickness = denominator > 0;
  if (!canCalculatePressureThickness) warnings.push({ code: 'INVALID_DENOMINATOR', message: 'Inputs produce a non-positive denominator; pressure design thickness cannot be computed.', severity: 'error' });

  const pressureDesignThicknessIn = canCalculatePressureThickness
    ? (inputs.designPressurePsi * inputs.outsideDiameterIn) / denominator
    : 0;
  const requiredThicknessWithAllowancesIn = pressureDesignThicknessIn + inputs.mechanicalAllowanceIn + inputs.corrosionAllowanceIn;
  const requiredNominalThicknessIn = inputs.millToleranceFraction < 1
    ? requiredThicknessWithAllowancesIn / (1 - inputs.millToleranceFraction)
    : null;

  const availableThicknessForPressureIn = inputs.providedThicknessIn - inputs.mechanicalAllowanceIn - inputs.corrosionAllowanceIn;
  const thicknessMarginIn = availableThicknessForPressureIn - pressureDesignThicknessIn;
  const isAcceptable = thicknessMarginIn >= 0 && warnings.every((w) => w.severity !== 'error');

  warnings.push({
    code: 'USER_INPUT_VALIDATION_REQUIRED',
    message: 'Reviewer validation required for allowable stress, weld factors, Y coefficient, and other user-entered code values.',
    severity: 'warning'
  });

  return {
    id: `b31-3-piping-${calculatedAt}`,
    calculationType: 'b31-3-piping',
    displayName: 'ASME B31.3 Piping Wall Thickness Foundation Helper',
    formulaVersion: 'asme-b31.3-foundation-v1',
    standardReferences: [
      { standard: 'ASME B31.3', edition: inputs.standardEdition, note: 'Foundation workflow summary only; owner/spec reviewer must verify governing clauses and values.' },
      { standard: 'API 570', note: 'Intended as an insertable engineering snapshot for piping inspection workflows.' }
    ],
    inputs,
    outputs: {
      pressureDesignThicknessIn,
      requiredThicknessWithAllowancesIn,
      requiredNominalThicknessIn,
      availableThicknessForPressureIn,
      thicknessMarginIn,
      isAcceptable,
    },
    warnings,
    calculatedAt,
    insertLabel: `B31.3 piping check (required nominal ${requiredNominalThicknessIn?.toFixed(4) ?? 'N/A'} in, margin ${thicknessMarginIn.toFixed(4)} in)`
  };
}
