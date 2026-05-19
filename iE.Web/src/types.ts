
export type NdeLogStatus =
  | 'Draft'
  | 'Requested'
  | 'Scheduled'
  | 'In Progress'
  | 'Results Received'
  | 'Reviewed'
  | 'Closed'
  | 'Cancelled'
  | 'Overdue';

export type NdeReportStatus = 'Not Started' | 'Not Available' | 'In Progress' | 'Complete';
export type NdeReportResult = 'Acceptable' | 'Rejectable' | 'Monitor' | 'Inconclusive';
export type NdeReportTestResult = 'Passed' | 'Failed' | 'Info Only';
export type NdeReportDraft = {
  id: string;
  ndeRequestId: string;
  requestNumber: string;
  reportNumber: string;
  method: string;
  stage?: NdeLogItem['ndeStage'];
  inspector?: string;
  examinationDate?: string;
  procedure?: string;
  acceptanceCriteria?: string;
  surfaceCondition?: string;
  examinedArea?: string;
  indicationsFound: boolean;
  result?: NdeReportResult;
  testResult?: NdeReportTestResult;
  findings?: string;
  recommendations?: string;
  notes?: string;
  status: 'Draft' | 'Complete';
  generatedAtUtc?: string;
  reportFileName?: string;
  reportDownloadUrl?: string;
};

export type NdeLogItem = {
  id: string;
  requestNumber: string;
  assetTag?: string;
  circuitId?: string;
  equipmentTag?: string;
  method: string;
  status: NdeLogStatus;
  priority: 'Low' | 'Normal' | 'High' | 'Critical';
  requestedBy?: string;
  assignedTo?: string;
  dueDate?: string;
  resultReceivedDate?: string;
  reportStatus: NdeReportStatus;
  reportNumber?: string;
  reportFileName?: string;
  reportDownloadUrl?: string;
  accessType?: string;
  reference?: string;
  project?: string;
  owningGroup?: string;
  code?: string;
  codeCriteria?: string;
  unit?: string;
  facilityId?: string;
  facilityName?: string;
  inspectionDetails?: string;
  ndeStage?: 'Prep' | 'Root' | 'Final' | 'Pre-Stress' | 'Post-Stress' | 'Material Verification' | 'Other';
  weldId?: string;
  location?: string;
  relatedRequestGroupId?: string;
  relatedRequestGroupLabel?: string;
};

export type NdeLogTransitionEvent = {
  id: string;
  ndeRequestId: string;
  fromStatus: NdeLogStatus;
  toStatus: NdeLogStatus;
  actor: string;
  timestampUtc: string;
  comment?: string;
};

export type NdeLogTransitionCommand = {
  nextStatus: NdeLogStatus;
  comment?: string;
  actor?: string;
};
export type Api510ShellTubeExternalReportTypeId = 'api510.external.exchanger.shell-tube';

export type Api510ShellTubeExternalChecklistField =
  | 'Create Finding'
  | 'Recommendation Required'
  | 'Repair Required'
  | 'Photo Required'
  | 'NDE Required'
  | 'Add to Summary';

export type Api510ShellTubeExternalComponentState = {
  condition: string;
  location: string;
  findingNotes: string;
  recommendationText: string;
  photoTag: string;
  checklist: Record<Api510ShellTubeExternalChecklistField, boolean>;
};

export type Api510ShellTubeExternalReportHeaderDraft = {
  inspectionDate: string;
  inspector: string;
  clientFacility: string;
  unitArea: string;
  equipmentIdentifier: string;
  service: string;
  inspectionReason: string;
  operatingStatus: string;
  inspectionScope: string;
};

export type Api510ShellTubeExternalDesignConditionsDraft = {
  shellSideDesignPressure: string;
  shellSideDesignTemperature: string;
  tubeSideDesignPressure: string;
  tubeSideDesignTemperature: string;
};

export type Api510ShellTubeExternalMaterialsDraft = {
  shellMaterialSpec: string;
  shellMaterialGrade: string;
  shellProductForm: string;
  tubeChannelMaterialSpec: string;
  tubeChannelMaterialGrade: string;
  tubeChannelProductForm: string;
};

export type Api510ShellTubeExternalSummaryFieldsDraft = {
  generalExternalCondition: string;
  coatingInsulationNotes: string;
  leakageStainingNotes: string;
  supportsAttachmentsNotes: string;
  cmlThicknessReviewNotes: string;
  coatingInsulationDetails: string;
  leakageStainingDetails: string;
  supportsAttachmentsDetails: string;
  cmlThicknessReviewDetails: string;
  findingSummary: string;
};

