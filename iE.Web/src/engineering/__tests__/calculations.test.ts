import { describe, expect, it } from 'vitest';
import { calculateCorrosionRate } from '../calculations/corrosionRate';
import { calculatePipeDimensions } from '../calculations/pipeLookup';
import { calculateRequiredThicknessMargin } from '../calculations/pressureVessel';
import { calculateTankShellCorrosionMargin } from '../calculations/tankShell';
import { toB31_3EngineeringSnapshot } from '../calculations/b31_3Piping';
import { applyMaterialPreset, buildCircuitBatchSnapshot, formatCircuitBatchSummary, mapB313RowResult, resolveCircuitConditionsFromReport, toResultDisplayRows } from '../calculations/b31_3CircuitBatch';
import { assessApi570Thickness, normalizeNpsValue, toApi570FindingDraftsFromAssessment } from '../calculations/api570ThicknessAssessment';

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


  it('corrosion tmin <= 0 does not project remaining life or next inspection', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.8,
      exposureTimeYears: 2,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.7,
      tminInches: 0
    });
    expect(result.outputs.remainingLifeYears).toBeNull();
    expect(result.outputs.nextInspectionYears).toBeNull();
    expect(result.warnings.some((w) => w.code === 'TMIN_NON_POSITIVE')).toBe(true);
    expectFoundationFields(result);
  });

  it('corrosion current thickness <= 0 does not project remaining life or next inspection', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.8,
      exposureTimeYears: 2,
      inspectionFactor: 0.5,
      currentThicknessInches: 0,
      tminInches: 0.5
    });
    expect(result.outputs.remainingLifeYears).toBeNull();
    expect(result.outputs.nextInspectionYears).toBeNull();
    expect(result.warnings.some((w) => w.code === 'CURRENT_THICKNESS_NON_POSITIVE')).toBe(true);
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


  it('pressure vessel non-positive required thickness is not acceptable', () => {
    const result = calculateRequiredThicknessMargin({ requiredWithCorrosionAllowanceIn: 0, providedThicknessIn: 0.5 });
    expect(result.warnings.some((w) => w.code === 'REQUIRED_THICKNESS_NON_POSITIVE')).toBe(true);
    expect(result.outputs.isAcceptable).toBe(false);
    expectFoundationFields(result);
  });

  it('pressure vessel non-positive provided thickness is not acceptable', () => {
    const result = calculateRequiredThicknessMargin({ requiredWithCorrosionAllowanceIn: 0.4, providedThicknessIn: 0 });
    expect(result.warnings.some((w) => w.code === 'PROVIDED_THICKNESS_NON_POSITIVE')).toBe(true);
    expect(result.outputs.isAcceptable).toBe(false);
    expectFoundationFields(result);
  });

  it('calculates tank shell corrosion margin', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0.4, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.outputs.requiredWithCorrosionAllowanceIn).toBeCloseTo(0.35, 6);
    expect(result.outputs.remainingMarginIn).toBeCloseTo(0.05, 6);
    expectFoundationFields(result);
  });


  it('tank shell invalid thickness inputs insert label requires review', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.insertLabel.toLowerCase()).toContain('requires review');
    expect(result.insertLabel.toLowerCase()).toContain('invalid thickness inputs');
    expectFoundationFields(result);
  });

  it('pipe lookup invalid ID label indicates invalid dimensions', () => {
    const result = calculatePipeDimensions({ nps: '4', schedule: 'XXS', outsideDiameter: 1, nominalThickness: 1 });
    expect(result.insertLabel.toLowerCase()).toContain('invalid dimensions');
    expectFoundationFields(result);
  });

  it('tank shell negative margin warning', () => {
    const result = calculateTankShellCorrosionMargin({ currentThicknessIn: 0.2, requiredThicknessIn: 0.3, corrosionAllowanceIn: 0.05 });
    expect(result.outputs.remainingMarginIn).toBeLessThan(0);
    expect(result.warnings.some((w) => w.code === 'NEGATIVE_MARGIN')).toBe(true);
    expectFoundationFields(result);
  });

  it('maps b31.3 engine result into engineering snapshot', () => {
    const result = toB31_3EngineeringSnapshot({
      pressurePsi: 285, temperatureF: 100, outsideDiameterIn: 8.625, spec: 'A106', grade: 'B', productForm: 'Pipe', unsNo: '', classConditionTemper: '', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless', wFactor: 1, yOverride: null, eOverride: null
    }, {
      success: true, message: 'ok', allowableStressPsi: 20000, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.08
    });
    expect(result.outputs.requiredThicknessIn).toBeCloseTo(0.08, 6);
    expect(result.warnings.some((w) => w.code === 'USER_INPUT_VALIDATION_REQUIRED')).toBe(true);
    expectFoundationFields(result);
  });

  it('b31.3 failed engine response creates error warning', () => {
    const result = toB31_3EngineeringSnapshot({
      pressurePsi: 100, temperatureF: 100, outsideDiameterIn: 6, spec: 'A999', grade: 'X', productForm: 'Pipe', unsNo: '', classConditionTemper: '', materialCategory: 'Unknown', jointType: 'Seamless', jointQualityKey: 'Seamless'
    }, {
      success: false, message: 'Allowable stress not found', allowableStressPsi: null, eFactor: null, yCoefficient: null, wFactor: null, requiredThicknessIn: null
    });
    expect(result.warnings.some((w) => w.code === 'B313_CALCULATION_FAILED')).toBe(true);
    expectFoundationFields(result);
  });



  it('pipe lookup result can be saved as report snapshot metadata', () => {
    const result = calculatePipeDimensions({ nps: '6', schedule: '40', outsideDiameter: 6.625, nominalThickness: 0.28 });
    const snapshot = { ...result, linkedSectionId: 'sec-pipe', linkedFieldId: 'field-pipe' };
    expect(snapshot.calculationType).toBe('pipe-lookup');
    expect(snapshot.formulaVersion).toBe('asme-b36-foundation-v1');
    expect(snapshot.standardReferences.length).toBeGreaterThan(0);
    expect(snapshot.insertLabel).toContain('Sch 40');
    expect(snapshot.inputs).toBeTypeOf('object');
    expect(snapshot.outputs).toBeTypeOf('object');
    expect(snapshot.calculatedAt).toBeTruthy();
    expect(snapshot.linkedSectionId).toBe('sec-pipe');
  });

  it('corrosion result can be saved as report snapshot metadata', () => {
    const result = calculateCorrosionRate({
      initialThicknessInches: 1,
      finalThicknessInches: 0.9,
      exposureTimeYears: 2,
      inspectionFactor: 0.5,
      currentThicknessInches: 0.9,
      tminInches: 0.5
    });
    const snapshot = { ...result, linkedSectionId: 'sec-1', linkedFieldId: 'field-1', linkedFindingId: 'finding-1' };
    expect(snapshot.linkedSectionId).toBe('sec-1');
    expect(snapshot.inputs).toBeTypeOf('object');
  });

  it('b31 circuit batch uses shared pressure/temperature for all rows and one snapshot', () => {
    const shared = {
      pressurePsi: 285,
      temperatureF: 100,
      sourceMetadata: {
        pressure: { value: 285, source: 'Inspection Context', isManual: false },
        temperature: { value: 100, source: 'Inspection Context', isManual: false }
      },
      defaultJointType: 'Seamless',
      defaultJointQualityKey: 'Seamless',
      wFactor: 1,
      yOverride: null,
      eOverride: null
    };
    const rows = [
      mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, { pressurePsi: 285, temperatureF: 100, outsideDiameterIn: 2.375, spec: 'A106', grade: 'B', productForm: 'Pipe', unsNo: '', classConditionTemper: '', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' }, { success: true, message: 'ok', allowableStressPsi: 20000, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.09 }, []),
      mapB313RowResult('2', { id: '2', nps: '4', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, { pressurePsi: 285, temperatureF: 100, outsideDiameterIn: 4.5, spec: 'A106', grade: 'B', productForm: 'Pipe', unsNo: '', classConditionTemper: '', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless' }, { success: false, message: 'Allowable stress missing', allowableStressPsi: null, eFactor: null, yCoefficient: null, wFactor: 1, requiredThicknessIn: null }, ['fallback OD'])
    ];
    expect(rows.every((r) => r.input?.pressurePsi === 285 && r.input?.temperatureF === 100)).toBe(true);
    expect(rows.every((r) => !('rowPressurePsi' in (r.input ?? {})) && !('rowTemperatureF' in (r.input ?? {})))).toBe(true);
    const snapshot = buildCircuitBatchSnapshot(shared, rows);
    expect(snapshot.calculationType).toBe('b31-3-piping-circuit-batch');
    expect(snapshot.outputs.failedRows).toBe(1);
    expect(snapshot.warnings.some((w) => w.code === 'B313_ROW_FAILED')).toBe(true);
    expect(snapshot.inputs.shared.sourceMetadata.pressure.source).toBe('Inspection Context');
  });

  it('b31 circuit report source metadata supports manual override warning', () => {
    const report = {
      id: 'r', clientOrganizationId: '', facilityId: '', templateId: '', status: '', createdAt: '', sections: [{ sectionId: 's1', sectionTitle: 'Inspection Context', order: 1, answers: [{ fieldId: 'design-pressure', label: 'Design Pressure', dataType: 'number', value: '285 psig', values: [] }, { fieldId: 'design-temperature', label: 'Design Temperature', dataType: 'number', value: '100 F', values: [] }] }], findings: [], photos: [], calculations: []
    } as any;
    const meta = resolveCircuitConditionsFromReport(report);
    expect(meta.pressure.value).toBe(285);
    expect(meta.temperature.value).toBe(100);
    const snapshot = buildCircuitBatchSnapshot({ pressurePsi: 300, temperatureF: 125, sourceMetadata: { pressure: { value: 300, source: 'Manual', isManual: true }, temperature: { value: 125, source: 'Manual', isManual: true } }, defaultJointType: 'Seamless', defaultJointQualityKey: 'Seamless' }, []);
    expect(snapshot.warnings.some((w) => w.code === 'CIRCUIT_PRESSURE_OR_TEMPERATURE_MANUAL_OVERRIDE')).toBe(true);
  });

  it('material presets populate expected row fields', () => {
    const base = { id: '1', nps: '2', spec: '', grade: '', productForm: '', materialCategory: '' };
    const a106 = applyMaterialPreset(base, 'A106_GR_B_SEAMLESS');
    expect(a106.spec).toBe('A106');
    expect(a106.jointType).toBe('Seamless');
    const a53 = applyMaterialPreset(base, 'A53_GR_B_ERW_EB');
    expect(a53.spec).toBe('A53');
    expect(a53.jointQualityKey).toBe('E/B');
  });

  it('snapshot inputs preserve row material fields', () => {
    const shared = { pressurePsi: 285, temperatureF: 100, sourceMetadata: { pressure: { value: 285, source: 'Inspection Context', isManual: false }, temperature: { value: 100, source: 'Inspection Context', isManual: false } }, defaultJointType: 'Seamless', defaultJointQualityKey: 'Seamless' };
    const rows = [mapB313RowResult('1', { id: '1', nps: '2', schedule: '40', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel', jointType: 'Seamless', jointQualityKey: 'Seamless', note: 'rack A' }, null, null, [])];
    const snapshot = buildCircuitBatchSnapshot(shared, rows);
    expect(snapshot.inputs.rows[0].spec).toBe('A106');
    expect(snapshot.inputs.rows[0].jointQualityKey).toBe('Seamless');
    expect(snapshot.inputs.rows[0].note).toBe('rack A');
  });

  it('insert summary formatter includes each pipe size material and Tmin', () => {
    const shared = { pressurePsi: 285, temperatureF: 100, sourceMetadata: { pressure: { value: 285, source: 'Inspection Context', isManual: false }, temperature: { value: 100, source: 'Inspection Context', isManual: false } }, defaultJointType: 'Seamless', defaultJointQualityKey: 'Seamless' };
    const rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.1234 }, [])];
    const summary = formatCircuitBatchSummary(buildCircuitBatchSnapshot(shared, rows));
    expect(summary).toContain('285 psig / 100°F');
    expect(summary).toContain('2 in A106 B');
    expect(summary).toContain('Tmin 0.1234 in');
  });

  it('results table data model includes expected columns', () => {
    const rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: false, message: 'failed', allowableStressPsi: null, eFactor: null, yCoefficient: null, wFactor: null, requiredThicknessIn: null }, [])];
    const display = toResultDisplayRows(rows)[0];
    expect(display).toHaveProperty('nps');
    expect(display).toHaveProperty('material');
    expect(display).toHaveProperty('outsideDiameterIn');
    expect(display).toHaveProperty('allowableStressPsi');
    expect(display).toHaveProperty('eFactor');
    expect(display).toHaveProperty('yCoefficient');
    expect(display).toHaveProperty('wFactor');
    expect(display).toHaveProperty('requiredThicknessIn');
    expect(display).toHaveProperty('statusMessage');
  });



  it('below Tmin row creates finding draft with recommendation details', () => {
    const b31Rows = [mapB313RowResult('b31-1', { id: 'b31-1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [
      { id: 'c1', cmlId: 'CML-LOW', location: 'elbow', nps: '2', currentThicknessIn: 0.19, currentInspectionDate: '2025-01-01' }
    ] }, b31Rows);
    const drafts = toApi570FindingDraftsFromAssessment(snapshot);
    expect(drafts).toHaveLength(1);
    expect(drafts[0].cmlId).toBe('CML-LOW');
    expect(drafts[0].recommendation).toContain('Below Tmin');
    expect(drafts[0].tminIn).toBeCloseTo(0.25, 6);
    expect(drafts[0].currentThicknessIn).toBeCloseTo(0.19, 6);
    expect(drafts[0].marginToTminIn).toBeCloseTo(-0.06, 6);
    expect(drafts[0].severity).toBe('Critical');
  });

  it('monitor row is not forced into finding draft by default', () => {
    const b31Rows = [mapB313RowResult('b31-1', { id: 'b31-1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [
      { id: 'c1', cmlId: 'CML-MON', location: 'run', nps: '2', currentThicknessIn: 0.26, currentInspectionDate: '2025-01-01' }
    ] }, b31Rows);
    expect(snapshot.outputs.rows[0].status).toBe('Monitor');
    expect(toApi570FindingDraftsFromAssessment(snapshot)).toHaveLength(0);
    expect(toApi570FindingDraftsFromAssessment(snapshot, { includeMonitorRows: true })).toHaveLength(1);
  });

  it('duplicate CML finding key can be prevented using snapshot id + CML id', () => {
    const b31Rows = [mapB313RowResult('b31-1', { id: 'b31-1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [
      { id: 'c1', cmlId: 'CML-DUP', location: 'run', nps: '2', currentThicknessIn: 0.2, currentInspectionDate: '2025-01-01' }
    ] }, b31Rows);
    const drafts = toApi570FindingDraftsFromAssessment(snapshot);
    const keys = drafts.map((d) => `api570-thickness:${d.assessmentSnapshotId}:${d.cmlId}`);
    const deduped = new Set(keys);
    expect(keys).toHaveLength(1);
    expect(deduped.size).toBe(1);
  });

  it('api570 assessment matches by NPS and handles status/margin/corrosion/missing Tmin', () => {
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [
      { id: 'c1', cmlId: 'CML-1', location: 'elbow', nps: '2', currentThicknessIn: 0.23, priorThicknessIn: 0.28, priorInspectionDate: '2024-01-01', currentInspectionDate: '2025-01-01' },
      { id: 'c2', cmlId: 'CML-2', location: 'run', nps: '2', currentThicknessIn: 0.3, priorThicknessIn: 0.32, priorInspectionDate: '2024-01-01', currentInspectionDate: '2025-01-01' },
      { id: 'c3', cmlId: 'CML-3', location: 'tee', nps: '8', currentThicknessIn: 0.4, currentInspectionDate: '2025-01-01' }
    ] }, b31Rows);
    expect(snapshot.calculationType).toBe('api-570-thickness-assessment');
    expect(snapshot.outputs.rows[0].status).toBe('Below Tmin');
    expect(snapshot.outputs.rows[0].marginToTminIn).toBeCloseTo(-0.02, 6);
    expect(snapshot.outputs.rows[0].corrosionRateInPerYear).toBeGreaterThan(0);
    expect(snapshot.outputs.rows[1].remainingLifeYears).not.toBeNull();
    expect(snapshot.outputs.rows[2].status).toBe('Missing Tmin');
    expect(snapshot.warnings.some((w) => w.code === 'MISSING_TMIN')).toBe(true);
    expect(snapshot.inputs.cmlReadings.length).toBe(3);
    expect(snapshot.inputs.monitorMarginThresholdIn).toBe(0.02);
  });

  it('api570 assessment normalizes common NPS formats', () => {
    expect(normalizeNpsValue('2')).toBe('2');
    expect(normalizeNpsValue('2.0')).toBe('2');
    expect(normalizeNpsValue('2 in')).toBe('2');
    expect(normalizeNpsValue('2"')).toBe('2');
    expect(normalizeNpsValue('NPS 2')).toBe('2');
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [{ id: 'c1', cmlId: 'CML-1', location: '', nps: 'NPS 2.0 in', currentThicknessIn: 0.26, currentInspectionDate: '2025-01-01' }] }, b31Rows);
    expect(snapshot.outputs.rows[0].status).toBe('Monitor');
  });

  it('api570 assessment prior date after current sets warning and null corrosion outputs', () => {
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.05, cmlReadings: [{ id: 'c1', cmlId: 'CML-1', location: '', nps: '2', currentThicknessIn: 0.3, priorThicknessIn: 0.31, priorInspectionDate: '2025-01-01', currentInspectionDate: '2024-01-01' }] }, b31Rows);
    expect(snapshot.warnings.some((w) => w.code === 'PRIOR_DATE_AFTER_CURRENT')).toBe(true);
    expect(snapshot.warnings.some((w) => w.code === 'INVALID_DATE_INTERVAL')).toBe(true);
    expect(snapshot.outputs.rows[0].corrosionRateInPerYear).toBeNull();
    expect(snapshot.outputs.rows[0].remainingLifeYears).toBeNull();
  });

  it('api570 assessment same prior/current date sets invalid interval warning and null corrosion outputs', () => {
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.05, cmlReadings: [{ id: 'c1', cmlId: 'CML-1', location: '', nps: '2', currentThicknessIn: 0.3, priorThicknessIn: 0.31, priorInspectionDate: '2025-01-01', currentInspectionDate: '2025-01-01' }] }, b31Rows);
    expect(snapshot.warnings.some((w) => w.code === 'INVALID_DATE_INTERVAL')).toBe(true);
    expect(snapshot.outputs.rows[0].corrosionRateInPerYear).toBeNull();
    expect(snapshot.outputs.rows[0].remainingLifeYears).toBeNull();
  });

  it('api570 assessment invalid date string sets invalid interval warning and null corrosion outputs', () => {
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.05, cmlReadings: [{ id: 'c1', cmlId: 'CML-1', location: '', nps: '2', currentThicknessIn: 0.3, priorThicknessIn: 0.31, priorInspectionDate: 'not-a-date', currentInspectionDate: '2025-01-01' }] }, b31Rows);
    expect(snapshot.warnings.some((w) => w.code === 'INVALID_DATE_INTERVAL')).toBe(true);
    expect(snapshot.outputs.rows[0].corrosionRateInPerYear).toBeNull();
    expect(snapshot.outputs.rows[0].remainingLifeYears).toBeNull();
  });

  it('api570 assessment valid date interval still calculates corrosion outputs', () => {
    const b31Rows = [mapB313RowResult('1', { id: '1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.05, cmlReadings: [{ id: 'c1', cmlId: 'CML-1', location: '', nps: '2', currentThicknessIn: 0.3, priorThicknessIn: 0.31, priorInspectionDate: '2024-01-01', currentInspectionDate: '2025-01-01' }] }, b31Rows);
    expect(snapshot.warnings.some((w) => w.code === 'INVALID_DATE_INTERVAL')).toBe(false);
    expect(snapshot.outputs.rows[0].corrosionRateInPerYear).toBeGreaterThan(0);
    expect(snapshot.outputs.rows[0].remainingLifeYears).not.toBeNull();
  });

});
