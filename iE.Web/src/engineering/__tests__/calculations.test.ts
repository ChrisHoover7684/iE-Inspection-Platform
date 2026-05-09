import { describe, expect, it } from 'vitest';
import { calculateCorrosionRate } from '../calculations/corrosionRate';
import { calculatePipeDimensions } from '../calculations/pipeLookup';
import { calculateRequiredThicknessMargin } from '../calculations/pressureVessel';
import { calculateTankShellCorrosionMargin } from '../calculations/tankShell';

describe('engineering calculations foundation', () => {
  it('calculates corrosion rate outputs', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.9,
      exposureTimeYears: 1,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.9,
      tminInches: 0.5
    });

    expect(result.outputs.corrosionRateInchesPerYear).toBeCloseTo(0.1, 6);
    expect(result.outputs.remainingLifeYears).toBeCloseTo(4, 6);
    expect(result.outputs.nextInspectionYears).toBeCloseTo(2, 6);
  });

  it('calculates pipe dimensions', () => {
    const result = calculatePipeDimensions({ nps: '4', schedule: '40', outsideDiameter: 4.5, nominalThickness: 0.237 });
    expect(result.outputs.insideDiameter).toBeCloseTo(4.026, 6);
    expect(result.outputs.lowerLimitMinus12_5).toBeCloseTo(0.207375, 6);
    expect(result.outputs.upperLimitPlus12_5).toBeCloseTo(0.266625, 6);
  });

  it('calculates pressure vessel margin', () => {
    const result = calculateRequiredThicknessMargin({ requiredWithCorrosionAllowanceIn: 0.5, providedThicknessIn: 0.625 });
    expect(result.outputs.marginIn).toBeCloseTo(0.125, 6);
    expect(result.outputs.isAcceptable).toBe(true);
  });

  it('calculates tank shell corrosion margin', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0.4, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.outputs.requiredWithCorrosionAllowanceIn).toBeCloseTo(0.35, 6);
    expect(result.outputs.remainingMarginIn).toBeCloseTo(0.05, 6);
  });
});
