using iE.Core.Equipment.Tanks.Models;

namespace iE.Core.Equipment.Tanks.Services
{
    public class TankSettlementService
    {
        public TankSettlementResult Calculate(TankSettlementInput input)
        {
            if (input.SettlementReadings.Count < 2)
                throw new ArgumentException("At least two settlement readings are required.");

            var maxSettlement = input.SettlementReadings.Max();
            var minSettlement = input.SettlementReadings.Min();
            var differential = maxSettlement - minSettlement;

            var shellHeightEquivalent = input.TankDiameter * 12; // inches
            var outOfPlaneLimit = shellHeightEquivalent * (input.OutOfPlaneLimitPercent / 100.0);
            var acceptable = differential <= outOfPlaneLimit;

            return new TankSettlementResult
            {
                MaxSettlement = Math.Round(maxSettlement, 3),
                MinSettlement = Math.Round(minSettlement, 3),
                DifferentialSettlement = Math.Round(differential, 3),
                OutOfPlaneLimit = Math.Round(outOfPlaneLimit, 3),
                IsOutOfPlaneAcceptable = acceptable,
                Status = acceptable ? "PASS" : "REVIEW - Settlement exceeds configured limit"
            };
        }
    }
}