export type Api510ShellTubeExternalDraftPayload = {
  reportTypeId: Api510ShellTubeExternalReportTypeId;
  reportTypeLabel: string;
  sourceStartWizardDraft: {
    storageKey: string;
    draft?: {
      reportTypeId: string;
      reportTypeLabel?: string;
      header: Record<string, string>;
      components: string[];
    };
  };
  reportHeader: Api510ShellTubeExternalReportHeaderDraft;
  designConditions: Api510ShellTubeExternalDesignConditionsDraft;
  materials: Api510ShellTubeExternalMaterialsDraft;
  componentStates: Record<string, Api510ShellTubeExternalComponentState | undefined>;
  checklistCounts: Record<Api510ShellTubeExternalChecklistField, number>;
  calculationSnapshots: import('./engineering/types').InspectionCalculationSnapshot[];
  findingDrafts: import('./reporting/api510CalculationFindings').Api510FindingDraft[];
  summaryFields: Api510ShellTubeExternalSummaryFieldsDraft;
  photos: {
    photoLog: string;
  };
  ndeTesting: {
    requirementsAndResults: string;
  };
  recommendations: {
    summary: string;
  };
  returnToService: {
    status: string;
    notes: string;
  };
  savedAt: string;
};


export type Api510RemainingExchangerExternalReportTypeId =
  | 'api510.external.exchanger.plate-frame'
  | 'api510.external.exchanger.double-pipe'
  | 'api510.external.exchanger.air-cooler';

export type Api510RemainingExchangerExternalChecklistField = Api510ShellTubeExternalChecklistField;

export type Api510RemainingExchangerExternalComponentState = Api510ShellTubeExternalComponentState;

export type Api510RemainingExchangerExternalReportHeaderDraft = Api510ShellTubeExternalReportHeaderDraft;

export type Api510RemainingExchangerExternalDesignConditionsDraft =
  | {
    hotSideDesignPressure: string;
    hotSideDesignTemperature: string;
    coldSideDesignPressure: string;
    coldSideDesignTemperature: string;
  }
  | {
    innerPipeSideDesignPressure: string;
    innerPipeSideDesignTemperature: string;
    annulusOuterPipeSideDesignPressure: string;
    annulusOuterPipeSideDesignTemperature: string;
  }
  | {
    tubeSideDesignPressure: string;
    tubeSideDesignTemperature: string;
    headerBoxDesignPressure: string;
    headerBoxDesignTemperature: string;
  };

export type Api510RemainingExchangerExternalMaterialsDraft = {
  primaryMaterialSpec: string;
  primaryMaterialGrade: string;
  primaryProductForm: string;
  secondaryMaterialSpec: string;
  secondaryMaterialGrade: string;
  secondaryProductForm: string;
};

export type Api510RemainingExchangerExternalSummaryFieldsDraft = {
  generalExternalCondition: string;
  coatingCorrosionNotes: string;
  leakageStainingNotes: string;
  supportsAttachmentsNotes: string;
  cmlThicknessReviewNotes: string;
  coatingCorrosionDetails: string;
  leakageStainingDetails: string;
  supportsAttachmentsDetails: string;
  cmlThicknessReviewDetails: string;
  calculationReviewNotes: string;
  findingSummary: string;
};

export type Api510RemainingExchangerExternalDraftPayload = {
  reportTypeId: Api510RemainingExchangerExternalReportTypeId;
  reportTypeLabel: string;
  sourceStartWizardDraft: Api510ShellTubeExternalDraftPayload['sourceStartWizardDraft'];
  reportHeader: Api510RemainingExchangerExternalReportHeaderDraft;
  designConditions: Api510RemainingExchangerExternalDesignConditionsDraft;
  materials: Api510RemainingExchangerExternalMaterialsDraft;
  componentStates: Record<string, Api510RemainingExchangerExternalComponentState | undefined>;
  checklistCounts: Record<Api510RemainingExchangerExternalChecklistField, number>;
  calculationReview: {
    status: 'review-only';
    notes: string;
  };
  summaryFields: Api510RemainingExchangerExternalSummaryFieldsDraft;
  photos: {
    photoLog: string;
  };
  ndeTesting: {
    requirementsAndResults: string;
  };
  recommendations: {
    summary: string;
  };
  returnToService: {
    status: string;
    notes: string;
  };
  savedAt: string;
};

