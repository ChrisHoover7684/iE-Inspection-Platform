using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch008
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

            // Row 39: SA-451, CPF3M, Cast pipe
            var row701 = new OldStressRowData();
            row701.LineNo = 39;
            row701.MaterialId = 8957;
            row701.SpecNo = "SA-451";
            row701.TypeGrade = "CPF3M";
            row701.ProductForm = "Cast pipe";
            row701.AlloyUNS = "J92800";
            row701.ClassCondition = "";
            row701.Notes = "G5, G16, G17, G32";
            row701.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row701);

            // Row 1: SA-351, CF8M, Castings
            var row702 = new OldStressRowData();
            row702.LineNo = 1;
            row702.MaterialId = 8950;
            row702.SpecNo = "SA-351";
            row702.TypeGrade = "CF8M";
            row702.ProductForm = "Castings";
            row702.AlloyUNS = "J92900";
            row702.ClassCondition = "";
            row702.Notes = "G5, G16, G17, G32";
            row702.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row702);

            // Row 2: SA-351, CF8M, Castings
            var row703 = new OldStressRowData();
            row703.LineNo = 2;
            row703.MaterialId = 8951;
            row703.SpecNo = "SA-351";
            row703.TypeGrade = "CF8M";
            row703.ProductForm = "Castings";
            row703.AlloyUNS = "J92900";
            row703.ClassCondition = "";
            row703.Notes = "G1, G5, G12, G18, G32, H1";
            row703.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.6, 16.3, 16, 15.8, 15.7, 15.5, 15.4, 14.9, 11.5, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row703);

            // Row 3: SA-351, CF8M, Castings
            var row704 = new OldStressRowData();
            row704.LineNo = 3;
            row704.MaterialId = 8952;
            row704.SpecNo = "SA-351";
            row704.TypeGrade = "CF8M";
            row704.ProductForm = "Castings";
            row704.AlloyUNS = "J92900";
            row704.ClassCondition = "";
            row704.Notes = "G1, G12, G18, G32, H1";
            row704.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row704);

            // Row 4: SA-451, CPF8M, Cast pipe
            var row705 = new OldStressRowData();
            row705.LineNo = 4;
            row705.MaterialId = 8958;
            row705.SpecNo = "SA-451";
            row705.TypeGrade = "CPF8M";
            row705.ProductForm = "Cast pipe";
            row705.AlloyUNS = "J92900";
            row705.ClassCondition = "";
            row705.Notes = "G5, G16, G17, G32";
            row705.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row705);

            // Row 5: SA-182, F316, Forgings
            var row706 = new OldStressRowData();
            row706.LineNo = 5;
            row706.MaterialId = 8939;
            row706.SpecNo = "SA-182";
            row706.TypeGrade = "F316";
            row706.ProductForm = "Forgings";
            row706.AlloyUNS = "S31600";
            row706.ClassCondition = "";
            row706.Notes = "G5, G12, G18";
            row706.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row706);

            // Row 6: SA-182, F316, Forgings
            var row707 = new OldStressRowData();
            row707.LineNo = 6;
            row707.MaterialId = 8940;
            row707.SpecNo = "SA-182";
            row707.TypeGrade = "F316";
            row707.ProductForm = "Forgings";
            row707.AlloyUNS = "S31600";
            row707.ClassCondition = "";
            row707.Notes = "G12, G18";
            row707.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row707);

            // Row 7: SA-336, F316, Forgings
            var row708 = new OldStressRowData();
            row708.LineNo = 7;
            row708.MaterialId = 8943;
            row708.SpecNo = "SA-336";
            row708.TypeGrade = "F316";
            row708.ProductForm = "Forgings";
            row708.AlloyUNS = "S31600";
            row708.ClassCondition = "";
            row708.Notes = "G5, G12, G18";
            row708.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row708);

            // Row 8: SA-336, F316, Forgings
            var row709 = new OldStressRowData();
            row709.LineNo = 8;
            row709.MaterialId = 8944;
            row709.SpecNo = "SA-336";
            row709.TypeGrade = "F316";
            row709.ProductForm = "Forgings";
            row709.AlloyUNS = "S31600";
            row709.ClassCondition = "";
            row709.Notes = "G12, G18";
            row709.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row709);

            // Row 9: SA-430, FP316, Forged pipe
            var row710 = new OldStressRowData();
            row710.LineNo = 9;
            row710.MaterialId = 8953;
            row710.SpecNo = "SA-430";
            row710.TypeGrade = "FP316";
            row710.ProductForm = "Forged pipe";
            row710.AlloyUNS = "S31600";
            row710.ClassCondition = "";
            row710.Notes = "G5, G12, G18, H1";
            row710.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row710);

            // Row 10: SA-430, FP316, Forged pipe
            var row711 = new OldStressRowData();
            row711.LineNo = 10;
            row711.MaterialId = 8954;
            row711.SpecNo = "SA-430";
            row711.TypeGrade = "FP316";
            row711.ProductForm = "Forged pipe";
            row711.AlloyUNS = "S31600";
            row711.ClassCondition = "";
            row711.Notes = "G12, G18, H1";
            row711.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row711);

            // Row 11: SA-182, F316H, Forgings
            var row712 = new OldStressRowData();
            row712.LineNo = 11;
            row712.MaterialId = 8942;
            row712.SpecNo = "SA-182";
            row712.TypeGrade = "F316H";
            row712.ProductForm = "Forgings";
            row712.AlloyUNS = "S31609";
            row712.ClassCondition = "";
            row712.Notes = "G5, G12, G18";
            row712.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row712);

            // Row 12: SA-182, F316H, Forgings
            var row713 = new OldStressRowData();
            row713.LineNo = 12;
            row713.MaterialId = 8941;
            row713.SpecNo = "SA-182";
            row713.TypeGrade = "F316H";
            row713.ProductForm = "Forgings";
            row713.AlloyUNS = "S31609";
            row713.ClassCondition = "";
            row713.Notes = "G18";
            row713.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row713);

            // Row 13: SA-336, F316H, Forgings
            var row714 = new OldStressRowData();
            row714.LineNo = 13;
            row714.MaterialId = 8945;
            row714.SpecNo = "SA-336";
            row714.TypeGrade = "F316H";
            row714.ProductForm = "Forgings";
            row714.AlloyUNS = "S31609";
            row714.ClassCondition = "";
            row714.Notes = "G5";
            row714.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row714);

            // Row 14: SA-336, F316H, Forgings
            var row715 = new OldStressRowData();
            row715.LineNo = 14;
            row715.MaterialId = 8946;
            row715.SpecNo = "SA-336";
            row715.TypeGrade = "F316H";
            row715.ProductForm = "Forgings";
            row715.AlloyUNS = "S31609";
            row715.ClassCondition = "";
            row715.Notes = "";
            row715.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row715);

            // Row 15: SA-430, FP316H, Forged pipe
            var row716 = new OldStressRowData();
            row716.LineNo = 15;
            row716.MaterialId = 8955;
            row716.SpecNo = "SA-430";
            row716.TypeGrade = "FP316H";
            row716.ProductForm = "Forged pipe";
            row716.AlloyUNS = "S31609";
            row716.ClassCondition = "";
            row716.Notes = "G5, G18, H1";
            row716.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15, 14.3, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row716);

            // Row 16: SA-430, FP316H, Forged pipe
            var row717 = new OldStressRowData();
            row717.LineNo = 16;
            row717.MaterialId = 8956;
            row717.SpecNo = "SA-430";
            row717.TypeGrade = "FP316H";
            row717.ProductForm = "Forged pipe";
            row717.AlloyUNS = "S31609";
            row717.ClassCondition = "";
            row717.Notes = "G18, H1";
            row717.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row717);

            // Row 17: SA-182, F316, Forgings
            var row718 = new OldStressRowData();
            row718.LineNo = 17;
            row718.MaterialId = 8959;
            row718.SpecNo = "SA-182";
            row718.TypeGrade = "F316";
            row718.ProductForm = "Forgings";
            row718.AlloyUNS = "S31600";
            row718.ClassCondition = "";
            row718.Notes = "G5, G12, G18";
            row718.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row718);

            // Row 18: SA-182, F316, Forgings
            var row719 = new OldStressRowData();
            row719.LineNo = 18;
            row719.MaterialId = 8960;
            row719.SpecNo = "SA-182";
            row719.TypeGrade = "F316";
            row719.ProductForm = "Forgings";
            row719.AlloyUNS = "S31600";
            row719.ClassCondition = "";
            row719.Notes = "G12, G18";
            row719.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row719);

            // Row 19: SA-213, TP316, Smls. tube
            var row720 = new OldStressRowData();
            row720.LineNo = 19;
            row720.MaterialId = 8963;
            row720.SpecNo = "SA-213";
            row720.TypeGrade = "TP316";
            row720.ProductForm = "Smls. tube";
            row720.AlloyUNS = "S31600";
            row720.ClassCondition = "";
            row720.Notes = "G5, G12, G18";
            row720.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row720);

            // Row 20: SA-213, TP316, Smls. tube
            var row721 = new OldStressRowData();
            row721.LineNo = 20;
            row721.MaterialId = 8964;
            row721.SpecNo = "SA-213";
            row721.TypeGrade = "TP316";
            row721.ProductForm = "Smls. tube";
            row721.AlloyUNS = "S31600";
            row721.ClassCondition = "";
            row721.Notes = "G12, G18";
            row721.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row721);

            // Row 21: SA-240, 316, Plate
            var row722 = new OldStressRowData();
            row722.LineNo = 21;
            row722.MaterialId = 8967;
            row722.SpecNo = "SA-240";
            row722.TypeGrade = "316";
            row722.ProductForm = "Plate";
            row722.AlloyUNS = "S31600";
            row722.ClassCondition = "";
            row722.Notes = "G5, G12, G18";
            row722.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row722);

            // Row 22: SA-240, 316, Plate
            var row723 = new OldStressRowData();
            row723.LineNo = 22;
            row723.MaterialId = 8968;
            row723.SpecNo = "SA-240";
            row723.TypeGrade = "316";
            row723.ProductForm = "Plate";
            row723.AlloyUNS = "S31600";
            row723.ClassCondition = "";
            row723.Notes = "G12, G18";
            row723.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row723);

            // Row 23: SA-249, TP316, Wld. tube
            var row724 = new OldStressRowData();
            row724.LineNo = 23;
            row724.MaterialId = 8971;
            row724.SpecNo = "SA-249";
            row724.TypeGrade = "TP316";
            row724.ProductForm = "Wld. tube";
            row724.AlloyUNS = "S31600";
            row724.ClassCondition = "";
            row724.Notes = "G12, G18, W14";
            row724.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.5, 12.8, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row724);

            // Row 24: SA-249, TP316, Wld. tube
            var row725 = new OldStressRowData();
            row725.LineNo = 24;
            row725.MaterialId = 8970;
            row725.SpecNo = "SA-249";
            row725.TypeGrade = "TP316";
            row725.ProductForm = "Wld. tube";
            row725.AlloyUNS = "S31600";
            row725.ClassCondition = "";
            row725.Notes = "G5, G12, G18, W12, W14";
            row725.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 14.5, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row725);

            // Row 25: SA-249, TP316, Wld. tube
            var row726 = new OldStressRowData();
            row726.LineNo = 25;
            row726.MaterialId = 8972;
            row726.SpecNo = "SA-249";
            row726.TypeGrade = "TP316";
            row726.ProductForm = "Wld. tube";
            row726.AlloyUNS = "S31600";
            row726.ClassCondition = "";
            row726.Notes = "G3, G5, G12, G18, G24";
            row726.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row726);

            // Row 26: SA-249, TP316, Wld. tube
            var row727 = new OldStressRowData();
            row727.LineNo = 26;
            row727.MaterialId = 8973;
            row727.SpecNo = "SA-249";
            row727.TypeGrade = "TP316";
            row727.ProductForm = "Wld. tube";
            row727.AlloyUNS = "S31600";
            row727.ClassCondition = "";
            row727.Notes = "G3, G12, G18, G24";
            row727.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row727);

            // Row 27: SA-312, TP316, Wld. & smls. pipe
            var row728 = new OldStressRowData();
            row728.LineNo = 27;
            row728.MaterialId = 8978;
            row728.SpecNo = "SA-312";
            row728.TypeGrade = "TP316";
            row728.ProductForm = "Wld. & smls. pipe";
            row728.AlloyUNS = "S31600";
            row728.ClassCondition = "";
            row728.Notes = "G5, G12, W12";
            row728.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row728);

            // Row 28: SA-312, TP316, Smls. pipe
            var row729 = new OldStressRowData();
            row729.LineNo = 28;
            row729.MaterialId = 8982;
            row729.SpecNo = "SA-312";
            row729.TypeGrade = "TP316";
            row729.ProductForm = "Smls. pipe";
            row729.AlloyUNS = "S31600";
            row729.ClassCondition = "";
            row729.Notes = "G5, G12, G18";
            row729.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row729);

            // Row 29: SA-312, TP316, Smls. pipe
            var row730 = new OldStressRowData();
            row730.LineNo = 29;
            row730.MaterialId = 8979;
            row730.SpecNo = "SA-312";
            row730.TypeGrade = "TP316";
            row730.ProductForm = "Smls. pipe";
            row730.AlloyUNS = "S31600";
            row730.ClassCondition = "";
            row730.Notes = "G12, G18";
            row730.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row730);

            // Row 30: SA-312, TP316, Wld. pipe
            var row731 = new OldStressRowData();
            row731.LineNo = 30;
            row731.MaterialId = 8983;
            row731.SpecNo = "SA-312";
            row731.TypeGrade = "TP316";
            row731.ProductForm = "Wld. pipe";
            row731.AlloyUNS = "S31600";
            row731.ClassCondition = "";
            row731.Notes = "G5, G12, G18, W14";
            row731.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 14.5, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row731);

            // Row 31: SA-312, TP316, Wld. pipe
            var row732 = new OldStressRowData();
            row732.LineNo = 31;
            row732.MaterialId = 8984;
            row732.SpecNo = "SA-312";
            row732.TypeGrade = "TP316";
            row732.ProductForm = "Wld. pipe";
            row732.AlloyUNS = "S31600";
            row732.ClassCondition = "";
            row732.Notes = "G12, G18, W14";
            row732.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.5, 12.8, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row732);

            // Row 32: SA-312, TP316, Wld. pipe
            var row733 = new OldStressRowData();
            row733.LineNo = 32;
            row733.MaterialId = 8980;
            row733.SpecNo = "SA-312";
            row733.TypeGrade = "TP316";
            row733.ProductForm = "Wld. pipe";
            row733.AlloyUNS = "S31600";
            row733.ClassCondition = "";
            row733.Notes = "G3, G5, G12, G18, G24";
            row733.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row733);

            // Row 33: SA-312, TP316, Wld. pipe
            var row734 = new OldStressRowData();
            row734.LineNo = 33;
            row734.MaterialId = 8981;
            row734.SpecNo = "SA-312";
            row734.TypeGrade = "TP316";
            row734.ProductForm = "Wld. pipe";
            row734.AlloyUNS = "S31600";
            row734.ClassCondition = "";
            row734.Notes = "G3, G12, G18, G24";
            row734.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row734);

            // Row 34: SA-358, 316, Wld. pipe
            var row735 = new OldStressRowData();
            row735.LineNo = 34;
            row735.MaterialId = 8991;
            row735.SpecNo = "SA-358";
            row735.TypeGrade = "316";
            row735.ProductForm = "Wld. pipe";
            row735.AlloyUNS = "S31600";
            row735.ClassCondition = "1";
            row735.Notes = "G5, W12";
            row735.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row735);

            // Row 35: SA-376, TP316, Smls. pipe
            var row736 = new OldStressRowData();
            row736.LineNo = 35;
            row736.MaterialId = 8993;
            row736.SpecNo = "SA-376";
            row736.TypeGrade = "TP316";
            row736.ProductForm = "Smls. pipe";
            row736.AlloyUNS = "S31600";
            row736.ClassCondition = "";
            row736.Notes = "G5, G12, G18, H1, W12";
            row736.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row736);

            // Row 36: SA-376, TP316, Smls. pipe
            var row737 = new OldStressRowData();
            row737.LineNo = 36;
            row737.MaterialId = 8994;
            row737.SpecNo = "SA-376";
            row737.TypeGrade = "TP316";
            row737.ProductForm = "Smls. pipe";
            row737.AlloyUNS = "S31600";
            row737.ClassCondition = "";
            row737.Notes = "G12, G18, H1";
            row737.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row737);

            // Row 1: ..., , 
            var row738 = new OldStressRowData();
            row738.LineNo = 1;
            row738.MaterialId = 8997;
            row738.SpecNo = "...";
            row738.TypeGrade = "";
            row738.ProductForm = "";
            row738.AlloyUNS = "";
            row738.ClassCondition = "";
            row738.Notes = "";
            row738.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row738);

            // Row 2: ..., , 
            var row739 = new OldStressRowData();
            row739.LineNo = 2;
            row739.MaterialId = 8999;
            row739.SpecNo = "...";
            row739.TypeGrade = "";
            row739.ProductForm = "";
            row739.AlloyUNS = "";
            row739.ClassCondition = "";
            row739.Notes = "";
            row739.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row739);

            // Row 3: SA-403, 316, Smls. & wld. fittings
            var row740 = new OldStressRowData();
            row740.LineNo = 3;
            row740.MaterialId = 9000;
            row740.SpecNo = "SA-403";
            row740.TypeGrade = "316";
            row740.ProductForm = "Smls. & wld. fittings";
            row740.AlloyUNS = "S31600";
            row740.ClassCondition = "";
            row740.Notes = "G5, G12, W12, W15";
            row740.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row740);

            // Row 4: ..., , 
            var row741 = new OldStressRowData();
            row741.LineNo = 4;
            row741.MaterialId = 8998;
            row741.SpecNo = "...";
            row741.TypeGrade = "";
            row741.ProductForm = "";
            row741.AlloyUNS = "";
            row741.ClassCondition = "";
            row741.Notes = "";
            row741.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row741);

            // Row 5: ..., , 
            var row742 = new OldStressRowData();
            row742.LineNo = 5;
            row742.MaterialId = 9001;
            row742.SpecNo = "...";
            row742.TypeGrade = "";
            row742.ProductForm = "";
            row742.AlloyUNS = "";
            row742.ClassCondition = "";
            row742.Notes = "";
            row742.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row742);

            // Row 6: ..., , 
            var row743 = new OldStressRowData();
            row743.LineNo = 6;
            row743.MaterialId = 9002;
            row743.SpecNo = "...";
            row743.TypeGrade = "";
            row743.ProductForm = "";
            row743.AlloyUNS = "";
            row743.ClassCondition = "";
            row743.Notes = "";
            row743.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row743);

            // Row 7: SA-409, TP316, Wld. pipe
            var row744 = new OldStressRowData();
            row744.LineNo = 7;
            row744.MaterialId = 9008;
            row744.SpecNo = "SA-409";
            row744.TypeGrade = "TP316";
            row744.ProductForm = "Wld. pipe";
            row744.AlloyUNS = "S31600";
            row744.ClassCondition = "";
            row744.Notes = "G5, W12";
            row744.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row744);

            // Row 8: SA-479, 316, Bar
            var row745 = new OldStressRowData();
            row745.LineNo = 8;
            row745.MaterialId = 9012;
            row745.SpecNo = "SA-479";
            row745.TypeGrade = "316";
            row745.ProductForm = "Bar";
            row745.AlloyUNS = "S31600";
            row745.ClassCondition = "";
            row745.Notes = "G5, G12, G18, G22, H1";
            row745.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row745);

            // Row 9: SA-479, 316, Bar
            var row746 = new OldStressRowData();
            row746.LineNo = 9;
            row746.MaterialId = 9013;
            row746.SpecNo = "SA-479";
            row746.TypeGrade = "316";
            row746.ProductForm = "Bar";
            row746.AlloyUNS = "S31600";
            row746.ClassCondition = "";
            row746.Notes = "G12, G18, G22, H1";
            row746.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row746);

            // Row 10: SA-688, TP316, Wld. tube
            var row747 = new OldStressRowData();
            row747.LineNo = 10;
            row747.MaterialId = 9016;
            row747.SpecNo = "SA-688";
            row747.TypeGrade = "TP316";
            row747.ProductForm = "Wld. tube";
            row747.AlloyUNS = "S31600";
            row747.ClassCondition = "";
            row747.Notes = "G5, W12";
            row747.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row747);

            // Row 11: SA-688, TP316, Wld. tube
            var row748 = new OldStressRowData();
            row748.LineNo = 11;
            row748.MaterialId = 9017;
            row748.SpecNo = "SA-688";
            row748.TypeGrade = "TP316";
            row748.ProductForm = "Wld. tube";
            row748.AlloyUNS = "S31600";
            row748.ClassCondition = "";
            row748.Notes = "G5, G12, G24";
            row748.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row748);

            // Row 12: SA-688, TP316, Wld. tube
            var row749 = new OldStressRowData();
            row749.LineNo = 12;
            row749.MaterialId = 9018;
            row749.SpecNo = "SA-688";
            row749.TypeGrade = "TP316";
            row749.ProductForm = "Wld. tube";
            row749.AlloyUNS = "S31600";
            row749.ClassCondition = "";
            row749.Notes = "G12, G24";
            row749.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row749);

            // Row 13: SA-813, TP316, Wld. pipe
            var row750 = new OldStressRowData();
            row750.LineNo = 13;
            row750.MaterialId = 9019;
            row750.SpecNo = "SA-813";
            row750.TypeGrade = "TP316";
            row750.ProductForm = "Wld. pipe";
            row750.AlloyUNS = "S31600";
            row750.ClassCondition = "";
            row750.Notes = "G5, W12";
            row750.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row750);

            // Row 14: SA-814, TP316, Wld. pipe
            var row751 = new OldStressRowData();
            row751.LineNo = 14;
            row751.MaterialId = 9021;
            row751.SpecNo = "SA-814";
            row751.TypeGrade = "TP316";
            row751.ProductForm = "Wld. pipe";
            row751.AlloyUNS = "S31600";
            row751.ClassCondition = "";
            row751.Notes = "G5, W12";
            row751.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row751);

            // Row 15: SA-182, F316H, Forgings
            var row752 = new OldStressRowData();
            row752.LineNo = 15;
            row752.MaterialId = 8961;
            row752.SpecNo = "SA-182";
            row752.TypeGrade = "F316H";
            row752.ProductForm = "Forgings";
            row752.AlloyUNS = "S31609";
            row752.ClassCondition = "";
            row752.Notes = "G5, G18";
            row752.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row752);

            // Row 16: SA-182, F316H, Forgings
            var row753 = new OldStressRowData();
            row753.LineNo = 16;
            row753.MaterialId = 8962;
            row753.SpecNo = "SA-182";
            row753.TypeGrade = "F316H";
            row753.ProductForm = "Forgings";
            row753.AlloyUNS = "S31609";
            row753.ClassCondition = "";
            row753.Notes = "G18";
            row753.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row753);

            // Row 17: SA-213, TP316H, Smls. tube
            var row754 = new OldStressRowData();
            row754.LineNo = 17;
            row754.MaterialId = 8965;
            row754.SpecNo = "SA-213";
            row754.TypeGrade = "TP316H";
            row754.ProductForm = "Smls. tube";
            row754.AlloyUNS = "S31609";
            row754.ClassCondition = "";
            row754.Notes = "G5, G18";
            row754.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row754);

            // Row 18: SA-213, TP316H, Smls. tube
            var row755 = new OldStressRowData();
            row755.LineNo = 18;
            row755.MaterialId = 8966;
            row755.SpecNo = "SA-213";
            row755.TypeGrade = "TP316H";
            row755.ProductForm = "Smls. tube";
            row755.AlloyUNS = "S31609";
            row755.ClassCondition = "";
            row755.Notes = "G18";
            row755.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row755);

            // Row 19: SA-240, 316H, Plate
            var row756 = new OldStressRowData();
            row756.LineNo = 19;
            row756.MaterialId = 8969;
            row756.SpecNo = "SA-240";
            row756.TypeGrade = "316H";
            row756.ProductForm = "Plate";
            row756.AlloyUNS = "S31609";
            row756.ClassCondition = "";
            row756.Notes = "G5";
            row756.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row756);

            // Row 20: SA-240, 316H, Plate
            var row757 = new OldStressRowData();
            row757.LineNo = 20;
            row757.MaterialId = 9786;
            row757.SpecNo = "SA-240";
            row757.TypeGrade = "316H";
            row757.ProductForm = "Plate";
            row757.AlloyUNS = "S31609";
            row757.ClassCondition = "";
            row757.Notes = "";
            row757.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row757);

            // Row 21: SA-249, TP316H, Wld. tube
            var row758 = new OldStressRowData();
            row758.LineNo = 21;
            row758.MaterialId = 8974;
            row758.SpecNo = "SA-249";
            row758.TypeGrade = "TP316H";
            row758.ProductForm = "Wld. tube";
            row758.AlloyUNS = "S31609";
            row758.ClassCondition = "";
            row758.Notes = "G3, G5, G18, G24";
            row758.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row758);

            // Row 22: SA-249, TP316H, Wld. tube
            var row759 = new OldStressRowData();
            row759.LineNo = 22;
            row759.MaterialId = 8975;
            row759.SpecNo = "SA-249";
            row759.TypeGrade = "TP316H";
            row759.ProductForm = "Wld. tube";
            row759.AlloyUNS = "S31609";
            row759.ClassCondition = "";
            row759.Notes = "G3, G18, G24";
            row759.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row759);

            // Row 23: SA-249, TP316H, Wld. tube
            var row760 = new OldStressRowData();
            row760.LineNo = 23;
            row760.MaterialId = 8977;
            row760.SpecNo = "SA-249";
            row760.TypeGrade = "TP316H";
            row760.ProductForm = "Wld. tube";
            row760.AlloyUNS = "S31609";
            row760.ClassCondition = "";
            row760.Notes = "G18, W14";
            row760.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.5, 12.8, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row760);

            // Row 24: SA-249, TP316H, Wld. tube
            var row761 = new OldStressRowData();
            row761.LineNo = 24;
            row761.MaterialId = 8976;
            row761.SpecNo = "SA-249";
            row761.TypeGrade = "TP316H";
            row761.ProductForm = "Wld. tube";
            row761.AlloyUNS = "S31609";
            row761.ClassCondition = "";
            row761.Notes = "G5, G18, W12, W14";
            row761.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 14.5, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row761);

            // Row 25: SA-312, TP316H, Smls. pipe
            var row762 = new OldStressRowData();
            row762.LineNo = 25;
            row762.MaterialId = 8985;
            row762.SpecNo = "SA-312";
            row762.TypeGrade = "TP316H";
            row762.ProductForm = "Smls. pipe";
            row762.AlloyUNS = "S31609";
            row762.ClassCondition = "";
            row762.Notes = "G5, G18, W12";
            row762.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row762);

            // Row 26: SA-312, TP316H, Smls. pipe
            var row763 = new OldStressRowData();
            row763.LineNo = 26;
            row763.MaterialId = 8986;
            row763.SpecNo = "SA-312";
            row763.TypeGrade = "TP316H";
            row763.ProductForm = "Smls. pipe";
            row763.AlloyUNS = "S31609";
            row763.ClassCondition = "";
            row763.Notes = "G18";
            row763.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row763);

            // Row 27: SA-312, TP316H, Wld. pipe
            var row764 = new OldStressRowData();
            row764.LineNo = 27;
            row764.MaterialId = 8987;
            row764.SpecNo = "SA-312";
            row764.TypeGrade = "TP316H";
            row764.ProductForm = "Wld. pipe";
            row764.AlloyUNS = "S31609";
            row764.ClassCondition = "";
            row764.Notes = "G5, G18, W14";
            row764.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 14.5, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row764);

            // Row 28: SA-312, TP316H, Wld. pipe
            var row765 = new OldStressRowData();
            row765.LineNo = 28;
            row765.MaterialId = 8989;
            row765.SpecNo = "SA-312";
            row765.TypeGrade = "TP316H";
            row765.ProductForm = "Wld. pipe";
            row765.AlloyUNS = "S31609";
            row765.ClassCondition = "";
            row765.Notes = "G18, W14";
            row765.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.5, 12.8, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row765);

            // Row 29: SA-312, TP316H, Wld. pipe
            var row766 = new OldStressRowData();
            row766.LineNo = 29;
            row766.MaterialId = 8988;
            row766.SpecNo = "SA-312";
            row766.TypeGrade = "TP316H";
            row766.ProductForm = "Wld. pipe";
            row766.AlloyUNS = "S31609";
            row766.ClassCondition = "";
            row766.Notes = "G3, G5, G18, G24";
            row766.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row766);

            // Row 30: SA-312, TP316H, Wld. pipe
            var row767 = new OldStressRowData();
            row767.LineNo = 30;
            row767.MaterialId = 8990;
            row767.SpecNo = "SA-312";
            row767.TypeGrade = "TP316H";
            row767.ProductForm = "Wld. pipe";
            row767.AlloyUNS = "S31609";
            row767.ClassCondition = "";
            row767.Notes = "G3, G18, G24";
            row767.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row767);

            // Row 31: SA-358, 316H, Wld. pipe
            var row768 = new OldStressRowData();
            row768.LineNo = 31;
            row768.MaterialId = 8992;
            row768.SpecNo = "SA-358";
            row768.TypeGrade = "316H";
            row768.ProductForm = "Wld. pipe";
            row768.AlloyUNS = "S31609";
            row768.ClassCondition = "1";
            row768.Notes = "G5, W12";
            row768.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row768);

            // Row 32: SA-376, TP316H, Smls. pipe
            var row769 = new OldStressRowData();
            row769.LineNo = 32;
            row769.MaterialId = 8995;
            row769.SpecNo = "SA-376";
            row769.TypeGrade = "TP316H";
            row769.ProductForm = "Smls. pipe";
            row769.AlloyUNS = "S31609";
            row769.ClassCondition = "";
            row769.Notes = "G5, G18, H1";
            row769.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row769);

            // Row 33: SA-376, TP316H, Smls. pipe
            var row770 = new OldStressRowData();
            row770.LineNo = 33;
            row770.MaterialId = 8996;
            row770.SpecNo = "SA-376";
            row770.TypeGrade = "TP316H";
            row770.ProductForm = "Smls. pipe";
            row770.AlloyUNS = "S31609";
            row770.ClassCondition = "";
            row770.Notes = "G18, H1";
            row770.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row770);

            // Row 34: ..., , 
            var row771 = new OldStressRowData();
            row771.LineNo = 34;
            row771.MaterialId = 9003;
            row771.SpecNo = "...";
            row771.TypeGrade = "";
            row771.ProductForm = "";
            row771.AlloyUNS = "";
            row771.ClassCondition = "";
            row771.Notes = "";
            row771.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row771);

            // Row 35: ..., , 
            var row772 = new OldStressRowData();
            row772.LineNo = 35;
            row772.MaterialId = 9005;
            row772.SpecNo = "...";
            row772.TypeGrade = "";
            row772.ProductForm = "";
            row772.AlloyUNS = "";
            row772.ClassCondition = "";
            row772.Notes = "";
            row772.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row772);

            // Row 36: SA-403, 316H, Smls. & wld. fittings
            var row773 = new OldStressRowData();
            row773.LineNo = 36;
            row773.MaterialId = 9007;
            row773.SpecNo = "SA-403";
            row773.TypeGrade = "316H";
            row773.ProductForm = "Smls. & wld. fittings";
            row773.AlloyUNS = "S31609";
            row773.ClassCondition = "";
            row773.Notes = "G5, G12, W12, W15";
            row773.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row773);

            // Row 37: ..., , 
            var row774 = new OldStressRowData();
            row774.LineNo = 37;
            row774.MaterialId = 9004;
            row774.SpecNo = "...";
            row774.TypeGrade = "";
            row774.ProductForm = "";
            row774.AlloyUNS = "";
            row774.ClassCondition = "";
            row774.Notes = "";
            row774.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row774);

            // Row 38: ..., , 
            var row775 = new OldStressRowData();
            row775.LineNo = 38;
            row775.MaterialId = 9006;
            row775.SpecNo = "...";
            row775.TypeGrade = "";
            row775.ProductForm = "";
            row775.AlloyUNS = "";
            row775.ClassCondition = "";
            row775.Notes = "";
            row775.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row775);

            // Row 1: SA-452, TP316H, Cast pipe
            var row776 = new OldStressRowData();
            row776.LineNo = 1;
            row776.MaterialId = 9010;
            row776.SpecNo = "SA-452";
            row776.TypeGrade = "TP316H";
            row776.ProductForm = "Cast pipe";
            row776.AlloyUNS = "S31609";
            row776.ClassCondition = "";
            row776.Notes = "G5, G16, G17, G32";
            row776.StressValues = new double?[] { 18.7, null, 18.7, null, 17.9, 17.5, 17.2, 17.1, 17, 17, 16.9, 16.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row776);

            // Row 2: SA-452, TP316H, Cast pipe
            var row777 = new OldStressRowData();
            row777.LineNo = 2;
            row777.MaterialId = 9009;
            row777.SpecNo = "SA-452";
            row777.TypeGrade = "TP316H";
            row777.ProductForm = "Cast pipe";
            row777.AlloyUNS = "S31609";
            row777.ClassCondition = "";
            row777.Notes = "G5, G32";
            row777.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row777);

            // Row 3: SA-452, TP316H, Cast pipe
            var row778 = new OldStressRowData();
            row778.LineNo = 3;
            row778.MaterialId = 9011;
            row778.SpecNo = "SA-452";
            row778.TypeGrade = "TP316H";
            row778.ProductForm = "Cast pipe";
            row778.AlloyUNS = "S31609";
            row778.ClassCondition = "";
            row778.Notes = "G32";
            row778.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row778);

            // Row 4: SA-479, 316H, Bar
            var row779 = new OldStressRowData();
            row779.LineNo = 4;
            row779.MaterialId = 9014;
            row779.SpecNo = "SA-479";
            row779.TypeGrade = "316H";
            row779.ProductForm = "Bar";
            row779.AlloyUNS = "S31609";
            row779.ClassCondition = "";
            row779.Notes = "G5, G18, H1";
            row779.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row779);

            // Row 5: SA-479, 316H, Bar
            var row780 = new OldStressRowData();
            row780.LineNo = 5;
            row780.MaterialId = 9015;
            row780.SpecNo = "SA-479";
            row780.TypeGrade = "316H";
            row780.ProductForm = "Bar";
            row780.AlloyUNS = "S31609";
            row780.ClassCondition = "";
            row780.Notes = "G18, H1";
            row780.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row780);

            // Row 6: SA-813, TP316H, Wld. pipe
            var row781 = new OldStressRowData();
            row781.LineNo = 6;
            row781.MaterialId = 9020;
            row781.SpecNo = "SA-813";
            row781.TypeGrade = "TP316H";
            row781.ProductForm = "Wld. pipe";
            row781.AlloyUNS = "S31609";
            row781.ClassCondition = "";
            row781.Notes = "G5, W12";
            row781.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row781);

            // Row 7: SA-814, TP316H, Wld. pipe
            var row782 = new OldStressRowData();
            row782.LineNo = 7;
            row782.MaterialId = 9022;
            row782.SpecNo = "SA-814";
            row782.TypeGrade = "TP316H";
            row782.ProductForm = "Wld. pipe";
            row782.AlloyUNS = "S31609";
            row782.ClassCondition = "";
            row782.Notes = "G5, W12";
            row782.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row782);

            // Row 8: SA-240, 316Cb, Plate
            var row783 = new OldStressRowData();
            row783.LineNo = 8;
            row783.MaterialId = 9023;
            row783.SpecNo = "SA-240";
            row783.TypeGrade = "316Cb";
            row783.ProductForm = "Plate";
            row783.AlloyUNS = "S31640";
            row783.ClassCondition = "";
            row783.Notes = "G5, G12";
            row783.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row783);

            // Row 9: SA-240, 316Cb, Plate
            var row784 = new OldStressRowData();
            row784.LineNo = 9;
            row784.MaterialId = 9024;
            row784.SpecNo = "SA-240";
            row784.TypeGrade = "316Cb";
            row784.ProductForm = "Plate";
            row784.AlloyUNS = "S31640";
            row784.ClassCondition = "";
            row784.Notes = "G12";
            row784.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row784);

            // Row 10: SA-182, F316LN, Forgings
            var row785 = new OldStressRowData();
            row785.LineNo = 10;
            row785.MaterialId = 9025;
            row785.SpecNo = "SA-182";
            row785.TypeGrade = "F316LN";
            row785.ProductForm = "Forgings";
            row785.AlloyUNS = "S31653";
            row785.ClassCondition = "";
            row785.Notes = "G5";
            row785.StressValues = new double?[] { 17.5, null, 17.5, null, 16.5, 15.7, 15.1, 14.7, 14.7, 14.6, 14.5, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row785);

            // Row 11: SA-336, F316LN, Forgings
            var row786 = new OldStressRowData();
            row786.LineNo = 11;
            row786.MaterialId = 9026;
            row786.SpecNo = "SA-336";
            row786.TypeGrade = "F316LN";
            row786.ProductForm = "Forgings";
            row786.AlloyUNS = "S31653";
            row786.ClassCondition = "";
            row786.Notes = "G5";
            row786.StressValues = new double?[] { 17.5, null, 17.5, null, 16.5, 15.7, 15.1, 14.7, 14.7, 14.6, 14.5, 14.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row786);

            // Row 12: SA-182, F316LN, Forgings
            var row787 = new OldStressRowData();
            row787.LineNo = 12;
            row787.MaterialId = 9027;
            row787.SpecNo = "SA-182";
            row787.TypeGrade = "F316LN";
            row787.ProductForm = "Forgings";
            row787.AlloyUNS = "S31653";
            row787.ClassCondition = "";
            row787.Notes = "G5";
            row787.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row787);

            // Row 13: SA-213, TP316LN, Smls. tube
            var row788 = new OldStressRowData();
            row788.LineNo = 13;
            row788.MaterialId = 9028;
            row788.SpecNo = "SA-213";
            row788.TypeGrade = "TP316LN";
            row788.ProductForm = "Smls. tube";
            row788.AlloyUNS = "S31653";
            row788.ClassCondition = "";
            row788.Notes = "G5";
            row788.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row788);

            // Row 14: SA-240, 316LN, Plate
            var row789 = new OldStressRowData();
            row789.LineNo = 14;
            row789.MaterialId = 9029;
            row789.SpecNo = "SA-240";
            row789.TypeGrade = "316LN";
            row789.ProductForm = "Plate";
            row789.AlloyUNS = "S31653";
            row789.ClassCondition = "";
            row789.Notes = "G5";
            row789.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row789);

            // Row 15: SA-249, TP316LN, Wld. tube
            var row790 = new OldStressRowData();
            row790.LineNo = 15;
            row790.MaterialId = 9030;
            row790.SpecNo = "SA-249";
            row790.TypeGrade = "TP316LN";
            row790.ProductForm = "Wld. tube";
            row790.AlloyUNS = "S31653";
            row790.ClassCondition = "";
            row790.Notes = "G5, W12";
            row790.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row790);

            // Row 16: SA-312, TP316LN, Wld. & smls. pipe
            var row791 = new OldStressRowData();
            row791.LineNo = 16;
            row791.MaterialId = 9031;
            row791.SpecNo = "SA-312";
            row791.TypeGrade = "TP316LN";
            row791.ProductForm = "Wld. & smls. pipe";
            row791.AlloyUNS = "S31653";
            row791.ClassCondition = "";
            row791.Notes = "G5, W12";
            row791.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row791);

            // Row 17: SA-358, 316LN, Wld. pipe
            var row792 = new OldStressRowData();
            row792.LineNo = 17;
            row792.MaterialId = 9032;
            row792.SpecNo = "SA-358";
            row792.TypeGrade = "316LN";
            row792.ProductForm = "Wld. pipe";
            row792.AlloyUNS = "S31653";
            row792.ClassCondition = "1";
            row792.Notes = "G5, W12";
            row792.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row792);

            // Row 18: SA-376, TP316LN, Smls. pipe
            var row793 = new OldStressRowData();
            row793.LineNo = 18;
            row793.MaterialId = 9033;
            row793.SpecNo = "SA-376";
            row793.TypeGrade = "TP316LN";
            row793.ProductForm = "Smls. pipe";
            row793.AlloyUNS = "S31653";
            row793.ClassCondition = "";
            row793.Notes = "G5";
            row793.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row793);

            // Row 19: SA-403, 316LN, Fittings
            var row794 = new OldStressRowData();
            row794.LineNo = 19;
            row794.MaterialId = 9034;
            row794.SpecNo = "SA-403";
            row794.TypeGrade = "316LN";
            row794.ProductForm = "Fittings";
            row794.AlloyUNS = "S31653";
            row794.ClassCondition = "";
            row794.Notes = "G5";
            row794.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row794);

            // Row 20: SA-403, 316LN, Wld. fittings
            var row795 = new OldStressRowData();
            row795.LineNo = 20;
            row795.MaterialId = 9035;
            row795.SpecNo = "SA-403";
            row795.TypeGrade = "316LN";
            row795.ProductForm = "Wld. fittings";
            row795.AlloyUNS = "S31653";
            row795.ClassCondition = "WP-W";
            row795.Notes = "G5, W12";
            row795.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row795);

            // Row 21: SA-479, 316LN, Bar
            var row796 = new OldStressRowData();
            row796.LineNo = 21;
            row796.MaterialId = 9036;
            row796.SpecNo = "SA-479";
            row796.TypeGrade = "316LN";
            row796.ProductForm = "Bar";
            row796.AlloyUNS = "S31653";
            row796.ClassCondition = "";
            row796.Notes = "G5";
            row796.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row796);

            // Row 22: SA-688, TP316LN, Wld. tube
            var row797 = new OldStressRowData();
            row797.LineNo = 22;
            row797.MaterialId = 9037;
            row797.SpecNo = "SA-688";
            row797.TypeGrade = "TP316LN";
            row797.ProductForm = "Wld. tube";
            row797.AlloyUNS = "S31653";
            row797.ClassCondition = "";
            row797.Notes = "G5, W12";
            row797.StressValues = new double?[] { 18.8, null, 18.8, null, 17.7, 16.8, 16.2, 15.8, 15.7, 15.6, 15.2, 14.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row797);

            // Row 23: SA-430, FP316N, Forged pipe
            var row798 = new OldStressRowData();
            row798.LineNo = 23;
            row798.MaterialId = 9041;
            row798.SpecNo = "SA-430";
            row798.TypeGrade = "FP316N";
            row798.ProductForm = "Forged pipe";
            row798.AlloyUNS = "S31651";
            row798.ClassCondition = "";
            row798.Notes = "G18, H1";
            row798.StressValues = new double?[] { 18.8, null, 18.8, null, 18, 17.6, 16.5, 15.6, 15.2, 14.9, 14.5, 14.2, 13.9, 13.7, 13.5, 13.2, 12.7, 12.2, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row798);

            // Row 24: SA-430, FP316N, Forged pipe
            var row799 = new OldStressRowData();
            row799.LineNo = 24;
            row799.MaterialId = 9038;
            row799.SpecNo = "SA-430";
            row799.TypeGrade = "FP316N";
            row799.ProductForm = "Forged pipe";
            row799.AlloyUNS = "S31651";
            row799.ClassCondition = "";
            row799.Notes = "G5, G18, H1";
            row799.StressValues = new double?[] { 18.8, null, 18.8, null, 18, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.3, 17.1, 17, 16.7, 16.3, 15.7, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row799);

            // Row 25: SA-430, FP316N, Forged pipe
            var row800 = new OldStressRowData();
            row800.LineNo = 25;
            row800.MaterialId = 9039;
            row800.SpecNo = "SA-430";
            row800.TypeGrade = "FP316N";
            row800.ProductForm = "Forged pipe";
            row800.AlloyUNS = "S31651";
            row800.ClassCondition = "";
            row800.Notes = "G5, G12";
            row800.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 17.6, 17.4, 17.4, 17.4, 17.4, 17.4, 17.3, 17.1, 17, 16.7, 16.3, 15.7, 12.4, 9.8, 7.4, null, null, null, null, null, null, null, null, null };
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
