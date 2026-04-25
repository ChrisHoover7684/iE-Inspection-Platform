using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter
    {
        public static void ImportAll(MaterialStressService service)
        {
            OldStressDataImporter_Batch001.ImportBatch001(service);
            OldStressDataImporter_Batch002.ImportBatch002(service);
            OldStressDataImporter_Batch003.ImportBatch003(service);
            OldStressDataImporter_Batch004.ImportBatch004(service);
            OldStressDataImporter_Batch005.ImportBatch005(service);
            OldStressDataImporter_Batch006.ImportBatch006(service);
            OldStressDataImporter_Batch007.ImportBatch007(service);
            OldStressDataImporter_Batch008.ImportBatch008(service);
            OldStressDataImporter_Batch009.ImportBatch009(service);
            OldStressDataImporter_Batch010.ImportBatch010(service);
            OldStressDataImporter_Batch011.ImportBatch011(service);
            OldStressDataImporter_Batch012.ImportBatch012(service);
            OldStressDataImporter_Batch013.ImportBatch013(service);
            OldStressDataImporter_Batch014.ImportBatch014(service);
            OldStressDataImporter_Batch015.ImportBatch015(service);
            OldStressDataImporter_Batch016.ImportBatch016(service);
        }
    }
}
