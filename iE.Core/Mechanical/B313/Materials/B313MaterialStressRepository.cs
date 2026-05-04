using iE.Core.MaterialStress.Importers;
using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.Mechanical.B313.Materials;

public sealed class B313MaterialStressRepository
{
    private readonly MaterialStressService _service;

    public B313MaterialStressRepository()
    {
        var library = new MaterialStressLibrary();
        _service = new MaterialStressService(library);
        B313StressImporter.Import(_service);
    }

    public StressLookupResult FindAllowableStress(string spec, string grade, string productForm, string unsNo, string classConditionTemper, double temperatureF)
        => _service.GetAllowableStress(new StressLookupRequest
        {
            Era = StressEra.From1999Onward,
            DesignCode = "B31_3",
            CalculationFamily = "Piping",
            SpecNo = spec,
            TypeGrade = grade,
            ProductForm = productForm,
            AlloyUNS = unsNo,
            ClassConditionTemper = classConditionTemper,
            Temperature = temperatureF,
            AllowPartialMatch = true
        });

    public IReadOnlyList<MaterialStressRecord> GetB313Records()
        => _service.Library.GetRecordsByEra(StressEra.From1999Onward)
            .Where(r => NormalizeKey(r.Dataset.DesignCode) == "B313"
                     && NormalizeKey(r.Dataset.CalculationFamily) == "PIPING")
            .ToList();

    private static string NormalizeKey(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return value.Trim().ToUpperInvariant()
            .Replace(" ", string.Empty)
            .Replace("_", string.Empty)
            .Replace("-", string.Empty)
            .Replace(".", string.Empty);
    }
}
