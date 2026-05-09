import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it } from 'vitest';
import { DashboardPage } from '../DashboardPage';

describe('DashboardPage (Home)', () => {
  it('renders the three main product sections', () => {
    render(
      <MemoryRouter>
        <DashboardPage />
      </MemoryRouter>,
    );

    expect(screen.getByRole('heading', { name: 'Engineering Tools' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'API Inspection Reports' })).toBeInTheDocument();
    expect(screen.getByRole('heading', { name: 'NDE Log / Reports' })).toBeInTheDocument();
  });
});
