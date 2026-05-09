import { describe, expect, it } from 'vitest';
import { getNavigationForRole } from '../roleNavigation';

const labelsFor = (role: Parameters<typeof getNavigationForRole>[0]) =>
  getNavigationForRole(role).flatMap((group) => group.items.map((item) => item.label));

describe('getNavigationForRole', () => {
  it('client excludes admin/internal items', () => {
    const labels = labelsFor('client');

    expect(labels).not.toContain('Admin');
    expect(labels).not.toContain('Users & Access');
    expect(labels).not.toContain('Audit Log');
    expect(labels).not.toContain('Stress Test Setup');
    expect(labels).not.toContain('Tenants');
  });

  it('admin includes key administrative items', () => {
    const labels = labelsFor('admin');

    expect(labels).toEqual(expect.arrayContaining(['Users & Access', 'Facilities', 'Custom NDE Types', 'Audit Log', 'Settings']));
  });

  it('owner includes Approval Queue', () => {
    expect(labelsFor('owner')).toContain('Approval Queue');
  });

  it('nde coordinator includes operational queue items', () => {
    expect(labelsFor('nde_coordinator')).toEqual(
      expect.arrayContaining(['NDE Requests', 'Schedule', 'Results Received', 'Overdue', 'Cancelled']),
    );
  });

  it('viewer includes read-only items and excludes internal/admin items', () => {
    const labels = labelsFor('viewer');

    expect(labels).toEqual(expect.arrayContaining(['API Inspection Reports', 'Assets', 'Downloads']));
    expect(labels).not.toEqual(expect.arrayContaining(['Users & Access', 'Audit Log', 'Settings', 'NDE Requests']));
  });

  it('internal system owner includes internal readiness items', () => {
    expect(labelsFor('internal_system_owner')).toEqual(
      expect.arrayContaining(['System Readiness', 'Stress Test Setup', 'Tenants']),
    );
  });
});
