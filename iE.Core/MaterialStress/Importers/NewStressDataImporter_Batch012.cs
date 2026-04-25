using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch012
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

            // Row 4: SA-312, TP317, Wld. pipe
            var row1101 = new OldStressRowData();
            row1101.LineNo = 4;
            row1101.MaterialId = 9535;
            row1101.SpecNo = "SA-312";
            row1101.TypeGrade = "TP317";
            row1101.ProductForm = "Wld. pipe";
            row1101.AlloyUNS = "S31700";
            row1101.ClassCondition = "";
            row1101.Notes = "G12, G24, T9";
            row1101.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1101);

            // Row 5: SA-312, TP317L, Smls. pipe
            var row1102 = new OldStressRowData();
            row1102.LineNo = 5;
            row1102.MaterialId = 9793;
            row1102.SpecNo = "SA-312";
            row1102.TypeGrade = "TP317L";
            row1102.ProductForm = "Smls. pipe";
            row1102.AlloyUNS = "S31703";
            row1102.ClassCondition = "";
            row1102.Notes = "G5";
            row1102.StressValues = new double?[] { 20, 20, 20, 20, 19.6, 18.9, 17.7, 16.9, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1102);

            // Row 6: SA-312, TP317L, Smls. pipe
            var row1103 = new OldStressRowData();
            row1103.LineNo = 6;
            row1103.MaterialId = 9794;
            row1103.SpecNo = "SA-312";
            row1103.TypeGrade = "TP317L";
            row1103.ProductForm = "Smls. pipe";
            row1103.AlloyUNS = "S31703";
            row1103.ClassCondition = "";
            row1103.Notes = "";
            row1103.StressValues = new double?[] { 20, 18.2, 17, 16, 15.2, 14, 13.1, 12.5, 12.2, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1103);

            // Row 7: SA-312, TP317L, Wld. pipe
            var row1104 = new OldStressRowData();
            row1104.LineNo = 7;
            row1104.MaterialId = 9795;
            row1104.SpecNo = "SA-312";
            row1104.TypeGrade = "TP317L";
            row1104.ProductForm = "Wld. pipe";
            row1104.AlloyUNS = "S31703";
            row1104.ClassCondition = "";
            row1104.Notes = "G5, G24";
            row1104.StressValues = new double?[] { 17, 17, 17, 17, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1104);

            // Row 8: SA-312, TP317L, Wld. pipe
            var row1105 = new OldStressRowData();
            row1105.LineNo = 8;
            row1105.MaterialId = 9796;
            row1105.SpecNo = "SA-312";
            row1105.TypeGrade = "TP317L";
            row1105.ProductForm = "Wld. pipe";
            row1105.AlloyUNS = "S31703";
            row1105.ClassCondition = "";
            row1105.Notes = "G24";
            row1105.StressValues = new double?[] { 17, 15.5, 14.5, 13.6, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1105);

            // Row 9: SA-403, 317, Smls. & wld. fittings
            var row1106 = new OldStressRowData();
            row1106.LineNo = 9;
            row1106.MaterialId = 9540;
            row1106.SpecNo = "SA-403";
            row1106.TypeGrade = "317";
            row1106.ProductForm = "Smls. & wld. fittings";
            row1106.AlloyUNS = "S31700";
            row1106.ClassCondition = "";
            row1106.Notes = "G5, G12, T8, W14";
            row1106.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1106);

            // Row 10: SA-403, 317L, Fittings
            var row1107 = new OldStressRowData();
            row1107.LineNo = 10;
            row1107.MaterialId = 9797;
            row1107.SpecNo = "SA-403";
            row1107.TypeGrade = "317L";
            row1107.ProductForm = "Fittings";
            row1107.AlloyUNS = "S31703";
            row1107.ClassCondition = "CR";
            row1107.Notes = "G5, G24";
            row1107.StressValues = new double?[] { 17, null, 17, null, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1107);

            // Row 11: SA-403, 317L, Fittings
            var row1108 = new OldStressRowData();
            row1108.LineNo = 11;
            row1108.MaterialId = 9798;
            row1108.SpecNo = "SA-403";
            row1108.TypeGrade = "317L";
            row1108.ProductForm = "Fittings";
            row1108.AlloyUNS = "S31703";
            row1108.ClassCondition = "CR";
            row1108.Notes = "G24";
            row1108.StressValues = new double?[] { 17, null, 14.5, null, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1108);

            // Row 12: SA-403, 317L, Fittings
            var row1109 = new OldStressRowData();
            row1109.LineNo = 12;
            row1109.MaterialId = 9541;
            row1109.SpecNo = "SA-403";
            row1109.TypeGrade = "317L";
            row1109.ProductForm = "Fittings";
            row1109.AlloyUNS = "S31703";
            row1109.ClassCondition = "WP-S";
            row1109.Notes = "G5";
            row1109.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, 17.7, 16.9, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1109);

            // Row 13: SA-403, 317L, Fittings
            var row1110 = new OldStressRowData();
            row1110.LineNo = 13;
            row1110.MaterialId = 9799;
            row1110.SpecNo = "SA-403";
            row1110.TypeGrade = "317L";
            row1110.ProductForm = "Fittings";
            row1110.AlloyUNS = "S31703";
            row1110.ClassCondition = "WP-S";
            row1110.Notes = "";
            row1110.StressValues = new double?[] { 20, null, 17, null, 15.2, 14, 13.1, 12.5, 12.2, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1110);

            // Row 14: SA-403, 317L, Fittings
            var row1111 = new OldStressRowData();
            row1111.LineNo = 14;
            row1111.MaterialId = 9800;
            row1111.SpecNo = "SA-403";
            row1111.TypeGrade = "317L";
            row1111.ProductForm = "Fittings";
            row1111.AlloyUNS = "S31703";
            row1111.ClassCondition = "WP-W";
            row1111.Notes = "G5, G24";
            row1111.StressValues = new double?[] { 17, null, 17, null, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1111);

            // Row 15: SA-403, 317L, Fittings
            var row1112 = new OldStressRowData();
            row1112.LineNo = 15;
            row1112.MaterialId = 9801;
            row1112.SpecNo = "SA-403";
            row1112.TypeGrade = "317L";
            row1112.ProductForm = "Fittings";
            row1112.AlloyUNS = "S31703";
            row1112.ClassCondition = "WP-W";
            row1112.Notes = "G24";
            row1112.StressValues = new double?[] { 17, null, 14.5, null, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1112);

            // Row 16: SA-403, 317L, Fittings
            var row1113 = new OldStressRowData();
            row1113.LineNo = 16;
            row1113.MaterialId = 9802;
            row1113.SpecNo = "SA-403";
            row1113.TypeGrade = "317L";
            row1113.ProductForm = "Fittings";
            row1113.AlloyUNS = "S31703";
            row1113.ClassCondition = "WP-WU";
            row1113.Notes = "G5, G24";
            row1113.StressValues = new double?[] { 17, null, 17, null, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1113);

            // Row 17: SA-403, 317L, Fittings
            var row1114 = new OldStressRowData();
            row1114.LineNo = 17;
            row1114.MaterialId = 9803;
            row1114.SpecNo = "SA-403";
            row1114.TypeGrade = "317L";
            row1114.ProductForm = "Fittings";
            row1114.AlloyUNS = "S31703";
            row1114.ClassCondition = "WP-WU";
            row1114.Notes = "G24";
            row1114.StressValues = new double?[] { 17, null, 14.5, null, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1114);

            // Row 18: SA-403, 317L, Fittings
            var row1115 = new OldStressRowData();
            row1115.LineNo = 18;
            row1115.MaterialId = 9804;
            row1115.SpecNo = "SA-403";
            row1115.TypeGrade = "317L";
            row1115.ProductForm = "Fittings";
            row1115.AlloyUNS = "S31703";
            row1115.ClassCondition = "WP-WX";
            row1115.Notes = "G5, G24";
            row1115.StressValues = new double?[] { 17, null, 17, null, 16.7, 16, 15.1, 14.3, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1115);

            // Row 19: SA-403, 317L, Fittings
            var row1116 = new OldStressRowData();
            row1116.LineNo = 19;
            row1116.MaterialId = 9805;
            row1116.SpecNo = "SA-403";
            row1116.TypeGrade = "317L";
            row1116.ProductForm = "Fittings";
            row1116.AlloyUNS = "S31703";
            row1116.ClassCondition = "WP-WX";
            row1116.Notes = "G24";
            row1116.StressValues = new double?[] { 17, null, 14.5, null, 12.9, 11.9, 11.2, 10.6, 10.4, 10.2, 10, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1116);

            // Row 20: SA-213, XM-15, Smls. tube
            var row1117 = new OldStressRowData();
            row1117.LineNo = 20;
            row1117.MaterialId = 9542;
            row1117.SpecNo = "SA-213";
            row1117.TypeGrade = "XM-15";
            row1117.ProductForm = "Smls. tube";
            row1117.AlloyUNS = "S38100";
            row1117.ClassCondition = "";
            row1117.Notes = "G5";
            row1117.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1117);

            // Row 21: SA-213, XM-15, Smls. tube
            var row1118 = new OldStressRowData();
            row1118.LineNo = 21;
            row1118.MaterialId = 9543;
            row1118.SpecNo = "SA-213";
            row1118.TypeGrade = "XM-15";
            row1118.ProductForm = "Smls. tube";
            row1118.AlloyUNS = "S38100";
            row1118.ClassCondition = "";
            row1118.Notes = "";
            row1118.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1118);

            // Row 22: SA-240, XM-15, Plate
            var row1119 = new OldStressRowData();
            row1119.LineNo = 22;
            row1119.MaterialId = 9544;
            row1119.SpecNo = "SA-240";
            row1119.TypeGrade = "XM-15";
            row1119.ProductForm = "Plate";
            row1119.AlloyUNS = "S38100";
            row1119.ClassCondition = "";
            row1119.Notes = "G5";
            row1119.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1119);

            // Row 23: SA-240, XM-15, Plate
            var row1120 = new OldStressRowData();
            row1120.LineNo = 23;
            row1120.MaterialId = 9545;
            row1120.SpecNo = "SA-240";
            row1120.TypeGrade = "XM-15";
            row1120.ProductForm = "Plate";
            row1120.AlloyUNS = "S38100";
            row1120.ClassCondition = "";
            row1120.Notes = "";
            row1120.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1120);

            // Row 24: SA-249, TPXM-15, Wld. tube
            var row1121 = new OldStressRowData();
            row1121.LineNo = 24;
            row1121.MaterialId = 9546;
            row1121.SpecNo = "SA-249";
            row1121.TypeGrade = "TPXM-15";
            row1121.ProductForm = "Wld. tube";
            row1121.AlloyUNS = "S38100";
            row1121.ClassCondition = "";
            row1121.Notes = "G5, G24";
            row1121.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1121);

            // Row 25: SA-249, TPXM-15, Wld. tube
            var row1122 = new OldStressRowData();
            row1122.LineNo = 25;
            row1122.MaterialId = 9547;
            row1122.SpecNo = "SA-249";
            row1122.TypeGrade = "TPXM-15";
            row1122.ProductForm = "Wld. tube";
            row1122.AlloyUNS = "S38100";
            row1122.ClassCondition = "";
            row1122.Notes = "G24";
            row1122.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1122);

            // Row 26: SA-312, TPXM-15, Wld. pipe
            var row1123 = new OldStressRowData();
            row1123.LineNo = 26;
            row1123.MaterialId = 9548;
            row1123.SpecNo = "SA-312";
            row1123.TypeGrade = "TPXM-15";
            row1123.ProductForm = "Wld. pipe";
            row1123.AlloyUNS = "S38100";
            row1123.ClassCondition = "";
            row1123.Notes = "G5, G24";
            row1123.StressValues = new double?[] { 17, null, 17, null, 16.1, 15.5, 14.8, 14.1, 13.8, 13.5, 13.2, 12.9, 12.6, 12.4, 12.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1123);

            // Row 27: SA-312, TPXM-15, Wld. pipe
            var row1124 = new OldStressRowData();
            row1124.LineNo = 27;
            row1124.MaterialId = 9549;
            row1124.SpecNo = "SA-312";
            row1124.TypeGrade = "TPXM-15";
            row1124.ProductForm = "Wld. pipe";
            row1124.AlloyUNS = "S38100";
            row1124.ClassCondition = "";
            row1124.Notes = "G24";
            row1124.StressValues = new double?[] { 17, null, 14.2, null, 12.7, 11.7, 11, 10.4, 10.2, 10, 9.8, 9.6, 9.4, 9.2, 9, 8.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1124);

            // Row 28: SA-351, CF10, Castings
            var row1125 = new OldStressRowData();
            row1125.LineNo = 28;
            row1125.MaterialId = 9550;
            row1125.SpecNo = "SA-351";
            row1125.TypeGrade = "CF10";
            row1125.ProductForm = "Castings";
            row1125.AlloyUNS = "J92590";
            row1125.ClassCondition = "";
            row1125.Notes = "G1, G5, G32, T6";
            row1125.StressValues = new double?[] { 20, null, 19, null, 17.7, 17.2, 17, 16.5, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.4, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row1125);

            // Row 29: SA-351, CF10, Castings
            var row1126 = new OldStressRowData();
            row1126.LineNo = 29;
            row1126.MaterialId = 9551;
            row1126.SpecNo = "SA-351";
            row1126.TypeGrade = "CF10";
            row1126.ProductForm = "Castings";
            row1126.AlloyUNS = "J92590";
            row1126.ClassCondition = "";
            row1126.Notes = "G1, G32, T7";
            row1126.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.2, 12, 11.7, 11.5, 11.3, 11.1, 10.8, 10.6, 10.4, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row1126);

            // Row 30: SA-351, CF10M, Castings
            var row1127 = new OldStressRowData();
            row1127.LineNo = 30;
            row1127.MaterialId = 9552;
            row1127.SpecNo = "SA-351";
            row1127.TypeGrade = "CF10M";
            row1127.ProductForm = "Castings";
            row1127.AlloyUNS = "";
            row1127.ClassCondition = "";
            row1127.Notes = "G1, G5, G32, T6";
            row1127.StressValues = new double?[] { 20, null, 20, null, 19.5, 19.3, 17.9, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.5, 15.3, 14.9, 11.5, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row1127);

            // Row 31: SA-351, CF10M, Castings
            var row1128 = new OldStressRowData();
            row1128.LineNo = 31;
            row1128.MaterialId = 9553;
            row1128.SpecNo = "SA-351";
            row1128.TypeGrade = "CF10M";
            row1128.ProductForm = "Castings";
            row1128.AlloyUNS = "";
            row1128.ClassCondition = "";
            row1128.Notes = "G1, G32, T8";
            row1128.StressValues = new double?[] { 20, null, 17.5, null, 15.7, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row1128);

            // Row 32: SA-351, CG8M, Castings
            var row1129 = new OldStressRowData();
            row1129.LineNo = 32;
            row1129.MaterialId = 9554;
            row1129.SpecNo = "SA-351";
            row1129.TypeGrade = "CG8M";
            row1129.ProductForm = "Castings";
            row1129.AlloyUNS = "J93000";
            row1129.ClassCondition = "";
            row1129.Notes = "G1, G5, G32";
            row1129.StressValues = new double?[] { 21.4, null, 20.8, null, 19.6, 19.1, 18.4, 17.5, 17.1, 16.8, 16.6, 16.4, 16.2, 16, 15.7, 12.2, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1129);

            // Row 33: SA-351, CG8M, Castings
            var row1130 = new OldStressRowData();
            row1130.LineNo = 33;
            row1130.MaterialId = 9555;
            row1130.SpecNo = "SA-351";
            row1130.TypeGrade = "CG8M";
            row1130.ProductForm = "Castings";
            row1130.AlloyUNS = "J93000";
            row1130.ClassCondition = "";
            row1130.Notes = "G1, G32";
            row1130.StressValues = new double?[] { 21.4, null, 18.8, null, 16.4, 14.7, 13.6, 12.9, 12.7, 12.5, 12.3, 12.1, 12, 11.8, 11.6, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1130);

            // Row 34: SA-213, , Smls. tube
            var row1131 = new OldStressRowData();
            row1131.LineNo = 34;
            row1131.MaterialId = 9556;
            row1131.SpecNo = "SA-213";
            row1131.TypeGrade = "";
            row1131.ProductForm = "Smls. tube";
            row1131.AlloyUNS = "S31725";
            row1131.ClassCondition = "";
            row1131.Notes = "G5";
            row1131.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1131);

            // Row 35: SA-213, , Smls. tube
            var row1132 = new OldStressRowData();
            row1132.LineNo = 35;
            row1132.MaterialId = 9557;
            row1132.SpecNo = "SA-213";
            row1132.TypeGrade = "";
            row1132.ProductForm = "Smls. tube";
            row1132.AlloyUNS = "S31725";
            row1132.ClassCondition = "";
            row1132.Notes = "";
            row1132.StressValues = new double?[] { 20, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1132);

            // Row 36: SA-240, , Plate
            var row1133 = new OldStressRowData();
            row1133.LineNo = 36;
            row1133.MaterialId = 9558;
            row1133.SpecNo = "SA-240";
            row1133.TypeGrade = "";
            row1133.ProductForm = "Plate";
            row1133.AlloyUNS = "S31725";
            row1133.ClassCondition = "";
            row1133.Notes = "G5";
            row1133.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1133);

            // Row 37: SA-240, , Plate
            var row1134 = new OldStressRowData();
            row1134.LineNo = 37;
            row1134.MaterialId = 9559;
            row1134.SpecNo = "SA-240";
            row1134.TypeGrade = "";
            row1134.ProductForm = "Plate";
            row1134.AlloyUNS = "S31725";
            row1134.ClassCondition = "";
            row1134.Notes = "";
            row1134.StressValues = new double?[] { 20, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1134);

            // Row 38: SA-249, , Wld. tube
            var row1135 = new OldStressRowData();
            row1135.LineNo = 38;
            row1135.MaterialId = 9560;
            row1135.SpecNo = "SA-249";
            row1135.TypeGrade = "";
            row1135.ProductForm = "Wld. tube";
            row1135.AlloyUNS = "S31725";
            row1135.ClassCondition = "";
            row1135.Notes = "G5, G24";
            row1135.StressValues = new double?[] { 17, null, 17, null, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1135);

            // Row 39: SA-249, , Wld. tube
            var row1136 = new OldStressRowData();
            row1136.LineNo = 39;
            row1136.MaterialId = 9561;
            row1136.SpecNo = "SA-249";
            row1136.TypeGrade = "";
            row1136.ProductForm = "Wld. tube";
            row1136.AlloyUNS = "S31725";
            row1136.ClassCondition = "";
            row1136.Notes = "G24";
            row1136.StressValues = new double?[] { 17, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1136);

            // Row 1: SA-312, , Smls. pipe
            var row1137 = new OldStressRowData();
            row1137.LineNo = 1;
            row1137.MaterialId = 9562;
            row1137.SpecNo = "SA-312";
            row1137.TypeGrade = "";
            row1137.ProductForm = "Smls. pipe";
            row1137.AlloyUNS = "S31725";
            row1137.ClassCondition = "";
            row1137.Notes = "G5";
            row1137.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1137);

            // Row 2: SA-312, , Smls. pipe
            var row1138 = new OldStressRowData();
            row1138.LineNo = 2;
            row1138.MaterialId = 9564;
            row1138.SpecNo = "SA-312";
            row1138.TypeGrade = "";
            row1138.ProductForm = "Smls. pipe";
            row1138.AlloyUNS = "S31725";
            row1138.ClassCondition = "";
            row1138.Notes = "";
            row1138.StressValues = new double?[] { 20, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1138);

            // Row 3: SA-312, , Wld. pipe
            var row1139 = new OldStressRowData();
            row1139.LineNo = 3;
            row1139.MaterialId = 9563;
            row1139.SpecNo = "SA-312";
            row1139.TypeGrade = "";
            row1139.ProductForm = "Wld. pipe";
            row1139.AlloyUNS = "S31725";
            row1139.ClassCondition = "";
            row1139.Notes = "G5, G24";
            row1139.StressValues = new double?[] { 17, null, 17, null, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1139);

            // Row 4: SA-312, , Wld. pipe
            var row1140 = new OldStressRowData();
            row1140.LineNo = 4;
            row1140.MaterialId = 9565;
            row1140.SpecNo = "SA-312";
            row1140.TypeGrade = "";
            row1140.ProductForm = "Wld. pipe";
            row1140.AlloyUNS = "S31725";
            row1140.ClassCondition = "";
            row1140.Notes = "G24";
            row1140.StressValues = new double?[] { 17, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1140);

            // Row 5: SA-358, , Wld. pipe
            var row1141 = new OldStressRowData();
            row1141.LineNo = 5;
            row1141.MaterialId = 9566;
            row1141.SpecNo = "SA-358";
            row1141.TypeGrade = "";
            row1141.ProductForm = "Wld. pipe";
            row1141.AlloyUNS = "S31725";
            row1141.ClassCondition = "";
            row1141.Notes = "G5, G24";
            row1141.StressValues = new double?[] { 17, null, 17, null, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1141);

            // Row 6: SA-358, , Wld. pipe
            var row1142 = new OldStressRowData();
            row1142.LineNo = 6;
            row1142.MaterialId = 9567;
            row1142.SpecNo = "SA-358";
            row1142.TypeGrade = "";
            row1142.ProductForm = "Wld. pipe";
            row1142.AlloyUNS = "S31725";
            row1142.ClassCondition = "";
            row1142.Notes = "G24";
            row1142.StressValues = new double?[] { 17, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1142);

            // Row 7: SA-376, , Smls. pipe
            var row1143 = new OldStressRowData();
            row1143.LineNo = 7;
            row1143.MaterialId = 9568;
            row1143.SpecNo = "SA-376";
            row1143.TypeGrade = "";
            row1143.ProductForm = "Smls. pipe";
            row1143.AlloyUNS = "S31725";
            row1143.ClassCondition = "";
            row1143.Notes = "G5";
            row1143.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1143);

            // Row 8: SA-376, , Smls. pipe
            var row1144 = new OldStressRowData();
            row1144.LineNo = 8;
            row1144.MaterialId = 9569;
            row1144.SpecNo = "SA-376";
            row1144.TypeGrade = "";
            row1144.ProductForm = "Smls. pipe";
            row1144.AlloyUNS = "S31725";
            row1144.ClassCondition = "";
            row1144.Notes = "";
            row1144.StressValues = new double?[] { 20, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1144);

            // Row 9: SA-409, , Wld. pipe
            var row1145 = new OldStressRowData();
            row1145.LineNo = 9;
            row1145.MaterialId = 9570;
            row1145.SpecNo = "SA-409";
            row1145.TypeGrade = "";
            row1145.ProductForm = "Wld. pipe";
            row1145.AlloyUNS = "S31725";
            row1145.ClassCondition = "";
            row1145.Notes = "G5, G24";
            row1145.StressValues = new double?[] { 17, null, 17, null, 16.6, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1145);

            // Row 10: SA-409, , Wld. pipe
            var row1146 = new OldStressRowData();
            row1146.LineNo = 10;
            row1146.MaterialId = 9571;
            row1146.SpecNo = "SA-409";
            row1146.TypeGrade = "";
            row1146.ProductForm = "Wld. pipe";
            row1146.AlloyUNS = "S31725";
            row1146.ClassCondition = "";
            row1146.Notes = "G24";
            row1146.StressValues = new double?[] { 17, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1146);

            // Row 11: SA-479, , Bar
            var row1147 = new OldStressRowData();
            row1147.LineNo = 11;
            row1147.MaterialId = 9572;
            row1147.SpecNo = "SA-479";
            row1147.TypeGrade = "";
            row1147.ProductForm = "Bar";
            row1147.AlloyUNS = "S31725";
            row1147.ClassCondition = "";
            row1147.Notes = "G5";
            row1147.StressValues = new double?[] { 20, null, 20, null, 19.6, 18.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1147);

            // Row 12: SA-479, , Bar
            var row1148 = new OldStressRowData();
            row1148.LineNo = 12;
            row1148.MaterialId = 9573;
            row1148.SpecNo = "SA-479";
            row1148.TypeGrade = "";
            row1148.ProductForm = "Bar";
            row1148.AlloyUNS = "S31725";
            row1148.ClassCondition = "";
            row1148.Notes = "";
            row1148.StressValues = new double?[] { 20, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1148);

            // Row 13: SA-479, ER308, Bar
            var row1149 = new OldStressRowData();
            row1149.LineNo = 13;
            row1149.MaterialId = 9574;
            row1149.SpecNo = "SA-479";
            row1149.TypeGrade = "ER308";
            row1149.ProductForm = "Bar";
            row1149.AlloyUNS = "S30880";
            row1149.ClassCondition = "";
            row1149.Notes = "G5";
            row1149.StressValues = new double?[] { 20, null, 20, null, 18.9, 18.3, 17.5, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1149);

            // Row 14: SA-351, CK3MCuN, Castings
            var row1150 = new OldStressRowData();
            row1150.LineNo = 14;
            row1150.MaterialId = 9575;
            row1150.SpecNo = "SA-351";
            row1150.TypeGrade = "CK3MCuN";
            row1150.ProductForm = "Castings";
            row1150.AlloyUNS = "J93254";
            row1150.ClassCondition = "";
            row1150.Notes = "G1, G5";
            row1150.StressValues = new double?[] { 22.9, null, 22.9, null, 21.7, 20.7, 20, 19.6, 19.4, 19.3, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1150);

            // Row 15: SA-351, CK3MCuN, Castings
            var row1151 = new OldStressRowData();
            row1151.LineNo = 15;
            row1151.MaterialId = 9576;
            row1151.SpecNo = "SA-351";
            row1151.TypeGrade = "CK3MCuN";
            row1151.ProductForm = "Castings";
            row1151.AlloyUNS = "J93254";
            row1151.ClassCondition = "";
            row1151.Notes = "G1";
            row1151.StressValues = new double?[] { 22.9, null, 20.7, null, 18.5, 17.1, 16.1, 15.4, 15.2, 15.1, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1151);

            // Row 16: SA-182, F44, Forgings
            var row1152 = new OldStressRowData();
            row1152.LineNo = 16;
            row1152.MaterialId = 9578;
            row1152.SpecNo = "SA-182";
            row1152.TypeGrade = "F44";
            row1152.ProductForm = "Forgings";
            row1152.AlloyUNS = "S31254";
            row1152.ClassCondition = "";
            row1152.Notes = "G5";
            row1152.StressValues = new double?[] { 26.9, null, 26.9, null, 25.5, 24.3, 23.5, 23, 22.8, 22.7, 22.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1152);

            // Row 17: SA-182, F44, Forgings
            var row1153 = new OldStressRowData();
            row1153.LineNo = 17;
            row1153.MaterialId = 9577;
            row1153.SpecNo = "SA-182";
            row1153.TypeGrade = "F44";
            row1153.ProductForm = "Forgings";
            row1153.AlloyUNS = "S31254";
            row1153.ClassCondition = "";
            row1153.Notes = "";
            row1153.StressValues = new double?[] { 26.9, null, 23.9, null, 21.4, 19.8, 18.6, 17.9, 17.6, 17.4, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1153);

            // Row 18: SA-249, , Wld. tube
            var row1154 = new OldStressRowData();
            row1154.LineNo = 18;
            row1154.MaterialId = 9582;
            row1154.SpecNo = "SA-249";
            row1154.TypeGrade = "";
            row1154.ProductForm = "Wld. tube";
            row1154.AlloyUNS = "S31254";
            row1154.ClassCondition = "";
            row1154.Notes = "G5, G24";
            row1154.StressValues = new double?[] { 22.8, null, 22.8, null, 21.7, 20.7, 20, 19.5, 19.4, 19.3, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1154);

            // Row 19: SA-249, , Wld. tube
            var row1155 = new OldStressRowData();
            row1155.LineNo = 19;
            row1155.MaterialId = 9581;
            row1155.SpecNo = "SA-249";
            row1155.TypeGrade = "";
            row1155.ProductForm = "Wld. tube";
            row1155.AlloyUNS = "S31254";
            row1155.ClassCondition = "";
            row1155.Notes = "G24";
            row1155.StressValues = new double?[] { 22.8, null, 20.3, null, 18.2, 16.8, 15.8, 15.2, 15, 14.8, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1155);

            // Row 20: SA-312, , Smls. pipe
            var row1156 = new OldStressRowData();
            row1156.LineNo = 20;
            row1156.MaterialId = 9585;
            row1156.SpecNo = "SA-312";
            row1156.TypeGrade = "";
            row1156.ProductForm = "Smls. pipe";
            row1156.AlloyUNS = "S31254";
            row1156.ClassCondition = "";
            row1156.Notes = "G5";
            row1156.StressValues = new double?[] { 26.9, null, 26.9, null, 25.5, 24.3, 23.5, 23, 22.8, 22.7, 22.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1156);

            // Row 21: SA-312, , Smls. pipe
            var row1157 = new OldStressRowData();
            row1157.LineNo = 21;
            row1157.MaterialId = 9583;
            row1157.SpecNo = "SA-312";
            row1157.TypeGrade = "";
            row1157.ProductForm = "Smls. pipe";
            row1157.AlloyUNS = "S31254";
            row1157.ClassCondition = "";
            row1157.Notes = "";
            row1157.StressValues = new double?[] { 26.9, null, 23.9, null, 21.4, 19.8, 18.6, 17.9, 17.6, 17.4, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1157);

            // Row 22: SA-312, , Wld. pipe
            var row1158 = new OldStressRowData();
            row1158.LineNo = 22;
            row1158.MaterialId = 9586;
            row1158.SpecNo = "SA-312";
            row1158.TypeGrade = "";
            row1158.ProductForm = "Wld. pipe";
            row1158.AlloyUNS = "S31254";
            row1158.ClassCondition = "";
            row1158.Notes = "G5, G24";
            row1158.StressValues = new double?[] { 22.8, null, 22.8, null, 21.7, 20.7, 20, 19.5, 19.4, 19.3, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1158);

            // Row 23: SA-312, , Wld. pipe
            var row1159 = new OldStressRowData();
            row1159.LineNo = 23;
            row1159.MaterialId = 9584;
            row1159.SpecNo = "SA-312";
            row1159.TypeGrade = "";
            row1159.ProductForm = "Wld. pipe";
            row1159.AlloyUNS = "S31254";
            row1159.ClassCondition = "";
            row1159.Notes = "G24";
            row1159.StressValues = new double?[] { 22.8, null, 20.3, null, 18.2, 16.8, 15.8, 15.2, 15, 14.8, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1159);

            // Row 24: SA-358, , Wld. pipe
            var row1160 = new OldStressRowData();
            row1160.LineNo = 24;
            row1160.MaterialId = 9588;
            row1160.SpecNo = "SA-358";
            row1160.TypeGrade = "";
            row1160.ProductForm = "Wld. pipe";
            row1160.AlloyUNS = "S31254";
            row1160.ClassCondition = "";
            row1160.Notes = "G5, G24";
            row1160.StressValues = new double?[] { 22.8, null, 22.8, null, 21.7, 20.7, 20, 19.5, 19.4, 19.3, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1160);

            // Row 25: SA-358, , Wld. pipe
            var row1161 = new OldStressRowData();
            row1161.LineNo = 25;
            row1161.MaterialId = 9587;
            row1161.SpecNo = "SA-358";
            row1161.TypeGrade = "";
            row1161.ProductForm = "Wld. pipe";
            row1161.AlloyUNS = "S31254";
            row1161.ClassCondition = "";
            row1161.Notes = "G24";
            row1161.StressValues = new double?[] { 22.8, null, 20.3, null, 18.2, 16.8, 15.8, 15.2, 15, 14.8, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1161);

            // Row 26: SA-403, , Fittings
            var row1162 = new OldStressRowData();
            row1162.LineNo = 26;
            row1162.MaterialId = 16933;
            row1162.SpecNo = "SA-403";
            row1162.TypeGrade = "";
            row1162.ProductForm = "Fittings";
            row1162.AlloyUNS = "S31254";
            row1162.ClassCondition = "";
            row1162.Notes = "G5, W12";
            row1162.StressValues = new double?[] { 26.9, null, 26.9, null, 25.5, 24.3, 23.5, 23, 22.8, 22.7, 22.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1162);

            // Row 27: SA-240, , Plate
            var row1163 = new OldStressRowData();
            row1163.LineNo = 27;
            row1163.MaterialId = 16931;
            row1163.SpecNo = "SA-240";
            row1163.TypeGrade = "";
            row1163.ProductForm = "Plate";
            row1163.AlloyUNS = "S31254";
            row1163.ClassCondition = "";
            row1163.Notes = "G5";
            row1163.StressValues = new double?[] { 27.1, null, 27.1, null, 25.8, 24.6, 23.7, 23.2, 23.1, 23, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1163);

            // Row 28: SA-240, , Plate
            var row1164 = new OldStressRowData();
            row1164.LineNo = 28;
            row1164.MaterialId = 16932;
            row1164.SpecNo = "SA-240";
            row1164.TypeGrade = "";
            row1164.ProductForm = "Plate";
            row1164.AlloyUNS = "S31254";
            row1164.ClassCondition = "";
            row1164.Notes = "";
            row1164.StressValues = new double?[] { 27.1, null, 24.5, null, 21.9, 20.2, 19.1, 18.3, 18, 17.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1164);

            // Row 29: SA-240, , Plate
            var row1165 = new OldStressRowData();
            row1165.LineNo = 29;
            row1165.MaterialId = 9580;
            row1165.SpecNo = "SA-240";
            row1165.TypeGrade = "";
            row1165.ProductForm = "Plate";
            row1165.AlloyUNS = "S31254";
            row1165.ClassCondition = "";
            row1165.Notes = "G5";
            row1165.StressValues = new double?[] { 28.6, null, 28.6, null, 27.2, 25.9, 25, 24.4, 24.3, 24.1, 23.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1165);

            // Row 30: SA-240, , Plate
            var row1166 = new OldStressRowData();
            row1166.LineNo = 30;
            row1166.MaterialId = 9579;
            row1166.SpecNo = "SA-240";
            row1166.TypeGrade = "";
            row1166.ProductForm = "Plate";
            row1166.AlloyUNS = "S31254";
            row1166.ClassCondition = "";
            row1166.Notes = "";
            row1166.StressValues = new double?[] { 28.6, null, 24.5, null, 21.9, 20.2, 19.1, 18.3, 18, 17.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1166);

            // Row 31: SA-182, FXM-11, Forgings
            var row1167 = new OldStressRowData();
            row1167.LineNo = 31;
            row1167.MaterialId = 9589;
            row1167.SpecNo = "SA-182";
            row1167.TypeGrade = "FXM-11";
            row1167.ProductForm = "Forgings";
            row1167.AlloyUNS = "S21904";
            row1167.ClassCondition = "";
            row1167.Notes = "";
            row1167.StressValues = new double?[] { 25.7, null, 25.7, null, 22, 19.6, 18.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1167);

            // Row 32: SA-312, TPXM-11, Smls. pipe
            var row1168 = new OldStressRowData();
            row1168.LineNo = 32;
            row1168.MaterialId = 9593;
            row1168.SpecNo = "SA-312";
            row1168.TypeGrade = "TPXM-11";
            row1168.ProductForm = "Smls. pipe";
            row1168.AlloyUNS = "S21904";
            row1168.ClassCondition = "";
            row1168.Notes = "G5";
            row1168.StressValues = new double?[] { 25.7, null, 25.7, null, 24, 22.8, 22, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1168);

            // Row 33: SA-312, TPXM-11, Smls. pipe
            var row1169 = new OldStressRowData();
            row1169.LineNo = 33;
            row1169.MaterialId = 9591;
            row1169.SpecNo = "SA-312";
            row1169.TypeGrade = "TPXM-11";
            row1169.ProductForm = "Smls. pipe";
            row1169.AlloyUNS = "S21904";
            row1169.ClassCondition = "";
            row1169.Notes = "";
            row1169.StressValues = new double?[] { 25.7, null, 25.7, null, 22, 19.6, 18.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1169);

            // Row 34: SA-312, TPXM-11, Wld. pipe
            var row1170 = new OldStressRowData();
            row1170.LineNo = 34;
            row1170.MaterialId = 9592;
            row1170.SpecNo = "SA-312";
            row1170.TypeGrade = "TPXM-11";
            row1170.ProductForm = "Wld. pipe";
            row1170.AlloyUNS = "S21904";
            row1170.ClassCondition = "";
            row1170.Notes = "G5, G24";
            row1170.StressValues = new double?[] { 21.9, null, 21.9, null, 20.4, 19.4, 18.7, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1170);

            // Row 35: SA-312, TPXM-11, Wld. pipe
            var row1171 = new OldStressRowData();
            row1171.LineNo = 35;
            row1171.MaterialId = 9590;
            row1171.SpecNo = "SA-312";
            row1171.TypeGrade = "TPXM-11";
            row1171.ProductForm = "Wld. pipe";
            row1171.AlloyUNS = "S21904";
            row1171.ClassCondition = "";
            row1171.Notes = "G24";
            row1171.StressValues = new double?[] { 21.9, null, 21.9, null, 18.7, 16.6, 15.3, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1171);

            // Row 36: SA-336, FXM-11, Forgings
            var row1172 = new OldStressRowData();
            row1172.LineNo = 36;
            row1172.MaterialId = 9595;
            row1172.SpecNo = "SA-336";
            row1172.TypeGrade = "FXM-11";
            row1172.ProductForm = "Forgings";
            row1172.AlloyUNS = "S21904";
            row1172.ClassCondition = "";
            row1172.Notes = "G5";
            row1172.StressValues = new double?[] { 25.7, null, 25.7, null, 24, 22.8, 22, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1172);

            // Row 37: SA-336, FXM-11, Forgings
            var row1173 = new OldStressRowData();
            row1173.LineNo = 37;
            row1173.MaterialId = 9594;
            row1173.SpecNo = "SA-336";
            row1173.TypeGrade = "FXM-11";
            row1173.ProductForm = "Forgings";
            row1173.AlloyUNS = "S21904";
            row1173.ClassCondition = "";
            row1173.Notes = "";
            row1173.StressValues = new double?[] { 25.7, null, 25.7, null, 22, 19.6, 18.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1173);

            // Row 38: SA-666, XM-11, Plate
            var row1174 = new OldStressRowData();
            row1174.LineNo = 38;
            row1174.MaterialId = 9806;
            row1174.SpecNo = "SA-666";
            row1174.TypeGrade = "XM-11";
            row1174.ProductForm = "Plate";
            row1174.AlloyUNS = "S21904";
            row1174.ClassCondition = "";
            row1174.Notes = "G5";
            row1174.StressValues = new double?[] { 25.7, null, 25.7, null, 24, 22.8, 22, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1174);

            // Row 39: SA-666, XM-11, Plate
            var row1175 = new OldStressRowData();
            row1175.LineNo = 39;
            row1175.MaterialId = 9807;
            row1175.SpecNo = "SA-666";
            row1175.TypeGrade = "XM-11";
            row1175.ProductForm = "Plate";
            row1175.AlloyUNS = "S21904";
            row1175.ClassCondition = "";
            row1175.Notes = "";
            row1175.StressValues = new double?[] { 25.7, null, 25.7, null, 22, 19.6, 18.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1175);

            // Row 1: SA-182, F45, Forgings
            var row1176 = new OldStressRowData();
            row1176.LineNo = 1;
            row1176.MaterialId = 9597;
            row1176.SpecNo = "SA-182";
            row1176.TypeGrade = "F45";
            row1176.ProductForm = "Forgings";
            row1176.AlloyUNS = "S30815";
            row1176.ClassCondition = "";
            row1176.Notes = "G5, G40, T5";
            row1176.StressValues = new double?[] { 24.9, null, 24.7, null, 23.3, 22.4, 21.8, 21.4, 21.2, 21, 20.8, 20.6, 20.3, 20, 19.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1176);

            // Row 2: SA-182, F45, Forgings
            var row1177 = new OldStressRowData();
            row1177.LineNo = 2;
            row1177.MaterialId = 9596;
            row1177.SpecNo = "SA-182";
            row1177.TypeGrade = "F45";
            row1177.ProductForm = "Forgings";
            row1177.AlloyUNS = "S30815";
            row1177.ClassCondition = "";
            row1177.Notes = "G40, T6";
            row1177.StressValues = new double?[] { 24.9, null, 24.7, null, 22, 19.9, 18.5, 17.7, 17.4, 17.2, 17, 16.8, 16.6, 16.4, 16.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1177);

            // Row 3: SA-213, , Smls. tube
            var row1178 = new OldStressRowData();
            row1178.LineNo = 3;
            row1178.MaterialId = 9599;
            row1178.SpecNo = "SA-213";
            row1178.TypeGrade = "";
            row1178.ProductForm = "Smls. tube";
            row1178.AlloyUNS = "S30815";
            row1178.ClassCondition = "";
            row1178.Notes = "G5, G40, T5";
            row1178.StressValues = new double?[] { 24.9, null, 24.7, null, 23.3, 22.4, 21.8, 21.4, 21.2, 21, 20.8, 20.6, 20.3, 20, 19.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1178);

            // Row 4: SA-213, , Smls. tube
            var row1179 = new OldStressRowData();
            row1179.LineNo = 4;
            row1179.MaterialId = 9598;
            row1179.SpecNo = "SA-213";
            row1179.TypeGrade = "";
            row1179.ProductForm = "Smls. tube";
            row1179.AlloyUNS = "S30815";
            row1179.ClassCondition = "";
            row1179.Notes = "G40, T6";
            row1179.StressValues = new double?[] { 24.9, null, 24.7, null, 22, 19.9, 18.5, 17.7, 17.4, 17.2, 17, 16.8, 16.6, 16.4, 16.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1179);

            // Row 5: SA-240, , Plate
            var row1180 = new OldStressRowData();
            row1180.LineNo = 5;
            row1180.MaterialId = 9601;
            row1180.SpecNo = "SA-240";
            row1180.TypeGrade = "";
            row1180.ProductForm = "Plate";
            row1180.AlloyUNS = "S30815";
            row1180.ClassCondition = "";
            row1180.Notes = "G5, G40, T5";
            row1180.StressValues = new double?[] { 24.9, null, 24.7, null, 23.3, 22.4, 21.8, 21.4, 21.2, 21, 20.8, 20.6, 20.3, 20, 19.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1180);

            // Row 6: SA-240, , Plate
            var row1181 = new OldStressRowData();
            row1181.LineNo = 6;
            row1181.MaterialId = 9600;
            row1181.SpecNo = "SA-240";
            row1181.TypeGrade = "";
            row1181.ProductForm = "Plate";
            row1181.AlloyUNS = "S30815";
            row1181.ClassCondition = "";
            row1181.Notes = "G40, T6";
            row1181.StressValues = new double?[] { 24.9, null, 24.7, null, 22, 19.9, 18.5, 17.7, 17.4, 17.2, 17, 16.8, 16.6, 16.4, 16.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1181);

            // Row 7: SA-249, , Wld. tube
            var row1182 = new OldStressRowData();
            row1182.LineNo = 7;
            row1182.MaterialId = 9603;
            row1182.SpecNo = "SA-249";
            row1182.TypeGrade = "";
            row1182.ProductForm = "Wld. tube";
            row1182.AlloyUNS = "S30815";
            row1182.ClassCondition = "";
            row1182.Notes = "G5, G24, G40, T5";
            row1182.StressValues = new double?[] { 21.1, null, 21, null, 19.8, 19.1, 18.5, 18.2, 18, 17.8, 17.7, 17.5, 17.3, 17, 16.3, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1182);

            // Row 8: SA-249, , Wld. tube
            var row1183 = new OldStressRowData();
            row1183.LineNo = 8;
            row1183.MaterialId = 9602;
            row1183.SpecNo = "SA-249";
            row1183.TypeGrade = "";
            row1183.ProductForm = "Wld. tube";
            row1183.AlloyUNS = "S30815";
            row1183.ClassCondition = "";
            row1183.Notes = "G24, G40, T6";
            row1183.StressValues = new double?[] { 21.1, null, 21, null, 18.7, 16.9, 15.8, 15, 14.8, 14.6, 14.5, 14.3, 14.2, 14, 13.8, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1183);

            // Row 9: SA-312, , Smls. pipe
            var row1184 = new OldStressRowData();
            row1184.LineNo = 9;
            row1184.MaterialId = 9606;
            row1184.SpecNo = "SA-312";
            row1184.TypeGrade = "";
            row1184.ProductForm = "Smls. pipe";
            row1184.AlloyUNS = "S30815";
            row1184.ClassCondition = "";
            row1184.Notes = "G5, G40, T5";
            row1184.StressValues = new double?[] { 24.9, null, 24.7, null, 23.3, 22.4, 21.8, 21.4, 21.2, 21, 20.8, 20.6, 20.3, 20, 19.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1184);

            // Row 10: SA-312, , Smls. pipe
            var row1185 = new OldStressRowData();
            row1185.LineNo = 10;
            row1185.MaterialId = 9604;
            row1185.SpecNo = "SA-312";
            row1185.TypeGrade = "";
            row1185.ProductForm = "Smls. pipe";
            row1185.AlloyUNS = "S30815";
            row1185.ClassCondition = "";
            row1185.Notes = "G40, T6";
            row1185.StressValues = new double?[] { 24.9, null, 24.7, null, 22, 19.9, 18.5, 17.7, 17.4, 17.2, 17, 16.8, 16.6, 16.4, 16.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1185);

            // Row 11: SA-312, , Wld. pipe
            var row1186 = new OldStressRowData();
            row1186.LineNo = 11;
            row1186.MaterialId = 9607;
            row1186.SpecNo = "SA-312";
            row1186.TypeGrade = "";
            row1186.ProductForm = "Wld. pipe";
            row1186.AlloyUNS = "S30815";
            row1186.ClassCondition = "";
            row1186.Notes = "G5, G24, G40, T5";
            row1186.StressValues = new double?[] { 21.1, null, 21, null, 19.8, 19.1, 18.5, 18.2, 18, 17.8, 17.7, 17.5, 17.3, 17, 16.3, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1186);

            // Row 12: SA-312, , Wld. pipe
            var row1187 = new OldStressRowData();
            row1187.LineNo = 12;
            row1187.MaterialId = 9605;
            row1187.SpecNo = "SA-312";
            row1187.TypeGrade = "";
            row1187.ProductForm = "Wld. pipe";
            row1187.AlloyUNS = "S30815";
            row1187.ClassCondition = "";
            row1187.Notes = "G24, G40, T6";
            row1187.StressValues = new double?[] { 21.1, null, 21, null, 18.7, 16.9, 15.8, 15, 14.8, 14.6, 14.5, 14.3, 14.2, 14, 13.8, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1187);

            // Row 13: SA-479, , Bar
            var row1188 = new OldStressRowData();
            row1188.LineNo = 13;
            row1188.MaterialId = 9609;
            row1188.SpecNo = "SA-479";
            row1188.TypeGrade = "";
            row1188.ProductForm = "Bar";
            row1188.AlloyUNS = "S30815";
            row1188.ClassCondition = "";
            row1188.Notes = "G5, G40, T5";
            row1188.StressValues = new double?[] { 24.9, null, 24.7, null, 23.3, 22.4, 21.8, 21.4, 21.2, 21, 20.8, 20.6, 20.3, 20, 19.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1188);

            // Row 14: SA-479, , Bar
            var row1189 = new OldStressRowData();
            row1189.LineNo = 14;
            row1189.MaterialId = 9608;
            row1189.SpecNo = "SA-479";
            row1189.TypeGrade = "";
            row1189.ProductForm = "Bar";
            row1189.AlloyUNS = "S30815";
            row1189.ClassCondition = "";
            row1189.Notes = "G40, T6";
            row1189.StressValues = new double?[] { 24.9, null, 24.7, null, 22, 19.9, 18.5, 17.7, 17.4, 17.2, 17, 16.8, 16.6, 16.4, 16.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1189);

            // Row 15: SA-182, F51, Forgings
            var row1190 = new OldStressRowData();
            row1190.LineNo = 15;
            row1190.MaterialId = 9824;
            row1190.SpecNo = "SA-182";
            row1190.TypeGrade = "F51";
            row1190.ProductForm = "Forgings";
            row1190.AlloyUNS = "S31803";
            row1190.ClassCondition = "";
            row1190.Notes = "G32";
            row1190.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 23.9, 23.3, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1190);

            // Row 16: SA-240, , Plate
            var row1191 = new OldStressRowData();
            row1191.LineNo = 16;
            row1191.MaterialId = 9610;
            row1191.SpecNo = "SA-240";
            row1191.TypeGrade = "";
            row1191.ProductForm = "Plate";
            row1191.AlloyUNS = "S31803";
            row1191.ClassCondition = "";
            row1191.Notes = "G32";
            row1191.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 23.9, 23.3, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1191);

            // Row 17: SA-789, , Smls. tube
            var row1192 = new OldStressRowData();
            row1192.LineNo = 17;
            row1192.MaterialId = 9611;
            row1192.SpecNo = "SA-789";
            row1192.TypeGrade = "";
            row1192.ProductForm = "Smls. tube";
            row1192.AlloyUNS = "S31803";
            row1192.ClassCondition = "";
            row1192.Notes = "G32";
            row1192.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 23.9, 23.3, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1192);

            // Row 18: SA-789, , Wld. tube
            var row1193 = new OldStressRowData();
            row1193.LineNo = 18;
            row1193.MaterialId = 9612;
            row1193.SpecNo = "SA-789";
            row1193.TypeGrade = "";
            row1193.ProductForm = "Wld. tube";
            row1193.AlloyUNS = "S31803";
            row1193.ClassCondition = "";
            row1193.Notes = "G24, G32";
            row1193.StressValues = new double?[] { 21.9, null, 21.9, null, 21.1, 20.3, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1193);

            // Row 19: SA-790, , Smls. pipe
            var row1194 = new OldStressRowData();
            row1194.LineNo = 19;
            row1194.MaterialId = 9613;
            row1194.SpecNo = "SA-790";
            row1194.TypeGrade = "";
            row1194.ProductForm = "Smls. pipe";
            row1194.AlloyUNS = "S31803";
            row1194.ClassCondition = "";
            row1194.Notes = "G32";
            row1194.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 23.9, 23.3, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1194);

            // Row 20: SA-790, , Wld. pipe
            var row1195 = new OldStressRowData();
            row1195.LineNo = 20;
            row1195.MaterialId = 9614;
            row1195.SpecNo = "SA-790";
            row1195.TypeGrade = "";
            row1195.ProductForm = "Wld. pipe";
            row1195.AlloyUNS = "S31803";
            row1195.ClassCondition = "";
            row1195.Notes = "G24, G32";
            row1195.StressValues = new double?[] { 21.9, null, 21.9, null, 21.1, 20.3, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1195);

            // Row 21: SA-815, , Smls. & wld. fittings
            var row1196 = new OldStressRowData();
            row1196.LineNo = 21;
            row1196.MaterialId = 9820;
            row1196.SpecNo = "SA-815";
            row1196.TypeGrade = "";
            row1196.ProductForm = "Smls. & wld. fittings";
            row1196.AlloyUNS = "S31803";
            row1196.ClassCondition = "";
            row1196.Notes = "G32, W14";
            row1196.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 23.9, 23.3, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1196);

            // Row 22: SA-351, CG6MMN, Castings
            var row1197 = new OldStressRowData();
            row1197.LineNo = 22;
            row1197.MaterialId = 9616;
            row1197.SpecNo = "SA-351";
            row1197.TypeGrade = "CG6MMN";
            row1197.ProductForm = "Castings";
            row1197.AlloyUNS = "J93790";
            row1197.ClassCondition = "";
            row1197.Notes = "G1";
            row1197.StressValues = new double?[] { 19.4, null, 19.3, null, 17.8, 16.8, 16, 15.4, 15.2, 14.9, 14.7, 14.6, 14.4, 14.2, 14.1, 13.9, 13.6, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1197);

            // Row 23: SA-182, FXM-19, Forgings
            var row1198 = new OldStressRowData();
            row1198.LineNo = 23;
            row1198.MaterialId = 9617;
            row1198.SpecNo = "SA-182";
            row1198.TypeGrade = "FXM-19";
            row1198.ProductForm = "Forgings";
            row1198.AlloyUNS = "S20910";
            row1198.ClassCondition = "";
            row1198.Notes = "G5, T8";
            row1198.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, 23.9, 23.6, 23.2, 22.8, 22.3, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1198);

            // Row 24: SA-213, XM-19, Smls. tube
            var row1199 = new OldStressRowData();
            row1199.LineNo = 24;
            row1199.MaterialId = 9823;
            row1199.SpecNo = "SA-213";
            row1199.TypeGrade = "XM-19";
            row1199.ProductForm = "Smls. tube";
            row1199.AlloyUNS = "S20910";
            row1199.ClassCondition = "";
            row1199.Notes = "T8";
            row1199.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25, 24.6, 24.2, 23.9, 23.5, 23.3, 23, 22.7, 22.5, 22.2, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1199);

            // Row 25: SA-240, XM-19, Plate
            var row1200 = new OldStressRowData();
            row1200.LineNo = 25;
            row1200.MaterialId = 9618;
            row1200.SpecNo = "SA-240";
            row1200.TypeGrade = "XM-19";
            row1200.ProductForm = "Plate";
            row1200.AlloyUNS = "S20910";
            row1200.ClassCondition = "";
            row1200.Notes = "G5, T8";
            row1200.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, 23.9, 23.6, 23.2, 22.8, 22.3, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
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
