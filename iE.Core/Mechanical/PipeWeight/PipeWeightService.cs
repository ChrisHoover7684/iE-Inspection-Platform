using System;

namespace iE.Core.Mechanical.PipeWeight
{
    public class PipeWeightService
    {
        public PipeWeightResult Calculate(PipeWeightInput input)
        {
            Validate(input);

            if (input.IsMetric)
            {
                // Convert mm → meters
                double odM = input.OuterDiameter / 1000.0;
                double wtM = input.WallThickness / 1000.0;
                double idM = odM - (2 * wtM);

                double area = Math.PI * (Math.Pow(odM, 2) - Math.Pow(idM, 2)) / 4;
                double volume = area * input.Length;

                double weightKg = volume * input.Density * 1000; // g/cm³ → kg/m³
                double weightLb = weightKg * 2.20462;

                return new PipeWeightResult
                {
                    WeightKg = weightKg,
                    WeightLb = weightLb,
                    Display = $"{weightKg:0.00} kg ({weightLb:0.00} lbs)"
                };
            }
            else
            {
                double id = input.OuterDiameter - 2 * input.WallThickness;

                double area = Math.PI * (Math.Pow(input.OuterDiameter, 2) - Math.Pow(id, 2)) / 4;
                double volume = area * (input.Length * 12); // ft → inches

                double weightLb = volume * input.Density;
                double weightKg = weightLb / 2.20462;

                return new PipeWeightResult
                {
                    WeightLb = weightLb,
                    WeightKg = weightKg,
                    Display = $"{weightLb:0.00} lbs ({weightKg:0.00} kg)"
                };
            }
        }

        private void Validate(PipeWeightInput input)
        {
            if (input.OuterDiameter <= 0)
                throw new Exception("Outer diameter must be > 0");

            if (input.WallThickness <= 0)
                throw new Exception("Wall thickness must be > 0");

            if (input.Length <= 0)
                throw new Exception("Length must be > 0");

            if (input.WallThickness >= input.OuterDiameter / 2)
                throw new Exception("Wall thickness must be less than half of OD");
        }
    }
}