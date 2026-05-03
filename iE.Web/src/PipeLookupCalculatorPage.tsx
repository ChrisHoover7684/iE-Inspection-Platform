import { useEffect, useState } from 'react';
import { pipeLookupApi } from './api';
import type { PipeLookupResult } from './types';

export function PipeLookupCalculatorPage() {
  const [npsOptions, setNpsOptions] = useState<string[]>([]);
  const [scheduleOptions, setScheduleOptions] = useState<string[]>([]);
  const [nps, setNps] = useState('0.5');
  const [schedule, setSchedule] = useState('40');
  const [result, setResult] = useState<PipeLookupResult | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    void pipeLookupApi.getNps().then((values) => {
      const filtered = values.filter((v) => Number(v) >= 0.5 && Number(v) <= 12);
      setNpsOptions(filtered);
      if (filtered.length > 0) setNps(filtered[0]);
    });
  }, []);

  useEffect(() => {
    if (!nps) return;
    void pipeLookupApi.getSchedules(nps).then((values) => {
      const allowed = ['10', '20', '40', '80', '160', 'STD', 'XS', 'XXS', '5S', '10S', '40S', '80S'];
      const filtered = values.filter((v) => allowed.includes(v));
      setScheduleOptions(filtered);
      if (filtered.length > 0) setSchedule(filtered[0]);
    });
  }, [nps]);

  const lookup = async () => {
    setError(null);
    try {
      setResult(await pipeLookupApi.lookup({ nps, schedule }));
    } catch (e) {
      setResult(null);
      setError(e instanceof Error ? e.message : 'Lookup failed');
    }
  };

  return <div className="page-shell">
    <h2>Pipe Lookup Calculator</h2>
    <div className="card stack">
      <label>NPS
        <select value={nps} onChange={(e) => setNps(e.target.value)}>
          {npsOptions.map((value) => <option key={value} value={value}>{value}"</option>)}
        </select>
      </label>
      <label>Schedule
        <select value={schedule} onChange={(e) => setSchedule(e.target.value)}>
          {scheduleOptions.map((value) => <option key={value} value={value}>{value}</option>)}
        </select>
      </label>
      <button onClick={() => void lookup()}>Lookup</button>
    </div>
    {error && <div className="alert error">{error}</div>}
    {result && <div className="card stack">
      <div><strong>NPS:</strong> {result.nps}"</div>
      <div><strong>Schedule:</strong> {result.schedule}</div>
      <div><strong>Outside Diameter (OD):</strong> {result.outsideDiameter} in</div>
      <div><strong>Nominal Wall Thickness:</strong> {result.nominalThickness} in</div>
      <div><strong>Inside Diameter (calculated):</strong> {result.insideDiameter} in</div>
      <div><strong>Mil Tolerance Lower (-12.5%):</strong> {result.lowerLimitMinus12_5} in</div>
      <div><strong>Mil Tolerance Upper (+12.5%):</strong> {result.upperLimitPlus12_5} in</div>
    </div>}
  </div>;
}
