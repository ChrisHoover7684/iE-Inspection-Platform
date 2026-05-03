using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using iE.Core.MaterialStress.Models;

namespace iE.Core.MaterialStress.Services
{
    public class MaterialStressService
    {
        private readonly MaterialStressLibrary _library;

        public MaterialStressService()
        {
            _library = new MaterialStressLibrary();
        }

        public MaterialStressService(MaterialStressLibrary library)
        {
            _library = library ?? new MaterialStressLibrary();
        }

        public MaterialStressLibrary Library => _library;

        /// <summary>
        /// Normalizes a string for comparison: trim, remove double spaces, convert to uppercase.
        /// </summary>
        private string Normalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            return value.Trim()
                        .Replace("  ", " ")
                        .ToUpperInvariant();
        }

        public StressLookupResult GetAllowableStress(StressLookupRequest request)
        {
            if (request == null)
                return StressLookupResult.NotFound("Invalid request: request is null");

            if (string.IsNullOrWhiteSpace(request.SpecNo))
                return StressLookupResult.NotFound("Invalid request: SpecNo is required");

            // Normalize request fields for comparison
            string reqSpec = Normalize(request.SpecNo);
            string reqGrade = Normalize(request.TypeGrade);
            string reqForm = Normalize(request.ProductForm);
            string reqUNS = Normalize(request.AlloyUNS);
            string reqClass = Normalize(request.ClassConditionTemper);
            StressEra reqEra = request.Era;

            // Find all records that match the era
            string requestedDesignCode = string.IsNullOrWhiteSpace(request.DesignCode)
                 ? "ASME_VIII_DIV1"
                 : request.DesignCode.Trim().ToUpperInvariant();

            string requestedFamily = string.IsNullOrWhiteSpace(request.CalculationFamily)
                ? ""
                : request.CalculationFamily.Trim().ToUpperInvariant();

            var eraRecords = _library.GetRecordsByEra(reqEra)
                .Where(r =>
                    Normalize(r.Dataset.DesignCode) == requestedDesignCode &&
                    (string.IsNullOrWhiteSpace(requestedFamily) ||
                     Normalize(r.Dataset.CalculationFamily) == requestedFamily))
                .ToList();

            // First: exact match on all provided fields (using normalized values)
            var exactMatches = new List<MaterialStressRecord>();
            foreach (var record in eraRecords)
            {
                bool match = Normalize(record.Material.SpecNo) == reqSpec;
                if (match && !string.IsNullOrWhiteSpace(reqGrade))
                    match = Normalize(record.Material.TypeGrade) == reqGrade;
                if (match && !string.IsNullOrWhiteSpace(reqForm))
                    match = Normalize(record.Material.ProductForm) == reqForm;
                if (match && !string.IsNullOrWhiteSpace(reqUNS))
                    match = Normalize(record.Material.AlloyUNS) == reqUNS;
                if (match && !string.IsNullOrWhiteSpace(reqClass))
                    match = Normalize(record.Material.ClassConditionTemper) == reqClass;

                if (match)
                    exactMatches.Add(record);
            }

            if (exactMatches.Count == 1)
            {
                return LookupStressFromRecord(exactMatches[0], request);
            }
            else if (exactMatches.Count > 1 && !request.AllowPartialMatch)
            {
                if (request.StrictMode)
                {
                    return StressLookupResult.NotFound($"Multiple exact matches found ({exactMatches.Count}) for {request.SpecNo} / {request.TypeGrade} / {request.ProductForm}");
                }
                var result = LookupStressFromRecord(exactMatches[0], request);
                result.Message = $"Warning: Multiple matches found ({exactMatches.Count}). Using first match.";
                return result;
            }

            // Weighted match (allow partial)
            if (request.AllowPartialMatch)
            {
                // Build target identity for weighted search
                var target = new MaterialIdentity
                {
                    SpecNo = request.SpecNo,
                    TypeGrade = request.TypeGrade,
                    ProductForm = request.ProductForm,
                    AlloyUNS = request.AlloyUNS,
                    ClassConditionTemper = request.ClassConditionTemper
                };
                var weightedMatches = _library.FindByWeightedKey(target);
                var eraWeighted = new List<MaterialStressRecord>();
                foreach (var match in weightedMatches)
                {
                    if (match.Dataset.Era == reqEra)
                        eraWeighted.Add(match);
                }

                if (eraWeighted.Count == 1)
                {
                    return LookupStressFromRecord(eraWeighted[0], request);
                }
                else if (eraWeighted.Count > 1)
                {
                    var result = LookupStressFromRecord(eraWeighted[0], request);
                    result.Message = $"Warning: Multiple partial matches found ({eraWeighted.Count}). Using first match.";
                    return result;
                }
            }

            // Spec-only match (allow partial)
            if (request.AllowPartialMatch)
            {
                var specMatches = _library.FindBySpecNo(request.SpecNo);
                var eraSpec = new List<MaterialStressRecord>();
                foreach (var match in specMatches)
                {
                    if (match.Dataset.Era == reqEra)
                        eraSpec.Add(match);
                }

                if (eraSpec.Count == 1)
                {
                    return LookupStressFromRecord(eraSpec[0], request);
                }
                else if (eraSpec.Count > 1)
                {
                    var result = LookupStressFromRecord(eraSpec[0], request);
                    result.Message = $"Warning: Multiple spec-only matches found ({eraSpec.Count}). Using first match.";
                    return result;
                }
            }

            // No match found – provide detailed debug information
            var sb = new StringBuilder();
            sb.AppendLine($"Material not found for era {reqEra}:");
            sb.AppendLine($"  SpecNo: {request.SpecNo}");
            if (!string.IsNullOrWhiteSpace(request.TypeGrade)) sb.AppendLine($"  Type/Grade: {request.TypeGrade}");
            if (!string.IsNullOrWhiteSpace(request.ProductForm)) sb.AppendLine($"  Product Form: {request.ProductForm}");

            // Debug: list all records with same spec normalized
            Debug.WriteLine($"Lookup failed for {reqEra}: {reqSpec} / {reqGrade} / {reqForm}");
            Debug.WriteLine("Available candidates with same normalized SpecNo:");
            foreach (var rec in eraRecords)
            {
                if (Normalize(rec.Material.SpecNo) == reqSpec)
                {
                    Debug.WriteLine($"  Spec='{rec.Material.SpecNo}', Grade='{rec.Material.TypeGrade}', Form='{rec.Material.ProductForm}'");
                }
            }

            return StressLookupResult.NotFound(sb.ToString());
        }

