import type { AppRole } from './roleNavigation';

const DEFAULT_ROLE: AppRole = 'admin';
const allowedRoles: AppRole[] = [
  'client',
  'owner',
  'inspector',
  'nde_coordinator',
  'admin',
  'viewer',
  'internal_system_owner',
];

export function normalizeRole(value: string | undefined): AppRole {
  const trimmedValue = value?.trim();

  if (!trimmedValue) {
    return DEFAULT_ROLE;
  }

  const normalized = trimmedValue.toLowerCase();
  return allowedRoles.includes(normalized as AppRole) ? (normalized as AppRole) : DEFAULT_ROLE;
}

// Temporary local-development role resolver.
// This is UI convenience only and is NOT security enforcement.
export function getCurrentUserRole(): AppRole {
  return normalizeRole(import.meta.env.VITE_DEV_ROLE as string | undefined);
}

export function getCurrentUserRoles(): AppRole[] {
  return [getCurrentUserRole()];
}
