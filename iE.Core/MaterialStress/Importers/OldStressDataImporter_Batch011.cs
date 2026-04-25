using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch011
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

            // Row 30: SA-479, 304LN, Bar
            var row1001 = new OldStressRowData();
            row1001.LineNo = 30;
            row1001.MaterialId = 9247;
            row1001.SpecNo = "SA-479";
            row1001.TypeGrade = "304LN";
            row1001.ProductForm = "Bar";
            row1001.AlloyUNS = "S30453";
            row1001.ClassCondition = "";
            row1001.Notes = "G5";
            row1001.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1001);

            // Row 31: SA-688, TP304LN, Wld. tube
            var row1002 = new OldStressRowData();
            row1002.LineNo = 31;
            row1002.MaterialId = 9248;
            row1002.SpecNo = "SA-688";
            row1002.TypeGrade = "TP304LN";
            row1002.ProductForm = "Wld. tube";
            row1002.AlloyUNS = "S30453";
            row1002.ClassCondition = "";
            row1002.Notes = "G5, W12";
            row1002.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1002);

            // Row 32: SA-813, TP304LN, Wld. pipe
            var row1003 = new OldStressRowData();
            row1003.LineNo = 32;
            row1003.MaterialId = 9249;
            row1003.SpecNo = "SA-813";
            row1003.TypeGrade = "TP304LN";
            row1003.ProductForm = "Wld. pipe";
            row1003.AlloyUNS = "S30453";
            row1003.ClassCondition = "";
            row1003.Notes = "G5, W12";
            row1003.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1003);

            // Row 33: SA-814, TP304LN, Wld. pipe
            var row1004 = new OldStressRowData();
            row1004.LineNo = 33;
            row1004.MaterialId = 9250;
            row1004.SpecNo = "SA-814";
            row1004.TypeGrade = "TP304LN";
            row1004.ProductForm = "Wld. pipe";
            row1004.AlloyUNS = "S30453";
            row1004.ClassCondition = "";
            row1004.Notes = "G5, W12";
            row1004.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1004);

            // Row 34: SA-430, FP304N, Forged pipe
            var row1005 = new OldStressRowData();
            row1005.LineNo = 34;
            row1005.MaterialId = 9251;
            row1005.SpecNo = "SA-430";
            row1005.TypeGrade = "FP304N";
            row1005.ProductForm = "Forged pipe";
            row1005.AlloyUNS = "S30451";
            row1005.ClassCondition = "";
            row1005.Notes = "G5, G12, G18, H1";
            row1005.StressValues = new double?[] { 18.8, null, 18.8, null, 17.8, 17.1, 16.7, 16.3, 16.2, 16.1, 15.9, 15.8, 15.6, 15.4, 15.1, 14.5, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1005);

            // Row 35: SA-430, FP304N, Forged pipe
            var row1006 = new OldStressRowData();
            row1006.LineNo = 35;
            row1006.MaterialId = 9252;
            row1006.SpecNo = "SA-430";
            row1006.TypeGrade = "FP304N";
            row1006.ProductForm = "Forged pipe";
            row1006.AlloyUNS = "S30451";
            row1006.ClassCondition = "";
            row1006.Notes = "G12, G18, H1";
            row1006.StressValues = new double?[] { 18.8, null, 18.8, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1006);

            // Row 36: SA-182, F304N, Forgings
            var row1007 = new OldStressRowData();
            row1007.LineNo = 36;
            row1007.MaterialId = 9253;
            row1007.SpecNo = "SA-182";
            row1007.TypeGrade = "F304N";
            row1007.ProductForm = "Forgings";
            row1007.AlloyUNS = "S30451";
            row1007.ClassCondition = "";
            row1007.Notes = "G5";
            row1007.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1007);

            // Row 37: SA-213, TP304N, Smls. tube
            var row1008 = new OldStressRowData();
            row1008.LineNo = 37;
            row1008.MaterialId = 9254;
            row1008.SpecNo = "SA-213";
            row1008.TypeGrade = "TP304N";
            row1008.ProductForm = "Smls. tube";
            row1008.AlloyUNS = "S30451";
            row1008.ClassCondition = "";
            row1008.Notes = "G5, G12, G18";
            row1008.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1008);

            // Row 38: SA-213, TP304N, Smls. tube
            var row1009 = new OldStressRowData();
            row1009.LineNo = 38;
            row1009.MaterialId = 9255;
            row1009.SpecNo = "SA-213";
            row1009.TypeGrade = "TP304N";
            row1009.ProductForm = "Smls. tube";
            row1009.AlloyUNS = "S30451";
            row1009.ClassCondition = "";
            row1009.Notes = "G12, G18";
            row1009.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1009);

            // Row 39: SA-240, 304N, Plate
            var row1010 = new OldStressRowData();
            row1010.LineNo = 39;
            row1010.MaterialId = 9256;
            row1010.SpecNo = "SA-240";
            row1010.TypeGrade = "304N";
            row1010.ProductForm = "Plate";
            row1010.AlloyUNS = "S30451";
            row1010.ClassCondition = "";
            row1010.Notes = "G5, G12";
            row1010.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1010);

            // Row 40: SA-240, 304N, Plate
            var row1011 = new OldStressRowData();
            row1011.LineNo = 40;
            row1011.MaterialId = 9257;
            row1011.SpecNo = "SA-240";
            row1011.TypeGrade = "304N";
            row1011.ProductForm = "Plate";
            row1011.AlloyUNS = "S30451";
            row1011.ClassCondition = "";
            row1011.Notes = "G12";
            row1011.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1011);

            // Row 1: SA-249, TP304N, Wld. tube
            var row1012 = new OldStressRowData();
            row1012.LineNo = 1;
            row1012.MaterialId = 9258;
            row1012.SpecNo = "SA-249";
            row1012.TypeGrade = "TP304N";
            row1012.ProductForm = "Wld. tube";
            row1012.AlloyUNS = "S30451";
            row1012.ClassCondition = "";
            row1012.Notes = "G5, G12, G18, W14";
            row1012.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1012);

            // Row 2: SA-249, TP304N, Wld. tube
            var row1013 = new OldStressRowData();
            row1013.LineNo = 2;
            row1013.MaterialId = 9261;
            row1013.SpecNo = "SA-249";
            row1013.TypeGrade = "TP304N";
            row1013.ProductForm = "Wld. tube";
            row1013.AlloyUNS = "S30451";
            row1013.ClassCondition = "";
            row1013.Notes = "G12, G18, W14";
            row1013.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1013);

            // Row 3: SA-249, TP304N, Wld. tube
            var row1014 = new OldStressRowData();
            row1014.LineNo = 3;
            row1014.MaterialId = 9259;
            row1014.SpecNo = "SA-249";
            row1014.TypeGrade = "TP304N";
            row1014.ProductForm = "Wld. tube";
            row1014.AlloyUNS = "S30451";
            row1014.ClassCondition = "";
            row1014.Notes = "G3, G5, G12, G18";
            row1014.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.6, 15.1, 14.8, 14.7, 14.6, 14.4, 14.1, 13.9, 13.5, 13.3, 12.7, 10.5, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1014);

            // Row 4: SA-249, TP304N, Wld. tube
            var row1015 = new OldStressRowData();
            row1015.LineNo = 4;
            row1015.MaterialId = 9262;
            row1015.SpecNo = "SA-249";
            row1015.TypeGrade = "TP304N";
            row1015.ProductForm = "Wld. tube";
            row1015.AlloyUNS = "S30451";
            row1015.ClassCondition = "";
            row1015.Notes = "G3, G12, G18, G24";
            row1015.StressValues = new double?[] { 17, null, 16.2, null, 14.2, 12.8, 11.8, 11.2, 11.1, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.5, 9.4, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1015);

            // Row 5: SA-249, TP304N, Wld. tube
            var row1016 = new OldStressRowData();
            row1016.LineNo = 5;
            row1016.MaterialId = 9260;
            row1016.SpecNo = "SA-249";
            row1016.TypeGrade = "TP304N";
            row1016.ProductForm = "Wld. tube";
            row1016.AlloyUNS = "S30451";
            row1016.ClassCondition = "";
            row1016.Notes = "G5, G12, G24";
            row1016.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.6, 15.1, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.5, 13.3, 12.7, 10.5, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1016);

            // Row 6: SA-312, TP304N, Wld. & smls. pipe
            var row1017 = new OldStressRowData();
            row1017.LineNo = 6;
            row1017.MaterialId = 9263;
            row1017.SpecNo = "SA-312";
            row1017.TypeGrade = "TP304N";
            row1017.ProductForm = "Wld. & smls. pipe";
            row1017.AlloyUNS = "S30451";
            row1017.ClassCondition = "";
            row1017.Notes = "G5, W12";
            row1017.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1017);

            // Row 7: SA-312, TP304N, Smls. pipe
            var row1018 = new OldStressRowData();
            row1018.LineNo = 7;
            row1018.MaterialId = 9265;
            row1018.SpecNo = "SA-312";
            row1018.TypeGrade = "TP304N";
            row1018.ProductForm = "Smls. pipe";
            row1018.AlloyUNS = "S30451";
            row1018.ClassCondition = "";
            row1018.Notes = "G5, G12, G18";
            row1018.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1018);

            // Row 8: SA-312, TP304N, Smls. pipe
            var row1019 = new OldStressRowData();
            row1019.LineNo = 8;
            row1019.MaterialId = 9266;
            row1019.SpecNo = "SA-312";
            row1019.TypeGrade = "TP304N";
            row1019.ProductForm = "Smls. pipe";
            row1019.AlloyUNS = "S30451";
            row1019.ClassCondition = "";
            row1019.Notes = "G12, G18";
            row1019.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1019);

            // Row 9: SA-312, TP304N, Wld. pipe
            var row1020 = new OldStressRowData();
            row1020.LineNo = 9;
            row1020.MaterialId = 9267;
            row1020.SpecNo = "SA-312";
            row1020.TypeGrade = "TP304N";
            row1020.ProductForm = "Wld. pipe";
            row1020.AlloyUNS = "S30451";
            row1020.ClassCondition = "";
            row1020.Notes = "G5, G12, G18, W14";
            row1020.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1020);

            // Row 10: SA-312, TP304N, Wld. pipe
            var row1021 = new OldStressRowData();
            row1021.LineNo = 10;
            row1021.MaterialId = 9270;
            row1021.SpecNo = "SA-312";
            row1021.TypeGrade = "TP304N";
            row1021.ProductForm = "Wld. pipe";
            row1021.AlloyUNS = "S30451";
            row1021.ClassCondition = "";
            row1021.Notes = "G12, G18, W14";
            row1021.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1021);

            // Row 11: SA-312, TP304N, Wld. pipe
            var row1022 = new OldStressRowData();
            row1022.LineNo = 11;
            row1022.MaterialId = 9268;
            row1022.SpecNo = "SA-312";
            row1022.TypeGrade = "TP304N";
            row1022.ProductForm = "Wld. pipe";
            row1022.AlloyUNS = "S30451";
            row1022.ClassCondition = "";
            row1022.Notes = "G3, G5, G12, G18";
            row1022.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.6, 15.1, 14.8, 14.7, 14.6, 14.4, 14.1, 13.9, 13.5, 13.3, 12.7, 10.5, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1022);

            // Row 12: SA-312, TP304N, Wld. pipe
            var row1023 = new OldStressRowData();
            row1023.LineNo = 12;
            row1023.MaterialId = 9264;
            row1023.SpecNo = "SA-312";
            row1023.TypeGrade = "TP304N";
            row1023.ProductForm = "Wld. pipe";
            row1023.AlloyUNS = "S30451";
            row1023.ClassCondition = "";
            row1023.Notes = "G3, G12, G18, G24";
            row1023.StressValues = new double?[] { 17, null, 16.2, null, 14.2, 12.8, 11.8, 11.2, 11.1, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.5, 9.4, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1023);

            // Row 13: SA-312, TP304N, Wld. pipe
            var row1024 = new OldStressRowData();
            row1024.LineNo = 13;
            row1024.MaterialId = 9269;
            row1024.SpecNo = "SA-312";
            row1024.TypeGrade = "TP304N";
            row1024.ProductForm = "Wld. pipe";
            row1024.AlloyUNS = "S30451";
            row1024.ClassCondition = "";
            row1024.Notes = "G5, G12, G24";
            row1024.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.6, 15.1, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.5, 13.3, 12.7, 10.5, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1024);

            // Row 14: SA-336, F304N, Forgings
            var row1025 = new OldStressRowData();
            row1025.LineNo = 14;
            row1025.MaterialId = 9272;
            row1025.SpecNo = "SA-336";
            row1025.TypeGrade = "F304N";
            row1025.ProductForm = "Forgings";
            row1025.AlloyUNS = "S30451";
            row1025.ClassCondition = "";
            row1025.Notes = "G5, G12";
            row1025.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1025);

            // Row 15: SA-336, F304N, Forgings
            var row1026 = new OldStressRowData();
            row1026.LineNo = 15;
            row1026.MaterialId = 9271;
            row1026.SpecNo = "SA-336";
            row1026.TypeGrade = "F304N";
            row1026.ProductForm = "Forgings";
            row1026.AlloyUNS = "S30451";
            row1026.ClassCondition = "";
            row1026.Notes = "G12";
            row1026.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1026);

            // Row 16: SA-358, 304N, Wld. pipe
            var row1027 = new OldStressRowData();
            row1027.LineNo = 16;
            row1027.MaterialId = 9273;
            row1027.SpecNo = "SA-358";
            row1027.TypeGrade = "304N";
            row1027.ProductForm = "Wld. pipe";
            row1027.AlloyUNS = "S30451";
            row1027.ClassCondition = "1";
            row1027.Notes = "G5, W12";
            row1027.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1027);

            // Row 17: SA-376, TP304N, Smls. pipe
            var row1028 = new OldStressRowData();
            row1028.LineNo = 17;
            row1028.MaterialId = 9274;
            row1028.SpecNo = "SA-376";
            row1028.TypeGrade = "TP304N";
            row1028.ProductForm = "Smls. pipe";
            row1028.AlloyUNS = "S30451";
            row1028.ClassCondition = "";
            row1028.Notes = "G5, G12, G18, H1";
            row1028.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1028);

            // Row 18: SA-376, TP304N, Smls. pipe
            var row1029 = new OldStressRowData();
            row1029.LineNo = 18;
            row1029.MaterialId = 9275;
            row1029.SpecNo = "SA-376";
            row1029.TypeGrade = "TP304N";
            row1029.ProductForm = "Smls. pipe";
            row1029.AlloyUNS = "S30451";
            row1029.ClassCondition = "";
            row1029.Notes = "G12, G18, H1";
            row1029.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1029);

            // Row 19: SA-403, 304N, Smls. & wld. fittings
            var row1030 = new OldStressRowData();
            row1030.LineNo = 19;
            row1030.MaterialId = 9277;
            row1030.SpecNo = "SA-403";
            row1030.TypeGrade = "304N";
            row1030.ProductForm = "Smls. & wld. fittings";
            row1030.AlloyUNS = "S30451";
            row1030.ClassCondition = "";
            row1030.Notes = "G5, W12, W15";
            row1030.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1030);

            // Row 20: ..., , 
            var row1031 = new OldStressRowData();
            row1031.LineNo = 20;
            row1031.MaterialId = 9276;
            row1031.SpecNo = "...";
            row1031.TypeGrade = "";
            row1031.ProductForm = "";
            row1031.AlloyUNS = "";
            row1031.ClassCondition = "";
            row1031.Notes = "";
            row1031.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1031);

            // Row 21: ..., , 
            var row1032 = new OldStressRowData();
            row1032.LineNo = 21;
            row1032.MaterialId = 9278;
            row1032.SpecNo = "...";
            row1032.TypeGrade = "";
            row1032.ProductForm = "";
            row1032.AlloyUNS = "";
            row1032.ClassCondition = "";
            row1032.Notes = "";
            row1032.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1032);

            // Row 22: ..., , 
            var row1033 = new OldStressRowData();
            row1033.LineNo = 22;
            row1033.MaterialId = 9279;
            row1033.SpecNo = "...";
            row1033.TypeGrade = "";
            row1033.ProductForm = "";
            row1033.AlloyUNS = "";
            row1033.ClassCondition = "";
            row1033.Notes = "";
            row1033.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1033);

            // Row 23: ..., , 
            var row1034 = new OldStressRowData();
            row1034.LineNo = 23;
            row1034.MaterialId = 9280;
            row1034.SpecNo = "...";
            row1034.TypeGrade = "";
            row1034.ProductForm = "";
            row1034.AlloyUNS = "";
            row1034.ClassCondition = "";
            row1034.Notes = "";
            row1034.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1034);

            // Row 24: SA-479, 304N, Bar
            var row1035 = new OldStressRowData();
            row1035.LineNo = 24;
            row1035.MaterialId = 9282;
            row1035.SpecNo = "SA-479";
            row1035.TypeGrade = "304N";
            row1035.ProductForm = "Bar";
            row1035.AlloyUNS = "S30451";
            row1035.ClassCondition = "";
            row1035.Notes = "G12, G18";
            row1035.StressValues = new double?[] { 20, null, 19.1, null, 16.7, 15, 13.9, 13.2, 13, 12.7, 12.5, 12.3, 12.1, 11.8, 11.5, 11.2, 11, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1035);

            // Row 25: SA-479, 304N, Bar
            var row1036 = new OldStressRowData();
            row1036.LineNo = 25;
            row1036.MaterialId = 9281;
            row1036.SpecNo = "SA-479";
            row1036.TypeGrade = "304N";
            row1036.ProductForm = "Bar";
            row1036.AlloyUNS = "S30451";
            row1036.ClassCondition = "";
            row1036.Notes = "G5, G12, G18";
            row1036.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, 16.3, 15.9, 15.6, 15, 12.4, 9.8, 7.7, 6.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1036);

            // Row 26: SA-688, TP304N, Wld. tube
            var row1037 = new OldStressRowData();
            row1037.LineNo = 26;
            row1037.MaterialId = 9283;
            row1037.SpecNo = "SA-688";
            row1037.TypeGrade = "TP304N";
            row1037.ProductForm = "Wld. tube";
            row1037.AlloyUNS = "S30451";
            row1037.ClassCondition = "";
            row1037.Notes = "G5, W12";
            row1037.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1037);

            // Row 27: SA-688, TP304N, Wld. tube
            var row1038 = new OldStressRowData();
            row1038.LineNo = 27;
            row1038.MaterialId = 9284;
            row1038.SpecNo = "SA-688";
            row1038.TypeGrade = "TP304N";
            row1038.ProductForm = "Wld. tube";
            row1038.AlloyUNS = "S30451";
            row1038.ClassCondition = "";
            row1038.Notes = "G5, G12, G24";
            row1038.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.6, 15.1, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 13.5, 13.3, 12.7, 10.5, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1038);

            // Row 28: SA-688, TP304N, Wld. tube
            var row1039 = new OldStressRowData();
            row1039.LineNo = 28;
            row1039.MaterialId = 9285;
            row1039.SpecNo = "SA-688";
            row1039.TypeGrade = "TP304N";
            row1039.ProductForm = "Wld. tube";
            row1039.AlloyUNS = "S30451";
            row1039.ClassCondition = "";
            row1039.Notes = "G12, G24";
            row1039.StressValues = new double?[] { 17, null, 16.2, null, 14.2, 12.8, 11.8, 11.2, 11.1, 10.8, 10.6, 10.5, 10.3, 10, 9.8, 9.5, 9.4, 8.3, 6.5, 5.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row1039);

            // Row 29: SA-813, TP304N, Wld. pipe
            var row1040 = new OldStressRowData();
            row1040.LineNo = 29;
            row1040.MaterialId = 9286;
            row1040.SpecNo = "SA-813";
            row1040.TypeGrade = "TP304N";
            row1040.ProductForm = "Wld. pipe";
            row1040.AlloyUNS = "S30451";
            row1040.ClassCondition = "";
            row1040.Notes = "G5, W12";
            row1040.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1040);

            // Row 30: SA-814, TP304N, Wld. pipe
            var row1041 = new OldStressRowData();
            row1041.LineNo = 30;
            row1041.MaterialId = 9287;
            row1041.SpecNo = "SA-814";
            row1041.TypeGrade = "TP304N";
            row1041.ProductForm = "Wld. pipe";
            row1041.AlloyUNS = "S30451";
            row1041.ClassCondition = "";
            row1041.Notes = "G5, W12";
            row1041.StressValues = new double?[] { 20, null, 20, null, 19, 18.3, 17.8, 17.4, 17.3, 17.1, 16.9, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1041);

            // Row 31: SA-479, , Bar
            var row1042 = new OldStressRowData();
            row1042.LineNo = 31;
            row1042.MaterialId = 9288;
            row1042.SpecNo = "SA-479";
            row1042.TypeGrade = "";
            row1042.ProductForm = "Bar";
            row1042.AlloyUNS = "S21800";
            row1042.ClassCondition = "";
            row1042.Notes = "";
            row1042.StressValues = new double?[] { 23.8, null, 23.4, null, 20.7, 18.5, 17.3, 16.5, 16.3, 16, 15.9, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1042);

            // Row 32: SA-336, F348H, Forgings
            var row1043 = new OldStressRowData();
            row1043.LineNo = 32;
            row1043.MaterialId = 9289;
            row1043.SpecNo = "SA-336";
            row1043.TypeGrade = "F348H";
            row1043.ProductForm = "Forgings";
            row1043.AlloyUNS = "S34809";
            row1043.ClassCondition = "";
            row1043.Notes = "G5, H2";
            row1043.StressValues = new double?[] { 16.3, null, 15.6, null, 14.3, 13.4, 13, 12.9, 12.8, 12.7, 12.7, 12.7, 12.7, 12.7, 12.6, 12.5, 12.1, 11, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1043);

            // Row 33: SA-336, F348H, Forgings
            var row1044 = new OldStressRowData();
            row1044.LineNo = 33;
            row1044.MaterialId = 9290;
            row1044.SpecNo = "SA-336";
            row1044.TypeGrade = "F348H";
            row1044.ProductForm = "Forgings";
            row1044.AlloyUNS = "S34809";
            row1044.ClassCondition = "";
            row1044.Notes = "H2";
            row1044.StressValues = new double?[] { 16.3, null, 15.3, null, 14.3, 13.3, 12.5, 11.9, 11.7, 11.5, 11.4, 11.3, 11.3, 11.2, 11.2, 11.2, 11.1, 11, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1044);

            // Row 34: SA-351, CF8C, Castings
            var row1045 = new OldStressRowData();
            row1045.LineNo = 34;
            row1045.MaterialId = 9310;
            row1045.SpecNo = "SA-351";
            row1045.TypeGrade = "CF8C";
            row1045.ProductForm = "Castings";
            row1045.AlloyUNS = "J92710";
            row1045.ClassCondition = "";
            row1045.Notes = "G5, G16, G17, G32";
            row1045.StressValues = new double?[] { 17.5, null, 16.7, null, 15.9, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1045);

            // Row 35: SA-351, CF8C, Castings
            var row1046 = new OldStressRowData();
            row1046.LineNo = 35;
            row1046.MaterialId = 9311;
            row1046.SpecNo = "SA-351";
            row1046.TypeGrade = "CF8C";
            row1046.ProductForm = "Castings";
            row1046.AlloyUNS = "J92710";
            row1046.ClassCondition = "";
            row1046.Notes = "G1, G5, G12, G32";
            row1046.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1046);

            // Row 36: SA-351, CF8C, Castings
            var row1047 = new OldStressRowData();
            row1047.LineNo = 36;
            row1047.MaterialId = 9312;
            row1047.SpecNo = "SA-351";
            row1047.TypeGrade = "CF8C";
            row1047.ProductForm = "Castings";
            row1047.AlloyUNS = "J92710";
            row1047.ClassCondition = "";
            row1047.Notes = "G1, G12, G32";
            row1047.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1047);

            // Row 37: SA-451, CPF8C, Cast pipe
            var row1048 = new OldStressRowData();
            row1048.LineNo = 37;
            row1048.MaterialId = 9317;
            row1048.SpecNo = "SA-451";
            row1048.TypeGrade = "CPF8C";
            row1048.ProductForm = "Cast pipe";
            row1048.AlloyUNS = "J92710";
            row1048.ClassCondition = "";
            row1048.Notes = "G5, G16, G17, G32";
            row1048.StressValues = new double?[] { 17.5, null, 16.7, null, 15.9, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1048);

            // Row 1: SA-182, F347, Forgings
            var row1049 = new OldStressRowData();
            row1049.LineNo = 1;
            row1049.MaterialId = 9292;
            row1049.SpecNo = "SA-182";
            row1049.TypeGrade = "F347";
            row1049.ProductForm = "Forgings";
            row1049.AlloyUNS = "S34700";
            row1049.ClassCondition = "";
            row1049.Notes = "G5, G12, G18, H2";
            row1049.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1049);

            // Row 2: SA-182, F347, Forgings
            var row1050 = new OldStressRowData();
            row1050.LineNo = 2;
            row1050.MaterialId = 9291;
            row1050.SpecNo = "SA-182";
            row1050.TypeGrade = "F347";
            row1050.ProductForm = "Forgings";
            row1050.AlloyUNS = "S34700";
            row1050.ClassCondition = "";
            row1050.Notes = "G5, G12, G18, H2";
            row1050.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1050);

            // Row 3: SA-336, F347, Forgings
            var row1051 = new OldStressRowData();
            row1051.LineNo = 3;
            row1051.MaterialId = 9304;
            row1051.SpecNo = "SA-336";
            row1051.TypeGrade = "F347";
            row1051.ProductForm = "Forgings";
            row1051.AlloyUNS = "S34700";
            row1051.ClassCondition = "";
            row1051.Notes = "G5, G12, G18, H2";
            row1051.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1051);

            // Row 4: SA-336, F347, Forgings
            var row1052 = new OldStressRowData();
            row1052.LineNo = 4;
            row1052.MaterialId = 9305;
            row1052.SpecNo = "SA-336";
            row1052.TypeGrade = "F347";
            row1052.ProductForm = "Forgings";
            row1052.AlloyUNS = "S34700";
            row1052.ClassCondition = "";
            row1052.Notes = "G12, G18, H2";
            row1052.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1052);

            // Row 5: SA-430, FP347, Forged pipe
            var row1053 = new OldStressRowData();
            row1053.LineNo = 5;
            row1053.MaterialId = 9314;
            row1053.SpecNo = "SA-430";
            row1053.TypeGrade = "FP347";
            row1053.ProductForm = "Forged pipe";
            row1053.AlloyUNS = "S34700";
            row1053.ClassCondition = "";
            row1053.Notes = "G5, G12, G18, H1, H2";
            row1053.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1053);

            // Row 6: SA-430, FP347, Forged pipe
            var row1054 = new OldStressRowData();
            row1054.LineNo = 6;
            row1054.MaterialId = 9313;
            row1054.SpecNo = "SA-430";
            row1054.TypeGrade = "FP347";
            row1054.ProductForm = "Forged pipe";
            row1054.AlloyUNS = "S34700";
            row1054.ClassCondition = "";
            row1054.Notes = "G5, G12, G18, H1, H2";
            row1054.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1054);

            // Row 7: SA-182, F347H, Forgings
            var row1055 = new OldStressRowData();
            row1055.LineNo = 7;
            row1055.MaterialId = 9293;
            row1055.SpecNo = "SA-182";
            row1055.TypeGrade = "F347H";
            row1055.ProductForm = "Forgings";
            row1055.AlloyUNS = "S34709";
            row1055.ClassCondition = "";
            row1055.Notes = "G5, G18";
            row1055.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1055);

            // Row 8: SA-182, F347H, Forgings
            var row1056 = new OldStressRowData();
            row1056.LineNo = 8;
            row1056.MaterialId = 9294;
            row1056.SpecNo = "SA-182";
            row1056.TypeGrade = "F347H";
            row1056.ProductForm = "Forgings";
            row1056.AlloyUNS = "S34709";
            row1056.ClassCondition = "";
            row1056.Notes = "G18";
            row1056.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1056);

            // Row 9: SA-182, F347H, Forgings
            var row1057 = new OldStressRowData();
            row1057.LineNo = 9;
            row1057.MaterialId = 9295;
            row1057.SpecNo = "SA-182";
            row1057.TypeGrade = "F347H";
            row1057.ProductForm = "Forgings";
            row1057.AlloyUNS = "S34709";
            row1057.ClassCondition = "";
            row1057.Notes = "G5, H2";
            row1057.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1057);

            // Row 10: SA-182, F347H, Forgings
            var row1058 = new OldStressRowData();
            row1058.LineNo = 10;
            row1058.MaterialId = 9296;
            row1058.SpecNo = "SA-182";
            row1058.TypeGrade = "F347H";
            row1058.ProductForm = "Forgings";
            row1058.AlloyUNS = "S34709";
            row1058.ClassCondition = "";
            row1058.Notes = "G5, H2";
            row1058.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1058);

            // Row 11: SA-336, F347H, Forgings
            var row1059 = new OldStressRowData();
            row1059.LineNo = 11;
            row1059.MaterialId = 9306;
            row1059.SpecNo = "SA-336";
            row1059.TypeGrade = "F347H";
            row1059.ProductForm = "Forgings";
            row1059.AlloyUNS = "S34709";
            row1059.ClassCondition = "";
            row1059.Notes = "G5, H2";
            row1059.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1059);

            // Row 12: SA-336, F347H, Forgings
            var row1060 = new OldStressRowData();
            row1060.LineNo = 12;
            row1060.MaterialId = 9307;
            row1060.SpecNo = "SA-336";
            row1060.TypeGrade = "F347H";
            row1060.ProductForm = "Forgings";
            row1060.AlloyUNS = "S34709";
            row1060.ClassCondition = "";
            row1060.Notes = "G5, H2";
            row1060.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1060);

            // Row 13: SA-430, FP347H, Forged pipe
            var row1061 = new OldStressRowData();
            row1061.LineNo = 13;
            row1061.MaterialId = 9316;
            row1061.SpecNo = "SA-430";
            row1061.TypeGrade = "FP347H";
            row1061.ProductForm = "Forged pipe";
            row1061.AlloyUNS = "S34709";
            row1061.ClassCondition = "";
            row1061.Notes = "G5, G18, H1, H2";
            row1061.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1061);

            // Row 14: SA-430, FP347H, Forged pipe
            var row1062 = new OldStressRowData();
            row1062.LineNo = 14;
            row1062.MaterialId = 9315;
            row1062.SpecNo = "SA-430";
            row1062.TypeGrade = "FP347H";
            row1062.ProductForm = "Forged pipe";
            row1062.AlloyUNS = "S34709";
            row1062.ClassCondition = "";
            row1062.Notes = "G5, G18, H1, H2";
            row1062.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1062);

            // Row 15: SA-182, F348, Forgings
            var row1063 = new OldStressRowData();
            row1063.LineNo = 15;
            row1063.MaterialId = 9299;
            row1063.SpecNo = "SA-182";
            row1063.TypeGrade = "F348";
            row1063.ProductForm = "Forgings";
            row1063.AlloyUNS = "S34800";
            row1063.ClassCondition = "";
            row1063.Notes = "G18";
            row1063.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1063);

            // Row 16: SA-182, F348, Forgings
            var row1064 = new OldStressRowData();
            row1064.LineNo = 16;
            row1064.MaterialId = 9298;
            row1064.SpecNo = "SA-182";
            row1064.TypeGrade = "F348";
            row1064.ProductForm = "Forgings";
            row1064.AlloyUNS = "S34800";
            row1064.ClassCondition = "";
            row1064.Notes = "G5, G12, G18, H2";
            row1064.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1064);

            // Row 17: SA-182, F348, Forgings
            var row1065 = new OldStressRowData();
            row1065.LineNo = 17;
            row1065.MaterialId = 9297;
            row1065.SpecNo = "SA-182";
            row1065.TypeGrade = "F348";
            row1065.ProductForm = "Forgings";
            row1065.AlloyUNS = "S34800";
            row1065.ClassCondition = "";
            row1065.Notes = "G5, G12, H2";
            row1065.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.4, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1065);

            // Row 18: SA-336, F348, Forgings
            var row1066 = new OldStressRowData();
            row1066.LineNo = 18;
            row1066.MaterialId = 9308;
            row1066.SpecNo = "SA-336";
            row1066.TypeGrade = "F348";
            row1066.ProductForm = "Forgings";
            row1066.AlloyUNS = "S34800";
            row1066.ClassCondition = "";
            row1066.Notes = "G5, G12, H2";
            row1066.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1066);

            // Row 19: SA-336, F348, Forgings
            var row1067 = new OldStressRowData();
            row1067.LineNo = 19;
            row1067.MaterialId = 9309;
            row1067.SpecNo = "SA-336";
            row1067.TypeGrade = "F348";
            row1067.ProductForm = "Forgings";
            row1067.AlloyUNS = "S34800";
            row1067.ClassCondition = "";
            row1067.Notes = "G12, H2";
            row1067.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1067);

            // Row 20: SA-182, F348H, Forgings
            var row1068 = new OldStressRowData();
            row1068.LineNo = 20;
            row1068.MaterialId = 9300;
            row1068.SpecNo = "SA-182";
            row1068.TypeGrade = "F348H";
            row1068.ProductForm = "Forgings";
            row1068.AlloyUNS = "S34809";
            row1068.ClassCondition = "";
            row1068.Notes = "G5, G18";
            row1068.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1068);

            // Row 21: SA-182, F348H, Forgings
            var row1069 = new OldStressRowData();
            row1069.LineNo = 21;
            row1069.MaterialId = 9301;
            row1069.SpecNo = "SA-182";
            row1069.TypeGrade = "F348H";
            row1069.ProductForm = "Forgings";
            row1069.AlloyUNS = "S34809";
            row1069.ClassCondition = "";
            row1069.Notes = "G18";
            row1069.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1069);

            // Row 22: SA-182, F348H, Forgings
            var row1070 = new OldStressRowData();
            row1070.LineNo = 22;
            row1070.MaterialId = 9302;
            row1070.SpecNo = "SA-182";
            row1070.TypeGrade = "F348H";
            row1070.ProductForm = "Forgings";
            row1070.AlloyUNS = "S34809";
            row1070.ClassCondition = "";
            row1070.Notes = "G5, H2";
            row1070.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.6, 13.5, 13.5, 13.4, 13.5, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1070);

            // Row 23: SA-182, F348H, Forgings
            var row1071 = new OldStressRowData();
            row1071.LineNo = 23;
            row1071.MaterialId = 9303;
            row1071.SpecNo = "SA-182";
            row1071.TypeGrade = "F348H";
            row1071.ProductForm = "Forgings";
            row1071.AlloyUNS = "S34809";
            row1071.ClassCondition = "";
            row1071.Notes = "G5, H2";
            row1071.StressValues = new double?[] { 17.5, null, 16.7, null, 15.4, 14.5, 14, 13.9, 13.8, 13.7, 13.7, 13.7, 13.7, 13.7, 13.6, 13.4, 13.2, 12.9, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1071);

            // Row 24: SA-182, F347, Forgings
            var row1072 = new OldStressRowData();
            row1072.LineNo = 24;
            row1072.MaterialId = 9318;
            row1072.SpecNo = "SA-182";
            row1072.TypeGrade = "F347";
            row1072.ProductForm = "Forgings";
            row1072.AlloyUNS = "S34700";
            row1072.ClassCondition = "";
            row1072.Notes = "G5, G12, G18, H2";
            row1072.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1072);

            // Row 25: SA-182, F347, Forgings
            var row1073 = new OldStressRowData();
            row1073.LineNo = 25;
            row1073.MaterialId = 9319;
            row1073.SpecNo = "SA-182";
            row1073.TypeGrade = "F347";
            row1073.ProductForm = "Forgings";
            row1073.AlloyUNS = "S34700";
            row1073.ClassCondition = "";
            row1073.Notes = "G12, G18, H2";
            row1073.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1073);

            // Row 26: SA-213, TP347, Smls. tube
            var row1074 = new OldStressRowData();
            row1074.LineNo = 26;
            row1074.MaterialId = 9326;
            row1074.SpecNo = "SA-213";
            row1074.TypeGrade = "TP347";
            row1074.ProductForm = "Smls. tube";
            row1074.AlloyUNS = "S34700";
            row1074.ClassCondition = "";
            row1074.Notes = "G5, G12, G18, H2";
            row1074.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1074);

            // Row 27: SA-213, TP347, Smls. tube
            var row1075 = new OldStressRowData();
            row1075.LineNo = 27;
            row1075.MaterialId = 9327;
            row1075.SpecNo = "SA-213";
            row1075.TypeGrade = "TP347";
            row1075.ProductForm = "Smls. tube";
            row1075.AlloyUNS = "S34700";
            row1075.ClassCondition = "";
            row1075.Notes = "G12, G18, H2";
            row1075.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1075);

            // Row 28: SA-240, 347, Plate
            var row1076 = new OldStressRowData();
            row1076.LineNo = 28;
            row1076.MaterialId = 9335;
            row1076.SpecNo = "SA-240";
            row1076.TypeGrade = "347";
            row1076.ProductForm = "Plate";
            row1076.AlloyUNS = "S34700";
            row1076.ClassCondition = "";
            row1076.Notes = "G5, G12, G18";
            row1076.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1076);

            // Row 29: SA-240, 347, Plate
            var row1077 = new OldStressRowData();
            row1077.LineNo = 29;
            row1077.MaterialId = 9334;
            row1077.SpecNo = "SA-240";
            row1077.TypeGrade = "347";
            row1077.ProductForm = "Plate";
            row1077.AlloyUNS = "S34700";
            row1077.ClassCondition = "";
            row1077.Notes = "G12, G18";
            row1077.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1077);

            // Row 30: SA-249, TP347, Wld. tube
            var row1078 = new OldStressRowData();
            row1078.LineNo = 30;
            row1078.MaterialId = 9343;
            row1078.SpecNo = "SA-249";
            row1078.TypeGrade = "TP347";
            row1078.ProductForm = "Wld. tube";
            row1078.AlloyUNS = "S34700";
            row1078.ClassCondition = "";
            row1078.Notes = "G12, G18, W14";
            row1078.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1078);

            // Row 31: SA-249, TP347, Wld. tube
            var row1079 = new OldStressRowData();
            row1079.LineNo = 31;
            row1079.MaterialId = 9340;
            row1079.SpecNo = "SA-249";
            row1079.TypeGrade = "TP347";
            row1079.ProductForm = "Wld. tube";
            row1079.AlloyUNS = "S34700";
            row1079.ClassCondition = "";
            row1079.Notes = "G5, G12, G18, W12, W14";
            row1079.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1079);

            // Row 32: SA-249, TP347, Wld. tube
            var row1080 = new OldStressRowData();
            row1080.LineNo = 32;
            row1080.MaterialId = 9341;
            row1080.SpecNo = "SA-249";
            row1080.TypeGrade = "TP347";
            row1080.ProductForm = "Wld. tube";
            row1080.AlloyUNS = "S34700";
            row1080.ClassCondition = "";
            row1080.Notes = "G3, G5, G12, G18, G24";
            row1080.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1080);

            // Row 33: SA-249, TP347, Wld. tube
            var row1081 = new OldStressRowData();
            row1081.LineNo = 33;
            row1081.MaterialId = 9342;
            row1081.SpecNo = "SA-249";
            row1081.TypeGrade = "TP347";
            row1081.ProductForm = "Wld. tube";
            row1081.AlloyUNS = "S34700";
            row1081.ClassCondition = "";
            row1081.Notes = "G3, G12, G18, G24";
            row1081.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1081);

            // Row 34: SA-312, TP347, Wld. & smls. pipe
            var row1082 = new OldStressRowData();
            row1082.LineNo = 34;
            row1082.MaterialId = 9358;
            row1082.SpecNo = "SA-312";
            row1082.TypeGrade = "TP347";
            row1082.ProductForm = "Wld. & smls. pipe";
            row1082.AlloyUNS = "S34700";
            row1082.ClassCondition = "";
            row1082.Notes = "G5, G12, H2, W12";
            row1082.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1082);

            // Row 35: SA-312, TP347, Smls. pipe
            var row1083 = new OldStressRowData();
            row1083.LineNo = 35;
            row1083.MaterialId = 9362;
            row1083.SpecNo = "SA-312";
            row1083.TypeGrade = "TP347";
            row1083.ProductForm = "Smls. pipe";
            row1083.AlloyUNS = "S34700";
            row1083.ClassCondition = "";
            row1083.Notes = "G5, G12, G18";
            row1083.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1083);

            // Row 36: SA-312, TP347, Smls. pipe
            var row1084 = new OldStressRowData();
            row1084.LineNo = 36;
            row1084.MaterialId = 9359;
            row1084.SpecNo = "SA-312";
            row1084.TypeGrade = "TP347";
            row1084.ProductForm = "Smls. pipe";
            row1084.AlloyUNS = "S34700";
            row1084.ClassCondition = "";
            row1084.Notes = "G12, G18, H2";
            row1084.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1084);

            // Row 37: SA-312, TP347, Wld. pipe
            var row1085 = new OldStressRowData();
            row1085.LineNo = 37;
            row1085.MaterialId = 9363;
            row1085.SpecNo = "SA-312";
            row1085.TypeGrade = "TP347";
            row1085.ProductForm = "Wld. pipe";
            row1085.AlloyUNS = "S34700";
            row1085.ClassCondition = "";
            row1085.Notes = "G5, G12, G18, W14";
            row1085.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 14.9, 14.7, 14.7, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1085);

            // Row 38: SA-312, TP347, Wld. pipe
            var row1086 = new OldStressRowData();
            row1086.LineNo = 38;
            row1086.MaterialId = 9364;
            row1086.SpecNo = "SA-312";
            row1086.TypeGrade = "TP347";
            row1086.ProductForm = "Wld. pipe";
            row1086.AlloyUNS = "S34700";
            row1086.ClassCondition = "";
            row1086.Notes = "G12, G18, W14";
            row1086.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1086);

            // Row 39: SA-312, TP347, Wld. pipe
            var row1087 = new OldStressRowData();
            row1087.LineNo = 39;
            row1087.MaterialId = 9360;
            row1087.SpecNo = "SA-312";
            row1087.TypeGrade = "TP347";
            row1087.ProductForm = "Wld. pipe";
            row1087.AlloyUNS = "S34700";
            row1087.ClassCondition = "";
            row1087.Notes = "G3, G5, G12, G18, G24, H2";
            row1087.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1087);

            // Row 40: SA-312, TP347, Wld. pipe
            var row1088 = new OldStressRowData();
            row1088.LineNo = 40;
            row1088.MaterialId = 9361;
            row1088.SpecNo = "SA-312";
            row1088.TypeGrade = "TP347";
            row1088.ProductForm = "Wld. pipe";
            row1088.AlloyUNS = "S34700";
            row1088.ClassCondition = "";
            row1088.Notes = "G3, G12, G18, G24, H2";
            row1088.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1088);

            // Row 1: SA-358, 347, Wld. pipe
            var row1089 = new OldStressRowData();
            row1089.LineNo = 1;
            row1089.MaterialId = 9381;
            row1089.SpecNo = "SA-358";
            row1089.TypeGrade = "347";
            row1089.ProductForm = "Wld. pipe";
            row1089.AlloyUNS = "S34700";
            row1089.ClassCondition = "1";
            row1089.Notes = "G5, W12";
            row1089.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1089);

            // Row 2: SA-376, TP347, Smls. pipe
            var row1090 = new OldStressRowData();
            row1090.LineNo = 2;
            row1090.MaterialId = 9383;
            row1090.SpecNo = "SA-376";
            row1090.TypeGrade = "TP347";
            row1090.ProductForm = "Smls. pipe";
            row1090.AlloyUNS = "S34700";
            row1090.ClassCondition = "";
            row1090.Notes = "G5, G12, G18, H1, H2";
            row1090.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1090);

            // Row 3: SA-376, TP347, Smls. pipe
            var row1091 = new OldStressRowData();
            row1091.LineNo = 3;
            row1091.MaterialId = 9384;
            row1091.SpecNo = "SA-376";
            row1091.TypeGrade = "TP347";
            row1091.ProductForm = "Smls. pipe";
            row1091.AlloyUNS = "S34700";
            row1091.ClassCondition = "";
            row1091.Notes = "G12, G18, H1, H2";
            row1091.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1091);

            // Row 4: SA-403, 347, Smls. & wld. fittings
            var row1092 = new OldStressRowData();
            row1092.LineNo = 4;
            row1092.MaterialId = 9389;
            row1092.SpecNo = "SA-403";
            row1092.TypeGrade = "347";
            row1092.ProductForm = "Smls. & wld. fittings";
            row1092.AlloyUNS = "S34700";
            row1092.ClassCondition = "";
            row1092.Notes = "G5, G12, W12, W15";
            row1092.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1092);

            // Row 5: ..., , 
            var row1093 = new OldStressRowData();
            row1093.LineNo = 5;
            row1093.MaterialId = 9391;
            row1093.SpecNo = "...";
            row1093.TypeGrade = "";
            row1093.ProductForm = "";
            row1093.AlloyUNS = "";
            row1093.ClassCondition = "";
            row1093.Notes = "";
            row1093.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1093);

            // Row 6: ..., , 
            var row1094 = new OldStressRowData();
            row1094.LineNo = 6;
            row1094.MaterialId = 9390;
            row1094.SpecNo = "...";
            row1094.TypeGrade = "";
            row1094.ProductForm = "";
            row1094.AlloyUNS = "";
            row1094.ClassCondition = "";
            row1094.Notes = "";
            row1094.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1094);

            // Row 7: ..., , 
            var row1095 = new OldStressRowData();
            row1095.LineNo = 7;
            row1095.MaterialId = 9392;
            row1095.SpecNo = "...";
            row1095.TypeGrade = "";
            row1095.ProductForm = "";
            row1095.AlloyUNS = "";
            row1095.ClassCondition = "";
            row1095.Notes = "";
            row1095.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1095);

            // Row 8: ..., , 
            var row1096 = new OldStressRowData();
            row1096.LineNo = 8;
            row1096.MaterialId = 9393;
            row1096.SpecNo = "...";
            row1096.TypeGrade = "";
            row1096.ProductForm = "";
            row1096.AlloyUNS = "";
            row1096.ClassCondition = "";
            row1096.Notes = "";
            row1096.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1096);

            // Row 9: SA-409, TP347, Wld. pipe
            var row1097 = new OldStressRowData();
            row1097.LineNo = 9;
            row1097.MaterialId = 9413;
            row1097.SpecNo = "SA-409";
            row1097.TypeGrade = "TP347";
            row1097.ProductForm = "Wld. pipe";
            row1097.AlloyUNS = "S34700";
            row1097.ClassCondition = "";
            row1097.Notes = "G5, W12";
            row1097.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1097);

            // Row 10: SA-479, 347, Bar
            var row1098 = new OldStressRowData();
            row1098.LineNo = 10;
            row1098.MaterialId = 9417;
            row1098.SpecNo = "SA-479";
            row1098.TypeGrade = "347";
            row1098.ProductForm = "Bar";
            row1098.AlloyUNS = "S34700";
            row1098.ClassCondition = "";
            row1098.Notes = "G5, G12, G18, G22, H2";
            row1098.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1098);

            // Row 11: SA-479, 347, Bar
            var row1099 = new OldStressRowData();
            row1099.LineNo = 11;
            row1099.MaterialId = 9418;
            row1099.SpecNo = "SA-479";
            row1099.TypeGrade = "347";
            row1099.ProductForm = "Bar";
            row1099.AlloyUNS = "S34700";
            row1099.ClassCondition = "";
            row1099.Notes = "G12, G18, G22, H2";
            row1099.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1099);

            // Row 12: SA-813, TP347, Wld. pipe
            var row1100 = new OldStressRowData();
            row1100.LineNo = 12;
            row1100.MaterialId = 9425;
            row1100.SpecNo = "SA-813";
            row1100.TypeGrade = "TP347";
            row1100.ProductForm = "Wld. pipe";
            row1100.AlloyUNS = "S34700";
            row1100.ClassCondition = "";
            row1100.Notes = "G5, W12";
            row1100.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
