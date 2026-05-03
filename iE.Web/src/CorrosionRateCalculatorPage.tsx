import { useState } from 'react';
import { corrosionRateApi } from './api';
import type { CorrosionRateInput, CorrosionRateResult } from './types';

const defaultInput: CorrosionRateInput = {
  initialThicknessInches: 1,
  finalThicknessInches: 0.9,
  useDates: false,
  exposureTimeYears: 1,
  initialDate: null,
  finalDate: null,
  inspectionFactor: 0.5,
  currentThicknessInches: 0.9,
  tminInches: 0.5
};

export function CorrosionRateCalculatorPage() {
  const [input, setInput] = useState<CorrosionRateInput>(defaultInput);
  const [result, setResult] = useState<CorrosionRateResult | null>(null);
  const [error, setError] = useState<string | null>(null);

  const calculate = async () => {
    setError(null);
    try {
      const response = await corrosionRateApi.calculate(input);
      setResult(response);
    } catch (e) {
      setResult(null);
      setError(e instanceof Error ? e.message : 'Calculation failed');
    }
  };

  return <div className="page-shell">
    <h2>Corrosion Rate Calculator</h2>
    <div className="card stack">
      <label>Initial Thickness (in)<input type="number" value={input.initialThicknessInches} onChange={(e) => setInput({ ...input, initialThicknessInches: Number(e.target.value) })} /></label>
      <label>Final Thickness (in)<input type="number" value={input.finalThicknessInches} onChange={(e) => setInput({ ...input, finalThicknessInches: Number(e.target.value) })} /></label>
      <label>Exposure Time (years)<input type="number" value={input.exposureTimeYears ?? ''} onChange={(e) => setInput({ ...input, exposureTimeYears: Number(e.target.value), useDates: false })} /></label>
      <label>Current Thickness (in)<input type="number" value={input.currentThicknessInches} onChange={(e) => setInput({ ...input, currentThicknessInches: Number(e.target.value) })} /></label>
      <label>Tmin (in)<input type="number" value={input.tminInches} onChange={(e) => setInput({ ...input, tminInches: Number(e.target.value) })} /></label>
      <label>Inspection Factor<input type="number" value={input.inspectionFactor} onChange={(e) => setInput({ ...input, inspectionFactor: Number(e.target.value) })} /></label>
      <button onClick={() => void calculate()}>Calculate</button>
    </div>
    {error && <div className="alert error">{error}</div>}
    {result && <div className="card stack">
      <div>Corrosion Rate (in/yr): {result.corrosionRateInchesPerYear}</div>
      <div>Remaining Life (years): {result.remainingLifeYears ?? 'N/A'}</div>
      <div>Next Inspection (years): {result.nextInspectionYears ?? 'N/A'}</div>
      <div>Next Inspection Date: {result.nextInspectionDate ? new Date(result.nextInspectionDate).toLocaleDateString() : 'N/A'}</div>
      {result.warnings.length > 0 && <div className="alert warning">
        <strong>Warnings</strong>
        <ul>{result.warnings.map((w) => <li key={w}>{w}</li>)}</ul>
      </div>}
    </div>}
  </div>;
}
