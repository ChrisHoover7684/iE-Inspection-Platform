using System.Linq;
using System;
using System.Collections.Generic;

namespace iE.Core.Equipment.Tanks.Data
{
    public class TankMaterial
    {
        public string Name { get; set; } = " ".Trim();
        public double YieldStrength { get; set; }
        public double TensileStrength { get; set; }
        public bool IsSpecialCase { get; set; }
    }

    public static class TankMaterialRepository
    {
        public static readonly Dictionary<string, TankMaterial> Materials =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["A283-C"] = new TankMaterial
                {
                    Name = "A283-C",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["A285-C"] = new TankMaterial
                {
                    Name = "A285-C",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["A36"] = new TankMaterial
                {
                    Name = "A36",
                    YieldStrength = 36000,
                    TensileStrength = 58000,
                    IsSpecialCase = false
                },

                ["A131-A, B, CS"] = new TankMaterial
                {
                    Name = "A131-A, B, CS",
                    YieldStrength = 34000,
                    TensileStrength = 58000,
                    IsSpecialCase = false
                },

                ["A131-EH 36"] = new TankMaterial
                {
                    Name = "A131-EH 36",
                    YieldStrength = 51000,
                    TensileStrength = 71000,
                    IsSpecialCase = false
                },

                ["A573-58"] = new TankMaterial
                {
                    Name = "A573-58",
                    YieldStrength = 32000,
                    TensileStrength = 58000,
                    IsSpecialCase = false
                },

                ["A573-65"] = new TankMaterial
                {
                    Name = "A573-65",
                    YieldStrength = 35000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["A573-70"] = new TankMaterial
                {
                    Name = "A573-70",
                    YieldStrength = 42000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A516-55"] = new TankMaterial
                {
                    Name = "A516-55",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["A516-60"] = new TankMaterial
                {
                    Name = "A516-60",
                    YieldStrength = 32000,
                    TensileStrength = 60000,
                    IsSpecialCase = false
                },

                ["A516-65"] = new TankMaterial
                {
                    Name = "A516-65",
                    YieldStrength = 35000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["A516-70"] = new TankMaterial
                {
                    Name = "A516-70",
                    YieldStrength = 38000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A662-C"] = new TankMaterial
                {
                    Name = "A662-C",
                    YieldStrength = 40000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["A682-C"] = new TankMaterial
                {
                    Name = "A682-C",
                    YieldStrength = 43000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A637-Class 1"] = new TankMaterial
                {
                    Name = "A637-Class 1",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A637-Class 2"] = new TankMaterial
                {
                    Name = "A637-Class 2",
                    YieldStrength = 60000,
                    TensileStrength = 80000,
                    IsSpecialCase = false
                },

                ["A633-C, D"] = new TankMaterial
                {
                    Name = "A633-C, D",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A678-A"] = new TankMaterial
                {
                    Name = "A678-A",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A678-B"] = new TankMaterial
                {
                    Name = "A678-B",
                    YieldStrength = 60000,
                    TensileStrength = 80000,
                    IsSpecialCase = false
                },

                ["A737-B"] = new TankMaterial
                {
                    Name = "A737-B",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A841"] = new TankMaterial
                {
                    Name = "A841",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["A10"] = new TankMaterial
                {
                    Name = "A10",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["A7"] = new TankMaterial
                {
                    Name = "A7",
                    YieldStrength = 33000,
                    TensileStrength = 60000,
                    IsSpecialCase = false
                },

                ["A442-56"] = new TankMaterial
                {
                    Name = "A442-56",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["A442-60"] = new TankMaterial
                {
                    Name = "A442-60",
                    YieldStrength = 32000,
                    TensileStrength = 60000,
                    IsSpecialCase = false
                },

                ["G40.21, 38W"] = new TankMaterial
                {
                    Name = "G40.21, 38W",
                    YieldStrength = 38000,
                    TensileStrength = 60000,
                    IsSpecialCase = false
                },

                ["G40.21, 44W (Note 7)"] = new TankMaterial
                {
                    Name = "G40.21, 44W (Note 7)",
                    YieldStrength = 44000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["G40.21, 44W (Note 8)"] = new TankMaterial
                {
                    Name = "G40.21, 44W (Note 8)",
                    YieldStrength = 44000,
                    TensileStrength = 64000,
                    IsSpecialCase = false
                },

                ["G40.21, 50W"] = new TankMaterial
                {
                    Name = "G40.21, 50W",
                    YieldStrength = 50000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["G40.21, 50WT (Note 7)"] = new TankMaterial
                {
                    Name = "G40.21, 50WT (Note 7)",
                    YieldStrength = 50000,
                    TensileStrength = 70000,
                    IsSpecialCase = false
                },

                ["G40.21, 50WT (Note 8)"] = new TankMaterial
                {
                    Name = "G40.21, 50WT (Note 8)",
                    YieldStrength = 50000,
                    TensileStrength = 65000,
                    IsSpecialCase = false
                },

                ["Unknown Material Specification and Grade"] = new TankMaterial
                {
                    Name = "Unknown Material Specification and Grade",
                    YieldStrength = 30000,
                    TensileStrength = 55000,
                    IsSpecialCase = false
                },

                ["Riveted Tanks: A7, A9 or A10"] = new TankMaterial
                {
                    Name = "Riveted Tanks: A7, A9 or A10",
                    YieldStrength = 0,
                    TensileStrength = 0,
                    IsSpecialCase = true
                },

                ["Riveted Tanks: Known"] = new TankMaterial
                {
                    Name = "Riveted Tanks: Known",
                    YieldStrength = 0,
                    TensileStrength = 0,
                    IsSpecialCase = true
                },

                ["Riveted Tanks: Unknown"] = new TankMaterial
                {
                    Name = "Riveted Tanks: Unknown",
                    YieldStrength = 0,
                    TensileStrength = 0,
                    IsSpecialCase = true
                },

            };

        public static TankMaterial? Get(string name)
        {
            return Materials.TryGetValue(name, out var material) ? material : null;
        }

        public static IReadOnlyList<TankMaterial> GetAll()
        {
            return Materials.Values.ToList();
        }
    }
}

