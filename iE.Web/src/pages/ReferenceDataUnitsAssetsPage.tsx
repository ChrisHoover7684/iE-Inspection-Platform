import { useMemo, useState } from 'react';
import { getFacilities } from '../organizationReferenceData';
import {
  defaultUnits,
  formatUnitValue,
  getUnitOptions,
  parseUnitName,
  parseUnitNumber,
  resetUnitsAndAssets,
  saveUnitOptions,
  type UnitReferenceOption,
} from '../ndeRequestReferenceData';

const makeId = (name: string) => name.toLowerCase().trim().replace(/[^a-z0-9]+/g, '-').replace(/(^-|-$)/g, '');

export function ReferenceDataUnitsAssetsPage() {
  const facilities = getFacilities().filter((f) => f.isActive);
  const [selectedFacilityId, setSelectedFacilityId] = useState(facilities[0]?.id ?? '');
  const [units, setUnits] = useState<UnitReferenceOption[]>(() => getUnitOptions());
  const [selectedUnitId, setSelectedUnitId] = useState('');
  const [newUnitNumber, setNewUnitNumber] = useState('');
  const [newUnitName, setNewUnitName] = useState('');
  const [newAsset, setNewAsset] = useState('');
  const [editUnitId, setEditUnitId] = useState('');
  const [editUnitNumber, setEditUnitNumber] = useState('');
  const [editUnitName, setEditUnitName] = useState('');

  const scopedUnits = useMemo(() => units.filter((u) => u.facilityId === selectedFacilityId), [units, selectedFacilityId]);
  const selectedUnit = scopedUnits.find((u) => u.id === selectedUnitId) ?? scopedUnits[0];
  const sortedUnits = useMemo(() => [...scopedUnits].sort((a, b) => a.name.localeCompare(b.name)), [scopedUnits]);
  const update = (next: UnitReferenceOption[]) => { setUnits(next); saveUnitOptions(next); };

  const addUnit = () => {
    const unitNumber = newUnitNumber.trim();
    const unitName = newUnitName.trim().toUpperCase();
    if (!unitNumber || !unitName) return;
    if (units.some((u) => parseUnitNumber(u.name) === unitNumber)) return;
    const value = formatUnitValue(unitNumber, unitName);
    update([...units, { id: makeId(`${selectedFacilityId}-${value}`), facilityId: selectedFacilityId, name: value, isActive: true, assets: [] }]);
    setNewUnitNumber('');
    setNewUnitName('');
  };

  const saveEditedUnit = () => {
    const unitNumber = editUnitNumber.trim();
    const unitName = editUnitName.trim().toUpperCase();
    if (!editUnitId || !unitNumber || !unitName) return;
    if (units.some((u) => u.id !== editUnitId && parseUnitNumber(u.name) === unitNumber)) return;
    const value = formatUnitValue(unitNumber, unitName);
    update(units.map((u) => (u.id === editUnitId ? { ...u, name: value } : u)));
    setEditUnitId('');
  };

  const selectedFacility = facilities.find((f) => f.id === selectedFacilityId);

  return <section className="nde-workspace reference-data-layout"><div className="card"><h2>Reference Data / Units & Assets</h2><p className="muted">Unit and asset options are demo reference data. Admin-managed setup will be connected later.</p><label>Facility<select aria-label="Facility" value={selectedFacilityId} onChange={(e) => { setSelectedFacilityId(e.target.value); setSelectedUnitId(''); }}>{facilities.map((f) => <option key={f.id} value={f.id}>{f.name} ({f.code})</option>)}</select></label><p>Facility: <strong>{selectedFacility?.name ?? '—'}</strong></p></div>
    <div className="card"><div className="reference-actions"><label>Unit #<input aria-label="Unit #" value={newUnitNumber} onChange={(e) => setNewUnitNumber(e.target.value)} /></label><label>Unit Name<input aria-label="Unit Name" value={newUnitName} onChange={(e) => setNewUnitName(e.target.value)} /></label><button onClick={addUnit}>Add Unit</button><button onClick={() => update(resetUnitsAndAssets())}>Reset to Demo Defaults</button></div></div>
    <div className="reference-data-grid">
      <div className="card" role="region" aria-label="Unit reference data"><h3>Units</h3><table className="reference-table"><thead><tr><th>Unit #</th><th>Unit Name</th><th>Status</th><th>Actions</th></tr></thead><tbody>{sortedUnits.map((u) => <tr key={u.id} className={selectedUnit?.id === u.id ? 'selected-reference-row' : ''}><td>{parseUnitNumber(u.name)}</td><td>{parseUnitName(u.name)}</td><td>{u.isActive ? 'Active' : 'Inactive'}</td><td><div className="reference-actions"><button onClick={() => setSelectedUnitId(u.id)}>Select</button><button onClick={() => { setEditUnitId(u.id); setEditUnitNumber(parseUnitNumber(u.name)); setEditUnitName(parseUnitName(u.name)); }}>Edit</button><button onClick={() => update(units.map((x) => x.id === u.id ? { ...x, isActive: !x.isActive } : x))}>{u.isActive ? 'Deactivate' : 'Reactivate'}</button></div></td></tr>)}</tbody></table>
        {editUnitId && <div className="reference-actions"><label>Edit Unit #<input value={editUnitNumber} onChange={(e) => setEditUnitNumber(e.target.value)} /></label><label>Edit Unit Name<input value={editUnitName} onChange={(e) => setEditUnitName(e.target.value)} /></label><button onClick={saveEditedUnit}>Save Unit</button><button onClick={() => setEditUnitId('')}>Cancel</button></div>}
      </div>
      <div className="card" role="region" aria-label="Asset reference data"><h3>Assets{selectedUnit ? ` — ${selectedUnit.name}` : ''}</h3>{selectedUnit && <><div className="reference-actions"><button disabled title="Excel asset import will be connected later.">Import Assets from Excel</button><span className="muted">Excel asset import will be connected later.</span></div><div className="reference-actions"><label>New Asset Tag<input aria-label="New Asset Tag" value={newAsset} onChange={(e) => setNewAsset(e.target.value)} /></label><button onClick={() => { const n = newAsset.trim().toUpperCase(); if (!n) return; if (selectedUnit.assets.some((a) => a.name.toUpperCase() === n)) return; update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: [...u.assets, { id: makeId(n), name: n, isActive: true }] } : u)); setNewAsset(''); }}>Add Asset</button></div><table className="reference-table"><thead><tr><th>Asset Tag</th><th>Status</th><th>Actions</th></tr></thead><tbody>{selectedUnit.assets.map((a) => <tr key={a.id}><td>{a.name}</td><td>{a.isActive ? 'Active' : 'Inactive'}</td><td><div className="reference-actions"><button onClick={() => { const next = prompt('Edit asset tag', a.name); const n = next?.trim().toUpperCase(); if (!n) return; if (selectedUnit.assets.some((x) => x.id !== a.id && x.name.toUpperCase() === n)) return; update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: u.assets.map((x) => x.id === a.id ? { ...x, name: n } : x) } : u)); }}>Edit</button><button onClick={() => update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: u.assets.map((x) => x.id === a.id ? { ...x, isActive: !x.isActive } : x) } : u))}>{a.isActive ? 'Deactivate' : 'Reactivate'}</button></div></td></tr>)}</tbody></table></>}</div>
    </div>
    <p className="muted">Default demo units loaded: {defaultUnits.length}</p></section>;
}
