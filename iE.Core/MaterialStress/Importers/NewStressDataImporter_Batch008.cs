using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch008
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch008(MaterialStressService service)
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

            // Row 21: SA-182, F316LN, Forgings
            var row701 = new OldStressRowData();
            row701.LineNo = 21;
            row701.MaterialId = 9025;
            row701.SpecNo = "SA-182";
            row701.TypeGrade = "F316LN";
            row701.ProductForm = "Forgings";
            row701.AlloyUNS = "S31653";
            row701.ClassCondition = "";
            row701.Notes = "G5";
            row701.StressValues = new double?[] { 20, null, 20, null, 18.9, 17.9, 17.2, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row701);

            // Row 22: SA-336, F316LN, Forgings
            var row702 = new OldStressRowData();
            row702.LineNo = 22;
            row702.MaterialId = 9026;
            row702.SpecNo = "SA-336";
            row702.TypeGrade = "F316LN";
            row702.ProductForm = "Forgings";
            row702.AlloyUNS = "S31653";
            row702.ClassCondition = "";
            row702.Notes = "G5";
            row702.StressValues = new double?[] { 20, null, 20, null, 18.9, 17.9, 17.2, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row702);

            // Row 23: SA-182, F316LN, Forgings
            var row703 = new OldStressRowData();
            row703.LineNo = 23;
            row703.MaterialId = 9027;
            row703.SpecNo = "SA-182";
            row703.TypeGrade = "F316LN";
            row703.ProductForm = "Forgings";
            row703.AlloyUNS = "S31653";
            row703.ClassCondition = "";
            row703.Notes = "G5";
            row703.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row703);

            // Row 24: SA-213, TP316LN, Smls. tube
            var row704 = new OldStressRowData();
            row704.LineNo = 24;
            row704.MaterialId = 9028;
            row704.SpecNo = "SA-213";
            row704.TypeGrade = "TP316LN";
            row704.ProductForm = "Smls. tube";
            row704.AlloyUNS = "S31653";
            row704.ClassCondition = "";
            row704.Notes = "G5";
            row704.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row704);

            // Row 25: SA-240, 316LN, Plate
            var row705 = new OldStressRowData();
            row705.LineNo = 25;
            row705.MaterialId = 9029;
            row705.SpecNo = "SA-240";
            row705.TypeGrade = "316LN";
            row705.ProductForm = "Plate";
            row705.AlloyUNS = "S31653";
            row705.ClassCondition = "";
            row705.Notes = "G5";
            row705.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row705);

            // Row 26: SA-249, TP316LN, Wld. tube
            var row706 = new OldStressRowData();
            row706.LineNo = 26;
            row706.MaterialId = 9030;
            row706.SpecNo = "SA-249";
            row706.TypeGrade = "TP316LN";
            row706.ProductForm = "Wld. tube";
            row706.AlloyUNS = "S31653";
            row706.ClassCondition = "";
            row706.Notes = "G5, W12";
            row706.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row706);

            // Row 27: SA-312, TP316LN, Smls. & wld. pipe
            var row707 = new OldStressRowData();
            row707.LineNo = 27;
            row707.MaterialId = 9031;
            row707.SpecNo = "SA-312";
            row707.TypeGrade = "TP316LN";
            row707.ProductForm = "Smls. & wld. pipe";
            row707.AlloyUNS = "S31653";
            row707.ClassCondition = "";
            row707.Notes = "G5, W12";
            row707.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row707);

            // Row 28: SA-358, 316LN, Wld. pipe
            var row708 = new OldStressRowData();
            row708.LineNo = 28;
            row708.MaterialId = 9032;
            row708.SpecNo = "SA-358";
            row708.TypeGrade = "316LN";
            row708.ProductForm = "Wld. pipe";
            row708.AlloyUNS = "S31653";
            row708.ClassCondition = "1";
            row708.Notes = "G5, W12";
            row708.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row708);

            // Row 29: SA-376, TP316LN, Smls. pipe
            var row709 = new OldStressRowData();
            row709.LineNo = 29;
            row709.MaterialId = 9033;
            row709.SpecNo = "SA-376";
            row709.TypeGrade = "TP316LN";
            row709.ProductForm = "Smls. pipe";
            row709.AlloyUNS = "S31653";
            row709.ClassCondition = "";
            row709.Notes = "G5";
            row709.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row709);

            // Row 30: SA-403, 316LN, Fittings
            var row710 = new OldStressRowData();
            row710.LineNo = 30;
            row710.MaterialId = 9034;
            row710.SpecNo = "SA-403";
            row710.TypeGrade = "316LN";
            row710.ProductForm = "Fittings";
            row710.AlloyUNS = "S31653";
            row710.ClassCondition = "";
            row710.Notes = "G5, W12";
            row710.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row710);

            // Row 31: SA-479, 316LN, Bar
            var row711 = new OldStressRowData();
            row711.LineNo = 31;
            row711.MaterialId = 9036;
            row711.SpecNo = "SA-479";
            row711.TypeGrade = "316LN";
            row711.ProductForm = "Bar";
            row711.AlloyUNS = "S31653";
            row711.ClassCondition = "";
            row711.Notes = "G5";
            row711.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row711);

            // Row 32: SA-688, TP316LN, Wld. tube
            var row712 = new OldStressRowData();
            row712.LineNo = 32;
            row712.MaterialId = 9037;
            row712.SpecNo = "SA-688";
            row712.TypeGrade = "TP316LN";
            row712.ProductForm = "Wld. tube";
            row712.AlloyUNS = "S31653";
            row712.ClassCondition = "";
            row712.Notes = "G5, W12";
            row712.StressValues = new double?[] { 20, null, 20, null, 20, 18.9, 17.5, 16.5, 16, 15.6, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row712);

            // Row 33: SA-430, FP316N, Forged pipe
            var row713 = new OldStressRowData();
            row713.LineNo = 33;
            row713.MaterialId = 9038;
            row713.SpecNo = "SA-430";
            row713.TypeGrade = "FP316N";
            row713.ProductForm = "Forged pipe";
            row713.AlloyUNS = "S31651";
            row713.ClassCondition = "";
            row713.Notes = "G5, G12, G18, H1, T7";
            row713.StressValues = new double?[] { 21.4, null, 21.4, null, 20.6, 20.1, 19.9, 19.9, 19.9, 19.9, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row713);

            // Row 34: SA-430, FP316N, Forged pipe
            var row714 = new OldStressRowData();
            row714.LineNo = 34;
            row714.MaterialId = 9040;
            row714.SpecNo = "SA-430";
            row714.TypeGrade = "FP316N";
            row714.ProductForm = "Forged pipe";
            row714.AlloyUNS = "S31651";
            row714.ClassCondition = "";
            row714.Notes = "G12, G18, H1, T8";
            row714.StressValues = new double?[] { 21.4, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row714);

            // Row 35: SA-182, F316N, Forgings
            var row715 = new OldStressRowData();
            row715.LineNo = 35;
            row715.MaterialId = 9042;
            row715.SpecNo = "SA-182";
            row715.TypeGrade = "F316N";
            row715.ProductForm = "Forgings";
            row715.AlloyUNS = "S31651";
            row715.ClassCondition = "";
            row715.Notes = "G5";
            row715.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row715);

            // Row 36: SA-213, TP316N, Smls. tube
            var row716 = new OldStressRowData();
            row716.LineNo = 36;
            row716.MaterialId = 9043;
            row716.SpecNo = "SA-213";
            row716.TypeGrade = "TP316N";
            row716.ProductForm = "Smls. tube";
            row716.AlloyUNS = "S31651";
            row716.ClassCondition = "";
            row716.Notes = "G5, G12, G18, T7";
            row716.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row716);

            // Row 37: SA-213, TP316N, Smls. tube
            var row717 = new OldStressRowData();
            row717.LineNo = 37;
            row717.MaterialId = 9044;
            row717.SpecNo = "SA-213";
            row717.TypeGrade = "TP316N";
            row717.ProductForm = "Smls. tube";
            row717.AlloyUNS = "S31651";
            row717.ClassCondition = "";
            row717.Notes = "G12, G18, T8";
            row717.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row717);

            // Row 38: SA-240, 316N, Plate
            var row718 = new OldStressRowData();
            row718.LineNo = 38;
            row718.MaterialId = 9045;
            row718.SpecNo = "SA-240";
            row718.TypeGrade = "316N";
            row718.ProductForm = "Plate";
            row718.AlloyUNS = "S31651";
            row718.ClassCondition = "";
            row718.Notes = "G5, G12, T7";
            row718.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row718);

            // Row 39: SA-240, 316N, Plate
            var row719 = new OldStressRowData();
            row719.LineNo = 39;
            row719.MaterialId = 9046;
            row719.SpecNo = "SA-240";
            row719.TypeGrade = "316N";
            row719.ProductForm = "Plate";
            row719.AlloyUNS = "S31651";
            row719.ClassCondition = "";
            row719.Notes = "G12, T8";
            row719.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row719);

            // Row 1: SA-249, TP316N, Wld. tube
            var row720 = new OldStressRowData();
            row720.LineNo = 1;
            row720.MaterialId = 9047;
            row720.SpecNo = "SA-249";
            row720.TypeGrade = "TP316N";
            row720.ProductForm = "Wld. tube";
            row720.AlloyUNS = "S31651";
            row720.ClassCondition = "";
            row720.Notes = "G5, G12, G18, T7, W13";
            row720.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row720);

            // Row 2: SA-249, TP316N, Wld. tube
            var row721 = new OldStressRowData();
            row721.LineNo = 2;
            row721.MaterialId = 9050;
            row721.SpecNo = "SA-249";
            row721.TypeGrade = "TP316N";
            row721.ProductForm = "Wld. tube";
            row721.AlloyUNS = "S31651";
            row721.ClassCondition = "";
            row721.Notes = "G12, G18, T8, W13";
            row721.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row721);

            // Row 3: SA-249, TP316N, Wld. tube
            var row722 = new OldStressRowData();
            row722.LineNo = 3;
            row722.MaterialId = 9051;
            row722.SpecNo = "SA-249";
            row722.TypeGrade = "TP316N";
            row722.ProductForm = "Wld. tube";
            row722.AlloyUNS = "S31651";
            row722.ClassCondition = "";
            row722.Notes = "G3, G12, G18, G24, T8";
            row722.StressValues = new double?[] { 19.4, null, 17.6, null, 16.1, 15, 14, 13.3, 12.9, 12.6, 12.3, 12.1, 11.9, 11.6, 11.4, 11.2, 11, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row722);

            // Row 4: SA-249, TP316N, Wld. tube
            var row723 = new OldStressRowData();
            row723.LineNo = 4;
            row723.MaterialId = 9048;
            row723.SpecNo = "SA-249";
            row723.TypeGrade = "TP316N";
            row723.ProductForm = "Wld. tube";
            row723.AlloyUNS = "S31651";
            row723.ClassCondition = "";
            row723.Notes = "G3, G5, G12, G18, G24, T7";
            row723.StressValues = new double?[] { 19.4, null, 19.4, null, 18.7, 18.2, 18.1, 17.9, 17.4, 17, 16.7, 16.3, 16, 15.7, 15.4, 15.1, 13.4, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row723);

            // Row 5: SA-312, TP316N, Smls. & wld. pipe
            var row724 = new OldStressRowData();
            row724.LineNo = 5;
            row724.MaterialId = 9055;
            row724.SpecNo = "SA-312";
            row724.TypeGrade = "TP316N";
            row724.ProductForm = "Smls. & wld. pipe";
            row724.AlloyUNS = "S31651";
            row724.ClassCondition = "";
            row724.Notes = "G5, G12, G18, T7, W12, W13";
            row724.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row724);

            // Row 6: SA-312, TP316N, Smls. & wld. pipe
            var row725 = new OldStressRowData();
            row725.LineNo = 6;
            row725.MaterialId = 9058;
            row725.SpecNo = "SA-312";
            row725.TypeGrade = "TP316N";
            row725.ProductForm = "Smls. & wld. pipe";
            row725.AlloyUNS = "S31651";
            row725.ClassCondition = "";
            row725.Notes = "G12, G18, T8, W13";
            row725.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row725);

            // Row 7: SA-312, TP316N, Wld. pipe
            var row726 = new OldStressRowData();
            row726.LineNo = 7;
            row726.MaterialId = 9059;
            row726.SpecNo = "SA-312";
            row726.TypeGrade = "TP316N";
            row726.ProductForm = "Wld. pipe";
            row726.AlloyUNS = "S31651";
            row726.ClassCondition = "";
            row726.Notes = "G3, G12, G18, G24, T8";
            row726.StressValues = new double?[] { 19.4, null, 17.6, null, 16.1, 15, 14, 13.3, 12.9, 12.6, 12.3, 12.1, 11.9, 11.6, 11.4, 11.2, 11, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row726);

            // Row 8: SA-312, TP316N, Wld. pipe
            var row727 = new OldStressRowData();
            row727.LineNo = 8;
            row727.MaterialId = 9056;
            row727.SpecNo = "SA-312";
            row727.TypeGrade = "TP316N";
            row727.ProductForm = "Wld. pipe";
            row727.AlloyUNS = "S31651";
            row727.ClassCondition = "";
            row727.Notes = "G3, G5, G12, G18, G24, T7";
            row727.StressValues = new double?[] { 19.4, null, 19.4, null, 18.7, 18.2, 18.1, 17.9, 17.4, 17, 16.7, 16.3, 16, 15.7, 15.4, 15.1, 13.4, 10.5, 8.3, 6.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row727);

            // Row 9: SA-336, F316N, Forgings
            var row728 = new OldStressRowData();
            row728.LineNo = 9;
            row728.MaterialId = 9060;
            row728.SpecNo = "SA-336";
            row728.TypeGrade = "F316N";
            row728.ProductForm = "Forgings";
            row728.AlloyUNS = "S31651";
            row728.ClassCondition = "";
            row728.Notes = "G5, G12, T7";
            row728.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row728);

            // Row 10: SA-336, F316N, Forgings
            var row729 = new OldStressRowData();
            row729.LineNo = 10;
            row729.MaterialId = 9061;
            row729.SpecNo = "SA-336";
            row729.TypeGrade = "F316N";
            row729.ProductForm = "Forgings";
            row729.AlloyUNS = "S31651";
            row729.ClassCondition = "";
            row729.Notes = "G12, T8";
            row729.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row729);

            // Row 11: SA-358, 316N, Wld. pipe
            var row730 = new OldStressRowData();
            row730.LineNo = 11;
            row730.MaterialId = 9062;
            row730.SpecNo = "SA-358";
            row730.TypeGrade = "316N";
            row730.ProductForm = "Wld. pipe";
            row730.AlloyUNS = "S31651";
            row730.ClassCondition = "1";
            row730.Notes = "G5, W12";
            row730.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row730);

            // Row 12: SA-376, TP316N, Smls. pipe
            var row731 = new OldStressRowData();
            row731.LineNo = 12;
            row731.MaterialId = 9063;
            row731.SpecNo = "SA-376";
            row731.TypeGrade = "TP316N";
            row731.ProductForm = "Smls. pipe";
            row731.AlloyUNS = "S31651";
            row731.ClassCondition = "";
            row731.Notes = "G5, G12, G18, H1, T7";
            row731.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row731);

            // Row 13: SA-376, TP316N, Smls. pipe
            var row732 = new OldStressRowData();
            row732.LineNo = 13;
            row732.MaterialId = 9064;
            row732.SpecNo = "SA-376";
            row732.TypeGrade = "TP316N";
            row732.ProductForm = "Smls. pipe";
            row732.AlloyUNS = "S31651";
            row732.ClassCondition = "";
            row732.Notes = "G12, G18, H1, T8";
            row732.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row732);

            // Row 14: SA-403, 316N, Smls. & wld. fittings
            var row733 = new OldStressRowData();
            row733.LineNo = 14;
            row733.MaterialId = 9070;
            row733.SpecNo = "SA-403";
            row733.TypeGrade = "316N";
            row733.ProductForm = "Smls. & wld. fittings";
            row733.AlloyUNS = "S31651";
            row733.ClassCondition = "";
            row733.Notes = "G5, G12, T7, W12, W14";
            row733.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row733);

            // Row 15: SA-479, 316N, Bar
            var row734 = new OldStressRowData();
            row734.LineNo = 15;
            row734.MaterialId = 9071;
            row734.SpecNo = "SA-479";
            row734.TypeGrade = "316N";
            row734.ProductForm = "Bar";
            row734.AlloyUNS = "S31651";
            row734.ClassCondition = "";
            row734.Notes = "G5, G12, G18, H1, T7";
            row734.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, 18.8, 18.5, 18.1, 17.8, 15.8, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row734);

            // Row 16: SA-479, 316N, Bar
            var row735 = new OldStressRowData();
            row735.LineNo = 16;
            row735.MaterialId = 9072;
            row735.SpecNo = "SA-479";
            row735.TypeGrade = "316N";
            row735.ProductForm = "Bar";
            row735.AlloyUNS = "S31651";
            row735.ClassCondition = "";
            row735.Notes = "G12, G18, H1, T8";
            row735.StressValues = new double?[] { 22.9, null, 20.7, null, 19, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.4, 13.2, 12.9, 12.3, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row735);

            // Row 17: SA-688, TP316N, Wld. tube
            var row736 = new OldStressRowData();
            row736.LineNo = 17;
            row736.MaterialId = 9073;
            row736.SpecNo = "SA-688";
            row736.TypeGrade = "TP316N";
            row736.ProductForm = "Wld. tube";
            row736.AlloyUNS = "S31651";
            row736.ClassCondition = "";
            row736.Notes = "G5, W12";
            row736.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row736);

            // Row 18: SA-813, TP316N, Wld. pipe
            var row737 = new OldStressRowData();
            row737.LineNo = 18;
            row737.MaterialId = 9074;
            row737.SpecNo = "SA-813";
            row737.TypeGrade = "TP316N";
            row737.ProductForm = "Wld. pipe";
            row737.AlloyUNS = "S31651";
            row737.ClassCondition = "";
            row737.Notes = "G5, W12";
            row737.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row737);

            // Row 19: SA-814, TP316N, Wld. pipe
            var row738 = new OldStressRowData();
            row738.LineNo = 19;
            row738.MaterialId = 9075;
            row738.SpecNo = "SA-814";
            row738.TypeGrade = "TP316N";
            row738.ProductForm = "Wld. pipe";
            row738.AlloyUNS = "S31651";
            row738.ClassCondition = "";
            row738.Notes = "G5, W12";
            row738.StressValues = new double?[] { 22.9, null, 22.9, null, 22, 21.5, 21.2, 21, 20.5, 20, 19.6, 19.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row738);

            // Row 20: SA-240, 316Ti, Plate
            var row739 = new OldStressRowData();
            row739.LineNo = 20;
            row739.MaterialId = 9076;
            row739.SpecNo = "SA-240";
            row739.TypeGrade = "316Ti";
            row739.ProductForm = "Plate";
            row739.AlloyUNS = "S31635";
            row739.ClassCondition = "";
            row739.Notes = "G5, G12, T8";
            row739.StressValues = new double?[] { 20, null, 20, null, 20, 19.4, 17.8, 16.8, 16.5, 16.2, 16, 15.9, 15.8, 15.7, 15.5, 15.3, 15.1, 12.3, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row739);

            // Row 21: SA-240, 316Ti, Plate
            var row740 = new OldStressRowData();
            row740.LineNo = 21;
            row740.MaterialId = 9077;
            row740.SpecNo = "SA-240";
            row740.TypeGrade = "316Ti";
            row740.ProductForm = "Plate";
            row740.AlloyUNS = "S31635";
            row740.ClassCondition = "";
            row740.Notes = "G12, T9";
            row740.StressValues = new double?[] { 20, null, 17.7, null, 15.8, 14.3, 13.2, 12.4, 12.2, 12, 11.9, 11.8, 11.7, 11.6, 11.5, 11.4, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row740);

            // Row 22: SA-240, XM-29, Plate
            var row741 = new OldStressRowData();
            row741.LineNo = 22;
            row741.MaterialId = 9085;
            row741.SpecNo = "SA-240";
            row741.TypeGrade = "XM-29";
            row741.ProductForm = "Plate";
            row741.AlloyUNS = "S24000";
            row741.ClassCondition = "";
            row741.Notes = "G5";
            row741.StressValues = new double?[] { 28.6, null, 27.9, null, 26, 24.9, 24.3, 23.8, 23.4, 23, 22.4, 21.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row741);

            // Row 23: SA-240, XM-29, Plate
            var row742 = new OldStressRowData();
            row742.LineNo = 23;
            row742.MaterialId = 9086;
            row742.SpecNo = "SA-240";
            row742.TypeGrade = "XM-29";
            row742.ProductForm = "Plate";
            row742.AlloyUNS = "S24000";
            row742.ClassCondition = "";
            row742.Notes = "";
            row742.StressValues = new double?[] { 28.6, null, 27.9, null, 25, 21.9, 20.1, 19.2, 18.8, 18.5, 18.2, 17.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row742);

            // Row 24: SA-249, XM-29, Wld. tube
            var row743 = new OldStressRowData();
            row743.LineNo = 24;
            row743.MaterialId = 9087;
            row743.SpecNo = "SA-249";
            row743.TypeGrade = "XM-29";
            row743.ProductForm = "Wld. tube";
            row743.AlloyUNS = "S24000";
            row743.ClassCondition = "";
            row743.Notes = "G5, G24";
            row743.StressValues = new double?[] { 24.3, null, 23.7, null, 22.1, 21.2, 20.7, 20.2, 19.9, 19.5, 19.1, 18.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row743);

            // Row 25: SA-249, XM-29, Wld. tube
            var row744 = new OldStressRowData();
            row744.LineNo = 25;
            row744.MaterialId = 9088;
            row744.SpecNo = "SA-249";
            row744.TypeGrade = "XM-29";
            row744.ProductForm = "Wld. tube";
            row744.AlloyUNS = "S24000";
            row744.ClassCondition = "";
            row744.Notes = "G24";
            row744.StressValues = new double?[] { 24.3, null, 23.7, null, 21.3, 18.7, 17.1, 16.3, 16, 15.7, 15.4, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row744);

            // Row 26: SA-312, XM-29, Wld. pipe
            var row745 = new OldStressRowData();
            row745.LineNo = 26;
            row745.MaterialId = 9089;
            row745.SpecNo = "SA-312";
            row745.TypeGrade = "XM-29";
            row745.ProductForm = "Wld. pipe";
            row745.AlloyUNS = "S24000";
            row745.ClassCondition = "";
            row745.Notes = "G5, G24";
            row745.StressValues = new double?[] { 24.3, null, 23.7, null, 22.1, 21.2, 20.7, 20.2, 19.9, 19.5, 19.1, 18.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row745);

            // Row 27: SA-312, XM-29, Wld. pipe
            var row746 = new OldStressRowData();
            row746.LineNo = 27;
            row746.MaterialId = 9090;
            row746.SpecNo = "SA-312";
            row746.TypeGrade = "XM-29";
            row746.ProductForm = "Wld. pipe";
            row746.AlloyUNS = "S24000";
            row746.ClassCondition = "";
            row746.Notes = "G24";
            row746.StressValues = new double?[] { 24.3, null, 23.7, null, 21.3, 18.7, 17.1, 16.3, 16, 15.7, 15.4, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row746);

            // Row 28: SA-479, XM-29, Bar
            var row747 = new OldStressRowData();
            row747.LineNo = 28;
            row747.MaterialId = 9091;
            row747.SpecNo = "SA-479";
            row747.TypeGrade = "XM-29";
            row747.ProductForm = "Bar";
            row747.AlloyUNS = "S24000";
            row747.ClassCondition = "";
            row747.Notes = "G5, G22";
            row747.StressValues = new double?[] { 28.6, null, 27.9, null, 26, 24.9, 24.3, 23.8, 23.4, 23, 22.4, 21.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row747);

            // Row 29: SA-479, XM-29, Bar
            var row748 = new OldStressRowData();
            row748.LineNo = 29;
            row748.MaterialId = 9092;
            row748.SpecNo = "SA-479";
            row748.TypeGrade = "XM-29";
            row748.ProductForm = "Bar";
            row748.AlloyUNS = "S24000";
            row748.ClassCondition = "";
            row748.Notes = "G22";
            row748.StressValues = new double?[] { 28.6, null, 27.9, null, 25, 21.9, 20.1, 19.2, 18.8, 18.5, 18.2, 17.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row748);

            // Row 30: SA-688, TPXM-29, Wld. tube
            var row749 = new OldStressRowData();
            row749.LineNo = 30;
            row749.MaterialId = 9093;
            row749.SpecNo = "SA-688";
            row749.TypeGrade = "TPXM-29";
            row749.ProductForm = "Wld. tube";
            row749.AlloyUNS = "S24000";
            row749.ClassCondition = "";
            row749.Notes = "G5, G24";
            row749.StressValues = new double?[] { 24.3, null, 23.7, null, 22.1, 21.2, 20.7, 20.2, 19.9, 19.5, 19.1, 18.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row749);

            // Row 31: SA-688, TPXM-29, Wld. tube
            var row750 = new OldStressRowData();
            row750.LineNo = 31;
            row750.MaterialId = 9094;
            row750.SpecNo = "SA-688";
            row750.TypeGrade = "TPXM-29";
            row750.ProductForm = "Wld. tube";
            row750.AlloyUNS = "S24000";
            row750.ClassCondition = "";
            row750.Notes = "G24";
            row750.StressValues = new double?[] { 24.3, null, 23.7, null, 21.3, 18.7, 17.1, 16.3, 16, 15.7, 15.4, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row750);

            // Row 32: SA-789, , Smls. tube
            var row751 = new OldStressRowData();
            row751.LineNo = 32;
            row751.MaterialId = 9095;
            row751.SpecNo = "SA-789";
            row751.TypeGrade = "";
            row751.ProductForm = "Smls. tube";
            row751.AlloyUNS = "S31500";
            row751.ClassCondition = "";
            row751.Notes = "G32";
            row751.StressValues = new double?[] { 26.3, null, 25.4, null, 24.4, 24.2, 24.2, 24.2, 24.2, 24.2, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row751);

            // Row 33: SA-789, , Wld. tube
            var row752 = new OldStressRowData();
            row752.LineNo = 33;
            row752.MaterialId = 9096;
            row752.SpecNo = "SA-789";
            row752.TypeGrade = "";
            row752.ProductForm = "Wld. tube";
            row752.AlloyUNS = "S31500";
            row752.ClassCondition = "";
            row752.Notes = "G24, G32";
            row752.StressValues = new double?[] { 22.3, null, 21.6, null, 20.7, 20.6, 20.6, 20.6, 20.6, 20.6, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row752);

            // Row 34: SA-790, , Smls. pipe
            var row753 = new OldStressRowData();
            row753.LineNo = 34;
            row753.MaterialId = 9097;
            row753.SpecNo = "SA-790";
            row753.TypeGrade = "";
            row753.ProductForm = "Smls. pipe";
            row753.AlloyUNS = "S31500";
            row753.ClassCondition = "";
            row753.Notes = "G32";
            row753.StressValues = new double?[] { 26.3, null, 25.4, null, 24.4, 24.2, 24.2, 24.2, 24.2, 24.2, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row753);

            // Row 35: SA-790, , Wld. pipe
            var row754 = new OldStressRowData();
            row754.LineNo = 35;
            row754.MaterialId = 9098;
            row754.SpecNo = "SA-790";
            row754.TypeGrade = "";
            row754.ProductForm = "Wld. pipe";
            row754.AlloyUNS = "S31500";
            row754.ClassCondition = "";
            row754.Notes = "G24, G32";
            row754.StressValues = new double?[] { 22.3, null, 21.6, null, 20.7, 20.6, 20.6, 20.6, 20.6, 20.6, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row754);

            // Row 36: SA-182, F304L, Forgings
            var row755 = new OldStressRowData();
            row755.LineNo = 36;
            row755.MaterialId = 9099;
            row755.SpecNo = "SA-182";
            row755.TypeGrade = "F304L";
            row755.ProductForm = "Forgings";
            row755.AlloyUNS = "S30403";
            row755.ClassCondition = "";
            row755.Notes = "G5, G42";
            row755.StressValues = new double?[] { 16.7, null, 16.7, null, 16.2, 15.6, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row755);

            // Row 37: SA-182, F304L, Forgings
            var row756 = new OldStressRowData();
            row756.LineNo = 37;
            row756.MaterialId = 9100;
            row756.SpecNo = "SA-182";
            row756.TypeGrade = "F304L";
            row756.ProductForm = "Forgings";
            row756.AlloyUNS = "S30403";
            row756.ClassCondition = "";
            row756.Notes = "G42";
            row756.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row756);

            // Row 38: SA-336, F304L, Forgings
            var row757 = new OldStressRowData();
            row757.LineNo = 38;
            row757.MaterialId = 9101;
            row757.SpecNo = "SA-336";
            row757.TypeGrade = "F304L";
            row757.ProductForm = "Forgings";
            row757.AlloyUNS = "S30403";
            row757.ClassCondition = "";
            row757.Notes = "G42";
            row757.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row757);

            // Row 1: SA-182, F304L, Forgings
            var row758 = new OldStressRowData();
            row758.LineNo = 1;
            row758.MaterialId = 9102;
            row758.SpecNo = "SA-182";
            row758.TypeGrade = "F304L";
            row758.ProductForm = "Forgings";
            row758.AlloyUNS = "S30403";
            row758.ClassCondition = "";
            row758.Notes = "G5, G42";
            row758.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row758);

            // Row 2: SA-182, F304L, Forgings
            var row759 = new OldStressRowData();
            row759.LineNo = 2;
            row759.MaterialId = 9841;
            row759.SpecNo = "SA-182";
            row759.TypeGrade = "F304L";
            row759.ProductForm = "Forgings";
            row759.AlloyUNS = "S30403";
            row759.ClassCondition = "";
            row759.Notes = "G42";
            row759.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row759);

            // Row 3: SA-213, TP304L, Smls. tube
            var row760 = new OldStressRowData();
            row760.LineNo = 3;
            row760.MaterialId = 9103;
            row760.SpecNo = "SA-213";
            row760.TypeGrade = "TP304L";
            row760.ProductForm = "Smls. tube";
            row760.AlloyUNS = "S30403";
            row760.ClassCondition = "";
            row760.Notes = "G5, G42";
            row760.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row760);

            // Row 4: SA-213, TP304L, Smls. tube
            var row761 = new OldStressRowData();
            row761.LineNo = 4;
            row761.MaterialId = 9104;
            row761.SpecNo = "SA-213";
            row761.TypeGrade = "TP304L";
            row761.ProductForm = "Smls. tube";
            row761.AlloyUNS = "S30403";
            row761.ClassCondition = "";
            row761.Notes = "G42";
            row761.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row761);

            // Row 5: SA-240, 304L, Plate
            var row762 = new OldStressRowData();
            row762.LineNo = 5;
            row762.MaterialId = 9105;
            row762.SpecNo = "SA-240";
            row762.TypeGrade = "304L";
            row762.ProductForm = "Plate";
            row762.AlloyUNS = "S30403";
            row762.ClassCondition = "";
            row762.Notes = "G5, G42";
            row762.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row762);

            // Row 6: SA-240, 304L, Plate
            var row763 = new OldStressRowData();
            row763.LineNo = 6;
            row763.MaterialId = 9106;
            row763.SpecNo = "SA-240";
            row763.TypeGrade = "304L";
            row763.ProductForm = "Plate";
            row763.AlloyUNS = "S30403";
            row763.ClassCondition = "";
            row763.Notes = "G42";
            row763.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row763);

            // Row 7: SA-249, TP304L, Wld. tube
            var row764 = new OldStressRowData();
            row764.LineNo = 7;
            row764.MaterialId = 9107;
            row764.SpecNo = "SA-249";
            row764.TypeGrade = "TP304L";
            row764.ProductForm = "Wld. tube";
            row764.AlloyUNS = "S30403";
            row764.ClassCondition = "";
            row764.Notes = "G5, W12";
            row764.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row764);

            // Row 8: SA-249, TP304L, Wld. tube
            var row765 = new OldStressRowData();
            row765.LineNo = 8;
            row765.MaterialId = 9108;
            row765.SpecNo = "SA-249";
            row765.TypeGrade = "TP304L";
            row765.ProductForm = "Wld. tube";
            row765.AlloyUNS = "S30403";
            row765.ClassCondition = "";
            row765.Notes = "G5, G24, G42";
            row765.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row765);

            // Row 9: SA-249, TP304L, Wld. tube
            var row766 = new OldStressRowData();
            row766.LineNo = 9;
            row766.MaterialId = 9109;
            row766.SpecNo = "SA-249";
            row766.TypeGrade = "TP304L";
            row766.ProductForm = "Wld. tube";
            row766.AlloyUNS = "S30403";
            row766.ClassCondition = "";
            row766.Notes = "G24, G42";
            row766.StressValues = new double?[] { 14.2, null, 12.1, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row766);

            // Row 10: SA-312, TP304L, Smls. & wld. pipe
            var row767 = new OldStressRowData();
            row767.LineNo = 10;
            row767.MaterialId = 9110;
            row767.SpecNo = "SA-312";
            row767.TypeGrade = "TP304L";
            row767.ProductForm = "Smls. & wld. pipe";
            row767.AlloyUNS = "S30403";
            row767.ClassCondition = "";
            row767.Notes = "G5, G42, W12";
            row767.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row767);

            // Row 11: SA-312, TP304L, Smls. pipe
            var row768 = new OldStressRowData();
            row768.LineNo = 11;
            row768.MaterialId = 9111;
            row768.SpecNo = "SA-312";
            row768.TypeGrade = "TP304L";
            row768.ProductForm = "Smls. pipe";
            row768.AlloyUNS = "S30403";
            row768.ClassCondition = "";
            row768.Notes = "G42";
            row768.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row768);

            // Row 12: SA-312, TP304L, Wld. pipe
            var row769 = new OldStressRowData();
            row769.LineNo = 12;
            row769.MaterialId = 9112;
            row769.SpecNo = "SA-312";
            row769.TypeGrade = "TP304L";
            row769.ProductForm = "Wld. pipe";
            row769.AlloyUNS = "S30403";
            row769.ClassCondition = "";
            row769.Notes = "G5, G24, G42";
            row769.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row769);

            // Row 13: SA-312, TP304L, Wld. pipe
            var row770 = new OldStressRowData();
            row770.LineNo = 13;
            row770.MaterialId = 9114;
            row770.SpecNo = "SA-312";
            row770.TypeGrade = "TP304L";
            row770.ProductForm = "Wld. pipe";
            row770.AlloyUNS = "S30403";
            row770.ClassCondition = "";
            row770.Notes = "G24, G42";
            row770.StressValues = new double?[] { 14.2, null, 12.1, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row770);

            // Row 14: SA-358, 304L, Wld. pipe
            var row771 = new OldStressRowData();
            row771.LineNo = 14;
            row771.MaterialId = 9115;
            row771.SpecNo = "SA-358";
            row771.TypeGrade = "304L";
            row771.ProductForm = "Wld. pipe";
            row771.AlloyUNS = "S30403";
            row771.ClassCondition = "1";
            row771.Notes = "G5";
            row771.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row771);

            // Row 15: SA-403, 304L, Smls. & wld. fittings
            var row772 = new OldStressRowData();
            row772.LineNo = 15;
            row772.MaterialId = 9121;
            row772.SpecNo = "SA-403";
            row772.TypeGrade = "304L";
            row772.ProductForm = "Smls. & wld. fittings";
            row772.AlloyUNS = "S30403";
            row772.ClassCondition = "";
            row772.Notes = "G5, W12, W14";
            row772.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row772);

            // Row 16: SA-409, TP304L, Wld. pipe
            var row773 = new OldStressRowData();
            row773.LineNo = 16;
            row773.MaterialId = 9122;
            row773.SpecNo = "SA-409";
            row773.TypeGrade = "TP304L";
            row773.ProductForm = "Wld. pipe";
            row773.AlloyUNS = "S30403";
            row773.ClassCondition = "";
            row773.Notes = "G5, W12";
            row773.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row773);

            // Row 17: SA-479, 304L, Bar
            var row774 = new OldStressRowData();
            row774.LineNo = 17;
            row774.MaterialId = 9123;
            row774.SpecNo = "SA-479";
            row774.TypeGrade = "304L";
            row774.ProductForm = "Bar";
            row774.AlloyUNS = "S30403";
            row774.ClassCondition = "";
            row774.Notes = "G5, G22, G42";
            row774.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row774);

            // Row 18: SA-479, 304L, Bar
            var row775 = new OldStressRowData();
            row775.LineNo = 18;
            row775.MaterialId = 9124;
            row775.SpecNo = "SA-479";
            row775.TypeGrade = "304L";
            row775.ProductForm = "Bar";
            row775.AlloyUNS = "S30403";
            row775.ClassCondition = "";
            row775.Notes = "G22, G42";
            row775.StressValues = new double?[] { 16.7, null, 14.3, null, 12.8, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row775);

            // Row 19: SA-688, TP304L, Wld. tube
            var row776 = new OldStressRowData();
            row776.LineNo = 19;
            row776.MaterialId = 9125;
            row776.SpecNo = "SA-688";
            row776.TypeGrade = "TP304L";
            row776.ProductForm = "Wld. tube";
            row776.AlloyUNS = "S30403";
            row776.ClassCondition = "";
            row776.Notes = "G5, W12";
            row776.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row776);

            // Row 20: SA-688, TP304L, Wld. tube
            var row777 = new OldStressRowData();
            row777.LineNo = 20;
            row777.MaterialId = 9126;
            row777.SpecNo = "SA-688";
            row777.TypeGrade = "TP304L";
            row777.ProductForm = "Wld. tube";
            row777.AlloyUNS = "S30403";
            row777.ClassCondition = "";
            row777.Notes = "G5, G24";
            row777.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row777);

            // Row 21: SA-688, TP304L, Wld. tube
            var row778 = new OldStressRowData();
            row778.LineNo = 21;
            row778.MaterialId = 9127;
            row778.SpecNo = "SA-688";
            row778.TypeGrade = "TP304L";
            row778.ProductForm = "Wld. tube";
            row778.AlloyUNS = "S30403";
            row778.ClassCondition = "";
            row778.Notes = "G24";
            row778.StressValues = new double?[] { 14.2, null, 12.1, null, 10.9, 9.9, 9.3, 8.8, 8.6, 8.5, 8.3, 8.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row778);

            // Row 22: SA-813, TP304L, Wld. pipe
            var row779 = new OldStressRowData();
            row779.LineNo = 22;
            row779.MaterialId = 9128;
            row779.SpecNo = "SA-813";
            row779.TypeGrade = "TP304L";
            row779.ProductForm = "Wld. pipe";
            row779.AlloyUNS = "S30403";
            row779.ClassCondition = "";
            row779.Notes = "G5, W12";
            row779.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row779);

            // Row 23: SA-814, TP304L, Wld. pipe
            var row780 = new OldStressRowData();
            row780.LineNo = 23;
            row780.MaterialId = 9129;
            row780.SpecNo = "SA-814";
            row780.TypeGrade = "TP304L";
            row780.ProductForm = "Wld. pipe";
            row780.AlloyUNS = "S30403";
            row780.ClassCondition = "";
            row780.Notes = "G5, W12";
            row780.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.8, 14.7, 14, 13.7, 13.5, 13.3, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row780);

            // Row 24: SA-182, F304, Forgings
            var row781 = new OldStressRowData();
            row781.LineNo = 24;
            row781.MaterialId = 9130;
            row781.SpecNo = "SA-182";
            row781.TypeGrade = "F304";
            row781.ProductForm = "Forgings";
            row781.AlloyUNS = "S30400";
            row781.ClassCondition = "";
            row781.Notes = "G5, G12, G18, T7";
            row781.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row781);

            // Row 25: SA-182, F304, Forgings
            var row782 = new OldStressRowData();
            row782.LineNo = 25;
            row782.MaterialId = 9131;
            row782.SpecNo = "SA-182";
            row782.TypeGrade = "F304";
            row782.ProductForm = "Forgings";
            row782.AlloyUNS = "S30400";
            row782.ClassCondition = "";
            row782.Notes = "G12, G18, T8";
            row782.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row782);

            // Row 26: SA-182, F304H, Forgings
            var row783 = new OldStressRowData();
            row783.LineNo = 26;
            row783.MaterialId = 9133;
            row783.SpecNo = "SA-182";
            row783.TypeGrade = "F304H";
            row783.ProductForm = "Forgings";
            row783.AlloyUNS = "S30409";
            row783.ClassCondition = "";
            row783.Notes = "G5, G18, T7";
            row783.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row783);

            // Row 27: SA-182, F304H, Forgings
            var row784 = new OldStressRowData();
            row784.LineNo = 27;
            row784.MaterialId = 9132;
            row784.SpecNo = "SA-182";
            row784.TypeGrade = "F304H";
            row784.ProductForm = "Forgings";
            row784.AlloyUNS = "S30409";
            row784.ClassCondition = "";
            row784.Notes = "G18, T8";
            row784.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row784);

            // Row 28: SA-336, F304, Forgings
            var row785 = new OldStressRowData();
            row785.LineNo = 28;
            row785.MaterialId = 9134;
            row785.SpecNo = "SA-336";
            row785.TypeGrade = "F304";
            row785.ProductForm = "Forgings";
            row785.AlloyUNS = "S30400";
            row785.ClassCondition = "";
            row785.Notes = "G5, G12, G18, T7";
            row785.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row785);

            // Row 29: SA-336, F304, Forgings
            var row786 = new OldStressRowData();
            row786.LineNo = 29;
            row786.MaterialId = 9135;
            row786.SpecNo = "SA-336";
            row786.TypeGrade = "F304";
            row786.ProductForm = "Forgings";
            row786.AlloyUNS = "S30400";
            row786.ClassCondition = "";
            row786.Notes = "G12, G18, T8";
            row786.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row786);

            // Row 30: SA-336, F304H, Forgings
            var row787 = new OldStressRowData();
            row787.LineNo = 30;
            row787.MaterialId = 9136;
            row787.SpecNo = "SA-336";
            row787.TypeGrade = "F304H";
            row787.ProductForm = "Forgings";
            row787.AlloyUNS = "S30409";
            row787.ClassCondition = "";
            row787.Notes = "G5, T7";
            row787.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row787);

            // Row 31: SA-336, F304H, Forgings
            var row788 = new OldStressRowData();
            row788.LineNo = 31;
            row788.MaterialId = 9137;
            row788.SpecNo = "SA-336";
            row788.TypeGrade = "F304H";
            row788.ProductForm = "Forgings";
            row788.AlloyUNS = "S30409";
            row788.ClassCondition = "";
            row788.Notes = "T8";
            row788.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row788);

            // Row 32: SA-351, CF3, Castings
            var row789 = new OldStressRowData();
            row789.LineNo = 32;
            row789.MaterialId = 9138;
            row789.SpecNo = "SA-351";
            row789.TypeGrade = "CF3";
            row789.ProductForm = "Castings";
            row789.AlloyUNS = "J92500";
            row789.ClassCondition = "";
            row789.Notes = "G1, G5, G16, G17, G32";
            row789.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row789);

            // Row 33: SA-351, CF3, Castings
            var row790 = new OldStressRowData();
            row790.LineNo = 33;
            row790.MaterialId = 9139;
            row790.SpecNo = "SA-351";
            row790.TypeGrade = "CF3";
            row790.ProductForm = "Castings";
            row790.AlloyUNS = "J92500";
            row790.ClassCondition = "";
            row790.Notes = "G1, G32";
            row790.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row790);

            // Row 34: SA-351, CF8, Castings
            var row791 = new OldStressRowData();
            row791.LineNo = 34;
            row791.MaterialId = 9141;
            row791.SpecNo = "SA-351";
            row791.TypeGrade = "CF8";
            row791.ProductForm = "Castings";
            row791.AlloyUNS = "J92600";
            row791.ClassCondition = "";
            row791.Notes = "G1, G5, G12, G16, G17, G18, G32, H1, T6";
            row791.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row791);

            // Row 35: SA-351, CF8, Castings
            var row792 = new OldStressRowData();
            row792.LineNo = 35;
            row792.MaterialId = 9142;
            row792.SpecNo = "SA-351";
            row792.TypeGrade = "CF8";
            row792.ProductForm = "Castings";
            row792.AlloyUNS = "J92600";
            row792.ClassCondition = "";
            row792.Notes = "G1, G12, G18, G32, H1, T7";
            row792.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row792);

            // Row 36: SA-351, CF8, Castings
            var row793 = new OldStressRowData();
            row793.LineNo = 36;
            row793.MaterialId = 9140;
            row793.SpecNo = "SA-351";
            row793.TypeGrade = "CF8";
            row793.ProductForm = "Castings";
            row793.AlloyUNS = "J92600";
            row793.ClassCondition = "";
            row793.Notes = "G1, G5, G12, G32, T6";
            row793.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 12.2, 9.5, 7.5, 6, 4.8, 3.9, 3.3, 2.7, 2.3, 2, 1.7, null, null, null };
            batch.Add(row793);

            // Row 37: SA-376, TP304, Smls. pipe
            var row794 = new OldStressRowData();
            row794.LineNo = 37;
            row794.MaterialId = 9144;
            row794.SpecNo = "SA-376";
            row794.TypeGrade = "TP304";
            row794.ProductForm = "Smls. pipe";
            row794.AlloyUNS = "S30400";
            row794.ClassCondition = "";
            row794.Notes = "G5, G12, S9, T7";
            row794.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row794);

            // Row 38: SA-376, TP304, Smls. pipe
            var row795 = new OldStressRowData();
            row795.LineNo = 38;
            row795.MaterialId = 9143;
            row795.SpecNo = "SA-376";
            row795.TypeGrade = "TP304";
            row795.ProductForm = "Smls. pipe";
            row795.AlloyUNS = "S30400";
            row795.ClassCondition = "";
            row795.Notes = "G12, S9, T8";
            row795.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row795);

            // Row 39: SA-430, FP304, Forged pipe
            var row796 = new OldStressRowData();
            row796.LineNo = 39;
            row796.MaterialId = 9145;
            row796.SpecNo = "SA-430";
            row796.TypeGrade = "FP304";
            row796.ProductForm = "Forged pipe";
            row796.AlloyUNS = "S30400";
            row796.ClassCondition = "";
            row796.Notes = "G5, G12, G18, H1, T7";
            row796.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row796);

            // Row 40: SA-430, FP304, Forged pipe
            var row797 = new OldStressRowData();
            row797.LineNo = 40;
            row797.MaterialId = 9146;
            row797.SpecNo = "SA-430";
            row797.TypeGrade = "FP304";
            row797.ProductForm = "Forged pipe";
            row797.AlloyUNS = "S30400";
            row797.ClassCondition = "";
            row797.Notes = "G12, G18, H1, T8";
            row797.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row797);

            // Row 1: SA-430, FP304H, Forged pipe
            var row798 = new OldStressRowData();
            row798.LineNo = 1;
            row798.MaterialId = 9147;
            row798.SpecNo = "SA-430";
            row798.TypeGrade = "FP304H";
            row798.ProductForm = "Forged pipe";
            row798.AlloyUNS = "S30409";
            row798.ClassCondition = "";
            row798.Notes = "G5, G18, H1, T7";
            row798.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, 14.9, 14.6, 14.3, 14, 12.4, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row798);

            // Row 2: SA-430, FP304H, Forged pipe
            var row799 = new OldStressRowData();
            row799.LineNo = 2;
            row799.MaterialId = 9148;
            row799.SpecNo = "SA-430";
            row799.TypeGrade = "FP304H";
            row799.ProductForm = "Forged pipe";
            row799.AlloyUNS = "S30409";
            row799.ClassCondition = "";
            row799.Notes = "G18, H1, T8";
            row799.StressValues = new double?[] { 20, null, 16.7, null, 15, 13.8, 12.9, 12.3, 12, 11.7, 11.5, 11.2, 11, 10.8, 10.6, 10.4, 10.1, 9.8, 7.7, 6.1, 4.7, 3.7, 2.9, 2.3, 1.8, 1.4, null, null, null };
            batch.Add(row799);

            // Row 3: SA-451, CPF3, Cast pipe
            var row800 = new OldStressRowData();
            row800.LineNo = 3;
            row800.MaterialId = 9149;
            row800.SpecNo = "SA-451";
            row800.TypeGrade = "CPF3";
            row800.ProductForm = "Cast pipe";
            row800.AlloyUNS = "J92500";
            row800.ClassCondition = "";
            row800.Notes = "G5, G16, G17, G32";
            row800.StressValues = new double?[] { 20, null, 18.9, null, 17.7, 17.1, 16.9, 16.6, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row800);

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
