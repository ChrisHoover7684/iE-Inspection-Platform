import { useState } from 'react';

type Tab = 'shells' | 'heads' | 'nozzles';

export function PressureVesselCalculatorPage() {
  const [tab, setTab] = useState<Tab>('shells');
  return (
    <div style={{ padding: 16 }}>
      <h1>Pressure Vessel Calculator</h1>
      <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>
        <button onClick={() => setTab('shells')}>Shells</button>
        <button onClick={() => setTab('heads')}>Heads</button>
        <button onClick={() => setTab('nozzles')}>Nozzles / UG-45</button>
      </div>
      {tab === 'shells' && <div>Shell thickness calculators (cylindrical, spherical, conical).</div>}
      {tab === 'heads' && <div>Head thickness calculator with geometry-specific inputs.</div>}
      {tab === 'nozzles' && <div>Nozzle minimum thickness and UG-45 workflow.</div>}
    </div>
  );
}
