export type PressureBoundarySide = 'shell-side' | 'tube-side' | 'channel-side' | 'shared' | 'unknown';

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
  parentComponentRequired: boolean;
  allowedParentComponentKeys: string[];
  pressureBoundarySide: PressureBoundarySide;
  designPressureFieldTag?: string;
  designTemperatureFieldTag?: string;
  designPressureFieldTagsByPressureBoundarySide?: Partial<Record<PressureBoundarySide, string>>;
  designTemperatureFieldTagsByPressureBoundarySide?: Partial<Record<PressureBoundarySide, string>>;
  designConditionSourceOptions?: string[];
  parentThicknessSourceOptions?: string[];
  nozzleLocationRequired?: boolean;
  nozzleLocationOptions?: string[];
  supportsTminCalculation: boolean;
  tminCalculationMethod?: string;
  supportsNozzleUg45: boolean;
  supportsFinding: boolean;
  supportsRecommendation: boolean;
  supportsRepairRequired: boolean;
  supportsPhotoTag: boolean;
  supportsSummary: boolean;
  supportsNdeRequest: boolean;
  reviewNotes?: string;
};

const base = (subtype: string): Omit<InspectionComponentDefinition, 'componentKey'|'label'|'requirementLevel'|'defaultSelected'|'fieldTagPrefix'|'pressureBoundarySide'> => ({
  standard: 'API 510', inspectionScope: 'External', equipmentFamily: 'Pressure Equipment', equipmentSubtype: subtype,
  parentComponentRequired: false, allowedParentComponentKeys: [], supportsTminCalculation: false, supportsNozzleUg45: false,
  supportsFinding: true, supportsRecommendation: true, supportsRepairRequired: true, supportsPhotoTag: true, supportsSummary: true, supportsNdeRequest: true,
  reviewNotes: 'External-only component availability for field catalog review.'
});

const build = (subtype: string, prefix: string, items: Array<{key:string;label:string;requirementLevel:'minimum'|'optional';side?:PressureBoundarySide;}>) =>
  items.map((i) => ({ ...base(subtype), componentKey: i.key, label: i.label, requirementLevel: i.requirementLevel, defaultSelected: i.requirementLevel === 'minimum', fieldTagPrefix: `${prefix}.${i.key}`, pressureBoundarySide: i.side ?? 'unknown' }));

const shellTubePrefix = 'api510.external.exchanger.shell-tube';
const shellTubeNozzleParents = ['shell', 'channel-channel-head', 'shell-cover', 'channel-cover', 'bonnet-head', 'tubesheet-area', 'other-component'];