export type StiApi653TankExternalChecklistField = Api510ShellTubeExternalChecklistField;

export type StiApi653TankExternalComponentState = Api510ShellTubeExternalComponentState;

export type StiApi653TankExternalReportHeaderDraft = Api510ShellTubeExternalReportHeaderDraft;

export type StiApi653TankExternalDetailsDraft = {
  tankId: string;
  tankService: string;
  tankConstruction: string;
};

export type StiApi653TankExternalSummaryFieldsDraft = {
  foundationNotes: string;
  shellNotes: string;
  roofNotes: string;
  chimeAreaNotes: string;
  nozzlesAppurtenancesNotes: string;
  ventsNotes: string;
  coatingNotes: string;
  containmentReleasePreventionNotes: string;
  overfillSpillPreventionNotes: string;
  anchorBoltsGroundingNotes: string;
  stairsPlatformsLaddersNotes: string;
  externalLeaksStainingNotes: string;
  findingSummary: string;
};

export type StiApi653TankExternalDraftPayload = {
  reportTypeId: 'sti653.external.tank';
  reportTypeLabel: string;
  sourceStartWizardDraft: Api510ShellTubeExternalDraftPayload['sourceStartWizardDraft'];
  reportHeader: StiApi653TankExternalReportHeaderDraft;
  tankDetails: StiApi653TankExternalDetailsDraft;
  sections: string[];
  componentStates: Record<string, StiApi653TankExternalComponentState | undefined>;
  checklistCounts: Record<StiApi653TankExternalChecklistField, number>;
  summaryFields: StiApi653TankExternalSummaryFieldsDraft;
  photos: {
    photoLog: string;
  };
  ndeTesting: {
    requirementsAndResults: string;
  };
  recommendations: {
    summary: string;
  };
  returnToService: {
    status: string;
    notes: string;
  };
  savedAt: string;
};

export type ReportTemplate = {
  id: string;
  name: string;
  standard: string;
  equipmentType: string;
  description?: string;
  sections: TemplateSection[];
};

export type TemplateSection = {
  id: string;
  title: string;
  order: number;
  isRepeatable: boolean;
  fields?: TemplateField[];
};

export type TemplateField = {
  id: string;
  label: string;
  dataType: string;
  isRequired: boolean;
  defaultValue?: string | null;
  isChecklistItem?: boolean;
  allowsComment?: boolean;
  allowsPhotoFlag?: boolean;
  allowsTransferToComponentSection?: boolean;
  allowsRecommendationFlag?: boolean;
  helpText?: string | null;
  options?: string[];
};

export type InspectionReport = {
  id: string;
  clientOrganizationId: string;
  facilityId: string;
  processUnitId?: string | null;
  assetId?: string | null;
  createdByUserId?: string | null;
  updatedByUserId?: string | null;
  templateId: string;
  reportNumber?: string;
  equipmentTag?: string;
  unit?: string;
  facilityName?: string;
  systemId?: string;
  circuitId?: string;
  service?: string;
  status: string;
  createdAt: string;
  updatedAt?: string | null;
  sections: InspectionReportSection[];
  findings: InspectionFinding[];
  photos: InspectionPhoto[];
  calculations: InspectionCalculationSnapshot[];
};

export type InspectionReportSection = {
  sectionId: string;
  sectionTitle: string;
  order: number;
  isRepeatable?: boolean;
  instanceNumber?: number | null;
  answers: InspectionReportAnswer[];
};

export type InspectionReportAnswer = {
  fieldId: string;
  label: string;
  dataType: string;
  value?: string | null;
  values: string[];
  comment?: string | null;
  photoRequired?: boolean | null;
  transferToComponentSection?: boolean | null;
  recommendationRequired?: boolean | null;
  repairRequired?: boolean | null;
};

export type InspectionFinding = {
  id: string;
  location: string;
  componentType: string;
  findingType: string;
  associatedChecklistItem: string;
  description: string;
  severity: string;
  repairRequired: boolean;
  repairRecommendation?: string | null;
  photoIds?: string[];
  addToSummary?: boolean;
  summaryText?: string | null;
};

export type InspectionPhoto = {
  id: string;
  photoNumber: string;
  description: string;
  relatedComponent: string;
  relatedChecklistItem: string;
  photoRequired: boolean;
  photoAttached: boolean;
  fileName?: string | null;
  fileUrl?: string | null;
};

