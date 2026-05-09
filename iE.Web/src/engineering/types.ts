export type EngineeringCalculationWarning = {
  code: string;
  message: string;
};

export type EngineeringCalculationResult<TInputs, TOutputs> = {
  calculationType: string;
  formulaVersion: string;
  inputs: TInputs;
  outputs: TOutputs;
  warnings: EngineeringCalculationWarning[];
};
