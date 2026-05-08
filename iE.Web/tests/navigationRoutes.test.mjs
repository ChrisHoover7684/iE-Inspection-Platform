import test from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';

function extractRoutesFromRoleNavigation(source) {
  return [...source.matchAll(/route:\s*'([^']+)'/g)].map((match) => match[1]);
}

function extractExplicitRoutesFromAppRoutes(source) {
  const explicitArrayMatch = source.match(/export const explicitAppRoutes:\s*string\[]\s*=\s*\[([\s\S]*?)\];/);
  if (!explicitArrayMatch) {
    return [];
  }

  return [...explicitArrayMatch[1].matchAll(/'([^']+)'/g)].map((match) => match[1]);
}

test('every role navigation route is explicitly covered by app routes', () => {
  const roleNavigationSource = readFileSync(new URL('../src/navigation/roleNavigation.ts', import.meta.url), 'utf8');
  const appRoutesSource = readFileSync(new URL('../src/navigation/appRoutes.ts', import.meta.url), 'utf8');

  const navigationRoutes = extractRoutesFromRoleNavigation(roleNavigationSource);
  const explicitAppRoutes = extractExplicitRoutesFromAppRoutes(appRoutesSource);

  const missingRoutes = navigationRoutes.filter((route) => !explicitAppRoutes.includes(route));

  assert.equal(missingRoutes.length, 0, `Missing app route coverage for navigation routes: ${missingRoutes.join(', ')}`);
});
