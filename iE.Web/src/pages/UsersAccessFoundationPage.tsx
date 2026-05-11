import { getFacilities, getFacilityAccessAssignments, getOrganizationAccount, getOrganizationUsers, getUsedSeatCount } from '../organizationReferenceData';

export function UsersAccessFoundationPage() {
  const organization = getOrganizationAccount();
  const facilities = getFacilities();
  const users = getOrganizationUsers();
  const access = getFacilityAccessAssignments();
  const usedSeats = getUsedSeatCount();

  return (
    <section className="nde-workspace reference-data-layout">
      <div className="card">
        <h2>Users & Access Foundation</h2>
        <p className="muted">Demo/localStorage foundation for organization seats, facilities, and facility authority assignments.</p>
        <p><strong>Organization:</strong> {organization.name}</p>
        <p><strong>Seats used:</strong> {usedSeats} / {organization.seatLimit}</p>
      </div>

      <div className="reference-data-grid">
        <div className="card">
          <h3>Organization Users</h3>
          <table className="reference-table"><thead><tr><th>Name</th><th>Email</th><th>Seat Status</th><th>Org Role</th></tr></thead>
            <tbody>{users.map((u) => <tr key={u.id}><td>{u.name}</td><td>{u.email}</td><td>{u.seatStatus}</td><td>{u.organizationRole}</td></tr>)}</tbody>
          </table>
        </div>
        <div className="card">
          <h3>Facility Access Roles</h3>
          <table className="reference-table"><thead><tr><th>User</th><th>Facility</th><th>Role</th><th>Status</th></tr></thead>
            <tbody>{access.map((a, idx) => {
              const user = users.find((u) => u.id === a.userId);
              const facility = facilities.find((f) => f.id === a.facilityId);
              return <tr key={`${a.userId}-${a.facilityId}-${idx}`}><td>{user?.name ?? a.userId}</td><td>{facility?.name ?? a.facilityId}</td><td>{a.role}</td><td>{a.isActive ? 'Active' : 'Inactive'}</td></tr>;
            })}</tbody>
          </table>
        </div>
      </div>
    </section>
  );
}
