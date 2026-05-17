export type InspectionComponentDefinition = {
  componentKey: string;
  label: string;
  standard: string;
  inspectionScope: string;
  equipmentFamily: string;
  equipmentSubtype: string;
  requirementLevel: 'minimum' | 'optional';
  defaultSelected: boolean;
  fieldTagPrefix: string;
  supportsFinding: boolean;
  supportsRecommendation: boolean;
  supportsRepairRequired: boolean;
  supportsPhotoTag: boolean;
  supportsSummary: boolean;
  supportsNdeRequest: boolean;
  reviewNotes?: string;
};

const mk = (d: InspectionComponentDefinition): InspectionComponentDefinition => d;
const base = {
  standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment',
  supportsFinding: true, supportsRecommendation: true, supportsRepairRequired: true, supportsPhotoTag: true, supportsSummary: true, supportsNdeRequest: true,
  reviewNotes: 'External-only component availability for field catalog review.'
};

const create = (equipmentSubtype: string, fieldBase: string, components: Array<{ key: string; label: string; requirementLevel: 'minimum'|'optional' }>) =>
  components.map((c) => mk({ ...base, equipmentSubtype, componentKey: c.key, label: c.label, requirementLevel: c.requirementLevel, defaultSelected: c.requirementLevel === 'minimum', fieldTagPrefix: `${fieldBase}.${c.key}` }));

export const API510_EXTERNAL_COMPONENT_DEFINITIONS: InspectionComponentDefinition[] = [
  ...create('Shell and Tube Exchanger', 'api510.external.exchanger.shell-tube', [
    { key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'channel-channel-head', label: 'Channel / Channel Head', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },
    { key: 'shell-cover', label: 'Shell Cover', requirementLevel: 'optional' },{ key: 'channel-cover', label: 'Channel Cover', requirementLevel: 'optional' },{ key: 'channel-head-dollar-plate', label: 'Channel Head / Dollar Plate', requirementLevel: 'optional' },{ key: 'bonnet-head', label: 'Bonnet Head', requirementLevel: 'optional' },{ key: 'tubesheet-area', label: 'Tubesheet Area', requirementLevel: 'optional' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'saddles-supports', label: 'Saddles / Supports', requirementLevel: 'optional' },{ key: 'expansion-joint', label: 'Expansion Joint', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'coating-insulation-area', label: 'Coating / Insulation Area', requirementLevel: 'optional' },{ key: 'platform-ladder-access', label: 'Platform / Ladder / Access', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...create('Plate and Frame Exchanger', 'api510.external.exchanger.plate-frame', [
    { key: 'frame-head', label: 'Frame Head', requirementLevel: 'minimum' },{ key: 'pressure-plate', label: 'Pressure Plate', requirementLevel: 'minimum' },{ key: 'plate-pack-external', label: 'Plate Pack External', requirementLevel: 'minimum' },{ key: 'ports-nozzles', label: 'Ports / Nozzles', requirementLevel: 'minimum' },{ key: 'tie-bolts', label: 'Tie Bolts', requirementLevel: 'minimum' },
    { key: 'carrying-bar', label: 'Carrying Bar', requirementLevel: 'optional' },{ key: 'guide-bar', label: 'Guide Bar', requirementLevel: 'optional' },{ key: 'gaskets-seals', label: 'Gaskets / Seals', requirementLevel: 'optional' },{ key: 'supports', label: 'Supports', requirementLevel: 'optional' },{ key: 'leakage-staining-areas', label: 'Leakage / Staining Areas', requirementLevel: 'optional' },{ key: 'coating-corrosion-areas', label: 'Coating / Corrosion Areas', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...create('Double Pipe Exchanger', 'api510.external.exchanger.double-pipe', [
    { key: 'inner-pipe-external', label: 'Inner Pipe External', requirementLevel: 'minimum' },{ key: 'outer-pipe', label: 'Outer Pipe', requirementLevel: 'minimum' },{ key: 'return-bends', label: 'Return Bends', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },
    { key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'supports', label: 'Supports', requirementLevel: 'optional' },{ key: 'expansion-joint', label: 'Expansion Joint', requirementLevel: 'optional' },{ key: 'insulation-coating-area', label: 'Insulation / Coating Area', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...create('Air Cooler / Fin Fan', 'api510.external.exchanger.air-cooler', [
    { key: 'header-box', label: 'Header Box', requirementLevel: 'minimum' },{ key: 'tube-bundle-external', label: 'Tube Bundle External', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'frame-supports', label: 'Frame / Supports', requirementLevel: 'minimum' },
    { key: 'tubes', label: 'Tubes', requirementLevel: 'optional' },{ key: 'fins', label: 'Fins', requirementLevel: 'optional' },{ key: 'tube-supports', label: 'Tube Supports', requirementLevel: 'optional' },{ key: 'plugs-covers', label: 'Plugs / Covers', requirementLevel: 'optional' },{ key: 'fan-guard-area', label: 'Fan / Guard Area', requirementLevel: 'optional' },{ key: 'louvers', label: 'Louvers', requirementLevel: 'optional' },{ key: 'access-platforms', label: 'Access Platforms', requirementLevel: 'optional' },{ key: 'coating-corrosion-areas', label: 'Coating / Corrosion Areas', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ])
];


const towerMinimum = [
  { key: 'shell-courses', label: 'Shell Courses', requirementLevel: 'minimum' as const },
  { key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' as const },
  { key: 'manways', label: 'Manways', requirementLevel: 'minimum' as const },
  { key: 'skirt', label: 'Skirt', requirementLevel: 'minimum' as const },
  { key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'minimum' as const },
  { key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'minimum' as const }
];
const towerOptionalCommon = [
  { key: 'heads', label: 'Heads', requirementLevel: 'optional' as const },{ key: 'insulation-jacketing', label: 'Insulation / Jacketing', requirementLevel: 'optional' as const },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' as const },{ key: 'supports-bracing', label: 'Supports / Bracing', requirementLevel: 'optional' as const },{ key: 'davits-lifting-attachments', label: 'Davits / Lifting Attachments', requirementLevel: 'optional' as const },{ key: 'external-piping-attachments', label: 'External Piping Attachments', requirementLevel: 'optional' as const },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' as const },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' as const },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' as const },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' as const }
];

export const API510_EXTERNAL_DRUM_AND_TOWER_COMPONENT_DEFINITIONS: InspectionComponentDefinition[] = [
  ...create('Horizontal Drum', 'api510.external.drum-vessel.horizontal-drum', [
    { key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'saddles-supports', label: 'Saddles / Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'instruments-connections', label: 'Instruments / Connections', requirementLevel: 'optional' },{ key: 'boot-sump', label: 'Boot / Sump', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...create('Vertical Drum', 'api510.external.drum-vessel.vertical-drum', [
    { key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'skirt-legs-supports', label: 'Skirt / Legs / Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' }
  ]),
  ...create('Distillation Tower', 'api510.external.tower-column.distillation', [...towerMinimum, ...towerOptionalCommon, { key: 'tray-manways', label: 'Tray Manways', requirementLevel: 'optional' }, { key: 'draw-nozzles', label: 'Draw Nozzles', requirementLevel: 'optional' }, { key: 'reboiler-condenser-connections', label: 'Reboiler / Condenser Connections', requirementLevel: 'optional' }])
];

export const API510_EXTERNAL_ALL_COMPONENT_DEFINITIONS = [...API510_EXTERNAL_COMPONENT_DEFINITIONS, ...API510_EXTERNAL_DRUM_AND_TOWER_COMPONENT_DEFINITIONS];
