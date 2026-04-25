using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch005
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch005(MaterialStressService service)
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

            // Row 26: SA-268, TP429, Wld. tube
            var row401 = new OldStressRowData();
            row401.LineNo = 26;
            row401.MaterialId = 8705;
            row401.SpecNo = "SA-268";
            row401.TypeGrade = "TP429";
            row401.ProductForm = "Wld. tube";
            row401.AlloyUNS = "S42900";
            row401.ClassCondition = "";
            row401.Notes = "G3, G32";
            row401.StressValues = new double?[] { 14.6, null, 14.6, null, 14.3, 14, 13.8, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row401);

            // Row 27: SA-268, TP429, Smls. & wld. tube
            var row402 = new OldStressRowData();
            row402.LineNo = 27;
            row402.MaterialId = 8704;
            row402.SpecNo = "SA-268";
            row402.TypeGrade = "TP429";
            row402.ProductForm = "Smls. & wld. tube";
            row402.AlloyUNS = "S42900";
            row402.ClassCondition = "";
            row402.Notes = "G32, T4, W13";
            row402.StressValues = new double?[] { 17.1, null, 17.1, null, 16.8, 16.5, 16.3, 15.9, 15.6, 15.2, 14.7, 14.1, 13.4, 12, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row402);

            // Row 28: SA-240, 429, Plate
            var row403 = new OldStressRowData();
            row403.LineNo = 28;
            row403.MaterialId = 8706;
            row403.SpecNo = "SA-240";
            row403.TypeGrade = "429";
            row403.ProductForm = "Plate";
            row403.AlloyUNS = "S42900";
            row403.ClassCondition = "";
            row403.Notes = "G32, T4";
            row403.StressValues = new double?[] { 18.6, null, 18.4, null, 17.8, 17.4, 17.2, 16.8, 16.6, 16.2, 15.7, 15.1, 14.4, 12, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row403);

            // Row 29: SA-268, TP430, Wld. tube
            var row404 = new OldStressRowData();
            row404.LineNo = 29;
            row404.MaterialId = 8707;
            row404.SpecNo = "SA-268";
            row404.TypeGrade = "TP430";
            row404.ProductForm = "Wld. tube";
            row404.AlloyUNS = "S43000";
            row404.ClassCondition = "";
            row404.Notes = "G3, G24, G32, T4";
            row404.StressValues = new double?[] { 14.6, null, 14.6, null, 14.3, 14, 13.8, 13.5, 13.2, 12.9, 12.5, 12, 11.4, 10.2, 7.8, 5.5, 3.8, 2.7, 2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row404);

            // Row 30: SA-268, TP430, Smls. & wld. tube
            var row405 = new OldStressRowData();
            row405.LineNo = 30;
            row405.MaterialId = 8708;
            row405.SpecNo = "SA-268";
            row405.TypeGrade = "TP430";
            row405.ProductForm = "Smls. & wld. tube";
            row405.AlloyUNS = "S43000";
            row405.ClassCondition = "";
            row405.Notes = "G32, T4, W12, W13";
            row405.StressValues = new double?[] { 17.1, null, 17.1, null, 16.8, 16.5, 16.3, 15.9, 15.6, 15.2, 14.7, 14.1, 13.4, 12, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row405);

            // Row 31: SA-240, 430, Plate
            var row406 = new OldStressRowData();
            row406.LineNo = 31;
            row406.MaterialId = 8710;
            row406.SpecNo = "SA-240";
            row406.TypeGrade = "430";
            row406.ProductForm = "Plate";
            row406.AlloyUNS = "S43000";
            row406.ClassCondition = "";
            row406.Notes = "G32, T4";
            row406.StressValues = new double?[] { 18.6, null, 18.4, null, 17.8, 17.4, 17.2, 16.8, 16.6, 16.2, 15.7, 15.1, 14.4, 12, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row406);

            // Row 32: SA-479, 430, Bar
            var row407 = new OldStressRowData();
            row407.LineNo = 32;
            row407.MaterialId = 8711;
            row407.SpecNo = "SA-479";
            row407.TypeGrade = "430";
            row407.ProductForm = "Bar";
            row407.AlloyUNS = "S43000";
            row407.ClassCondition = "";
            row407.Notes = "G18, G22, G32, T4";
            row407.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, 17.1, 16.4, 15.6, 12, 9.2, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row407);

            // Row 33: SA-747, CB7Cu-1, Castings
            var row408 = new OldStressRowData();
            row408.LineNo = 33;
            row408.MaterialId = 8713;
            row408.SpecNo = "SA-747";
            row408.TypeGrade = "CB7Cu-1";
            row408.ProductForm = "Castings";
            row408.AlloyUNS = "J92180";
            row408.ClassCondition = "";
            row408.Notes = "G1, W1";
            row408.StressValues = new double?[] { 32, null, 32, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row408);

            // Row 34: SA-564, 630, Bar
            var row409 = new OldStressRowData();
            row409.LineNo = 34;
            row409.MaterialId = 8714;
            row409.SpecNo = "SA-564";
            row409.TypeGrade = "630";
            row409.ProductForm = "Bar";
            row409.AlloyUNS = "S17400";
            row409.ClassCondition = "H1150";
            row409.Notes = "G8, G31, G32, W1";
            row409.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 37.5, 36.8, 36.2, 35.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row409);

            // Row 35: SA-693, 630, Plate
            var row410 = new OldStressRowData();
            row410.LineNo = 35;
            row410.MaterialId = 8715;
            row410.SpecNo = "SA-693";
            row410.TypeGrade = "630";
            row410.ProductForm = "Plate";
            row410.AlloyUNS = "S17400";
            row410.ClassCondition = "H1150";
            row410.Notes = "G8, W1";
            row410.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 37.5, 36.8, 36.2, 35.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row410);

            // Row 36: SA-705, 630, Forgings
            var row411 = new OldStressRowData();
            row411.LineNo = 36;
            row411.MaterialId = 8716;
            row411.SpecNo = "SA-705";
            row411.TypeGrade = "630";
            row411.ProductForm = "Forgings";
            row411.AlloyUNS = "S17400";
            row411.ClassCondition = "H1150";
            row411.Notes = "G8, W1";
            row411.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 37.5, 36.8, 36.2, 35.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row411);

            // Row 37: SA-564, 630, Bar
            var row412 = new OldStressRowData();
            row412.LineNo = 37;
            row412.MaterialId = 8717;
            row412.SpecNo = "SA-564";
            row412.TypeGrade = "630";
            row412.ProductForm = "Bar";
            row412.AlloyUNS = "S17400";
            row412.ClassCondition = "H1100";
            row412.Notes = "G8, G31, G32, W1";
            row412.StressValues = new double?[] { 40, null, 40, null, 40, 38.9, 38.1, 37.5, 37.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row412);

            // Row 38: SA-693, 630, Plate
            var row413 = new OldStressRowData();
            row413.LineNo = 38;
            row413.MaterialId = 8718;
            row413.SpecNo = "SA-693";
            row413.TypeGrade = "630";
            row413.ProductForm = "Plate";
            row413.AlloyUNS = "S17400";
            row413.ClassCondition = "H1100";
            row413.Notes = "G8, W1";
            row413.StressValues = new double?[] { 40, null, 40, null, 40, 38.9, 38.1, 37.5, 37.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row413);

            // Row 39: SA-705, 630, Forgings
            var row414 = new OldStressRowData();
            row414.LineNo = 39;
            row414.MaterialId = 8719;
            row414.SpecNo = "SA-705";
            row414.TypeGrade = "630";
            row414.ProductForm = "Forgings";
            row414.AlloyUNS = "S17400";
            row414.ClassCondition = "H1100";
            row414.Notes = "G8, W1";
            row414.StressValues = new double?[] { 40, null, 40, null, 40, 38.9, 38.1, 37.5, 37.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row414);

            // Row 1: SA-564, 630, Bar
            var row415 = new OldStressRowData();
            row415.LineNo = 1;
            row415.MaterialId = 8720;
            row415.SpecNo = "SA-564";
            row415.TypeGrade = "630";
            row415.ProductForm = "Bar";
            row415.AlloyUNS = "S17400";
            row415.ClassCondition = "H1075";
            row415.Notes = "G8, W1";
            row415.StressValues = new double?[] { 41.4, null, 41.4, null, 41.4, 40.3, 39.5, 38.9, 38.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row415);

            // Row 2: SA-693, 630, Plate
            var row416 = new OldStressRowData();
            row416.LineNo = 2;
            row416.MaterialId = 8721;
            row416.SpecNo = "SA-693";
            row416.TypeGrade = "630";
            row416.ProductForm = "Plate";
            row416.AlloyUNS = "S17400";
            row416.ClassCondition = "H1075";
            row416.Notes = "G8, W1";
            row416.StressValues = new double?[] { 41.4, null, 41.4, null, 41.4, 40.3, 39.5, 38.9, 38.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row416);

            // Row 3: SA-705, 630, Forgings
            var row417 = new OldStressRowData();
            row417.LineNo = 3;
            row417.MaterialId = 8722;
            row417.SpecNo = "SA-705";
            row417.TypeGrade = "630";
            row417.ProductForm = "Forgings";
            row417.AlloyUNS = "S17400";
            row417.ClassCondition = "H1075";
            row417.Notes = "G8, W1";
            row417.StressValues = new double?[] { 41.4, null, 41.4, null, 41.4, 40.3, 39.5, 38.9, 38.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row417);

            // Row 4: SA-240, 201-1, Plate
            var row418 = new OldStressRowData();
            row418.LineNo = 4;
            row418.MaterialId = 8723;
            row418.SpecNo = "SA-240";
            row418.TypeGrade = "201-1";
            row418.ProductForm = "Plate";
            row418.AlloyUNS = "S20100";
            row418.ClassCondition = "";
            row418.Notes = "";
            row418.StressValues = new double?[] { 25.3, null, 19.3, null, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row418);

            // Row 5: SA-666, 201-1, Plate
            var row419 = new OldStressRowData();
            row419.LineNo = 5;
            row419.MaterialId = 9784;
            row419.SpecNo = "SA-666";
            row419.TypeGrade = "201-1";
            row419.ProductForm = "Plate";
            row419.AlloyUNS = "S20100";
            row419.ClassCondition = "";
            row419.Notes = "";
            row419.StressValues = new double?[] { 25.3, null, 19.3, null, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row419);

            // Row 6: SA-240, 201-2, Plate
            var row420 = new OldStressRowData();
            row420.LineNo = 6;
            row420.MaterialId = 8724;
            row420.SpecNo = "SA-240";
            row420.TypeGrade = "201-2";
            row420.ProductForm = "Plate";
            row420.AlloyUNS = "S20100";
            row420.ClassCondition = "";
            row420.Notes = "";
            row420.StressValues = new double?[] { 27.1, null, 22.8, null, 19.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row420);

            // Row 7: SA-666, 201-2, Plate
            var row421 = new OldStressRowData();
            row421.LineNo = 7;
            row421.MaterialId = 9785;
            row421.SpecNo = "SA-666";
            row421.TypeGrade = "201-2";
            row421.ProductForm = "Plate";
            row421.AlloyUNS = "S20100";
            row421.ClassCondition = "";
            row421.Notes = "";
            row421.StressValues = new double?[] { 27.1, null, 22.8, null, 19.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row421);

            // Row 8: SA-240, , Plate
            var row422 = new OldStressRowData();
            row422.LineNo = 8;
            row422.MaterialId = 9082;
            row422.SpecNo = "SA-240";
            row422.TypeGrade = "";
            row422.ProductForm = "Plate";
            row422.AlloyUNS = "S44400";
            row422.ClassCondition = "";
            row422.Notes = "G32";
            row422.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.2, 15.9, 15.4, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row422);

            // Row 9: SA-268, , Wld. tube
            var row423 = new OldStressRowData();
            row423.LineNo = 9;
            row423.MaterialId = 9083;
            row423.SpecNo = "SA-268";
            row423.TypeGrade = "";
            row423.ProductForm = "Wld. tube";
            row423.AlloyUNS = "S44400";
            row423.ClassCondition = "";
            row423.Notes = "G24, G32";
            row423.StressValues = new double?[] { 14.6, null, 14.6, null, 14.1, 13.8, 13.5, 13.1, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row423);

            // Row 10: SA-268, , Smls. tube
            var row424 = new OldStressRowData();
            row424.LineNo = 10;
            row424.MaterialId = 9084;
            row424.SpecNo = "SA-268";
            row424.TypeGrade = "";
            row424.ProductForm = "Smls. tube";
            row424.AlloyUNS = "S44400";
            row424.ClassCondition = "";
            row424.Notes = "G32";
            row424.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.2, 15.9, 15.4, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row424);

            // Row 11: SA-268, TP439, Wld. tube
            var row425 = new OldStressRowData();
            row425.LineNo = 11;
            row425.MaterialId = 9078;
            row425.SpecNo = "SA-268";
            row425.TypeGrade = "TP439";
            row425.ProductForm = "Wld. tube";
            row425.AlloyUNS = "S43035";
            row425.ClassCondition = "";
            row425.Notes = "G24, G32";
            row425.StressValues = new double?[] { 14.6, null, 14.6, null, 13.3, 12.4, 11.8, 11.6, 11.5, 11.5, 11.4, 11.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row425);

            // Row 12: SA-268, TP439, Smls. tube
            var row426 = new OldStressRowData();
            row426.LineNo = 12;
            row426.MaterialId = 9079;
            row426.SpecNo = "SA-268";
            row426.TypeGrade = "TP439";
            row426.ProductForm = "Smls. tube";
            row426.AlloyUNS = "S43035";
            row426.ClassCondition = "";
            row426.Notes = "G32";
            row426.StressValues = new double?[] { 17.1, null, 17.1, null, 15.7, 14.6, 13.9, 13.6, 13.6, 13.5, 13.4, 13.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row426);

            // Row 13: SA-803, TP439, Wld. tube
            var row427 = new OldStressRowData();
            row427.LineNo = 13;
            row427.MaterialId = 9080;
            row427.SpecNo = "SA-803";
            row427.TypeGrade = "TP439";
            row427.ProductForm = "Wld. tube";
            row427.AlloyUNS = "S43035";
            row427.ClassCondition = "";
            row427.Notes = "G24, G32";
            row427.StressValues = new double?[] { 14.6, null, 14.6, null, 13.3, 12.4, 11.8, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row427);

            // Row 14: SA-731, TP439, Wld. pipe
            var row428 = new OldStressRowData();
            row428.LineNo = 14;
            row428.MaterialId = 9842;
            row428.SpecNo = "SA-731";
            row428.TypeGrade = "TP439";
            row428.ProductForm = "Wld. pipe";
            row428.AlloyUNS = "S43035";
            row428.ClassCondition = "";
            row428.Notes = "G24, G32";
            row428.StressValues = new double?[] { 14.6, null, 14.6, null, 13.3, 12.4, 11.8, 11.6, 11.5, 11.5, 11.4, 11.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row428);

            // Row 15: SA-731, TP439, Smls. pipe
            var row429 = new OldStressRowData();
            row429.LineNo = 15;
            row429.MaterialId = 9843;
            row429.SpecNo = "SA-731";
            row429.TypeGrade = "TP439";
            row429.ProductForm = "Smls. pipe";
            row429.AlloyUNS = "S43035";
            row429.ClassCondition = "";
            row429.Notes = "G32";
            row429.StressValues = new double?[] { 17.1, null, 17.1, null, 15.7, 14.6, 13.9, 13.6, 13.6, 13.5, 13.4, 13.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row429);

            // Row 16: SA-479, 439, Bar
            var row430 = new OldStressRowData();
            row430.LineNo = 16;
            row430.MaterialId = 9081;
            row430.SpecNo = "SA-479";
            row430.TypeGrade = "439";
            row430.ProductForm = "Bar";
            row430.AlloyUNS = "S43035";
            row430.ClassCondition = "";
            row430.Notes = "G22, G32, T4";
            row430.StressValues = new double?[] { 20, null, 20, null, 19.3, 18.8, 18.4, 17.9, 17.7, 17.3, 16.9, 16.4, 15.8, 12.2, 9.2, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row430);

            // Row 17: SA-240, 26-3-3, Plate
            var row431 = new OldStressRowData();
            row431.LineNo = 17;
            row431.MaterialId = 8725;
            row431.SpecNo = "SA-240";
            row431.TypeGrade = "26-3-3";
            row431.ProductForm = "Plate";
            row431.AlloyUNS = "S44660";
            row431.ClassCondition = "";
            row431.Notes = "G32, H5";
            row431.StressValues = new double?[] { 24.3, null, 24.3, null, 24.2, 23.9, 23.8, 23.6, 23.5, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row431);

            // Row 18: SA-268, 26-3-3, Smls. tube
            var row432 = new OldStressRowData();
            row432.LineNo = 18;
            row432.MaterialId = 8726;
            row432.SpecNo = "SA-268";
            row432.TypeGrade = "26-3-3";
            row432.ProductForm = "Smls. tube";
            row432.AlloyUNS = "S44660";
            row432.ClassCondition = "";
            row432.Notes = "G32, H5";
            row432.StressValues = new double?[] { 24.3, null, 24.3, null, 24.2, 23.9, 23.8, 23.6, 23.5, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row432);

            // Row 19: SA-268, 26-3-3, Wld. tube
            var row433 = new OldStressRowData();
            row433.LineNo = 19;
            row433.MaterialId = 16843;
            row433.SpecNo = "SA-268";
            row433.TypeGrade = "26-3-3";
            row433.ProductForm = "Wld. tube";
            row433.AlloyUNS = "S44660";
            row433.ClassCondition = "";
            row433.Notes = "G32, H5, W12";
            row433.StressValues = new double?[] { 24.3, null, 24.3, null, 24.2, 23.9, 23.8, 23.6, 23.5, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row433);

            // Row 20: SA-268, 26-3-3, Wld. tube
            var row434 = new OldStressRowData();
            row434.LineNo = 20;
            row434.MaterialId = 8727;
            row434.SpecNo = "SA-268";
            row434.TypeGrade = "26-3-3";
            row434.ProductForm = "Wld. tube";
            row434.AlloyUNS = "S44660";
            row434.ClassCondition = "";
            row434.Notes = "G24, G32, H5";
            row434.StressValues = new double?[] { 20.6, null, 20.6, null, 20.6, 20.3, 20.2, 20.1, 20, 19.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row434);

            // Row 21: SA-803, 26-3-3, Wld. tube
            var row435 = new OldStressRowData();
            row435.LineNo = 21;
            row435.MaterialId = 8728;
            row435.SpecNo = "SA-803";
            row435.TypeGrade = "26-3-3";
            row435.ProductForm = "Wld. tube";
            row435.AlloyUNS = "S44660";
            row435.ClassCondition = "";
            row435.Notes = "G24, G32, H5";
            row435.StressValues = new double?[] { 20.6, null, 20.6, null, 20.6, 20.3, 20.2, 20.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row435);

            // Row 22: SA-240, 329, Plate
            var row436 = new OldStressRowData();
            row436.LineNo = 22;
            row436.MaterialId = 8729;
            row436.SpecNo = "SA-240";
            row436.TypeGrade = "329";
            row436.ProductForm = "Plate";
            row436.AlloyUNS = "S32900";
            row436.ClassCondition = "";
            row436.Notes = "G32";
            row436.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 24.3, 24.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row436);

            // Row 23: SA-789, , Wld. tube
            var row437 = new OldStressRowData();
            row437.LineNo = 23;
            row437.MaterialId = 8730;
            row437.SpecNo = "SA-789";
            row437.TypeGrade = "";
            row437.ProductForm = "Wld. tube";
            row437.AlloyUNS = "S32900";
            row437.ClassCondition = "";
            row437.Notes = "G24, G32";
            row437.StressValues = new double?[] { 21.9, null, 21.9, null, 21, 20.6, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row437);

            // Row 24: SA-789, , Smls. tube
            var row438 = new OldStressRowData();
            row438.LineNo = 24;
            row438.MaterialId = 8731;
            row438.SpecNo = "SA-789";
            row438.TypeGrade = "";
            row438.ProductForm = "Smls. tube";
            row438.AlloyUNS = "S32900";
            row438.ClassCondition = "";
            row438.Notes = "G32";
            row438.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 24.3, 24.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row438);

            // Row 25: SA-790, , Wld. pipe
            var row439 = new OldStressRowData();
            row439.LineNo = 25;
            row439.MaterialId = 8732;
            row439.SpecNo = "SA-790";
            row439.TypeGrade = "";
            row439.ProductForm = "Wld. pipe";
            row439.AlloyUNS = "S32900";
            row439.ClassCondition = "";
            row439.Notes = "G24, G32";
            row439.StressValues = new double?[] { 21.9, null, 21.9, null, 21, 20.6, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row439);

            // Row 26: SA-790, , Smls. pipe
            var row440 = new OldStressRowData();
            row440.LineNo = 26;
            row440.MaterialId = 8733;
            row440.SpecNo = "SA-790";
            row440.TypeGrade = "";
            row440.ProductForm = "Smls. pipe";
            row440.AlloyUNS = "S32900";
            row440.ClassCondition = "";
            row440.Notes = "G32";
            row440.StressValues = new double?[] { 25.7, null, 25.7, null, 24.8, 24.3, 24.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row440);

            // Row 27: SA-240, , Plate
            var row441 = new OldStressRowData();
            row441.LineNo = 27;
            row441.MaterialId = 8734;
            row441.SpecNo = "SA-240";
            row441.TypeGrade = "";
            row441.ProductForm = "Plate";
            row441.AlloyUNS = "S32950";
            row441.ClassCondition = "";
            row441.Notes = "G32";
            row441.StressValues = new double?[] { 28.6, null, 28.5, null, 27, 26.4, 26.4, 26.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row441);

            // Row 28: SA-789, , Wld. tube
            var row442 = new OldStressRowData();
            row442.LineNo = 28;
            row442.MaterialId = 8735;
            row442.SpecNo = "SA-789";
            row442.TypeGrade = "";
            row442.ProductForm = "Wld. tube";
            row442.AlloyUNS = "S32950";
            row442.ClassCondition = "";
            row442.Notes = "G24, G32";
            row442.StressValues = new double?[] { 24.3, null, 24.2, null, 23, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row442);

            // Row 29: SA-789, , Smls. tube
            var row443 = new OldStressRowData();
            row443.LineNo = 29;
            row443.MaterialId = 8736;
            row443.SpecNo = "SA-789";
            row443.TypeGrade = "";
            row443.ProductForm = "Smls. tube";
            row443.AlloyUNS = "S32950";
            row443.ClassCondition = "";
            row443.Notes = "G32";
            row443.StressValues = new double?[] { 28.6, null, 28.5, null, 27, 26.4, 26.4, 26.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row443);

            // Row 30: SA-790, , Wld. pipe
            var row444 = new OldStressRowData();
            row444.LineNo = 30;
            row444.MaterialId = 8737;
            row444.SpecNo = "SA-790";
            row444.TypeGrade = "";
            row444.ProductForm = "Wld. pipe";
            row444.AlloyUNS = "S32950";
            row444.ClassCondition = "";
            row444.Notes = "G24, G32";
            row444.StressValues = new double?[] { 24.3, null, 24.2, null, 23, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row444);

            // Row 31: SA-790, , Smls. pipe
            var row445 = new OldStressRowData();
            row445.LineNo = 31;
            row445.MaterialId = 8738;
            row445.SpecNo = "SA-790";
            row445.TypeGrade = "";
            row445.ProductForm = "Smls. pipe";
            row445.AlloyUNS = "S32950";
            row445.ClassCondition = "";
            row445.Notes = "G32";
            row445.StressValues = new double?[] { 28.6, null, 28.5, null, 27, 26.4, 26.4, 26.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row445);

            // Row 32: SA-268, TP446-1, Smls. tube
            var row446 = new OldStressRowData();
            row446.LineNo = 32;
            row446.MaterialId = 8739;
            row446.SpecNo = "SA-268";
            row446.TypeGrade = "TP446-1";
            row446.ProductForm = "Smls. tube";
            row446.AlloyUNS = "S44600";
            row446.ClassCondition = "";
            row446.Notes = "G32";
            row446.StressValues = new double?[] { 20, null, 20, null, 19.3, 18.8, 18.4, 17.9, 17.7, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row446);

            // Row 33: SA-182, FXM-27Cb, Forgings
            var row447 = new OldStressRowData();
            row447.LineNo = 33;
            row447.MaterialId = 8741;
            row447.SpecNo = "SA-182";
            row447.TypeGrade = "FXM-27Cb";
            row447.ProductForm = "Forgings";
            row447.AlloyUNS = "S44627";
            row447.ClassCondition = "";
            row447.Notes = "G32";
            row447.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.1, 16.1, 16.1, 16.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row447);

            // Row 34: SA-240, XM-27, Plate
            var row448 = new OldStressRowData();
            row448.LineNo = 34;
            row448.MaterialId = 8743;
            row448.SpecNo = "SA-240";
            row448.TypeGrade = "XM-27";
            row448.ProductForm = "Plate";
            row448.AlloyUNS = "S44627";
            row448.ClassCondition = "";
            row448.Notes = "G32";
            row448.StressValues = new double?[] { 18.6, null, 18.6, null, 18.3, 18.1, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row448);

            // Row 35: SA-268, TPXM-27, Wld. tube
            var row449 = new OldStressRowData();
            row449.LineNo = 35;
            row449.MaterialId = 8744;
            row449.SpecNo = "SA-268";
            row449.TypeGrade = "TPXM-27";
            row449.ProductForm = "Wld. tube";
            row449.AlloyUNS = "S44627";
            row449.ClassCondition = "";
            row449.Notes = "G24, G32";
            row449.StressValues = new double?[] { 15.8, null, 15.8, null, 15.5, 15.4, 15.4, 15.4, 15.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row449);

            // Row 36: SA-268, TPXM-27, Smls. tube
            var row450 = new OldStressRowData();
            row450.LineNo = 36;
            row450.MaterialId = 8745;
            row450.SpecNo = "SA-268";
            row450.TypeGrade = "TPXM-27";
            row450.ProductForm = "Smls. tube";
            row450.AlloyUNS = "S44627";
            row450.ClassCondition = "";
            row450.Notes = "G32";
            row450.StressValues = new double?[] { 18.6, null, 18.6, null, 18.3, 18.1, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row450);

            // Row 37: SA-479, XM-27, Bar
            var row451 = new OldStressRowData();
            row451.LineNo = 37;
            row451.MaterialId = 8746;
            row451.SpecNo = "SA-479";
            row451.TypeGrade = "XM-27";
            row451.ProductForm = "Bar";
            row451.AlloyUNS = "S44627";
            row451.ClassCondition = "";
            row451.Notes = "G22, G32";
            row451.StressValues = new double?[] { 18.6, null, 18.6, null, 18.3, 18.1, 18.1, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row451);

            // Row 38: SA-731, TPXM-27, Smls. pipe
            var row452 = new OldStressRowData();
            row452.LineNo = 38;
            row452.MaterialId = 8747;
            row452.SpecNo = "SA-731";
            row452.TypeGrade = "TPXM-27";
            row452.ProductForm = "Smls. pipe";
            row452.AlloyUNS = "S44627";
            row452.ClassCondition = "";
            row452.Notes = "G32";
            row452.StressValues = new double?[] { 18.6, null, 18.6, null, 18.3, 18.1, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row452);

            // Row 39: SA-731, TPXM-27, Wld. pipe
            var row453 = new OldStressRowData();
            row453.LineNo = 39;
            row453.MaterialId = 8748;
            row453.SpecNo = "SA-731";
            row453.TypeGrade = "TPXM-27";
            row453.ProductForm = "Wld. pipe";
            row453.AlloyUNS = "S44627";
            row453.ClassCondition = "";
            row453.Notes = "G24, G32";
            row453.StressValues = new double?[] { 15.8, null, 15.8, null, 15.5, 15.4, 15.4, 15.4, 15.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row453);

            // Row 1: SA-731, TPXM-33, Smls. pipe
            var row454 = new OldStressRowData();
            row454.LineNo = 1;
            row454.MaterialId = 8749;
            row454.SpecNo = "SA-731";
            row454.TypeGrade = "TPXM-33";
            row454.ProductForm = "Smls. pipe";
            row454.AlloyUNS = "S44626";
            row454.ClassCondition = "";
            row454.Notes = "G32";
            row454.StressValues = new double?[] { 18.6, null, 18.6, null, 18.4, 18.2, 18, 17.6, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row454);

            // Row 2: SA-731, TPXM-33, Wld. pipe
            var row455 = new OldStressRowData();
            row455.LineNo = 2;
            row455.MaterialId = 8750;
            row455.SpecNo = "SA-731";
            row455.TypeGrade = "TPXM-33";
            row455.ProductForm = "Wld. pipe";
            row455.AlloyUNS = "S44626";
            row455.ClassCondition = "";
            row455.Notes = "G24, G32";
            row455.StressValues = new double?[] { 15.8, null, 15.8, null, 15.7, 15.4, 15.3, 15, 14.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row455);

            // Row 3: SA-240, XM-33, Plate
            var row456 = new OldStressRowData();
            row456.LineNo = 3;
            row456.MaterialId = 8751;
            row456.SpecNo = "SA-240";
            row456.TypeGrade = "XM-33";
            row456.ProductForm = "Plate";
            row456.AlloyUNS = "S44626";
            row456.ClassCondition = "";
            row456.Notes = "G32";
            row456.StressValues = new double?[] { 19.4, null, 19.4, null, 19.3, 19, 18.8, 18.4, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row456);

            // Row 4: SA-268, TPXM-33, Smls. tube
            var row457 = new OldStressRowData();
            row457.LineNo = 4;
            row457.MaterialId = 8752;
            row457.SpecNo = "SA-268";
            row457.TypeGrade = "TPXM-33";
            row457.ProductForm = "Smls. tube";
            row457.AlloyUNS = "S44626";
            row457.ClassCondition = "";
            row457.Notes = "G32";
            row457.StressValues = new double?[] { 19.4, null, 19.4, null, 19.3, 19, 18.8, 18.4, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row457);

            // Row 5: SA-268, TPXM-33, Wld. tube
            var row458 = new OldStressRowData();
            row458.LineNo = 5;
            row458.MaterialId = 8753;
            row458.SpecNo = "SA-268";
            row458.TypeGrade = "TPXM-33";
            row458.ProductForm = "Wld. tube";
            row458.AlloyUNS = "S44626";
            row458.ClassCondition = "";
            row458.Notes = "G24, G32";
            row458.StressValues = new double?[] { 16.5, null, 16.5, null, 16.4, 16.2, 16, 15.7, 15.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row458);

            // Row 6: SA-479, , Bar
            var row459 = new OldStressRowData();
            row459.LineNo = 6;
            row459.MaterialId = 8754;
            row459.SpecNo = "SA-479";
            row459.TypeGrade = "";
            row459.ProductForm = "Bar";
            row459.AlloyUNS = "S44700";
            row459.ClassCondition = "";
            row459.Notes = "G22, G32";
            row459.StressValues = new double?[] { 20, null, 20, null, 19.3, 19.2, 19.2, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row459);

            // Row 7: SA-240, , Plate
            var row460 = new OldStressRowData();
            row460.LineNo = 7;
            row460.MaterialId = 8757;
            row460.SpecNo = "SA-240";
            row460.TypeGrade = "";
            row460.ProductForm = "Plate";
            row460.AlloyUNS = "S44700";
            row460.ClassCondition = "";
            row460.Notes = "G32";
            row460.StressValues = new double?[] { 22.9, null, 22.8, null, 22.1, 21.9, 21.9, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row460);

            // Row 8: SA-268, 29-4, Smls. tube
            var row461 = new OldStressRowData();
            row461.LineNo = 8;
            row461.MaterialId = 8758;
            row461.SpecNo = "SA-268";
            row461.TypeGrade = "29-4";
            row461.ProductForm = "Smls. tube";
            row461.AlloyUNS = "S44700";
            row461.ClassCondition = "";
            row461.Notes = "G32";
            row461.StressValues = new double?[] { 22.9, null, 22.8, null, 22.1, 21.9, 21.9, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row461);

            // Row 9: SA-268, 29-4, Wld. tube
            var row462 = new OldStressRowData();
            row462.LineNo = 9;
            row462.MaterialId = 8759;
            row462.SpecNo = "SA-268";
            row462.TypeGrade = "29-4";
            row462.ProductForm = "Wld. tube";
            row462.AlloyUNS = "S44700";
            row462.ClassCondition = "";
            row462.Notes = "G24, G32";
            row462.StressValues = new double?[] { 19.4, null, 19.4, null, 18.8, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row462);

            // Row 10: SA-479, , Bar
            var row463 = new OldStressRowData();
            row463.LineNo = 10;
            row463.MaterialId = 8760;
            row463.SpecNo = "SA-479";
            row463.TypeGrade = "";
            row463.ProductForm = "Bar";
            row463.AlloyUNS = "S44800";
            row463.ClassCondition = "";
            row463.Notes = "G22, G32";
            row463.StressValues = new double?[] { 20, null, 19.6, null, 19.3, 19.2, 18.9, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row463);

            // Row 11: SA-240, , Plate
            var row464 = new OldStressRowData();
            row464.LineNo = 11;
            row464.MaterialId = 8761;
            row464.SpecNo = "SA-240";
            row464.TypeGrade = "";
            row464.ProductForm = "Plate";
            row464.AlloyUNS = "S44800";
            row464.ClassCondition = "";
            row464.Notes = "G32";
            row464.StressValues = new double?[] { 22.9, null, 22.4, null, 22.1, 21.9, 21.6, 21.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row464);

            // Row 12: SA-268, 29-4-2, Smls. tube
            var row465 = new OldStressRowData();
            row465.LineNo = 12;
            row465.MaterialId = 8762;
            row465.SpecNo = "SA-268";
            row465.TypeGrade = "29-4-2";
            row465.ProductForm = "Smls. tube";
            row465.AlloyUNS = "S44800";
            row465.ClassCondition = "";
            row465.Notes = "G32";
            row465.StressValues = new double?[] { 22.9, null, 22.4, null, 22.1, 21.9, 21.6, 21.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row465);

            // Row 13: SA-268, 29-4-2, Wld. tube
            var row466 = new OldStressRowData();
            row466.LineNo = 13;
            row466.MaterialId = 8763;
            row466.SpecNo = "SA-268";
            row466.TypeGrade = "29-4-2";
            row466.ProductForm = "Wld. tube";
            row466.AlloyUNS = "S44800";
            row466.ClassCondition = "";
            row466.Notes = "G24, G32";
            row466.StressValues = new double?[] { 19.4, null, 19.1, null, 18.8, 18.6, 18.4, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row466);

            // Row 14: SA-268, , Smls. tube
            var row467 = new OldStressRowData();
            row467.LineNo = 14;
            row467.MaterialId = 8755;
            row467.SpecNo = "SA-268";
            row467.TypeGrade = "";
            row467.ProductForm = "Smls. tube";
            row467.AlloyUNS = "S44735";
            row467.ClassCondition = "";
            row467.Notes = "G32";
            row467.StressValues = new double?[] { 21.4, null, 21, null, 20.7, 20.5, 20.3, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row467);

            // Row 15: SA-268, , Wld. tube
            var row468 = new OldStressRowData();
            row468.LineNo = 15;
            row468.MaterialId = 8756;
            row468.SpecNo = "SA-268";
            row468.TypeGrade = "";
            row468.ProductForm = "Wld. tube";
            row468.AlloyUNS = "S44735";
            row468.ClassCondition = "";
            row468.Notes = "G24, G32";
            row468.StressValues = new double?[] { 18.2, null, 17.9, null, 17.6, 17.5, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row468);

            // Row 16: ..., , 
            var row469 = new OldStressRowData();
            row469.LineNo = 16;
            row469.MaterialId = 8767;
            row469.SpecNo = "...";
            row469.TypeGrade = "";
            row469.ProductForm = "";
            row469.AlloyUNS = "";
            row469.ClassCondition = "";
            row469.Notes = "";
            row469.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row469);

            // Row 17: SA-372, D, Forgings
            var row470 = new OldStressRowData();
            row470.LineNo = 17;
            row470.MaterialId = 8768;
            row470.SpecNo = "SA-372";
            row470.TypeGrade = "D";
            row470.ProductForm = "Forgings";
            row470.AlloyUNS = "K14508";
            row470.ClassCondition = "";
            row470.Notes = "G25, W2, W11, W15";
            row470.StressValues = new double?[] { 30, null, 30, 30, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row470);

            // Row 18: SA-487, 2, Castings
            var row471 = new OldStressRowData();
            row471.LineNo = 18;
            row471.MaterialId = 8770;
            row471.SpecNo = "SA-487";
            row471.TypeGrade = "2";
            row471.ProductForm = "Castings";
            row471.AlloyUNS = "J13005";
            row471.ClassCondition = "A";
            row471.Notes = "G1";
            row471.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.2, 24.2, 24.1, 24.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row471);

            // Row 19: SA-487, 2, Castings
            var row472 = new OldStressRowData();
            row472.LineNo = 19;
            row472.MaterialId = 8771;
            row472.SpecNo = "SA-487";
            row472.TypeGrade = "2";
            row472.ProductForm = "Castings";
            row472.AlloyUNS = "J13005";
            row472.ClassCondition = "B";
            row472.Notes = "G1";
            row472.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.6, 25.6, 25.6, 25.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row472);

            // Row 20: SA-302, A, Plate
            var row473 = new OldStressRowData();
            row473.LineNo = 20;
            row473.MaterialId = 8772;
            row473.SpecNo = "SA-302";
            row473.TypeGrade = "A";
            row473.ProductForm = "Plate";
            row473.AlloyUNS = "K12021";
            row473.ClassCondition = "";
            row473.Notes = "G11, S2, T3";
            row473.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 20, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row473);

            // Row 21: SA-672, H75, Wld. pipe
            var row474 = new OldStressRowData();
            row474.LineNo = 21;
            row474.MaterialId = 8774;
            row474.SpecNo = "SA-672";
            row474.TypeGrade = "H75";
            row474.ProductForm = "Wld. pipe";
            row474.AlloyUNS = "K12021";
            row474.ClassCondition = "";
            row474.Notes = "S6, W10, W12";
            row474.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row474);

            // Row 22: SA-302, B, Plate
            var row475 = new OldStressRowData();
            row475.LineNo = 22;
            row475.MaterialId = 8775;
            row475.SpecNo = "SA-302";
            row475.TypeGrade = "B";
            row475.ProductForm = "Plate";
            row475.AlloyUNS = "K12022";
            row475.ClassCondition = "";
            row475.Notes = "G11, S2, T3";
            row475.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 20, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row475);

            // Row 23: SA-533, A, Plate
            var row476 = new OldStressRowData();
            row476.LineNo = 23;
            row476.MaterialId = 8776;
            row476.SpecNo = "SA-533";
            row476.TypeGrade = "A";
            row476.ProductForm = "Plate";
            row476.AlloyUNS = "K12521";
            row476.ClassCondition = "1";
            row476.Notes = "G23, T4";
            row476.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.1, 13.3, 10, 6.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row476);

            // Row 24: SA-533, A, Plate
            var row477 = new OldStressRowData();
            row477.LineNo = 24;
            row477.MaterialId = 8777;
            row477.SpecNo = "SA-533";
            row477.TypeGrade = "A";
            row477.ProductForm = "Plate";
            row477.AlloyUNS = "K12521";
            row477.ClassCondition = "2";
            row477.Notes = "";
            row477.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row477);

            // Row 25: SA-533, A, Plate
            var row478 = new OldStressRowData();
            row478.LineNo = 25;
            row478.MaterialId = 8778;
            row478.SpecNo = "SA-533";
            row478.TypeGrade = "A";
            row478.ProductForm = "Plate";
            row478.AlloyUNS = "K12521";
            row478.ClassCondition = "3";
            row478.Notes = "";
            row478.StressValues = new double?[] { 28.6, null, 28.6, null, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row478);

            // Row 26: SA-533, D, Plate
            var row479 = new OldStressRowData();
            row479.LineNo = 26;
            row479.MaterialId = 8779;
            row479.SpecNo = "SA-533";
            row479.TypeGrade = "D";
            row479.ProductForm = "Plate";
            row479.AlloyUNS = "K12529";
            row479.ClassCondition = "1";
            row479.Notes = "";
            row479.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row479);

            // Row 27: SA-533, D, Plate
            var row480 = new OldStressRowData();
            row480.LineNo = 27;
            row480.MaterialId = 8780;
            row480.SpecNo = "SA-533";
            row480.TypeGrade = "D";
            row480.ProductForm = "Plate";
            row480.AlloyUNS = "K12529";
            row480.ClassCondition = "2";
            row480.Notes = "";
            row480.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row480);

            // Row 28: SA-533, D, Plate
            var row481 = new OldStressRowData();
            row481.LineNo = 28;
            row481.MaterialId = 8781;
            row481.SpecNo = "SA-533";
            row481.TypeGrade = "D";
            row481.ProductForm = "Plate";
            row481.AlloyUNS = "K12529";
            row481.ClassCondition = "3";
            row481.Notes = "";
            row481.StressValues = new double?[] { 28.6, null, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row481);

            // Row 29: SA-302, C, Plate
            var row482 = new OldStressRowData();
            row482.LineNo = 29;
            row482.MaterialId = 8782;
            row482.SpecNo = "SA-302";
            row482.TypeGrade = "C";
            row482.ProductForm = "Plate";
            row482.AlloyUNS = "K12039";
            row482.ClassCondition = "";
            row482.Notes = "G11, S2, T3";
            row482.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 20, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row482);

            // Row 30: SA-533, B, Plate
            var row483 = new OldStressRowData();
            row483.LineNo = 30;
            row483.MaterialId = 8783;
            row483.SpecNo = "SA-533";
            row483.TypeGrade = "B";
            row483.ProductForm = "Plate";
            row483.AlloyUNS = "K12539";
            row483.ClassCondition = "1";
            row483.Notes = "G23";
            row483.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row483);

            // Row 31: SA-672, H80, Wld. pipe
            var row484 = new OldStressRowData();
            row484.LineNo = 31;
            row484.MaterialId = 8784;
            row484.SpecNo = "SA-672";
            row484.TypeGrade = "H80";
            row484.ProductForm = "Wld. pipe";
            row484.AlloyUNS = "K12039";
            row484.ClassCondition = "";
            row484.Notes = "G26, W10, W12";
            row484.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row484);

            // Row 32: SA-672, J80, Wld. pipe
            var row485 = new OldStressRowData();
            row485.LineNo = 32;
            row485.MaterialId = 8785;
            row485.SpecNo = "SA-672";
            row485.TypeGrade = "J80";
            row485.ProductForm = "Wld. pipe";
            row485.AlloyUNS = "K12539";
            row485.ClassCondition = "";
            row485.Notes = "G26, W10, W12";
            row485.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row485);

            // Row 33: SA-533, B, Plate
            var row486 = new OldStressRowData();
            row486.LineNo = 33;
            row486.MaterialId = 8786;
            row486.SpecNo = "SA-533";
            row486.TypeGrade = "B";
            row486.ProductForm = "Plate";
            row486.AlloyUNS = "K12539";
            row486.ClassCondition = "2";
            row486.Notes = "";
            row486.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row486);

            // Row 34: SA-672, J90, Wld. pipe
            var row487 = new OldStressRowData();
            row487.LineNo = 34;
            row487.MaterialId = 8787;
            row487.SpecNo = "SA-672";
            row487.TypeGrade = "J90";
            row487.ProductForm = "Wld. pipe";
            row487.AlloyUNS = "K12539";
            row487.ClassCondition = "";
            row487.Notes = "G26, W10, W12";
            row487.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row487);

            // Row 35: SA-533, B, Plate
            var row488 = new OldStressRowData();
            row488.LineNo = 35;
            row488.MaterialId = 8788;
            row488.SpecNo = "SA-533";
            row488.TypeGrade = "B";
            row488.ProductForm = "Plate";
            row488.AlloyUNS = "K12539";
            row488.ClassCondition = "3";
            row488.Notes = "";
            row488.StressValues = new double?[] { 28.6, null, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row488);

            // Row 36: SA-672, J100, Wld. pipe
            var row489 = new OldStressRowData();
            row489.LineNo = 36;
            row489.MaterialId = 8789;
            row489.SpecNo = "SA-672";
            row489.TypeGrade = "J100";
            row489.ProductForm = "Wld. pipe";
            row489.AlloyUNS = "K12539";
            row489.ClassCondition = "";
            row489.Notes = "G26, W10, W12";
            row489.StressValues = new double?[] { 28.6, null, 28.6, null, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row489);

            // Row 1: SA-302, D, Plate
            var row490 = new OldStressRowData();
            row490.LineNo = 1;
            row490.MaterialId = 8790;
            row490.SpecNo = "SA-302";
            row490.TypeGrade = "D";
            row490.ProductForm = "Plate";
            row490.AlloyUNS = "K12054";
            row490.ClassCondition = "";
            row490.Notes = "G11, S2, T3";
            row490.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 20, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row490);

            // Row 2: SA-533, C, Plate
            var row491 = new OldStressRowData();
            row491.LineNo = 2;
            row491.MaterialId = 8791;
            row491.SpecNo = "SA-533";
            row491.TypeGrade = "C";
            row491.ProductForm = "Plate";
            row491.AlloyUNS = "K12554";
            row491.ClassCondition = "1";
            row491.Notes = "G23";
            row491.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row491);

            // Row 3: SA-533, C, Plate
            var row492 = new OldStressRowData();
            row492.LineNo = 3;
            row492.MaterialId = 8792;
            row492.SpecNo = "SA-533";
            row492.TypeGrade = "C";
            row492.ProductForm = "Plate";
            row492.AlloyUNS = "K12554";
            row492.ClassCondition = "2";
            row492.Notes = "";
            row492.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row492);

            // Row 4: SA-533, C, Plate
            var row493 = new OldStressRowData();
            row493.LineNo = 4;
            row493.MaterialId = 8793;
            row493.SpecNo = "SA-533";
            row493.TypeGrade = "C";
            row493.ProductForm = "Plate";
            row493.AlloyUNS = "K12554";
            row493.ClassCondition = "3";
            row493.Notes = "";
            row493.StressValues = new double?[] { 28.6, null, 28.6, null, 28.6, 28.6, 28.6, 28.6, 28.6, 28.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row493);

            // Row 5: SA-225, C, Plate
            var row494 = new OldStressRowData();
            row494.LineNo = 5;
            row494.MaterialId = 8794;
            row494.SpecNo = "SA-225";
            row494.TypeGrade = "C";
            row494.ProductForm = "Plate";
            row494.AlloyUNS = "K12524";
            row494.ClassCondition = "";
            row494.Notes = "";
            row494.StressValues = new double?[] { 30, 30, 30, null, 30, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row494);

            // Row 6: SA-487, 1, Castings
            var row495 = new OldStressRowData();
            row495.LineNo = 6;
            row495.MaterialId = 8764;
            row495.SpecNo = "SA-487";
            row495.TypeGrade = "1";
            row495.ProductForm = "Castings";
            row495.AlloyUNS = "J13002";
            row495.ClassCondition = "A";
            row495.Notes = "";
            row495.StressValues = new double?[] { 24.3, null, 23, null, 22.4, 22.4, 22.4, 21.9, 21.5, 20.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row495);

            // Row 7: SA-487, 1, Castings
            var row496 = new OldStressRowData();
            row496.LineNo = 7;
            row496.MaterialId = 8765;
            row496.SpecNo = "SA-487";
            row496.TypeGrade = "1";
            row496.ProductForm = "Castings";
            row496.AlloyUNS = "J13002";
            row496.ClassCondition = "A";
            row496.Notes = "G1";
            row496.StressValues = new double?[] { 24.3, null, 23.1, null, 22.5, 22.5, 22.5, 21.9, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row496);

            // Row 8: SA-487, 1, Castings
            var row497 = new OldStressRowData();
            row497.LineNo = 8;
            row497.MaterialId = 8766;
            row497.SpecNo = "SA-487";
            row497.TypeGrade = "1";
            row497.ProductForm = "Castings";
            row497.AlloyUNS = "J13002";
            row497.ClassCondition = "B";
            row497.Notes = "G1";
            row497.StressValues = new double?[] { 25.7, null, 24.6, null, 24.2, 24.1, 24.1, 23.9, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row497);

            // Row 9: SA-335, P15, Smls. pipe
            var row498 = new OldStressRowData();
            row498.LineNo = 9;
            row498.MaterialId = 8797;
            row498.SpecNo = "SA-335";
            row498.TypeGrade = "P15";
            row498.ProductForm = "Smls. pipe";
            row498.AlloyUNS = "K11578";
            row498.ClassCondition = "";
            row498.Notes = "T3";
            row498.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 17.1, 17.1, 16.8, 16.6, 16.3, 15.9, 15.4, 13.8, 12.5, 10, 6.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row498);

            // Row 10: SA-487, 4, Castings
            var row499 = new OldStressRowData();
            row499.LineNo = 10;
            row499.MaterialId = 8798;
            row499.SpecNo = "SA-487";
            row499.TypeGrade = "4";
            row499.ProductForm = "Castings";
            row499.AlloyUNS = "J13047";
            row499.ClassCondition = "A";
            row499.Notes = "G1";
            row499.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row499);

            // Row 11: SA-487, 4, Castings
            var row500 = new OldStressRowData();
            row500.LineNo = 11;
            row500.MaterialId = 8799;
            row500.SpecNo = "SA-487";
            row500.TypeGrade = "4";
            row500.ProductForm = "Castings";
            row500.AlloyUNS = "J13047";
            row500.ClassCondition = "B";
            row500.Notes = "G1";
            row500.StressValues = new double?[] { 30, null, 30, 30, 30, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row500);

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
