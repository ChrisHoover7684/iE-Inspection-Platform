using iE.Core.MaterialStress.Models;

namespace iE.Core.MaterialStress.Services;

public sealed class SharedStressLookupService
{
    private readonly MaterialStressService _materialStressService;

    public SharedStressLookupService(MaterialStressService materialStressService)
    {
        _materialStressService = materialStressService ?? throw new ArgumentNullException(nameof(materialStressService));
    }

    public StressLookupResult GetAllowableStress(SharedStressLookupRequest request)
    {
        if (request.CalculationFamily is CalculationFamily.Piping or CalculationFamily.Tanks)
        {
            return StressLookupResult.NotFound($"{request.CalculationFamily} stress table not loaded/available yet.");
        }

        if (request.CalculationFamily != CalculationFamily.PressureVessel)
        {
            return StressLookupResult.NotFound($"Unsupported calculation family '{request.CalculationFamily}'.");
        }

        if (!string.Equals(request.DesignCode?.Trim(), "ASME_VIII_DIV1", StringComparison.OrdinalIgnoreCase))
        {
            return StressLookupResult.NotFound("PressureVessel lookup currently requires DesignCode = ASME_VIII_DIV1.");
        }

        return _materialStressService.GetAllowableStress(new StressLookupRequest
        {
            CalculationFamily = CalculationFamily.PressureVessel.ToString(),
            DesignCode = "ASME_VIII_DIV1",
            Era = request.Era,
            SpecNo = request.SpecNo,
            TypeGrade = request.TypeGrade,
            ProductForm = request.ProductForm,
            AlloyUNS = request.AlloyUNS,
            ClassConditionTemper = request.ClassConditionTemper,
            Temperature = request.TemperatureF,
            AllowPartialMatch = request.AllowPartialMatch,
            StrictMode = request.StrictMode,
            AllowExtrapolation = request.AllowExtrapolation,
            MaxExtrapolationDegrees = request.MaxExtrapolationDegrees
        });
    }
}
