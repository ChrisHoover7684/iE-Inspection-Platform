using iE.Core.MaterialStress.Importers;

namespace iE.Core.MaterialStress.Services;

public static class MaterialStressServiceFactory
{
    public static MaterialStressService CreateInitialized()
    {
        var service = new MaterialStressService();
        NewStressDataImporter.ImportAll(service);
        OldStressDataImporter.ImportAll(service);
        return service;
    }
}
