import type { ReportTemplate } from '../types';

export type ReportTemplateCatalogNode = {
  standard: string;
  inspectionScope: string;
  equipmentFamily: string;
  equipmentSubtype: string;
  templateId: string;
  displayName: string;
};

export const buildExternalTemplateCatalog = (templates: ReportTemplate[]): ReportTemplateCatalogNode[] =>
  templates
    .filter((template) => (template.inspectionScope ?? 'External').toLowerCase() === 'external')
    .map((template) => ({
      standard: template.standard,
      inspectionScope: template.inspectionScope ?? 'External',
      equipmentFamily: template.equipmentFamily ?? template.equipmentType,
      equipmentSubtype: template.equipmentSubtype ?? template.name,
      templateId: template.id,
      displayName: template.name
    }));

export const API_510_EXTERNAL_TEMPLATE_IDS = new Set([
  'api-510-exchanger-shell-tube-external','api-510-exchanger-plate-frame-external','api-510-exchanger-double-pipe-external','api-510-exchanger-air-cooler-external',
  'api-510-drum-horizontal-external','api-510-drum-vertical-external','api-510-drum-separator-ko-external','api-510-drum-accumulator-receiver-external','api-510-vessel-generic-external','api-510-vessel-external',
  'api-510-tower-distillation-external','api-510-tower-absorber-stripper-external','api-510-tower-packed-column-external','api-510-tower-tray-column-external'
]);
