using System;
using System.Collections.Generic;
using System.Linq;

namespace iE.Core.Equipment.Tanks.Data
{
    public class TankJointEfficiencyData
    {
        public string Name { get; set; } = string.Empty;
        public double JointEfficiency { get; set; }
        public bool UsesKFactorFormula { get; set; }
        public string? Formula { get; set; }
    }

    public static class TankJointEfficiencyRepository
    {
        private static readonly IReadOnlyList<TankJointEfficiencyData> BaseJointEfficiencies =
            new List<TankJointEfficiencyData>
            {
                new()
                {
                    Name = "API 650",
                    JointEfficiency = 1.00,
                    UsesKFactorFormula = false
                },
                new()
                {
                    Name = "API 12C",
                    JointEfficiency = 0.85,
                    UsesKFactorFormula = false
                },
                new()
                {
                    Name = "Unknown",
                    JointEfficiency = 0.70,
                    UsesKFactorFormula = false
                },
                new()
                {
                    Name = "Riveted",
                    JointEfficiency = 0.45,
                    UsesKFactorFormula = false
                },
                new()
                {
                    Name = "Lap(b)",
                    JointEfficiency = 0.50,
                    UsesKFactorFormula = true,
                    Formula = "0.50 + k/5"
                }
            };

        public static List<TankJointEfficiencyData> GetAll(double kFactor = 0)
        {
            return BaseJointEfficiencies
                .Select(item => new TankJointEfficiencyData
                {
                    Name = item.Name,
                    JointEfficiency = item.UsesKFactorFormula
                        ? CalculateLapBJointEfficiency(kFactor)
                        : item.JointEfficiency,
                    UsesKFactorFormula = item.UsesKFactorFormula,
                    Formula = item.Formula
                })
                .ToList();
        }

        public static List<string> GetOptions()
        {
            return BaseJointEfficiencies.Select(x => x.Name).ToList();
        }

        public static double CalculateLapBJointEfficiency(double kFactor)
        {
            return 0.50 + (kFactor / 5.0);
        }
    }
}
