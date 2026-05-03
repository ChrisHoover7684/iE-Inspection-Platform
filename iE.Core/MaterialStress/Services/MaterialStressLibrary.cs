using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using iE.Core.MaterialStress.Models;

namespace iE.Core.MaterialStress.Services
{
    public class MaterialStressLibrary
    {
        private readonly Dictionary<string, List<MaterialStressRecord>> _recordsByCompositeKey;
        private readonly Dictionary<string, List<MaterialStressRecord>> _recordsByWeightedKey;
        private readonly List<StressDataset> _datasets;
        private readonly ReaderWriterLockSlim _lock;

        public IReadOnlyList<StressDataset> Datasets
        {
            get { return _datasets.AsReadOnly(); }
        }

        public int TotalRecordCount { get; private set; }

        public MaterialStressLibrary()
        {
            _recordsByCompositeKey = new Dictionary<string, List<MaterialStressRecord>>();
            _recordsByWeightedKey = new Dictionary<string, List<MaterialStressRecord>>();
            _datasets = new List<StressDataset>();
            _lock = new ReaderWriterLockSlim();
            TotalRecordCount = 0;
        }

        public StressLookupResult Lookup(StressLookupRequest request)
        {
            if (request == null)
                return StressLookupResult.NotFound("Request is null");

            var material = new MaterialIdentity
            {
                SpecNo = request.SpecNo ?? "",
                TypeGrade = request.TypeGrade ?? "",
                ProductForm = request.ProductForm ?? "",
                AlloyUNS = request.AlloyUNS ?? "",
                ClassConditionTemper = request.ClassConditionTemper ?? ""
            };

            // 1. Try exact match
            var candidates = FindByExactKey(material)
                .Where(r => r.Dataset.Era == request.Era)
                .ToList();

            // 2. Fallback to weighted match
            if (!candidates.Any() && request.AllowPartialMatch)
            {
                candidates = FindByWeightedKey(material)
                    .Where(r => r.Dataset.Era == request.Era)
                    .ToList();
            }

            if (!candidates.Any())
                return StressLookupResult.NotFound("No matching material found");

            // Pick first match (you can refine later)
            var record = candidates.First();

            bool wasExtrapolated;
            double? lowerTemp, lowerStress, upperTemp, upperStress;

            var stress = MaterialStressInterpolator.InterpolateStress(
                record.AllowableStressByTemperature,
                request.Temperature,
                request.AllowExtrapolation,
                request.MaxExtrapolationDegrees,
                out wasExtrapolated,
                out lowerTemp,
                out lowerStress,
                out upperTemp,
                out upperStress
            );

            if (!stress.HasValue)
                return StressLookupResult.NotFound("No stress value available for temperature");

            bool wasInterpolated = lowerTemp.HasValue && upperTemp.HasValue && lowerTemp != upperTemp;

            var result = StressLookupResult.Success(
                stress.Value,
                request.Temperature,
                record,
                wasInterpolated,
                wasExtrapolated
            );

            result.LowerBoundTemperature = lowerTemp;
            result.LowerBoundStressPsi = lowerStress.HasValue ? Math.Round(lowerStress.Value * 1000.0, 0) : null;
            result.UpperBoundTemperature = upperTemp;
            result.UpperBoundStressPsi = upperStress.HasValue ? Math.Round(upperStress.Value * 1000.0, 0) : null;

            return result;
        }

        public void AddRecord(MaterialStressRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");

            _lock.EnterWriteLock();
            try
            {
                bool datasetExists = false;
                foreach (var d in _datasets)
                {
                    if (d.Era == record.Dataset.Era)
                    {
                        datasetExists = true;
                        break;
                    }
                }
                if (!datasetExists)
                {
                    _datasets.Add(record.Dataset);
                }

                string compositeKey = record.Material.GetCompositeKey();
                if (!_recordsByCompositeKey.ContainsKey(compositeKey))
                    _recordsByCompositeKey[compositeKey] = new List<MaterialStressRecord>();
                _recordsByCompositeKey[compositeKey].Add(record);

                string weightedKey = record.Material.GetWeightedKey();
                if (!_recordsByWeightedKey.ContainsKey(weightedKey))
                    _recordsByWeightedKey[weightedKey] = new List<MaterialStressRecord>();
                _recordsByWeightedKey[weightedKey].Add(record);

                TotalRecordCount++;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddRecords(IEnumerable<MaterialStressRecord> records)
        {
            foreach (var record in records)
            {
                AddRecord(record);
            }
        }

        public IReadOnlyList<MaterialStressRecord> FindByExactKey(MaterialIdentity material)
        {
            string key = material.GetCompositeKey();
            _lock.EnterReadLock();
            try
            {
                if (_recordsByCompositeKey.ContainsKey(key))
                {
                    return _recordsByCompositeKey[key].AsReadOnly();
                }
                return new List<MaterialStressRecord>().AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IReadOnlyList<MaterialStressRecord> FindByWeightedKey(MaterialIdentity material)
        {
            string key = material.GetWeightedKey();
            _lock.EnterReadLock();
            try
            {
                if (_recordsByWeightedKey.ContainsKey(key))
                {
                    return _recordsByWeightedKey[key].AsReadOnly();
                }
                return new List<MaterialStressRecord>().AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IReadOnlyList<MaterialStressRecord> FindBySpecNo(string specNo)
        {
            if (string.IsNullOrWhiteSpace(specNo))
                return new List<MaterialStressRecord>().AsReadOnly();

            string upperSpec = specNo.ToUpperInvariant();
            _lock.EnterReadLock();
            try
            {
                var results = new List<MaterialStressRecord>();
                foreach (var kvp in _recordsByCompositeKey)
                {
                    if (kvp.Key.Contains(upperSpec))
                    {
                        results.AddRange(kvp.Value);
                    }
                }
                return results.Distinct().ToList().AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public IReadOnlyList<MaterialStressRecord> GetRecordsByEra(StressEra era)
        {
            _lock.EnterReadLock();
            try
            {
                var results = new List<MaterialStressRecord>();
                foreach (var kvp in _recordsByCompositeKey)
                {
                    foreach (var record in kvp.Value)
                    {
                        if (record.Dataset.Era == era)
                        {
                            results.Add(record);
                        }
                    }
                }
                return results.AsReadOnly();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                _recordsByCompositeKey.Clear();
                _recordsByWeightedKey.Clear();
                _datasets.Clear();
                TotalRecordCount = 0;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public LibraryStatistics GetStatistics()
        {
            _lock.EnterReadLock();
            try
            {
                var stats = new LibraryStatistics();
                stats.TotalRecords = TotalRecordCount;
                stats.UniqueMaterials = _recordsByCompositeKey.Count;
                stats.RecordsByEra = new Dictionary<StressEra, int>();

                foreach (StressEra era in Enum.GetValues(typeof(StressEra)))
                {
                    stats.RecordsByEra[era] = 0;
                }

                foreach (var kvp in _recordsByCompositeKey)
                {
                    foreach (var record in kvp.Value)
                    {
                        if (stats.RecordsByEra.ContainsKey(record.Dataset.Era))
                            stats.RecordsByEra[record.Dataset.Era]++;
                        else
                            stats.RecordsByEra[record.Dataset.Era] = 1;
                    }
                }

                return stats;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    public class LibraryStatistics
    {
        public int TotalRecords { get; set; }
        public int UniqueMaterials { get; set; }
        public Dictionary<StressEra, int> RecordsByEra { get; set; }

    }
}