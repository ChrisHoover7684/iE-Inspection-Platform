import { describe, expect, it } from 'vitest';
import { calculateCorrosionRate } from '../calculations/corrosionRate';
import { calculatePipeDimensions } from '../calculations/pipeLookup';
import { calculateRequiredThicknessMargin } from '../calculations/pressureVessel';
import { calculateTankShellCorrosionMargin } from '../calculations/tankShell';

describe('engineering calculations foundation', () => {
  const expectFoundationFields = (result: { calculationType: string; formulaVersion: string; displayName: string; calculatedAt: string; insertLabel: string; warnings: unknown[]; standardReferences: unknown[]; }) => {
    expect(result.calculationType).toBeTruthy();
    expect(result.formulaVersion).toBeTruthy();
    expect(result.displayName).toBeTruthy();
    expect(result.calculatedAt).toBeTruthy();
    expect(result.insertLabel).toBeTruthy();
    expect(Array.isArray(result.warnings)).toBe(true);
    expect(Array.isArray(result.standardReferences)).toBe(true);
  };

  it('corrosion valid case', () => {
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
    expectFoundationFields(result);
  });

  it('corrosion exposure time <= 0', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.9,
      exposureTimeYears: 0,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.9,
      tminInches: 0.5
    });
    expect(result.outputs.corrosionRateInchesPerYear).toBe(0);
    expect(result.warnings.some((w) => w.code === 'EXPOSURE_TIME_NON_POSITIVE')).toBe(true);
    expectFoundationFields(result);
  });

  it('corrosion current thickness below Tmin gives remainingLifeYears null and warning', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.8,
      exposureTimeYears: 2,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.4,
      tminInches: 0.5
    });
    expect(result.outputs.remainingLifeYears).toBeNull();
    expect(result.warnings.some((w) => w.code === 'CURRENT_AT_OR_BELOW_TMIN')).toBe(true);
    expectFoundationFields(result);
  });

  it('corrosion initial <= final gives warning and no misleading positive corrosion rate', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 0.8,
      finalThicknessInches: 0.8,
      exposureTimeYears: 1,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.8,
      tminInches: 0.5
    });
    expect(result.outputs.corrosionRateInchesPerYear).toBe(0);
    expect(result.warnings.some((w) => w.code === 'INITIAL_NOT_GREATER_THAN_FINAL')).toBe(true);
    expectFoundationFields(result);
  });

  it('pipe lookup valid case', () => {
    const result = calculatePipeDimensions({ nps: '4', schedule: '40', outsideDiameter: 4.5, nominalThickness: 0.237 });
    expect(result.outputs.insideDiameter).toBeCloseTo(4.026, 6);
    expect(result.outputs.lowerLimitMinus12_5).toBeCloseTo(0.207375, 6);
    expect(result.outputs.upperLimitPlus12_5).toBeCloseTo(0.266625, 6);
    expectFoundationFields(result);
  });

  it('pipe lookup impossible ID warning', () => {
    const result = calculatePipeDimensions({ nps: '4', schedule: 'XXS', outsideDiameter: 1, nominalThickness: 1 });
    expect(result.outputs.insideDiameter).toBeLessThanOrEqual(0);
    expect(result.warnings.some((w) => w.code === 'INSIDE_DIAMETER_NON_POSITIVE')).toBe(true);
    expectFoundationFields(result);
  });

  it('calculates pressure vessel margin', () => {
    const result = calculateRequiredThicknessMargin({ requiredWithCorrosionAllowanceIn: 0.5, providedThicknessIn: 0.625 });
    expect(result.outputs.marginIn).toBeCloseTo(0.125, 6);
    expect(result.outputs.isAcceptable).toBe(true);
    expectFoundationFields(result);
  });

  it('pressure vessel negative margin warning', () => {
    const result = calculateRequiredThicknessMargin({ requiredWithCorrosionAllowanceIn: 0.5, providedThicknessIn: 0.4 });
    expect(result.outputs.marginIn).toBeLessThan(0);
    expect(result.warnings.some((w) => w.code === 'NEGATIVE_MARGIN')).toBe(true);
    expectFoundationFields(result);
  });

  it('calculates tank shell corrosion margin', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0.4, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.outputs.requiredWithCorrosionAllowanceIn).toBeCloseTo(0.35, 6);
    expect(result.outputs.remainingMarginIn).toBeCloseTo(0.05, 6);
    expectFoundationFields(result);
  });

  it('tank shell negative margin warning', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0.2, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.outputs.remainingMarginIn).toBeLessThan(0);
    expect(result.warnings.some((w) => w.code === 'NEGATIVE_MARGIN')).toBe(true);
    expectFoundationFields(result);
  });
});
