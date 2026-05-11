export type ProjectOption = {
  id: string;
  name: string;
  isActive: boolean;
};

export type AssetReferenceOption = {
  id: string;
  name: string;
  isActive: boolean;
};

export type UnitReferenceOption = {
  id: string;
  name: string;
  isActive: boolean;
  assets: AssetReferenceOption[];
};

export const defaultProjectOptions: ProjectOption[] = [
  { id: 'demo-turnaround-2026', name: 'Demo Turnaround 2026', isActive: true },
  { id: 'unit-73-maintenance', name: 'Unit 73 Maintenance', isActive: true },
  { id: 'piping-reliability-program', name: 'Piping Reliability Program', isActive: true },
  { id: 'tank-inspection-program', name: 'Tank Inspection Program', isActive: true },
  { id: 'facility-maintenance-program', name: 'Facility Maintenance Program', isActive: true },
  { id: 'other', name: 'Other', isActive: true },
];

export const defaultUnits: UnitReferenceOption[] = [
  { id: '01-crude', name: '01-CRUDE', isActive: true, assets: [{ id: 'p-102a', name: 'P-102A', isActive: true }, { id: 'cir-3c-118', name: 'CIR-3C-118', isActive: true }, { id: 'e-1101', name: 'E-1101', isActive: true }, { id: 'v-101', name: 'V-101', isActive: true }] },
  { id: '02-vac', name: '02-VAC', isActive: true, assets: [{ id: 'p-300c', name: 'P-300C', isActive: true }, { id: 'cir-4a-220', name: 'CIR-4A-220', isActive: true }, { id: 'e-4401', name: 'E-4401', isActive: true }, { id: 'hx-22b', name: 'HX-22B', isActive: true }, { id: 'cir-9d-032', name: 'CIR-9D-032', isActive: true }] },
  { id: '03-fcc', name: '03-FCC', isActive: true, assets: [{ id: 'l-5507', name: 'L-5507', isActive: true }, { id: 'e-3301', name: 'E-3301', isActive: true }, { id: 'v-301', name: 'V-301', isActive: true }] },
  { id: '04-hydrotreating', name: '04-HYDROTREATING', isActive: true, assets: [] },
  { id: '05-reformer', name: '05-REFORMER', isActive: true, assets: [] },
  { id: '06-utilities', name: '06-UTILITIES', isActive: true, assets: [{ id: 'psv-91', name: 'PSV-91', isActive: true }, { id: 'p-601a', name: 'P-601A', isActive: true }] },
  { id: '07-tank-farm', name: '07-TANK FARM', isActive: true, assets: [{ id: 'tk-804', name: 'TK-804', isActive: true }, { id: 'tk-805', name: 'TK-805', isActive: true }] },
];

const projectStorageKey = 'ie.nde.referenceData.projects';
const unitsAssetsStorageKey = 'ie.nde.referenceData.unitsAssets';

const isProjectOption = (value: unknown): value is ProjectOption => {
  if (!value || typeof value !== 'object') return false;
  const candidate = value as ProjectOption;
  return typeof candidate.id === 'string' && typeof candidate.name === 'string' && typeof candidate.isActive === 'boolean';
};
const hasStorage = () => typeof window !== 'undefined' && !!window.localStorage;

export function getProjectOptions(): ProjectOption[] { try { const saved = hasStorage() ? window.localStorage.getItem(projectStorageKey) : null; const parsed = saved ? JSON.parse(saved) : null; return Array.isArray(parsed) ? (parsed.filter(isProjectOption) || defaultProjectOptions) : defaultProjectOptions; } catch { return defaultProjectOptions; } }
export function saveProjectOptions(projectOptions: ProjectOption[]): void { try { if (hasStorage()) window.localStorage.setItem(projectStorageKey, JSON.stringify(projectOptions)); } catch {} }
export function resetProjectOptions(): ProjectOption[] { saveProjectOptions(defaultProjectOptions); return defaultProjectOptions; }

export function getUnitOptions(): UnitReferenceOption[] { try { if (!hasStorage()) return defaultUnits; const saved = window.localStorage.getItem(unitsAssetsStorageKey); return saved ? JSON.parse(saved) as UnitReferenceOption[] : defaultUnits; } catch { return defaultUnits; } }
export function saveUnitOptions(unitOptions: UnitReferenceOption[]): void { try { if (hasStorage()) window.localStorage.setItem(unitsAssetsStorageKey, JSON.stringify(unitOptions)); } catch {} }
export function resetUnitOptions(): UnitReferenceOption[] { saveUnitOptions(defaultUnits); return defaultUnits; }
export function getAssetsForUnit(unitName: string): AssetReferenceOption[] { const unit = getUnitOptions().find((u) => u.name === unitName); return unit?.assets.filter((a) => a.isActive) ?? []; }
export function saveAssetsForUnit(unitName: string, assets: AssetReferenceOption[]): void { const next = getUnitOptions().map((u) => u.name === unitName ? { ...u, assets } : u); saveUnitOptions(next); }
export function resetUnitsAndAssets(): UnitReferenceOption[] { return resetUnitOptions(); }

export const owningGroupOptions = ['Inspection','Maintenance','Operations','Construction','Turnaround','Reliability','Engineering','Other'] as const;
export const ndeMethodOptions = ['PT','MT','RT','UT Thickness','PAUT','PMI','VT','ET','MFL','LRUT','Other'] as const;
export const accessMethodOptions = ['Ground','Platform','Ladder','Scaffold','Aerial Lift','Rope Access','Confined Space','Other'] as const;
