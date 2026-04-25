using System;
using System.Collections.Generic;

namespace iE.Core.Mechanical.ThermalExpansion
{
    public class ThermalExpansionService
    {
        private readonly Dictionary<string, double> _materialCTE = new()
        {
            { "Carbon Steel", 6.5e-6 },
            { "Stainless Steel", 9.0e-6 },
            { "Copper", 9.3e-6 },
            { "PVC", 30.0e-6 }
        };

        public ThermalExpansionResult Calculate(ThermalExpansionInput input)
        {
            Validate(input);

            double cte = _materialCTE[input.Material];
            double deltaTF = input.IsCelsiusDeltaT ? input.DeltaT * 1.8 : input.DeltaT;
            double expansion = cte * input.LengthInches * deltaTF;

            return new ThermalExpansionResult
            {
                Material = input.Material,
                CoefficientInPerInF = cte,
                DeltaTFahrenheit = Math.Round(deltaTF, 2),
                LengthInches = Math.Round(input.LengthInches, 2),
                ExpansionInches = Math.Round(expansion, 4),
                Display =
                    $"Material: {input.Material}\n" +
                    $"ΔT: {deltaTF:0.##}°F\n" +
                    $"Length: {input.LengthInches:0.##} in\n" +
                    $"Expansion (ΔL): {expansion:0.0000} in"
            };
        }

        private void Validate(ThermalExpansionInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Material))
                throw new Exception("Material is required.");

            if (!_materialCTE.ContainsKey(input.Material))
                throw new Exception($"Material '{input.Material}' was not found.");

            if (input.LengthInches <= 0)
                throw new Exception("Length must be > 0.");

            if (input.DeltaT == 0)
                throw new Exception("Delta T must not be zero.");
        }
    }
}