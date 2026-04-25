using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch005
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

            // Row 12: SA-182, F91, Forgings
            var row401 = new OldStressRowData();
            row401.LineNo = 12;
            row401.MaterialId = 8659;
            row401.SpecNo = "SA-182";
            row401.TypeGrade = "F91";
            row401.ProductForm = "Forgings";
            row401.AlloyUNS = "";
            row401.ClassCondition = "";
            row401.Notes = "G18, H3";
            row401.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row401);

            // Row 13: SA-182, F91, Forgings
            var row402 = new OldStressRowData();
            row402.LineNo = 13;
            row402.MaterialId = 8660;
            row402.SpecNo = "SA-182";
            row402.TypeGrade = "F91";
            row402.ProductForm = "Forgings";
            row402.AlloyUNS = "";
            row402.ClassCondition = "";
            row402.Notes = "G18, H3";
            row402.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row402);

            // Row 14: SA-213, T91, Smls. tube
            var row403 = new OldStressRowData();
            row403.LineNo = 14;
            row403.MaterialId = 8661;
            row403.SpecNo = "SA-213";
            row403.TypeGrade = "T91";
            row403.ProductForm = "Smls. tube";
            row403.AlloyUNS = "";
            row403.ClassCondition = "";
            row403.Notes = "G18";
            row403.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row403);

            // Row 15: SA-234, WP91, Fittings
            var row404 = new OldStressRowData();
            row404.LineNo = 15;
            row404.MaterialId = 8662;
            row404.SpecNo = "SA-234";
            row404.TypeGrade = "WP91";
            row404.ProductForm = "Fittings";
            row404.AlloyUNS = "";
            row404.ClassCondition = "";
            row404.Notes = "G18";
            row404.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row404);

            // Row 16: SA-234, WP91, Fittings
            var row405 = new OldStressRowData();
            row405.LineNo = 16;
            row405.MaterialId = 8663;
            row405.SpecNo = "SA-234";
            row405.TypeGrade = "WP91";
            row405.ProductForm = "Fittings";
            row405.AlloyUNS = "";
            row405.ClassCondition = "";
            row405.Notes = "G18";
            row405.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row405);

            // Row 17: SA-335, P91, Smls. pipe
            var row406 = new OldStressRowData();
            row406.LineNo = 17;
            row406.MaterialId = 8664;
            row406.SpecNo = "SA-335";
            row406.TypeGrade = "P91";
            row406.ProductForm = "Smls. pipe";
            row406.AlloyUNS = "";
            row406.ClassCondition = "";
            row406.Notes = "G18";
            row406.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row406);

            // Row 18: SA-335, P91, Smls. pipe
            var row407 = new OldStressRowData();
            row407.LineNo = 18;
            row407.MaterialId = 8665;
            row407.SpecNo = "SA-335";
            row407.TypeGrade = "P91";
            row407.ProductForm = "Smls. pipe";
            row407.AlloyUNS = "";
            row407.ClassCondition = "";
            row407.Notes = "G18, H3";
            row407.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row407);

            // Row 19: SA-336, F91, Forgings
            var row408 = new OldStressRowData();
            row408.LineNo = 19;
            row408.MaterialId = 8666;
            row408.SpecNo = "SA-336";
            row408.TypeGrade = "F91";
            row408.ProductForm = "Forgings";
            row408.AlloyUNS = "";
            row408.ClassCondition = "";
            row408.Notes = "G18, H3";
            row408.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row408);

            // Row 20: SA-336, F91, Forgings
            var row409 = new OldStressRowData();
            row409.LineNo = 20;
            row409.MaterialId = 8667;
            row409.SpecNo = "SA-336";
            row409.TypeGrade = "F91";
            row409.ProductForm = "Forgings";
            row409.AlloyUNS = "";
            row409.ClassCondition = "";
            row409.Notes = "G18, H3";
            row409.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row409);

            // Row 21: SA-369, FP91, Forged pipe
            var row410 = new OldStressRowData();
            row410.LineNo = 21;
            row410.MaterialId = 8668;
            row410.SpecNo = "SA-369";
            row410.TypeGrade = "FP91";
            row410.ProductForm = "Forged pipe";
            row410.AlloyUNS = "";
            row410.ClassCondition = "";
            row410.Notes = "G18";
            row410.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row410);

            // Row 22: SA-369, FP91, Forged pipe
            var row411 = new OldStressRowData();
            row411.LineNo = 22;
            row411.MaterialId = 8669;
            row411.SpecNo = "SA-369";
            row411.TypeGrade = "FP91";
            row411.ProductForm = "Forged pipe";
            row411.AlloyUNS = "";
            row411.ClassCondition = "";
            row411.Notes = "G18";
            row411.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row411);

            // Row 23: SA-387, 91, Plate
            var row412 = new OldStressRowData();
            row412.LineNo = 23;
            row412.MaterialId = 8670;
            row412.SpecNo = "SA-387";
            row412.TypeGrade = "91";
            row412.ProductForm = "Plate";
            row412.AlloyUNS = "";
            row412.ClassCondition = "2";
            row412.Notes = "G18, H3";
            row412.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row412);

            // Row 24: SA-387, 91, Plate
            var row413 = new OldStressRowData();
            row413.LineNo = 24;
            row413.MaterialId = 8671;
            row413.SpecNo = "SA-387";
            row413.TypeGrade = "91";
            row413.ProductForm = "Plate";
            row413.AlloyUNS = "";
            row413.ClassCondition = "2";
            row413.Notes = "G18, H3";
            row413.StressValues = new double?[] { 21.3, null, 21.3, null, 21.2, 21.2, 21.1, 20.8, 20.5, 20, 19.4, 18.7, 17.8, 16.7, 15.5, 14.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row413);

            // Row 25: SA-240, TP409, Plate
            var row414 = new OldStressRowData();
            row414.LineNo = 25;
            row414.MaterialId = 8672;
            row414.SpecNo = "SA-240";
            row414.TypeGrade = "TP409";
            row414.ProductForm = "Plate";
            row414.AlloyUNS = "S40900";
            row414.ClassCondition = "";
            row414.Notes = "";
            row414.StressValues = new double?[] { 13.8, null, 13.1, null, 12.7, 12.2, 11.8, 11.4, 11.3, 11.1, 10.7, 10.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row414);

            // Row 26: SA-268, TP409, Wld. tube
            var row415 = new OldStressRowData();
            row415.LineNo = 26;
            row415.MaterialId = 8673;
            row415.SpecNo = "SA-268";
            row415.TypeGrade = "TP409";
            row415.ProductForm = "Wld. tube";
            row415.AlloyUNS = "S40900";
            row415.ClassCondition = "";
            row415.Notes = "G24";
            row415.StressValues = new double?[] { 11.7, null, 11.1, null, 10.8, 10.4, 10, 9.7, 9.6, 9.4, 9.1, 8.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row415);

            // Row 27: SA-268, TP409, Smls. tube
            var row416 = new OldStressRowData();
            row416.LineNo = 27;
            row416.MaterialId = 8674;
            row416.SpecNo = "SA-268";
            row416.TypeGrade = "TP409";
            row416.ProductForm = "Smls. tube";
            row416.AlloyUNS = "S40900";
            row416.ClassCondition = "";
            row416.Notes = "";
            row416.StressValues = new double?[] { 13.8, null, 13.1, null, 12.7, 12.2, 11.8, 11.4, 11.3, 11.1, 10.7, 10.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row416);

            // Row 28: SA-479, 403, Bar
            var row417 = new OldStressRowData();
            row417.LineNo = 28;
            row417.MaterialId = 8675;
            row417.SpecNo = "SA-479";
            row417.TypeGrade = "403";
            row417.ProductForm = "Bar";
            row417.AlloyUNS = "S40300";
            row417.ClassCondition = "A";
            row417.Notes = "";
            row417.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row417);

            // Row 29: SA-479, 403, Bar
            var row418 = new OldStressRowData();
            row418.LineNo = 29;
            row418.MaterialId = 8676;
            row418.SpecNo = "SA-479";
            row418.TypeGrade = "403";
            row418.ProductForm = "Bar";
            row418.AlloyUNS = "S40300";
            row418.ClassCondition = "1";
            row418.Notes = "";
            row418.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row418);

            // Row 30: SA-240, 405, Plate
            var row419 = new OldStressRowData();
            row419.LineNo = 30;
            row419.MaterialId = 8679;
            row419.SpecNo = "SA-240";
            row419.TypeGrade = "405";
            row419.ProductForm = "Plate";
            row419.AlloyUNS = "S40500";
            row419.ClassCondition = "";
            row419.Notes = "G32";
            row419.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row419);

            // Row 31: SA-240, 405, Plate
            var row420 = new OldStressRowData();
            row420.LineNo = 31;
            row420.MaterialId = 8677;
            row420.SpecNo = "SA-240";
            row420.TypeGrade = "405";
            row420.ProductForm = "Plate";
            row420.AlloyUNS = "S40500";
            row420.ClassCondition = "";
            row420.Notes = "G32";
            row420.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row420);

            // Row 32: SA-479, 405, Bar
            var row421 = new OldStressRowData();
            row421.LineNo = 32;
            row421.MaterialId = 8678;
            row421.SpecNo = "SA-479";
            row421.TypeGrade = "405";
            row421.ProductForm = "Bar";
            row421.AlloyUNS = "S40500";
            row421.ClassCondition = "";
            row421.Notes = "G32";
            row421.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row421);

            // Row 33: SA-479, 405, Bar
            var row422 = new OldStressRowData();
            row422.LineNo = 33;
            row422.MaterialId = 8680;
            row422.SpecNo = "SA-479";
            row422.TypeGrade = "405";
            row422.ProductForm = "Bar";
            row422.AlloyUNS = "S40500";
            row422.ClassCondition = "";
            row422.Notes = "G32";
            row422.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row422);

            // Row 34: SA-268, TP405, Wld. & smls. tube
            var row423 = new OldStressRowData();
            row423.LineNo = 34;
            row423.MaterialId = 8681;
            row423.SpecNo = "SA-268";
            row423.TypeGrade = "TP405";
            row423.ProductForm = "Wld. & smls. tube";
            row423.AlloyUNS = "S40500";
            row423.ClassCondition = "";
            row423.Notes = "G32, W14";
            row423.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row423);

            // Row 35: SA-268, TP405, Wld. tube
            var row424 = new OldStressRowData();
            row424.LineNo = 35;
            row424.MaterialId = 8682;
            row424.SpecNo = "SA-268";
            row424.TypeGrade = "TP405";
            row424.ProductForm = "Wld. tube";
            row424.AlloyUNS = "S40500";
            row424.ClassCondition = "";
            row424.Notes = "G3, G24, G32";
            row424.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, 9.9, 9.4, 8.8, 8.2, 7.1, 3.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row424);

            // Row 36: SA-268, , Wld. tube
            var row425 = new OldStressRowData();
            row425.LineNo = 36;
            row425.MaterialId = 8683;
            row425.SpecNo = "SA-268";
            row425.TypeGrade = "";
            row425.ProductForm = "Wld. tube";
            row425.AlloyUNS = "S40800";
            row425.ClassCondition = "";
            row425.Notes = "G32";
            row425.StressValues = new double?[] { 11.7, null, 11.1, null, 10.8, 10.4, 10, 9.7, 9.6, 9.4, 9.1, 8.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row425);

            // Row 37: SA-268, , Smls. tube
            var row426 = new OldStressRowData();
            row426.LineNo = 37;
            row426.MaterialId = 8684;
            row426.SpecNo = "SA-268";
            row426.TypeGrade = "";
            row426.ProductForm = "Smls. tube";
            row426.AlloyUNS = "S40800";
            row426.ClassCondition = "";
            row426.Notes = "G32";
            row426.StressValues = new double?[] { 13.8, null, 13.1, null, 12.7, 12.2, 11.8, 11.4, 11.3, 11.1, 10.7, 10.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row426);

            // Row 1: SA-240, 410S, Plate
            var row427 = new OldStressRowData();
            row427.LineNo = 1;
            row427.MaterialId = 8686;
            row427.SpecNo = "SA-240";
            row427.TypeGrade = "410S";
            row427.ProductForm = "Plate";
            row427.AlloyUNS = "S41008";
            row427.ClassCondition = "";
            row427.Notes = "";
            row427.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row427);

            // Row 2: SA-240, 410S, Plate
            var row428 = new OldStressRowData();
            row428.LineNo = 2;
            row428.MaterialId = 8685;
            row428.SpecNo = "SA-240";
            row428.TypeGrade = "410S";
            row428.ProductForm = "Plate";
            row428.AlloyUNS = "S41008";
            row428.ClassCondition = "";
            row428.Notes = "";
            row428.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.4, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row428);

            // Row 3: SA-268, TP410, Wld. & smls. tube
            var row429 = new OldStressRowData();
            row429.LineNo = 3;
            row429.MaterialId = 8687;
            row429.SpecNo = "SA-268";
            row429.TypeGrade = "TP410";
            row429.ProductForm = "Wld. & smls. tube";
            row429.AlloyUNS = "S41000";
            row429.ClassCondition = "";
            row429.Notes = "W14";
            row429.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.4, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row429);

            // Row 4: SA-268, TP410, Wld. tube
            var row430 = new OldStressRowData();
            row430.LineNo = 4;
            row430.MaterialId = 8688;
            row430.SpecNo = "SA-268";
            row430.TypeGrade = "TP410";
            row430.ProductForm = "Wld. tube";
            row430.AlloyUNS = "S41000";
            row430.ClassCondition = "";
            row430.Notes = "G3, G24";
            row430.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, 9.9, 9.4, 8.8, 8.2, 7.1, 5.5, 3.7, 2.4, 1.5, 0.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row430);

            // Row 5: SA-240, 410, Plate
            var row431 = new OldStressRowData();
            row431.LineNo = 5;
            row431.MaterialId = 8689;
            row431.SpecNo = "SA-240";
            row431.TypeGrade = "410";
            row431.ProductForm = "Plate";
            row431.AlloyUNS = "S41000";
            row431.ClassCondition = "";
            row431.Notes = "";
            row431.StressValues = new double?[] { 16.3, null, 15.5, null, 15, 14.4, 13.9, 13.5, 13.3, 13.1, 12.7, 12, 11.3, 10.5, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row431);

            // Row 6: SA-182, F6a, Forgings
            var row432 = new OldStressRowData();
            row432.LineNo = 6;
            row432.MaterialId = 8690;
            row432.SpecNo = "SA-182";
            row432.TypeGrade = "F6a";
            row432.ProductForm = "Forgings";
            row432.AlloyUNS = "S41000";
            row432.ClassCondition = "1";
            row432.Notes = "";
            row432.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, 13.6, 12.9, 12.1, 11, 9.2, 6.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row432);

            // Row 7: SA-479, 410, Bar
            var row433 = new OldStressRowData();
            row433.LineNo = 7;
            row433.MaterialId = 8691;
            row433.SpecNo = "SA-479";
            row433.TypeGrade = "410";
            row433.ProductForm = "Bar";
            row433.AlloyUNS = "S41000";
            row433.ClassCondition = "";
            row433.Notes = "G22";
            row433.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, 13.6, 12.9, 12.1, 11, 9.2, 6.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row433);

            // Row 8: SA-479, 410, Bar
            var row434 = new OldStressRowData();
            row434.LineNo = 8;
            row434.MaterialId = 8693;
            row434.SpecNo = "SA-479";
            row434.TypeGrade = "410";
            row434.ProductForm = "Bar";
            row434.AlloyUNS = "S41000";
            row434.ClassCondition = "A";
            row434.Notes = "";
            row434.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row434);

            // Row 9: SA-479, 410, Bar
            var row435 = new OldStressRowData();
            row435.LineNo = 9;
            row435.MaterialId = 8692;
            row435.SpecNo = "SA-479";
            row435.TypeGrade = "410";
            row435.ProductForm = "Bar";
            row435.AlloyUNS = "S41000";
            row435.ClassCondition = "1";
            row435.Notes = "G18";
            row435.StressValues = new double?[] { 17.5, null, 16.7, null, 16.1, 15.6, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row435);

            // Row 10: SA-479, 410, Bar
            var row436 = new OldStressRowData();
            row436.LineNo = 10;
            row436.MaterialId = 8694;
            row436.SpecNo = "SA-479";
            row436.TypeGrade = "410";
            row436.ProductForm = "Bar";
            row436.AlloyUNS = "S41000";
            row436.ClassCondition = "1";
            row436.Notes = "";
            row436.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row436);

            // Row 11: SA-182, F6a, Forgings
            var row437 = new OldStressRowData();
            row437.LineNo = 11;
            row437.MaterialId = 8696;
            row437.SpecNo = "SA-182";
            row437.TypeGrade = "F6a";
            row437.ProductForm = "Forgings";
            row437.AlloyUNS = "S41000";
            row437.ClassCondition = "2";
            row437.Notes = "";
            row437.StressValues = new double?[] { 21.2, null, 20.2, null, 19.5, 18.8, 18.2, 17.6, 17.3, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row437);

            // Row 12: SA-182, F6a, Forgings
            var row438 = new OldStressRowData();
            row438.LineNo = 12;
            row438.MaterialId = 8695;
            row438.SpecNo = "SA-182";
            row438.TypeGrade = "F6a";
            row438.ProductForm = "Forgings";
            row438.AlloyUNS = "S41000";
            row438.ClassCondition = "2";
            row438.Notes = "";
            row438.StressValues = new double?[] { 21.3, null, 20.3, null, 19.6, 18.9, 18.2, 17.6, 17.3, 17.1, 16.5, 15.7, 14.4, 12.3, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row438);

            // Row 13: SA-217, CA15, Castings
            var row439 = new OldStressRowData();
            row439.LineNo = 13;
            row439.MaterialId = 8697;
            row439.SpecNo = "SA-217";
            row439.TypeGrade = "CA15";
            row439.ProductForm = "Castings";
            row439.AlloyUNS = "J91150";
            row439.ClassCondition = "";
            row439.Notes = "G17";
            row439.StressValues = new double?[] { 22.5, null, 21.4, null, 20.6, 19.9, 19.2, 18.6, 18.3, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row439);

            // Row 14: SA-217, CA15, Castings
            var row440 = new OldStressRowData();
            row440.LineNo = 14;
            row440.MaterialId = 8698;
            row440.SpecNo = "SA-217";
            row440.TypeGrade = "CA15";
            row440.ProductForm = "Castings";
            row440.AlloyUNS = "J91150";
            row440.ClassCondition = "";
            row440.Notes = "G1";
            row440.StressValues = new double?[] { 22.5, null, 21.5, null, 20.7, 20, 19.3, 18.7, 18.4, 18.1, 17.5, 16.7, 14.9, 11, 7.6, 5, 3.3, 2.2, 1.5, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row440);

            // Row 15: SA-426, CPCA15, Cast pipe
            var row441 = new OldStressRowData();
            row441.LineNo = 15;
            row441.MaterialId = 8699;
            row441.SpecNo = "SA-426";
            row441.TypeGrade = "CPCA15";
            row441.ProductForm = "Cast pipe";
            row441.AlloyUNS = "J91150";
            row441.ClassCondition = "";
            row441.Notes = "G17";
            row441.StressValues = new double?[] { 22.5, null, 21.4, null, 20.6, 19.9, 19.2, 18.6, 18.3, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row441);

            // Row 16: SA-487, CA6NM, Castings
            var row442 = new OldStressRowData();
            row442.LineNo = 16;
            row442.MaterialId = 8700;
            row442.SpecNo = "SA-487";
            row442.TypeGrade = "CA6NM";
            row442.ProductForm = "Castings";
            row442.AlloyUNS = "J91540";
            row442.ClassCondition = "A";
            row442.Notes = "G17";
            row442.StressValues = new double?[] { 27.5, null, 27.5, null, 26.9, 26.2, 25.8, 25.2, 24.8, 24.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row442);

            // Row 17: SA-487, CA6NM, Castings
            var row443 = new OldStressRowData();
            row443.LineNo = 17;
            row443.MaterialId = 8701;
            row443.SpecNo = "SA-487";
            row443.TypeGrade = "CA6NM";
            row443.ProductForm = "Castings";
            row443.AlloyUNS = "J91540";
            row443.ClassCondition = "A";
            row443.Notes = "G1";
            row443.StressValues = new double?[] { 27.5, null, 27.5, 27.2, 26.9, 26.3, 25.8, 25.3, 24.9, 24.4, 23.9, 23.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row443);

            // Row 18: SA-182, F6NM, Forgings
            var row444 = new OldStressRowData();
            row444.LineNo = 18;
            row444.MaterialId = 8702;
            row444.SpecNo = "SA-182";
            row444.TypeGrade = "F6NM";
            row444.ProductForm = "Forgings";
            row444.AlloyUNS = "S41500";
            row444.ClassCondition = "";
            row444.Notes = "G17";
            row444.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.4, 27.4, 26.3, 25.7, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row444);

            // Row 19: SA-268, TP429, Wld. tube
            var row445 = new OldStressRowData();
            row445.LineNo = 19;
            row445.MaterialId = 8705;
            row445.SpecNo = "SA-268";
            row445.TypeGrade = "TP429";
            row445.ProductForm = "Wld. tube";
            row445.AlloyUNS = "S42900";
            row445.ClassCondition = "";
            row445.Notes = "G3, G32";
            row445.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row445);

            // Row 20: SA-268, TP429, Wld. tube
            var row446 = new OldStressRowData();
            row446.LineNo = 20;
            row446.MaterialId = 8703;
            row446.SpecNo = "SA-268";
            row446.TypeGrade = "TP429";
            row446.ProductForm = "Wld. tube";
            row446.AlloyUNS = "S42900";
            row446.ClassCondition = "";
            row446.Notes = "G32";
            row446.StressValues = new double?[] { 12.7, null, 12.1, null, 11.7, 11.3, 10.9, 10.5, 10.4, 10.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row446);

            // Row 21: SA-268, TP429, Wld. & smls. tube
            var row447 = new OldStressRowData();
            row447.LineNo = 21;
            row447.MaterialId = 8704;
            row447.SpecNo = "SA-268";
            row447.TypeGrade = "TP429";
            row447.ProductForm = "Wld. & smls. tube";
            row447.AlloyUNS = "S42900";
            row447.ClassCondition = "";
            row447.Notes = "G32, W14";
            row447.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.5, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row447);

            // Row 22: SA-240, 429, Plate
            var row448 = new OldStressRowData();
            row448.LineNo = 22;
            row448.MaterialId = 8706;
            row448.SpecNo = "SA-240";
            row448.TypeGrade = "429";
            row448.ProductForm = "Plate";
            row448.AlloyUNS = "S42900";
            row448.ClassCondition = "";
            row448.Notes = "G32";
            row448.StressValues = new double?[] { 16.3, null, 15.5, null, 15, 14.4, 13.9, 13.5, 13.3, 13.1, 12.7, 12, 11.3, 10.5, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row448);

            // Row 23: SA-268, TP430, Wld. tube
            var row449 = new OldStressRowData();
            row449.LineNo = 23;
            row449.MaterialId = 8707;
            row449.SpecNo = "SA-268";
            row449.TypeGrade = "TP430";
            row449.ProductForm = "Wld. tube";
            row449.AlloyUNS = "S43000";
            row449.ClassCondition = "";
            row449.Notes = "G3, G24, G32";
            row449.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, 9.9, 9.4, 8.8, 8.2, 7.2, 5.5, 3.8, 2.7, 2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row449);

            // Row 24: SA-268, TP430, Wld. & smls. tube
            var row450 = new OldStressRowData();
            row450.LineNo = 24;
            row450.MaterialId = 8708;
            row450.SpecNo = "SA-268";
            row450.TypeGrade = "TP430";
            row450.ProductForm = "Wld. & smls. tube";
            row450.AlloyUNS = "S43000";
            row450.ClassCondition = "";
            row450.Notes = "G32, W14";
            row450.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.7, 11.1, 10.4, 9.7, 8.5, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row450);

            // Row 25: SA-268, TP430, Wld. & smls. tube
            var row451 = new OldStressRowData();
            row451.LineNo = 25;
            row451.MaterialId = 8709;
            row451.SpecNo = "SA-268";
            row451.TypeGrade = "TP430";
            row451.ProductForm = "Wld. & smls. tube";
            row451.AlloyUNS = "S43000";
            row451.ClassCondition = "";
            row451.Notes = "G32, W12";
            row451.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row451);

            // Row 26: SA-240, 430, Plate
            var row452 = new OldStressRowData();
            row452.LineNo = 26;
            row452.MaterialId = 8710;
            row452.SpecNo = "SA-240";
            row452.TypeGrade = "430";
            row452.ProductForm = "Plate";
            row452.AlloyUNS = "S43000";
            row452.ClassCondition = "";
            row452.Notes = "G32";
            row452.StressValues = new double?[] { 16.3, null, 15.5, null, 15, 14.4, 13.9, 13.5, 13.3, 13.1, 12.7, 12, 11.3, 10.5, 9.2, 6.5, 4.5, 3.2, 2.4, 1.8, null, null, null, null, null, null, null, null, null };
            batch.Add(row452);

            // Row 27: SA-479, 430, Bar
            var row453 = new OldStressRowData();
            row453.LineNo = 27;
            row453.MaterialId = 8712;
            row453.SpecNo = "SA-479";
            row453.TypeGrade = "430";
            row453.ProductForm = "Bar";
            row453.AlloyUNS = "S43000";
            row453.ClassCondition = "";
            row453.Notes = "G18, G32";
            row453.StressValues = new double?[] { 17.5, null, 16.7, null, 16.1, 15.6, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row453);

            // Row 28: SA-479, 430, Bar
            var row454 = new OldStressRowData();
            row454.LineNo = 28;
            row454.MaterialId = 8711;
            row454.SpecNo = "SA-479";
            row454.TypeGrade = "430";
            row454.ProductForm = "Bar";
            row454.AlloyUNS = "S43000";
            row454.ClassCondition = "";
            row454.Notes = "G22, G32";
            row454.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, 13.6, 12.9, 12.1, 11, 9.2, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row454);

            // Row 29: SA-747, CB7Cu-1, Castings
            var row455 = new OldStressRowData();
            row455.LineNo = 29;
            row455.MaterialId = 8713;
            row455.SpecNo = "SA-747";
            row455.TypeGrade = "CB7Cu-1";
            row455.ProductForm = "Castings";
            row455.AlloyUNS = "J92180";
            row455.ClassCondition = "";
            row455.Notes = "G1, W1";
            row455.StressValues = new double?[] { 28, null, 28, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row455);

            // Row 30: SA-564, 630, Bar
            var row456 = new OldStressRowData();
            row456.LineNo = 30;
            row456.MaterialId = 8714;
            row456.SpecNo = "SA-564";
            row456.TypeGrade = "630";
            row456.ProductForm = "Bar";
            row456.AlloyUNS = "S17400";
            row456.ClassCondition = "H1150";
            row456.Notes = "G8, G31, G32, W1";
            row456.StressValues = new double?[] { 33.7, null, 33.7, null, 33.7, 32.8, 32.1, 31.6, 31.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row456);

            // Row 31: SA-693, 630, Plate
            var row457 = new OldStressRowData();
            row457.LineNo = 31;
            row457.MaterialId = 8715;
            row457.SpecNo = "SA-693";
            row457.TypeGrade = "630";
            row457.ProductForm = "Plate";
            row457.AlloyUNS = "S17400";
            row457.ClassCondition = "H1150";
            row457.Notes = "G8, W1";
            row457.StressValues = new double?[] { 33.7, null, 33.7, null, 33.7, 32.8, 32.1, 31.6, 31.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row457);

            // Row 32: SA-705, 630, Forgings
            var row458 = new OldStressRowData();
            row458.LineNo = 32;
            row458.MaterialId = 8716;
            row458.SpecNo = "SA-705";
            row458.TypeGrade = "630";
            row458.ProductForm = "Forgings";
            row458.AlloyUNS = "S17400";
            row458.ClassCondition = "H1150";
            row458.Notes = "G8, W1";
            row458.StressValues = new double?[] { 33.7, null, 33.7, null, 33.7, 32.8, 32.1, 31.6, 31.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row458);

            // Row 33: SA-564, 630, Bar
            var row459 = new OldStressRowData();
            row459.LineNo = 33;
            row459.MaterialId = 8717;
            row459.SpecNo = "SA-564";
            row459.TypeGrade = "630";
            row459.ProductForm = "Bar";
            row459.AlloyUNS = "S17400";
            row459.ClassCondition = "H1100";
            row459.Notes = "G8, G31, G32, W1";
            row459.StressValues = new double?[] { 35, null, 35, null, 35, 34.1, 33.3, 32.8, 32.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row459);

            // Row 34: SA-693, 630, Plate
            var row460 = new OldStressRowData();
            row460.LineNo = 34;
            row460.MaterialId = 8718;
            row460.SpecNo = "SA-693";
            row460.TypeGrade = "630";
            row460.ProductForm = "Plate";
            row460.AlloyUNS = "S17400";
            row460.ClassCondition = "H1100";
            row460.Notes = "G8, W1";
            row460.StressValues = new double?[] { 35, null, 35, null, 35, 34.1, 33.3, 32.8, 32.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row460);

            // Row 35: SA-705, 630, Forgings
            var row461 = new OldStressRowData();
            row461.LineNo = 35;
            row461.MaterialId = 8719;
            row461.SpecNo = "SA-705";
            row461.TypeGrade = "630";
            row461.ProductForm = "Forgings";
            row461.AlloyUNS = "S17400";
            row461.ClassCondition = "H1100";
            row461.Notes = "G8, W1";
            row461.StressValues = new double?[] { 35, null, 35, null, 35, 34.1, 33.3, 32.8, 32.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row461);

            // Row 36: SA-564, 630, Bar
            var row462 = new OldStressRowData();
            row462.LineNo = 36;
            row462.MaterialId = 8720;
            row462.SpecNo = "SA-564";
            row462.TypeGrade = "630";
            row462.ProductForm = "Bar";
            row462.AlloyUNS = "S17400";
            row462.ClassCondition = "H1075";
            row462.Notes = "G8, W1";
            row462.StressValues = new double?[] { 36.2, null, 36.2, null, 36.2, 35.2, 34.5, 34, 33.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row462);

            // Row 37: SA-693, 630, Plate
            var row463 = new OldStressRowData();
            row463.LineNo = 37;
            row463.MaterialId = 8721;
            row463.SpecNo = "SA-693";
            row463.TypeGrade = "630";
            row463.ProductForm = "Plate";
            row463.AlloyUNS = "S17400";
            row463.ClassCondition = "H1075";
            row463.Notes = "G8, W1";
            row463.StressValues = new double?[] { 36.2, null, 36.2, null, 36.2, 35.2, 34.5, 34, 33.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row463);

            // Row 38: SA-705, 630, Forgings
            var row464 = new OldStressRowData();
            row464.LineNo = 38;
            row464.MaterialId = 8722;
            row464.SpecNo = "SA-705";
            row464.TypeGrade = "630";
            row464.ProductForm = "Forgings";
            row464.AlloyUNS = "S17400";
            row464.ClassCondition = "H1075";
            row464.Notes = "G8, W1";
            row464.StressValues = new double?[] { 36.2, null, 36.2, null, 36.2, 35.2, 34.5, 34, 33.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row464);

            // Row 1: SA-240, 201-1, Plate
            var row465 = new OldStressRowData();
            row465.LineNo = 1;
            row465.MaterialId = 8723;
            row465.SpecNo = "SA-240";
            row465.TypeGrade = "201-1";
            row465.ProductForm = "Plate";
            row465.AlloyUNS = "S20100";
            row465.ClassCondition = "";
            row465.Notes = "";
            row465.StressValues = new double?[] { 23.8, null, 19.3, null, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row465);

            // Row 2: SA-666, 201-1, Plate
            var row466 = new OldStressRowData();
            row466.LineNo = 2;
            row466.MaterialId = 9784;
            row466.SpecNo = "SA-666";
            row466.TypeGrade = "201-1";
            row466.ProductForm = "Plate";
            row466.AlloyUNS = "S20100";
            row466.ClassCondition = "";
            row466.Notes = "";
            row466.StressValues = new double?[] { 22.5, null, 19.3, null, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row466);

            // Row 3: SA-240, 201-2, Plate
            var row467 = new OldStressRowData();
            row467.LineNo = 3;
            row467.MaterialId = 8724;
            row467.SpecNo = "SA-240";
            row467.TypeGrade = "201-2";
            row467.ProductForm = "Plate";
            row467.AlloyUNS = "S20100";
            row467.ClassCondition = "";
            row467.Notes = "";
            row467.StressValues = new double?[] { 23.8, null, 20.8, null, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row467);

            // Row 4: SA-666, 201-2, Plate
            var row468 = new OldStressRowData();
            row468.LineNo = 4;
            row468.MaterialId = 9785;
            row468.SpecNo = "SA-666";
            row468.TypeGrade = "201-2";
            row468.ProductForm = "Plate";
            row468.AlloyUNS = "S20100";
            row468.ClassCondition = "";
            row468.Notes = "";
            row468.StressValues = new double?[] { 23.8, null, 20.8, null, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row468);

            // Row 5: SA-240, , Plate
            var row469 = new OldStressRowData();
            row469.LineNo = 5;
            row469.MaterialId = 9082;
            row469.SpecNo = "SA-240";
            row469.TypeGrade = "";
            row469.ProductForm = "Plate";
            row469.AlloyUNS = "S44400";
            row469.ClassCondition = "";
            row469.Notes = "G32";
            row469.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row469);

            // Row 6: SA-268, , Wld. tube
            var row470 = new OldStressRowData();
            row470.LineNo = 6;
            row470.MaterialId = 9083;
            row470.SpecNo = "SA-268";
            row470.TypeGrade = "";
            row470.ProductForm = "Wld. tube";
            row470.AlloyUNS = "S44400";
            row470.ClassCondition = "";
            row470.Notes = "G24, G32";
            row470.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.5, 10.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row470);

            // Row 7: SA-268, , Smls. tube
            var row471 = new OldStressRowData();
            row471.LineNo = 7;
            row471.MaterialId = 9084;
            row471.SpecNo = "SA-268";
            row471.TypeGrade = "";
            row471.ProductForm = "Smls. tube";
            row471.AlloyUNS = "S44400";
            row471.ClassCondition = "";
            row471.Notes = "G32";
            row471.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.8, 12.4, 12.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row471);

            // Row 8: SA-268, TP439, Wld. tube
            var row472 = new OldStressRowData();
            row472.LineNo = 8;
            row472.MaterialId = 9078;
            row472.SpecNo = "SA-268";
            row472.TypeGrade = "TP439";
            row472.ProductForm = "Wld. tube";
            row472.AlloyUNS = "S43035";
            row472.ClassCondition = "";
            row472.Notes = "G24, G32";
            row472.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, 9.9, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row472);

            // Row 9: SA-268, TP439, Smls. tube
            var row473 = new OldStressRowData();
            row473.LineNo = 9;
            row473.MaterialId = 9079;
            row473.SpecNo = "SA-268";
            row473.TypeGrade = "TP439";
            row473.ProductForm = "Smls. tube";
            row473.AlloyUNS = "S43035";
            row473.ClassCondition = "";
            row473.Notes = "G32";
            row473.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.6, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row473);

            // Row 10: SA-803, TP439, Wld. tube
            var row474 = new OldStressRowData();
            row474.LineNo = 10;
            row474.MaterialId = 9080;
            row474.SpecNo = "SA-803";
            row474.TypeGrade = "TP439";
            row474.ProductForm = "Wld. tube";
            row474.AlloyUNS = "S43035";
            row474.ClassCondition = "";
            row474.Notes = "G24, G32";
            row474.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row474);

            // Row 11: SA-731, TP439, Wld. pipe
            var row475 = new OldStressRowData();
            row475.LineNo = 11;
            row475.MaterialId = 9842;
            row475.SpecNo = "SA-731";
            row475.TypeGrade = "TP439";
            row475.ProductForm = "Wld. pipe";
            row475.AlloyUNS = "S43035";
            row475.ClassCondition = "";
            row475.Notes = "G24, G32";
            row475.StressValues = new double?[] { 12.8, null, 12.2, null, 11.8, 11.3, 10.9, 10.6, 10.4, 10.3, 9.9, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row475);

            // Row 12: SA-731, TP439, Smls. pipe
            var row476 = new OldStressRowData();
            row476.LineNo = 12;
            row476.MaterialId = 9843;
            row476.SpecNo = "SA-731";
            row476.TypeGrade = "TP439";
            row476.ProductForm = "Smls. pipe";
            row476.AlloyUNS = "S43035";
            row476.ClassCondition = "";
            row476.Notes = "G32";
            row476.StressValues = new double?[] { 15, null, 14.3, null, 13.8, 13.3, 12.9, 12.4, 12.3, 12.1, 11.6, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row476);

            // Row 13: SA-479, 439, Bar
            var row477 = new OldStressRowData();
            row477.LineNo = 13;
            row477.MaterialId = 9081;
            row477.SpecNo = "SA-479";
            row477.TypeGrade = "439";
            row477.ProductForm = "Bar";
            row477.AlloyUNS = "S43035";
            row477.ClassCondition = "";
            row477.Notes = "G22, G32";
            row477.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.5, 15, 14.5, 14.3, 14.1, 13.6, 12.9, 12.1, 11, 9.2, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row477);

            // Row 14: SA-240, 26-3-3, Plate
            var row478 = new OldStressRowData();
            row478.LineNo = 14;
            row478.MaterialId = 8725;
            row478.SpecNo = "SA-240";
            row478.TypeGrade = "26-3-3";
            row478.ProductForm = "Plate";
            row478.AlloyUNS = "S44660";
            row478.ClassCondition = "";
            row478.Notes = "G32";
            row478.StressValues = new double?[] { 21.2, null, 21.2, null, 21.2, 20.9, 20.8, 20.7, 20.6, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row478);

            // Row 15: SA-268, 26-3-3, Smls. tube
            var row479 = new OldStressRowData();
            row479.LineNo = 15;
            row479.MaterialId = 8726;
            row479.SpecNo = "SA-268";
            row479.TypeGrade = "26-3-3";
            row479.ProductForm = "Smls. tube";
            row479.AlloyUNS = "S44660";
            row479.ClassCondition = "";
            row479.Notes = "G32";
            row479.StressValues = new double?[] { 21.2, null, 21.2, null, 21.2, 20.9, 20.8, 20.7, 20.6, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row479);

            // Row 16: SA-268, 26-3-3, Wld. tube
            var row480 = new OldStressRowData();
            row480.LineNo = 16;
            row480.MaterialId = 8727;
            row480.SpecNo = "SA-268";
            row480.TypeGrade = "26-3-3";
            row480.ProductForm = "Wld. tube";
            row480.AlloyUNS = "S44660";
            row480.ClassCondition = "";
            row480.Notes = "G24, G32";
            row480.StressValues = new double?[] { 18, null, 18, null, 18, 17.8, 17.7, 17.6, 17.5, 17.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row480);

            // Row 17: SA-803, 26-3-3, Wld. tube
            var row481 = new OldStressRowData();
            row481.LineNo = 17;
            row481.MaterialId = 8728;
            row481.SpecNo = "SA-803";
            row481.TypeGrade = "26-3-3";
            row481.ProductForm = "Wld. tube";
            row481.AlloyUNS = "S44660";
            row481.ClassCondition = "";
            row481.Notes = "G24, G32";
            row481.StressValues = new double?[] { 18, null, 18, null, 18, 17.8, 17.7, 17.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row481);

            // Row 18: SA-240, 329, Plate
            var row482 = new OldStressRowData();
            row482.LineNo = 18;
            row482.MaterialId = 8729;
            row482.SpecNo = "SA-240";
            row482.TypeGrade = "329";
            row482.ProductForm = "Plate";
            row482.AlloyUNS = "S32900";
            row482.ClassCondition = "";
            row482.Notes = "G32";
            row482.StressValues = new double?[] { 22.5, null, 21.9, null, 20.5, 19.8, 19.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row482);

            // Row 19: SA-789, , Wld. tube
            var row483 = new OldStressRowData();
            row483.LineNo = 19;
            row483.MaterialId = 8730;
            row483.SpecNo = "SA-789";
            row483.TypeGrade = "";
            row483.ProductForm = "Wld. tube";
            row483.AlloyUNS = "S32900";
            row483.ClassCondition = "";
            row483.Notes = "G24, G32";
            row483.StressValues = new double?[] { 19.1, null, 19.1, null, 18.4, 18, 18, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row483);

            // Row 20: SA-789, , Smls. tube
            var row484 = new OldStressRowData();
            row484.LineNo = 20;
            row484.MaterialId = 8731;
            row484.SpecNo = "SA-789";
            row484.TypeGrade = "";
            row484.ProductForm = "Smls. tube";
            row484.AlloyUNS = "S32900";
            row484.ClassCondition = "";
            row484.Notes = "G32";
            row484.StressValues = new double?[] { 22.5, null, 22.5, null, 21.6, 21.2, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row484);

            // Row 21: SA-790, , Wld. tube
            var row485 = new OldStressRowData();
            row485.LineNo = 21;
            row485.MaterialId = 8732;
            row485.SpecNo = "SA-790";
            row485.TypeGrade = "";
            row485.ProductForm = "Wld. tube";
            row485.AlloyUNS = "S32900";
            row485.ClassCondition = "";
            row485.Notes = "G24, G32";
            row485.StressValues = new double?[] { 19.1, null, 19.1, null, 18.4, 18, 18, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row485);

            // Row 22: SA-790, , Smls. tube
            var row486 = new OldStressRowData();
            row486.LineNo = 22;
            row486.MaterialId = 8733;
            row486.SpecNo = "SA-790";
            row486.TypeGrade = "";
            row486.ProductForm = "Smls. tube";
            row486.AlloyUNS = "S32900";
            row486.ClassCondition = "";
            row486.Notes = "G32";
            row486.StressValues = new double?[] { 22.5, null, 22.5, null, 21.6, 21.2, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row486);

            // Row 23: SA-240, , Plate
            var row487 = new OldStressRowData();
            row487.LineNo = 23;
            row487.MaterialId = 8734;
            row487.SpecNo = "SA-240";
            row487.TypeGrade = "";
            row487.ProductForm = "Plate";
            row487.AlloyUNS = "S32950";
            row487.ClassCondition = "";
            row487.Notes = "G32";
            row487.StressValues = new double?[] { 25, null, 24.9, null, 23.7, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row487);

            // Row 24: SA-789, , Wld. tube
            var row488 = new OldStressRowData();
            row488.LineNo = 24;
            row488.MaterialId = 8735;
            row488.SpecNo = "SA-789";
            row488.TypeGrade = "";
            row488.ProductForm = "Wld. tube";
            row488.AlloyUNS = "S32950";
            row488.ClassCondition = "";
            row488.Notes = "G24, G32";
            row488.StressValues = new double?[] { 21.3, null, 21.2, null, 20.1, 19.6, 19.6, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row488);

            // Row 25: SA-789, , Smls. tube
            var row489 = new OldStressRowData();
            row489.LineNo = 25;
            row489.MaterialId = 8736;
            row489.SpecNo = "SA-789";
            row489.TypeGrade = "";
            row489.ProductForm = "Smls. tube";
            row489.AlloyUNS = "S32950";
            row489.ClassCondition = "";
            row489.Notes = "G32";
            row489.StressValues = new double?[] { 25, null, 24.9, null, 23.7, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row489);

            // Row 26: SA-790, , Wld. pipe
            var row490 = new OldStressRowData();
            row490.LineNo = 26;
            row490.MaterialId = 8737;
            row490.SpecNo = "SA-790";
            row490.TypeGrade = "";
            row490.ProductForm = "Wld. pipe";
            row490.AlloyUNS = "S32950";
            row490.ClassCondition = "";
            row490.Notes = "G24, G32";
            row490.StressValues = new double?[] { 21.3, null, 21.2, null, 20.1, 19.6, 19.6, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row490);

            // Row 27: SA-790, , Smls. pipe
            var row491 = new OldStressRowData();
            row491.LineNo = 27;
            row491.MaterialId = 8738;
            row491.SpecNo = "SA-790";
            row491.TypeGrade = "";
            row491.ProductForm = "Smls. pipe";
            row491.AlloyUNS = "S32950";
            row491.ClassCondition = "";
            row491.Notes = "G32";
            row491.StressValues = new double?[] { 25, null, 24.9, null, 23.7, 23.1, 23.1, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row491);

            // Row 28: SA-268, TP446-1, Smls. tube
            var row492 = new OldStressRowData();
            row492.LineNo = 28;
            row492.MaterialId = 8739;
            row492.SpecNo = "SA-268";
            row492.TypeGrade = "TP446-1";
            row492.ProductForm = "Smls. tube";
            row492.AlloyUNS = "S44600";
            row492.ClassCondition = "";
            row492.Notes = "G32";
            row492.StressValues = new double?[] { 17.5, null, 16.7, null, 16.1, 15.6, 15, 14.5, 14.3, 14.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row492);

            // Row 29: SA-268, TP446-1, Smls. tube
            var row493 = new OldStressRowData();
            row493.LineNo = 29;
            row493.MaterialId = 8740;
            row493.SpecNo = "SA-268";
            row493.TypeGrade = "TP446-1";
            row493.ProductForm = "Smls. tube";
            row493.AlloyUNS = "S44600";
            row493.ClassCondition = "";
            row493.Notes = "G32";
            row493.StressValues = new double?[] { 17.5, null, 16.6, null, 16.1, 15.6, 15, 14.5, 14.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row493);

            // Row 30: SA-182, FXM-27Cb, Forgings
            var row494 = new OldStressRowData();
            row494.LineNo = 30;
            row494.MaterialId = 8741;
            row494.SpecNo = "SA-182";
            row494.TypeGrade = "FXM-27Cb";
            row494.ProductForm = "Forgings";
            row494.AlloyUNS = "S44627";
            row494.ClassCondition = "";
            row494.Notes = "G32";
            row494.StressValues = new double?[] { 15, null, 15, null, 14.6, 14.2, 14.2, 14.2, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row494);

            // Row 31: SA-336, FXM-27Cb, Forgings
            var row495 = new OldStressRowData();
            row495.LineNo = 31;
            row495.MaterialId = 8742;
            row495.SpecNo = "SA-336";
            row495.TypeGrade = "FXM-27Cb";
            row495.ProductForm = "Forgings";
            row495.AlloyUNS = "S44627";
            row495.ClassCondition = "";
            row495.Notes = "G32";
            row495.StressValues = new double?[] { 15, null, 15, null, 14.6, 14.2, 14.2, 14.2, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row495);

            // Row 32: SA-240, XM-27, Plate
            var row496 = new OldStressRowData();
            row496.LineNo = 32;
            row496.MaterialId = 8743;
            row496.SpecNo = "SA-240";
            row496.TypeGrade = "XM-27";
            row496.ProductForm = "Plate";
            row496.AlloyUNS = "S44627";
            row496.ClassCondition = "";
            row496.Notes = "G32";
            row496.StressValues = new double?[] { 16.2, null, 16.2, null, 15.9, 15.9, 15.9, 15.9, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row496);

            // Row 33: SA-268, TPXM-27, Wld. tube
            var row497 = new OldStressRowData();
            row497.LineNo = 33;
            row497.MaterialId = 8744;
            row497.SpecNo = "SA-268";
            row497.TypeGrade = "TPXM-27";
            row497.ProductForm = "Wld. tube";
            row497.AlloyUNS = "S44627";
            row497.ClassCondition = "";
            row497.Notes = "G24, G32";
            row497.StressValues = new double?[] { 13.8, null, 13.8, null, 13.5, 13.5, 13.5, 13.5, 13.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row497);

            // Row 34: SA-268, TPXM-27, Smls. tube
            var row498 = new OldStressRowData();
            row498.LineNo = 34;
            row498.MaterialId = 8745;
            row498.SpecNo = "SA-268";
            row498.TypeGrade = "TPXM-27";
            row498.ProductForm = "Smls. tube";
            row498.AlloyUNS = "S44627";
            row498.ClassCondition = "";
            row498.Notes = "G32";
            row498.StressValues = new double?[] { 16.2, null, 16.2, null, 15.9, 15.9, 15.9, 15.9, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row498);

            // Row 35: SA-479, XM-27, Bar
            var row499 = new OldStressRowData();
            row499.LineNo = 35;
            row499.MaterialId = 8746;
            row499.SpecNo = "SA-479";
            row499.TypeGrade = "XM-27";
            row499.ProductForm = "Bar";
            row499.AlloyUNS = "S44627";
            row499.ClassCondition = "";
            row499.Notes = "G22, G32";
            row499.StressValues = new double?[] { 16.2, null, 16.2, null, 15.9, 15.9, 15.9, 15.9, 15.9, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row499);

            // Row 36: SA-731, TPXM-27, Smls. pipe
            var row500 = new OldStressRowData();
            row500.LineNo = 36;
            row500.MaterialId = 8747;
            row500.SpecNo = "SA-731";
            row500.TypeGrade = "TPXM-27";
            row500.ProductForm = "Smls. pipe";
            row500.AlloyUNS = "S44627";
            row500.ClassCondition = "";
            row500.Notes = "G32";
            row500.StressValues = new double?[] { 16.2, null, 16.2, null, 15.9, 15.9, 15.9, 15.9, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
