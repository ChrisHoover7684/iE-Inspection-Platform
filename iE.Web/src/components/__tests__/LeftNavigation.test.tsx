import { fireEvent, render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it, vi } from 'vitest';
import { LeftNavigation } from '../LeftNavigation';
import { getNavigationForRole } from '../../navigation/roleNavigation';

describe('LeftNavigation', () => {
  const groups = getNavigationForRole('admin');

  it('renders navigation with aria label and group headings', () => {
    render(
      <MemoryRouter initialEntries={['/users-access']}>
        <LeftNavigation groups={groups} isCollapsed={false} onToggleCollapsed={vi.fn()} />
      </MemoryRouter>,
    );

    expect(screen.getByRole('navigation', { name: 'Primary' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Dashboard' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Admin' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Collapse navigation' })).toBeInTheDocument();
  });

  it('renders allowed nav items and marks active route', () => {
    render(
      <MemoryRouter initialEntries={['/users-access']}>
        <LeftNavigation groups={groups} isCollapsed={false} onToggleCollapsed={vi.fn()} />
      </MemoryRouter>,
    );

    const activeLink = screen.getByRole('link', { name: 'Users & Access' });
    expect(activeLink).toBeInTheDocument();
    expect(activeLink).toHaveClass('active');
  });

  it('applies collapsed class and toggles via button', () => {
    const onToggle = vi.fn();
    render(
      <MemoryRouter>
        <LeftNavigation groups={groups} isCollapsed onToggleCollapsed={onToggle} />
      </MemoryRouter>,
    );

    const nav = screen.getByRole('navigation', { name: 'Primary' });
    expect(nav).toHaveClass('collapsed');

    fireEvent.click(screen.getByRole('button', { name: 'Expand navigation' }));
    expect(onToggle).toHaveBeenCalledTimes(1);
  });
});
