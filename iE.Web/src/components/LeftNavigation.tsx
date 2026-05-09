import { NavLink } from 'react-router-dom';
import type { NavigationGroup } from '../navigation/roleNavigation';

type LeftNavigationProps = {
  groups: NavigationGroup[];
  isCollapsed: boolean;
  onToggleCollapsed: () => void;
};

export function LeftNavigation({ groups, isCollapsed, onToggleCollapsed }: LeftNavigationProps) {
  return (
    <nav className={`left-nav${isCollapsed ? ' collapsed' : ''}`} aria-label="Primary">
      <button
        type="button"
        className="left-nav-toggle"
        aria-label={isCollapsed ? 'Expand navigation' : 'Collapse navigation'}
        onClick={onToggleCollapsed}
      >
        {isCollapsed ? '»' : '«'}
      </button>

      {groups.map((group) => (
        <section key={group.key} className="left-nav-group" aria-labelledby={`group-${group.key}`}>
          <h2 id={`group-${group.key}`} className="left-nav-heading" aria-label={group.label}>
            {isCollapsed ? group.label.charAt(0) : group.label}
          </h2>
          <ul className="left-nav-list">
            {group.items.map((item) => (
              <li key={item.key}>
                <NavLink
                  to={item.route}
                  aria-label={item.label}
                  title={item.label}
                  className={({ isActive }) => `left-nav-link${isActive ? ' active' : ''}`}
                >
                  {isCollapsed ? item.label.split(' ').map((part) => part[0]).join('').slice(0, 3).toUpperCase() : item.label}
                </NavLink>
              </li>
            ))}
          </ul>
        </section>
      ))}
    </nav>
  );
}
