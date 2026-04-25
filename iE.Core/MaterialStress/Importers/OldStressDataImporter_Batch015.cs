using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers


{
    public static class OldStressDataImporter_Batch015
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch015(MaterialStressService service)
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

            // Row 6: SA-249, TPXM-19, Wld. tube
            var row1401 = new OldStressRowData();
            row1401.LineNo = 6;
            row1401.MaterialId = 9620;
            row1401.SpecNo = "SA-249";
            row1401.TypeGrade = "TPXM-19";
            row1401.ProductForm = "Wld. tube";
            row1401.AlloyUNS = "S20910";
            row1401.ClassCondition = "";
            row1401.Notes = "G24";
            row1401.StressValues = new double?[] { 21.3, null, 21.2, null, 20.1, 19.3, 19, 18.6, 18.5, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 16.9, 16.6, 16.2, 11.1, 7.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1401);

            // Row 7: SA-312, TPXM-19, Wld. & smls. pipe
            var row1402 = new OldStressRowData();
            row1402.LineNo = 7;
            row1402.MaterialId = 9621;
            row1402.SpecNo = "SA-312";
            row1402.TypeGrade = "TPXM-19";
            row1402.ProductForm = "Wld. & smls. pipe";
            row1402.AlloyUNS = "S20910";
            row1402.ClassCondition = "";
            row1402.Notes = "G5, W12";
            row1402.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1402);

            // Row 8: SA-312, TPXM-19, Smls. pipe
            var row1403 = new OldStressRowData();
            row1403.LineNo = 8;
            row1403.MaterialId = 9623;
            row1403.SpecNo = "SA-312";
            row1403.TypeGrade = "TPXM-19";
            row1403.ProductForm = "Smls. pipe";
            row1403.AlloyUNS = "S20910";
            row1403.ClassCondition = "";
            row1403.Notes = "";
            row1403.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1403);

            // Row 9: SA-312, TPXM-19, Wld. pipe
            var row1404 = new OldStressRowData();
            row1404.LineNo = 9;
            row1404.MaterialId = 9622;
            row1404.SpecNo = "SA-312";
            row1404.TypeGrade = "TPXM-19";
            row1404.ProductForm = "Wld. pipe";
            row1404.AlloyUNS = "S20910";
            row1404.ClassCondition = "";
            row1404.Notes = "G24";
            row1404.StressValues = new double?[] { 21.3, null, 21.2, null, 20.1, 19.3, 19, 18.6, 18.5, 18.4, 18.2, 18, 17.8, 17.5, 17.2, 16.9, 16.6, 16.2, 11.1, 7.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1404);

            // Row 10: SA-358, XM-19, Wld. pipe
            var row1405 = new OldStressRowData();
            row1405.LineNo = 10;
            row1405.MaterialId = 9624;
            row1405.SpecNo = "SA-358";
            row1405.TypeGrade = "XM-19";
            row1405.ProductForm = "Wld. pipe";
            row1405.AlloyUNS = "S20910";
            row1405.ClassCondition = "1";
            row1405.Notes = "G5, W12";
            row1405.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1405);

            // Row 11: ..., , 
            var row1406 = new OldStressRowData();
            row1406.LineNo = 11;
            row1406.MaterialId = 9625;
            row1406.SpecNo = "...";
            row1406.TypeGrade = "";
            row1406.ProductForm = "";
            row1406.AlloyUNS = "";
            row1406.ClassCondition = "";
            row1406.Notes = "";
            row1406.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1406);

            // Row 12: ..., , 
            var row1407 = new OldStressRowData();
            row1407.LineNo = 12;
            row1407.MaterialId = 9627;
            row1407.SpecNo = "...";
            row1407.TypeGrade = "";
            row1407.ProductForm = "";
            row1407.AlloyUNS = "";
            row1407.ClassCondition = "";
            row1407.Notes = "";
            row1407.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1407);

            // Row 13: SA-403, XM-19, Smls. & wld. fittings
            var row1408 = new OldStressRowData();
            row1408.LineNo = 13;
            row1408.MaterialId = 9626;
            row1408.SpecNo = "SA-403";
            row1408.TypeGrade = "XM-19";
            row1408.ProductForm = "Smls. & wld. fittings";
            row1408.AlloyUNS = "S20910";
            row1408.ClassCondition = "";
            row1408.Notes = "G5, W12, W15";
            row1408.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1408);

            // Row 14: ..., , 
            var row1409 = new OldStressRowData();
            row1409.LineNo = 14;
            row1409.MaterialId = 9628;
            row1409.SpecNo = "...";
            row1409.TypeGrade = "";
            row1409.ProductForm = "";
            row1409.AlloyUNS = "";
            row1409.ClassCondition = "";
            row1409.Notes = "";
            row1409.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1409);

            // Row 15: ..., , 
            var row1410 = new OldStressRowData();
            row1410.LineNo = 15;
            row1410.MaterialId = 9629;
            row1410.SpecNo = "...";
            row1410.TypeGrade = "";
            row1410.ProductForm = "";
            row1410.AlloyUNS = "";
            row1410.ClassCondition = "";
            row1410.Notes = "";
            row1410.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1410);

            // Row 16: SA-479, XM-19, Bar
            var row1411 = new OldStressRowData();
            row1411.LineNo = 16;
            row1411.MaterialId = 9630;
            row1411.SpecNo = "SA-479";
            row1411.TypeGrade = "XM-19";
            row1411.ProductForm = "Bar";
            row1411.AlloyUNS = "S20910";
            row1411.ClassCondition = "";
            row1411.Notes = "G5, G22";
            row1411.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, 20.9, 20.6, 20.3, 19.9, 19.5, 19, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1411);

            // Row 17: SA-813, TPXM-19, Wld. pipe
            var row1412 = new OldStressRowData();
            row1412.LineNo = 17;
            row1412.MaterialId = 9631;
            row1412.SpecNo = "SA-813";
            row1412.TypeGrade = "TPXM-19";
            row1412.ProductForm = "Wld. pipe";
            row1412.AlloyUNS = "S20910";
            row1412.ClassCondition = "";
            row1412.Notes = "G5, W12";
            row1412.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1412);

            // Row 18: SA-814, TPXM-19, Wld. pipe
            var row1413 = new OldStressRowData();
            row1413.LineNo = 18;
            row1413.MaterialId = 9632;
            row1413.SpecNo = "SA-814";
            row1413.TypeGrade = "TPXM-19";
            row1413.ProductForm = "Wld. pipe";
            row1413.AlloyUNS = "S20910";
            row1413.ClassCondition = "";
            row1413.Notes = "G5, W12";
            row1413.StressValues = new double?[] { 25, null, 24.9, null, 23.6, 22.7, 22.3, 21.9, 21.8, 21.6, 21.4, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1413);

            // Row 19: SA-240, , Plate
            var row1414 = new OldStressRowData();
            row1414.LineNo = 19;
            row1414.MaterialId = 9633;
            row1414.SpecNo = "SA-240";
            row1414.TypeGrade = "";
            row1414.ProductForm = "Plate";
            row1414.AlloyUNS = "S32304";
            row1414.ClassCondition = "";
            row1414.Notes = "G32";
            row1414.StressValues = new double?[] { 21.7, null, 21, null, 19.7, 19, 18.6, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1414);

            // Row 20: SA-789, , Smls. tube
            var row1415 = new OldStressRowData();
            row1415.LineNo = 20;
            row1415.MaterialId = 9634;
            row1415.SpecNo = "SA-789";
            row1415.TypeGrade = "";
            row1415.ProductForm = "Smls. tube";
            row1415.AlloyUNS = "S32304";
            row1415.ClassCondition = "";
            row1415.Notes = "G32";
            row1415.StressValues = new double?[] { 21.7, null, 21, null, 19.7, 19, 18.6, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1415);

            // Row 21: SA-789, , Wld. tube
            var row1416 = new OldStressRowData();
            row1416.LineNo = 21;
            row1416.MaterialId = 9635;
            row1416.SpecNo = "SA-789";
            row1416.TypeGrade = "";
            row1416.ProductForm = "Wld. tube";
            row1416.AlloyUNS = "S32304";
            row1416.ClassCondition = "";
            row1416.Notes = "G24, G32";
            row1416.StressValues = new double?[] { 18.4, null, 17.9, null, 16.7, 16.2, 15.8, 15.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1416);

            // Row 22: SA-790, , Smls. pipe
            var row1417 = new OldStressRowData();
            row1417.LineNo = 22;
            row1417.MaterialId = 9636;
            row1417.SpecNo = "SA-790";
            row1417.TypeGrade = "";
            row1417.ProductForm = "Smls. pipe";
            row1417.AlloyUNS = "S32304";
            row1417.ClassCondition = "";
            row1417.Notes = "G32";
            row1417.StressValues = new double?[] { 21.7, null, 21, null, 19.7, 19, 18.6, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1417);

            // Row 23: SA-790, , Wld. pipe
            var row1418 = new OldStressRowData();
            row1418.LineNo = 23;
            row1418.MaterialId = 9637;
            row1418.SpecNo = "SA-790";
            row1418.TypeGrade = "";
            row1418.ProductForm = "Wld. pipe";
            row1418.AlloyUNS = "S32304";
            row1418.ClassCondition = "";
            row1418.Notes = "G24, G32";
            row1418.StressValues = new double?[] { 18.4, null, 17.9, null, 16.7, 16.2, 15.8, 15.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1418);

            // Row 24: ..., , 
            var row1419 = new OldStressRowData();
            row1419.LineNo = 24;
            row1419.MaterialId = 9662;
            row1419.SpecNo = "...";
            row1419.TypeGrade = "";
            row1419.ProductForm = "";
            row1419.AlloyUNS = "";
            row1419.ClassCondition = "";
            row1419.Notes = "";
            row1419.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1419);

            // Row 25: ..., , 
            var row1420 = new OldStressRowData();
            row1420.LineNo = 25;
            row1420.MaterialId = 9664;
            row1420.SpecNo = "...";
            row1420.TypeGrade = "";
            row1420.ProductForm = "";
            row1420.AlloyUNS = "";
            row1420.ClassCondition = "";
            row1420.Notes = "";
            row1420.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1420);

            // Row 26: SA-403, 309, Smls. & wld. fittings
            var row1421 = new OldStressRowData();
            row1421.LineNo = 26;
            row1421.MaterialId = 9667;
            row1421.SpecNo = "SA-403";
            row1421.TypeGrade = "309";
            row1421.ProductForm = "Smls. & wld. fittings";
            row1421.AlloyUNS = "S30900";
            row1421.ClassCondition = "";
            row1421.Notes = "G5, G12, W12, W15";
            row1421.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1421);

            // Row 27: ..., , 
            var row1422 = new OldStressRowData();
            row1422.LineNo = 27;
            row1422.MaterialId = 9663;
            row1422.SpecNo = "...";
            row1422.TypeGrade = "";
            row1422.ProductForm = "";
            row1422.AlloyUNS = "";
            row1422.ClassCondition = "";
            row1422.Notes = "";
            row1422.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1422);

            // Row 28: ..., , 
            var row1423 = new OldStressRowData();
            row1423.LineNo = 28;
            row1423.MaterialId = 9665;
            row1423.SpecNo = "...";
            row1423.TypeGrade = "";
            row1423.ProductForm = "";
            row1423.AlloyUNS = "";
            row1423.ClassCondition = "";
            row1423.Notes = "";
            row1423.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1423);

            // Row 29: ..., , 
            var row1424 = new OldStressRowData();
            row1424.LineNo = 29;
            row1424.MaterialId = 9666;
            row1424.SpecNo = "...";
            row1424.TypeGrade = "";
            row1424.ProductForm = "";
            row1424.AlloyUNS = "";
            row1424.ClassCondition = "";
            row1424.Notes = "";
            row1424.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1424);

            // Row 30: SA-213, TP309S, Smls. tube
            var row1425 = new OldStressRowData();
            row1425.LineNo = 30;
            row1425.MaterialId = 9640;
            row1425.SpecNo = "SA-213";
            row1425.TypeGrade = "TP309S";
            row1425.ProductForm = "Smls. tube";
            row1425.AlloyUNS = "S30908";
            row1425.ClassCondition = "";
            row1425.Notes = "G5, G12, G18";
            row1425.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1425);

            // Row 31: SA-213, TP309S, Smls. tube
            var row1426 = new OldStressRowData();
            row1426.LineNo = 31;
            row1426.MaterialId = 9641;
            row1426.SpecNo = "SA-213";
            row1426.TypeGrade = "TP309S";
            row1426.ProductForm = "Smls. tube";
            row1426.AlloyUNS = "S30908";
            row1426.ClassCondition = "";
            row1426.Notes = "G12, G18";
            row1426.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1426);

            // Row 32: SA-240, 309S, Plate
            var row1427 = new OldStressRowData();
            row1427.LineNo = 32;
            row1427.MaterialId = 9644;
            row1427.SpecNo = "SA-240";
            row1427.TypeGrade = "309S";
            row1427.ProductForm = "Plate";
            row1427.AlloyUNS = "S30908";
            row1427.ClassCondition = "";
            row1427.Notes = "G5, G12, G18";
            row1427.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1427);

            // Row 33: SA-240, 309S, Plate
            var row1428 = new OldStressRowData();
            row1428.LineNo = 33;
            row1428.MaterialId = 9645;
            row1428.SpecNo = "SA-240";
            row1428.TypeGrade = "309S";
            row1428.ProductForm = "Plate";
            row1428.AlloyUNS = "S30908";
            row1428.ClassCondition = "";
            row1428.Notes = "G12, G18";
            row1428.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1428);

            // Row 34: SA-249, TP309S, Wld. tube
            var row1429 = new OldStressRowData();
            row1429.LineNo = 34;
            row1429.MaterialId = 9649;
            row1429.SpecNo = "SA-249";
            row1429.TypeGrade = "TP309S";
            row1429.ProductForm = "Wld. tube";
            row1429.AlloyUNS = "S30908";
            row1429.ClassCondition = "";
            row1429.Notes = "G5, G12, G24";
            row1429.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1429);

            // Row 35: SA-249, TP309S, Wld. tube
            var row1430 = new OldStressRowData();
            row1430.LineNo = 35;
            row1430.MaterialId = 9650;
            row1430.SpecNo = "SA-249";
            row1430.TypeGrade = "TP309S";
            row1430.ProductForm = "Wld. tube";
            row1430.AlloyUNS = "S30908";
            row1430.ClassCondition = "";
            row1430.Notes = "G12, G24";
            row1430.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1430);

            // Row 36: SA-312, TP309S, Wld. & smls. pipe
            var row1431 = new OldStressRowData();
            row1431.LineNo = 36;
            row1431.MaterialId = 9656;
            row1431.SpecNo = "SA-312";
            row1431.TypeGrade = "TP309S";
            row1431.ProductForm = "Wld. & smls. pipe";
            row1431.AlloyUNS = "S30908";
            row1431.ClassCondition = "";
            row1431.Notes = "G5, G12, G18, W12";
            row1431.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1431);

            // Row 37: SA-312, TP309S, Smls. pipe
            var row1432 = new OldStressRowData();
            row1432.LineNo = 37;
            row1432.MaterialId = 9657;
            row1432.SpecNo = "SA-312";
            row1432.TypeGrade = "TP309S";
            row1432.ProductForm = "Smls. pipe";
            row1432.AlloyUNS = "S30908";
            row1432.ClassCondition = "";
            row1432.Notes = "G12, G18";
            row1432.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1432);

            // Row 1: SA-312, TP309S, Wld. pipe
            var row1433 = new OldStressRowData();
            row1433.LineNo = 1;
            row1433.MaterialId = 9658;
            row1433.SpecNo = "SA-312";
            row1433.TypeGrade = "TP309S";
            row1433.ProductForm = "Wld. pipe";
            row1433.AlloyUNS = "S30908";
            row1433.ClassCondition = "";
            row1433.Notes = "G5, G12, G18, W14";
            row1433.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1433);

            // Row 2: SA-312, TP309S, Wld. pipe
            var row1434 = new OldStressRowData();
            row1434.LineNo = 2;
            row1434.MaterialId = 9659;
            row1434.SpecNo = "SA-312";
            row1434.TypeGrade = "TP309S";
            row1434.ProductForm = "Wld. pipe";
            row1434.AlloyUNS = "S30908";
            row1434.ClassCondition = "";
            row1434.Notes = "G12, G18, W14";
            row1434.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1434);

            // Row 3: SA-312, TP309S, Wld. pipe
            var row1435 = new OldStressRowData();
            row1435.LineNo = 3;
            row1435.MaterialId = 9660;
            row1435.SpecNo = "SA-312";
            row1435.TypeGrade = "TP309S";
            row1435.ProductForm = "Wld. pipe";
            row1435.AlloyUNS = "S30908";
            row1435.ClassCondition = "";
            row1435.Notes = "G3, G5, G12, G18, G24";
            row1435.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1435);

            // Row 4: SA-312, TP309S, Wld. pipe
            var row1436 = new OldStressRowData();
            row1436.LineNo = 4;
            row1436.MaterialId = 9655;
            row1436.SpecNo = "SA-312";
            row1436.TypeGrade = "TP309S";
            row1436.ProductForm = "Wld. pipe";
            row1436.AlloyUNS = "S30908";
            row1436.ClassCondition = "";
            row1436.Notes = "G3, G12, G18, G24";
            row1436.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1436);

            // Row 5: SA-358, 309S, Wld. pipe
            var row1437 = new OldStressRowData();
            row1437.LineNo = 5;
            row1437.MaterialId = 9661;
            row1437.SpecNo = "SA-358";
            row1437.TypeGrade = "309S";
            row1437.ProductForm = "Wld. pipe";
            row1437.AlloyUNS = "S30908";
            row1437.ClassCondition = "1";
            row1437.Notes = "G5, W12";
            row1437.StressValues = new double?[] { 18.8, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1437);

            // Row 6: SA-479, 309S, Bar
            var row1438 = new OldStressRowData();
            row1438.LineNo = 6;
            row1438.MaterialId = 9670;
            row1438.SpecNo = "SA-479";
            row1438.TypeGrade = "309S";
            row1438.ProductForm = "Bar";
            row1438.AlloyUNS = "S30908";
            row1438.ClassCondition = "";
            row1438.Notes = "G5, G12, G18, G22";
            row1438.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1438);

            // Row 7: SA-479, 309S, Bar
            var row1439 = new OldStressRowData();
            row1439.LineNo = 7;
            row1439.MaterialId = 9671;
            row1439.SpecNo = "SA-479";
            row1439.TypeGrade = "309S";
            row1439.ProductForm = "Bar";
            row1439.AlloyUNS = "S30908";
            row1439.ClassCondition = "";
            row1439.Notes = "G12, G18, G22";
            row1439.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1439);

            // Row 8: SA-813, TP309S, Wld. pipe
            var row1440 = new OldStressRowData();
            row1440.LineNo = 8;
            row1440.MaterialId = 9672;
            row1440.SpecNo = "SA-813";
            row1440.TypeGrade = "TP309S";
            row1440.ProductForm = "Wld. pipe";
            row1440.AlloyUNS = "S30908";
            row1440.ClassCondition = "";
            row1440.Notes = "G5, G12, G24";
            row1440.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1440);

            // Row 9: SA-813, TP309S, Wld. pipe
            var row1441 = new OldStressRowData();
            row1441.LineNo = 9;
            row1441.MaterialId = 9673;
            row1441.SpecNo = "SA-813";
            row1441.TypeGrade = "TP309S";
            row1441.ProductForm = "Wld. pipe";
            row1441.AlloyUNS = "S30908";
            row1441.ClassCondition = "";
            row1441.Notes = "G12, G24";
            row1441.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1441);

            // Row 10: SA-814, TP309S, Wld. pipe
            var row1442 = new OldStressRowData();
            row1442.LineNo = 10;
            row1442.MaterialId = 9674;
            row1442.SpecNo = "SA-814";
            row1442.TypeGrade = "TP309S";
            row1442.ProductForm = "Wld. pipe";
            row1442.AlloyUNS = "S30908";
            row1442.ClassCondition = "";
            row1442.Notes = "G5, G12, G24";
            row1442.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1442);

            // Row 11: SA-814, TP309S, Wld. pipe
            var row1443 = new OldStressRowData();
            row1443.LineNo = 11;
            row1443.MaterialId = 9675;
            row1443.SpecNo = "SA-814";
            row1443.TypeGrade = "TP309S";
            row1443.ProductForm = "Wld. pipe";
            row1443.AlloyUNS = "S30908";
            row1443.ClassCondition = "";
            row1443.Notes = "G12, G24";
            row1443.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1443);

            // Row 12: SA-213, TP309H, Smls. tube
            var row1444 = new OldStressRowData();
            row1444.LineNo = 12;
            row1444.MaterialId = 9638;
            row1444.SpecNo = "SA-213";
            row1444.TypeGrade = "TP309H";
            row1444.ProductForm = "Smls. tube";
            row1444.AlloyUNS = "S30909";
            row1444.ClassCondition = "";
            row1444.Notes = "G5, G18";
            row1444.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1444);

            // Row 13: SA-213, TP309H, Smls. tube
            var row1445 = new OldStressRowData();
            row1445.LineNo = 13;
            row1445.MaterialId = 9639;
            row1445.SpecNo = "SA-213";
            row1445.TypeGrade = "TP309H";
            row1445.ProductForm = "Smls. tube";
            row1445.AlloyUNS = "S30909";
            row1445.ClassCondition = "";
            row1445.Notes = "G18";
            row1445.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.4, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1445);

            // Row 14: SA-240, 309H, Plate
            var row1446 = new OldStressRowData();
            row1446.LineNo = 14;
            row1446.MaterialId = 9642;
            row1446.SpecNo = "SA-240";
            row1446.TypeGrade = "309H";
            row1446.ProductForm = "Plate";
            row1446.AlloyUNS = "S30909";
            row1446.ClassCondition = "";
            row1446.Notes = "G5, G18, H1";
            row1446.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1446);

            // Row 15: SA-240, 309H, Plate
            var row1447 = new OldStressRowData();
            row1447.LineNo = 15;
            row1447.MaterialId = 9643;
            row1447.SpecNo = "SA-240";
            row1447.TypeGrade = "309H";
            row1447.ProductForm = "Plate";
            row1447.AlloyUNS = "S30909";
            row1447.ClassCondition = "";
            row1447.Notes = "G18, H1";
            row1447.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.4, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1447);

            // Row 16: SA-249, TP309H, Wld. tube
            var row1448 = new OldStressRowData();
            row1448.LineNo = 16;
            row1448.MaterialId = 9646;
            row1448.SpecNo = "SA-249";
            row1448.TypeGrade = "TP309H";
            row1448.ProductForm = "Wld. tube";
            row1448.AlloyUNS = "S30909";
            row1448.ClassCondition = "";
            row1448.Notes = "G5, W12";
            row1448.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1448);

            // Row 17: SA-249, TP309H, Wld. tube
            var row1449 = new OldStressRowData();
            row1449.LineNo = 17;
            row1449.MaterialId = 9647;
            row1449.SpecNo = "SA-249";
            row1449.TypeGrade = "TP309H";
            row1449.ProductForm = "Wld. tube";
            row1449.AlloyUNS = "S30909";
            row1449.ClassCondition = "";
            row1449.Notes = "G5, G24";
            row1449.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1449);

            // Row 18: SA-249, TP309H, Wld. tube
            var row1450 = new OldStressRowData();
            row1450.LineNo = 18;
            row1450.MaterialId = 9648;
            row1450.SpecNo = "SA-249";
            row1450.TypeGrade = "TP309H";
            row1450.ProductForm = "Wld. tube";
            row1450.AlloyUNS = "S30909";
            row1450.ClassCondition = "";
            row1450.Notes = "G24";
            row1450.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.4, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1450);

            // Row 19: SA-312, TP309H, Smls. pipe
            var row1451 = new OldStressRowData();
            row1451.LineNo = 19;
            row1451.MaterialId = 9651;
            row1451.SpecNo = "SA-312";
            row1451.TypeGrade = "TP309H";
            row1451.ProductForm = "Smls. pipe";
            row1451.AlloyUNS = "S30909";
            row1451.ClassCondition = "";
            row1451.Notes = "G5, G18";
            row1451.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1451);

            // Row 20: SA-312, TP309H, Smls. pipe
            var row1452 = new OldStressRowData();
            row1452.LineNo = 20;
            row1452.MaterialId = 9652;
            row1452.SpecNo = "SA-312";
            row1452.TypeGrade = "TP309H";
            row1452.ProductForm = "Smls. pipe";
            row1452.AlloyUNS = "S30909";
            row1452.ClassCondition = "";
            row1452.Notes = "G18";
            row1452.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.4, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1452);

            // Row 21: SA-312, TP309H, Wld. pipe
            var row1453 = new OldStressRowData();
            row1453.LineNo = 21;
            row1453.MaterialId = 9653;
            row1453.SpecNo = "SA-312";
            row1453.TypeGrade = "TP309H";
            row1453.ProductForm = "Wld. pipe";
            row1453.AlloyUNS = "S30909";
            row1453.ClassCondition = "";
            row1453.Notes = "G3, G5, G18, G24";
            row1453.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1453);

            // Row 22: SA-312, TP309H, Wld. pipe
            var row1454 = new OldStressRowData();
            row1454.LineNo = 22;
            row1454.MaterialId = 9654;
            row1454.SpecNo = "SA-312";
            row1454.TypeGrade = "TP309H";
            row1454.ProductForm = "Wld. pipe";
            row1454.AlloyUNS = "S30909";
            row1454.ClassCondition = "";
            row1454.Notes = "G3, G18, G24";
            row1454.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.4, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1454);

            // Row 23: SA-479, 309H, Bar
            var row1455 = new OldStressRowData();
            row1455.LineNo = 23;
            row1455.MaterialId = 9668;
            row1455.SpecNo = "SA-479";
            row1455.TypeGrade = "309H";
            row1455.ProductForm = "Bar";
            row1455.AlloyUNS = "S30909";
            row1455.ClassCondition = "";
            row1455.Notes = "G5, G18";
            row1455.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1455);

            // Row 24: SA-479, 309H, Bar
            var row1456 = new OldStressRowData();
            row1456.LineNo = 24;
            row1456.MaterialId = 9669;
            row1456.SpecNo = "SA-479";
            row1456.TypeGrade = "309H";
            row1456.ProductForm = "Bar";
            row1456.AlloyUNS = "S30909";
            row1456.ClassCondition = "";
            row1456.Notes = "G18";
            row1456.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.4, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1456);

            // Row 25: SA-213, TP309Cb, Smls. tube
            var row1457 = new OldStressRowData();
            row1457.LineNo = 25;
            row1457.MaterialId = 9676;
            row1457.SpecNo = "SA-213";
            row1457.TypeGrade = "TP309Cb";
            row1457.ProductForm = "Smls. tube";
            row1457.AlloyUNS = "S30940";
            row1457.ClassCondition = "";
            row1457.Notes = "G5, G12";
            row1457.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1457);

            // Row 26: SA-213, TP309Cb, Smls. tube
            var row1458 = new OldStressRowData();
            row1458.LineNo = 26;
            row1458.MaterialId = 9677;
            row1458.SpecNo = "SA-213";
            row1458.TypeGrade = "TP309Cb";
            row1458.ProductForm = "Smls. tube";
            row1458.AlloyUNS = "S30940";
            row1458.ClassCondition = "";
            row1458.Notes = "G12";
            row1458.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1458);

            // Row 27: SA-240, 309Cb, Plate
            var row1459 = new OldStressRowData();
            row1459.LineNo = 27;
            row1459.MaterialId = 9678;
            row1459.SpecNo = "SA-240";
            row1459.TypeGrade = "309Cb";
            row1459.ProductForm = "Plate";
            row1459.AlloyUNS = "S30940";
            row1459.ClassCondition = "";
            row1459.Notes = "G5, G12";
            row1459.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1459);

            // Row 28: SA-240, 309Cb, Plate
            var row1460 = new OldStressRowData();
            row1460.LineNo = 28;
            row1460.MaterialId = 9679;
            row1460.SpecNo = "SA-240";
            row1460.TypeGrade = "309Cb";
            row1460.ProductForm = "Plate";
            row1460.AlloyUNS = "S30940";
            row1460.ClassCondition = "";
            row1460.Notes = "G12";
            row1460.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1460);

            // Row 29: SA-249, TP309Cb, Wld. tube
            var row1461 = new OldStressRowData();
            row1461.LineNo = 29;
            row1461.MaterialId = 9680;
            row1461.SpecNo = "SA-249";
            row1461.TypeGrade = "TP309Cb";
            row1461.ProductForm = "Wld. tube";
            row1461.AlloyUNS = "S30940";
            row1461.ClassCondition = "";
            row1461.Notes = "G5, G12, G24";
            row1461.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1461);

            // Row 30: SA-249, TP309Cb, Wld. tube
            var row1462 = new OldStressRowData();
            row1462.LineNo = 30;
            row1462.MaterialId = 9681;
            row1462.SpecNo = "SA-249";
            row1462.TypeGrade = "TP309Cb";
            row1462.ProductForm = "Wld. tube";
            row1462.AlloyUNS = "S30940";
            row1462.ClassCondition = "";
            row1462.Notes = "G12, G24";
            row1462.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1462);

            // Row 31: SA-312, TP309Cb, Wld. & smls. pipe
            var row1463 = new OldStressRowData();
            row1463.LineNo = 31;
            row1463.MaterialId = 9682;
            row1463.SpecNo = "SA-312";
            row1463.TypeGrade = "TP309Cb";
            row1463.ProductForm = "Wld. & smls. pipe";
            row1463.AlloyUNS = "S30940";
            row1463.ClassCondition = "";
            row1463.Notes = "G5, G12, W12";
            row1463.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1463);

            // Row 32: SA-312, TP309Cb, Smls. pipe
            var row1464 = new OldStressRowData();
            row1464.LineNo = 32;
            row1464.MaterialId = 9683;
            row1464.SpecNo = "SA-312";
            row1464.TypeGrade = "TP309Cb";
            row1464.ProductForm = "Smls. pipe";
            row1464.AlloyUNS = "S30940";
            row1464.ClassCondition = "";
            row1464.Notes = "G12";
            row1464.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1464);

            // Row 33: SA-312, TP309Cb, Wld. pipe
            var row1465 = new OldStressRowData();
            row1465.LineNo = 33;
            row1465.MaterialId = 9684;
            row1465.SpecNo = "SA-312";
            row1465.TypeGrade = "TP309Cb";
            row1465.ProductForm = "Wld. pipe";
            row1465.AlloyUNS = "S30940";
            row1465.ClassCondition = "";
            row1465.Notes = "G5, G12, G24";
            row1465.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1465);

            // Row 34: SA-312, TP309Cb, Wld. pipe
            var row1466 = new OldStressRowData();
            row1466.LineNo = 34;
            row1466.MaterialId = 9685;
            row1466.SpecNo = "SA-312";
            row1466.TypeGrade = "TP309Cb";
            row1466.ProductForm = "Wld. pipe";
            row1466.AlloyUNS = "S30940";
            row1466.ClassCondition = "";
            row1466.Notes = "G12, G24";
            row1466.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1466);

            // Row 35: SA-479, 309Cb, Bar
            var row1467 = new OldStressRowData();
            row1467.LineNo = 35;
            row1467.MaterialId = 9686;
            row1467.SpecNo = "SA-479";
            row1467.TypeGrade = "309Cb";
            row1467.ProductForm = "Bar";
            row1467.AlloyUNS = "S30940";
            row1467.ClassCondition = "";
            row1467.Notes = "G5, G12, G22";
            row1467.StressValues = new double?[] { 18.8, null, 18.8, null, 18.7, 18.3, 17.9, 17.5, 17.3, 17.1, 16.8, 16.5, 16.1, 15.6, 15.1, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1467);

            // Row 36: SA-479, 309Cb, Bar
            var row1468 = new OldStressRowData();
            row1468.LineNo = 36;
            row1468.MaterialId = 9687;
            row1468.SpecNo = "SA-479";
            row1468.TypeGrade = "309Cb";
            row1468.ProductForm = "Bar";
            row1468.AlloyUNS = "S30940";
            row1468.ClassCondition = "";
            row1468.Notes = "G12, G22";
            row1468.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.1, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1468);

            // Row 37: SA-813, TP309Cb, Wld. pipe
            var row1469 = new OldStressRowData();
            row1469.LineNo = 37;
            row1469.MaterialId = 9688;
            row1469.SpecNo = "SA-813";
            row1469.TypeGrade = "TP309Cb";
            row1469.ProductForm = "Wld. pipe";
            row1469.AlloyUNS = "S30940";
            row1469.ClassCondition = "";
            row1469.Notes = "G5, G12, G24";
            row1469.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1469);

            // Row 38: SA-813, TP309Cb, Wld. pipe
            var row1470 = new OldStressRowData();
            row1470.LineNo = 38;
            row1470.MaterialId = 9689;
            row1470.SpecNo = "SA-813";
            row1470.TypeGrade = "TP309Cb";
            row1470.ProductForm = "Wld. pipe";
            row1470.AlloyUNS = "S30940";
            row1470.ClassCondition = "";
            row1470.Notes = "G12, G24";
            row1470.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1470);

            // Row 39: SA-814, TP309Cb, Wld. pipe
            var row1471 = new OldStressRowData();
            row1471.LineNo = 39;
            row1471.MaterialId = 9690;
            row1471.SpecNo = "SA-814";
            row1471.TypeGrade = "TP309Cb";
            row1471.ProductForm = "Wld. pipe";
            row1471.AlloyUNS = "S30940";
            row1471.ClassCondition = "";
            row1471.Notes = "G5, G12, G13, G24";
            row1471.StressValues = new double?[] { 16, null, 16, null, 15.9, 15.6, 15.2, 14.9, 14.7, 14.5, 14.3, 14, 13.7, 13.3, 12.8, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1471);

            // Row 40: SA-814, TP309Cb, Wld. pipe
            var row1472 = new OldStressRowData();
            row1472.LineNo = 40;
            row1472.MaterialId = 9691;
            row1472.SpecNo = "SA-814";
            row1472.TypeGrade = "TP309Cb";
            row1472.ProductForm = "Wld. pipe";
            row1472.AlloyUNS = "S30940";
            row1472.ClassCondition = "";
            row1472.Notes = "G12, G24";
            row1472.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12, 11.8, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1472);

            // Row 1: SA-351, CE8MN, Castings
            var row1473 = new OldStressRowData();
            row1473.LineNo = 1;
            row1473.MaterialId = 9692;
            row1473.SpecNo = "SA-351";
            row1473.TypeGrade = "CE8MN";
            row1473.ProductForm = "Castings";
            row1473.AlloyUNS = "J93345";
            row1473.ClassCondition = "";
            row1473.Notes = "G1, G32";
            row1473.StressValues = new double?[] { 23.8, null, 23.7, null, 21.9, 21.1, 21.1, 21.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1473);

            // Row 2: SA-240, , Plate
            var row1474 = new OldStressRowData();
            row1474.LineNo = 2;
            row1474.MaterialId = 9693;
            row1474.SpecNo = "SA-240";
            row1474.TypeGrade = "";
            row1474.ProductForm = "Plate";
            row1474.AlloyUNS = "S44635";
            row1474.ClassCondition = "";
            row1474.Notes = "G32";
            row1474.StressValues = new double?[] { 22.5, null, 21.8, null, 20.5, 19.7, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1474);

            // Row 3: SA-268, , Wld. tube
            var row1475 = new OldStressRowData();
            row1475.LineNo = 3;
            row1475.MaterialId = 9694;
            row1475.SpecNo = "SA-268";
            row1475.TypeGrade = "";
            row1475.ProductForm = "Wld. tube";
            row1475.AlloyUNS = "S44635";
            row1475.ClassCondition = "";
            row1475.Notes = "G24, G32";
            row1475.StressValues = new double?[] { 19.1, null, 18.5, null, 17.4, 16.7, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1475);

            // Row 4: SA-351, CD4MCu, Castings
            var row1476 = new OldStressRowData();
            row1476.LineNo = 4;
            row1476.MaterialId = 9695;
            row1476.SpecNo = "SA-351";
            row1476.TypeGrade = "CD4MCu";
            row1476.ProductForm = "Castings";
            row1476.AlloyUNS = "J93370";
            row1476.ClassCondition = "";
            row1476.Notes = "G7, G29, G32, H4";
            row1476.StressValues = new double?[] { 25, null, 25, null, 24.4, 24.1, 24.1, 24, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1476);

            // Row 5: SA-240, , Plate
            var row1477 = new OldStressRowData();
            row1477.LineNo = 5;
            row1477.MaterialId = 9696;
            row1477.SpecNo = "SA-240";
            row1477.TypeGrade = "";
            row1477.ProductForm = "Plate";
            row1477.AlloyUNS = "S32550";
            row1477.ClassCondition = "";
            row1477.Notes = "G32, G33";
            row1477.StressValues = new double?[] { 27.5, null, 27.4, null, 25.7, 24.7, 24.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1477);

            // Row 6: SA-479, , Bar
            var row1478 = new OldStressRowData();
            row1478.LineNo = 6;
            row1478.MaterialId = 9697;
            row1478.SpecNo = "SA-479";
            row1478.TypeGrade = "";
            row1478.ProductForm = "Bar";
            row1478.AlloyUNS = "S32550";
            row1478.ClassCondition = "";
            row1478.Notes = "G32, G33";
            row1478.StressValues = new double?[] { 27.5, null, 27.4, null, 25.7, 24.7, 24.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1478);

            // Row 7: SA-789, , Smls. tube
            var row1479 = new OldStressRowData();
            row1479.LineNo = 7;
            row1479.MaterialId = 9698;
            row1479.SpecNo = "SA-789";
            row1479.TypeGrade = "";
            row1479.ProductForm = "Smls. tube";
            row1479.AlloyUNS = "S32550";
            row1479.ClassCondition = "";
            row1479.Notes = "G32, G33";
            row1479.StressValues = new double?[] { 27.5, null, 27.4, null, 25.7, 24.7, 24.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1479);

            // Row 8: SA-789, , Wld. tube
            var row1480 = new OldStressRowData();
            row1480.LineNo = 8;
            row1480.MaterialId = 9699;
            row1480.SpecNo = "SA-789";
            row1480.TypeGrade = "";
            row1480.ProductForm = "Wld. tube";
            row1480.AlloyUNS = "S32550";
            row1480.ClassCondition = "";
            row1480.Notes = "G24, G32, G33";
            row1480.StressValues = new double?[] { 23.4, null, 23.3, null, 21.9, 21, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1480);

            // Row 9: SA-790, , Smls. tube
            var row1481 = new OldStressRowData();
            row1481.LineNo = 9;
            row1481.MaterialId = 9700;
            row1481.SpecNo = "SA-790";
            row1481.TypeGrade = "";
            row1481.ProductForm = "Smls. tube";
            row1481.AlloyUNS = "S32550";
            row1481.ClassCondition = "";
            row1481.Notes = "G32, G33";
            row1481.StressValues = new double?[] { 27.5, null, 27.4, null, 25.7, 24.7, 24.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1481);

            // Row 10: SA-790, , Wld. pipe
            var row1482 = new OldStressRowData();
            row1482.LineNo = 10;
            row1482.MaterialId = 9701;
            row1482.SpecNo = "SA-790";
            row1482.TypeGrade = "";
            row1482.ProductForm = "Wld. pipe";
            row1482.AlloyUNS = "S32550";
            row1482.ClassCondition = "";
            row1482.Notes = "G24, G32, G33";
            row1482.StressValues = new double?[] { 23.4, null, 23.3, null, 21.9, 21, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1482);

            // Row 11: SA-240, , Plate
            var row1483 = new OldStressRowData();
            row1483.LineNo = 11;
            row1483.MaterialId = 9702;
            row1483.SpecNo = "SA-240";
            row1483.TypeGrade = "";
            row1483.ProductForm = "Plate";
            row1483.AlloyUNS = "S31200";
            row1483.ClassCondition = "";
            row1483.Notes = "G32";
            row1483.StressValues = new double?[] { 25, null, 25, null, 23.7, 23, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1483);

            // Row 12: SA-789, , Smls. tube
            var row1484 = new OldStressRowData();
            row1484.LineNo = 12;
            row1484.MaterialId = 9808;
            row1484.SpecNo = "SA-789";
            row1484.TypeGrade = "";
            row1484.ProductForm = "Smls. tube";
            row1484.AlloyUNS = "S31260";
            row1484.ClassCondition = "";
            row1484.Notes = "G32";
            row1484.StressValues = new double?[] { 25, null, 25, null, 23.7, 23.1, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1484);

            // Row 13: SA-789, , Wld. tube
            var row1485 = new OldStressRowData();
            row1485.LineNo = 13;
            row1485.MaterialId = 9809;
            row1485.SpecNo = "SA-789";
            row1485.TypeGrade = "";
            row1485.ProductForm = "Wld. tube";
            row1485.AlloyUNS = "S31260";
            row1485.ClassCondition = "";
            row1485.Notes = "G24, G32";
            row1485.StressValues = new double?[] { 21.3, null, 21.3, null, 20.1, 19.6, 19.6, 19.6, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1485);

            // Row 14: SA-790, , Smls. pipe
            var row1486 = new OldStressRowData();
            row1486.LineNo = 14;
            row1486.MaterialId = 9810;
            row1486.SpecNo = "SA-790";
            row1486.TypeGrade = "";
            row1486.ProductForm = "Smls. pipe";
            row1486.AlloyUNS = "S31260";
            row1486.ClassCondition = "";
            row1486.Notes = "G32";
            row1486.StressValues = new double?[] { 25, null, 25, null, 23.7, 23.1, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1486);

            // Row 15: SA-790, , Wld. pipe
            var row1487 = new OldStressRowData();
            row1487.LineNo = 15;
            row1487.MaterialId = 9811;
            row1487.SpecNo = "SA-790";
            row1487.TypeGrade = "";
            row1487.ProductForm = "Wld. pipe";
            row1487.AlloyUNS = "S31260";
            row1487.ClassCondition = "";
            row1487.Notes = "G24, G32";
            row1487.StressValues = new double?[] { 21.3, null, 21.3, null, 20.1, 19.6, 19.6, 19.6, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1487);

            // Row 16: SA-240, , Plate
            var row1488 = new OldStressRowData();
            row1488.LineNo = 16;
            row1488.MaterialId = 9812;
            row1488.SpecNo = "SA-240";
            row1488.TypeGrade = "";
            row1488.ProductForm = "Plate";
            row1488.AlloyUNS = "S31260";
            row1488.ClassCondition = "";
            row1488.Notes = "G32";
            row1488.StressValues = new double?[] { 25, null, 25, null, 23.7, 23.1, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1488);

            // Row 17: SA-789, , Smls. tube
            var row1489 = new OldStressRowData();
            row1489.LineNo = 17;
            row1489.MaterialId = 9703;
            row1489.SpecNo = "SA-789";
            row1489.TypeGrade = "";
            row1489.ProductForm = "Smls. tube";
            row1489.AlloyUNS = "S32750";
            row1489.ClassCondition = "";
            row1489.Notes = "G32, G33";
            row1489.StressValues = new double?[] { 29, null, 28.9, null, 27.3, 26.4, 25.9, 25.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1489);

            // Row 18: SA-789, , Wld. tube
            var row1490 = new OldStressRowData();
            row1490.LineNo = 18;
            row1490.MaterialId = 9704;
            row1490.SpecNo = "SA-789";
            row1490.TypeGrade = "";
            row1490.ProductForm = "Wld. tube";
            row1490.AlloyUNS = "S32750";
            row1490.ClassCondition = "";
            row1490.Notes = "G24, G32, G33";
            row1490.StressValues = new double?[] { 24.7, null, 24.6, null, 23.2, 22.4, 22, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1490);

            // Row 19: SA-790, , Smls. pipe
            var row1491 = new OldStressRowData();
            row1491.LineNo = 19;
            row1491.MaterialId = 9705;
            row1491.SpecNo = "SA-790";
            row1491.TypeGrade = "";
            row1491.ProductForm = "Smls. pipe";
            row1491.AlloyUNS = "S32750";
            row1491.ClassCondition = "";
            row1491.Notes = "G32, G33";
            row1491.StressValues = new double?[] { 29, null, 28.9, null, 27.3, 26.4, 25.9, 25.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1491);

            // Row 20: SA-790, , Wld. pipe
            var row1492 = new OldStressRowData();
            row1492.LineNo = 20;
            row1492.MaterialId = 9706;
            row1492.SpecNo = "SA-790";
            row1492.TypeGrade = "";
            row1492.ProductForm = "Wld. pipe";
            row1492.AlloyUNS = "S32750";
            row1492.ClassCondition = "";
            row1492.Notes = "G24, G32, G33";
            row1492.StressValues = new double?[] { 24.7, null, 24.6, null, 23.2, 22.4, 22, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1492);

            // Row 21: SA-351, CH8, Castings
            var row1493 = new OldStressRowData();
            row1493.LineNo = 21;
            row1493.MaterialId = 9707;
            row1493.SpecNo = "SA-351";
            row1493.TypeGrade = "CH8";
            row1493.ProductForm = "Castings";
            row1493.AlloyUNS = "J93400";
            row1493.ClassCondition = "";
            row1493.Notes = "G5, G16, G17, G32";
            row1493.StressValues = new double?[] { 16.2, null, 14.9, null, 14.2, 13.7, 13.4, 13.2, 13.1, 13, 13, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1493);

            // Row 22: SA-351, CH8, Castings
            var row1494 = new OldStressRowData();
            row1494.LineNo = 22;
            row1494.MaterialId = 9708;
            row1494.SpecNo = "SA-351";
            row1494.TypeGrade = "CH8";
            row1494.ProductForm = "Castings";
            row1494.AlloyUNS = "J93400";
            row1494.ClassCondition = "";
            row1494.Notes = "G1, G5, G12, G32";
            row1494.StressValues = new double?[] { 16.3, null, 14.9, null, 14.2, 13.8, 13.5, 13.3, 13.2, 13.1, 13, 13, 12.8, 12.5, 11.8, 10.5, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1494);

            // Row 23: SA-351, CH8, Castings
            var row1495 = new OldStressRowData();
            row1495.LineNo = 23;
            row1495.MaterialId = 9709;
            row1495.SpecNo = "SA-351";
            row1495.TypeGrade = "CH8";
            row1495.ProductForm = "Castings";
            row1495.AlloyUNS = "J93400";
            row1495.ClassCondition = "";
            row1495.Notes = "G1, G12, G32";
            row1495.StressValues = new double?[] { 16.3, null, 14.9, null, 14.2, 13.8, 13.2, 12.5, 12.2, 11.9, 11.7, 11.4, 11.1, 10.9, 10.8, 9.9, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1495);

            // Row 24: SA-451, CPH8, Cast pipe
            var row1496 = new OldStressRowData();
            row1496.LineNo = 24;
            row1496.MaterialId = 9710;
            row1496.SpecNo = "SA-451";
            row1496.TypeGrade = "CPH8";
            row1496.ProductForm = "Cast pipe";
            row1496.AlloyUNS = "J93400";
            row1496.ClassCondition = "";
            row1496.Notes = "G5, G16, G17, G32";
            row1496.StressValues = new double?[] { 16.2, null, 14.9, null, 14.2, 13.7, 13.4, 13.2, 13.1, 13, 13, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1496);

            // Row 25: SA-351, CH20, Castings
            var row1497 = new OldStressRowData();
            row1497.LineNo = 25;
            row1497.MaterialId = 9713;
            row1497.SpecNo = "SA-351";
            row1497.TypeGrade = "CH20";
            row1497.ProductForm = "Castings";
            row1497.AlloyUNS = "J93402";
            row1497.ClassCondition = "";
            row1497.Notes = "G5, G16, G17";
            row1497.StressValues = new double?[] { 17.5, null, 16, null, 15.3, 14.8, 14.5, 14.2, 14.1, 14, 14, 13.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1497);

            // Row 26: SA-351, CH20, Castings
            var row1498 = new OldStressRowData();
            row1498.LineNo = 26;
            row1498.MaterialId = 9711;
            row1498.SpecNo = "SA-351";
            row1498.TypeGrade = "CH20";
            row1498.ProductForm = "Castings";
            row1498.AlloyUNS = "J93402";
            row1498.ClassCondition = "";
            row1498.Notes = "G1, G5, G12";
            row1498.StressValues = new double?[] { 17.5, null, 16.1, null, 15.3, 14.8, 14.5, 14.3, 14.2, 14.1, 14, 13.9, 13.8, 13.3, 12.5, 10.5, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1498);

            // Row 27: SA-351, CH20, Castings
            var row1499 = new OldStressRowData();
            row1499.LineNo = 27;
            row1499.MaterialId = 9712;
            row1499.SpecNo = "SA-351";
            row1499.TypeGrade = "CH20";
            row1499.ProductForm = "Castings";
            row1499.AlloyUNS = "J93402";
            row1499.ClassCondition = "";
            row1499.Notes = "G1, G12";
            row1499.StressValues = new double?[] { 17.5, null, 16.1, null, 15.3, 14.8, 14.1, 13.4, 13.1, 12.7, 12.5, 12.2, 11.9, 11.7, 11.2, 10.2, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1499);

            // Row 28: SA-451, CPH20, Cast pipe
            var row1500 = new OldStressRowData();
            row1500.LineNo = 28;
            row1500.MaterialId = 9714;
            row1500.SpecNo = "SA-451";
            row1500.TypeGrade = "CPH20";
            row1500.ProductForm = "Cast pipe";
            row1500.AlloyUNS = "J93402";
            row1500.ClassCondition = "";
            row1500.Notes = "G5, G16, G17";
            row1500.StressValues = new double?[] { 17.5, null, 16, null, 15.3, 14.8, 14.5, 14.2, 14.1, 14, 14, 13.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1500);

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
