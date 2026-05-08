import type { AppRole } from './roleNavigation';

const DEFAULT_ROLE: AppRole = 'admin';

// Temporary local-development role resolver.
// This is UI convenience only and is NOT security enforcement.
export function getCurrentUserRole(): AppRole {
  const value = (import.meta.env.VITE_DEV_ROLE as string | undefined)?.trim();

  if (!value) {
    return DEFAULT_ROLE;
  }

  const normalized = value.toLowerCase();
  const allowedRoles: AppRole[] = [
    'client',
    'owner',
    'inspector',
    'nde_coordinator',
    'admin',
    'viewer',
    'internal_system_owner',
  ];

  return allowedRoles.includes(normalized as AppRole) ? (normalized as AppRole) : DEFAULT_ROLE;
}

export function getCurrentUserRoles(): AppRole[] {
  return [getCurrentUserRole()];
}
