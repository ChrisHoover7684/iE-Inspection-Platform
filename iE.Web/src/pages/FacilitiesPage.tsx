import { useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { getOrganizationAccount, getFacilities, resetFacilities, addFacility, setFacilityActiveStatus, updateFacility, type FacilityReferenceOption } from '../organizationReferenceData';
import { getUnitOptions } from '../ndeRequestReferenceData';

export function FacilitiesPage() {
  const organization = getOrganizationAccount();
  const [facilities, setFacilities] = useState(() => getFacilities());
  const [newCode, setNewCode] = useState('');
  const [newName, setNewName] = useState('');
  const [message, setMessage] = useState<string | null>(null);
  const [editId, setEditId] = useState<string>('');
  const [editCode, setEditCode] = useState('');
  const [editName, setEditName] = useState('');

  const units = useMemo(() => getUnitOptions(), [facilities]);
  const activeCount = facilities.filter((f) => f.isActive).length;
  const inactiveCount = facilities.length - activeCount;

  const getAssetCount = (facilityId: string) => units.filter((u) => u.facilityId === facilityId).reduce((total, u) => total + u.assets.length, 0);

  const startEdit = (facility: FacilityReferenceOption) => {
    setEditId(facility.id);
    setEditCode(facility.code);
    setEditName(facility.name);
    setMessage(null);
  };

  const refreshFacilities = () => setFacilities(getFacilities());

  const handleAdd = () => {
    const code = newCode.trim().toUpperCase();
    const name = newName.trim();
    if (!code || !name) {
      setMessage('Facility Code and Facility Name are required.');
      return;
    }
    if (facilities.some((f) => f.code.trim().toUpperCase() === code)) {
      setMessage('A facility with this code already exists.');
      return;
    }
    addFacility({ code, name });
    setNewCode('');
    setNewName('');
    setMessage('Facility added (demo/localStorage).');
    refreshFacilities();
  };

  const handleSaveEdit = () => {
    const code = editCode.trim().toUpperCase();
    const name = editName.trim();
    if (!code || !name) {
      setMessage('Facility Code and Facility Name are required.');
      return;
    }
    if (facilities.some((f) => f.id !== editId && f.code.trim().toUpperCase() === code)) {
      setMessage('A facility with this code already exists.');
      return;
    }
    updateFacility(editId, { code, name });
    setEditId('');
    setMessage('Facility updated (demo/localStorage).');
    refreshFacilities();
  };

  return <section className="nde-workspace reference-data-layout">
    <div className="card">
      <h2>Facilities</h2>
      <p><strong>Organization:</strong> {organization.name}</p>
      <p className="muted">Facility setup is demo/local until backend persistence is connected.</p>
      <p><strong>Active Facilities:</strong> {activeCount} &nbsp; <strong>Inactive Facilities:</strong> {inactiveCount}</p>
    </div>

    <div className="card">
      <h3>Add Facility</h3>
      <div className="nde-modal-grid">
        <label>Facility Code<input aria-label="Facility Code" value={newCode} onChange={(event) => setNewCode(event.target.value)} /></label>
        <label>Facility Name<input aria-label="Facility Name" value={newName} onChange={(event) => setNewName(event.target.value)} /></label>
      </div>
      <div className="row">
        <button type="button" onClick={handleAdd}>Add Facility</button>
        <button type="button" onClick={() => { resetFacilities(); refreshFacilities(); setMessage('Facilities reset to demo defaults.'); }}>Reset to Demo Defaults</button>
      </div>
      {message && <p className="muted">{message}</p>}
    </div>

    <div className="card">
      <table className="reference-table">
        <thead><tr><th>Facility Code</th><th>Facility Name</th><th>Status</th><th>Units</th><th>Assets</th><th>Actions</th></tr></thead>
        <tbody>
          {facilities.map((facility) => {
            const facilityUnits = units.filter((u) => u.facilityId === facility.id).length;
            return <tr key={facility.id}>
              <td>{facility.code}</td>
              <td>{facility.name}</td>
              <td>{facility.isActive ? 'Active' : 'Inactive'}</td>
              <td>{facilityUnits}</td>
              <td>{getAssetCount(facility.id)}</td>
              <td>
                <div className="row">
                  <button type="button" onClick={() => startEdit(facility)}>Edit</button>
                  <button type="button" onClick={() => { setFacilityActiveStatus(facility.id, !facility.isActive); refreshFacilities(); }}>
                    {facility.isActive ? 'Deactivate' : 'Reactivate'}
                  </button>
                  <Link to={`/reference-data-units-assets?facilityId=${facility.id}`}>Manage Units &amp; Assets</Link>
                </div>
              </td>
            </tr>;
          })}
        </tbody>
      </table>
    </div>

    {editId && <div className="card">
      <h3>Edit Facility</h3>
      <div className="nde-modal-grid">
        <label>Facility Code<input value={editCode} onChange={(event) => setEditCode(event.target.value)} /></label>
        <label>Facility Name<input value={editName} onChange={(event) => setEditName(event.target.value)} /></label>
      </div>
      <div className="row">
        <button type="button" onClick={handleSaveEdit}>Save Facility</button>
        <button type="button" onClick={() => setEditId('')}>Cancel</button>
      </div>
    </div>}
  </section>;
}