export type InlineSuggestion = {
  promptType: string;
  suggestion: string;
  severity: string;
  replacementText?: string | null;
};

export type InlineSuggestionsResponse = {
  suggestions: InlineSuggestion[];
  severity: string;
  wasEvaluated: boolean;
  mode: string;
};

export type UiAlert = {
  id: string;
  severity: string;
  title: string;
  message: string;
  fieldId?: string | null;
  sectionId?: string | null;
  findingId?: string | null;
};

export type NarrativeResult = {
  summary: string;
  sections: { title: string; narrative: string }[];
  recommendedActions: string[];
};


export type CorrosionRateInput = {
  initialThicknessInches: number;
  finalThicknessInches: number;
  useDates: boolean;
  exposureTimeYears?: number | null;
  initialDate?: string | null;
  finalDate?: string | null;
  inspectionFactor: number;
  currentThicknessInches: number;
  tminInches: number;
};

export type CorrosionRateResult = {
  thicknessLossInches: number;
  exposureTimeYears: number;
  corrosionRateInchesPerYear: number;
  corrosionRateMpy: number;
  corrosionRateMmPerYear: number;
  remainingLifeYears?: number | null;
  nextInspectionYears?: number | null;
  nextInspectionDate?: string | null;
  warnings: string[];
  display: string;
};


export type PipeLookupInput = {
  nps: string;
  schedule: string;
};

export type PipeLookupResult = {
  nps: string;
  schedule: string;
  outsideDiameter: number;
  nominalThickness: number;
  insideDiameter: number;
  lowerLimitMinus12_5: number;
  upperLimitPlus12_5: number;
  display: string;
};

export type B313ThicknessInput = {
  pressurePsi: number;
  temperatureF: number;
  outsideDiameterIn: number;
  spec: string;
  grade: string;
  productForm: string;
  unsNo: string;
  classConditionTemper: string;
  materialCategory: string;
  jointType: string;
  jointQualityKey: string;
  wFactor?: number;
  yOverride?: number | null;
  eOverride?: number | null;
};

export type B313ThicknessResult = {
  success: boolean;
  message: string;
  allowableStressPsi: number | null;
  eFactor: number | null;
  yCoefficient: number | null;
  wFactor: number | null;
  requiredThicknessIn: number | null;
};

export type LwnLookupInput = {
  size: string;
  schedule: string;
};

export type LwnLookupResult = {
  size: string;
  schedule: string;
  nominalThickness: number;
  outsideDiameter: number;
  insideDiameter: number;
  minThickness: number;
  maxThickness: number;
  display: string;
};


export type CylindricalShellInput = { designPressurePsi:number; allowableStressPsi:number; insideDiameterIn:number; outsideDiameterIn:number; originalThicknessIn:number; jointEfficiency:number; corrosionAllowanceIn:number; providedThicknessIn:number; };
export type CylindricalShellResult = { radiusIn:number; circumferentialRequiredThicknessIn:number; longitudinalRequiredThicknessIn:number; governingRequiredThicknessIn:number; requiredWithCorrosionAllowanceIn:number; marginIn:number; };
export type SphericalShellInput = CylindricalShellInput;
export type SphericalShellResult = { insideRadiusIn:number; governingRequiredThicknessIn:number; requiredWithCorrosionAllowanceIn:number; marginIn:number; warnings:string[]; };
export type ConicalShellInput = { designPressurePsi:number; allowableStressPsi:number; effectiveInsideDiameterIn:number; halfApexAngleDeg:number; jointEfficiency:number; corrosionAllowanceIn:number; providedThicknessIn:number; };
export type ConicalShellResult = { formulaRequiredThicknessIn:number; requiredWithCorrosionAllowanceIn:number; marginIn:number; warnings:string[]; };
export type HeadType = 'Ellipsoidal2To1'|'Hemispherical'|'TorisphericalAsmeFd'|'Conical'|'Toriconical'|'FlatUg34';
export type HeadThicknessInput = { headType:HeadType; designPressurePsi:number; allowableStressPsi:number; jointEfficiency:number; effectiveInsideDiameterIn:number; effectiveInsideRadiusIn:number; crownRadiusIn:number; halfApexAngleDeg:number; flatHeadCFactor:number; corrosionAllowanceIn:number; providedThicknessIn:number; };
export type HeadThicknessResult = { governingRequiredThicknessIn:number; requiredWithCorrosionAllowanceIn:number; marginIn:number; warnings:string[]; };
export type NozzleType = 'PipeNozzle'|'ForgedNozzle'|'LongWeldNeck'|'FittingNozzle';
export type AttachmentLocation = 'Shell'|'Head';
export type CodeEra = 'Pre1999'|'Post1999';
export type NozzleThicknessInput = { designCode:string; designPressurePsi:number; externalPressurePsi:number; designTemperatureF:number; jointEfficiency:number; corrosionAllowanceIn:number; manualAllowableStress:boolean; allowableStressPsi:number; materialSpec:string; materialGrade:string; materialProductForm:string; codeEra:CodeEra; attachmentLocation:AttachmentLocation; shellOrHeadRequiredThicknessIn:number; shellOrHeadExternalRequiredThicknessIn:number; ug16MinimumThicknessIn:number; nozzleType:NozzleType; useOdForTa:boolean; useIdForTa:boolean; outsideDiameterIn:number; insideDiameterIn:number; nominalThicknessIn:number; originalThicknessIn:number; nominalPipeSize:string; ug45TableMinimumThicknessIn:number|null; };
export type NozzleThicknessResult = { isValid:boolean; errorMessage:string; jointEfficiencyUsed:number; insideRadiusUsed:number; taRequiredThicknessIn:number; tb1RequiredThicknessIn:number; tb2RequiredThicknessIn:number; tb3TableThicknessIn:number; tb3PlusCorrosionAllowanceIn:number; tbRequiredThicknessIn:number; pressureRequiredThicknessIn:number; governingRequiredThicknessIn:number; marginIn:number; providedThicknessIn:number; corrodedThicknessIn:number; requiredThicknessPlusCorrosionAllowanceIn:number; isAcceptable:boolean; warnings:string[]; };
export type Ug45TableEntry = { nps:string; minimumThicknessIn:number|null; isAvailable:boolean; };

