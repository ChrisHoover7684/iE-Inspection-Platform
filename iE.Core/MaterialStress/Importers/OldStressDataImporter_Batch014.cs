using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch014
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

            // Row 20: SA-312, TP317L, Wld. pipe
            var row1301 = new OldStressRowData();
            row1301.LineNo = 20;
            row1301.MaterialId = 9796;
            row1301.SpecNo = "SA-312";
            row1301.TypeGrade = "TP317L";
            row1301.ProductForm = "Wld. pipe";
            row1301.AlloyUNS = "S31703";
            row1301.ClassCondition = "";
            row1301.Notes = "G24";
            row1301.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1301);

            // Row 21: ..., , 
            var row1302 = new OldStressRowData();
            row1302.LineNo = 21;
            row1302.MaterialId = 9537;
            row1302.SpecNo = "...";
            row1302.TypeGrade = "";
            row1302.ProductForm = "";
            row1302.AlloyUNS = "";
            row1302.ClassCondition = "";
            row1302.Notes = "";
            row1302.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1302);

            // Row 22: SA-403, 317, Smls. & wld. fittings
            var row1303 = new OldStressRowData();
            row1303.LineNo = 22;
            row1303.MaterialId = 9540;
            row1303.SpecNo = "SA-403";
            row1303.TypeGrade = "317";
            row1303.ProductForm = "Smls. & wld. fittings";
            row1303.AlloyUNS = "S31700";
            row1303.ClassCondition = "";
            row1303.Notes = "G5, G12, W15";
            row1303.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1303);

            // Row 23: ..., , 
            var row1304 = new OldStressRowData();
            row1304.LineNo = 23;
            row1304.MaterialId = 9538;
            row1304.SpecNo = "...";
            row1304.TypeGrade = "";
            row1304.ProductForm = "";
            row1304.AlloyUNS = "";
            row1304.ClassCondition = "";
            row1304.Notes = "";
            row1304.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1304);

            // Row 24: ..., , 
            var row1305 = new OldStressRowData();
            row1305.LineNo = 24;
            row1305.MaterialId = 9539;
            row1305.SpecNo = "...";
            row1305.TypeGrade = "";
            row1305.ProductForm = "";
            row1305.AlloyUNS = "";
            row1305.ClassCondition = "";
            row1305.Notes = "";
            row1305.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1305);

            // Row 25: SA-403, 317L, Fittings
            var row1306 = new OldStressRowData();
            row1306.LineNo = 25;
            row1306.MaterialId = 9797;
            row1306.SpecNo = "SA-403";
            row1306.TypeGrade = "317L";
            row1306.ProductForm = "Fittings";
            row1306.AlloyUNS = "S31703";
            row1306.ClassCondition = "CR";
            row1306.Notes = "G5, G24";
            row1306.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1306);

            // Row 26: SA-403, 317L, Fittings
            var row1307 = new OldStressRowData();
            row1307.LineNo = 26;
            row1307.MaterialId = 9798;
            row1307.SpecNo = "SA-403";
            row1307.TypeGrade = "317L";
            row1307.ProductForm = "Fittings";
            row1307.AlloyUNS = "S31703";
            row1307.ClassCondition = "CR";
            row1307.Notes = "G24";
            row1307.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1307);

            // Row 27: SA-403, 317L, Fittings
            var row1308 = new OldStressRowData();
            row1308.LineNo = 27;
            row1308.MaterialId = 9541;
            row1308.SpecNo = "SA-403";
            row1308.TypeGrade = "317L";
            row1308.ProductForm = "Fittings";
            row1308.AlloyUNS = "S31703";
            row1308.ClassCondition = "WP-S";
            row1308.Notes = "G5";
            row1308.StressValues = new double?[] { 18.8, null, 18.1, null, 17.1, 16.7, 16.5, 16.5, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1308);

            // Row 28: SA-403, 317L, Fittings
            var row1309 = new OldStressRowData();
            row1309.LineNo = 28;
            row1309.MaterialId = 9799;
            row1309.SpecNo = "SA-403";
            row1309.TypeGrade = "317L";
            row1309.ProductForm = "Fittings";
            row1309.AlloyUNS = "S31703";
            row1309.ClassCondition = "WP-S";
            row1309.Notes = "";
            row1309.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, 13.1, 12.5, 12.3, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1309);

            // Row 29: SA-403, 317L, Fittings
            var row1310 = new OldStressRowData();
            row1310.LineNo = 29;
            row1310.MaterialId = 9800;
            row1310.SpecNo = "SA-403";
            row1310.TypeGrade = "317L";
            row1310.ProductForm = "Fittings";
            row1310.AlloyUNS = "S31703";
            row1310.ClassCondition = "WP-W";
            row1310.Notes = "G5, G24";
            row1310.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1310);

            // Row 30: SA-403, 317L, Fittings
            var row1311 = new OldStressRowData();
            row1311.LineNo = 30;
            row1311.MaterialId = 9801;
            row1311.SpecNo = "SA-403";
            row1311.TypeGrade = "317L";
            row1311.ProductForm = "Fittings";
            row1311.AlloyUNS = "S31703";
            row1311.ClassCondition = "WP-W";
            row1311.Notes = "G24";
            row1311.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1311);

            // Row 31: SA-403, 317L, Fittings
            var row1312 = new OldStressRowData();
            row1312.LineNo = 31;
            row1312.MaterialId = 9802;
            row1312.SpecNo = "SA-403";
            row1312.TypeGrade = "317L";
            row1312.ProductForm = "Fittings";
            row1312.AlloyUNS = "S31703";
            row1312.ClassCondition = "WP-WU";
            row1312.Notes = "G5, G24";
            row1312.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1312);

            // Row 32: SA-403, 317L, Fittings
            var row1313 = new OldStressRowData();
            row1313.LineNo = 32;
            row1313.MaterialId = 9803;
            row1313.SpecNo = "SA-403";
            row1313.TypeGrade = "317L";
            row1313.ProductForm = "Fittings";
            row1313.AlloyUNS = "S31703";
            row1313.ClassCondition = "WP-WU";
            row1313.Notes = "G24";
            row1313.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1313);

            // Row 33: SA-403, 317L, Fittings
            var row1314 = new OldStressRowData();
            row1314.LineNo = 33;
            row1314.MaterialId = 9804;
            row1314.SpecNo = "SA-403";
            row1314.TypeGrade = "317L";
            row1314.ProductForm = "Fittings";
            row1314.AlloyUNS = "S31703";
            row1314.ClassCondition = "WP-WX";
            row1314.Notes = "G5, G24";
            row1314.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1314);

            // Row 34: SA-403, 317L, Fittings
            var row1315 = new OldStressRowData();
            row1315.LineNo = 34;
            row1315.MaterialId = 9805;
            row1315.SpecNo = "SA-403";
            row1315.TypeGrade = "317L";
            row1315.ProductForm = "Fittings";
            row1315.AlloyUNS = "S31703";
            row1315.ClassCondition = "WP-WX";
            row1315.Notes = "G24";
            row1315.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1315);

            // Row 35: SA-213, XM-15, Smls. tube
            var row1316 = new OldStressRowData();
            row1316.LineNo = 35;
            row1316.MaterialId = 9542;
            row1316.SpecNo = "SA-213";
            row1316.TypeGrade = "XM-15";
            row1316.ProductForm = "Smls. tube";
            row1316.AlloyUNS = "S38100";
            row1316.ClassCondition = "";
            row1316.Notes = "G5";
            row1316.StressValues = new double?[] { 18.8, null, 17.7, null, 16.6, 16.1, 15.9, 15.9, 15.9, 15.9, 15.5, 15.1, 14.9, 14.6, 14.3, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1316);

            // Row 36: SA-213, XM-15, Smls. tube
            var row1317 = new OldStressRowData();
            row1317.LineNo = 36;
            row1317.MaterialId = 9543;
            row1317.SpecNo = "SA-213";
            row1317.TypeGrade = "XM-15";
            row1317.ProductForm = "Smls. tube";
            row1317.AlloyUNS = "S38100";
            row1317.ClassCondition = "";
            row1317.Notes = "";
            row1317.StressValues = new double?[] { 18.8, null, 15.6, null, 14, 12.9, 12.1, 11.4, 11.2, 11, 10.8, 10.5, 10.3, 10.1, 9.9, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1317);

            // Row 37: SA-240, XM-15, Plate
            var row1318 = new OldStressRowData();
            row1318.LineNo = 37;
            row1318.MaterialId = 9544;
            row1318.SpecNo = "SA-240";
            row1318.TypeGrade = "XM-15";
            row1318.ProductForm = "Plate";
            row1318.AlloyUNS = "S38100";
            row1318.ClassCondition = "";
            row1318.Notes = "G5";
            row1318.StressValues = new double?[] { 18.8, null, 17.7, null, 16.6, 16.1, 15.9, 15.9, 15.9, 15.9, 15.5, 15.1, 14.9, 14.6, 14.3, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1318);

            // Row 38: SA-240, XM-15, Plate
            var row1319 = new OldStressRowData();
            row1319.LineNo = 38;
            row1319.MaterialId = 9545;
            row1319.SpecNo = "SA-240";
            row1319.TypeGrade = "XM-15";
            row1319.ProductForm = "Plate";
            row1319.AlloyUNS = "S38100";
            row1319.ClassCondition = "";
            row1319.Notes = "";
            row1319.StressValues = new double?[] { 18.8, null, 15.6, null, 14, 12.9, 12.1, 11.4, 11.2, 11, 10.8, 10.5, 10.3, 10.1, 9.9, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1319);

            // Row 1: SA-249, TPXM-15, Wld. tube
            var row1320 = new OldStressRowData();
            row1320.LineNo = 1;
            row1320.MaterialId = 9546;
            row1320.SpecNo = "SA-249";
            row1320.TypeGrade = "TPXM-15";
            row1320.ProductForm = "Wld. tube";
            row1320.AlloyUNS = "S38100";
            row1320.ClassCondition = "";
            row1320.Notes = "G5, G24";
            row1320.StressValues = new double?[] { 15.9, null, 15.1, null, 14.1, 13.7, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.6, 12.4, 12.2, 11.7, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1320);

            // Row 2: SA-249, TPXM-15, Wld. tube
            var row1321 = new OldStressRowData();
            row1321.LineNo = 2;
            row1321.MaterialId = 9547;
            row1321.SpecNo = "SA-249";
            row1321.TypeGrade = "TPXM-15";
            row1321.ProductForm = "Wld. tube";
            row1321.AlloyUNS = "S38100";
            row1321.ClassCondition = "";
            row1321.Notes = "G24";
            row1321.StressValues = new double?[] { 15.9, null, 13.3, null, 11.9, 11, 10.3, 9.7, 9.5, 9.4, 9.2, 8.9, 8.8, 8.6, 8.4, 8.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1321);

            // Row 3: SA-312, TPXM-15, Wld. tube
            var row1322 = new OldStressRowData();
            row1322.LineNo = 3;
            row1322.MaterialId = 9548;
            row1322.SpecNo = "SA-312";
            row1322.TypeGrade = "TPXM-15";
            row1322.ProductForm = "Wld. tube";
            row1322.AlloyUNS = "S38100";
            row1322.ClassCondition = "";
            row1322.Notes = "G5, G24";
            row1322.StressValues = new double?[] { 15.9, null, 15.1, null, 14.1, 13.7, 13.5, 13.5, 13.5, 13.5, 13.2, 12.9, 12.6, 12.4, 12.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1322);

            // Row 4: SA-312, TPXM-15, Wld. tube
            var row1323 = new OldStressRowData();
            row1323.LineNo = 4;
            row1323.MaterialId = 9549;
            row1323.SpecNo = "SA-312";
            row1323.TypeGrade = "TPXM-15";
            row1323.ProductForm = "Wld. tube";
            row1323.AlloyUNS = "S38100";
            row1323.ClassCondition = "";
            row1323.Notes = "G24";
            row1323.StressValues = new double?[] { 15.9, null, 13.3, null, 11.9, 11, 10.3, 9.7, 9.5, 9.4, 9.2, 8.9, 8.8, 8.6, 8.4, 8.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1323);

            // Row 5: SA-351, CF10, Castings
            var row1324 = new OldStressRowData();
            row1324.LineNo = 5;
            row1324.MaterialId = 9550;
            row1324.SpecNo = "SA-351";
            row1324.TypeGrade = "CF10";
            row1324.ProductForm = "Castings";
            row1324.AlloyUNS = "J92590";
            row1324.ClassCondition = "";
            row1324.Notes = "G1, G5, G32";
            row1324.StressValues = new double?[] { 17.5, null, 16.6, null, 15.5, 15.1, 14.8, 14.8, 14.8, 14.8, 14.7, 14.6, 14.4, 14.2, 13.9, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row1324);

            // Row 6: SA-351, CF10, Castings
            var row1325 = new OldStressRowData();
            row1325.LineNo = 6;
            row1325.MaterialId = 9551;
            row1325.SpecNo = "SA-351";
            row1325.TypeGrade = "CF10";
            row1325.ProductForm = "Castings";
            row1325.AlloyUNS = "J92590";
            row1325.ClassCondition = "";
            row1325.Notes = "G1, G32";
            row1325.StressValues = new double?[] { 17.5, null, 16.6, null, 15, 13.8, 12.9, 12.1, 12, 11.8, 11.5, 11.2, 11, 10.9, 10.7, 10.4, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row1325);

            // Row 7: SA-351, CF10M, Castings
            var row1326 = new OldStressRowData();
            row1326.LineNo = 7;
            row1326.MaterialId = 9552;
            row1326.SpecNo = "SA-351";
            row1326.TypeGrade = "CF10M";
            row1326.ProductForm = "Castings";
            row1326.AlloyUNS = "";
            row1326.ClassCondition = "";
            row1326.Notes = "G1, G5, G32";
            row1326.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.6, 16.3, 16, 15.8, 15.7, 15.5, 15.4, 14.9, 11.5, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row1326);

            // Row 8: SA-351, CF10M, Castings
            var row1327 = new OldStressRowData();
            row1327.LineNo = 8;
            row1327.MaterialId = 9553;
            row1327.SpecNo = "SA-351";
            row1327.TypeGrade = "CF10M";
            row1327.ProductForm = "Castings";
            row1327.AlloyUNS = "";
            row1327.ClassCondition = "";
            row1327.Notes = "G1, G32";
            row1327.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row1327);

            // Row 9: SA-351, CG8M, Castings
            var row1328 = new OldStressRowData();
            row1328.LineNo = 9;
            row1328.MaterialId = 9554;
            row1328.SpecNo = "SA-351";
            row1328.TypeGrade = "CG8M";
            row1328.ProductForm = "Castings";
            row1328.AlloyUNS = "J93000";
            row1328.ClassCondition = "";
            row1328.Notes = "G1, G5, G32";
            row1328.StressValues = new double?[] { 18.8, null, 18.2, null, 17.2, 16.7, 16.5, 16.5, 16.5, 16.5, 16.5, 16.4, 16.2, 16, 15.7, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1328);

            // Row 10: SA-351, CG8M, Castings
            var row1329 = new OldStressRowData();
            row1329.LineNo = 10;
            row1329.MaterialId = 9555;
            row1329.SpecNo = "SA-351";
            row1329.TypeGrade = "CG8M";
            row1329.ProductForm = "Castings";
            row1329.AlloyUNS = "J93000";
            row1329.ClassCondition = "";
            row1329.Notes = "G1, G32";
            row1329.StressValues = new double?[] { 18.8, null, 18.2, null, 16.4, 14.7, 13.6, 12.9, 12.7, 12.5, 12.3, 12.2, 12, 11.8, 11.6, 11.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1329);

            // Row 11: SA-213, , Smls. tube
            var row1330 = new OldStressRowData();
            row1330.LineNo = 11;
            row1330.MaterialId = 9556;
            row1330.SpecNo = "SA-213";
            row1330.TypeGrade = "";
            row1330.ProductForm = "Smls. tube";
            row1330.AlloyUNS = "S31725";
            row1330.ClassCondition = "";
            row1330.Notes = "G5";
            row1330.StressValues = new double?[] { 18.8, null, 18.2, null, 17.1, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1330);

            // Row 12: SA-213, , Smls. tube
            var row1331 = new OldStressRowData();
            row1331.LineNo = 12;
            row1331.MaterialId = 9557;
            row1331.SpecNo = "SA-213";
            row1331.TypeGrade = "";
            row1331.ProductForm = "Smls. tube";
            row1331.AlloyUNS = "S31725";
            row1331.ClassCondition = "";
            row1331.Notes = "";
            row1331.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1331);

            // Row 13: SA-240, , Plate
            var row1332 = new OldStressRowData();
            row1332.LineNo = 13;
            row1332.MaterialId = 9558;
            row1332.SpecNo = "SA-240";
            row1332.TypeGrade = "";
            row1332.ProductForm = "Plate";
            row1332.AlloyUNS = "S31725";
            row1332.ClassCondition = "";
            row1332.Notes = "G5";
            row1332.StressValues = new double?[] { 18.8, null, 18.2, null, 17.1, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1332);

            // Row 14: SA-240, , Plate
            var row1333 = new OldStressRowData();
            row1333.LineNo = 14;
            row1333.MaterialId = 9559;
            row1333.SpecNo = "SA-240";
            row1333.TypeGrade = "";
            row1333.ProductForm = "Plate";
            row1333.AlloyUNS = "S31725";
            row1333.ClassCondition = "";
            row1333.Notes = "";
            row1333.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1333);

            // Row 15: SA-249, , Wld. tube
            var row1334 = new OldStressRowData();
            row1334.LineNo = 15;
            row1334.MaterialId = 9560;
            row1334.SpecNo = "SA-249";
            row1334.TypeGrade = "";
            row1334.ProductForm = "Wld. tube";
            row1334.AlloyUNS = "S31725";
            row1334.ClassCondition = "";
            row1334.Notes = "G5, G24";
            row1334.StressValues = new double?[] { 16, null, 15.5, null, 14.5, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1334);

            // Row 16: SA-249, , Wld. tube
            var row1335 = new OldStressRowData();
            row1335.LineNo = 16;
            row1335.MaterialId = 9561;
            row1335.SpecNo = "SA-249";
            row1335.TypeGrade = "";
            row1335.ProductForm = "Wld. tube";
            row1335.AlloyUNS = "S31725";
            row1335.ClassCondition = "";
            row1335.Notes = "G24";
            row1335.StressValues = new double?[] { 16, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1335);

            // Row 17: SA-312, , Smls. tube
            var row1336 = new OldStressRowData();
            row1336.LineNo = 17;
            row1336.MaterialId = 9562;
            row1336.SpecNo = "SA-312";
            row1336.TypeGrade = "";
            row1336.ProductForm = "Smls. tube";
            row1336.AlloyUNS = "S31725";
            row1336.ClassCondition = "";
            row1336.Notes = "G5";
            row1336.StressValues = new double?[] { 18.8, null, 18.2, null, 17.1, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1336);

            // Row 18: SA-312, , Smls. tube
            var row1337 = new OldStressRowData();
            row1337.LineNo = 18;
            row1337.MaterialId = 9564;
            row1337.SpecNo = "SA-312";
            row1337.TypeGrade = "";
            row1337.ProductForm = "Smls. tube";
            row1337.AlloyUNS = "S31725";
            row1337.ClassCondition = "";
            row1337.Notes = "";
            row1337.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1337);

            // Row 19: SA-312, , Wld. tube
            var row1338 = new OldStressRowData();
            row1338.LineNo = 19;
            row1338.MaterialId = 9563;
            row1338.SpecNo = "SA-312";
            row1338.TypeGrade = "";
            row1338.ProductForm = "Wld. tube";
            row1338.AlloyUNS = "S31725";
            row1338.ClassCondition = "";
            row1338.Notes = "G5, G24";
            row1338.StressValues = new double?[] { 16, null, 15.5, null, 14.5, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1338);

            // Row 20: SA-312, , Wld. tube
            var row1339 = new OldStressRowData();
            row1339.LineNo = 20;
            row1339.MaterialId = 9565;
            row1339.SpecNo = "SA-312";
            row1339.TypeGrade = "";
            row1339.ProductForm = "Wld. tube";
            row1339.AlloyUNS = "S31725";
            row1339.ClassCondition = "";
            row1339.Notes = "G24";
            row1339.StressValues = new double?[] { 16, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1339);

            // Row 21: SA-358, , Wld. pipe
            var row1340 = new OldStressRowData();
            row1340.LineNo = 21;
            row1340.MaterialId = 9566;
            row1340.SpecNo = "SA-358";
            row1340.TypeGrade = "";
            row1340.ProductForm = "Wld. pipe";
            row1340.AlloyUNS = "S31725";
            row1340.ClassCondition = "";
            row1340.Notes = "G5, G24";
            row1340.StressValues = new double?[] { 16, null, 15.5, null, 14.5, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1340);

            // Row 22: SA-358, , Wld. pipe
            var row1341 = new OldStressRowData();
            row1341.LineNo = 22;
            row1341.MaterialId = 9567;
            row1341.SpecNo = "SA-358";
            row1341.TypeGrade = "";
            row1341.ProductForm = "Wld. pipe";
            row1341.AlloyUNS = "S31725";
            row1341.ClassCondition = "";
            row1341.Notes = "G24";
            row1341.StressValues = new double?[] { 16, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1341);

            // Row 23: SA-376, , Smls. pipe
            var row1342 = new OldStressRowData();
            row1342.LineNo = 23;
            row1342.MaterialId = 9568;
            row1342.SpecNo = "SA-376";
            row1342.TypeGrade = "";
            row1342.ProductForm = "Smls. pipe";
            row1342.AlloyUNS = "S31725";
            row1342.ClassCondition = "";
            row1342.Notes = "G5";
            row1342.StressValues = new double?[] { 18.8, null, 18.2, null, 17.1, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1342);

            // Row 24: SA-376, , Smls. pipe
            var row1343 = new OldStressRowData();
            row1343.LineNo = 24;
            row1343.MaterialId = 9569;
            row1343.SpecNo = "SA-376";
            row1343.TypeGrade = "";
            row1343.ProductForm = "Smls. pipe";
            row1343.AlloyUNS = "S31725";
            row1343.ClassCondition = "";
            row1343.Notes = "";
            row1343.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1343);

            // Row 25: SA-409, , Wld. pipe
            var row1344 = new OldStressRowData();
            row1344.LineNo = 25;
            row1344.MaterialId = 9570;
            row1344.SpecNo = "SA-409";
            row1344.TypeGrade = "";
            row1344.ProductForm = "Wld. pipe";
            row1344.AlloyUNS = "S31725";
            row1344.ClassCondition = "";
            row1344.Notes = "G5, G24";
            row1344.StressValues = new double?[] { 16, null, 15.5, null, 14.5, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1344);

            // Row 26: SA-409, , Wld. pipe
            var row1345 = new OldStressRowData();
            row1345.LineNo = 26;
            row1345.MaterialId = 9571;
            row1345.SpecNo = "SA-409";
            row1345.TypeGrade = "";
            row1345.ProductForm = "Wld. pipe";
            row1345.AlloyUNS = "S31725";
            row1345.ClassCondition = "";
            row1345.Notes = "G24";
            row1345.StressValues = new double?[] { 16, null, 14.4, null, 12.9, 11.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1345);

            // Row 27: SA-479, , Bar
            var row1346 = new OldStressRowData();
            row1346.LineNo = 27;
            row1346.MaterialId = 9572;
            row1346.SpecNo = "SA-479";
            row1346.TypeGrade = "";
            row1346.ProductForm = "Bar";
            row1346.AlloyUNS = "S31725";
            row1346.ClassCondition = "";
            row1346.Notes = "G5";
            row1346.StressValues = new double?[] { 18.8, null, 18.2, null, 17.1, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1346);

            // Row 28: SA-479, , Bar
            var row1347 = new OldStressRowData();
            row1347.LineNo = 28;
            row1347.MaterialId = 9573;
            row1347.SpecNo = "SA-479";
            row1347.TypeGrade = "";
            row1347.ProductForm = "Bar";
            row1347.AlloyUNS = "S31725";
            row1347.ClassCondition = "";
            row1347.Notes = "";
            row1347.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1347);

            // Row 29: SA-479, ER308, Bar
            var row1348 = new OldStressRowData();
            row1348.LineNo = 29;
            row1348.MaterialId = 9574;
            row1348.SpecNo = "SA-479";
            row1348.TypeGrade = "ER308";
            row1348.ProductForm = "Bar";
            row1348.AlloyUNS = "S30880";
            row1348.ClassCondition = "";
            row1348.Notes = "G5";
            row1348.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1348);

            // Row 30: SA-351, CK3MCuN, Castings
            var row1349 = new OldStressRowData();
            row1349.LineNo = 30;
            row1349.MaterialId = 9575;
            row1349.SpecNo = "SA-351";
            row1349.TypeGrade = "CK3MCuN";
            row1349.ProductForm = "Castings";
            row1349.AlloyUNS = "J93254";
            row1349.ClassCondition = "";
            row1349.Notes = "G1, G5";
            row1349.StressValues = new double?[] { 20, null, 20, null, 19, 18.1, 17.5, 17.1, 17, 16.9, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1349);

            // Row 31: SA-351, CK3MCuN, Castings
            var row1350 = new OldStressRowData();
            row1350.LineNo = 31;
            row1350.MaterialId = 9576;
            row1350.SpecNo = "SA-351";
            row1350.TypeGrade = "CK3MCuN";
            row1350.ProductForm = "Castings";
            row1350.AlloyUNS = "J93254";
            row1350.ClassCondition = "";
            row1350.Notes = "G1";
            row1350.StressValues = new double?[] { 20, null, 20, null, 18.5, 17.1, 16, 15.5, 15.3, 15.1, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1350);

            // Row 32: SA-182, F44, Forgings
            var row1351 = new OldStressRowData();
            row1351.LineNo = 32;
            row1351.MaterialId = 9578;
            row1351.SpecNo = "SA-182";
            row1351.TypeGrade = "F44";
            row1351.ProductForm = "Forgings";
            row1351.AlloyUNS = "S31254";
            row1351.ClassCondition = "";
            row1351.Notes = "G5";
            row1351.StressValues = new double?[] { 23.5, null, 23.5, null, 22.4, 21.3, 20.5, 20.1, 19.9, 19.9, 19.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1351);

            // Row 33: SA-182, F44, Forgings
            var row1352 = new OldStressRowData();
            row1352.LineNo = 33;
            row1352.MaterialId = 9577;
            row1352.SpecNo = "SA-182";
            row1352.TypeGrade = "F44";
            row1352.ProductForm = "Forgings";
            row1352.AlloyUNS = "S31254";
            row1352.ClassCondition = "";
            row1352.Notes = "";
            row1352.StressValues = new double?[] { 23.5, null, 23.5, null, 21.4, 19.9, 18.5, 17.9, 17.7, 17.5, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1352);

            // Row 34: SA-240, , Plate
            var row1353 = new OldStressRowData();
            row1353.LineNo = 34;
            row1353.MaterialId = 9580;
            row1353.SpecNo = "SA-240";
            row1353.TypeGrade = "";
            row1353.ProductForm = "Plate";
            row1353.AlloyUNS = "S31254";
            row1353.ClassCondition = "";
            row1353.Notes = "G5";
            row1353.StressValues = new double?[] { 23.5, null, 23.5, null, 22.4, 21.3, 20.5, 20.1, 19.9, 19.9, 19.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1353);

            // Row 35: SA-240, , Plate
            var row1354 = new OldStressRowData();
            row1354.LineNo = 35;
            row1354.MaterialId = 9579;
            row1354.SpecNo = "SA-240";
            row1354.TypeGrade = "";
            row1354.ProductForm = "Plate";
            row1354.AlloyUNS = "S31254";
            row1354.ClassCondition = "";
            row1354.Notes = "";
            row1354.StressValues = new double?[] { 23.5, null, 23.5, null, 21.4, 19.9, 18.5, 17.9, 17.7, 17.5, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1354);

            // Row 36: SA-249, , Wld. tube
            var row1355 = new OldStressRowData();
            row1355.LineNo = 36;
            row1355.MaterialId = 9582;
            row1355.SpecNo = "SA-249";
            row1355.TypeGrade = "";
            row1355.ProductForm = "Wld. tube";
            row1355.AlloyUNS = "S31254";
            row1355.ClassCondition = "";
            row1355.Notes = "G5, G24";
            row1355.StressValues = new double?[] { 20, null, 20, null, 19, 18.1, 17.4, 17.1, 16.9, 16.9, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1355);

            // Row 37: SA-249, , Wld. tube
            var row1356 = new OldStressRowData();
            row1356.LineNo = 37;
            row1356.MaterialId = 9581;
            row1356.SpecNo = "SA-249";
            row1356.TypeGrade = "";
            row1356.ProductForm = "Wld. tube";
            row1356.AlloyUNS = "S31254";
            row1356.ClassCondition = "";
            row1356.Notes = "G24";
            row1356.StressValues = new double?[] { 20, null, 20, null, 18.2, 16.9, 15.7, 15.2, 15, 14.9, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1356);

            // Row 1: SA-312, , Smls. pipe
            var row1357 = new OldStressRowData();
            row1357.LineNo = 1;
            row1357.MaterialId = 9585;
            row1357.SpecNo = "SA-312";
            row1357.TypeGrade = "";
            row1357.ProductForm = "Smls. pipe";
            row1357.AlloyUNS = "S31254";
            row1357.ClassCondition = "";
            row1357.Notes = "G5";
            row1357.StressValues = new double?[] { 23.5, null, 23.5, null, 22.4, 21.3, 20.5, 20.1, 19.9, 19.9, 19.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1357);

            // Row 2: SA-312, , Smls. pipe
            var row1358 = new OldStressRowData();
            row1358.LineNo = 2;
            row1358.MaterialId = 9583;
            row1358.SpecNo = "SA-312";
            row1358.TypeGrade = "";
            row1358.ProductForm = "Smls. pipe";
            row1358.AlloyUNS = "S31254";
            row1358.ClassCondition = "";
            row1358.Notes = "";
            row1358.StressValues = new double?[] { 23.5, null, 23.5, null, 21.4, 19.9, 18.5, 17.9, 17.7, 17.5, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1358);

            // Row 3: SA-312, , Wld. pipe
            var row1359 = new OldStressRowData();
            row1359.LineNo = 3;
            row1359.MaterialId = 9586;
            row1359.SpecNo = "SA-312";
            row1359.TypeGrade = "";
            row1359.ProductForm = "Wld. pipe";
            row1359.AlloyUNS = "S31254";
            row1359.ClassCondition = "";
            row1359.Notes = "G5, G24";
            row1359.StressValues = new double?[] { 20, null, 20, null, 19, 18.1, 17.4, 17.1, 16.9, 16.9, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1359);

            // Row 4: SA-312, , Wld. pipe
            var row1360 = new OldStressRowData();
            row1360.LineNo = 4;
            row1360.MaterialId = 9584;
            row1360.SpecNo = "SA-312";
            row1360.TypeGrade = "";
            row1360.ProductForm = "Wld. pipe";
            row1360.AlloyUNS = "S31254";
            row1360.ClassCondition = "";
            row1360.Notes = "G24";
            row1360.StressValues = new double?[] { 20, null, 20, null, 18.2, 16.9, 15.7, 15.2, 15, 14.9, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1360);

            // Row 5: SA-358, , Wld. pipe
            var row1361 = new OldStressRowData();
            row1361.LineNo = 5;
            row1361.MaterialId = 9588;
            row1361.SpecNo = "SA-358";
            row1361.TypeGrade = "";
            row1361.ProductForm = "Wld. pipe";
            row1361.AlloyUNS = "S31254";
            row1361.ClassCondition = "";
            row1361.Notes = "G5, G24";
            row1361.StressValues = new double?[] { 20, null, 20, null, 19, 18.1, 17.4, 17.1, 16.9, 16.9, 16.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1361);

            // Row 6: SA-358, , Wld. pipe
            var row1362 = new OldStressRowData();
            row1362.LineNo = 6;
            row1362.MaterialId = 9587;
            row1362.SpecNo = "SA-358";
            row1362.TypeGrade = "";
            row1362.ProductForm = "Wld. pipe";
            row1362.AlloyUNS = "S31254";
            row1362.ClassCondition = "";
            row1362.Notes = "G24";
            row1362.StressValues = new double?[] { 20, null, 20, null, 18.2, 16.9, 15.7, 15.2, 15, 14.9, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1362);

            // Row 7: SA-182, FXM-11, Forgings
            var row1363 = new OldStressRowData();
            row1363.LineNo = 7;
            row1363.MaterialId = 9589;
            row1363.SpecNo = "SA-182";
            row1363.TypeGrade = "FXM-11";
            row1363.ProductForm = "Forgings";
            row1363.AlloyUNS = "S21904";
            row1363.ClassCondition = "";
            row1363.Notes = "";
            row1363.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 19.7, 17.9, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1363);

            // Row 8: SA-312, TPXM-11, Smls. pipe
            var row1364 = new OldStressRowData();
            row1364.LineNo = 8;
            row1364.MaterialId = 9593;
            row1364.SpecNo = "SA-312";
            row1364.TypeGrade = "TPXM-11";
            row1364.ProductForm = "Smls. pipe";
            row1364.AlloyUNS = "S21904";
            row1364.ClassCondition = "";
            row1364.Notes = "G5";
            row1364.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 20, 19.2, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1364);

            // Row 9: SA-312, TPXM-11, Smls. pipe
            var row1365 = new OldStressRowData();
            row1365.LineNo = 9;
            row1365.MaterialId = 9591;
            row1365.SpecNo = "SA-312";
            row1365.TypeGrade = "TPXM-11";
            row1365.ProductForm = "Smls. pipe";
            row1365.AlloyUNS = "S21904";
            row1365.ClassCondition = "";
            row1365.Notes = "";
            row1365.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 19.7, 17.9, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1365);

            // Row 10: SA-312, TPXM-11, Wld. pipe
            var row1366 = new OldStressRowData();
            row1366.LineNo = 10;
            row1366.MaterialId = 9592;
            row1366.SpecNo = "SA-312";
            row1366.TypeGrade = "TPXM-11";
            row1366.ProductForm = "Wld. pipe";
            row1366.AlloyUNS = "S21904";
            row1366.ClassCondition = "";
            row1366.Notes = "G5, G24";
            row1366.StressValues = new double?[] { 19.1, null, 19, null, 18, 17, 16.3, 16, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1366);

            // Row 11: SA-312, TPXM-11, Wld. pipe
            var row1367 = new OldStressRowData();
            row1367.LineNo = 11;
            row1367.MaterialId = 9590;
            row1367.SpecNo = "SA-312";
            row1367.TypeGrade = "TPXM-11";
            row1367.ProductForm = "Wld. pipe";
            row1367.AlloyUNS = "S21904";
            row1367.ClassCondition = "";
            row1367.Notes = "G24";
            row1367.StressValues = new double?[] { 19.1, null, 19, null, 18, 16.7, 15.2, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1367);

            // Row 12: SA-336, FXM-11, Forgings
            var row1368 = new OldStressRowData();
            row1368.LineNo = 12;
            row1368.MaterialId = 9595;
            row1368.SpecNo = "SA-336";
            row1368.TypeGrade = "FXM-11";
            row1368.ProductForm = "Forgings";
            row1368.AlloyUNS = "S21904";
            row1368.ClassCondition = "";
            row1368.Notes = "G5";
            row1368.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 20, 19.2, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1368);

            // Row 13: SA-336, FXM-11, Forgings
            var row1369 = new OldStressRowData();
            row1369.LineNo = 13;
            row1369.MaterialId = 9594;
            row1369.SpecNo = "SA-336";
            row1369.TypeGrade = "FXM-11";
            row1369.ProductForm = "Forgings";
            row1369.AlloyUNS = "S21904";
            row1369.ClassCondition = "";
            row1369.Notes = "";
            row1369.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 19.7, 17.9, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1369);

            // Row 14: SA-666, XM-11, Plate
            var row1370 = new OldStressRowData();
            row1370.LineNo = 14;
            row1370.MaterialId = 9806;
            row1370.SpecNo = "SA-666";
            row1370.TypeGrade = "XM-11";
            row1370.ProductForm = "Plate";
            row1370.AlloyUNS = "S21904";
            row1370.ClassCondition = "";
            row1370.Notes = "G5";
            row1370.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 20, 19.2, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1370);

            // Row 15: SA-666, XM-11, Plate
            var row1371 = new OldStressRowData();
            row1371.LineNo = 15;
            row1371.MaterialId = 9807;
            row1371.SpecNo = "SA-666";
            row1371.TypeGrade = "XM-11";
            row1371.ProductForm = "Plate";
            row1371.AlloyUNS = "S21904";
            row1371.ClassCondition = "";
            row1371.Notes = "";
            row1371.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 19.7, 17.9, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1371);

            // Row 16: SA-182, F45, Forgings
            var row1372 = new OldStressRowData();
            row1372.LineNo = 16;
            row1372.MaterialId = 9597;
            row1372.SpecNo = "SA-182";
            row1372.TypeGrade = "F45";
            row1372.ProductForm = "Forgings";
            row1372.AlloyUNS = "S30815";
            row1372.ClassCondition = "";
            row1372.Notes = "G5";
            row1372.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 19.1, 18.7, 18.6, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1372);

            // Row 17: SA-182, F45, Forgings
            var row1373 = new OldStressRowData();
            row1373.LineNo = 17;
            row1373.MaterialId = 9596;
            row1373.SpecNo = "SA-182";
            row1373.TypeGrade = "F45";
            row1373.ProductForm = "Forgings";
            row1373.AlloyUNS = "S30815";
            row1373.ClassCondition = "";
            row1373.Notes = "";
            row1373.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 18.4, 17.7, 17.5, 17.3, 17.1, 16.8, 16.6, 16.3, 16.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1373);

            // Row 18: SA-213, , Smls. tube
            var row1374 = new OldStressRowData();
            row1374.LineNo = 18;
            row1374.MaterialId = 9599;
            row1374.SpecNo = "SA-213";
            row1374.TypeGrade = "";
            row1374.ProductForm = "Smls. tube";
            row1374.AlloyUNS = "S30815";
            row1374.ClassCondition = "";
            row1374.Notes = "G5";
            row1374.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 19.1, 18.7, 18.6, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1374);

            // Row 19: SA-213, , Smls. tube
            var row1375 = new OldStressRowData();
            row1375.LineNo = 19;
            row1375.MaterialId = 9598;
            row1375.SpecNo = "SA-213";
            row1375.TypeGrade = "";
            row1375.ProductForm = "Smls. tube";
            row1375.AlloyUNS = "S30815";
            row1375.ClassCondition = "";
            row1375.Notes = "";
            row1375.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 18.4, 17.7, 17.5, 17.3, 17.1, 16.8, 16.6, 16.3, 16.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1375);

            // Row 20: SA-240, , Plate
            var row1376 = new OldStressRowData();
            row1376.LineNo = 20;
            row1376.MaterialId = 9601;
            row1376.SpecNo = "SA-240";
            row1376.TypeGrade = "";
            row1376.ProductForm = "Plate";
            row1376.AlloyUNS = "S30815";
            row1376.ClassCondition = "";
            row1376.Notes = "G5";
            row1376.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 19.1, 18.7, 18.6, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1376);

            // Row 21: SA-240, , Plate
            var row1377 = new OldStressRowData();
            row1377.LineNo = 21;
            row1377.MaterialId = 9600;
            row1377.SpecNo = "SA-240";
            row1377.TypeGrade = "";
            row1377.ProductForm = "Plate";
            row1377.AlloyUNS = "S30815";
            row1377.ClassCondition = "";
            row1377.Notes = "";
            row1377.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 18.4, 17.7, 17.5, 17.3, 17.1, 16.8, 16.6, 16.3, 16.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1377);

            // Row 22: SA-249, , Wld. tube
            var row1378 = new OldStressRowData();
            row1378.LineNo = 22;
            row1378.MaterialId = 9603;
            row1378.SpecNo = "SA-249";
            row1378.TypeGrade = "";
            row1378.ProductForm = "Wld. tube";
            row1378.AlloyUNS = "S30815";
            row1378.ClassCondition = "";
            row1378.Notes = "G5, G24";
            row1378.StressValues = new double?[] { 18.5, null, 18.4, null, 17.3, 16.6, 16.2, 15.9, 15.8, 15.6, 15.5, 15.3, 15.1, 14.9, 14.6, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1378);

            // Row 23: SA-249, , Wld. tube
            var row1379 = new OldStressRowData();
            row1379.LineNo = 23;
            row1379.MaterialId = 9602;
            row1379.SpecNo = "SA-249";
            row1379.TypeGrade = "";
            row1379.ProductForm = "Wld. tube";
            row1379.AlloyUNS = "S30815";
            row1379.ClassCondition = "";
            row1379.Notes = "G24";
            row1379.StressValues = new double?[] { 18.5, null, 18.4, null, 17.3, 16.7, 15.6, 15, 14.9, 14.7, 14.5, 14.3, 14.1, 13.9, 13.7, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1379);

            // Row 24: SA-312, , Smls. pipe
            var row1380 = new OldStressRowData();
            row1380.LineNo = 24;
            row1380.MaterialId = 9606;
            row1380.SpecNo = "SA-312";
            row1380.TypeGrade = "";
            row1380.ProductForm = "Smls. pipe";
            row1380.AlloyUNS = "S30815";
            row1380.ClassCondition = "";
            row1380.Notes = "G5";
            row1380.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 19.1, 18.7, 18.6, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1380);

            // Row 25: SA-312, , Smls. pipe
            var row1381 = new OldStressRowData();
            row1381.LineNo = 25;
            row1381.MaterialId = 9604;
            row1381.SpecNo = "SA-312";
            row1381.TypeGrade = "";
            row1381.ProductForm = "Smls. pipe";
            row1381.AlloyUNS = "S30815";
            row1381.ClassCondition = "";
            row1381.Notes = "";
            row1381.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 18.4, 17.7, 17.5, 17.3, 17.1, 16.8, 16.6, 16.3, 16.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1381);

            // Row 26: SA-312, , Wld. pipe
            var row1382 = new OldStressRowData();
            row1382.LineNo = 26;
            row1382.MaterialId = 9607;
            row1382.SpecNo = "SA-312";
            row1382.TypeGrade = "";
            row1382.ProductForm = "Wld. pipe";
            row1382.AlloyUNS = "S30815";
            row1382.ClassCondition = "";
            row1382.Notes = "G5, G24";
            row1382.StressValues = new double?[] { 18.5, null, 18.4, null, 17.3, 16.6, 16.2, 15.9, 15.8, 15.6, 15.5, 15.3, 15.1, 14.9, 14.6, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1382);

            // Row 27: SA-312, , Wld. pipe
            var row1383 = new OldStressRowData();
            row1383.LineNo = 27;
            row1383.MaterialId = 9605;
            row1383.SpecNo = "SA-312";
            row1383.TypeGrade = "";
            row1383.ProductForm = "Wld. pipe";
            row1383.AlloyUNS = "S30815";
            row1383.ClassCondition = "";
            row1383.Notes = "G24";
            row1383.StressValues = new double?[] { 18.5, null, 18.4, null, 17.3, 16.7, 15.6, 15, 14.9, 14.7, 14.5, 14.3, 14.1, 13.9, 13.7, 12.7, 9.9, 7.7, 5.9, 4.4, 3.4, 2.6, 2, 1.6, 1.4, 1.1, 0.85, 0.73, 0.6 };
            batch.Add(row1383);

            // Row 28: SA-479, , Bar
            var row1384 = new OldStressRowData();
            row1384.LineNo = 28;
            row1384.MaterialId = 9609;
            row1384.SpecNo = "SA-479";
            row1384.TypeGrade = "";
            row1384.ProductForm = "Bar";
            row1384.AlloyUNS = "S30815";
            row1384.ClassCondition = "";
            row1384.Notes = "G5";
            row1384.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 19.1, 18.7, 18.6, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1384);

            // Row 29: SA-479, , Bar
            var row1385 = new OldStressRowData();
            row1385.LineNo = 29;
            row1385.MaterialId = 9608;
            row1385.SpecNo = "SA-479";
            row1385.TypeGrade = "";
            row1385.ProductForm = "Bar";
            row1385.AlloyUNS = "S30815";
            row1385.ClassCondition = "";
            row1385.Notes = "";
            row1385.StressValues = new double?[] { 21.8, null, 21.6, null, 20.4, 19.6, 18.4, 17.7, 17.5, 17.3, 17.1, 16.8, 16.6, 16.3, 16.1, 14.9, 11.6, 9, 6.9, 5.2, 4, 3.1, 2.4, 1.9, 1.6, 1.3, 1, 0.86, 0.71 };
            batch.Add(row1385);

            // Row 30: SA-182, F51, Forgings
            var row1386 = new OldStressRowData();
            row1386.LineNo = 30;
            row1386.MaterialId = 9824;
            row1386.SpecNo = "SA-182";
            row1386.TypeGrade = "F51";
            row1386.ProductForm = "Forgings";
            row1386.AlloyUNS = "S31803";
            row1386.ClassCondition = "";
            row1386.Notes = "G32";
            row1386.StressValues = new double?[] { 22.5, null, 22.5, null, 21.7, 20.9, 20.4, 20.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1386);

            // Row 31: SA-240, , Plate
            var row1387 = new OldStressRowData();
            row1387.LineNo = 31;
            row1387.MaterialId = 9610;
            row1387.SpecNo = "SA-240";
            row1387.TypeGrade = "";
            row1387.ProductForm = "Plate";
            row1387.AlloyUNS = "S31803";
            row1387.ClassCondition = "";
            row1387.Notes = "G32";
            row1387.StressValues = new double?[] { 22.5, null, 22.5, null, 21.7, 20.9, 20.4, 20.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1387);

            // Row 32: SA-789, , Smls. tube
            var row1388 = new OldStressRowData();
            row1388.LineNo = 32;
            row1388.MaterialId = 9611;
            row1388.SpecNo = "SA-789";
            row1388.TypeGrade = "";
            row1388.ProductForm = "Smls. tube";
            row1388.AlloyUNS = "S31803";
            row1388.ClassCondition = "";
            row1388.Notes = "G32";
            row1388.StressValues = new double?[] { 22.5, null, 22.5, null, 21.7, 20.9, 20.4, 20.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1388);

            // Row 33: SA-789, , Wld. tube
            var row1389 = new OldStressRowData();
            row1389.LineNo = 33;
            row1389.MaterialId = 9612;
            row1389.SpecNo = "SA-789";
            row1389.TypeGrade = "";
            row1389.ProductForm = "Wld. tube";
            row1389.AlloyUNS = "S31803";
            row1389.ClassCondition = "";
            row1389.Notes = "G24, G32";
            row1389.StressValues = new double?[] { 19.1, null, 19.1, null, 18.4, 17.8, 17.3, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1389);

            // Row 34: SA-790, , Smls. pipe
            var row1390 = new OldStressRowData();
            row1390.LineNo = 34;
            row1390.MaterialId = 9613;
            row1390.SpecNo = "SA-790";
            row1390.TypeGrade = "";
            row1390.ProductForm = "Smls. pipe";
            row1390.AlloyUNS = "S31803";
            row1390.ClassCondition = "";
            row1390.Notes = "G32";
            row1390.StressValues = new double?[] { 22.5, null, 22.5, null, 21.7, 20.9, 20.4, 20.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1390);

            // Row 35: SA-790, , Wld. pipe
            var row1391 = new OldStressRowData();
            row1391.LineNo = 35;
            row1391.MaterialId = 9614;
            row1391.SpecNo = "SA-790";
            row1391.TypeGrade = "";
            row1391.ProductForm = "Wld. pipe";
            row1391.AlloyUNS = "S31803";
            row1391.ClassCondition = "";
            row1391.Notes = "G24, G32";
            row1391.StressValues = new double?[] { 19.1, null, 19.1, null, 18.4, 17.8, 17.3, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1391);

            // Row 36: ..., , 
            var row1392 = new OldStressRowData();
            row1392.LineNo = 36;
            row1392.MaterialId = 9615;
            row1392.SpecNo = "...";
            row1392.TypeGrade = "";
            row1392.ProductForm = "";
            row1392.AlloyUNS = "";
            row1392.ClassCondition = "";
            row1392.Notes = "";
            row1392.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1392);

            // Row 37: SA-815, , Smls. & wld. fittings
            var row1393 = new OldStressRowData();
            row1393.LineNo = 37;
            row1393.MaterialId = 9820;
            row1393.SpecNo = "SA-815";
            row1393.TypeGrade = "";
            row1393.ProductForm = "Smls. & wld. fittings";
            row1393.AlloyUNS = "S31803";
            row1393.ClassCondition = "";
            row1393.Notes = "G32, W15";
            row1393.StressValues = new double?[] { 22.5, null, 22.5, null, 21.7, 20.9, 20.4, 20.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1393);

            // Row 38: ..., , 
            var row1394 = new OldStressRowData();
            row1394.LineNo = 38;
            row1394.MaterialId = 9821;
            row1394.SpecNo = "...";
            row1394.TypeGrade = "";
            row1394.ProductForm = "";
            row1394.AlloyUNS = "";
            row1394.ClassCondition = "";
            row1394.Notes = "";
            row1394.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1394);

            // Row 39: ..., , 
            var row1395 = new OldStressRowData();
            row1395.LineNo = 39;
            row1395.MaterialId = 9822;
            row1395.SpecNo = "...";
            row1395.TypeGrade = "";
            row1395.ProductForm = "";
            row1395.AlloyUNS = "";
            row1395.ClassCondition = "";
            row1395.Notes = "";
            row1395.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1395);

            // Row 1: SA-351, CG6MMN, Castings
            var row1396 = new OldStressRowData();
            row1396.LineNo = 1;
            row1396.MaterialId = 9616;
            row1396.SpecNo = "SA-351";
            row1396.TypeGrade = "CG6MMN";
            row1396.ProductForm = "Castings";
            row1396.AlloyUNS = "J93790";
            row1396.ClassCondition = "";
            row1396.Notes = "G1";
            row1396.StressValues = new double?[] { 18.8, null, 16.9, null, 14.9, 13.6, 13, 12.6, 12.4, 12.3, 12.2, 12, 11.9, 11.8, 11.6, 11.4, 11.3, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1396);

            // Row 2: SA-182, FXM-19, Forgings
            var row1397 = new OldStressRowData();
            row1397.LineNo = 2;
            row1397.MaterialId = 9617;
            row1397.SpecNo = "SA-182";
            row1397.TypeGrade = "FXM-19";
            row1397.ProductForm = "Forgings";
            row1397.AlloyUNS = "S20910";
            row1397.ClassCondition = "";
            row1397.Notes = "G5";
            row1397.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1397);

            // Row 3: SA-213, XM-19, Smls. tube
            var row1398 = new OldStressRowData();
            row1398.LineNo = 3;
            row1398.MaterialId = 9823;
            row1398.SpecNo = "SA-213";
            row1398.TypeGrade = "XM-19";
            row1398.ProductForm = "Smls. tube";
            row1398.AlloyUNS = "S20910";
            row1398.ClassCondition = "";
            row1398.Notes = "";
            row1398.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1398);

            // Row 4: SA-240, XM-19, Plate
            var row1399 = new OldStressRowData();
            row1399.LineNo = 4;
            row1399.MaterialId = 9618;
            row1399.SpecNo = "SA-240";
            row1399.TypeGrade = "XM-19";
            row1399.ProductForm = "Plate";
            row1399.AlloyUNS = "S20910";
            row1399.ClassCondition = "";
            row1399.Notes = "G5";
            row1399.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1399);

            // Row 5: SA-249, TPXM-19, Wld. tube
            var row1400 = new OldStressRowData();
            row1400.LineNo = 5;
            row1400.MaterialId = 9619;
            row1400.SpecNo = "SA-249";
            row1400.TypeGrade = "TPXM-19";
            row1400.ProductForm = "Wld. tube";
            row1400.AlloyUNS = "S20910";
            row1400.ClassCondition = "";
            row1400.Notes = "G5, W12";
            row1400.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1400);

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
