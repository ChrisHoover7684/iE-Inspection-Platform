using iE.Core.Equipment.Tanks.Models;

namespace iE.Core.Equipment.Tanks.Services
{
    public class TankBottomService
    {
        public TankBottomResult Calculate(TankBottomInput input)
        {
            var requiredBottomThickness = Math.Max(0.1, input.NominalBottomThickness - input.CorrosionAllowance);

            var result = new TankBottomResult
            {
                RequiredBottomThickness = Math.Round(requiredBottomThickness, 3),
                BottomPasses = input.ActualBottomThickness >= requiredBottomThickness,
                BottomStatus = input.ActualBottomThickness >= requiredBottomThickness
                    ? "PASS"
                    : "FAIL - Below required bottom thickness"
            };

            if (input.HasAnnularRing)
            {
                var requiredAnnularThickness = Math.Max(0.1, input.AnnularRingNominalThickness - input.CorrosionAllowance);
                result.RequiredAnnularRingThickness = Math.Round(requiredAnnularThickness, 3);
                result.AnnularRingPasses = input.ActualAnnularRingThickness >= requiredAnnularThickness;
                result.AnnularRingStatus = result.AnnularRingPasses
                    ? "PASS"
                    : "FAIL - Below required annular ring thickness";
            }
            else
            {
                result.RequiredAnnularRingThickness = 0;
                result.AnnularRingPasses = true;
                result.AnnularRingStatus = "N/A - No annular ring";
            }

            return result;
        }

        public TankMrtResult CalculateMrt(TankMrtInput input)
        {
            if (input.Or > 20)
            {
                throw new ArgumentException("Or cannot be greater than 20 years.");
            }

            var mrt = Math.Min(input.RTbc, input.RTip) - input.Or * (input.StPr + input.UPr);

            var hasEnhancedProtection =
                string.Equals(input.BottomProtectionType, "RPB", StringComparison.OrdinalIgnoreCase)
                || string.Equals(input.BottomProtectionType, "Double Bottom", StringComparison.OrdinalIgnoreCase);

            var requiredMrt = hasEnhancedProtection ? 0.05 : 0.10;

            if (input.BottomCoated)
            {
                var coatedRequiredMrt = 0.10 - input.CoatingLife * input.UPr;
                requiredMrt = Math.Max(requiredMrt, coatedRequiredMrt);
            }

            return new TankMrtResult
            {
                MRT = Math.Round(mrt, 3),
                RequiredMRT = Math.Round(requiredMrt, 3),
                Passes = mrt >= requiredMrt,
                Status = mrt >= requiredMrt ? "PASS" : "FAIL - Below required MRT"
            };
        }

    }
}
