import { describe, expect, it, vi } from 'vitest';
import { calculateCorrosionRate } from '../calculations/corrosionRate';
import { calculatePipeDimensions } from '../calculations/pipeLookup';
import { calculateRequiredThicknessMargin } from '../calculations/pressureVessel';
import { calculateTankShellCorrosionMargin } from '../calculations/tankShell';
import { toB31_3EngineeringSnapshot } from '../calculations/b31_3Piping';
import { applyMaterialPreset, buildCircuitBatchSnapshot, formatCircuitBatchSummary, mapB313RowResult, resolveCircuitConditionsFromReport, toResultDisplayRows } from '../calculations/b31_3CircuitBatch';
import { assessApi570Thickness, buildApi570FindingDedupeKey, normalizeNpsValue, toApi570FindingDraftsFromAssessment } from '../calculations/api570ThicknessAssessment';
import { API510_EXTERNAL_COMPONENT_PRESETS, API510_INTERNAL_COMPONENT_PRESETS, API570_COMPONENT_PRESETS, createAndAddComponentSection, createComponentSection, upsertFindingFromComponentSection } from '../../reporting/componentSections';
import { FIELD_CATALOG_WORD_INTRO, FIELD_CATALOG_WORD_TITLE, buildFieldCatalogWordReviewDocument, exportFieldCatalogWordReview } from '../../reporting/fieldCatalogWordExport';
import { API510_EXTERNAL_DRUM_VESSEL_COMPONENT_PRESETS, API510_EXTERNAL_EXCHANGER_COMPONENT_PRESETS, API510_EXTERNAL_TOWER_COLUMN_COMPONENT_PRESETS, API570_EXTERNAL_COMPONENT_PRESETS, externalInspectionFieldSets, futureOnlyApi510InternalFields, getMvpExternalInspectionFields } from '../../reporting/inspectionFieldCatalog';
import { API510_EXTERNAL_COMPONENT_DEFINITIONS } from '../../reporting/inspectionComponentCatalog';
import { resolveComponentCalculationContext } from '../../reporting/componentCalculationContext';
import { buildComponentCalculationPrefill } from '../../reporting/componentCalculationPrefill';

