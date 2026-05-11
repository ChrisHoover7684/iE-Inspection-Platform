export type OrganizationAccount = {
  id: string;
  name: string;
  seatLimit: number;
};

export type FacilityReferenceOption = {
  id: string;
  name: string;
  code: string;
  isActive: boolean;
};

export type OrganizationUser = {
  id: string;
  name: string;
  email: string;
  seatStatus: 'Active' | 'Invited' | 'Suspended';
  organizationRole: 'Organization Admin' | 'Member';
};

export type FacilityAuthorityRole =
  | 'Facility Admin'
  | 'Inspection Manager'
  | 'Inspector'
  | 'NDE Coordinator'
  | 'NDE Requester'
  | 'Report Reviewer'
  | 'Viewer';

export type FacilityAccessAssignment = {
  userId: string;
  facilityId: string;
  unitName?: string;
  role: FacilityAuthorityRole;
  isActive: boolean;
};

export const defaultOrganizationAccount: OrganizationAccount = {
  id: 'org-demo-inspection',
  name: 'Demo Inspection Services',
  seatLimit: 25,
};

export const defaultFacilities: FacilityReferenceOption[] = [
  { id: 'fac-demo-main', name: 'Demo Facility', code: 'DEMO', isActive: true },
  { id: 'fac-west-terminal', name: 'West Terminal', code: 'WEST', isActive: true },
  { id: 'fac-north-plant', name: 'North Plant', code: 'NORTH', isActive: true },
];

export const defaultOrganizationUsers: OrganizationUser[] = [
  { id: 'user-1', name: 'Jordan Rivera', email: 'jordan.rivera@example.test', seatStatus: 'Active', organizationRole: 'Organization Admin' },
  { id: 'user-2', name: 'Mina Patel', email: 'mina.patel@example.test', seatStatus: 'Active', organizationRole: 'Member' },
  { id: 'user-3', name: 'Leslie Tran', email: 'leslie.tran@example.test', seatStatus: 'Invited', organizationRole: 'Member' },
  { id: 'user-4', name: 'Noah Brooks', email: 'noah.brooks@example.test', seatStatus: 'Suspended', organizationRole: 'Member' },
];

export const defaultFacilityAccessAssignments: FacilityAccessAssignment[] = [
  { userId: 'user-1', facilityId: 'fac-demo-main', role: 'Facility Admin', isActive: true },
  { userId: 'user-1', facilityId: 'fac-west-terminal', role: 'Inspection Manager', isActive: true },
  { userId: 'user-2', facilityId: 'fac-demo-main', role: 'NDE Coordinator', isActive: true },
  { userId: 'user-2', facilityId: 'fac-west-terminal', unitName: '02-VAC', role: 'Inspector', isActive: true },
  { userId: 'user-3', facilityId: 'fac-north-plant', role: 'NDE Requester', isActive: true },
  { userId: 'user-4', facilityId: 'fac-demo-main', role: 'Viewer', isActive: false },
];

const orgStorageKey = 'ie.org.account';
const facilityStorageKey = 'ie.org.facilities';
const usersStorageKey = 'ie.org.users';
const accessStorageKey = 'ie.org.facilityAccess';

const hasStorage = () => typeof window !== 'undefined' && !!window.localStorage;

function readFromStorage<T>(key: string, fallback: T): T {
  try {
    if (!hasStorage()) return fallback;
    const saved = window.localStorage.getItem(key);
    return saved ? (JSON.parse(saved) as T) : fallback;
  } catch {
    return fallback;
  }
}

function writeToStorage<T>(key: string, value: T): void {
  try {
    if (hasStorage()) window.localStorage.setItem(key, JSON.stringify(value));
  } catch {
    // demo-only localStorage foundation.
  }
}

export function getOrganizationAccount(): OrganizationAccount {
  return readFromStorage(orgStorageKey, defaultOrganizationAccount);
}
export function getFacilities(): FacilityReferenceOption[] {
  return readFromStorage(facilityStorageKey, defaultFacilities);
}
export function getOrganizationUsers(): OrganizationUser[] {
  return readFromStorage(usersStorageKey, defaultOrganizationUsers);
}
export function getFacilityAccessAssignments(): FacilityAccessAssignment[] {
  return readFromStorage(accessStorageKey, defaultFacilityAccessAssignments);
}

export function saveOrganizationAccount(account: OrganizationAccount): void { writeToStorage(orgStorageKey, account); }
export function saveFacilities(facilities: FacilityReferenceOption[]): void { writeToStorage(facilityStorageKey, facilities); }
export function saveOrganizationUsers(users: OrganizationUser[]): void { writeToStorage(usersStorageKey, users); }
export function saveFacilityAccessAssignments(assignments: FacilityAccessAssignment[]): void { writeToStorage(accessStorageKey, assignments); }



const makeFacilityId = (code: string) => `fac-${code.toLowerCase().replace(/[^a-z0-9]+/g, '-')}`;

export function resetFacilities(): FacilityReferenceOption[] {
  saveFacilities(defaultFacilities);
  return defaultFacilities;
}

export function getActiveFacilities(): FacilityReferenceOption[] {
  return getFacilities().filter((facility) => facility.isActive);
}

export function addFacility(input: { code: string; name: string }): FacilityReferenceOption {
  const code = input.code.trim().toUpperCase();
  const name = input.name.trim();
  const nextFacility: FacilityReferenceOption = { id: makeFacilityId(code), code, name, isActive: true };
  const facilities = getFacilities();
  saveFacilities([...facilities, nextFacility]);
  return nextFacility;
}

export function updateFacility(facilityId: string, updates: { code: string; name: string }): FacilityReferenceOption[] {
  const code = updates.code.trim().toUpperCase();
  const name = updates.name.trim();
  const next = getFacilities().map((facility) => facility.id === facilityId ? { ...facility, code, name } : facility);
  saveFacilities(next);
  return next;
}

export function setFacilityActiveStatus(facilityId: string, isActive: boolean): FacilityReferenceOption[] {
  const next = getFacilities().map((facility) => facility.id === facilityId ? { ...facility, isActive } : facility);
  saveFacilities(next);
  return next;
}

export function getUsedSeatCount(): number {
  return getOrganizationUsers().filter((user) => user.seatStatus === 'Active' || user.seatStatus === 'Invited').length;
}
