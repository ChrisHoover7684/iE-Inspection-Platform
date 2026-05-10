import { useState } from 'react';
import { calculateB31_3PipingWallThickness } from './engineering/calculations/b31_3Piping';

const defaults = {
  designPressurePsi: 285,
  outsideDiameterIn: 8.625,
  allowableStressPsi: 20000,
  weldJointQualityFactor: 1,
  coefficientY: 0.4,
  mechanicalAllowanceIn: 0,
  corrosionAllowanceIn: 0.125,
  millToleranceFraction: 0.125,
  providedThicknessIn: 0.322,
  standardEdition: 'Owner-selected edition required'
};

export function B31_3PipingCalculatorPage() {
  const [input, setInput] = useState(defaults);
  const result = calculateB31_3PipingWallThickness(input);

  return <div className="page-shell">
    <h2>ASME B31.3 Piping Calculator</h2>
    <p className="muted">Foundation tool only. Do not treat as final code compliance without owner/spec reviewer validation.</p>
    <div className="card stack">
      {Object.entries(input).map(([key, value]) => <label key={key}>{key}
        <input type={typeof value === 'number' ? 'number' : 'text'} value={value as string | number} onChange={(e) => setInput({ ...input, [key]: typeof value === 'number' ? Number(e.target.value) : e.target.value })} />
      </label>)}
    </div>
    <div className="card stack">
      <div><strong>Pressure Design Thickness:</strong> {result.outputs.pressureDesignThicknessIn.toFixed(4)} in</div>
      <div><strong>Required + Allowances:</strong> {result.outputs.requiredThicknessWithAllowancesIn.toFixed(4)} in</div>
      <div><strong>Required Nominal Thickness:</strong> {result.outputs.requiredNominalThicknessIn?.toFixed(4) ?? 'N/A'} in</div>
      <div><strong>Margin:</strong> {result.outputs.thicknessMarginIn.toFixed(4)} in</div>
      <div><strong>Insert Label:</strong> {result.insertLabel}</div>
      {result.warnings.length > 0 && <ul>{result.warnings.map((w) => <li key={w.code}>[{w.severity.toUpperCase()}] {w.message}</li>)}</ul>}
    </div>
  </div>;
}
