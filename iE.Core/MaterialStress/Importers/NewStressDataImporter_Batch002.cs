using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch002
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

            // Row 28: SA-727, , Forgings
            var row101 = new OldStressRowData();
            row101.LineNo = 28;
            row101.MaterialId = 8370;
            row101.SpecNo = "SA-727";
            row101.TypeGrade = "";
            row101.ProductForm = "Forgings";
            row101.AlloyUNS = "K02506";
            row101.ClassCondition = "";
            row101.Notes = "G10, G22, T1";
            row101.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 15.6, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row101);

            // Row 29: SA-178, C, Wld. tube
            var row102 = new OldStressRowData();
            row102.LineNo = 29;
            row102.MaterialId = 16831;
            row102.SpecNo = "SA-178";
            row102.TypeGrade = "C";
            row102.ProductForm = "Wld. tube";
            row102.AlloyUNS = "K03503";
            row102.ClassCondition = "";
            row102.Notes = "G4, G10, S1, T2";
            row102.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 15.6, 13, 10.8, 8.7, 5, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row102);

            // Row 30: SA-178, C, Wld. tube
            var row103 = new OldStressRowData();
            row103.LineNo = 30;
            row103.MaterialId = 8371;
            row103.SpecNo = "SA-178";
            row103.TypeGrade = "C";
            row103.ProductForm = "Wld. tube";
            row103.AlloyUNS = "K03503";
            row103.ClassCondition = "";
            row103.Notes = "G10, S1, T1, W13";
            row103.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 15.6, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row103);

            // Row 31: SA-178, C, Wld. tube
            var row104 = new OldStressRowData();
            row104.LineNo = 31;
            row104.MaterialId = 8373;
            row104.SpecNo = "SA-178";
            row104.TypeGrade = "C";
            row104.ProductForm = "Wld. tube";
            row104.AlloyUNS = "K03503";
            row104.ClassCondition = "";
            row104.Notes = "G3, G10, G24, G35, S1, T2, W6";
            row104.StressValues = new double?[] { 14.6, null, 14.6, null, 14.6, 14.6, 14.6, 14.6, 14.6, 13.3, 11.1, 9.2, 7.4, 5, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row104);

            // Row 32: SA-210, A-1, Smls. tube
            var row105 = new OldStressRowData();
            row105.LineNo = 32;
            row105.MaterialId = 8374;
            row105.SpecNo = "SA-210";
            row105.TypeGrade = "A-1";
            row105.ProductForm = "Smls. tube";
            row105.AlloyUNS = "K02707";
            row105.ClassCondition = "";
            row105.Notes = "G10, S1, T1";
            row105.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 15.6, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row105);

            // Row 33: SA-556, B2, Smls. tube
            var row106 = new OldStressRowData();
            row106.LineNo = 33;
            row106.MaterialId = 8375;
            row106.SpecNo = "SA-556";
            row106.TypeGrade = "B2";
            row106.ProductForm = "Smls. tube";
            row106.AlloyUNS = "K02707";
            row106.ClassCondition = "";
            row106.Notes = "G10, T1";
            row106.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 15.6, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row106);

            // Row 34: SA-557, B2, Wld. tube
            var row107 = new OldStressRowData();
            row107.LineNo = 34;
            row107.MaterialId = 8376;
            row107.SpecNo = "SA-557";
            row107.TypeGrade = "B2";
            row107.ProductForm = "Wld. tube";
            row107.AlloyUNS = "K03007";
            row107.ClassCondition = "";
            row107.Notes = "G24, G35, T1, W6";
            row107.StressValues = new double?[] { 14.6, 14.6, 14.6, null, 14.6, 14.6, 14.6, 14.6, 14.6, 13.3, 11.1, 9.2, 7.4, 5, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row107);

            // Row 35: SA/CSA-G40.21, 38W, Plate, bar
            var row108 = new OldStressRowData();
            row108.LineNo = 35;
            row108.MaterialId = 9839;
            row108.SpecNo = "SA/CSA-G40.21";
            row108.TypeGrade = "38W";
            row108.ProductForm = "Plate, bar";
            row108.AlloyUNS = "";
            row108.ClassCondition = "";
            row108.Notes = "G34, G35, G37, G38";
            row108.StressValues = new double?[] { 16.6, null, 16.6, null, 16.6, 16.6, 16.6, 16.6, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row108);

            // Row 36: SA/EN 10028-2, P295GH, Plate
            var row109 = new OldStressRowData();
            row109.LineNo = 36;
            row109.MaterialId = 16920;
            row109.SpecNo = "SA/EN 10028-2";
            row109.TypeGrade = "P295GH";
            row109.ProductForm = "Plate";
            row109.AlloyUNS = "";
            row109.ClassCondition = "";
            row109.Notes = "G10, S1, T2";
            row109.StressValues = new double?[] { 18.3, 18.3, 18.3, 18.3, 18.3, 18.3, 18.3, 17.4, 16.8, 16.2, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row109);

            // Row 37: SA-675, 65, Bar
            var row110 = new OldStressRowData();
            row110.LineNo = 37;
            row110.MaterialId = 8377;
            row110.SpecNo = "SA-675";
            row110.TypeGrade = "65";
            row110.ProductForm = "Bar";
            row110.AlloyUNS = "";
            row110.ClassCondition = "";
            row110.Notes = "G10, G15, G18, G22, G35, S1, T2";
            row110.StressValues = new double?[] { 18.6, 18.6, 18.6, 18.6, 18.6, 18.5, 17.7, 16.6, 16.1, 15.5, 13.9, 11.4, 8.7, 5, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row110);

            // Row 1: SA-352, LCB, Castings
            var row111 = new OldStressRowData();
            row111.LineNo = 1;
            row111.MaterialId = 8379;
            row111.SpecNo = "SA-352";
            row111.TypeGrade = "LCB";
            row111.ProductForm = "Castings";
            row111.AlloyUNS = "J03003";
            row111.ClassCondition = "";
            row111.Notes = "G1, G17";
            row111.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row111);

            // Row 2: SA-515, 65, Plate
            var row112 = new OldStressRowData();
            row112.LineNo = 2;
            row112.MaterialId = 8380;
            row112.SpecNo = "SA-515";
            row112.TypeGrade = "65";
            row112.ProductForm = "Plate";
            row112.AlloyUNS = "K02800";
            row112.ClassCondition = "";
            row112.Notes = "G10, S1, T2";
            row112.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row112);

            // Row 3: SA-516, 65, Plate
            var row113 = new OldStressRowData();
            row113.LineNo = 3;
            row113.MaterialId = 8382;
            row113.SpecNo = "SA-516";
            row113.TypeGrade = "65";
            row113.ProductForm = "Plate";
            row113.AlloyUNS = "K02403";
            row113.ClassCondition = "";
            row113.Notes = "G10, S1, T2";
            row113.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row113);

            // Row 4: SA-671, CB65, Wld. pipe
            var row114 = new OldStressRowData();
            row114.LineNo = 4;
            row114.MaterialId = 8383;
            row114.SpecNo = "SA-671";
            row114.TypeGrade = "CB65";
            row114.ProductForm = "Wld. pipe";
            row114.AlloyUNS = "K02800";
            row114.ClassCondition = "";
            row114.Notes = "S6, W10, W12";
            row114.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row114);

            // Row 5: SA-671, CC65, Wld. pipe
            var row115 = new OldStressRowData();
            row115.LineNo = 5;
            row115.MaterialId = 8384;
            row115.SpecNo = "SA-671";
            row115.TypeGrade = "CC65";
            row115.ProductForm = "Wld. pipe";
            row115.AlloyUNS = "K02403";
            row115.ClassCondition = "";
            row115.Notes = "S6, W10, W12";
            row115.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row115);

            // Row 6: SA-672, B65, Wld. pipe
            var row116 = new OldStressRowData();
            row116.LineNo = 6;
            row116.MaterialId = 8385;
            row116.SpecNo = "SA-672";
            row116.TypeGrade = "B65";
            row116.ProductForm = "Wld. pipe";
            row116.AlloyUNS = "K02800";
            row116.ClassCondition = "";
            row116.Notes = "S6, W10, W12";
            row116.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row116);

            // Row 7: SA-672, C65, Wld. pipe
            var row117 = new OldStressRowData();
            row117.LineNo = 7;
            row117.MaterialId = 8386;
            row117.SpecNo = "SA-672";
            row117.TypeGrade = "C65";
            row117.ProductForm = "Wld. pipe";
            row117.AlloyUNS = "K02403";
            row117.ClassCondition = "";
            row117.Notes = "S6, W10, W12";
            row117.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 17.9, 17.3, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row117);

            // Row 8: SA-414, E, Sheet
            var row118 = new OldStressRowData();
            row118.LineNo = 8;
            row118.MaterialId = 8387;
            row118.SpecNo = "SA-414";
            row118.TypeGrade = "E";
            row118.ProductForm = "Sheet";
            row118.AlloyUNS = "K02704";
            row118.ClassCondition = "";
            row118.Notes = "G10, G35, T1";
            row118.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 16.9, 13.9, 11.4, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row118);

            // Row 9: SA-662, B, Plate
            var row119 = new OldStressRowData();
            row119.LineNo = 9;
            row119.MaterialId = 8388;
            row119.SpecNo = "SA-662";
            row119.TypeGrade = "B";
            row119.ProductForm = "Plate";
            row119.AlloyUNS = "K02203";
            row119.ClassCondition = "";
            row119.Notes = "T1";
            row119.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row119);

            // Row 10: SA-537, , Plate
            var row120 = new OldStressRowData();
            row120.LineNo = 10;
            row120.MaterialId = 8389;
            row120.SpecNo = "SA-537";
            row120.TypeGrade = "";
            row120.ProductForm = "Plate";
            row120.AlloyUNS = "K12437";
            row120.ClassCondition = "1";
            row120.Notes = "T1";
            row120.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row120);

            // Row 11: SA-691, CMSH-70, Wld. pipe
            var row121 = new OldStressRowData();
            row121.LineNo = 11;
            row121.MaterialId = 8390;
            row121.SpecNo = "SA-691";
            row121.TypeGrade = "CMSH-70";
            row121.ProductForm = "Wld. pipe";
            row121.AlloyUNS = "K12437";
            row121.ClassCondition = "";
            row121.Notes = "G26, T1, W10, W12";
            row121.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row121);

            // Row 12: SA/EN 10028-2, P295GH, Plate
            var row122 = new OldStressRowData();
            row122.LineNo = 12;
            row122.MaterialId = 16925;
            row122.SpecNo = "SA/EN 10028-2";
            row122.TypeGrade = "P295GH";
            row122.ProductForm = "Plate";
            row122.AlloyUNS = "";
            row122.ClassCondition = "";
            row122.Notes = "G10, S1, T1";
            row122.StressValues = new double?[] { 19, 19, 19, 19, 19, 19, 19, 19, 18.5, 16.9, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row122);

            // Row 13: SA/EN 10028-2, P295GH, Plate
            var row123 = new OldStressRowData();
            row123.LineNo = 13;
            row123.MaterialId = 16926;
            row123.SpecNo = "SA/EN 10028-2";
            row123.TypeGrade = "P295GH";
            row123.ProductForm = "Plate";
            row123.AlloyUNS = "";
            row123.ClassCondition = "";
            row123.Notes = "G10, G43, S1, T1";
            row123.StressValues = new double?[] { 19, 19, 19, 19, 19, 19, 19, 19, 19, 16.9, 13.9, 11.4, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row123);

            // Row 14: ..., , 
            var row124 = new OldStressRowData();
            row124.LineNo = 14;
            row124.MaterialId = 8391;
            row124.SpecNo = "...";
            row124.TypeGrade = "";
            row124.ProductForm = "";
            row124.AlloyUNS = "";
            row124.ClassCondition = "";
            row124.Notes = "";
            row124.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row124);

            // Row 15: SA-455, , Plate
            var row125 = new OldStressRowData();
            row125.LineNo = 15;
            row125.MaterialId = 8392;
            row125.SpecNo = "SA-455";
            row125.TypeGrade = "";
            row125.ProductForm = "Plate";
            row125.AlloyUNS = "K03300";
            row125.ClassCondition = "";
            row125.Notes = "";
            row125.StressValues = new double?[] { 20, 20, 20, null, 20, 19.9, 19, 17.9, 17.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row125);

            // Row 16: SA-675, 70, Bar
            var row126 = new OldStressRowData();
            row126.LineNo = 16;
            row126.MaterialId = 8393;
            row126.SpecNo = "SA-675";
            row126.TypeGrade = "70";
            row126.ProductForm = "Bar";
            row126.AlloyUNS = "";
            row126.ClassCondition = "";
            row126.Notes = "G10, G15, G18, G22, G35, S1, T2";
            row126.StressValues = new double?[] { 20, 20, 20, null, 20, 19.9, 19, 17.9, 17.3, 16.7, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row126);

            // Row 17: SA-105, , Forgings
            var row127 = new OldStressRowData();
            row127.LineNo = 17;
            row127.MaterialId = 8394;
            row127.SpecNo = "SA-105";
            row127.TypeGrade = "";
            row127.ProductForm = "Forgings";
            row127.AlloyUNS = "K03504";
            row127.ClassCondition = "";
            row127.Notes = "G10, G18, G35, S1, T2";
            row127.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row127);

            // Row 18: SA-181, , Forgings
            var row128 = new OldStressRowData();
            row128.LineNo = 18;
            row128.MaterialId = 8395;
            row128.SpecNo = "SA-181";
            row128.TypeGrade = "";
            row128.ProductForm = "Forgings";
            row128.AlloyUNS = "K03502";
            row128.ClassCondition = "70";
            row128.Notes = "G10, G18, G35, S1, T2";
            row128.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row128);

            // Row 19: SA-216, WCB, Castings
            var row129 = new OldStressRowData();
            row129.LineNo = 19;
            row129.MaterialId = 8396;
            row129.SpecNo = "SA-216";
            row129.TypeGrade = "WCB";
            row129.ProductForm = "Castings";
            row129.AlloyUNS = "J03002";
            row129.ClassCondition = "";
            row129.Notes = "G1, G10, G17, G18, S1, T2";
            row129.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row129);

            // Row 20: SA-266, 2, Forgings
            var row130 = new OldStressRowData();
            row130.LineNo = 20;
            row130.MaterialId = 8397;
            row130.SpecNo = "SA-266";
            row130.TypeGrade = "2";
            row130.ProductForm = "Forgings";
            row130.AlloyUNS = "K03506";
            row130.ClassCondition = "";
            row130.Notes = "G10, G18, S1, T2";
            row130.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row130);

            // Row 21: SA-266, 4, Forgings
            var row131 = new OldStressRowData();
            row131.LineNo = 21;
            row131.MaterialId = 8398;
            row131.SpecNo = "SA-266";
            row131.TypeGrade = "4";
            row131.ProductForm = "Forgings";
            row131.AlloyUNS = "K03017";
            row131.ClassCondition = "";
            row131.Notes = "G10, T2";
            row131.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row131);

            // Row 22: SA-350, LF2, Forgings
            var row132 = new OldStressRowData();
            row132.LineNo = 22;
            row132.MaterialId = 8399;
            row132.SpecNo = "SA-350";
            row132.TypeGrade = "LF2";
            row132.ProductForm = "Forgings";
            row132.AlloyUNS = "K03011";
            row132.ClassCondition = "";
            row132.Notes = "G10, T2";
            row132.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row132);

            // Row 23: SA-508, 1, Forgings
            var row133 = new OldStressRowData();
            row133.LineNo = 23;
            row133.MaterialId = 8400;
            row133.SpecNo = "SA-508";
            row133.TypeGrade = "1";
            row133.ProductForm = "Forgings";
            row133.AlloyUNS = "K13502";
            row133.ClassCondition = "";
            row133.Notes = "G10, T2";
            row133.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row133);

            // Row 24: SA-508, 1A, Forgings
            var row134 = new OldStressRowData();
            row134.LineNo = 24;
            row134.MaterialId = 8401;
            row134.SpecNo = "SA-508";
            row134.TypeGrade = "1A";
            row134.ProductForm = "Forgings";
            row134.AlloyUNS = "K13502";
            row134.ClassCondition = "";
            row134.Notes = "G10, T2";
            row134.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row134);

            // Row 25: SA-541, 1, Forgings
            var row135 = new OldStressRowData();
            row135.LineNo = 25;
            row135.MaterialId = 8402;
            row135.SpecNo = "SA-541";
            row135.TypeGrade = "1";
            row135.ProductForm = "Forgings";
            row135.AlloyUNS = "K03506";
            row135.ClassCondition = "";
            row135.Notes = "G10, T2";
            row135.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row135);

            // Row 26: SA-541, 1A, Forgings
            var row136 = new OldStressRowData();
            row136.LineNo = 26;
            row136.MaterialId = 8403;
            row136.SpecNo = "SA-541";
            row136.TypeGrade = "1A";
            row136.ProductForm = "Forgings";
            row136.AlloyUNS = "K03506";
            row136.ClassCondition = "";
            row136.Notes = "G10, T2";
            row136.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row136);

            // Row 27: SA-660, WCB, Cast pipe
            var row137 = new OldStressRowData();
            row137.LineNo = 27;
            row137.MaterialId = 8404;
            row137.SpecNo = "SA-660";
            row137.TypeGrade = "WCB";
            row137.ProductForm = "Cast pipe";
            row137.AlloyUNS = "J03003";
            row137.ClassCondition = "";
            row137.Notes = "G1, G10, G17, G18, S1, T2";
            row137.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.6, 18.4, 17.8, 17.2, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row137);

            // Row 28: SA-765, II, Forgings
            var row138 = new OldStressRowData();
            row138.LineNo = 28;
            row138.MaterialId = 8405;
            row138.SpecNo = "SA-765";
            row138.TypeGrade = "II";
            row138.ProductForm = "Forgings";
            row138.AlloyUNS = "K03047";
            row138.ClassCondition = "";
            row138.Notes = "";
            row138.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 19.6, 18.4, 17.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row138);

            // Row 29: SA-515, 70, Plate
            var row139 = new OldStressRowData();
            row139.LineNo = 29;
            row139.MaterialId = 8406;
            row139.SpecNo = "SA-515";
            row139.TypeGrade = "70";
            row139.ProductForm = "Plate";
            row139.AlloyUNS = "K03101";
            row139.ClassCondition = "";
            row139.Notes = "G10, S1, T2";
            row139.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row139);

            // Row 30: SA-516, 70, Plate
            var row140 = new OldStressRowData();
            row140.LineNo = 30;
            row140.MaterialId = 8407;
            row140.SpecNo = "SA-516";
            row140.TypeGrade = "70";
            row140.ProductForm = "Plate";
            row140.AlloyUNS = "K02700";
            row140.ClassCondition = "";
            row140.Notes = "G10, S1, T2";
            row140.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row140);

            // Row 31: SA-671, CB70, Wld. pipe
            var row141 = new OldStressRowData();
            row141.LineNo = 31;
            row141.MaterialId = 8408;
            row141.SpecNo = "SA-671";
            row141.TypeGrade = "CB70";
            row141.ProductForm = "Wld. pipe";
            row141.AlloyUNS = "K03101";
            row141.ClassCondition = "";
            row141.Notes = "S5, W10, W12";
            row141.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row141);

            // Row 32: SA-671, CC70, Wld. pipe
            var row142 = new OldStressRowData();
            row142.LineNo = 32;
            row142.MaterialId = 8409;
            row142.SpecNo = "SA-671";
            row142.TypeGrade = "CC70";
            row142.ProductForm = "Wld. pipe";
            row142.AlloyUNS = "K02700";
            row142.ClassCondition = "";
            row142.Notes = "S6, W10, W12";
            row142.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row142);

            // Row 33: SA-672, B70, Wld. pipe
            var row143 = new OldStressRowData();
            row143.LineNo = 33;
            row143.MaterialId = 8410;
            row143.SpecNo = "SA-672";
            row143.TypeGrade = "B70";
            row143.ProductForm = "Wld. pipe";
            row143.AlloyUNS = "K03101";
            row143.ClassCondition = "";
            row143.Notes = "S5, W10, W12";
            row143.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row143);

            // Row 34: SA-672, C70, Wld. pipe
            var row144 = new OldStressRowData();
            row144.LineNo = 34;
            row144.MaterialId = 8411;
            row144.SpecNo = "SA-672";
            row144.TypeGrade = "C70";
            row144.ProductForm = "Wld. pipe";
            row144.AlloyUNS = "K02700";
            row144.ClassCondition = "";
            row144.Notes = "S6, W10, W12";
            row144.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 19.4, 18.8, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row144);

            // Row 1: SA-106, C, Smls. pipe
            var row145 = new OldStressRowData();
            row145.LineNo = 1;
            row145.MaterialId = 8412;
            row145.SpecNo = "SA-106";
            row145.TypeGrade = "C";
            row145.ProductForm = "Smls. pipe";
            row145.AlloyUNS = "K03501";
            row145.ClassCondition = "";
            row145.Notes = "G10, S1, T1";
            row145.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row145);

            // Row 2: SA-178, D, Wld. tube
            var row146 = new OldStressRowData();
            row146.LineNo = 2;
            row146.MaterialId = 16832;
            row146.SpecNo = "SA-178";
            row146.TypeGrade = "D";
            row146.ProductForm = "Wld. tube";
            row146.AlloyUNS = "";
            row146.ClassCondition = "";
            row146.Notes = "G10, S1, T1, W13";
            row146.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row146);

            // Row 3: SA-178, D, Wld. tube
            var row147 = new OldStressRowData();
            row147.LineNo = 3;
            row147.MaterialId = 8413;
            row147.SpecNo = "SA-178";
            row147.TypeGrade = "D";
            row147.ProductForm = "Wld. tube";
            row147.AlloyUNS = "";
            row147.ClassCondition = "";
            row147.Notes = "G4, G10, S1, T4";
            row147.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 5.7, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row147);

            // Row 4: SA-178, D, Wld. tube
            var row148 = new OldStressRowData();
            row148.LineNo = 4;
            row148.MaterialId = 8414;
            row148.SpecNo = "SA-178";
            row148.TypeGrade = "D";
            row148.ProductForm = "Wld. tube";
            row148.AlloyUNS = "";
            row148.ClassCondition = "";
            row148.Notes = "G3, G10, S1, T2";
            row148.StressValues = new double?[] { 17, null, 17, null, 17, 17, 17, 17, 16.8, 15.5, 12.6, 10.2, 7.9, 5.7, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row148);

            // Row 5: SA-210, C, Smls. tube
            var row149 = new OldStressRowData();
            row149.LineNo = 5;
            row149.MaterialId = 8415;
            row149.SpecNo = "SA-210";
            row149.TypeGrade = "C";
            row149.ProductForm = "Smls. tube";
            row149.AlloyUNS = "K03501";
            row149.ClassCondition = "";
            row149.Notes = "G10, S1, T1";
            row149.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row149);

            // Row 6: SA-216, WCC, Castings
            var row150 = new OldStressRowData();
            row150.LineNo = 6;
            row150.MaterialId = 8416;
            row150.SpecNo = "SA-216";
            row150.TypeGrade = "WCC";
            row150.ProductForm = "Castings";
            row150.AlloyUNS = "J02503";
            row150.ClassCondition = "";
            row150.Notes = "G1, G10, G17, G18, S1, T1";
            row150.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row150);

            // Row 7: SA-234, WPC, Smls. & wld. fittings
            var row151 = new OldStressRowData();
            row151.LineNo = 7;
            row151.MaterialId = 8417;
            row151.SpecNo = "SA-234";
            row151.TypeGrade = "WPC";
            row151.ProductForm = "Smls. & wld. fittings";
            row151.AlloyUNS = "K03501";
            row151.ClassCondition = "";
            row151.Notes = "G10, G18, T1";
            row151.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row151);

            // Row 8: SA-352, LCC, Castings
            var row152 = new OldStressRowData();
            row152.LineNo = 8;
            row152.MaterialId = 8419;
            row152.SpecNo = "SA-352";
            row152.TypeGrade = "LCC";
            row152.ProductForm = "Castings";
            row152.AlloyUNS = "J02505";
            row152.ClassCondition = "";
            row152.Notes = "G17, T1";
            row152.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row152);

            // Row 9: SA-487, 16, Castings
            var row153 = new OldStressRowData();
            row153.LineNo = 9;
            row153.MaterialId = 8420;
            row153.SpecNo = "SA-487";
            row153.TypeGrade = "16";
            row153.ProductForm = "Castings";
            row153.AlloyUNS = "";
            row153.ClassCondition = "A";
            row153.Notes = "";
            row153.StressValues = new double?[] { 20, null, 19.9, null, 18.8, 18.1, 17.9, 17.9, 17.9, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row153);

            // Row 10: SA-537, , Plate
            var row154 = new OldStressRowData();
            row154.LineNo = 10;
            row154.MaterialId = 9813;
            row154.SpecNo = "SA-537";
            row154.TypeGrade = "";
            row154.ProductForm = "Plate";
            row154.AlloyUNS = "K12437";
            row154.ClassCondition = "3";
            row154.Notes = "G21, G23, W11";
            row154.StressValues = new double?[] { 20, 20, 20, null, 19.7, 19.5, 18.9, 18, 17.6, 17.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row154);

            // Row 11: SA-556, C2, Smls. tube
            var row155 = new OldStressRowData();
            row155.LineNo = 11;
            row155.MaterialId = 8421;
            row155.SpecNo = "SA-556";
            row155.TypeGrade = "C2";
            row155.ProductForm = "Smls. tube";
            row155.AlloyUNS = "K03006";
            row155.ClassCondition = "";
            row155.Notes = "G10, T1";
            row155.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row155);

            // Row 12: SA-557, C2, Tube
            var row156 = new OldStressRowData();
            row156.LineNo = 12;
            row156.MaterialId = 8422;
            row156.SpecNo = "SA-557";
            row156.TypeGrade = "C2";
            row156.ProductForm = "Tube";
            row156.AlloyUNS = "K03505";
            row156.ClassCondition = "";
            row156.Notes = "G24, G35, T2, W6";
            row156.StressValues = new double?[] { 17, 17, 17, null, 17, 17, 17, 17, 16.8, 15.5, 12.6, 10.2, 7.9, 5.7, 3.4, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row156);

            // Row 13: SA-660, WCC, Cast pipe
            var row157 = new OldStressRowData();
            row157.LineNo = 13;
            row157.MaterialId = 8423;
            row157.SpecNo = "SA-660";
            row157.TypeGrade = "WCC";
            row157.ProductForm = "Cast pipe";
            row157.AlloyUNS = "J02505";
            row157.ClassCondition = "";
            row157.Notes = "G1, G10, G17, G18, S1, T1";
            row157.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row157);

            // Row 14: SA-695, B/40, Bar
            var row158 = new OldStressRowData();
            row158.LineNo = 14;
            row158.MaterialId = 8424;
            row158.SpecNo = "SA-695";
            row158.TypeGrade = "B/40";
            row158.ProductForm = "Bar";
            row158.AlloyUNS = "K03504";
            row158.ClassCondition = "";
            row158.Notes = "G10, T1";
            row158.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19.8, 18.3, 14.8, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row158);

            // Row 15: SA-696, C, Bar
            var row159 = new OldStressRowData();
            row159.LineNo = 15;
            row159.MaterialId = 8425;
            row159.SpecNo = "SA-696";
            row159.TypeGrade = "C";
            row159.ProductForm = "Bar";
            row159.AlloyUNS = "K03200";
            row159.ClassCondition = "";
            row159.Notes = "T1";
            row159.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19.8, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row159);

            // Row 16: SA-414, F, Sheet
            var row160 = new OldStressRowData();
            row160.LineNo = 16;
            row160.MaterialId = 8426;
            row160.SpecNo = "SA-414";
            row160.TypeGrade = "F";
            row160.ProductForm = "Sheet";
            row160.AlloyUNS = "K03102";
            row160.ClassCondition = "";
            row160.Notes = "G10, G35, T1";
            row160.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 18.3, 14.8, 12, 9.3, 6.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row160);

            // Row 17: SA-662, C, Plate
            var row161 = new OldStressRowData();
            row161.LineNo = 17;
            row161.MaterialId = 8427;
            row161.SpecNo = "SA-662";
            row161.TypeGrade = "C";
            row161.ProductForm = "Plate";
            row161.AlloyUNS = "K02007";
            row161.ClassCondition = "";
            row161.Notes = "T1";
            row161.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row161);

            // Row 18: SA-537, , Plate
            var row162 = new OldStressRowData();
            row162.LineNo = 18;
            row162.MaterialId = 8428;
            row162.SpecNo = "SA-537";
            row162.TypeGrade = "";
            row162.ProductForm = "Plate";
            row162.AlloyUNS = "K12437";
            row162.ClassCondition = "2";
            row162.Notes = "G21, G23, T1, W11";
            row162.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row162);

            // Row 19: SA-738, C, Plate
            var row163 = new OldStressRowData();
            row163.LineNo = 19;
            row163.MaterialId = 8430;
            row163.SpecNo = "SA-738";
            row163.TypeGrade = "C";
            row163.ProductForm = "Plate";
            row163.AlloyUNS = "";
            row163.ClassCondition = "";
            row163.Notes = "G21, G23, W11";
            row163.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row163);

            // Row 20: SA-537, , Plate
            var row164 = new OldStressRowData();
            row164.LineNo = 20;
            row164.MaterialId = 8431;
            row164.SpecNo = "SA-537";
            row164.TypeGrade = "";
            row164.ProductForm = "Plate";
            row164.AlloyUNS = "K12437";
            row164.ClassCondition = "1";
            row164.Notes = "G23, T1";
            row164.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row164);

            // Row 21: SA-671, CD70, Wld. pipe
            var row165 = new OldStressRowData();
            row165.LineNo = 21;
            row165.MaterialId = 8432;
            row165.SpecNo = "SA-671";
            row165.TypeGrade = "CD70";
            row165.ProductForm = "Wld. pipe";
            row165.AlloyUNS = "K12437";
            row165.ClassCondition = "";
            row165.Notes = "S6, T1, W10, W12";
            row165.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row165);

            // Row 22: SA-672, D70, Wld. pipe
            var row166 = new OldStressRowData();
            row166.LineNo = 22;
            row166.MaterialId = 8433;
            row166.SpecNo = "SA-672";
            row166.TypeGrade = "D70";
            row166.ProductForm = "Wld. pipe";
            row166.AlloyUNS = "K12437";
            row166.ClassCondition = "";
            row166.Notes = "S6, T1, W10, W12";
            row166.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row166);

            // Row 23: SA-691, CMSH-70, Wld. pipe
            var row167 = new OldStressRowData();
            row167.LineNo = 23;
            row167.MaterialId = 8434;
            row167.SpecNo = "SA-691";
            row167.TypeGrade = "CMSH-70";
            row167.ProductForm = "Wld. pipe";
            row167.AlloyUNS = "K12437";
            row167.ClassCondition = "";
            row167.Notes = "S6, T1, W10, W12";
            row167.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.5, 19.5, 19.5, 19.5, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row167);

            // Row 24: SA-455, , Plate
            var row168 = new OldStressRowData();
            row168.LineNo = 24;
            row168.MaterialId = 8435;
            row168.SpecNo = "SA-455";
            row168.TypeGrade = "";
            row168.ProductForm = "Plate";
            row168.AlloyUNS = "K03300";
            row168.ClassCondition = "";
            row168.Notes = "";
            row168.StressValues = new double?[] { 20.9, 20.9, 20.9, null, 20.9, 20.9, 20.1, 18.9, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row168);

            // Row 25: SA-266, 3, Forgings
            var row169 = new OldStressRowData();
            row169.LineNo = 25;
            row169.MaterialId = 8437;
            row169.SpecNo = "SA-266";
            row169.TypeGrade = "3";
            row169.ProductForm = "Forgings";
            row169.AlloyUNS = "K05001";
            row169.ClassCondition = "";
            row169.Notes = "G10, G18, S1, T2, W2, W8, W11";
            row169.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 20.4, 19.2, 18.5, 17.9, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row169);

            // Row 26: SA-455, , Plate
            var row170 = new OldStressRowData();
            row170.LineNo = 26;
            row170.MaterialId = 8438;
            row170.SpecNo = "SA-455";
            row170.TypeGrade = "";
            row170.ProductForm = "Plate";
            row170.AlloyUNS = "K03300";
            row170.ClassCondition = "";
            row170.Notes = "";
            row170.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 20.6, 19.4, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row170);

            // Row 27: SA-299, , Plate
            var row171 = new OldStressRowData();
            row171.LineNo = 27;
            row171.MaterialId = 8439;
            row171.SpecNo = "SA-299";
            row171.TypeGrade = "";
            row171.ProductForm = "Plate";
            row171.AlloyUNS = "K02803";
            row171.ClassCondition = "";
            row171.Notes = "G10, S1, T2";
            row171.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 21.4, 20.4, 19.8, 19.1, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row171);

            // Row 28: SA-671, CK75, Wld. pipe
            var row172 = new OldStressRowData();
            row172.LineNo = 28;
            row172.MaterialId = 8440;
            row172.SpecNo = "SA-671";
            row172.TypeGrade = "CK75";
            row172.ProductForm = "Wld. pipe";
            row172.AlloyUNS = "K02803";
            row172.ClassCondition = "";
            row172.Notes = "S6, W10, W12";
            row172.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 20.4, 19.8, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row172);

            // Row 29: SA-672, N75, Wld. pipe
            var row173 = new OldStressRowData();
            row173.LineNo = 29;
            row173.MaterialId = 8441;
            row173.SpecNo = "SA-672";
            row173.TypeGrade = "N75";
            row173.ProductForm = "Wld. pipe";
            row173.AlloyUNS = "K02803";
            row173.ClassCondition = "";
            row173.Notes = "S6, W10, W12";
            row173.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 20.4, 19.8, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row173);

            // Row 30: SA-691, CMS-75, Wld. pipe
            var row174 = new OldStressRowData();
            row174.LineNo = 30;
            row174.MaterialId = 8442;
            row174.SpecNo = "SA-691";
            row174.TypeGrade = "CMS-75";
            row174.ProductForm = "Wld. pipe";
            row174.AlloyUNS = "K02803";
            row174.ClassCondition = "";
            row174.Notes = "S6, W10, W12";
            row174.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 20.4, 19.8, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row174);

            // Row 31: SA-299, , Plate
            var row175 = new OldStressRowData();
            row175.LineNo = 31;
            row175.MaterialId = 8443;
            row175.SpecNo = "SA-299";
            row175.TypeGrade = "";
            row175.ProductForm = "Plate";
            row175.AlloyUNS = "K02803";
            row175.ClassCondition = "";
            row175.Notes = "G10, S1, T1";
            row175.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 21.4, 21.4, 20.8, 19.6, 15.7, 12.6, 9.3, 6.7, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row175);

            // Row 32: SA-691, CMS-75, Wld. pipe
            var row176 = new OldStressRowData();
            row176.LineNo = 32;
            row176.MaterialId = 8444;
            row176.SpecNo = "SA-691";
            row176.TypeGrade = "CMS-75";
            row176.ProductForm = "Wld. pipe";
            row176.AlloyUNS = "K02803";
            row176.ClassCondition = "";
            row176.Notes = "T1, W10, W12";
            row176.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 20.8, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row176);

            // Row 33: SA-372, B, Forgings
            var row177 = new OldStressRowData();
            row177.LineNo = 33;
            row177.MaterialId = 8445;
            row177.SpecNo = "SA-372";
            row177.TypeGrade = "B";
            row177.ProductForm = "Forgings";
            row177.AlloyUNS = "K04001";
            row177.ClassCondition = "";
            row177.Notes = "W2, W11";
            row177.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row177);

            // Row 34: SA-414, G, Sheet
            var row178 = new OldStressRowData();
            row178.LineNo = 34;
            row178.MaterialId = 8446;
            row178.SpecNo = "SA-414";
            row178.TypeGrade = "G";
            row178.ProductForm = "Sheet";
            row178.AlloyUNS = "K03103";
            row178.ClassCondition = "";
            row178.Notes = "G10, G35, T1";
            row178.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 19.6, 15.7, 12.6, 9.3, 6.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row178);

            // Row 35: SA-738, A, Plate
            var row179 = new OldStressRowData();
            row179.LineNo = 35;
            row179.MaterialId = 8447;
            row179.SpecNo = "SA-738";
            row179.TypeGrade = "A";
            row179.ProductForm = "Plate";
            row179.AlloyUNS = "K12447";
            row179.ClassCondition = "";
            row179.Notes = "T1";
            row179.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row179);

            // Row 36: SA-537, , Plate
            var row180 = new OldStressRowData();
            row180.LineNo = 36;
            row180.MaterialId = 9814;
            row180.SpecNo = "SA-537";
            row180.TypeGrade = "";
            row180.ProductForm = "Plate";
            row180.AlloyUNS = "K12437";
            row180.ClassCondition = "3";
            row180.Notes = "G23, T1, W11";
            row180.StressValues = new double?[] { 21.4, null, 21.4, null, 21.1, 20.9, 20.9, 20.9, 20.9, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row180);

            // Row 37: SA-537, , Plate
            var row181 = new OldStressRowData();
            row181.LineNo = 37;
            row181.MaterialId = 8448;
            row181.SpecNo = "SA-537";
            row181.TypeGrade = "";
            row181.ProductForm = "Plate";
            row181.AlloyUNS = "K12437";
            row181.ClassCondition = "2";
            row181.Notes = "G23, T1, W11";
            row181.StressValues = new double?[] { 21.4, null, 21.4, null, 21.1, 20.9, 20.9, 20.9, 20.9, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row181);

            // Row 38: SA-691, CMSH-80, Wld. pipe
            var row182 = new OldStressRowData();
            row182.LineNo = 38;
            row182.MaterialId = 8449;
            row182.SpecNo = "SA-691";
            row182.TypeGrade = "CMSH-80";
            row182.ProductForm = "Wld. pipe";
            row182.AlloyUNS = "K12437";
            row182.ClassCondition = "";
            row182.Notes = "G26, T1, W10, W12";
            row182.StressValues = new double?[] { 21.4, null, 21.4, null, 21.1, 20.9, 20.9, 20.9, 20.9, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row182);

            // Row 39: SA-738, C, Plate
            var row183 = new OldStressRowData();
            row183.LineNo = 39;
            row183.MaterialId = 8450;
            row183.SpecNo = "SA-738";
            row183.TypeGrade = "C";
            row183.ProductForm = "Plate";
            row183.AlloyUNS = "";
            row183.ClassCondition = "";
            row183.Notes = "G21, G23, W11";
            row183.StressValues = new double?[] { 21.4, null, 21.4, null, 21.1, 20.9, 20.9, 20.9, 20.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row183);

            // Row 1: SA-537, , Plate
            var row184 = new OldStressRowData();
            row184.LineNo = 1;
            row184.MaterialId = 9815;
            row184.SpecNo = "SA-537";
            row184.TypeGrade = "";
            row184.ProductForm = "Plate";
            row184.AlloyUNS = "K12437";
            row184.ClassCondition = "3";
            row184.Notes = "G23, T1, W11";
            row184.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row184);

            // Row 2: SA-537, , Plate
            var row185 = new OldStressRowData();
            row185.LineNo = 2;
            row185.MaterialId = 8452;
            row185.SpecNo = "SA-537";
            row185.TypeGrade = "";
            row185.ProductForm = "Plate";
            row185.AlloyUNS = "K12437";
            row185.ClassCondition = "2";
            row185.Notes = "G23, S6, T1, W10, W11, W12";
            row185.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row185);

            // Row 3: SA-671, CD80, Wld. pipe
            var row186 = new OldStressRowData();
            row186.LineNo = 3;
            row186.MaterialId = 8453;
            row186.SpecNo = "SA-671";
            row186.TypeGrade = "CD80";
            row186.ProductForm = "Wld. pipe";
            row186.AlloyUNS = "K12437";
            row186.ClassCondition = "";
            row186.Notes = "S6, T1, W10, W12";
            row186.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row186);

            // Row 4: SA-672, D80, Wld. pipe
            var row187 = new OldStressRowData();
            row187.LineNo = 4;
            row187.MaterialId = 8454;
            row187.SpecNo = "SA-672";
            row187.TypeGrade = "D80";
            row187.ProductForm = "Wld. pipe";
            row187.AlloyUNS = "K12437";
            row187.ClassCondition = "";
            row187.Notes = "S6, T1, W10, W12";
            row187.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row187);

            // Row 5: SA-691, CMSH-80, Wld. pipe
            var row188 = new OldStressRowData();
            row188.LineNo = 5;
            row188.MaterialId = 8455;
            row188.SpecNo = "SA-691";
            row188.TypeGrade = "CMSH-80";
            row188.ProductForm = "Wld. pipe";
            row188.AlloyUNS = "K12437";
            row188.ClassCondition = "";
            row188.Notes = "S6, T1, W10, W12";
            row188.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row188);

            // Row 6: SA-738, C, Plate
            var row189 = new OldStressRowData();
            row189.LineNo = 6;
            row189.MaterialId = 8456;
            row189.SpecNo = "SA-738";
            row189.TypeGrade = "C";
            row189.ProductForm = "Plate";
            row189.AlloyUNS = "";
            row189.ClassCondition = "";
            row189.Notes = "G23, W11";
            row189.StressValues = new double?[] { 22.9, null, 22.9, null, 22.6, 22.3, 22.3, 22.3, 22.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row189);

            // Row 7: SA-612, , Plate
            var row190 = new OldStressRowData();
            row190.LineNo = 7;
            row190.MaterialId = 8458;
            row190.SpecNo = "SA-612";
            row190.TypeGrade = "";
            row190.ProductForm = "Plate";
            row190.AlloyUNS = "K02900";
            row190.ClassCondition = "";
            row190.Notes = "T1";
            row190.StressValues = new double?[] { 23.1, null, 23.1, null, 22.8, 22.6, 22.6, 22.5, 22, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row190);

            // Row 8: SA-612, , Plate
            var row191 = new OldStressRowData();
            row191.LineNo = 8;
            row191.MaterialId = 8459;
            row191.SpecNo = "SA-612";
            row191.TypeGrade = "";
            row191.ProductForm = "Plate";
            row191.AlloyUNS = "K02900";
            row191.ClassCondition = "";
            row191.Notes = "";
            row191.StressValues = new double?[] { 23.7, null, 23.7, null, 23.4, 23.2, 23.2, 22.5, 22, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row191);

            // Row 9: SA-612, , Plate
            var row192 = new OldStressRowData();
            row192.LineNo = 9;
            row192.MaterialId = 8457;
            row192.SpecNo = "SA-612";
            row192.TypeGrade = "";
            row192.ProductForm = "Plate";
            row192.AlloyUNS = "K02900";
            row192.ClassCondition = "";
            row192.Notes = "";
            row192.StressValues = new double?[] { 23.1, null, 23.1, null, 22.8, 22.6, 22.6, 22.5, 22, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row192);

            // Row 10: SA-738, B, Plate
            var row193 = new OldStressRowData();
            row193.LineNo = 10;
            row193.MaterialId = 8460;
            row193.SpecNo = "SA-738";
            row193.TypeGrade = "B";
            row193.ProductForm = "Plate";
            row193.AlloyUNS = "K12447";
            row193.ClassCondition = "";
            row193.Notes = "G20";
            row193.StressValues = new double?[] { 24.3, 24.3, 24.3, null, 24, 23.7, 23.7, 23.7, 23.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row193);

            // Row 11: SA-372, C, Forgings
            var row194 = new OldStressRowData();
            row194.LineNo = 11;
            row194.MaterialId = 8461;
            row194.SpecNo = "SA-372";
            row194.TypeGrade = "C";
            row194.ProductForm = "Forgings";
            row194.AlloyUNS = "K04801";
            row194.ClassCondition = "";
            row194.Notes = "W2, W11";
            row194.StressValues = new double?[] { 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, 24.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row194);

            // Row 12: SA-724, A, Plate
            var row195 = new OldStressRowData();
            row195.LineNo = 12;
            row195.MaterialId = 8462;
            row195.SpecNo = "SA-724";
            row195.TypeGrade = "A";
            row195.ProductForm = "Plate";
            row195.AlloyUNS = "K11831";
            row195.ClassCondition = "";
            row195.Notes = "";
            row195.StressValues = new double?[] { 25.7, 25.7, 25.7, 25.6, 25.4, 25.1, 25.1, 25.1, 24.4, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row195);

            // Row 13: SA-724, C, Plate
            var row196 = new OldStressRowData();
            row196.LineNo = 13;
            row196.MaterialId = 8463;
            row196.SpecNo = "SA-724";
            row196.TypeGrade = "C";
            row196.ProductForm = "Plate";
            row196.AlloyUNS = "K12037";
            row196.ClassCondition = "";
            row196.Notes = "";
            row196.StressValues = new double?[] { 25.7, 25.7, 25.7, 25.6, 25.4, 25.1, 25.1, 25.1, 24.4, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row196);

            // Row 14: SA-724, B, Plate
            var row197 = new OldStressRowData();
            row197.LineNo = 14;
            row197.MaterialId = 8464;
            row197.SpecNo = "SA-724";
            row197.TypeGrade = "B";
            row197.ProductForm = "Plate";
            row197.AlloyUNS = "K12031";
            row197.ClassCondition = "";
            row197.Notes = "";
            row197.StressValues = new double?[] { 27.1, 27.1, 27.1, 27, 26.8, 26.5, 26.5, 26.5, 24.4, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row197);

            // Row 15: SA-812, 65, Sheet
            var row198 = new OldStressRowData();
            row198.LineNo = 15;
            row198.MaterialId = 8465;
            row198.SpecNo = "SA-812";
            row198.TypeGrade = "65";
            row198.ProductForm = "Sheet";
            row198.AlloyUNS = "";
            row198.ClassCondition = "";
            row198.Notes = "";
            row198.StressValues = new double?[] { 24.3, 24.3, 24.3, null, 24.3, 24.3, 24.3, 24.3, 24.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row198);

            // Row 16: SA-737, B, Plate
            var row199 = new OldStressRowData();
            row199.LineNo = 16;
            row199.MaterialId = 8466;
            row199.SpecNo = "SA-737";
            row199.TypeGrade = "B";
            row199.ProductForm = "Plate";
            row199.AlloyUNS = "K12001";
            row199.ClassCondition = "";
            row199.Notes = "G19, T1";
            row199.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 19.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row199);

            // Row 17: SA-812, 80, Sheet
            var row200 = new OldStressRowData();
            row200.LineNo = 17;
            row200.MaterialId = 8467;
            row200.SpecNo = "SA-812";
            row200.TypeGrade = "80";
            row200.ProductForm = "Sheet";
            row200.AlloyUNS = "";
            row200.ClassCondition = "";
            row200.Notes = "";
            row200.StressValues = new double?[] { 28.6, null, 28.5, null, 28.1, 28.1, 28.1, 28.1, 24.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
