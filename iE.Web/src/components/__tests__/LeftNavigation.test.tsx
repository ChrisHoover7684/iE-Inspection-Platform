import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it } from 'vitest';
import { LeftNavigation } from '../LeftNavigation';
import { getNavigationForRole } from '../../navigation/roleNavigation';

describe('LeftNavigation', () => {
  const groups = getNavigationForRole('admin');

  it('renders navigation with aria label and group headings', () => {
    render(
      <MemoryRouter initialEntries={['/users-access']}>
        <LeftNavigation groups={groups} />
      </MemoryRouter>,
    );

    expect(screen.getByRole('navigation', { name: 'Primary' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Dashboard' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'Admin' })).toBeInTheDocument();
  });

  it('renders allowed nav items and marks active route', () => {
    render(
      <MemoryRouter initialEntries={['/users-access']}>
        <LeftNavigation groups={groups} />
      </MemoryRouter>,
    );

    const activeLink = screen.getByRole('link', { name: 'Users & Access' });
    expect(activeLink).toBeInTheDocument();
    expect(activeLink).toHaveClass('active');
  });
});
