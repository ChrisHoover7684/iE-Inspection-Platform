export type AppRole =
  | 'client'
  | 'owner'
  | 'inspector'
  | 'nde_coordinator'
  | 'admin'
  | 'viewer'
  | 'internal_system_owner';

export type NavigationGroupKey =
  | 'dashboard'
  | 'work'
  | 'reports'
  | 'assets'
  | 'logs'
  | 'admin'
  | 'settings';

export type NavigationItem = {
  key: string;
  label: string;
  route: string;
  group: NavigationGroupKey;
};

export type NavigationGroup = {
  key: NavigationGroupKey;
  label: string;
  items: NavigationItem[];
};

export const allNavigationItems: NavigationItem[] = [
  { key: 'dashboard', label: 'Dashboard', route: '/dashboard', group: 'dashboard' },
  { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests', group: 'work' },
  { key: 'approvalQueue', label: 'Approval Queue', route: '/approval-queue', group: 'work' },
  { key: 'schedule', label: 'Schedule', route: '/schedule', group: 'work' },
  { key: 'resultsReceived', label: 'Results Received', route: '/results-received', group: 'work' },
  { key: 'overdue', label: 'Overdue', route: '/overdue', group: 'work' },
  { key: 'cancelled', label: 'Cancelled', route: '/cancelled', group: 'work' },
  { key: 'myReports', label: 'My Reports', route: '/my-reports', group: 'reports' },
  { key: 'returnedReports', label: 'Returned Reports', route: '/returned-reports', group: 'reports' },
  { key: 'ndeReports', label: 'NDE Reports', route: '/nde-reports', group: 'reports' },
  { key: 'reports', label: 'Reports', route: '/reports', group: 'reports' },
  { key: 'exports', label: 'Exports', route: '/exports', group: 'reports' },
  { key: 'downloads', label: 'Downloads', route: '/downloads', group: 'reports' },
  { key: 'assets', label: 'Assets', route: '/assets', group: 'assets' },
  { key: 'apiInspectionLog', label: 'API Inspection Log', route: '/api-inspection-log', group: 'logs' },
  { key: 'auditLog', label: 'Audit Log', route: '/audit-log', group: 'logs' },
  { key: 'usersAccess', label: 'Users & Access', route: '/users-access', group: 'admin' },
  { key: 'facilities', label: 'Facilities', route: '/facilities', group: 'admin' },
  { key: 'customNdeTypes', label: 'Custom NDE Types', route: '/custom-nde-types', group: 'admin' },
  { key: 'systemReadiness', label: 'System Readiness', route: '/system-readiness', group: 'admin' },
  { key: 'stressTestSetup', label: 'Stress Test Setup', route: '/stress-test-setup', group: 'admin' },
  { key: 'tenants', label: 'Tenants', route: '/tenants', group: 'admin' },
  { key: 'settings', label: 'Settings', route: '/settings', group: 'settings' },
  { key: 'help', label: 'Help', route: '/help', group: 'settings' },
];

const groupLabels: Record<NavigationGroupKey, string> = {
  dashboard: 'Dashboard',
  work: 'Work',
  reports: 'Reports',
  assets: 'Assets',
  logs: 'Logs',
  admin: 'Admin',
  settings: 'Settings',
};

const roleItemKeys: Record<AppRole, string[]> = {
  client: ['dashboard', 'ndeRequests', 'reports', 'downloads', 'assets', 'help'],
  owner: ['dashboard', 'approvalQueue', 'ndeRequests', 'ndeReports', 'apiInspectionLog', 'assets', 'reports', 'exports'],
  inspector: ['dashboard', 'myReports', 'apiInspectionLog', 'ndeRequests', 'assets', 'returnedReports'],
  nde_coordinator: ['dashboard', 'ndeRequests', 'schedule', 'resultsReceived', 'overdue', 'cancelled', 'assets'],
  admin: ['dashboard', 'usersAccess', 'facilities', 'assets', 'customNdeTypes', 'reports', 'ndeRequests', 'auditLog', 'settings'],
  viewer: ['dashboard', 'reports', 'assets', 'downloads'],
  internal_system_owner: ['dashboard', 'systemReadiness', 'stressTestSetup', 'tenants', 'auditLog', 'settings'],
};

export function getNavigationForRole(role: AppRole): NavigationGroup[] {
  const allowedKeys = new Set(roleItemKeys[role]);
  const visibleItems = allNavigationItems.filter((item) => allowedKeys.has(item.key));

  return (Object.keys(groupLabels) as NavigationGroupKey[])
    .map((groupKey) => ({
      key: groupKey,
      label: groupLabels[groupKey],
      items: visibleItems.filter((item) => item.group === groupKey),
    }))
    .filter((group) => group.items.length > 0);
}


export const navigationRoutes = allNavigationItems.map((item) => item.route);
