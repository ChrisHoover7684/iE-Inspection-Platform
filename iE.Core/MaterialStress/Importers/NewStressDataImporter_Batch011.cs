using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch011
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch011(MaterialStressService service)
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

            // Row 16: SA-249, TP348H, Wld. tube
            var row1001 = new OldStressRowData();
            row1001.LineNo = 16;
            row1001.MaterialId = 9356;
            row1001.SpecNo = "SA-249";
            row1001.TypeGrade = "TP348H";
            row1001.ProductForm = "Wld. tube";
            row1001.AlloyUNS = "S34809";
            row1001.ClassCondition = "";
            row1001.Notes = "G18, T9, W13";
            row1001.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1001);

            // Row 17: SA-249, TP348H, Wld. tube
            var row1002 = new OldStressRowData();
            row1002.LineNo = 17;
            row1002.MaterialId = 9353;
            row1002.SpecNo = "SA-249";
            row1002.TypeGrade = "TP348H";
            row1002.ProductForm = "Wld. tube";
            row1002.AlloyUNS = "S34809";
            row1002.ClassCondition = "";
            row1002.Notes = "G3, G5, G18, G24, T8";
            row1002.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 14, 13.7, 12, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1002);

            // Row 18: SA-249, TP348H, Wld. tube
            var row1003 = new OldStressRowData();
            row1003.LineNo = 18;
            row1003.MaterialId = 9354;
            row1003.SpecNo = "SA-249";
            row1003.TypeGrade = "TP348H";
            row1003.ProductForm = "Wld. tube";
            row1003.AlloyUNS = "S34809";
            row1003.ClassCondition = "";
            row1003.Notes = "G3, G18, G24, T9";
            row1003.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 11.4, 11.3, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1003);

            // Row 19: SA-312, TP348H, Smls. & wld. pipe
            var row1004 = new OldStressRowData();
            row1004.LineNo = 19;
            row1004.MaterialId = 9378;
            row1004.SpecNo = "SA-312";
            row1004.TypeGrade = "TP348H";
            row1004.ProductForm = "Smls. & wld. pipe";
            row1004.AlloyUNS = "S34809";
            row1004.ClassCondition = "";
            row1004.Notes = "G5, G18, H2, T8, W12";
            row1004.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1004);

            // Row 20: SA-312, TP348H, Smls. pipe
            var row1005 = new OldStressRowData();
            row1005.LineNo = 20;
            row1005.MaterialId = 9379;
            row1005.SpecNo = "SA-312";
            row1005.TypeGrade = "TP348H";
            row1005.ProductForm = "Smls. pipe";
            row1005.AlloyUNS = "S34809";
            row1005.ClassCondition = "";
            row1005.Notes = "G18, H2, T9";
            row1005.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1005);

            // Row 21: SA-312, TP348H, Wld. pipe
            var row1006 = new OldStressRowData();
            row1006.LineNo = 21;
            row1006.MaterialId = 9376;
            row1006.SpecNo = "SA-312";
            row1006.TypeGrade = "TP348H";
            row1006.ProductForm = "Wld. pipe";
            row1006.AlloyUNS = "S34809";
            row1006.ClassCondition = "";
            row1006.Notes = "G5, G24, H2, T8";
            row1006.StressValues = new double?[] { 17, null, 17, null, 16, 15.1, 14.6, 14.3, 14.3, 14.3, 14.3, 14.3, 14.3, 14.2, 14.1, 14, 13.7, 12, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1006);

            // Row 22: SA-312, TP348H, Wld. pipe
            var row1007 = new OldStressRowData();
            row1007.LineNo = 22;
            row1007.MaterialId = 9377;
            row1007.SpecNo = "SA-312";
            row1007.TypeGrade = "TP348H";
            row1007.ProductForm = "Wld. pipe";
            row1007.AlloyUNS = "S34809";
            row1007.ClassCondition = "";
            row1007.Notes = "G24, H2, T9";
            row1007.StressValues = new double?[] { 17, null, 15.6, null, 14.6, 13.6, 12.8, 12.2, 11.9, 11.8, 11.6, 11.5, 11.5, 11.4, 11.4, 11.4, 11.4, 11.3, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1007);

            // Row 23: SA-403, 348H, Smls. & wld. fittings
            var row1008 = new OldStressRowData();
            row1008.LineNo = 23;
            row1008.MaterialId = 9412;
            row1008.SpecNo = "SA-403";
            row1008.TypeGrade = "348H";
            row1008.ProductForm = "Smls. & wld. fittings";
            row1008.AlloyUNS = "S34809";
            row1008.ClassCondition = "";
            row1008.Notes = "G5, T8, W12, W14";
            row1008.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1008);

            // Row 24: SA-479, 348H, Bar
            var row1009 = new OldStressRowData();
            row1009.LineNo = 24;
            row1009.MaterialId = 9423;
            row1009.SpecNo = "SA-479";
            row1009.TypeGrade = "348H";
            row1009.ProductForm = "Bar";
            row1009.AlloyUNS = "S34809";
            row1009.ClassCondition = "";
            row1009.Notes = "G5, G18, T8";
            row1009.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, 16.8, 16.7, 16.6, 16.4, 16.2, 14.1, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1009);

            // Row 25: SA-479, 348H, Bar
            var row1010 = new OldStressRowData();
            row1010.LineNo = 25;
            row1010.MaterialId = 9424;
            row1010.SpecNo = "SA-479";
            row1010.TypeGrade = "348H";
            row1010.ProductForm = "Bar";
            row1010.AlloyUNS = "S34809";
            row1010.ClassCondition = "";
            row1010.Notes = "G18, T9";
            row1010.StressValues = new double?[] { 20, null, 18.4, null, 17.1, 16, 15, 14.3, 14, 13.8, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 13.4, 13.3, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1010);

            // Row 26: SA-813, TP348H, Wld. pipe
            var row1011 = new OldStressRowData();
            row1011.LineNo = 26;
            row1011.MaterialId = 9428;
            row1011.SpecNo = "SA-813";
            row1011.TypeGrade = "TP348H";
            row1011.ProductForm = "Wld. pipe";
            row1011.AlloyUNS = "S34809";
            row1011.ClassCondition = "";
            row1011.Notes = "G5, W12";
            row1011.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1011);

            // Row 27: SA-814, TP348H, Wld. pipe
            var row1012 = new OldStressRowData();
            row1012.LineNo = 27;
            row1012.MaterialId = 9431;
            row1012.SpecNo = "SA-814";
            row1012.TypeGrade = "TP348H";
            row1012.ProductForm = "Wld. pipe";
            row1012.AlloyUNS = "S34809";
            row1012.ClassCondition = "";
            row1012.Notes = "G5, W12";
            row1012.StressValues = new double?[] { 20, null, 20, null, 18.8, 17.8, 17.1, 16.9, 16.8, 16.8, 16.8, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1012);

            // Row 28: SA-376, TP321, Smls. pipe
            var row1013 = new OldStressRowData();
            row1013.LineNo = 28;
            row1013.MaterialId = 9432;
            row1013.SpecNo = "SA-376";
            row1013.TypeGrade = "TP321";
            row1013.ProductForm = "Smls. pipe";
            row1013.AlloyUNS = "S32100";
            row1013.ClassCondition = "";
            row1013.Notes = "G5, G12, T7";
            row1013.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 16.7, 16.1, 15.2, 14.9, 14.6, 14.3, 14.1, 13.9, 13.8, 13.6, 13.5, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1013);

            // Row 29: SA-376, TP321, Smls. pipe
            var row1014 = new OldStressRowData();
            row1014.LineNo = 29;
            row1014.MaterialId = 9433;
            row1014.SpecNo = "SA-376";
            row1014.TypeGrade = "TP321";
            row1014.ProductForm = "Smls. pipe";
            row1014.AlloyUNS = "S32100";
            row1014.ClassCondition = "";
            row1014.Notes = "G12, T7";
            row1014.StressValues = new double?[] { 16.7, null, 15, null, 13.8, 12.8, 11.9, 11.3, 11, 10.8, 10.6, 10.5, 10.3, 10.2, 10.1, 10, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1014);

            // Row 30: SA-376, TP321H, Smls. pipe
            var row1015 = new OldStressRowData();
            row1015.LineNo = 30;
            row1015.MaterialId = 9434;
            row1015.SpecNo = "SA-376";
            row1015.TypeGrade = "TP321H";
            row1015.ProductForm = "Smls. pipe";
            row1015.AlloyUNS = "S32109";
            row1015.ClassCondition = "";
            row1015.Notes = "G5, T8";
            row1015.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 16.7, 16.1, 15.2, 14.9, 14.6, 14.3, 14.1, 13.9, 13.8, 13.6, 13.5, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1015);

            // Row 31: SA-182, F321, Forgings
            var row1016 = new OldStressRowData();
            row1016.LineNo = 31;
            row1016.MaterialId = 9436;
            row1016.SpecNo = "SA-182";
            row1016.TypeGrade = "F321";
            row1016.ProductForm = "Forgings";
            row1016.AlloyUNS = "S32100";
            row1016.ClassCondition = "";
            row1016.Notes = "G5, G12, G18, H2, T6";
            row1016.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1016);

            // Row 32: SA-182, F321, Forgings
            var row1017 = new OldStressRowData();
            row1017.LineNo = 32;
            row1017.MaterialId = 9437;
            row1017.SpecNo = "SA-182";
            row1017.TypeGrade = "F321";
            row1017.ProductForm = "Forgings";
            row1017.AlloyUNS = "S32100";
            row1017.ClassCondition = "";
            row1017.Notes = "G12, G18, H2, T7";
            row1017.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1017);

            // Row 33: SA-336, F321, Forgings
            var row1018 = new OldStressRowData();
            row1018.LineNo = 33;
            row1018.MaterialId = 9442;
            row1018.SpecNo = "SA-336";
            row1018.TypeGrade = "F321";
            row1018.ProductForm = "Forgings";
            row1018.AlloyUNS = "S32100";
            row1018.ClassCondition = "";
            row1018.Notes = "G5, G12, G18, H2, T6";
            row1018.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1018);

            // Row 34: SA-336, F321, Forgings
            var row1019 = new OldStressRowData();
            row1019.LineNo = 34;
            row1019.MaterialId = 9443;
            row1019.SpecNo = "SA-336";
            row1019.TypeGrade = "F321";
            row1019.ProductForm = "Forgings";
            row1019.AlloyUNS = "S32100";
            row1019.ClassCondition = "";
            row1019.Notes = "G12, G18, H2, T7";
            row1019.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1019);

            // Row 35: SA-430, FP321, Forged pipe
            var row1020 = new OldStressRowData();
            row1020.LineNo = 35;
            row1020.MaterialId = 9447;
            row1020.SpecNo = "SA-430";
            row1020.TypeGrade = "FP321";
            row1020.ProductForm = "Forged pipe";
            row1020.AlloyUNS = "S32100";
            row1020.ClassCondition = "";
            row1020.Notes = "G5, G12, G18, H1, H2, T6";
            row1020.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1020);

            // Row 36: SA-430, FP321, Forged pipe
            var row1021 = new OldStressRowData();
            row1021.LineNo = 36;
            row1021.MaterialId = 9448;
            row1021.SpecNo = "SA-430";
            row1021.TypeGrade = "FP321";
            row1021.ProductForm = "Forged pipe";
            row1021.AlloyUNS = "S32100";
            row1021.ClassCondition = "";
            row1021.Notes = "G12, G18, H1, H2, T7";
            row1021.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1021);

            // Row 1: SA-182, F321H, Forgings
            var row1022 = new OldStressRowData();
            row1022.LineNo = 1;
            row1022.MaterialId = 9439;
            row1022.SpecNo = "SA-182";
            row1022.TypeGrade = "F321H";
            row1022.ProductForm = "Forgings";
            row1022.AlloyUNS = "S32109";
            row1022.ClassCondition = "";
            row1022.Notes = "G5, G18, H2, T8";
            row1022.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1022);

            // Row 2: SA-182, F321H, Forgings
            var row1023 = new OldStressRowData();
            row1023.LineNo = 2;
            row1023.MaterialId = 9440;
            row1023.SpecNo = "SA-182";
            row1023.TypeGrade = "F321H";
            row1023.ProductForm = "Forgings";
            row1023.AlloyUNS = "S32109";
            row1023.ClassCondition = "";
            row1023.Notes = "G18, H2, T8";
            row1023.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1023);

            // Row 3: SA-336, F321H, Forgings
            var row1024 = new OldStressRowData();
            row1024.LineNo = 3;
            row1024.MaterialId = 9445;
            row1024.SpecNo = "SA-336";
            row1024.TypeGrade = "F321H";
            row1024.ProductForm = "Forgings";
            row1024.AlloyUNS = "S32109";
            row1024.ClassCondition = "";
            row1024.Notes = "G5, H2, T8";
            row1024.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1024);

            // Row 4: SA-336, F321H, Forgings
            var row1025 = new OldStressRowData();
            row1025.LineNo = 4;
            row1025.MaterialId = 9446;
            row1025.SpecNo = "SA-336";
            row1025.TypeGrade = "F321H";
            row1025.ProductForm = "Forgings";
            row1025.AlloyUNS = "S32109";
            row1025.ClassCondition = "";
            row1025.Notes = "H2, T8";
            row1025.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1025);

            // Row 5: SA-430, FP321H, Forged pipe
            var row1026 = new OldStressRowData();
            row1026.LineNo = 5;
            row1026.MaterialId = 9451;
            row1026.SpecNo = "SA-430";
            row1026.TypeGrade = "FP321H";
            row1026.ProductForm = "Forged pipe";
            row1026.AlloyUNS = "S32109";
            row1026.ClassCondition = "";
            row1026.Notes = "G5, G18, H1, H2, T8";
            row1026.StressValues = new double?[] { 20, null, 19, null, 17.8, 17.5, 17.5, 17.5, 17.5, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1026);

            // Row 6: SA-430, FP321H, Forged pipe
            var row1027 = new OldStressRowData();
            row1027.LineNo = 6;
            row1027.MaterialId = 9450;
            row1027.SpecNo = "SA-430";
            row1027.TypeGrade = "FP321H";
            row1027.ProductForm = "Forged pipe";
            row1027.AlloyUNS = "S32109";
            row1027.ClassCondition = "";
            row1027.Notes = "G18, H1, H2, T8";
            row1027.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1027);

            // Row 7: SA-182, F321, Forgings
            var row1028 = new OldStressRowData();
            row1028.LineNo = 7;
            row1028.MaterialId = 9455;
            row1028.SpecNo = "SA-182";
            row1028.TypeGrade = "F321";
            row1028.ProductForm = "Forgings";
            row1028.AlloyUNS = "S32100";
            row1028.ClassCondition = "";
            row1028.Notes = "G5, G12, G18, T7";
            row1028.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1028);

            // Row 8: SA-182, F321, Forgings
            var row1029 = new OldStressRowData();
            row1029.LineNo = 8;
            row1029.MaterialId = 9454;
            row1029.SpecNo = "SA-182";
            row1029.TypeGrade = "F321";
            row1029.ProductForm = "Forgings";
            row1029.AlloyUNS = "S32100";
            row1029.ClassCondition = "";
            row1029.Notes = "G12, G18, T7";
            row1029.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1029);

            // Row 9: SA-182, F321, Forgings
            var row1030 = new OldStressRowData();
            row1030.LineNo = 9;
            row1030.MaterialId = 9453;
            row1030.SpecNo = "SA-182";
            row1030.TypeGrade = "F321";
            row1030.ProductForm = "Forgings";
            row1030.AlloyUNS = "S32100";
            row1030.ClassCondition = "";
            row1030.Notes = "G5, G12, H2, T6";
            row1030.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1030);

            // Row 10: SA-213, TP321, Smls. tube
            var row1031 = new OldStressRowData();
            row1031.LineNo = 10;
            row1031.MaterialId = 9460;
            row1031.SpecNo = "SA-213";
            row1031.TypeGrade = "TP321";
            row1031.ProductForm = "Smls. tube";
            row1031.AlloyUNS = "S32100";
            row1031.ClassCondition = "";
            row1031.Notes = "G5, G12, G18, T7";
            row1031.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1031);

            // Row 11: SA-213, TP321, Smls. tube
            var row1032 = new OldStressRowData();
            row1032.LineNo = 11;
            row1032.MaterialId = 9459;
            row1032.SpecNo = "SA-213";
            row1032.TypeGrade = "TP321";
            row1032.ProductForm = "Smls. tube";
            row1032.AlloyUNS = "S32100";
            row1032.ClassCondition = "";
            row1032.Notes = "G12, G18, T7";
            row1032.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1032);

            // Row 12: SA-213, TP321, Smls. tube
            var row1033 = new OldStressRowData();
            row1033.LineNo = 12;
            row1033.MaterialId = 9458;
            row1033.SpecNo = "SA-213";
            row1033.TypeGrade = "TP321";
            row1033.ProductForm = "Smls. tube";
            row1033.AlloyUNS = "S32100";
            row1033.ClassCondition = "";
            row1033.Notes = "G5, G12, H2, T6";
            row1033.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1033);

            // Row 13: SA-240, 321, Plate
            var row1034 = new OldStressRowData();
            row1034.LineNo = 13;
            row1034.MaterialId = 9465;
            row1034.SpecNo = "SA-240";
            row1034.TypeGrade = "321";
            row1034.ProductForm = "Plate";
            row1034.AlloyUNS = "S32100";
            row1034.ClassCondition = "";
            row1034.Notes = "G5, G12, G18, T7";
            row1034.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1034);

            // Row 14: SA-240, 321, Plate
            var row1035 = new OldStressRowData();
            row1035.LineNo = 14;
            row1035.MaterialId = 9464;
            row1035.SpecNo = "SA-240";
            row1035.TypeGrade = "321";
            row1035.ProductForm = "Plate";
            row1035.AlloyUNS = "S32100";
            row1035.ClassCondition = "";
            row1035.Notes = "G12, G18, T7";
            row1035.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1035);

            // Row 15: SA-240, 321, Plate
            var row1036 = new OldStressRowData();
            row1036.LineNo = 15;
            row1036.MaterialId = 9463;
            row1036.SpecNo = "SA-240";
            row1036.TypeGrade = "321";
            row1036.ProductForm = "Plate";
            row1036.AlloyUNS = "S32100";
            row1036.ClassCondition = "";
            row1036.Notes = "G5, G12, T6";
            row1036.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1036);

            // Row 16: SA-249, TP321, Wld. tube
            var row1037 = new OldStressRowData();
            row1037.LineNo = 16;
            row1037.MaterialId = 9471;
            row1037.SpecNo = "SA-249";
            row1037.TypeGrade = "TP321";
            row1037.ProductForm = "Wld. tube";
            row1037.AlloyUNS = "S32100";
            row1037.ClassCondition = "";
            row1037.Notes = "G12, G18, T7, W13";
            row1037.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1037);

            // Row 17: SA-249, TP321, Wld. tube
            var row1038 = new OldStressRowData();
            row1038.LineNo = 17;
            row1038.MaterialId = 9467;
            row1038.SpecNo = "SA-249";
            row1038.TypeGrade = "TP321";
            row1038.ProductForm = "Wld. tube";
            row1038.AlloyUNS = "S32100";
            row1038.ClassCondition = "";
            row1038.Notes = "G5, G12, G18, T7, W12, W13";
            row1038.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1038);

            // Row 18: SA-249, TP321, Wld. tube
            var row1039 = new OldStressRowData();
            row1039.LineNo = 18;
            row1039.MaterialId = 9468;
            row1039.SpecNo = "SA-249";
            row1039.TypeGrade = "TP321";
            row1039.ProductForm = "Wld. tube";
            row1039.AlloyUNS = "S32100";
            row1039.ClassCondition = "";
            row1039.Notes = "G3, G5, G12, G18, G24, T7";
            row1039.StressValues = new double?[] { 17, null, 17, null, 16.2, 15.9, 15.9, 15.5, 15.2, 14.9, 14.6, 14.4, 14.2, 14.1, 13.9, 13.8, 8.2, 5.9, 4.3, 3.1, 2.2, 1.4, 0.9, 0.68, 0.43, 0.26, null, null, null };
            batch.Add(row1039);

            // Row 19: SA-249, TP321, Wld. tube
            var row1040 = new OldStressRowData();
            row1040.LineNo = 19;
            row1040.MaterialId = 9469;
            row1040.SpecNo = "SA-249";
            row1040.TypeGrade = "TP321";
            row1040.ProductForm = "Wld. tube";
            row1040.AlloyUNS = "S32100";
            row1040.ClassCondition = "";
            row1040.Notes = "G3, G12, G18, G24, T7";
            row1040.StressValues = new double?[] { 17, null, 15.3, null, 14.1, 13, 12.2, 11.5, 11.2, 11, 10.8, 10.7, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.4, 0.9, 0.68, 0.43, 0.26, null, null, null };
            batch.Add(row1040);

            // Row 20: SA-312, TP321, Smls. pipe
            var row1041 = new OldStressRowData();
            row1041.LineNo = 20;
            row1041.MaterialId = 9478;
            row1041.SpecNo = "SA-312";
            row1041.TypeGrade = "TP321";
            row1041.ProductForm = "Smls. pipe";
            row1041.AlloyUNS = "S32100";
            row1041.ClassCondition = "";
            row1041.Notes = "G5, G12, G18, T7";
            row1041.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1041);

            // Row 21: SA-312, TP321, Smls. pipe
            var row1042 = new OldStressRowData();
            row1042.LineNo = 21;
            row1042.MaterialId = 9480;
            row1042.SpecNo = "SA-312";
            row1042.TypeGrade = "TP321";
            row1042.ProductForm = "Smls. pipe";
            row1042.AlloyUNS = "S32100";
            row1042.ClassCondition = "";
            row1042.Notes = "G12, G18, T7";
            row1042.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1042);

            // Row 22: SA-312, TP321, Smls. pipe
            var row1043 = new OldStressRowData();
            row1043.LineNo = 22;
            row1043.MaterialId = 9479;
            row1043.SpecNo = "SA-312";
            row1043.TypeGrade = "TP321";
            row1043.ProductForm = "Smls. pipe";
            row1043.AlloyUNS = "S32100";
            row1043.ClassCondition = "";
            row1043.Notes = "G5, G12, H2, T6, W12";
            row1043.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1043);

            // Row 23: SA-312, TP321, Wld. pipe
            var row1044 = new OldStressRowData();
            row1044.LineNo = 23;
            row1044.MaterialId = 9483;
            row1044.SpecNo = "SA-312";
            row1044.TypeGrade = "TP321";
            row1044.ProductForm = "Wld. pipe";
            row1044.AlloyUNS = "S32100";
            row1044.ClassCondition = "";
            row1044.Notes = "G5, G12, G18, T7, W13";
            row1044.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1044);

            // Row 24: SA-312, TP321, Wld. pipe
            var row1045 = new OldStressRowData();
            row1045.LineNo = 24;
            row1045.MaterialId = 9485;
            row1045.SpecNo = "SA-312";
            row1045.TypeGrade = "TP321";
            row1045.ProductForm = "Wld. pipe";
            row1045.AlloyUNS = "S32100";
            row1045.ClassCondition = "";
            row1045.Notes = "G12, G18, T7, W13";
            row1045.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1045);

            // Row 25: SA-312, TP321, Wld. pipe
            var row1046 = new OldStressRowData();
            row1046.LineNo = 25;
            row1046.MaterialId = 9481;
            row1046.SpecNo = "SA-312";
            row1046.TypeGrade = "TP321";
            row1046.ProductForm = "Wld. pipe";
            row1046.AlloyUNS = "S32100";
            row1046.ClassCondition = "";
            row1046.Notes = "G3, G5, G12, G18, G24, T7";
            row1046.StressValues = new double?[] { 17, null, 17, null, 16.2, 15.9, 15.9, 15.5, 15.2, 14.9, 14.6, 14.4, 14.2, 14.1, 13.9, 13.8, 8.2, 5.9, 4.3, 3.1, 2.2, 1.4, 0.9, 0.68, 0.43, 0.26, null, null, null };
            batch.Add(row1046);

            // Row 26: SA-312, TP321, Wld. pipe
            var row1047 = new OldStressRowData();
            row1047.LineNo = 26;
            row1047.MaterialId = 9482;
            row1047.SpecNo = "SA-312";
            row1047.TypeGrade = "TP321";
            row1047.ProductForm = "Wld. pipe";
            row1047.AlloyUNS = "S32100";
            row1047.ClassCondition = "";
            row1047.Notes = "G3, G12, G18, G24, T7";
            row1047.StressValues = new double?[] { 17, null, 15.3, null, 14.1, 13, 12.2, 11.5, 11.2, 11, 10.8, 10.7, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.4, 0.9, 0.68, 0.43, 0.26, null, null, null };
            batch.Add(row1047);

            // Row 27: SA-358, 321, Wld. pipe
            var row1048 = new OldStressRowData();
            row1048.LineNo = 27;
            row1048.MaterialId = 9494;
            row1048.SpecNo = "SA-358";
            row1048.TypeGrade = "321";
            row1048.ProductForm = "Wld. pipe";
            row1048.AlloyUNS = "S32100";
            row1048.ClassCondition = "1";
            row1048.Notes = "G5, W12";
            row1048.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1048);

            // Row 28: SA-376, TP321, Smls. pipe
            var row1049 = new OldStressRowData();
            row1049.LineNo = 28;
            row1049.MaterialId = 9497;
            row1049.SpecNo = "SA-376";
            row1049.TypeGrade = "TP321";
            row1049.ProductForm = "Smls. pipe";
            row1049.AlloyUNS = "S32100";
            row1049.ClassCondition = "";
            row1049.Notes = "G5, G12, G18, H1, T7";
            row1049.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1049);

            // Row 29: SA-376, TP321, Smls. pipe
            var row1050 = new OldStressRowData();
            row1050.LineNo = 29;
            row1050.MaterialId = 9496;
            row1050.SpecNo = "SA-376";
            row1050.TypeGrade = "TP321";
            row1050.ProductForm = "Smls. pipe";
            row1050.AlloyUNS = "S32100";
            row1050.ClassCondition = "";
            row1050.Notes = "G12, G18, H1, T7";
            row1050.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1050);

            // Row 30: SA-376, TP321, Smls. pipe
            var row1051 = new OldStressRowData();
            row1051.LineNo = 30;
            row1051.MaterialId = 9495;
            row1051.SpecNo = "SA-376";
            row1051.TypeGrade = "TP321";
            row1051.ProductForm = "Smls. pipe";
            row1051.AlloyUNS = "S32100";
            row1051.ClassCondition = "";
            row1051.Notes = "G5, G12, H2, T6";
            row1051.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1051);

            // Row 31: SA-403, 321, Smls. & wld. fittings
            var row1052 = new OldStressRowData();
            row1052.LineNo = 31;
            row1052.MaterialId = 9504;
            row1052.SpecNo = "SA-403";
            row1052.TypeGrade = "321";
            row1052.ProductForm = "Smls. & wld. fittings";
            row1052.AlloyUNS = "S32100";
            row1052.ClassCondition = "";
            row1052.Notes = "G5, G12, T6, W12, W14";
            row1052.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1052);

            // Row 32: SA-409, TP321, Wld. pipe
            var row1053 = new OldStressRowData();
            row1053.LineNo = 32;
            row1053.MaterialId = 9512;
            row1053.SpecNo = "SA-409";
            row1053.TypeGrade = "TP321";
            row1053.ProductForm = "Wld. pipe";
            row1053.AlloyUNS = "S32100";
            row1053.ClassCondition = "";
            row1053.Notes = "G5, W12";
            row1053.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1053);

            // Row 33: SA-479, 321, Bar
            var row1054 = new OldStressRowData();
            row1054.LineNo = 33;
            row1054.MaterialId = 9514;
            row1054.SpecNo = "SA-479";
            row1054.TypeGrade = "321";
            row1054.ProductForm = "Bar";
            row1054.AlloyUNS = "S32100";
            row1054.ClassCondition = "";
            row1054.Notes = "G5, G12, G18, T7";
            row1054.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1054);

            // Row 34: SA-479, 321, Bar
            var row1055 = new OldStressRowData();
            row1055.LineNo = 34;
            row1055.MaterialId = 9515;
            row1055.SpecNo = "SA-479";
            row1055.TypeGrade = "321";
            row1055.ProductForm = "Bar";
            row1055.AlloyUNS = "S32100";
            row1055.ClassCondition = "";
            row1055.Notes = "G12, G18, G22, T7";
            row1055.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1055);

            // Row 35: SA-479, 321, Bar
            var row1056 = new OldStressRowData();
            row1056.LineNo = 35;
            row1056.MaterialId = 9513;
            row1056.SpecNo = "SA-479";
            row1056.TypeGrade = "321";
            row1056.ProductForm = "Bar";
            row1056.AlloyUNS = "S32100";
            row1056.ClassCondition = "";
            row1056.Notes = "G5, G12, G22, H2, T6";
            row1056.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1056);

            // Row 36: SA-813, TP321, Wld. pipe
            var row1057 = new OldStressRowData();
            row1057.LineNo = 36;
            row1057.MaterialId = 9518;
            row1057.SpecNo = "SA-813";
            row1057.TypeGrade = "TP321";
            row1057.ProductForm = "Wld. pipe";
            row1057.AlloyUNS = "S32100";
            row1057.ClassCondition = "";
            row1057.Notes = "G5, W12";
            row1057.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1057);

            // Row 37: SA-814, TP321, Wld. pipe
            var row1058 = new OldStressRowData();
            row1058.LineNo = 37;
            row1058.MaterialId = 9520;
            row1058.SpecNo = "SA-814";
            row1058.TypeGrade = "TP321";
            row1058.ProductForm = "Wld. pipe";
            row1058.AlloyUNS = "S32100";
            row1058.ClassCondition = "";
            row1058.Notes = "G5, W12";
            row1058.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1058);

            // Row 1: SA-182, F321H, Forgings
            var row1059 = new OldStressRowData();
            row1059.LineNo = 1;
            row1059.MaterialId = 9456;
            row1059.SpecNo = "SA-182";
            row1059.TypeGrade = "F321H";
            row1059.ProductForm = "Forgings";
            row1059.AlloyUNS = "S32109";
            row1059.ClassCondition = "";
            row1059.Notes = "G5, G18, T8";
            row1059.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1059);

            // Row 2: SA-182, F321H, Forgings
            var row1060 = new OldStressRowData();
            row1060.LineNo = 2;
            row1060.MaterialId = 9457;
            row1060.SpecNo = "SA-182";
            row1060.TypeGrade = "F321H";
            row1060.ProductForm = "Forgings";
            row1060.AlloyUNS = "S32109";
            row1060.ClassCondition = "";
            row1060.Notes = "G18, T8";
            row1060.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1060);

            // Row 3: SA-213, TP321H, Smls. tube
            var row1061 = new OldStressRowData();
            row1061.LineNo = 3;
            row1061.MaterialId = 9461;
            row1061.SpecNo = "SA-213";
            row1061.TypeGrade = "TP321H";
            row1061.ProductForm = "Smls. tube";
            row1061.AlloyUNS = "S32109";
            row1061.ClassCondition = "";
            row1061.Notes = "G5, G18, T8";
            row1061.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1061);

            // Row 4: SA-213, TP321H, Smls. tube
            var row1062 = new OldStressRowData();
            row1062.LineNo = 4;
            row1062.MaterialId = 9462;
            row1062.SpecNo = "SA-213";
            row1062.TypeGrade = "TP321H";
            row1062.ProductForm = "Smls. tube";
            row1062.AlloyUNS = "S32109";
            row1062.ClassCondition = "";
            row1062.Notes = "G18, T8";
            row1062.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1062);

            // Row 5: SA-240, 321H, Plate
            var row1063 = new OldStressRowData();
            row1063.LineNo = 5;
            row1063.MaterialId = 9466;
            row1063.SpecNo = "SA-240";
            row1063.TypeGrade = "321H";
            row1063.ProductForm = "Plate";
            row1063.AlloyUNS = "S32109";
            row1063.ClassCondition = "";
            row1063.Notes = "G5, G18, H2, T8";
            row1063.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1063);

            // Row 6: SA-240, 321H, Plate
            var row1064 = new OldStressRowData();
            row1064.LineNo = 6;
            row1064.MaterialId = 16930;
            row1064.SpecNo = "SA-240";
            row1064.TypeGrade = "321H";
            row1064.ProductForm = "Plate";
            row1064.AlloyUNS = "S32109";
            row1064.ClassCondition = "";
            row1064.Notes = "G18, H2, T8";
            row1064.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1064);

            // Row 7: SA-249, TP321H, Wld. tube
            var row1065 = new OldStressRowData();
            row1065.LineNo = 7;
            row1065.MaterialId = 9475;
            row1065.SpecNo = "SA-249";
            row1065.TypeGrade = "TP321H";
            row1065.ProductForm = "Wld. tube";
            row1065.AlloyUNS = "S32109";
            row1065.ClassCondition = "";
            row1065.Notes = "G18, T8, W13";
            row1065.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1065);

            // Row 8: SA-249, TP321H, Wld. tube
            var row1066 = new OldStressRowData();
            row1066.LineNo = 8;
            row1066.MaterialId = 9477;
            row1066.SpecNo = "SA-249";
            row1066.TypeGrade = "TP321H";
            row1066.ProductForm = "Wld. tube";
            row1066.AlloyUNS = "S32109";
            row1066.ClassCondition = "";
            row1066.Notes = "G5, G18, T8, W12, W13";
            row1066.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1066);

            // Row 9: SA-249, TP321H, Wld. tube
            var row1067 = new OldStressRowData();
            row1067.LineNo = 9;
            row1067.MaterialId = 9473;
            row1067.SpecNo = "SA-249";
            row1067.TypeGrade = "TP321H";
            row1067.ProductForm = "Wld. tube";
            row1067.AlloyUNS = "S32109";
            row1067.ClassCondition = "";
            row1067.Notes = "G3, G5, G18, G24, T8";
            row1067.StressValues = new double?[] { 17, null, 17, null, 16.2, 15.9, 15.9, 15.5, 15.2, 14.9, 14.6, 14.4, 14.2, 14.1, 13.9, 13.8, 10.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 0.94, null, null, null };
            batch.Add(row1067);

            // Row 10: SA-249, TP321H, Wld. tube
            var row1068 = new OldStressRowData();
            row1068.LineNo = 10;
            row1068.MaterialId = 9474;
            row1068.SpecNo = "SA-249";
            row1068.TypeGrade = "TP321H";
            row1068.ProductForm = "Wld. tube";
            row1068.AlloyUNS = "S32109";
            row1068.ClassCondition = "";
            row1068.Notes = "G3, G18, G24, T8";
            row1068.StressValues = new double?[] { 17, null, 15.3, null, 14.1, 13, 12.2, 11.5, 11.2, 11, 10.8, 10.7, 10.5, 10.4, 10.3, 10.2, 10.1, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 0.94, null, null, null };
            batch.Add(row1068);

            // Row 11: SA-312, TP321H, Smls. pipe
            var row1069 = new OldStressRowData();
            row1069.LineNo = 11;
            row1069.MaterialId = 9487;
            row1069.SpecNo = "SA-312";
            row1069.TypeGrade = "TP321H";
            row1069.ProductForm = "Smls. pipe";
            row1069.AlloyUNS = "S32109";
            row1069.ClassCondition = "";
            row1069.Notes = "G5, G18, T8, W12";
            row1069.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1069);

            // Row 12: SA-312, TP321H, Smls. pipe
            var row1070 = new OldStressRowData();
            row1070.LineNo = 12;
            row1070.MaterialId = 9488;
            row1070.SpecNo = "SA-312";
            row1070.TypeGrade = "TP321H";
            row1070.ProductForm = "Smls. pipe";
            row1070.AlloyUNS = "S32109";
            row1070.ClassCondition = "";
            row1070.Notes = "G18, T8";
            row1070.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1070);

            // Row 13: SA-312, TP321H, Wld. pipe
            var row1071 = new OldStressRowData();
            row1071.LineNo = 13;
            row1071.MaterialId = 9491;
            row1071.SpecNo = "SA-312";
            row1071.TypeGrade = "TP321H";
            row1071.ProductForm = "Wld. pipe";
            row1071.AlloyUNS = "S32109";
            row1071.ClassCondition = "";
            row1071.Notes = "G5, G18, T8, W13";
            row1071.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1071);

            // Row 14: SA-312, TP321H, Wld. pipe
            var row1072 = new OldStressRowData();
            row1072.LineNo = 14;
            row1072.MaterialId = 9492;
            row1072.SpecNo = "SA-312";
            row1072.TypeGrade = "TP321H";
            row1072.ProductForm = "Wld. pipe";
            row1072.AlloyUNS = "S32109";
            row1072.ClassCondition = "";
            row1072.Notes = "G18, T8, W13";
            row1072.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1072);

            // Row 15: SA-312, TP321H, Wld. pipe
            var row1073 = new OldStressRowData();
            row1073.LineNo = 15;
            row1073.MaterialId = 9493;
            row1073.SpecNo = "SA-312";
            row1073.TypeGrade = "TP321H";
            row1073.ProductForm = "Wld. pipe";
            row1073.AlloyUNS = "S32109";
            row1073.ClassCondition = "";
            row1073.Notes = "G3, G18, G24, T8";
            row1073.StressValues = new double?[] { 17, null, 15.3, null, 14.1, 13, 12.2, 11.5, 11.2, 11, 10.8, 10.7, 10.5, 10.4, 10.3, 10.2, 10.1, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 0.94, null, null, null };
            batch.Add(row1073);

            // Row 16: SA-312, TP321H, Wld. pipe
            var row1074 = new OldStressRowData();
            row1074.LineNo = 16;
            row1074.MaterialId = 9489;
            row1074.SpecNo = "SA-312";
            row1074.TypeGrade = "TP321H";
            row1074.ProductForm = "Wld. pipe";
            row1074.AlloyUNS = "S32109";
            row1074.ClassCondition = "";
            row1074.Notes = "G3, G5, G18, G24, T8";
            row1074.StressValues = new double?[] { 17, null, 17, null, 16.2, 15.9, 15.9, 15.5, 15.2, 14.9, 14.6, 14.4, 14.2, 14.1, 13.9, 13.8, 10.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 0.94, null, null, null };
            batch.Add(row1074);

            // Row 17: SA-376, TP321H, Smls. pipe
            var row1075 = new OldStressRowData();
            row1075.LineNo = 17;
            row1075.MaterialId = 9498;
            row1075.SpecNo = "SA-376";
            row1075.TypeGrade = "TP321H";
            row1075.ProductForm = "Smls. pipe";
            row1075.AlloyUNS = "S32109";
            row1075.ClassCondition = "";
            row1075.Notes = "G5, G18, H1, T8";
            row1075.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1075);

            // Row 18: SA-376, TP321H, Smls. pipe
            var row1076 = new OldStressRowData();
            row1076.LineNo = 18;
            row1076.MaterialId = 9499;
            row1076.SpecNo = "SA-376";
            row1076.TypeGrade = "TP321H";
            row1076.ProductForm = "Smls. pipe";
            row1076.AlloyUNS = "S32109";
            row1076.ClassCondition = "";
            row1076.Notes = "G18, H1, T8";
            row1076.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1076);

            // Row 19: SA-403, 321H, Smls. & wld. fittings
            var row1077 = new OldStressRowData();
            row1077.LineNo = 19;
            row1077.MaterialId = 9511;
            row1077.SpecNo = "SA-403";
            row1077.TypeGrade = "321H";
            row1077.ProductForm = "Smls. & wld. fittings";
            row1077.AlloyUNS = "S32109";
            row1077.ClassCondition = "";
            row1077.Notes = "G5, T8, W12, W14";
            row1077.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1077);

            // Row 20: SA-479, 321H, Bar
            var row1078 = new OldStressRowData();
            row1078.LineNo = 20;
            row1078.MaterialId = 9517;
            row1078.SpecNo = "SA-479";
            row1078.TypeGrade = "321H";
            row1078.ProductForm = "Bar";
            row1078.AlloyUNS = "S32109";
            row1078.ClassCondition = "";
            row1078.Notes = "G18, T8";
            row1078.StressValues = new double?[] { 20, null, 18, null, 16.5, 15.3, 14.3, 13.5, 13.2, 13, 12.7, 12.6, 12.4, 12.3, 12.1, 12, 11.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1078);

            // Row 21: SA-479, 321H, Bar
            var row1079 = new OldStressRowData();
            row1079.LineNo = 21;
            row1079.MaterialId = 9516;
            row1079.SpecNo = "SA-479";
            row1079.TypeGrade = "321H";
            row1079.ProductForm = "Bar";
            row1079.AlloyUNS = "S32109";
            row1079.ClassCondition = "";
            row1079.Notes = "G5, G18, T8";
            row1079.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, 16.7, 16.5, 16.4, 16.2, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1079);

            // Row 22: SA-813, TP321H, Wld. pipe
            var row1080 = new OldStressRowData();
            row1080.LineNo = 22;
            row1080.MaterialId = 9519;
            row1080.SpecNo = "SA-813";
            row1080.TypeGrade = "TP321H";
            row1080.ProductForm = "Wld. pipe";
            row1080.AlloyUNS = "S32109";
            row1080.ClassCondition = "";
            row1080.Notes = "G5, W12";
            row1080.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1080);

            // Row 23: SA-814, TP321H, Wld. pipe
            var row1081 = new OldStressRowData();
            row1081.LineNo = 23;
            row1081.MaterialId = 9521;
            row1081.SpecNo = "SA-814";
            row1081.TypeGrade = "TP321H";
            row1081.ProductForm = "Wld. pipe";
            row1081.AlloyUNS = "S32109";
            row1081.ClassCondition = "";
            row1081.Notes = "G5, W12";
            row1081.StressValues = new double?[] { 20, null, 20, null, 19.1, 18.7, 18.7, 18.3, 17.9, 17.5, 17.2, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1081);

            // Row 24: SA-240, 305, Plate
            var row1082 = new OldStressRowData();
            row1082.LineNo = 24;
            row1082.MaterialId = 9522;
            row1082.SpecNo = "SA-240";
            row1082.TypeGrade = "305";
            row1082.ProductForm = "Plate";
            row1082.AlloyUNS = "S30500";
            row1082.ClassCondition = "";
            row1082.Notes = "G5";
            row1082.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1082);

            // Row 25: SA-182, F317L, Forgings
            var row1083 = new OldStressRowData();
            row1083.LineNo = 25;
            row1083.MaterialId = 9787;
            row1083.SpecNo = "SA-182";
            row1083.TypeGrade = "F317L";
            row1083.ProductForm = "Forgings";
            row1083.AlloyUNS = "S31703";
            row1083.ClassCondition = "";
            row1083.Notes = "G5";
            row1083.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1083);

            // Row 26: SA-182, F317L, Forgings
            var row1084 = new OldStressRowData();
            row1084.LineNo = 26;
            row1084.MaterialId = 9788;
            row1084.SpecNo = "SA-182";
            row1084.TypeGrade = "F317L";
            row1084.ProductForm = "Forgings";
            row1084.AlloyUNS = "S31703";
            row1084.ClassCondition = "";
            row1084.Notes = "";
            row1084.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1084);

            // Row 27: SA-182, F317L, Forgings
            var row1085 = new OldStressRowData();
            row1085.LineNo = 27;
            row1085.MaterialId = 9789;
            row1085.SpecNo = "SA-182";
            row1085.TypeGrade = "F317L";
            row1085.ProductForm = "Forgings";
            row1085.AlloyUNS = "S31703";
            row1085.ClassCondition = "";
            row1085.Notes = "G5";
            row1085.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1085);

            // Row 28: SA-182, F317L, Forgings
            var row1086 = new OldStressRowData();
            row1086.LineNo = 28;
            row1086.MaterialId = 9790;
            row1086.SpecNo = "SA-182";
            row1086.TypeGrade = "F317L";
            row1086.ProductForm = "Forgings";
            row1086.AlloyUNS = "S31703";
            row1086.ClassCondition = "";
            row1086.Notes = "";
            row1086.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1086);

            // Row 29: SA-182, F317, Forgings
            var row1087 = new OldStressRowData();
            row1087.LineNo = 29;
            row1087.MaterialId = 9525;
            row1087.SpecNo = "SA-182";
            row1087.TypeGrade = "F317";
            row1087.ProductForm = "Forgings";
            row1087.AlloyUNS = "S31700";
            row1087.ClassCondition = "";
            row1087.Notes = "G5, G12, T8";
            row1087.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1087);

            // Row 30: SA-182, F317, Forgings
            var row1088 = new OldStressRowData();
            row1088.LineNo = 30;
            row1088.MaterialId = 9526;
            row1088.SpecNo = "SA-182";
            row1088.TypeGrade = "F317";
            row1088.ProductForm = "Forgings";
            row1088.AlloyUNS = "S31700";
            row1088.ClassCondition = "";
            row1088.Notes = "G12, T9";
            row1088.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1088);

            // Row 31: SA-240, 317, Plate
            var row1089 = new OldStressRowData();
            row1089.LineNo = 31;
            row1089.MaterialId = 9527;
            row1089.SpecNo = "SA-240";
            row1089.TypeGrade = "317";
            row1089.ProductForm = "Plate";
            row1089.AlloyUNS = "S31700";
            row1089.ClassCondition = "";
            row1089.Notes = "G5, G12, T8";
            row1089.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1089);

            // Row 32: SA-240, 317, Plate
            var row1090 = new OldStressRowData();
            row1090.LineNo = 32;
            row1090.MaterialId = 9528;
            row1090.SpecNo = "SA-240";
            row1090.TypeGrade = "317";
            row1090.ProductForm = "Plate";
            row1090.AlloyUNS = "S31700";
            row1090.ClassCondition = "";
            row1090.Notes = "G12, T9";
            row1090.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1090);

            // Row 33: SA-240, 317L, Plate
            var row1091 = new OldStressRowData();
            row1091.LineNo = 33;
            row1091.MaterialId = 9529;
            row1091.SpecNo = "SA-240";
            row1091.TypeGrade = "317L";
            row1091.ProductForm = "Plate";
            row1091.AlloyUNS = "S31703";
            row1091.ClassCondition = "";
            row1091.Notes = "G5";
            row1091.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, 17.7, 16.9, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1091);

            // Row 34: SA-240, 317L, Plate
            var row1092 = new OldStressRowData();
            row1092.LineNo = 34;
            row1092.MaterialId = 9530;
            row1092.SpecNo = "SA-240";
            row1092.TypeGrade = "317L";
            row1092.ProductForm = "Plate";
            row1092.AlloyUNS = "S31703";
            row1092.ClassCondition = "";
            row1092.Notes = "";
            row1092.StressValues = new double?[] { 20, null, 17, null, 15.2, 14, 13.1, 12.5, 12.2, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1092);

            // Row 35: SA-249, TP317, Wld. tube
            var row1093 = new OldStressRowData();
            row1093.LineNo = 35;
            row1093.MaterialId = 9523;
            row1093.SpecNo = "SA-249";
            row1093.TypeGrade = "TP317";
            row1093.ProductForm = "Wld. tube";
            row1093.AlloyUNS = "S31700";
            row1093.ClassCondition = "";
            row1093.Notes = "G5, W12";
            row1093.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1093);

            // Row 36: SA-249, TP317, Wld. tube
            var row1094 = new OldStressRowData();
            row1094.LineNo = 36;
            row1094.MaterialId = 9531;
            row1094.SpecNo = "SA-249";
            row1094.TypeGrade = "TP317";
            row1094.ProductForm = "Wld. tube";
            row1094.AlloyUNS = "S31700";
            row1094.ClassCondition = "";
            row1094.Notes = "G5, G12, G24, T8";
            row1094.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1094);

            // Row 37: SA-249, TP317, Wld. tube
            var row1095 = new OldStressRowData();
            row1095.LineNo = 37;
            row1095.MaterialId = 9532;
            row1095.SpecNo = "SA-249";
            row1095.TypeGrade = "TP317";
            row1095.ProductForm = "Wld. tube";
            row1095.AlloyUNS = "S31700";
            row1095.ClassCondition = "";
            row1095.Notes = "G12, G24, T9";
            row1095.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1095);

            // Row 38: SA-249, TP317L, Wld. tube
            var row1096 = new OldStressRowData();
            row1096.LineNo = 38;
            row1096.MaterialId = 9791;
            row1096.SpecNo = "SA-249";
            row1096.TypeGrade = "TP317L";
            row1096.ProductForm = "Wld. tube";
            row1096.AlloyUNS = "S31703";
            row1096.ClassCondition = "";
            row1096.Notes = "G5, G24";
            row1096.StressValues = new double?[] { 17, null, 17, null, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1096);

            // Row 39: SA-249, TP317L, Wld. tube
            var row1097 = new OldStressRowData();
            row1097.LineNo = 39;
            row1097.MaterialId = 9792;
            row1097.SpecNo = "SA-249";
            row1097.TypeGrade = "TP317L";
            row1097.ProductForm = "Wld. tube";
            row1097.AlloyUNS = "S31703";
            row1097.ClassCondition = "";
            row1097.Notes = "G24";
            row1097.StressValues = new double?[] { 17, null, 14.5, null, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1097);

            // Row 1: SA-312, TP317, Smls. & wld. pipe
            var row1098 = new OldStressRowData();
            row1098.LineNo = 1;
            row1098.MaterialId = 9536;
            row1098.SpecNo = "SA-312";
            row1098.TypeGrade = "TP317";
            row1098.ProductForm = "Smls. & wld. pipe";
            row1098.AlloyUNS = "S31700";
            row1098.ClassCondition = "";
            row1098.Notes = "G5, G12, T8, W12";
            row1098.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1098);

            // Row 2: SA-312, TP317, Smls. pipe
            var row1099 = new OldStressRowData();
            row1099.LineNo = 2;
            row1099.MaterialId = 9533;
            row1099.SpecNo = "SA-312";
            row1099.TypeGrade = "TP317";
            row1099.ProductForm = "Smls. pipe";
            row1099.AlloyUNS = "S31700";
            row1099.ClassCondition = "";
            row1099.Notes = "G12, T9";
            row1099.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1099);

            // Row 3: SA-312, TP317, Wld. pipe
            var row1100 = new OldStressRowData();
            row1100.LineNo = 3;
            row1100.MaterialId = 9534;
            row1100.SpecNo = "SA-312";
            row1100.TypeGrade = "TP317";
            row1100.ProductForm = "Wld. pipe";
            row1100.AlloyUNS = "S31700";
            row1100.ClassCondition = "";
            row1100.Notes = "G5, G12, G24, T8";
            row1100.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1100);

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
