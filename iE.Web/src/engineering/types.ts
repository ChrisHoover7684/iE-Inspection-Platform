export type EngineeringCalculationWarning = {
  code: string;
  message: string;
  severity: 'info' | 'warning' | 'error';
};

export type EngineeringCalculationReference = {
  standard: string;
  edition?: string;
  paragraph?: string;
  note?: string;
};

export type EngineeringCalculationResult<TInputs, TOutputs> = {
  id: string;
  calculationType: string;
  displayName: string;
  formulaVersion: string;
  standardReferences: EngineeringCalculationReference[];
  inputs: TInputs;
  outputs: TOutputs;
  warnings: EngineeringCalculationWarning[];
  calculatedAt: string;
  insertLabel: string;
};
