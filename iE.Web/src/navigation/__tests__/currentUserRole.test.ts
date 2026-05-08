import { describe, expect, it } from 'vitest';
import { normalizeRole } from '../currentUserRole';

describe('normalizeRole', () => {
  it('defaults safely when value is missing', () => {
    expect(normalizeRole(undefined)).toBe('admin');
    expect(normalizeRole('')).toBe('admin');
  });

  it('defaults safely when value is invalid', () => {
    expect(normalizeRole('not-a-role')).toBe('admin');
  });

  it('resolves valid role values', () => {
    expect(normalizeRole('viewer')).toBe('viewer');
    expect(normalizeRole(' NDE_COORDINATOR ')).toBe('nde_coordinator');
  });
});