import { executeApi510ComponentCalculation } from '../../reporting/componentCalculationExecution';
import { pressureVesselApi } from '../../api';

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
    expect(buildApi570FindingDedupeKey(drafts[0].assessmentSnapshotId, drafts[0].cmlId)).toBe(`api570-thickness:${snapshot.id}:CML-LOW`);
  });

  it('finding dedupe key uses saved snapshot id override', () => {
    const b31Rows = [mapB313RowResult('b31-1', { id: 'b31-1', nps: '2', spec: 'A106', grade: 'B', productForm: 'Pipe', materialCategory: 'Carbon Steel' }, null, { success: true, message: 'ok', allowableStressPsi: 1, eFactor: 1, yCoefficient: 0.4, wFactor: 1, requiredThicknessIn: 0.25 }, [])];
    const snapshot = assessApi570Thickness({ b31SnapshotId: 'snap-1', monitorMarginThresholdIn: 0.02, cmlReadings: [
      { id: 'c1', cmlId: 'CML-SAVED', location: 'elbow', nps: '2', currentThicknessIn: 0.19, currentInspectionDate: '2025-01-01' }
    ] }, b31Rows);
    const drafts = toApi570FindingDraftsFromAssessment(snapshot, { assessmentSnapshotIdOverride: 'saved-assessment-1' });
    expect(buildApi570FindingDedupeKey(drafts[0].assessmentSnapshotId, drafts[0].cmlId)).toBe('api570-thickness:saved-assessment-1:CML-SAVED');
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
    const keys = drafts.map((d) => buildApi570FindingDedupeKey(d.assessmentSnapshotId, d.cmlId));
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


describe('component section builder', () => {
  it('API 570 component presets exist', () => {
    expect(API570_COMPONENT_PRESETS).toContain('Valve');
    expect(API570_COMPONENT_PRESETS).toContain('CUI Area');
  });

  it('API 510 presets are split external/internal and internal is not in external list', () => {
    expect(API510_EXTERNAL_COMPONENT_PRESETS).toContain('Shell');
    expect(API510_INTERNAL_COMPONENT_PRESETS).toContain('Demister / Internals');
    expect(API510_EXTERNAL_COMPONENT_PRESETS).not.toContain('Demister / Internals');
  });

  it('creating component section adds repeatable section data', () => {
    const report: any = { sections: [], findings: [] };
    const next = createAndAddComponentSection(report, 'Valve');
    expect(next.sections).toHaveLength(1);
    expect(next.sections[0].isRepeatable).toBe(true);
    expect(next.sections[0].instanceNumber).toBe(1);
  });

  it('Create Finding checkbox creates a finding payload and preserves photo/summary tags', () => {
    const section = createComponentSection(1, 'Valve');
    section.answers.find((a) => a.fieldId === 'create-finding')!.value = 'true';
    section.answers.find((a) => a.fieldId === 'component-tag')!.value = 'XV-101';
    section.answers.find((a) => a.fieldId === 'component-location')!.value = 'North rack';
    section.answers.find((a) => a.fieldId === 'finding-notes')!.value = 'Minor seepage';
    section.answers.find((a) => a.fieldId === 'photo-tag')!.value = 'IMG-570-01';
    section.answers.find((a) => a.fieldId === 'add-to-summary')!.value = 'true';
    const updated = upsertFindingFromComponentSection({ id: 'r', clientOrganizationId: '', facilityId: '', templateId: '', status: '', createdAt: '', sections: [], findings: [], photos: [], calculations: [] } as any, section);
    expect(updated.findings).toHaveLength(1);
    expect(updated.findings[0].photoIds?.[0]).toBe('IMG-570-01');
    expect(updated.findings[0].addToSummary).toBe(true);
    expect(updated.findings[0].summaryText).toBe('Minor seepage');
  });



  it('component section field data types map to select/textarea/boolean controls', () => {
    const section = createComponentSection(1, 'Valve');
    expect(section.answers.find((a) => a.fieldId === 'component-type')?.dataType).toBe('select');
    expect(section.answers.find((a) => a.fieldId === 'component-condition')?.dataType).toBe('select');
    expect(section.answers.find((a) => a.fieldId === 'finding-notes')?.dataType).toBe('textarea');
    expect(section.answers.find((a) => a.fieldId === 'recommendation-text')?.dataType).toBe('textarea');
    expect(section.answers.find((a) => a.fieldId === 'create-finding')?.dataType).toBe('boolean');
  });

  it('create finding unchecked is a no-op', () => {
    const section = createComponentSection(1, 'Valve');
    section.answers.find((a) => a.fieldId === 'component-tag')!.value = 'XV-101';
    const report: any = { id: 'r', clientOrganizationId: '', facilityId: '', templateId: '', status: '', createdAt: '', sections: [], findings: [], photos: [], calculations: [] };
    const updated = upsertFindingFromComponentSection(report, section);
    expect(updated.findings).toHaveLength(0);
  });

  it('duplicate component findings are prevented by upsert', () => {
    const section = createComponentSection(1, 'Valve');
    section.answers.find((a) => a.fieldId === 'create-finding')!.value = 'true';
    section.answers.find((a) => a.fieldId === 'component-tag')!.value = 'XV-101';
    const base: any = { id: 'r', clientOrganizationId: '', facilityId: '', templateId: '', status: '', createdAt: '', sections: [], findings: [], photos: [], calculations: [] };
    const first = upsertFindingFromComponentSection(base, section);
    const second = upsertFindingFromComponentSection(first, section);
    expect(second.findings).toHaveLength(1);
  });
});


describe('inspection field catalog', () => {
  it('exposes API 570 and API 510 external MVP field sets with unique tags', () => {
    expect(externalInspectionFieldSets.map((s) => s.id)).toEqual(expect.arrayContaining(['api-570-external-piping', 'api-510-external-exchanger', 'api-510-external-drum-vessel', 'api-510-external-tower-column']));
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(new Set(tags).size).toBe(tags.length);
    expect(tags.every((tag) => !tag.startsWith('api510.internal'))).toBe(true);
  });

  it('api 570 component field catalog contains all MVP component action fields', () => {
    const fields = externalInspectionFieldSets.find((s) => s.id === 'api-570-external-piping')?.fields ?? [];
    const tags = fields.map((f) => f.fieldTag);
    expect(tags).toEqual(expect.arrayContaining([
      'api570.external.piping.component.type',
      'api570.external.piping.component.tag-name',
      'api570.external.piping.component.location',
      'api570.external.piping.component.condition',
      'api570.external.piping.component.finding-notes',
      'api570.external.piping.component.recommendation-text',
      'api570.external.piping.component.photo-tag',
      'api570.external.piping.component.create-finding',
      'api570.external.piping.component.recommendation-required',
      'api570.external.piping.component.repair-required',
      'api570.external.piping.component.photo-required',
      'api570.external.piping.component.nde-required',
      'api570.external.piping.component.add-to-summary'
    ]));

    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.create-finding' && f.supportsFinding)).toBe(true);
    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.recommendation-required' && f.supportsRecommendation)).toBe(true);
    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.repair-required' && f.supportsRepairRequired)).toBe(true);
    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.photo-required' && f.supportsPhotoTag)).toBe(true);
    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.nde-required' && f.supportsNdeRequest)).toBe(true);
    expect(fields.some((f) => f.fieldTag === 'api570.external.piping.component.add-to-summary' && f.supportsSummary)).toBe(true);
  });

  it('api 570 component presets are included in catalog metadata', () => {
    const pipingSet = externalInspectionFieldSets.find((s) => s.id === 'api-570-external-piping');
    expect(pipingSet?.componentPresets).toEqual(API570_EXTERNAL_COMPONENT_PRESETS);
  });


  it('api 510 external component presets and durable tags are present', () => {
    const exchanger = externalInspectionFieldSets.find((s) => s.id === 'api-510-external-exchanger');
    const vessel = externalInspectionFieldSets.find((s) => s.id === 'api-510-external-drum-vessel');
    const tower = externalInspectionFieldSets.find((s) => s.id === 'api-510-external-tower-column');
    expect(exchanger?.componentPresets).toEqual(API510_EXTERNAL_EXCHANGER_COMPONENT_PRESETS);
    expect(vessel?.componentPresets).toEqual(API510_EXTERNAL_DRUM_VESSEL_COMPONENT_PRESETS);
    expect(tower?.componentPresets).toEqual(API510_EXTERNAL_TOWER_COLUMN_COMPONENT_PRESETS);

    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(tags).toEqual(expect.arrayContaining([
      'api510.external.exchanger.shell-tube.shell.condition',
      'api510.external.exchanger.shell-tube.channel-channel-head.condition',
      'api510.external.exchanger.plate-frame.plate-pack-external.condition',
      'api510.external.exchanger.double-pipe.outer-pipe.condition',
      'api510.external.exchanger.air-cooler.header-box.condition',
      'api510.external.exchanger.air-cooler.tube-bundle-external.condition',
      'api510.external.exchanger.air-cooler.nozzles.condition',
      'api510.external.exchanger.air-cooler.frame-supports.condition',
      'api510.external.drum-vessel.horizontal-drum.shell.condition',
      'api510.external.drum-vessel.vertical-drum.shell.condition',
      'api510.external.drum-vessel.vertical-drum.nozzles.condition',
      'api510.external.drum-vessel.horizontal-drum.nozzles.condition',
      'api510.external.tower-column.distillation.shell-courses.condition',
      'api510.external.tower-column.distillation.platforms-ladders-handrails.condition'
    ]));
  });


  it('includes accumulator/receiver and absorber/stripper subtype condition tags', () => {
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(tags).toEqual(expect.arrayContaining([
      'api510.external.drum-vessel.accumulator-receiver.shell.condition',
      'api510.external.drum-vessel.accumulator-receiver.heads.condition',
      'api510.external.drum-vessel.accumulator-receiver.nozzles.condition',
      'api510.external.drum-vessel.accumulator-receiver.supports.condition',
      'api510.external.tower-column.absorber-stripper.shell-courses.condition',
      'api510.external.tower-column.absorber-stripper.nozzles.condition',
      'api510.external.tower-column.absorber-stripper.manways.condition',
      'api510.external.tower-column.absorber-stripper.platforms-ladders-handrails.condition',
      'api510.external.tower-column.absorber-stripper.distributor-nozzles.condition',
      'api510.external.tower-column.absorber-stripper.reboiler-overhead-connections.condition'
    ]));
  });

  it('future API 510 internal fields are excluded from MVP export', () => {
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(futureOnlyApi510InternalFields.every((f) => !tags.includes(f.fieldTag))).toBe(true);
  });

  it('word export creates a real docx blob output', async () => {
    const blob = await exportFieldCatalogWordReview();
    expect(blob.type).toBe('application/vnd.openxmlformats-officedocument.wordprocessingml.document');
    expect(blob.size).toBeGreaterThan(0);
  });

  it('word export includes title, intro, presets metadata and MVP fields', async () => {
    const doc = buildFieldCatalogWordReviewDocument();
    expect(doc).toContain(FIELD_CATALOG_WORD_TITLE);
    expect(doc).toContain(FIELD_CATALOG_WORD_INTRO);
    expect(doc).toContain('Component Presets:');
    expect(doc).toContain('Control Valve / Control Loop');
    expect(doc).toContain('API 510 External Exchanger: Shell and Tube Exchanger, Plate and Frame Exchanger, Double Pipe Exchanger, Air Cooler / Fin Fan');
    expect(doc).toContain('api510.external.exchanger.shell-tube.shell.condition');
    expect(doc).toContain('api510.external.drum-vessel.horizontal-drum.nozzles.condition');
    expect(doc).toContain('api510.external.tower-column.distillation.platforms-ladders-handrails.condition');
    expect(doc).toContain('api570.external.piping.component.create-finding');
    expect(doc).toContain('api570.external.piping.component.repair-required');
    expect(doc).toContain('Review Notes');
  });


  it('api510 component availability includes exchanger minimum/optional sets and excludes internal', () => {
    const fields = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(fields).toEqual(expect.arrayContaining([
      'api510.external.exchanger.shell-tube.shell.condition',
      'api510.external.exchanger.shell-tube.channel-channel-head.condition',
      'api510.external.exchanger.shell-tube.nozzles.condition',
      'api510.external.exchanger.shell-tube.shell-cover.condition',
      'api510.external.exchanger.shell-tube.bonnet-head.condition',
      'api510.external.exchanger.shell-tube.tubesheet-area.condition',
      'api510.external.exchanger.plate-frame.frame-head.condition',
      'api510.external.exchanger.double-pipe.inner-pipe-external.condition',
      'api510.external.exchanger.air-cooler.header-box.condition',
      'api510.external.exchanger.air-cooler.tube-bundle-external.condition',
      'api510.external.exchanger.air-cooler.nozzles.condition',
      'api510.external.exchanger.air-cooler.frame-supports.condition',
      'api510.external.drum-vessel.horizontal-drum.shell.condition',
      'api510.external.drum-vessel.vertical-drum.shell.condition',
      'api510.external.drum-vessel.vertical-drum.nozzles.condition',
      'api510.external.drum-vessel.vertical-drum.shell.condition',
      'api510.external.tower-column.distillation.shell-courses.condition',
      'api510.external.tower-column.distillation.tray-manways.condition'
    ]));
    expect(fields.some((t) => t.includes('api510.internal'))).toBe(false);
  });

  it('word export includes component availability metadata columns', () => {
    const doc = buildFieldCatalogWordReviewDocument();
    expect(doc).toContain('Component Availability Metadata:');
    expect(doc).toContain('Equipment Family|Equipment Subtype|Component|Requirement Level|Default Selected|Parent Component Required|Allowed Parent Components|Pressure Boundary Side|Design Pressure Field Tag|Design Temperature Field Tag|Design Pressure Tags By Side|Design Temperature Tags By Side|Design Condition Source Options|Parent Thickness Source Options|Nozzle Location Required|Nozzle Location Options|Supports Tmin Calculation|Tmin Calculation Method|Supports Nozzle UG-45|Supports Finding|Supports Recommendation|Supports Photo Tag|Supports NDE Request|Calculator Attachment Notes');
    expect(doc).toContain('Pressure Equipment|Shell and Tube Exchanger|Shell|minimum|true');
    expect(doc).toContain('Pressure Equipment|Shell and Tube Exchanger|Shell Cover|optional|false');
    expect(doc).toContain('Pressure Equipment|Accumulator / Receiver|Shell|minimum|true');
    expect(doc).toContain('Pressure Equipment|Absorber / Stripper|Shell Courses|minimum|true');
    expect(doc).toContain('shell-side:api510.external.exchanger.shell-tube.shell-side.design-pressure');
    expect(doc).toContain('Nozzles|minimum|true|true|shell,channel-channel-head');
    expect(doc).not.toContain('channel-side:api510.external.exchanger.shell-tube.channel-side.design-pressure');
  });


  it('component definition model includes shell and tube minimum + optional + nozzle parent/ug45 metadata', () => {
    const sAndT = API510_EXTERNAL_COMPONENT_DEFINITIONS.filter((d) => d.equipmentSubtype === 'Shell and Tube Exchanger');
    const min = sAndT.filter((d) => d.requirementLevel === 'minimum').map((d) => d.label);
    expect(min).toEqual(expect.arrayContaining(['Shell', 'Channel / Channel Head', 'Nozzles']));
    const optional = sAndT.filter((d) => d.requirementLevel === 'optional').map((d) => d.label);
    expect(optional).toEqual(expect.arrayContaining(['Shell Cover', 'Bonnet Head', 'Tubesheet Area']));
    const nozzle = sAndT.find((d) => d.componentKey === 'nozzles');
    expect(sAndT.find((d) => d.componentKey === 'channel-channel-head')?.pressureBoundarySide).toBe('tube-side');
    expect(sAndT.find((d) => d.componentKey === 'channel-cover')?.pressureBoundarySide).toBe('tube-side');
    expect(sAndT.find((d) => d.componentKey === 'channel-head-dollar-plate')?.pressureBoundarySide).toBe('tube-side');
    expect(sAndT.find((d) => d.componentKey === 'bonnet-head')?.pressureBoundarySide).toBe('tube-side');
    expect(nozzle?.parentComponentRequired).toBe(true);
    expect(nozzle?.allowedParentComponentKeys).toEqual(expect.arrayContaining(['shell', 'channel-channel-head']));
    expect(nozzle?.designConditionSourceOptions).toEqual(['shell-side', 'tube-side']);
    expect(nozzle?.supportsNozzleUg45).toBe(true);
    expect(nozzle?.designPressureFieldTagsByPressureBoundarySide?.['tube-side']).toBe('api510.external.exchanger.shell-tube.tube-side.design-pressure');
    expect(nozzle?.designTemperatureFieldTagsByPressureBoundarySide?.['tube-side']).toBe('api510.external.exchanger.shell-tube.tube-side.design-temperature');
  });


  it('every API 510 external component preset has at least one generated field tag', () => {
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    [...API510_EXTERNAL_EXCHANGER_COMPONENT_PRESETS, ...API510_EXTERNAL_DRUM_VESSEL_COMPONENT_PRESETS, ...API510_EXTERNAL_TOWER_COLUMN_COMPONENT_PRESETS]
      .forEach((preset) => {
        const defs = API510_EXTERNAL_COMPONENT_DEFINITIONS.filter((d) => d.equipmentSubtype === preset);
        expect(defs.length).toBeGreaterThan(0);
        expect(defs.some((d) => tags.includes(`${d.fieldTagPrefix}.condition`))).toBe(true);
      });
  });

  it('shell and tube side-specific design pressure and temperature fields exist', () => {
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(tags).toEqual(expect.arrayContaining([
      'api510.external.exchanger.shell-tube.shell-side.design-pressure',
      'api510.external.exchanger.shell-tube.shell-side.design-temperature',
      'api510.external.exchanger.shell-tube.tube-side.design-pressure',
      'api510.external.exchanger.shell-tube.tube-side.design-temperature',
    ]));
    expect(tags).not.toEqual(expect.arrayContaining([
      'api510.external.exchanger.shell-tube.channel-side.design-pressure',
      'api510.external.exchanger.shell-tube.channel-side.design-temperature'
    ]));
  });

  it('adds api510 vessel and tower generic design condition tags', () => {
    const tags = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(tags).toEqual(expect.arrayContaining([
      'api510.external.drum-vessel.design-pressure',
      'api510.external.drum-vessel.design-temperature',
      'api510.external.tower-column.design-pressure',
      'api510.external.tower-column.design-temperature'
    ]));
  });

  it('resolver maps shell/tube/channel sides to design fields and validates required context', () => {
    const nozzle = API510_EXTERNAL_COMPONENT_DEFINITIONS.find((d) => d.equipmentSubtype === 'Shell and Tube Exchanger' && d.componentKey === 'nozzles');
    expect(nozzle).toBeTruthy();

    const shell = resolveComponentCalculationContext(nozzle!, 'shell-side', 'shell', 'shell');
    expect(shell.designPressureFieldTag).toBe('api510.external.exchanger.shell-tube.shell-side.design-pressure');

    const tube = resolveComponentCalculationContext(nozzle!, 'tube-side', 'shell', 'shell');
    expect(tube.designTemperatureFieldTag).toBe('api510.external.exchanger.shell-tube.tube-side.design-temperature');

    const channel = resolveComponentCalculationContext(nozzle!, 'channel-side', 'channel-head', 'channel-head');
    expect(channel.selectedParentComponent).toBe('channel-channel-head');
    expect(channel.selectedNozzleLocation).toBe('channel-channel-head');
    expect(channel.pressureBoundarySide).toBe('tube-side');
    expect(channel.designPressureFieldTag).toBe('api510.external.exchanger.shell-tube.tube-side.design-pressure');
    expect(channel.validationWarnings.some((w) => w.includes('deprecated'))).toBe(true);

    const missingParent = resolveComponentCalculationContext(nozzle!, 'shell-side', undefined, 'shell');
    expect(missingParent.validationWarnings.some((w) => w.includes('Parent component is required'))).toBe(true);

    const missingLocation = resolveComponentCalculationContext(nozzle!, 'shell-side', 'shell', undefined);
    expect(missingLocation.validationWarnings.some((w) => w.includes('Nozzle location is required'))).toBe(true);
  });

  it('api510 shell-and-tube prefill resolves side tags/values and warnings without affecting api570 fields', () => {
    const values = {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 275,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 500,
      'api510.external.exchanger.shell-tube.tube-side.design-pressure': 300,
      'api510.external.exchanger.shell-tube.tube-side.design-temperature': 450,
      'api570.external.piping.component.condition': 'Acceptable'
    };

    const channelHead = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'channel-channel-head', undefined, undefined, undefined, values);
    expect(channelHead?.resolvedPressureSide).toBe('tube-side');
    expect(channelHead?.designPressureFieldTag).toBe('api510.external.exchanger.shell-tube.tube-side.design-pressure');
    expect(channelHead?.designTemperatureFieldTag).toBe('api510.external.exchanger.shell-tube.tube-side.design-temperature');
    expect(channelHead?.designPressureValue).toBe(300);
    expect(channelHead?.designTemperatureValue).toBe(450);
    expect(channelHead?.notes.some((n) => n.includes('Channel / Channel Head'))).toBe(true);

    const shell = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, values);
    expect(shell?.resolvedPressureSide).toBe('shell-side');
    expect(shell?.supportsTminCalculation).toBe(true);
    expect(shell?.calculationMethodLabel).toBe('UG-27 cylindrical shell');
    expect(shell?.designPressureValue).toBe(275);
    expect(shell?.designTemperatureValue).toBe(500);

    const shellCover = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell-cover', undefined, undefined, undefined, values);
    expect(shellCover?.resolvedPressureSide).toBe('shell-side');
    expect(shellCover?.supportsTminCalculation).toBe(true);
    expect(shellCover?.calculationMethodLabel).toBe('UG-32 formed head');

    expect(channelHead?.supportsTminCalculation).toBe(true);
    expect(channelHead?.calculationMethodLabel).toBe('UG-32 formed head');

    const channelCover = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'channel-cover', undefined, undefined, undefined, values);
    expect(channelCover?.resolvedPressureSide).toBe('tube-side');
    expect(channelCover?.supportsTminCalculation).toBe(true);

    const bonnetHead = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'bonnet-head', undefined, undefined, undefined, values);
    expect(bonnetHead?.resolvedPressureSide).toBe('tube-side');
    expect(bonnetHead?.supportsTminCalculation).toBe(true);

    const nozzleTube = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'tube-side', 'channel-channel-head', 'channel-channel-head', values);
    expect(nozzleTube?.resolvedPressureSide).toBe('tube-side');
    expect(nozzleTube?.designPressureValue).toBe(300);
    expect(nozzleTube?.designTemperatureValue).toBe(450);
    expect(nozzleTube?.supportsUg45).toBe(true);

    const nozzleShell = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', values);
    expect(nozzleShell?.resolvedPressureSide).toBe('shell-side');
    expect(nozzleShell?.designPressureValue).toBe(275);
    expect(nozzleShell?.designTemperatureValue).toBe(500);

    const nozzleChannelSide = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'channel-side', 'channel-head', 'channel-head', values);
    expect(nozzleChannelSide?.resolvedPressureSide).toBe('tube-side');
    expect(nozzleChannelSide?.missingRequiredInputWarnings.some((w) => w.includes('deprecated'))).toBe(true);
    expect(nozzleChannelSide?.notes.some((n) => n.includes('normalized to tube-side'))).toBe(true);

    const missingParent = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', undefined, 'shell', values);
    expect(missingParent?.missingRequiredInputWarnings.some((w) => w.includes('Parent component is required'))).toBe(true);

    const missingLocation = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', undefined, values);
    expect(missingLocation?.missingRequiredInputWarnings.some((w) => w.includes('Nozzle location is required'))).toBe(true);

    expect(values['api570.external.piping.component.condition']).toBe('Acceptable');
  });

  it('api510 drum/vessel shell prefill resolves shared design tags for horizontal and vertical drums', () => {
    const values = {
      'api510.external.drum-vessel.design-pressure': 175,
      'api510.external.drum-vessel.design-temperature': 325
    };
    const horizontal = buildComponentCalculationPrefill('Horizontal Drum', 'shell', undefined, undefined, undefined, values);
    const vertical = buildComponentCalculationPrefill('Vertical Drum', 'shell', undefined, undefined, undefined, values);
    expect(horizontal?.designPressureFieldTag).toBe('api510.external.drum-vessel.design-pressure');
    expect(horizontal?.designTemperatureFieldTag).toBe('api510.external.drum-vessel.design-temperature');
    expect(horizontal?.designPressureValue).toBe(175);
    expect(horizontal?.designTemperatureValue).toBe(325);
    expect(vertical?.designPressureValue).toBe(175);
    expect(vertical?.designTemperatureValue).toBe(325);
  });

  it('api510 drum/vessel heads and supports expose expected executability metadata', () => {
    const heads = buildComponentCalculationPrefill('Horizontal Drum', 'heads', undefined, undefined, undefined, {});
    const supports = buildComponentCalculationPrefill('Horizontal Drum', 'saddles-supports', undefined, undefined, undefined, {});
    expect(heads?.supportsTminCalculation).toBe(true);
    expect(heads?.calculationMethodLabel).toBe('UG-32 formed head');
    expect(supports?.supportsTminCalculation).toBe(false);
  });

  it('api510 drum/vessel nozzle requires parent and location and exposes shell/head options', () => {
    const nozzle = API510_EXTERNAL_COMPONENT_DEFINITIONS.find((d) => d.equipmentSubtype === 'Horizontal Drum' && d.componentKey === 'nozzles');
    expect(nozzle?.allowedParentComponentKeys).toEqual(['shell', 'heads']);
    expect(nozzle?.nozzleLocationOptions).toEqual(['shell', 'heads']);
    const missing = buildComponentCalculationPrefill('Horizontal Drum', 'nozzles', undefined, undefined, undefined, {});
    expect(missing?.missingRequiredInputWarnings.some((w) => w.includes('Parent component is required'))).toBe(true);
    expect(missing?.missingRequiredInputWarnings.some((w) => w.includes('Nozzle location is required'))).toBe(true);
  });


  it('shell UG-27 execution requires design pressure/temp and material inputs', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {});
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { jointEfficiency: 1 } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('Design pressure is missing'))).toBe(true);
    expect(result.warnings.some((w) => w.includes('Material allowable stress input is missing'))).toBe(true);
  });

  it('UG-27 blocks when either inside or outside diameter is missing', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0 } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('both inside diameter and outside diameter'))).toBe(true);
  });

  it('UG-27 blocks when provided/original thickness is missing', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, corrosionAllowanceIn: 0 } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('Original thickness is required'))).toBe(true);
    expect(result.warnings.some((w) => w.includes('Provided/current thickness is required'))).toBe(true);
  });

  it('UG-27 blocks when corrosion allowance is omitted but allows zero when provided', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const omitted = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5 } });
    expect(omitted.success).toBe(false);
    expect(omitted.warnings.some((w) => w.includes('Corrosion allowance is required'))).toBe(true);
    vi.spyOn(pressureVesselApi, 'calculateCylindrical').mockResolvedValueOnce({ result: { radiusIn: 24, circumferentialRequiredThicknessIn: 0.2, longitudinalRequiredThicknessIn: 0.2, governingRequiredThicknessIn: 0.2, requiredWithCorrosionAllowanceIn: 0.2, marginIn: 0.3 }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const zeroAllowed = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0, diameterBasis: 'both-known' } });
    expect(zeroAllowed.success).toBe(true);
  });

  it('UG-27 blocks execution for invalid diameterBasis and does not call cylindrical API', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const calculateCylindricalSpy = vi.spyOn(pressureVesselApi, 'calculateCylindrical');
    const result = await executeApi510ComponentCalculation({
      prefill: prefill!,
      calculationType: 'ug-27-shell-tmin',
      manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0, diameterBasis: 'invalid-basis' }
    });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('UG-27 diameter basis must be inside-diameter-basis, outside-diameter-basis, or both-known'))).toBe(true);
    expect(calculateCylindricalSpy).not.toHaveBeenCalled();
  });

  it('UG-27 defaults snapshot metadata diameterBasisUsed to both-known when diameterBasis is omitted', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateCylindrical').mockResolvedValueOnce({ result: { radiusIn: 24, circumferentialRequiredThicknessIn: 0.2, longitudinalRequiredThicknessIn: 0.2, governingRequiredThicknessIn: 0.2, requiredWithCorrosionAllowanceIn: 0.2, marginIn: 0.3 }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({
      prefill: prefill!,
      calculationType: 'ug-27-shell-tmin',
      manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0 }
    });
    expect(result.success).toBe(true);
    const metadata = (result.snapshotReadyPayload?.outputs as any)?.metadata as { diameterBasisUsed?: string };
    expect(metadata.diameterBasisUsed).toBe('both-known');
  });

  it('shell execution returns snapshot-ready payload', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateCylindrical').mockResolvedValueOnce({ result: { radiusIn: 24, circumferentialRequiredThicknessIn: 0.2, longitudinalRequiredThicknessIn: 0.2, governingRequiredThicknessIn: 0.2, requiredWithCorrosionAllowanceIn: 0.2, marginIn: 0.3 }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0, diameterBasis: 'both-known' } });
    expect(result.success).toBe(true);
    expect(result.snapshotReadyPayload).toBeTruthy();
    const metadata = (result.snapshotReadyPayload?.outputs as any)?.metadata as { diameterBasisUsed?: string; diameterRequirement?: string };
    expect(metadata.diameterBasisUsed).toBe('both-known');
    expect(metadata.diameterRequirement).toBe('both-required');
  });

  it('nozzle UG-45 execution warns when parent thickness source is missing', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500 } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('Parent thickness source is required'))).toBe(true);
  });

  it('nozzle UG-45 execution includes parent component and nozzle location in snapshot metadata', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateNozzle').mockResolvedValueOnce({ result: { isValid: true, errorMessage: '', jointEfficiencyUsed: 1, insideRadiusUsed: 1, taRequiredThicknessIn: 0.1, tb1RequiredThicknessIn: 0.1, tb2RequiredThicknessIn: 0.1, tb3TableThicknessIn: 0.1, tb3PlusCorrosionAllowanceIn: 0.1, tbRequiredThicknessIn: 0.1, pressureRequiredThicknessIn: 0.1, governingRequiredThicknessIn: 0.1, marginIn: 0.05, providedThicknessIn: 0.154, corrodedThicknessIn: 0.154, requiredThicknessPlusCorrosionAllowanceIn: 0.1, isAcceptable: true, warnings: [] }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(true);
    const meta = result.snapshotReadyPayload?.inputs as { metadata?: { parentComponent?: string; nozzleLocation?: string } };
    expect(meta.metadata?.parentComponent).toBe('shell');
    expect(meta.metadata?.nozzleLocation).toBe('shell');
    expect((meta.metadata as any)?.defaultDecisions?.externalPressure).toBe('explicit-not-applicable');
    expect((meta.metadata as any)?.defaultDecisions?.ug16MinimumThickness).toBe('explicit-not-applicable');
  });

  it('UG-45 blocks when nozzle geometry and thickness inputs are missing', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0 } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('Nozzle outside and inside diameter'))).toBe(true);
    expect(result.warnings.some((w) => w.includes('nominal/original thickness'))).toBe(true);
  });

  it('UG-45 blocks when shell/head required thickness values are not provided', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('Shell/head required thickness is required'))).toBe(true);
  });

  it('UG-45 maps shell parent to shell attachment location', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const spy = vi.spyOn(pressureVesselApi, 'calculateNozzle').mockResolvedValueOnce({ result: { isValid: true, errorMessage: '', jointEfficiencyUsed: 1, insideRadiusUsed: 1, taRequiredThicknessIn: 0.1, tb1RequiredThicknessIn: 0.1, tb2RequiredThicknessIn: 0.1, tb3TableThicknessIn: 0.1, tb3PlusCorrosionAllowanceIn: 0.1, tbRequiredThicknessIn: 0.1, pressureRequiredThicknessIn: 0.1, governingRequiredThicknessIn: 0.1, marginIn: 0.05, providedThicknessIn: 0.154, corrodedThicknessIn: 0.154, requiredThicknessPlusCorrosionAllowanceIn: 0.1, isAcceptable: true, warnings: [] }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    const callArg = spy.mock.calls[0]?.[0] as any;
    expect(callArg.input.attachmentLocation).toBe('Shell');
  });

  it('UG-45 supports drum/vessel head parent attachment mapping', async () => {
    const prefill = buildComponentCalculationPrefill('Horizontal Drum', 'nozzles', undefined, 'heads', 'heads', {
      'api510.external.drum-vessel.design-pressure': 250,
      'api510.external.drum-vessel.design-temperature': 450
    });
    const spy = vi.spyOn(pressureVesselApi, 'calculateNozzle').mockResolvedValueOnce({ result: { isValid: true, errorMessage: '', jointEfficiencyUsed: 1, insideRadiusUsed: 1, taRequiredThicknessIn: 0.1, tb1RequiredThicknessIn: 0.1, tb2RequiredThicknessIn: 0.1, tb3TableThicknessIn: 0.1, tb3PlusCorrosionAllowanceIn: 0.1, tbRequiredThicknessIn: 0.1, pressureRequiredThicknessIn: 0.1, governingRequiredThicknessIn: 0.1, marginIn: 0.05, providedThicknessIn: 0.154, corrodedThicknessIn: 0.154, requiredThicknessPlusCorrosionAllowanceIn: 0.1, isAcceptable: true, warnings: [] }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(true);
    expect((spy.mock.calls[0]?.[0] as any).input.attachmentLocation).toBe('Head');
  });

  it('UG-45 blocks unsupported parent attachment location', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'tube-side', 'channel-channel-head', 'channel-channel-head', {
      'api510.external.exchanger.shell-tube.tube-side.design-pressure': 300,
      'api510.external.exchanger.shell-tube.tube-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('not supported'))).toBe(true);
  });


  it('UG-45 blocks when external pressure is omitted and not marked not-applicable', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(false);
    expect(result.warnings.some((w) => w.includes('External pressure is required'))).toBe(true);
  });

  it('UG-45 allows external pressure zero when explicitly provided', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    const spy = vi.spyOn(pressureVesselApi, 'calculateNozzle').mockResolvedValueOnce({ result: { isValid: true, errorMessage: '', jointEfficiencyUsed: 1, insideRadiusUsed: 1, taRequiredThicknessIn: 0.1, tb1RequiredThicknessIn: 0.1, tb2RequiredThicknessIn: 0.1, tb3TableThicknessIn: 0.1, tb3PlusCorrosionAllowanceIn: 0.1, tbRequiredThicknessIn: 0.1, pressureRequiredThicknessIn: 0.1, governingRequiredThicknessIn: 0.1, marginIn: 0.05, providedThicknessIn: 0.154, corrodedThicknessIn: 0.154, requiredThicknessPlusCorrosionAllowanceIn: 0.1, isAcceptable: true, warnings: [] }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressurePsi: 0, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(true);
    const callArg = spy.mock.calls[0]?.[0] as any;
    expect(callArg.input.externalPressurePsi).toBe(0);
  });

  it('API error is caught and returned as structured failure', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateCylindrical').mockRejectedValueOnce(new Error('boom'));
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0 } });
    expect(result.success).toBe(false);
    expect(result.resultSummary).toBe('API calculation failed');
    expect(result.warnings.some((w) => w.includes('API calculation failed'))).toBe(true);
  });

  it('API invalid result returns success false and propagates warnings to snapshot', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'nozzles', 'shell-side', 'shell', 'shell', {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateNozzle').mockResolvedValueOnce({ result: { isValid: false, errorMessage: 'invalid per backend', jointEfficiencyUsed: 1, insideRadiusUsed: 1, taRequiredThicknessIn: 0.1, tb1RequiredThicknessIn: 0.1, tb2RequiredThicknessIn: 0.1, tb3TableThicknessIn: 0.1, tb3PlusCorrosionAllowanceIn: 0.1, tbRequiredThicknessIn: 0.1, pressureRequiredThicknessIn: 0.1, governingRequiredThicknessIn: 0.1, marginIn: 0.05, providedThicknessIn: 0.154, corrodedThicknessIn: 0.154, requiredThicknessPlusCorrosionAllowanceIn: 0.1, isAcceptable: false, warnings: ['backend result warn'] }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: ['envelope warn'] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-45-nozzle-neck-tmin', manualInputs: { allowableStressPsi: 17500, parentThicknessSource: 'selected-parent', jointEfficiency: 1, corrosionAllowanceIn: 0, shellOrHeadRequiredThicknessIn: 0.25, shellOrHeadExternalRequiredThicknessIn: 0.25, outsideDiameterIn: 2.375, insideDiameterIn: 2, nominalThicknessIn: 0.154, originalThicknessIn: 0.154, nominalPipeSize: 'NPS 2', externalPressureApplicable: false, ug16MinimumThicknessApplicable: false } });
    expect(result.success).toBe(false);
    expect(result.warnings).toEqual(expect.arrayContaining(['envelope warn', 'backend result warn']));
    expect(result.snapshotReadyPayload?.warnings.map((w: any) => w.message)).toEqual(expect.arrayContaining(['envelope warn', 'backend result warn']));
  });

  it('successful UG-27 snapshot includes source design field tags', async () => {
    const prefill = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'shell', undefined, undefined, undefined, {
      'api510.external.exchanger.shell-tube.shell-side.design-pressure': 250,
      'api510.external.exchanger.shell-tube.shell-side.design-temperature': 450
    });
    vi.spyOn(pressureVesselApi, 'calculateCylindrical').mockResolvedValueOnce({ result: { radiusIn: 24, circumferentialRequiredThicknessIn: 0.2, longitudinalRequiredThicknessIn: 0.2, governingRequiredThicknessIn: 0.2, requiredWithCorrosionAllowanceIn: 0.2, marginIn: 0.3 }, resolvedAllowableStressPsi: 17500, materialMatched: null, temperatureUsed: 450, wasInterpolated: false, wasExtrapolated: false, stressSourceMessage: 'manual', warnings: [] });
    const result = await executeApi510ComponentCalculation({ prefill: prefill!, calculationType: 'ug-27-shell-tmin', manualInputs: { allowableStressPsi: 17500, jointEfficiency: 1, insideDiameterIn: 48, outsideDiameterIn: 48.5, originalThicknessIn: 0.5, providedThicknessIn: 0.5, corrosionAllowanceIn: 0 } });
    const tags = (result.snapshotReadyPayload?.inputs as any)?.sourceDesignFieldTags;
    expect(tags?.pressure).toBe('api510.external.exchanger.shell-tube.shell-side.design-pressure');
    expect(tags?.temperature).toBe('api510.external.exchanger.shell-tube.shell-side.design-temperature');
  });

  it('tubesheet area and channel cover remain non-executable/review-only', async () => {
    const values = {
      'api510.external.exchanger.shell-tube.tube-side.design-pressure': 300,
      'api510.external.exchanger.shell-tube.tube-side.design-temperature': 450
    };
    const tubesheet = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'tubesheet-area', undefined, undefined, undefined, values);
    const tubesheetResult = await executeApi510ComponentCalculation({ prefill: tubesheet!, calculationType: 'review-only' });
    expect(tubesheetResult.success).toBe(false);

    const channelCover = buildComponentCalculationPrefill('Shell and Tube Exchanger', 'channel-cover', undefined, undefined, undefined, values);
    const channelResult = await executeApi510ComponentCalculation({ prefill: channelCover!, calculationType: 'ug-32-formed-head-tmin', manualInputs: { allowableStressPsi: 17500 } });
    expect(channelResult.success).toBe(false);
  });

  it('generated API 510 nozzle detail fields exist across external component groups and docx', () => {
    const fields = getMvpExternalInspectionFields().map((f) => f.fieldTag);
    expect(fields).toContain('api510.external.drum-vessel.horizontal-drum.nozzles.nozzle-id-tag');
    expect(fields).toContain('api510.external.tower-column.distillation.nozzles.nozzle-id-tag');
    expect(fields).toContain('api510.external.exchanger.air-cooler.nozzles.nozzle-id-tag');
    expect(fields).toContain('api510.external.exchanger.double-pipe.nozzles.nozzle-id-tag');

    const doc = buildFieldCatalogWordReviewDocument();
    expect(doc).toContain('api510.external.exchanger.air-cooler.nozzles.nozzle-id-tag');
    expect(doc).toContain('api510.external.drum-vessel.horizontal-drum.nozzles.nozzle-id-tag');
  });

});