        private StressLookupResult LookupStressFromRecord(MaterialStressRecord record, StressLookupRequest request)
        {
            // Interpolate stress value (returns value in ksi if database stores ksi)
            double? stress = MaterialStressInterpolator.InterpolateStress(
                record.AllowableStressByTemperature,
                request.Temperature,
                request.AllowExtrapolation,
                request.MaxExtrapolationDegrees,
                out bool wasExtrapolated,
                out double? lowerTemp,
                out double? lowerStress,
                out double? upperTemp,
                out double? upperStress
            );

            if (!stress.HasValue)
            {
                string rangeMessage = "";
                if (record.MinTemperature.HasValue && record.MaxTemperature.HasValue)
                {
                    rangeMessage = $"Temperature {request.Temperature}°F is outside valid range ({record.MinTemperature}°F - {record.MaxTemperature}°F)";
                }
                else
                {
                    rangeMessage = $"Cannot interpolate stress at {request.Temperature}°F from available data";
                }
                return StressLookupResult.NotFound(rangeMessage);
            }

            // Stresses are stored as ksi. Convert to psi exactly once in result model.
            double finalStressKsi = stress.Value * record.WeldEfficiency;

            var result = StressLookupResult.Success(
                finalStressKsi,
                request.Temperature,
                record,
                interpolated: lowerTemp.HasValue && upperTemp.HasValue,
                extrapolated: wasExtrapolated
            );

            result.LowerBoundTemperature = lowerTemp;
            result.LowerBoundStressPsi = lowerStress.HasValue ? Math.Round(lowerStress.Value * 1000.0, 0) : null;
            result.UpperBoundTemperature = upperTemp;
            result.UpperBoundStressPsi = upperStress.HasValue ? Math.Round(upperStress.Value * 1000.0, 0) : null;

            if (!result.WasInterpolated && !result.WasExtrapolated)
                result.Message = "Exact allowable stress match found.";
            else if (result.WasInterpolated)
                result.Message = "Interpolated between temperature points.";

            return result;
        }

        public void ImportRecord(
                StressEra era,
                double designMargin,
                string specNo,
                string typeGrade,
                string productForm,
                string alloyUNS,
                string classConditionTemper,
                SortedDictionary<double, double?> stressTable,
                string notes,
                int lineNo,
                int materialId,
                string designCode = "ASME_VIII_DIV1",
                string calculationFamily = "PressureVessel")
        {
            if (notes == null) notes = "";

            var record = new MaterialStressRecord();
            record.Dataset.Era = era;
            record.Dataset.DisplayName = GetEraDisplayName(era);
            record.Dataset.DesignMargin = designMargin;
            record.Dataset.DesignCode = designCode;

            record.Material.SpecNo = specNo?.Trim() ?? "";
            record.Material.TypeGrade = typeGrade?.Trim() ?? "";
            record.Material.ProductForm = productForm?.Trim() ?? "";
            record.Material.AlloyUNS = alloyUNS?.Trim() ?? "";
            record.Material.ClassConditionTemper = classConditionTemper?.Trim() ?? "";
            record.Material.Notes = notes;

            // Ensure stress values are stored in PSI. If the imported table is in KSI, multiply by 1000 here.
            // Example:
            // var converted = new SortedDictionary<double, double?>();
            // foreach (var kv in stressTable)
            //     converted[kv.Key] = kv.Value.HasValue ? kv.Value.Value * 1000.0 : (double?)null;
            // record.AllowableStressByTemperature = converted;
            record.AllowableStressByTemperature = stressTable; // Assumes already PSI

            record.SourceLineNumber = lineNo;
            record.MaterialId = materialId;

            _library.AddRecord(record);
        }

        private static string GetEraDisplayName(StressEra era)
        {
            switch (era)
            {
                case StressEra.Pre1950:
                    return "Pre-1950 Design";
                case StressEra.From1950To1998:
                    return "1950-1998 Design";
                case StressEra.From1999Onward:
                    return "1999+ Design";
                default:
                    return "Unknown Era";
            }
        }

        public bool IsInitialized()
        {
            return _library.TotalRecordCount > 0;
        }

        public string GetStatusMessage()
        {
            var stats = _library.GetStatistics();
            return $"Material Stress Library: {stats.TotalRecords} records, {stats.UniqueMaterials} unique materials";
        }
    }
}