export type PressureVesselMaterialStressInput = { designCode:string; stressEra:string; designTemperatureF:number; materialSpec:string; materialGrade:string; productForm:string; alloyUNS:string; classConditionTemper:string; manualAllowableStress:boolean; allowableStressPsi:number|null; calculationFamily?:'PressureVessel'; };
export type PressureVesselMaterialStressResolveResult = { isValid:boolean; allowableStressPsi:number|null; materialMatched:string|null; temperatureUsed:number; wasInterpolated:boolean; wasExtrapolated:boolean; message:string; warnings:string[]; };
export type CylindricalShellCalculationRequest = { input:CylindricalShellInput; materialStress:PressureVesselMaterialStressInput; };
export type SphericalShellCalculationRequest = { input:SphericalShellInput; materialStress:PressureVesselMaterialStressInput; };
export type ConicalShellCalculationRequest = { input:ConicalShellInput; materialStress:PressureVesselMaterialStressInput|null; };
export type HeadCalculationRequest = { input:HeadThicknessInput; materialStress:PressureVesselMaterialStressInput|null; };
export type NozzleCalculationRequest = { input:NozzleThicknessInput; };
export type CalculationEnvelope<T> = { resolvedAllowableStressPsi:number; materialMatched:string|null; temperatureUsed:number; wasInterpolated:boolean; wasExtrapolated:boolean; stressSourceMessage:string; result:T; warnings:string[]; };
import type { InspectionCalculationSnapshot } from './engineering/types';

export type Api510DrumVesselExternalReportTypeId =
  | 'api510.external.drum-vessel.horizontal-drum'
  | 'api510.external.drum-vessel.vertical-drum'
  | 'api510.external.drum-vessel.separator-ko-drum'
  | 'api510.external.drum-vessel.accumulator-receiver'
  | 'api510.external.drum-vessel.generic-pressure-vessel';

export type Api510DrumVesselExternalChecklistField = Api510ShellTubeExternalChecklistField;

export type Api510DrumVesselExternalComponentState = {
  condition: string;
  location: string;
  findingNotes: string;
  recommendationText: string;
  photoTag: string;
  checklist: Record<Api510DrumVesselExternalChecklistField, boolean>;
};

export type Api510DrumVesselExternalReportHeaderDraft = Api510ShellTubeExternalReportHeaderDraft;

export type Api510DrumVesselExternalDesignConditionsDraft = {
  vesselDesignPressure: string;
  vesselDesignTemperature: string;
};

