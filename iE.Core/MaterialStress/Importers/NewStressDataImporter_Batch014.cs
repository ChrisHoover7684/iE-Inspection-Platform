using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch014
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch014(MaterialStressService service)
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

            // Row 12: SA-213, TP310S, Smls. tube
            var row1301 = new OldStressRowData();
            row1301.LineNo = 12;
            row1301.MaterialId = 9725;
            row1301.SpecNo = "SA-213";
            row1301.TypeGrade = "TP310S";
            row1301.ProductForm = "Smls. tube";
            row1301.AlloyUNS = "S31008";
            row1301.ClassCondition = "";
            row1301.Notes = "G12, G18, T6";
            row1301.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1301);

            // Row 13: SA-240, 310S, Plate
            var row1302 = new OldStressRowData();
            row1302.LineNo = 13;
            row1302.MaterialId = 9728;
            row1302.SpecNo = "SA-240";
            row1302.TypeGrade = "310S";
            row1302.ProductForm = "Plate";
            row1302.AlloyUNS = "S31008";
            row1302.ClassCondition = "";
            row1302.Notes = "G5, G12, G18, T5";
            row1302.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1302);

            // Row 14: SA-240, 310S, Plate
            var row1303 = new OldStressRowData();
            row1303.LineNo = 14;
            row1303.MaterialId = 9729;
            row1303.SpecNo = "SA-240";
            row1303.TypeGrade = "310S";
            row1303.ProductForm = "Plate";
            row1303.AlloyUNS = "S31008";
            row1303.ClassCondition = "";
            row1303.Notes = "G12, G18, T6";
            row1303.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1303);

            // Row 15: SA-249, TP310S, Wld. tube
            var row1304 = new OldStressRowData();
            row1304.LineNo = 15;
            row1304.MaterialId = 9733;
            row1304.SpecNo = "SA-249";
            row1304.TypeGrade = "TP310S";
            row1304.ProductForm = "Wld. tube";
            row1304.AlloyUNS = "S31008";
            row1304.ClassCondition = "";
            row1304.Notes = "G5, G12, G24, T5";
            row1304.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1304);

            // Row 16: SA-249, TP310S, Wld. tube
            var row1305 = new OldStressRowData();
            row1305.LineNo = 16;
            row1305.MaterialId = 9734;
            row1305.SpecNo = "SA-249";
            row1305.TypeGrade = "TP310S";
            row1305.ProductForm = "Wld. tube";
            row1305.AlloyUNS = "S31008";
            row1305.ClassCondition = "";
            row1305.Notes = "G12, G24, T6";
            row1305.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1305);

            // Row 17: SA-312, TP310S, Smls. & wld. pipe
            var row1306 = new OldStressRowData();
            row1306.LineNo = 17;
            row1306.MaterialId = 9740;
            row1306.SpecNo = "SA-312";
            row1306.TypeGrade = "TP310S";
            row1306.ProductForm = "Smls. & wld. pipe";
            row1306.AlloyUNS = "S31008";
            row1306.ClassCondition = "";
            row1306.Notes = "G5, G12, G18, T5, W12";
            row1306.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1306);

            // Row 18: SA-312, TP310S, Smls. pipe
            var row1307 = new OldStressRowData();
            row1307.LineNo = 18;
            row1307.MaterialId = 9741;
            row1307.SpecNo = "SA-312";
            row1307.TypeGrade = "TP310S";
            row1307.ProductForm = "Smls. pipe";
            row1307.AlloyUNS = "S31008";
            row1307.ClassCondition = "";
            row1307.Notes = "G12, G18, T6";
            row1307.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1307);

            // Row 19: SA-312, TP310S, Wld. pipe
            var row1308 = new OldStressRowData();
            row1308.LineNo = 19;
            row1308.MaterialId = 9742;
            row1308.SpecNo = "SA-312";
            row1308.TypeGrade = "TP310S";
            row1308.ProductForm = "Wld. pipe";
            row1308.AlloyUNS = "S31008";
            row1308.ClassCondition = "";
            row1308.Notes = "G3, G5, G12, G14, G18, G24, T5";
            row1308.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1308);

            // Row 20: SA-312, TP310S, Wld. pipe
            var row1309 = new OldStressRowData();
            row1309.LineNo = 20;
            row1309.MaterialId = 9743;
            row1309.SpecNo = "SA-312";
            row1309.TypeGrade = "TP310S";
            row1309.ProductForm = "Wld. pipe";
            row1309.AlloyUNS = "S31008";
            row1309.ClassCondition = "";
            row1309.Notes = "G3, G12, G14, G18, G24, T6";
            row1309.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1309);

            // Row 21: SA-358, 310S, Wld. pipe
            var row1310 = new OldStressRowData();
            row1310.LineNo = 21;
            row1310.MaterialId = 9748;
            row1310.SpecNo = "SA-358";
            row1310.TypeGrade = "310S";
            row1310.ProductForm = "Wld. pipe";
            row1310.AlloyUNS = "S31008";
            row1310.ClassCondition = "1";
            row1310.Notes = "G5, W12";
            row1310.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1310);

            // Row 22: SA-479, 310S, Bar
            var row1311 = new OldStressRowData();
            row1311.LineNo = 22;
            row1311.MaterialId = 9758;
            row1311.SpecNo = "SA-479";
            row1311.TypeGrade = "310S";
            row1311.ProductForm = "Bar";
            row1311.AlloyUNS = "S31008";
            row1311.ClassCondition = "";
            row1311.Notes = "G12, G18, G22, T6";
            row1311.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1311);

            // Row 23: SA-479, 310S, Bar
            var row1312 = new OldStressRowData();
            row1312.LineNo = 23;
            row1312.MaterialId = 9757;
            row1312.SpecNo = "SA-479";
            row1312.TypeGrade = "310S";
            row1312.ProductForm = "Bar";
            row1312.AlloyUNS = "S31008";
            row1312.ClassCondition = "";
            row1312.Notes = "G5, G12, G18, G22, T5";
            row1312.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1312);

            // Row 24: SA-813, TP310S, Wld. pipe
            var row1313 = new OldStressRowData();
            row1313.LineNo = 24;
            row1313.MaterialId = 9759;
            row1313.SpecNo = "SA-813";
            row1313.TypeGrade = "TP310S";
            row1313.ProductForm = "Wld. pipe";
            row1313.AlloyUNS = "S31008";
            row1313.ClassCondition = "";
            row1313.Notes = "G5, G12, G24, T5";
            row1313.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1313);

            // Row 25: SA-813, TP310S, Wld. pipe
            var row1314 = new OldStressRowData();
            row1314.LineNo = 25;
            row1314.MaterialId = 9760;
            row1314.SpecNo = "SA-813";
            row1314.TypeGrade = "TP310S";
            row1314.ProductForm = "Wld. pipe";
            row1314.AlloyUNS = "S31008";
            row1314.ClassCondition = "";
            row1314.Notes = "G12, G24, T6";
            row1314.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1314);

            // Row 26: SA-814, TP310S, Wld. pipe
            var row1315 = new OldStressRowData();
            row1315.LineNo = 26;
            row1315.MaterialId = 9761;
            row1315.SpecNo = "SA-814";
            row1315.TypeGrade = "TP310S";
            row1315.ProductForm = "Wld. pipe";
            row1315.AlloyUNS = "S31008";
            row1315.ClassCondition = "";
            row1315.Notes = "G5, G12, G24, T5";
            row1315.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1315);

            // Row 27: SA-814, TP310S, Wld. pipe
            var row1316 = new OldStressRowData();
            row1316.LineNo = 27;
            row1316.MaterialId = 9762;
            row1316.SpecNo = "SA-814";
            row1316.TypeGrade = "TP310S";
            row1316.ProductForm = "Wld. pipe";
            row1316.AlloyUNS = "S31008";
            row1316.ClassCondition = "";
            row1316.Notes = "G12, G24, T6";
            row1316.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1316);

            // Row 28: SA-213, TP310H, Smls. tube
            var row1317 = new OldStressRowData();
            row1317.LineNo = 28;
            row1317.MaterialId = 9723;
            row1317.SpecNo = "SA-213";
            row1317.TypeGrade = "TP310H";
            row1317.ProductForm = "Smls. tube";
            row1317.AlloyUNS = "S31009";
            row1317.ClassCondition = "";
            row1317.Notes = "G5, G18, T6";
            row1317.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 16.7, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1317);

            // Row 29: SA-240, 310H, Plate
            var row1318 = new OldStressRowData();
            row1318.LineNo = 29;
            row1318.MaterialId = 9726;
            row1318.SpecNo = "SA-240";
            row1318.TypeGrade = "310H";
            row1318.ProductForm = "Plate";
            row1318.AlloyUNS = "S31009";
            row1318.ClassCondition = "";
            row1318.Notes = "G5, G18, T6";
            row1318.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 16.7, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1318);

            // Row 30: SA-240, 310H, Plate
            var row1319 = new OldStressRowData();
            row1319.LineNo = 30;
            row1319.MaterialId = 9727;
            row1319.SpecNo = "SA-240";
            row1319.TypeGrade = "310H";
            row1319.ProductForm = "Plate";
            row1319.AlloyUNS = "S31009";
            row1319.ClassCondition = "";
            row1319.Notes = "G18, T7";
            row1319.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1319);

            // Row 31: SA-249, TP310H, Wld. tube
            var row1320 = new OldStressRowData();
            row1320.LineNo = 31;
            row1320.MaterialId = 9730;
            row1320.SpecNo = "SA-249";
            row1320.TypeGrade = "TP310H";
            row1320.ProductForm = "Wld. tube";
            row1320.AlloyUNS = "S31009";
            row1320.ClassCondition = "";
            row1320.Notes = "G5, W12";
            row1320.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1320);

            // Row 32: SA-249, TP310H, Wld. tube
            var row1321 = new OldStressRowData();
            row1321.LineNo = 32;
            row1321.MaterialId = 9731;
            row1321.SpecNo = "SA-249";
            row1321.TypeGrade = "TP310H";
            row1321.ProductForm = "Wld. tube";
            row1321.AlloyUNS = "S31009";
            row1321.ClassCondition = "";
            row1321.Notes = "G5, G12, G24, T6";
            row1321.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 14.2, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1321);

            // Row 33: SA-249, TP310H, Wld. tube
            var row1322 = new OldStressRowData();
            row1322.LineNo = 33;
            row1322.MaterialId = 9732;
            row1322.SpecNo = "SA-249";
            row1322.TypeGrade = "TP310H";
            row1322.ProductForm = "Wld. tube";
            row1322.AlloyUNS = "S31009";
            row1322.ClassCondition = "";
            row1322.Notes = "G12, G24, T7";
            row1322.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 10.3, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1322);

            // Row 34: SA-312, TP310H, Smls. pipe
            var row1323 = new OldStressRowData();
            row1323.LineNo = 34;
            row1323.MaterialId = 9735;
            row1323.SpecNo = "SA-312";
            row1323.TypeGrade = "TP310H";
            row1323.ProductForm = "Smls. pipe";
            row1323.AlloyUNS = "S31009";
            row1323.ClassCondition = "";
            row1323.Notes = "G5, G18, T6";
            row1323.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 16.7, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1323);

            // Row 35: SA-312, TP310H, Smls. pipe
            var row1324 = new OldStressRowData();
            row1324.LineNo = 35;
            row1324.MaterialId = 9736;
            row1324.SpecNo = "SA-312";
            row1324.TypeGrade = "TP310H";
            row1324.ProductForm = "Smls. pipe";
            row1324.AlloyUNS = "S31009";
            row1324.ClassCondition = "";
            row1324.Notes = "G18, T7";
            row1324.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1324);

            // Row 36: SA-312, TP310H, Wld. pipe
            var row1325 = new OldStressRowData();
            row1325.LineNo = 36;
            row1325.MaterialId = 9737;
            row1325.SpecNo = "SA-312";
            row1325.TypeGrade = "TP310H";
            row1325.ProductForm = "Wld. pipe";
            row1325.AlloyUNS = "S31009";
            row1325.ClassCondition = "";
            row1325.Notes = "G3, G5, G18, G24, T6";
            row1325.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 14.2, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1325);

            // Row 37: SA-312, TP310H, Wld. pipe
            var row1326 = new OldStressRowData();
            row1326.LineNo = 37;
            row1326.MaterialId = 9738;
            row1326.SpecNo = "SA-312";
            row1326.TypeGrade = "TP310H";
            row1326.ProductForm = "Wld. pipe";
            row1326.AlloyUNS = "S31009";
            row1326.ClassCondition = "";
            row1326.Notes = "G3, G18, G24, T7";
            row1326.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 10.3, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1326);

            // Row 38: SA-479, 310H, Bar
            var row1327 = new OldStressRowData();
            row1327.LineNo = 38;
            row1327.MaterialId = 9755;
            row1327.SpecNo = "SA-479";
            row1327.TypeGrade = "310H";
            row1327.ProductForm = "Bar";
            row1327.AlloyUNS = "S31009";
            row1327.ClassCondition = "";
            row1327.Notes = "G5, G18, T6";
            row1327.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 16.7, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1327);

            // Row 39: SA-479, 310H, Bar
            var row1328 = new OldStressRowData();
            row1328.LineNo = 39;
            row1328.MaterialId = 9756;
            row1328.SpecNo = "SA-479";
            row1328.TypeGrade = "310H";
            row1328.ProductForm = "Bar";
            row1328.AlloyUNS = "S31009";
            row1328.ClassCondition = "";
            row1328.Notes = "G18, T7";
            row1328.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1328);

            // Row 1: SA-213, TP310Cb, Smls. tube
            var row1329 = new OldStressRowData();
            row1329.LineNo = 1;
            row1329.MaterialId = 16975;
            row1329.SpecNo = "SA-213";
            row1329.TypeGrade = "TP310Cb";
            row1329.ProductForm = "Smls. tube";
            row1329.AlloyUNS = "S31040";
            row1329.ClassCondition = "";
            row1329.Notes = "G5, G12, T5";
            row1329.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1329);

            // Row 2: SA-213, TP310Cb, Smls. tube
            var row1330 = new OldStressRowData();
            row1330.LineNo = 2;
            row1330.MaterialId = 16970;
            row1330.SpecNo = "SA-213";
            row1330.TypeGrade = "TP310Cb";
            row1330.ProductForm = "Smls. tube";
            row1330.AlloyUNS = "S31040";
            row1330.ClassCondition = "";
            row1330.Notes = "G12, T6";
            row1330.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1330);

            // Row 3: SA-240, 310Cb, Plate
            var row1331 = new OldStressRowData();
            row1331.LineNo = 3;
            row1331.MaterialId = 16971;
            row1331.SpecNo = "SA-240";
            row1331.TypeGrade = "310Cb";
            row1331.ProductForm = "Plate";
            row1331.AlloyUNS = "S31040";
            row1331.ClassCondition = "";
            row1331.Notes = "G5, G12, T5";
            row1331.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1331);

            // Row 4: SA-240, 310Cb, Plate
            var row1332 = new OldStressRowData();
            row1332.LineNo = 4;
            row1332.MaterialId = 16972;
            row1332.SpecNo = "SA-240";
            row1332.TypeGrade = "310Cb";
            row1332.ProductForm = "Plate";
            row1332.AlloyUNS = "S31040";
            row1332.ClassCondition = "";
            row1332.Notes = "G12, T6";
            row1332.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1332);

            // Row 5: SA-249, TP310Cb, Wld. tube
            var row1333 = new OldStressRowData();
            row1333.LineNo = 5;
            row1333.MaterialId = 16973;
            row1333.SpecNo = "SA-249";
            row1333.TypeGrade = "TP310Cb";
            row1333.ProductForm = "Wld. tube";
            row1333.AlloyUNS = "S31040";
            row1333.ClassCondition = "";
            row1333.Notes = "G5, G12, G24, T5";
            row1333.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1333);

            // Row 6: SA-249, TP310Cb, Wld. tube
            var row1334 = new OldStressRowData();
            row1334.LineNo = 6;
            row1334.MaterialId = 16974;
            row1334.SpecNo = "SA-249";
            row1334.TypeGrade = "TP310Cb";
            row1334.ProductForm = "Wld. tube";
            row1334.AlloyUNS = "S31040";
            row1334.ClassCondition = "";
            row1334.Notes = "G12, G24, T6";
            row1334.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1334);

            // Row 7: SA-312, TP310Cb, Smls. & wld. pipe
            var row1335 = new OldStressRowData();
            row1335.LineNo = 7;
            row1335.MaterialId = 9769;
            row1335.SpecNo = "SA-312";
            row1335.TypeGrade = "TP310Cb";
            row1335.ProductForm = "Smls. & wld. pipe";
            row1335.AlloyUNS = "S31040";
            row1335.ClassCondition = "";
            row1335.Notes = "G5, G12, T5, W12";
            row1335.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1335);

            // Row 8: SA-312, TP310Cb, Smls. pipe
            var row1336 = new OldStressRowData();
            row1336.LineNo = 8;
            row1336.MaterialId = 9770;
            row1336.SpecNo = "SA-312";
            row1336.TypeGrade = "TP310Cb";
            row1336.ProductForm = "Smls. pipe";
            row1336.AlloyUNS = "S31040";
            row1336.ClassCondition = "";
            row1336.Notes = "G12, T6";
            row1336.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1336);

            // Row 9: SA-312, TP310Cb, Wld. pipe
            var row1337 = new OldStressRowData();
            row1337.LineNo = 9;
            row1337.MaterialId = 9771;
            row1337.SpecNo = "SA-312";
            row1337.TypeGrade = "TP310Cb";
            row1337.ProductForm = "Wld. pipe";
            row1337.AlloyUNS = "S31040";
            row1337.ClassCondition = "";
            row1337.Notes = "G5, G12, G24, T5";
            row1337.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1337);

            // Row 10: SA-312, TP310Cb, Wld. pipe
            var row1338 = new OldStressRowData();
            row1338.LineNo = 10;
            row1338.MaterialId = 9772;
            row1338.SpecNo = "SA-312";
            row1338.TypeGrade = "TP310Cb";
            row1338.ProductForm = "Wld. pipe";
            row1338.AlloyUNS = "S31040";
            row1338.ClassCondition = "";
            row1338.Notes = "G12, G14, G24, T6";
            row1338.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1338);

            // Row 11: SA-479, 310Cb, Bar
            var row1339 = new OldStressRowData();
            row1339.LineNo = 11;
            row1339.MaterialId = 9773;
            row1339.SpecNo = "SA-479";
            row1339.TypeGrade = "310Cb";
            row1339.ProductForm = "Bar";
            row1339.AlloyUNS = "S31040";
            row1339.ClassCondition = "";
            row1339.Notes = "G5, G12, G22, T5";
            row1339.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1339);

            // Row 12: SA-479, 310Cb, Bar
            var row1340 = new OldStressRowData();
            row1340.LineNo = 12;
            row1340.MaterialId = 9774;
            row1340.SpecNo = "SA-479";
            row1340.TypeGrade = "310Cb";
            row1340.ProductForm = "Bar";
            row1340.AlloyUNS = "S31040";
            row1340.ClassCondition = "";
            row1340.Notes = "G12, G22, T6";
            row1340.StressValues = new double?[] { 20, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1340);

            // Row 13: SA-813, TP310Cb, Wld. pipe
            var row1341 = new OldStressRowData();
            row1341.LineNo = 13;
            row1341.MaterialId = 9775;
            row1341.SpecNo = "SA-813";
            row1341.TypeGrade = "TP310Cb";
            row1341.ProductForm = "Wld. pipe";
            row1341.AlloyUNS = "S31040";
            row1341.ClassCondition = "";
            row1341.Notes = "G5, G12, G24, T5";
            row1341.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1341);

            // Row 14: SA-813, TP310Cb, Wld. pipe
            var row1342 = new OldStressRowData();
            row1342.LineNo = 14;
            row1342.MaterialId = 9776;
            row1342.SpecNo = "SA-813";
            row1342.TypeGrade = "TP310Cb";
            row1342.ProductForm = "Wld. pipe";
            row1342.AlloyUNS = "S31040";
            row1342.ClassCondition = "";
            row1342.Notes = "G12, G24, T6";
            row1342.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1342);

            // Row 15: SA-814, TP310Cb, Wld. pipe
            var row1343 = new OldStressRowData();
            row1343.LineNo = 15;
            row1343.MaterialId = 9777;
            row1343.SpecNo = "SA-814";
            row1343.TypeGrade = "TP310Cb";
            row1343.ProductForm = "Wld. pipe";
            row1343.AlloyUNS = "S31040";
            row1343.ClassCondition = "";
            row1343.Notes = "G5, G12, G24, T5";
            row1343.StressValues = new double?[] { 17, null, 17, null, 17, 16.9, 16.4, 15.7, 15.5, 15.2, 15, 14.8, 14.6, 14.4, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1343);

            // Row 16: SA-814, TP310Cb, Wld. pipe
            var row1344 = new OldStressRowData();
            row1344.LineNo = 16;
            row1344.MaterialId = 9836;
            row1344.SpecNo = "SA-814";
            row1344.TypeGrade = "TP310Cb";
            row1344.ProductForm = "Wld. pipe";
            row1344.AlloyUNS = "S31040";
            row1344.ClassCondition = "";
            row1344.Notes = "G12, G24, T6";
            row1344.StressValues = new double?[] { 17, null, 15, null, 13.7, 12.8, 12.1, 11.7, 11.5, 11.3, 11.1, 11, 10.8, 10.7, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1344);

            // Row 17: SA-213, TP310MoLN, Smls. tube
            var row1345 = new OldStressRowData();
            row1345.LineNo = 17;
            row1345.MaterialId = 9778;
            row1345.SpecNo = "SA-213";
            row1345.TypeGrade = "TP310MoLN";
            row1345.ProductForm = "Smls. tube";
            row1345.AlloyUNS = "S31050";
            row1345.ClassCondition = "";
            row1345.Notes = "G5";
            row1345.StressValues = new double?[] { 22.3, null, 22, null, 20.8, 20, 19.5, 19, 18.8, 18.7, 18.5, 18.4, 18.2, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1345);

            // Row 18: SA-213, TP310MoLN, Smls. tube
            var row1346 = new OldStressRowData();
            row1346.LineNo = 18;
            row1346.MaterialId = 9825;
            row1346.SpecNo = "SA-213";
            row1346.TypeGrade = "TP310MoLN";
            row1346.ProductForm = "Smls. tube";
            row1346.AlloyUNS = "S31050";
            row1346.ClassCondition = "";
            row1346.Notes = "";
            row1346.StressValues = new double?[] { 22.3, null, 21, null, 19.1, 17.8, 16.8, 15.9, 15.5, 15.1, 14.8, 14.4, 14, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1346);

            // Row 19: SA-249, TP310MoLN, Wld. tube
            var row1347 = new OldStressRowData();
            row1347.LineNo = 19;
            row1347.MaterialId = 9826;
            row1347.SpecNo = "SA-249";
            row1347.TypeGrade = "TP310MoLN";
            row1347.ProductForm = "Wld. tube";
            row1347.AlloyUNS = "S31050";
            row1347.ClassCondition = "";
            row1347.Notes = "G5, G24";
            row1347.StressValues = new double?[] { 18.9, null, 18.7, null, 17.7, 17, 16.5, 16.2, 16, 15.9, 15.8, 15.6, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1347);

            // Row 20: SA-249, TP310MoLN, Wld. tube
            var row1348 = new OldStressRowData();
            row1348.LineNo = 20;
            row1348.MaterialId = 9827;
            row1348.SpecNo = "SA-249";
            row1348.TypeGrade = "TP310MoLN";
            row1348.ProductForm = "Wld. tube";
            row1348.AlloyUNS = "S31050";
            row1348.ClassCondition = "";
            row1348.Notes = "G24";
            row1348.StressValues = new double?[] { 18.9, null, 17.8, null, 16.2, 15.1, 14.3, 13.5, 13.2, 12.9, 12.5, 12.2, 11.9, 11.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1348);

            // Row 21: SA-312, TP310MoLN, Wld. pipe
            var row1349 = new OldStressRowData();
            row1349.LineNo = 21;
            row1349.MaterialId = 9828;
            row1349.SpecNo = "SA-312";
            row1349.TypeGrade = "TP310MoLN";
            row1349.ProductForm = "Wld. pipe";
            row1349.AlloyUNS = "S31050";
            row1349.ClassCondition = "";
            row1349.Notes = "G5, G24";
            row1349.StressValues = new double?[] { 18.9, null, 18.7, null, 17.7, 17, 16.5, 16.2, 16, 15.9, 15.8, 15.6, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1349);

            // Row 22: SA-312, TP310MoLN, Wld. pipe
            var row1350 = new OldStressRowData();
            row1350.LineNo = 22;
            row1350.MaterialId = 9829;
            row1350.SpecNo = "SA-312";
            row1350.TypeGrade = "TP310MoLN";
            row1350.ProductForm = "Wld. pipe";
            row1350.AlloyUNS = "S31050";
            row1350.ClassCondition = "";
            row1350.Notes = "G24";
            row1350.StressValues = new double?[] { 18.9, null, 17.8, null, 16.2, 15.1, 14.3, 13.5, 13.2, 12.9, 12.5, 12.2, 11.9, 11.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1350);

            // Row 23: SA-240, 310MoLN, Plate
            var row1351 = new OldStressRowData();
            row1351.LineNo = 23;
            row1351.MaterialId = 9830;
            row1351.SpecNo = "SA-240";
            row1351.TypeGrade = "310MoLN";
            row1351.ProductForm = "Plate";
            row1351.AlloyUNS = "S31050";
            row1351.ClassCondition = "";
            row1351.Notes = "G5";
            row1351.StressValues = new double?[] { 22.9, null, 22.6, null, 21.4, 20.5, 20, 19.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1351);

            // Row 24: SA-240, 310MoLN, Plate
            var row1352 = new OldStressRowData();
            row1352.LineNo = 24;
            row1352.MaterialId = 9831;
            row1352.SpecNo = "SA-240";
            row1352.TypeGrade = "310MoLN";
            row1352.ProductForm = "Plate";
            row1352.AlloyUNS = "S31050";
            row1352.ClassCondition = "";
            row1352.Notes = "";
            row1352.StressValues = new double?[] { 22.9, null, 19.9, null, 18.1, 16.8, 15.9, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1352);

            // Row 25: SA-213, TP310MoLN, Smls. tube
            var row1353 = new OldStressRowData();
            row1353.LineNo = 25;
            row1353.MaterialId = 9832;
            row1353.SpecNo = "SA-213";
            row1353.TypeGrade = "TP310MoLN";
            row1353.ProductForm = "Smls. tube";
            row1353.AlloyUNS = "S31050";
            row1353.ClassCondition = "";
            row1353.Notes = "G5";
            row1353.StressValues = new double?[] { 24, null, 23.7, null, 22.4, 21.6, 21, 20.5, 20.3, 20.1, 20, 19.8, 19.6, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1353);

            // Row 26: SA-213, TP310MoLN, Smls. tube
            var row1354 = new OldStressRowData();
            row1354.LineNo = 26;
            row1354.MaterialId = 9833;
            row1354.SpecNo = "SA-213";
            row1354.TypeGrade = "TP310MoLN";
            row1354.ProductForm = "Smls. tube";
            row1354.AlloyUNS = "S31050";
            row1354.ClassCondition = "";
            row1354.Notes = "";
            row1354.StressValues = new double?[] { 24, null, 22.1, null, 20.1, 18.7, 17.7, 16.8, 16.4, 16, 15.6, 15.2, 14.8, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1354);

            // Row 27: SA-249, TP310MoLN, Wld. tube
            var row1355 = new OldStressRowData();
            row1355.LineNo = 27;
            row1355.MaterialId = 9835;
            row1355.SpecNo = "SA-249";
            row1355.TypeGrade = "TP310MoLN";
            row1355.ProductForm = "Wld. tube";
            row1355.AlloyUNS = "S31050";
            row1355.ClassCondition = "";
            row1355.Notes = "G5, G24";
            row1355.StressValues = new double?[] { 20.4, null, 20.2, null, 19.1, 18.3, 17.8, 17.4, 17.3, 17.1, 17, 16.8, 16.6, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1355);

            // Row 28: SA-249, TP310MoLN, Wld. tube
            var row1356 = new OldStressRowData();
            row1356.LineNo = 28;
            row1356.MaterialId = 9834;
            row1356.SpecNo = "SA-249";
            row1356.TypeGrade = "TP310MoLN";
            row1356.ProductForm = "Wld. tube";
            row1356.AlloyUNS = "S31050";
            row1356.ClassCondition = "";
            row1356.Notes = "G24";
            row1356.StressValues = new double?[] { 20.4, null, 18.8, null, 17.1, 15.9, 15, 14.3, 13.9, 13.6, 13.2, 12.9, 12.6, 12.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1356);

            // Row 29: SA-312, TP310MoLN, Wld. pipe
            var row1357 = new OldStressRowData();
            row1357.LineNo = 29;
            row1357.MaterialId = 9837;
            row1357.SpecNo = "SA-312";
            row1357.TypeGrade = "TP310MoLN";
            row1357.ProductForm = "Wld. pipe";
            row1357.AlloyUNS = "S31050";
            row1357.ClassCondition = "";
            row1357.Notes = "G5, G24";
            row1357.StressValues = new double?[] { 20.4, null, 20.2, null, 19.1, 18.3, 17.8, 17.4, 17.3, 17.1, 17, 16.8, 16.6, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1357);

            // Row 30: SA-312, TP310MoLN, Wld. pipe
            var row1358 = new OldStressRowData();
            row1358.LineNo = 30;
            row1358.MaterialId = 9838;
            row1358.SpecNo = "SA-312";
            row1358.TypeGrade = "TP310MoLN";
            row1358.ProductForm = "Wld. pipe";
            row1358.AlloyUNS = "S31050";
            row1358.ClassCondition = "";
            row1358.Notes = "G24";
            row1358.StressValues = new double?[] { 20.4, null, 18.8, null, 17.1, 15.9, 15, 14.3, 13.9, 13.6, 13.2, 12.9, 12.6, 12.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1358);

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
