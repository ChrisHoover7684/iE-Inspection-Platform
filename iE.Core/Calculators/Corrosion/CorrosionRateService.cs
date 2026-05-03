using System.Text;

namespace iE.Core.Calculators.Corrosion
{
    public class CorrosionRateService
    {
        public CorrosionRateResult Calculate(CorrosionRateInput input)
        {
            Validate(input);

            double exposureTime = ResolveExposureTimeYears(input);
            double thicknessLoss = input.InitialThicknessInches - input.FinalThicknessInches;

            var warnings = new List<string>();
            if (thicknessLoss < 0)
            {
                warnings.Add("Current thickness is greater than previous thickness; corrosion rate is reported as zero.");
            }
            else if (thicknessLoss == 0)
            {
                warnings.Add("No measurable thickness loss was detected; corrosion rate is reported as zero.");
            }

            double corrosionRateInPerYr = Math.Max(0, thicknessLoss / exposureTime);
            double corrosionRateMpy = corrosionRateInPerYr * 1000.0;
            double corrosionRateMmPerYr = corrosionRateInPerYr * 25.4;

            double? remainingLifeYears = null;
            if (input.TminRequiredThicknessInches.HasValue)
            {
                double tmin = input.TminRequiredThicknessInches.Value;
                if (input.FinalThicknessInches < tmin)
                {
                    warnings.Add("Current thickness is below Tmin (required thickness).");
                    remainingLifeYears = 0;
                }
                else if (corrosionRateInPerYr > 0)
                {
                    remainingLifeYears = (input.FinalThicknessInches - tmin) / corrosionRateInPerYr;
                }
            }

            return new CorrosionRateResult
            {
                ThicknessLossInches = Math.Round(thicknessLoss, 5),
                ExposureTimeYears = Math.Round(exposureTime, 2),
                CorrosionRateInchesPerYear = Math.Round(corrosionRateInPerYr, 5),
                CorrosionRateMpy = Math.Round(corrosionRateMpy, 2),
                CorrosionRateMmPerYear = Math.Round(corrosionRateMmPerYr, 4),
                RemainingLifeYears = remainingLifeYears.HasValue ? Math.Round(remainingLifeYears.Value, 2) : null,
                Warnings = warnings,
                Display = BuildDisplay(corrosionRateInPerYr, corrosionRateMpy, corrosionRateMmPerYr, exposureTime, remainingLifeYears, warnings)
            };
        }

        private static string BuildDisplay(double inPerYear, double mpy, double mmPerYear, double exposureTime, double? remainingLifeYears, List<string> warnings)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Corrosion Rate:");
            sb.AppendLine($"{inPerYear:F5} inches/year");
            sb.AppendLine($"{mpy:F2} MPY (mils/year)");
            sb.AppendLine($"{mmPerYear:F4} mm/year");
            sb.AppendLine();
            sb.AppendLine($"Exposure Time: {exposureTime:F2} years");

            if (remainingLifeYears.HasValue)
                sb.AppendLine($"Remaining Life: {remainingLifeYears.Value:F2} years");

            if (warnings.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Warnings:");
                foreach (var warning in warnings)
                    sb.AppendLine($"- {warning}");
            }

            return sb.ToString().TrimEnd();
        }

        private double ResolveExposureTimeYears(CorrosionRateInput input)
        {
            if (input.UseDates)
            {
                if (!input.InitialDate.HasValue || !input.FinalDate.HasValue)
                    throw new Exception("Initial date and final date are required when UseDates is true.");

                if (input.FinalDate.Value <= input.InitialDate.Value)
                    throw new Exception("Final date must be after initial date.");

                TimeSpan span = input.FinalDate.Value - input.InitialDate.Value;
                return span.TotalDays / 365.25;
            }

            if (!input.ExposureTimeYears.HasValue || input.ExposureTimeYears.Value <= 0)
                throw new Exception("Exposure time must be a valid positive number.");

            return input.ExposureTimeYears.Value;
        }

        private void Validate(CorrosionRateInput input)
        {
            if (input.InitialThicknessInches <= 0)
                throw new Exception("Initial thickness must be > 0.");

            if (input.FinalThicknessInches <= 0)
                throw new Exception("Final thickness must be > 0.");

            if (input.TminRequiredThicknessInches.HasValue && input.TminRequiredThicknessInches.Value <= 0)
                throw new Exception("Tmin (required thickness) must be > 0 when provided.");
        }
    }
}
