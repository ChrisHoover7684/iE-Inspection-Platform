using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers


{
    public static class OldStressDataImporter_Batch002
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch002(MaterialStressService service)
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

            // Row 26: SA-420, WPL6, Smls. & wld. fittings
            var row101 = new OldStressRowData();
            row101.LineNo = 26;
            row101.MaterialId = 8365;
            row101.SpecNo = "SA-420";
            row101.TypeGrade = "WPL6";
            row101.ProductForm = "Smls. & wld. fittings";
            row101.AlloyUNS = "";
            row101.ClassCondition = "";
            row101.Notes = "G10";
            row101.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row101);

            // Row 27: ..., , 
            var row102 = new OldStressRowData();
            row102.LineNo = 27;
            row102.MaterialId = 8366;
            row102.SpecNo = "...";
            row102.TypeGrade = "";
            row102.ProductForm = "";
            row102.AlloyUNS = "";
            row102.ClassCondition = "";
            row102.Notes = "";
            row102.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row102);

            // Row 28: SA-524, I, Smls. pipe
            var row103 = new OldStressRowData();
            row103.LineNo = 28;
            row103.MaterialId = 8367;
            row103.SpecNo = "SA-524";
            row103.TypeGrade = "I";
            row103.ProductForm = "Smls. pipe";
            row103.AlloyUNS = "K02104";
            row103.ClassCondition = "";
            row103.Notes = "G10";
            row103.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row103);

            // Row 29: SA-695, B/35, Bar
            var row104 = new OldStressRowData();
            row104.LineNo = 29;
            row104.MaterialId = 8368;
            row104.SpecNo = "SA-695";
            row104.TypeGrade = "B/35";
            row104.ProductForm = "Bar";
            row104.AlloyUNS = "K03504";
            row104.ClassCondition = "";
            row104.Notes = "G10, G22";
            row104.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row104);

            // Row 30: SA-696, B, Bar
            var row105 = new OldStressRowData();
            row105.LineNo = 30;
            row105.MaterialId = 8369;
            row105.SpecNo = "SA-696";
            row105.TypeGrade = "B";
            row105.ProductForm = "Bar";
            row105.AlloyUNS = "K03200";
            row105.ClassCondition = "";
            row105.Notes = "";
            row105.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row105);

            // Row 31: SA-727, , Forgings
            var row106 = new OldStressRowData();
            row106.LineNo = 31;
            row106.MaterialId = 8370;
            row106.SpecNo = "SA-727";
            row106.TypeGrade = "";
            row106.ProductForm = "Forgings";
            row106.AlloyUNS = "K02506";
            row106.ClassCondition = "";
            row106.Notes = "G10, G22";
            row106.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row106);

            // Row 32: SA-178, C, Wld. tube
            var row107 = new OldStressRowData();
            row107.LineNo = 32;
            row107.MaterialId = 8371;
            row107.SpecNo = "SA-178";
            row107.TypeGrade = "C";
            row107.ProductForm = "Wld. tube";
            row107.AlloyUNS = "K03503";
            row107.ClassCondition = "";
            row107.Notes = "G4, G10, S1, W14";
            row107.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row107);

            // Row 33: SA-178, C, Wld. tube
            var row108 = new OldStressRowData();
            row108.LineNo = 33;
            row108.MaterialId = 8372;
            row108.SpecNo = "SA-178";
            row108.TypeGrade = "C";
            row108.ProductForm = "Wld. tube";
            row108.AlloyUNS = "K03503";
            row108.ClassCondition = "";
            row108.Notes = "G3, G10, S1";
            row108.StressValues = new double?[] { 12.8, null, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.2, 11, 9.2, 7.4, 5, 3.4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row108);

            // Row 34: SA-178, C, Wld. tube
            var row109 = new OldStressRowData();
            row109.LineNo = 34;
            row109.MaterialId = 8373;
            row109.SpecNo = "SA-178";
            row109.TypeGrade = "C";
            row109.ProductForm = "Wld. tube";
            row109.AlloyUNS = "K03503";
            row109.ClassCondition = "";
            row109.Notes = "G24, G35, W6";
            row109.StressValues = new double?[] { 12.8, 12.8, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.2, 11, 9.2, 7.4, 5, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row109);

            // Row 35: SA-210, A1, Smls. tube
            var row110 = new OldStressRowData();
            row110.LineNo = 35;
            row110.MaterialId = 8374;
            row110.SpecNo = "SA-210";
            row110.TypeGrade = "A1";
            row110.ProductForm = "Smls. tube";
            row110.AlloyUNS = "K02707";
            row110.ClassCondition = "";
            row110.Notes = "G10, S1";
            row110.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row110);

            // Row 36: SA-556, B2, Smls. tube
            var row111 = new OldStressRowData();
            row111.LineNo = 36;
            row111.MaterialId = 8375;
            row111.SpecNo = "SA-556";
            row111.TypeGrade = "B2";
            row111.ProductForm = "Smls. tube";
            row111.AlloyUNS = "K02707";
            row111.ClassCondition = "";
            row111.Notes = "G10";
            row111.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row111);

            // Row 37: SA-557, B2, Wld. tube
            var row112 = new OldStressRowData();
            row112.LineNo = 37;
            row112.MaterialId = 8376;
            row112.SpecNo = "SA-557";
            row112.TypeGrade = "B2";
            row112.ProductForm = "Wld. tube";
            row112.AlloyUNS = "K03007";
            row112.ClassCondition = "";
            row112.Notes = "G24, G35, W6";
            row112.StressValues = new double?[] { 12.8, 12.8, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.2, 10.9, 9.2, 7.4, 5, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row112);

            // Row 38: SA/CSA-G40.21, 38W, Plate, bar
            var row113 = new OldStressRowData();
            row113.LineNo = 38;
            row113.MaterialId = 9839;
            row113.SpecNo = "SA/CSA-G40.21";
            row113.TypeGrade = "38W";
            row113.ProductForm = "Plate, bar";
            row113.AlloyUNS = "";
            row113.ClassCondition = "";
            row113.Notes = "G34, G35, G37, G38";
            row113.StressValues = new double?[] { 14.5, null, 14.5, null, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row113);

            // Row 39: SA-675, 65, Bar
            var row114 = new OldStressRowData();
            row114.LineNo = 39;
            row114.MaterialId = 8377;
            row114.SpecNo = "SA-675";
            row114.TypeGrade = "65";
            row114.ProductForm = "Bar";
            row114.AlloyUNS = "";
            row114.ClassCondition = "";
            row114.Notes = "G10, G15, G18, G22, G35, S1";
            row114.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 8.7, 5, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row114);

            // Row 40: SA-675, 65, Bar
            var row115 = new OldStressRowData();
            row115.LineNo = 40;
            row115.MaterialId = 8378;
            row115.SpecNo = "SA-675";
            row115.TypeGrade = "65";
            row115.ProductForm = "Bar";
            row115.AlloyUNS = "";
            row115.ClassCondition = "";
            row115.Notes = "";
            row115.StressValues = new double?[] { 16.2, null, 16.2, null, 16.2, 16.2, 16.2, 16.2, 16.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row115);

            // Row 1: SA-352, LCB, Castings
            var row116 = new OldStressRowData();
            row116.LineNo = 1;
            row116.MaterialId = 8379;
            row116.SpecNo = "SA-352";
            row116.TypeGrade = "LCB";
            row116.ProductForm = "Castings";
            row116.AlloyUNS = "J03003";
            row116.ClassCondition = "";
            row116.Notes = "G1, G17";
            row116.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row116);

            // Row 2: SA-515, 65, Plate
            var row117 = new OldStressRowData();
            row117.LineNo = 2;
            row117.MaterialId = 8380;
            row117.SpecNo = "SA-515";
            row117.TypeGrade = "65";
            row117.ProductForm = "Plate";
            row117.AlloyUNS = "K02800";
            row117.ClassCondition = "";
            row117.Notes = "G10, S1";
            row117.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row117);

            // Row 3: SA-515, 65, Plate
            var row118 = new OldStressRowData();
            row118.LineNo = 3;
            row118.MaterialId = 8381;
            row118.SpecNo = "SA-515";
            row118.TypeGrade = "65";
            row118.ProductForm = "Plate";
            row118.AlloyUNS = "K02800";
            row118.ClassCondition = "";
            row118.Notes = "";
            row118.StressValues = new double?[] { 16.2, null, 16.2, null, 16.2, 16.2, 16.2, 16.2, 16.2, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row118);

            // Row 4: SA-516, 65, Plate
            var row119 = new OldStressRowData();
            row119.LineNo = 4;
            row119.MaterialId = 8382;
            row119.SpecNo = "SA-516";
            row119.TypeGrade = "65";
            row119.ProductForm = "Plate";
            row119.AlloyUNS = "K02403";
            row119.ClassCondition = "";
            row119.Notes = "G10, S1";
            row119.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row119);

            // Row 5: SA-671, CB65, Wld. pipe
            var row120 = new OldStressRowData();
            row120.LineNo = 5;
            row120.MaterialId = 8383;
            row120.SpecNo = "SA-671";
            row120.TypeGrade = "CB65";
            row120.ProductForm = "Wld. pipe";
            row120.AlloyUNS = "K02800";
            row120.ClassCondition = "";
            row120.Notes = "S6, W10, W12";
            row120.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row120);

            // Row 6: SA-671, CC65, Wld. pipe
            var row121 = new OldStressRowData();
            row121.LineNo = 6;
            row121.MaterialId = 8384;
            row121.SpecNo = "SA-671";
            row121.TypeGrade = "CC65";
            row121.ProductForm = "Wld. pipe";
            row121.AlloyUNS = "K02403";
            row121.ClassCondition = "";
            row121.Notes = "S6, W10, W12";
            row121.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row121);

            // Row 7: SA-672, B65, Wld. pipe
            var row122 = new OldStressRowData();
            row122.LineNo = 7;
            row122.MaterialId = 8385;
            row122.SpecNo = "SA-672";
            row122.TypeGrade = "B65";
            row122.ProductForm = "Wld. pipe";
            row122.AlloyUNS = "K02800";
            row122.ClassCondition = "";
            row122.Notes = "S6, W10, W12";
            row122.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row122);

            // Row 8: SA-672, C65, Wld. pipe
            var row123 = new OldStressRowData();
            row123.LineNo = 8;
            row123.MaterialId = 8386;
            row123.SpecNo = "SA-672";
            row123.TypeGrade = "C65";
            row123.ProductForm = "Wld. pipe";
            row123.AlloyUNS = "K02403";
            row123.ClassCondition = "";
            row123.Notes = "S6, W10, W12";
            row123.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row123);

            // Row 9: SA-414, E, Sheet
            var row124 = new OldStressRowData();
            row124.LineNo = 9;
            row124.MaterialId = 8387;
            row124.SpecNo = "SA-414";
            row124.TypeGrade = "E";
            row124.ProductForm = "Sheet";
            row124.AlloyUNS = "K02704";
            row124.ClassCondition = "";
            row124.Notes = "G10, G35";
            row124.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row124);

            // Row 10: SA-662, B, Plate
            var row125 = new OldStressRowData();
            row125.LineNo = 10;
            row125.MaterialId = 8388;
            row125.SpecNo = "SA-662";
            row125.TypeGrade = "B";
            row125.ProductForm = "Plate";
            row125.AlloyUNS = "K02203";
            row125.ClassCondition = "";
            row125.Notes = "";
            row125.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row125);

            // Row 11: SA-537, , Plate
            var row126 = new OldStressRowData();
            row126.LineNo = 11;
            row126.MaterialId = 8389;
            row126.SpecNo = "SA-537";
            row126.TypeGrade = "";
            row126.ProductForm = "Plate";
            row126.AlloyUNS = "K12437";
            row126.ClassCondition = "1";
            row126.Notes = "";
            row126.StressValues = new double?[] { 16.3, null, 16.3, null, 16, 16, 16, 16, 16, 16, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row126);

            // Row 12: SA-691, CMSH-70, Wld. pipe
            var row127 = new OldStressRowData();
            row127.LineNo = 12;
            row127.MaterialId = 8390;
            row127.SpecNo = "SA-691";
            row127.TypeGrade = "CMSH-70";
            row127.ProductForm = "Wld. pipe";
            row127.AlloyUNS = "K12437";
            row127.ClassCondition = "";
            row127.Notes = "G26, W10, W12";
            row127.StressValues = new double?[] { 16.3, null, 16.3, null, 16, 16, 16, 16, 16, 16, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row127);

            // Row 13: SA-266, 2, Forgings
            var row128 = new OldStressRowData();
            row128.LineNo = 13;
            row128.MaterialId = 8391;
            row128.SpecNo = "SA-266";
            row128.TypeGrade = "2";
            row128.ProductForm = "Forgings";
            row128.AlloyUNS = "K03506";
            row128.ClassCondition = "";
            row128.Notes = "G10";
            row128.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row128);

            // Row 14: SA-455, , Plate
            var row129 = new OldStressRowData();
            row129.LineNo = 14;
            row129.MaterialId = 8392;
            row129.SpecNo = "SA-455";
            row129.TypeGrade = "";
            row129.ProductForm = "Plate";
            row129.AlloyUNS = "K03300";
            row129.ClassCondition = "";
            row129.Notes = "";
            row129.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row129);

            // Row 15: SA-675, 70, Bar
            var row130 = new OldStressRowData();
            row130.LineNo = 15;
            row130.MaterialId = 8393;
            row130.SpecNo = "SA-675";
            row130.TypeGrade = "70";
            row130.ProductForm = "Bar";
            row130.AlloyUNS = "";
            row130.ClassCondition = "";
            row130.Notes = "G10, G15, G18, G22, G35, S1";
            row130.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row130);

            // Row 16: SA-105, , Forgings
            var row131 = new OldStressRowData();
            row131.LineNo = 16;
            row131.MaterialId = 8394;
            row131.SpecNo = "SA-105";
            row131.TypeGrade = "";
            row131.ProductForm = "Forgings";
            row131.AlloyUNS = "K03504";
            row131.ClassCondition = "";
            row131.Notes = "G10, G18, G35, S1";
            row131.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row131);

            // Row 17: SA-181, , Forgings
            var row132 = new OldStressRowData();
            row132.LineNo = 17;
            row132.MaterialId = 8395;
            row132.SpecNo = "SA-181";
            row132.TypeGrade = "";
            row132.ProductForm = "Forgings";
            row132.AlloyUNS = "K03502";
            row132.ClassCondition = "70";
            row132.Notes = "G10, G18, G35, S1";
            row132.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row132);

            // Row 18: SA-216, WCB, Castings
            var row133 = new OldStressRowData();
            row133.LineNo = 18;
            row133.MaterialId = 8396;
            row133.SpecNo = "SA-216";
            row133.TypeGrade = "WCB";
            row133.ProductForm = "Castings";
            row133.AlloyUNS = "J03002";
            row133.ClassCondition = "";
            row133.Notes = "G1, G10, G17, G18, S1";
            row133.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row133);

            // Row 19: SA-266, 2, Forgings
            var row134 = new OldStressRowData();
            row134.LineNo = 19;
            row134.MaterialId = 8397;
            row134.SpecNo = "SA-266";
            row134.TypeGrade = "2";
            row134.ProductForm = "Forgings";
            row134.AlloyUNS = "K03506";
            row134.ClassCondition = "";
            row134.Notes = "G10, G18, S1";
            row134.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row134);

            // Row 20: SA-266, 4, Forgings
            var row135 = new OldStressRowData();
            row135.LineNo = 20;
            row135.MaterialId = 8398;
            row135.SpecNo = "SA-266";
            row135.TypeGrade = "4";
            row135.ProductForm = "Forgings";
            row135.AlloyUNS = "K03017";
            row135.ClassCondition = "";
            row135.Notes = "G10";
            row135.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row135);

            // Row 21: SA-350, LF2, Forgings
            var row136 = new OldStressRowData();
            row136.LineNo = 21;
            row136.MaterialId = 8399;
            row136.SpecNo = "SA-350";
            row136.TypeGrade = "LF2";
            row136.ProductForm = "Forgings";
            row136.AlloyUNS = "K03011";
            row136.ClassCondition = "";
            row136.Notes = "G10";
            row136.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row136);

            // Row 22: SA-508, 1, Forgings
            var row137 = new OldStressRowData();
            row137.LineNo = 22;
            row137.MaterialId = 8400;
            row137.SpecNo = "SA-508";
            row137.TypeGrade = "1";
            row137.ProductForm = "Forgings";
            row137.AlloyUNS = "K13502";
            row137.ClassCondition = "";
            row137.Notes = "G10";
            row137.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row137);

            // Row 23: SA-508, 1A, Forgings
            var row138 = new OldStressRowData();
            row138.LineNo = 23;
            row138.MaterialId = 8401;
            row138.SpecNo = "SA-508";
            row138.TypeGrade = "1A";
            row138.ProductForm = "Forgings";
            row138.AlloyUNS = "K13502";
            row138.ClassCondition = "";
            row138.Notes = "G10";
            row138.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row138);

            // Row 24: SA-541, 1, Forgings
            var row139 = new OldStressRowData();
            row139.LineNo = 24;
            row139.MaterialId = 8402;
            row139.SpecNo = "SA-541";
            row139.TypeGrade = "1";
            row139.ProductForm = "Forgings";
            row139.AlloyUNS = "K03506";
            row139.ClassCondition = "";
            row139.Notes = "G10";
            row139.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row139);

            // Row 25: SA-541, 1A, Forgings
            var row140 = new OldStressRowData();
            row140.LineNo = 25;
            row140.MaterialId = 8403;
            row140.SpecNo = "SA-541";
            row140.TypeGrade = "1A";
            row140.ProductForm = "Forgings";
            row140.AlloyUNS = "K03506";
            row140.ClassCondition = "";
            row140.Notes = "G10";
            row140.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row140);

            // Row 26: SA-660, WCB, Cast pipe
            var row141 = new OldStressRowData();
            row141.LineNo = 26;
            row141.MaterialId = 8404;
            row141.SpecNo = "SA-660";
            row141.TypeGrade = "WCB";
            row141.ProductForm = "Cast pipe";
            row141.AlloyUNS = "J03003";
            row141.ClassCondition = "";
            row141.Notes = "G1, G10, G17, G18, S1";
            row141.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row141);

            // Row 27: SA-765, II, Forgings
            var row142 = new OldStressRowData();
            row142.LineNo = 27;
            row142.MaterialId = 8405;
            row142.SpecNo = "SA-765";
            row142.TypeGrade = "II";
            row142.ProductForm = "Forgings";
            row142.AlloyUNS = "K03047";
            row142.ClassCondition = "";
            row142.Notes = "";
            row142.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row142);

            // Row 28: SA-515, 70, Plate
            var row143 = new OldStressRowData();
            row143.LineNo = 28;
            row143.MaterialId = 8406;
            row143.SpecNo = "SA-515";
            row143.TypeGrade = "70";
            row143.ProductForm = "Plate";
            row143.AlloyUNS = "K03101";
            row143.ClassCondition = "";
            row143.Notes = "G10, S1";
            row143.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row143);

            // Row 29: SA-516, 70, Plate
            var row144 = new OldStressRowData();
            row144.LineNo = 29;
            row144.MaterialId = 8407;
            row144.SpecNo = "SA-516";
            row144.TypeGrade = "70";
            row144.ProductForm = "Plate";
            row144.AlloyUNS = "K02700";
            row144.ClassCondition = "";
            row144.Notes = "G10, S1";
            row144.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row144);

            // Row 30: SA-671, CB70, Wld. pipe
            var row145 = new OldStressRowData();
            row145.LineNo = 30;
            row145.MaterialId = 8408;
            row145.SpecNo = "SA-671";
            row145.TypeGrade = "CB70";
            row145.ProductForm = "Wld. pipe";
            row145.AlloyUNS = "K03101";
            row145.ClassCondition = "";
            row145.Notes = "S5, W10, W12";
            row145.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row145);

            // Row 31: SA-671, CC70, Wld. pipe
            var row146 = new OldStressRowData();
            row146.LineNo = 31;
            row146.MaterialId = 8409;
            row146.SpecNo = "SA-671";
            row146.TypeGrade = "CC70";
            row146.ProductForm = "Wld. pipe";
            row146.AlloyUNS = "K02700";
            row146.ClassCondition = "";
            row146.Notes = "S6, W10, W12";
            row146.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row146);

            // Row 32: SA-672, B70, Wld. pipe
            var row147 = new OldStressRowData();
            row147.LineNo = 32;
            row147.MaterialId = 8410;
            row147.SpecNo = "SA-672";
            row147.TypeGrade = "B70";
            row147.ProductForm = "Wld. pipe";
            row147.AlloyUNS = "K03101";
            row147.ClassCondition = "";
            row147.Notes = "S5, W10, W12";
            row147.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row147);

            // Row 33: SA-672, C70, Wld. pipe
            var row148 = new OldStressRowData();
            row148.LineNo = 33;
            row148.MaterialId = 8411;
            row148.SpecNo = "SA-672";
            row148.TypeGrade = "C70";
            row148.ProductForm = "Wld. pipe";
            row148.AlloyUNS = "K02700";
            row148.ClassCondition = "";
            row148.Notes = "S6, W10, W12";
            row148.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row148);

            // Row 34: SA-106, C, Smls. pipe
            var row149 = new OldStressRowData();
            row149.LineNo = 34;
            row149.MaterialId = 8412;
            row149.SpecNo = "SA-106";
            row149.TypeGrade = "C";
            row149.ProductForm = "Smls. pipe";
            row149.AlloyUNS = "K03501";
            row149.ClassCondition = "";
            row149.Notes = "G10, S1";
            row149.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row149);

            // Row 35: SA-178, D, Wld. tube
            var row150 = new OldStressRowData();
            row150.LineNo = 35;
            row150.MaterialId = 8413;
            row150.SpecNo = "SA-178";
            row150.TypeGrade = "D";
            row150.ProductForm = "Wld. tube";
            row150.AlloyUNS = "";
            row150.ClassCondition = "";
            row150.Notes = "G4, G10, S1, W14";
            row150.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row150);

            // Row 36: SA-178, D, Wld. tube
            var row151 = new OldStressRowData();
            row151.LineNo = 36;
            row151.MaterialId = 8414;
            row151.SpecNo = "SA-178";
            row151.TypeGrade = "D";
            row151.ProductForm = "Wld. tube";
            row151.AlloyUNS = "";
            row151.ClassCondition = "";
            row151.Notes = "G3, G10, S1";
            row151.StressValues = new double?[] { 14.9, null, 14.9, null, 14.9, 14.9, 14.9, 14.9, 14.9, 14.1, 12.6, 10.2, 9.3, 6.7, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row151);

            // Row 37: SA-210, C, Smls. tube
            var row152 = new OldStressRowData();
            row152.LineNo = 37;
            row152.MaterialId = 8415;
            row152.SpecNo = "SA-210";
            row152.TypeGrade = "C";
            row152.ProductForm = "Smls. tube";
            row152.AlloyUNS = "K03501";
            row152.ClassCondition = "";
            row152.Notes = "G10, S1";
            row152.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row152);

            // Row 38: SA-216, WCC, Castings
            var row153 = new OldStressRowData();
            row153.LineNo = 38;
            row153.MaterialId = 8416;
            row153.SpecNo = "SA-216";
            row153.TypeGrade = "WCC";
            row153.ProductForm = "Castings";
            row153.AlloyUNS = "J02503";
            row153.ClassCondition = "";
            row153.Notes = "G1, G10, G17, G18, S1";
            row153.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row153);

            // Row 1: SA-234, WPC, Smls. & wld. fittings
            var row154 = new OldStressRowData();
            row154.LineNo = 1;
            row154.MaterialId = 8417;
            row154.SpecNo = "SA-234";
            row154.TypeGrade = "WPC";
            row154.ProductForm = "Smls. & wld. fittings";
            row154.AlloyUNS = "K03501";
            row154.ClassCondition = "";
            row154.Notes = "G10, G18";
            row154.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row154);

            // Row 2: ..., , 
            var row155 = new OldStressRowData();
            row155.LineNo = 2;
            row155.MaterialId = 8418;
            row155.SpecNo = "...";
            row155.TypeGrade = "";
            row155.ProductForm = "";
            row155.AlloyUNS = "";
            row155.ClassCondition = "";
            row155.Notes = "";
            row155.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row155);

            // Row 3: SA-352, LCC, Castings
            var row156 = new OldStressRowData();
            row156.LineNo = 3;
            row156.MaterialId = 8419;
            row156.SpecNo = "SA-352";
            row156.TypeGrade = "LCC";
            row156.ProductForm = "Castings";
            row156.AlloyUNS = "J02505";
            row156.ClassCondition = "";
            row156.Notes = "G17";
            row156.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row156);

            // Row 4: SA-487, 16, Castings
            var row157 = new OldStressRowData();
            row157.LineNo = 4;
            row157.MaterialId = 8420;
            row157.SpecNo = "SA-487";
            row157.TypeGrade = "16";
            row157.ProductForm = "Castings";
            row157.AlloyUNS = "";
            row157.ClassCondition = "A";
            row157.Notes = "";
            row157.StressValues = new double?[] { 17.5, null, 17.4, null, 16.4, 15.8, 15.8, 15.8, 15.8, 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row157);

            // Row 5: SA-537, , Plate
            var row158 = new OldStressRowData();
            row158.LineNo = 5;
            row158.MaterialId = 9813;
            row158.SpecNo = "SA-537";
            row158.TypeGrade = "";
            row158.ProductForm = "Plate";
            row158.AlloyUNS = "K12437";
            row158.ClassCondition = "3";
            row158.Notes = "G21, G23, W11";
            row158.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row158);

            // Row 6: SA-556, C2, Smls. tube
            var row159 = new OldStressRowData();
            row159.LineNo = 6;
            row159.MaterialId = 8421;
            row159.SpecNo = "SA-556";
            row159.TypeGrade = "C2";
            row159.ProductForm = "Smls. tube";
            row159.AlloyUNS = "K03006";
            row159.ClassCondition = "";
            row159.Notes = "G10";
            row159.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row159);

            // Row 7: SA-557, C2, Tube
            var row160 = new OldStressRowData();
            row160.LineNo = 7;
            row160.MaterialId = 8422;
            row160.SpecNo = "SA-557";
            row160.TypeGrade = "C2";
            row160.ProductForm = "Tube";
            row160.AlloyUNS = "K03505";
            row160.ClassCondition = "";
            row160.Notes = "G24, G35, W6";
            row160.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.1, 12.5, 10.2, 7.9, 5.7, 3.4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row160);

            // Row 8: SA-660, WCC, Cast pipe
            var row161 = new OldStressRowData();
            row161.LineNo = 8;
            row161.MaterialId = 8423;
            row161.SpecNo = "SA-660";
            row161.TypeGrade = "WCC";
            row161.ProductForm = "Cast pipe";
            row161.AlloyUNS = "J02505";
            row161.ClassCondition = "";
            row161.Notes = "G1, G10, G17, G18, S1";
            row161.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row161);

            // Row 9: SA-695, B/40, Bar
            var row162 = new OldStressRowData();
            row162.LineNo = 9;
            row162.MaterialId = 8424;
            row162.SpecNo = "SA-695";
            row162.TypeGrade = "B/40";
            row162.ProductForm = "Bar";
            row162.AlloyUNS = "K03504";
            row162.ClassCondition = "";
            row162.Notes = "G10";
            row162.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row162);

            // Row 10: SA-696, C, Bar
            var row163 = new OldStressRowData();
            row163.LineNo = 10;
            row163.MaterialId = 8425;
            row163.SpecNo = "SA-696";
            row163.TypeGrade = "C";
            row163.ProductForm = "Bar";
            row163.AlloyUNS = "K03200";
            row163.ClassCondition = "";
            row163.Notes = "";
            row163.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row163);

            // Row 11: SA-414, F, Sheet
            var row164 = new OldStressRowData();
            row164.LineNo = 11;
            row164.MaterialId = 8426;
            row164.SpecNo = "SA-414";
            row164.TypeGrade = "F";
            row164.ProductForm = "Sheet";
            row164.AlloyUNS = "K03102";
            row164.ClassCondition = "";
            row164.Notes = "G10, G35";
            row164.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row164);

            // Row 12: SA-662, C, Plate
            var row165 = new OldStressRowData();
            row165.LineNo = 12;
            row165.MaterialId = 8427;
            row165.SpecNo = "SA-662";
            row165.TypeGrade = "C";
            row165.ProductForm = "Plate";
            row165.AlloyUNS = "K02007";
            row165.ClassCondition = "";
            row165.Notes = "";
            row165.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row165);

            // Row 13: SA-537, , Plate
            var row166 = new OldStressRowData();
            row166.LineNo = 13;
            row166.MaterialId = 8429;
            row166.SpecNo = "SA-537";
            row166.TypeGrade = "";
            row166.ProductForm = "Plate";
            row166.AlloyUNS = "K12437";
            row166.ClassCondition = "2";
            row166.Notes = "";
            row166.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row166);

            // Row 14: SA-537, , Plate
            var row167 = new OldStressRowData();
            row167.LineNo = 14;
            row167.MaterialId = 8428;
            row167.SpecNo = "SA-537";
            row167.TypeGrade = "";
            row167.ProductForm = "Plate";
            row167.AlloyUNS = "K12437";
            row167.ClassCondition = "2";
            row167.Notes = "G21, G23, W11";
            row167.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row167);

            // Row 15: SA-738, C, Plate
            var row168 = new OldStressRowData();
            row168.LineNo = 15;
            row168.MaterialId = 8430;
            row168.SpecNo = "SA-738";
            row168.TypeGrade = "C";
            row168.ProductForm = "Plate";
            row168.AlloyUNS = "";
            row168.ClassCondition = "";
            row168.Notes = "G21, G23, W11";
            row168.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row168);

            // Row 16: SA-537, , Plate
            var row169 = new OldStressRowData();
            row169.LineNo = 16;
            row169.MaterialId = 8431;
            row169.SpecNo = "SA-537";
            row169.TypeGrade = "";
            row169.ProductForm = "Plate";
            row169.AlloyUNS = "K12437";
            row169.ClassCondition = "1";
            row169.Notes = "G23";
            row169.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row169);

            // Row 17: SA-671, CD70, Wld. pipe
            var row170 = new OldStressRowData();
            row170.LineNo = 17;
            row170.MaterialId = 8432;
            row170.SpecNo = "SA-671";
            row170.TypeGrade = "CD70";
            row170.ProductForm = "Wld. pipe";
            row170.AlloyUNS = "K12437";
            row170.ClassCondition = "";
            row170.Notes = "S6, W10, W12";
            row170.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row170);

            // Row 18: SA-672, D70, Wld. pipe
            var row171 = new OldStressRowData();
            row171.LineNo = 18;
            row171.MaterialId = 8433;
            row171.SpecNo = "SA-672";
            row171.TypeGrade = "D70";
            row171.ProductForm = "Wld. pipe";
            row171.AlloyUNS = "K12437";
            row171.ClassCondition = "";
            row171.Notes = "S6, W10, W12";
            row171.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.4, 17.4, 17.4, 17.2, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row171);

            // Row 19: SA-691, CMSH-70, Wld. pipe
            var row172 = new OldStressRowData();
            row172.LineNo = 19;
            row172.MaterialId = 8434;
            row172.SpecNo = "SA-691";
            row172.TypeGrade = "CMSH-70";
            row172.ProductForm = "Wld. pipe";
            row172.AlloyUNS = "K12437";
            row172.ClassCondition = "";
            row172.Notes = "S6, W10, W12";
            row172.StressValues = new double?[] { 17.5, null, 17.5, null, 17.2, 17.2, 17.2, 17.2, 17.2, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row172);

            // Row 20: SA-455, , Plate
            var row173 = new OldStressRowData();
            row173.LineNo = 20;
            row173.MaterialId = 8436;
            row173.SpecNo = "SA-455";
            row173.TypeGrade = "";
            row173.ProductForm = "Plate";
            row173.AlloyUNS = "K03300";
            row173.ClassCondition = "";
            row173.Notes = "";
            row173.StressValues = new double?[] { 18.2, null, 18.2, null, 18.2, 18.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row173);

            // Row 21: SA-455, , Plate
            var row174 = new OldStressRowData();
            row174.LineNo = 21;
            row174.MaterialId = 8435;
            row174.SpecNo = "SA-455";
            row174.TypeGrade = "";
            row174.ProductForm = "Plate";
            row174.AlloyUNS = "K03300";
            row174.ClassCondition = "";
            row174.Notes = "";
            row174.StressValues = new double?[] { 18.3, 18.3, 18.3, null, 18.3, 18.3, 18.3, 18.3, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row174);

            // Row 22: SA-266, 3, Forgings
            var row175 = new OldStressRowData();
            row175.LineNo = 22;
            row175.MaterialId = 8437;
            row175.SpecNo = "SA-266";
            row175.TypeGrade = "3";
            row175.ProductForm = "Forgings";
            row175.AlloyUNS = "K05001";
            row175.ClassCondition = "";
            row175.Notes = "G10, G18, S1, W2, W8, W11";
            row175.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row175);

            // Row 23: SA-455, , Plate
            var row176 = new OldStressRowData();
            row176.LineNo = 23;
            row176.MaterialId = 8438;
            row176.SpecNo = "SA-455";
            row176.TypeGrade = "";
            row176.ProductForm = "Plate";
            row176.AlloyUNS = "K03300";
            row176.ClassCondition = "";
            row176.Notes = "";
            row176.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row176);

            // Row 24: SA-299, , Plate
            var row177 = new OldStressRowData();
            row177.LineNo = 24;
            row177.MaterialId = 8439;
            row177.SpecNo = "SA-299";
            row177.TypeGrade = "";
            row177.ProductForm = "Plate";
            row177.AlloyUNS = "K02803";
            row177.ClassCondition = "";
            row177.Notes = "G10, S1";
            row177.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row177);

            // Row 25: SA-671, CK75, Wld. pipe
            var row178 = new OldStressRowData();
            row178.LineNo = 25;
            row178.MaterialId = 8440;
            row178.SpecNo = "SA-671";
            row178.TypeGrade = "CK75";
            row178.ProductForm = "Wld. pipe";
            row178.AlloyUNS = "K02803";
            row178.ClassCondition = "";
            row178.Notes = "S6, W10, W12";
            row178.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row178);

            // Row 26: SA-672, N75, Wld. pipe
            var row179 = new OldStressRowData();
            row179.LineNo = 26;
            row179.MaterialId = 8441;
            row179.SpecNo = "SA-672";
            row179.TypeGrade = "N75";
            row179.ProductForm = "Wld. pipe";
            row179.AlloyUNS = "K02803";
            row179.ClassCondition = "";
            row179.Notes = "S6, W10, W12";
            row179.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row179);

            // Row 27: SA-691, CMS-75, Wld. pipe
            var row180 = new OldStressRowData();
            row180.LineNo = 27;
            row180.MaterialId = 8442;
            row180.SpecNo = "SA-691";
            row180.TypeGrade = "CMS-75";
            row180.ProductForm = "Wld. pipe";
            row180.AlloyUNS = "K02803";
            row180.ClassCondition = "";
            row180.Notes = "S6, W10, W12";
            row180.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row180);

            // Row 28: SA-299, , Plate
            var row181 = new OldStressRowData();
            row181.LineNo = 28;
            row181.MaterialId = 8443;
            row181.SpecNo = "SA-299";
            row181.TypeGrade = "";
            row181.ProductForm = "Plate";
            row181.AlloyUNS = "K02803";
            row181.ClassCondition = "";
            row181.Notes = "G10, S1";
            row181.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row181);

            // Row 29: SA-691, CMS-75, Wld. pipe
            var row182 = new OldStressRowData();
            row182.LineNo = 29;
            row182.MaterialId = 8444;
            row182.SpecNo = "SA-691";
            row182.TypeGrade = "CMS-75";
            row182.ProductForm = "Wld. pipe";
            row182.AlloyUNS = "K02803";
            row182.ClassCondition = "";
            row182.Notes = "W10, W12";
            row182.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row182);

            // Row 30: SA-372, B, Forgings
            var row183 = new OldStressRowData();
            row183.LineNo = 30;
            row183.MaterialId = 8445;
            row183.SpecNo = "SA-372";
            row183.TypeGrade = "B";
            row183.ProductForm = "Forgings";
            row183.AlloyUNS = "K04001";
            row183.ClassCondition = "";
            row183.Notes = "W2, W11";
            row183.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row183);

            // Row 31: SA-372, B, Forgings
            var row184 = new OldStressRowData();
            row184.LineNo = 31;
            row184.MaterialId = 9780;
            row184.SpecNo = "SA-372";
            row184.TypeGrade = "B";
            row184.ProductForm = "Forgings";
            row184.AlloyUNS = "K04001";
            row184.ClassCondition = "";
            row184.Notes = "W2, W11";
            row184.StressValues = new double?[] { 18.8, null, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row184);

            // Row 32: SA-414, G, Sheet
            var row185 = new OldStressRowData();
            row185.LineNo = 32;
            row185.MaterialId = 8446;
            row185.SpecNo = "SA-414";
            row185.TypeGrade = "G";
            row185.ProductForm = "Sheet";
            row185.AlloyUNS = "K03103";
            row185.ClassCondition = "";
            row185.Notes = "G10, G35";
            row185.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, 15.7, 12.6, 9.3, 6.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row185);

            // Row 33: SA-738, A, Plate
            var row186 = new OldStressRowData();
            row186.LineNo = 33;
            row186.MaterialId = 8447;
            row186.SpecNo = "SA-738";
            row186.TypeGrade = "A";
            row186.ProductForm = "Plate";
            row186.AlloyUNS = "K12447";
            row186.ClassCondition = "";
            row186.Notes = "";
            row186.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.4, 18.4, 18.4, 18.4, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row186);

            // Row 34: SA-537, , Plate
            var row187 = new OldStressRowData();
            row187.LineNo = 34;
            row187.MaterialId = 9814;
            row187.SpecNo = "SA-537";
            row187.TypeGrade = "";
            row187.ProductForm = "Plate";
            row187.AlloyUNS = "K12437";
            row187.ClassCondition = "3";
            row187.Notes = "G23, W11";
            row187.StressValues = new double?[] { 18.8, null, 18.7, null, 18.7, 18.6, 18.6, 18.6, 18.4, 18.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row187);

            // Row 35: SA-537, , Plate
            var row188 = new OldStressRowData();
            row188.LineNo = 35;
            row188.MaterialId = 8448;
            row188.SpecNo = "SA-537";
            row188.TypeGrade = "";
            row188.ProductForm = "Plate";
            row188.AlloyUNS = "K12437";
            row188.ClassCondition = "2";
            row188.Notes = "G23, W11";
            row188.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.6, 18.6, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row188);

            // Row 36: SA-691, CMSH-80, Wld. pipe
            var row189 = new OldStressRowData();
            row189.LineNo = 36;
            row189.MaterialId = 8449;
            row189.SpecNo = "SA-691";
            row189.TypeGrade = "CMSH-80";
            row189.ProductForm = "Wld. pipe";
            row189.AlloyUNS = "K12437";
            row189.ClassCondition = "";
            row189.Notes = "G26, W10, W12";
            row189.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.6, 18.6, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row189);

            // Row 37: SA-738, C, Plate
            var row190 = new OldStressRowData();
            row190.LineNo = 37;
            row190.MaterialId = 8450;
            row190.SpecNo = "SA-738";
            row190.TypeGrade = "C";
            row190.ProductForm = "Plate";
            row190.AlloyUNS = "";
            row190.ClassCondition = "";
            row190.Notes = "G21, G23, W11";
            row190.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.6, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row190);

            // Row 38: SA-537, , Plate
            var row191 = new OldStressRowData();
            row191.LineNo = 38;
            row191.MaterialId = 9815;
            row191.SpecNo = "SA-537";
            row191.TypeGrade = "";
            row191.ProductForm = "Plate";
            row191.AlloyUNS = "K12437";
            row191.ClassCondition = "3";
            row191.Notes = "G23, W11";
            row191.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.7, 19.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row191);

            // Row 39: SA-537, , Plate
            var row192 = new OldStressRowData();
            row192.LineNo = 39;
            row192.MaterialId = 8452;
            row192.SpecNo = "SA-537";
            row192.TypeGrade = "";
            row192.ProductForm = "Plate";
            row192.AlloyUNS = "K12437";
            row192.ClassCondition = "2";
            row192.Notes = "S6, W10, W12";
            row192.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row192);

            // Row 40: SA-537, , Plate
            var row193 = new OldStressRowData();
            row193.LineNo = 40;
            row193.MaterialId = 8451;
            row193.SpecNo = "SA-537";
            row193.TypeGrade = "";
            row193.ProductForm = "Plate";
            row193.AlloyUNS = "K12437";
            row193.ClassCondition = "2";
            row193.Notes = "G23, W11";
            row193.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row193);

            // Row 1: SA-671, CD80, Wld. pipe
            var row194 = new OldStressRowData();
            row194.LineNo = 1;
            row194.MaterialId = 8453;
            row194.SpecNo = "SA-671";
            row194.TypeGrade = "CD80";
            row194.ProductForm = "Wld. pipe";
            row194.AlloyUNS = "K12437";
            row194.ClassCondition = "";
            row194.Notes = "S6, W10, W12";
            row194.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row194);

            // Row 2: SA-672, D80, Wld. pipe
            var row195 = new OldStressRowData();
            row195.LineNo = 2;
            row195.MaterialId = 8454;
            row195.SpecNo = "SA-672";
            row195.TypeGrade = "D80";
            row195.ProductForm = "Wld. pipe";
            row195.AlloyUNS = "K12437";
            row195.ClassCondition = "";
            row195.Notes = "S6, W10, W12";
            row195.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row195);

            // Row 3: SA-691, CMSH-80, Wld. pipe
            var row196 = new OldStressRowData();
            row196.LineNo = 3;
            row196.MaterialId = 8455;
            row196.SpecNo = "SA-691";
            row196.TypeGrade = "CMSH-80";
            row196.ProductForm = "Wld. pipe";
            row196.AlloyUNS = "K12437";
            row196.ClassCondition = "";
            row196.Notes = "S6, W10, W12";
            row196.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row196);

            // Row 4: SA-738, C, Plate
            var row197 = new OldStressRowData();
            row197.LineNo = 4;
            row197.MaterialId = 8456;
            row197.SpecNo = "SA-738";
            row197.TypeGrade = "C";
            row197.ProductForm = "Plate";
            row197.AlloyUNS = "";
            row197.ClassCondition = "";
            row197.Notes = "G23, W11";
            row197.StressValues = new double?[] { 20, null, 20, null, 20, 19.8, 19.8, 19.8, 19.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row197);

            // Row 5: SA-612, , Plate
            var row198 = new OldStressRowData();
            row198.LineNo = 5;
            row198.MaterialId = 8458;
            row198.SpecNo = "SA-612";
            row198.TypeGrade = "";
            row198.ProductForm = "Plate";
            row198.AlloyUNS = "K02900";
            row198.ClassCondition = "";
            row198.Notes = "";
            row198.StressValues = new double?[] { 20.2, null, 20.2, null, 19.8, 19.8, 19.8, 19.8, 19.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row198);

            // Row 6: SA-612, , Plate
            var row199 = new OldStressRowData();
            row199.LineNo = 6;
            row199.MaterialId = 8459;
            row199.SpecNo = "SA-612";
            row199.TypeGrade = "";
            row199.ProductForm = "Plate";
            row199.AlloyUNS = "K02900";
            row199.ClassCondition = "";
            row199.Notes = "";
            row199.StressValues = new double?[] { 20.7, null, 20.7, null, 20.4, 20.4, 20.4, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row199);

            // Row 7: SA-612, , Plate
            var row200 = new OldStressRowData();
            row200.LineNo = 7;
            row200.MaterialId = 8457;
            row200.SpecNo = "SA-612";
            row200.TypeGrade = "";
            row200.ProductForm = "Plate";
            row200.AlloyUNS = "K02900";
            row200.ClassCondition = "";
            row200.Notes = "";
            row200.StressValues = new double?[] { 20.2, null, 20.2, null, 19.8, 19.8, 19.8, 19.8, 19, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row200);

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
