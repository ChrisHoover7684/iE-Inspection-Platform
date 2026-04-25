using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch006
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

            // Row 37: SA-731, TPXM-27, Wld. pipe
            var row501 = new OldStressRowData();
            row501.LineNo = 37;
            row501.MaterialId = 8748;
            row501.SpecNo = "SA-731";
            row501.TypeGrade = "TPXM-27";
            row501.ProductForm = "Wld. pipe";
            row501.AlloyUNS = "S44627";
            row501.ClassCondition = "";
            row501.Notes = "G24, G32";
            row501.StressValues = new double?[] { 13.8, null, 13.8, null, 13.5, 13.5, 13.5, 13.5, 13.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row501);

            // Row 1: SA-731, TPXM-33, Smls. pipe
            var row502 = new OldStressRowData();
            row502.LineNo = 1;
            row502.MaterialId = 8749;
            row502.SpecNo = "SA-731";
            row502.TypeGrade = "TPXM-33";
            row502.ProductForm = "Smls. pipe";
            row502.AlloyUNS = "S44626";
            row502.ClassCondition = "";
            row502.Notes = "G32";
            row502.StressValues = new double?[] { 16.3, null, 16.3, null, 16.1, 15.9, 15.7, 15.4, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row502);

            // Row 2: SA-731, TPXM-33, Wld. pipe
            var row503 = new OldStressRowData();
            row503.LineNo = 2;
            row503.MaterialId = 8750;
            row503.SpecNo = "SA-731";
            row503.TypeGrade = "TPXM-33";
            row503.ProductForm = "Wld. pipe";
            row503.AlloyUNS = "S44626";
            row503.ClassCondition = "";
            row503.Notes = "G24, G32";
            row503.StressValues = new double?[] { 13.9, null, 13.9, null, 13.7, 13.5, 13.3, 13.1, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row503);

            // Row 3: SA-240, XM-33, Plate
            var row504 = new OldStressRowData();
            row504.LineNo = 3;
            row504.MaterialId = 8751;
            row504.SpecNo = "SA-240";
            row504.TypeGrade = "XM-33";
            row504.ProductForm = "Plate";
            row504.AlloyUNS = "S44626";
            row504.ClassCondition = "";
            row504.Notes = "G32";
            row504.StressValues = new double?[] { 17, null, 17, null, 16.8, 16.6, 16.4, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row504);

            // Row 4: SA-268, TPXM-33, Smls. tube
            var row505 = new OldStressRowData();
            row505.LineNo = 4;
            row505.MaterialId = 8752;
            row505.SpecNo = "SA-268";
            row505.TypeGrade = "TPXM-33";
            row505.ProductForm = "Smls. tube";
            row505.AlloyUNS = "S44626";
            row505.ClassCondition = "";
            row505.Notes = "G32";
            row505.StressValues = new double?[] { 17, null, 17, null, 16.8, 16.6, 16.4, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row505);

            // Row 5: SA-268, TPXM-33, Wld. tube
            var row506 = new OldStressRowData();
            row506.LineNo = 5;
            row506.MaterialId = 8753;
            row506.SpecNo = "SA-268";
            row506.TypeGrade = "TPXM-33";
            row506.ProductForm = "Wld. tube";
            row506.AlloyUNS = "S44626";
            row506.ClassCondition = "";
            row506.Notes = "G24, G32";
            row506.StressValues = new double?[] { 14.5, null, 14.5, null, 14.3, 14.1, 13.9, 13.7, 13.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row506);

            // Row 6: SA-479, , Bar
            var row507 = new OldStressRowData();
            row507.LineNo = 6;
            row507.MaterialId = 8754;
            row507.SpecNo = "SA-479";
            row507.TypeGrade = "";
            row507.ProductForm = "Bar";
            row507.AlloyUNS = "S44700";
            row507.ClassCondition = "";
            row507.Notes = "G22, G32";
            row507.StressValues = new double?[] { 17.5, null, 17.5, null, 16.9, 16.9, 16.9, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row507);

            // Row 7: SA-240, , Plate
            var row508 = new OldStressRowData();
            row508.LineNo = 7;
            row508.MaterialId = 8757;
            row508.SpecNo = "SA-240";
            row508.TypeGrade = "";
            row508.ProductForm = "Plate";
            row508.AlloyUNS = "S44700";
            row508.ClassCondition = "";
            row508.Notes = "G32";
            row508.StressValues = new double?[] { 20, null, 20, null, 19.3, 19.3, 19.3, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row508);

            // Row 8: SA-268, 29-4, Smls. tube
            var row509 = new OldStressRowData();
            row509.LineNo = 8;
            row509.MaterialId = 8758;
            row509.SpecNo = "SA-268";
            row509.TypeGrade = "29-4";
            row509.ProductForm = "Smls. tube";
            row509.AlloyUNS = "S44700";
            row509.ClassCondition = "";
            row509.Notes = "G32";
            row509.StressValues = new double?[] { 20, null, 20, null, 19.3, 19.3, 19.3, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row509);

            // Row 9: SA-268, 29-4, Wld. tube
            var row510 = new OldStressRowData();
            row510.LineNo = 9;
            row510.MaterialId = 8759;
            row510.SpecNo = "SA-268";
            row510.TypeGrade = "29-4";
            row510.ProductForm = "Wld. tube";
            row510.AlloyUNS = "S44700";
            row510.ClassCondition = "";
            row510.Notes = "G24, G32";
            row510.StressValues = new double?[] { 17, null, 17, null, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row510);

            // Row 10: SA-479, , Bar
            var row511 = new OldStressRowData();
            row511.LineNo = 10;
            row511.MaterialId = 8760;
            row511.SpecNo = "SA-479";
            row511.TypeGrade = "";
            row511.ProductForm = "Bar";
            row511.AlloyUNS = "S44800";
            row511.ClassCondition = "";
            row511.Notes = "G22, G32";
            row511.StressValues = new double?[] { 17.5, null, 17, null, 16, 15.8, 15.8, 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row511);

            // Row 11: SA-240, , Plate
            var row512 = new OldStressRowData();
            row512.LineNo = 11;
            row512.MaterialId = 8761;
            row512.SpecNo = "SA-240";
            row512.TypeGrade = "";
            row512.ProductForm = "Plate";
            row512.AlloyUNS = "S44800";
            row512.ClassCondition = "";
            row512.Notes = "G32";
            row512.StressValues = new double?[] { 20, null, 19.4, null, 18.3, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row512);

            // Row 12: SA-268, 29-4-2, Smls. tube
            var row513 = new OldStressRowData();
            row513.LineNo = 12;
            row513.MaterialId = 8762;
            row513.SpecNo = "SA-268";
            row513.TypeGrade = "29-4-2";
            row513.ProductForm = "Smls. tube";
            row513.AlloyUNS = "S44800";
            row513.ClassCondition = "";
            row513.Notes = "G32";
            row513.StressValues = new double?[] { 20, null, 19.4, null, 18.3, 18.1, 18.1, 18.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row513);

            // Row 13: SA-268, 29-4-2, Wld. tube
            var row514 = new OldStressRowData();
            row514.LineNo = 13;
            row514.MaterialId = 8763;
            row514.SpecNo = "SA-268";
            row514.TypeGrade = "29-4-2";
            row514.ProductForm = "Wld. tube";
            row514.AlloyUNS = "S44800";
            row514.ClassCondition = "";
            row514.Notes = "G24, G32";
            row514.StressValues = new double?[] { 17, null, 16.5, null, 15.6, 15.4, 15.4, 15.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row514);

            // Row 14: SA-268, , Smls. tube
            var row515 = new OldStressRowData();
            row515.LineNo = 14;
            row515.MaterialId = 8755;
            row515.SpecNo = "SA-268";
            row515.TypeGrade = "";
            row515.ProductForm = "Smls. tube";
            row515.AlloyUNS = "S44735";
            row515.ClassCondition = "";
            row515.Notes = "G32";
            row515.StressValues = new double?[] { 18.8, null, 18.4, null, 18.2, 18, 17.7, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row515);

            // Row 15: SA-268, , Wld. tube
            var row516 = new OldStressRowData();
            row516.LineNo = 15;
            row516.MaterialId = 8756;
            row516.SpecNo = "SA-268";
            row516.TypeGrade = "";
            row516.ProductForm = "Wld. tube";
            row516.AlloyUNS = "S44735";
            row516.ClassCondition = "";
            row516.Notes = "G24, G32";
            row516.StressValues = new double?[] { 16, null, 15.6, null, 15.5, 15.3, 15, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row516);

            // Row 16: SA-372, D, Forgings
            var row517 = new OldStressRowData();
            row517.LineNo = 16;
            row517.MaterialId = 8767;
            row517.SpecNo = "SA-372";
            row517.TypeGrade = "D";
            row517.ProductForm = "Forgings";
            row517.AlloyUNS = "K14508";
            row517.ClassCondition = "";
            row517.Notes = "G25, W1";
            row517.StressValues = new double?[] { 26.2, null, 25, null, 24.6, 24.6, 24.6, 24.6, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row517);

            // Row 17: SA-372, D, Forgings
            var row518 = new OldStressRowData();
            row518.LineNo = 17;
            row518.MaterialId = 8768;
            row518.SpecNo = "SA-372";
            row518.TypeGrade = "D";
            row518.ProductForm = "Forgings";
            row518.AlloyUNS = "K14508";
            row518.ClassCondition = "";
            row518.Notes = "W2, W11";
            row518.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row518);

            // Row 18: SA-372, D, Forgings
            var row519 = new OldStressRowData();
            row519.LineNo = 18;
            row519.MaterialId = 9782;
            row519.SpecNo = "SA-372";
            row519.TypeGrade = "D";
            row519.ProductForm = "Forgings";
            row519.AlloyUNS = "K14508";
            row519.ClassCondition = "";
            row519.Notes = "W2, W11";
            row519.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row519);

            // Row 19: SA-487, 2, Castings
            var row520 = new OldStressRowData();
            row520.LineNo = 19;
            row520.MaterialId = 8769;
            row520.SpecNo = "SA-487";
            row520.TypeGrade = "2";
            row520.ProductForm = "Castings";
            row520.AlloyUNS = "J13005";
            row520.ClassCondition = "A";
            row520.Notes = "";
            row520.StressValues = new double?[] { 21.3, null, 21.2, null, 21.2, 21.2, 21.1, 21.1, 21.1, 21.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row520);

            // Row 20: SA-487, 2, Castings
            var row521 = new OldStressRowData();
            row521.LineNo = 20;
            row521.MaterialId = 8770;
            row521.SpecNo = "SA-487";
            row521.TypeGrade = "2";
            row521.ProductForm = "Castings";
            row521.AlloyUNS = "J13005";
            row521.ClassCondition = "A";
            row521.Notes = "G1";
            row521.StressValues = new double?[] { 21.2, null, 20, null, 19.6, 19.5, 19.5, 19.5, 19.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row521);

            // Row 21: SA-487, 2, Castings
            var row522 = new OldStressRowData();
            row522.LineNo = 21;
            row522.MaterialId = 8771;
            row522.SpecNo = "SA-487";
            row522.TypeGrade = "2";
            row522.ProductForm = "Castings";
            row522.AlloyUNS = "J13005";
            row522.ClassCondition = "B";
            row522.Notes = "G1";
            row522.StressValues = new double?[] { 22.5, null, 21.3, null, 20.9, 20.9, 20.9, 20.9, 20.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row522);

            // Row 22: SA-302, A, Plate
            var row523 = new OldStressRowData();
            row523.LineNo = 22;
            row523.MaterialId = 8772;
            row523.SpecNo = "SA-302";
            row523.TypeGrade = "A";
            row523.ProductForm = "Plate";
            row523.AlloyUNS = "K12021";
            row523.ClassCondition = "";
            row523.Notes = "G11, S2";
            row523.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.3, 17.7, 16.8, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row523);

            // Row 23: SA-302, A, Plate
            var row524 = new OldStressRowData();
            row524.LineNo = 23;
            row524.MaterialId = 8773;
            row524.SpecNo = "SA-302";
            row524.TypeGrade = "A";
            row524.ProductForm = "Plate";
            row524.AlloyUNS = "K12021";
            row524.ClassCondition = "";
            row524.Notes = "";
            row524.StressValues = new double?[] { 18.7, null, 18.7, null, 18.7, 18.7, 18.7, 18.7, 18.7, 18.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row524);

            // Row 24: SA-672, H75, Wld. pipe
            var row525 = new OldStressRowData();
            row525.LineNo = 24;
            row525.MaterialId = 8774;
            row525.SpecNo = "SA-672";
            row525.TypeGrade = "H75";
            row525.ProductForm = "Wld. pipe";
            row525.AlloyUNS = "K12021";
            row525.ClassCondition = "";
            row525.Notes = "S6, W10, W12";
            row525.StressValues = new double?[] { 18.7, null, 18.7, null, 18.7, 18.7, 18.7, 18.7, 18.7, 18.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row525);

            // Row 25: SA-302, B, Plate
            var row526 = new OldStressRowData();
            row526.LineNo = 25;
            row526.MaterialId = 8775;
            row526.SpecNo = "SA-302";
            row526.TypeGrade = "B";
            row526.ProductForm = "Plate";
            row526.AlloyUNS = "K12022";
            row526.ClassCondition = "";
            row526.Notes = "G11, S2";
            row526.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.6, 18.8, 17.9, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row526);

            // Row 26: SA-533, A, Plate
            var row527 = new OldStressRowData();
            row527.LineNo = 26;
            row527.MaterialId = 8776;
            row527.SpecNo = "SA-533";
            row527.TypeGrade = "A";
            row527.ProductForm = "Plate";
            row527.AlloyUNS = "K12521";
            row527.ClassCondition = "1";
            row527.Notes = "G23";
            row527.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, 16.8, 13.3, 10, 6.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row527);

            // Row 27: SA-533, A, Plate
            var row528 = new OldStressRowData();
            row528.LineNo = 27;
            row528.MaterialId = 8777;
            row528.SpecNo = "SA-533";
            row528.TypeGrade = "A";
            row528.ProductForm = "Plate";
            row528.AlloyUNS = "K12521";
            row528.ClassCondition = "2";
            row528.Notes = "";
            row528.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row528);

            // Row 28: SA-533, A, Plate
            var row529 = new OldStressRowData();
            row529.LineNo = 28;
            row529.MaterialId = 8778;
            row529.SpecNo = "SA-533";
            row529.TypeGrade = "A";
            row529.ProductForm = "Plate";
            row529.AlloyUNS = "K12521";
            row529.ClassCondition = "3";
            row529.Notes = "";
            row529.StressValues = new double?[] { 25, null, 25, null, 25, 25, 25, 25, 25, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row529);

            // Row 29: SA-533, D, Plate
            var row530 = new OldStressRowData();
            row530.LineNo = 29;
            row530.MaterialId = 8779;
            row530.SpecNo = "SA-533";
            row530.TypeGrade = "D";
            row530.ProductForm = "Plate";
            row530.AlloyUNS = "K12529";
            row530.ClassCondition = "1";
            row530.Notes = "";
            row530.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row530);

            // Row 30: SA-533, D, Plate
            var row531 = new OldStressRowData();
            row531.LineNo = 30;
            row531.MaterialId = 8780;
            row531.SpecNo = "SA-533";
            row531.TypeGrade = "D";
            row531.ProductForm = "Plate";
            row531.AlloyUNS = "K12529";
            row531.ClassCondition = "2";
            row531.Notes = "";
            row531.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row531);

            // Row 31: SA-533, D, Plate
            var row532 = new OldStressRowData();
            row532.LineNo = 31;
            row532.MaterialId = 8781;
            row532.SpecNo = "SA-533";
            row532.TypeGrade = "D";
            row532.ProductForm = "Plate";
            row532.AlloyUNS = "K12529";
            row532.ClassCondition = "3";
            row532.Notes = "";
            row532.StressValues = new double?[] { 25, null, 25, 25, 25, 25, 25, 25, 25, 25, 24.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row532);

            // Row 32: SA-302, C, Plate
            var row533 = new OldStressRowData();
            row533.LineNo = 32;
            row533.MaterialId = 8782;
            row533.SpecNo = "SA-302";
            row533.TypeGrade = "C";
            row533.ProductForm = "Plate";
            row533.AlloyUNS = "K12039";
            row533.ClassCondition = "";
            row533.Notes = "G11, S2";
            row533.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.6, 18.8, 17.9, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row533);

            // Row 33: SA-533, B, Plate
            var row534 = new OldStressRowData();
            row534.LineNo = 33;
            row534.MaterialId = 8783;
            row534.SpecNo = "SA-533";
            row534.TypeGrade = "B";
            row534.ProductForm = "Plate";
            row534.AlloyUNS = "K12539";
            row534.ClassCondition = "1";
            row534.Notes = "G23";
            row534.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row534);

            // Row 34: SA-672, H80, Wld. pipe
            var row535 = new OldStressRowData();
            row535.LineNo = 34;
            row535.MaterialId = 8784;
            row535.SpecNo = "SA-672";
            row535.TypeGrade = "H80";
            row535.ProductForm = "Wld. pipe";
            row535.AlloyUNS = "K12039";
            row535.ClassCondition = "";
            row535.Notes = "G26, W10, W12";
            row535.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row535);

            // Row 35: SA-672, J80, Wld. pipe
            var row536 = new OldStressRowData();
            row536.LineNo = 35;
            row536.MaterialId = 8785;
            row536.SpecNo = "SA-672";
            row536.TypeGrade = "J80";
            row536.ProductForm = "Wld. pipe";
            row536.AlloyUNS = "K12539";
            row536.ClassCondition = "";
            row536.Notes = "G26, W10, W12";
            row536.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row536);

            // Row 36: SA-533, B, Plate
            var row537 = new OldStressRowData();
            row537.LineNo = 36;
            row537.MaterialId = 8786;
            row537.SpecNo = "SA-533";
            row537.TypeGrade = "B";
            row537.ProductForm = "Plate";
            row537.AlloyUNS = "K12539";
            row537.ClassCondition = "2";
            row537.Notes = "";
            row537.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row537);

            // Row 37: SA-672, J90, Wld. pipe
            var row538 = new OldStressRowData();
            row538.LineNo = 37;
            row538.MaterialId = 8787;
            row538.SpecNo = "SA-672";
            row538.TypeGrade = "J90";
            row538.ProductForm = "Wld. pipe";
            row538.AlloyUNS = "K12539";
            row538.ClassCondition = "";
            row538.Notes = "G26, W10, W12";
            row538.StressValues = new double?[] { 22.5, null, 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row538);

            // Row 38: SA-533, B, Plate
            var row539 = new OldStressRowData();
            row539.LineNo = 38;
            row539.MaterialId = 8788;
            row539.SpecNo = "SA-533";
            row539.TypeGrade = "B";
            row539.ProductForm = "Plate";
            row539.AlloyUNS = "K12539";
            row539.ClassCondition = "3";
            row539.Notes = "";
            row539.StressValues = new double?[] { 25, null, 25, 25, 25, 25, 25, 25, 25, 25, 24.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row539);

            // Row 39: SA-672, J100, Wld. pipe
            var row540 = new OldStressRowData();
            row540.LineNo = 39;
            row540.MaterialId = 8789;
            row540.SpecNo = "SA-672";
            row540.TypeGrade = "J100";
            row540.ProductForm = "Wld. pipe";
            row540.AlloyUNS = "K12539";
            row540.ClassCondition = "";
            row540.Notes = "G26, W10, W12";
            row540.StressValues = new double?[] { 25, null, 25, null, 25, 25, 25, 25, 25, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row540);

            // Row 1: SA-302, D, Plate
            var row541 = new OldStressRowData();
            row541.LineNo = 1;
            row541.MaterialId = 8790;
            row541.SpecNo = "SA-302";
            row541.TypeGrade = "D";
            row541.ProductForm = "Plate";
            row541.AlloyUNS = "K12054";
            row541.ClassCondition = "";
            row541.Notes = "G11, S2";
            row541.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.6, 18.8, 17.9, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row541);

            // Row 2: SA-533, C, Plate
            var row542 = new OldStressRowData();
            row542.LineNo = 2;
            row542.MaterialId = 8791;
            row542.SpecNo = "SA-533";
            row542.TypeGrade = "C";
            row542.ProductForm = "Plate";
            row542.AlloyUNS = "K12554";
            row542.ClassCondition = "1";
            row542.Notes = "G23";
            row542.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row542);

            // Row 3: SA-533, C, Plate
            var row543 = new OldStressRowData();
            row543.LineNo = 3;
            row543.MaterialId = 8792;
            row543.SpecNo = "SA-533";
            row543.TypeGrade = "C";
            row543.ProductForm = "Plate";
            row543.AlloyUNS = "K12554";
            row543.ClassCondition = "2";
            row543.Notes = "";
            row543.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22, 21.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row543);

            // Row 4: SA-533, C, Plate
            var row544 = new OldStressRowData();
            row544.LineNo = 4;
            row544.MaterialId = 8793;
            row544.SpecNo = "SA-533";
            row544.TypeGrade = "C";
            row544.ProductForm = "Plate";
            row544.AlloyUNS = "K12554";
            row544.ClassCondition = "3";
            row544.Notes = "";
            row544.StressValues = new double?[] { 25, null, 25, null, 25, 25, 25, 25, 25, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row544);

            // Row 5: SA-225, C, Plate
            var row545 = new OldStressRowData();
            row545.LineNo = 5;
            row545.MaterialId = 8794;
            row545.SpecNo = "SA-225";
            row545.TypeGrade = "C";
            row545.ProductForm = "Plate";
            row545.AlloyUNS = "K12524";
            row545.ClassCondition = "";
            row545.Notes = "";
            row545.StressValues = new double?[] { 26.3, 26.3, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row545);

            // Row 6: SA-487, 1, Castings
            var row546 = new OldStressRowData();
            row546.LineNo = 6;
            row546.MaterialId = 8764;
            row546.SpecNo = "SA-487";
            row546.TypeGrade = "1";
            row546.ProductForm = "Castings";
            row546.AlloyUNS = "J13002";
            row546.ClassCondition = "A";
            row546.Notes = "";
            row546.StressValues = new double?[] { 21.3, null, 20.1, null, 19.6, 19.6, 19.6, 19.2, 18.8, 18.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row546);

            // Row 7: SA-487, 1, Castings
            var row547 = new OldStressRowData();
            row547.LineNo = 7;
            row547.MaterialId = 8765;
            row547.SpecNo = "SA-487";
            row547.TypeGrade = "1";
            row547.ProductForm = "Castings";
            row547.AlloyUNS = "J13002";
            row547.ClassCondition = "A";
            row547.Notes = "G1";
            row547.StressValues = new double?[] { 21.3, null, 20.2, null, 19.7, 19.7, 19.7, 19.2, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row547);

            // Row 8: SA-487, 1, Castings
            var row548 = new OldStressRowData();
            row548.LineNo = 8;
            row548.MaterialId = 8766;
            row548.SpecNo = "SA-487";
            row548.TypeGrade = "1";
            row548.ProductForm = "Castings";
            row548.AlloyUNS = "J13002";
            row548.ClassCondition = "B";
            row548.Notes = "G1";
            row548.StressValues = new double?[] { 22.5, null, 21.5, null, 21.2, 21.1, 21.1, 20.9, 20.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row548);

            // Row 9: SA-335, P15, Smls. pipe
            var row549 = new OldStressRowData();
            row549.LineNo = 9;
            row549.MaterialId = 8797;
            row549.SpecNo = "SA-335";
            row549.TypeGrade = "P15";
            row549.ProductForm = "Smls. pipe";
            row549.AlloyUNS = "K11578";
            row549.ClassCondition = "";
            row549.Notes = "";
            row549.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 15, 15, 14.4, 13.8, 12.5, 10, 6.3, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row549);

            // Row 10: SA-487, 4, Castings
            var row550 = new OldStressRowData();
            row550.LineNo = 10;
            row550.MaterialId = 8798;
            row550.SpecNo = "SA-487";
            row550.TypeGrade = "4";
            row550.ProductForm = "Castings";
            row550.AlloyUNS = "J13047";
            row550.ClassCondition = "A";
            row550.Notes = "G1";
            row550.StressValues = new double?[] { 22.5, null, 21.4, null, 21, 21, 21, 21, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row550);

            // Row 11: SA-487, 4, Castings
            var row551 = new OldStressRowData();
            row551.LineNo = 11;
            row551.MaterialId = 8799;
            row551.SpecNo = "SA-487";
            row551.TypeGrade = "4";
            row551.ProductForm = "Castings";
            row551.AlloyUNS = "J13047";
            row551.ClassCondition = "B";
            row551.Notes = "G1";
            row551.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row551);

            // Row 12: SA-487, 4, Castings
            var row552 = new OldStressRowData();
            row552.LineNo = 12;
            row552.MaterialId = 8800;
            row552.SpecNo = "SA-487";
            row552.TypeGrade = "4";
            row552.ProductForm = "Castings";
            row552.AlloyUNS = "J13047";
            row552.ClassCondition = "E";
            row552.Notes = "G1";
            row552.StressValues = new double?[] { 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row552);

            // Row 13: SA-541, 3, Forgings
            var row553 = new OldStressRowData();
            row553.LineNo = 13;
            row553.MaterialId = 8801;
            row553.SpecNo = "SA-541";
            row553.TypeGrade = "3";
            row553.ProductForm = "Forgings";
            row553.AlloyUNS = "K12045";
            row553.ClassCondition = "1";
            row553.Notes = "G23";
            row553.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row553);

            // Row 14: SA-541, 3, Forgings
            var row554 = new OldStressRowData();
            row554.LineNo = 14;
            row554.MaterialId = 8802;
            row554.SpecNo = "SA-541";
            row554.TypeGrade = "3";
            row554.ProductForm = "Forgings";
            row554.AlloyUNS = "K12045";
            row554.ClassCondition = "2";
            row554.Notes = "";
            row554.StressValues = new double?[] { 22.5, null, 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row554);

            // Row 15: SA-592, F, Forgings
            var row555 = new OldStressRowData();
            row555.LineNo = 15;
            row555.MaterialId = 8803;
            row555.SpecNo = "SA-592";
            row555.TypeGrade = "F";
            row555.ProductForm = "Forgings";
            row555.AlloyUNS = "K11576";
            row555.ClassCondition = "";
            row555.Notes = "S7";
            row555.StressValues = new double?[] { 26.3, null, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row555);

            // Row 16: SA-517, F, Plate
            var row556 = new OldStressRowData();
            row556.LineNo = 16;
            row556.MaterialId = 8804;
            row556.SpecNo = "SA-517";
            row556.TypeGrade = "F";
            row556.ProductForm = "Plate";
            row556.AlloyUNS = "K11576";
            row556.ClassCondition = "";
            row556.Notes = "";
            row556.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row556);

            // Row 17: SA-592, F, Forgings
            var row557 = new OldStressRowData();
            row557.LineNo = 17;
            row557.MaterialId = 8805;
            row557.SpecNo = "SA-592";
            row557.TypeGrade = "F";
            row557.ProductForm = "Forgings";
            row557.AlloyUNS = "K11576";
            row557.ClassCondition = "";
            row557.Notes = "";
            row557.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row557);

            // Row 18: SA-423, 2, Smls. tube
            var row558 = new OldStressRowData();
            row558.LineNo = 18;
            row558.MaterialId = 8806;
            row558.SpecNo = "SA-423";
            row558.TypeGrade = "2";
            row558.ProductForm = "Smls. tube";
            row558.AlloyUNS = "K11540";
            row558.ClassCondition = "";
            row558.Notes = "";
            row558.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row558);

            // Row 19: SA-423, 2, Wld. tube
            var row559 = new OldStressRowData();
            row559.LineNo = 19;
            row559.MaterialId = 8807;
            row559.SpecNo = "SA-423";
            row559.TypeGrade = "2";
            row559.ProductForm = "Wld. tube";
            row559.AlloyUNS = "K11540";
            row559.ClassCondition = "";
            row559.Notes = "W14";
            row559.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row559);

            // Row 20: SA-423, 2, Wld. tube
            var row560 = new OldStressRowData();
            row560.LineNo = 20;
            row560.MaterialId = 8808;
            row560.SpecNo = "SA-423";
            row560.TypeGrade = "2";
            row560.ProductForm = "Wld. tube";
            row560.AlloyUNS = "K11540";
            row560.ClassCondition = "";
            row560.Notes = "G3, G24";
            row560.StressValues = new double?[] { 12.8, null, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row560);

            // Row 21: SA-508, 2, Forgings
            var row561 = new OldStressRowData();
            row561.LineNo = 21;
            row561.MaterialId = 8809;
            row561.SpecNo = "SA-508";
            row561.TypeGrade = "2";
            row561.ProductForm = "Forgings";
            row561.AlloyUNS = "K12766";
            row561.ClassCondition = "1";
            row561.Notes = "G23";
            row561.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row561);

            // Row 22: SA-541, 2, Forgings
            var row562 = new OldStressRowData();
            row562.LineNo = 22;
            row562.MaterialId = 8810;
            row562.SpecNo = "SA-541";
            row562.TypeGrade = "2";
            row562.ProductForm = "Forgings";
            row562.AlloyUNS = "K12765";
            row562.ClassCondition = "1";
            row562.Notes = "G23";
            row562.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row562);

            // Row 23: SA-508, 2, Forgings
            var row563 = new OldStressRowData();
            row563.LineNo = 23;
            row563.MaterialId = 8811;
            row563.SpecNo = "SA-508";
            row563.TypeGrade = "2";
            row563.ProductForm = "Forgings";
            row563.AlloyUNS = "K12766";
            row563.ClassCondition = "2";
            row563.Notes = "";
            row563.StressValues = new double?[] { 22.5, null, 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row563);

            // Row 24: SA-541, 2, Forgings
            var row564 = new OldStressRowData();
            row564.LineNo = 24;
            row564.MaterialId = 8812;
            row564.SpecNo = "SA-541";
            row564.TypeGrade = "2";
            row564.ProductForm = "Forgings";
            row564.AlloyUNS = "K12765";
            row564.ClassCondition = "2";
            row564.Notes = "";
            row564.StressValues = new double?[] { 22.5, null, 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row564);

            // Row 25: SA-508, 3, Forgings
            var row565 = new OldStressRowData();
            row565.LineNo = 25;
            row565.MaterialId = 8813;
            row565.SpecNo = "SA-508";
            row565.TypeGrade = "3";
            row565.ProductForm = "Forgings";
            row565.AlloyUNS = "K12042";
            row565.ClassCondition = "1";
            row565.Notes = "G23";
            row565.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row565);

            // Row 26: SA-508, 3, Forgings
            var row566 = new OldStressRowData();
            row566.LineNo = 26;
            row566.MaterialId = 8814;
            row566.SpecNo = "SA-508";
            row566.TypeGrade = "3";
            row566.ProductForm = "Forgings";
            row566.AlloyUNS = "K12042";
            row566.ClassCondition = "2";
            row566.Notes = "";
            row566.StressValues = new double?[] { 22.5, null, 22.5, null, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row566);

            // Row 27: SA-217, WC5, Castings
            var row567 = new OldStressRowData();
            row567.LineNo = 27;
            row567.MaterialId = 8815;
            row567.SpecNo = "SA-217";
            row567.TypeGrade = "WC5";
            row567.ProductForm = "Castings";
            row567.AlloyUNS = "J22000";
            row567.ClassCondition = "";
            row567.Notes = "";
            row567.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row567);

            // Row 28: SA-217, WC5, Castings
            var row568 = new OldStressRowData();
            row568.LineNo = 28;
            row568.MaterialId = 8816;
            row568.SpecNo = "SA-217";
            row568.TypeGrade = "WC5";
            row568.ProductForm = "Castings";
            row568.AlloyUNS = "J22000";
            row568.ClassCondition = "";
            row568.Notes = "G1, G17, G18";
            row568.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 16.3, 11, 6.9, 4.6, 2.8, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row568);

            // Row 29: SA-217, WC4, Castings
            var row569 = new OldStressRowData();
            row569.LineNo = 29;
            row569.MaterialId = 8817;
            row569.SpecNo = "SA-217";
            row569.TypeGrade = "WC4";
            row569.ProductForm = "Castings";
            row569.AlloyUNS = "J12082";
            row569.ClassCondition = "";
            row569.Notes = "G1, G18";
            row569.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 15, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row569);

            // Row 30: SA-217, WC4, Castings
            var row570 = new OldStressRowData();
            row570.LineNo = 30;
            row570.MaterialId = 8818;
            row570.SpecNo = "SA-217";
            row570.TypeGrade = "WC4";
            row570.ProductForm = "Castings";
            row570.AlloyUNS = "J12082";
            row570.ClassCondition = "";
            row570.Notes = "G17";
            row570.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row570);

            // Row 31: SA-517, P, Plate
            var row571 = new OldStressRowData();
            row571.LineNo = 31;
            row571.MaterialId = 8820;
            row571.SpecNo = "SA-517";
            row571.TypeGrade = "P";
            row571.ProductForm = "Plate";
            row571.AlloyUNS = "K21650";
            row571.ClassCondition = "";
            row571.Notes = "";
            row571.StressValues = new double?[] { 26.3, null, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row571);

            // Row 32: SA-517, P, Plate
            var row572 = new OldStressRowData();
            row572.LineNo = 32;
            row572.MaterialId = 8819;
            row572.SpecNo = "SA-517";
            row572.TypeGrade = "P";
            row572.ProductForm = "Plate";
            row572.AlloyUNS = "K21650";
            row572.ClassCondition = "";
            row572.Notes = "";
            row572.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row572);

            // Row 33: SA-517, P, Plate
            var row573 = new OldStressRowData();
            row573.LineNo = 33;
            row573.MaterialId = 8821;
            row573.SpecNo = "SA-517";
            row573.TypeGrade = "P";
            row573.ProductForm = "Plate";
            row573.AlloyUNS = "K21650";
            row573.ClassCondition = "";
            row573.Notes = "";
            row573.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row573);

            // Row 34: SA-350, LF5, Forgings
            var row574 = new OldStressRowData();
            row574.LineNo = 34;
            row574.MaterialId = 8795;
            row574.SpecNo = "SA-350";
            row574.TypeGrade = "LF5";
            row574.ProductForm = "Forgings";
            row574.AlloyUNS = "K13050";
            row574.ClassCondition = "1";
            row574.Notes = "";
            row574.StressValues = new double?[] { 15, null, 14.4, null, 13.7, 13.4, 13.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row574);

            // Row 35: SA-350, LF5, Forgings
            var row575 = new OldStressRowData();
            row575.LineNo = 35;
            row575.MaterialId = 8796;
            row575.SpecNo = "SA-350";
            row575.TypeGrade = "LF5";
            row575.ProductForm = "Forgings";
            row575.AlloyUNS = "K13050";
            row575.ClassCondition = "2";
            row575.Notes = "";
            row575.StressValues = new double?[] { 17.5, null, 16.8, null, 16, 15.6, 15.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row575);

            // Row 36: SA-182, FR, Forgings
            var row576 = new OldStressRowData();
            row576.LineNo = 36;
            row576.MaterialId = 8822;
            row576.SpecNo = "SA-182";
            row576.TypeGrade = "FR";
            row576.ProductForm = "Forgings";
            row576.AlloyUNS = "K22035";
            row576.ClassCondition = "";
            row576.Notes = "";
            row576.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row576);

            // Row 37: SA-234, WPR, Fittings
            var row577 = new OldStressRowData();
            row577.LineNo = 37;
            row577.MaterialId = 8823;
            row577.SpecNo = "SA-234";
            row577.TypeGrade = "WPR";
            row577.ProductForm = "Fittings";
            row577.AlloyUNS = "K22035";
            row577.ClassCondition = "";
            row577.Notes = "";
            row577.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row577);

            // Row 38: SA-333, 9, Pipe
            var row578 = new OldStressRowData();
            row578.LineNo = 38;
            row578.MaterialId = 8824;
            row578.SpecNo = "SA-333";
            row578.TypeGrade = "9";
            row578.ProductForm = "Pipe";
            row578.AlloyUNS = "K22035";
            row578.ClassCondition = "";
            row578.Notes = "";
            row578.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row578);

            // Row 39: SA-333, 9, Smls. pipe
            var row579 = new OldStressRowData();
            row579.LineNo = 39;
            row579.MaterialId = 8825;
            row579.SpecNo = "SA-333";
            row579.TypeGrade = "9";
            row579.ProductForm = "Smls. pipe";
            row579.AlloyUNS = "K22035";
            row579.ClassCondition = "";
            row579.Notes = "";
            row579.StressValues = new double?[] { 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row579);

            // Row 40: SA-333, 9, Wld. pipe
            var row580 = new OldStressRowData();
            row580.LineNo = 40;
            row580.MaterialId = 8826;
            row580.SpecNo = "SA-333";
            row580.TypeGrade = "9";
            row580.ProductForm = "Wld. pipe";
            row580.AlloyUNS = "K22035";
            row580.ClassCondition = "";
            row580.Notes = "G24";
            row580.StressValues = new double?[] { 13.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row580);

            // Row 1: SA-334, 9, Tube
            var row581 = new OldStressRowData();
            row581.LineNo = 1;
            row581.MaterialId = 8827;
            row581.SpecNo = "SA-334";
            row581.TypeGrade = "9";
            row581.ProductForm = "Tube";
            row581.AlloyUNS = "K22035";
            row581.ClassCondition = "";
            row581.Notes = "";
            row581.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row581);

            // Row 2: SA-350, LF9, Forgings
            var row582 = new OldStressRowData();
            row582.LineNo = 2;
            row582.MaterialId = 8828;
            row582.SpecNo = "SA-350";
            row582.TypeGrade = "LF9";
            row582.ProductForm = "Forgings";
            row582.AlloyUNS = "K22036";
            row582.ClassCondition = "";
            row582.Notes = "";
            row582.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row582);

            // Row 3: SA-420, WPL9, Smls. & wld. fittings
            var row583 = new OldStressRowData();
            row583.LineNo = 3;
            row583.MaterialId = 8829;
            row583.SpecNo = "SA-420";
            row583.TypeGrade = "WPL9";
            row583.ProductForm = "Smls. & wld. fittings";
            row583.AlloyUNS = "K22035";
            row583.ClassCondition = "";
            row583.Notes = "";
            row583.StressValues = new double?[] { 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row583);

            // Row 4: ..., , 
            var row584 = new OldStressRowData();
            row584.LineNo = 4;
            row584.MaterialId = 8830;
            row584.SpecNo = "...";
            row584.TypeGrade = "";
            row584.ProductForm = "";
            row584.AlloyUNS = "";
            row584.ClassCondition = "";
            row584.Notes = "";
            row584.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row584);

            // Row 5: ..., , 
            var row585 = new OldStressRowData();
            row585.LineNo = 5;
            row585.MaterialId = 8831;
            row585.SpecNo = "...";
            row585.TypeGrade = "";
            row585.ProductForm = "";
            row585.AlloyUNS = "";
            row585.ClassCondition = "";
            row585.Notes = "";
            row585.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row585);

            // Row 6: SA-723, 1, Forgings
            var row586 = new OldStressRowData();
            row586.LineNo = 6;
            row586.MaterialId = 8832;
            row586.SpecNo = "SA-723";
            row586.TypeGrade = "1";
            row586.ProductForm = "Forgings";
            row586.AlloyUNS = "K23550";
            row586.ClassCondition = "1";
            row586.Notes = "W1";
            row586.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.3, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row586);

            // Row 7: SA-723, 1, Forgings
            var row587 = new OldStressRowData();
            row587.LineNo = 7;
            row587.MaterialId = 8833;
            row587.SpecNo = "SA-723";
            row587.TypeGrade = "1";
            row587.ProductForm = "Forgings";
            row587.AlloyUNS = "K23550";
            row587.ClassCondition = "2";
            row587.Notes = "W1";
            row587.StressValues = new double?[] { 33.8, null, 33.8, null, 33.8, 33.8, 33.8, 33.8, 33.2, 32.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row587);

            // Row 8: SA-723, 1, Forgings
            var row588 = new OldStressRowData();
            row588.LineNo = 8;
            row588.MaterialId = 8834;
            row588.SpecNo = "SA-723";
            row588.TypeGrade = "1";
            row588.ProductForm = "Forgings";
            row588.AlloyUNS = "K23550";
            row588.ClassCondition = "3";
            row588.Notes = "W1";
            row588.StressValues = new double?[] { 38.8, null, 38.8, null, 38.8, 38.8, 38.8, 38.8, 38.1, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row588);

            // Row 9: SA-723, 1, Forgings
            var row589 = new OldStressRowData();
            row589.LineNo = 9;
            row589.MaterialId = 8835;
            row589.SpecNo = "SA-723";
            row589.TypeGrade = "1";
            row589.ProductForm = "Forgings";
            row589.AlloyUNS = "K23550";
            row589.ClassCondition = "4";
            row589.Notes = "W1";
            row589.StressValues = new double?[] { 43.8, null, 43.8, null, 43.8, 43.8, 43.8, 43.8, 43.1, 41.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row589);

            // Row 10: SA-723, 1, Forgings
            var row590 = new OldStressRowData();
            row590.LineNo = 10;
            row590.MaterialId = 8836;
            row590.SpecNo = "SA-723";
            row590.TypeGrade = "1";
            row590.ProductForm = "Forgings";
            row590.AlloyUNS = "K23550";
            row590.ClassCondition = "5";
            row590.Notes = "W1";
            row590.StressValues = new double?[] { 47.5, null, 47.5, null, 47.5, 47.5, 47.5, 47.5, 46.8, 45.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row590);

            // Row 11: SA-333, 7, Pipe
            var row591 = new OldStressRowData();
            row591.LineNo = 11;
            row591.MaterialId = 8837;
            row591.SpecNo = "SA-333";
            row591.TypeGrade = "7";
            row591.ProductForm = "Pipe";
            row591.AlloyUNS = "K21903";
            row591.ClassCondition = "";
            row591.Notes = "";
            row591.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row591);

            // Row 12: SA-333, 7, Wld. pipe
            var row592 = new OldStressRowData();
            row592.LineNo = 12;
            row592.MaterialId = 8838;
            row592.SpecNo = "SA-333";
            row592.TypeGrade = "7";
            row592.ProductForm = "Wld. pipe";
            row592.AlloyUNS = "K21903";
            row592.ClassCondition = "";
            row592.Notes = "G24";
            row592.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row592);

            // Row 13: SA-334, 7, Tube
            var row593 = new OldStressRowData();
            row593.LineNo = 13;
            row593.MaterialId = 8839;
            row593.SpecNo = "SA-334";
            row593.TypeGrade = "7";
            row593.ProductForm = "Tube";
            row593.AlloyUNS = "K21903";
            row593.ClassCondition = "";
            row593.Notes = "";
            row593.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row593);

            // Row 14: SA-334, 7, Tube
            var row594 = new OldStressRowData();
            row594.LineNo = 14;
            row594.MaterialId = 8840;
            row594.SpecNo = "SA-334";
            row594.TypeGrade = "7";
            row594.ProductForm = "Tube";
            row594.AlloyUNS = "K21903";
            row594.ClassCondition = "";
            row594.Notes = "G24";
            row594.StressValues = new double?[] { 13.9, 13.9, 13.9, null, 13.9, 13.9, 13.9, 13.9, 13.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row594);

            // Row 15: ..., , 
            var row595 = new OldStressRowData();
            row595.LineNo = 15;
            row595.MaterialId = 8841;
            row595.SpecNo = "...";
            row595.TypeGrade = "";
            row595.ProductForm = "";
            row595.AlloyUNS = "";
            row595.ClassCondition = "";
            row595.Notes = "";
            row595.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row595);

            // Row 16: SA-203, A, Plate
            var row596 = new OldStressRowData();
            row596.LineNo = 16;
            row596.MaterialId = 8843;
            row596.SpecNo = "SA-203";
            row596.TypeGrade = "A";
            row596.ProductForm = "Plate";
            row596.AlloyUNS = "K21703";
            row596.ClassCondition = "";
            row596.Notes = "";
            row596.StressValues = new double?[] { 16.2, null, 16.2, null, 16.2, 16.2, 16.2, 16.2, 16.2, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row596);

            // Row 17: SA-203, A, Plate
            var row597 = new OldStressRowData();
            row597.LineNo = 17;
            row597.MaterialId = 8842;
            row597.SpecNo = "SA-203";
            row597.TypeGrade = "A";
            row597.ProductForm = "Plate";
            row597.AlloyUNS = "K21703";
            row597.ClassCondition = "";
            row597.Notes = "";
            row597.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row597);

            // Row 18: SA-203, B, Plate
            var row598 = new OldStressRowData();
            row598.LineNo = 18;
            row598.MaterialId = 8844;
            row598.SpecNo = "SA-203";
            row598.TypeGrade = "B";
            row598.ProductForm = "Plate";
            row598.AlloyUNS = "K22103";
            row598.ClassCondition = "";
            row598.Notes = "S1";
            row598.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row598);

            // Row 19: SA-203, B, Plate
            var row599 = new OldStressRowData();
            row599.LineNo = 19;
            row599.MaterialId = 8845;
            row599.SpecNo = "SA-203";
            row599.TypeGrade = "B";
            row599.ProductForm = "Plate";
            row599.AlloyUNS = "K22103";
            row599.ClassCondition = "";
            row599.Notes = "";
            row599.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row599);

            // Row 20: SA-352, LC2, Castings
            var row600 = new OldStressRowData();
            row600.LineNo = 20;
            row600.MaterialId = 8846;
            row600.SpecNo = "SA-352";
            row600.TypeGrade = "LC2";
            row600.ProductForm = "Castings";
            row600.AlloyUNS = "J22500";
            row600.ClassCondition = "";
            row600.Notes = "G1";
            row600.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