export type Api510DrumVesselExternalMaterialsDraft = {
  shellMaterialSpec: string;
  shellMaterialGrade: string;
  shellProductForm: string;
  headMaterialSpec: string;
  headMaterialGrade: string;
  headProductForm: string;
  headType: string;
};

export type Api510DrumVesselExternalSummaryFieldsDraft = Api510ShellTubeExternalSummaryFieldsDraft;

export type Api510DrumVesselExternalDraftPayload = {
  reportTypeId: Api510DrumVesselExternalReportTypeId;
  reportTypeLabel: string;
  equipmentSubtype: string;
  sourceStartWizardDraft: {
    storageKey: string;
    draft?: {
      reportTypeId: string;
      reportTypeLabel?: string;
      header: Record<string, string>;
      components: string[];
    };
  };
  reportHeader: Api510DrumVesselExternalReportHeaderDraft;
  designConditions: Api510DrumVesselExternalDesignConditionsDraft;
  materials: Api510DrumVesselExternalMaterialsDraft;
  componentStates: Record<string, Api510DrumVesselExternalComponentState | undefined>;
  checklistCounts: Record<Api510DrumVesselExternalChecklistField, number>;
  calculationSnapshots: import('./engineering/types').InspectionCalculationSnapshot[];
  findingDrafts: import('./reporting/api510CalculationFindings').Api510FindingDraft[];
  summaryFields: Api510DrumVesselExternalSummaryFieldsDraft;
  photos: {
    photoLog: string;
  };
  ndeTesting: {
    requirementsAndResults: string;
  };
  recommendations: {
    summary: string;
  };
  returnToService: {
    status: string;
    notes: string;
  };
  savedAt: string;
};

export type Api510TowerColumnExternalReportTypeId =
  | 'api510.external.tower-column.distillation'
  | 'api510.external.tower-column.absorber-stripper'
  | 'api510.external.tower-column.packed-column'
  | 'api510.external.tower-column.tray-column';

export type Api510TowerColumnExternalChecklistField = Api510ShellTubeExternalChecklistField;

export type Api510TowerColumnExternalComponentState = {
  condition: string;
  location: string;
  findingNotes: string;
  recommendationText: string;
  photoTag: string;
  checklist: Record<Api510TowerColumnExternalChecklistField, boolean>;
};

export type Api510TowerColumnExternalReportHeaderDraft = Api510ShellTubeExternalReportHeaderDraft;

export type Api510TowerColumnExternalDesignConditionsDraft = {
  vesselDesignPressure: string;
  vesselDesignTemperature: string;
};

export type Api510TowerColumnExternalMaterialsDraft = {
  shellCourseMaterialSpec: string;
  shellCourseMaterialGrade: string;
  shellCourseProductForm: string;
};

export type Api510TowerColumnExternalSummaryFieldsDraft = {
  shellCourseCorrosionCoatingCondition: string;
  nozzleExternalCondition: string;
  manwayExternalCondition: string;
  skirtCondition: string;
  baseRingAnchorBoltCondition: string;
  platformLadderHandrailCondition: string;
  insulationJacketingCondition: string;
  leakageStaining: string;
  externalDeformationBulgingMechanicalDamage: string;
  cmlThicknessReviewNotes: string;
  externalSummary: string;
  coatingInsulationDetails: string;
  leakageStainingDetails: string;
  supportsAttachmentsDetails: string;
  cmlThicknessReviewDetails: string;
  findingSummary: string;
};

export type Api510TowerColumnExternalDraftPayload = {
  reportTypeId: Api510TowerColumnExternalReportTypeId;
  reportTypeLabel: string;
  equipmentSubtype: string;
  sourceStartWizardDraft: {
    storageKey: string;
    draft?: {
      reportTypeId: string;
      reportTypeLabel?: string;
      header: Record<string, string>;
      components: string[];
    };
  };
  reportHeader: Api510TowerColumnExternalReportHeaderDraft;
  designConditions: Api510TowerColumnExternalDesignConditionsDraft;
  materials: Api510TowerColumnExternalMaterialsDraft;
  componentStates: Record<string, Api510TowerColumnExternalComponentState | undefined>;
  checklistCounts: Record<Api510TowerColumnExternalChecklistField, number>;
  summaryFields: Api510TowerColumnExternalSummaryFieldsDraft;
  photos: {
    photoLog: string;
  };
  ndeTesting: {
    requirementsAndResults: string;
  };
  recommendations: {
    summary: string;
  };
  returnToService: {
    status: string;
    notes: string;
  };
  savedAt: string;
};
