using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch007
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch007(MaterialStressService service)
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

            // Row 39: SA-351, CN7M, Castings
            var row601 = new OldStressRowData();
            row601.LineNo = 39;
            row601.MaterialId = 16929;
            row601.SpecNo = "SA-351";
            row601.TypeGrade = "CN7M";
            row601.ProductForm = "Castings";
            row601.AlloyUNS = "J95150";
            row601.ClassCondition = "";
            row601.Notes = "G1";
            row601.StressValues = new double?[] { 16.7, null, 14.4, null, 12.9, 11.8, 10.8, 10, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row601);

            // Row 1: SA-240, 204, Plate
            var row602 = new OldStressRowData();
            row602.LineNo = 1;
            row602.MaterialId = 9816;
            row602.SpecNo = "SA-240";
            row602.TypeGrade = "204";
            row602.ProductForm = "Plate";
            row602.AlloyUNS = "S20400";
            row602.ClassCondition = "";
            row602.Notes = "G5";
            row602.StressValues = new double?[] { 27.1, null, 23.6, null, 20.6, 18.9, 18.1, 17.9, 17.9, 17.9, 17.8, 17.7, 17.4, 16.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row602);

            // Row 2: SA-240, 204, Plate
            var row603 = new OldStressRowData();
            row603.LineNo = 2;
            row603.MaterialId = 9817;
            row603.SpecNo = "SA-240";
            row603.TypeGrade = "204";
            row603.ProductForm = "Plate";
            row603.AlloyUNS = "S20400";
            row603.ClassCondition = "";
            row603.Notes = "";
            row603.StressValues = new double?[] { 27.1, null, 23.6, null, 20.3, 17.9, 16.5, 15.8, 15.6, 15.5, 15.3, 15.1, 14.8, 14.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row603);

            // Row 3: SA-182, F316L, Forgings
            var row604 = new OldStressRowData();
            row604.LineNo = 3;
            row604.MaterialId = 8910;
            row604.SpecNo = "SA-182";
            row604.TypeGrade = "F316L";
            row604.ProductForm = "Forgings";
            row604.AlloyUNS = "S31603";
            row604.ClassCondition = "";
            row604.Notes = "G5, G42";
            row604.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row604);

            // Row 4: SA-182, F316L, Forgings
            var row605 = new OldStressRowData();
            row605.LineNo = 4;
            row605.MaterialId = 8909;
            row605.SpecNo = "SA-182";
            row605.TypeGrade = "F316L";
            row605.ProductForm = "Forgings";
            row605.AlloyUNS = "S31603";
            row605.ClassCondition = "";
            row605.Notes = "G42";
            row605.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row605);

            // Row 5: SA-336, F316L, Forgings
            var row606 = new OldStressRowData();
            row606.LineNo = 5;
            row606.MaterialId = 8911;
            row606.SpecNo = "SA-336";
            row606.TypeGrade = "F316L";
            row606.ProductForm = "Forgings";
            row606.AlloyUNS = "S31603";
            row606.ClassCondition = "";
            row606.Notes = "G5, G42";
            row606.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row606);

            // Row 6: SA-336, F316L, Forgings
            var row607 = new OldStressRowData();
            row607.LineNo = 6;
            row607.MaterialId = 8912;
            row607.SpecNo = "SA-336";
            row607.TypeGrade = "F316L";
            row607.ProductForm = "Forgings";
            row607.AlloyUNS = "S31603";
            row607.ClassCondition = "";
            row607.Notes = "G42";
            row607.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row607);

            // Row 7: SA-182, F316L, Forgings
            var row608 = new OldStressRowData();
            row608.LineNo = 7;
            row608.MaterialId = 8913;
            row608.SpecNo = "SA-182";
            row608.TypeGrade = "F316L";
            row608.ProductForm = "Forgings";
            row608.AlloyUNS = "S31603";
            row608.ClassCondition = "";
            row608.Notes = "G5, G42";
            row608.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row608);

            // Row 8: SA-182, F316L, Forgings
            var row609 = new OldStressRowData();
            row609.LineNo = 8;
            row609.MaterialId = 9840;
            row609.SpecNo = "SA-182";
            row609.TypeGrade = "F316L";
            row609.ProductForm = "Forgings";
            row609.AlloyUNS = "S31603";
            row609.ClassCondition = "";
            row609.Notes = "G42";
            row609.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row609);

            // Row 9: SA-213, TP316L, Smls. tube
            var row610 = new OldStressRowData();
            row610.LineNo = 9;
            row610.MaterialId = 8914;
            row610.SpecNo = "SA-213";
            row610.TypeGrade = "TP316L";
            row610.ProductForm = "Smls. tube";
            row610.AlloyUNS = "S31603";
            row610.ClassCondition = "";
            row610.Notes = "G5, G42";
            row610.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row610);

            // Row 10: SA-213, TP316L, Smls. tube
            var row611 = new OldStressRowData();
            row611.LineNo = 10;
            row611.MaterialId = 8915;
            row611.SpecNo = "SA-213";
            row611.TypeGrade = "TP316L";
            row611.ProductForm = "Smls. tube";
            row611.AlloyUNS = "S31603";
            row611.ClassCondition = "";
            row611.Notes = "G42";
            row611.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row611);

            // Row 11: SA-240, 316L, Plate
            var row612 = new OldStressRowData();
            row612.LineNo = 11;
            row612.MaterialId = 8916;
            row612.SpecNo = "SA-240";
            row612.TypeGrade = "316L";
            row612.ProductForm = "Plate";
            row612.AlloyUNS = "S31603";
            row612.ClassCondition = "";
            row612.Notes = "G5, G42";
            row612.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row612);

            // Row 12: SA-240, 316L, Plate
            var row613 = new OldStressRowData();
            row613.LineNo = 12;
            row613.MaterialId = 8917;
            row613.SpecNo = "SA-240";
            row613.TypeGrade = "316L";
            row613.ProductForm = "Plate";
            row613.AlloyUNS = "S31603";
            row613.ClassCondition = "";
            row613.Notes = "G42";
            row613.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row613);

            // Row 13: SA-249, TP316L, Wld. tube
            var row614 = new OldStressRowData();
            row614.LineNo = 13;
            row614.MaterialId = 8918;
            row614.SpecNo = "SA-249";
            row614.TypeGrade = "TP316L";
            row614.ProductForm = "Wld. tube";
            row614.AlloyUNS = "S31603";
            row614.ClassCondition = "";
            row614.Notes = "G5, W12";
            row614.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row614);

            // Row 14: SA-249, TP316L, Wld. tube
            var row615 = new OldStressRowData();
            row615.LineNo = 14;
            row615.MaterialId = 8919;
            row615.SpecNo = "SA-249";
            row615.TypeGrade = "TP316L";
            row615.ProductForm = "Wld. tube";
            row615.AlloyUNS = "S31603";
            row615.ClassCondition = "";
            row615.Notes = "G5, G24, G42";
            row615.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.2, 11, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row615);

            // Row 15: SA-249, TP316L, Wld. tube
            var row616 = new OldStressRowData();
            row616.LineNo = 15;
            row616.MaterialId = 8920;
            row616.SpecNo = "SA-249";
            row616.TypeGrade = "TP316L";
            row616.ProductForm = "Wld. tube";
            row616.AlloyUNS = "S31603";
            row616.ClassCondition = "";
            row616.Notes = "G24, G42";
            row616.StressValues = new double?[] { 14.2, null, 12.1, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.1, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row616);

            // Row 16: SA-312, TP316L, Smls. & wld. pipe
            var row617 = new OldStressRowData();
            row617.LineNo = 16;
            row617.MaterialId = 8921;
            row617.SpecNo = "SA-312";
            row617.TypeGrade = "TP316L";
            row617.ProductForm = "Smls. & wld. pipe";
            row617.AlloyUNS = "S31603";
            row617.ClassCondition = "";
            row617.Notes = "G5, G42, W12";
            row617.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row617);

            // Row 17: SA-312, TP316L, Smls. pipe
            var row618 = new OldStressRowData();
            row618.LineNo = 17;
            row618.MaterialId = 8922;
            row618.SpecNo = "SA-312";
            row618.TypeGrade = "TP316L";
            row618.ProductForm = "Smls. pipe";
            row618.AlloyUNS = "S31603";
            row618.ClassCondition = "";
            row618.Notes = "G42";
            row618.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row618);

            // Row 18: SA-312, TP316L, Wld. pipe
            var row619 = new OldStressRowData();
            row619.LineNo = 18;
            row619.MaterialId = 8923;
            row619.SpecNo = "SA-312";
            row619.TypeGrade = "TP316L";
            row619.ProductForm = "Wld. pipe";
            row619.AlloyUNS = "S31603";
            row619.ClassCondition = "";
            row619.Notes = "G5, G24, G42";
            row619.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.2, 11, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row619);

            // Row 19: SA-312, TP316L, Wld. pipe
            var row620 = new OldStressRowData();
            row620.LineNo = 19;
            row620.MaterialId = 8924;
            row620.SpecNo = "SA-312";
            row620.TypeGrade = "TP316L";
            row620.ProductForm = "Wld. pipe";
            row620.AlloyUNS = "S31603";
            row620.ClassCondition = "";
            row620.Notes = "G24, G42";
            row620.StressValues = new double?[] { 14.2, null, 12.1, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.1, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row620);

            // Row 20: SA-358, 316L, Wld. pipe
            var row621 = new OldStressRowData();
            row621.LineNo = 20;
            row621.MaterialId = 8925;
            row621.SpecNo = "SA-358";
            row621.TypeGrade = "316L";
            row621.ProductForm = "Wld. pipe";
            row621.AlloyUNS = "S31603";
            row621.ClassCondition = "1";
            row621.Notes = "G5";
            row621.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row621);

            // Row 21: SA-403, 316L, Smls. & wld. fittings
            var row622 = new OldStressRowData();
            row622.LineNo = 21;
            row622.MaterialId = 8928;
            row622.SpecNo = "SA-403";
            row622.TypeGrade = "316L";
            row622.ProductForm = "Smls. & wld. fittings";
            row622.AlloyUNS = "S31603";
            row622.ClassCondition = "";
            row622.Notes = "G5, W12, W14";
            row622.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row622);

            // Row 22: SA-409, TP316L, Wld. pipe
            var row623 = new OldStressRowData();
            row623.LineNo = 22;
            row623.MaterialId = 8931;
            row623.SpecNo = "SA-409";
            row623.TypeGrade = "TP316L";
            row623.ProductForm = "Wld. pipe";
            row623.AlloyUNS = "S31603";
            row623.ClassCondition = "";
            row623.Notes = "G5";
            row623.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row623);

            // Row 23: SA-479, 316L, Bar
            var row624 = new OldStressRowData();
            row624.LineNo = 23;
            row624.MaterialId = 8932;
            row624.SpecNo = "SA-479";
            row624.TypeGrade = "316L";
            row624.ProductForm = "Bar";
            row624.AlloyUNS = "S31603";
            row624.ClassCondition = "";
            row624.Notes = "G5, G22, G42";
            row624.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row624);

            // Row 24: SA-479, 316L, Bar
            var row625 = new OldStressRowData();
            row625.LineNo = 24;
            row625.MaterialId = 8933;
            row625.SpecNo = "SA-479";
            row625.TypeGrade = "316L";
            row625.ProductForm = "Bar";
            row625.AlloyUNS = "S31603";
            row625.ClassCondition = "";
            row625.Notes = "G22, G42";
            row625.StressValues = new double?[] { 16.7, null, 14.2, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row625);

            // Row 25: SA-688, TP316L, Wld. tube
            var row626 = new OldStressRowData();
            row626.LineNo = 25;
            row626.MaterialId = 8934;
            row626.SpecNo = "SA-688";
            row626.TypeGrade = "TP316L";
            row626.ProductForm = "Wld. tube";
            row626.AlloyUNS = "S31603";
            row626.ClassCondition = "";
            row626.Notes = "G5, W12";
            row626.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row626);

            // Row 26: SA-688, TP316L, Wld. tube
            var row627 = new OldStressRowData();
            row627.LineNo = 26;
            row627.MaterialId = 8935;
            row627.SpecNo = "SA-688";
            row627.TypeGrade = "TP316L";
            row627.ProductForm = "Wld. tube";
            row627.AlloyUNS = "S31603";
            row627.ClassCondition = "";
            row627.Notes = "G5, G24";
            row627.StressValues = new double?[] { 14.2, null, 14.2, null, 14.2, 13.4, 12.5, 11.9, 11.7, 11.4, 11.2, 11, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row627);

            // Row 27: SA-688, TP316L, Wld. tube
            var row628 = new OldStressRowData();
            row628.LineNo = 27;
            row628.MaterialId = 8936;
            row628.SpecNo = "SA-688";
            row628.TypeGrade = "TP316L";
            row628.ProductForm = "Wld. tube";
            row628.AlloyUNS = "S31603";
            row628.ClassCondition = "";
            row628.Notes = "G24";
            row628.StressValues = new double?[] { 14.2, null, 12.1, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.1, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row628);

            // Row 28: SA-813, TP316L, Wld. pipe
            var row629 = new OldStressRowData();
            row629.LineNo = 28;
            row629.MaterialId = 8937;
            row629.SpecNo = "SA-813";
            row629.TypeGrade = "TP316L";
            row629.ProductForm = "Wld. pipe";
            row629.AlloyUNS = "S31603";
            row629.ClassCondition = "";
            row629.Notes = "G5, W12";
            row629.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row629);

            // Row 29: SA-814, TP316L, Wld. pipe
            var row630 = new OldStressRowData();
            row630.LineNo = 29;
            row630.MaterialId = 8938;
            row630.SpecNo = "SA-814";
            row630.TypeGrade = "TP316L";
            row630.ProductForm = "Wld. pipe";
            row630.AlloyUNS = "S31603";
            row630.ClassCondition = "";
            row630.Notes = "G5, W12";
            row630.StressValues = new double?[] { 16.7, null, 16.7, null, 16.7, 15.7, 14.8, 14, 13.7, 13.5, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row630);

            // Row 30: SA-351, CF3M, Castings
            var row631 = new OldStressRowData();
            row631.LineNo = 30;
            row631.MaterialId = 8948;
            row631.SpecNo = "SA-351";
            row631.TypeGrade = "CF3M";
            row631.ProductForm = "Castings";
            row631.AlloyUNS = "J92800";
            row631.ClassCondition = "";
            row631.Notes = "G1, G5, G16, G17, G32";
            row631.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 17.9, 17, 16.6, 16.3, 16, 15.8, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row631);

            // Row 31: SA-351, CF3M, Castings
            var row632 = new OldStressRowData();
            row632.LineNo = 31;
            row632.MaterialId = 8949;
            row632.SpecNo = "SA-351";
            row632.TypeGrade = "CF3M";
            row632.ProductForm = "Castings";
            row632.AlloyUNS = "J92800";
            row632.ClassCondition = "";
            row632.Notes = "G1, G32";
            row632.StressValues = new double?[] { 20, null, 17.2, null, 15.5, 14.2, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row632);

            // Row 32: SA-451, CPF3M, Cast pipe
            var row633 = new OldStressRowData();
            row633.LineNo = 32;
            row633.MaterialId = 8957;
            row633.SpecNo = "SA-451";
            row633.TypeGrade = "CPF3M";
            row633.ProductForm = "Cast pipe";
            row633.AlloyUNS = "J92800";
            row633.ClassCondition = "";
            row633.Notes = "G5, G16, G17, G32";
            row633.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 17.9, 17, 16.6, 16.3, 16.1, 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row633);

            // Row 33: SA-351, CF8M, Castings
            var row634 = new OldStressRowData();
            row634.LineNo = 33;
            row634.MaterialId = 8951;
            row634.SpecNo = "SA-351";
            row634.TypeGrade = "CF8M";
            row634.ProductForm = "Castings";
            row634.AlloyUNS = "J92900";
            row634.ClassCondition = "";
            row634.Notes = "G1, G5, G12, G16, G17, G18, G32, H1, T6";
            row634.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 17.9, 17, 16.6, 16.3, 16, 15.8, 15.7, 15.5, 15.4, 14.9, 11.5, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row634);

            // Row 34: SA-351, CF8M, Castings
            var row635 = new OldStressRowData();
            row635.LineNo = 34;
            row635.MaterialId = 8952;
            row635.SpecNo = "SA-351";
            row635.TypeGrade = "CF8M";
            row635.ProductForm = "Castings";
            row635.AlloyUNS = "J92900";
            row635.ClassCondition = "";
            row635.Notes = "G1, G12, G18, G32, H1, T8";
            row635.StressValues = new double?[] { 20, null, 17.2, null, 15.5, 14.2, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 8.9, 6.9, 5.4, 4.3, 3.4, 2.8, 2.3, 1.9, 1.6, null, null, null };
            batch.Add(row635);

            // Row 35: SA-451, CPF8M, Cast pipe
            var row636 = new OldStressRowData();
            row636.LineNo = 35;
            row636.MaterialId = 8958;
            row636.SpecNo = "SA-451";
            row636.TypeGrade = "CPF8M";
            row636.ProductForm = "Cast pipe";
            row636.AlloyUNS = "J92900";
            row636.ClassCondition = "";
            row636.Notes = "G5, G16, G17, G32";
            row636.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 17.9, 17, 16.6, 16.3, 16, 15.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row636);

            // Row 36: SA-182, F316, Forgings
            var row637 = new OldStressRowData();
            row637.LineNo = 36;
            row637.MaterialId = 8939;
            row637.SpecNo = "SA-182";
            row637.TypeGrade = "F316";
            row637.ProductForm = "Forgings";
            row637.AlloyUNS = "S31600";
            row637.ClassCondition = "";
            row637.Notes = "G5, G12, G18, T8";
            row637.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row637);

            // Row 37: SA-182, F316, Forgings
            var row638 = new OldStressRowData();
            row638.LineNo = 37;
            row638.MaterialId = 8940;
            row638.SpecNo = "SA-182";
            row638.TypeGrade = "F316";
            row638.ProductForm = "Forgings";
            row638.AlloyUNS = "S31600";
            row638.ClassCondition = "";
            row638.Notes = "G12, G18, T9";
            row638.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row638);

            // Row 38: SA-336, F316, Forgings
            var row639 = new OldStressRowData();
            row639.LineNo = 38;
            row639.MaterialId = 8943;
            row639.SpecNo = "SA-336";
            row639.TypeGrade = "F316";
            row639.ProductForm = "Forgings";
            row639.AlloyUNS = "S31600";
            row639.ClassCondition = "";
            row639.Notes = "G5, G12, G18, T8";
            row639.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row639);

            // Row 39: SA-336, F316, Forgings
            var row640 = new OldStressRowData();
            row640.LineNo = 39;
            row640.MaterialId = 8944;
            row640.SpecNo = "SA-336";
            row640.TypeGrade = "F316";
            row640.ProductForm = "Forgings";
            row640.AlloyUNS = "S31600";
            row640.ClassCondition = "";
            row640.Notes = "G12, G18, T9";
            row640.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row640);

            // Row 40: SA-430, FP316, Forged pipe
            var row641 = new OldStressRowData();
            row641.LineNo = 40;
            row641.MaterialId = 8953;
            row641.SpecNo = "SA-430";
            row641.TypeGrade = "FP316";
            row641.ProductForm = "Forged pipe";
            row641.AlloyUNS = "S31600";
            row641.ClassCondition = "";
            row641.Notes = "G5, G12, G18, H1, T8";
            row641.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row641);

            // Row 41: SA-430, FP316, Forged pipe
            var row642 = new OldStressRowData();
            row642.LineNo = 41;
            row642.MaterialId = 8954;
            row642.SpecNo = "SA-430";
            row642.TypeGrade = "FP316";
            row642.ProductForm = "Forged pipe";
            row642.AlloyUNS = "S31600";
            row642.ClassCondition = "";
            row642.Notes = "G12, G18, H1, T9";
            row642.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row642);

            // Row 1: SA-182, F316H, Forgings
            var row643 = new OldStressRowData();
            row643.LineNo = 1;
            row643.MaterialId = 8942;
            row643.SpecNo = "SA-182";
            row643.TypeGrade = "F316H";
            row643.ProductForm = "Forgings";
            row643.AlloyUNS = "S31609";
            row643.ClassCondition = "";
            row643.Notes = "G5, G12, G18, T8";
            row643.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row643);

            // Row 2: SA-182, F316H, Forgings
            var row644 = new OldStressRowData();
            row644.LineNo = 2;
            row644.MaterialId = 8941;
            row644.SpecNo = "SA-182";
            row644.TypeGrade = "F316H";
            row644.ProductForm = "Forgings";
            row644.AlloyUNS = "S31609";
            row644.ClassCondition = "";
            row644.Notes = "G18, T9";
            row644.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row644);

            // Row 3: SA-336, F316H, Forgings
            var row645 = new OldStressRowData();
            row645.LineNo = 3;
            row645.MaterialId = 8945;
            row645.SpecNo = "SA-336";
            row645.TypeGrade = "F316H";
            row645.ProductForm = "Forgings";
            row645.AlloyUNS = "S31609";
            row645.ClassCondition = "";
            row645.Notes = "G5, T8";
            row645.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row645);

            // Row 4: SA-336, F316H, Forgings
            var row646 = new OldStressRowData();
            row646.LineNo = 4;
            row646.MaterialId = 8946;
            row646.SpecNo = "SA-336";
            row646.TypeGrade = "F316H";
            row646.ProductForm = "Forgings";
            row646.AlloyUNS = "S31609";
            row646.ClassCondition = "";
            row646.Notes = "T9";
            row646.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row646);

            // Row 5: SA-430, FP316H, Forged pipe
            var row647 = new OldStressRowData();
            row647.LineNo = 5;
            row647.MaterialId = 8955;
            row647.SpecNo = "SA-430";
            row647.TypeGrade = "FP316H";
            row647.ProductForm = "Forged pipe";
            row647.AlloyUNS = "S31609";
            row647.ClassCondition = "";
            row647.Notes = "G5, G18, H1, T8";
            row647.StressValues = new double?[] { 20, null, 20, null, 19.4, 19.2, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row647);

            // Row 6: SA-430, FP316H, Forged pipe
            var row648 = new OldStressRowData();
            row648.LineNo = 6;
            row648.MaterialId = 8956;
            row648.SpecNo = "SA-430";
            row648.TypeGrade = "FP316H";
            row648.ProductForm = "Forged pipe";
            row648.AlloyUNS = "S31609";
            row648.ClassCondition = "";
            row648.Notes = "G18, H1, T9";
            row648.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row648);

            // Row 7: SA-182, F316, Forgings
            var row649 = new OldStressRowData();
            row649.LineNo = 7;
            row649.MaterialId = 8959;
            row649.SpecNo = "SA-182";
            row649.TypeGrade = "F316";
            row649.ProductForm = "Forgings";
            row649.AlloyUNS = "S31600";
            row649.ClassCondition = "";
            row649.Notes = "G5, G12, G18, T8";
            row649.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row649);

            // Row 8: SA-182, F316, Forgings
            var row650 = new OldStressRowData();
            row650.LineNo = 8;
            row650.MaterialId = 8960;
            row650.SpecNo = "SA-182";
            row650.TypeGrade = "F316";
            row650.ProductForm = "Forgings";
            row650.AlloyUNS = "S31600";
            row650.ClassCondition = "";
            row650.Notes = "G12, G18, T9";
            row650.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row650);

            // Row 9: SA-213, TP316, Smls. tube
            var row651 = new OldStressRowData();
            row651.LineNo = 9;
            row651.MaterialId = 8963;
            row651.SpecNo = "SA-213";
            row651.TypeGrade = "TP316";
            row651.ProductForm = "Smls. tube";
            row651.AlloyUNS = "S31600";
            row651.ClassCondition = "";
            row651.Notes = "G5, G12, G18, T8";
            row651.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row651);

            // Row 10: SA-213, TP316, Smls. tube
            var row652 = new OldStressRowData();
            row652.LineNo = 10;
            row652.MaterialId = 8964;
            row652.SpecNo = "SA-213";
            row652.TypeGrade = "TP316";
            row652.ProductForm = "Smls. tube";
            row652.AlloyUNS = "S31600";
            row652.ClassCondition = "";
            row652.Notes = "G12, G18, T9";
            row652.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row652);

            // Row 11: SA-240, 316, Plate
            var row653 = new OldStressRowData();
            row653.LineNo = 11;
            row653.MaterialId = 8967;
            row653.SpecNo = "SA-240";
            row653.TypeGrade = "316";
            row653.ProductForm = "Plate";
            row653.AlloyUNS = "S31600";
            row653.ClassCondition = "";
            row653.Notes = "G5, G12, G18, T8";
            row653.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row653);

            // Row 12: SA-240, 316, Plate
            var row654 = new OldStressRowData();
            row654.LineNo = 12;
            row654.MaterialId = 8968;
            row654.SpecNo = "SA-240";
            row654.TypeGrade = "316";
            row654.ProductForm = "Plate";
            row654.AlloyUNS = "S31600";
            row654.ClassCondition = "";
            row654.Notes = "G12, G18, T9";
            row654.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row654);

            // Row 13: SA-249, TP316, Wld. tube
            var row655 = new OldStressRowData();
            row655.LineNo = 13;
            row655.MaterialId = 8971;
            row655.SpecNo = "SA-249";
            row655.TypeGrade = "TP316";
            row655.ProductForm = "Wld. tube";
            row655.AlloyUNS = "S31600";
            row655.ClassCondition = "";
            row655.Notes = "G12, G18, T9, W13";
            row655.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row655);

            // Row 14: SA-249, TP316, Wld. tube
            var row656 = new OldStressRowData();
            row656.LineNo = 14;
            row656.MaterialId = 8970;
            row656.SpecNo = "SA-249";
            row656.TypeGrade = "TP316";
            row656.ProductForm = "Wld. tube";
            row656.AlloyUNS = "S31600";
            row656.ClassCondition = "";
            row656.Notes = "G5, G12, G18, T8, W12, W13";
            row656.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row656);

            // Row 15: SA-249, TP316, Wld. tube
            var row657 = new OldStressRowData();
            row657.LineNo = 15;
            row657.MaterialId = 8972;
            row657.SpecNo = "SA-249";
            row657.TypeGrade = "TP316";
            row657.ProductForm = "Wld. tube";
            row657.AlloyUNS = "S31600";
            row657.ClassCondition = "";
            row657.Notes = "G3, G5, G12, G18, G24, T7";
            row657.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row657);

            // Row 16: SA-249, TP316, Wld. tube
            var row658 = new OldStressRowData();
            row658.LineNo = 16;
            row658.MaterialId = 8973;
            row658.SpecNo = "SA-249";
            row658.TypeGrade = "TP316";
            row658.ProductForm = "Wld. tube";
            row658.AlloyUNS = "S31600";
            row658.ClassCondition = "";
            row658.Notes = "G3, G12, G18, G24, T9";
            row658.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row658);

            // Row 17: SA-312, TP316, Smls. & wld. pipe
            var row659 = new OldStressRowData();
            row659.LineNo = 17;
            row659.MaterialId = 8978;
            row659.SpecNo = "SA-312";
            row659.TypeGrade = "TP316";
            row659.ProductForm = "Smls. & wld. pipe";
            row659.AlloyUNS = "S31600";
            row659.ClassCondition = "";
            row659.Notes = "G5, G12, G18, T8, W12, W13";
            row659.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row659);

            // Row 18: SA-312, TP316, Smls. & wld. pipe
            var row660 = new OldStressRowData();
            row660.LineNo = 18;
            row660.MaterialId = 8979;
            row660.SpecNo = "SA-312";
            row660.TypeGrade = "TP316";
            row660.ProductForm = "Smls. & wld. pipe";
            row660.AlloyUNS = "S31600";
            row660.ClassCondition = "";
            row660.Notes = "G12, G18, T9, W13";
            row660.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row660);

            // Row 19: SA-312, TP316, Wld. pipe
            var row661 = new OldStressRowData();
            row661.LineNo = 19;
            row661.MaterialId = 8980;
            row661.SpecNo = "SA-312";
            row661.TypeGrade = "TP316";
            row661.ProductForm = "Wld. pipe";
            row661.AlloyUNS = "S31600";
            row661.ClassCondition = "";
            row661.Notes = "G3, G5, G12, G18, G24, T7";
            row661.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row661);

            // Row 20: SA-312, TP316, Wld. pipe
            var row662 = new OldStressRowData();
            row662.LineNo = 20;
            row662.MaterialId = 8981;
            row662.SpecNo = "SA-312";
            row662.TypeGrade = "TP316";
            row662.ProductForm = "Wld. pipe";
            row662.AlloyUNS = "S31600";
            row662.ClassCondition = "";
            row662.Notes = "G3, G12, G18, G24, T9";
            row662.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row662);

            // Row 21: SA-358, 316, Wld. pipe
            var row663 = new OldStressRowData();
            row663.LineNo = 21;
            row663.MaterialId = 8991;
            row663.SpecNo = "SA-358";
            row663.TypeGrade = "316";
            row663.ProductForm = "Wld. pipe";
            row663.AlloyUNS = "S31600";
            row663.ClassCondition = "1";
            row663.Notes = "G5, W12";
            row663.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row663);

            // Row 22: SA-376, TP316, Smls. pipe
            var row664 = new OldStressRowData();
            row664.LineNo = 22;
            row664.MaterialId = 8993;
            row664.SpecNo = "SA-376";
            row664.TypeGrade = "TP316";
            row664.ProductForm = "Smls. pipe";
            row664.AlloyUNS = "S31600";
            row664.ClassCondition = "";
            row664.Notes = "G5, G12, G18, H1, T8, W12";
            row664.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row664);

            // Row 23: SA-376, TP316, Smls. pipe
            var row665 = new OldStressRowData();
            row665.LineNo = 23;
            row665.MaterialId = 8994;
            row665.SpecNo = "SA-376";
            row665.TypeGrade = "TP316";
            row665.ProductForm = "Smls. pipe";
            row665.AlloyUNS = "S31600";
            row665.ClassCondition = "";
            row665.Notes = "G12, G18, H1, T9";
            row665.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row665);

            // Row 24: SA-403, 316, Smls. & wld. fittings
            var row666 = new OldStressRowData();
            row666.LineNo = 24;
            row666.MaterialId = 9000;
            row666.SpecNo = "SA-403";
            row666.TypeGrade = "316";
            row666.ProductForm = "Smls. & wld. fittings";
            row666.AlloyUNS = "S31600";
            row666.ClassCondition = "";
            row666.Notes = "G5, G12, T8, W12, W14";
            row666.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row666);

            // Row 25: SA-409, TP316, Wld. pipe
            var row667 = new OldStressRowData();
            row667.LineNo = 25;
            row667.MaterialId = 9008;
            row667.SpecNo = "SA-409";
            row667.TypeGrade = "TP316";
            row667.ProductForm = "Wld. pipe";
            row667.AlloyUNS = "S31600";
            row667.ClassCondition = "";
            row667.Notes = "G5, W12";
            row667.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row667);

            // Row 26: SA-479, 316, Bar
            var row668 = new OldStressRowData();
            row668.LineNo = 26;
            row668.MaterialId = 9012;
            row668.SpecNo = "SA-479";
            row668.TypeGrade = "316";
            row668.ProductForm = "Bar";
            row668.AlloyUNS = "S31600";
            row668.ClassCondition = "";
            row668.Notes = "G5, G12, G18, G22, H1, T8";
            row668.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row668);

            // Row 27: SA-479, 316, Bar
            var row669 = new OldStressRowData();
            row669.LineNo = 27;
            row669.MaterialId = 9013;
            row669.SpecNo = "SA-479";
            row669.TypeGrade = "316";
            row669.ProductForm = "Bar";
            row669.AlloyUNS = "S31600";
            row669.ClassCondition = "";
            row669.Notes = "G12, G18, G22, H1, T9";
            row669.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row669);

            // Row 28: SA-688, TP316, Wld. tube
            var row670 = new OldStressRowData();
            row670.LineNo = 28;
            row670.MaterialId = 9016;
            row670.SpecNo = "SA-688";
            row670.TypeGrade = "TP316";
            row670.ProductForm = "Wld. tube";
            row670.AlloyUNS = "S31600";
            row670.ClassCondition = "";
            row670.Notes = "G5, W12";
            row670.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row670);

            // Row 29: SA-688, TP316, Wld. tube
            var row671 = new OldStressRowData();
            row671.LineNo = 29;
            row671.MaterialId = 9017;
            row671.SpecNo = "SA-688";
            row671.TypeGrade = "TP316";
            row671.ProductForm = "Wld. tube";
            row671.AlloyUNS = "S31600";
            row671.ClassCondition = "";
            row671.Notes = "G5, G12, G24, T7";
            row671.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row671);

            // Row 30: SA-688, TP316, Wld. tube
            var row672 = new OldStressRowData();
            row672.LineNo = 30;
            row672.MaterialId = 9018;
            row672.SpecNo = "SA-688";
            row672.TypeGrade = "TP316";
            row672.ProductForm = "Wld. tube";
            row672.AlloyUNS = "S31600";
            row672.ClassCondition = "";
            row672.Notes = "G12, G24, T9";
            row672.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row672);

            // Row 31: SA-813, TP316, Wld. pipe
            var row673 = new OldStressRowData();
            row673.LineNo = 31;
            row673.MaterialId = 9019;
            row673.SpecNo = "SA-813";
            row673.TypeGrade = "TP316";
            row673.ProductForm = "Wld. pipe";
            row673.AlloyUNS = "S31600";
            row673.ClassCondition = "";
            row673.Notes = "G5, W12";
            row673.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row673);

            // Row 32: SA-814, TP316, Wld. pipe
            var row674 = new OldStressRowData();
            row674.LineNo = 32;
            row674.MaterialId = 9021;
            row674.SpecNo = "SA-814";
            row674.TypeGrade = "TP316";
            row674.ProductForm = "Wld. pipe";
            row674.AlloyUNS = "S31600";
            row674.ClassCondition = "";
            row674.Notes = "G5, W12";
            row674.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row674);

            // Row 33: SA-182, F316H, Forgings
            var row675 = new OldStressRowData();
            row675.LineNo = 33;
            row675.MaterialId = 8961;
            row675.SpecNo = "SA-182";
            row675.TypeGrade = "F316H";
            row675.ProductForm = "Forgings";
            row675.AlloyUNS = "S31609";
            row675.ClassCondition = "";
            row675.Notes = "G5, G18, T8";
            row675.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row675);

            // Row 34: SA-182, F316H, Forgings
            var row676 = new OldStressRowData();
            row676.LineNo = 34;
            row676.MaterialId = 8962;
            row676.SpecNo = "SA-182";
            row676.TypeGrade = "F316H";
            row676.ProductForm = "Forgings";
            row676.AlloyUNS = "S31609";
            row676.ClassCondition = "";
            row676.Notes = "G18, T9";
            row676.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row676);

            // Row 35: SA-213, TP316H, Smls. tube
            var row677 = new OldStressRowData();
            row677.LineNo = 35;
            row677.MaterialId = 8965;
            row677.SpecNo = "SA-213";
            row677.TypeGrade = "TP316H";
            row677.ProductForm = "Smls. tube";
            row677.AlloyUNS = "S31609";
            row677.ClassCondition = "";
            row677.Notes = "G5, G18, T8";
            row677.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row677);

            // Row 36: SA-213, TP316H, Smls. tube
            var row678 = new OldStressRowData();
            row678.LineNo = 36;
            row678.MaterialId = 8966;
            row678.SpecNo = "SA-213";
            row678.TypeGrade = "TP316H";
            row678.ProductForm = "Smls. tube";
            row678.AlloyUNS = "S31609";
            row678.ClassCondition = "";
            row678.Notes = "G18, T9";
            row678.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row678);

            // Row 37: SA-240, 316H, Plate
            var row679 = new OldStressRowData();
            row679.LineNo = 37;
            row679.MaterialId = 8969;
            row679.SpecNo = "SA-240";
            row679.TypeGrade = "316H";
            row679.ProductForm = "Plate";
            row679.AlloyUNS = "S31609";
            row679.ClassCondition = "";
            row679.Notes = "G5, G18, T8";
            row679.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row679);

            // Row 38: SA-240, 316H, Plate
            var row680 = new OldStressRowData();
            row680.LineNo = 38;
            row680.MaterialId = 9786;
            row680.SpecNo = "SA-240";
            row680.TypeGrade = "316H";
            row680.ProductForm = "Plate";
            row680.AlloyUNS = "S31609";
            row680.ClassCondition = "";
            row680.Notes = "G18, T9";
            row680.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row680);

            // Row 1: SA-249, TP316H, Wld. tube
            var row681 = new OldStressRowData();
            row681.LineNo = 1;
            row681.MaterialId = 8974;
            row681.SpecNo = "SA-249";
            row681.TypeGrade = "TP316H";
            row681.ProductForm = "Wld. tube";
            row681.AlloyUNS = "S31609";
            row681.ClassCondition = "";
            row681.Notes = "G3, G5, G18, G24, T7";
            row681.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row681);

            // Row 2: SA-249, TP316H, Wld. tube
            var row682 = new OldStressRowData();
            row682.LineNo = 2;
            row682.MaterialId = 8975;
            row682.SpecNo = "SA-249";
            row682.TypeGrade = "TP316H";
            row682.ProductForm = "Wld. tube";
            row682.AlloyUNS = "S31609";
            row682.ClassCondition = "";
            row682.Notes = "G3, G18, G24, T9";
            row682.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row682);

            // Row 3: SA-249, TP316H, Wld. tube
            var row683 = new OldStressRowData();
            row683.LineNo = 3;
            row683.MaterialId = 8977;
            row683.SpecNo = "SA-249";
            row683.TypeGrade = "TP316H";
            row683.ProductForm = "Wld. tube";
            row683.AlloyUNS = "S31609";
            row683.ClassCondition = "";
            row683.Notes = "G18, T9, W13";
            row683.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row683);

            // Row 4: SA-249, TP316H, Wld. tube
            var row684 = new OldStressRowData();
            row684.LineNo = 4;
            row684.MaterialId = 8976;
            row684.SpecNo = "SA-249";
            row684.TypeGrade = "TP316H";
            row684.ProductForm = "Wld. tube";
            row684.AlloyUNS = "S31609";
            row684.ClassCondition = "";
            row684.Notes = "G5, G18, T8, W12, W13";
            row684.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row684);

            // Row 5: SA-312, TP316H, Smls. & wld. pipe
            var row685 = new OldStressRowData();
            row685.LineNo = 5;
            row685.MaterialId = 8985;
            row685.SpecNo = "SA-312";
            row685.TypeGrade = "TP316H";
            row685.ProductForm = "Smls. & wld. pipe";
            row685.AlloyUNS = "S31609";
            row685.ClassCondition = "";
            row685.Notes = "G5, G18, T8, W12, W13";
            row685.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row685);

            // Row 6: SA-312, TP316H, Smls. & wld. pipe
            var row686 = new OldStressRowData();
            row686.LineNo = 6;
            row686.MaterialId = 8986;
            row686.SpecNo = "SA-312";
            row686.TypeGrade = "TP316H";
            row686.ProductForm = "Smls. & wld. pipe";
            row686.AlloyUNS = "S31609";
            row686.ClassCondition = "";
            row686.Notes = "G18, T9, W13";
            row686.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row686);

            // Row 7: SA-312, TP316H, Wld. pipe
            var row687 = new OldStressRowData();
            row687.LineNo = 7;
            row687.MaterialId = 8988;
            row687.SpecNo = "SA-312";
            row687.TypeGrade = "TP316H";
            row687.ProductForm = "Wld. pipe";
            row687.AlloyUNS = "S31609";
            row687.ClassCondition = "";
            row687.Notes = "G3, G5, G18, G24, T7";
            row687.StressValues = new double?[] { 17, null, 17, null, 17, 16.4, 15.3, 14.5, 14.1, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.9, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row687);

            // Row 8: SA-312, TP316H, Wld. pipe
            var row688 = new OldStressRowData();
            row688.LineNo = 8;
            row688.MaterialId = 8990;
            row688.SpecNo = "SA-312";
            row688.TypeGrade = "TP316H";
            row688.ProductForm = "Wld. pipe";
            row688.AlloyUNS = "S31609";
            row688.ClassCondition = "";
            row688.Notes = "G3, G18, G24, T9";
            row688.StressValues = new double?[] { 17, null, 14.7, null, 13.2, 12.1, 11.3, 10.7, 10.5, 10.3, 10.1, 10, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.4, 1.1, null, null, null };
            batch.Add(row688);

            // Row 9: SA-358, 316H, Wld. pipe
            var row689 = new OldStressRowData();
            row689.LineNo = 9;
            row689.MaterialId = 8992;
            row689.SpecNo = "SA-358";
            row689.TypeGrade = "316H";
            row689.ProductForm = "Wld. pipe";
            row689.AlloyUNS = "S31609";
            row689.ClassCondition = "1";
            row689.Notes = "G5, W12";
            row689.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row689);

            // Row 10: SA-376, TP316H, Smls. pipe
            var row690 = new OldStressRowData();
            row690.LineNo = 10;
            row690.MaterialId = 8995;
            row690.SpecNo = "SA-376";
            row690.TypeGrade = "TP316H";
            row690.ProductForm = "Smls. pipe";
            row690.AlloyUNS = "S31609";
            row690.ClassCondition = "";
            row690.Notes = "G5, G18, H1, T8";
            row690.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row690);

            // Row 11: SA-376, TP316H, Smls. pipe
            var row691 = new OldStressRowData();
            row691.LineNo = 11;
            row691.MaterialId = 8996;
            row691.SpecNo = "SA-376";
            row691.TypeGrade = "TP316H";
            row691.ProductForm = "Smls. pipe";
            row691.AlloyUNS = "S31609";
            row691.ClassCondition = "";
            row691.Notes = "G18, H1, T9";
            row691.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row691);

            // Row 12: SA-403, 316H, Smls. & wld. fittings
            var row692 = new OldStressRowData();
            row692.LineNo = 12;
            row692.MaterialId = 9007;
            row692.SpecNo = "SA-403";
            row692.TypeGrade = "316H";
            row692.ProductForm = "Smls. & wld. fittings";
            row692.AlloyUNS = "S31609";
            row692.ClassCondition = "";
            row692.Notes = "G5, G12, T8, W12, W14";
            row692.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row692);

            // Row 13: SA-452, TP316H, Cast pipe
            var row693 = new OldStressRowData();
            row693.LineNo = 13;
            row693.MaterialId = 9009;
            row693.SpecNo = "SA-452";
            row693.TypeGrade = "TP316H";
            row693.ProductForm = "Cast pipe";
            row693.AlloyUNS = "S31609";
            row693.ClassCondition = "";
            row693.Notes = "G5, G16, G17, G32, T8";
            row693.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row693);

            // Row 14: SA-452, TP316H, Cast pipe
            var row694 = new OldStressRowData();
            row694.LineNo = 14;
            row694.MaterialId = 9011;
            row694.SpecNo = "SA-452";
            row694.TypeGrade = "TP316H";
            row694.ProductForm = "Cast pipe";
            row694.AlloyUNS = "S31609";
            row694.ClassCondition = "";
            row694.Notes = "G32, T9";
            row694.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row694);

            // Row 15: SA-479, 316H, Bar
            var row695 = new OldStressRowData();
            row695.LineNo = 15;
            row695.MaterialId = 9014;
            row695.SpecNo = "SA-479";
            row695.TypeGrade = "316H";
            row695.ProductForm = "Bar";
            row695.AlloyUNS = "S31609";
            row695.ClassCondition = "";
            row695.Notes = "G5, G18, H1, T8";
            row695.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row695);

            // Row 16: SA-479, 316H, Bar
            var row696 = new OldStressRowData();
            row696.LineNo = 16;
            row696.MaterialId = 9015;
            row696.SpecNo = "SA-479";
            row696.TypeGrade = "316H";
            row696.ProductForm = "Bar";
            row696.AlloyUNS = "S31609";
            row696.ClassCondition = "";
            row696.Notes = "G18, H1, T9";
            row696.StressValues = new double?[] { 20, null, 17.3, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.8, 11.6, 11.5, 11.4, 11.3, 11.2, 11.1, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row696);

            // Row 17: SA-813, TP316H, Wld. pipe
            var row697 = new OldStressRowData();
            row697.LineNo = 17;
            row697.MaterialId = 9020;
            row697.SpecNo = "SA-813";
            row697.TypeGrade = "TP316H";
            row697.ProductForm = "Wld. pipe";
            row697.AlloyUNS = "S31609";
            row697.ClassCondition = "";
            row697.Notes = "G5, W12";
            row697.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row697);

            // Row 18: SA-814, TP316H, Wld. pipe
            var row698 = new OldStressRowData();
            row698.LineNo = 18;
            row698.MaterialId = 9022;
            row698.SpecNo = "SA-814";
            row698.TypeGrade = "TP316H";
            row698.ProductForm = "Wld. pipe";
            row698.AlloyUNS = "S31609";
            row698.ClassCondition = "";
            row698.Notes = "G5, W12";
            row698.StressValues = new double?[] { 20, null, 20, null, 20, 19.3, 18, 17, 16.6, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row698);

            // Row 19: SA-240, 316Cb, Plate
            var row699 = new OldStressRowData();
            row699.LineNo = 19;
            row699.MaterialId = 9023;
            row699.SpecNo = "SA-240";
            row699.TypeGrade = "316Cb";
            row699.ProductForm = "Plate";
            row699.AlloyUNS = "S31640";
            row699.ClassCondition = "";
            row699.Notes = "G5, G12, T8";
            row699.StressValues = new double?[] { 20, null, 20, null, 20, 19.4, 17.8, 16.8, 16.5, 16.2, 16, 15.9, 15.8, 15.7, 15.5, 15.3, 15.1, 12.3, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row699);

            // Row 20: SA-240, 316Cb, Plate
            var row700 = new OldStressRowData();
            row700.LineNo = 20;
            row700.MaterialId = 9024;
            row700.SpecNo = "SA-240";
            row700.TypeGrade = "316Cb";
            row700.ProductForm = "Plate";
            row700.AlloyUNS = "S31640";
            row700.ClassCondition = "";
            row700.Notes = "G12, T9";
            row700.StressValues = new double?[] { 20, null, 17.7, null, 15.8, 14.3, 13.2, 12.4, 12.2, 12, 11.9, 11.8, 11.7, 11.6, 11.5, 11.4, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row700);

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
