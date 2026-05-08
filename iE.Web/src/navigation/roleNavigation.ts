export type AppRole =
  | 'client'
  | 'owner'
  | 'inspector'
  | 'ndeCoordinator'
  | 'admin'
  | 'viewer'
  | 'internalSystemOwner';

export type NavigationItem = {
  key: string;
  label: string;
  route: string;
};

export const roleNavigation: Record<AppRole, NavigationItem[]> = {
  client: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests' },
    { key: 'reports', label: 'Reports', route: '/reports' },
    { key: 'downloads', label: 'Downloads', route: '/downloads' },
    { key: 'assets', label: 'Assets', route: '/assets' },
    { key: 'help', label: 'Help', route: '/help' },
  ],
  owner: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'approvalQueue', label: 'Approval Queue', route: '/approval-queue' },
    { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests' },
    { key: 'ndeReports', label: 'NDE Reports', route: '/nde-reports' },
    { key: 'apiInspectionLog', label: 'API Inspection Log', route: '/api-inspection-log' },
    { key: 'assets', label: 'Assets', route: '/assets' },
    { key: 'reports', label: 'Reports', route: '/reports' },
    { key: 'exports', label: 'Exports', route: '/exports' },
  ],
  inspector: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'myReports', label: 'My Reports', route: '/my-reports' },
    { key: 'apiInspectionLog', label: 'API Inspection Log', route: '/api-inspection-log' },
    { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests' },
    { key: 'assets', label: 'Assets', route: '/assets' },
    { key: 'returnedReports', label: 'Returned Reports', route: '/returned-reports' },
  ],
  ndeCoordinator: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests' },
    { key: 'schedule', label: 'Schedule', route: '/schedule' },
    { key: 'resultsReceived', label: 'Results Received', route: '/results-received' },
    { key: 'overdue', label: 'Overdue', route: '/overdue' },
    { key: 'cancelled', label: 'Cancelled', route: '/cancelled' },
    { key: 'assets', label: 'Assets', route: '/assets' },
  ],
  admin: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'usersAccess', label: 'Users & Access', route: '/users-access' },
    { key: 'facilities', label: 'Facilities', route: '/facilities' },
    { key: 'assets', label: 'Assets', route: '/assets' },
    { key: 'customNdeTypes', label: 'Custom NDE Types', route: '/custom-nde-types' },
    { key: 'reports', label: 'Reports', route: '/reports' },
    { key: 'ndeRequests', label: 'NDE Requests', route: '/nde-requests' },
    { key: 'auditLog', label: 'Audit Log', route: '/audit-log' },
    { key: 'settings', label: 'Settings', route: '/settings' },
  ],
  viewer: [
    { key: 'dashboard', label: 'Dashboard', route: '/dashboard' },
    { key: 'reports', label: 'Reports', route: '/reports' },
    { key: 'assets', label: 'Assets', route: '/assets' },
    { key: 'downloads', label: 'Downloads', route: '/downloads' },
  ],
  internalSystemOwner: [
    { key: 'internalDashboard', label: 'Internal Dashboard', route: '/internal-dashboard' },
    { key: 'readiness', label: 'Readiness', route: '/readiness' },
    { key: 'operationsDocs', label: 'Operations Docs', route: '/operations-docs' },
    { key: 'stressTestTooling', label: 'Stress-Test Tooling', route: '/stress-test-tooling' },
  ],
};
