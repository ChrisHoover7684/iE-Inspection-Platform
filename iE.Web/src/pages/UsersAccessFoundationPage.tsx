import { useMemo, useState } from 'react';
import {
  getFacilities,
  getFacilityAccessAssignments,
  getOrganizationAccount,
  getOrganizationUsers,
  getUsedSeatCount,
  saveFacilityAccessAssignments,
  saveOrganizationUsers,
  type FacilityAuthorityRole,
  type OrganizationUser,
} from '../organizationReferenceData';

const authorityRoleOptions: FacilityAuthorityRole[] = ['Facility Admin', 'Inspection Manager', 'Inspector', 'NDE Coordinator', 'NDE Requester', 'Report Reviewer', 'Viewer'];
const makeId = () => `user-${Math.random().toString(36).slice(2, 9)}`;

export function UsersAccessFoundationPage() {
  const organization = getOrganizationAccount();
  const facilities = getFacilities();
  const [users, setUsers] = useState(() => getOrganizationUsers());
  const [access, setAccess] = useState(() => getFacilityAccessAssignments());
  const usedSeats = useMemo(() => users.filter((user) => user.seatStatus === 'Active' || user.seatStatus === 'Invited').length, [users]);

  const [newUser, setNewUser] = useState({ name: '', email: '', organizationRole: 'Member' as OrganizationUser['organizationRole'] });
  const [newAccess, setNewAccess] = useState({ userId: '', facilityId: '', unitName: 'All Units', role: 'Viewer' as FacilityAuthorityRole });

  const persistUsers = (next: OrganizationUser[]) => { setUsers(next); saveOrganizationUsers(next); };
  const persistAccess = (next: typeof access) => { setAccess(next); saveFacilityAccessAssignments(next); };

  const addUser = () => {
    if (!newUser.name.trim() || !newUser.email.trim()) return;
    persistUsers([...users, { id: makeId(), name: newUser.name.trim(), email: newUser.email.trim(), organizationRole: newUser.organizationRole, seatStatus: 'Invited' }]);
    setNewUser({ name: '', email: '', organizationRole: 'Member' });
  };

  return (
    <section className="nde-workspace reference-data-layout">
      <div className="card">
        <h2>Users & Access Foundation</h2>
        <p className="muted">Demo/localStorage foundation for organization seats, facilities, and facility + unit authority assignments.</p>
        <p><strong>Organization:</strong> {organization.name}</p>
        <p><strong>Seats used:</strong> {usedSeats} / {organization.seatLimit}</p>
      </div>

      <div className="card">
        <h3>Add User</h3>
        <div className="nde-modal-grid">
          <label>Name<input value={newUser.name} onChange={(event) => setNewUser((current) => ({ ...current, name: event.target.value }))} /></label>
          <label>Email<input value={newUser.email} onChange={(event) => setNewUser((current) => ({ ...current, email: event.target.value }))} /></label>
          <label>Organization Role<select value={newUser.organizationRole} onChange={(event) => setNewUser((current) => ({ ...current, organizationRole: event.target.value as OrganizationUser['organizationRole'] }))}><option>Member</option><option>Organization Admin</option></select></label>
        </div>
        <button type="button" onClick={addUser}>Add User</button>
      </div>

      <div className="reference-data-grid">
        <div className="card">
          <h3>Organization Users</h3>
          <table className="reference-table"><thead><tr><th>Name</th><th>Email</th><th>Seat Status</th><th>Org Role</th><th>Actions</th></tr></thead>
            <tbody>{users.map((u) => <tr key={u.id}><td>{u.name}</td><td>{u.email}</td><td>{u.seatStatus}</td><td>{u.organizationRole}</td><td><div className="row"><button type="button" onClick={() => persistUsers(users.map((user) => user.id === u.id ? { ...user, seatStatus: user.seatStatus === 'Suspended' ? 'Active' : 'Suspended' } : user))}>{u.seatStatus === 'Suspended' ? 'Reactivate' : 'Suspend'}</button><button type="button" onClick={() => persistUsers(users.map((user) => user.id === u.id ? { ...user, organizationRole: user.organizationRole === 'Member' ? 'Organization Admin' : 'Member' } : user))}>Toggle Role</button></div></td></tr>)}</tbody>
          </table>
        </div>
        <div className="card">
          <h3>Facility Access Roles</h3>
          <div className="nde-modal-grid">
            <label>User<select value={newAccess.userId} onChange={(event) => setNewAccess((current) => ({ ...current, userId: event.target.value }))}><option value="">Select user</option>{users.map((u) => <option key={u.id} value={u.id}>{u.name}</option>)}</select></label>
            <label>Facility<select value={newAccess.facilityId} onChange={(event) => setNewAccess((current) => ({ ...current, facilityId: event.target.value }))}><option value="">Select facility</option>{facilities.map((f) => <option key={f.id} value={f.id}>{f.name}</option>)}</select></label>
            <label>Unit<select value={newAccess.unitName} onChange={(event) => setNewAccess((current) => ({ ...current, unitName: event.target.value }))}><option>All Units</option><option>01-CRUDE</option><option>02-VAC</option><option>03-FCC</option></select></label>
            <label>Role<select value={newAccess.role} onChange={(event) => setNewAccess((current) => ({ ...current, role: event.target.value as FacilityAuthorityRole }))}>{authorityRoleOptions.map((role) => <option key={role}>{role}</option>)}</select></label>
          </div>
          <button type="button" onClick={() => {
            if (!newAccess.userId || !newAccess.facilityId) return;
            persistAccess([...access, { userId: newAccess.userId, facilityId: newAccess.facilityId, unitName: newAccess.unitName === 'All Units' ? undefined : newAccess.unitName, role: newAccess.role, isActive: true }]);
          }}>Add Access Assignment</button>
          <table className="reference-table"><thead><tr><th>User</th><th>Facility</th><th>Unit</th><th>Role</th><th>Status</th><th>Actions</th></tr></thead>
            <tbody>{access.map((a, idx) => {
              const user = users.find((u) => u.id === a.userId);
              const facility = facilities.find((f) => f.id === a.facilityId);
              return <tr key={`${a.userId}-${a.facilityId}-${a.unitName ?? 'all'}-${idx}`}><td>{user?.name ?? a.userId}</td><td>{facility?.name ?? a.facilityId}</td><td>{a.unitName ?? 'All Units'}</td><td>{a.role}</td><td>{a.isActive ? 'Active' : 'Inactive'}</td><td><div className="row"><button type="button" onClick={() => persistAccess(access.map((assignment, assignmentIndex) => assignmentIndex === idx ? { ...assignment, isActive: !assignment.isActive } : assignment))}>{a.isActive ? 'Deactivate' : 'Activate'}</button><button type="button" onClick={() => persistAccess(access.filter((_, assignmentIndex) => assignmentIndex !== idx))}>Remove</button></div></td></tr>;
            })}</tbody>
          </table>
        </div>
      </div>
    </section>
  );
}
