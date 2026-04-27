export type ReportTemplate = {
  id: string;
  name: string;
  standard: string;
  equipmentType: string;
  sections: TemplateSection[];
};

export type TemplateSection = {
  id: string;
  title: string;
  order: number;
  isRepeatable: boolean;
};

export type InspectionReport = {
  id: string;
  clientOrganizationId: string;
  facilityId: string;
  processUnitId?: string | null;
  assetId?: string | null;
  templateId: string;
  status: string;
  createdAt: string;
  updatedAt?: string | null;
  sections: InspectionReportSection[];
  findings: InspectionFinding[];
};

export type InspectionReportSection = {
  sectionId: string;
  sectionTitle: string;
  order: number;
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
};

export type InspectionFinding = {
  id: string;
  componentLocation: string;
  componentType: string;
  findingType: string;
  associatedChecklistItem: string;
  detailedDescription: string;
  severity: string;
  recommendationRequired: boolean;
  recommendationText?: string | null;
};
