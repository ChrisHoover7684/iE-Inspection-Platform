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
                throw new ArgumentException("Or cannot exceed 20 years.");
            }

            var mrt = Math.Min(input.RTbc, input.RTip) - input.Or * (input.StPr + input.UPr);

            var requiredMrt = IsRpbOrDoubleBottom(input.BottomProtectionType) ? 0.05 : 0.10;
            if (input.BottomCoated)
            {
                var coatedRequiredMrt = 0.10 - input.CoatingLife * input.UPr;
                requiredMrt = Math.Max(requiredMrt, coatedRequiredMrt);
            }

            var passes = mrt >= requiredMrt;

            return new TankMrtResult
            {
                Mrt = Math.Round(mrt, 3),
                RequiredMrt = Math.Round(requiredMrt, 3),
                Passes = passes,
                Status = passes ? "PASS" : "FAIL - Below required MRT"
            };
        }

        private static bool IsRpbOrDoubleBottom(string bottomProtectionType)
        {
            return !string.IsNullOrWhiteSpace(bottomProtectionType) &&
                   (bottomProtectionType.Equals("RPB", StringComparison.OrdinalIgnoreCase) ||
                    bottomProtectionType.Equals("Double Bottom", StringComparison.OrdinalIgnoreCase));
        }

    }
}
