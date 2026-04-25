using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch009
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch009(MaterialStressService service)
        {
            var batchData = GetBatchData();

            foreach (var data in batchData)
            {
                var stressTable = BuildStressTable(data.StressValues);

                service.ImportRecord(
                    StressEra.From1950To1998,
                    4.0,
                    data.SpecNo,
                    data.TypeGrade,
                    data.ProductForm,
                    data.AlloyUNS,
                    data.ClassCondition,
                    stressTable,
                    data.Notes,
                    data.LineNo,
                    data.MaterialId
                );
            }
        }

        private static SortedDictionary<double, double?> BuildStressTable(double?[] values)
        {
            var dict = new SortedDictionary<double, double?>();

            for (int i = 0; i < Temperatures.Length && i < values.Length; i++)
            {
                dict[Temperatures[i]] = values[i];
            }

            return dict;
        }

        private static List<OldStressRowData> GetBatchData()
        {
            var batch = new List<OldStressRowData>();

            // Row 26: SA-430, FP316N, Forged pipe
            var row801 = new OldStressRowData();
            row801.LineNo = 26;
            row801.MaterialId = 9040;
            row801.SpecNo = "SA-430";
            row801.TypeGrade = "FP316N";
            row801.ProductForm = "Forged pipe";
            row801.AlloyUNS = "S31651";
            row801.ClassCondition = "";
            row801.Notes = "G12";
            row801.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row801);

            // Row 27: SA-182, F316N, Forgings
            var row802 = new OldStressRowData();
            row802.LineNo = 27;
            row802.MaterialId = 9042;
            row802.SpecNo = "SA-182";
            row802.TypeGrade = "F316N";
            row802.ProductForm = "Forgings";
            row802.AlloyUNS = "S31651";
            row802.ClassCondition = "";
            row802.Notes = "G5";
            row802.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row802);

            // Row 28: SA-213, TP316N, Smls. tube
            var row803 = new OldStressRowData();
            row803.LineNo = 28;
            row803.MaterialId = 9043;
            row803.SpecNo = "SA-213";
            row803.TypeGrade = "TP316N";
            row803.ProductForm = "Smls. tube";
            row803.AlloyUNS = "S31651";
            row803.ClassCondition = "";
            row803.Notes = "G5, G12, G18";
            row803.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row803);

            // Row 29: SA-213, TP316N, Smls. tube
            var row804 = new OldStressRowData();
            row804.LineNo = 29;
            row804.MaterialId = 9044;
            row804.SpecNo = "SA-213";
            row804.TypeGrade = "TP316N";
            row804.ProductForm = "Smls. tube";
            row804.AlloyUNS = "S31651";
            row804.ClassCondition = "";
            row804.Notes = "G12, G18";
            row804.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row804);

            // Row 30: SA-240, 316N, Plate
            var row805 = new OldStressRowData();
            row805.LineNo = 30;
            row805.MaterialId = 9045;
            row805.SpecNo = "SA-240";
            row805.TypeGrade = "316N";
            row805.ProductForm = "Plate";
            row805.AlloyUNS = "S31651";
            row805.ClassCondition = "";
            row805.Notes = "G5, G12";
            row805.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row805);

            // Row 31: SA-240, 316N, Plate
            var row806 = new OldStressRowData();
            row806.LineNo = 31;
            row806.MaterialId = 9046;
            row806.SpecNo = "SA-240";
            row806.TypeGrade = "316N";
            row806.ProductForm = "Plate";
            row806.AlloyUNS = "S31651";
            row806.ClassCondition = "";
            row806.Notes = "G12";
            row806.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row806);

            // Row 32: SA-249, TP316N, Wld. tube
            var row807 = new OldStressRowData();
            row807.LineNo = 32;
            row807.MaterialId = 9047;
            row807.SpecNo = "SA-249";
            row807.TypeGrade = "TP316N";
            row807.ProductForm = "Wld. tube";
            row807.AlloyUNS = "S31651";
            row807.ClassCondition = "";
            row807.Notes = "G5, G12, G18, W14";
            row807.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row807);

            // Row 33: SA-249, TP316N, Wld. tube
            var row808 = new OldStressRowData();
            row808.LineNo = 33;
            row808.MaterialId = 9050;
            row808.SpecNo = "SA-249";
            row808.TypeGrade = "TP316N";
            row808.ProductForm = "Wld. tube";
            row808.AlloyUNS = "S31651";
            row808.ClassCondition = "";
            row808.Notes = "G12, G18, W14";
            row808.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row808);

            // Row 34: SA-249, TP316N, Wld. tube
            var row809 = new OldStressRowData();
            row809.LineNo = 34;
            row809.MaterialId = 9051;
            row809.SpecNo = "SA-249";
            row809.TypeGrade = "TP316N";
            row809.ProductForm = "Wld. tube";
            row809.AlloyUNS = "S31651";
            row809.ClassCondition = "";
            row809.Notes = "G3, G12, G18";
            row809.StressValues = new double?[] { 17, null, 17, null, 16.2, 15, 14, 13.3, 12.9, 12.7, 12.3, 12.1, 11.8, 11.6, 11.5, 11.2, 10.8, 10.4, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row809);

            // Row 35: SA-249, TP316N, Wld. tube
            var row810 = new OldStressRowData();
            row810.LineNo = 35;
            row810.MaterialId = 9048;
            row810.SpecNo = "SA-249";
            row810.TypeGrade = "TP316N";
            row810.ProductForm = "Wld. tube";
            row810.AlloyUNS = "S31651";
            row810.ClassCondition = "";
            row810.Notes = "G3, G5, G12, G18, G24";
            row810.StressValues = new double?[] { 17, null, 17, null, 16.3, 16, 15.8, 15.8, 15.8, 15.8, 15.7, 15.6, 15.6, 15.4, 15.1, 14.8, 13.4, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row810);

            // Row 36: SA-249, TP316N, Wld. tube
            var row811 = new OldStressRowData();
            row811.LineNo = 36;
            row811.MaterialId = 9049;
            row811.SpecNo = "SA-249";
            row811.TypeGrade = "TP316N";
            row811.ProductForm = "Wld. tube";
            row811.AlloyUNS = "S31651";
            row811.ClassCondition = "";
            row811.Notes = "G12, G24";
            row811.StressValues = new double?[] { 17, null, 17, null, 16.2, 15, 14, 13.3, 12.9, 12.7, 12.3, 12.1, 11.8, 11.6, 11.5, 11.2, 10.8, 10.4, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row811);

            // Row 37: SA-312, TP316N, Smls. pipe
            var row812 = new OldStressRowData();
            row812.LineNo = 37;
            row812.MaterialId = 9052;
            row812.SpecNo = "SA-312";
            row812.TypeGrade = "TP316N";
            row812.ProductForm = "Smls. pipe";
            row812.AlloyUNS = "S31651";
            row812.ClassCondition = "";
            row812.Notes = "G5, G12, G18";
            row812.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row812);

            // Row 38: SA-312, TP316N, Smls. pipe
            var row813 = new OldStressRowData();
            row813.LineNo = 38;
            row813.MaterialId = 9054;
            row813.SpecNo = "SA-312";
            row813.TypeGrade = "TP316N";
            row813.ProductForm = "Smls. pipe";
            row813.AlloyUNS = "S31651";
            row813.ClassCondition = "";
            row813.Notes = "G12, G18";
            row813.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row813);

            // Row 39: SA-312, TP316N, Wld. & smls. tube
            var row814 = new OldStressRowData();
            row814.LineNo = 39;
            row814.MaterialId = 9053;
            row814.SpecNo = "SA-312";
            row814.TypeGrade = "TP316N";
            row814.ProductForm = "Wld. & smls. tube";
            row814.AlloyUNS = "S31651";
            row814.ClassCondition = "";
            row814.Notes = "G5, G12, W12";
            row814.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row814);

            // Row 1: SA-312, TP316N, Wld. pipe
            var row815 = new OldStressRowData();
            row815.LineNo = 1;
            row815.MaterialId = 9055;
            row815.SpecNo = "SA-312";
            row815.TypeGrade = "TP316N";
            row815.ProductForm = "Wld. pipe";
            row815.AlloyUNS = "S31651";
            row815.ClassCondition = "";
            row815.Notes = "G5, G12, G18, W14";
            row815.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row815);

            // Row 2: SA-312, TP316N, Wld. pipe
            var row816 = new OldStressRowData();
            row816.LineNo = 2;
            row816.MaterialId = 9058;
            row816.SpecNo = "SA-312";
            row816.TypeGrade = "TP316N";
            row816.ProductForm = "Wld. pipe";
            row816.AlloyUNS = "S31651";
            row816.ClassCondition = "";
            row816.Notes = "G12, G18, W14";
            row816.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row816);

            // Row 3: SA-312, TP316N, Wld. pipe
            var row817 = new OldStressRowData();
            row817.LineNo = 3;
            row817.MaterialId = 9059;
            row817.SpecNo = "SA-312";
            row817.TypeGrade = "TP316N";
            row817.ProductForm = "Wld. pipe";
            row817.AlloyUNS = "S31651";
            row817.ClassCondition = "";
            row817.Notes = "G3, G12, G18";
            row817.StressValues = new double?[] { 17, null, 17, null, 16.2, 15, 14, 13.3, 12.9, 12.7, 12.3, 12.1, 11.8, 11.6, 11.5, 11.2, 10.8, 10.4, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row817);

            // Row 4: SA-312, TP316N, Wld. pipe
            var row818 = new OldStressRowData();
            row818.LineNo = 4;
            row818.MaterialId = 9056;
            row818.SpecNo = "SA-312";
            row818.TypeGrade = "TP316N";
            row818.ProductForm = "Wld. pipe";
            row818.AlloyUNS = "S31651";
            row818.ClassCondition = "";
            row818.Notes = "G3, G5, G12, G18, G24";
            row818.StressValues = new double?[] { 17, null, 17, null, 16.3, 16, 15.8, 15.8, 15.8, 15.8, 15.7, 15.6, 15.6, 15.4, 15.1, 14.8, 13.4, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row818);

            // Row 5: SA-312, TP316N, Wld. pipe
            var row819 = new OldStressRowData();
            row819.LineNo = 5;
            row819.MaterialId = 9057;
            row819.SpecNo = "SA-312";
            row819.TypeGrade = "TP316N";
            row819.ProductForm = "Wld. pipe";
            row819.AlloyUNS = "S31651";
            row819.ClassCondition = "";
            row819.Notes = "G12, G24";
            row819.StressValues = new double?[] { 17, null, 17, null, 16.2, 15, 14, 13.3, 12.9, 12.7, 12.3, 12.1, 11.8, 11.6, 11.5, 11.2, 10.8, 10.4, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row819);

            // Row 6: SA-336, F316N, Forgings
            var row820 = new OldStressRowData();
            row820.LineNo = 6;
            row820.MaterialId = 9060;
            row820.SpecNo = "SA-336";
            row820.TypeGrade = "F316N";
            row820.ProductForm = "Forgings";
            row820.AlloyUNS = "S31651";
            row820.ClassCondition = "";
            row820.Notes = "G5, G12";
            row820.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row820);

            // Row 7: SA-336, F316N, Forgings
            var row821 = new OldStressRowData();
            row821.LineNo = 7;
            row821.MaterialId = 9061;
            row821.SpecNo = "SA-336";
            row821.TypeGrade = "F316N";
            row821.ProductForm = "Forgings";
            row821.AlloyUNS = "S31651";
            row821.ClassCondition = "";
            row821.Notes = "G12";
            row821.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row821);

            // Row 8: SA-358, 316N, Wld. pipe
            var row822 = new OldStressRowData();
            row822.LineNo = 8;
            row822.MaterialId = 9062;
            row822.SpecNo = "SA-358";
            row822.TypeGrade = "316N";
            row822.ProductForm = "Wld. pipe";
            row822.AlloyUNS = "S31651";
            row822.ClassCondition = "1";
            row822.Notes = "G5, W12";
            row822.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row822);

            // Row 9: SA-376, TP316N, Smls. pipe
            var row823 = new OldStressRowData();
            row823.LineNo = 9;
            row823.MaterialId = 9063;
            row823.SpecNo = "SA-376";
            row823.TypeGrade = "TP316N";
            row823.ProductForm = "Smls. pipe";
            row823.AlloyUNS = "S31651";
            row823.ClassCondition = "";
            row823.Notes = "G5, G12, G18, H1";
            row823.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row823);

            // Row 10: SA-376, TP316N, Smls. pipe
            var row824 = new OldStressRowData();
            row824.LineNo = 10;
            row824.MaterialId = 9064;
            row824.SpecNo = "SA-376";
            row824.TypeGrade = "TP316N";
            row824.ProductForm = "Smls. pipe";
            row824.AlloyUNS = "S31651";
            row824.ClassCondition = "";
            row824.Notes = "G12, G18, H1";
            row824.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row824);

            // Row 11: ..., , 
            var row825 = new OldStressRowData();
            row825.LineNo = 11;
            row825.MaterialId = 9066;
            row825.SpecNo = "...";
            row825.TypeGrade = "";
            row825.ProductForm = "";
            row825.AlloyUNS = "";
            row825.ClassCondition = "";
            row825.Notes = "";
            row825.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row825);

            // Row 12: ..., , 
            var row826 = new OldStressRowData();
            row826.LineNo = 12;
            row826.MaterialId = 9065;
            row826.SpecNo = "...";
            row826.TypeGrade = "";
            row826.ProductForm = "";
            row826.AlloyUNS = "";
            row826.ClassCondition = "";
            row826.Notes = "";
            row826.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row826);

            // Row 13: SA-403, 316N, Smls. & wld. fittings
            var row827 = new OldStressRowData();
            row827.LineNo = 13;
            row827.MaterialId = 9070;
            row827.SpecNo = "SA-403";
            row827.TypeGrade = "316N";
            row827.ProductForm = "Smls. & wld. fittings";
            row827.AlloyUNS = "S31651";
            row827.ClassCondition = "";
            row827.Notes = "G5, G12, W12, W15";
            row827.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row827);

            // Row 14: ..., , 
            var row828 = new OldStressRowData();
            row828.LineNo = 14;
            row828.MaterialId = 9067;
            row828.SpecNo = "...";
            row828.TypeGrade = "";
            row828.ProductForm = "";
            row828.AlloyUNS = "";
            row828.ClassCondition = "";
            row828.Notes = "";
            row828.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row828);

            // Row 15: ..., , 
            var row829 = new OldStressRowData();
            row829.LineNo = 15;
            row829.MaterialId = 9068;
            row829.SpecNo = "...";
            row829.TypeGrade = "";
            row829.ProductForm = "";
            row829.AlloyUNS = "";
            row829.ClassCondition = "";
            row829.Notes = "";
            row829.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row829);

            // Row 16: ..., , 
            var row830 = new OldStressRowData();
            row830.LineNo = 16;
            row830.MaterialId = 9069;
            row830.SpecNo = "...";
            row830.TypeGrade = "";
            row830.ProductForm = "";
            row830.AlloyUNS = "";
            row830.ClassCondition = "";
            row830.Notes = "";
            row830.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row830);

            // Row 17: SA-479, 316N, Bar
            var row831 = new OldStressRowData();
            row831.LineNo = 17;
            row831.MaterialId = 9071;
            row831.SpecNo = "SA-479";
            row831.TypeGrade = "316N";
            row831.ProductForm = "Bar";
            row831.AlloyUNS = "S31651";
            row831.ClassCondition = "";
            row831.Notes = "G5, G12, G18, H1";
            row831.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, 18.3, 18.1, 17.8, 17.4, 15.8, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row831);

            // Row 18: SA-479, 316N, Bar
            var row832 = new OldStressRowData();
            row832.LineNo = 18;
            row832.MaterialId = 9072;
            row832.SpecNo = "SA-479";
            row832.TypeGrade = "316N";
            row832.ProductForm = "Bar";
            row832.AlloyUNS = "S31651";
            row832.ClassCondition = "";
            row832.Notes = "G12, G18, H1";
            row832.StressValues = new double?[] { 20, null, 20, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row832);

            // Row 19: SA-688, TP316N, Wld. tube
            var row833 = new OldStressRowData();
            row833.LineNo = 19;
            row833.MaterialId = 9073;
            row833.SpecNo = "SA-688";
            row833.TypeGrade = "TP316N";
            row833.ProductForm = "Wld. tube";
            row833.AlloyUNS = "S31651";
            row833.ClassCondition = "";
            row833.Notes = "G5, W12";
            row833.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row833);

            // Row 20: SA-813, TP316N, Wld. pipe
            var row834 = new OldStressRowData();
            row834.LineNo = 20;
            row834.MaterialId = 9074;
            row834.SpecNo = "SA-813";
            row834.TypeGrade = "TP316N";
            row834.ProductForm = "Wld. pipe";
            row834.AlloyUNS = "S31651";
            row834.ClassCondition = "";
            row834.Notes = "G5, W12";
            row834.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row834);

            // Row 21: SA-814, TP316N, Wld. pipe
            var row835 = new OldStressRowData();
            row835.LineNo = 21;
            row835.MaterialId = 9075;
            row835.SpecNo = "SA-814";
            row835.TypeGrade = "TP316N";
            row835.ProductForm = "Wld. pipe";
            row835.AlloyUNS = "S31651";
            row835.ClassCondition = "";
            row835.Notes = "G5, W12";
            row835.StressValues = new double?[] { 20, null, 20, null, 19.2, 18.8, 18.6, 18.6, 18.6, 18.6, 18.5, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row835);

            // Row 22: SA-240, 316Ti, Plate
            var row836 = new OldStressRowData();
            row836.LineNo = 22;
            row836.MaterialId = 9076;
            row836.SpecNo = "SA-240";
            row836.TypeGrade = "316Ti";
            row836.ProductForm = "Plate";
            row836.AlloyUNS = "S31635";
            row836.ClassCondition = "";
            row836.Notes = "G5, G12";
            row836.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row836);

            // Row 23: SA-240, 316Ti, Plate
            var row837 = new OldStressRowData();
            row837.LineNo = 23;
            row837.MaterialId = 9077;
            row837.SpecNo = "SA-240";
            row837.TypeGrade = "316Ti";
            row837.ProductForm = "Plate";
            row837.AlloyUNS = "S31635";
            row837.ClassCondition = "";
            row837.Notes = "G12";
            row837.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row837);

            // Row 24: SA-240, XM-29, Plate
            var row838 = new OldStressRowData();
            row838.LineNo = 24;
            row838.MaterialId = 9085;
            row838.SpecNo = "SA-240";
            row838.TypeGrade = "XM-29";
            row838.ProductForm = "Plate";
            row838.AlloyUNS = "S24000";
            row838.ClassCondition = "";
            row838.Notes = "G5";
            row838.StressValues = new double?[] { 25, null, 24.5, null, 22.6, 21.6, 21.4, 20.9, 20.6, 20, 19.5, 19, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row838);

            // Row 25: SA-240, XM-29, Plate
            var row839 = new OldStressRowData();
            row839.LineNo = 25;
            row839.MaterialId = 9086;
            row839.SpecNo = "SA-240";
            row839.TypeGrade = "XM-29";
            row839.ProductForm = "Plate";
            row839.AlloyUNS = "S24000";
            row839.ClassCondition = "";
            row839.Notes = "";
            row839.StressValues = new double?[] { 25, null, 24.5, null, 22.6, 20.4, 18.8, 18.1, 17.7, 17.3, 17, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row839);

            // Row 26: SA-249, XM-29, Wld. tube
            var row840 = new OldStressRowData();
            row840.LineNo = 26;
            row840.MaterialId = 9087;
            row840.SpecNo = "SA-249";
            row840.TypeGrade = "XM-29";
            row840.ProductForm = "Wld. tube";
            row840.AlloyUNS = "S24000";
            row840.ClassCondition = "";
            row840.Notes = "G5, G24";
            row840.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 18.3, 18.2, 17.8, 17.5, 17, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row840);

            // Row 27: SA-249, XM-29, Wld. tube
            var row841 = new OldStressRowData();
            row841.LineNo = 27;
            row841.MaterialId = 9088;
            row841.SpecNo = "SA-249";
            row841.TypeGrade = "XM-29";
            row841.ProductForm = "Wld. tube";
            row841.AlloyUNS = "S24000";
            row841.ClassCondition = "";
            row841.Notes = "G24";
            row841.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 17.3, 16, 15.4, 15, 14.7, 14.4, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row841);

            // Row 28: SA-312, XM-29, Wld. pipe
            var row842 = new OldStressRowData();
            row842.LineNo = 28;
            row842.MaterialId = 9089;
            row842.SpecNo = "SA-312";
            row842.TypeGrade = "XM-29";
            row842.ProductForm = "Wld. pipe";
            row842.AlloyUNS = "S24000";
            row842.ClassCondition = "";
            row842.Notes = "G5, G24";
            row842.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 18.3, 18.2, 17.8, 17.5, 17, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row842);

            // Row 29: SA-312, XM-29, Wld. pipe
            var row843 = new OldStressRowData();
            row843.LineNo = 29;
            row843.MaterialId = 9090;
            row843.SpecNo = "SA-312";
            row843.TypeGrade = "XM-29";
            row843.ProductForm = "Wld. pipe";
            row843.AlloyUNS = "S24000";
            row843.ClassCondition = "";
            row843.Notes = "G24";
            row843.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 17.3, 16, 15.4, 15, 14.7, 14.4, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row843);

            // Row 30: SA-479, XM-29, Bar
            var row844 = new OldStressRowData();
            row844.LineNo = 30;
            row844.MaterialId = 9091;
            row844.SpecNo = "SA-479";
            row844.TypeGrade = "XM-29";
            row844.ProductForm = "Bar";
            row844.AlloyUNS = "S24000";
            row844.ClassCondition = "";
            row844.Notes = "G5, G22";
            row844.StressValues = new double?[] { 25, null, 24.5, null, 22.6, 21.6, 21.4, 20.9, 20.6, 20, 19.5, 19, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row844);

            // Row 31: SA-479, XM-29, Bar
            var row845 = new OldStressRowData();
            row845.LineNo = 31;
            row845.MaterialId = 9092;
            row845.SpecNo = "SA-479";
            row845.TypeGrade = "XM-29";
            row845.ProductForm = "Bar";
            row845.AlloyUNS = "S24000";
            row845.ClassCondition = "";
            row845.Notes = "G22";
            row845.StressValues = new double?[] { 25, null, 24.5, null, 22.6, 20.4, 18.8, 18.1, 17.7, 17.3, 17, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row845);

            // Row 32: SA-688, TPXM-29, Wld. tube
            var row846 = new OldStressRowData();
            row846.LineNo = 32;
            row846.MaterialId = 9093;
            row846.SpecNo = "SA-688";
            row846.TypeGrade = "TPXM-29";
            row846.ProductForm = "Wld. tube";
            row846.AlloyUNS = "S24000";
            row846.ClassCondition = "";
            row846.Notes = "G5, G24";
            row846.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 18.3, 18.2, 17.8, 17.5, 17, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row846);

            // Row 33: SA-688, TPXM-29, Wld. tube
            var row847 = new OldStressRowData();
            row847.LineNo = 33;
            row847.MaterialId = 9094;
            row847.SpecNo = "SA-688";
            row847.TypeGrade = "TPXM-29";
            row847.ProductForm = "Wld. tube";
            row847.AlloyUNS = "S24000";
            row847.ClassCondition = "";
            row847.Notes = "G24";
            row847.StressValues = new double?[] { 21.2, null, 20.8, null, 19.2, 17.3, 16, 15.4, 15, 14.7, 14.4, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row847);

            // Row 34: SA-789, , Smls. tube
            var row848 = new OldStressRowData();
            row848.LineNo = 34;
            row848.MaterialId = 9095;
            row848.SpecNo = "SA-789";
            row848.TypeGrade = "";
            row848.ProductForm = "Smls. tube";
            row848.AlloyUNS = "S31500";
            row848.ClassCondition = "";
            row848.Notes = "G32";
            row848.StressValues = new double?[] { 23, null, 22.2, null, 21.3, 21.2, 21.2, 21.2, 21.2, 21.2, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row848);

            // Row 35: SA-789, , Wld. tube
            var row849 = new OldStressRowData();
            row849.LineNo = 35;
            row849.MaterialId = 9096;
            row849.SpecNo = "SA-789";
            row849.TypeGrade = "";
            row849.ProductForm = "Wld. tube";
            row849.AlloyUNS = "S31500";
            row849.ClassCondition = "";
            row849.Notes = "G24, G32";
            row849.StressValues = new double?[] { 19.6, null, 18.9, null, 18.1, 18, 18, 18, 18, 18, 18, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row849);

            // Row 36: SA-790, , Smls. tube
            var row850 = new OldStressRowData();
            row850.LineNo = 36;
            row850.MaterialId = 9097;
            row850.SpecNo = "SA-790";
            row850.TypeGrade = "";
            row850.ProductForm = "Smls. tube";
            row850.AlloyUNS = "S31500";
            row850.ClassCondition = "";
            row850.Notes = "G32";
            row850.StressValues = new double?[] { 23, null, 22.2, null, 21.3, 21.2, 21.2, 21.2, 21.2, 21.2, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row850);

            // Row 37: SA-790, , Wld. tube
            var row851 = new OldStressRowData();
            row851.LineNo = 37;
            row851.MaterialId = 9098;
            row851.SpecNo = "SA-790";
            row851.TypeGrade = "";
            row851.ProductForm = "Wld. tube";
            row851.AlloyUNS = "S31500";
            row851.ClassCondition = "";
            row851.Notes = "G24, G32";
            row851.StressValues = new double?[] { 19.6, null, 18.9, null, 18.1, 18, 18, 18, 18, 18, 18, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row851);

            // Row 38: SA-182, F304L, Forgings
            var row852 = new OldStressRowData();
            row852.LineNo = 38;
            row852.MaterialId = 9099;
            row852.SpecNo = "SA-182";
            row852.TypeGrade = "F304L";
            row852.ProductForm = "Forgings";
            row852.AlloyUNS = "S30403";
            row852.ClassCondition = "";
            row852.Notes = "G5";
            row852.StressValues = new double?[] { 16.3, null, 15.4, null, 14.2, 13.6, 13.4, 13.2, 13.1, 13.1, 13, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row852);

            // Row 39: SA-182, F304L, Forgings
            var row853 = new OldStressRowData();
            row853.LineNo = 39;
            row853.MaterialId = 9100;
            row853.SpecNo = "SA-182";
            row853.TypeGrade = "F304L";
            row853.ProductForm = "Forgings";
            row853.AlloyUNS = "S30403";
            row853.ClassCondition = "";
            row853.Notes = "";
            row853.StressValues = new double?[] { 16.3, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row853);

            // Row 40: SA-336, F304L, Forgings
            var row854 = new OldStressRowData();
            row854.LineNo = 40;
            row854.MaterialId = 9101;
            row854.SpecNo = "SA-336";
            row854.TypeGrade = "F304L";
            row854.ProductForm = "Forgings";
            row854.AlloyUNS = "S30403";
            row854.ClassCondition = "";
            row854.Notes = "";
            row854.StressValues = new double?[] { 16.3, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row854);

            // Row 1: SA-182, F304L, Forgings
            var row855 = new OldStressRowData();
            row855.LineNo = 1;
            row855.MaterialId = 9102;
            row855.SpecNo = "SA-182";
            row855.TypeGrade = "F304L";
            row855.ProductForm = "Forgings";
            row855.AlloyUNS = "S30403";
            row855.ClassCondition = "";
            row855.Notes = "G5";
            row855.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row855);

            // Row 2: SA-182, F304L, Forgings
            var row856 = new OldStressRowData();
            row856.LineNo = 2;
            row856.MaterialId = 9841;
            row856.SpecNo = "SA-182";
            row856.TypeGrade = "F304L";
            row856.ProductForm = "Forgings";
            row856.AlloyUNS = "S30403";
            row856.ClassCondition = "";
            row856.Notes = "";
            row856.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row856);

            // Row 3: SA-213, TP304L, Smls. tube
            var row857 = new OldStressRowData();
            row857.LineNo = 3;
            row857.MaterialId = 9103;
            row857.SpecNo = "SA-213";
            row857.TypeGrade = "TP304L";
            row857.ProductForm = "Smls. tube";
            row857.AlloyUNS = "S30403";
            row857.ClassCondition = "";
            row857.Notes = "G5";
            row857.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row857);

            // Row 4: SA-213, TP304L, Smls. tube
            var row858 = new OldStressRowData();
            row858.LineNo = 4;
            row858.MaterialId = 9104;
            row858.SpecNo = "SA-213";
            row858.TypeGrade = "TP304L";
            row858.ProductForm = "Smls. tube";
            row858.AlloyUNS = "S30403";
            row858.ClassCondition = "";
            row858.Notes = "";
            row858.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row858);

            // Row 5: SA-240, 304L, Plate
            var row859 = new OldStressRowData();
            row859.LineNo = 5;
            row859.MaterialId = 9105;
            row859.SpecNo = "SA-240";
            row859.TypeGrade = "304L";
            row859.ProductForm = "Plate";
            row859.AlloyUNS = "S30403";
            row859.ClassCondition = "";
            row859.Notes = "G5";
            row859.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row859);

            // Row 6: SA-240, 304L, Plate
            var row860 = new OldStressRowData();
            row860.LineNo = 6;
            row860.MaterialId = 9106;
            row860.SpecNo = "SA-240";
            row860.TypeGrade = "304L";
            row860.ProductForm = "Plate";
            row860.AlloyUNS = "S30403";
            row860.ClassCondition = "";
            row860.Notes = "";
            row860.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row860);

            // Row 7: SA-249, TP304L, Wld. tube
            var row861 = new OldStressRowData();
            row861.LineNo = 7;
            row861.MaterialId = 9107;
            row861.SpecNo = "SA-249";
            row861.TypeGrade = "TP304L";
            row861.ProductForm = "Wld. tube";
            row861.AlloyUNS = "S30403";
            row861.ClassCondition = "";
            row861.Notes = "G5, W12";
            row861.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row861);

            // Row 8: SA-249, TP304L, Wld. tube
            var row862 = new OldStressRowData();
            row862.LineNo = 8;
            row862.MaterialId = 9108;
            row862.SpecNo = "SA-249";
            row862.TypeGrade = "TP304L";
            row862.ProductForm = "Wld. tube";
            row862.AlloyUNS = "S30403";
            row862.ClassCondition = "";
            row862.Notes = "G5, G24";
            row862.StressValues = new double?[] { 14.2, null, 14, null, 13, 12.5, 12.3, 11.9, 11.7, 11.5, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row862);

            // Row 9: SA-249, TP304L, Wld. tube
            var row863 = new OldStressRowData();
            row863.LineNo = 9;
            row863.MaterialId = 9109;
            row863.SpecNo = "SA-249";
            row863.TypeGrade = "TP304L";
            row863.ProductForm = "Wld. tube";
            row863.AlloyUNS = "S30403";
            row863.ClassCondition = "";
            row863.Notes = "G24";
            row863.StressValues = new double?[] { 14.2, null, 12.2, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row863);

            // Row 10: SA-312, TP304L, Smls. pipe
            var row864 = new OldStressRowData();
            row864.LineNo = 10;
            row864.MaterialId = 9110;
            row864.SpecNo = "SA-312";
            row864.TypeGrade = "TP304L";
            row864.ProductForm = "Smls. pipe";
            row864.AlloyUNS = "S30403";
            row864.ClassCondition = "";
            row864.Notes = "G5";
            row864.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row864);

            // Row 11: SA-312, TP304L, Smls. pipe
            var row865 = new OldStressRowData();
            row865.LineNo = 11;
            row865.MaterialId = 9111;
            row865.SpecNo = "SA-312";
            row865.TypeGrade = "TP304L";
            row865.ProductForm = "Smls. pipe";
            row865.AlloyUNS = "S30403";
            row865.ClassCondition = "";
            row865.Notes = "";
            row865.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row865);

            // Row 12: SA-312, TP304L, Wld. pipe
            var row866 = new OldStressRowData();
            row866.LineNo = 12;
            row866.MaterialId = 9112;
            row866.SpecNo = "SA-312";
            row866.TypeGrade = "TP304L";
            row866.ProductForm = "Wld. pipe";
            row866.AlloyUNS = "S30403";
            row866.ClassCondition = "";
            row866.Notes = "G5, G24";
            row866.StressValues = new double?[] { 14.2, null, 14, null, 13, 12.5, 12.3, 11.9, 11.7, 11.5, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row866);

            // Row 13: SA-312, TP304L, Wld. pipe
            var row867 = new OldStressRowData();
            row867.LineNo = 13;
            row867.MaterialId = 9114;
            row867.SpecNo = "SA-312";
            row867.TypeGrade = "TP304L";
            row867.ProductForm = "Wld. pipe";
            row867.AlloyUNS = "S30403";
            row867.ClassCondition = "";
            row867.Notes = "G24";
            row867.StressValues = new double?[] { 14.2, null, 12.2, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row867);

            // Row 14: SA-312, TP304L, Wld. & smls. pipe
            var row868 = new OldStressRowData();
            row868.LineNo = 14;
            row868.MaterialId = 9113;
            row868.SpecNo = "SA-312";
            row868.TypeGrade = "TP304L";
            row868.ProductForm = "Wld. & smls. pipe";
            row868.AlloyUNS = "S30403";
            row868.ClassCondition = "";
            row868.Notes = "G5, W12";
            row868.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row868);

            // Row 15: SA-358, 304L, Wld. pipe
            var row869 = new OldStressRowData();
            row869.LineNo = 15;
            row869.MaterialId = 9115;
            row869.SpecNo = "SA-358";
            row869.TypeGrade = "304L";
            row869.ProductForm = "Wld. pipe";
            row869.AlloyUNS = "S30403";
            row869.ClassCondition = "1";
            row869.Notes = "G5";
            row869.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row869);

            // Row 16: ..., , 
            var row870 = new OldStressRowData();
            row870.LineNo = 16;
            row870.MaterialId = 9117;
            row870.SpecNo = "...";
            row870.TypeGrade = "";
            row870.ProductForm = "";
            row870.AlloyUNS = "";
            row870.ClassCondition = "";
            row870.Notes = "";
            row870.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row870);

            // Row 17: ..., , 
            var row871 = new OldStressRowData();
            row871.LineNo = 17;
            row871.MaterialId = 9116;
            row871.SpecNo = "...";
            row871.TypeGrade = "";
            row871.ProductForm = "";
            row871.AlloyUNS = "";
            row871.ClassCondition = "";
            row871.Notes = "";
            row871.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row871);

            // Row 18: SA-403, 304L, Smls. & wld. fittings
            var row872 = new OldStressRowData();
            row872.LineNo = 18;
            row872.MaterialId = 9121;
            row872.SpecNo = "SA-403";
            row872.TypeGrade = "304L";
            row872.ProductForm = "Smls. & wld. fittings";
            row872.AlloyUNS = "S30403";
            row872.ClassCondition = "";
            row872.Notes = "G5, W12, W15";
            row872.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row872);

            // Row 19: ..., , 
            var row873 = new OldStressRowData();
            row873.LineNo = 19;
            row873.MaterialId = 9118;
            row873.SpecNo = "...";
            row873.TypeGrade = "";
            row873.ProductForm = "";
            row873.AlloyUNS = "";
            row873.ClassCondition = "";
            row873.Notes = "";
            row873.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row873);

            // Row 20: ..., , 
            var row874 = new OldStressRowData();
            row874.LineNo = 20;
            row874.MaterialId = 9119;
            row874.SpecNo = "...";
            row874.TypeGrade = "";
            row874.ProductForm = "";
            row874.AlloyUNS = "";
            row874.ClassCondition = "";
            row874.Notes = "";
            row874.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row874);

            // Row 21: ..., , 
            var row875 = new OldStressRowData();
            row875.LineNo = 21;
            row875.MaterialId = 9120;
            row875.SpecNo = "...";
            row875.TypeGrade = "";
            row875.ProductForm = "";
            row875.AlloyUNS = "";
            row875.ClassCondition = "";
            row875.Notes = "";
            row875.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row875);

            // Row 22: SA-409, TP304L, Wld. pipe
            var row876 = new OldStressRowData();
            row876.LineNo = 22;
            row876.MaterialId = 9122;
            row876.SpecNo = "SA-409";
            row876.TypeGrade = "TP304L";
            row876.ProductForm = "Wld. pipe";
            row876.AlloyUNS = "S30403";
            row876.ClassCondition = "";
            row876.Notes = "G5, W12";
            row876.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row876);

            // Row 23: SA-479, 304L, Bar
            var row877 = new OldStressRowData();
            row877.LineNo = 23;
            row877.MaterialId = 9123;
            row877.SpecNo = "SA-479";
            row877.TypeGrade = "304L";
            row877.ProductForm = "Bar";
            row877.AlloyUNS = "S30403";
            row877.ClassCondition = "";
            row877.Notes = "G5, G22";
            row877.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row877);

            // Row 24: SA-479, 304L, Bar
            var row878 = new OldStressRowData();
            row878.LineNo = 24;
            row878.MaterialId = 9124;
            row878.SpecNo = "SA-479";
            row878.TypeGrade = "304L";
            row878.ProductForm = "Bar";
            row878.AlloyUNS = "S30403";
            row878.ClassCondition = "";
            row878.Notes = "G22";
            row878.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.3, 10.1, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row878);

            // Row 25: SA-688, TP304L, Wld. tube
            var row879 = new OldStressRowData();
            row879.LineNo = 25;
            row879.MaterialId = 9125;
            row879.SpecNo = "SA-688";
            row879.TypeGrade = "TP304L";
            row879.ProductForm = "Wld. tube";
            row879.AlloyUNS = "S30403";
            row879.ClassCondition = "";
            row879.Notes = "G5, W12";
            row879.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row879);

            // Row 26: SA-688, TP304L, Wld. tube
            var row880 = new OldStressRowData();
            row880.LineNo = 26;
            row880.MaterialId = 9126;
            row880.SpecNo = "SA-688";
            row880.TypeGrade = "TP304L";
            row880.ProductForm = "Wld. tube";
            row880.AlloyUNS = "S30403";
            row880.ClassCondition = "";
            row880.Notes = "G5, G24";
            row880.StressValues = new double?[] { 14.2, null, 14, null, 13, 12.5, 12.3, 11.9, 11.7, 11.5, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row880);

            // Row 27: SA-688, TP304L, Wld. tube
            var row881 = new OldStressRowData();
            row881.LineNo = 27;
            row881.MaterialId = 9127;
            row881.SpecNo = "SA-688";
            row881.TypeGrade = "TP304L";
            row881.ProductForm = "Wld. tube";
            row881.AlloyUNS = "S30403";
            row881.ClassCondition = "";
            row881.Notes = "G24";
            row881.StressValues = new double?[] { 14.2, null, 12.2, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row881);

            // Row 28: SA-813, TP304L, Wld. pipe
            var row882 = new OldStressRowData();
            row882.LineNo = 28;
            row882.MaterialId = 9128;
            row882.SpecNo = "SA-813";
            row882.TypeGrade = "TP304L";
            row882.ProductForm = "Wld. pipe";
            row882.AlloyUNS = "S30403";
            row882.ClassCondition = "";
            row882.Notes = "G5, W12";
            row882.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row882);

            // Row 29: SA-814, TP304L, Wld. pipe
            var row883 = new OldStressRowData();
            row883.LineNo = 29;
            row883.MaterialId = 9129;
            row883.SpecNo = "SA-814";
            row883.TypeGrade = "TP304L";
            row883.ProductForm = "Wld. pipe";
            row883.AlloyUNS = "S30403";
            row883.ClassCondition = "";
            row883.Notes = "G5, W12";
            row883.StressValues = new double?[] { 16.7, null, 16.5, null, 15.3, 14.7, 14.4, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row883);

            // Row 30: SA-182, F304, Forgings
            var row884 = new OldStressRowData();
            row884.LineNo = 30;
            row884.MaterialId = 9130;
            row884.SpecNo = "SA-182";
            row884.TypeGrade = "F304";
            row884.ProductForm = "Forgings";
            row884.AlloyUNS = "S30400";
            row884.ClassCondition = "";
            row884.Notes = "G5, G12, G18";
            row884.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row884);

            // Row 31: SA-182, F304, Forgings
            var row885 = new OldStressRowData();
            row885.LineNo = 31;
            row885.MaterialId = 9131;
            row885.SpecNo = "SA-182";
            row885.TypeGrade = "F304";
            row885.ProductForm = "Forgings";
            row885.AlloyUNS = "S30400";
            row885.ClassCondition = "";
            row885.Notes = "G12, G18";
            row885.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row885);

            // Row 32: SA-182, F304H, Forgings
            var row886 = new OldStressRowData();
            row886.LineNo = 32;
            row886.MaterialId = 9133;
            row886.SpecNo = "SA-182";
            row886.TypeGrade = "F304H";
            row886.ProductForm = "Forgings";
            row886.AlloyUNS = "S30409";
            row886.ClassCondition = "";
            row886.Notes = "G5, G18";
            row886.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row886);

            // Row 33: SA-182, F304H, Forgings
            var row887 = new OldStressRowData();
            row887.LineNo = 33;
            row887.MaterialId = 9132;
            row887.SpecNo = "SA-182";
            row887.TypeGrade = "F304H";
            row887.ProductForm = "Forgings";
            row887.AlloyUNS = "S30409";
            row887.ClassCondition = "";
            row887.Notes = "G18";
            row887.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row887);

            // Row 34: SA-336, F304, Forgings
            var row888 = new OldStressRowData();
            row888.LineNo = 34;
            row888.MaterialId = 9134;
            row888.SpecNo = "SA-336";
            row888.TypeGrade = "F304";
            row888.ProductForm = "Forgings";
            row888.AlloyUNS = "S30400";
            row888.ClassCondition = "";
            row888.Notes = "G5, G12, G18";
            row888.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row888);

            // Row 35: SA-336, F304, Forgings
            var row889 = new OldStressRowData();
            row889.LineNo = 35;
            row889.MaterialId = 9135;
            row889.SpecNo = "SA-336";
            row889.TypeGrade = "F304";
            row889.ProductForm = "Forgings";
            row889.AlloyUNS = "S30400";
            row889.ClassCondition = "";
            row889.Notes = "G12, G18";
            row889.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row889);

            // Row 36: SA-336, F304H, Forgings
            var row890 = new OldStressRowData();
            row890.LineNo = 36;
            row890.MaterialId = 9136;
            row890.SpecNo = "SA-336";
            row890.TypeGrade = "F304H";
            row890.ProductForm = "Forgings";
            row890.AlloyUNS = "S30409";
            row890.ClassCondition = "";
            row890.Notes = "G5";
            row890.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row890);

            // Row 37: SA-336, F304H, Forgings
            var row891 = new OldStressRowData();
            row891.LineNo = 37;
            row891.MaterialId = 9137;
            row891.SpecNo = "SA-336";
            row891.TypeGrade = "F304H";
            row891.ProductForm = "Forgings";
            row891.AlloyUNS = "S30409";
            row891.ClassCondition = "";
            row891.Notes = "";
            row891.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row891);

            // Row 1: SA-351, CF3, Castings
            var row892 = new OldStressRowData();
            row892.LineNo = 1;
            row892.MaterialId = 9138;
            row892.SpecNo = "SA-351";
            row892.TypeGrade = "CF3";
            row892.ProductForm = "Castings";
            row892.AlloyUNS = "J92500";
            row892.ClassCondition = "";
            row892.Notes = "G1, G5, G16, G17, G32";
            row892.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row892);

            // Row 2: SA-351, CF3, Castings
            var row893 = new OldStressRowData();
            row893.LineNo = 2;
            row893.MaterialId = 9139;
            row893.SpecNo = "SA-351";
            row893.TypeGrade = "CF3";
            row893.ProductForm = "Castings";
            row893.AlloyUNS = "J92500";
            row893.ClassCondition = "";
            row893.Notes = "G1, G32";
            row893.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row893);

            // Row 3: SA-351, CF8, Castings
            var row894 = new OldStressRowData();
            row894.LineNo = 3;
            row894.MaterialId = 9141;
            row894.SpecNo = "SA-351";
            row894.TypeGrade = "CF8";
            row894.ProductForm = "Castings";
            row894.AlloyUNS = "J92600";
            row894.ClassCondition = "";
            row894.Notes = "G1, G5, G12, G16, G17, G18, G32, H1";
            row894.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row894);

            // Row 4: SA-351, CF8, Castings
            var row895 = new OldStressRowData();
            row895.LineNo = 4;
            row895.MaterialId = 9142;
            row895.SpecNo = "SA-351";
            row895.TypeGrade = "CF8";
            row895.ProductForm = "Castings";
            row895.AlloyUNS = "J92600";
            row895.ClassCondition = "";
            row895.Notes = "G1, G12, G18, G32, H1";
            row895.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row895);

            // Row 5: SA-351, CF8, Castings
            var row896 = new OldStressRowData();
            row896.LineNo = 5;
            row896.MaterialId = 9140;
            row896.SpecNo = "SA-351";
            row896.TypeGrade = "CF8";
            row896.ProductForm = "Castings";
            row896.AlloyUNS = "J92600";
            row896.ClassCondition = "";
            row896.Notes = "G1, G5, G12, G32";
            row896.StressValues = new double?[] { 17.5, null, 16.5, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row896);

            // Row 6: SA-376, TP304, Smls. pipe
            var row897 = new OldStressRowData();
            row897.LineNo = 6;
            row897.MaterialId = 9144;
            row897.SpecNo = "SA-376";
            row897.TypeGrade = "TP304";
            row897.ProductForm = "Smls. pipe";
            row897.AlloyUNS = "S30400";
            row897.ClassCondition = "";
            row897.Notes = "G5, G12, S9";
            row897.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row897);

            // Row 7: SA-376, TP304, Smls. pipe
            var row898 = new OldStressRowData();
            row898.LineNo = 7;
            row898.MaterialId = 9143;
            row898.SpecNo = "SA-376";
            row898.TypeGrade = "TP304";
            row898.ProductForm = "Smls. pipe";
            row898.AlloyUNS = "S30400";
            row898.ClassCondition = "";
            row898.Notes = "G12, S9";
            row898.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row898);

            // Row 8: SA-430, FP304, Forged pipe
            var row899 = new OldStressRowData();
            row899.LineNo = 8;
            row899.MaterialId = 9145;
            row899.SpecNo = "SA-430";
            row899.TypeGrade = "FP304";
            row899.ProductForm = "Forged pipe";
            row899.AlloyUNS = "S30400";
            row899.ClassCondition = "";
            row899.Notes = "G5, G12, G18, H1";
            row899.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row899);

            // Row 9: SA-430, FP304, Forged pipe
            var row900 = new OldStressRowData();
            row900.LineNo = 9;
            row900.MaterialId = 9146;
            row900.SpecNo = "SA-430";
            row900.TypeGrade = "FP304";
            row900.ProductForm = "Forged pipe";
            row900.AlloyUNS = "S30400";
            row900.ClassCondition = "";
            row900.Notes = "G12, G18, H1";
            row900.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row900);

            return batch;
        }

        private class OldStressRowData
        {
            public int LineNo { get; set; }
            public int MaterialId { get; set; }
            public string SpecNo { get; set; } = "";
            public string TypeGrade { get; set; } = "";
            public string ProductForm { get; set; } = "";
            public string AlloyUNS { get; set; } = "";
            public string ClassCondition { get; set; } = "";
            public string Notes { get; set; } = "";
            public double?[] StressValues { get; set; } = new double?[29];
        }
    }
}
