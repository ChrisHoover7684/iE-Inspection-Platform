import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe, expect, it } from 'vitest';
import App from '../App';
import { allNavigationRoutes } from '../navigation/roleNavigation';

const nonPlaceholderRoutes = new Set(['/dashboard', '/calculators/corrosion-rate']);

describe('App routes', () => {
  it('every navigation route resolves without redirecting to dashboard', () => {
    for (const route of allNavigationRoutes) {
      render(
        <MemoryRouter initialEntries={[route]}>
          <App />
        </MemoryRouter>,
      );

      if (nonPlaceholderRoutes.has(route)) {
        expect(screen.queryByText('Coming soon')).not.toBeInTheDocument();
      } else {
        expect(screen.getByText(/Coming soon:/)).toBeInTheDocument();
      }
    }
  });
});
