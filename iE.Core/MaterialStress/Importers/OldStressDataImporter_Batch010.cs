using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch010
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch010(MaterialStressService service)
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

            // Row 10: SA-430, FP304H, Forged pipe
            var row901 = new OldStressRowData();
            row901.LineNo = 10;
            row901.MaterialId = 9147;
            row901.SpecNo = "SA-430";
            row901.TypeGrade = "FP304H";
            row901.ProductForm = "Forged pipe";
            row901.AlloyUNS = "S30409";
            row901.ClassCondition = "";
            row901.Notes = "G5, G18, H1";
            row901.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.7, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row901);

            // Row 11: SA-430, FP304H, Forged pipe
            var row902 = new OldStressRowData();
            row902.LineNo = 11;
            row902.MaterialId = 9148;
            row902.SpecNo = "SA-430";
            row902.TypeGrade = "FP304H";
            row902.ProductForm = "Forged pipe";
            row902.AlloyUNS = "S30409";
            row902.ClassCondition = "";
            row902.Notes = "G18, H1";
            row902.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row902);

            // Row 12: SA-451, CPF3, Cast pipe
            var row903 = new OldStressRowData();
            row903.LineNo = 12;
            row903.MaterialId = 9149;
            row903.SpecNo = "SA-451";
            row903.TypeGrade = "CPF3";
            row903.ProductForm = "Cast pipe";
            row903.AlloyUNS = "J92500";
            row903.ClassCondition = "";
            row903.Notes = "G5, G16, G17, G32";
            row903.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row903);

            // Row 13: SA-451, CPF8, Cast pipe
            var row904 = new OldStressRowData();
            row904.LineNo = 13;
            row904.MaterialId = 9150;
            row904.SpecNo = "SA-451";
            row904.TypeGrade = "CPF8";
            row904.ProductForm = "Cast pipe";
            row904.AlloyUNS = "J92600";
            row904.ClassCondition = "";
            row904.Notes = "G5, G16, G17, G32";
            row904.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row904);

            // Row 14: SA-182, F304, Forgings
            var row905 = new OldStressRowData();
            row905.LineNo = 14;
            row905.MaterialId = 9152;
            row905.SpecNo = "SA-182";
            row905.TypeGrade = "F304";
            row905.ProductForm = "Forgings";
            row905.AlloyUNS = "S30400";
            row905.ClassCondition = "";
            row905.Notes = "G12, G18";
            row905.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row905);

            // Row 15: SA-182, F304, Forgings
            var row906 = new OldStressRowData();
            row906.LineNo = 15;
            row906.MaterialId = 9151;
            row906.SpecNo = "SA-182";
            row906.TypeGrade = "F304";
            row906.ProductForm = "Forgings";
            row906.AlloyUNS = "S30400";
            row906.ClassCondition = "";
            row906.Notes = "G5, G12, G18";
            row906.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row906);

            // Row 16: SA-182, F304H, Forgings
            var row907 = new OldStressRowData();
            row907.LineNo = 16;
            row907.MaterialId = 9154;
            row907.SpecNo = "SA-182";
            row907.TypeGrade = "F304H";
            row907.ProductForm = "Forgings";
            row907.AlloyUNS = "S30409";
            row907.ClassCondition = "";
            row907.Notes = "G18";
            row907.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row907);

            // Row 17: SA-182, F304H, Forgings
            var row908 = new OldStressRowData();
            row908.LineNo = 17;
            row908.MaterialId = 9153;
            row908.SpecNo = "SA-182";
            row908.TypeGrade = "F304H";
            row908.ProductForm = "Forgings";
            row908.AlloyUNS = "S30409";
            row908.ClassCondition = "";
            row908.Notes = "G5, G18";
            row908.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row908);

            // Row 18: SA-213, TP304, Smls. tube
            var row909 = new OldStressRowData();
            row909.LineNo = 18;
            row909.MaterialId = 9156;
            row909.SpecNo = "SA-213";
            row909.TypeGrade = "TP304";
            row909.ProductForm = "Smls. tube";
            row909.AlloyUNS = "S30400";
            row909.ClassCondition = "";
            row909.Notes = "G12, G18";
            row909.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row909);

            // Row 19: SA-213, TP304, Smls. tube
            var row910 = new OldStressRowData();
            row910.LineNo = 19;
            row910.MaterialId = 9155;
            row910.SpecNo = "SA-213";
            row910.TypeGrade = "TP304";
            row910.ProductForm = "Smls. tube";
            row910.AlloyUNS = "S30400";
            row910.ClassCondition = "";
            row910.Notes = "G5, G12, G18";
            row910.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row910);

            // Row 20: SA-213, TP304H, Smls. tube
            var row911 = new OldStressRowData();
            row911.LineNo = 20;
            row911.MaterialId = 9158;
            row911.SpecNo = "SA-213";
            row911.TypeGrade = "TP304H";
            row911.ProductForm = "Smls. tube";
            row911.AlloyUNS = "S30409";
            row911.ClassCondition = "";
            row911.Notes = "G18";
            row911.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row911);

            // Row 21: SA-213, TP304H, Smls. tube
            var row912 = new OldStressRowData();
            row912.LineNo = 21;
            row912.MaterialId = 9157;
            row912.SpecNo = "SA-213";
            row912.TypeGrade = "TP304H";
            row912.ProductForm = "Smls. tube";
            row912.AlloyUNS = "S30409";
            row912.ClassCondition = "";
            row912.Notes = "G5, G18";
            row912.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row912);

            // Row 22: SA-240, 302, Plate
            var row913 = new OldStressRowData();
            row913.LineNo = 22;
            row913.MaterialId = 9159;
            row913.SpecNo = "SA-240";
            row913.TypeGrade = "302";
            row913.ProductForm = "Plate";
            row913.AlloyUNS = "S30200";
            row913.ClassCondition = "";
            row913.Notes = "G5";
            row913.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row913);

            // Row 23: SA-240, 302, Plate
            var row914 = new OldStressRowData();
            row914.LineNo = 23;
            row914.MaterialId = 9160;
            row914.SpecNo = "SA-240";
            row914.TypeGrade = "302";
            row914.ProductForm = "Plate";
            row914.AlloyUNS = "S30200";
            row914.ClassCondition = "";
            row914.Notes = "";
            row914.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row914);

            // Row 24: SA-240, 304, Plate
            var row915 = new OldStressRowData();
            row915.LineNo = 24;
            row915.MaterialId = 9162;
            row915.SpecNo = "SA-240";
            row915.TypeGrade = "304";
            row915.ProductForm = "Plate";
            row915.AlloyUNS = "S30400";
            row915.ClassCondition = "";
            row915.Notes = "G12, G18";
            row915.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row915);

            // Row 25: SA-240, 304, Plate
            var row916 = new OldStressRowData();
            row916.LineNo = 25;
            row916.MaterialId = 9161;
            row916.SpecNo = "SA-240";
            row916.TypeGrade = "304";
            row916.ProductForm = "Plate";
            row916.AlloyUNS = "S30400";
            row916.ClassCondition = "";
            row916.Notes = "G5, G12, G18, H1";
            row916.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row916);

            // Row 26: SA-240, 304H, Plate
            var row917 = new OldStressRowData();
            row917.LineNo = 26;
            row917.MaterialId = 9163;
            row917.SpecNo = "SA-240";
            row917.TypeGrade = "304H";
            row917.ProductForm = "Plate";
            row917.AlloyUNS = "S30409";
            row917.ClassCondition = "";
            row917.Notes = "G5";
            row917.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row917);

            // Row 27: SA-240, 304H, Plate
            var row918 = new OldStressRowData();
            row918.LineNo = 27;
            row918.MaterialId = 9164;
            row918.SpecNo = "SA-240";
            row918.TypeGrade = "304H";
            row918.ProductForm = "Plate";
            row918.AlloyUNS = "S30409";
            row918.ClassCondition = "";
            row918.Notes = "";
            row918.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row918);

            // Row 28: SA-249, TP304, Wld. tube
            var row919 = new OldStressRowData();
            row919.LineNo = 28;
            row919.MaterialId = 9167;
            row919.SpecNo = "SA-249";
            row919.TypeGrade = "TP304";
            row919.ProductForm = "Wld. tube";
            row919.AlloyUNS = "S30400";
            row919.ClassCondition = "";
            row919.Notes = "G3, G5, G12, G18";
            row919.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 12, 10.5, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row919);

            // Row 29: SA-249, TP304, Wld. tube
            var row920 = new OldStressRowData();
            row920.LineNo = 29;
            row920.MaterialId = 9169;
            row920.SpecNo = "SA-249";
            row920.TypeGrade = "TP304";
            row920.ProductForm = "Wld. tube";
            row920.AlloyUNS = "S30400";
            row920.ClassCondition = "";
            row920.Notes = "G12, G18, W14";
            row920.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row920);

            // Row 30: SA-249, TP304, Wld. tube
            var row921 = new OldStressRowData();
            row921.LineNo = 30;
            row921.MaterialId = 9170;
            row921.SpecNo = "SA-249";
            row921.TypeGrade = "TP304";
            row921.ProductForm = "Wld. tube";
            row921.AlloyUNS = "S30400";
            row921.ClassCondition = "";
            row921.Notes = "G3, G12, G18";
            row921.StressValues = new double?[] { 16, null, 14.2, null, 12.8, 11.7, 11, 10.3, 10.2, 10, 9.8, 9.5, 9.4, 9.3, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row921);

            // Row 31: SA-249, TP304, Wld. tube
            var row922 = new OldStressRowData();
            row922.LineNo = 31;
            row922.MaterialId = 9165;
            row922.SpecNo = "SA-249";
            row922.TypeGrade = "TP304";
            row922.ProductForm = "Wld. tube";
            row922.AlloyUNS = "S30400";
            row922.ClassCondition = "";
            row922.Notes = "G5, G12, G18, W12, W14";
            row922.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row922);

            // Row 32: SA-249, TP304, Wld. tube
            var row923 = new OldStressRowData();
            row923.LineNo = 32;
            row923.MaterialId = 9166;
            row923.SpecNo = "SA-249";
            row923.TypeGrade = "TP304";
            row923.ProductForm = "Wld. tube";
            row923.AlloyUNS = "S30400";
            row923.ClassCondition = "";
            row923.Notes = "G5, G12, G24";
            row923.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 11.7, 10.4, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row923);

            // Row 33: SA-249, TP304, Wld. tube
            var row924 = new OldStressRowData();
            row924.LineNo = 33;
            row924.MaterialId = 9168;
            row924.SpecNo = "SA-249";
            row924.TypeGrade = "TP304";
            row924.ProductForm = "Wld. tube";
            row924.AlloyUNS = "S30400";
            row924.ClassCondition = "";
            row924.Notes = "G12, G24";
            row924.StressValues = new double?[] { 16, null, 13.3, null, 12, 11, 10.4, 9.7, 9.6, 9.4, 9.2, 9, 8.8, 8.7, 8.5, 8.3, 8.1, 7.5, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row924);

            // Row 34: SA-249, TP304H, Wld. tube
            var row925 = new OldStressRowData();
            row925.LineNo = 34;
            row925.MaterialId = 9172;
            row925.SpecNo = "SA-249";
            row925.TypeGrade = "TP304H";
            row925.ProductForm = "Wld. tube";
            row925.AlloyUNS = "S30409";
            row925.ClassCondition = "";
            row925.Notes = "G3, G5, G18";
            row925.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 12, 10.5, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row925);

            // Row 35: SA-249, TP304H, Wld. tube
            var row926 = new OldStressRowData();
            row926.LineNo = 35;
            row926.MaterialId = 9174;
            row926.SpecNo = "SA-249";
            row926.TypeGrade = "TP304H";
            row926.ProductForm = "Wld. tube";
            row926.AlloyUNS = "S30409";
            row926.ClassCondition = "";
            row926.Notes = "G18, W14";
            row926.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row926);

            // Row 36: SA-249, TP304H, Wld. tube
            var row927 = new OldStressRowData();
            row927.LineNo = 36;
            row927.MaterialId = 9175;
            row927.SpecNo = "SA-249";
            row927.TypeGrade = "TP304H";
            row927.ProductForm = "Wld. tube";
            row927.AlloyUNS = "S30409";
            row927.ClassCondition = "";
            row927.Notes = "G3, G18";
            row927.StressValues = new double?[] { 16, null, 14.2, null, 12.8, 11.7, 11, 10.3, 10.2, 10, 9.8, 9.5, 9.4, 9.3, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row927);

            // Row 37: SA-249, TP304H, Wld. tube
            var row928 = new OldStressRowData();
            row928.LineNo = 37;
            row928.MaterialId = 9176;
            row928.SpecNo = "SA-249";
            row928.TypeGrade = "TP304H";
            row928.ProductForm = "Wld. tube";
            row928.AlloyUNS = "S30409";
            row928.ClassCondition = "";
            row928.Notes = "G5, G18, W12, W14";
            row928.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row928);

            // Row 38: SA-249, TP304H, Wld. tube
            var row929 = new OldStressRowData();
            row929.LineNo = 38;
            row929.MaterialId = 9171;
            row929.SpecNo = "SA-249";
            row929.TypeGrade = "TP304H";
            row929.ProductForm = "Wld. tube";
            row929.AlloyUNS = "S30409";
            row929.ClassCondition = "";
            row929.Notes = "G5, G24";
            row929.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 11.7, 10.4, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row929);

            // Row 39: SA-249, TP304H, Wld. tube
            var row930 = new OldStressRowData();
            row930.LineNo = 39;
            row930.MaterialId = 9173;
            row930.SpecNo = "SA-249";
            row930.TypeGrade = "TP304H";
            row930.ProductForm = "Wld. tube";
            row930.AlloyUNS = "S30409";
            row930.ClassCondition = "";
            row930.Notes = "G24";
            row930.StressValues = new double?[] { 16, null, 13.3, null, 12, 11, 10.4, 9.7, 9.6, 9.4, 9.2, 9, 8.8, 8.7, 8.5, 8.3, 8.1, 7.5, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row930);

            // Row 1: SA-312, TP304, Wld. & smls. pipe
            var row931 = new OldStressRowData();
            row931.LineNo = 1;
            row931.MaterialId = 9177;
            row931.SpecNo = "SA-312";
            row931.TypeGrade = "TP304";
            row931.ProductForm = "Wld. & smls. pipe";
            row931.AlloyUNS = "S30400";
            row931.ClassCondition = "";
            row931.Notes = "G5, W12";
            row931.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row931);

            // Row 2: SA-312, TP304, Smls. pipe
            var row932 = new OldStressRowData();
            row932.LineNo = 2;
            row932.MaterialId = 9178;
            row932.SpecNo = "SA-312";
            row932.TypeGrade = "TP304";
            row932.ProductForm = "Smls. pipe";
            row932.AlloyUNS = "S30400";
            row932.ClassCondition = "";
            row932.Notes = "G5, G12, G18";
            row932.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row932);

            // Row 3: SA-312, TP304, Smls. pipe
            var row933 = new OldStressRowData();
            row933.LineNo = 3;
            row933.MaterialId = 9182;
            row933.SpecNo = "SA-312";
            row933.TypeGrade = "TP304";
            row933.ProductForm = "Smls. pipe";
            row933.AlloyUNS = "S30400";
            row933.ClassCondition = "";
            row933.Notes = "G12, G18";
            row933.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row933);

            // Row 4: SA-312, TP304, Wld. pipe
            var row934 = new OldStressRowData();
            row934.LineNo = 4;
            row934.MaterialId = 9180;
            row934.SpecNo = "SA-312";
            row934.TypeGrade = "TP304";
            row934.ProductForm = "Wld. pipe";
            row934.AlloyUNS = "S30400";
            row934.ClassCondition = "";
            row934.Notes = "G5, G12, G18, W14";
            row934.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row934);

            // Row 5: SA-312, TP304, Wld. pipe
            var row935 = new OldStressRowData();
            row935.LineNo = 5;
            row935.MaterialId = 9184;
            row935.SpecNo = "SA-312";
            row935.TypeGrade = "TP304";
            row935.ProductForm = "Wld. pipe";
            row935.AlloyUNS = "S30400";
            row935.ClassCondition = "";
            row935.Notes = "G12, G18, W14";
            row935.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row935);

            // Row 6: SA-312, TP304, Wld. pipe
            var row936 = new OldStressRowData();
            row936.LineNo = 6;
            row936.MaterialId = 9181;
            row936.SpecNo = "SA-312";
            row936.TypeGrade = "TP304";
            row936.ProductForm = "Wld. pipe";
            row936.AlloyUNS = "S30400";
            row936.ClassCondition = "";
            row936.Notes = "G3, G5, G12, G18";
            row936.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 12, 10.5, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row936);

            // Row 7: SA-312, TP304, Wld. pipe
            var row937 = new OldStressRowData();
            row937.LineNo = 7;
            row937.MaterialId = 9185;
            row937.SpecNo = "SA-312";
            row937.TypeGrade = "TP304";
            row937.ProductForm = "Wld. pipe";
            row937.AlloyUNS = "S30400";
            row937.ClassCondition = "";
            row937.Notes = "G3, G12, G18";
            row937.StressValues = new double?[] { 16, null, 14.2, null, 12.8, 11.7, 11, 10.3, 10.2, 10, 9.8, 9.5, 9.4, 9.3, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row937);

            // Row 8: SA-312, TP304, Wld. pipe
            var row938 = new OldStressRowData();
            row938.LineNo = 8;
            row938.MaterialId = 9179;
            row938.SpecNo = "SA-312";
            row938.TypeGrade = "TP304";
            row938.ProductForm = "Wld. pipe";
            row938.AlloyUNS = "S30400";
            row938.ClassCondition = "";
            row938.Notes = "G5, G12, G24";
            row938.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 11.7, 10.4, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row938);

            // Row 9: SA-312, TP304, Wld. pipe
            var row939 = new OldStressRowData();
            row939.LineNo = 9;
            row939.MaterialId = 9183;
            row939.SpecNo = "SA-312";
            row939.TypeGrade = "TP304";
            row939.ProductForm = "Wld. pipe";
            row939.AlloyUNS = "S30400";
            row939.ClassCondition = "";
            row939.Notes = "G12, G24";
            row939.StressValues = new double?[] { 16, null, 13.3, null, 12, 11, 10.4, 9.7, 9.6, 9.4, 9.2, 9, 8.8, 8.7, 8.5, 8.3, 8.1, 7.5, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row939);

            // Row 10: SA-312, TP304H, Smls. pipe
            var row940 = new OldStressRowData();
            row940.LineNo = 10;
            row940.MaterialId = 9186;
            row940.SpecNo = "SA-312";
            row940.TypeGrade = "TP304H";
            row940.ProductForm = "Smls. pipe";
            row940.AlloyUNS = "S30409";
            row940.ClassCondition = "";
            row940.Notes = "G5, G18";
            row940.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row940);

            // Row 11: SA-312, TP304H, Smls. pipe
            var row941 = new OldStressRowData();
            row941.LineNo = 11;
            row941.MaterialId = 9187;
            row941.SpecNo = "SA-312";
            row941.TypeGrade = "TP304H";
            row941.ProductForm = "Smls. pipe";
            row941.AlloyUNS = "S30409";
            row941.ClassCondition = "";
            row941.Notes = "G18";
            row941.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row941);

            // Row 12: SA-312, TP304H, Wld. pipe
            var row942 = new OldStressRowData();
            row942.LineNo = 12;
            row942.MaterialId = 9189;
            row942.SpecNo = "SA-312";
            row942.TypeGrade = "TP304H";
            row942.ProductForm = "Wld. pipe";
            row942.AlloyUNS = "S30409";
            row942.ClassCondition = "";
            row942.Notes = "G5, G18, W14";
            row942.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row942);

            // Row 13: SA-312, TP304H, Wld. pipe
            var row943 = new OldStressRowData();
            row943.LineNo = 13;
            row943.MaterialId = 9192;
            row943.SpecNo = "SA-312";
            row943.TypeGrade = "TP304H";
            row943.ProductForm = "Wld. pipe";
            row943.AlloyUNS = "S30409";
            row943.ClassCondition = "";
            row943.Notes = "G18, W14";
            row943.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.6, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row943);

            // Row 14: SA-312, TP304H, Wld. pipe
            var row944 = new OldStressRowData();
            row944.LineNo = 14;
            row944.MaterialId = 9190;
            row944.SpecNo = "SA-312";
            row944.TypeGrade = "TP304H";
            row944.ProductForm = "Wld. pipe";
            row944.AlloyUNS = "S30409";
            row944.ClassCondition = "";
            row944.Notes = "G3, G5, G18";
            row944.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 12, 10.5, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row944);

            // Row 15: SA-312, TP304H, Wld. pipe
            var row945 = new OldStressRowData();
            row945.LineNo = 15;
            row945.MaterialId = 9193;
            row945.SpecNo = "SA-312";
            row945.TypeGrade = "TP304H";
            row945.ProductForm = "Wld. pipe";
            row945.AlloyUNS = "S30409";
            row945.ClassCondition = "";
            row945.Notes = "G3, G18";
            row945.StressValues = new double?[] { 16, null, 14.2, null, 12.8, 11.7, 11, 10.3, 10.2, 10, 9.8, 9.5, 9.4, 9.3, 9, 8.8, 8.6, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row945);

            // Row 16: SA-312, TP304H, Wld. pipe
            var row946 = new OldStressRowData();
            row946.LineNo = 16;
            row946.MaterialId = 9188;
            row946.SpecNo = "SA-312";
            row946.TypeGrade = "TP304H";
            row946.ProductForm = "Wld. pipe";
            row946.AlloyUNS = "S30409";
            row946.ClassCondition = "";
            row946.Notes = "G5, G24";
            row946.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 11.7, 10.4, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row946);

            // Row 17: SA-312, TP304H, Wld. pipe
            var row947 = new OldStressRowData();
            row947.LineNo = 17;
            row947.MaterialId = 9191;
            row947.SpecNo = "SA-312";
            row947.TypeGrade = "TP304H";
            row947.ProductForm = "Wld. pipe";
            row947.AlloyUNS = "S30409";
            row947.ClassCondition = "";
            row947.Notes = "G24";
            row947.StressValues = new double?[] { 16, null, 13.3, null, 12, 11, 10.4, 9.7, 9.6, 9.4, 9.2, 9, 8.8, 8.7, 8.5, 8.3, 8.1, 7.5, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row947);

            // Row 18: SA-312, TP304H, Wld. & smls. pipe
            var row948 = new OldStressRowData();
            row948.LineNo = 18;
            row948.MaterialId = 9194;
            row948.SpecNo = "SA-312";
            row948.TypeGrade = "TP304H";
            row948.ProductForm = "Wld. & smls. pipe";
            row948.AlloyUNS = "S30409";
            row948.ClassCondition = "";
            row948.Notes = "G5, W12";
            row948.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row948);

            // Row 19: SA-358, 304, Wld. pipe
            var row949 = new OldStressRowData();
            row949.LineNo = 19;
            row949.MaterialId = 9195;
            row949.SpecNo = "SA-358";
            row949.TypeGrade = "304";
            row949.ProductForm = "Wld. pipe";
            row949.AlloyUNS = "S30400";
            row949.ClassCondition = "1";
            row949.Notes = "G5, W12";
            row949.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row949);

            // Row 20: SA-358, 304H, Wld. pipe
            var row950 = new OldStressRowData();
            row950.LineNo = 20;
            row950.MaterialId = 9196;
            row950.SpecNo = "SA-358";
            row950.TypeGrade = "304H";
            row950.ProductForm = "Wld. pipe";
            row950.AlloyUNS = "S30409";
            row950.ClassCondition = "1";
            row950.Notes = "G5, W12";
            row950.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row950);

            // Row 21: SA-358, 304LN, Wld. pipe
            var row951 = new OldStressRowData();
            row951.LineNo = 21;
            row951.MaterialId = 9197;
            row951.SpecNo = "SA-358";
            row951.TypeGrade = "304LN";
            row951.ProductForm = "Wld. pipe";
            row951.AlloyUNS = "S30453";
            row951.ClassCondition = "1";
            row951.Notes = "G5, W12";
            row951.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row951);

            // Row 22: SA-376, TP304, Smls. pipe
            var row952 = new OldStressRowData();
            row952.LineNo = 22;
            row952.MaterialId = 9198;
            row952.SpecNo = "SA-376";
            row952.TypeGrade = "TP304";
            row952.ProductForm = "Smls. pipe";
            row952.AlloyUNS = "S30400";
            row952.ClassCondition = "";
            row952.Notes = "G5, G12, G18, H1";
            row952.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row952);

            // Row 23: SA-376, TP304, Smls. pipe
            var row953 = new OldStressRowData();
            row953.LineNo = 23;
            row953.MaterialId = 9199;
            row953.SpecNo = "SA-376";
            row953.TypeGrade = "TP304";
            row953.ProductForm = "Smls. pipe";
            row953.AlloyUNS = "S30400";
            row953.ClassCondition = "";
            row953.Notes = "G12, G18, H1";
            row953.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row953);

            // Row 24: SA-376, TP304H, Smls. pipe
            var row954 = new OldStressRowData();
            row954.LineNo = 24;
            row954.MaterialId = 9200;
            row954.SpecNo = "SA-376";
            row954.TypeGrade = "TP304H";
            row954.ProductForm = "Smls. pipe";
            row954.AlloyUNS = "S30409";
            row954.ClassCondition = "";
            row954.Notes = "G5, G18, H1";
            row954.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row954);

            // Row 25: SA-376, TP304H, Smls. pipe
            var row955 = new OldStressRowData();
            row955.LineNo = 25;
            row955.MaterialId = 9201;
            row955.SpecNo = "SA-376";
            row955.TypeGrade = "TP304H";
            row955.ProductForm = "Smls. pipe";
            row955.AlloyUNS = "S30409";
            row955.ClassCondition = "";
            row955.Notes = "G18, H1";
            row955.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row955);

            // Row 26: ..., , 
            var row956 = new OldStressRowData();
            row956.LineNo = 26;
            row956.MaterialId = 9202;
            row956.SpecNo = "...";
            row956.TypeGrade = "";
            row956.ProductForm = "";
            row956.AlloyUNS = "";
            row956.ClassCondition = "";
            row956.Notes = "";
            row956.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row956);

            // Row 27: ..., , 
            var row957 = new OldStressRowData();
            row957.LineNo = 27;
            row957.MaterialId = 9203;
            row957.SpecNo = "...";
            row957.TypeGrade = "";
            row957.ProductForm = "";
            row957.AlloyUNS = "";
            row957.ClassCondition = "";
            row957.Notes = "";
            row957.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row957);

            // Row 28: SA-403, 304, Smls. & wld. fittings
            var row958 = new OldStressRowData();
            row958.LineNo = 28;
            row958.MaterialId = 9207;
            row958.SpecNo = "SA-403";
            row958.TypeGrade = "304";
            row958.ProductForm = "Smls. & wld. fittings";
            row958.AlloyUNS = "S30400";
            row958.ClassCondition = "";
            row958.Notes = "G5, G12, W12, W15";
            row958.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row958);

            // Row 29: ..., , 
            var row959 = new OldStressRowData();
            row959.LineNo = 29;
            row959.MaterialId = 9205;
            row959.SpecNo = "...";
            row959.TypeGrade = "";
            row959.ProductForm = "";
            row959.AlloyUNS = "";
            row959.ClassCondition = "";
            row959.Notes = "";
            row959.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row959);

            // Row 30: ..., , 
            var row960 = new OldStressRowData();
            row960.LineNo = 30;
            row960.MaterialId = 9204;
            row960.SpecNo = "...";
            row960.TypeGrade = "";
            row960.ProductForm = "";
            row960.AlloyUNS = "";
            row960.ClassCondition = "";
            row960.Notes = "";
            row960.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row960);

            // Row 31: ..., , 
            var row961 = new OldStressRowData();
            row961.LineNo = 31;
            row961.MaterialId = 9206;
            row961.SpecNo = "...";
            row961.TypeGrade = "";
            row961.ProductForm = "";
            row961.AlloyUNS = "";
            row961.ClassCondition = "";
            row961.Notes = "";
            row961.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row961);

            // Row 32: ..., , 
            var row962 = new OldStressRowData();
            row962.LineNo = 32;
            row962.MaterialId = 9208;
            row962.SpecNo = "...";
            row962.TypeGrade = "";
            row962.ProductForm = "";
            row962.AlloyUNS = "";
            row962.ClassCondition = "";
            row962.Notes = "";
            row962.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row962);

            // Row 33: ..., , 
            var row963 = new OldStressRowData();
            row963.LineNo = 33;
            row963.MaterialId = 9211;
            row963.SpecNo = "...";
            row963.TypeGrade = "";
            row963.ProductForm = "";
            row963.AlloyUNS = "";
            row963.ClassCondition = "";
            row963.Notes = "";
            row963.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row963);

            // Row 34: SA-403, 304H, Smls. & wld. fittings
            var row964 = new OldStressRowData();
            row964.LineNo = 34;
            row964.MaterialId = 9213;
            row964.SpecNo = "SA-403";
            row964.TypeGrade = "304H";
            row964.ProductForm = "Smls. & wld. fittings";
            row964.AlloyUNS = "S30409";
            row964.ClassCondition = "";
            row964.Notes = "G5, W12, W15";
            row964.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row964);

            // Row 35: ..., , 
            var row965 = new OldStressRowData();
            row965.LineNo = 35;
            row965.MaterialId = 9209;
            row965.SpecNo = "...";
            row965.TypeGrade = "";
            row965.ProductForm = "";
            row965.AlloyUNS = "";
            row965.ClassCondition = "";
            row965.Notes = "";
            row965.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row965);

            // Row 36: ..., , 
            var row966 = new OldStressRowData();
            row966.LineNo = 36;
            row966.MaterialId = 9210;
            row966.SpecNo = "...";
            row966.TypeGrade = "";
            row966.ProductForm = "";
            row966.AlloyUNS = "";
            row966.ClassCondition = "";
            row966.Notes = "";
            row966.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row966);

            // Row 37: ..., , 
            var row967 = new OldStressRowData();
            row967.LineNo = 37;
            row967.MaterialId = 9212;
            row967.SpecNo = "...";
            row967.TypeGrade = "";
            row967.ProductForm = "";
            row967.AlloyUNS = "";
            row967.ClassCondition = "";
            row967.Notes = "";
            row967.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row967);

            // Row 38: SA-409, TP304, Wld. pipe
            var row968 = new OldStressRowData();
            row968.LineNo = 38;
            row968.MaterialId = 9214;
            row968.SpecNo = "SA-409";
            row968.TypeGrade = "TP304";
            row968.ProductForm = "Wld. pipe";
            row968.AlloyUNS = "S30400";
            row968.ClassCondition = "";
            row968.Notes = "G5, W12";
            row968.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row968);

            // Row 39: SA-452, TP304H, Cast pipe
            var row969 = new OldStressRowData();
            row969.LineNo = 39;
            row969.MaterialId = 9215;
            row969.SpecNo = "SA-452";
            row969.TypeGrade = "TP304H";
            row969.ProductForm = "Cast pipe";
            row969.AlloyUNS = "S30409";
            row969.ClassCondition = "";
            row969.Notes = "G5, G16, G17, G32";
            row969.StressValues = new double?[] { 18.7, null, 17, null, 16, 15.4, 15.1, 14.9, 14.8, 14.8, 14.7, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row969);

            // Row 40: SA-452, TP304H, Cast pipe
            var row970 = new OldStressRowData();
            row970.LineNo = 40;
            row970.MaterialId = 9216;
            row970.SpecNo = "SA-452";
            row970.TypeGrade = "TP304H";
            row970.ProductForm = "Cast pipe";
            row970.AlloyUNS = "S30409";
            row970.ClassCondition = "";
            row970.Notes = "G5, G32";
            row970.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row970);

            // Row 41: SA-452, TP304H, Cast pipe
            var row971 = new OldStressRowData();
            row971.LineNo = 41;
            row971.MaterialId = 9217;
            row971.SpecNo = "SA-452";
            row971.TypeGrade = "TP304H";
            row971.ProductForm = "Cast pipe";
            row971.AlloyUNS = "S30409";
            row971.ClassCondition = "";
            row971.Notes = "G32";
            row971.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row971);

            // Row 1: SA-479, 302, Bar
            var row972 = new OldStressRowData();
            row972.LineNo = 1;
            row972.MaterialId = 9218;
            row972.SpecNo = "SA-479";
            row972.TypeGrade = "302";
            row972.ProductForm = "Bar";
            row972.AlloyUNS = "S30200";
            row972.ClassCondition = "";
            row972.Notes = "G5, G24";
            row972.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row972);

            // Row 2: SA-479, 302, Bar
            var row973 = new OldStressRowData();
            row973.LineNo = 2;
            row973.MaterialId = 9219;
            row973.SpecNo = "SA-479";
            row973.TypeGrade = "302";
            row973.ProductForm = "Bar";
            row973.AlloyUNS = "S30200";
            row973.ClassCondition = "";
            row973.Notes = "G22";
            row973.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row973);

            // Row 3: SA-479, 304, Bar
            var row974 = new OldStressRowData();
            row974.LineNo = 3;
            row974.MaterialId = 9220;
            row974.SpecNo = "SA-479";
            row974.TypeGrade = "304";
            row974.ProductForm = "Bar";
            row974.AlloyUNS = "S30400";
            row974.ClassCondition = "";
            row974.Notes = "G5, G12, G18, G22";
            row974.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row974);

            // Row 4: SA-479, 304, Bar
            var row975 = new OldStressRowData();
            row975.LineNo = 4;
            row975.MaterialId = 9221;
            row975.SpecNo = "SA-479";
            row975.TypeGrade = "304";
            row975.ProductForm = "Bar";
            row975.AlloyUNS = "S30400";
            row975.ClassCondition = "";
            row975.Notes = "G12, G18, G22";
            row975.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row975);

            // Row 5: SA-479, 304H, Bar
            var row976 = new OldStressRowData();
            row976.LineNo = 5;
            row976.MaterialId = 9222;
            row976.SpecNo = "SA-479";
            row976.TypeGrade = "304H";
            row976.ProductForm = "Bar";
            row976.AlloyUNS = "S30409";
            row976.ClassCondition = "";
            row976.Notes = "G5, G18, G22";
            row976.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, 14.9, 14.7, 14.4, 14.1, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row976);

            // Row 6: SA-479, 304H, Bar
            var row977 = new OldStressRowData();
            row977.LineNo = 6;
            row977.MaterialId = 9223;
            row977.SpecNo = "SA-479";
            row977.TypeGrade = "304H";
            row977.ProductForm = "Bar";
            row977.AlloyUNS = "S30409";
            row977.ClassCondition = "";
            row977.Notes = "G18, G22";
            row977.StressValues = new double?[] { 18.8, null, 16.7, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row977);

            // Row 7: SA-688, TP304, Wld. tube
            var row978 = new OldStressRowData();
            row978.LineNo = 7;
            row978.MaterialId = 9224;
            row978.SpecNo = "SA-688";
            row978.TypeGrade = "TP304";
            row978.ProductForm = "Wld. tube";
            row978.AlloyUNS = "S30400";
            row978.ClassCondition = "";
            row978.Notes = "G5, W12";
            row978.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row978);

            // Row 8: SA-688, TP304, Wld. tube
            var row979 = new OldStressRowData();
            row979.LineNo = 8;
            row979.MaterialId = 9225;
            row979.SpecNo = "SA-688";
            row979.TypeGrade = "TP304";
            row979.ProductForm = "Wld. tube";
            row979.AlloyUNS = "S30400";
            row979.ClassCondition = "";
            row979.Notes = "G5, G12, G24";
            row979.StressValues = new double?[] { 16, null, 15.1, null, 14.1, 13.8, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.7, 12.5, 12.2, 11.7, 10.4, 8.3, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row979);

            // Row 9: SA-688, TP304, Wld. tube
            var row980 = new OldStressRowData();
            row980.LineNo = 9;
            row980.MaterialId = 9226;
            row980.SpecNo = "SA-688";
            row980.TypeGrade = "TP304";
            row980.ProductForm = "Wld. tube";
            row980.AlloyUNS = "S30400";
            row980.ClassCondition = "";
            row980.Notes = "G12, G24";
            row980.StressValues = new double?[] { 16, null, 13.3, null, 12, 11, 10.4, 9.7, 9.6, 9.4, 9.2, 9, 8.8, 8.7, 8.5, 8.3, 8.1, 7.5, 6.6, 5.2, 4, 3.2, 2.5, 2, 1.6, 1.2, null, null, null };
            batch.Add(row980);

            // Row 10: SA-813, TP304, Wld. pipe
            var row981 = new OldStressRowData();
            row981.LineNo = 10;
            row981.MaterialId = 9227;
            row981.SpecNo = "SA-813";
            row981.TypeGrade = "TP304";
            row981.ProductForm = "Wld. pipe";
            row981.AlloyUNS = "S30400";
            row981.ClassCondition = "";
            row981.Notes = "G5, W12";
            row981.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row981);

            // Row 11: SA-813, TP304H, Wld. pipe
            var row982 = new OldStressRowData();
            row982.LineNo = 11;
            row982.MaterialId = 9228;
            row982.SpecNo = "SA-813";
            row982.TypeGrade = "TP304H";
            row982.ProductForm = "Wld. pipe";
            row982.AlloyUNS = "S30409";
            row982.ClassCondition = "";
            row982.Notes = "G5, W12";
            row982.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row982);

            // Row 12: SA-814, TP304, Wld. pipe
            var row983 = new OldStressRowData();
            row983.LineNo = 12;
            row983.MaterialId = 9229;
            row983.SpecNo = "SA-814";
            row983.TypeGrade = "TP304";
            row983.ProductForm = "Wld. pipe";
            row983.AlloyUNS = "S30400";
            row983.ClassCondition = "";
            row983.Notes = "G5, W12";
            row983.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row983);

            // Row 13: SA-814, TP304H, Wld. pipe
            var row984 = new OldStressRowData();
            row984.LineNo = 13;
            row984.MaterialId = 9230;
            row984.SpecNo = "SA-814";
            row984.TypeGrade = "TP304H";
            row984.ProductForm = "Wld. pipe";
            row984.AlloyUNS = "S30409";
            row984.ClassCondition = "";
            row984.Notes = "G5, W12";
            row984.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row984);

            // Row 14: SA-351, CF3A, Castings
            var row985 = new OldStressRowData();
            row985.LineNo = 14;
            row985.MaterialId = 9231;
            row985.SpecNo = "SA-351";
            row985.TypeGrade = "CF3A";
            row985.ProductForm = "Castings";
            row985.AlloyUNS = "J92500";
            row985.ClassCondition = "";
            row985.Notes = "G1, G5, G16, G17, G32";
            row985.StressValues = new double?[] { 19.3, null, 18.2, null, 16.9, 16.5, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row985);

            // Row 15: SA-351, CF3A, Castings
            var row986 = new OldStressRowData();
            row986.LineNo = 15;
            row986.MaterialId = 9236;
            row986.SpecNo = "SA-351";
            row986.TypeGrade = "CF3A";
            row986.ProductForm = "Castings";
            row986.AlloyUNS = "J92500";
            row986.ClassCondition = "";
            row986.Notes = "G1, G32";
            row986.StressValues = new double?[] { 19.3, null, 18.2, null, 16.7, 16.1, 15.1, 14.2, 14, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row986);

            // Row 16: SA-351, CF8A, Castings
            var row987 = new OldStressRowData();
            row987.LineNo = 16;
            row987.MaterialId = 9232;
            row987.SpecNo = "SA-351";
            row987.TypeGrade = "CF8A";
            row987.ProductForm = "Castings";
            row987.AlloyUNS = "J92600";
            row987.ClassCondition = "";
            row987.Notes = "G1, G5, G16, G17, G32";
            row987.StressValues = new double?[] { 19.3, null, 18.2, null, 16.9, 16.5, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row987);

            // Row 17: SA-351, CF8A, Castings
            var row988 = new OldStressRowData();
            row988.LineNo = 17;
            row988.MaterialId = 9233;
            row988.SpecNo = "SA-351";
            row988.TypeGrade = "CF8A";
            row988.ProductForm = "Castings";
            row988.AlloyUNS = "J92600";
            row988.ClassCondition = "";
            row988.Notes = "G1, G32";
            row988.StressValues = new double?[] { 19.3, null, 18.2, null, 16.9, 16.1, 15.1, 14.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row988);

            // Row 18: SA-451, CPF3A, Cast pipe
            var row989 = new OldStressRowData();
            row989.LineNo = 18;
            row989.MaterialId = 9234;
            row989.SpecNo = "SA-451";
            row989.TypeGrade = "CPF3A";
            row989.ProductForm = "Cast pipe";
            row989.AlloyUNS = "J92500";
            row989.ClassCondition = "";
            row989.Notes = "G5, G16, G17, G32";
            row989.StressValues = new double?[] { 19.3, null, 18.2, null, 16.9, 16.5, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row989);

            // Row 19: SA-451, CPF8A, Cast pipe
            var row990 = new OldStressRowData();
            row990.LineNo = 19;
            row990.MaterialId = 9235;
            row990.SpecNo = "SA-451";
            row990.TypeGrade = "CPF8A";
            row990.ProductForm = "Cast pipe";
            row990.AlloyUNS = "J92600";
            row990.ClassCondition = "";
            row990.Notes = "G5, G16, G17, G32";
            row990.StressValues = new double?[] { 19.3, null, 18.2, null, 16.9, 16.5, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row990);

            // Row 20: SA-182, F304LN, Forgings
            var row991 = new OldStressRowData();
            row991.LineNo = 20;
            row991.MaterialId = 9237;
            row991.SpecNo = "SA-182";
            row991.TypeGrade = "F304LN";
            row991.ProductForm = "Forgings";
            row991.AlloyUNS = "S30453";
            row991.ClassCondition = "";
            row991.Notes = "G5";
            row991.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row991);

            // Row 21: SA-336, F304LN, Forgings
            var row992 = new OldStressRowData();
            row992.LineNo = 21;
            row992.MaterialId = 9238;
            row992.SpecNo = "SA-336";
            row992.TypeGrade = "F304LN";
            row992.ProductForm = "Forgings";
            row992.AlloyUNS = "S30453";
            row992.ClassCondition = "";
            row992.Notes = "G5";
            row992.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row992);

            // Row 22: SA-182, F304LN, Forgings
            var row993 = new OldStressRowData();
            row993.LineNo = 22;
            row993.MaterialId = 9239;
            row993.SpecNo = "SA-182";
            row993.TypeGrade = "F304LN";
            row993.ProductForm = "Forgings";
            row993.AlloyUNS = "S30453";
            row993.ClassCondition = "";
            row993.Notes = "G5";
            row993.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row993);

            // Row 23: SA-213, TP304LN, Smls. tube
            var row994 = new OldStressRowData();
            row994.LineNo = 23;
            row994.MaterialId = 9240;
            row994.SpecNo = "SA-213";
            row994.TypeGrade = "TP304LN";
            row994.ProductForm = "Smls. tube";
            row994.AlloyUNS = "S30453";
            row994.ClassCondition = "";
            row994.Notes = "G5";
            row994.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row994);

            // Row 24: SA-240, 304LN, Plate
            var row995 = new OldStressRowData();
            row995.LineNo = 24;
            row995.MaterialId = 9241;
            row995.SpecNo = "SA-240";
            row995.TypeGrade = "304LN";
            row995.ProductForm = "Plate";
            row995.AlloyUNS = "S30453";
            row995.ClassCondition = "";
            row995.Notes = "G5";
            row995.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row995);

            // Row 25: SA-249, TP304LN, Wld. tube
            var row996 = new OldStressRowData();
            row996.LineNo = 25;
            row996.MaterialId = 9242;
            row996.SpecNo = "SA-249";
            row996.TypeGrade = "TP304LN";
            row996.ProductForm = "Wld. tube";
            row996.AlloyUNS = "S30453";
            row996.ClassCondition = "";
            row996.Notes = "G5, W12";
            row996.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row996);

            // Row 26: SA-312, TP304LN, Wld. & smls. pipe
            var row997 = new OldStressRowData();
            row997.LineNo = 26;
            row997.MaterialId = 9243;
            row997.SpecNo = "SA-312";
            row997.TypeGrade = "TP304LN";
            row997.ProductForm = "Wld. & smls. pipe";
            row997.AlloyUNS = "S30453";
            row997.ClassCondition = "";
            row997.Notes = "G5, W12";
            row997.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row997);

            // Row 27: SA-376, TP304LN, Smls. pipe
            var row998 = new OldStressRowData();
            row998.LineNo = 27;
            row998.MaterialId = 9244;
            row998.SpecNo = "SA-376";
            row998.TypeGrade = "TP304LN";
            row998.ProductForm = "Smls. pipe";
            row998.AlloyUNS = "S30453";
            row998.ClassCondition = "";
            row998.Notes = "G5";
            row998.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row998);

            // Row 28: SA-403, 304LN, Smls. & wld. fittings
            var row999 = new OldStressRowData();
            row999.LineNo = 28;
            row999.MaterialId = 9245;
            row999.SpecNo = "SA-403";
            row999.TypeGrade = "304LN";
            row999.ProductForm = "Smls. & wld. fittings";
            row999.AlloyUNS = "S30453";
            row999.ClassCondition = "WP";
            row999.Notes = "G5, W12";
            row999.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row999);

            // Row 29: ..., , 
            var row1000 = new OldStressRowData();
            row1000.LineNo = 29;
            row1000.MaterialId = 9246;
            row1000.SpecNo = "...";
            row1000.TypeGrade = "";
            row1000.ProductForm = "";
            row1000.AlloyUNS = "";
            row1000.ClassCondition = "";
            row1000.Notes = "";
            row1000.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1000);

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
