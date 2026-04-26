using iE.Core.Equipment.Tanks.Models;

namespace iE.Core.Equipment.Tanks.Services
{
    public class TankSettlementService
    {
        // API 653 Annex B Table B-1 (diameter in ft, arc length in ft, allowable differential in inches)
        private static readonly double[] TableDiameters = [20, 40, 60, 80, 100, 120, 150, 200, 250, 300];
        private static readonly double[] TableArcLengths = [5, 10, 15, 20, 25, 30, 35];
        private static readonly double[,] TableB1AllowableDifferential =
        {
            { 0.35, 0.55, 0.70, 0.85, 1.00, 1.10, 1.20 },
            { 0.40, 0.65, 0.85, 1.05, 1.20, 1.35, 1.45 },
            { 0.45, 0.75, 1.00, 1.25, 1.45, 1.60, 1.75 },
            { 0.50, 0.85, 1.15, 1.45, 1.65, 1.85, 2.00 },
            { 0.55, 0.95, 1.30, 1.60, 1.85, 2.05, 2.25 },
            { 0.60, 1.05, 1.40, 1.75, 2.00, 2.20, 2.45 },
            { 0.70, 1.20, 1.60, 2.00, 2.30, 2.55, 2.80 },
            { 0.80, 1.35, 1.85, 2.30, 2.65, 2.95, 3.20 },
            { 0.90, 1.50, 2.05, 2.55, 2.95, 3.25, 3.55 },
            { 1.00, 1.65, 2.25, 2.80, 3.25, 3.60, 3.90 }
        };

        public TankSettlementResult Calculate(TankSettlementInput input)
        {
            if (input.SettlementElevationPoints.Count < 2)
                throw new ArgumentException("At least two settlement readings are required.");
            if (input.BottomThickness <= 0)
                throw new ArgumentException("BottomThickness must be greater than zero.");

            var elevations = input.SettlementElevationPoints;
            var pointCount = elevations.Count;
            var diameter = input.TankDiameter;
            var radius = diameter / 2.0;

            var maxElevation = elevations.Max();
            var minElevation = elevations.Min();

            var arcLength = Math.PI * diameter / pointCount;
            var maxDifferential = CalculateMaxDifferential(elevations);
            var allowableDifferential = GetAllowableDifferentialSettlement(diameter, arcLength);

            var tiltRadians = Math.Atan2(maxElevation - minElevation, diameter * 12.0);
            var tiltDegrees = tiltRadians * (180.0 / Math.PI);

            var edgeSettlement = maxElevation - input.ReferenceElevation;
            var allowableEdgeSettlement = 0.37 * diameter;

            var centerSettlement = input.ReferenceElevation - input.CenterElevation;
            var allowableCenterSettlement = Math.Min((0.26 * radius * radius) / input.BottomThickness, 12.0);

            var differentialOk = maxDifferential <= allowableDifferential;
            var edgeOk = Math.Abs(edgeSettlement) <= allowableEdgeSettlement;
            var centerOk = Math.Abs(centerSettlement) <= allowableCenterSettlement;
            var isAcceptable = differentialOk && edgeOk && centerOk;


            return new TankSettlementResult
            {
                PointCount = pointCount,
                ArcLength = Math.Round(arcLength, 3),

                MaxElevation = Math.Round(maxElevation, 3),
                MinElevation = Math.Round(minElevation, 3),
                MaxDifferential = Math.Round(maxDifferential, 3),
                AllowableDifferentialSettlement = Math.Round(allowableDifferential, 3),

                TiltRadians = Math.Round(tiltRadians, 6),
                TiltDegrees = Math.Round(tiltDegrees, 3),

                EdgeSettlement = Math.Round(edgeSettlement, 3),
                AllowableEdgeSettlement = Math.Round(allowableEdgeSettlement, 3),

                CenterSettlement = Math.Round(centerSettlement, 3),
                AllowableCenterSettlement = Math.Round(allowableCenterSettlement, 3),

                IsDifferentialAcceptable = differentialOk,
                IsEdgeAcceptable = edgeOk,
                IsCenterAcceptable = centerOk,
                IsAcceptable = isAcceptable,
                Status = isAcceptable ? "PASS" : "REVIEW - Settlement exceeds allowable API 653 criteria"
            };
        }

        private static double CalculateMaxDifferential(IReadOnlyList<double> elevations)
        {
            var maxDifferential = 0.0;
            for (var i = 0; i < elevations.Count; i++)
            {
                var next = (i + 1) % elevations.Count;
                var differential = Math.Abs(elevations[next] - elevations[i]);
                if (differential > maxDifferential)
                    maxDifferential = differential;
            }

            return maxDifferential;
        }

        private static double GetAllowableDifferentialSettlement(double diameter, double arcLength)
        {
            var dBracket = GetBracket(TableDiameters, diameter);
            var sBracket = GetBracket(TableArcLengths, arcLength);

            var q11 = TableB1AllowableDifferential[dBracket.lowerIndex, sBracket.lowerIndex];
            var q21 = TableB1AllowableDifferential[dBracket.upperIndex, sBracket.lowerIndex];
            var q12 = TableB1AllowableDifferential[dBracket.lowerIndex, sBracket.upperIndex];
            var q22 = TableB1AllowableDifferential[dBracket.upperIndex, sBracket.upperIndex];

            if (dBracket.lowerIndex == dBracket.upperIndex && sBracket.lowerIndex == sBracket.upperIndex)
                return q11;

            if (dBracket.lowerIndex == dBracket.upperIndex)
                return LinearInterpolate(
                    TableArcLengths[sBracket.lowerIndex],
                    TableArcLengths[sBracket.upperIndex],
                    q11,
                    q12,
                    arcLength);

            if (sBracket.lowerIndex == sBracket.upperIndex)
                return LinearInterpolate(
                    TableDiameters[dBracket.lowerIndex],
                    TableDiameters[dBracket.upperIndex],
                    q11,
                    q21,
                    diameter);

            var x1 = TableDiameters[dBracket.lowerIndex];
            var x2 = TableDiameters[dBracket.upperIndex];
            var y1 = TableArcLengths[sBracket.lowerIndex];
            var y2 = TableArcLengths[sBracket.upperIndex];

            var denominator = (x2 - x1) * (y2 - y1);
            if (Math.Abs(denominator) < double.Epsilon)
                return q11;

            return
                q11 * (x2 - diameter) * (y2 - arcLength) / denominator +
                q21 * (diameter - x1) * (y2 - arcLength) / denominator +
                q12 * (x2 - diameter) * (arcLength - y1) / denominator +
                q22 * (diameter - x1) * (arcLength - y1) / denominator;
        }

        private static (int lowerIndex, int upperIndex) GetBracket(IReadOnlyList<double> axis, double value)
        {
            if (value <= axis[0])
                return (0, 0);
            if (value >= axis[^1])
                return (axis.Count - 1, axis.Count - 1);

            for (var i = 0; i < axis.Count - 1; i++)
            {
                if (value >= axis[i] && value <= axis[i + 1])
                    return (i, i + 1);
            }

            return (axis.Count - 1, axis.Count - 1);
        }

        private static double LinearInterpolate(double x1, double x2, double y1, double y2, double x)
        {
            if (Math.Abs(x2 - x1) < double.Epsilon)
                return y1;

            return y1 + (y2 - y1) * ((x - x1) / (x2 - x1));
        }
    }
}
