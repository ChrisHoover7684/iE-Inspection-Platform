using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch004
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

            // Row 32: SA-426, CP11, Cast pipe
            var row301 = new OldStressRowData();
            row301.LineNo = 32;
            row301.MaterialId = 8559;
            row301.SpecNo = "SA-426";
            row301.TypeGrade = "CP11";
            row301.ProductForm = "Cast pipe";
            row301.AlloyUNS = "J12072";
            row301.ClassCondition = "";
            row301.Notes = "G17";
            row301.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row301);

            // Row 33: SA-739, B11, Bar
            var row302 = new OldStressRowData();
            row302.LineNo = 33;
            row302.MaterialId = 8560;
            row302.SpecNo = "SA-739";
            row302.TypeGrade = "B11";
            row302.ProductForm = "Bar";
            row302.AlloyUNS = "K11797";
            row302.ClassCondition = "";
            row302.Notes = "";
            row302.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row302);

            // Row 34: SA-199, T11, Smls. tube
            var row303 = new OldStressRowData();
            row303.LineNo = 34;
            row303.MaterialId = 8561;
            row303.SpecNo = "SA-199";
            row303.TypeGrade = "T11";
            row303.ProductForm = "Smls. tube";
            row303.AlloyUNS = "K11597";
            row303.ClassCondition = "";
            row303.Notes = "";
            row303.StressValues = new double?[] { 14, null, 14, null, 14, 14, 13.5, 13.1, 12.8, 12.6, 12.3, 12, 11.7, 11.3, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row303);

            // Row 35: SA-182, F11, Forgings
            var row304 = new OldStressRowData();
            row304.LineNo = 35;
            row304.MaterialId = 8562;
            row304.SpecNo = "SA-182";
            row304.TypeGrade = "F11";
            row304.ProductForm = "Forgings";
            row304.AlloyUNS = "K11597";
            row304.ClassCondition = "1";
            row304.Notes = "G18, S4";
            row304.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row304);

            // Row 36: SA-213, T11, Smls. tube
            var row305 = new OldStressRowData();
            row305.LineNo = 36;
            row305.MaterialId = 8563;
            row305.SpecNo = "SA-213";
            row305.TypeGrade = "T11";
            row305.ProductForm = "Smls. tube";
            row305.AlloyUNS = "K11597";
            row305.ClassCondition = "";
            row305.Notes = "S4";
            row305.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row305);

            // Row 37: SA-213, T11, Smls. tube
            var row306 = new OldStressRowData();
            row306.LineNo = 37;
            row306.MaterialId = 8564;
            row306.SpecNo = "SA-213";
            row306.TypeGrade = "T11";
            row306.ProductForm = "Smls. tube";
            row306.AlloyUNS = "K11597";
            row306.ClassCondition = "";
            row306.Notes = "";
            row306.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row306);

            // Row 38: SA-234, WP11, Smls. & wld. fittings
            var row307 = new OldStressRowData();
            row307.LineNo = 38;
            row307.MaterialId = 8565;
            row307.SpecNo = "SA-234";
            row307.TypeGrade = "WP11";
            row307.ProductForm = "Smls. & wld. fittings";
            row307.AlloyUNS = "";
            row307.ClassCondition = "1";
            row307.Notes = "G18, S4";
            row307.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row307);

            // Row 39: ..., , 
            var row308 = new OldStressRowData();
            row308.LineNo = 39;
            row308.MaterialId = 8566;
            row308.SpecNo = "...";
            row308.TypeGrade = "";
            row308.ProductForm = "";
            row308.AlloyUNS = "";
            row308.ClassCondition = "";
            row308.Notes = "";
            row308.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row308);

            // Row 40: ..., , 
            var row309 = new OldStressRowData();
            row309.LineNo = 40;
            row309.MaterialId = 8567;
            row309.SpecNo = "...";
            row309.TypeGrade = "";
            row309.ProductForm = "";
            row309.AlloyUNS = "";
            row309.ClassCondition = "";
            row309.Notes = "";
            row309.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row309);

            // Row 1: SA-335, P11, Smls. pipe
            var row310 = new OldStressRowData();
            row310.LineNo = 1;
            row310.MaterialId = 8568;
            row310.SpecNo = "SA-335";
            row310.TypeGrade = "P11";
            row310.ProductForm = "Smls. pipe";
            row310.AlloyUNS = "K11597";
            row310.ClassCondition = "";
            row310.Notes = "S4";
            row310.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row310);

            // Row 2: SA-335, P11, Smls. pipe
            var row311 = new OldStressRowData();
            row311.LineNo = 2;
            row311.MaterialId = 8569;
            row311.SpecNo = "SA-335";
            row311.TypeGrade = "P11";
            row311.ProductForm = "Smls. pipe";
            row311.AlloyUNS = "K11597";
            row311.ClassCondition = "";
            row311.Notes = "";
            row311.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row311);

            // Row 3: SA-336, F11, Forgings
            var row312 = new OldStressRowData();
            row312.LineNo = 3;
            row312.MaterialId = 8570;
            row312.SpecNo = "SA-336";
            row312.TypeGrade = "F11";
            row312.ProductForm = "Forgings";
            row312.AlloyUNS = "K11597";
            row312.ClassCondition = "1";
            row312.Notes = "G18, S4";
            row312.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row312);

            // Row 4: SA-369, FP11, Forged pipe
            var row313 = new OldStressRowData();
            row313.LineNo = 4;
            row313.MaterialId = 8571;
            row313.SpecNo = "SA-369";
            row313.TypeGrade = "FP11";
            row313.ProductForm = "Forged pipe";
            row313.AlloyUNS = "K11597";
            row313.ClassCondition = "";
            row313.Notes = "S4";
            row313.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row313);

            // Row 5: SA-369, FP11, Forged pipe
            var row314 = new OldStressRowData();
            row314.LineNo = 5;
            row314.MaterialId = 8572;
            row314.SpecNo = "SA-369";
            row314.TypeGrade = "FP11";
            row314.ProductForm = "Forged pipe";
            row314.AlloyUNS = "K11597";
            row314.ClassCondition = "";
            row314.Notes = "";
            row314.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row314);

            // Row 6: SA-387, 11, Plate
            var row315 = new OldStressRowData();
            row315.LineNo = 6;
            row315.MaterialId = 8573;
            row315.SpecNo = "SA-387";
            row315.TypeGrade = "11";
            row315.ProductForm = "Plate";
            row315.AlloyUNS = "K11789";
            row315.ClassCondition = "1";
            row315.Notes = "S4";
            row315.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.6, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row315);

            // Row 7: SA-691, 1 1/4 CR, Wld. pipe
            var row316 = new OldStressRowData();
            row316.LineNo = 7;
            row316.MaterialId = 8574;
            row316.SpecNo = "SA-691";
            row316.TypeGrade = "1 1/4 CR";
            row316.ProductForm = "Wld. pipe";
            row316.AlloyUNS = "K11789";
            row316.ClassCondition = "";
            row316.Notes = "G27, W10, W12";
            row316.StressValues = new double?[] { 15, null, 15, null, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row316);

            // Row 8: SA-691, 1 1/4 CR, Wld. pipe
            var row317 = new OldStressRowData();
            row317.LineNo = 8;
            row317.MaterialId = 8575;
            row317.SpecNo = "SA-691";
            row317.TypeGrade = "1 1/4 CR";
            row317.ProductForm = "Wld. pipe";
            row317.AlloyUNS = "K11789";
            row317.ClassCondition = "";
            row317.Notes = "G26, W10, W12";
            row317.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row317);

            // Row 9: SA-182, F11, Forgings
            var row318 = new OldStressRowData();
            row318.LineNo = 9;
            row318.MaterialId = 8576;
            row318.SpecNo = "SA-182";
            row318.TypeGrade = "F11";
            row318.ProductForm = "Forgings";
            row318.AlloyUNS = "K11572";
            row318.ClassCondition = "2";
            row318.Notes = "G18, S4";
            row318.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row318);

            // Row 10: SA-336, F11, Forgings
            var row319 = new OldStressRowData();
            row319.LineNo = 10;
            row319.MaterialId = 8577;
            row319.SpecNo = "SA-336";
            row319.TypeGrade = "F11";
            row319.ProductForm = "Forgings";
            row319.AlloyUNS = "K11572";
            row319.ClassCondition = "2";
            row319.Notes = "G18, S4";
            row319.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row319);

            // Row 11: SA-336, F11, Forgings
            var row320 = new OldStressRowData();
            row320.LineNo = 11;
            row320.MaterialId = 8578;
            row320.SpecNo = "SA-336";
            row320.TypeGrade = "F11";
            row320.ProductForm = "Forgings";
            row320.AlloyUNS = "K11572";
            row320.ClassCondition = "3";
            row320.Notes = "";
            row320.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.3, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row320);

            // Row 12: SA-387, 11, Plate
            var row321 = new OldStressRowData();
            row321.LineNo = 12;
            row321.MaterialId = 8579;
            row321.SpecNo = "SA-387";
            row321.TypeGrade = "11";
            row321.ProductForm = "Plate";
            row321.AlloyUNS = "K11789";
            row321.ClassCondition = "2";
            row321.Notes = "S4";
            row321.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.3, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row321);

            // Row 13: SA-691, 1 1/4 CR, Wld. pipe
            var row322 = new OldStressRowData();
            row322.LineNo = 13;
            row322.MaterialId = 8580;
            row322.SpecNo = "SA-691";
            row322.TypeGrade = "1 1/4 CR";
            row322.ProductForm = "Wld. pipe";
            row322.AlloyUNS = "K11789";
            row322.ClassCondition = "";
            row322.Notes = "G26, W10, W12";
            row322.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row322);

            // Row 14: SA-592, E, Forgings
            var row323 = new OldStressRowData();
            row323.LineNo = 14;
            row323.MaterialId = 8581;
            row323.SpecNo = "SA-592";
            row323.TypeGrade = "E";
            row323.ProductForm = "Forgings";
            row323.AlloyUNS = "K11695";
            row323.ClassCondition = "";
            row323.Notes = "S7";
            row323.StressValues = new double?[] { 26.3, null, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row323);

            // Row 15: SA-592, E, Forgings
            var row324 = new OldStressRowData();
            row324.LineNo = 15;
            row324.MaterialId = 8582;
            row324.SpecNo = "SA-592";
            row324.TypeGrade = "E";
            row324.ProductForm = "Forgings";
            row324.AlloyUNS = "K11695";
            row324.ClassCondition = "";
            row324.Notes = "";
            row324.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row324);

            // Row 16: SA-517, E, Plate
            var row325 = new OldStressRowData();
            row325.LineNo = 16;
            row325.MaterialId = 8584;
            row325.SpecNo = "SA-517";
            row325.TypeGrade = "E";
            row325.ProductForm = "Plate";
            row325.AlloyUNS = "K21604";
            row325.ClassCondition = "";
            row325.Notes = "";
            row325.StressValues = new double?[] { 26.3, null, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row325);

            // Row 17: SA-517, E, Plate
            var row326 = new OldStressRowData();
            row326.LineNo = 17;
            row326.MaterialId = 8583;
            row326.SpecNo = "SA-517";
            row326.TypeGrade = "E";
            row326.ProductForm = "Plate";
            row326.AlloyUNS = "K21604";
            row326.ClassCondition = "";
            row326.Notes = "";
            row326.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row326);

            // Row 18: SA-517, E, Plate
            var row327 = new OldStressRowData();
            row327.LineNo = 18;
            row327.MaterialId = 8585;
            row327.SpecNo = "SA-517";
            row327.TypeGrade = "E";
            row327.ProductForm = "Plate";
            row327.AlloyUNS = "K21604";
            row327.ClassCondition = "";
            row327.Notes = "";
            row327.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row327);

            // Row 19: SA-199, T22, Tube
            var row328 = new OldStressRowData();
            row328.LineNo = 19;
            row328.MaterialId = 8586;
            row328.SpecNo = "SA-199";
            row328.TypeGrade = "T22";
            row328.ProductForm = "Tube";
            row328.AlloyUNS = "K21590";
            row328.ClassCondition = "";
            row328.Notes = "W7";
            row328.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row328);

            // Row 20: SA-182, F22, Forgings
            var row329 = new OldStressRowData();
            row329.LineNo = 20;
            row329.MaterialId = 8587;
            row329.SpecNo = "SA-182";
            row329.TypeGrade = "F22";
            row329.ProductForm = "Forgings";
            row329.AlloyUNS = "K21590";
            row329.ClassCondition = "1";
            row329.Notes = "G18, S4, W7, W9";
            row329.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row329);

            // Row 21: SA-213, T22, Smls. tube
            var row330 = new OldStressRowData();
            row330.LineNo = 21;
            row330.MaterialId = 8588;
            row330.SpecNo = "SA-213";
            row330.TypeGrade = "T22";
            row330.ProductForm = "Smls. tube";
            row330.AlloyUNS = "K21590";
            row330.ClassCondition = "";
            row330.Notes = "S4, W7, W9";
            row330.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row330);

            // Row 22: SA-234, WP22, Smls. & wld. fittings
            var row331 = new OldStressRowData();
            row331.LineNo = 22;
            row331.MaterialId = 8589;
            row331.SpecNo = "SA-234";
            row331.TypeGrade = "WP22";
            row331.ProductForm = "Smls. & wld. fittings";
            row331.AlloyUNS = "K21590";
            row331.ClassCondition = "1";
            row331.Notes = "G18, S4, W7, W9";
            row331.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row331);

            // Row 23: ..., , 
            var row332 = new OldStressRowData();
            row332.LineNo = 23;
            row332.MaterialId = 8590;
            row332.SpecNo = "...";
            row332.TypeGrade = "";
            row332.ProductForm = "";
            row332.AlloyUNS = "";
            row332.ClassCondition = "";
            row332.Notes = "";
            row332.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row332);

            // Row 24: SA-335, P22, Smls. pipe
            var row333 = new OldStressRowData();
            row333.LineNo = 24;
            row333.MaterialId = 8591;
            row333.SpecNo = "SA-335";
            row333.TypeGrade = "P22";
            row333.ProductForm = "Smls. pipe";
            row333.AlloyUNS = "K21590";
            row333.ClassCondition = "";
            row333.Notes = "S4, W7, W9";
            row333.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row333);

            // Row 25: SA-336, F22, Forgings
            var row334 = new OldStressRowData();
            row334.LineNo = 25;
            row334.MaterialId = 8592;
            row334.SpecNo = "SA-336";
            row334.TypeGrade = "F22";
            row334.ProductForm = "Forgings";
            row334.AlloyUNS = "K21590";
            row334.ClassCondition = "1";
            row334.Notes = "G18, S4, W7, W9";
            row334.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row334);

            // Row 26: SA-369, FP22, Forged pipe
            var row335 = new OldStressRowData();
            row335.LineNo = 26;
            row335.MaterialId = 8593;
            row335.SpecNo = "SA-369";
            row335.TypeGrade = "FP22";
            row335.ProductForm = "Forged pipe";
            row335.AlloyUNS = "K21590";
            row335.ClassCondition = "";
            row335.Notes = "S4, W7, W9";
            row335.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row335);

            // Row 27: SA-387, 22, Plate
            var row336 = new OldStressRowData();
            row336.LineNo = 27;
            row336.MaterialId = 8594;
            row336.SpecNo = "SA-387";
            row336.TypeGrade = "22";
            row336.ProductForm = "Plate";
            row336.AlloyUNS = "K21590";
            row336.ClassCondition = "1";
            row336.Notes = "S4, W7, W9";
            row336.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row336);

            // Row 28: SA-691, 2 1/4 CR, Wld. pipe
            var row337 = new OldStressRowData();
            row337.LineNo = 28;
            row337.MaterialId = 8595;
            row337.SpecNo = "SA-691";
            row337.TypeGrade = "2 1/4 CR";
            row337.ProductForm = "Wld. pipe";
            row337.AlloyUNS = "K21590";
            row337.ClassCondition = "";
            row337.Notes = "G26, W10, W12";
            row337.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row337);

            // Row 29: SA-217, WC9, Castings
            var row338 = new OldStressRowData();
            row338.LineNo = 29;
            row338.MaterialId = 8596;
            row338.SpecNo = "SA-217";
            row338.TypeGrade = "WC9";
            row338.ProductForm = "Castings";
            row338.AlloyUNS = "J21890";
            row338.ClassCondition = "";
            row338.Notes = "G1, G17, G18, S4, W7, W9";
            row338.StressValues = new double?[] { 17.5, null, 17.5, null, 17.3, 16.9, 16.8, 16.8, 16.7, 16.6, 16.1, 15.7, 15, 14.2, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row338);

            // Row 30: SA-426, CP22, Cast pipe
            var row339 = new OldStressRowData();
            row339.LineNo = 30;
            row339.MaterialId = 8597;
            row339.SpecNo = "SA-426";
            row339.TypeGrade = "CP22";
            row339.ProductForm = "Cast pipe";
            row339.AlloyUNS = "J21890";
            row339.ClassCondition = "";
            row339.Notes = "G17";
            row339.StressValues = new double?[] { 17.5, null, 17.5, null, 17.3, 16.9, 16.8, 16.8, 16.7, 16.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row339);

            // Row 31: SA-182, F22, Forgings
            var row340 = new OldStressRowData();
            row340.LineNo = 31;
            row340.MaterialId = 8598;
            row340.SpecNo = "SA-182";
            row340.TypeGrade = "F22";
            row340.ProductForm = "Forgings";
            row340.AlloyUNS = "K21590";
            row340.ClassCondition = "3";
            row340.Notes = "G18, S4, W7, W9";
            row340.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row340);

            // Row 32: SA-336, F22, Forgings
            var row341 = new OldStressRowData();
            row341.LineNo = 32;
            row341.MaterialId = 8599;
            row341.SpecNo = "SA-336";
            row341.TypeGrade = "F22";
            row341.ProductForm = "Forgings";
            row341.AlloyUNS = "K21590";
            row341.ClassCondition = "3";
            row341.Notes = "G18, S4, W7, W9";
            row341.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row341);

            // Row 33: SA-387, 22, Plate
            var row342 = new OldStressRowData();
            row342.LineNo = 33;
            row342.MaterialId = 8600;
            row342.SpecNo = "SA-387";
            row342.TypeGrade = "22";
            row342.ProductForm = "Plate";
            row342.AlloyUNS = "K21590";
            row342.ClassCondition = "2";
            row342.Notes = "S4, W7, W9";
            row342.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row342);

            // Row 34: SA-691, 2 1/4 CR, Wld. pipe
            var row343 = new OldStressRowData();
            row343.LineNo = 34;
            row343.MaterialId = 8601;
            row343.SpecNo = "SA-691";
            row343.TypeGrade = "2 1/4 CR";
            row343.ProductForm = "Wld. pipe";
            row343.AlloyUNS = "K21590";
            row343.ClassCondition = "";
            row343.Notes = "G26, W10, W12";
            row343.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row343);

            // Row 35: SA-739, B22, Bar
            var row344 = new OldStressRowData();
            row344.LineNo = 35;
            row344.MaterialId = 8602;
            row344.SpecNo = "SA-739";
            row344.TypeGrade = "B22";
            row344.ProductForm = "Bar";
            row344.AlloyUNS = "K21390";
            row344.ClassCondition = "";
            row344.Notes = "W7";
            row344.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 15.8, 11.4, 7.8, 5.1, 3.2, 2, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row344);

            // Row 36: SA-487, 8, Castings
            var row345 = new OldStressRowData();
            row345.LineNo = 36;
            row345.MaterialId = 8603;
            row345.SpecNo = "SA-487";
            row345.TypeGrade = "8";
            row345.ProductForm = "Castings";
            row345.AlloyUNS = "J22091";
            row345.ClassCondition = "A";
            row345.Notes = "G1, W7";
            row345.StressValues = new double?[] { 21.3, 21.3, 21.3, null, 21, 20.5, 20.4, 20.4, 20.3, 20.1, 19.6, 19.1, 18.2, 15.8, 11.4, 7.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row345);

            // Row 37: SA-199, T21, Smls. tube
            var row346 = new OldStressRowData();
            row346.LineNo = 37;
            row346.MaterialId = 8604;
            row346.SpecNo = "SA-199";
            row346.TypeGrade = "T21";
            row346.ProductForm = "Smls. tube";
            row346.AlloyUNS = "K31545";
            row346.ClassCondition = "";
            row346.Notes = "";
            row346.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row346);

            // Row 38: SA-213, T21, Smls. tube
            var row347 = new OldStressRowData();
            row347.LineNo = 38;
            row347.MaterialId = 8605;
            row347.SpecNo = "SA-213";
            row347.TypeGrade = "T21";
            row347.ProductForm = "Smls. tube";
            row347.AlloyUNS = "K31545";
            row347.ClassCondition = "";
            row347.Notes = "S4";
            row347.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row347);

            // Row 39: SA-213, T21, Smls. tube
            var row348 = new OldStressRowData();
            row348.LineNo = 39;
            row348.MaterialId = 8606;
            row348.SpecNo = "SA-213";
            row348.TypeGrade = "T21";
            row348.ProductForm = "Smls. tube";
            row348.AlloyUNS = "K31545";
            row348.ClassCondition = "";
            row348.Notes = "";
            row348.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row348);

            // Row 1: SA-335, P21, Smls. pipe
            var row349 = new OldStressRowData();
            row349.LineNo = 1;
            row349.MaterialId = 8607;
            row349.SpecNo = "SA-335";
            row349.TypeGrade = "P21";
            row349.ProductForm = "Smls. pipe";
            row349.AlloyUNS = "K31545";
            row349.ClassCondition = "";
            row349.Notes = "S4";
            row349.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row349);

            // Row 2: SA-335, P21, Smls. pipe
            var row350 = new OldStressRowData();
            row350.LineNo = 2;
            row350.MaterialId = 8608;
            row350.SpecNo = "SA-335";
            row350.TypeGrade = "P21";
            row350.ProductForm = "Smls. pipe";
            row350.AlloyUNS = "K31545";
            row350.ClassCondition = "";
            row350.Notes = "";
            row350.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row350);

            // Row 3: SA-336, F21, Forgings
            var row351 = new OldStressRowData();
            row351.LineNo = 3;
            row351.MaterialId = 8609;
            row351.SpecNo = "SA-336";
            row351.TypeGrade = "F21";
            row351.ProductForm = "Forgings";
            row351.AlloyUNS = "K31545";
            row351.ClassCondition = "1";
            row351.Notes = "G18, S4";
            row351.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row351);

            // Row 4: SA-336, F21, Forgings
            var row352 = new OldStressRowData();
            row352.LineNo = 4;
            row352.MaterialId = 8610;
            row352.SpecNo = "SA-336";
            row352.TypeGrade = "F21";
            row352.ProductForm = "Forgings";
            row352.AlloyUNS = "K31545";
            row352.ClassCondition = "1";
            row352.Notes = "";
            row352.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row352);

            // Row 5: SA-369, FP21, Forged pipe
            var row353 = new OldStressRowData();
            row353.LineNo = 5;
            row353.MaterialId = 8611;
            row353.SpecNo = "SA-369";
            row353.TypeGrade = "FP21";
            row353.ProductForm = "Forged pipe";
            row353.AlloyUNS = "K31545";
            row353.ClassCondition = "";
            row353.Notes = "S4";
            row353.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row353);

            // Row 6: SA-369, FP21, Forged pipe
            var row354 = new OldStressRowData();
            row354.LineNo = 6;
            row354.MaterialId = 8612;
            row354.SpecNo = "SA-369";
            row354.TypeGrade = "FP21";
            row354.ProductForm = "Forged pipe";
            row354.AlloyUNS = "K31545";
            row354.ClassCondition = "";
            row354.Notes = "";
            row354.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row354);

            // Row 7: SA-387, 21, Plate
            var row355 = new OldStressRowData();
            row355.LineNo = 7;
            row355.MaterialId = 8613;
            row355.SpecNo = "SA-387";
            row355.TypeGrade = "21";
            row355.ProductForm = "Plate";
            row355.AlloyUNS = "K31545";
            row355.ClassCondition = "1";
            row355.Notes = "S4";
            row355.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.8, 14.5, 13.9, 13.2, 12, 9, 7, 5.5, 4, 2.7, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row355);

            // Row 8: SA-387, 21, Plate
            var row356 = new OldStressRowData();
            row356.LineNo = 8;
            row356.MaterialId = 8614;
            row356.SpecNo = "SA-387";
            row356.TypeGrade = "21";
            row356.ProductForm = "Plate";
            row356.AlloyUNS = "K31545";
            row356.ClassCondition = "1";
            row356.Notes = "";
            row356.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row356);

            // Row 9: SA-426, CP21, Cast pipe
            var row357 = new OldStressRowData();
            row357.LineNo = 9;
            row357.MaterialId = 8615;
            row357.SpecNo = "SA-426";
            row357.TypeGrade = "CP21";
            row357.ProductForm = "Cast pipe";
            row357.AlloyUNS = "J31545";
            row357.ClassCondition = "";
            row357.Notes = "G17";
            row357.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row357);

            // Row 10: SA-182, F21, Forgings
            var row358 = new OldStressRowData();
            row358.LineNo = 10;
            row358.MaterialId = 8616;
            row358.SpecNo = "SA-182";
            row358.TypeGrade = "F21";
            row358.ProductForm = "Forgings";
            row358.AlloyUNS = "K31545";
            row358.ClassCondition = "";
            row358.Notes = "G18, S4";
            row358.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row358);

            // Row 11: SA-336, F21, Forgings
            var row359 = new OldStressRowData();
            row359.LineNo = 11;
            row359.MaterialId = 8617;
            row359.SpecNo = "SA-336";
            row359.TypeGrade = "F21";
            row359.ProductForm = "Forgings";
            row359.AlloyUNS = "K31545";
            row359.ClassCondition = "3";
            row359.Notes = "G18, S4";
            row359.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row359);

            // Row 12: SA-387, 21, Plate
            var row360 = new OldStressRowData();
            row360.LineNo = 12;
            row360.MaterialId = 8618;
            row360.SpecNo = "SA-387";
            row360.TypeGrade = "21";
            row360.ProductForm = "Plate";
            row360.AlloyUNS = "K31545";
            row360.ClassCondition = "2";
            row360.Notes = "S4";
            row360.StressValues = new double?[] { 18.8, null, 18.8, null, 18.3, 18, 17.9, 17.8, 17.7, 17.5, 17.2, 16.9, 16.4, 13.1, 9.5, 6.8, 4.9, 3.2, 2.4, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row360);

            // Row 13: SA-182, F3V, Forgings
            var row361 = new OldStressRowData();
            row361.LineNo = 13;
            row361.MaterialId = 8620;
            row361.SpecNo = "SA-182";
            row361.TypeGrade = "F3V";
            row361.ProductForm = "Forgings";
            row361.AlloyUNS = "K31830";
            row361.ClassCondition = "";
            row361.Notes = "W13";
            row361.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row361);

            // Row 14: SA-336, F3V, Forgings
            var row362 = new OldStressRowData();
            row362.LineNo = 14;
            row362.MaterialId = 8623;
            row362.SpecNo = "SA-336";
            row362.TypeGrade = "F3V";
            row362.ProductForm = "Forgings";
            row362.AlloyUNS = "K31830";
            row362.ClassCondition = "";
            row362.Notes = "W13";
            row362.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row362);

            // Row 15: SA-508, 3V, Forgings
            var row363 = new OldStressRowData();
            row363.LineNo = 15;
            row363.MaterialId = 8621;
            row363.SpecNo = "SA-508";
            row363.TypeGrade = "3V";
            row363.ProductForm = "Forgings";
            row363.AlloyUNS = "K31830";
            row363.ClassCondition = "";
            row363.Notes = "W13";
            row363.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row363);

            // Row 16: SA-541, 3V, Forgings
            var row364 = new OldStressRowData();
            row364.LineNo = 16;
            row364.MaterialId = 8622;
            row364.SpecNo = "SA-541";
            row364.TypeGrade = "3V";
            row364.ProductForm = "Forgings";
            row364.AlloyUNS = "K31830";
            row364.ClassCondition = "";
            row364.Notes = "W13";
            row364.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row364);

            // Row 17: SA-542, C, Plate
            var row365 = new OldStressRowData();
            row365.LineNo = 17;
            row365.MaterialId = 8619;
            row365.SpecNo = "SA-542";
            row365.TypeGrade = "C";
            row365.ProductForm = "Plate";
            row365.AlloyUNS = "K31830";
            row365.ClassCondition = "4a";
            row365.Notes = "W13";
            row365.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row365);

            // Row 18: SA-832, , Plate
            var row366 = new OldStressRowData();
            row366.LineNo = 18;
            row366.MaterialId = 8624;
            row366.SpecNo = "SA-832";
            row366.TypeGrade = "";
            row366.ProductForm = "Plate";
            row366.AlloyUNS = "K31830";
            row366.ClassCondition = "";
            row366.Notes = "W13";
            row366.StressValues = new double?[] { 21.3, null, 21.3, null, 20.4, 19.8, 19.4, 19.1, 18.9, 18.7, 18.5, 18.2, 17.9, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row366);

            // Row 19: SA-199, T5, Smls. tube
            var row367 = new OldStressRowData();
            row367.LineNo = 19;
            row367.MaterialId = 8625;
            row367.SpecNo = "SA-199";
            row367.TypeGrade = "T5";
            row367.ProductForm = "Smls. tube";
            row367.AlloyUNS = "K41545";
            row367.ClassCondition = "";
            row367.Notes = "";
            row367.StressValues = new double?[] { 15, null, 14.1, null, 13.6, 13.4, 13.3, 13.1, 12.9, 12.8, 12.4, 12.1, 11.6, 10.9, 8, 5.8, 4.2, 2.9, 2, 1.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row367);

            // Row 20: SA-213, T5, Smls. tube
            var row368 = new OldStressRowData();
            row368.LineNo = 20;
            row368.MaterialId = 8626;
            row368.SpecNo = "SA-213";
            row368.TypeGrade = "T5";
            row368.ProductForm = "Smls. tube";
            row368.AlloyUNS = "K41545";
            row368.ClassCondition = "";
            row368.Notes = "";
            row368.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row368);

            // Row 21: SA-234, WP5, Smls. & wld. fittings
            var row369 = new OldStressRowData();
            row369.LineNo = 21;
            row369.MaterialId = 8627;
            row369.SpecNo = "SA-234";
            row369.TypeGrade = "WP5";
            row369.ProductForm = "Smls. & wld. fittings";
            row369.AlloyUNS = "K41545";
            row369.ClassCondition = "";
            row369.Notes = "";
            row369.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row369);

            // Row 22: ..., , 
            var row370 = new OldStressRowData();
            row370.LineNo = 22;
            row370.MaterialId = 8628;
            row370.SpecNo = "...";
            row370.TypeGrade = "";
            row370.ProductForm = "";
            row370.AlloyUNS = "";
            row370.ClassCondition = "";
            row370.Notes = "";
            row370.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row370);

            // Row 23: SA-335, P5, Smls. pipe
            var row371 = new OldStressRowData();
            row371.LineNo = 23;
            row371.MaterialId = 8629;
            row371.SpecNo = "SA-335";
            row371.TypeGrade = "P5";
            row371.ProductForm = "Smls. pipe";
            row371.AlloyUNS = "K41545";
            row371.ClassCondition = "";
            row371.Notes = "";
            row371.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row371);

            // Row 24: SA-369, FP5, Forged pipe
            var row372 = new OldStressRowData();
            row372.LineNo = 24;
            row372.MaterialId = 8630;
            row372.SpecNo = "SA-369";
            row372.TypeGrade = "FP5";
            row372.ProductForm = "Forged pipe";
            row372.AlloyUNS = "K41545";
            row372.ClassCondition = "";
            row372.Notes = "G18";
            row372.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row372);

            // Row 25: SA-387, 5, Plate
            var row373 = new OldStressRowData();
            row373.LineNo = 25;
            row373.MaterialId = 8631;
            row373.SpecNo = "SA-387";
            row373.TypeGrade = "5";
            row373.ProductForm = "Plate";
            row373.AlloyUNS = "K41545";
            row373.ClassCondition = "1";
            row373.Notes = "";
            row373.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row373);

            // Row 26: SA-691, 5CR, Wld. pipe
            var row374 = new OldStressRowData();
            row374.LineNo = 26;
            row374.MaterialId = 8632;
            row374.SpecNo = "SA-691";
            row374.TypeGrade = "5CR";
            row374.ProductForm = "Wld. pipe";
            row374.AlloyUNS = "K41545";
            row374.ClassCondition = "";
            row374.Notes = "G26, W10, W12";
            row374.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row374);

            // Row 27: SA-336, F5, Forgings
            var row375 = new OldStressRowData();
            row375.LineNo = 27;
            row375.MaterialId = 8633;
            row375.SpecNo = "SA-336";
            row375.TypeGrade = "F5";
            row375.ProductForm = "Forgings";
            row375.AlloyUNS = "K41545";
            row375.ClassCondition = "";
            row375.Notes = "G18";
            row375.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row375);

            // Row 28: SA-182, F5, Forgings
            var row376 = new OldStressRowData();
            row376.LineNo = 28;
            row376.MaterialId = 8634;
            row376.SpecNo = "SA-182";
            row376.TypeGrade = "F5";
            row376.ProductForm = "Forgings";
            row376.AlloyUNS = "K41545";
            row376.ClassCondition = "";
            row376.Notes = "G18";
            row376.StressValues = new double?[] { 17.5, null, 17.5, null, 17, 16.8, 16.8, 16.5, 16.3, 16, 15.4, 14.9, 14.1, 10.8, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row376);

            // Row 29: SA-182, F5, Forgings
            var row377 = new OldStressRowData();
            row377.LineNo = 29;
            row377.MaterialId = 8635;
            row377.SpecNo = "SA-182";
            row377.TypeGrade = "F5";
            row377.ProductForm = "Forgings";
            row377.AlloyUNS = "K41545";
            row377.ClassCondition = "";
            row377.Notes = "";
            row377.StressValues = new double?[] { 17.5, null, 17.5, null, 17, 16.8, 16.8, 16.5, 16.3, 16, 15.4, 14.8, 14.1, 10.8, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row377);

            // Row 30: SA-387, 5, Plate
            var row378 = new OldStressRowData();
            row378.LineNo = 30;
            row378.MaterialId = 8636;
            row378.SpecNo = "SA-387";
            row378.TypeGrade = "5";
            row378.ProductForm = "Plate";
            row378.AlloyUNS = "K41545";
            row378.ClassCondition = "2";
            row378.Notes = "";
            row378.StressValues = new double?[] { 18.8, null, 18.7, null, 18.2, 18, 18, 17.7, 17.4, 17.1, 16.5, 16, 15.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row378);

            // Row 31: SA-336, F5A, Forgings
            var row379 = new OldStressRowData();
            row379.LineNo = 31;
            row379.MaterialId = 8637;
            row379.SpecNo = "SA-336";
            row379.TypeGrade = "F5A";
            row379.ProductForm = "Forgings";
            row379.AlloyUNS = "K42544";
            row379.ClassCondition = "";
            row379.Notes = "G18";
            row379.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 19.2, 18.7, 18.6, 18.2, 17.6, 17, 14.3, 10.8, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row379);

            // Row 32: SA-336, F5A, Forgings
            var row380 = new OldStressRowData();
            row380.LineNo = 32;
            row380.MaterialId = 8638;
            row380.SpecNo = "SA-336";
            row380.TypeGrade = "F5A";
            row380.ProductForm = "Forgings";
            row380.AlloyUNS = "K42544";
            row380.ClassCondition = "";
            row380.Notes = "";
            row380.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 19.2, 18.9, 18.6, 18.2, 17.6, 17, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row380);

            // Row 33: SA-217, C5, Castings
            var row381 = new OldStressRowData();
            row381.LineNo = 33;
            row381.MaterialId = 8639;
            row381.SpecNo = "SA-217";
            row381.TypeGrade = "C5";
            row381.ProductForm = "Castings";
            row381.AlloyUNS = "J42045";
            row381.ClassCondition = "";
            row381.Notes = "G1, G17, G18";
            row381.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, 19.8, 19.1, 14.8, 10.8, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row381);

            // Row 34: SA-217, C5, Castings
            var row382 = new OldStressRowData();
            row382.LineNo = 34;
            row382.MaterialId = 8640;
            row382.SpecNo = "SA-217";
            row382.TypeGrade = "C5";
            row382.ProductForm = "Castings";
            row382.AlloyUNS = "J42045";
            row382.ClassCondition = "";
            row382.Notes = "G1";
            row382.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, 19.8, 19.1, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row382);

            // Row 35: SA-426, CP5, Cast pipe
            var row383 = new OldStressRowData();
            row383.LineNo = 35;
            row383.MaterialId = 8641;
            row383.SpecNo = "SA-426";
            row383.TypeGrade = "CP5";
            row383.ProductForm = "Cast pipe";
            row383.AlloyUNS = "J42045";
            row383.ClassCondition = "";
            row383.Notes = "G17";
            row383.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row383);

            // Row 36: SA-182, F5a, Forgings
            var row384 = new OldStressRowData();
            row384.LineNo = 36;
            row384.MaterialId = 8642;
            row384.SpecNo = "SA-182";
            row384.TypeGrade = "F5a";
            row384.ProductForm = "Forgings";
            row384.AlloyUNS = "K42544";
            row384.ClassCondition = "";
            row384.Notes = "G18";
            row384.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, 19.8, 19.1, 14.8, 10.8, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row384);

            // Row 37: SA-182, F5a, Forgings
            var row385 = new OldStressRowData();
            row385.LineNo = 37;
            row385.MaterialId = 8643;
            row385.SpecNo = "SA-182";
            row385.TypeGrade = "F5a";
            row385.ProductForm = "Forgings";
            row385.AlloyUNS = "K42544";
            row385.ClassCondition = "";
            row385.Notes = "";
            row385.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, 19.8, 19.1, 14.3, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row385);

            // Row 38: SA-213, T5b, Smls. tube
            var row386 = new OldStressRowData();
            row386.LineNo = 38;
            row386.MaterialId = 8644;
            row386.SpecNo = "SA-213";
            row386.TypeGrade = "T5b";
            row386.ProductForm = "Smls. tube";
            row386.AlloyUNS = "K51545";
            row386.ClassCondition = "";
            row386.Notes = "";
            row386.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row386);

            // Row 39: SA-335, P5b, Smls. pipe
            var row387 = new OldStressRowData();
            row387.LineNo = 39;
            row387.MaterialId = 8645;
            row387.SpecNo = "SA-335";
            row387.TypeGrade = "P5b";
            row387.ProductForm = "Smls. pipe";
            row387.AlloyUNS = "K51545";
            row387.ClassCondition = "";
            row387.Notes = "";
            row387.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row387);

            // Row 40: SA-213, T5c, Smls. tube
            var row388 = new OldStressRowData();
            row388.LineNo = 40;
            row388.MaterialId = 8646;
            row388.SpecNo = "SA-213";
            row388.TypeGrade = "T5c";
            row388.ProductForm = "Smls. tube";
            row388.AlloyUNS = "K41245";
            row388.ClassCondition = "";
            row388.Notes = "";
            row388.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row388);

            // Row 41: SA-335, P5c, Smls. pipe
            var row389 = new OldStressRowData();
            row389.LineNo = 41;
            row389.MaterialId = 8647;
            row389.SpecNo = "SA-335";
            row389.TypeGrade = "P5c";
            row389.ProductForm = "Smls. pipe";
            row389.AlloyUNS = "K41245";
            row389.ClassCondition = "";
            row389.Notes = "";
            row389.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 10.9, 8, 5.8, 4.2, 2.9, 1.8, 1, null, null, null, null, null, null, null, null, null };
            batch.Add(row389);

            // Row 1: SA-199, T9, Smls. tube
            var row390 = new OldStressRowData();
            row390.LineNo = 1;
            row390.MaterialId = 8648;
            row390.SpecNo = "SA-199";
            row390.TypeGrade = "T9";
            row390.ProductForm = "Smls. tube";
            row390.AlloyUNS = "K81590";
            row390.ClassCondition = "";
            row390.Notes = "";
            row390.StressValues = new double?[] { 15, null, 14.1, null, 13.6, 13.4, 13.3, 13.1, 12.9, 12.8, 12.4, 12.1, 11.6, 11, 10.3, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row390);

            // Row 2: SA-213, T9, Smls. tube
            var row391 = new OldStressRowData();
            row391.LineNo = 2;
            row391.MaterialId = 8649;
            row391.SpecNo = "SA-213";
            row391.TypeGrade = "T9";
            row391.ProductForm = "Smls. tube";
            row391.AlloyUNS = "K81590";
            row391.ClassCondition = "";
            row391.Notes = "";
            row391.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 11.4, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row391);

            // Row 3: SA-234, WP9, Fittings
            var row392 = new OldStressRowData();
            row392.LineNo = 3;
            row392.MaterialId = 8650;
            row392.SpecNo = "SA-234";
            row392.TypeGrade = "WP9";
            row392.ProductForm = "Fittings";
            row392.AlloyUNS = "K90941";
            row392.ClassCondition = "";
            row392.Notes = "G18";
            row392.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 11.4, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row392);

            // Row 4: SA-335, P9, Smls. pipe
            var row393 = new OldStressRowData();
            row393.LineNo = 4;
            row393.MaterialId = 8651;
            row393.SpecNo = "SA-335";
            row393.TypeGrade = "P9";
            row393.ProductForm = "Smls. pipe";
            row393.AlloyUNS = "K81590";
            row393.ClassCondition = "";
            row393.Notes = "";
            row393.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 11.4, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row393);

            // Row 5: SA-369, FP9, Forged pipe
            var row394 = new OldStressRowData();
            row394.LineNo = 5;
            row394.MaterialId = 8652;
            row394.SpecNo = "SA-369";
            row394.TypeGrade = "FP9";
            row394.ProductForm = "Forged pipe";
            row394.AlloyUNS = "K90941";
            row394.ClassCondition = "";
            row394.Notes = "";
            row394.StressValues = new double?[] { 15, null, 15, null, 14.5, 14.4, 14.4, 14.2, 13.9, 13.7, 13.2, 12.8, 12.1, 11.4, 10.6, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row394);

            // Row 6: SA-182, F9, Forgings
            var row395 = new OldStressRowData();
            row395.LineNo = 6;
            row395.MaterialId = 8653;
            row395.SpecNo = "SA-182";
            row395.TypeGrade = "F9";
            row395.ProductForm = "Forgings";
            row395.AlloyUNS = "K90941";
            row395.ClassCondition = "";
            row395.Notes = "G18";
            row395.StressValues = new double?[] { 21.3, null, 21.2, null, 20.6, 20.4, 20.4, 20.1, 19.9, 19.4, 18.7, 18.1, 17.1, 16.2, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row395);

            // Row 7: SA-182, F9, Forgings
            var row396 = new OldStressRowData();
            row396.LineNo = 7;
            row396.MaterialId = 8654;
            row396.SpecNo = "SA-182";
            row396.TypeGrade = "F9";
            row396.ProductForm = "Forgings";
            row396.AlloyUNS = "K90941";
            row396.ClassCondition = "";
            row396.Notes = "";
            row396.StressValues = new double?[] { 21.3, null, 21.2, null, 20.6, 20.4, 20.4, 20.1, 19.7, 19.4, 18.7, 18.1, 17.1, 16.2, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row396);

            // Row 8: SA-336, F9, Forgings
            var row397 = new OldStressRowData();
            row397.LineNo = 8;
            row397.MaterialId = 8655;
            row397.SpecNo = "SA-336";
            row397.TypeGrade = "F9";
            row397.ProductForm = "Forgings";
            row397.AlloyUNS = "K81590";
            row397.ClassCondition = "";
            row397.Notes = "";
            row397.StressValues = new double?[] { 21.3, null, 21.2, null, 20.6, 20.4, 20.4, 20.1, 19.7, 19.4, 18.7, 18.1, 17.1, 15.2, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row397);

            // Row 9: SA-217, C12, Castings
            var row398 = new OldStressRowData();
            row398.LineNo = 9;
            row398.MaterialId = 8656;
            row398.SpecNo = "SA-217";
            row398.TypeGrade = "C12";
            row398.ProductForm = "Castings";
            row398.AlloyUNS = "J82090";
            row398.ClassCondition = "";
            row398.Notes = "G1, G18";
            row398.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.3, 20.9, 20.5, 19.8, 19.1, 18.2, 16.5, 11, 7.4, 5, 3.3, 2.2, 1.5, null, null, null, null, null, null, null, null, null };
            batch.Add(row398);

            // Row 10: SA-217, C12, Castings
            var row399 = new OldStressRowData();
            row399.LineNo = 10;
            row399.MaterialId = 8657;
            row399.SpecNo = "SA-217";
            row399.TypeGrade = "C12";
            row399.ProductForm = "Castings";
            row399.AlloyUNS = "J82090";
            row399.ClassCondition = "";
            row399.Notes = "";
            row399.StressValues = new double?[] { 22.5, null, 22.4, null, 21.2, 21.2, 21.2, 21.2, 21, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row399);

            // Row 11: SA-426, CP9, Cast pipe
            var row400 = new OldStressRowData();
            row400.LineNo = 11;
            row400.MaterialId = 8658;
            row400.SpecNo = "SA-426";
            row400.TypeGrade = "CP9";
            row400.ProductForm = "Cast pipe";
            row400.AlloyUNS = "J82090";
            row400.ClassCondition = "";
            row400.Notes = "G17";
            row400.StressValues = new double?[] { 22.5, null, 22.4, null, 21.8, 21.6, 21.6, 21.6, 20.9, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
