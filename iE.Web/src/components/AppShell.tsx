import { useMemo, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { getCurrentUserRole } from '../navigation/currentUserRole';
import { getNavigationForRole, getNavigationItemForRoute } from '../navigation/roleNavigation';
import { LeftNavigation } from './LeftNavigation';
import { TopBar } from './TopBar';

type AppShellProps = {
  children: React.ReactNode;
};

export function AppShell({ children }: AppShellProps) {
  const role = getCurrentUserRole();
  const groups = useMemo(() => getNavigationForRole(role), [role]);
  const location = useLocation();
  const [isNavCollapsed, setIsNavCollapsed] = useState(false);
  const isApi570ReportRoute = location.pathname === '/reports/api-570-piping-external';

  const currentItem = getNavigationItemForRoute(location.pathname);

  return (
    <div className="app-shell">
      <TopBar />
      <div className="app-shell-body">
        <LeftNavigation
          groups={groups}
          isCollapsed={isNavCollapsed}
          onToggleCollapsed={() => setIsNavCollapsed((prev) => !prev)}
        />
        <main className="app-shell-main">
          {!isApi570ReportRoute && (
            <header className="page-header">
              <h1>{currentItem?.label ?? 'Coming soon'}</h1>
              <p className="muted">Role-based visibility in navigation is a UI convenience only.</p>
            </header>
          )}
          {children}
        </main>
      </div>
    </div>
  );
}
