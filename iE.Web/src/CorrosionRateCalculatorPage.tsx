import { useEffect, useState } from 'react';
import { ApiError, getApiBaseUrl } from './api';

type CorrosionResult = {
  metalLossInches: number;
  yearsBetweenReadings: number;
  corrosionRateInchesPerYear: number;
  corrosionRateMpy: number;
  remainingLifeYears?: number | null;
  warnings: string[];
};

export function CorrosionRateCalculatorPage() {
  const [previousThicknessInches, setPreviousThicknessInches] = useState('0.280');
  const [currentThicknessInches, setCurrentThicknessInches] = useState('0.250');
  const [previousDate, setPreviousDate] = useState('2020-01-01');
  const [currentDate, setCurrentDate] = useState('2025-01-01');
  const [requiredThicknessTminInches, setRequiredThicknessTminInches] = useState('0.180');
  const [result, setResult] = useState<CorrosionResult | null>(null);
  const [error, setError] = useState('');

  useEffect(() => {
    const run = async () => {
      setError('');
      try {
        const response = await fetch(`${getApiBaseUrl()}/api/calculators/corrosion-rate/calculate`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            previousThicknessInches: Number(previousThicknessInches),
            currentThicknessInches: Number(currentThicknessInches),
            previousDate,
            currentDate,
            requiredThicknessTminInches: requiredThicknessTminInches ? Number(requiredThicknessTminInches) : null
          })
        });
        if (!response.ok) {
          const payload = await response.json();
          throw new ApiError(payload.error || 'Failed to calculate corrosion rate.');
        }
        setResult((await response.json()) as CorrosionResult);
      } catch (err) {
        setResult(null);
        setError(err instanceof Error ? err.message : 'Unable to calculate.');
      }
    };

    void run();
  }, [previousThicknessInches, currentThicknessInches, previousDate, currentDate, requiredThicknessTminInches]);

  return <div className="page-shell"><h1>Corrosion Rate Calculator</h1><section><h2>Inputs</h2>
    <label>Previous Thickness (in) <input value={previousThicknessInches} onChange={(e) => setPreviousThicknessInches(e.target.value)} /></label>
    <label>Current Thickness (in) <input value={currentThicknessInches} onChange={(e) => setCurrentThicknessInches(e.target.value)} /></label>
    <label>Previous Date <input type="date" value={previousDate} onChange={(e) => setPreviousDate(e.target.value)} /></label>
    <label>Current Date <input type="date" value={currentDate} onChange={(e) => setCurrentDate(e.target.value)} /></label>
    <label>Required Thickness Tmin (in, optional) <input value={requiredThicknessTminInches} onChange={(e) => setRequiredThicknessTminInches(e.target.value)} /></label>
  </section>
  <section><h2>Results</h2>{error && <p className="error">{error}</p>}
  {result && <>
    {result.warnings?.map((warning) => <p className="warning" key={warning}>{warning}</p>)}
    <p>Metal Loss: {result.metalLossInches.toFixed(3)} in</p>
    <p>Years Between Readings: {result.yearsBetweenReadings.toFixed(2)}</p>
    <p>Corrosion Rate: {result.corrosionRateInchesPerYear.toFixed(6)} in/yr</p>
    <p>Corrosion Rate: {result.corrosionRateMpy.toFixed(2)} MPY</p>
    <p>Remaining Life: {result.remainingLifeYears == null ? 'N/A' : `${result.remainingLifeYears.toFixed(2)} years`}</p>
  </>}
  </section></div>;
}
