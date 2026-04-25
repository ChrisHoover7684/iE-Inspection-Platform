using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter
    {
        public static void ImportAll(MaterialStressService service)
        {
            NewStressDataImporter_Batch001.ImportBatch001(service);
            NewStressDataImporter_Batch002.ImportBatch002(service);
            NewStressDataImporter_Batch003.ImportBatch003(service);
            NewStressDataImporter_Batch004.ImportBatch004(service);
            NewStressDataImporter_Batch005.ImportBatch005(service);
            NewStressDataImporter_Batch006.ImportBatch006(service);
            NewStressDataImporter_Batch007.ImportBatch007(service);
            NewStressDataImporter_Batch008.ImportBatch008(service);
            NewStressDataImporter_Batch009.ImportBatch009(service);
            NewStressDataImporter_Batch010.ImportBatch010(service);
            NewStressDataImporter_Batch011.ImportBatch011(service);
            NewStressDataImporter_Batch012.ImportBatch012(service);
            NewStressDataImporter_Batch013.ImportBatch013(service);
            NewStressDataImporter_Batch014.ImportBatch014(service);
        }
    }
}