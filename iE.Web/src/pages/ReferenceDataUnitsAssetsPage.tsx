import { useMemo, useState } from 'react';
import { defaultUnits, getUnitOptions, resetUnitsAndAssets, saveUnitOptions, type UnitReferenceOption } from '../ndeRequestReferenceData';

const makeId = (name: string) => name.toLowerCase().trim().replace(/[^a-z0-9]+/g, '-').replace(/(^-|-$)/g, '');

export function ReferenceDataUnitsAssetsPage() {
  const [units, setUnits] = useState<UnitReferenceOption[]>(() => getUnitOptions());
  const [selectedUnitId, setSelectedUnitId] = useState('');
  const [newUnit, setNewUnit] = useState('');
  const [newAsset, setNewAsset] = useState('');
  const selectedUnit = units.find((u) => u.id === selectedUnitId) ?? units[0];
  const sortedUnits = useMemo(() => [...units].sort((a, b) => a.name.localeCompare(b.name)), [units]);
  const update = (next: UnitReferenceOption[]) => { setUnits(next); saveUnitOptions(next); };

  return <section className="nde-workspace"><div className="card"><h2>Reference Data / Units & Assets</h2><p className="muted">Unit and asset options are demo reference data. Admin-managed setup will be connected later.</p><p>Facility: <strong>Demo Facility</strong></p></div>
  <div className="card"><label>New Unit<input value={newUnit} onChange={(e) => setNewUnit(e.target.value)} /></label><button onClick={() => { const n = newUnit.trim(); if (!n) return; update([...units, { id: makeId(n), name: n, isActive: true, assets: [] }]); setNewUnit(''); }}>Add Unit</button><button onClick={() => update(resetUnitsAndAssets())}>Reset to Demo Defaults</button></div>
  <div className="card" role="region" aria-label="Unit reference data"><h3>Units</h3>{sortedUnits.map((u) => <div key={u.id} className="row"><button onClick={() => setSelectedUnitId(u.id)}>{u.name}</button><input aria-label={`Unit ${u.name}`} value={u.name} onChange={(e) => update(units.map((x) => x.id === u.id ? { ...x, name: e.target.value } : x))} /><button onClick={() => update(units.map((x) => x.id === u.id ? { ...x, isActive: !x.isActive } : x))}>{u.isActive ? 'Deactivate' : 'Reactivate'}</button></div>)}</div>
  <div className="card" role="region" aria-label="Asset reference data"><h3>Assets{selectedUnit ? ` — ${selectedUnit.name}` : ''}</h3>{selectedUnit && <><label>New Asset<input value={newAsset} onChange={(e) => setNewAsset(e.target.value)} /></label><button onClick={() => { const n = newAsset.trim(); if (!n) return; update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: [...u.assets, { id: makeId(n), name: n, isActive: true }] } : u)); setNewAsset(''); }}>Add Asset</button>{selectedUnit.assets.map((a) => <div className="row" key={a.id}><input aria-label={`Asset ${a.name}`} value={a.name} onChange={(e) => update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: u.assets.map((x) => x.id === a.id ? { ...x, name: e.target.value } : x) } : u))} /><button onClick={() => update(units.map((u) => u.id === selectedUnit.id ? { ...u, assets: u.assets.map((x) => x.id === a.id ? { ...x, isActive: !x.isActive } : x) } : u))}>{a.isActive ? 'Deactivate' : 'Reactivate'}</button></div>)}</>}</div>
  <p className="muted">Default demo units loaded: {defaultUnits.length}</p></section>;
}
