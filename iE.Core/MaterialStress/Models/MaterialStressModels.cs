using System;
using System.Collections.Generic;
using System.Linq;

namespace iE.Core.MaterialStress.Models
{
    /// <summary>
    /// Defines the design margin / safety factor era for stress values
    /// </summary>
    public enum StressEra
    {
        Pre1950,
        From1950To1998,
        From1999Onward
    }

    /// <summary>
    /// Dataset metadata containing the design margin/safety factor basis
    /// </summary>
    public class StressDataset
    {
        public string CalculationFamily { get; set; } = "";
        public string DesignCode { get; set; } = "ASME_VIII_DIV1";
        public StressEra Era { get; set; }
        
        public string DisplayName { get; set; } = "";
        public double DesignMargin { get; set; }
        public string SourceFile { get; set; } = "";
        public string SheetName { get; set; } = "";
        public DateTime? LoadedDate { get; set; }

        public override string ToString()
        {
            return $"{DisplayName} (Margin: {DesignMargin})";
        }
    }

    /// <summary>
    /// Core material identification properties for lookup
    /// </summary>
    public class MaterialIdentity
    {
        public string SpecNo { get; set; } = "";
        public string TypeGrade { get; set; } = "";
        public string ProductForm { get; set; } = "";
        public string AlloyUNS { get; set; } = "";
        public string ClassConditionTemper { get; set; } = "";
        public string Notes { get; set; } = "";

        public string GetCompositeKey()
        {
            return $"{SpecNo}|{TypeGrade}|{ProductForm}|{AlloyUNS}|{ClassConditionTemper}".ToUpperInvariant();
        }

        public string GetWeightedKey()
        {
            return $"{SpecNo}|{TypeGrade}|{ProductForm}".ToUpperInvariant();
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(SpecNo)) parts.Add(SpecNo);
            if (!string.IsNullOrWhiteSpace(TypeGrade)) parts.Add(TypeGrade);
            if (!string.IsNullOrWhiteSpace(ProductForm)) parts.Add(ProductForm);
            if (!string.IsNullOrWhiteSpace(AlloyUNS)) parts.Add(AlloyUNS);
            return string.Join(" - ", parts);
        }
    }

    /// <summary>
    /// Complete material stress record with temperature-stress mapping
    /// </summary>
    public class MaterialStressRecord
    {
        public StressDataset Dataset { get; set; }
        public MaterialIdentity Material { get; set; }
        public SortedDictionary<double, double?> AllowableStressByTemperature { get; set; }
        public double WeldEfficiency { get; set; }
        public int SourceLineNumber { get; set; }
        public int MaterialId { get; set; }

        public MaterialStressRecord()
        {
            Dataset = new StressDataset();
            Material = new MaterialIdentity();
            AllowableStressByTemperature = new SortedDictionary<double, double?>();
            WeldEfficiency = 1.0;
        }

        public double? MinTemperature
        {
            get
            {
                var valid = AllowableStressByTemperature
                    .Where(kvp => kvp.Value.HasValue)
                    .Select(kvp => kvp.Key)
                    .ToList();
                return valid.Count > 0 ? (double?)valid.Min() : null;
            }
        }

        public double? MaxTemperature
        {
            get
            {
                var valid = AllowableStressByTemperature
                    .Where(kvp => kvp.Value.HasValue)
                    .Select(kvp => kvp.Key)
                    .ToList();
                return valid.Count > 0 ? (double?)valid.Max() : null;
            }
        }

        public override string ToString()
        {
            return $"{Material} - {Dataset.DisplayName}";
        }
    }
}