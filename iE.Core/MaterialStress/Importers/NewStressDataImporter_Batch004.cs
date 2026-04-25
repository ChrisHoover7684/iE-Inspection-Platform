using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch004
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch004(MaterialStressService service)
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

            // Row 3: SA-369, FP22, Forged pipe
            var row301 = new OldStressRowData();
            row301.LineNo = 3;
            row301.MaterialId = 8593;
            row301.SpecNo = "SA-369";
            row301.TypeGrade = "FP22";
            row301.ProductForm = "Forged pipe";
            row301.AlloyUNS = "K21590";
            row301.ClassCondition = "";
            row301.Notes = "S4, T4, W7, W9";
            row301.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row301);

            // Row 4: SA-387, 22, Plate
            var row302 = new OldStressRowData();
            row302.LineNo = 4;
            row302.MaterialId = 8594;
            row302.SpecNo = "SA-387";
            row302.TypeGrade = "22";
            row302.ProductForm = "Plate";
            row302.AlloyUNS = "K21590";
            row302.ClassCondition = "1";
            row302.Notes = "S4, T4, W7, W9";
            row302.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row302);

            // Row 5: SA-691, 2 1/4 CR, Wld. pipe
            var row303 = new OldStressRowData();
            row303.LineNo = 5;
            row303.MaterialId = 8595;
            row303.SpecNo = "SA-691";
            row303.TypeGrade = "2 1/4 CR";
            row303.ProductForm = "Wld. pipe";
            row303.AlloyUNS = "K21590";
            row303.ClassCondition = "";
            row303.Notes = "G26, W10, W12";
            row303.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row303);

            // Row 6: SA-217, WC9, Castings
            var row304 = new OldStressRowData();
            row304.LineNo = 6;
            row304.MaterialId = 8596;
            row304.SpecNo = "SA-217";
            row304.TypeGrade = "WC9";
            row304.ProductForm = "Castings";
            row304.AlloyUNS = "J21890";
            row304.ClassCondition = "";
            row304.Notes = "G1, G17, G18, S4, T4, W7, W9";
            row304.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.4, 19.3, 19.2, 19.1, 18.8, 18.5, 17.9, 17.2, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row304);

            // Row 7: SA-426, CP22, Cast pipe
            var row305 = new OldStressRowData();
            row305.LineNo = 7;
            row305.MaterialId = 8597;
            row305.SpecNo = "SA-426";
            row305.TypeGrade = "CP22";
            row305.ProductForm = "Cast pipe";
            row305.AlloyUNS = "J21890";
            row305.ClassCondition = "";
            row305.Notes = "G17";
            row305.StressValues = new double?[] { 20, null, 20, null, 19.7, 19.4, 19.3, 19.2, 19.1, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row305);

            // Row 8: SA-182, F22, Forgings
            var row306 = new OldStressRowData();
            row306.LineNo = 8;
            row306.MaterialId = 8598;
            row306.SpecNo = "SA-182";
            row306.TypeGrade = "F22";
            row306.ProductForm = "Forgings";
            row306.AlloyUNS = "K21590";
            row306.ClassCondition = "3";
            row306.Notes = "G18, S4, T4, W7, W9";
            row306.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.7, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row306);

            // Row 9: SA-336, F22, Forgings
            var row307 = new OldStressRowData();
            row307.LineNo = 9;
            row307.MaterialId = 8599;
            row307.SpecNo = "SA-336";
            row307.TypeGrade = "F22";
            row307.ProductForm = "Forgings";
            row307.AlloyUNS = "K21590";
            row307.ClassCondition = "3";
            row307.Notes = "G18, S4, T4, W7, W9";
            row307.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.7, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row307);

            // Row 10: SA-387, 22, Plate
            var row308 = new OldStressRowData();
            row308.LineNo = 10;
            row308.MaterialId = 8600;
            row308.SpecNo = "SA-387";
            row308.TypeGrade = "22";
            row308.ProductForm = "Plate";
            row308.AlloyUNS = "K21590";
            row308.ClassCondition = "2";
            row308.Notes = "S4, T4, W7, W9";
            row308.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.7, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row308);

            // Row 11: SA-691, 2 1/4 CR, Wld. pipe
            var row309 = new OldStressRowData();
            row309.LineNo = 11;
            row309.MaterialId = 8601;
            row309.SpecNo = "SA-691";
            row309.TypeGrade = "2 1/4 CR";
            row309.ProductForm = "Wld. pipe";
            row309.AlloyUNS = "K21590";
            row309.ClassCondition = "";
            row309.Notes = "G26, W10, W12";
            row309.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row309);

            // Row 12: SA-739, B22, Bar
            var row310 = new OldStressRowData();
            row310.LineNo = 12;
            row310.MaterialId = 8602;
            row310.SpecNo = "SA-739";
            row310.TypeGrade = "B22";
            row310.ProductForm = "Bar";
            row310.AlloyUNS = "K21390";
            row310.ClassCondition = "";
            row310.Notes = "T4, W7";
            row310.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.7, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row310);

            // Row 13: SA-487, 8, Castings
            var row311 = new OldStressRowData();
            row311.LineNo = 13;
            row311.MaterialId = 8603;
            row311.SpecNo = "SA-487";
            row311.TypeGrade = "8";
            row311.ProductForm = "Castings";
            row311.AlloyUNS = "J22091";
            row311.ClassCondition = "A";
            row311.Notes = "G1, T4, W7";
            row311.StressValues = new double?[] { 24.3, 24.3, 24.3, null, 23.7, 23.5, 23.5, 23.3, 23.2, 22.9, 22.4, 21.7, 20.8, 15.8, 11.4, 7.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row311);

            // Row 14: SA-508, 22, Forgings
            var row312 = new OldStressRowData();
            row312.LineNo = 14;
            row312.MaterialId = 16835;
            row312.SpecNo = "SA-508";
            row312.TypeGrade = "22";
            row312.ProductForm = "Forgings";
            row312.AlloyUNS = "K21590";
            row312.ClassCondition = "3";
            row312.Notes = "G39";
            row312.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 23.8, 23.3, 23.1, 22.9, 22.6, 21.9, 20.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row312);

            // Row 15: SA-541, 22, Forgings
            var row313 = new OldStressRowData();
            row313.LineNo = 15;
            row313.MaterialId = 16836;
            row313.SpecNo = "SA-541";
            row313.TypeGrade = "22";
            row313.ProductForm = "Forgings";
            row313.AlloyUNS = "K21390";
            row313.ClassCondition = "3";
            row313.Notes = "G39";
            row313.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 23.8, 23.3, 23.1, 22.9, 22.6, 21.9, 20.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row313);

            // Row 16: SA-542, B, Plate
            var row314 = new OldStressRowData();
            row314.LineNo = 16;
            row314.MaterialId = 16837;
            row314.SpecNo = "SA-542";
            row314.TypeGrade = "B";
            row314.ProductForm = "Plate";
            row314.AlloyUNS = "K21590";
            row314.ClassCondition = "4";
            row314.Notes = "G39";
            row314.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 23.8, 23.3, 23.1, 22.9, 22.6, 21.9, 20.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row314);

            // Row 17: SA-182, F22V, Forgings
            var row315 = new OldStressRowData();
            row315.LineNo = 17;
            row315.MaterialId = 16838;
            row315.SpecNo = "SA-182";
            row315.TypeGrade = "F22V";
            row315.ProductForm = "Forgings";
            row315.AlloyUNS = "K31835";
            row315.ClassCondition = "";
            row315.Notes = "G39";
            row315.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 24.3, 23.7, 23.2, 22.8, 22.2, 21.6, 21, 20.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row315);

            // Row 18: SA-336, F22V, Forgings
            var row316 = new OldStressRowData();
            row316.LineNo = 18;
            row316.MaterialId = 16839;
            row316.SpecNo = "SA-336";
            row316.TypeGrade = "F22V";
            row316.ProductForm = "Forgings";
            row316.AlloyUNS = "K31835";
            row316.ClassCondition = "";
            row316.Notes = "G39";
            row316.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 24.3, 23.7, 23.2, 22.8, 22.2, 21.6, 21, 20.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row316);

            // Row 19: SA-541, 22V, Forgings
            var row317 = new OldStressRowData();
            row317.LineNo = 19;
            row317.MaterialId = 16840;
            row317.SpecNo = "SA-541";
            row317.TypeGrade = "22V";
            row317.ProductForm = "Forgings";
            row317.AlloyUNS = "K31835";
            row317.ClassCondition = "";
            row317.Notes = "G39";
            row317.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 24.3, 23.7, 23.2, 22.8, 22.2, 21.6, 21, 20.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row317);

            // Row 20: SA-542, D, Plate
            var row318 = new OldStressRowData();
            row318.LineNo = 20;
            row318.MaterialId = 16841;
            row318.SpecNo = "SA-542";
            row318.TypeGrade = "D";
            row318.ProductForm = "Plate";
            row318.AlloyUNS = "K31835";
            row318.ClassCondition = "4a";
            row318.Notes = "G39";
            row318.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 24.3, 23.7, 23.2, 22.8, 22.2, 21.6, 21, 20.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row318);

            // Row 21: SA-832, 22V, Plate
            var row319 = new OldStressRowData();
            row319.LineNo = 21;
            row319.MaterialId = 16842;
            row319.SpecNo = "SA-832";
            row319.TypeGrade = "22V";
            row319.ProductForm = "Plate";
            row319.AlloyUNS = "K31835";
            row319.ClassCondition = "";
            row319.Notes = "G39";
            row319.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.3, 24.3, 23.7, 23.2, 22.8, 22.2, 21.6, 21, 20.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row319);

            // Row 22: SA-199, T21, Smls. tube
            var row320 = new OldStressRowData();
            row320.LineNo = 22;
            row320.MaterialId = 16936;
            row320.SpecNo = "SA-199";
            row320.TypeGrade = "T21";
            row320.ProductForm = "Smls. tube";
            row320.AlloyUNS = "K31545";
            row320.ClassCondition = "";
            row320.Notes = "T4";
            row320.StressValues = new double?[] { 16.7, 15.9, 15.6, null, 15.1, 15, 15, 15, 15, 15, 14.9, 14.8, 14.5, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row320);

            // Row 23: SA-213, T21, Smls. tube
            var row321 = new OldStressRowData();
            row321.LineNo = 23;
            row321.MaterialId = 16937;
            row321.SpecNo = "SA-213";
            row321.TypeGrade = "T21";
            row321.ProductForm = "Smls. tube";
            row321.AlloyUNS = "K31545";
            row321.ClassCondition = "";
            row321.Notes = "S4, T3";
            row321.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row321);

            // Row 24: SA-335, P21, Smls. pipe
            var row322 = new OldStressRowData();
            row322.LineNo = 24;
            row322.MaterialId = 16938;
            row322.SpecNo = "SA-335";
            row322.TypeGrade = "P21";
            row322.ProductForm = "Smls. pipe";
            row322.AlloyUNS = "K31545";
            row322.ClassCondition = "";
            row322.Notes = "S4, T3";
            row322.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row322);

            // Row 25: SA-336, F21, Forgings
            var row323 = new OldStressRowData();
            row323.LineNo = 25;
            row323.MaterialId = 16939;
            row323.SpecNo = "SA-336";
            row323.TypeGrade = "F21";
            row323.ProductForm = "Forgings";
            row323.AlloyUNS = "K31545";
            row323.ClassCondition = "1";
            row323.Notes = "G18, S4, T3";
            row323.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row323);

            // Row 26: SA-369, FP21, Forged pipe
            var row324 = new OldStressRowData();
            row324.LineNo = 26;
            row324.MaterialId = 16940;
            row324.SpecNo = "SA-369";
            row324.TypeGrade = "FP21";
            row324.ProductForm = "Forged pipe";
            row324.AlloyUNS = "K31545";
            row324.ClassCondition = "";
            row324.Notes = "S4, T3";
            row324.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row324);

            // Row 27: SA-387, 21, Plate
            var row325 = new OldStressRowData();
            row325.LineNo = 27;
            row325.MaterialId = 16941;
            row325.SpecNo = "SA-387";
            row325.TypeGrade = "21";
            row325.ProductForm = "Plate";
            row325.AlloyUNS = "K31545";
            row325.ClassCondition = "1";
            row325.Notes = "S4, T3";
            row325.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row325);

            // Row 28: SA-426, CP21, Cast pipe
            var row326 = new OldStressRowData();
            row326.LineNo = 28;
            row326.MaterialId = 16942;
            row326.SpecNo = "SA-426";
            row326.TypeGrade = "CP21";
            row326.ProductForm = "Cast pipe";
            row326.AlloyUNS = "J31545";
            row326.ClassCondition = "";
            row326.Notes = "G17";
            row326.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row326);

            // Row 29: SA-182, F21, Forgings
            var row327 = new OldStressRowData();
            row327.LineNo = 29;
            row327.MaterialId = 16943;
            row327.SpecNo = "SA-182";
            row327.TypeGrade = "F21";
            row327.ProductForm = "Forgings";
            row327.AlloyUNS = "K31545";
            row327.ClassCondition = "";
            row327.Notes = "G18, S4, T3";
            row327.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.1, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row327);

            // Row 30: SA-336, F21, Forgings
            var row328 = new OldStressRowData();
            row328.LineNo = 30;
            row328.MaterialId = 16944;
            row328.SpecNo = "SA-336";
            row328.TypeGrade = "F21";
            row328.ProductForm = "Forgings";
            row328.AlloyUNS = "K31545";
            row328.ClassCondition = "3";
            row328.Notes = "G18, S4, T3";
            row328.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.1, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row328);

            // Row 31: SA-387, 21, Plate
            var row329 = new OldStressRowData();
            row329.LineNo = 31;
            row329.MaterialId = 16945;
            row329.SpecNo = "SA-387";
            row329.TypeGrade = "21";
            row329.ProductForm = "Plate";
            row329.AlloyUNS = "K31545";
            row329.ClassCondition = "2";
            row329.Notes = "S4, T3";
            row329.StressValues = new double?[] { 21.4, null, 21.4, null, 20.9, 20.6, 20.5, 20.4, 20.2, 20, 19.7, 19.3, 18.1, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row329);

            // Row 32: SA-182, F3V, Forgings
            var row330 = new OldStressRowData();
            row330.LineNo = 32;
            row330.MaterialId = 16946;
            row330.SpecNo = "SA-182";
            row330.TypeGrade = "F3V";
            row330.ProductForm = "Forgings";
            row330.AlloyUNS = "K31830";
            row330.ClassCondition = "";
            row330.Notes = "G39";
            row330.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row330);

            // Row 33: SA-336, F3V, Forgings
            var row331 = new OldStressRowData();
            row331.LineNo = 33;
            row331.MaterialId = 16947;
            row331.SpecNo = "SA-336";
            row331.TypeGrade = "F3V";
            row331.ProductForm = "Forgings";
            row331.AlloyUNS = "K31830";
            row331.ClassCondition = "";
            row331.Notes = "G39";
            row331.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row331);

            // Row 34: SA-508, 3V, Forgings
            var row332 = new OldStressRowData();
            row332.LineNo = 34;
            row332.MaterialId = 16948;
            row332.SpecNo = "SA-508";
            row332.TypeGrade = "3V";
            row332.ProductForm = "Forgings";
            row332.AlloyUNS = "K31830";
            row332.ClassCondition = "";
            row332.Notes = "G39";
            row332.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row332);

            // Row 35: SA-541, 3V, Forgings
            var row333 = new OldStressRowData();
            row333.LineNo = 35;
            row333.MaterialId = 16949;
            row333.SpecNo = "SA-541";
            row333.TypeGrade = "3V";
            row333.ProductForm = "Forgings";
            row333.AlloyUNS = "K31830";
            row333.ClassCondition = "";
            row333.Notes = "G39";
            row333.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row333);

            // Row 36: SA-542, C, Plate
            var row334 = new OldStressRowData();
            row334.LineNo = 36;
            row334.MaterialId = 16950;
            row334.SpecNo = "SA-542";
            row334.TypeGrade = "C";
            row334.ProductForm = "Plate";
            row334.AlloyUNS = "K31830";
            row334.ClassCondition = "4a";
            row334.Notes = "G39";
            row334.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row334);

            // Row 37: SA-832, 21V, Plate
            var row335 = new OldStressRowData();
            row335.LineNo = 37;
            row335.MaterialId = 16951;
            row335.SpecNo = "SA-832";
            row335.TypeGrade = "21V";
            row335.ProductForm = "Plate";
            row335.AlloyUNS = "K31830";
            row335.ClassCondition = "";
            row335.Notes = "G39";
            row335.StressValues = new double?[] { 24.3, null, 24.3, null, 23.3, 22.6, 22.2, 21.8, 21.6, 21.4, 21.1, 20.8, 20.4, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row335);

            // Row 1: SA-199, T5, Smls. tube
            var row336 = new OldStressRowData();
            row336.LineNo = 1;
            row336.MaterialId = 16952;
            row336.SpecNo = "SA-199";
            row336.TypeGrade = "T5";
            row336.ProductForm = "Smls. tube";
            row336.AlloyUNS = "K41545";
            row336.ClassCondition = "";
            row336.Notes = "T4";
            row336.StressValues = new double?[] { 16.7, null, 15.1, null, 14.5, 14.3, 14.2, 14, 13.8, 13.6, 13.3, 12.8, 12.3, 10.9, 8, 5.8, 4.2, 2.9, 2, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row336);

            // Row 2: SA-213, T5, Smls. tube
            var row337 = new OldStressRowData();
            row337.LineNo = 2;
            row337.MaterialId = 16953;
            row337.SpecNo = "SA-213";
            row337.TypeGrade = "T5";
            row337.ProductForm = "Smls. tube";
            row337.AlloyUNS = "K41545";
            row337.ClassCondition = "";
            row337.Notes = "T4";
            row337.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row337);

            // Row 3: SA-234, WP5, Smls. & wld. fittings
            var row338 = new OldStressRowData();
            row338.LineNo = 3;
            row338.MaterialId = 16954;
            row338.SpecNo = "SA-234";
            row338.TypeGrade = "WP5";
            row338.ProductForm = "Smls. & wld. fittings";
            row338.AlloyUNS = "K41545";
            row338.ClassCondition = "";
            row338.Notes = "T4";
            row338.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row338);

            // Row 4: SA-335, P5, Smls. pipe
            var row339 = new OldStressRowData();
            row339.LineNo = 4;
            row339.MaterialId = 16955;
            row339.SpecNo = "SA-335";
            row339.TypeGrade = "P5";
            row339.ProductForm = "Smls. pipe";
            row339.AlloyUNS = "K41545";
            row339.ClassCondition = "";
            row339.Notes = "T4";
            row339.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row339);

            // Row 5: SA-369, FP5, Forged pipe
            var row340 = new OldStressRowData();
            row340.LineNo = 5;
            row340.MaterialId = 16956;
            row340.SpecNo = "SA-369";
            row340.TypeGrade = "FP5";
            row340.ProductForm = "Forged pipe";
            row340.AlloyUNS = "K41545";
            row340.ClassCondition = "";
            row340.Notes = "G18, T4";
            row340.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row340);

            // Row 6: SA-387, 5, Plate
            var row341 = new OldStressRowData();
            row341.LineNo = 6;
            row341.MaterialId = 16957;
            row341.SpecNo = "SA-387";
            row341.TypeGrade = "5";
            row341.ProductForm = "Plate";
            row341.AlloyUNS = "K41545";
            row341.ClassCondition = "1";
            row341.Notes = "T4";
            row341.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row341);

            // Row 7: SA-691, 5CR, Wld. pipe
            var row342 = new OldStressRowData();
            row342.LineNo = 7;
            row342.MaterialId = 16958;
            row342.SpecNo = "SA-691";
            row342.TypeGrade = "5CR";
            row342.ProductForm = "Wld. pipe";
            row342.AlloyUNS = "K41545";
            row342.ClassCondition = "";
            row342.Notes = "G26, W10, W12";
            row342.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row342);

            // Row 8: SA-336, F5, Forgings
            var row343 = new OldStressRowData();
            row343.LineNo = 8;
            row343.MaterialId = 16959;
            row343.SpecNo = "SA-336";
            row343.TypeGrade = "F5";
            row343.ProductForm = "Forgings";
            row343.AlloyUNS = "K41545";
            row343.ClassCondition = "";
            row343.Notes = "G18, T4";
            row343.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row343);

            // Row 9: SA-182, F5, Forgings
            var row344 = new OldStressRowData();
            row344.LineNo = 9;
            row344.MaterialId = 16960;
            row344.SpecNo = "SA-182";
            row344.TypeGrade = "F5";
            row344.ProductForm = "Forgings";
            row344.AlloyUNS = "K41545";
            row344.ClassCondition = "";
            row344.Notes = "G18, T3";
            row344.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 19.2, 18.9, 18.6, 18.2, 17.6, 17, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row344);

            // Row 10: SA-387, 5, Plate
            var row345 = new OldStressRowData();
            row345.LineNo = 10;
            row345.MaterialId = 16961;
            row345.SpecNo = "SA-387";
            row345.TypeGrade = "5";
            row345.ProductForm = "Plate";
            row345.AlloyUNS = "K41545";
            row345.ClassCondition = "2";
            row345.Notes = "T3";
            row345.StressValues = new double?[] { 21.4, null, 21.4, null, 20.8, 20.6, 20.5, 20.2, 19.9, 19.5, 18.9, 18.2, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row345);

            // Row 11: SA-336, F5A, Forgings
            var row346 = new OldStressRowData();
            row346.LineNo = 11;
            row346.MaterialId = 16962;
            row346.SpecNo = "SA-336";
            row346.TypeGrade = "F5A";
            row346.ProductForm = "Forgings";
            row346.AlloyUNS = "K42544";
            row346.ClassCondition = "";
            row346.Notes = "G18, T3";
            row346.StressValues = new double?[] { 22.9, null, 22.8, null, 22.1, 22, 21.9, 21.6, 21.3, 20.8, 20.2, 19.1, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row346);

            // Row 12: SA-217, C5, Castings
            var row347 = new OldStressRowData();
            row347.LineNo = 12;
            row347.MaterialId = 16963;
            row347.SpecNo = "SA-217";
            row347.TypeGrade = "C5";
            row347.ProductForm = "Castings";
            row347.AlloyUNS = "J42045";
            row347.ClassCondition = "";
            row347.Notes = "G1, G17, G18, T3";
            row347.StressValues = new double?[] { 25.7, null, 25.7, null, 24.9, 24.7, 24.6, 24.3, 23.9, 23.4, 22.7, 19.1, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row347);

            // Row 13: SA-426, CP5, Cast pipe
            var row348 = new OldStressRowData();
            row348.LineNo = 13;
            row348.MaterialId = 16964;
            row348.SpecNo = "SA-426";
            row348.TypeGrade = "CP5";
            row348.ProductForm = "Cast pipe";
            row348.AlloyUNS = "J42045";
            row348.ClassCondition = "";
            row348.Notes = "G17";
            row348.StressValues = new double?[] { 25.7, null, 25.7, null, 24.9, 24.7, 24.6, 24.3, 23.9, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row348);

            // Row 14: SA-182, F5a, Forgings
            var row349 = new OldStressRowData();
            row349.LineNo = 14;
            row349.MaterialId = 16965;
            row349.SpecNo = "SA-182";
            row349.TypeGrade = "F5a";
            row349.ProductForm = "Forgings";
            row349.AlloyUNS = "K42544";
            row349.ClassCondition = "";
            row349.Notes = "G18, T3";
            row349.StressValues = new double?[] { 25.7, null, 25.7, null, 24.9, 24.7, 24.6, 24.3, 23.9, 23.4, 22.7, 19.1, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row349);

            // Row 15: SA-213, T5b, Smls. tube
            var row350 = new OldStressRowData();
            row350.LineNo = 15;
            row350.MaterialId = 8644;
            row350.SpecNo = "SA-213";
            row350.TypeGrade = "T5b";
            row350.ProductForm = "Smls. tube";
            row350.AlloyUNS = "K51545";
            row350.ClassCondition = "";
            row350.Notes = "T4";
            row350.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row350);

            // Row 16: SA-335, P5b, Smls. pipe
            var row351 = new OldStressRowData();
            row351.LineNo = 16;
            row351.MaterialId = 8645;
            row351.SpecNo = "SA-335";
            row351.TypeGrade = "P5b";
            row351.ProductForm = "Smls. pipe";
            row351.AlloyUNS = "K51545";
            row351.ClassCondition = "";
            row351.Notes = "T4";
            row351.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row351);

            // Row 17: SA-213, T5c, Smls. tube
            var row352 = new OldStressRowData();
            row352.LineNo = 17;
            row352.MaterialId = 8646;
            row352.SpecNo = "SA-213";
            row352.TypeGrade = "T5c";
            row352.ProductForm = "Smls. tube";
            row352.AlloyUNS = "K41245";
            row352.ClassCondition = "";
            row352.Notes = "T4";
            row352.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row352);

            // Row 18: SA-335, P5c, Smls. pipe
            var row353 = new OldStressRowData();
            row353.LineNo = 18;
            row353.MaterialId = 8647;
            row353.SpecNo = "SA-335";
            row353.TypeGrade = "P5c";
            row353.ProductForm = "Smls. pipe";
            row353.AlloyUNS = "K41245";
            row353.ClassCondition = "";
            row353.Notes = "T4";
            row353.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row353);

            // Row 19: SA-199, T9, Smls. tube
            var row354 = new OldStressRowData();
            row354.LineNo = 19;
            row354.MaterialId = 8648;
            row354.SpecNo = "SA-199";
            row354.TypeGrade = "T9";
            row354.ProductForm = "Smls. tube";
            row354.AlloyUNS = "K81590";
            row354.ClassCondition = "";
            row354.Notes = "T5";
            row354.StressValues = new double?[] { 16.7, null, 15.1, null, 14.5, 14.3, 14.2, 14, 13.8, 13.6, 13.3, 12.8, 12.3, 11.7, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row354);

            // Row 20: SA-213, T9, Smls. tube
            var row355 = new OldStressRowData();
            row355.LineNo = 20;
            row355.MaterialId = 8649;
            row355.SpecNo = "SA-213";
            row355.TypeGrade = "T9";
            row355.ProductForm = "Smls. tube";
            row355.AlloyUNS = "K81590";
            row355.ClassCondition = "";
            row355.Notes = "T5";
            row355.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 13, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row355);

            // Row 21: SA-234, WP9, Fittings
            var row356 = new OldStressRowData();
            row356.LineNo = 21;
            row356.MaterialId = 8650;
            row356.SpecNo = "SA-234";
            row356.TypeGrade = "WP9";
            row356.ProductForm = "Fittings";
            row356.AlloyUNS = "K90941";
            row356.ClassCondition = "";
            row356.Notes = "G18, T5";
            row356.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 13, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row356);

            // Row 22: SA-335, P9, Smls. pipe
            var row357 = new OldStressRowData();
            row357.LineNo = 22;
            row357.MaterialId = 8651;
            row357.SpecNo = "SA-335";
            row357.TypeGrade = "P9";
            row357.ProductForm = "Smls. pipe";
            row357.AlloyUNS = "K81590";
            row357.ClassCondition = "";
            row357.Notes = "T5";
            row357.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 13, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row357);

            // Row 23: SA-369, FP9, Forged pipe
            var row358 = new OldStressRowData();
            row358.LineNo = 23;
            row358.MaterialId = 8652;
            row358.SpecNo = "SA-369";
            row358.TypeGrade = "FP9";
            row358.ProductForm = "Forged pipe";
            row358.AlloyUNS = "K90941";
            row358.ClassCondition = "";
            row358.Notes = "T5";
            row358.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.5, 16.4, 16.2, 15.9, 15.6, 15.1, 14.5, 13.8, 13, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row358);

            // Row 24: SA-182, F9, Forgings
            var row359 = new OldStressRowData();
            row359.LineNo = 24;
            row359.MaterialId = 8654;
            row359.SpecNo = "SA-182";
            row359.TypeGrade = "F9";
            row359.ProductForm = "Forgings";
            row359.AlloyUNS = "K90941";
            row359.ClassCondition = "";
            row359.Notes = "G18, T4";
            row359.StressValues = new double?[] { 24.3, null, 24.2, null, 23.5, 23.4, 23.3, 22.9, 22.6, 22.1, 21.4, 20.6, 19.6, 16.4, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row359);

            // Row 25: SA-336, F9, Forgings
            var row360 = new OldStressRowData();
            row360.LineNo = 25;
            row360.MaterialId = 8655;
            row360.SpecNo = "SA-336";
            row360.TypeGrade = "F9";
            row360.ProductForm = "Forgings";
            row360.AlloyUNS = "K81590";
            row360.ClassCondition = "";
            row360.Notes = "T4";
            row360.StressValues = new double?[] { 24.3, null, 24.2, null, 23.5, 23.4, 23.3, 22.9, 22.6, 22.1, 21.4, 20.6, 19.6, 16.4, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row360);

            // Row 26: SA-217, C12, Castings
            var row361 = new OldStressRowData();
            row361.LineNo = 26;
            row361.MaterialId = 8656;
            row361.SpecNo = "SA-217";
            row361.TypeGrade = "C12";
            row361.ProductForm = "Castings";
            row361.AlloyUNS = "J82090";
            row361.ClassCondition = "";
            row361.Notes = "G1, G18, T4";
            row361.StressValues = new double?[] { 25.7, null, 25.7, null, 24.9, 24.7, 24.6, 24.3, 23.9, 23.4, 22.7, 21.8, 20.8, 16.4, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row361);

            // Row 27: SA-426, CP9, Cast pipe
            var row362 = new OldStressRowData();
            row362.LineNo = 27;
            row362.MaterialId = 8658;
            row362.SpecNo = "SA-426";
            row362.TypeGrade = "CP9";
            row362.ProductForm = "Cast pipe";
            row362.AlloyUNS = "J82090";
            row362.ClassCondition = "";
            row362.Notes = "G17";
            row362.StressValues = new double?[] { 25.7, null, 25.7, null, 24.9, 24.7, 24.6, 24.3, 23.9, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row362);

            // Row 28: SA-182, F91, Forgings
            var row363 = new OldStressRowData();
            row363.LineNo = 28;
            row363.MaterialId = 8659;
            row363.SpecNo = "SA-182";
            row363.TypeGrade = "F91";
            row363.ProductForm = "Forgings";
            row363.AlloyUNS = "K90901";
            row363.ClassCondition = "";
            row363.Notes = "G18, H3, T7";
            row363.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row363);

            // Row 29: SA-182, F91, Forgings
            var row364 = new OldStressRowData();
            row364.LineNo = 29;
            row364.MaterialId = 8660;
            row364.SpecNo = "SA-182";
            row364.TypeGrade = "F91";
            row364.ProductForm = "Forgings";
            row364.AlloyUNS = "K90901";
            row364.ClassCondition = "";
            row364.Notes = "G18, H3, T7";
            row364.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row364);

            // Row 30: SA-213, T91, Smls. tube
            var row365 = new OldStressRowData();
            row365.LineNo = 30;
            row365.MaterialId = 8661;
            row365.SpecNo = "SA-213";
            row365.TypeGrade = "T91";
            row365.ProductForm = "Smls. tube";
            row365.AlloyUNS = "K90901";
            row365.ClassCondition = "";
            row365.Notes = "G18, T7";
            row365.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row365);

            // Row 31: SA-234, WP91, Fittings
            var row366 = new OldStressRowData();
            row366.LineNo = 31;
            row366.MaterialId = 8662;
            row366.SpecNo = "SA-234";
            row366.TypeGrade = "WP91";
            row366.ProductForm = "Fittings";
            row366.AlloyUNS = "K90901";
            row366.ClassCondition = "";
            row366.Notes = "G18, T7";
            row366.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row366);

            // Row 32: SA-234, WP91, Fittings
            var row367 = new OldStressRowData();
            row367.LineNo = 32;
            row367.MaterialId = 8663;
            row367.SpecNo = "SA-234";
            row367.TypeGrade = "WP91";
            row367.ProductForm = "Fittings";
            row367.AlloyUNS = "K90901";
            row367.ClassCondition = "";
            row367.Notes = "G18, T7";
            row367.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row367);

            // Row 33: SA-335, P91, Smls. pipe
            var row368 = new OldStressRowData();
            row368.LineNo = 33;
            row368.MaterialId = 8664;
            row368.SpecNo = "SA-335";
            row368.TypeGrade = "P91";
            row368.ProductForm = "Smls. pipe";
            row368.AlloyUNS = "K90901";
            row368.ClassCondition = "";
            row368.Notes = "G18, T7";
            row368.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row368);

            // Row 34: SA-335, P91, Smls. pipe
            var row369 = new OldStressRowData();
            row369.LineNo = 34;
            row369.MaterialId = 8665;
            row369.SpecNo = "SA-335";
            row369.TypeGrade = "P91";
            row369.ProductForm = "Smls. pipe";
            row369.AlloyUNS = "K90901";
            row369.ClassCondition = "";
            row369.Notes = "G18, H3, T7";
            row369.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row369);

            // Row 35: SA-336, F91, Forgings
            var row370 = new OldStressRowData();
            row370.LineNo = 35;
            row370.MaterialId = 8666;
            row370.SpecNo = "SA-336";
            row370.TypeGrade = "F91";
            row370.ProductForm = "Forgings";
            row370.AlloyUNS = "K90901";
            row370.ClassCondition = "";
            row370.Notes = "G18, H3, T7";
            row370.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row370);

            // Row 36: SA-336, F91, Forgings
            var row371 = new OldStressRowData();
            row371.LineNo = 36;
            row371.MaterialId = 8667;
            row371.SpecNo = "SA-336";
            row371.TypeGrade = "F91";
            row371.ProductForm = "Forgings";
            row371.AlloyUNS = "K90901";
            row371.ClassCondition = "";
            row371.Notes = "G18, H3, T7";
            row371.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row371);

            // Row 37: SA-369, FP91, Forged pipe
            var row372 = new OldStressRowData();
            row372.LineNo = 37;
            row372.MaterialId = 8668;
            row372.SpecNo = "SA-369";
            row372.TypeGrade = "FP91";
            row372.ProductForm = "Forged pipe";
            row372.AlloyUNS = "K90901";
            row372.ClassCondition = "";
            row372.Notes = "G18, T7";
            row372.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row372);

            // Row 38: SA-369, FP91, Forged pipe
            var row373 = new OldStressRowData();
            row373.LineNo = 38;
            row373.MaterialId = 8669;
            row373.SpecNo = "SA-369";
            row373.TypeGrade = "FP91";
            row373.ProductForm = "Forged pipe";
            row373.AlloyUNS = "K90901";
            row373.ClassCondition = "";
            row373.Notes = "G18, T7";
            row373.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row373);

            // Row 39: SA-387, 91, Plate
            var row374 = new OldStressRowData();
            row374.LineNo = 39;
            row374.MaterialId = 8670;
            row374.SpecNo = "SA-387";
            row374.TypeGrade = "91";
            row374.ProductForm = "Plate";
            row374.AlloyUNS = "K90901";
            row374.ClassCondition = "2";
            row374.Notes = "G18, H3, T7";
            row374.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 14, 10.3, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row374);

            // Row 40: SA-387, 91, Plate
            var row375 = new OldStressRowData();
            row375.LineNo = 40;
            row375.MaterialId = 8671;
            row375.SpecNo = "SA-387";
            row375.TypeGrade = "91";
            row375.ProductForm = "Plate";
            row375.AlloyUNS = "K90901";
            row375.ClassCondition = "2";
            row375.Notes = "G18, H3, T7";
            row375.StressValues = new double?[] { 24.3, null, 24.3, null, 24.3, 24.2, 24.1, 23.7, 23.4, 22.9, 22.2, 21.3, 20.3, 19.1, 17.8, 16.3, 12.9, 9.6, 7, 4.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row375);

            // Row 1: SA-240, TP409, Plate
            var row376 = new OldStressRowData();
            row376.LineNo = 1;
            row376.MaterialId = 8672;
            row376.SpecNo = "SA-240";
            row376.TypeGrade = "TP409";
            row376.ProductForm = "Plate";
            row376.AlloyUNS = "S40900";
            row376.ClassCondition = "";
            row376.Notes = "";
            row376.StressValues = new double?[] { 15.7, null, 15.7, null, 15.4, 14.5, 13.9, 13.6, 13.6, 13.5, 13.4, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row376);

            // Row 2: SA-268, TP409, Wld. tube
            var row377 = new OldStressRowData();
            row377.LineNo = 2;
            row377.MaterialId = 8673;
            row377.SpecNo = "SA-268";
            row377.TypeGrade = "TP409";
            row377.ProductForm = "Wld. tube";
            row377.AlloyUNS = "S40900";
            row377.ClassCondition = "";
            row377.Notes = "G24";
            row377.StressValues = new double?[] { 13.4, null, 13.4, null, 13.1, 12.3, 11.8, 11.6, 11.5, 11.5, 11.4, 11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row377);

            // Row 3: SA-268, TP409, Smls. tube
            var row378 = new OldStressRowData();
            row378.LineNo = 3;
            row378.MaterialId = 8674;
            row378.SpecNo = "SA-268";
            row378.TypeGrade = "TP409";
            row378.ProductForm = "Smls. tube";
            row378.AlloyUNS = "S40900";
            row378.ClassCondition = "";
            row378.Notes = "";
            row378.StressValues = new double?[] { 15.7, null, 15.7, null, 15.4, 14.5, 13.9, 13.6, 13.6, 13.5, 13.4, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row378);

            // Row 4: SA-479, 403, Bar
            var row379 = new OldStressRowData();
            row379.LineNo = 4;
            row379.MaterialId = 8675;
            row379.SpecNo = "SA-479";
            row379.TypeGrade = "403";
            row379.ProductForm = "Bar";
            row379.AlloyUNS = "S40300";
            row379.ClassCondition = "A";
            row379.Notes = "";
            row379.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row379);

            // Row 5: SA-479, 403, Bar
            var row380 = new OldStressRowData();
            row380.LineNo = 5;
            row380.MaterialId = 8676;
            row380.SpecNo = "SA-479";
            row380.TypeGrade = "403";
            row380.ProductForm = "Bar";
            row380.AlloyUNS = "S40300";
            row380.ClassCondition = "1";
            row380.Notes = "";
            row380.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row380);

            // Row 6: SA-240, 405, Plate
            var row381 = new OldStressRowData();
            row381.LineNo = 6;
            row381.MaterialId = 8679;
            row381.SpecNo = "SA-240";
            row381.TypeGrade = "405";
            row381.ProductForm = "Plate";
            row381.AlloyUNS = "S40500";
            row381.ClassCondition = "";
            row381.Notes = "G32, T5";
            row381.StressValues = new double?[] { 16.7, null, 15.3, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, 13.1, 12.6, 12, 11.3, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row381);

            // Row 7: SA-240, 405, Plate
            var row382 = new OldStressRowData();
            row382.LineNo = 7;
            row382.MaterialId = 8677;
            row382.SpecNo = "SA-240";
            row382.TypeGrade = "405";
            row382.ProductForm = "Plate";
            row382.AlloyUNS = "S40500";
            row382.ClassCondition = "";
            row382.Notes = "G32";
            row382.StressValues = new double?[] { 16.7, null, 15.3, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row382);

            // Row 8: SA-479, 405, Bar
            var row383 = new OldStressRowData();
            row383.LineNo = 8;
            row383.MaterialId = 8680;
            row383.SpecNo = "SA-479";
            row383.TypeGrade = "405";
            row383.ProductForm = "Bar";
            row383.AlloyUNS = "S40500";
            row383.ClassCondition = "";
            row383.Notes = "G32, T5";
            row383.StressValues = new double?[] { 16.7, null, 15.3, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, 13.1, 12.6, 12, 11.3, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row383);

            // Row 9: SA-268, TP405, Smls. & wld. tube
            var row384 = new OldStressRowData();
            row384.LineNo = 9;
            row384.MaterialId = 8681;
            row384.SpecNo = "SA-268";
            row384.TypeGrade = "TP405";
            row384.ProductForm = "Smls. & wld. tube";
            row384.AlloyUNS = "S40500";
            row384.ClassCondition = "";
            row384.Notes = "G32, T5, W13";
            row384.StressValues = new double?[] { 17.1, null, 17.1, null, 16.8, 16.5, 16.3, 15.9, 15.6, 15.2, 14.7, 14.1, 13.4, 12.6, 8.4, 4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row384);

            // Row 10: SA-268, TP405, Wld. tube
            var row385 = new OldStressRowData();
            row385.LineNo = 10;
            row385.MaterialId = 8682;
            row385.SpecNo = "SA-268";
            row385.TypeGrade = "TP405";
            row385.ProductForm = "Wld. tube";
            row385.AlloyUNS = "S40500";
            row385.ClassCondition = "";
            row385.Notes = "G3, G24, G32, T5";
            row385.StressValues = new double?[] { 14.6, null, 14.6, null, 14.3, 14, 13.8, 13.5, 13.2, 12.9, 12.5, 12, 11.4, 10.7, 7.1, 3.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row385);

            // Row 11: SA-268, , Wld. tube
            var row386 = new OldStressRowData();
            row386.LineNo = 11;
            row386.MaterialId = 8683;
            row386.SpecNo = "SA-268";
            row386.TypeGrade = "";
            row386.ProductForm = "Wld. tube";
            row386.AlloyUNS = "S40800";
            row386.ClassCondition = "";
            row386.Notes = "G32";
            row386.StressValues = new double?[] { 13.4, null, 13.4, null, 13.1, 12.9, 12.7, 12.4, 12.1, 11.8, 11.4, 11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row386);

            // Row 12: SA-268, , Smls. tube
            var row387 = new OldStressRowData();
            row387.LineNo = 12;
            row387.MaterialId = 8684;
            row387.SpecNo = "SA-268";
            row387.TypeGrade = "";
            row387.ProductForm = "Smls. tube";
            row387.AlloyUNS = "S40800";
            row387.ClassCondition = "";
            row387.Notes = "G32";
            row387.StressValues = new double?[] { 15.7, null, 15.7, null, 15.4, 15.1, 14.9, 14.5, 14.3, 13.9, 13.5, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row387);

            // Row 13: SA-240, 410S, Plate
            var row388 = new OldStressRowData();
            row388.LineNo = 13;
            row388.MaterialId = 8685;
            row388.SpecNo = "SA-240";
            row388.TypeGrade = "410S";
            row388.ProductForm = "Plate";
            row388.AlloyUNS = "S41008";
            row388.ClassCondition = "";
            row388.Notes = "T4";
            row388.StressValues = new double?[] { 17.1, null, 17.1, null, 16.8, 16.5, 16.3, 15.9, 15.6, 15.2, 14.7, 14.1, 13.4, 12.3, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row388);

            // Row 14: SA-268, TP410, Smls. & wld. tube
            var row389 = new OldStressRowData();
            row389.LineNo = 14;
            row389.MaterialId = 8687;
            row389.SpecNo = "SA-268";
            row389.TypeGrade = "TP410";
            row389.ProductForm = "Smls. & wld. tube";
            row389.AlloyUNS = "S41000";
            row389.ClassCondition = "";
            row389.Notes = "T4, W13";
            row389.StressValues = new double?[] { 17.1, null, 17.1, null, 16.8, 16.5, 16.3, 15.9, 15.6, 15.2, 14.7, 14.1, 13.4, 12.3, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row389);

            // Row 15: SA-268, TP410, Wld. tube
            var row390 = new OldStressRowData();
            row390.LineNo = 15;
            row390.MaterialId = 8688;
            row390.SpecNo = "SA-268";
            row390.TypeGrade = "TP410";
            row390.ProductForm = "Wld. tube";
            row390.AlloyUNS = "S41000";
            row390.ClassCondition = "";
            row390.Notes = "G3, G24, T4";
            row390.StressValues = new double?[] { 14.6, null, 14.6, null, 14.3, 14, 13.8, 13.5, 13.2, 12.9, 12.5, 12, 11.4, 10.5, 7.5, 5.4, 3.7, 2.5, 1.5, 0.85, null, null, null, null, null, null, null, null, null };
            batch.Add(row390);

            // Row 16: SA-240, 410, Plate
            var row391 = new OldStressRowData();
            row391.LineNo = 16;
            row391.MaterialId = 8689;
            row391.SpecNo = "SA-240";
            row391.TypeGrade = "410";
            row391.ProductForm = "Plate";
            row391.AlloyUNS = "S41000";
            row391.ClassCondition = "";
            row391.Notes = "T4";
            row391.StressValues = new double?[] { 18.6, null, 18.4, null, 17.8, 17.4, 17.2, 16.8, 16.6, 16.2, 15.7, 15.1, 14.4, 12.3, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row391);

            // Row 17: SA-182, F6a, Forgings
            var row392 = new OldStressRowData();
            row392.LineNo = 17;
            row392.MaterialId = 8690;
            row392.SpecNo = "SA-182";
            row392.TypeGrade = "F6a";
            row392.ProductForm = "Forgings";
            row392.AlloyUNS = "S41000";
            row392.ClassCondition = "1";
            row392.Notes = "T4";
            row392.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, 17.1, 16.4, 15.6, 12.3, 8.8, 6.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row392);

            // Row 18: SA-479, 410, Bar
            var row393 = new OldStressRowData();
            row393.LineNo = 18;
            row393.MaterialId = 8691;
            row393.SpecNo = "SA-479";
            row393.TypeGrade = "410";
            row393.ProductForm = "Bar";
            row393.AlloyUNS = "S41000";
            row393.ClassCondition = "";
            row393.Notes = "G22, T4";
            row393.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, 17.1, 16.4, 15.6, 12.3, 8.8, 6.4, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row393);

            // Row 19: SA-479, 410, Bar
            var row394 = new OldStressRowData();
            row394.LineNo = 19;
            row394.MaterialId = 8693;
            row394.SpecNo = "SA-479";
            row394.TypeGrade = "410";
            row394.ProductForm = "Bar";
            row394.AlloyUNS = "S41000";
            row394.ClassCondition = "A";
            row394.Notes = "";
            row394.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row394);

            // Row 20: SA-479, 410, Bar
            var row395 = new OldStressRowData();
            row395.LineNo = 20;
            row395.MaterialId = 8692;
            row395.SpecNo = "SA-479";
            row395.TypeGrade = "410";
            row395.ProductForm = "Bar";
            row395.AlloyUNS = "S41000";
            row395.ClassCondition = "1";
            row395.Notes = "G18";
            row395.StressValues = new double?[] { 20, null, 20, null, 19.6, 19.3, 19, 18.5, 18.1, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row395);

            // Row 21: SA-182, F6a, Forgings
            var row396 = new OldStressRowData();
            row396.LineNo = 21;
            row396.MaterialId = 8695;
            row396.SpecNo = "SA-182";
            row396.TypeGrade = "F6a";
            row396.ProductForm = "Forgings";
            row396.AlloyUNS = "S41000";
            row396.ClassCondition = "2";
            row396.Notes = "T3";
            row396.StressValues = new double?[] { 24.3, null, 24.3, null, 23.8, 23.4, 23, 22.5, 22, 21.5, 20.8, 19.9, 17.2, 12.3, 8.8, 6.4, 4.4, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row396);

            // Row 22: SA-217, CA15, Castings
            var row397 = new OldStressRowData();
            row397.LineNo = 22;
            row397.MaterialId = 8698;
            row397.SpecNo = "SA-217";
            row397.TypeGrade = "CA15";
            row397.ProductForm = "Castings";
            row397.AlloyUNS = "J91150";
            row397.ClassCondition = "";
            row397.Notes = "G1, G17, T3";
            row397.StressValues = new double?[] { 25.7, null, 25.7, null, 25.2, 24.8, 24.4, 23.8, 23.3, 22.7, 22, 21.1, 15.9, 11, 7.6, 5, 3.3, 2.2, 1.5, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row397);

            // Row 23: SA-426, CPCA15, Cast pipe
            var row398 = new OldStressRowData();
            row398.LineNo = 23;
            row398.MaterialId = 8699;
            row398.SpecNo = "SA-426";
            row398.TypeGrade = "CPCA15";
            row398.ProductForm = "Cast pipe";
            row398.AlloyUNS = "J91150";
            row398.ClassCondition = "";
            row398.Notes = "G17";
            row398.StressValues = new double?[] { 25.7, null, 25.7, null, 25.2, 24.8, 24.4, 23.8, 23.3, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row398);

            // Row 24: SA-487, CA6NM, Castings
            var row399 = new OldStressRowData();
            row399.LineNo = 24;
            row399.MaterialId = 8701;
            row399.SpecNo = "SA-487";
            row399.TypeGrade = "CA6NM";
            row399.ProductForm = "Castings";
            row399.AlloyUNS = "J91540";
            row399.ClassCondition = "A";
            row399.Notes = "G1, G17";
            row399.StressValues = new double?[] { 31.4, null, 31.4, 31.3, 30.8, 30.1, 29.4, 28.8, 28.4, 27.9, 27.4, 26.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row399);

            // Row 25: SA-182, F6NM, Forgings
            var row400 = new OldStressRowData();
            row400.LineNo = 25;
            row400.MaterialId = 8702;
            row400.SpecNo = "SA-182";
            row400.TypeGrade = "F6NM";
            row400.ProductForm = "Forgings";
            row400.AlloyUNS = "S41500";
            row400.ClassCondition = "";
            row400.Notes = "G17";
            row400.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.5, 31.3, 30, 29.4, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row400);

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
