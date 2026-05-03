using System;
using System.Collections.Generic;

namespace iE.Core.Inspection.CorrosionRate
{
    public class CorrosionRateService
    {
        public CorrosionRateResult Calculate(CorrosionRateInput input)
        {
            Validate(input);

            double exposureTime = ResolveExposureTimeYears(input);
            double thicknessLoss = input.InitialThicknessInches - input.FinalThicknessInches;

            double corrosionRateInPerYr = thicknessLoss / exposureTime;
            double corrosionRateMpy = corrosionRateInPerYr * 1000.0;
            double corrosionRateMmPerYr = corrosionRateInPerYr * 25.4;
            var warnings = new List<string>();

            if (corrosionRateInPerYr <= 0)
            {
                warnings.Add("No measurable corrosion rate");
            }

            if (input.CurrentThicknessInches <= input.TminInches)
            {
                warnings.Add("Current thickness below Tmin");
            }

            double? remainingLifeYears = null;
            double? nextInspectionYears = null;
            DateTime? nextInspectionDate = null;

            if (input.CurrentThicknessInches > input.TminInches && corrosionRateInPerYr > 0)
            {
                remainingLifeYears = (input.CurrentThicknessInches - input.TminInches) / corrosionRateInPerYr;
                nextInspectionYears = remainingLifeYears.Value * input.InspectionFactor;
                nextInspectionDate = DateTime.UtcNow.Date.AddDays(nextInspectionYears.Value * 365.25);
            }
            else
            {
                warnings.Add("Remaining life not calculable");
                warnings.Add("Remaining life cannot be calculated");
            }

            return new CorrosionRateResult
            {
                ThicknessLossInches = Math.Round(thicknessLoss, 5),
                ExposureTimeYears = Math.Round(exposureTime, 2),
                CorrosionRateInchesPerYear = Math.Round(corrosionRateInPerYr, 5),
                CorrosionRateMpy = Math.Round(corrosionRateMpy, 2),
                CorrosionRateMmPerYear = Math.Round(corrosionRateMmPerYr, 4),
                RemainingLifeYears = remainingLifeYears.HasValue ? Math.Round(remainingLifeYears.Value, 2) : null,
                NextInspectionYears = nextInspectionYears.HasValue ? Math.Round(nextInspectionYears.Value, 2) : null,
                NextInspectionDate = nextInspectionDate,
                Warnings = warnings,
                Display =
                    $"Corrosion Rate:\n" +
                    $"{corrosionRateInPerYr:F5} inches/year\n" +
                    $"{corrosionRateMpy:F2} MPY (mils/year)\n" +
                    $"{corrosionRateMmPerYr:F4} mm/year\n\n" +
                    $"Exposure Time: {exposureTime:F2} years"
            };
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

            if (input.InitialThicknessInches <= input.FinalThicknessInches)
                throw new Exception("Initial thickness must be greater than final thickness.");

            if (input.CurrentThicknessInches <= 0)
                throw new Exception("Current thickness must be > 0.");

            if (input.TminInches <= 0)
                throw new Exception("Tmin must be > 0.");

            if (input.InspectionFactor <= 0)
                throw new Exception("Inspection factor must be > 0.");
        }
    }
}
