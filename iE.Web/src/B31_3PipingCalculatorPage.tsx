import { useState } from 'react';
import { ApiError, b313Api } from './api';
import { toB31_3EngineeringSnapshot } from './engineering/calculations/b31_3Piping';
import type { B313ThicknessInput } from './types';

const defaults: B313ThicknessInput = {
  pressurePsi: 285,
  temperatureF: 100,
  outsideDiameterIn: 8.625,
  spec: 'A106',
  grade: 'B',
  productForm: 'Pipe',
  unsNo: '',
  classConditionTemper: '',
  materialCategory: 'Carbon Steel',
  jointType: 'Seamless',
  jointQualityKey: 'Seamless',
  wFactor: 1,
  yOverride: null,
  eOverride: null,
};

export function B31_3PipingCalculatorPage() {
  const [input, setInput] = useState<B313ThicknessInput>(defaults);
  const [snapshot, setSnapshot] = useState<ReturnType<typeof toB31_3EngineeringSnapshot> | null>(null);
  const [error, setError] = useState<string | null>(null);

  const calculate = async () => {
    setError(null);
    try {
      const apiResult = await b313Api.calculateThickness(input);
      setSnapshot(toB31_3EngineeringSnapshot(input, apiResult));
    } catch (e) {
      setSnapshot(null);
      setError(e instanceof ApiError ? e.message : e instanceof Error ? e.message : 'B31.3 calculation failed');
    }
  };

  return <div className="page-shell">
    <h2>ASME B31.3 Piping Calculator</h2>
    <p className="muted">Uses backend B31.3 engine/material stress source of truth. Reviewer validation still required.</p>
    <div className="card stack">
      {Object.entries(input).map(([key, value]) => <label key={key}>{key}
        <input type={typeof value === 'number' ? 'number' : 'text'} value={(value ?? '') as string | number} onChange={(e) => {
          const nextValue = typeof value === 'number' || value === null
            ? (e.target.value === '' ? null : Number(e.target.value))
            : e.target.value;
          setInput({ ...input, [key]: nextValue } as B313ThicknessInput);
        }} />
      </label>)}
      <button type="button" onClick={() => void calculate()}>Calculate</button>
    </div>

    {error && <div className="alert error">{error}</div>}

    {snapshot && <div className="card stack">
      <div><strong>Required Thickness:</strong> {snapshot.outputs.requiredThicknessIn?.toFixed(4) ?? 'N/A'} in</div>
      <div><strong>Allowable Stress:</strong> {snapshot.outputs.allowableStressPsi?.toFixed(2) ?? 'N/A'} psi</div>
      <div><strong>E:</strong> {snapshot.outputs.eFactor ?? 'N/A'}</div>
      <div><strong>Y:</strong> {snapshot.outputs.yCoefficient ?? 'N/A'}</div>
      <div><strong>W:</strong> {snapshot.outputs.wFactor ?? 'N/A'}</div>
      <div><strong>Insert Label:</strong> {snapshot.insertLabel}</div>
      {snapshot.warnings.length > 0 && <ul>{snapshot.warnings.map((w) => <li key={w.code}>[{w.severity.toUpperCase()}] {w.message}</li>)}</ul>}
    </div>}
  </div>;
}
