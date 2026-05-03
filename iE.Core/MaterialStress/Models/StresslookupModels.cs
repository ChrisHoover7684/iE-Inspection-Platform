using System;

namespace iE.Core.MaterialStress.Models
{
    /// <summary>
    /// Request model for stress lookup
    /// </summary>
    public class StressLookupRequest
    {
        public string CalculationFamily { get; set; } = ""; // Piping, PressureVessel, Tanks
        public string DesignCode { get; set; } = "ASME_VIII_DIV1";

        public StressEra Era { get; set; }
        public string SpecNo { get; set; } = "";
        public string TypeGrade { get; set; } = "";
        public string ProductForm { get; set; } = "";
        public string AlloyUNS { get; set; } = "";
        public string ClassConditionTemper { get; set; } = "";
        public double Temperature { get; set; }
        public bool AllowPartialMatch { get; set; }
        public bool StrictMode { get; set; }
        public bool AllowExtrapolation { get; set; }
        public double MaxExtrapolationDegrees { get; set; }


        public StressLookupRequest()
        {
            CalculationFamily = "";
            DesignCode = "ASME_VIII_DIV1";
            AllowPartialMatch = false;
            StrictMode = false;
            AllowExtrapolation = false;
            MaxExtrapolationDegrees = 50;
        }
    }

    /// <summary>
    /// Result model for stress lookup
    /// </summary>
public class StressLookupResult
    {
        public bool Found { get; set; }
        public double? AllowableStress { get; set; }
        public double TemperatureUsed { get; set; }
        public double DesignMargin { get; set; }
        public string Message { get; set; }
        public MaterialStressRecord Record { get; set; }

        public double? LowerBoundTemperature { get; set; }
        public double? LowerBoundStressPsi { get; set; }
        public double? UpperBoundTemperature { get; set; }
        public double? UpperBoundStressPsi { get; set; }
        public bool WasInterpolated { get; set; }
        public bool WasExtrapolated { get; set; }
        public double? AllowableStressKsi { get; set; }
        public double? AllowableStressPsi { get; set; }
        public string DisplayStress { get; set; } = "";

        public StressLookupResult()
        {
            Message = "";
            Found = false;
        }

        public static StressLookupResult NotFound(string message)
        {
            return new StressLookupResult
            {
                Found = false,
                Message = message,
                DesignMargin = 0
            };
        }


public static StressLookupResult Success(double stressKsi, double tempUsed, MaterialStressRecord record, bool interpolated = false, bool extrapolated = false)
{
    var result = new StressLookupResult
    {
        Found = true,
        AllowableStress = Math.Round(stressKsi * 1000.0, 0),
        AllowableStressKsi = Math.Round(stressKsi, 3),
        AllowableStressPsi = Math.Round(stressKsi * 1000.0, 0),
        DisplayStress = $"{stressKsi * 1000.0:N0} psi",

        TemperatureUsed = tempUsed,
        DesignMargin = record.Dataset.DesignMargin,
        Record = record,
        WasInterpolated = interpolated,
        WasExtrapolated = extrapolated
    };

    if (interpolated)
        result.Message = "Interpolated between temperature points";
    else if (extrapolated)
        result.Message = "Extrapolated beyond temperature range";
    else
        result.Message = "Exact match found";

    return result;
}
    }
}
