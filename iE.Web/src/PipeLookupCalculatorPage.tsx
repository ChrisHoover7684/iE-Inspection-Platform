import { useEffect, useState } from 'react';
import { getApiBaseUrl } from './api';

type PipeLookupResult = {
  nps: string;
  schedule: string;
  outsideDiameterInches: number;
  nominalWallThicknessInches: number;
  insideDiameterInches: number;
};

export function PipeLookupCalculatorPage() {
  const [nps, setNps] = useState('1/2"');
  const [schedule, setSchedule] = useState('40');
  const [result, setResult] = useState<PipeLookupResult | null>(null);

  useEffect(() => {
    const run = async () => {
      const response = await fetch(`${getApiBaseUrl()}/api/calculators/pipe-lookup/lookup?nps=${encodeURIComponent(nps)}&schedule=${encodeURIComponent(schedule)}`);
      if (!response.ok) return;
      setResult((await response.json()) as PipeLookupResult);
    };
    void run();
  }, [nps, schedule]);

  return <div className="page-shell"><h1>Pipe Lookup</h1><section><h2>Inputs</h2>
    <label>NPS <select value={nps} onChange={(e) => setNps(e.target.value)}>{['1/2"','3/4"','1"','1.5"','2"','3"','4"','6"','8"','10"','12"'].map((v) => <option key={v}>{v}</option>)}</select></label>
    <label>Schedule <select value={schedule} onChange={(e) => setSchedule(e.target.value)}>{['10','20','40','80','160'].map((v) => <option key={v}>{v}</option>)}</select></label>
  </section>
  <section><h2>Results</h2>{result && <>
    <p>Outside Diameter (OD): {result.outsideDiameterInches.toFixed(3)} in</p>
    <p>Nominal Wall Thickness: {result.nominalWallThicknessInches.toFixed(3)} in</p>
    <p>Inside Diameter: {result.insideDiameterInches.toFixed(3)} in</p>
  </>}</section></div>;
}
