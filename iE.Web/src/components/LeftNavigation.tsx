import { NavLink } from 'react-router-dom';
import type { NavigationGroup } from '../navigation/roleNavigation';

type LeftNavigationProps = {
  groups: NavigationGroup[];
};

export function LeftNavigation({ groups }: LeftNavigationProps) {
  return (
    <nav className="left-nav" aria-label="Primary">
      {groups.map((group) => (
        <section key={group.key} className="left-nav-group" aria-labelledby={`group-${group.key}`}>
          <h2 id={`group-${group.key}`} className="left-nav-heading">
            {group.label}
          </h2>
          <ul className="left-nav-list">
            {group.items.map((item) => (
              <li key={item.key}>
                <NavLink to={item.route} className={({ isActive }) => `left-nav-link${isActive ? ' active' : ''}`}>
                  {item.label}
                </NavLink>
              </li>
            ))}
          </ul>
        </section>
      ))}
    </nav>
  );
}
