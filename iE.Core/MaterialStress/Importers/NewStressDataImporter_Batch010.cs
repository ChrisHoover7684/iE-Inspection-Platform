using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch010
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

            // Row 28: SA-688, TP304N, Wld. tube
            var row901 = new OldStressRowData();
            row901.LineNo = 28;
            row901.MaterialId = 9284;
            row901.SpecNo = "SA-688";
            row901.TypeGrade = "TP304N";
            row901.ProductForm = "Wld. tube";
            row901.AlloyUNS = "S30451";
            row901.ClassCondition = "";
            row901.Notes = "G5, G12, G24, T7";
            row901.StressValues = new double?[] { 19.4, null, 19.4, null, 18.5, 17.3, 16, 15.2, 14.9, 14.6, 14.4, 14.1, 13.8, 13.6, 13.3, 13, 10.5, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row901);

            // Row 29: SA-688, TP304N, Wld. tube
            var row902 = new OldStressRowData();
            row902.LineNo = 29;
            row902.MaterialId = 9285;
            row902.SpecNo = "SA-688";
            row902.TypeGrade = "TP304N";
            row902.ProductForm = "Wld. tube";
            row902.AlloyUNS = "S30451";
            row902.ClassCondition = "";
            row902.Notes = "G12, G24, T8";
            row902.StressValues = new double?[] { 19.4, null, 16.2, null, 14.2, 12.8, 11.9, 11.3, 11, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.6, 9.4, 8.3, 6.6, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row902);

            // Row 30: SA-813, TP304N, Wld. pipe
            var row903 = new OldStressRowData();
            row903.LineNo = 30;
            row903.MaterialId = 9286;
            row903.SpecNo = "SA-813";
            row903.TypeGrade = "TP304N";
            row903.ProductForm = "Wld. pipe";
            row903.AlloyUNS = "S30451";
            row903.ClassCondition = "";
            row903.Notes = "G5, W12";
            row903.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row903);

            // Row 31: SA-814, TP304N, Wld. pipe
            var row904 = new OldStressRowData();
            row904.LineNo = 31;
            row904.MaterialId = 9287;
            row904.SpecNo = "SA-814";
            row904.TypeGrade = "TP304N";
            row904.ProductForm = "Wld. pipe";
            row904.AlloyUNS = "S30451";
            row904.ClassCondition = "";
            row904.Notes = "G5, W12";
            row904.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.3, 18.9, 17.9, 17.5, 17.2, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row904);

            // Row 32: SA-479, , Bar
            var row905 = new OldStressRowData();
            row905.LineNo = 32;
            row905.MaterialId = 9288;
            row905.SpecNo = "SA-479";
            row905.TypeGrade = "";
            row905.ProductForm = "Bar";
            row905.AlloyUNS = "S21800";
            row905.ClassCondition = "";
            row905.Notes = "";
            row905.StressValues = new double?[] { 27.1, null, 25.9, null, 22.1, 19.8, 18.4, 17.6, 17.3, 17.1, 16.9, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row905);

            // Row 33: SA-336, F348H, Forgings
            var row906 = new OldStressRowData();
            row906.LineNo = 33;
            row906.MaterialId = 9289;
            row906.SpecNo = "SA-336";
            row906.TypeGrade = "F348H";
            row906.ProductForm = "Forgings";
            row906.AlloyUNS = "S34809";
            row906.ClassCondition = "";
            row906.Notes = "G5, H2, T9";
            row906.StressValues = new double?[] { 16.7, null, 16.7, null, 16.3, 15.4, 14.9, 14.6, 14.6, 14.6, 14.6, 14.6, 14.5, 14.5, 14.4, 14.2, 14, 13.7, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row906);

            // Row 34: SA-336, F348H, Forgings
            var row907 = new OldStressRowData();
            row907.LineNo = 34;
            row907.MaterialId = 9290;
            row907.SpecNo = "SA-336";
            row907.TypeGrade = "F348H";
            row907.ProductForm = "Forgings";
            row907.AlloyUNS = "S34809";
            row907.ClassCondition = "";
            row907.Notes = "H2, T9";
            row907.StressValues = new double?[] { 16.7, null, 15.3, null, 14.3, 13.3, 12.5, 11.9, 11.7, 11.5, 11.4, 11.3, 11.2, 11.2, 11.2, 11.2, 11.1, 11.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row907);

            // Row 35: SA-351, CF8C, Castings
            var row908 = new OldStressRowData();
            row908.LineNo = 35;
            row908.MaterialId = 9310;
            row908.SpecNo = "SA-351";
            row908.TypeGrade = "CF8C";
            row908.ProductForm = "Castings";
            row908.AlloyUNS = "J92710";
            row908.ClassCondition = "";
            row908.Notes = "G5, G16, G17, G32";
            row908.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row908);

            // Row 36: SA-351, CF8C, Castings
            var row909 = new OldStressRowData();
            row909.LineNo = 36;
            row909.MaterialId = 9311;
            row909.SpecNo = "SA-351";
            row909.TypeGrade = "CF8C";
            row909.ProductForm = "Castings";
            row909.AlloyUNS = "J92710";
            row909.ClassCondition = "";
            row909.Notes = "G1, G5, G12, G32, T7";
            row909.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row909);

            // Row 37: SA-351, CF8C, Castings
            var row910 = new OldStressRowData();
            row910.LineNo = 37;
            row910.MaterialId = 9312;
            row910.SpecNo = "SA-351";
            row910.TypeGrade = "CF8C";
            row910.ProductForm = "Castings";
            row910.AlloyUNS = "J92710";
            row910.ClassCondition = "";
            row910.Notes = "G1, G12, G32, T7";
            row910.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row910);

            // Row 38: SA-451, CPF8C, Cast pipe
            var row911 = new OldStressRowData();
            row911.LineNo = 38;
            row911.MaterialId = 9317;
            row911.SpecNo = "SA-451";
            row911.TypeGrade = "CPF8C";
            row911.ProductForm = "Cast pipe";
            row911.AlloyUNS = "J92710";
            row911.ClassCondition = "";
            row911.Notes = "G5, G16, G17, G32";
            row911.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row911);

            // Row 1: SA-182, F347, Forgings
            var row912 = new OldStressRowData();
            row912.LineNo = 1;
            row912.MaterialId = 9292;
            row912.SpecNo = "SA-182";
            row912.TypeGrade = "F347";
            row912.ProductForm = "Forgings";
            row912.AlloyUNS = "S34700";
            row912.ClassCondition = "";
            row912.Notes = "G5, G12, G18, H2, T7";
            row912.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row912);

            // Row 2: SA-336, F347, Forgings
            var row913 = new OldStressRowData();
            row913.LineNo = 2;
            row913.MaterialId = 9304;
            row913.SpecNo = "SA-336";
            row913.TypeGrade = "F347";
            row913.ProductForm = "Forgings";
            row913.AlloyUNS = "S34700";
            row913.ClassCondition = "";
            row913.Notes = "G5, G12, G18, H2, T7";
            row913.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row913);

            // Row 3: SA-336, F347, Forgings
            var row914 = new OldStressRowData();
            row914.LineNo = 3;
            row914.MaterialId = 9305;
            row914.SpecNo = "SA-336";
            row914.TypeGrade = "F347";
            row914.ProductForm = "Forgings";
            row914.AlloyUNS = "S34700";
            row914.ClassCondition = "";
            row914.Notes = "G12, G18, H2, T7";
            row914.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row914);

            // Row 4: SA-430, FP347, Forged pipe
            var row915 = new OldStressRowData();
            row915.LineNo = 4;
            row915.MaterialId = 9314;
            row915.SpecNo = "SA-430";
            row915.TypeGrade = "FP347";
            row915.ProductForm = "Forged pipe";
            row915.AlloyUNS = "S34700";
            row915.ClassCondition = "";
            row915.Notes = "G5, G12, G18, H1, H2, T7";
            row915.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row915);

            // Row 5: SA-182, F347H, Forgings
            var row916 = new OldStressRowData();
            row916.LineNo = 5;
            row916.MaterialId = 9294;
            row916.SpecNo = "SA-182";
            row916.TypeGrade = "F347H";
            row916.ProductForm = "Forgings";
            row916.AlloyUNS = "S34709";
            row916.ClassCondition = "";
            row916.Notes = "G18, T9";
            row916.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row916);

            // Row 6: SA-182, F347H, Forgings
            var row917 = new OldStressRowData();
            row917.LineNo = 6;
            row917.MaterialId = 9295;
            row917.SpecNo = "SA-182";
            row917.TypeGrade = "F347H";
            row917.ProductForm = "Forgings";
            row917.AlloyUNS = "S34709";
            row917.ClassCondition = "";
            row917.Notes = "G5, G18, H2, T8";
            row917.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 15.1, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row917);

            // Row 7: SA-336, F347H, Forgings
            var row918 = new OldStressRowData();
            row918.LineNo = 7;
            row918.MaterialId = 9306;
            row918.SpecNo = "SA-336";
            row918.TypeGrade = "F347H";
            row918.ProductForm = "Forgings";
            row918.AlloyUNS = "S34709";
            row918.ClassCondition = "";
            row918.Notes = "G5, H2, T8";
            row918.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 15.1, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row918);

            // Row 8: SA-336, F347H, Forgings
            var row919 = new OldStressRowData();
            row919.LineNo = 8;
            row919.MaterialId = 9307;
            row919.SpecNo = "SA-336";
            row919.TypeGrade = "F347H";
            row919.ProductForm = "Forgings";
            row919.AlloyUNS = "S34709";
            row919.ClassCondition = "";
            row919.Notes = "G5, H2, T8";
            row919.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 15.1, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row919);

            // Row 9: SA-430, FP347H, Forged pipe
            var row920 = new OldStressRowData();
            row920.LineNo = 9;
            row920.MaterialId = 9316;
            row920.SpecNo = "SA-430";
            row920.TypeGrade = "FP347H";
            row920.ProductForm = "Forged pipe";
            row920.AlloyUNS = "S34709";
            row920.ClassCondition = "";
            row920.Notes = "G5, G18, H1, H2, T8";
            row920.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 15.1, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row920);

            // Row 10: SA-182, F348, Forgings
            var row921 = new OldStressRowData();
            row921.LineNo = 10;
            row921.MaterialId = 9299;
            row921.SpecNo = "SA-182";
            row921.TypeGrade = "F348";
            row921.ProductForm = "Forgings";
            row921.AlloyUNS = "S34800";
            row921.ClassCondition = "";
            row921.Notes = "G18, T7";
            row921.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row921);

            // Row 11: SA-182, F348, Forgings
            var row922 = new OldStressRowData();
            row922.LineNo = 11;
            row922.MaterialId = 9298;
            row922.SpecNo = "SA-182";
            row922.TypeGrade = "F348";
            row922.ProductForm = "Forgings";
            row922.AlloyUNS = "S34800";
            row922.ClassCondition = "";
            row922.Notes = "G5, G12, G18, H2, T7";
            row922.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row922);

            // Row 12: SA-336, F348, Forgings
            var row923 = new OldStressRowData();
            row923.LineNo = 12;
            row923.MaterialId = 9308;
            row923.SpecNo = "SA-336";
            row923.TypeGrade = "F348";
            row923.ProductForm = "Forgings";
            row923.AlloyUNS = "S34800";
            row923.ClassCondition = "";
            row923.Notes = "G5, G12, H2, T7";
            row923.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.8, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row923);

            // Row 13: SA-336, F348, Forgings
            var row924 = new OldStressRowData();
            row924.LineNo = 13;
            row924.MaterialId = 9309;
            row924.SpecNo = "SA-336";
            row924.TypeGrade = "F348";
            row924.ProductForm = "Forgings";
            row924.AlloyUNS = "S34800";
            row924.ClassCondition = "";
            row924.Notes = "G12, H2, T7";
            row924.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row924);

            // Row 14: SA-182, F348H, Forgings
            var row925 = new OldStressRowData();
            row925.LineNo = 14;
            row925.MaterialId = 9301;
            row925.SpecNo = "SA-182";
            row925.TypeGrade = "F348H";
            row925.ProductForm = "Forgings";
            row925.AlloyUNS = "S34809";
            row925.ClassCondition = "";
            row925.Notes = "G18, T9";
            row925.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row925);

            // Row 15: SA-182, F348H, Forgings
            var row926 = new OldStressRowData();
            row926.LineNo = 15;
            row926.MaterialId = 9302;
            row926.SpecNo = "SA-182";
            row926.TypeGrade = "F348H";
            row926.ProductForm = "Forgings";
            row926.AlloyUNS = "S34809";
            row926.ClassCondition = "";
            row926.Notes = "G5, G18, H2, T8";
            row926.StressValues = new double?[] { 20, null, 19.1, null, 17.6, 16.6, 16, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.6, 15.5, 15.3, 15.1, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row926);

            // Row 16: SA-182, F347, Forgings
            var row927 = new OldStressRowData();
            row927.LineNo = 16;
            row927.MaterialId = 9318;
            row927.SpecNo = "SA-182";
            row927.TypeGrade = "F347";
            row927.ProductForm = "Forgings";
            row927.AlloyUNS = "S34700";
            row927.ClassCondition = "";
            row927.Notes = "G5, G12, G18, H2, T6";
            row927.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row927);

            // Row 17: SA-182, F347, Forgings
            var row928 = new OldStressRowData();
            row928.LineNo = 17;
            row928.MaterialId = 9319;
            row928.SpecNo = "SA-182";
            row928.TypeGrade = "F347";
            row928.ProductForm = "Forgings";
            row928.AlloyUNS = "S34700";
            row928.ClassCondition = "";
            row928.Notes = "G12, G18, H2, T7";
            row928.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row928);

            // Row 18: SA-213, TP347, Smls. tube
            var row929 = new OldStressRowData();
            row929.LineNo = 18;
            row929.MaterialId = 9326;
            row929.SpecNo = "SA-213";
            row929.TypeGrade = "TP347";
            row929.ProductForm = "Smls. tube";
            row929.AlloyUNS = "S34700";
            row929.ClassCondition = "";
            row929.Notes = "G5, G12, G18, H2, T6";
            row929.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row929);

            // Row 19: SA-213, TP347, Smls. tube
            var row930 = new OldStressRowData();
            row930.LineNo = 19;
            row930.MaterialId = 9327;
            row930.SpecNo = "SA-213";
            row930.TypeGrade = "TP347";
            row930.ProductForm = "Smls. tube";
            row930.AlloyUNS = "S34700";
            row930.ClassCondition = "";
            row930.Notes = "G12, G18, H2, T7";
            row930.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row930);

            // Row 20: SA-240, 347, Plate
            var row931 = new OldStressRowData();
            row931.LineNo = 20;
            row931.MaterialId = 9335;
            row931.SpecNo = "SA-240";
            row931.TypeGrade = "347";
            row931.ProductForm = "Plate";
            row931.AlloyUNS = "S34700";
            row931.ClassCondition = "";
            row931.Notes = "G5, G12, G18, T6";
            row931.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row931);

            // Row 21: SA-240, 347, Plate
            var row932 = new OldStressRowData();
            row932.LineNo = 21;
            row932.MaterialId = 9334;
            row932.SpecNo = "SA-240";
            row932.TypeGrade = "347";
            row932.ProductForm = "Plate";
            row932.AlloyUNS = "S34700";
            row932.ClassCondition = "";
            row932.Notes = "G12, G18, T7";
            row932.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row932);

            // Row 22: SA-249, TP347, Wld. tube
            var row933 = new OldStressRowData();
            row933.LineNo = 22;
            row933.MaterialId = 9343;
            row933.SpecNo = "SA-249";
            row933.TypeGrade = "TP347";
            row933.ProductForm = "Wld. tube";
            row933.AlloyUNS = "S34700";
            row933.ClassCondition = "";
            row933.Notes = "G12, G18, T7, W13";
            row933.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row933);

            // Row 23: SA-249, TP347, Wld. tube
            var row934 = new OldStressRowData();
            row934.LineNo = 23;
            row934.MaterialId = 9340;
            row934.SpecNo = "SA-249";
            row934.TypeGrade = "TP347";
            row934.ProductForm = "Wld. tube";
            row934.AlloyUNS = "S34700";
            row934.ClassCondition = "";
            row934.Notes = "G5, G12, G18, T6, W12, W13";
            row934.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row934);

            // Row 24: SA-249, TP347, Wld. tube
            var row935 = new OldStressRowData();
            row935.LineNo = 24;
            row935.MaterialId = 9341;
            row935.SpecNo = "SA-249";
            row935.TypeGrade = "TP347";
            row935.ProductForm = "Wld. tube";
            row935.AlloyUNS = "S34700";
            row935.ClassCondition = "";
            row935.Notes = "G3, G5, G12, G18, G24, T6";
            row935.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 13.6, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row935);

            // Row 25: SA-249, TP347, Wld. tube
            var row936 = new OldStressRowData();
            row936.LineNo = 25;
            row936.MaterialId = 9342;
            row936.SpecNo = "SA-249";
            row936.TypeGrade = "TP347";
            row936.ProductForm = "Wld. tube";
            row936.AlloyUNS = "S34700";
            row936.ClassCondition = "";
            row936.Notes = "G3, G12, G18, G24, T7";
            row936.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row936);

            // Row 26: SA-312, TP347, Smls. & wld. pipe
            var row937 = new OldStressRowData();
            row937.LineNo = 26;
            row937.MaterialId = 9363;
            row937.SpecNo = "SA-312";
            row937.TypeGrade = "TP347";
            row937.ProductForm = "Smls. & wld. pipe";
            row937.AlloyUNS = "S34700";
            row937.ClassCondition = "";
            row937.Notes = "G5, G12, G18, H2, T6, W12, W13";
            row937.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row937);

            // Row 27: SA-312, TP347, Smls. & wld. pipe
            var row938 = new OldStressRowData();
            row938.LineNo = 27;
            row938.MaterialId = 9364;
            row938.SpecNo = "SA-312";
            row938.TypeGrade = "TP347";
            row938.ProductForm = "Smls. & wld. pipe";
            row938.AlloyUNS = "S34700";
            row938.ClassCondition = "";
            row938.Notes = "G12, G18, H2, T7, W13";
            row938.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row938);

            // Row 28: SA-312, TP347, Wld. pipe
            var row939 = new OldStressRowData();
            row939.LineNo = 28;
            row939.MaterialId = 9360;
            row939.SpecNo = "SA-312";
            row939.TypeGrade = "TP347";
            row939.ProductForm = "Wld. pipe";
            row939.AlloyUNS = "S34700";
            row939.ClassCondition = "";
            row939.Notes = "G3, G5, G12, G18, G24, H2, T6";
            row939.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 13.6, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row939);

            // Row 29: SA-312, TP347, Wld. pipe
            var row940 = new OldStressRowData();
            row940.LineNo = 29;
            row940.MaterialId = 9361;
            row940.SpecNo = "SA-312";
            row940.TypeGrade = "TP347";
            row940.ProductForm = "Wld. pipe";
            row940.AlloyUNS = "S34700";
            row940.ClassCondition = "";
            row940.Notes = "G3, G12, G18, G24, H2, T7";
            row940.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row940);

            // Row 30: SA-358, 347, Wld. pipe
            var row941 = new OldStressRowData();
            row941.LineNo = 30;
            row941.MaterialId = 9381;
            row941.SpecNo = "SA-358";
            row941.TypeGrade = "347";
            row941.ProductForm = "Wld. pipe";
            row941.AlloyUNS = "S34700";
            row941.ClassCondition = "1";
            row941.Notes = "G5, W12";
            row941.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row941);

            // Row 31: SA-376, TP347, Smls. pipe
            var row942 = new OldStressRowData();
            row942.LineNo = 31;
            row942.MaterialId = 9383;
            row942.SpecNo = "SA-376";
            row942.TypeGrade = "TP347";
            row942.ProductForm = "Smls. pipe";
            row942.AlloyUNS = "S34700";
            row942.ClassCondition = "";
            row942.Notes = "G5, G12, G18, H1, H2, T6";
            row942.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row942);

            // Row 32: SA-376, TP347, Smls. pipe
            var row943 = new OldStressRowData();
            row943.LineNo = 32;
            row943.MaterialId = 9384;
            row943.SpecNo = "SA-376";
            row943.TypeGrade = "TP347";
            row943.ProductForm = "Smls. pipe";
            row943.AlloyUNS = "S34700";
            row943.ClassCondition = "";
            row943.Notes = "G12, G18, H1, H2, T7";
            row943.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row943);

            // Row 33: SA-403, 347, Smls. & wld. fittings
            var row944 = new OldStressRowData();
            row944.LineNo = 33;
            row944.MaterialId = 9389;
            row944.SpecNo = "SA-403";
            row944.TypeGrade = "347";
            row944.ProductForm = "Smls. & wld. fittings";
            row944.AlloyUNS = "S34700";
            row944.ClassCondition = "";
            row944.Notes = "G5, G12, T6, W12, W14";
            row944.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row944);

            // Row 34: SA-409, TP347, Wld. pipe
            var row945 = new OldStressRowData();
            row945.LineNo = 34;
            row945.MaterialId = 9413;
            row945.SpecNo = "SA-409";
            row945.TypeGrade = "TP347";
            row945.ProductForm = "Wld. pipe";
            row945.AlloyUNS = "S34700";
            row945.ClassCondition = "";
            row945.Notes = "G5, W12";
            row945.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row945);

            // Row 35: SA-479, 347, Bar
            var row946 = new OldStressRowData();
            row946.LineNo = 35;
            row946.MaterialId = 9417;
            row946.SpecNo = "SA-479";
            row946.TypeGrade = "347";
            row946.ProductForm = "Bar";
            row946.AlloyUNS = "S34700";
            row946.ClassCondition = "";
            row946.Notes = "G5, G12, G18, G22, H2, T6";
            row946.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row946);

            // Row 36: SA-479, 347, Bar
            var row947 = new OldStressRowData();
            row947.LineNo = 36;
            row947.MaterialId = 9418;
            row947.SpecNo = "SA-479";
            row947.TypeGrade = "347";
            row947.ProductForm = "Bar";
            row947.AlloyUNS = "S34700";
            row947.ClassCondition = "";
            row947.Notes = "G12, G18, G22, H2, T7";
            row947.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row947);

            // Row 37: SA-813, TP347, Wld. pipe
            var row948 = new OldStressRowData();
            row948.LineNo = 37;
            row948.MaterialId = 9425;
            row948.SpecNo = "SA-813";
            row948.TypeGrade = "TP347";
            row948.ProductForm = "Wld. pipe";
            row948.AlloyUNS = "S34700";
            row948.ClassCondition = "";
            row948.Notes = "G5, W12";
            row948.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row948);

            // Row 1: SA-182, F347H, Forgings
            var row949 = new OldStressRowData();
            row949.LineNo = 1;
            row949.MaterialId = 9320;
            row949.SpecNo = "SA-182";
            row949.TypeGrade = "F347H";
            row949.ProductForm = "Forgings";
            row949.AlloyUNS = "S34709";
            row949.ClassCondition = "";
            row949.Notes = "G5, G18, H2, T8";
            row949.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row949);

            // Row 2: SA-182, F347H, Forgings
            var row950 = new OldStressRowData();
            row950.LineNo = 2;
            row950.MaterialId = 9321;
            row950.SpecNo = "SA-182";
            row950.TypeGrade = "F347H";
            row950.ProductForm = "Forgings";
            row950.AlloyUNS = "S34709";
            row950.ClassCondition = "";
            row950.Notes = "G18, H2, T9";
            row950.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row950);

            // Row 3: SA-213, TP347H, Smls. tube
            var row951 = new OldStressRowData();
            row951.LineNo = 3;
            row951.MaterialId = 9328;
            row951.SpecNo = "SA-213";
            row951.TypeGrade = "TP347H";
            row951.ProductForm = "Smls. tube";
            row951.AlloyUNS = "S34709";
            row951.ClassCondition = "";
            row951.Notes = "G5, G18, H2, T8";
            row951.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row951);

            // Row 4: SA-213, TP347H, Smls. tube
            var row952 = new OldStressRowData();
            row952.LineNo = 4;
            row952.MaterialId = 9329;
            row952.SpecNo = "SA-213";
            row952.TypeGrade = "TP347H";
            row952.ProductForm = "Smls. tube";
            row952.AlloyUNS = "S34709";
            row952.ClassCondition = "";
            row952.Notes = "G18, H2, T9";
            row952.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row952);

            // Row 5: SA-240, 347H, Plate
            var row953 = new OldStressRowData();
            row953.LineNo = 5;
            row953.MaterialId = 9336;
            row953.SpecNo = "SA-240";
            row953.TypeGrade = "347H";
            row953.ProductForm = "Plate";
            row953.AlloyUNS = "S34709";
            row953.ClassCondition = "";
            row953.Notes = "G5, G18, H2, T8";
            row953.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row953);

            // Row 6: SA-240, 347H, Plate
            var row954 = new OldStressRowData();
            row954.LineNo = 6;
            row954.MaterialId = 9783;
            row954.SpecNo = "SA-240";
            row954.TypeGrade = "347H";
            row954.ProductForm = "Plate";
            row954.AlloyUNS = "S34709";
            row954.ClassCondition = "";
            row954.Notes = "G18, H2, T9";
            row954.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row954);

            // Row 7: SA-249, TP347H, Wld. tube
            var row955 = new OldStressRowData();
            row955.LineNo = 7;
            row955.MaterialId = 9346;
            row955.SpecNo = "SA-249";
            row955.TypeGrade = "TP347H";
            row955.ProductForm = "Wld. tube";
            row955.AlloyUNS = "S34709";
            row955.ClassCondition = "";
            row955.Notes = "G5, G18, T8, W12, W13";
            row955.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row955);

            // Row 8: SA-249, TP347H, Wld. tube
            var row956 = new OldStressRowData();
            row956.LineNo = 8;
            row956.MaterialId = 9347;
            row956.SpecNo = "SA-249";
            row956.TypeGrade = "TP347H";
            row956.ProductForm = "Wld. tube";
            row956.AlloyUNS = "S34709";
            row956.ClassCondition = "";
            row956.Notes = "G18, T9, W13";
            row956.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row956);

            // Row 9: SA-249, TP347H, Wld. tube
            var row957 = new OldStressRowData();
            row957.LineNo = 9;
            row957.MaterialId = 9344;
            row957.SpecNo = "SA-249";
            row957.TypeGrade = "TP347H";
            row957.ProductForm = "Wld. tube";
            row957.AlloyUNS = "S34709";
            row957.ClassCondition = "";
            row957.Notes = "G3, G5, G18, G24, T8";
            row957.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 14, 13.7, 12, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row957);

            // Row 10: SA-249, TP347H, Wld. tube
            var row958 = new OldStressRowData();
            row958.LineNo = 10;
            row958.MaterialId = 9345;
            row958.SpecNo = "SA-249";
            row958.TypeGrade = "TP347H";
            row958.ProductForm = "Wld. tube";
            row958.AlloyUNS = "S34709";
            row958.ClassCondition = "";
            row958.Notes = "G3, G18, G24, T9";
            row958.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 11.4, 11.3, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row958);

            // Row 11: SA-312, TP347H, Smls. & wld. pipe
            var row959 = new OldStressRowData();
            row959.LineNo = 11;
            row959.MaterialId = 9365;
            row959.SpecNo = "SA-312";
            row959.TypeGrade = "TP347H";
            row959.ProductForm = "Smls. & wld. pipe";
            row959.AlloyUNS = "S34709";
            row959.ClassCondition = "";
            row959.Notes = "G5, G18, H2, T8, W12, W13";
            row959.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row959);

            // Row 12: SA-312, TP347H, Smls. & wld. pipe
            var row960 = new OldStressRowData();
            row960.LineNo = 12;
            row960.MaterialId = 9366;
            row960.SpecNo = "SA-312";
            row960.TypeGrade = "TP347H";
            row960.ProductForm = "Smls. & wld. pipe";
            row960.AlloyUNS = "S34709";
            row960.ClassCondition = "";
            row960.Notes = "G18, H2, T9, W13";
            row960.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row960);

            // Row 13: SA-312, TP347H, Wld. pipe
            var row961 = new OldStressRowData();
            row961.LineNo = 13;
            row961.MaterialId = 9367;
            row961.SpecNo = "SA-312";
            row961.TypeGrade = "TP347H";
            row961.ProductForm = "Wld. pipe";
            row961.AlloyUNS = "S34709";
            row961.ClassCondition = "";
            row961.Notes = "G3, G5, G18, G24, H2, T8";
            row961.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 14, 13.7, 12, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row961);

            // Row 14: SA-312, TP347H, Wld. pipe
            var row962 = new OldStressRowData();
            row962.LineNo = 14;
            row962.MaterialId = 9368;
            row962.SpecNo = "SA-312";
            row962.TypeGrade = "TP347H";
            row962.ProductForm = "Wld. pipe";
            row962.AlloyUNS = "S34709";
            row962.ClassCondition = "";
            row962.Notes = "G3, G18, G24, H2, T9";
            row962.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 11.4, 11.3, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row962);

            // Row 15: SA-376, TP347H, Smls. pipe
            var row963 = new OldStressRowData();
            row963.LineNo = 15;
            row963.MaterialId = 9385;
            row963.SpecNo = "SA-376";
            row963.TypeGrade = "TP347H";
            row963.ProductForm = "Smls. pipe";
            row963.AlloyUNS = "S34709";
            row963.ClassCondition = "";
            row963.Notes = "G5, G18, H1, H2, T8";
            row963.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row963);

            // Row 16: SA-376, TP347H, Smls. pipe
            var row964 = new OldStressRowData();
            row964.LineNo = 16;
            row964.MaterialId = 9386;
            row964.SpecNo = "SA-376";
            row964.TypeGrade = "TP347H";
            row964.ProductForm = "Smls. pipe";
            row964.AlloyUNS = "S34709";
            row964.ClassCondition = "";
            row964.Notes = "G18, H1, H2, T9";
            row964.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row964);

            // Row 17: SA-403, 347H, Smls. & wld. fittings
            var row965 = new OldStressRowData();
            row965.LineNo = 17;
            row965.MaterialId = 9400;
            row965.SpecNo = "SA-403";
            row965.TypeGrade = "347H";
            row965.ProductForm = "Smls. & wld. fittings";
            row965.AlloyUNS = "S34709";
            row965.ClassCondition = "";
            row965.Notes = "G5, T8, W12, W14";
            row965.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row965);

            // Row 18: SA-452, TP347H, Cast pipe
            var row966 = new OldStressRowData();
            row966.LineNo = 18;
            row966.MaterialId = 9415;
            row966.SpecNo = "SA-452";
            row966.TypeGrade = "TP347H";
            row966.ProductForm = "Cast pipe";
            row966.AlloyUNS = "S34709";
            row966.ClassCondition = "";
            row966.Notes = "G5, G16, G17, G32";
            row966.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row966);

            // Row 19: SA-452, TP347H, Cast pipe
            var row967 = new OldStressRowData();
            row967.LineNo = 19;
            row967.MaterialId = 9416;
            row967.SpecNo = "SA-452";
            row967.TypeGrade = "TP347H";
            row967.ProductForm = "Cast pipe";
            row967.AlloyUNS = "S34709";
            row967.ClassCondition = "";
            row967.Notes = "G32";
            row967.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row967);

            // Row 20: SA-479, 347H, Bar
            var row968 = new OldStressRowData();
            row968.LineNo = 20;
            row968.MaterialId = 9419;
            row968.SpecNo = "SA-479";
            row968.TypeGrade = "347H";
            row968.ProductForm = "Bar";
            row968.AlloyUNS = "S34709";
            row968.ClassCondition = "";
            row968.Notes = "G5, G18, T8";
            row968.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row968);

            // Row 21: SA-479, 347H, Bar
            var row969 = new OldStressRowData();
            row969.LineNo = 21;
            row969.MaterialId = 9420;
            row969.SpecNo = "SA-479";
            row969.TypeGrade = "347H";
            row969.ProductForm = "Bar";
            row969.AlloyUNS = "S34709";
            row969.ClassCondition = "";
            row969.Notes = "G18, T9";
            row969.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row969);

            // Row 22: SA-813, TP347H, Wld. pipe
            var row970 = new OldStressRowData();
            row970.LineNo = 22;
            row970.MaterialId = 9426;
            row970.SpecNo = "SA-813";
            row970.TypeGrade = "TP347H";
            row970.ProductForm = "Wld. pipe";
            row970.AlloyUNS = "S34709";
            row970.ClassCondition = "";
            row970.Notes = "G5, W12";
            row970.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row970);

            // Row 23: SA-814, TP347H, Wld. pipe
            var row971 = new OldStressRowData();
            row971.LineNo = 23;
            row971.MaterialId = 9429;
            row971.SpecNo = "SA-814";
            row971.TypeGrade = "TP347H";
            row971.ProductForm = "Wld. pipe";
            row971.AlloyUNS = "S34709";
            row971.ClassCondition = "";
            row971.Notes = "G5, W12";
            row971.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row971);

            // Row 24: SA-182, F348, Forgings
            var row972 = new OldStressRowData();
            row972.LineNo = 24;
            row972.MaterialId = 9322;
            row972.SpecNo = "SA-182";
            row972.TypeGrade = "F348";
            row972.ProductForm = "Forgings";
            row972.AlloyUNS = "S34800";
            row972.ClassCondition = "";
            row972.Notes = "G5, G12, G18, H2, T6";
            row972.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row972);

            // Row 25: SA-182, F348, Forgings
            var row973 = new OldStressRowData();
            row973.LineNo = 25;
            row973.MaterialId = 9323;
            row973.SpecNo = "SA-182";
            row973.TypeGrade = "F348";
            row973.ProductForm = "Forgings";
            row973.AlloyUNS = "S34800";
            row973.ClassCondition = "";
            row973.Notes = "G12, G18, H2, T7";
            row973.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row973);

            // Row 26: SA-213, TP348, Smls. tube
            var row974 = new OldStressRowData();
            row974.LineNo = 26;
            row974.MaterialId = 9330;
            row974.SpecNo = "SA-213";
            row974.TypeGrade = "TP348";
            row974.ProductForm = "Smls. tube";
            row974.AlloyUNS = "S34800";
            row974.ClassCondition = "";
            row974.Notes = "G5, G12, G18, H2, T6";
            row974.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row974);

            // Row 27: SA-213, TP348, Smls. tube
            var row975 = new OldStressRowData();
            row975.LineNo = 27;
            row975.MaterialId = 9331;
            row975.SpecNo = "SA-213";
            row975.TypeGrade = "TP348";
            row975.ProductForm = "Smls. tube";
            row975.AlloyUNS = "S34800";
            row975.ClassCondition = "";
            row975.Notes = "G12, G18, H2, T7";
            row975.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row975);

            // Row 28: SA-240, 348, Plate
            var row976 = new OldStressRowData();
            row976.LineNo = 28;
            row976.MaterialId = 9337;
            row976.SpecNo = "SA-240";
            row976.TypeGrade = "348";
            row976.ProductForm = "Plate";
            row976.AlloyUNS = "S34800";
            row976.ClassCondition = "";
            row976.Notes = "G5, G12, T6";
            row976.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row976);

            // Row 29: SA-240, 348, Plate
            var row977 = new OldStressRowData();
            row977.LineNo = 29;
            row977.MaterialId = 9338;
            row977.SpecNo = "SA-240";
            row977.TypeGrade = "348";
            row977.ProductForm = "Plate";
            row977.AlloyUNS = "S34800";
            row977.ClassCondition = "";
            row977.Notes = "G12, T7";
            row977.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row977);

            // Row 30: SA-249, TP348, Wld. tube
            var row978 = new OldStressRowData();
            row978.LineNo = 30;
            row978.MaterialId = 9352;
            row978.SpecNo = "SA-249";
            row978.TypeGrade = "TP348";
            row978.ProductForm = "Wld. tube";
            row978.AlloyUNS = "S34800";
            row978.ClassCondition = "";
            row978.Notes = "G12, G18, T7, W13";
            row978.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row978);

            // Row 31: SA-249, TP348, Wld. tube
            var row979 = new OldStressRowData();
            row979.LineNo = 31;
            row979.MaterialId = 9349;
            row979.SpecNo = "SA-249";
            row979.TypeGrade = "TP348";
            row979.ProductForm = "Wld. tube";
            row979.AlloyUNS = "S34800";
            row979.ClassCondition = "";
            row979.Notes = "G5, G12, G18, T6, W12, W13";
            row979.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row979);

            // Row 32: SA-249, TP348, Wld. tube
            var row980 = new OldStressRowData();
            row980.LineNo = 32;
            row980.MaterialId = 9350;
            row980.SpecNo = "SA-249";
            row980.TypeGrade = "TP348";
            row980.ProductForm = "Wld. tube";
            row980.AlloyUNS = "S34800";
            row980.ClassCondition = "";
            row980.Notes = "G3, G5, G12, G18, G24, T6";
            row980.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 13.6, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row980);

            // Row 33: SA-249, TP348, Wld. tube
            var row981 = new OldStressRowData();
            row981.LineNo = 33;
            row981.MaterialId = 9351;
            row981.SpecNo = "SA-249";
            row981.TypeGrade = "TP348";
            row981.ProductForm = "Wld. tube";
            row981.AlloyUNS = "S34800";
            row981.ClassCondition = "";
            row981.Notes = "G3, G12, G18, G24, T7";
            row981.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row981);

            // Row 34: SA-312, TP348, Smls. & wld. pipe
            var row982 = new OldStressRowData();
            row982.LineNo = 34;
            row982.MaterialId = 9374;
            row982.SpecNo = "SA-312";
            row982.TypeGrade = "TP348";
            row982.ProductForm = "Smls. & wld. pipe";
            row982.AlloyUNS = "S34800";
            row982.ClassCondition = "";
            row982.Notes = "G5, G12, G18, H2, T6, W12";
            row982.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row982);

            // Row 35: SA-312, TP348, Smls. pipe
            var row983 = new OldStressRowData();
            row983.LineNo = 35;
            row983.MaterialId = 9375;
            row983.SpecNo = "SA-312";
            row983.TypeGrade = "TP348";
            row983.ProductForm = "Smls. pipe";
            row983.AlloyUNS = "S34800";
            row983.ClassCondition = "";
            row983.Notes = "G12, G18, H2, T7";
            row983.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row983);

            // Row 36: SA-312, TP348, Wld. pipe
            var row984 = new OldStressRowData();
            row984.LineNo = 36;
            row984.MaterialId = 9372;
            row984.SpecNo = "SA-312";
            row984.TypeGrade = "TP348";
            row984.ProductForm = "Wld. pipe";
            row984.AlloyUNS = "S34800";
            row984.ClassCondition = "";
            row984.Notes = "G5, G12, G24, H2, T6";
            row984.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 13.6, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row984);

            // Row 37: SA-312, TP348, Wld. pipe
            var row985 = new OldStressRowData();
            row985.LineNo = 37;
            row985.MaterialId = 9373;
            row985.SpecNo = "SA-312";
            row985.TypeGrade = "TP348";
            row985.ProductForm = "Wld. pipe";
            row985.AlloyUNS = "S34800";
            row985.ClassCondition = "";
            row985.Notes = "G12, G24, H2, T7";
            row985.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.77, 0.68, null, null, null };
            batch.Add(row985);

            // Row 1: SA-358, 348, Wld. pipe
            var row986 = new OldStressRowData();
            row986.LineNo = 1;
            row986.MaterialId = 9382;
            row986.SpecNo = "SA-358";
            row986.TypeGrade = "348";
            row986.ProductForm = "Wld. pipe";
            row986.AlloyUNS = "S34800";
            row986.ClassCondition = "1";
            row986.Notes = "G5, W12";
            row986.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row986);

            // Row 2: SA-376, TP348, Smls. pipe
            var row987 = new OldStressRowData();
            row987.LineNo = 2;
            row987.MaterialId = 9387;
            row987.SpecNo = "SA-376";
            row987.TypeGrade = "TP348";
            row987.ProductForm = "Smls. pipe";
            row987.AlloyUNS = "S34800";
            row987.ClassCondition = "";
            row987.Notes = "G5, G12, G18, H1, H2, T6";
            row987.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row987);

            // Row 3: SA-376, TP348, Smls. pipe
            var row988 = new OldStressRowData();
            row988.LineNo = 3;
            row988.MaterialId = 9388;
            row988.SpecNo = "SA-376";
            row988.TypeGrade = "TP348";
            row988.ProductForm = "Smls. pipe";
            row988.AlloyUNS = "S34800";
            row988.ClassCondition = "";
            row988.Notes = "G12, G18, H1, H2, T7";
            row988.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row988);

            // Row 4: SA-403, 348, Smls. & wld. fittings
            var row989 = new OldStressRowData();
            row989.LineNo = 4;
            row989.MaterialId = 9402;
            row989.SpecNo = "SA-403";
            row989.TypeGrade = "348";
            row989.ProductForm = "Smls. & wld. fittings";
            row989.AlloyUNS = "S34800";
            row989.ClassCondition = "";
            row989.Notes = "G5, G12, T6, W12, W14";
            row989.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row989);

            // Row 5: SA-409, TP348, Wld. pipe
            var row990 = new OldStressRowData();
            row990.LineNo = 5;
            row990.MaterialId = 9414;
            row990.SpecNo = "SA-409";
            row990.TypeGrade = "TP348";
            row990.ProductForm = "Wld. pipe";
            row990.AlloyUNS = "S34800";
            row990.ClassCondition = "";
            row990.Notes = "G5, W12";
            row990.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row990);

            // Row 6: SA-479, 348, Bar
            var row991 = new OldStressRowData();
            row991.LineNo = 6;
            row991.MaterialId = 9421;
            row991.SpecNo = "SA-479";
            row991.TypeGrade = "348";
            row991.ProductForm = "Bar";
            row991.AlloyUNS = "S34800";
            row991.ClassCondition = "";
            row991.Notes = "G5, G12, G18, G22, H2, T6";
            row991.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.2, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row991);

            // Row 7: SA-479, 348, Bar
            var row992 = new OldStressRowData();
            row992.LineNo = 7;
            row992.MaterialId = 9422;
            row992.SpecNo = "SA-479";
            row992.TypeGrade = "348";
            row992.ProductForm = "Bar";
            row992.AlloyUNS = "S34800";
            row992.ClassCondition = "";
            row992.Notes = "G12, G18, G22, H2, T7";
            row992.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row992);

            // Row 8: SA-813, TP348, Wld. pipe
            var row993 = new OldStressRowData();
            row993.LineNo = 8;
            row993.MaterialId = 9427;
            row993.SpecNo = "SA-813";
            row993.TypeGrade = "TP348";
            row993.ProductForm = "Wld. pipe";
            row993.AlloyUNS = "S34800";
            row993.ClassCondition = "";
            row993.Notes = "G5, W12";
            row993.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row993);

            // Row 9: SA-814, TP348, Wld. pipe
            var row994 = new OldStressRowData();
            row994.LineNo = 9;
            row994.MaterialId = 9430;
            row994.SpecNo = "SA-814";
            row994.TypeGrade = "TP348";
            row994.ProductForm = "Wld. pipe";
            row994.AlloyUNS = "S34800";
            row994.ClassCondition = "";
            row994.Notes = "G5, W12";
            row994.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row994);

            // Row 10: SA-182, F348H, Forgings
            var row995 = new OldStressRowData();
            row995.LineNo = 10;
            row995.MaterialId = 9324;
            row995.SpecNo = "SA-182";
            row995.TypeGrade = "F348H";
            row995.ProductForm = "Forgings";
            row995.AlloyUNS = "S34809";
            row995.ClassCondition = "";
            row995.Notes = "G5, G18, H2, T8";
            row995.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row995);

            // Row 11: SA-182, F348H, Forgings
            var row996 = new OldStressRowData();
            row996.LineNo = 11;
            row996.MaterialId = 9325;
            row996.SpecNo = "SA-182";
            row996.TypeGrade = "F348H";
            row996.ProductForm = "Forgings";
            row996.AlloyUNS = "S34809";
            row996.ClassCondition = "";
            row996.Notes = "G18, H2, T9";
            row996.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row996);

            // Row 12: SA-213, TP348H, Smls. tube
            var row997 = new OldStressRowData();
            row997.LineNo = 12;
            row997.MaterialId = 9332;
            row997.SpecNo = "SA-213";
            row997.TypeGrade = "TP348H";
            row997.ProductForm = "Smls. tube";
            row997.AlloyUNS = "S34809";
            row997.ClassCondition = "";
            row997.Notes = "G5, G18, H2, T8";
            row997.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row997);

            // Row 13: SA-213, TP348H, Smls. tube
            var row998 = new OldStressRowData();
            row998.LineNo = 13;
            row998.MaterialId = 9333;
            row998.SpecNo = "SA-213";
            row998.TypeGrade = "TP348H";
            row998.ProductForm = "Smls. tube";
            row998.AlloyUNS = "S34809";
            row998.ClassCondition = "";
            row998.Notes = "G18, H2, T9";
            row998.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row998);

            // Row 14: SA-240, 348H, Plate
            var row999 = new OldStressRowData();
            row999.LineNo = 14;
            row999.MaterialId = 9339;
            row999.SpecNo = "SA-240";
            row999.TypeGrade = "348H";
            row999.ProductForm = "Plate";
            row999.AlloyUNS = "S34809";
            row999.ClassCondition = "";
            row999.Notes = "G5";
            row999.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row999);

            // Row 15: SA-249, TP348H, Wld. tube
            var row1000 = new OldStressRowData();
            row1000.LineNo = 15;
            row1000.MaterialId = 9355;
            row1000.SpecNo = "SA-249";
            row1000.TypeGrade = "TP348H";
            row1000.ProductForm = "Wld. tube";
            row1000.AlloyUNS = "S34809";
            row1000.ClassCondition = "";
            row1000.Notes = "G5, G18, T8, W12, W13";
            row1000.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
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
