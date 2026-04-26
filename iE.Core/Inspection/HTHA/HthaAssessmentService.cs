using System.Globalization;

namespace iE.Core.Inspection.HTHA
{
    public class HthaAssessmentService
    {
        private const double PsiaToMpa = 0.00689476;

        private static readonly Dictionary<string, (double[] pressures, double[] temps)> Curves =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["Carbon Steel (C ≤ 0.3%, PWHT)"] =
                (
                    new[] { 0d, 50d, 100d, 101d, 150d, 200d, 250d, 295d, 300d, 350d, 400d, 500d, 600d, 900d, 1600d, 2400d, 3000d, 13000d },
                    new[] { 1100d, 1080d, 1055d, 699d, 600d, 575d, 555d, 550d, 548d, 540d, 535d, 525d, 515d, 500d, 475d, 450d, 430d, 430d }
                ),
                ["Carbon Steel (C ≤ 0.3%, non-PWHT)"] =
                (
                    new[] { 0d, 50d, 100d, 101d, 150d, 200d, 250d, 295d, 300d, 350d, 400d, 500d, 600d, 900d, 1600d, 2400d, 3000d, 13000d },
                    new[] { 1050d, 1030d, 1005d, 649d, 550d, 525d, 505d, 500d, 498d, 490d, 485d, 475d, 465d, 450d, 425d, 400d, 380d, 380d }
                ),
                ["1.0Cr-0.5Mo"] =
                (
                    new[] { 0d, 300d, 400d, 450d, 500d, 600d, 700d, 1220d, 1221d, 1300d, 1500d, 1700d, 1900d, 2300d, 2999d, 3000d, 13000d },
                    new[] { 1100d, 1000d, 970d, 955d, 945d, 920d, 900d, 900d, 950d, 775d, 695d, 660d, 650d, 640d, 625d, 625d, 625d }
                ),
                ["1.25Cr-0.5Mo"] =
                (
                    new[] { 0d, 350d, 351d, 900d, 901d, 1220d, 1221d, 1300d, 1500d, 1700d, 1900d, 2300d, 2999d, 3000d, 13000d },
                    new[] { 1150d, 1100d, 1100d, 990d, 991d, 950d, 950d, 775d, 695d, 660d, 650d, 640d, 625d, 625d, 625d }
                ),
                ["2.25Cr-1.0Mo"] =
                (
                    new[] { 0d, 300d, 400d, 1400d, 1999d, 2000d, 6000d },
                    new[] { 1200d, 1140d, 1120d, 950d, 856d, 855d, 855d }
                ),
                ["3Cr-1Mo"] =
                (
                    new[] { 0d, 500d, 800d, 1200d, 1400d, 1800d, 6000d },
                    new[] { 1250d, 1145d, 1095d, 1025d, 1000d, 950d, 950d }
                ),
                ["6Cr-0.5Mo"] =
                (
                    new[] { 0d, 500d, 700d, 1050d, 6000d },
                    new[] { 1300d, 1205d, 1170d, 1115d, 1115d }
                )
            };

        private static readonly Dictionary<string, List<string>> GeneralRecommendations = new()
        {
            ["Critical"] =
            [
                "IMMEDIATE ACTION REQUIRED: Shutdown and inspect per API 571 Section 4.2.6.",
                "Perform advanced NDE (PAUT/TOFD) to check for HTHA damage.",
                "Consult a materials engineer for mitigation options (e.g., cladding upgrade)."
            ],
            ["Warning"] =
            [
                "High Risk: Schedule inspection at next turnaround.",
                "Monitor operating conditions closely - avoid excursions.",
                "Consider upgrading to more resistant material during next maintenance."
            ],
            ["Caution"] =
            [
                "Moderate Risk: Include in routine inspection plans.",
                "Review historical data for temperature/pressure fluctuations.",
                "Consider corrosion coupons for long-term monitoring."
            ],
            ["Safe"] =
            [
                "Low Risk: Maintain standard inspection frequency.",
                "Document baseline measurements for future reference.",
                "Reassess if process conditions change significantly."
            ]
        };

        private static readonly Dictionary<string, List<string>> MaterialSpecificRecommendations = new()
        {
            ["Carbon"] =
            [
                "Carbon steels are most vulnerable to HTHA - monitor hardness (<200 HB).",
                "PWHT is critical for carbon steels above 450°F (232°C)."
            ],
            ["CrMo"] =
            [
                "Cr-Mo alloys require verified PWHT and hardness control.",
                "For 2.25Cr-1Mo and higher alloys, consider hydrogen probes."
            ]
        };

        public IReadOnlyCollection<string> GetSupportedMaterials() => Curves.Keys.ToList().AsReadOnly();

