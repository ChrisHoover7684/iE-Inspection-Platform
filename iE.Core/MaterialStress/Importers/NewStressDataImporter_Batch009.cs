using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch009
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
                    StressEra.From1999Onward,
                    3.5,
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

            // Row 4: SA-451, CPF8, Cast pipe
            var row801 = new OldStressRowData();
            row801.LineNo = 4;
            row801.MaterialId = 9150;
            row801.SpecNo = "SA-451";
            row801.TypeGrade = "CPF8";
            row801.ProductForm = "Cast pipe";
            row801.AlloyUNS = "J92600";
            row801.ClassCondition = "";
            row801.Notes = "G5, G16, G17, G32";
            row801.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row801);

            // Row 5: SA-182, F304, Forgings
            var row802 = new OldStressRowData();
            row802.LineNo = 5;
            row802.MaterialId = 9152;
            row802.SpecNo = "SA-182";
            row802.TypeGrade = "F304";
            row802.ProductForm = "Forgings";
            row802.AlloyUNS = "S30400";
            row802.ClassCondition = "";
            row802.Notes = "G12, G18, T8";
            row802.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row802);

            // Row 6: SA-182, F304, Forgings
            var row803 = new OldStressRowData();
            row803.LineNo = 6;
            row803.MaterialId = 9151;
            row803.SpecNo = "SA-182";
            row803.TypeGrade = "F304";
            row803.ProductForm = "Forgings";
            row803.AlloyUNS = "S30400";
            row803.ClassCondition = "";
            row803.Notes = "G5, G12, G18, T7";
            row803.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row803);

            // Row 7: SA-182, F304H, Forgings
            var row804 = new OldStressRowData();
            row804.LineNo = 7;
            row804.MaterialId = 9154;
            row804.SpecNo = "SA-182";
            row804.TypeGrade = "F304H";
            row804.ProductForm = "Forgings";
            row804.AlloyUNS = "S30409";
            row804.ClassCondition = "";
            row804.Notes = "G18, T8";
            row804.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row804);

            // Row 8: SA-182, F304H, Forgings
            var row805 = new OldStressRowData();
            row805.LineNo = 8;
            row805.MaterialId = 9153;
            row805.SpecNo = "SA-182";
            row805.TypeGrade = "F304H";
            row805.ProductForm = "Forgings";
            row805.AlloyUNS = "S30409";
            row805.ClassCondition = "";
            row805.Notes = "G5, G18, T7";
            row805.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row805);

            // Row 9: SA-213, TP304, Smls. tube
            var row806 = new OldStressRowData();
            row806.LineNo = 9;
            row806.MaterialId = 9156;
            row806.SpecNo = "SA-213";
            row806.TypeGrade = "TP304";
            row806.ProductForm = "Smls. tube";
            row806.AlloyUNS = "S30400";
            row806.ClassCondition = "";
            row806.Notes = "G12, G18, T8";
            row806.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row806);

            // Row 10: SA-213, TP304, Smls. tube
            var row807 = new OldStressRowData();
            row807.LineNo = 10;
            row807.MaterialId = 9155;
            row807.SpecNo = "SA-213";
            row807.TypeGrade = "TP304";
            row807.ProductForm = "Smls. tube";
            row807.AlloyUNS = "S30400";
            row807.ClassCondition = "";
            row807.Notes = "G5, G12, G18, T7";
            row807.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row807);

            // Row 11: SA-213, TP304H, Smls. tube
            var row808 = new OldStressRowData();
            row808.LineNo = 11;
            row808.MaterialId = 9158;
            row808.SpecNo = "SA-213";
            row808.TypeGrade = "TP304H";
            row808.ProductForm = "Smls. tube";
            row808.AlloyUNS = "S30409";
            row808.ClassCondition = "";
            row808.Notes = "G18, T8";
            row808.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row808);

            // Row 12: SA-213, TP304H, Smls. tube
            var row809 = new OldStressRowData();
            row809.LineNo = 12;
            row809.MaterialId = 9157;
            row809.SpecNo = "SA-213";
            row809.TypeGrade = "TP304H";
            row809.ProductForm = "Smls. tube";
            row809.AlloyUNS = "S30409";
            row809.ClassCondition = "";
            row809.Notes = "G5, G18, T7";
            row809.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row809);

            // Row 13: SA-240, 302, Plate
            var row810 = new OldStressRowData();
            row810.LineNo = 13;
            row810.MaterialId = 9159;
            row810.SpecNo = "SA-240";
            row810.TypeGrade = "302";
            row810.ProductForm = "Plate";
            row810.AlloyUNS = "S30200";
            row810.ClassCondition = "";
            row810.Notes = "G5";
            row810.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row810);

            // Row 14: SA-240, 302, Plate
            var row811 = new OldStressRowData();
            row811.LineNo = 14;
            row811.MaterialId = 9160;
            row811.SpecNo = "SA-240";
            row811.TypeGrade = "302";
            row811.ProductForm = "Plate";
            row811.AlloyUNS = "S30200";
            row811.ClassCondition = "";
            row811.Notes = "";
            row811.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row811);

            // Row 15: SA-240, 304, Plate
            var row812 = new OldStressRowData();
            row812.LineNo = 15;
            row812.MaterialId = 9162;
            row812.SpecNo = "SA-240";
            row812.TypeGrade = "304";
            row812.ProductForm = "Plate";
            row812.AlloyUNS = "S30400";
            row812.ClassCondition = "";
            row812.Notes = "G12, G18, T8";
            row812.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row812);

            // Row 16: SA-240, 304, Plate
            var row813 = new OldStressRowData();
            row813.LineNo = 16;
            row813.MaterialId = 9161;
            row813.SpecNo = "SA-240";
            row813.TypeGrade = "304";
            row813.ProductForm = "Plate";
            row813.AlloyUNS = "S30400";
            row813.ClassCondition = "";
            row813.Notes = "G5, G12, G18, H1, T7";
            row813.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row813);

            // Row 17: SA-240, 304H, Plate
            var row814 = new OldStressRowData();
            row814.LineNo = 17;
            row814.MaterialId = 9163;
            row814.SpecNo = "SA-240";
            row814.TypeGrade = "304H";
            row814.ProductForm = "Plate";
            row814.AlloyUNS = "S30409";
            row814.ClassCondition = "";
            row814.Notes = "G5, G18, T7";
            row814.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row814);

            // Row 18: SA-240, 304H, Plate
            var row815 = new OldStressRowData();
            row815.LineNo = 18;
            row815.MaterialId = 9164;
            row815.SpecNo = "SA-240";
            row815.TypeGrade = "304H";
            row815.ProductForm = "Plate";
            row815.AlloyUNS = "S30409";
            row815.ClassCondition = "";
            row815.Notes = "G18, T8";
            row815.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row815);

            // Row 19: SA-249, TP304, Wld. tube
            var row816 = new OldStressRowData();
            row816.LineNo = 19;
            row816.MaterialId = 9169;
            row816.SpecNo = "SA-249";
            row816.TypeGrade = "TP304";
            row816.ProductForm = "Wld. tube";
            row816.AlloyUNS = "S30400";
            row816.ClassCondition = "";
            row816.Notes = "G12, G18, T8, W13";
            row816.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row816);

            // Row 20: SA-249, TP304, Wld. tube
            var row817 = new OldStressRowData();
            row817.LineNo = 20;
            row817.MaterialId = 9165;
            row817.SpecNo = "SA-249";
            row817.TypeGrade = "TP304";
            row817.ProductForm = "Wld. tube";
            row817.AlloyUNS = "S30400";
            row817.ClassCondition = "";
            row817.Notes = "G5, G12, G18, T7, W12, W13";
            row817.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row817);

            // Row 21: SA-249, TP304, Wld. tube
            var row818 = new OldStressRowData();
            row818.LineNo = 21;
            row818.MaterialId = 9166;
            row818.SpecNo = "SA-249";
            row818.TypeGrade = "TP304";
            row818.ProductForm = "Wld. tube";
            row818.AlloyUNS = "S30400";
            row818.ClassCondition = "";
            row818.Notes = "G3, G5, G12, G18, G24, T7";
            row818.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, 10.5, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row818);

            // Row 22: SA-249, TP304, Wld. tube
            var row819 = new OldStressRowData();
            row819.LineNo = 22;
            row819.MaterialId = 9168;
            row819.SpecNo = "SA-249";
            row819.TypeGrade = "TP304";
            row819.ProductForm = "Wld. tube";
            row819.AlloyUNS = "S30400";
            row819.ClassCondition = "";
            row819.Notes = "G3, G12, G18, G24, T8";
            row819.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row819);

            // Row 23: SA-249, TP304H, Wld. tube
            var row820 = new OldStressRowData();
            row820.LineNo = 23;
            row820.MaterialId = 9174;
            row820.SpecNo = "SA-249";
            row820.TypeGrade = "TP304H";
            row820.ProductForm = "Wld. tube";
            row820.AlloyUNS = "S30409";
            row820.ClassCondition = "";
            row820.Notes = "G18, T8, W13";
            row820.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row820);

            // Row 24: SA-249, TP304H, Wld. tube
            var row821 = new OldStressRowData();
            row821.LineNo = 24;
            row821.MaterialId = 9176;
            row821.SpecNo = "SA-249";
            row821.TypeGrade = "TP304H";
            row821.ProductForm = "Wld. tube";
            row821.AlloyUNS = "S30409";
            row821.ClassCondition = "";
            row821.Notes = "G5, G18, T7, W12, W13";
            row821.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row821);

            // Row 25: SA-249, TP304H, Wld. tube
            var row822 = new OldStressRowData();
            row822.LineNo = 25;
            row822.MaterialId = 9171;
            row822.SpecNo = "SA-249";
            row822.TypeGrade = "TP304H";
            row822.ProductForm = "Wld. tube";
            row822.AlloyUNS = "S30409";
            row822.ClassCondition = "";
            row822.Notes = "G3, G5, G18, G24, T7";
            row822.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, 10.5, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row822);

            // Row 26: SA-249, TP304H, Wld. tube
            var row823 = new OldStressRowData();
            row823.LineNo = 26;
            row823.MaterialId = 9173;
            row823.SpecNo = "SA-249";
            row823.TypeGrade = "TP304H";
            row823.ProductForm = "Wld. tube";
            row823.AlloyUNS = "S30409";
            row823.ClassCondition = "";
            row823.Notes = "G3, G18, G24, T8";
            row823.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row823);

            // Row 27: SA-312, TP304, Smls. & wld. pipe
            var row824 = new OldStressRowData();
            row824.LineNo = 27;
            row824.MaterialId = 9180;
            row824.SpecNo = "SA-312";
            row824.TypeGrade = "TP304";
            row824.ProductForm = "Smls. & wld. pipe";
            row824.AlloyUNS = "S30400";
            row824.ClassCondition = "";
            row824.Notes = "G5, G12, G18, T7, W12, W13";
            row824.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row824);

            // Row 28: SA-312, TP304, Smls. & wld. pipe
            var row825 = new OldStressRowData();
            row825.LineNo = 28;
            row825.MaterialId = 9184;
            row825.SpecNo = "SA-312";
            row825.TypeGrade = "TP304";
            row825.ProductForm = "Smls. & wld. pipe";
            row825.AlloyUNS = "S30400";
            row825.ClassCondition = "";
            row825.Notes = "G12, G18, T8, W13";
            row825.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row825);

            // Row 29: SA-312, TP304, Wld. pipe
            var row826 = new OldStressRowData();
            row826.LineNo = 29;
            row826.MaterialId = 9181;
            row826.SpecNo = "SA-312";
            row826.TypeGrade = "TP304";
            row826.ProductForm = "Wld. pipe";
            row826.AlloyUNS = "S30400";
            row826.ClassCondition = "";
            row826.Notes = "G3, G5, G12, G18, G24, T7";
            row826.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, 10.5, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row826);

            // Row 30: SA-312, TP304, Wld. pipe
            var row827 = new OldStressRowData();
            row827.LineNo = 30;
            row827.MaterialId = 9185;
            row827.SpecNo = "SA-312";
            row827.TypeGrade = "TP304";
            row827.ProductForm = "Wld. pipe";
            row827.AlloyUNS = "S30400";
            row827.ClassCondition = "";
            row827.Notes = "G3, G12, G18, G24, T8";
            row827.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row827);

            // Row 31: SA-312, TP304H, Smls. & wld. pipe
            var row828 = new OldStressRowData();
            row828.LineNo = 31;
            row828.MaterialId = 9189;
            row828.SpecNo = "SA-312";
            row828.TypeGrade = "TP304H";
            row828.ProductForm = "Smls. & wld. pipe";
            row828.AlloyUNS = "S30409";
            row828.ClassCondition = "";
            row828.Notes = "G5, G18, T7, W12, W13";
            row828.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row828);

            // Row 32: SA-312, TP304H, Smls. & wld. pipe
            var row829 = new OldStressRowData();
            row829.LineNo = 32;
            row829.MaterialId = 9192;
            row829.SpecNo = "SA-312";
            row829.TypeGrade = "TP304H";
            row829.ProductForm = "Smls. & wld. pipe";
            row829.AlloyUNS = "S30409";
            row829.ClassCondition = "";
            row829.Notes = "G18, T8, W13";
            row829.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row829);

            // Row 33: SA-312, TP304H, Wld. pipe
            var row830 = new OldStressRowData();
            row830.LineNo = 33;
            row830.MaterialId = 9190;
            row830.SpecNo = "SA-312";
            row830.TypeGrade = "TP304H";
            row830.ProductForm = "Wld. pipe";
            row830.AlloyUNS = "S30409";
            row830.ClassCondition = "";
            row830.Notes = "G3, G5, G18, G24, T7";
            row830.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, 10.5, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row830);

            // Row 34: SA-312, TP304H, Wld. pipe
            var row831 = new OldStressRowData();
            row831.LineNo = 34;
            row831.MaterialId = 9193;
            row831.SpecNo = "SA-312";
            row831.TypeGrade = "TP304H";
            row831.ProductForm = "Wld. pipe";
            row831.AlloyUNS = "S30409";
            row831.ClassCondition = "";
            row831.Notes = "G3, G18, G24, T8";
            row831.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row831);

            // Row 35: SA-358, 304, Wld. pipe
            var row832 = new OldStressRowData();
            row832.LineNo = 35;
            row832.MaterialId = 9195;
            row832.SpecNo = "SA-358";
            row832.TypeGrade = "304";
            row832.ProductForm = "Wld. pipe";
            row832.AlloyUNS = "S30400";
            row832.ClassCondition = "1";
            row832.Notes = "G5, W12";
            row832.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row832);

            // Row 36: SA-358, 304H, Wld. pipe
            var row833 = new OldStressRowData();
            row833.LineNo = 36;
            row833.MaterialId = 9196;
            row833.SpecNo = "SA-358";
            row833.TypeGrade = "304H";
            row833.ProductForm = "Wld. pipe";
            row833.AlloyUNS = "S30409";
            row833.ClassCondition = "1";
            row833.Notes = "G5, W12";
            row833.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row833);

            // Row 37: SA-358, 304LN, Wld. pipe
            var row834 = new OldStressRowData();
            row834.LineNo = 37;
            row834.MaterialId = 9197;
            row834.SpecNo = "SA-358";
            row834.TypeGrade = "304LN";
            row834.ProductForm = "Wld. pipe";
            row834.AlloyUNS = "S30453";
            row834.ClassCondition = "1";
            row834.Notes = "G5, W12";
            row834.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row834);

            // Row 1: SA-376, TP304, Smls. pipe
            var row835 = new OldStressRowData();
            row835.LineNo = 1;
            row835.MaterialId = 9198;
            row835.SpecNo = "SA-376";
            row835.TypeGrade = "TP304";
            row835.ProductForm = "Smls. pipe";
            row835.AlloyUNS = "S30400";
            row835.ClassCondition = "";
            row835.Notes = "G5, G12, G18, H1, T7";
            row835.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row835);

            // Row 2: SA-376, TP304, Smls. pipe
            var row836 = new OldStressRowData();
            row836.LineNo = 2;
            row836.MaterialId = 9199;
            row836.SpecNo = "SA-376";
            row836.TypeGrade = "TP304";
            row836.ProductForm = "Smls. pipe";
            row836.AlloyUNS = "S30400";
            row836.ClassCondition = "";
            row836.Notes = "G12, G18, H1, T8";
            row836.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row836);

            // Row 3: SA-376, TP304H, Smls. pipe
            var row837 = new OldStressRowData();
            row837.LineNo = 3;
            row837.MaterialId = 9200;
            row837.SpecNo = "SA-376";
            row837.TypeGrade = "TP304H";
            row837.ProductForm = "Smls. pipe";
            row837.AlloyUNS = "S30409";
            row837.ClassCondition = "";
            row837.Notes = "G5, G18, H1, T7";
            row837.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row837);

            // Row 4: SA-376, TP304H, Smls. pipe
            var row838 = new OldStressRowData();
            row838.LineNo = 4;
            row838.MaterialId = 9201;
            row838.SpecNo = "SA-376";
            row838.TypeGrade = "TP304H";
            row838.ProductForm = "Smls. pipe";
            row838.AlloyUNS = "S30409";
            row838.ClassCondition = "";
            row838.Notes = "G18, H1, T8";
            row838.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row838);

            // Row 5: SA-403, 304, Smls. & wld. fittings
            var row839 = new OldStressRowData();
            row839.LineNo = 5;
            row839.MaterialId = 9207;
            row839.SpecNo = "SA-403";
            row839.TypeGrade = "304";
            row839.ProductForm = "Smls. & wld. fittings";
            row839.AlloyUNS = "S30400";
            row839.ClassCondition = "";
            row839.Notes = "G5, G12, T7, W12, W14";
            row839.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row839);

            // Row 6: SA-403, 304H, Smls. & wld. fittings
            var row840 = new OldStressRowData();
            row840.LineNo = 6;
            row840.MaterialId = 9213;
            row840.SpecNo = "SA-403";
            row840.TypeGrade = "304H";
            row840.ProductForm = "Smls. & wld. fittings";
            row840.AlloyUNS = "S30409";
            row840.ClassCondition = "";
            row840.Notes = "G5, T7, W12, W14";
            row840.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row840);

            // Row 7: SA-409, TP304, Wld. pipe
            var row841 = new OldStressRowData();
            row841.LineNo = 7;
            row841.MaterialId = 9214;
            row841.SpecNo = "SA-409";
            row841.TypeGrade = "TP304";
            row841.ProductForm = "Wld. pipe";
            row841.AlloyUNS = "S30400";
            row841.ClassCondition = "";
            row841.Notes = "G5, W12";
            row841.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row841);

            // Row 8: SA-452, TP304H, Cast pipe
            var row842 = new OldStressRowData();
            row842.LineNo = 8;
            row842.MaterialId = 9216;
            row842.SpecNo = "SA-452";
            row842.TypeGrade = "TP304H";
            row842.ProductForm = "Cast pipe";
            row842.AlloyUNS = "S30409";
            row842.ClassCondition = "";
            row842.Notes = "G5, G16, G17, G32, T7";
            row842.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row842);

            // Row 9: SA-452, TP304H, Cast pipe
            var row843 = new OldStressRowData();
            row843.LineNo = 9;
            row843.MaterialId = 9217;
            row843.SpecNo = "SA-452";
            row843.TypeGrade = "TP304H";
            row843.ProductForm = "Cast pipe";
            row843.AlloyUNS = "S30409";
            row843.ClassCondition = "";
            row843.Notes = "G32, T8";
            row843.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row843);

            // Row 10: SA-479, 302, Bar
            var row844 = new OldStressRowData();
            row844.LineNo = 10;
            row844.MaterialId = 9218;
            row844.SpecNo = "SA-479";
            row844.TypeGrade = "302";
            row844.ProductForm = "Bar";
            row844.AlloyUNS = "S30200";
            row844.ClassCondition = "";
            row844.Notes = "G5, G24";
            row844.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row844);

            // Row 11: SA-479, 302, Bar
            var row845 = new OldStressRowData();
            row845.LineNo = 11;
            row845.MaterialId = 9219;
            row845.SpecNo = "SA-479";
            row845.TypeGrade = "302";
            row845.ProductForm = "Bar";
            row845.AlloyUNS = "S30200";
            row845.ClassCondition = "";
            row845.Notes = "G22";
            row845.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row845);

            // Row 12: SA-479, 304, Bar
            var row846 = new OldStressRowData();
            row846.LineNo = 12;
            row846.MaterialId = 9220;
            row846.SpecNo = "SA-479";
            row846.TypeGrade = "304";
            row846.ProductForm = "Bar";
            row846.AlloyUNS = "S30400";
            row846.ClassCondition = "";
            row846.Notes = "G5, G12, G18, G22, T7";
            row846.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row846);

            // Row 13: SA-479, 304, Bar
            var row847 = new OldStressRowData();
            row847.LineNo = 13;
            row847.MaterialId = 9221;
            row847.SpecNo = "SA-479";
            row847.TypeGrade = "304";
            row847.ProductForm = "Bar";
            row847.AlloyUNS = "S30400";
            row847.ClassCondition = "";
            row847.Notes = "G12, G18, G22, T8";
            row847.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row847);

            // Row 14: SA-479, 304H, Bar
            var row848 = new OldStressRowData();
            row848.LineNo = 14;
            row848.MaterialId = 9222;
            row848.SpecNo = "SA-479";
            row848.TypeGrade = "304H";
            row848.ProductForm = "Bar";
            row848.AlloyUNS = "S30409";
            row848.ClassCondition = "";
            row848.Notes = "G5, G18, G22, T7";
            row848.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row848);

            // Row 15: SA-479, 304H, Bar
            var row849 = new OldStressRowData();
            row849.LineNo = 15;
            row849.MaterialId = 9223;
            row849.SpecNo = "SA-479";
            row849.TypeGrade = "304H";
            row849.ProductForm = "Bar";
            row849.AlloyUNS = "S30409";
            row849.ClassCondition = "";
            row849.Notes = "G18, G22, T8";
            row849.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row849);

            // Row 16: SA-688, TP304, Wld. tube
            var row850 = new OldStressRowData();
            row850.LineNo = 16;
            row850.MaterialId = 9224;
            row850.SpecNo = "SA-688";
            row850.TypeGrade = "TP304";
            row850.ProductForm = "Wld. tube";
            row850.AlloyUNS = "S30400";
            row850.ClassCondition = "";
            row850.Notes = "G5, W12";
            row850.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row850);

            // Row 17: SA-688, TP304, Wld. tube
            var row851 = new OldStressRowData();
            row851.LineNo = 17;
            row851.MaterialId = 9225;
            row851.SpecNo = "SA-688";
            row851.TypeGrade = "TP304";
            row851.ProductForm = "Wld. tube";
            row851.AlloyUNS = "S30400";
            row851.ClassCondition = "";
            row851.Notes = "G5, G12, G24, T7";
            row851.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, 10.5, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row851);

            // Row 18: SA-688, TP304, Wld. tube
            var row852 = new OldStressRowData();
            row852.LineNo = 18;
            row852.MaterialId = 9226;
            row852.SpecNo = "SA-688";
            row852.TypeGrade = "TP304";
            row852.ProductForm = "Wld. tube";
            row852.AlloyUNS = "S30400";
            row852.ClassCondition = "";
            row852.Notes = "G12, G24, T8";
            row852.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.1, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row852);

            // Row 19: SA-813, TP304, Wld. pipe
            var row853 = new OldStressRowData();
            row853.LineNo = 19;
            row853.MaterialId = 9227;
            row853.SpecNo = "SA-813";
            row853.TypeGrade = "TP304";
            row853.ProductForm = "Wld. pipe";
            row853.AlloyUNS = "S30400";
            row853.ClassCondition = "";
            row853.Notes = "G5, W12";
            row853.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row853);

            // Row 20: SA-813, TP304H, Wld. pipe
            var row854 = new OldStressRowData();
            row854.LineNo = 20;
            row854.MaterialId = 9228;
            row854.SpecNo = "SA-813";
            row854.TypeGrade = "TP304H";
            row854.ProductForm = "Wld. pipe";
            row854.AlloyUNS = "S30409";
            row854.ClassCondition = "";
            row854.Notes = "G5, W12";
            row854.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row854);

            // Row 21: SA-814, TP304, Wld. pipe
            var row855 = new OldStressRowData();
            row855.LineNo = 21;
            row855.MaterialId = 9229;
            row855.SpecNo = "SA-814";
            row855.TypeGrade = "TP304";
            row855.ProductForm = "Wld. pipe";
            row855.AlloyUNS = "S30400";
            row855.ClassCondition = "";
            row855.Notes = "G5, W12";
            row855.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row855);

            // Row 22: SA-814, TP304H, Wld. pipe
            var row856 = new OldStressRowData();
            row856.LineNo = 22;
            row856.MaterialId = 9230;
            row856.SpecNo = "SA-814";
            row856.TypeGrade = "TP304H";
            row856.ProductForm = "Wld. pipe";
            row856.AlloyUNS = "S30409";
            row856.ClassCondition = "";
            row856.Notes = "G5, W12";
            row856.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row856);

            // Row 23: SA-351, CF3A, Castings
            var row857 = new OldStressRowData();
            row857.LineNo = 23;
            row857.MaterialId = 9231;
            row857.SpecNo = "SA-351";
            row857.TypeGrade = "CF3A";
            row857.ProductForm = "Castings";
            row857.AlloyUNS = "J92500";
            row857.ClassCondition = "";
            row857.Notes = "G1, G5, G16, G17, G32";
            row857.StressValues = new double?[] { 22, null, 20.8, null, 19.4, 18.8, 18.6, 18.6, 18.6, 18.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row857);

            // Row 24: SA-351, CF3A, Castings
            var row858 = new OldStressRowData();
            row858.LineNo = 24;
            row858.MaterialId = 9236;
            row858.SpecNo = "SA-351";
            row858.TypeGrade = "CF3A";
            row858.ProductForm = "Castings";
            row858.AlloyUNS = "J92500";
            row858.ClassCondition = "";
            row858.Notes = "G1, G32";
            row858.StressValues = new double?[] { 22, null, 19.5, null, 17.5, 16.1, 15.1, 14.3, 14, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row858);

            // Row 25: SA-351, CF8A, Castings
            var row859 = new OldStressRowData();
            row859.LineNo = 25;
            row859.MaterialId = 9232;
            row859.SpecNo = "SA-351";
            row859.TypeGrade = "CF8A";
            row859.ProductForm = "Castings";
            row859.AlloyUNS = "J92600";
            row859.ClassCondition = "";
            row859.Notes = "G1, G5, G16, G17, G32";
            row859.StressValues = new double?[] { 22, null, 20.8, null, 19.4, 18.8, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row859);

            // Row 26: SA-351, CF8A, Castings
            var row860 = new OldStressRowData();
            row860.LineNo = 26;
            row860.MaterialId = 9233;
            row860.SpecNo = "SA-351";
            row860.TypeGrade = "CF8A";
            row860.ProductForm = "Castings";
            row860.AlloyUNS = "J92600";
            row860.ClassCondition = "";
            row860.Notes = "G1, G32";
            row860.StressValues = new double?[] { 22, null, 19.5, null, 17.5, 16.1, 15.1, 14.3, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row860);

            // Row 27: SA-451, CPF3A, Cast pipe
            var row861 = new OldStressRowData();
            row861.LineNo = 27;
            row861.MaterialId = 9234;
            row861.SpecNo = "SA-451";
            row861.TypeGrade = "CPF3A";
            row861.ProductForm = "Cast pipe";
            row861.AlloyUNS = "J92500";
            row861.ClassCondition = "";
            row861.Notes = "G5, G16, G17, G32";
            row861.StressValues = new double?[] { 22, null, 20.8, null, 19.4, 18.8, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row861);

            // Row 28: SA-451, CPF8A, Cast pipe
            var row862 = new OldStressRowData();
            row862.LineNo = 28;
            row862.MaterialId = 9235;
            row862.SpecNo = "SA-451";
            row862.TypeGrade = "CPF8A";
            row862.ProductForm = "Cast pipe";
            row862.AlloyUNS = "J92600";
            row862.ClassCondition = "";
            row862.Notes = "G5, G16, G17, G32";
            row862.StressValues = new double?[] { 22, null, 20.8, null, 19.4, 18.8, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row862);

            // Row 29: SA-182, F304LN, Forgings
            var row863 = new OldStressRowData();
            row863.LineNo = 29;
            row863.MaterialId = 9237;
            row863.SpecNo = "SA-182";
            row863.TypeGrade = "F304LN";
            row863.ProductForm = "Forgings";
            row863.AlloyUNS = "S30453";
            row863.ClassCondition = "";
            row863.Notes = "G5";
            row863.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row863);

            // Row 30: SA-336, F304LN, Forgings
            var row864 = new OldStressRowData();
            row864.LineNo = 30;
            row864.MaterialId = 9238;
            row864.SpecNo = "SA-336";
            row864.TypeGrade = "F304LN";
            row864.ProductForm = "Forgings";
            row864.AlloyUNS = "S30453";
            row864.ClassCondition = "";
            row864.Notes = "G5";
            row864.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row864);

            // Row 31: SA-182, F304LN, Forgings
            var row865 = new OldStressRowData();
            row865.LineNo = 31;
            row865.MaterialId = 9239;
            row865.SpecNo = "SA-182";
            row865.TypeGrade = "F304LN";
            row865.ProductForm = "Forgings";
            row865.AlloyUNS = "S30453";
            row865.ClassCondition = "";
            row865.Notes = "G5";
            row865.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row865);

            // Row 32: SA-213, TP304LN, Smls. tube
            var row866 = new OldStressRowData();
            row866.LineNo = 32;
            row866.MaterialId = 9240;
            row866.SpecNo = "SA-213";
            row866.TypeGrade = "TP304LN";
            row866.ProductForm = "Smls. tube";
            row866.AlloyUNS = "S30453";
            row866.ClassCondition = "";
            row866.Notes = "G5";
            row866.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row866);

            // Row 33: SA-240, 304LN, Plate
            var row867 = new OldStressRowData();
            row867.LineNo = 33;
            row867.MaterialId = 9241;
            row867.SpecNo = "SA-240";
            row867.TypeGrade = "304LN";
            row867.ProductForm = "Plate";
            row867.AlloyUNS = "S30453";
            row867.ClassCondition = "";
            row867.Notes = "G5";
            row867.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row867);

            // Row 34: SA-249, TP304LN, Wld. tube
            var row868 = new OldStressRowData();
            row868.LineNo = 34;
            row868.MaterialId = 9242;
            row868.SpecNo = "SA-249";
            row868.TypeGrade = "TP304LN";
            row868.ProductForm = "Wld. tube";
            row868.AlloyUNS = "S30453";
            row868.ClassCondition = "";
            row868.Notes = "G5, W12";
            row868.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row868);

            // Row 35: SA-312, TP304LN, Smls. & wld. pipe
            var row869 = new OldStressRowData();
            row869.LineNo = 35;
            row869.MaterialId = 9243;
            row869.SpecNo = "SA-312";
            row869.TypeGrade = "TP304LN";
            row869.ProductForm = "Smls. & wld. pipe";
            row869.AlloyUNS = "S30453";
            row869.ClassCondition = "";
            row869.Notes = "G5, W12";
            row869.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row869);

            // Row 36: SA-376, TP304LN, Smls. pipe
            var row870 = new OldStressRowData();
            row870.LineNo = 36;
            row870.MaterialId = 9244;
            row870.SpecNo = "SA-376";
            row870.TypeGrade = "TP304LN";
            row870.ProductForm = "Smls. pipe";
            row870.AlloyUNS = "S30453";
            row870.ClassCondition = "";
            row870.Notes = "G5";
            row870.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row870);

            // Row 37: SA-403, 304LN, Smls. & wld. fittings
            var row871 = new OldStressRowData();
            row871.LineNo = 37;
            row871.MaterialId = 9245;
            row871.SpecNo = "SA-403";
            row871.TypeGrade = "304LN";
            row871.ProductForm = "Smls. & wld. fittings";
            row871.AlloyUNS = "S30453";
            row871.ClassCondition = "WP";
            row871.Notes = "G5, W12";
            row871.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row871);

            // Row 38: SA-479, 304LN, Bar
            var row872 = new OldStressRowData();
            row872.LineNo = 38;
            row872.MaterialId = 9247;
            row872.SpecNo = "SA-479";
            row872.TypeGrade = "304LN";
            row872.ProductForm = "Bar";
            row872.AlloyUNS = "S30453";
            row872.ClassCondition = "";
            row872.Notes = "G5";
            row872.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row872);

            // Row 39: SA-688, TP304LN, Wld. tube
            var row873 = new OldStressRowData();
            row873.LineNo = 39;
            row873.MaterialId = 9248;
            row873.SpecNo = "SA-688";
            row873.TypeGrade = "TP304LN";
            row873.ProductForm = "Wld. tube";
            row873.AlloyUNS = "S30453";
            row873.ClassCondition = "";
            row873.Notes = "G5, W12";
            row873.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row873);

            // Row 1: SA-813, TP304LN, Wld. pipe
            var row874 = new OldStressRowData();
            row874.LineNo = 1;
            row874.MaterialId = 9249;
            row874.SpecNo = "SA-813";
            row874.TypeGrade = "TP304LN";
            row874.ProductForm = "Wld. pipe";
            row874.AlloyUNS = "S30453";
            row874.ClassCondition = "";
            row874.Notes = "G5, W12";
            row874.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row874);

            // Row 2: SA-814, TP304LN, Wld. pipe
            var row875 = new OldStressRowData();
            row875.LineNo = 2;
            row875.MaterialId = 9250;
            row875.SpecNo = "SA-814";
            row875.TypeGrade = "TP304LN";
            row875.ProductForm = "Wld. pipe";
            row875.AlloyUNS = "S30453";
            row875.ClassCondition = "";
            row875.Notes = "G5, W12";
            row875.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row875);

            // Row 3: SA-430, FP304N, Forged pipe
            var row876 = new OldStressRowData();
            row876.LineNo = 3;
            row876.MaterialId = 9251;
            row876.SpecNo = "SA-430";
            row876.TypeGrade = "FP304N";
            row876.ProductForm = "Forged pipe";
            row876.AlloyUNS = "S30451";
            row876.ClassCondition = "";
            row876.Notes = "G5, G12, G18, H1, T7";
            row876.StressValues = new double?[] { 21.4, null, 21.4, null, 20.4, 19.6, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row876);

            // Row 4: SA-430, FP304N, Forged pipe
            var row877 = new OldStressRowData();
            row877.LineNo = 4;
            row877.MaterialId = 9252;
            row877.SpecNo = "SA-430";
            row877.TypeGrade = "FP304N";
            row877.ProductForm = "Forged pipe";
            row877.AlloyUNS = "S30451";
            row877.ClassCondition = "";
            row877.Notes = "G12, G18, H1, T8";
            row877.StressValues = new double?[] { 21.4, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row877);

            // Row 5: SA-182, F304N, Forgings
            var row878 = new OldStressRowData();
            row878.LineNo = 5;
            row878.MaterialId = 9253;
            row878.SpecNo = "SA-182";
            row878.TypeGrade = "F304N";
            row878.ProductForm = "Forgings";
            row878.AlloyUNS = "S30451";
            row878.ClassCondition = "";
            row878.Notes = "G5";
            row878.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row878);

            // Row 6: SA-213, TP304N, Smls. tube
            var row879 = new OldStressRowData();
            row879.LineNo = 6;
            row879.MaterialId = 9254;
            row879.SpecNo = "SA-213";
            row879.TypeGrade = "TP304N";
            row879.ProductForm = "Smls. tube";
            row879.AlloyUNS = "S30451";
            row879.ClassCondition = "";
            row879.Notes = "G5, G12, G18, T7";
            row879.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row879);

            // Row 7: SA-213, TP304N, Smls. tube
            var row880 = new OldStressRowData();
            row880.LineNo = 7;
            row880.MaterialId = 9255;
            row880.SpecNo = "SA-213";
            row880.TypeGrade = "TP304N";
            row880.ProductForm = "Smls. tube";
            row880.AlloyUNS = "S30451";
            row880.ClassCondition = "";
            row880.Notes = "G12, G18, T8";
            row880.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row880);

            // Row 8: SA-240, 304N, Plate
            var row881 = new OldStressRowData();
            row881.LineNo = 8;
            row881.MaterialId = 9256;
            row881.SpecNo = "SA-240";
            row881.TypeGrade = "304N";
            row881.ProductForm = "Plate";
            row881.AlloyUNS = "S30451";
            row881.ClassCondition = "";
            row881.Notes = "G5, G12, T7";
            row881.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row881);

            // Row 9: SA-240, 304N, Plate
            var row882 = new OldStressRowData();
            row882.LineNo = 9;
            row882.MaterialId = 9257;
            row882.SpecNo = "SA-240";
            row882.TypeGrade = "304N";
            row882.ProductForm = "Plate";
            row882.AlloyUNS = "S30451";
            row882.ClassCondition = "";
            row882.Notes = "G12, T8";
            row882.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row882);

            // Row 10: SA-249, TP304N, Wld. tube
            var row883 = new OldStressRowData();
            row883.LineNo = 10;
            row883.MaterialId = 9258;
            row883.SpecNo = "SA-249";
            row883.TypeGrade = "TP304N";
            row883.ProductForm = "Wld. tube";
            row883.AlloyUNS = "S30451";
            row883.ClassCondition = "";
            row883.Notes = "G5, G12, G18, T7, W13";
            row883.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row883);

            // Row 11: SA-249, TP304N, Wld. tube
            var row884 = new OldStressRowData();
            row884.LineNo = 11;
            row884.MaterialId = 9261;
            row884.SpecNo = "SA-249";
            row884.TypeGrade = "TP304N";
            row884.ProductForm = "Wld. tube";
            row884.AlloyUNS = "S30451";
            row884.ClassCondition = "";
            row884.Notes = "G12, G18, T8, W13";
            row884.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row884);

            // Row 12: SA-249, TP304N, Wld. tube
            var row885 = new OldStressRowData();
            row885.LineNo = 12;
            row885.MaterialId = 9259;
            row885.SpecNo = "SA-249";
            row885.TypeGrade = "TP304N";
            row885.ProductForm = "Wld. tube";
            row885.AlloyUNS = "S30451";
            row885.ClassCondition = "";
            row885.Notes = "G3, G5, G12, G18, T5";
            row885.StressValues = new double?[] { 19.4, null, 19.4, null, 18.5, 17.3, 16, 15.2, 14.9, 14.6, 14.4, 14.1, 13.8, 13.6, 13.3, 13, 10.5, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row885);

            // Row 13: SA-249, TP304N, Wld. tube
            var row886 = new OldStressRowData();
            row886.LineNo = 13;
            row886.MaterialId = 9262;
            row886.SpecNo = "SA-249";
            row886.TypeGrade = "TP304N";
            row886.ProductForm = "Wld. tube";
            row886.AlloyUNS = "S30451";
            row886.ClassCondition = "";
            row886.Notes = "G3, G12, G18, G24, T8";
            row886.StressValues = new double?[] { 19.4, null, 16.2, null, 14.2, 12.8, 11.9, 11.3, 11, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.6, 9.4, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row886);

            // Row 14: SA-249, TP304N, Wld. tube
            var row887 = new OldStressRowData();
            row887.LineNo = 14;
            row887.MaterialId = 9260;
            row887.SpecNo = "SA-249";
            row887.TypeGrade = "TP304N";
            row887.ProductForm = "Wld. tube";
            row887.AlloyUNS = "S30451";
            row887.ClassCondition = "";
            row887.Notes = "G5, G12, G24, T5";
            row887.StressValues = new double?[] { 19.4, null, 19.4, null, 18.5, 17.3, 16, 15.2, 14.9, 14.6, 14.4, 14.1, 13.8, 13.6, 13.3, 13, 10.5, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row887);

            // Row 15: SA-312, TP304N, Smls. & wld. pipe
            var row888 = new OldStressRowData();
            row888.LineNo = 15;
            row888.MaterialId = 9267;
            row888.SpecNo = "SA-312";
            row888.TypeGrade = "TP304N";
            row888.ProductForm = "Smls. & wld. pipe";
            row888.AlloyUNS = "S30451";
            row888.ClassCondition = "";
            row888.Notes = "G5, G12, G18, T7, W12, W13";
            row888.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row888);

            // Row 16: SA-312, TP304N, Smls. & wld. pipe
            var row889 = new OldStressRowData();
            row889.LineNo = 16;
            row889.MaterialId = 9270;
            row889.SpecNo = "SA-312";
            row889.TypeGrade = "TP304N";
            row889.ProductForm = "Smls. & wld. pipe";
            row889.AlloyUNS = "S30451";
            row889.ClassCondition = "";
            row889.Notes = "G12, G18, T8, W13";
            row889.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row889);

            // Row 17: SA-312, TP304N, Wld. pipe
            var row890 = new OldStressRowData();
            row890.LineNo = 17;
            row890.MaterialId = 9268;
            row890.SpecNo = "SA-312";
            row890.TypeGrade = "TP304N";
            row890.ProductForm = "Wld. pipe";
            row890.AlloyUNS = "S30451";
            row890.ClassCondition = "";
            row890.Notes = "G3, G5, G12, G18, G24, T5";
            row890.StressValues = new double?[] { 19.4, null, 19.4, null, 18.5, 17.3, 16, 15.2, 14.9, 14.6, 14.4, 14.1, 13.8, 13.6, 13.3, 13, 10.5, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row890);

            // Row 18: SA-312, TP304N, Wld. pipe
            var row891 = new OldStressRowData();
            row891.LineNo = 18;
            row891.MaterialId = 9264;
            row891.SpecNo = "SA-312";
            row891.TypeGrade = "TP304N";
            row891.ProductForm = "Wld. pipe";
            row891.AlloyUNS = "S30451";
            row891.ClassCondition = "";
            row891.Notes = "G3, G12, G18, G24, T8";
            row891.StressValues = new double?[] { 19.4, null, 16.2, null, 14.2, 12.8, 11.9, 11.3, 11, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.6, 9.4, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row891);

            // Row 19: SA-336, F304N, Forgings
            var row892 = new OldStressRowData();
            row892.LineNo = 19;
            row892.MaterialId = 9272;
            row892.SpecNo = "SA-336";
            row892.TypeGrade = "F304N";
            row892.ProductForm = "Forgings";
            row892.AlloyUNS = "S30451";
            row892.ClassCondition = "";
            row892.Notes = "G5, G12, T7";
            row892.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row892);

            // Row 20: SA-336, F304N, Forgings
            var row893 = new OldStressRowData();
            row893.LineNo = 20;
            row893.MaterialId = 9271;
            row893.SpecNo = "SA-336";
            row893.TypeGrade = "F304N";
            row893.ProductForm = "Forgings";
            row893.AlloyUNS = "S30451";
            row893.ClassCondition = "";
            row893.Notes = "G12, T8";
            row893.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row893);

            // Row 21: SA-358, 304N, Wld. pipe
            var row894 = new OldStressRowData();
            row894.LineNo = 21;
            row894.MaterialId = 9273;
            row894.SpecNo = "SA-358";
            row894.TypeGrade = "304N";
            row894.ProductForm = "Wld. pipe";
            row894.AlloyUNS = "S30451";
            row894.ClassCondition = "1";
            row894.Notes = "G5, W12";
            row894.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row894);

            // Row 22: SA-376, TP304N, Smls. pipe
            var row895 = new OldStressRowData();
            row895.LineNo = 22;
            row895.MaterialId = 9274;
            row895.SpecNo = "SA-376";
            row895.TypeGrade = "TP304N";
            row895.ProductForm = "Smls. pipe";
            row895.AlloyUNS = "S30451";
            row895.ClassCondition = "";
            row895.Notes = "G5, G12, G18, H1, T7";
            row895.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row895);

            // Row 23: SA-376, TP304N, Smls. pipe
            var row896 = new OldStressRowData();
            row896.LineNo = 23;
            row896.MaterialId = 9275;
            row896.SpecNo = "SA-376";
            row896.TypeGrade = "TP304N";
            row896.ProductForm = "Smls. pipe";
            row896.AlloyUNS = "S30451";
            row896.ClassCondition = "";
            row896.Notes = "G12, G18, H1, T8";
            row896.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row896);

            // Row 24: SA-403, 304N, Smls. & wld. fittings
            var row897 = new OldStressRowData();
            row897.LineNo = 24;
            row897.MaterialId = 9277;
            row897.SpecNo = "SA-403";
            row897.TypeGrade = "304N";
            row897.ProductForm = "Smls. & wld. fittings";
            row897.AlloyUNS = "S30451";
            row897.ClassCondition = "";
            row897.Notes = "G5, T7, W12, W14";
            row897.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row897);

            // Row 25: SA-479, 304N, Bar
            var row898 = new OldStressRowData();
            row898.LineNo = 25;
            row898.MaterialId = 9282;
            row898.SpecNo = "SA-479";
            row898.TypeGrade = "304N";
            row898.ProductForm = "Bar";
            row898.AlloyUNS = "S30451";
            row898.ClassCondition = "";
            row898.Notes = "G12, G18, T8";
            row898.StressValues = new double?[] { 22.9, null, 19.1, null, 16.7, 15.1, 14, 13.3, 13, 12.8, 12.5, 12.3, 12.1, 11.8, 11.6, 11.3, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row898);

            // Row 26: SA-479, 304N, Bar
            var row899 = new OldStressRowData();
            row899.LineNo = 26;
            row899.MaterialId = 9281;
            row899.SpecNo = "SA-479";
            row899.TypeGrade = "304N";
            row899.ProductForm = "Bar";
            row899.AlloyUNS = "S30451";
            row899.ClassCondition = "";
            row899.Notes = "G5, G12, G18, T7";
            row899.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, 16.3, 16, 15.6, 15.2, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row899);

            // Row 27: SA-688, TP304N, Wld. tube
            var row900 = new OldStressRowData();
            row900.LineNo = 27;
            row900.MaterialId = 9283;
            row900.SpecNo = "SA-688";
            row900.TypeGrade = "TP304N";
            row900.ProductForm = "Wld. tube";
            row900.AlloyUNS = "S30451";
            row900.ClassCondition = "";
            row900.Notes = "G5, W12";
            row900.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