export const API510_EXTERNAL_COMPONENT_DEFINITIONS: InspectionComponentDefinition[] = [
  ...build('Shell and Tube Exchanger', shellTubePrefix, [
    { key: 'shell', label: 'Shell', requirementLevel: 'minimum', side: 'shell-side' },{ key: 'channel-channel-head', label: 'Channel / Channel Head', requirementLevel: 'minimum', side: 'tube-side' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum', side: 'shared' },
    { key: 'shell-cover', label: 'Shell Cover', requirementLevel: 'optional', side: 'shell-side' },{ key: 'channel-cover', label: 'Channel Cover', requirementLevel: 'optional', side: 'tube-side' },{ key: 'channel-head-dollar-plate', label: 'Channel Head / Dollar Plate', requirementLevel: 'optional', side: 'tube-side' },{ key: 'bonnet-head', label: 'Bonnet Head', requirementLevel: 'optional', side: 'tube-side' },{ key: 'tubesheet-area', label: 'Tubesheet Area', requirementLevel: 'optional', side: 'shared' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'saddles-supports', label: 'Saddles / Supports', requirementLevel: 'optional' },{ key: 'expansion-joint', label: 'Expansion Joint', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'coating-insulation-area', label: 'Coating / Insulation Area', requirementLevel: 'optional' },{ key: 'platform-ladder-access', label: 'Platform / Ladder / Access', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...build('Plate and Frame Exchanger', 'api510.external.exchanger.plate-frame', [
    { key: 'frame-head', label: 'Frame Head', requirementLevel: 'minimum' },{ key: 'pressure-plate', label: 'Pressure Plate', requirementLevel: 'minimum' },{ key: 'plate-pack-external', label: 'Plate Pack External', requirementLevel: 'minimum' },{ key: 'ports-nozzles', label: 'Ports / Nozzles', requirementLevel: 'minimum', side: 'shared' },{ key: 'tie-bolts', label: 'Tie Bolts', requirementLevel: 'minimum' },
    { key: 'carrying-bar', label: 'Carrying Bar', requirementLevel: 'optional' },{ key: 'guide-bar', label: 'Guide Bar', requirementLevel: 'optional' },{ key: 'gaskets-seals', label: 'Gaskets / Seals', requirementLevel: 'optional' },{ key: 'supports', label: 'Supports', requirementLevel: 'optional' },{ key: 'leakage-staining-areas', label: 'Leakage / Staining Areas', requirementLevel: 'optional' },{ key: 'coating-corrosion-areas', label: 'Coating / Corrosion Areas', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...build('Double Pipe Exchanger', 'api510.external.exchanger.double-pipe', [
    { key: 'inner-pipe-external', label: 'Inner Pipe External', requirementLevel: 'minimum' },{ key: 'outer-pipe', label: 'Outer Pipe', requirementLevel: 'minimum' },{ key: 'return-bends', label: 'Return Bends', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum', side: 'shared' },
    { key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'supports', label: 'Supports', requirementLevel: 'optional' },{ key: 'expansion-joint', label: 'Expansion Joint', requirementLevel: 'optional' },{ key: 'insulation-coating-area', label: 'Insulation / Coating Area', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...build('Air Cooler / Fin Fan', 'api510.external.exchanger.air-cooler', [
    { key: 'header-box', label: 'Header Box', requirementLevel: 'minimum' },{ key: 'tube-bundle-external', label: 'Tube Bundle External', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'frame-supports', label: 'Frame / Supports', requirementLevel: 'minimum' },
    { key: 'tubes', label: 'Tubes', requirementLevel: 'optional' },{ key: 'fins', label: 'Fins', requirementLevel: 'optional' },{ key: 'tube-supports', label: 'Tube Supports', requirementLevel: 'optional' },{ key: 'plugs-covers', label: 'Plugs / Covers', requirementLevel: 'optional' },{ key: 'fan-guard-area', label: 'Fan / Guard Area', requirementLevel: 'optional' },{ key: 'louvers', label: 'Louvers', requirementLevel: 'optional' },{ key: 'access-platforms', label: 'Access Platforms', requirementLevel: 'optional' },{ key: 'coating-corrosion-areas', label: 'Coating / Corrosion Areas', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }
  ]),
  ...build('Horizontal Drum', 'api510.external.drum-vessel.horizontal-drum', [{ key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'saddles-supports', label: 'Saddles / Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'instruments-connections', label: 'Instruments / Connections', requirementLevel: 'optional' },{ key: 'boot-sump', label: 'Boot / Sump', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }]),
  ...build('Vertical Drum', 'api510.external.drum-vessel.vertical-drum', [{ key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'skirt-legs-supports', label: 'Skirt / Legs / Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' },{ key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'optional' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'instruments-connections', label: 'Instruments / Connections', requirementLevel: 'optional' },{ key: 'boot-sump', label: 'Boot / Sump', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }]),
  ...build('Separator / KO Drum', 'api510.external.drum-vessel.separator-ko-drum', [{ key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'supports', label: 'Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' },{ key: 'boot-sump', label: 'Boot / Sump', requirementLevel: 'optional' },{ key: 'drain-connections', label: 'Drain Connections', requirementLevel: 'optional' },{ key: 'vents', label: 'Vents', requirementLevel: 'optional' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'instruments-level-connections', label: 'Instruments / Level Connections', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }]),
  ...build('Accumulator / Receiver', 'api510.external.drum-vessel.accumulator-receiver', [{ key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'supports', label: 'Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'optional' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'instruments-connections', label: 'Instruments / Connections', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }]),
  ...build('Generic Pressure Vessel', 'api510.external.drum-vessel.generic-pressure-vessel', [{ key: 'shell', label: 'Shell', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'supports', label: 'Supports', requirementLevel: 'minimum' },{ key: 'manway', label: 'Manway', requirementLevel: 'minimum' },{ key: 'flanges-gaskets-bolting', label: 'Flanges / Gaskets / Bolting', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'instruments-connections', label: 'Instruments / Connections', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'insulation', label: 'Insulation', requirementLevel: 'optional' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' }]),
  ...build('Distillation Tower', 'api510.external.tower-column.distillation', [{ key: 'shell-courses', label: 'Shell Courses', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'manways', label: 'Manways', requirementLevel: 'minimum' },{ key: 'skirt', label: 'Skirt', requirementLevel: 'minimum' },{ key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'minimum' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'optional' },{ key: 'insulation-jacketing', label: 'Insulation / Jacketing', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'supports-bracing', label: 'Supports / Bracing', requirementLevel: 'optional' },{ key: 'davits-lifting-attachments', label: 'Davits / Lifting Attachments', requirementLevel: 'optional' },{ key: 'external-piping-attachments', label: 'External Piping Attachments', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' },{ key: 'tray-manways', label: 'Tray Manways', requirementLevel: 'optional' },{ key: 'draw-nozzles', label: 'Draw Nozzles', requirementLevel: 'optional' },{ key: 'reboiler-condenser-connections', label: 'Reboiler / Condenser Connections', requirementLevel: 'optional' }]),
  ...build('Absorber / Stripper', 'api510.external.tower-column.absorber-stripper', [{ key: 'shell-courses', label: 'Shell Courses', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'manways', label: 'Manways', requirementLevel: 'minimum' },{ key: 'skirt', label: 'Skirt', requirementLevel: 'minimum' },{ key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'minimum' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'optional' },{ key: 'insulation-jacketing', label: 'Insulation / Jacketing', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'supports-bracing', label: 'Supports / Bracing', requirementLevel: 'optional' },{ key: 'davits-lifting-attachments', label: 'Davits / Lifting Attachments', requirementLevel: 'optional' },{ key: 'external-piping-attachments', label: 'External Piping Attachments', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' },{ key: 'packing-access-manways', label: 'Packing Access Manways', requirementLevel: 'optional' },{ key: 'distributor-nozzles', label: 'Distributor Nozzles', requirementLevel: 'optional' },{ key: 'reboiler-overhead-connections', label: 'Reboiler / Overhead Connections', requirementLevel: 'optional' }]),
  ...build('Packed Column', 'api510.external.tower-column.packed-column', [{ key: 'shell-courses', label: 'Shell Courses', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'manways', label: 'Manways', requirementLevel: 'minimum' },{ key: 'skirt', label: 'Skirt', requirementLevel: 'minimum' },{ key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'minimum' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'optional' },{ key: 'insulation-jacketing', label: 'Insulation / Jacketing', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'supports-bracing', label: 'Supports / Bracing', requirementLevel: 'optional' },{ key: 'davits-lifting-attachments', label: 'Davits / Lifting Attachments', requirementLevel: 'optional' },{ key: 'external-piping-attachments', label: 'External Piping Attachments', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' },{ key: 'packing-support-access', label: 'Packing Support Access', requirementLevel: 'optional' },{ key: 'distributor-access', label: 'Distributor Access', requirementLevel: 'optional' },{ key: 'mist-eliminator-access', label: 'Mist Eliminator Access', requirementLevel: 'optional' }]),
  ...build('Tray Column', 'api510.external.tower-column.tray-column', [{ key: 'shell-courses', label: 'Shell Courses', requirementLevel: 'minimum' },{ key: 'nozzles', label: 'Nozzles', requirementLevel: 'minimum' },{ key: 'manways', label: 'Manways', requirementLevel: 'minimum' },{ key: 'skirt', label: 'Skirt', requirementLevel: 'minimum' },{ key: 'base-ring-anchor-bolts', label: 'Base Ring / Anchor Bolts', requirementLevel: 'minimum' },{ key: 'platforms-ladders-handrails', label: 'Platforms / Ladders / Handrails', requirementLevel: 'minimum' },{ key: 'heads', label: 'Heads', requirementLevel: 'optional' },{ key: 'insulation-jacketing', label: 'Insulation / Jacketing', requirementLevel: 'optional' },{ key: 'coating', label: 'Coating', requirementLevel: 'optional' },{ key: 'supports-bracing', label: 'Supports / Bracing', requirementLevel: 'optional' },{ key: 'davits-lifting-attachments', label: 'Davits / Lifting Attachments', requirementLevel: 'optional' },{ key: 'external-piping-attachments', label: 'External Piping Attachments', requirementLevel: 'optional' },{ key: 'vents-drains', label: 'Vents / Drains', requirementLevel: 'optional' },{ key: 'nameplate-markings', label: 'Nameplate / Markings', requirementLevel: 'optional' },{ key: 'cml-locations', label: 'CML Locations', requirementLevel: 'optional' },{ key: 'other-component', label: 'Other Component', requirementLevel: 'optional' },{ key: 'tray-access-manways', label: 'Tray Access Manways', requirementLevel: 'optional' },{ key: 'downcomer-tray-access-locations', label: 'Downcomer / Tray Access Locations', requirementLevel: 'optional' },{ key: 'draw-pan-connections', label: 'Draw Pan Connections', requirementLevel: 'optional' }])

].map((d) => {
  if (d.equipmentSubtype === 'Shell and Tube Exchanger') {
    if (d.componentKey === 'shell') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'UG-27 cylindrical shell',
      designPressureFieldTag: `${shellTubePrefix}.shell-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.shell-side.design-temperature`
    });
    if (d.componentKey === 'shell-cover') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'UG-32 formed head',
      designPressureFieldTag: `${shellTubePrefix}.shell-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.shell-side.design-temperature`
    });
    if (d.componentKey === 'channel-channel-head') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'UG-32 formed head',
      designPressureFieldTag: `${shellTubePrefix}.tube-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.tube-side.design-temperature`,
      reviewNotes: 'Tube-side design conditions apply for channel/channel-head components.'
    });
    if (d.componentKey === 'channel-cover') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'flat cover / channel cover method placeholder',
      designPressureFieldTag: `${shellTubePrefix}.tube-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.tube-side.design-temperature`
    });
    if (d.componentKey === 'channel-head-dollar-plate') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'flat cover / channel cover method placeholder',
      designPressureFieldTag: `${shellTubePrefix}.tube-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.tube-side.design-temperature`
    });
    if (d.componentKey === 'bonnet-head') return ({
      ...d,
      supportsTminCalculation: true,
      tminCalculationMethod: 'UG-32 formed head',
      designPressureFieldTag: `${shellTubePrefix}.tube-side.design-pressure`,
      designTemperatureFieldTag: `${shellTubePrefix}.tube-side.design-temperature`
    });
    if (d.componentKey === 'tubesheet-area') return ({
      ...d,
      supportsTminCalculation: false,
      tminCalculationMethod: 'review only unless calculation method is selected',
      reviewNotes: 'Shared/interface region; review only unless a specific calculation method is selected.'
    });
  }

  if (d.fieldTagPrefix.startsWith('api510.external.drum-vessel.')) {
    if (d.componentKey === 'shell') return ({ ...d, pressureBoundarySide: 'shared', supportsTminCalculation: true, tminCalculationMethod: 'UG-27 cylindrical shell', designPressureFieldTag: 'api510.external.drum-vessel.design-pressure', designTemperatureFieldTag: 'api510.external.drum-vessel.design-temperature' });
    if (d.componentKey === 'heads') return ({ ...d, pressureBoundarySide: 'shared', supportsTminCalculation: true, tminCalculationMethod: 'UG-32 formed head', designPressureFieldTag: 'api510.external.drum-vessel.design-pressure', designTemperatureFieldTag: 'api510.external.drum-vessel.design-temperature' });
    if (d.componentKey === 'nozzles') return ({ ...d, pressureBoundarySide: 'shared', parentComponentRequired: true, allowedParentComponentKeys: ['shell', 'heads'], nozzleLocationRequired: true, nozzleLocationOptions: ['shell', 'heads'], designConditionSourceOptions: ['vessel-side'], parentThicknessSourceOptions: ['selected-parent', 'manual-entry'], supportsTminCalculation: true, tminCalculationMethod: 'UG-45 nozzle neck minimum thickness', supportsNozzleUg45: true, designPressureFieldTag: 'api510.external.drum-vessel.design-pressure', designTemperatureFieldTag: 'api510.external.drum-vessel.design-temperature' });
    if (['saddles-supports','skirt-legs-supports','supports','manway'].includes(d.componentKey)) return ({ ...d, pressureBoundarySide: 'shared', supportsTminCalculation: false, tminCalculationMethod: 'review only for first-pass drum/vessel workflow', reviewNotes: 'Review-only component for first-pass API 510 external drum/vessel workflow.' });
    return ({ ...d, pressureBoundarySide: 'shared', designPressureFieldTag: 'api510.external.drum-vessel.design-pressure', designTemperatureFieldTag: 'api510.external.drum-vessel.design-temperature' });
  }

  if (d.componentKey !== 'nozzles') return d;
  if (d.equipmentSubtype === 'Shell and Tube Exchanger') return ({
    ...d,
    parentComponentRequired: true,
    allowedParentComponentKeys: shellTubeNozzleParents,
    nozzleLocationRequired: true,
    nozzleLocationOptions: ['shell', 'channel-channel-head', 'bonnet-head', 'tubesheet-area'],
    designConditionSourceOptions: ['shell-side', 'tube-side'],
    parentThicknessSourceOptions: ['selected-parent', 'manual-entry'],
    supportsTminCalculation: true,
    tminCalculationMethod: 'UG-45 nozzle neck minimum thickness',
    supportsNozzleUg45: true,
    designPressureFieldTag: `${shellTubePrefix}.shell-side.design-pressure`,
    designTemperatureFieldTag: `${shellTubePrefix}.shell-side.design-temperature`,
    designPressureFieldTagsByPressureBoundarySide: {
      'shell-side': `${shellTubePrefix}.shell-side.design-pressure`,
      'tube-side': `${shellTubePrefix}.tube-side.design-pressure`
    },
    designTemperatureFieldTagsByPressureBoundarySide: {
      'shell-side': `${shellTubePrefix}.shell-side.design-temperature`,
      'tube-side': `${shellTubePrefix}.tube-side.design-temperature`
    },
    reviewNotes: 'Nozzle can be assigned to shell-side/tube-side parent. Channel / Channel Head is treated as tube-side for design pressure and temperature. UG-45 metadata required.'
  });
  if (d.fieldTagPrefix.startsWith('api510.external.drum-vessel.')) return ({ ...d, parentComponentRequired: true, allowedParentComponentKeys: ['shell', 'heads'], nozzleLocationRequired: true, nozzleLocationOptions: ['shell', 'heads'], designConditionSourceOptions: ['vessel-side'], designPressureFieldTag: 'api510.external.drum-vessel.design-pressure', designTemperatureFieldTag: 'api510.external.drum-vessel.design-temperature' });
  if (d.fieldTagPrefix.startsWith('api510.external.tower-column.')) return ({ ...d, parentComponentRequired: true, allowedParentComponentKeys: ['shell-courses', 'heads'], nozzleLocationRequired: true, nozzleLocationOptions: ['shell-courses', 'heads'], designConditionSourceOptions: ['tower-side'], designPressureFieldTag: 'api510.external.tower-column.design-pressure', designTemperatureFieldTag: 'api510.external.tower-column.design-temperature' });
  if (d.equipmentSubtype === 'Air Cooler / Fin Fan') return ({ ...d, parentComponentRequired: true, allowedParentComponentKeys: ['header-box'], nozzleLocationRequired: true, nozzleLocationOptions: ['header-box'], designConditionSourceOptions: ['tube-side', 'header-box'] });
  if (d.equipmentSubtype === 'Double Pipe Exchanger') return ({ ...d, parentComponentRequired: true, allowedParentComponentKeys: ['inner-pipe-external', 'outer-pipe', 'return-bends'], nozzleLocationRequired: true, nozzleLocationOptions: ['inner-pipe-external', 'outer-pipe', 'return-bends'], designConditionSourceOptions: ['inner-pipe-side', 'annulus-side'] });
  if (d.equipmentSubtype === 'Plate and Frame Exchanger') return ({ ...d, parentComponentRequired: true, allowedParentComponentKeys: ['frame-head', 'pressure-plate', 'plate-pack-external'], nozzleLocationRequired: true, nozzleLocationOptions: ['frame-head', 'pressure-plate', 'plate-pack-external'], designConditionSourceOptions: ['hot-side', 'cold-side', 'shared'] });
  return d;
});

export const API510_EXCHANGER_DESIGN_CONDITION_FIELDS = [
  'api510.external.exchanger.shell-tube.shell-side.design-pressure','api510.external.exchanger.shell-tube.shell-side.design-temperature','api510.external.exchanger.shell-tube.tube-side.design-pressure','api510.external.exchanger.shell-tube.tube-side.design-temperature',
  'api510.external.exchanger.plate-frame.hot-side.design-pressure','api510.external.exchanger.plate-frame.hot-side.design-temperature','api510.external.exchanger.plate-frame.cold-side.design-pressure','api510.external.exchanger.plate-frame.cold-side.design-temperature',
  'api510.external.exchanger.double-pipe.inner-pipe-side.design-pressure','api510.external.exchanger.double-pipe.inner-pipe-side.design-temperature','api510.external.exchanger.double-pipe.annulus-side.design-pressure','api510.external.exchanger.double-pipe.annulus-side.design-temperature',
  'api510.external.exchanger.air-cooler.tube-side.design-pressure','api510.external.exchanger.air-cooler.tube-side.design-temperature','api510.external.exchanger.air-cooler.header-box.design-pressure','api510.external.exchanger.air-cooler.header-box.design-temperature'
] as const;

export const API510_VESSEL_TOWER_DESIGN_CONDITION_FIELDS = [
  'api510.external.drum-vessel.design-pressure','api510.external.drum-vessel.design-temperature',
  'api510.external.tower-column.design-pressure','api510.external.tower-column.design-temperature'
] as const;
