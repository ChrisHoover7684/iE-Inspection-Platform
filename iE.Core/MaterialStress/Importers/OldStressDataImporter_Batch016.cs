using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch016
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch016(MaterialStressService service)
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

            // Row 29: SA-351, CK20, Castings
            var row1501 = new OldStressRowData();
            row1501.LineNo = 29;
            row1501.MaterialId = 9715;
            row1501.SpecNo = "SA-351";
            row1501.TypeGrade = "CK20";
            row1501.ProductForm = "Castings";
            row1501.AlloyUNS = "J94202";
            row1501.ClassCondition = "";
            row1501.Notes = "G5, G16, G17";
            row1501.StressValues = new double?[] { 16.2, null, 14.9, null, 14.2, 13.7, 13.4, 13.2, 13.1, 13, 13, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1501);

            // Row 30: SA-351, CK20, Castings
            var row1502 = new OldStressRowData();
            row1502.LineNo = 30;
            row1502.MaterialId = 9716;
            row1502.SpecNo = "SA-351";
            row1502.TypeGrade = "CK20";
            row1502.ProductForm = "Castings";
            row1502.AlloyUNS = "J94202";
            row1502.ClassCondition = "";
            row1502.Notes = "G1, G5, G12";
            row1502.StressValues = new double?[] { 16.3, null, 14.9, null, 14.2, 13.8, 13.5, 13.3, 13.2, 13.1, 13, 13, 12.8, 12.5, 11.9, 11, 9.8, 8.5, 7.3, 6, 4.8, 3.5, 2.4, 1.6, 1.1, 0.8, null, null, null };
            batch.Add(row1502);

            // Row 31: SA-351, CK20, Castings
            var row1503 = new OldStressRowData();
            row1503.LineNo = 31;
            row1503.MaterialId = 9717;
            row1503.SpecNo = "SA-351";
            row1503.TypeGrade = "CK20";
            row1503.ProductForm = "Castings";
            row1503.AlloyUNS = "J94202";
            row1503.ClassCondition = "";
            row1503.Notes = "G1, G12";
            row1503.StressValues = new double?[] { 16.3, null, 14.9, null, 14.2, 13.8, 13.2, 12.5, 12.2, 11.9, 11.7, 11.4, 11.1, 10.9, 10.6, 10.3, 9.8, 8.5, 7.3, 6, 4.8, 3.5, 2.4, 1.6, 1.1, 0.8, null, null, null };
            batch.Add(row1503);

            // Row 32: SA-451, CPK20, Cast pipe
            var row1504 = new OldStressRowData();
            row1504.LineNo = 32;
            row1504.MaterialId = 9718;
            row1504.SpecNo = "SA-451";
            row1504.TypeGrade = "CPK20";
            row1504.ProductForm = "Cast pipe";
            row1504.AlloyUNS = "J94202";
            row1504.ClassCondition = "";
            row1504.Notes = "G5, G16, G17";
            row1504.StressValues = new double?[] { 16.2, null, 14.9, null, 14.2, 13.7, 13.4, 13.2, 13.1, 13, 13, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1504);

            // Row 33: SA-182, F310, Forgings
            var row1505 = new OldStressRowData();
            row1505.LineNo = 33;
            row1505.MaterialId = 9719;
            row1505.SpecNo = "SA-182";
            row1505.TypeGrade = "F310";
            row1505.ProductForm = "Forgings";
            row1505.AlloyUNS = "S31000";
            row1505.ClassCondition = "";
            row1505.Notes = "G5";
            row1505.StressValues = new double?[] { 17.5, null, 17.2, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1505);

            // Row 34: SA-182, F310, Forgings
            var row1506 = new OldStressRowData();
            row1506.LineNo = 34;
            row1506.MaterialId = 9720;
            row1506.SpecNo = "SA-182";
            row1506.TypeGrade = "F310";
            row1506.ProductForm = "Forgings";
            row1506.AlloyUNS = "S31000";
            row1506.ClassCondition = "";
            row1506.Notes = "G5, G12, G14";
            row1506.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1506);

            // Row 35: SA-182, F310, Forgings
            var row1507 = new OldStressRowData();
            row1507.LineNo = 35;
            row1507.MaterialId = 9721;
            row1507.SpecNo = "SA-182";
            row1507.TypeGrade = "F310";
            row1507.ProductForm = "Forgings";
            row1507.AlloyUNS = "S31000";
            row1507.ClassCondition = "";
            row1507.Notes = "G5, G12";
            row1507.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1507);

            // Row 36: SA-336, F310, Forgings
            var row1508 = new OldStressRowData();
            row1508.LineNo = 36;
            row1508.MaterialId = 9746;
            row1508.SpecNo = "SA-336";
            row1508.TypeGrade = "F310";
            row1508.ProductForm = "Forgings";
            row1508.AlloyUNS = "S31000";
            row1508.ClassCondition = "";
            row1508.Notes = "G5, G12";
            row1508.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1508);

            // Row 37: SA-336, F310, Forgings
            var row1509 = new OldStressRowData();
            row1509.LineNo = 37;
            row1509.MaterialId = 9747;
            row1509.SpecNo = "SA-336";
            row1509.TypeGrade = "F310";
            row1509.ProductForm = "Forgings";
            row1509.AlloyUNS = "S31000";
            row1509.ClassCondition = "";
            row1509.Notes = "G5, G12";
            row1509.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1509);

            // Row 1: ..., , 
            var row1510 = new OldStressRowData();
            row1510.LineNo = 1;
            row1510.MaterialId = 9749;
            row1510.SpecNo = "...";
            row1510.TypeGrade = "";
            row1510.ProductForm = "";
            row1510.AlloyUNS = "";
            row1510.ClassCondition = "";
            row1510.Notes = "";
            row1510.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1510);

            // Row 2: ..., , 
            var row1511 = new OldStressRowData();
            row1511.LineNo = 2;
            row1511.MaterialId = 9751;
            row1511.SpecNo = "...";
            row1511.TypeGrade = "";
            row1511.ProductForm = "";
            row1511.AlloyUNS = "";
            row1511.ClassCondition = "";
            row1511.Notes = "";
            row1511.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1511);

            // Row 3: SA-403, 310, Smls. & wld. fittings
            var row1512 = new OldStressRowData();
            row1512.LineNo = 3;
            row1512.MaterialId = 9754;
            row1512.SpecNo = "SA-403";
            row1512.TypeGrade = "310";
            row1512.ProductForm = "Smls. & wld. fittings";
            row1512.AlloyUNS = "S31000";
            row1512.ClassCondition = "";
            row1512.Notes = "G5, G12, W12, W15";
            row1512.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1512);

            // Row 4: ..., , 
            var row1513 = new OldStressRowData();
            row1513.LineNo = 4;
            row1513.MaterialId = 9750;
            row1513.SpecNo = "...";
            row1513.TypeGrade = "";
            row1513.ProductForm = "";
            row1513.AlloyUNS = "";
            row1513.ClassCondition = "";
            row1513.Notes = "";
            row1513.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1513);

            // Row 5: ..., , 
            var row1514 = new OldStressRowData();
            row1514.LineNo = 5;
            row1514.MaterialId = 9752;
            row1514.SpecNo = "...";
            row1514.TypeGrade = "";
            row1514.ProductForm = "";
            row1514.AlloyUNS = "";
            row1514.ClassCondition = "";
            row1514.Notes = "";
            row1514.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1514);

            // Row 6: ..., , 
            var row1515 = new OldStressRowData();
            row1515.LineNo = 6;
            row1515.MaterialId = 9753;
            row1515.SpecNo = "...";
            row1515.TypeGrade = "";
            row1515.ProductForm = "";
            row1515.AlloyUNS = "";
            row1515.ClassCondition = "";
            row1515.Notes = "";
            row1515.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1515);

            // Row 7: SA-213, TP310S, Smls. tube
            var row1516 = new OldStressRowData();
            row1516.LineNo = 7;
            row1516.MaterialId = 9724;
            row1516.SpecNo = "SA-213";
            row1516.TypeGrade = "TP310S";
            row1516.ProductForm = "Smls. tube";
            row1516.AlloyUNS = "S31008";
            row1516.ClassCondition = "";
            row1516.Notes = "G5, G12, G18";
            row1516.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1516);

            // Row 8: SA-213, TP310S, Smls. tube
            var row1517 = new OldStressRowData();
            row1517.LineNo = 8;
            row1517.MaterialId = 9725;
            row1517.SpecNo = "SA-213";
            row1517.TypeGrade = "TP310S";
            row1517.ProductForm = "Smls. tube";
            row1517.AlloyUNS = "S31008";
            row1517.ClassCondition = "";
            row1517.Notes = "G12, G18";
            row1517.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1517);

            // Row 9: SA-240, 310S, Plate
            var row1518 = new OldStressRowData();
            row1518.LineNo = 9;
            row1518.MaterialId = 9728;
            row1518.SpecNo = "SA-240";
            row1518.TypeGrade = "310S";
            row1518.ProductForm = "Plate";
            row1518.AlloyUNS = "S31008";
            row1518.ClassCondition = "";
            row1518.Notes = "G5, G12, G18";
            row1518.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1518);

            // Row 10: SA-240, 310S, Plate
            var row1519 = new OldStressRowData();
            row1519.LineNo = 10;
            row1519.MaterialId = 9729;
            row1519.SpecNo = "SA-240";
            row1519.TypeGrade = "310S";
            row1519.ProductForm = "Plate";
            row1519.AlloyUNS = "S31008";
            row1519.ClassCondition = "";
            row1519.Notes = "G12, G18";
            row1519.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1519);

            // Row 11: SA-249, TP310S, Wld. tube
            var row1520 = new OldStressRowData();
            row1520.LineNo = 11;
            row1520.MaterialId = 9733;
            row1520.SpecNo = "SA-249";
            row1520.TypeGrade = "TP310S";
            row1520.ProductForm = "Wld. tube";
            row1520.AlloyUNS = "S31008";
            row1520.ClassCondition = "";
            row1520.Notes = "G5, G12, G24";
            row1520.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1520);

            // Row 12: SA-249, TP310S, Wld. tube
            var row1521 = new OldStressRowData();
            row1521.LineNo = 12;
            row1521.MaterialId = 9734;
            row1521.SpecNo = "SA-249";
            row1521.TypeGrade = "TP310S";
            row1521.ProductForm = "Wld. tube";
            row1521.AlloyUNS = "S31008";
            row1521.ClassCondition = "";
            row1521.Notes = "G12, G24";
            row1521.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1521);

            // Row 13: SA-312, TP310S, Wld. & smls. pipe
            var row1522 = new OldStressRowData();
            row1522.LineNo = 13;
            row1522.MaterialId = 9739;
            row1522.SpecNo = "SA-312";
            row1522.TypeGrade = "TP310S";
            row1522.ProductForm = "Wld. & smls. pipe";
            row1522.AlloyUNS = "S31008";
            row1522.ClassCondition = "";
            row1522.Notes = "G5, W12";
            row1522.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1522);

            // Row 14: SA-312, TP310S, Smls. pipe
            var row1523 = new OldStressRowData();
            row1523.LineNo = 14;
            row1523.MaterialId = 9740;
            row1523.SpecNo = "SA-312";
            row1523.TypeGrade = "TP310S";
            row1523.ProductForm = "Smls. pipe";
            row1523.AlloyUNS = "S31008";
            row1523.ClassCondition = "";
            row1523.Notes = "G5, G12, G18";
            row1523.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1523);

            // Row 15: SA-312, TP310S, Smls. pipe
            var row1524 = new OldStressRowData();
            row1524.LineNo = 15;
            row1524.MaterialId = 9741;
            row1524.SpecNo = "SA-312";
            row1524.TypeGrade = "TP310S";
            row1524.ProductForm = "Smls. pipe";
            row1524.AlloyUNS = "S31008";
            row1524.ClassCondition = "";
            row1524.Notes = "G12, G18";
            row1524.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1524);

            // Row 16: SA-312, TP310S, Wld. pipe
            var row1525 = new OldStressRowData();
            row1525.LineNo = 16;
            row1525.MaterialId = 9744;
            row1525.SpecNo = "SA-312";
            row1525.TypeGrade = "TP310S";
            row1525.ProductForm = "Wld. pipe";
            row1525.AlloyUNS = "S31008";
            row1525.ClassCondition = "";
            row1525.Notes = "G3, G5, G12, G18";
            row1525.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1525);

            // Row 17: SA-312, TP310S, Wld. pipe
            var row1526 = new OldStressRowData();
            row1526.LineNo = 17;
            row1526.MaterialId = 9745;
            row1526.SpecNo = "SA-312";
            row1526.TypeGrade = "TP310S";
            row1526.ProductForm = "Wld. pipe";
            row1526.AlloyUNS = "S31008";
            row1526.ClassCondition = "";
            row1526.Notes = "G3, G12, G18";
            row1526.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1526);

            // Row 18: SA-312, TP310S, Wld. pipe
            var row1527 = new OldStressRowData();
            row1527.LineNo = 18;
            row1527.MaterialId = 9742;
            row1527.SpecNo = "SA-312";
            row1527.TypeGrade = "TP310S";
            row1527.ProductForm = "Wld. pipe";
            row1527.AlloyUNS = "S31008";
            row1527.ClassCondition = "";
            row1527.Notes = "G5, G12, G14, G24";
            row1527.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1527);

            // Row 19: SA-312, TP310S, Wld. pipe
            var row1528 = new OldStressRowData();
            row1528.LineNo = 19;
            row1528.MaterialId = 9743;
            row1528.SpecNo = "SA-312";
            row1528.TypeGrade = "TP310S";
            row1528.ProductForm = "Wld. pipe";
            row1528.AlloyUNS = "S31008";
            row1528.ClassCondition = "";
            row1528.Notes = "G12, G14, G24";
            row1528.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1528);

            // Row 20: SA-358, 310S, Wld. pipe
            var row1529 = new OldStressRowData();
            row1529.LineNo = 20;
            row1529.MaterialId = 9748;
            row1529.SpecNo = "SA-358";
            row1529.TypeGrade = "310S";
            row1529.ProductForm = "Wld. pipe";
            row1529.AlloyUNS = "S31008";
            row1529.ClassCondition = "1";
            row1529.Notes = "G5, W12";
            row1529.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1529);

            // Row 21: SA-479, 310S, Bar
            var row1530 = new OldStressRowData();
            row1530.LineNo = 21;
            row1530.MaterialId = 9758;
            row1530.SpecNo = "SA-479";
            row1530.TypeGrade = "310S";
            row1530.ProductForm = "Bar";
            row1530.AlloyUNS = "S31008";
            row1530.ClassCondition = "";
            row1530.Notes = "G12, G18, G22";
            row1530.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1530);

            // Row 22: SA-479, 310S, Bar
            var row1531 = new OldStressRowData();
            row1531.LineNo = 22;
            row1531.MaterialId = 9757;
            row1531.SpecNo = "SA-479";
            row1531.TypeGrade = "310S";
            row1531.ProductForm = "Bar";
            row1531.AlloyUNS = "S31008";
            row1531.ClassCondition = "";
            row1531.Notes = "G5, G12, G18, G22";
            row1531.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 12.3, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1531);

            // Row 23: SA-813, TP310S, Wld. pipe
            var row1532 = new OldStressRowData();
            row1532.LineNo = 23;
            row1532.MaterialId = 9759;
            row1532.SpecNo = "SA-813";
            row1532.TypeGrade = "TP310S";
            row1532.ProductForm = "Wld. pipe";
            row1532.AlloyUNS = "S31008";
            row1532.ClassCondition = "";
            row1532.Notes = "G5, G12, G24";
            row1532.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1532);

            // Row 24: SA-813, TP310S, Wld. pipe
            var row1533 = new OldStressRowData();
            row1533.LineNo = 24;
            row1533.MaterialId = 9760;
            row1533.SpecNo = "SA-813";
            row1533.TypeGrade = "TP310S";
            row1533.ProductForm = "Wld. pipe";
            row1533.AlloyUNS = "S31008";
            row1533.ClassCondition = "";
            row1533.Notes = "G12, G24";
            row1533.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1533);

            // Row 25: SA-814, TP310S, Wld. pipe
            var row1534 = new OldStressRowData();
            row1534.LineNo = 25;
            row1534.MaterialId = 9761;
            row1534.SpecNo = "SA-814";
            row1534.TypeGrade = "TP310S";
            row1534.ProductForm = "Wld. pipe";
            row1534.AlloyUNS = "S31008";
            row1534.ClassCondition = "";
            row1534.Notes = "G5, G12, G24";
            row1534.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1534);

            // Row 26: SA-814, TP310S, Wld. pipe
            var row1535 = new OldStressRowData();
            row1535.LineNo = 26;
            row1535.MaterialId = 9762;
            row1535.SpecNo = "SA-814";
            row1535.TypeGrade = "TP310S";
            row1535.ProductForm = "Wld. pipe";
            row1535.AlloyUNS = "S31008";
            row1535.ClassCondition = "";
            row1535.Notes = "G12, G24";
            row1535.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1535);

            // Row 27: SA-213, TP310H, Smls. tube
            var row1536 = new OldStressRowData();
            row1536.LineNo = 27;
            row1536.MaterialId = 9722;
            row1536.SpecNo = "SA-213";
            row1536.TypeGrade = "TP310H";
            row1536.ProductForm = "Smls. tube";
            row1536.AlloyUNS = "S31009";
            row1536.ClassCondition = "";
            row1536.Notes = "G5, G18";
            row1536.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1536);

            // Row 28: SA-213, TP310H, Smls. tube
            var row1537 = new OldStressRowData();
            row1537.LineNo = 28;
            row1537.MaterialId = 9723;
            row1537.SpecNo = "SA-213";
            row1537.TypeGrade = "TP310H";
            row1537.ProductForm = "Smls. tube";
            row1537.AlloyUNS = "S31009";
            row1537.ClassCondition = "";
            row1537.Notes = "G5, G18";
            row1537.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.1, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1537);

            // Row 29: SA-240, 310H, Plate
            var row1538 = new OldStressRowData();
            row1538.LineNo = 29;
            row1538.MaterialId = 9726;
            row1538.SpecNo = "SA-240";
            row1538.TypeGrade = "310H";
            row1538.ProductForm = "Plate";
            row1538.AlloyUNS = "S31009";
            row1538.ClassCondition = "";
            row1538.Notes = "G5, G18";
            row1538.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1538);

            // Row 30: SA-240, 310H, Plate
            var row1539 = new OldStressRowData();
            row1539.LineNo = 30;
            row1539.MaterialId = 9727;
            row1539.SpecNo = "SA-240";
            row1539.TypeGrade = "310H";
            row1539.ProductForm = "Plate";
            row1539.AlloyUNS = "S31009";
            row1539.ClassCondition = "";
            row1539.Notes = "G18";
            row1539.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.1, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1539);

            // Row 31: SA-249, TP310H, Wld. tube
            var row1540 = new OldStressRowData();
            row1540.LineNo = 31;
            row1540.MaterialId = 9730;
            row1540.SpecNo = "SA-249";
            row1540.TypeGrade = "TP310H";
            row1540.ProductForm = "Wld. tube";
            row1540.AlloyUNS = "S31009";
            row1540.ClassCondition = "";
            row1540.Notes = "G5, W12";
            row1540.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1540);

            // Row 32: SA-249, TP310H, Wld. tube
            var row1541 = new OldStressRowData();
            row1541.LineNo = 32;
            row1541.MaterialId = 9731;
            row1541.SpecNo = "SA-249";
            row1541.TypeGrade = "TP310H";
            row1541.ProductForm = "Wld. tube";
            row1541.AlloyUNS = "S31009";
            row1541.ClassCondition = "";
            row1541.Notes = "G5, G12, G24";
            row1541.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1541);

            // Row 33: SA-249, TP310H, Wld. tube
            var row1542 = new OldStressRowData();
            row1542.LineNo = 33;
            row1542.MaterialId = 9732;
            row1542.SpecNo = "SA-249";
            row1542.TypeGrade = "TP310H";
            row1542.ProductForm = "Wld. tube";
            row1542.AlloyUNS = "S31009";
            row1542.ClassCondition = "";
            row1542.Notes = "G12, G24";
            row1542.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.1, 11.1, 11, 10.8, 10.6, 10.5, 10.3, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1542);

            // Row 34: SA-312, TP310H, Smls. pipe
            var row1543 = new OldStressRowData();
            row1543.LineNo = 34;
            row1543.MaterialId = 9735;
            row1543.SpecNo = "SA-312";
            row1543.TypeGrade = "TP310H";
            row1543.ProductForm = "Smls. pipe";
            row1543.AlloyUNS = "S31009";
            row1543.ClassCondition = "";
            row1543.Notes = "G5, G18";
            row1543.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1543);

            // Row 35: SA-312, TP310H, Smls. pipe
            var row1544 = new OldStressRowData();
            row1544.LineNo = 35;
            row1544.MaterialId = 9736;
            row1544.SpecNo = "SA-312";
            row1544.TypeGrade = "TP310H";
            row1544.ProductForm = "Smls. pipe";
            row1544.AlloyUNS = "S31009";
            row1544.ClassCondition = "";
            row1544.Notes = "G18";
            row1544.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.1, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1544);

            // Row 36: SA-312, TP310H, Wld. pipe
            var row1545 = new OldStressRowData();
            row1545.LineNo = 36;
            row1545.MaterialId = 9737;
            row1545.SpecNo = "SA-312";
            row1545.TypeGrade = "TP310H";
            row1545.ProductForm = "Wld. pipe";
            row1545.AlloyUNS = "S31009";
            row1545.ClassCondition = "";
            row1545.Notes = "G3, G5, G18, G24";
            row1545.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1545);

            // Row 37: SA-312, TP310H, Wld. pipe
            var row1546 = new OldStressRowData();
            row1546.LineNo = 37;
            row1546.MaterialId = 9738;
            row1546.SpecNo = "SA-312";
            row1546.TypeGrade = "TP310H";
            row1546.ProductForm = "Wld. pipe";
            row1546.AlloyUNS = "S31009";
            row1546.ClassCondition = "";
            row1546.Notes = "G3, G18, G24";
            row1546.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.1, 11.1, 11, 10.8, 10.6, 10.5, 10.3, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1546);

            // Row 38: SA-479, 310H, Bar
            var row1547 = new OldStressRowData();
            row1547.LineNo = 38;
            row1547.MaterialId = 9755;
            row1547.SpecNo = "SA-479";
            row1547.TypeGrade = "310H";
            row1547.ProductForm = "Bar";
            row1547.AlloyUNS = "S31009";
            row1547.ClassCondition = "";
            row1547.Notes = "G5, G18";
            row1547.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1547);

            // Row 39: SA-479, 310H, Bar
            var row1548 = new OldStressRowData();
            row1548.LineNo = 39;
            row1548.MaterialId = 9756;
            row1548.SpecNo = "SA-479";
            row1548.TypeGrade = "310H";
            row1548.ProductForm = "Bar";
            row1548.AlloyUNS = "S31009";
            row1548.ClassCondition = "";
            row1548.Notes = "G18";
            row1548.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.1, 13.1, 12.9, 12.7, 12.5, 12.3, 12.1, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 0.97, 0.75, null, null, null };
            batch.Add(row1548);

            // Row 1: SA-213, TP310Cb, Smls. tube
            var row1549 = new OldStressRowData();
            row1549.LineNo = 1;
            row1549.MaterialId = 9764;
            row1549.SpecNo = "SA-213";
            row1549.TypeGrade = "TP310Cb";
            row1549.ProductForm = "Smls. tube";
            row1549.AlloyUNS = "S31040";
            row1549.ClassCondition = "";
            row1549.Notes = "G5, G12";
            row1549.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1549);

            // Row 2: SA-213, TP310Cb, Smls. tube
            var row1550 = new OldStressRowData();
            row1550.LineNo = 2;
            row1550.MaterialId = 9763;
            row1550.SpecNo = "SA-213";
            row1550.TypeGrade = "TP310Cb";
            row1550.ProductForm = "Smls. tube";
            row1550.AlloyUNS = "S31040";
            row1550.ClassCondition = "";
            row1550.Notes = "G12";
            row1550.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1550);

            // Row 3: SA-240, 310Cb, Plate
            var row1551 = new OldStressRowData();
            row1551.LineNo = 3;
            row1551.MaterialId = 9765;
            row1551.SpecNo = "SA-240";
            row1551.TypeGrade = "310Cb";
            row1551.ProductForm = "Plate";
            row1551.AlloyUNS = "S31040";
            row1551.ClassCondition = "";
            row1551.Notes = "G5, G12";
            row1551.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1551);

            // Row 4: SA-240, 310Cb, Plate
            var row1552 = new OldStressRowData();
            row1552.LineNo = 4;
            row1552.MaterialId = 9766;
            row1552.SpecNo = "SA-240";
            row1552.TypeGrade = "310Cb";
            row1552.ProductForm = "Plate";
            row1552.AlloyUNS = "S31040";
            row1552.ClassCondition = "";
            row1552.Notes = "G12";
            row1552.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1552);

            // Row 5: SA-249, TP310Cb, Wld. tube
            var row1553 = new OldStressRowData();
            row1553.LineNo = 5;
            row1553.MaterialId = 9767;
            row1553.SpecNo = "SA-249";
            row1553.TypeGrade = "TP310Cb";
            row1553.ProductForm = "Wld. tube";
            row1553.AlloyUNS = "S31040";
            row1553.ClassCondition = "";
            row1553.Notes = "G5, G12, G24";
            row1553.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1553);

            // Row 6: SA-249, TP310Cb, Wld. tube
            var row1554 = new OldStressRowData();
            row1554.LineNo = 6;
            row1554.MaterialId = 9768;
            row1554.SpecNo = "SA-249";
            row1554.TypeGrade = "TP310Cb";
            row1554.ProductForm = "Wld. tube";
            row1554.AlloyUNS = "S31040";
            row1554.ClassCondition = "";
            row1554.Notes = "G12, G24";
            row1554.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1554);

            // Row 7: SA-312, TP310Cb, Wld. & smls. pipe
            var row1555 = new OldStressRowData();
            row1555.LineNo = 7;
            row1555.MaterialId = 9769;
            row1555.SpecNo = "SA-312";
            row1555.TypeGrade = "TP310Cb";
            row1555.ProductForm = "Wld. & smls. pipe";
            row1555.AlloyUNS = "S31040";
            row1555.ClassCondition = "";
            row1555.Notes = "G5, G12, W12";
            row1555.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1555);

            // Row 8: SA-312, TP310Cb, Smls. pipe
            var row1556 = new OldStressRowData();
            row1556.LineNo = 8;
            row1556.MaterialId = 9770;
            row1556.SpecNo = "SA-312";
            row1556.TypeGrade = "TP310Cb";
            row1556.ProductForm = "Smls. pipe";
            row1556.AlloyUNS = "S31040";
            row1556.ClassCondition = "";
            row1556.Notes = "G12";
            row1556.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1556);

            // Row 9: SA-312, TP310Cb, Wld. pipe
            var row1557 = new OldStressRowData();
            row1557.LineNo = 9;
            row1557.MaterialId = 9771;
            row1557.SpecNo = "SA-312";
            row1557.TypeGrade = "TP310Cb";
            row1557.ProductForm = "Wld. pipe";
            row1557.AlloyUNS = "S31040";
            row1557.ClassCondition = "";
            row1557.Notes = "G5, G12, G24";
            row1557.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1557);

            // Row 10: SA-312, TP310Cb, Wld. pipe
            var row1558 = new OldStressRowData();
            row1558.LineNo = 10;
            row1558.MaterialId = 9772;
            row1558.SpecNo = "SA-312";
            row1558.TypeGrade = "TP310Cb";
            row1558.ProductForm = "Wld. pipe";
            row1558.AlloyUNS = "S31040";
            row1558.ClassCondition = "";
            row1558.Notes = "G12, G14, G24";
            row1558.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1558);

            // Row 11: SA-479, 310Cb, Bar
            var row1559 = new OldStressRowData();
            row1559.LineNo = 11;
            row1559.MaterialId = 9773;
            row1559.SpecNo = "SA-479";
            row1559.TypeGrade = "310Cb";
            row1559.ProductForm = "Bar";
            row1559.AlloyUNS = "S31040";
            row1559.ClassCondition = "";
            row1559.Notes = "G5, G12, G22";
            row1559.StressValues = new double?[] { 18.8, null, 18.4, null, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.2, 17, 16.7, 16.3, 15.9, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1559);

            // Row 12: SA-479, 310Cb, Bar
            var row1560 = new OldStressRowData();
            row1560.LineNo = 12;
            row1560.MaterialId = 9774;
            row1560.SpecNo = "SA-479";
            row1560.TypeGrade = "310Cb";
            row1560.ProductForm = "Bar";
            row1560.AlloyUNS = "S31040";
            row1560.ClassCondition = "";
            row1560.Notes = "G12, G22";
            row1560.StressValues = new double?[] { 18.8, null, 17.6, null, 16.1, 15.1, 14.3, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1560);

            // Row 13: SA-813, TP310Cb, Wld. pipe
            var row1561 = new OldStressRowData();
            row1561.LineNo = 13;
            row1561.MaterialId = 9775;
            row1561.SpecNo = "SA-813";
            row1561.TypeGrade = "TP310Cb";
            row1561.ProductForm = "Wld. pipe";
            row1561.AlloyUNS = "S31040";
            row1561.ClassCondition = "";
            row1561.Notes = "G5, G12, G24";
            row1561.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1561);

            // Row 14: SA-813, TP310Cb, Wld. pipe
            var row1562 = new OldStressRowData();
            row1562.LineNo = 14;
            row1562.MaterialId = 9776;
            row1562.SpecNo = "SA-813";
            row1562.TypeGrade = "TP310Cb";
            row1562.ProductForm = "Wld. pipe";
            row1562.AlloyUNS = "S31040";
            row1562.ClassCondition = "";
            row1562.Notes = "G12, G24";
            row1562.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1562);

            // Row 15: SA-814, TP310Cb, Wld. pipe
            var row1563 = new OldStressRowData();
            row1563.LineNo = 15;
            row1563.MaterialId = 9777;
            row1563.SpecNo = "SA-814";
            row1563.TypeGrade = "TP310Cb";
            row1563.ProductForm = "Wld. pipe";
            row1563.AlloyUNS = "S31040";
            row1563.ClassCondition = "";
            row1563.Notes = "G5, G12, G24";
            row1563.StressValues = new double?[] { 16, null, 15.6, null, 15, 14.8, 14.8, 14.8, 14.8, 14.8, 14.6, 14.5, 14.2, 13.9, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1563);

            // Row 16: SA-814, TP310Cb, Wld. pipe
            var row1564 = new OldStressRowData();
            row1564.LineNo = 16;
            row1564.MaterialId = 9836;
            row1564.SpecNo = "SA-814";
            row1564.TypeGrade = "TP310Cb";
            row1564.ProductForm = "Wld. pipe";
            row1564.AlloyUNS = "S31040";
            row1564.ClassCondition = "";
            row1564.Notes = "G12, G24";
            row1564.StressValues = new double?[] { 16, null, 15, null, 13.7, 12.8, 12.2, 11.6, 11.5, 11.3, 11.1, 11, 10.8, 10.6, 10.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1564);

            // Row 17: SA-213, TP310MoLN, Smls. tube
            var row1565 = new OldStressRowData();
            row1565.LineNo = 17;
            row1565.MaterialId = 9778;
            row1565.SpecNo = "SA-213";
            row1565.TypeGrade = "TP310MoLN";
            row1565.ProductForm = "Smls. tube";
            row1565.AlloyUNS = "S31050";
            row1565.ClassCondition = "";
            row1565.Notes = "G5";
            row1565.StressValues = new double?[] { 19.5, null, 19.3, null, 18.2, 17.5, 17, 16.7, 16.5, 16.4, 16.2, 16, 15.9, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1565);

            // Row 18: SA-213, TP310MoLN, Smls. tube
            var row1566 = new OldStressRowData();
            row1566.LineNo = 18;
            row1566.MaterialId = 9825;
            row1566.SpecNo = "SA-213";
            row1566.TypeGrade = "TP310MoLN";
            row1566.ProductForm = "Smls. tube";
            row1566.AlloyUNS = "S31050";
            row1566.ClassCondition = "";
            row1566.Notes = "";
            row1566.StressValues = new double?[] { 19.5, null, 19.3, null, 18.2, 17.5, 16.8, 15.9, 15.5, 15.1, 14.8, 14.4, 14, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1566);

            // Row 19: SA-249, TP310MoLN, Wld. tube
            var row1567 = new OldStressRowData();
            row1567.LineNo = 19;
            row1567.MaterialId = 9826;
            row1567.SpecNo = "SA-249";
            row1567.TypeGrade = "TP310MoLN";
            row1567.ProductForm = "Wld. tube";
            row1567.AlloyUNS = "S31050";
            row1567.ClassCondition = "";
            row1567.Notes = "G5, G24";
            row1567.StressValues = new double?[] { 16.6, null, 16.4, null, 15.5, 14.9, 14.5, 14.2, 14, 13.9, 13.8, 13.6, 13.5, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1567);

            // Row 20: SA-249, TP310MoLN, Wld. tube
            var row1568 = new OldStressRowData();
            row1568.LineNo = 20;
            row1568.MaterialId = 9827;
            row1568.SpecNo = "SA-249";
            row1568.TypeGrade = "TP310MoLN";
            row1568.ProductForm = "Wld. tube";
            row1568.AlloyUNS = "S31050";
            row1568.ClassCondition = "";
            row1568.Notes = "G24";
            row1568.StressValues = new double?[] { 16.6, null, 16.4, null, 15.5, 14.9, 14.3, 13.5, 13.2, 12.8, 12.6, 12.2, 11.9, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1568);

            // Row 21: SA-312, TP310MoLN, Wld. pipe
            var row1569 = new OldStressRowData();
            row1569.LineNo = 21;
            row1569.MaterialId = 9828;
            row1569.SpecNo = "SA-312";
            row1569.TypeGrade = "TP310MoLN";
            row1569.ProductForm = "Wld. pipe";
            row1569.AlloyUNS = "S31050";
            row1569.ClassCondition = "";
            row1569.Notes = "G5, G24";
            row1569.StressValues = new double?[] { 16.6, null, 16.4, null, 15.5, 14.9, 14.5, 14.2, 14, 13.9, 13.8, 13.6, 13.5, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1569);

            // Row 22: SA-312, TP310MoLN, Wld. pipe
            var row1570 = new OldStressRowData();
            row1570.LineNo = 22;
            row1570.MaterialId = 9829;
            row1570.SpecNo = "SA-312";
            row1570.TypeGrade = "TP310MoLN";
            row1570.ProductForm = "Wld. pipe";
            row1570.AlloyUNS = "S31050";
            row1570.ClassCondition = "";
            row1570.Notes = "G24";
            row1570.StressValues = new double?[] { 16.6, null, 16.4, null, 15.5, 14.9, 14.3, 13.5, 13.2, 12.8, 12.6, 12.2, 11.9, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1570);

            // Row 23: SA-240, 310MoLN, Plate
            var row1571 = new OldStressRowData();
            row1571.LineNo = 23;
            row1571.MaterialId = 9830;
            row1571.SpecNo = "SA-240";
            row1571.TypeGrade = "310MoLN";
            row1571.ProductForm = "Plate";
            row1571.AlloyUNS = "S31050";
            row1571.ClassCondition = "";
            row1571.Notes = "G5";
            row1571.StressValues = new double?[] { 20, null, 20, null, 18.7, 18, 17.5, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1571);

            // Row 24: SA-240, 310MoLN, Plate
            var row1572 = new OldStressRowData();
            row1572.LineNo = 24;
            row1572.MaterialId = 9831;
            row1572.SpecNo = "SA-240";
            row1572.TypeGrade = "310MoLN";
            row1572.ProductForm = "Plate";
            row1572.AlloyUNS = "S31050";
            row1572.ClassCondition = "";
            row1572.Notes = "";
            row1572.StressValues = new double?[] { 20, null, 20, null, 18.1, 16.8, 15.9, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1572);

            // Row 25: SA-213, TP310MoLN, Smls. tube
            var row1573 = new OldStressRowData();
            row1573.LineNo = 25;
            row1573.MaterialId = 9832;
            row1573.SpecNo = "SA-213";
            row1573.TypeGrade = "TP310MoLN";
            row1573.ProductForm = "Smls. tube";
            row1573.AlloyUNS = "S31050";
            row1573.ClassCondition = "";
            row1573.Notes = "G5";
            row1573.StressValues = new double?[] { 21, null, 20.8, null, 19.6, 18.8, 18.3, 18, 17.8, 17.6, 17.4, 17.3, 17.1, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1573);

            // Row 26: SA-213, TP310MoLN, Smls. tube
            var row1574 = new OldStressRowData();
            row1574.LineNo = 26;
            row1574.MaterialId = 9833;
            row1574.SpecNo = "SA-213";
            row1574.TypeGrade = "TP310MoLN";
            row1574.ProductForm = "Smls. tube";
            row1574.AlloyUNS = "S31050";
            row1574.ClassCondition = "";
            row1574.Notes = "";
            row1574.StressValues = new double?[] { 21, null, 20.8, null, 19.6, 18.7, 17.7, 16.8, 16.4, 16, 15.6, 15.2, 14.8, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1574);

            // Row 27: SA-249, TP310MoLN, Wld. tube
            var row1575 = new OldStressRowData();
            row1575.LineNo = 27;
            row1575.MaterialId = 9835;
            row1575.SpecNo = "SA-249";
            row1575.TypeGrade = "TP310MoLN";
            row1575.ProductForm = "Wld. tube";
            row1575.AlloyUNS = "S31050";
            row1575.ClassCondition = "";
            row1575.Notes = "G5, G24";
            row1575.StressValues = new double?[] { 17.9, null, 17.7, null, 16.7, 16, 15.6, 15.3, 15.1, 15, 14.8, 14.7, 14.5, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1575);

            // Row 28: SA-249, TP310MoLN, Wld. tube
            var row1576 = new OldStressRowData();
            row1576.LineNo = 28;
            row1576.MaterialId = 9834;
            row1576.SpecNo = "SA-249";
            row1576.TypeGrade = "TP310MoLN";
            row1576.ProductForm = "Wld. tube";
            row1576.AlloyUNS = "S31050";
            row1576.ClassCondition = "";
            row1576.Notes = "G24";
            row1576.StressValues = new double?[] { 17.9, null, 17.7, null, 16.7, 15.9, 15, 14.3, 13.9, 13.6, 13.3, 12.9, 12.6, 12.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1576);

            // Row 29: SA-312, TP310MoLN, Wld. pipe
            var row1577 = new OldStressRowData();
            row1577.LineNo = 29;
            row1577.MaterialId = 9837;
            row1577.SpecNo = "SA-312";
            row1577.TypeGrade = "TP310MoLN";
            row1577.ProductForm = "Wld. pipe";
            row1577.AlloyUNS = "S31050";
            row1577.ClassCondition = "";
            row1577.Notes = "G5, G24";
            row1577.StressValues = new double?[] { 17.9, null, 17.7, null, 16.7, 16, 15.6, 15.3, 15.1, 15, 14.8, 14.7, 14.5, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1577);

            // Row 30: SA-312, TP310MoLN, Wld. pipe
            var row1578 = new OldStressRowData();
            row1578.LineNo = 30;
            row1578.MaterialId = 9838;
            row1578.SpecNo = "SA-312";
            row1578.TypeGrade = "TP310MoLN";
            row1578.ProductForm = "Wld. pipe";
            row1578.AlloyUNS = "S31050";
            row1578.ClassCondition = "";
            row1578.Notes = "G24";
            row1578.StressValues = new double?[] { 17.9, null, 17.7, null, 16.7, 15.9, 15, 14.3, 13.9, 13.6, 13.3, 12.9, 12.6, 12.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1578);

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
