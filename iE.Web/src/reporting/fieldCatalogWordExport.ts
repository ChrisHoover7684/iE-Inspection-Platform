import { externalInspectionFieldSets, getMvpExternalInspectionFields } from './inspectionFieldCatalog';

export const FIELD_CATALOG_WORD_TITLE = 'External Inspection Field Catalog Review';
export const FIELD_CATALOG_WORD_INTRO = 'This document is a field catalog review document. It is not a final report template. Use it to review field names, tags, grouping, options, and layout order before generating report templates.';

export function buildFieldCatalogWordReviewDocument(): string {
  const groups = externalInspectionFieldSets
    .map((s) => `## ${s.standard} / ${s.inspectionScope} / ${s.equipmentFamily} / ${s.equipmentSubtype}`)
    .join('\n');
  const rows = getMvpExternalInspectionFields()
    .map((f) => `| ${f.fieldTag} | ${f.label} | ${f.sectionGroup} | ${f.componentType} | ${f.dataType} | ${f.options.join(', ')} | ${f.required} | ${f.supportsFinding} | ${f.supportsRecommendation} | ${f.supportsRepairRequired} | ${f.supportsPhotoTag} | ${f.supportsSummary} | ${f.supportsNdeRequest} | ${f.defaultLayoutOrder} | ${f.reviewNotes ?? ''} |`)
    .join('\n');

  return [
    `# ${FIELD_CATALOG_WORD_TITLE}`,
    FIELD_CATALOG_WORD_INTRO,
    groups,
    '| Field Tag | Label | Section Group | Component Type | Data Type | Options | Required | Supports Finding | Supports Recommendation | Supports Repair Required | Supports Photo Tag | Supports Summary | Supports NDE Request | Default Layout Order | Review Notes |',
    '|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|',
    rows
  ].join('\n\n');
}

export function exportFieldCatalogWordReview(filename = 'external-inspection-field-catalog-review.md'): Blob {
  const content = buildFieldCatalogWordReviewDocument();
  return new Blob([content], { type: 'text/markdown' });
}
