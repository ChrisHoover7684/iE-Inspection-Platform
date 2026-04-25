using System;
using System.Collections.Generic;
using System.Linq;
using iE.Core.MaterialStress.Models;

namespace iE.Core.MaterialStress.Services
{
    public static class MaterialStressInterpolator
    {
        public static double? InterpolateStress(
            SortedDictionary<double, double?> stressTable,
            double targetTemperature,
            bool allowExtrapolation,
            double maxExtrapolationDegrees,
            out bool wasExtrapolated,
            out double? lowerTemp,
            out double? lowerStress,
            out double? upperTemp,
            out double? upperStress)
        {
            wasExtrapolated = false;
            lowerTemp = null;
            lowerStress = null;
            upperTemp = null;
            upperStress = null;

            if (stressTable == null || stressTable.Count == 0)
                return null;

            var validPoints = new List<TempStressPoint>();
            foreach (var kvp in stressTable)
            {
                if (kvp.Value.HasValue)
                {
                    validPoints.Add(new TempStressPoint { Temperature = kvp.Key, Stress = kvp.Value.Value });
                }
            }
            validPoints = validPoints.OrderBy(p => p.Temperature).ToList();

            if (validPoints.Count == 0)
                return null;

            // Exact match check
            if (stressTable.ContainsKey(targetTemperature) && stressTable[targetTemperature].HasValue)
            {
                return stressTable[targetTemperature].Value;
            }

            // Find surrounding points
            TempStressPoint lower = null;
            TempStressPoint upper = null;

            foreach (var point in validPoints)
            {
                if (point.Temperature <= targetTemperature)
                    lower = point;
                if (point.Temperature >= targetTemperature && upper == null)
                    upper = point;
            }

            if (lower != null)
            {
                lowerTemp = lower.Temperature;
                lowerStress = lower.Stress;
            }
            if (upper != null)
            {
                upperTemp = upper.Temperature;
                upperStress = upper.Stress;
            }

            // Both bounds available - interpolate
            if (lower != null && upper != null)
            {
                double t1 = lower.Temperature;
                double t2 = upper.Temperature;
                double s1 = lower.Stress;
                double s2 = upper.Stress;

                if (Math.Abs(t2 - t1) < 0.001)
                    return s1;

                double stress = s1 + (s2 - s1) * (targetTemperature - t1) / (t2 - t1);
                return Math.Round(stress, 2);
            }

            // Extrapolation logic
            if (!allowExtrapolation)
                return null;

            if (lower != null && upper == null && targetTemperature > lower.Temperature)
            {
                if ((targetTemperature - lower.Temperature) <= maxExtrapolationDegrees)
                {
                    wasExtrapolated = true;
                    return lower.Stress;
                }
            }
            else if (upper != null && lower == null && targetTemperature < upper.Temperature)
            {
                if ((upper.Temperature - targetTemperature) <= maxExtrapolationDegrees)
                {
                    wasExtrapolated = true;
                    return upper.Stress;
                }
            }

            return null;
        }

        private class TempStressPoint
        {
            public double Temperature { get; set; }
            public double Stress { get; set; }
        }
    }
}