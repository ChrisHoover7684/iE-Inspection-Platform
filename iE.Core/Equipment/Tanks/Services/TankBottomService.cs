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
    }
}
