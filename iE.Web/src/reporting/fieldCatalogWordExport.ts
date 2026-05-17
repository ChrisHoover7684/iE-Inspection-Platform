import { externalInspectionFieldSets, getMvpExternalInspectionFields } from './inspectionFieldCatalog';

export const FIELD_CATALOG_WORD_TITLE = 'External Inspection Field Catalog Review';
export const FIELD_CATALOG_WORD_INTRO = 'This document is a field catalog review document. It is not a final report template. Use it to review field names, tags, grouping, options, and layout order before generating report templates.';

export function buildFieldCatalogWordReviewDocument(): string {
  const rows = getMvpExternalInspectionFields().map((f) => [f.fieldTag, f.label, f.sectionGroup, f.componentType, f.dataType, f.options.join(', '), String(f.required), String(f.supportsFinding), String(f.supportsRecommendation), String(f.supportsRepairRequired), String(f.supportsPhotoTag), String(f.supportsSummary), String(f.supportsNdeRequest), String(f.defaultLayoutOrder), f.reviewNotes ?? '']);
  return [FIELD_CATALOG_WORD_TITLE, FIELD_CATALOG_WORD_INTRO, 'Standard | Inspection Scope | Equipment Family | Equipment Subtype | Section Group | Component Type', ...externalInspectionFieldSets.map((s) => `${s.standard} | ${s.inspectionScope} | ${s.equipmentFamily} | ${s.equipmentSubtype}`), 'Field Tag | Label | Section Group | Component Type | Data Type | Options | Required | Supports Finding | Supports Recommendation | Supports Repair Required | Supports Photo Tag | Supports Summary | Supports NDE Request | Default Layout Order | Review Notes', ...rows.map((r) => r.join(' | '))].join('\n');
}

export function exportFieldCatalogWordReview(filename = 'external-inspection-field-catalog-review.docx'): Blob {
  const content = buildFieldCatalogWordReviewDocument();
  return new Blob([content], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });
}
