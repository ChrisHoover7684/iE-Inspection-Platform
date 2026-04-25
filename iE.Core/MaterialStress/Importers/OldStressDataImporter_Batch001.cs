using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch001
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch001(MaterialStressService service)
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

            // Row 1: SA-620, , Sheet
            var row1 = new OldStressRowData();
            row1.LineNo = 1;
            row1.MaterialId = 8266;
            row1.SpecNo = "SA-620";
            row1.TypeGrade = "";
            row1.ProductForm = "Sheet";
            row1.AlloyUNS = "K00040";
            row1.ClassCondition = "";
            row1.Notes = "";
            row1.StressValues = new double?[] { 10, 10, 10, null, 10, 10, 10, 10, 10, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1);

            // Row 2: SA-675, 45, Bar
            var row2 = new OldStressRowData();
            row2.LineNo = 2;
            row2.MaterialId = 8268;
            row2.SpecNo = "SA-675";
            row2.TypeGrade = "45";
            row2.ProductForm = "Bar";
            row2.AlloyUNS = "";
            row2.ClassCondition = "";
            row2.Notes = "";
            row2.StressValues = new double?[] { 11.2, null, 11.2, null, 11.2, 11.2, 11.2, 11.1, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row2);

            // Row 3: SA-675, 45, Bar
            var row3 = new OldStressRowData();
            row3.LineNo = 3;
            row3.MaterialId = 8267;
            row3.SpecNo = "SA-675";
            row3.TypeGrade = "45";
            row3.ProductForm = "Bar";
            row3.AlloyUNS = "";
            row3.ClassCondition = "";
            row3.Notes = "G10, G22, G35";
            row3.StressValues = new double?[] { 11.3, 11.3, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11, 10.2, 9, 7.8, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row3);

            // Row 4: SA-134, A283A, Wld. pipe
            var row4 = new OldStressRowData();
            row4.LineNo = 4;
            row4.MaterialId = 8269;
            row4.SpecNo = "SA-134";
            row4.TypeGrade = "A283A";
            row4.ProductForm = "Wld. pipe";
            row4.AlloyUNS = "";
            row4.ClassCondition = "";
            row4.Notes = "G37, W12";
            row4.StressValues = new double?[] { 11.3, null, 11.3, null, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row4);

            // Row 5: SA-283, A, Plate
            var row5 = new OldStressRowData();
            row5.LineNo = 5;
            row5.MaterialId = 8270;
            row5.SpecNo = "SA-283";
            row5.TypeGrade = "A";
            row5.ProductForm = "Plate";
            row5.AlloyUNS = "";
            row5.ClassCondition = "";
            row5.Notes = "G34, G37";
            row5.StressValues = new double?[] { 11.3, 11.3, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row5);

            // Row 6: SA-285, A, Plate
            var row6 = new OldStressRowData();
            row6.LineNo = 6;
            row6.MaterialId = 8271;
            row6.SpecNo = "SA-285";
            row6.TypeGrade = "A";
            row6.ProductForm = "Plate";
            row6.AlloyUNS = "K01700";
            row6.ClassCondition = "";
            row6.Notes = "G10";
            row6.StressValues = new double?[] { 11.3, null, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11, 9.7, 8.3, 6.6, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row6);

            // Row 7: SA-285, A, Plate
            var row7 = new OldStressRowData();
            row7.LineNo = 7;
            row7.MaterialId = 8272;
            row7.SpecNo = "SA-285";
            row7.TypeGrade = "A";
            row7.ProductForm = "Plate";
            row7.AlloyUNS = "K01700";
            row7.ClassCondition = "";
            row7.Notes = "G10, G35";
            row7.StressValues = new double?[] { 11.3, 11.3, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11, 10.3, 9, 7.8, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row7);

            // Row 8: SA-672, A45, Wld. pipe
            var row8 = new OldStressRowData();
            row8.LineNo = 8;
            row8.MaterialId = 8273;
            row8.SpecNo = "SA-672";
            row8.TypeGrade = "A45";
            row8.ProductForm = "Wld. pipe";
            row8.AlloyUNS = "K01700";
            row8.ClassCondition = "";
            row8.Notes = "S6, W10, W12";
            row8.StressValues = new double?[] { 11.3, null, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row8);

            // Row 9: SA-414, A, Sheet
            var row9 = new OldStressRowData();
            row9.LineNo = 9;
            row9.MaterialId = 8275;
            row9.SpecNo = "SA-414";
            row9.TypeGrade = "A";
            row9.ProductForm = "Sheet";
            row9.AlloyUNS = "K01501";
            row9.ClassCondition = "";
            row9.Notes = "G10, G35";
            row9.StressValues = new double?[] { 11.3, 11.3, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11, 10.3, 9, 7.8, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row9);

            // Row 10: SA-178, A, Wld. tube
            var row10 = new OldStressRowData();
            row10.LineNo = 10;
            row10.MaterialId = 8276;
            row10.SpecNo = "SA-178";
            row10.TypeGrade = "A";
            row10.ProductForm = "Wld. tube";
            row10.AlloyUNS = "K01200";
            row10.ClassCondition = "";
            row10.Notes = "G10, S1, W14";
            row10.StressValues = new double?[] { 11.8, null, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.7, 9, 7.1, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row10);

            // Row 11: SA-178, A, Wld. tube
            var row11 = new OldStressRowData();
            row11.LineNo = 11;
            row11.MaterialId = 8277;
            row11.SpecNo = "SA-178";
            row11.TypeGrade = "A";
            row11.ProductForm = "Wld. tube";
            row11.AlloyUNS = "K01200";
            row11.ClassCondition = "";
            row11.Notes = "G4, G10, S1";
            row11.StressValues = new double?[] { 11.8, null, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.7, 9, 7.1, 4.3, 2.6, 1.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row11);

            // Row 12: SA-178, A, Wld. tube
            var row12 = new OldStressRowData();
            row12.LineNo = 12;
            row12.MaterialId = 8278;
            row12.SpecNo = "SA-178";
            row12.TypeGrade = "A";
            row12.ProductForm = "Wld. tube";
            row12.AlloyUNS = "K01200";
            row12.ClassCondition = "";
            row12.Notes = "G3, G10, S1";
            row12.StressValues = new double?[] { 10, null, 10, null, 10, 10, 10, 10, 10, 9.8, 9.1, 7.7, 6.1, 4.3, 2.6, 1.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row12);

            // Row 13: SA-178, A, Wld. tube
            var row13 = new OldStressRowData();
            row13.LineNo = 13;
            row13.MaterialId = 8279;
            row13.SpecNo = "SA-178";
            row13.TypeGrade = "A";
            row13.ProductForm = "Wld. tube";
            row13.AlloyUNS = "K01200";
            row13.ClassCondition = "";
            row13.Notes = "G24, G35, W6";
            row13.StressValues = new double?[] { 10, 10, 10, null, 10, 10, 10, 10, 10, 9.8, 9.1, 7.8, 6.7, 5.5, 3.8, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row13);

            // Row 14: SA-179, , Smls. tube
            var row14 = new OldStressRowData();
            row14.LineNo = 14;
            row14.MaterialId = 8280;
            row14.SpecNo = "SA-179";
            row14.TypeGrade = "";
            row14.ProductForm = "Smls. tube";
            row14.AlloyUNS = "K01200";
            row14.ClassCondition = "";
            row14.Notes = "G10, G35";
            row14.StressValues = new double?[] { 11.8, 11.8, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.6, 9.2, 7.9, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row14);

            // Row 15: SA-192, , Smls. tube
            var row15 = new OldStressRowData();
            row15.LineNo = 15;
            row15.MaterialId = 8282;
            row15.SpecNo = "SA-192";
            row15.TypeGrade = "";
            row15.ProductForm = "Smls. tube";
            row15.AlloyUNS = "K01201";
            row15.ClassCondition = "";
            row15.Notes = "G10, S1";
            row15.StressValues = new double?[] { 11.8, null, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.7, 9, 7.1, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row15);

            // Row 16: SA-192, , Smls. tube
            var row16 = new OldStressRowData();
            row16.LineNo = 16;
            row16.MaterialId = 8281;
            row16.SpecNo = "SA-192";
            row16.TypeGrade = "";
            row16.ProductForm = "Smls. tube";
            row16.AlloyUNS = "K01201";
            row16.ClassCondition = "";
            row16.Notes = "G10";
            row16.StressValues = new double?[] { 11.8, 11.8, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.6, 9.2, 7.9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row16);

            // Row 17: SA-214, , Wld. tube
            var row17 = new OldStressRowData();
            row17.LineNo = 17;
            row17.MaterialId = 8283;
            row17.SpecNo = "SA-214";
            row17.TypeGrade = "";
            row17.ProductForm = "Wld. tube";
            row17.AlloyUNS = "K01807";
            row17.ClassCondition = "";
            row17.Notes = "G24, G35, W6";
            row17.StressValues = new double?[] { 10, 10, 10, null, 10, 10, 10, 10, 10, 9.8, 9.1, 7.8, 6.7, 5.5, 3.8, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row17);

            // Row 18: SA-226, , Wld. tube
            var row18 = new OldStressRowData();
            row18.LineNo = 18;
            row18.MaterialId = 8284;
            row18.SpecNo = "SA-226";
            row18.TypeGrade = "";
            row18.ProductForm = "Wld. tube";
            row18.AlloyUNS = "K01201";
            row18.ClassCondition = "";
            row18.Notes = "G10, S1, W14";
            row18.StressValues = new double?[] { 11.8, null, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.7, 9, 7.1, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row18);

            // Row 19: SA-226, , Wld. tube
            var row19 = new OldStressRowData();
            row19.LineNo = 19;
            row19.MaterialId = 8285;
            row19.SpecNo = "SA-226";
            row19.TypeGrade = "";
            row19.ProductForm = "Wld. tube";
            row19.AlloyUNS = "K01201";
            row19.ClassCondition = "";
            row19.Notes = "G4, G10, S1";
            row19.StressValues = new double?[] { 11.8, null, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.7, 9, 7.1, 4.3, 2.6, 1.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row19);

            // Row 20: SA-226, , Wld. tube
            var row20 = new OldStressRowData();
            row20.LineNo = 20;
            row20.MaterialId = 8286;
            row20.SpecNo = "SA-226";
            row20.TypeGrade = "";
            row20.ProductForm = "Wld. tube";
            row20.AlloyUNS = "K01201";
            row20.ClassCondition = "";
            row20.Notes = "G3, G10, S1";
            row20.StressValues = new double?[] { 10, null, 10, null, 10, 10, 10, 10, 10, 9.8, 9.1, 7.7, 6.1, 4.3, 2.6, 1.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row20);

            // Row 21: SA-226, , Wld. tube
            var row21 = new OldStressRowData();
            row21.LineNo = 21;
            row21.MaterialId = 8287;
            row21.SpecNo = "SA-226";
            row21.TypeGrade = "";
            row21.ProductForm = "Wld. tube";
            row21.AlloyUNS = "K01201";
            row21.ClassCondition = "";
            row21.Notes = "G24, G35, W6";
            row21.StressValues = new double?[] { 10, 10, 10, null, 10, 10, 10, 10, 10, 9.8, 9.1, 7.8, 6.7, 5.5, 3.8, 2.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row21);

            // Row 22: SA-556, A2, Smls. tube
            var row22 = new OldStressRowData();
            row22.LineNo = 22;
            row22.MaterialId = 8288;
            row22.SpecNo = "SA-556";
            row22.TypeGrade = "A2";
            row22.ProductForm = "Smls. tube";
            row22.AlloyUNS = "K01807";
            row22.ClassCondition = "";
            row22.Notes = "G10";
            row22.StressValues = new double?[] { 11.8, 11.8, 11.8, null, 11.8, 11.8, 11.8, 11.8, 11.8, 11.5, 10.6, 9.2, 7.9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row22);

            // Row 23: SA-557, A2, Wld. tube
            var row23 = new OldStressRowData();
            row23.LineNo = 23;
            row23.MaterialId = 8289;
            row23.SpecNo = "SA-557";
            row23.TypeGrade = "A2";
            row23.ProductForm = "Wld. tube";
            row23.AlloyUNS = "K01807";
            row23.ClassCondition = "";
            row23.Notes = "G24, G35, W6";
            row23.StressValues = new double?[] { 10, 10, 10, null, 10, 10, 10, 10, 10, 9.8, 8.3, 7.8, 6.7, 4.3, 2.6, 1.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row23);

            // Row 24: SA-53, E/A, Wld. pipe
            var row24 = new OldStressRowData();
            row24.LineNo = 24;
            row24.MaterialId = 8292;
            row24.SpecNo = "SA-53";
            row24.TypeGrade = "E/A";
            row24.ProductForm = "Wld. pipe";
            row24.AlloyUNS = "K02504";
            row24.ClassCondition = "";
            row24.Notes = "G3, G10, S1";
            row24.StressValues = new double?[] { 10.2, null, 10.2, null, 10.2, 10.2, 10.2, 10.2, 10.2, 9.9, 9.1, 7.7, 6.1, 4.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row24);

            // Row 25: SA-53, E/A, Wld. pipe
            var row25 = new OldStressRowData();
            row25.LineNo = 25;
            row25.MaterialId = 8291;
            row25.SpecNo = "SA-53";
            row25.TypeGrade = "E/A";
            row25.ProductForm = "Wld. pipe";
            row25.AlloyUNS = "K02504";
            row25.ClassCondition = "";
            row25.Notes = "G10, S1, W12, W14";
            row25.StressValues = new double?[] { 12, null, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9, 7.1, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row25);

            // Row 26: SA-53, E/A, Wld. pipe
            var row26 = new OldStressRowData();
            row26.LineNo = 26;
            row26.MaterialId = 8294;
            row26.SpecNo = "SA-53";
            row26.TypeGrade = "E/A";
            row26.ProductForm = "Wld. pipe";
            row26.AlloyUNS = "K02504";
            row26.ClassCondition = "";
            row26.Notes = "G24, G35, W6";
            row26.StressValues = new double?[] { 10.2, 10.2, 10.2, null, 10.2, 10.2, 10.2, 10.2, 10.2, 9.9, 9.1, 7.9, 6.7, 5.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row26);

            // Row 27: SA-53, F, Wld. pipe
            var row27 = new OldStressRowData();
            row27.LineNo = 27;
            row27.MaterialId = 8274;
            row27.SpecNo = "SA-53";
            row27.TypeGrade = "F";
            row27.ProductForm = "Wld. pipe";
            row27.AlloyUNS = "";
            row27.ClassCondition = "";
            row27.Notes = "G2, G10, G18";
            row27.StressValues = new double?[] { 7.2, null, 7.2, null, 7.2, 7.2, 7.2, 7.2, 7.2, 7, 6.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row27);

            // Row 28: SA-53, S/A, Smls. pipe
            var row28 = new OldStressRowData();
            row28.LineNo = 28;
            row28.MaterialId = 8290;
            row28.SpecNo = "SA-53";
            row28.TypeGrade = "S/A";
            row28.ProductForm = "Smls. pipe";
            row28.AlloyUNS = "K02504";
            row28.ClassCondition = "";
            row28.Notes = "G10, S1";
            row28.StressValues = new double?[] { 12, null, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9, 7.1, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row28);

            // Row 29: SA-53, S/A, Smls. pipe
            var row29 = new OldStressRowData();
            row29.LineNo = 29;
            row29.MaterialId = 8293;
            row29.SpecNo = "SA-53";
            row29.TypeGrade = "S/A";
            row29.ProductForm = "Smls. pipe";
            row29.AlloyUNS = "K02504";
            row29.ClassCondition = "";
            row29.Notes = "G10, G35";
            row29.StressValues = new double?[] { 12, 12, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9.3, 7.9, 6.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row29);

            // Row 30: SA-106, A, Smls. pipe
            var row30 = new OldStressRowData();
            row30.LineNo = 30;
            row30.MaterialId = 8295;
            row30.SpecNo = "SA-106";
            row30.TypeGrade = "A";
            row30.ProductForm = "Smls. pipe";
            row30.AlloyUNS = "K02501";
            row30.ClassCondition = "";
            row30.Notes = "G10, S1";
            row30.StressValues = new double?[] { 12, null, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9, 7.1, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row30);

            // Row 31: SA-106, A, Smls. pipe
            var row31 = new OldStressRowData();
            row31.LineNo = 31;
            row31.MaterialId = 8297;
            row31.SpecNo = "SA-106";
            row31.TypeGrade = "A";
            row31.ProductForm = "Smls. pipe";
            row31.AlloyUNS = "K02501";
            row31.ClassCondition = "";
            row31.Notes = "";
            row31.StressValues = new double?[] { 12, null, 12, null, 12, 12, 12, 12, 12, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row31);

            // Row 32: SA-106, A, Smls. pipe
            var row32 = new OldStressRowData();
            row32.LineNo = 32;
            row32.MaterialId = 8296;
            row32.SpecNo = "SA-106";
            row32.TypeGrade = "A";
            row32.ProductForm = "Smls. pipe";
            row32.AlloyUNS = "K02501";
            row32.ClassCondition = "";
            row32.Notes = "G10";
            row32.StressValues = new double?[] { 12, 12, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9.3, 7.9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row32);

            // Row 33: SA-135, A, Wld. pipe
            var row33 = new OldStressRowData();
            row33.LineNo = 33;
            row33.MaterialId = 8298;
            row33.SpecNo = "SA-135";
            row33.TypeGrade = "A";
            row33.ProductForm = "Wld. pipe";
            row33.AlloyUNS = "";
            row33.ClassCondition = "";
            row33.Notes = "G24, G35, W6";
            row33.StressValues = new double?[] { 10.2, 10.2, 10.2, null, 10.2, 10.2, 10.2, 10.2, 10.2, 9.9, 9.1, 7.9, 6.7, 5.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row33);

            // Row 34: SA-369, FPA, Forged pipe
            var row34 = new OldStressRowData();
            row34.LineNo = 34;
            row34.MaterialId = 8299;
            row34.SpecNo = "SA-369";
            row34.TypeGrade = "FPA";
            row34.ProductForm = "Forged pipe";
            row34.AlloyUNS = "K02501";
            row34.ClassCondition = "";
            row34.Notes = "G10, S1";
            row34.StressValues = new double?[] { 12, null, 12, null, 12, 12, 12, 12, 12, 11.7, 10.7, 9, 7.1, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row34);

            // Row 35: SA-587, , Wld. pipe
            var row35 = new OldStressRowData();
            row35.LineNo = 35;
            row35.MaterialId = 8301;
            row35.SpecNo = "SA-587";
            row35.TypeGrade = "";
            row35.ProductForm = "Wld. pipe";
            row35.AlloyUNS = "K11500";
            row35.ClassCondition = "";
            row35.Notes = "G37";
            row35.StressValues = new double?[] { 12, null, 12, null, 12, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row35);

            // Row 36: SA-587, , Wld. pipe
            var row36 = new OldStressRowData();
            row36.LineNo = 36;
            row36.MaterialId = 8300;
            row36.SpecNo = "SA-587";
            row36.TypeGrade = "";
            row36.ProductForm = "Wld. pipe";
            row36.AlloyUNS = "K11500";
            row36.ClassCondition = "";
            row36.Notes = "G24, W6";
            row36.StressValues = new double?[] { 10.2, 10.2, 10.2, null, 10.2, 10.2, 10.2, 10.2, 10.2, 9.9, 9.1, 7.9, 6.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row36);

            // Row 37: SA-675, 50, Bar
            var row37 = new OldStressRowData();
            row37.LineNo = 37;
            row37.MaterialId = 8302;
            row37.SpecNo = "SA-675";
            row37.TypeGrade = "50";
            row37.ProductForm = "Bar";
            row37.AlloyUNS = "";
            row37.ClassCondition = "";
            row37.Notes = "G10, G15, G18, S1";
            row37.StressValues = new double?[] { 12.5, null, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, 11, 9.4, 7.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row37);

            // Row 38: SA-675, 50, Bar
            var row38 = new OldStressRowData();
            row38.LineNo = 38;
            row38.MaterialId = 8304;
            row38.SpecNo = "SA-675";
            row38.TypeGrade = "50";
            row38.ProductForm = "Bar";
            row38.AlloyUNS = "";
            row38.ClassCondition = "";
            row38.Notes = "";
            row38.StressValues = new double?[] { 12.5, null, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row38);

            // Row 39: SA-675, 50, Bar
            var row39 = new OldStressRowData();
            row39.LineNo = 39;
            row39.MaterialId = 8303;
            row39.SpecNo = "SA-675";
            row39.TypeGrade = "50";
            row39.ProductForm = "Bar";
            row39.AlloyUNS = "";
            row39.ClassCondition = "";
            row39.Notes = "G10, G22, G35";
            row39.StressValues = new double?[] { 12.5, 12.5, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, 11, 9.4, 7.8, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row39);

            // Row 1: SA-134, A283B, Wld. pipe
            var row40 = new OldStressRowData();
            row40.LineNo = 1;
            row40.MaterialId = 8305;
            row40.SpecNo = "SA-134";
            row40.TypeGrade = "A283B";
            row40.ProductForm = "Wld. pipe";
            row40.AlloyUNS = "";
            row40.ClassCondition = "";
            row40.Notes = "G37, W12";
            row40.StressValues = new double?[] { 12.5, null, 12.5, null, 12.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row40);

            // Row 2: SA-283, B, Plate
            var row41 = new OldStressRowData();
            row41.LineNo = 2;
            row41.MaterialId = 8306;
            row41.SpecNo = "SA-283";
            row41.TypeGrade = "B";
            row41.ProductForm = "Plate";
            row41.AlloyUNS = "";
            row41.ClassCondition = "";
            row41.Notes = "G34, G37";
            row41.StressValues = new double?[] { 12.5, 12.5, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row41);

            // Row 3: SA-285, B, Plate
            var row42 = new OldStressRowData();
            row42.LineNo = 3;
            row42.MaterialId = 8307;
            row42.SpecNo = "SA-285";
            row42.TypeGrade = "B";
            row42.ProductForm = "Plate";
            row42.AlloyUNS = "K02200";
            row42.ClassCondition = "";
            row42.Notes = "G10, S1";
            row42.StressValues = new double?[] { 12.5, null, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, 11, 9.4, 7.3, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row42);

            // Row 4: SA-285, B, Plate
            var row43 = new OldStressRowData();
            row43.LineNo = 4;
            row43.MaterialId = 8308;
            row43.SpecNo = "SA-285";
            row43.TypeGrade = "B";
            row43.ProductForm = "Plate";
            row43.AlloyUNS = "K02200";
            row43.ClassCondition = "";
            row43.Notes = "G10, G35";
            row43.StressValues = new double?[] { 12.5, 12.5, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, 11.2, 9.6, 8.1, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row43);

            // Row 5: SA-672, A50, Wld. pipe
            var row44 = new OldStressRowData();
            row44.LineNo = 5;
            row44.MaterialId = 8309;
            row44.SpecNo = "SA-672";
            row44.TypeGrade = "A50";
            row44.ProductForm = "Wld. pipe";
            row44.AlloyUNS = "K02200";
            row44.ClassCondition = "";
            row44.Notes = "S6, W10, W12";
            row44.StressValues = new double?[] { 12.5, null, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row44);

            // Row 6: SA-414, B, Sheet
            var row45 = new OldStressRowData();
            row45.LineNo = 6;
            row45.MaterialId = 8310;
            row45.SpecNo = "SA-414";
            row45.TypeGrade = "B";
            row45.ProductForm = "Sheet";
            row45.AlloyUNS = "K02201";
            row45.ClassCondition = "";
            row45.Notes = "G10, G35";
            row45.StressValues = new double?[] { 12.5, 12.5, 12.5, null, 12.5, 12.5, 12.5, 12.5, 12.5, 12.1, 11.2, 9.6, 8.1, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row45);

            // Row 7: SA-675, 55, Bar
            var row46 = new OldStressRowData();
            row46.LineNo = 7;
            row46.MaterialId = 8311;
            row46.SpecNo = "SA-675";
            row46.TypeGrade = "55";
            row46.ProductForm = "Bar";
            row46.AlloyUNS = "";
            row46.ClassCondition = "";
            row46.Notes = "G10, G15, G18, G22, S1";
            row46.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 12.1, 10.2, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row46);

            // Row 8: SA-675, 55, Bar
            var row47 = new OldStressRowData();
            row47.LineNo = 8;
            row47.MaterialId = 8312;
            row47.SpecNo = "SA-675";
            row47.TypeGrade = "55";
            row47.ProductForm = "Bar";
            row47.AlloyUNS = "";
            row47.ClassCondition = "";
            row47.Notes = "";
            row47.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row47);

            // Row 9: SA-134, A283C, Wld. pipe
            var row48 = new OldStressRowData();
            row48.LineNo = 9;
            row48.MaterialId = 8313;
            row48.SpecNo = "SA-134";
            row48.TypeGrade = "A283C";
            row48.ProductForm = "Wld. pipe";
            row48.AlloyUNS = "K02401";
            row48.ClassCondition = "";
            row48.Notes = "G37, W12";
            row48.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row48);

            // Row 10: SA-283, C, Plate
            var row49 = new OldStressRowData();
            row49.LineNo = 10;
            row49.MaterialId = 8314;
            row49.SpecNo = "SA-283";
            row49.TypeGrade = "C";
            row49.ProductForm = "Plate";
            row49.AlloyUNS = "K02401";
            row49.ClassCondition = "";
            row49.Notes = "G34, G37";
            row49.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row49);

            // Row 11: SA-285, C, Plate
            var row50 = new OldStressRowData();
            row50.LineNo = 11;
            row50.MaterialId = 8315;
            row50.SpecNo = "SA-285";
            row50.TypeGrade = "C";
            row50.ProductForm = "Plate";
            row50.AlloyUNS = "K02801";
            row50.ClassCondition = "";
            row50.Notes = "G10, G35, S1";
            row50.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 12.1, 10.2, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row50);

            // Row 12: SA-333, 1, Wld. & smls. pipe
            var row51 = new OldStressRowData();
            row51.LineNo = 12;
            row51.MaterialId = 8316;
            row51.SpecNo = "SA-333";
            row51.TypeGrade = "1";
            row51.ProductForm = "Wld. & smls. pipe";
            row51.AlloyUNS = "K03008";
            row51.ClassCondition = "";
            row51.Notes = "W12";
            row51.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row51);

            // Row 13: SA-334, 1, Wld. & smls. tube
            var row52 = new OldStressRowData();
            row52.LineNo = 13;
            row52.MaterialId = 8318;
            row52.SpecNo = "SA-334";
            row52.TypeGrade = "1";
            row52.ProductForm = "Wld. & smls. tube";
            row52.AlloyUNS = "K03008";
            row52.ClassCondition = "";
            row52.Notes = "W12";
            row52.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row52);

            // Row 14: SA-334, 1, Wld. tube
            var row53 = new OldStressRowData();
            row53.LineNo = 14;
            row53.MaterialId = 8317;
            row53.SpecNo = "SA-334";
            row53.TypeGrade = "1";
            row53.ProductForm = "Wld. tube";
            row53.AlloyUNS = "K03008";
            row53.ClassCondition = "";
            row53.Notes = "G24, W6";
            row53.StressValues = new double?[] { 11.7, 11.7, 11.7, null, 11.7, 11.7, 11.7, 11.7, 11.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row53);

            // Row 15: SA-516, 55, Plate
            var row54 = new OldStressRowData();
            row54.LineNo = 15;
            row54.MaterialId = 8319;
            row54.SpecNo = "SA-516";
            row54.TypeGrade = "55";
            row54.ProductForm = "Plate";
            row54.AlloyUNS = "K01800";
            row54.ClassCondition = "";
            row54.Notes = "G10, S1";
            row54.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 12.1, 10.2, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row54);

            // Row 16: SA-524, II, Smls. pipe
            var row55 = new OldStressRowData();
            row55.LineNo = 16;
            row55.MaterialId = 8320;
            row55.SpecNo = "SA-524";
            row55.TypeGrade = "II";
            row55.ProductForm = "Smls. pipe";
            row55.AlloyUNS = "K02104";
            row55.ClassCondition = "";
            row55.Notes = "G10";
            row55.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 12.1, 10.2, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row55);

            // Row 17: SA-671, CA55, Wld. pipe
            var row56 = new OldStressRowData();
            row56.LineNo = 17;
            row56.MaterialId = 8321;
            row56.SpecNo = "SA-671";
            row56.TypeGrade = "CA55";
            row56.ProductForm = "Wld. pipe";
            row56.AlloyUNS = "K02801";
            row56.ClassCondition = "";
            row56.Notes = "S6, W10, W12";
            row56.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row56);

            // Row 18: SA-671, CE55, Wld. pipe
            var row57 = new OldStressRowData();
            row57.LineNo = 18;
            row57.MaterialId = 8322;
            row57.SpecNo = "SA-671";
            row57.TypeGrade = "CE55";
            row57.ProductForm = "Wld. pipe";
            row57.AlloyUNS = "K02202";
            row57.ClassCondition = "";
            row57.Notes = "S6, W10, W12";
            row57.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row57);

            // Row 19: SA-672, A55, Wld. pipe
            var row58 = new OldStressRowData();
            row58.LineNo = 19;
            row58.MaterialId = 8323;
            row58.SpecNo = "SA-672";
            row58.TypeGrade = "A55";
            row58.ProductForm = "Wld. pipe";
            row58.AlloyUNS = "K02801";
            row58.ClassCondition = "";
            row58.Notes = "S6, W10, W12";
            row58.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row58);

            // Row 20: SA-672, B55, Wld. pipe
            var row59 = new OldStressRowData();
            row59.LineNo = 20;
            row59.MaterialId = 8324;
            row59.SpecNo = "SA-672";
            row59.TypeGrade = "B55";
            row59.ProductForm = "Wld. pipe";
            row59.AlloyUNS = "K02001";
            row59.ClassCondition = "";
            row59.Notes = "S6, W10, W12";
            row59.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row59);

            // Row 21: SA-672, C55, Wld. pipe
            var row60 = new OldStressRowData();
            row60.LineNo = 21;
            row60.MaterialId = 8325;
            row60.SpecNo = "SA-672";
            row60.TypeGrade = "C55";
            row60.ProductForm = "Wld. pipe";
            row60.AlloyUNS = "K01800";
            row60.ClassCondition = "";
            row60.Notes = "S6, W10, W12";
            row60.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row60);

            // Row 22: SA-672, E55, Wld. pipe
            var row61 = new OldStressRowData();
            row61.LineNo = 22;
            row61.MaterialId = 8326;
            row61.SpecNo = "SA-672";
            row61.TypeGrade = "E55";
            row61.ProductForm = "Wld. pipe";
            row61.AlloyUNS = "K02202";
            row61.ClassCondition = "";
            row61.Notes = "S6, W10, W12";
            row61.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row61);

            // Row 23: SA-414, C, Sheet
            var row62 = new OldStressRowData();
            row62.LineNo = 23;
            row62.MaterialId = 8327;
            row62.SpecNo = "SA-414";
            row62.TypeGrade = "C";
            row62.ProductForm = "Sheet";
            row62.AlloyUNS = "K02503";
            row62.ClassCondition = "";
            row62.Notes = "G10, G35";
            row62.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 12.1, 10.2, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row62);

            // Row 24: SA-36, , Bar
            var row63 = new OldStressRowData();
            row63.LineNo = 24;
            row63.MaterialId = 8328;
            row63.SpecNo = "SA-36";
            row63.TypeGrade = "";
            row63.ProductForm = "Bar";
            row63.AlloyUNS = "K02600";
            row63.ClassCondition = "";
            row63.Notes = "G10, G15, G18";
            row63.StressValues = new double?[] { 13.3, null, 13.3, null, 13.3, 13.3, 13.3, 13.3, 13.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row63);

            // Row 25: SA-36, , Bar
            var row64 = new OldStressRowData();
            row64.LineNo = 25;
            row64.MaterialId = 8329;
            row64.SpecNo = "SA-36";
            row64.TypeGrade = "";
            row64.ProductForm = "Bar";
            row64.AlloyUNS = "K02600";
            row64.ClassCondition = "";
            row64.Notes = "G10, G35";
            row64.StressValues = new double?[] { 14.5, 14.5, 14.5, null, 14.5, 14.5, 14.5, 14.5, 14.5, 13.9, 12.6, 10.5, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row64);

            // Row 26: SA-36, , Plate, sheet
            var row65 = new OldStressRowData();
            row65.LineNo = 26;
            row65.MaterialId = 8330;
            row65.SpecNo = "SA-36";
            row65.TypeGrade = "";
            row65.ProductForm = "Plate, sheet";
            row65.AlloyUNS = "K02600";
            row65.ClassCondition = "";
            row65.Notes = "G10, G34, G35, G36";
            row65.StressValues = new double?[] { 14.5, null, 14.5, null, 14.5, 14.5, 14.5, 14.5, 14.5, 13.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row65);

            // Row 27: SA-662, A, Plate, sheet
            var row66 = new OldStressRowData();
            row66.LineNo = 27;
            row66.MaterialId = 8331;
            row66.SpecNo = "SA-662";
            row66.TypeGrade = "A";
            row66.ProductForm = "Plate, sheet";
            row66.AlloyUNS = "K01701";
            row66.ClassCondition = "";
            row66.Notes = "";
            row66.StressValues = new double?[] { 14.5, 14.5, 14.5, null, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row66);

            // Row 28: SA-181, , Forgings
            var row67 = new OldStressRowData();
            row67.LineNo = 28;
            row67.MaterialId = 8332;
            row67.SpecNo = "SA-181";
            row67.TypeGrade = "";
            row67.ProductForm = "Forgings";
            row67.AlloyUNS = "K03502";
            row67.ClassCondition = "60";
            row67.Notes = "G10, G18, G35, S1";
            row67.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row67);

            // Row 29: SA-216, WCA, Castings
            var row68 = new OldStressRowData();
            row68.LineNo = 29;
            row68.MaterialId = 8333;
            row68.SpecNo = "SA-216";
            row68.TypeGrade = "WCA";
            row68.ProductForm = "Castings";
            row68.AlloyUNS = "J02502";
            row68.ClassCondition = "";
            row68.Notes = "G1, G10, G17, G18, S1";
            row68.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row68);

            // Row 30: SA-266, 1, Forgings
            var row69 = new OldStressRowData();
            row69.LineNo = 30;
            row69.MaterialId = 8334;
            row69.SpecNo = "SA-266";
            row69.TypeGrade = "1";
            row69.ProductForm = "Forgings";
            row69.AlloyUNS = "K03506";
            row69.ClassCondition = "";
            row69.Notes = "G10, G18, S1";
            row69.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row69);

            // Row 31: SA-350, LF1, Forgings
            var row70 = new OldStressRowData();
            row70.LineNo = 31;
            row70.MaterialId = 8335;
            row70.SpecNo = "SA-350";
            row70.TypeGrade = "LF1";
            row70.ProductForm = "Forgings";
            row70.AlloyUNS = "K03009";
            row70.ClassCondition = "1";
            row70.Notes = "G10";
            row70.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row70);

            // Row 32: SA-352, LCA, Castings
            var row71 = new OldStressRowData();
            row71.LineNo = 32;
            row71.MaterialId = 8336;
            row71.SpecNo = "SA-352";
            row71.TypeGrade = "LCA";
            row71.ProductForm = "Castings";
            row71.AlloyUNS = "J02504";
            row71.ClassCondition = "";
            row71.Notes = "G17";
            row71.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row71);

            // Row 33: SA-660, WCA, Cast pipe
            var row72 = new OldStressRowData();
            row72.LineNo = 33;
            row72.MaterialId = 8337;
            row72.SpecNo = "SA-660";
            row72.TypeGrade = "WCA";
            row72.ProductForm = "Cast pipe";
            row72.AlloyUNS = "J02504";
            row72.ClassCondition = "";
            row72.Notes = "G1, G10, G17, G18, S1";
            row72.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row72);

            // Row 34: SA-675, 60, Bar
            var row73 = new OldStressRowData();
            row73.LineNo = 34;
            row73.MaterialId = 8338;
            row73.SpecNo = "SA-675";
            row73.TypeGrade = "60";
            row73.ProductForm = "Bar";
            row73.AlloyUNS = "";
            row73.ClassCondition = "";
            row73.Notes = "G10, G15, G18, S1";
            row73.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row73);

            // Row 35: SA-675, 60, Bar
            var row74 = new OldStressRowData();
            row74.LineNo = 35;
            row74.MaterialId = 8339;
            row74.SpecNo = "SA-675";
            row74.TypeGrade = "60";
            row74.ProductForm = "Bar";
            row74.AlloyUNS = "";
            row74.ClassCondition = "";
            row74.Notes = "G10, G22, G35";
            row74.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row74);

            // Row 36: SA-765, I, Forgings
            var row75 = new OldStressRowData();
            row75.LineNo = 36;
            row75.MaterialId = 8340;
            row75.SpecNo = "SA-765";
            row75.TypeGrade = "I";
            row75.ProductForm = "Forgings";
            row75.AlloyUNS = "K03046";
            row75.ClassCondition = "";
            row75.Notes = "";
            row75.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row75);

            // Row 1: SA-515, 60, Plate
            var row76 = new OldStressRowData();
            row76.LineNo = 1;
            row76.MaterialId = 8341;
            row76.SpecNo = "SA-515";
            row76.TypeGrade = "60";
            row76.ProductForm = "Plate";
            row76.AlloyUNS = "K02401";
            row76.ClassCondition = "";
            row76.Notes = "G10, S1";
            row76.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row76);

            // Row 2: SA-516, 60, Plate
            var row77 = new OldStressRowData();
            row77.LineNo = 2;
            row77.MaterialId = 8342;
            row77.SpecNo = "SA-516";
            row77.TypeGrade = "60";
            row77.ProductForm = "Plate";
            row77.AlloyUNS = "K02100";
            row77.ClassCondition = "";
            row77.Notes = "G10, S1";
            row77.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row77);

            // Row 3: SA-671, CB60, Wld. pipe
            var row78 = new OldStressRowData();
            row78.LineNo = 3;
            row78.MaterialId = 8343;
            row78.SpecNo = "SA-671";
            row78.TypeGrade = "CB60";
            row78.ProductForm = "Wld. pipe";
            row78.AlloyUNS = "K02401";
            row78.ClassCondition = "";
            row78.Notes = "S6, W10, W12";
            row78.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row78);

            // Row 4: SA-671, CC60, Wld. pipe
            var row79 = new OldStressRowData();
            row79.LineNo = 4;
            row79.MaterialId = 8344;
            row79.SpecNo = "SA-671";
            row79.TypeGrade = "CC60";
            row79.ProductForm = "Wld. pipe";
            row79.AlloyUNS = "K02100";
            row79.ClassCondition = "";
            row79.Notes = "S6, W10, W12";
            row79.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row79);

            // Row 5: SA-671, CE60, Wld. pipe
            var row80 = new OldStressRowData();
            row80.LineNo = 5;
            row80.MaterialId = 8345;
            row80.SpecNo = "SA-671";
            row80.TypeGrade = "CE60";
            row80.ProductForm = "Wld. pipe";
            row80.AlloyUNS = "K02402";
            row80.ClassCondition = "";
            row80.Notes = "S6, W10, W12";
            row80.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row80);

            // Row 6: SA-672, B60, Wld. pipe
            var row81 = new OldStressRowData();
            row81.LineNo = 6;
            row81.MaterialId = 8346;
            row81.SpecNo = "SA-672";
            row81.TypeGrade = "B60";
            row81.ProductForm = "Wld. pipe";
            row81.AlloyUNS = "K02401";
            row81.ClassCondition = "";
            row81.Notes = "S6, W10, W12";
            row81.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row81);

            // Row 7: SA-672, C60, Wld. pipe
            var row82 = new OldStressRowData();
            row82.LineNo = 7;
            row82.MaterialId = 8347;
            row82.SpecNo = "SA-672";
            row82.TypeGrade = "C60";
            row82.ProductForm = "Wld. pipe";
            row82.AlloyUNS = "K02100";
            row82.ClassCondition = "";
            row82.Notes = "S6, W10, W12";
            row82.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row82);

            // Row 8: SA-672, E60, Wld. pipe
            var row83 = new OldStressRowData();
            row83.LineNo = 8;
            row83.MaterialId = 8348;
            row83.SpecNo = "SA-672";
            row83.TypeGrade = "E60";
            row83.ProductForm = "Wld. pipe";
            row83.AlloyUNS = "K02402";
            row83.ClassCondition = "";
            row83.Notes = "S6, W10, W12";
            row83.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row83);

            // Row 9: SA-134, A283D, Wld. pipe
            var row84 = new OldStressRowData();
            row84.LineNo = 9;
            row84.MaterialId = 8349;
            row84.SpecNo = "SA-134";
            row84.TypeGrade = "A283D";
            row84.ProductForm = "Wld. pipe";
            row84.AlloyUNS = "K02702";
            row84.ClassCondition = "";
            row84.Notes = "G37, W12";
            row84.StressValues = new double?[] { 15, null, 15, null, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row84);

            // Row 10: SA-283, D, Plate
            var row85 = new OldStressRowData();
            row85.LineNo = 10;
            row85.MaterialId = 8350;
            row85.SpecNo = "SA-283";
            row85.TypeGrade = "D";
            row85.ProductForm = "Plate";
            row85.AlloyUNS = "K02702";
            row85.ClassCondition = "";
            row85.Notes = "G34, G37";
            row85.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row85);

            // Row 11: SA-53, E/B, Wld. pipe
            var row86 = new OldStressRowData();
            row86.LineNo = 11;
            row86.MaterialId = 8353;
            row86.SpecNo = "SA-53";
            row86.TypeGrade = "E/B";
            row86.ProductForm = "Wld. pipe";
            row86.AlloyUNS = "K03005";
            row86.ClassCondition = "";
            row86.Notes = "G10, S1, W12, W14";
            row86.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row86);

            // Row 12: SA-53, E/B, Wld. pipe
            var row87 = new OldStressRowData();
            row87.LineNo = 12;
            row87.MaterialId = 8351;
            row87.SpecNo = "SA-53";
            row87.TypeGrade = "E/B";
            row87.ProductForm = "Wld. pipe";
            row87.AlloyUNS = "K03005";
            row87.ClassCondition = "";
            row87.Notes = "G3, G10, G24, G35, S1, W6";
            row87.StressValues = new double?[] { 12.8, 12.8, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.2, 11, 9.2, 7.4, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row87);

            // Row 13: SA-53, S/B, Smls. pipe
            var row88 = new OldStressRowData();
            row88.LineNo = 13;
            row88.MaterialId = 8352;
            row88.SpecNo = "SA-53";
            row88.TypeGrade = "S/B";
            row88.ProductForm = "Smls. pipe";
            row88.AlloyUNS = "K03005";
            row88.ClassCondition = "";
            row88.Notes = "G10, S1";
            row88.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row88);

            // Row 14: SA-53, S/B, Smls. pipe
            var row89 = new OldStressRowData();
            row89.LineNo = 14;
            row89.MaterialId = 8354;
            row89.SpecNo = "SA-53";
            row89.TypeGrade = "S/B";
            row89.ProductForm = "Smls. pipe";
            row89.AlloyUNS = "K03005";
            row89.ClassCondition = "";
            row89.Notes = "G10, G35";
            row89.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row89);

            // Row 15: SA-106, B, Smls. pipe
            var row90 = new OldStressRowData();
            row90.LineNo = 15;
            row90.MaterialId = 8355;
            row90.SpecNo = "SA-106";
            row90.TypeGrade = "B";
            row90.ProductForm = "Smls. pipe";
            row90.AlloyUNS = "K03006";
            row90.ClassCondition = "";
            row90.Notes = "G10, S1";
            row90.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row90);

            // Row 16: SA-135, B, Wld. pipe
            var row91 = new OldStressRowData();
            row91.LineNo = 16;
            row91.MaterialId = 8356;
            row91.SpecNo = "SA-135";
            row91.TypeGrade = "B";
            row91.ProductForm = "Wld. pipe";
            row91.AlloyUNS = "";
            row91.ClassCondition = "";
            row91.Notes = "G24, G35, W6";
            row91.StressValues = new double?[] { 12.8, 12.8, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.2, 11, 9.2, 7.4, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row91);

            // Row 17: SA-234, WPB, Smls. & wld. fittings
            var row92 = new OldStressRowData();
            row92.LineNo = 17;
            row92.MaterialId = 8357;
            row92.SpecNo = "SA-234";
            row92.TypeGrade = "WPB";
            row92.ProductForm = "Smls. & wld. fittings";
            row92.AlloyUNS = "K03006";
            row92.ClassCondition = "";
            row92.Notes = "G10, G18, S1";
            row92.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row92);

            // Row 18: ..., , 
            var row93 = new OldStressRowData();
            row93.LineNo = 18;
            row93.MaterialId = 8358;
            row93.SpecNo = "...";
            row93.TypeGrade = "";
            row93.ProductForm = "";
            row93.AlloyUNS = "";
            row93.ClassCondition = "";
            row93.Notes = "";
            row93.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row93);

            // Row 19: SA-333, 6, Wld. & smls. pipe
            var row94 = new OldStressRowData();
            row94.LineNo = 19;
            row94.MaterialId = 8359;
            row94.SpecNo = "SA-333";
            row94.TypeGrade = "6";
            row94.ProductForm = "Wld. & smls. pipe";
            row94.AlloyUNS = "K03006";
            row94.ClassCondition = "";
            row94.Notes = "G10, W12";
            row94.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row94);

            // Row 20: SA-334, 6, Wld. & smls. tube
            var row95 = new OldStressRowData();
            row95.LineNo = 20;
            row95.MaterialId = 8361;
            row95.SpecNo = "SA-334";
            row95.TypeGrade = "6";
            row95.ProductForm = "Wld. & smls. tube";
            row95.AlloyUNS = "K03006";
            row95.ClassCondition = "";
            row95.Notes = "W12";
            row95.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row95);

            // Row 21: SA-334, 6, Wld. tube
            var row96 = new OldStressRowData();
            row96.LineNo = 21;
            row96.MaterialId = 8360;
            row96.SpecNo = "SA-334";
            row96.TypeGrade = "6";
            row96.ProductForm = "Wld. tube";
            row96.AlloyUNS = "K03006";
            row96.ClassCondition = "";
            row96.Notes = "G24, W6";
            row96.StressValues = new double?[] { 12.8, 12.8, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row96);

            // Row 22: SA-369, FPB, Forged pipe
            var row97 = new OldStressRowData();
            row97.LineNo = 22;
            row97.MaterialId = 8362;
            row97.SpecNo = "SA-369";
            row97.TypeGrade = "FPB";
            row97.ProductForm = "Forged pipe";
            row97.AlloyUNS = "K03006";
            row97.ClassCondition = "";
            row97.Notes = "G10, G18, S1";
            row97.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, 4, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row97);

            // Row 23: SA-372, A, Forgings
            var row98 = new OldStressRowData();
            row98.LineNo = 23;
            row98.MaterialId = 8363;
            row98.SpecNo = "SA-372";
            row98.TypeGrade = "A";
            row98.ProductForm = "Forgings";
            row98.AlloyUNS = "K03002";
            row98.ClassCondition = "";
            row98.Notes = "";
            row98.StressValues = new double?[] { null, null, null, null, null, null, null, null, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row98);

            // Row 24: SA-372, A, Forgings
            var row99 = new OldStressRowData();
            row99.LineNo = 24;
            row99.MaterialId = 9779;
            row99.SpecNo = "SA-372";
            row99.TypeGrade = "A";
            row99.ProductForm = "Forgings";
            row99.AlloyUNS = "K03002";
            row99.ClassCondition = "";
            row99.Notes = "";
            row99.StressValues = new double?[] { 15, null, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row99);

            // Row 25: SA-414, D, Sheet
            var row100 = new OldStressRowData();
            row100.LineNo = 25;
            row100.MaterialId = 8364;
            row100.SpecNo = "SA-414";
            row100.TypeGrade = "D";
            row100.ProductForm = "Sheet";
            row100.AlloyUNS = "K02505";
            row100.ClassCondition = "";
            row100.Notes = "G10, G35";
            row100.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.4, 13, 10.8, 8.7, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row100);

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
