using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch006
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch006(MaterialStressService service)
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

            // Row 12: SA-487, 4, Castings
            var row501 = new OldStressRowData();
            row501.LineNo = 12;
            row501.MaterialId = 8800;
            row501.SpecNo = "SA-487";
            row501.TypeGrade = "4";
            row501.ProductForm = "Castings";
            row501.AlloyUNS = "J13047";
            row501.ClassCondition = "E";
            row501.Notes = "G1";
            row501.StressValues = new double?[] { 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row501);

            // Row 13: SA-541, 3, Forgings
            var row502 = new OldStressRowData();
            row502.LineNo = 13;
            row502.MaterialId = 8801;
            row502.SpecNo = "SA-541";
            row502.TypeGrade = "3";
            row502.ProductForm = "Forgings";
            row502.AlloyUNS = "K12045";
            row502.ClassCondition = "1";
            row502.Notes = "G23";
            row502.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row502);

            // Row 14: SA-541, 3, Forgings
            var row503 = new OldStressRowData();
            row503.LineNo = 14;
            row503.MaterialId = 8802;
            row503.SpecNo = "SA-541";
            row503.TypeGrade = "3";
            row503.ProductForm = "Forgings";
            row503.AlloyUNS = "K12045";
            row503.ClassCondition = "2";
            row503.Notes = "";
            row503.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row503);

            // Row 15: SA-592, F, Forgings
            var row504 = new OldStressRowData();
            row504.LineNo = 15;
            row504.MaterialId = 8803;
            row504.SpecNo = "SA-592";
            row504.TypeGrade = "F";
            row504.ProductForm = "Forgings";
            row504.AlloyUNS = "K11576";
            row504.ClassCondition = "";
            row504.Notes = "S7";
            row504.StressValues = new double?[] { 30, null, 30, null, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row504);

            // Row 16: SA-517, F, Plate
            var row505 = new OldStressRowData();
            row505.LineNo = 16;
            row505.MaterialId = 8804;
            row505.SpecNo = "SA-517";
            row505.TypeGrade = "F";
            row505.ProductForm = "Plate";
            row505.AlloyUNS = "K11576";
            row505.ClassCondition = "";
            row505.Notes = "";
            row505.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row505);

            // Row 17: SA-592, F, Forgings
            var row506 = new OldStressRowData();
            row506.LineNo = 17;
            row506.MaterialId = 8805;
            row506.SpecNo = "SA-592";
            row506.TypeGrade = "F";
            row506.ProductForm = "Forgings";
            row506.AlloyUNS = "K11576";
            row506.ClassCondition = "";
            row506.Notes = "";
            row506.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row506);

            // Row 18: SA-423, 2, Smls. & wld. tube
            var row507 = new OldStressRowData();
            row507.LineNo = 18;
            row507.MaterialId = 8807;
            row507.SpecNo = "SA-423";
            row507.TypeGrade = "2";
            row507.ProductForm = "Smls. & wld. tube";
            row507.AlloyUNS = "K11540";
            row507.ClassCondition = "";
            row507.Notes = "W13";
            row507.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row507);

            // Row 19: SA-423, 2, Wld. tube
            var row508 = new OldStressRowData();
            row508.LineNo = 19;
            row508.MaterialId = 8808;
            row508.SpecNo = "SA-423";
            row508.TypeGrade = "2";
            row508.ProductForm = "Wld. tube";
            row508.AlloyUNS = "K11540";
            row508.ClassCondition = "";
            row508.Notes = "G3, G24";
            row508.StressValues = new double?[] { 14.6, null, 14.6, null, 14.6, 14.6, 14.6, 14.6, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row508);

            // Row 20: SA-508, 2, Forgings
            var row509 = new OldStressRowData();
            row509.LineNo = 20;
            row509.MaterialId = 8809;
            row509.SpecNo = "SA-508";
            row509.TypeGrade = "2";
            row509.ProductForm = "Forgings";
            row509.AlloyUNS = "K12766";
            row509.ClassCondition = "1";
            row509.Notes = "G23";
            row509.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row509);

            // Row 21: SA-541, 2, Forgings
            var row510 = new OldStressRowData();
            row510.LineNo = 21;
            row510.MaterialId = 8810;
            row510.SpecNo = "SA-541";
            row510.TypeGrade = "2";
            row510.ProductForm = "Forgings";
            row510.AlloyUNS = "K12765";
            row510.ClassCondition = "1";
            row510.Notes = "G23";
            row510.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row510);

            // Row 22: SA-508, 2, Forgings
            var row511 = new OldStressRowData();
            row511.LineNo = 22;
            row511.MaterialId = 8811;
            row511.SpecNo = "SA-508";
            row511.TypeGrade = "2";
            row511.ProductForm = "Forgings";
            row511.AlloyUNS = "K12766";
            row511.ClassCondition = "2";
            row511.Notes = "";
            row511.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row511);

            // Row 23: SA-541, 2, Forgings
            var row512 = new OldStressRowData();
            row512.LineNo = 23;
            row512.MaterialId = 8812;
            row512.SpecNo = "SA-541";
            row512.TypeGrade = "2";
            row512.ProductForm = "Forgings";
            row512.AlloyUNS = "K12765";
            row512.ClassCondition = "2";
            row512.Notes = "";
            row512.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row512);

            // Row 24: SA-508, 3, Forgings
            var row513 = new OldStressRowData();
            row513.LineNo = 24;
            row513.MaterialId = 8813;
            row513.SpecNo = "SA-508";
            row513.TypeGrade = "3";
            row513.ProductForm = "Forgings";
            row513.AlloyUNS = "K12042";
            row513.ClassCondition = "1";
            row513.Notes = "G23";
            row513.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, 22.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row513);

            // Row 25: SA-508, 3, Forgings
            var row514 = new OldStressRowData();
            row514.LineNo = 25;
            row514.MaterialId = 8814;
            row514.SpecNo = "SA-508";
            row514.TypeGrade = "3";
            row514.ProductForm = "Forgings";
            row514.AlloyUNS = "K12042";
            row514.ClassCondition = "2";
            row514.Notes = "";
            row514.StressValues = new double?[] { 25.7, null, 25.7, null, 25.7, 25.7, 25.7, 25.7, 25.7, 25.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row514);

            // Row 26: SA-217, WC5, Castings
            var row515 = new OldStressRowData();
            row515.LineNo = 26;
            row515.MaterialId = 8816;
            row515.SpecNo = "SA-217";
            row515.TypeGrade = "WC5";
            row515.ProductForm = "Castings";
            row515.AlloyUNS = "J22000";
            row515.ClassCondition = "";
            row515.Notes = "G1, G17, G18, T4";
            row515.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 20, 19.3, 16.3, 11, 6.9, 4.6, 2.8, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row515);

            // Row 27: SA-217, WC4, Castings
            var row516 = new OldStressRowData();
            row516.LineNo = 27;
            row516.MaterialId = 8817;
            row516.SpecNo = "SA-217";
            row516.TypeGrade = "WC4";
            row516.ProductForm = "Castings";
            row516.AlloyUNS = "J12082";
            row516.ClassCondition = "";
            row516.Notes = "G1, G17, G18, T4";
            row516.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 20, 20, 19.3, 15, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row516);

            // Row 28: SA-517, P, Plate
            var row517 = new OldStressRowData();
            row517.LineNo = 28;
            row517.MaterialId = 8819;
            row517.SpecNo = "SA-517";
            row517.TypeGrade = "P";
            row517.ProductForm = "Plate";
            row517.AlloyUNS = "K21650";
            row517.ClassCondition = "";
            row517.Notes = "";
            row517.StressValues = new double?[] { 30, null, 30, 30, 30, 30, 30, 30, 30, 29.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row517);

            // Row 29: SA-517, P, Plate
            var row518 = new OldStressRowData();
            row518.LineNo = 29;
            row518.MaterialId = 8821;
            row518.SpecNo = "SA-517";
            row518.TypeGrade = "P";
            row518.ProductForm = "Plate";
            row518.AlloyUNS = "K21650";
            row518.ClassCondition = "";
            row518.Notes = "";
            row518.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row518);

            // Row 30: SA-350, LF5, Forgings
            var row519 = new OldStressRowData();
            row519.LineNo = 30;
            row519.MaterialId = 8795;
            row519.SpecNo = "SA-350";
            row519.TypeGrade = "LF5";
            row519.ProductForm = "Forgings";
            row519.AlloyUNS = "K13050";
            row519.ClassCondition = "1";
            row519.Notes = "";
            row519.StressValues = new double?[] { 17.1, null, 16.5, null, 15.7, 15.3, 15.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row519);

            // Row 31: SA-350, LF5, Forgings
            var row520 = new OldStressRowData();
            row520.LineNo = 31;
            row520.MaterialId = 8796;
            row520.SpecNo = "SA-350";
            row520.TypeGrade = "LF5";
            row520.ProductForm = "Forgings";
            row520.AlloyUNS = "K13050";
            row520.ClassCondition = "2";
            row520.Notes = "";
            row520.StressValues = new double?[] { 20, null, 19.2, null, 18.3, 17.8, 17.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row520);

            // Row 32: SA-372, L, Forgings
            var row521 = new OldStressRowData();
            row521.LineNo = 32;
            row521.MaterialId = 16844;
            row521.SpecNo = "SA-372";
            row521.TypeGrade = "L";
            row521.ProductForm = "Forgings";
            row521.AlloyUNS = "K24055";
            row521.ClassCondition = "";
            row521.Notes = "W2, W11";
            row521.StressValues = new double?[] { 44.3, null, 44.3, null, 44.3, 44.3, 44.3, 44.1, 42.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row521);

            // Row 33: SA-182, FR, Forgings
            var row522 = new OldStressRowData();
            row522.LineNo = 33;
            row522.MaterialId = 8822;
            row522.SpecNo = "SA-182";
            row522.TypeGrade = "FR";
            row522.ProductForm = "Forgings";
            row522.AlloyUNS = "K22035";
            row522.ClassCondition = "";
            row522.Notes = "";
            row522.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row522);

            // Row 34: SA-234, WPR, Fittings
            var row523 = new OldStressRowData();
            row523.LineNo = 34;
            row523.MaterialId = 8823;
            row523.SpecNo = "SA-234";
            row523.TypeGrade = "WPR";
            row523.ProductForm = "Fittings";
            row523.AlloyUNS = "K22035";
            row523.ClassCondition = "";
            row523.Notes = "";
            row523.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row523);

            // Row 35: SA-333, 9, Pipe
            var row524 = new OldStressRowData();
            row524.LineNo = 35;
            row524.MaterialId = 8824;
            row524.SpecNo = "SA-333";
            row524.TypeGrade = "9";
            row524.ProductForm = "Pipe";
            row524.AlloyUNS = "K22035";
            row524.ClassCondition = "";
            row524.Notes = "";
            row524.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row524);

            // Row 36: SA-333, 9, Smls. pipe
            var row525 = new OldStressRowData();
            row525.LineNo = 36;
            row525.MaterialId = 8825;
            row525.SpecNo = "SA-333";
            row525.TypeGrade = "9";
            row525.ProductForm = "Smls. pipe";
            row525.AlloyUNS = "K22035";
            row525.ClassCondition = "";
            row525.Notes = "";
            row525.StressValues = new double?[] { 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row525);

            // Row 37: SA-333, 9, Wld. pipe
            var row526 = new OldStressRowData();
            row526.LineNo = 37;
            row526.MaterialId = 8826;
            row526.SpecNo = "SA-333";
            row526.TypeGrade = "9";
            row526.ProductForm = "Wld. pipe";
            row526.AlloyUNS = "K22035";
            row526.ClassCondition = "";
            row526.Notes = "G24";
            row526.StressValues = new double?[] { 15.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row526);

            // Row 1: SA-334, 9, Tube
            var row527 = new OldStressRowData();
            row527.LineNo = 1;
            row527.MaterialId = 8827;
            row527.SpecNo = "SA-334";
            row527.TypeGrade = "9";
            row527.ProductForm = "Tube";
            row527.AlloyUNS = "K22035";
            row527.ClassCondition = "";
            row527.Notes = "";
            row527.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row527);

            // Row 2: SA-350, LF9, Forgings
            var row528 = new OldStressRowData();
            row528.LineNo = 2;
            row528.MaterialId = 8828;
            row528.SpecNo = "SA-350";
            row528.TypeGrade = "LF9";
            row528.ProductForm = "Forgings";
            row528.AlloyUNS = "K22036";
            row528.ClassCondition = "";
            row528.Notes = "";
            row528.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row528);

            // Row 3: SA-420, WPL9, Smls. & wld. fittings
            var row529 = new OldStressRowData();
            row529.LineNo = 3;
            row529.MaterialId = 8829;
            row529.SpecNo = "SA-420";
            row529.TypeGrade = "WPL9";
            row529.ProductForm = "Smls. & wld. fittings";
            row529.AlloyUNS = "K22035";
            row529.ClassCondition = "";
            row529.Notes = "";
            row529.StressValues = new double?[] { 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row529);

            // Row 4: SA-723, 1, Forgings
            var row530 = new OldStressRowData();
            row530.LineNo = 4;
            row530.MaterialId = 8832;
            row530.SpecNo = "SA-723";
            row530.TypeGrade = "1";
            row530.ProductForm = "Forgings";
            row530.AlloyUNS = "K23550";
            row530.ClassCondition = "1";
            row530.Notes = "W1";
            row530.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.4, 31.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row530);

            // Row 5: SA-723, 1, Forgings
            var row531 = new OldStressRowData();
            row531.LineNo = 5;
            row531.MaterialId = 8833;
            row531.SpecNo = "SA-723";
            row531.TypeGrade = "1";
            row531.ProductForm = "Forgings";
            row531.AlloyUNS = "K23550";
            row531.ClassCondition = "2";
            row531.Notes = "W1";
            row531.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 38.6, 38.6, 38.6, 38, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row531);

            // Row 6: SA-723, 1, Forgings
            var row532 = new OldStressRowData();
            row532.LineNo = 6;
            row532.MaterialId = 8834;
            row532.SpecNo = "SA-723";
            row532.TypeGrade = "1";
            row532.ProductForm = "Forgings";
            row532.AlloyUNS = "K23550";
            row532.ClassCondition = "3";
            row532.Notes = "W1";
            row532.StressValues = new double?[] { 44.3, null, 44.3, null, 44.3, 44.3, 44.3, 44.3, 43.6, 42.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row532);

            // Row 7: SA-723, 1, Forgings
            var row533 = new OldStressRowData();
            row533.LineNo = 7;
            row533.MaterialId = 8835;
            row533.SpecNo = "SA-723";
            row533.TypeGrade = "1";
            row533.ProductForm = "Forgings";
            row533.AlloyUNS = "K23550";
            row533.ClassCondition = "4";
            row533.Notes = "W1";
            row533.StressValues = new double?[] { 50, null, 50, null, 50, 50, 50, 50, 49.2, 48, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row533);

            // Row 8: SA-723, 1, Forgings
            var row534 = new OldStressRowData();
            row534.LineNo = 8;
            row534.MaterialId = 8836;
            row534.SpecNo = "SA-723";
            row534.TypeGrade = "1";
            row534.ProductForm = "Forgings";
            row534.AlloyUNS = "K23550";
            row534.ClassCondition = "5";
            row534.Notes = "W1";
            row534.StressValues = new double?[] { 54.3, null, 54.3, null, 54.3, 54.3, 54.3, 54.3, 53.5, 52.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row534);

            // Row 9: SA-333, 7, Pipe
            var row535 = new OldStressRowData();
            row535.LineNo = 9;
            row535.MaterialId = 8837;
            row535.SpecNo = "SA-333";
            row535.TypeGrade = "7";
            row535.ProductForm = "Pipe";
            row535.AlloyUNS = "K21903";
            row535.ClassCondition = "";
            row535.Notes = "";
            row535.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.5, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row535);

            // Row 10: SA-333, 7, Wld. pipe
            var row536 = new OldStressRowData();
            row536.LineNo = 10;
            row536.MaterialId = 8838;
            row536.SpecNo = "SA-333";
            row536.TypeGrade = "7";
            row536.ProductForm = "Wld. pipe";
            row536.AlloyUNS = "K21903";
            row536.ClassCondition = "";
            row536.Notes = "G24";
            row536.StressValues = new double?[] { 15.8, null, 15.8, null, 15.8, 15.8, 15.8, 14.9, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row536);

            // Row 11: SA-334, 7, Tube
            var row537 = new OldStressRowData();
            row537.LineNo = 11;
            row537.MaterialId = 8839;
            row537.SpecNo = "SA-334";
            row537.TypeGrade = "7";
            row537.ProductForm = "Tube";
            row537.AlloyUNS = "K21903";
            row537.ClassCondition = "";
            row537.Notes = "";
            row537.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.5, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row537);

            // Row 12: SA-334, 7, Tube
            var row538 = new OldStressRowData();
            row538.LineNo = 12;
            row538.MaterialId = 8840;
            row538.SpecNo = "SA-334";
            row538.TypeGrade = "7";
            row538.ProductForm = "Tube";
            row538.AlloyUNS = "K21903";
            row538.ClassCondition = "";
            row538.Notes = "G24";
            row538.StressValues = new double?[] { 15.8, 15.8, 15.8, null, 15.8, 15.8, 15.8, 14.9, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row538);

            // Row 13: SA-203, A, Plate
            var row539 = new OldStressRowData();
            row539.LineNo = 13;
            row539.MaterialId = 8842;
            row539.SpecNo = "SA-203";
            row539.TypeGrade = "A";
            row539.ProductForm = "Plate";
            row539.AlloyUNS = "K21703";
            row539.ClassCondition = "";
            row539.Notes = "T2";
            row539.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 18.5, 17.6, 16.6, 13.9, 11.4, 9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row539);

            // Row 14: SA-203, B, Plate
            var row540 = new OldStressRowData();
            row540.LineNo = 14;
            row540.MaterialId = 8845;
            row540.SpecNo = "SA-203";
            row540.TypeGrade = "B";
            row540.ProductForm = "Plate";
            row540.AlloyUNS = "K22103";
            row540.ClassCondition = "";
            row540.Notes = "S1, T2";
            row540.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19, 18, 14.8, 12, 9.3, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row540);

            // Row 15: SA-352, LC2, Castings
            var row541 = new OldStressRowData();
            row541.LineNo = 15;
            row541.MaterialId = 8846;
            row541.SpecNo = "SA-352";
            row541.TypeGrade = "LC2";
            row541.ProductForm = "Castings";
            row541.AlloyUNS = "J22500";
            row541.ClassCondition = "";
            row541.Notes = "G1";
            row541.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row541);

            // Row 16: SA-543, C, Plate
            var row542 = new OldStressRowData();
            row542.LineNo = 16;
            row542.MaterialId = 8847;
            row542.SpecNo = "SA-543";
            row542.TypeGrade = "C";
            row542.ProductForm = "Plate";
            row542.AlloyUNS = "";
            row542.ClassCondition = "3";
            row542.Notes = "";
            row542.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.5, 25.3, 25, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row542);

            // Row 17: SA-543, C, Plate
            var row543 = new OldStressRowData();
            row543.LineNo = 17;
            row543.MaterialId = 8848;
            row543.SpecNo = "SA-543";
            row543.TypeGrade = "C";
            row543.ProductForm = "Plate";
            row543.AlloyUNS = "";
            row543.ClassCondition = "1";
            row543.Notes = "";
            row543.StressValues = new double?[] { 30, null, 30, 30, 30, 29.7, 29.5, 29.1, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row543);

            // Row 18: SA-543, C, Plate
            var row544 = new OldStressRowData();
            row544.LineNo = 18;
            row544.MaterialId = 8849;
            row544.SpecNo = "SA-543";
            row544.TypeGrade = "C";
            row544.ProductForm = "Plate";
            row544.AlloyUNS = "";
            row544.ClassCondition = "2";
            row544.Notes = "";
            row544.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.5, 32.3, 31.9, 31.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row544);

            // Row 19: SA-723, 2, Forgings
            var row545 = new OldStressRowData();
            row545.LineNo = 19;
            row545.MaterialId = 8850;
            row545.SpecNo = "SA-723";
            row545.TypeGrade = "2";
            row545.ProductForm = "Forgings";
            row545.AlloyUNS = "K34035";
            row545.ClassCondition = "1";
            row545.Notes = "W1";
            row545.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.4, 31.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row545);

            // Row 20: SA-723, 2, Forgings
            var row546 = new OldStressRowData();
            row546.LineNo = 20;
            row546.MaterialId = 8851;
            row546.SpecNo = "SA-723";
            row546.TypeGrade = "2";
            row546.ProductForm = "Forgings";
            row546.AlloyUNS = "K34035";
            row546.ClassCondition = "2";
            row546.Notes = "W1";
            row546.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 38.6, 38.6, 38.6, 38, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row546);

            // Row 21: SA-723, 2, Forgings
            var row547 = new OldStressRowData();
            row547.LineNo = 21;
            row547.MaterialId = 8852;
            row547.SpecNo = "SA-723";
            row547.TypeGrade = "2";
            row547.ProductForm = "Forgings";
            row547.AlloyUNS = "K34035";
            row547.ClassCondition = "3";
            row547.Notes = "W1";
            row547.StressValues = new double?[] { 44.3, null, 44.3, null, 44.3, 44.3, 44.3, 44.3, 43.6, 42.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row547);

            // Row 22: SA-723, 2, Forgings
            var row548 = new OldStressRowData();
            row548.LineNo = 22;
            row548.MaterialId = 8853;
            row548.SpecNo = "SA-723";
            row548.TypeGrade = "2";
            row548.ProductForm = "Forgings";
            row548.AlloyUNS = "K34035";
            row548.ClassCondition = "4";
            row548.Notes = "W1";
            row548.StressValues = new double?[] { 50, null, 50, null, 50, 50, 50, 50, 49.2, 48, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row548);

            // Row 23: SA-723, 2, Forgings
            var row549 = new OldStressRowData();
            row549.LineNo = 23;
            row549.MaterialId = 8854;
            row549.SpecNo = "SA-723";
            row549.TypeGrade = "2";
            row549.ProductForm = "Forgings";
            row549.AlloyUNS = "K34035";
            row549.ClassCondition = "5";
            row549.Notes = "W1";
            row549.StressValues = new double?[] { 54.3, null, 54.3, null, 54.3, 54.3, 54.3, 54.3, 53.5, 52.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row549);

            // Row 24: SA-543, B, Plate
            var row550 = new OldStressRowData();
            row550.LineNo = 24;
            row550.MaterialId = 8855;
            row550.SpecNo = "SA-543";
            row550.TypeGrade = "B";
            row550.ProductForm = "Plate";
            row550.AlloyUNS = "K42339";
            row550.ClassCondition = "3";
            row550.Notes = "";
            row550.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.5, 25.3, 25, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row550);

            // Row 25: SA-543, B, Plate
            var row551 = new OldStressRowData();
            row551.LineNo = 25;
            row551.MaterialId = 8856;
            row551.SpecNo = "SA-543";
            row551.TypeGrade = "B";
            row551.ProductForm = "Plate";
            row551.AlloyUNS = "K42339";
            row551.ClassCondition = "1";
            row551.Notes = "";
            row551.StressValues = new double?[] { 30, null, 30, 30, 30, 29.7, 29.5, 29.1, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row551);

            // Row 26: SA-372, M, Forgings
            var row552 = new OldStressRowData();
            row552.LineNo = 26;
            row552.MaterialId = 16845;
            row552.SpecNo = "SA-372";
            row552.TypeGrade = "M";
            row552.ProductForm = "Forgings";
            row552.AlloyUNS = "";
            row552.ClassCondition = "A";
            row552.Notes = "W2, W11";
            row552.StressValues = new double?[] { 30, null, 30, 30, 30, 29.7, 29.5, 29.1, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row552);

            // Row 27: SA-543, B, Plate
            var row553 = new OldStressRowData();
            row553.LineNo = 27;
            row553.MaterialId = 8857;
            row553.SpecNo = "SA-543";
            row553.TypeGrade = "B";
            row553.ProductForm = "Plate";
            row553.AlloyUNS = "K42339";
            row553.ClassCondition = "2";
            row553.Notes = "";
            row553.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.5, 32.3, 31.9, 31.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row553);

            // Row 28: SA-372, M, Forgings
            var row554 = new OldStressRowData();
            row554.LineNo = 28;
            row554.MaterialId = 16846;
            row554.SpecNo = "SA-372";
            row554.TypeGrade = "M";
            row554.ProductForm = "Forgings";
            row554.AlloyUNS = "";
            row554.ClassCondition = "B";
            row554.Notes = "W2, W11";
            row554.StressValues = new double?[] { 34.3, null, 34.3, 34.3, 34.3, 33.9, 33.7, 33.3, 32.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row554);

            // Row 29: SA-333, 3, Pipe
            var row555 = new OldStressRowData();
            row555.LineNo = 29;
            row555.MaterialId = 8858;
            row555.SpecNo = "SA-333";
            row555.TypeGrade = "3";
            row555.ProductForm = "Pipe";
            row555.AlloyUNS = "K31918";
            row555.ClassCondition = "";
            row555.Notes = "";
            row555.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.5, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row555);

            // Row 30: SA-333, 3, Wld. pipe
            var row556 = new OldStressRowData();
            row556.LineNo = 30;
            row556.MaterialId = 8859;
            row556.SpecNo = "SA-333";
            row556.TypeGrade = "3";
            row556.ProductForm = "Wld. pipe";
            row556.AlloyUNS = "K31918";
            row556.ClassCondition = "";
            row556.Notes = "G24";
            row556.StressValues = new double?[] { 15.8, null, 15.8, null, 15.8, 15.8, 15.8, 14.9, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row556);

            // Row 31: SA-334, 3, Tube
            var row557 = new OldStressRowData();
            row557.LineNo = 31;
            row557.MaterialId = 8860;
            row557.SpecNo = "SA-334";
            row557.TypeGrade = "3";
            row557.ProductForm = "Tube";
            row557.AlloyUNS = "K31918";
            row557.ClassCondition = "";
            row557.Notes = "";
            row557.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.5, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row557);

            // Row 32: SA-334, 3, Wld. tube
            var row558 = new OldStressRowData();
            row558.LineNo = 32;
            row558.MaterialId = 8861;
            row558.SpecNo = "SA-334";
            row558.TypeGrade = "3";
            row558.ProductForm = "Wld. tube";
            row558.AlloyUNS = "K31918";
            row558.ClassCondition = "";
            row558.Notes = "G24";
            row558.StressValues = new double?[] { 15.8, 15.8, 15.8, null, 15.8, 15.8, 15.8, 14.9, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row558);

            // Row 33: SA-420, WPL3, Fittings
            var row559 = new OldStressRowData();
            row559.LineNo = 33;
            row559.MaterialId = 8862;
            row559.SpecNo = "SA-420";
            row559.TypeGrade = "WPL3";
            row559.ProductForm = "Fittings";
            row559.AlloyUNS = "";
            row559.ClassCondition = "";
            row559.Notes = "";
            row559.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 17.5, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row559);

            // Row 34: SA-203, D, Plate
            var row560 = new OldStressRowData();
            row560.LineNo = 34;
            row560.MaterialId = 8863;
            row560.SpecNo = "SA-203";
            row560.TypeGrade = "D";
            row560.ProductForm = "Plate";
            row560.AlloyUNS = "K31718";
            row560.ClassCondition = "";
            row560.Notes = "T2";
            row560.StressValues = new double?[] { 18.6, 18.6, 18.6, null, 18.6, 18.6, 18.6, 18.5, 17.6, 16.6, 13.9, 11.4, 9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row560);

            // Row 35: SA-350, LF3, Forgings
            var row561 = new OldStressRowData();
            row561.LineNo = 35;
            row561.MaterialId = 8866;
            row561.SpecNo = "SA-350";
            row561.TypeGrade = "LF3";
            row561.ProductForm = "Forgings";
            row561.AlloyUNS = "K32025";
            row561.ClassCondition = "";
            row561.Notes = "";
            row561.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 18.8, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row561);

            // Row 36: SA-765, III, Forgings
            var row562 = new OldStressRowData();
            row562.LineNo = 36;
            row562.MaterialId = 8867;
            row562.SpecNo = "SA-765";
            row562.TypeGrade = "III";
            row562.ProductForm = "Forgings";
            row562.AlloyUNS = "K32026";
            row562.ClassCondition = "";
            row562.Notes = "";
            row562.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 18.8, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row562);

            // Row 1: SA-203, E, Plate
            var row563 = new OldStressRowData();
            row563.LineNo = 1;
            row563.MaterialId = 8869;
            row563.SpecNo = "SA-203";
            row563.TypeGrade = "E";
            row563.ProductForm = "Plate";
            row563.AlloyUNS = "K32018";
            row563.ClassCondition = "";
            row563.Notes = "S1";
            row563.StressValues = new double?[] { 20, null, 20, null, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row563);

            // Row 2: SA-203, E, Plate
            var row564 = new OldStressRowData();
            row564.LineNo = 2;
            row564.MaterialId = 8868;
            row564.SpecNo = "SA-203";
            row564.TypeGrade = "E";
            row564.ProductForm = "Plate";
            row564.AlloyUNS = "K32018";
            row564.ClassCondition = "";
            row564.Notes = "T2";
            row564.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 19, 18, 14.8, 12, 9.3, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row564);

            // Row 3: SA-352, LC3, Castings
            var row565 = new OldStressRowData();
            row565.LineNo = 3;
            row565.MaterialId = 8870;
            row565.SpecNo = "SA-352";
            row565.TypeGrade = "LC3";
            row565.ProductForm = "Castings";
            row565.AlloyUNS = "J31550";
            row565.ClassCondition = "";
            row565.Notes = "G1";
            row565.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 19, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row565);

            // Row 4: SA-203, F, Plate
            var row566 = new OldStressRowData();
            row566.LineNo = 4;
            row566.MaterialId = 8872;
            row566.SpecNo = "SA-203";
            row566.TypeGrade = "F";
            row566.ProductForm = "Plate";
            row566.AlloyUNS = "";
            row566.ClassCondition = "";
            row566.Notes = "";
            row566.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 21.4, 21.4, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row566);

            // Row 5: SA-203, F, Plate
            var row567 = new OldStressRowData();
            row567.LineNo = 5;
            row567.MaterialId = 8873;
            row567.SpecNo = "SA-203";
            row567.TypeGrade = "F";
            row567.ProductForm = "Plate";
            row567.AlloyUNS = "";
            row567.ClassCondition = "";
            row567.Notes = "";
            row567.StressValues = new double?[] { 22.9, 22.9, 22.9, null, 22.9, 22.9, 22.9, 22.9, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row567);

            // Row 6: SA-508, 4N, Forgings
            var row568 = new OldStressRowData();
            row568.LineNo = 6;
            row568.MaterialId = 8874;
            row568.SpecNo = "SA-508";
            row568.TypeGrade = "4N";
            row568.ProductForm = "Forgings";
            row568.AlloyUNS = "K22375";
            row568.ClassCondition = "3";
            row568.Notes = "";
            row568.StressValues = new double?[] { 25.7, null, 25.7, 25.7, 25.7, 25.5, 25.3, 25, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row568);

            // Row 7: SA-508, 4N, Forgings
            var row569 = new OldStressRowData();
            row569.LineNo = 7;
            row569.MaterialId = 8875;
            row569.SpecNo = "SA-508";
            row569.TypeGrade = "4N";
            row569.ProductForm = "Forgings";
            row569.AlloyUNS = "K22375";
            row569.ClassCondition = "1";
            row569.Notes = "";
            row569.StressValues = new double?[] { 30, null, 30, 30, 30, 29.7, 29.5, 29.1, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row569);

            // Row 8: SA-508, 4N, Forgings
            var row570 = new OldStressRowData();
            row570.LineNo = 8;
            row570.MaterialId = 8877;
            row570.SpecNo = "SA-508";
            row570.TypeGrade = "4N";
            row570.ProductForm = "Forgings";
            row570.AlloyUNS = "K22375";
            row570.ClassCondition = "2";
            row570.Notes = "";
            row570.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.5, 32.3, 31.9, 31.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row570);

            // Row 9: SA-723, 3, Forgings
            var row571 = new OldStressRowData();
            row571.LineNo = 9;
            row571.MaterialId = 8878;
            row571.SpecNo = "SA-723";
            row571.TypeGrade = "3";
            row571.ProductForm = "Forgings";
            row571.AlloyUNS = "K44045";
            row571.ClassCondition = "1";
            row571.Notes = "W1";
            row571.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.4, 31.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row571);

            // Row 10: SA-723, 3, Forgings
            var row572 = new OldStressRowData();
            row572.LineNo = 10;
            row572.MaterialId = 8879;
            row572.SpecNo = "SA-723";
            row572.TypeGrade = "3";
            row572.ProductForm = "Forgings";
            row572.AlloyUNS = "K44045";
            row572.ClassCondition = "2";
            row572.Notes = "W1";
            row572.StressValues = new double?[] { 38.6, null, 38.6, null, 38.6, 38.6, 38.6, 38.6, 38, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row572);

            // Row 11: SA-723, 3, Forgings
            var row573 = new OldStressRowData();
            row573.LineNo = 11;
            row573.MaterialId = 8880;
            row573.SpecNo = "SA-723";
            row573.TypeGrade = "3";
            row573.ProductForm = "Forgings";
            row573.AlloyUNS = "K44045";
            row573.ClassCondition = "3";
            row573.Notes = "W1";
            row573.StressValues = new double?[] { 44.3, null, 44.3, null, 44.3, 44.3, 44.3, 44.3, 43.6, 42.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row573);

            // Row 12: SA-723, 3, Forgings
            var row574 = new OldStressRowData();
            row574.LineNo = 12;
            row574.MaterialId = 8881;
            row574.SpecNo = "SA-723";
            row574.TypeGrade = "3";
            row574.ProductForm = "Forgings";
            row574.AlloyUNS = "K44045";
            row574.ClassCondition = "4";
            row574.Notes = "W1";
            row574.StressValues = new double?[] { 50, null, 50, null, 50, 50, 50, 50, 49.2, 48, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row574);

            // Row 13: SA-723, 3, Forgings
            var row575 = new OldStressRowData();
            row575.LineNo = 13;
            row575.MaterialId = 8882;
            row575.SpecNo = "SA-723";
            row575.TypeGrade = "3";
            row575.ProductForm = "Forgings";
            row575.AlloyUNS = "K44045";
            row575.ClassCondition = "5";
            row575.Notes = "W1";
            row575.StressValues = new double?[] { 54.3, null, 54.3, null, 54.3, 54.3, 54.3, 54.3, 53.5, 52.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row575);

            // Row 14: SA-645, , Plate
            var row576 = new OldStressRowData();
            row576.LineNo = 14;
            row576.MaterialId = 8883;
            row576.SpecNo = "SA-645";
            row576.TypeGrade = "";
            row576.ProductForm = "Plate";
            row576.AlloyUNS = "K41583";
            row576.ClassCondition = "";
            row576.Notes = "";
            row576.StressValues = new double?[] { 27.1, null, 27.1, 26.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row576);

            // Row 15: SA-522, II, Forgings
            var row577 = new OldStressRowData();
            row577.LineNo = 15;
            row577.MaterialId = 8884;
            row577.SpecNo = "SA-522";
            row577.TypeGrade = "II";
            row577.ProductForm = "Forgings";
            row577.AlloyUNS = "K71340";
            row577.ClassCondition = "";
            row577.Notes = "G41, W5";
            row577.StressValues = new double?[] { 27.1, 27.1, 25.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row577);

            // Row 16: SA-553, II, Plate
            var row578 = new OldStressRowData();
            row578.LineNo = 16;
            row578.MaterialId = 8887;
            row578.SpecNo = "SA-553";
            row578.TypeGrade = "II";
            row578.ProductForm = "Plate";
            row578.AlloyUNS = "K71340";
            row578.ClassCondition = "";
            row578.Notes = "G41, W5";
            row578.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row578);

            // Row 17: SA-553, II, Plate
            var row579 = new OldStressRowData();
            row579.LineNo = 17;
            row579.MaterialId = 8886;
            row579.SpecNo = "SA-553";
            row579.TypeGrade = "II";
            row579.ProductForm = "Plate";
            row579.AlloyUNS = "K71340";
            row579.ClassCondition = "";
            row579.Notes = "G41, W4";
            row579.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row579);

            // Row 18: SA-333, 8, Smls. & wld. pipe
            var row580 = new OldStressRowData();
            row580.LineNo = 18;
            row580.MaterialId = 8888;
            row580.SpecNo = "SA-333";
            row580.TypeGrade = "8";
            row580.ProductForm = "Smls. & wld. pipe";
            row580.AlloyUNS = "K81340";
            row580.ClassCondition = "";
            row580.Notes = "G41, W12";
            row580.StressValues = new double?[] { 28.6, 28.6, 26.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row580);

            // Row 19: SA-333, 8, Smls. & wld. pipe
            var row581 = new OldStressRowData();
            row581.LineNo = 19;
            row581.MaterialId = 8889;
            row581.SpecNo = "SA-333";
            row581.TypeGrade = "8";
            row581.ProductForm = "Smls. & wld. pipe";
            row581.AlloyUNS = "K81340";
            row581.ClassCondition = "";
            row581.Notes = "G41, W5, W12";
            row581.StressValues = new double?[] { 27.1, 27.1, 25.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row581);

            // Row 20: SA-333, 8, Smls. pipe
            var row582 = new OldStressRowData();
            row582.LineNo = 20;
            row582.MaterialId = 8890;
            row582.SpecNo = "SA-333";
            row582.TypeGrade = "8";
            row582.ProductForm = "Smls. pipe";
            row582.AlloyUNS = "K81340";
            row582.ClassCondition = "";
            row582.Notes = "G41, W4";
            row582.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row582);

            // Row 21: SA-333, 8, Smls. pipe
            var row583 = new OldStressRowData();
            row583.LineNo = 21;
            row583.MaterialId = 8891;
            row583.SpecNo = "SA-333";
            row583.TypeGrade = "8";
            row583.ProductForm = "Smls. pipe";
            row583.AlloyUNS = "K81340";
            row583.ClassCondition = "";
            row583.Notes = "G41, W5";
            row583.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row583);

            // Row 22: SA-333, 8, Wld. pipe
            var row584 = new OldStressRowData();
            row584.LineNo = 22;
            row584.MaterialId = 8892;
            row584.SpecNo = "SA-333";
            row584.TypeGrade = "8";
            row584.ProductForm = "Wld. pipe";
            row584.AlloyUNS = "K81340";
            row584.ClassCondition = "";
            row584.Notes = "G24, G41, W3";
            row584.StressValues = new double?[] { 24.3, 24.3, 22.7, 22.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row584);

            // Row 23: SA-334, 8, Wld. tube
            var row585 = new OldStressRowData();
            row585.LineNo = 23;
            row585.MaterialId = 8893;
            row585.SpecNo = "SA-334";
            row585.TypeGrade = "8";
            row585.ProductForm = "Wld. tube";
            row585.AlloyUNS = "K81340";
            row585.ClassCondition = "";
            row585.Notes = "G41, W12";
            row585.StressValues = new double?[] { 28.6, 28.6, 26.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row585);

            // Row 24: SA-334, 8, Smls. & wld. tube
            var row586 = new OldStressRowData();
            row586.LineNo = 24;
            row586.MaterialId = 8894;
            row586.SpecNo = "SA-334";
            row586.TypeGrade = "8";
            row586.ProductForm = "Smls. & wld. tube";
            row586.AlloyUNS = "K81340";
            row586.ClassCondition = "";
            row586.Notes = "G41, W5, W12";
            row586.StressValues = new double?[] { 27.1, 27.1, 25.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row586);

            // Row 25: SA-334, 8, Smls. tube
            var row587 = new OldStressRowData();
            row587.LineNo = 25;
            row587.MaterialId = 8895;
            row587.SpecNo = "SA-334";
            row587.TypeGrade = "8";
            row587.ProductForm = "Smls. tube";
            row587.AlloyUNS = "K81340";
            row587.ClassCondition = "";
            row587.Notes = "G41, W4";
            row587.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row587);

            // Row 26: SA-334, 8, Smls. tube
            var row588 = new OldStressRowData();
            row588.LineNo = 26;
            row588.MaterialId = 8896;
            row588.SpecNo = "SA-334";
            row588.TypeGrade = "8";
            row588.ProductForm = "Smls. tube";
            row588.AlloyUNS = "K81340";
            row588.ClassCondition = "";
            row588.Notes = "G41, W5";
            row588.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row588);

            // Row 27: SA-334, 8, Wld. tube
            var row589 = new OldStressRowData();
            row589.LineNo = 27;
            row589.MaterialId = 8897;
            row589.SpecNo = "SA-334";
            row589.TypeGrade = "8";
            row589.ProductForm = "Wld. tube";
            row589.AlloyUNS = "K81340";
            row589.ClassCondition = "";
            row589.Notes = "G24, G41, W3";
            row589.StressValues = new double?[] { 24.3, 24.3, 22.7, 22.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row589);

            // Row 28: SA-353, , Plate
            var row590 = new OldStressRowData();
            row590.LineNo = 28;
            row590.MaterialId = 8898;
            row590.SpecNo = "SA-353";
            row590.TypeGrade = "";
            row590.ProductForm = "Plate";
            row590.AlloyUNS = "K81340";
            row590.ClassCondition = "";
            row590.Notes = "G41, W4";
            row590.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row590);

            // Row 29: SA-353, , Plate
            var row591 = new OldStressRowData();
            row591.LineNo = 29;
            row591.MaterialId = 8899;
            row591.SpecNo = "SA-353";
            row591.TypeGrade = "";
            row591.ProductForm = "Plate";
            row591.AlloyUNS = "K81340";
            row591.ClassCondition = "";
            row591.Notes = "G41, W5";
            row591.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row591);

            // Row 30: SA-353, , Plate
            var row592 = new OldStressRowData();
            row592.LineNo = 30;
            row592.MaterialId = 8900;
            row592.SpecNo = "SA-353";
            row592.TypeGrade = "";
            row592.ProductForm = "Plate";
            row592.AlloyUNS = "K81340";
            row592.ClassCondition = "";
            row592.Notes = "G41, W5";
            row592.StressValues = new double?[] { 27.1, 27.1, 25.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row592);

            // Row 31: SA-420, WPL8, Smls. & wld. fittings
            var row593 = new OldStressRowData();
            row593.LineNo = 31;
            row593.MaterialId = 8901;
            row593.SpecNo = "SA-420";
            row593.TypeGrade = "WPL8";
            row593.ProductForm = "Smls. & wld. fittings";
            row593.AlloyUNS = "K81340";
            row593.ClassCondition = "";
            row593.Notes = "G41, W4";
            row593.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row593);

            // Row 32: SA-420, WPL8, Smls. & wld. fittings
            var row594 = new OldStressRowData();
            row594.LineNo = 32;
            row594.MaterialId = 8902;
            row594.SpecNo = "SA-420";
            row594.TypeGrade = "WPL8";
            row594.ProductForm = "Smls. & wld. fittings";
            row594.AlloyUNS = "K81340";
            row594.ClassCondition = "";
            row594.Notes = "G41, W3";
            row594.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row594);

            // Row 33: SA-522, I, Forgings
            var row595 = new OldStressRowData();
            row595.LineNo = 33;
            row595.MaterialId = 8903;
            row595.SpecNo = "SA-522";
            row595.TypeGrade = "I";
            row595.ProductForm = "Forgings";
            row595.AlloyUNS = "K81340";
            row595.ClassCondition = "";
            row595.Notes = "G41, S8, W4";
            row595.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row595);

            // Row 34: SA-522, I, Forgings
            var row596 = new OldStressRowData();
            row596.LineNo = 34;
            row596.MaterialId = 8904;
            row596.SpecNo = "SA-522";
            row596.TypeGrade = "I";
            row596.ProductForm = "Forgings";
            row596.AlloyUNS = "K81340";
            row596.ClassCondition = "";
            row596.Notes = "G41, S8, W5";
            row596.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row596);

            // Row 35: SA-553, I, Plate
            var row597 = new OldStressRowData();
            row597.LineNo = 35;
            row597.MaterialId = 8905;
            row597.SpecNo = "SA-553";
            row597.TypeGrade = "I";
            row597.ProductForm = "Plate";
            row597.AlloyUNS = "K81340";
            row597.ClassCondition = "";
            row597.Notes = "G41, W4";
            row597.StressValues = new double?[] { 28.6, 28.6, 26.7, 25.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row597);

            // Row 36: SA-553, I, Plate
            var row598 = new OldStressRowData();
            row598.LineNo = 36;
            row598.MaterialId = 8906;
            row598.SpecNo = "SA-553";
            row598.TypeGrade = "I";
            row598.ProductForm = "Plate";
            row598.AlloyUNS = "K81340";
            row598.ClassCondition = "";
            row598.Notes = "G41, W5";
            row598.StressValues = new double?[] { 27.1, 27.1, 25.4, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row598);

            // Row 37: SA-638, 660, Bar
            var row599 = new OldStressRowData();
            row599.LineNo = 37;
            row599.MaterialId = 8907;
            row599.SpecNo = "SA-638";
            row599.TypeGrade = "660";
            row599.ProductForm = "Bar";
            row599.AlloyUNS = "S66286";
            row599.ClassCondition = "";
            row599.Notes = "G9, G28, W1";
            row599.StressValues = new double?[] { 37.1, null, 37.1, null, 37.1, 37.1, 36.5, 35.8, 35.4, 35, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row599);

            // Row 38: SA-351, CN7M, Castings
            var row600 = new OldStressRowData();
            row600.LineNo = 38;
            row600.MaterialId = 8908;
            row600.SpecNo = "SA-351";
            row600.TypeGrade = "CN7M";
            row600.ProductForm = "Castings";
            row600.AlloyUNS = "J95150";
            row600.ClassCondition = "";
            row600.Notes = "G1, G5";
            row600.StressValues = new double?[] { 16.7, null, 16, null, 13.6, 12.8, 12.1, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row600);

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
