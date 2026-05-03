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
  systemId?: string;
  circuitId?: string;
  service?: string;
  status: string;
  createdAt: string;
  updatedAt?: string | null;
  sections: InspectionReportSection[];
  findings: InspectionFinding[];
  photos: InspectionPhoto[];
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