        public HthaAssessmentResult Assess(HthaAssessmentInput input)
        {
            Validate(input);

            var result = new HthaAssessmentResult
            {
                Material = input.Material,
                TemperatureF = input.TemperatureF,
                TemperatureC = (input.TemperatureF - 32d) * 5d / 9d,
                HydrogenPartialPressurePsia = input.HydrogenPartialPressurePsia,
                HydrogenPartialPressureMpa = input.HydrogenPartialPressurePsia * PsiaToMpa
            };

            double maxAllowableTemp = InterpolateTemperature(input.Material, input.HydrogenPartialPressurePsia);

            if (double.IsNaN(maxAllowableTemp))
            {
                result.CurveDataAvailable = false;
                result.RiskCategory = "Unknown";
                result.RiskLevel = "CURVE DATA UNAVAILABLE";
                result.Display = BuildUnavailableDisplay(result);
                return result;
            }

            result.CurveDataAvailable = true;
            result.MaxAllowableTemperatureF = maxAllowableTemp;
            result.TemperatureRatio = input.TemperatureF / maxAllowableTemp;
            (result.RiskCategory, result.RiskLevel) = ResolveRisk(result.TemperatureRatio.Value);

            result.GeneralRecommendations = GeneralRecommendations[result.RiskCategory].ToList();
            string materialType = input.Material.Contains("Cr", StringComparison.OrdinalIgnoreCase) ? "CrMo" : "Carbon";
            result.MaterialSpecificRecommendations = MaterialSpecificRecommendations[materialType].ToList();
            result.Display = BuildDisplay(result);

            return result;
        }

        private static void Validate(HthaAssessmentInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Material))
                throw new Exception("Material is required.");

            if (input.TemperatureF <= 0)
                throw new Exception("Temperature must be greater than zero.");

            if (input.HydrogenPartialPressurePsia <= 0)
                throw new Exception("Hydrogen partial pressure must be greater than zero.");
        }

        private static (string category, string level) ResolveRisk(double tempRatio)
        {
            if (tempRatio >= 1.0)
                return ("Critical", "CRITICAL RISK (On/Above Curve)");

            if (tempRatio >= 0.85)
                return ("Warning", "WARNING (Approaching Curve)");

            if (tempRatio >= 0.5)
                return ("Caution", "CAUTION (Mid-Range)");

            return ("Safe", "SAFE (Well Below Curve)");
        }

        private static string BuildUnavailableDisplay(HthaAssessmentResult result)
        {
            return $"HTHA Risk Assessment (API RP 941)\n" +
                   $"Material: {result.Material}\n" +
                   $"Temperature: {result.TemperatureF.ToString("F1", CultureInfo.InvariantCulture)}°F ({result.TemperatureC.ToString("F1", CultureInfo.InvariantCulture)}°C)\n" +
                   $"H2 Partial Pressure: {result.HydrogenPartialPressurePsia.ToString("F1", CultureInfo.InvariantCulture)} psia ({result.HydrogenPartialPressureMpa.ToString("F2", CultureInfo.InvariantCulture)} MPa)\n\n" +
                   "Result: CURVE DATA UNAVAILABLE\n" +
                   "Recommendation: Consult API 941 Figure 1 for this material.";
        }

        private static string BuildDisplay(HthaAssessmentResult result)
        {
            return $"HTHA Risk Assessment (API RP 941)\n" +
                   $"Material: {result.Material}\n" +
                   $"Temperature: {result.TemperatureF.ToString("F1", CultureInfo.InvariantCulture)}°F ({result.TemperatureC.ToString("F1", CultureInfo.InvariantCulture)}°C)\n" +
                   $"H2 Partial Pressure: {result.HydrogenPartialPressurePsia.ToString("F1", CultureInfo.InvariantCulture)} psia ({result.HydrogenPartialPressureMpa.ToString("F2", CultureInfo.InvariantCulture)} MPa)\n" +
                   $"Result: {result.RiskLevel}\n" +
                   $"Temperature is at {result.TemperatureRatio!.Value.ToString("P0", CultureInfo.InvariantCulture)} of the Nelson Curve limit.";
        }

        private static double InterpolateTemperature(string material, double pressurePsia)
        {
            if (!Curves.TryGetValue(material, out var curve))
                return double.NaN;

            double[] pressures = curve.pressures;
            double[] temps = curve.temps;

            for (int i = 0; i < pressures.Length; i++)
            {
                if (Math.Abs(pressurePsia - pressures[i]) < 0.1)
                    return temps[i];
            }

            if (pressurePsia <= pressures[0])
                return temps[0];

            if (pressurePsia >= pressures[^1])
                return temps[^1];

            for (int i = 1; i < pressures.Length; i++)
            {
                if (pressurePsia < pressures[i])
                {
                    return temps[i - 1] + (pressurePsia - pressures[i - 1]) *
                           (temps[i] - temps[i - 1]) / (pressures[i] - pressures[i - 1]);
                }
            }

            return temps[^1];
        }
    }
}
