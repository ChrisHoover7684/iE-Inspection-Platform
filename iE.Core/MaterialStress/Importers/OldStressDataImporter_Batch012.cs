using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch012
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch012(MaterialStressService service)
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

            // Row 13: SA-182, F347H, Forgings
            var row1101 = new OldStressRowData();
            row1101.LineNo = 13;
            row1101.MaterialId = 9320;
            row1101.SpecNo = "SA-182";
            row1101.TypeGrade = "F347H";
            row1101.ProductForm = "Forgings";
            row1101.AlloyUNS = "S34709";
            row1101.ClassCondition = "";
            row1101.Notes = "G5, G18, H2";
            row1101.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1101);

            // Row 14: SA-182, F347H, Forgings
            var row1102 = new OldStressRowData();
            row1102.LineNo = 14;
            row1102.MaterialId = 9321;
            row1102.SpecNo = "SA-182";
            row1102.TypeGrade = "F347H";
            row1102.ProductForm = "Forgings";
            row1102.AlloyUNS = "S34709";
            row1102.ClassCondition = "";
            row1102.Notes = "G18, H2";
            row1102.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1102);

            // Row 15: SA-213, TP347H, Smls. tube
            var row1103 = new OldStressRowData();
            row1103.LineNo = 15;
            row1103.MaterialId = 9328;
            row1103.SpecNo = "SA-213";
            row1103.TypeGrade = "TP347H";
            row1103.ProductForm = "Smls. tube";
            row1103.AlloyUNS = "S34709";
            row1103.ClassCondition = "";
            row1103.Notes = "G5, G18, H2";
            row1103.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1103);

            // Row 16: SA-213, TP347H, Smls. tube
            var row1104 = new OldStressRowData();
            row1104.LineNo = 16;
            row1104.MaterialId = 9329;
            row1104.SpecNo = "SA-213";
            row1104.TypeGrade = "TP347H";
            row1104.ProductForm = "Smls. tube";
            row1104.AlloyUNS = "S34709";
            row1104.ClassCondition = "";
            row1104.Notes = "G18, H2";
            row1104.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1104);

            // Row 17: SA-240, 347H, Plate
            var row1105 = new OldStressRowData();
            row1105.LineNo = 17;
            row1105.MaterialId = 9336;
            row1105.SpecNo = "SA-240";
            row1105.TypeGrade = "347H";
            row1105.ProductForm = "Plate";
            row1105.AlloyUNS = "S34709";
            row1105.ClassCondition = "";
            row1105.Notes = "G5, H2";
            row1105.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1105);

            // Row 18: SA-240, 347H, Plate
            var row1106 = new OldStressRowData();
            row1106.LineNo = 18;
            row1106.MaterialId = 9783;
            row1106.SpecNo = "SA-240";
            row1106.TypeGrade = "347H";
            row1106.ProductForm = "Plate";
            row1106.AlloyUNS = "S34709";
            row1106.ClassCondition = "";
            row1106.Notes = "H2";
            row1106.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1106);

            // Row 19: SA-249, TP347H, Wld. tube
            var row1107 = new OldStressRowData();
            row1107.LineNo = 19;
            row1107.MaterialId = 9346;
            row1107.SpecNo = "SA-249";
            row1107.TypeGrade = "TP347H";
            row1107.ProductForm = "Wld. tube";
            row1107.AlloyUNS = "S34709";
            row1107.ClassCondition = "";
            row1107.Notes = "G5, G18, W14";
            row1107.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1107);

            // Row 20: SA-249, TP347H, Wld. tube
            var row1108 = new OldStressRowData();
            row1108.LineNo = 20;
            row1108.MaterialId = 9347;
            row1108.SpecNo = "SA-249";
            row1108.TypeGrade = "TP347H";
            row1108.ProductForm = "Wld. tube";
            row1108.AlloyUNS = "S34709";
            row1108.ClassCondition = "";
            row1108.Notes = "G18, W14";
            row1108.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1108);

            // Row 21: SA-249, TP347H, Wld. tube
            var row1109 = new OldStressRowData();
            row1109.LineNo = 21;
            row1109.MaterialId = 9344;
            row1109.SpecNo = "SA-249";
            row1109.TypeGrade = "TP347H";
            row1109.ProductForm = "Wld. tube";
            row1109.AlloyUNS = "S34709";
            row1109.ClassCondition = "";
            row1109.Notes = "G3, G5, G18, G24";
            row1109.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 12, 11.5, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1109);

            // Row 22: SA-249, TP347H, Wld. tube
            var row1110 = new OldStressRowData();
            row1110.LineNo = 22;
            row1110.MaterialId = 9345;
            row1110.SpecNo = "SA-249";
            row1110.TypeGrade = "TP347H";
            row1110.ProductForm = "Wld. tube";
            row1110.AlloyUNS = "S34709";
            row1110.ClassCondition = "";
            row1110.Notes = "G3, G18, G24";
            row1110.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 11.3, 11.2, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1110);

            // Row 23: SA-249, TP347H, Wld. tube
            var row1111 = new OldStressRowData();
            row1111.LineNo = 23;
            row1111.MaterialId = 9348;
            row1111.SpecNo = "SA-249";
            row1111.TypeGrade = "TP347H";
            row1111.ProductForm = "Wld. tube";
            row1111.AlloyUNS = "S34709";
            row1111.ClassCondition = "";
            row1111.Notes = "G5, W12";
            row1111.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1111);

            // Row 24: SA-312, TP347H, Smls. pipe
            var row1112 = new OldStressRowData();
            row1112.LineNo = 24;
            row1112.MaterialId = 9365;
            row1112.SpecNo = "SA-312";
            row1112.TypeGrade = "TP347H";
            row1112.ProductForm = "Smls. pipe";
            row1112.AlloyUNS = "S34709";
            row1112.ClassCondition = "";
            row1112.Notes = "G5, G18, H2, W12";
            row1112.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1112);

            // Row 25: SA-312, TP347H, Smls. pipe
            var row1113 = new OldStressRowData();
            row1113.LineNo = 25;
            row1113.MaterialId = 9366;
            row1113.SpecNo = "SA-312";
            row1113.TypeGrade = "TP347H";
            row1113.ProductForm = "Smls. pipe";
            row1113.AlloyUNS = "S34709";
            row1113.ClassCondition = "";
            row1113.Notes = "G18, H2";
            row1113.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1113);

            // Row 26: SA-312, TP347H, Wld. pipe
            var row1114 = new OldStressRowData();
            row1114.LineNo = 26;
            row1114.MaterialId = 9369;
            row1114.SpecNo = "SA-312";
            row1114.TypeGrade = "TP347H";
            row1114.ProductForm = "Wld. pipe";
            row1114.AlloyUNS = "S34709";
            row1114.ClassCondition = "";
            row1114.Notes = "G5, G18, W14";
            row1114.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1114);

            // Row 27: SA-312, TP347H, Wld. pipe
            var row1115 = new OldStressRowData();
            row1115.LineNo = 27;
            row1115.MaterialId = 9370;
            row1115.SpecNo = "SA-312";
            row1115.TypeGrade = "TP347H";
            row1115.ProductForm = "Wld. pipe";
            row1115.AlloyUNS = "S34709";
            row1115.ClassCondition = "";
            row1115.Notes = "G18, W14";
            row1115.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.5, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1115);

            // Row 28: SA-312, TP347H, Wld. pipe
            var row1116 = new OldStressRowData();
            row1116.LineNo = 28;
            row1116.MaterialId = 9367;
            row1116.SpecNo = "SA-312";
            row1116.TypeGrade = "TP347H";
            row1116.ProductForm = "Wld. pipe";
            row1116.AlloyUNS = "S34709";
            row1116.ClassCondition = "";
            row1116.Notes = "G3, G5, G18, G24, H2";
            row1116.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 12, 11.5, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1116);

            // Row 29: SA-312, TP347H, Wld. pipe
            var row1117 = new OldStressRowData();
            row1117.LineNo = 29;
            row1117.MaterialId = 9368;
            row1117.SpecNo = "SA-312";
            row1117.TypeGrade = "TP347H";
            row1117.ProductForm = "Wld. pipe";
            row1117.AlloyUNS = "S34709";
            row1117.ClassCondition = "";
            row1117.Notes = "G3, G18, G24, H2";
            row1117.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 11.3, 11.2, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1117);

            // Row 30: SA-376, TP347H, Smls. pipe
            var row1118 = new OldStressRowData();
            row1118.LineNo = 30;
            row1118.MaterialId = 9385;
            row1118.SpecNo = "SA-376";
            row1118.TypeGrade = "TP347H";
            row1118.ProductForm = "Smls. pipe";
            row1118.AlloyUNS = "S34709";
            row1118.ClassCondition = "";
            row1118.Notes = "G5, G18, H1, H2";
            row1118.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1118);

            // Row 31: SA-376, TP347H, Smls. pipe
            var row1119 = new OldStressRowData();
            row1119.LineNo = 31;
            row1119.MaterialId = 9386;
            row1119.SpecNo = "SA-376";
            row1119.TypeGrade = "TP347H";
            row1119.ProductForm = "Smls. pipe";
            row1119.AlloyUNS = "S34709";
            row1119.ClassCondition = "";
            row1119.Notes = "G18, H1, H2";
            row1119.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1119);

            // Row 32: ..., , 
            var row1120 = new OldStressRowData();
            row1120.LineNo = 32;
            row1120.MaterialId = 9394;
            row1120.SpecNo = "...";
            row1120.TypeGrade = "";
            row1120.ProductForm = "";
            row1120.AlloyUNS = "";
            row1120.ClassCondition = "";
            row1120.Notes = "";
            row1120.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1120);

            // Row 33: ..., , 
            var row1121 = new OldStressRowData();
            row1121.LineNo = 33;
            row1121.MaterialId = 9396;
            row1121.SpecNo = "...";
            row1121.TypeGrade = "";
            row1121.ProductForm = "";
            row1121.AlloyUNS = "";
            row1121.ClassCondition = "";
            row1121.Notes = "";
            row1121.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1121);

            // Row 34: ..., , 
            var row1122 = new OldStressRowData();
            row1122.LineNo = 34;
            row1122.MaterialId = 9399;
            row1122.SpecNo = "...";
            row1122.TypeGrade = "";
            row1122.ProductForm = "";
            row1122.AlloyUNS = "";
            row1122.ClassCondition = "";
            row1122.Notes = "";
            row1122.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1122);

            // Row 35: SA-403, 347H, Smls. & wld. fittings
            var row1123 = new OldStressRowData();
            row1123.LineNo = 35;
            row1123.MaterialId = 9400;
            row1123.SpecNo = "SA-403";
            row1123.TypeGrade = "347H";
            row1123.ProductForm = "Smls. & wld. fittings";
            row1123.AlloyUNS = "S34709";
            row1123.ClassCondition = "";
            row1123.Notes = "G5, W12, W15";
            row1123.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1123);

            // Row 36: ..., , 
            var row1124 = new OldStressRowData();
            row1124.LineNo = 36;
            row1124.MaterialId = 9395;
            row1124.SpecNo = "...";
            row1124.TypeGrade = "";
            row1124.ProductForm = "";
            row1124.AlloyUNS = "";
            row1124.ClassCondition = "";
            row1124.Notes = "";
            row1124.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1124);

            // Row 37: ..., , 
            var row1125 = new OldStressRowData();
            row1125.LineNo = 37;
            row1125.MaterialId = 9397;
            row1125.SpecNo = "...";
            row1125.TypeGrade = "";
            row1125.ProductForm = "";
            row1125.AlloyUNS = "";
            row1125.ClassCondition = "";
            row1125.Notes = "";
            row1125.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1125);

            // Row 38: ..., , 
            var row1126 = new OldStressRowData();
            row1126.LineNo = 38;
            row1126.MaterialId = 9398;
            row1126.SpecNo = "...";
            row1126.TypeGrade = "";
            row1126.ProductForm = "";
            row1126.AlloyUNS = "";
            row1126.ClassCondition = "";
            row1126.Notes = "";
            row1126.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1126);

            // Row 1: SA-452, TP347H, Cast pipe
            var row1127 = new OldStressRowData();
            row1127.LineNo = 1;
            row1127.MaterialId = 9415;
            row1127.SpecNo = "SA-452";
            row1127.TypeGrade = "TP347H";
            row1127.ProductForm = "Cast pipe";
            row1127.AlloyUNS = "S34709";
            row1127.ClassCondition = "";
            row1127.Notes = "G5, G16, G17, G32";
            row1127.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1127);

            // Row 2: SA-452, TP347H, Cast pipe
            var row1128 = new OldStressRowData();
            row1128.LineNo = 2;
            row1128.MaterialId = 9416;
            row1128.SpecNo = "SA-452";
            row1128.TypeGrade = "TP347H";
            row1128.ProductForm = "Cast pipe";
            row1128.AlloyUNS = "S34709";
            row1128.ClassCondition = "";
            row1128.Notes = "G32";
            row1128.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1128);

            // Row 3: SA-479, 347H, Bar
            var row1129 = new OldStressRowData();
            row1129.LineNo = 3;
            row1129.MaterialId = 9419;
            row1129.SpecNo = "SA-479";
            row1129.TypeGrade = "347H";
            row1129.ProductForm = "Bar";
            row1129.AlloyUNS = "S34709";
            row1129.ClassCondition = "";
            row1129.Notes = "G5, G18";
            row1129.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1129);

            // Row 4: SA-479, 347H, Bar
            var row1130 = new OldStressRowData();
            row1130.LineNo = 4;
            row1130.MaterialId = 9420;
            row1130.SpecNo = "SA-479";
            row1130.TypeGrade = "347H";
            row1130.ProductForm = "Bar";
            row1130.AlloyUNS = "S34709";
            row1130.ClassCondition = "";
            row1130.Notes = "G18";
            row1130.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1130);

            // Row 5: SA-813, TP347H, Wld. pipe
            var row1131 = new OldStressRowData();
            row1131.LineNo = 5;
            row1131.MaterialId = 9426;
            row1131.SpecNo = "SA-813";
            row1131.TypeGrade = "TP347H";
            row1131.ProductForm = "Wld. pipe";
            row1131.AlloyUNS = "S34709";
            row1131.ClassCondition = "";
            row1131.Notes = "G5, W12";
            row1131.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1131);

            // Row 6: SA-814, TP347H, Wld. pipe
            var row1132 = new OldStressRowData();
            row1132.LineNo = 6;
            row1132.MaterialId = 9429;
            row1132.SpecNo = "SA-814";
            row1132.TypeGrade = "TP347H";
            row1132.ProductForm = "Wld. pipe";
            row1132.AlloyUNS = "S34709";
            row1132.ClassCondition = "";
            row1132.Notes = "G5, W12";
            row1132.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1132);

            // Row 7: SA-182, F348, Forgings
            var row1133 = new OldStressRowData();
            row1133.LineNo = 7;
            row1133.MaterialId = 9322;
            row1133.SpecNo = "SA-182";
            row1133.TypeGrade = "F348";
            row1133.ProductForm = "Forgings";
            row1133.AlloyUNS = "S34800";
            row1133.ClassCondition = "";
            row1133.Notes = "G5, G12, G18, H2";
            row1133.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1133);

            // Row 8: SA-182, F348, Forgings
            var row1134 = new OldStressRowData();
            row1134.LineNo = 8;
            row1134.MaterialId = 9323;
            row1134.SpecNo = "SA-182";
            row1134.TypeGrade = "F348";
            row1134.ProductForm = "Forgings";
            row1134.AlloyUNS = "S34800";
            row1134.ClassCondition = "";
            row1134.Notes = "G12, G18, H2";
            row1134.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1134);

            // Row 9: SA-213, TP348, Smls. tube
            var row1135 = new OldStressRowData();
            row1135.LineNo = 9;
            row1135.MaterialId = 9330;
            row1135.SpecNo = "SA-213";
            row1135.TypeGrade = "TP348";
            row1135.ProductForm = "Smls. tube";
            row1135.AlloyUNS = "S34800";
            row1135.ClassCondition = "";
            row1135.Notes = "G5, G12, G18, H2";
            row1135.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1135);

            // Row 10: SA-213, TP348, Smls. tube
            var row1136 = new OldStressRowData();
            row1136.LineNo = 10;
            row1136.MaterialId = 9331;
            row1136.SpecNo = "SA-213";
            row1136.TypeGrade = "TP348";
            row1136.ProductForm = "Smls. tube";
            row1136.AlloyUNS = "S34800";
            row1136.ClassCondition = "";
            row1136.Notes = "G12, G18, H2";
            row1136.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1136);

            // Row 11: SA-240, 348, Plate
            var row1137 = new OldStressRowData();
            row1137.LineNo = 11;
            row1137.MaterialId = 9337;
            row1137.SpecNo = "SA-240";
            row1137.TypeGrade = "348";
            row1137.ProductForm = "Plate";
            row1137.AlloyUNS = "S34800";
            row1137.ClassCondition = "";
            row1137.Notes = "G5, G12";
            row1137.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1137);

            // Row 12: SA-240, 348, Plate
            var row1138 = new OldStressRowData();
            row1138.LineNo = 12;
            row1138.MaterialId = 9338;
            row1138.SpecNo = "SA-240";
            row1138.TypeGrade = "348";
            row1138.ProductForm = "Plate";
            row1138.AlloyUNS = "S34800";
            row1138.ClassCondition = "";
            row1138.Notes = "G12";
            row1138.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1138);

            // Row 13: SA-249, TP348, Wld. tube
            var row1139 = new OldStressRowData();
            row1139.LineNo = 13;
            row1139.MaterialId = 9352;
            row1139.SpecNo = "SA-249";
            row1139.TypeGrade = "TP348";
            row1139.ProductForm = "Wld. tube";
            row1139.AlloyUNS = "S34800";
            row1139.ClassCondition = "";
            row1139.Notes = "G12, G18, W14";
            row1139.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.5, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1139);

            // Row 14: SA-249, TP348, Wld. tube
            var row1140 = new OldStressRowData();
            row1140.LineNo = 14;
            row1140.MaterialId = 9349;
            row1140.SpecNo = "SA-249";
            row1140.TypeGrade = "TP348";
            row1140.ProductForm = "Wld. tube";
            row1140.AlloyUNS = "S34800";
            row1140.ClassCondition = "";
            row1140.Notes = "G5, G12, G18, W12, W14";
            row1140.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1140);

            // Row 15: SA-249, TP348, Wld. tube
            var row1141 = new OldStressRowData();
            row1141.LineNo = 15;
            row1141.MaterialId = 9350;
            row1141.SpecNo = "SA-249";
            row1141.TypeGrade = "TP348";
            row1141.ProductForm = "Wld. tube";
            row1141.AlloyUNS = "S34800";
            row1141.ClassCondition = "";
            row1141.Notes = "G3, G5, G12, G18, G24";
            row1141.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1141);

            // Row 16: SA-249, TP348, Wld. tube
            var row1142 = new OldStressRowData();
            row1142.LineNo = 16;
            row1142.MaterialId = 9351;
            row1142.SpecNo = "SA-249";
            row1142.TypeGrade = "TP348";
            row1142.ProductForm = "Wld. tube";
            row1142.AlloyUNS = "S34800";
            row1142.ClassCondition = "";
            row1142.Notes = "G3, G12, G18, G24";
            row1142.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1142);

            // Row 17: SA-312, TP348, Wld. & smls. pipe
            var row1143 = new OldStressRowData();
            row1143.LineNo = 17;
            row1143.MaterialId = 9371;
            row1143.SpecNo = "SA-312";
            row1143.TypeGrade = "TP348";
            row1143.ProductForm = "Wld. & smls. pipe";
            row1143.AlloyUNS = "S34800";
            row1143.ClassCondition = "";
            row1143.Notes = "G5, G12, H2, W12";
            row1143.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1143);

            // Row 18: SA-312, TP348, Smls. pipe
            var row1144 = new OldStressRowData();
            row1144.LineNo = 18;
            row1144.MaterialId = 9374;
            row1144.SpecNo = "SA-312";
            row1144.TypeGrade = "TP348";
            row1144.ProductForm = "Smls. pipe";
            row1144.AlloyUNS = "S34800";
            row1144.ClassCondition = "";
            row1144.Notes = "G5, G12, G18";
            row1144.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1144);

            // Row 19: SA-312, TP348, Smls. pipe
            var row1145 = new OldStressRowData();
            row1145.LineNo = 19;
            row1145.MaterialId = 9375;
            row1145.SpecNo = "SA-312";
            row1145.TypeGrade = "TP348";
            row1145.ProductForm = "Smls. pipe";
            row1145.AlloyUNS = "S34800";
            row1145.ClassCondition = "";
            row1145.Notes = "G12, G18, H2";
            row1145.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1145);

            // Row 20: SA-312, TP348, Wld. pipe
            var row1146 = new OldStressRowData();
            row1146.LineNo = 20;
            row1146.MaterialId = 9372;
            row1146.SpecNo = "SA-312";
            row1146.TypeGrade = "TP348";
            row1146.ProductForm = "Wld. pipe";
            row1146.AlloyUNS = "S34800";
            row1146.ClassCondition = "";
            row1146.Notes = "G5, G12, G24, H2";
            row1146.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1146);

            // Row 21: SA-312, TP348, Wld. pipe
            var row1147 = new OldStressRowData();
            row1147.LineNo = 21;
            row1147.MaterialId = 9373;
            row1147.SpecNo = "SA-312";
            row1147.TypeGrade = "TP348";
            row1147.ProductForm = "Wld. pipe";
            row1147.AlloyUNS = "S34800";
            row1147.ClassCondition = "";
            row1147.Notes = "G12, G24, H2";
            row1147.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 10.3, 7.8, 5.2, 3.8, 2.8, 1.9, 1.3, 1, 0.8, 0.7, null, null, null };
            batch.Add(row1147);

            // Row 22: SA-358, 348, Wld. pipe
            var row1148 = new OldStressRowData();
            row1148.LineNo = 22;
            row1148.MaterialId = 9382;
            row1148.SpecNo = "SA-358";
            row1148.TypeGrade = "348";
            row1148.ProductForm = "Wld. pipe";
            row1148.AlloyUNS = "S34800";
            row1148.ClassCondition = "1";
            row1148.Notes = "G5, W12";
            row1148.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1148);

            // Row 23: SA-376, TP348, Smls. pipe
            var row1149 = new OldStressRowData();
            row1149.LineNo = 23;
            row1149.MaterialId = 9387;
            row1149.SpecNo = "SA-376";
            row1149.TypeGrade = "TP348";
            row1149.ProductForm = "Smls. pipe";
            row1149.AlloyUNS = "S34800";
            row1149.ClassCondition = "";
            row1149.Notes = "G5, G12, G18, H1, H2";
            row1149.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1149);

            // Row 24: SA-376, TP348, Smls. pipe
            var row1150 = new OldStressRowData();
            row1150.LineNo = 24;
            row1150.MaterialId = 9388;
            row1150.SpecNo = "SA-376";
            row1150.TypeGrade = "TP348";
            row1150.ProductForm = "Smls. pipe";
            row1150.AlloyUNS = "S34800";
            row1150.ClassCondition = "";
            row1150.Notes = "G12, G18, H1, H2";
            row1150.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1150);

            // Row 25: ..., , 
            var row1151 = new OldStressRowData();
            row1151.LineNo = 25;
            row1151.MaterialId = 9401;
            row1151.SpecNo = "...";
            row1151.TypeGrade = "";
            row1151.ProductForm = "";
            row1151.AlloyUNS = "";
            row1151.ClassCondition = "";
            row1151.Notes = "";
            row1151.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1151);

            // Row 26: ..., , 
            var row1152 = new OldStressRowData();
            row1152.LineNo = 26;
            row1152.MaterialId = 9403;
            row1152.SpecNo = "...";
            row1152.TypeGrade = "";
            row1152.ProductForm = "";
            row1152.AlloyUNS = "";
            row1152.ClassCondition = "";
            row1152.Notes = "";
            row1152.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1152);

            // Row 27: SA-403, 348, Smls. & wld. fittings
            var row1153 = new OldStressRowData();
            row1153.LineNo = 27;
            row1153.MaterialId = 9402;
            row1153.SpecNo = "SA-403";
            row1153.TypeGrade = "348";
            row1153.ProductForm = "Smls. & wld. fittings";
            row1153.AlloyUNS = "S34800";
            row1153.ClassCondition = "";
            row1153.Notes = "G5, G12, W12, W15";
            row1153.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1153);

            // Row 28: ..., , 
            var row1154 = new OldStressRowData();
            row1154.LineNo = 28;
            row1154.MaterialId = 9404;
            row1154.SpecNo = "...";
            row1154.TypeGrade = "";
            row1154.ProductForm = "";
            row1154.AlloyUNS = "";
            row1154.ClassCondition = "";
            row1154.Notes = "";
            row1154.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1154);

            // Row 29: ..., , 
            var row1155 = new OldStressRowData();
            row1155.LineNo = 29;
            row1155.MaterialId = 9405;
            row1155.SpecNo = "...";
            row1155.TypeGrade = "";
            row1155.ProductForm = "";
            row1155.AlloyUNS = "";
            row1155.ClassCondition = "";
            row1155.Notes = "";
            row1155.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1155);

            // Row 30: SA-409, TP348, Wld. pipe
            var row1156 = new OldStressRowData();
            row1156.LineNo = 30;
            row1156.MaterialId = 9414;
            row1156.SpecNo = "SA-409";
            row1156.TypeGrade = "TP348";
            row1156.ProductForm = "Wld. pipe";
            row1156.AlloyUNS = "S34800";
            row1156.ClassCondition = "";
            row1156.Notes = "G5, W12";
            row1156.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1156);

            // Row 31: SA-479, 348, Bar
            var row1157 = new OldStressRowData();
            row1157.LineNo = 31;
            row1157.MaterialId = 9421;
            row1157.SpecNo = "SA-479";
            row1157.TypeGrade = "348";
            row1157.ProductForm = "Bar";
            row1157.AlloyUNS = "S34800";
            row1157.ClassCondition = "";
            row1157.Notes = "G5, G12, G18, G22, H2";
            row1157.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1157);

            // Row 32: SA-479, 348, Bar
            var row1158 = new OldStressRowData();
            row1158.LineNo = 32;
            row1158.MaterialId = 9422;
            row1158.SpecNo = "SA-479";
            row1158.TypeGrade = "348";
            row1158.ProductForm = "Bar";
            row1158.AlloyUNS = "S34800";
            row1158.ClassCondition = "";
            row1158.Notes = "G12, G18, G22, H2";
            row1158.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 12.1, 9.1, 6.1, 4.4, 3.3, 2.2, 1.5, 1.2, 0.9, 0.8, null, null, null };
            batch.Add(row1158);

            // Row 33: SA-813, TP348, Wld. pipe
            var row1159 = new OldStressRowData();
            row1159.LineNo = 33;
            row1159.MaterialId = 9427;
            row1159.SpecNo = "SA-813";
            row1159.TypeGrade = "TP348";
            row1159.ProductForm = "Wld. pipe";
            row1159.AlloyUNS = "S34800";
            row1159.ClassCondition = "";
            row1159.Notes = "G5, W12";
            row1159.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1159);

            // Row 34: SA-814, TP348, Wld. pipe
            var row1160 = new OldStressRowData();
            row1160.LineNo = 34;
            row1160.MaterialId = 9430;
            row1160.SpecNo = "SA-814";
            row1160.TypeGrade = "TP348";
            row1160.ProductForm = "Wld. pipe";
            row1160.AlloyUNS = "S34800";
            row1160.ClassCondition = "";
            row1160.Notes = "G5, W12";
            row1160.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1160);

            // Row 35: SA-182, F348H, Forgings
            var row1161 = new OldStressRowData();
            row1161.LineNo = 35;
            row1161.MaterialId = 9324;
            row1161.SpecNo = "SA-182";
            row1161.TypeGrade = "F348H";
            row1161.ProductForm = "Forgings";
            row1161.AlloyUNS = "S34809";
            row1161.ClassCondition = "";
            row1161.Notes = "G5, G18, H2";
            row1161.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1161);

            // Row 36: SA-182, F348H, Forgings
            var row1162 = new OldStressRowData();
            row1162.LineNo = 36;
            row1162.MaterialId = 9325;
            row1162.SpecNo = "SA-182";
            row1162.TypeGrade = "F348H";
            row1162.ProductForm = "Forgings";
            row1162.AlloyUNS = "S34809";
            row1162.ClassCondition = "";
            row1162.Notes = "G18, H2";
            row1162.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1162);

            // Row 37: SA-213, TP348H, Smls. tube
            var row1163 = new OldStressRowData();
            row1163.LineNo = 37;
            row1163.MaterialId = 9332;
            row1163.SpecNo = "SA-213";
            row1163.TypeGrade = "TP348H";
            row1163.ProductForm = "Smls. tube";
            row1163.AlloyUNS = "S34809";
            row1163.ClassCondition = "";
            row1163.Notes = "G5, G18, H2";
            row1163.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1163);

            // Row 38: SA-213, TP348H, Smls. tube
            var row1164 = new OldStressRowData();
            row1164.LineNo = 38;
            row1164.MaterialId = 9333;
            row1164.SpecNo = "SA-213";
            row1164.TypeGrade = "TP348H";
            row1164.ProductForm = "Smls. tube";
            row1164.AlloyUNS = "S34809";
            row1164.ClassCondition = "";
            row1164.Notes = "G18, H2";
            row1164.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1164);

            // Row 39: SA-240, 348H, Plate
            var row1165 = new OldStressRowData();
            row1165.LineNo = 39;
            row1165.MaterialId = 9339;
            row1165.SpecNo = "SA-240";
            row1165.TypeGrade = "348H";
            row1165.ProductForm = "Plate";
            row1165.AlloyUNS = "S34809";
            row1165.ClassCondition = "";
            row1165.Notes = "G5";
            row1165.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1165);

            // Row 1: SA-249, TP348H, Wld. tube
            var row1166 = new OldStressRowData();
            row1166.LineNo = 1;
            row1166.MaterialId = 9355;
            row1166.SpecNo = "SA-249";
            row1166.TypeGrade = "TP348H";
            row1166.ProductForm = "Wld. tube";
            row1166.AlloyUNS = "S34809";
            row1166.ClassCondition = "";
            row1166.Notes = "G5, G18, W14";
            row1166.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1166);

            // Row 2: SA-249, TP348H, Wld. tube
            var row1167 = new OldStressRowData();
            row1167.LineNo = 2;
            row1167.MaterialId = 9356;
            row1167.SpecNo = "SA-249";
            row1167.TypeGrade = "TP348H";
            row1167.ProductForm = "Wld. tube";
            row1167.AlloyUNS = "S34809";
            row1167.ClassCondition = "";
            row1167.Notes = "G18, W14";
            row1167.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.5, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1167);

            // Row 3: SA-249, TP348H, Wld. tube
            var row1168 = new OldStressRowData();
            row1168.LineNo = 3;
            row1168.MaterialId = 9353;
            row1168.SpecNo = "SA-249";
            row1168.TypeGrade = "TP348H";
            row1168.ProductForm = "Wld. tube";
            row1168.AlloyUNS = "S34809";
            row1168.ClassCondition = "";
            row1168.Notes = "G3, G5, G18, G24";
            row1168.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 12, 11.5, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1168);

            // Row 4: SA-249, TP348H, Wld. tube
            var row1169 = new OldStressRowData();
            row1169.LineNo = 4;
            row1169.MaterialId = 9354;
            row1169.SpecNo = "SA-249";
            row1169.TypeGrade = "TP348H";
            row1169.ProductForm = "Wld. tube";
            row1169.AlloyUNS = "S34809";
            row1169.ClassCondition = "";
            row1169.Notes = "G3, G18, G24";
            row1169.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 11.3, 11.2, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1169);

            // Row 5: SA-249, TP348H, Wld. tube
            var row1170 = new OldStressRowData();
            row1170.LineNo = 5;
            row1170.MaterialId = 9357;
            row1170.SpecNo = "SA-249";
            row1170.TypeGrade = "TP348H";
            row1170.ProductForm = "Wld. tube";
            row1170.AlloyUNS = "S34809";
            row1170.ClassCondition = "";
            row1170.Notes = "G5, W12";
            row1170.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1170);

            // Row 6: SA-312, TP348H, Wld. & smls. pipe
            var row1171 = new OldStressRowData();
            row1171.LineNo = 6;
            row1171.MaterialId = 9380;
            row1171.SpecNo = "SA-312";
            row1171.TypeGrade = "TP348H";
            row1171.ProductForm = "Wld. & smls. pipe";
            row1171.AlloyUNS = "S34809";
            row1171.ClassCondition = "";
            row1171.Notes = "G5, H2, W12";
            row1171.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1171);

            // Row 7: SA-312, TP348H, Smls. pipe
            var row1172 = new OldStressRowData();
            row1172.LineNo = 7;
            row1172.MaterialId = 9378;
            row1172.SpecNo = "SA-312";
            row1172.TypeGrade = "TP348H";
            row1172.ProductForm = "Smls. pipe";
            row1172.AlloyUNS = "S34809";
            row1172.ClassCondition = "";
            row1172.Notes = "G5, G18";
            row1172.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1172);

            // Row 8: SA-312, TP348H, Smls. pipe
            var row1173 = new OldStressRowData();
            row1173.LineNo = 8;
            row1173.MaterialId = 9379;
            row1173.SpecNo = "SA-312";
            row1173.TypeGrade = "TP348H";
            row1173.ProductForm = "Smls. pipe";
            row1173.AlloyUNS = "S34809";
            row1173.ClassCondition = "";
            row1173.Notes = "G18, H2";
            row1173.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1173);

            // Row 9: SA-312, TP348H, Wld. pipe
            var row1174 = new OldStressRowData();
            row1174.LineNo = 9;
            row1174.MaterialId = 9376;
            row1174.SpecNo = "SA-312";
            row1174.TypeGrade = "TP348H";
            row1174.ProductForm = "Wld. pipe";
            row1174.AlloyUNS = "S34809";
            row1174.ClassCondition = "";
            row1174.Notes = "G5, G24, H2";
            row1174.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.7, 12.6, 12.5, 12.5, 12.5, 12.5, 12.5, 12.4, 12.3, 12, 11.5, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1174);

            // Row 10: SA-312, TP348H, Wld. pipe
            var row1175 = new OldStressRowData();
            row1175.LineNo = 10;
            row1175.MaterialId = 9377;
            row1175.SpecNo = "SA-312";
            row1175.TypeGrade = "TP348H";
            row1175.ProductForm = "Wld. pipe";
            row1175.AlloyUNS = "S34809";
            row1175.ClassCondition = "";
            row1175.Notes = "G24, H2";
            row1175.StressValues = new double?[] { 16, null, 15.2, null, 14, 13.2, 12.8, 12.2, 12, 11.7, 11.6, 11.6, 11.5, 11.5, 11.4, 11.4, 11.3, 11.2, 8.9, 6.7, 5, 3.7, 2.7, 2.1, 1.6, 1.1, null, null, null };
            batch.Add(row1175);

            // Row 11: ..., , 
            var row1176 = new OldStressRowData();
            row1176.LineNo = 11;
            row1176.MaterialId = 9406;
            row1176.SpecNo = "...";
            row1176.TypeGrade = "";
            row1176.ProductForm = "";
            row1176.AlloyUNS = "";
            row1176.ClassCondition = "";
            row1176.Notes = "";
            row1176.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1176);

            // Row 12: ..., , 
            var row1177 = new OldStressRowData();
            row1177.LineNo = 12;
            row1177.MaterialId = 9408;
            row1177.SpecNo = "...";
            row1177.TypeGrade = "";
            row1177.ProductForm = "";
            row1177.AlloyUNS = "";
            row1177.ClassCondition = "";
            row1177.Notes = "";
            row1177.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1177);

            // Row 13: ..., , 
            var row1178 = new OldStressRowData();
            row1178.LineNo = 13;
            row1178.MaterialId = 9411;
            row1178.SpecNo = "...";
            row1178.TypeGrade = "";
            row1178.ProductForm = "";
            row1178.AlloyUNS = "";
            row1178.ClassCondition = "";
            row1178.Notes = "";
            row1178.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1178);

            // Row 14: SA-403, 348H, Smls. & wld. fittings
            var row1179 = new OldStressRowData();
            row1179.LineNo = 14;
            row1179.MaterialId = 9412;
            row1179.SpecNo = "SA-403";
            row1179.TypeGrade = "348H";
            row1179.ProductForm = "Smls. & wld. fittings";
            row1179.AlloyUNS = "S34809";
            row1179.ClassCondition = "";
            row1179.Notes = "G5, W12, W15";
            row1179.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1179);

            // Row 15: ..., , 
            var row1180 = new OldStressRowData();
            row1180.LineNo = 15;
            row1180.MaterialId = 9407;
            row1180.SpecNo = "...";
            row1180.TypeGrade = "";
            row1180.ProductForm = "";
            row1180.AlloyUNS = "";
            row1180.ClassCondition = "";
            row1180.Notes = "";
            row1180.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1180);

            // Row 16: ..., , 
            var row1181 = new OldStressRowData();
            row1181.LineNo = 16;
            row1181.MaterialId = 9409;
            row1181.SpecNo = "...";
            row1181.TypeGrade = "";
            row1181.ProductForm = "";
            row1181.AlloyUNS = "";
            row1181.ClassCondition = "";
            row1181.Notes = "";
            row1181.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1181);

            // Row 17: ..., , 
            var row1182 = new OldStressRowData();
            row1182.LineNo = 17;
            row1182.MaterialId = 9410;
            row1182.SpecNo = "...";
            row1182.TypeGrade = "";
            row1182.ProductForm = "";
            row1182.AlloyUNS = "";
            row1182.ClassCondition = "";
            row1182.Notes = "";
            row1182.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1182);

            // Row 18: SA-479, 348H, Bar
            var row1183 = new OldStressRowData();
            row1183.LineNo = 18;
            row1183.MaterialId = 9423;
            row1183.SpecNo = "SA-479";
            row1183.TypeGrade = "348H";
            row1183.ProductForm = "Bar";
            row1183.AlloyUNS = "S34809";
            row1183.ClassCondition = "";
            row1183.Notes = "G5, G18";
            row1183.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, 14.7, 14.7, 14.6, 14.4, 14.1, 13.5, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1183);

            // Row 19: SA-479, 348H, Bar
            var row1184 = new OldStressRowData();
            row1184.LineNo = 19;
            row1184.MaterialId = 9424;
            row1184.SpecNo = "SA-479";
            row1184.TypeGrade = "348H";
            row1184.ProductForm = "Bar";
            row1184.AlloyUNS = "S34809";
            row1184.ClassCondition = "";
            row1184.Notes = "G18";
            row1184.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.3, 14.1, 13.8, 13.7, 13.6, 13.5, 13.5, 13.4, 13.4, 13.3, 13.2, 10.5, 7.9, 5.9, 4.4, 3.2, 2.5, 1.8, 1.3, null, null, null };
            batch.Add(row1184);

            // Row 20: SA-813, TP348H, Wld. pipe
            var row1185 = new OldStressRowData();
            row1185.LineNo = 20;
            row1185.MaterialId = 9428;
            row1185.SpecNo = "SA-813";
            row1185.TypeGrade = "TP348H";
            row1185.ProductForm = "Wld. pipe";
            row1185.AlloyUNS = "S34809";
            row1185.ClassCondition = "";
            row1185.Notes = "G5, W12";
            row1185.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1185);

            // Row 21: SA-814, TP348H, Wld. pipe
            var row1186 = new OldStressRowData();
            row1186.LineNo = 21;
            row1186.MaterialId = 9431;
            row1186.SpecNo = "SA-814";
            row1186.TypeGrade = "TP348H";
            row1186.ProductForm = "Wld. pipe";
            row1186.AlloyUNS = "S34809";
            row1186.ClassCondition = "";
            row1186.Notes = "G5, W12";
            row1186.StressValues = new double?[] { 18.8, null, 17.9, null, 16.4, 15.5, 15, 14.9, 14.8, 14.7, 14.7, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1186);

            // Row 22: SA-376, TP321, Smls. pipe
            var row1187 = new OldStressRowData();
            row1187.LineNo = 22;
            row1187.MaterialId = 9432;
            row1187.SpecNo = "SA-376";
            row1187.TypeGrade = "TP321";
            row1187.ProductForm = "Smls. pipe";
            row1187.AlloyUNS = "S32100";
            row1187.ClassCondition = "";
            row1187.Notes = "G5, G12";
            row1187.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.2, 14.9, 14.6, 14.4, 14.1, 14, 13.8, 13.7, 13.5, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1187);

            // Row 23: SA-376, TP321, Smls. pipe
            var row1188 = new OldStressRowData();
            row1188.LineNo = 23;
            row1188.MaterialId = 9433;
            row1188.SpecNo = "SA-376";
            row1188.TypeGrade = "TP321";
            row1188.ProductForm = "Smls. pipe";
            row1188.AlloyUNS = "S32100";
            row1188.ClassCondition = "";
            row1188.Notes = "G12";
            row1188.StressValues = new double?[] { 16.7, null, 15, null, 13.8, 12.8, 11.9, 11.3, 11.1, 10.8, 10.6, 10.5, 10.3, 10.2, 10.1, 10, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1188);

            // Row 24: SA-376, TP321H, Smls. pipe
            var row1189 = new OldStressRowData();
            row1189.LineNo = 24;
            row1189.MaterialId = 9434;
            row1189.SpecNo = "SA-376";
            row1189.TypeGrade = "TP321H";
            row1189.ProductForm = "Smls. pipe";
            row1189.AlloyUNS = "S32109";
            row1189.ClassCondition = "";
            row1189.Notes = "G5";
            row1189.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.2, 14.9, 14.6, 14.4, 14.1, 14, 13.8, 13.7, 13.5, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1189);

            // Row 25: SA-376, TP321H, Smls. pipe
            var row1190 = new OldStressRowData();
            row1190.LineNo = 25;
            row1190.MaterialId = 9435;
            row1190.SpecNo = "SA-376";
            row1190.TypeGrade = "TP321H";
            row1190.ProductForm = "Smls. pipe";
            row1190.AlloyUNS = "S32109";
            row1190.ClassCondition = "";
            row1190.Notes = "G5";
            row1190.StressValues = new double?[] { 16.7, null, 15, null, 13.8, 12.8, 11.9, 11.3, 11.1, 10.8, 10.6, 10.5, 10.3, 10.2, 10.1, 10, 9.9, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1190);

            // Row 26: SA-182, F321, Forgings
            var row1191 = new OldStressRowData();
            row1191.LineNo = 26;
            row1191.MaterialId = 9438;
            row1191.SpecNo = "SA-182";
            row1191.TypeGrade = "F321";
            row1191.ProductForm = "Forgings";
            row1191.AlloyUNS = "S32100";
            row1191.ClassCondition = "";
            row1191.Notes = "G12, G18";
            row1191.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1191);

            // Row 27: SA-182, F321, Forgings
            var row1192 = new OldStressRowData();
            row1192.LineNo = 27;
            row1192.MaterialId = 9436;
            row1192.SpecNo = "SA-182";
            row1192.TypeGrade = "F321";
            row1192.ProductForm = "Forgings";
            row1192.AlloyUNS = "S32100";
            row1192.ClassCondition = "";
            row1192.Notes = "G5, G12, G18, H2";
            row1192.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1192);

            // Row 28: SA-182, F321, Forgings
            var row1193 = new OldStressRowData();
            row1193.LineNo = 28;
            row1193.MaterialId = 9437;
            row1193.SpecNo = "SA-182";
            row1193.TypeGrade = "F321";
            row1193.ProductForm = "Forgings";
            row1193.AlloyUNS = "S32100";
            row1193.ClassCondition = "";
            row1193.Notes = "G12, H2";
            row1193.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.4, 9.2, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1193);

            // Row 29: SA-336, F321, Forgings
            var row1194 = new OldStressRowData();
            row1194.LineNo = 29;
            row1194.MaterialId = 9444;
            row1194.SpecNo = "SA-336";
            row1194.TypeGrade = "F321";
            row1194.ProductForm = "Forgings";
            row1194.AlloyUNS = "S32100";
            row1194.ClassCondition = "";
            row1194.Notes = "G12, G18";
            row1194.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1194);

            // Row 30: SA-336, F321, Forgings
            var row1195 = new OldStressRowData();
            row1195.LineNo = 30;
            row1195.MaterialId = 9442;
            row1195.SpecNo = "SA-336";
            row1195.TypeGrade = "F321";
            row1195.ProductForm = "Forgings";
            row1195.AlloyUNS = "S32100";
            row1195.ClassCondition = "";
            row1195.Notes = "G5, G12, G18, H2";
            row1195.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1195);

            // Row 31: SA-336, F321, Forgings
            var row1196 = new OldStressRowData();
            row1196.LineNo = 31;
            row1196.MaterialId = 9443;
            row1196.SpecNo = "SA-336";
            row1196.TypeGrade = "F321";
            row1196.ProductForm = "Forgings";
            row1196.AlloyUNS = "S32100";
            row1196.ClassCondition = "";
            row1196.Notes = "G12, H2";
            row1196.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.4, 9.2, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1196);

            // Row 32: SA-430, FP321, Forged pipe
            var row1197 = new OldStressRowData();
            row1197.LineNo = 32;
            row1197.MaterialId = 9449;
            row1197.SpecNo = "SA-430";
            row1197.TypeGrade = "FP321";
            row1197.ProductForm = "Forged pipe";
            row1197.AlloyUNS = "S32100";
            row1197.ClassCondition = "";
            row1197.Notes = "G18, H1";
            row1197.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1197);

            // Row 33: SA-430, FP321, Forged pipe
            var row1198 = new OldStressRowData();
            row1198.LineNo = 33;
            row1198.MaterialId = 9447;
            row1198.SpecNo = "SA-430";
            row1198.TypeGrade = "FP321";
            row1198.ProductForm = "Forged pipe";
            row1198.AlloyUNS = "S32100";
            row1198.ClassCondition = "";
            row1198.Notes = "G5, G12, G18, H1, H2";
            row1198.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1198);

            // Row 34: SA-430, FP321, Forged pipe
            var row1199 = new OldStressRowData();
            row1199.LineNo = 34;
            row1199.MaterialId = 9448;
            row1199.SpecNo = "SA-430";
            row1199.TypeGrade = "FP321";
            row1199.ProductForm = "Forged pipe";
            row1199.AlloyUNS = "S32100";
            row1199.ClassCondition = "";
            row1199.Notes = "G12, H2";
            row1199.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.4, 9.2, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1199);

            // Row 35: SA-182, F321H, Forgings
            var row1200 = new OldStressRowData();
            row1200.LineNo = 35;
            row1200.MaterialId = 9441;
            row1200.SpecNo = "SA-182";
            row1200.TypeGrade = "F321H";
            row1200.ProductForm = "Forgings";
            row1200.AlloyUNS = "S32109";
            row1200.ClassCondition = "";
            row1200.Notes = "G18";
            row1200.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1200);

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
