export type ApiInspectionReportOption = {
  id: string;
  label: string;
  category?: string;
  standard: 'API 570' | 'API 510 External' | 'STI / API 653 External';
};

type Group = { label: string; options: ApiInspectionReportOption[] };

export const apiInspectionReportHierarchy: {
  api570: { label: string; options: ApiInspectionReportOption[] };
  api510External: { label: string; groups: Group[] };
  sti653External: { label: string; options: ApiInspectionReportOption[] };
} = {
  api570: { label: 'API 570', options: [
    { id: 'api570.piping-external', label: 'Piping External', standard: 'API 570' },
    { id: 'api570.piping-cui-external', label: 'Piping CUI External', standard: 'API 570' }
  ]},
  api510External: { label: 'API 510 External', groups: [
    { label: 'Exchangers', options: [
      { id: 'api510.external.exchanger.shell-tube', label: 'Shell and Tube Exchanger External', category: 'Exchangers', standard: 'API 510 External' },
      { id: 'api510.external.exchanger.plate-frame', label: 'Plate and Frame Exchanger External', category: 'Exchangers', standard: 'API 510 External' },
      { id: 'api510.external.exchanger.double-pipe', label: 'Double Pipe Exchanger External', category: 'Exchangers', standard: 'API 510 External' },
      { id: 'api510.external.exchanger.air-cooler', label: 'Air Cooler / Fin Fan External', category: 'Exchangers', standard: 'API 510 External' }
    ]},
    { label: 'Drums / Pressure Vessels', options: [
      { id: 'api510.external.drum-vessel.horizontal-drum', label: 'Horizontal Drum External', category: 'Drums / Pressure Vessels', standard: 'API 510 External' },
      { id: 'api510.external.drum-vessel.vertical-drum', label: 'Vertical Drum External', category: 'Drums / Pressure Vessels', standard: 'API 510 External' },
      { id: 'api510.external.drum-vessel.separator-ko-drum', label: 'Separator / KO Drum External', category: 'Drums / Pressure Vessels', standard: 'API 510 External' },
      { id: 'api510.external.drum-vessel.accumulator-receiver', label: 'Accumulator / Receiver External', category: 'Drums / Pressure Vessels', standard: 'API 510 External' },
      { id: 'api510.external.drum-vessel.generic-pressure-vessel', label: 'Generic Pressure Vessel External', category: 'Drums / Pressure Vessels', standard: 'API 510 External' }
    ]},
    { label: 'Towers / Columns', options: [
      { id: 'api510.external.tower-column.distillation', label: 'Distillation Tower External', category: 'Towers / Columns', standard: 'API 510 External' },
      { id: 'api510.external.tower-column.absorber-stripper', label: 'Absorber / Stripper External', category: 'Towers / Columns', standard: 'API 510 External' },
      { id: 'api510.external.tower-column.packed-column', label: 'Packed Column External', category: 'Towers / Columns', standard: 'API 510 External' },
      { id: 'api510.external.tower-column.tray-column', label: 'Tray Column External', category: 'Towers / Columns', standard: 'API 510 External' }
    ]}
  ]},
  sti653External: { label: 'STI / API 653 External', options: [
    { id: 'sti653.external.tank', label: 'Tank External', standard: 'STI / API 653 External' }
  ]}
};

export const allApiInspectionReportOptions: ApiInspectionReportOption[] = [
  ...apiInspectionReportHierarchy.api570.options,
  ...apiInspectionReportHierarchy.api510External.groups.flatMap((g) => g.options),
  ...apiInspectionReportHierarchy.sti653External.options
];
