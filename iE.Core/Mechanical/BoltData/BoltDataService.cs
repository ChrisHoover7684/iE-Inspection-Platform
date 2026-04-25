using System;
using System.Collections.Generic;
using System.Linq;

namespace iE.Core.Mechanical.BoltData
{
    public class BoltDataService
    {
        public BoltDataResult Lookup(BoltDataInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FlangeSize))
                throw new Exception("Flange size is required.");

            if (string.IsNullOrWhiteSpace(input.FlangeRating))
                throw new Exception("Flange rating is required.");

            string size = NormalizeSize(input.FlangeSize);
            string rating = NormalizeRating(input.FlangeRating);

            if (!BoltDataRepository.BoltData.TryGetValue(size, out var ratingData))
                throw new Exception($"Flange size '{input.FlangeSize}' was not found.");

            if (!ratingData.TryGetValue(rating, out BoltInfo? boltInfo))
                throw new Exception($"Flange rating '{input.FlangeRating}' was not found for size {input.FlangeSize}.");

            return new BoltDataResult
            {
                FlangeSize = size,
                FlangeRating = rating,

                NumberOfBolts = boltInfo.NumberOfBolts,
                BoltDiameter = boltInfo.BoltDiameter,
                StudBoltLengthRF = boltInfo.StudBoltLengthRF,
                StudBoltLengthMFTG = boltInfo.StudBoltLengthMFTG,
                StudBoltLengthRJ = boltInfo.StudBoltLengthRJ,
                BoltCircleDiameter = boltInfo.BoltCircleDiameter,
                FlangeDiameter = boltInfo.FlangeDiameter,
                BoltHoleDiameter = boltInfo.BoltHoleDiameter,

                Display =
                    $"Flange Size: {size}\" \n" +
                    $"Flange Rating: {rating}\n" +
                    $"Number of Bolts: {boltInfo.NumberOfBolts}\n" +
                    $"Bolt Diameter: {boltInfo.BoltDiameter}\n" +
                    $"Stud Bolt Length RF: {boltInfo.StudBoltLengthRF}\n" +
                    $"Stud Bolt Length MF/TG: {boltInfo.StudBoltLengthMFTG}\n" +
                    $"Stud Bolt Length RJ: {boltInfo.StudBoltLengthRJ}\n" +
                    $"Bolt Circle: {boltInfo.BoltCircleDiameter}\n" +
                    $"Flange Diameter: {boltInfo.FlangeDiameter}\n" +
                    $"Bolt Hole: {boltInfo.BoltHoleDiameter}"
            };
        }

        public List<string> GetFlangeSizes()
        {
            return BoltDataRepository.BoltData.Keys
                .OrderBy(SizeSortValue)
                .ToList();
        }

        public List<string> GetRatingsForSize(string flangeSize)
        {
            string size = NormalizeSize(flangeSize);

            if (!BoltDataRepository.BoltData.TryGetValue(size, out var ratingData))
                throw new Exception($"Flange size '{flangeSize}' was not found.");

            return ratingData.Keys
                .OrderBy(r => int.TryParse(r, out int n) ? n : int.MaxValue)
                .ToList();
        }

        private string NormalizeSize(string value)
        {
            return value
                .Replace("\"", "")
                .Trim();
        }

        private string NormalizeRating(string value)
        {
            return value
                .Replace("#", "")
                .Replace("Class", "", StringComparison.OrdinalIgnoreCase)
                .Trim();
        }

        private double SizeSortValue(string size)
        {
            string normalized = NormalizeSize(size);

            return normalized switch
            {
                "1/2" => 0.5,
                "3/4" => 0.75,
                "1-1/4" => 1.25,
                "1-1/2" => 1.5,
                "2-1/2" => 2.5,
                _ => double.TryParse(normalized, out double n) ? n : double.MaxValue
            };
        }
    }
}