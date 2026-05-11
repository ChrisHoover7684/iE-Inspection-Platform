export type ProjectOption = {
  id: string;
  name: string;
  isActive: boolean;
};

export const defaultProjectOptions: ProjectOption[] = [
  { id: 'demo-turnaround-2026', name: 'Demo Turnaround 2026', isActive: true },
  { id: 'unit-73-maintenance', name: 'Unit 73 Maintenance', isActive: true },
  { id: 'piping-reliability-program', name: 'Piping Reliability Program', isActive: true },
  { id: 'tank-inspection-program', name: 'Tank Inspection Program', isActive: true },
  { id: 'facility-maintenance-program', name: 'Facility Maintenance Program', isActive: true },
  { id: 'other', name: 'Other', isActive: true },
];

const projectStorageKey = 'ie.nde.referenceData.projects';

const isProjectOption = (value: unknown): value is ProjectOption => {
  if (!value || typeof value !== 'object') return false;
  const candidate = value as ProjectOption;
  return typeof candidate.id === 'string'
    && typeof candidate.name === 'string'
    && typeof candidate.isActive === 'boolean';
};

export function getProjectOptions(): ProjectOption[] {
  try {
    if (typeof window === 'undefined' || !window.localStorage) {
      return defaultProjectOptions;
    }

    const saved = window.localStorage.getItem(projectStorageKey);
    if (!saved) {
      return defaultProjectOptions;
    }

    const parsed = JSON.parse(saved);
    if (!Array.isArray(parsed)) {
      return defaultProjectOptions;
    }

    const validOptions = parsed.filter(isProjectOption);
    return validOptions.length > 0 ? validOptions : defaultProjectOptions;
  } catch {
    return defaultProjectOptions;
  }
}

export function saveProjectOptions(projectOptions: ProjectOption[]): void {
  try {
    if (typeof window === 'undefined' || !window.localStorage) {
      return;
    }

    window.localStorage.setItem(projectStorageKey, JSON.stringify(projectOptions));
  } catch {
    // Demo-only persistence best effort.
  }
}

export function resetProjectOptions(): ProjectOption[] {
  saveProjectOptions(defaultProjectOptions);
  return defaultProjectOptions;
}

export const owningGroupOptions = [
  'Inspection',
  'Maintenance',
  'Operations',
  'Construction',
  'Turnaround',
  'Reliability',
  'Engineering',
  'Other',
] as const;

export const ndeMethodOptions = [
  'PT',
  'MT',
  'RT',
  'UT Thickness',
  'PAUT',
  'PMI',
  'VT',
  'ET',
  'MFL',
  'LRUT',
  'Other',
] as const;

export const accessMethodOptions = [
  'Ground',
  'Platform',
  'Ladder',
  'Scaffold',
  'Aerial Lift',
  'Rope Access',
  'Confined Space',
  'Other',
] as const;
