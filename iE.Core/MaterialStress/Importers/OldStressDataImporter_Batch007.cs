using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch007
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

            // Row 21: SA-543, C, Plate
            var row601 = new OldStressRowData();
            row601.LineNo = 21;
            row601.MaterialId = 8847;
            row601.SpecNo = "SA-543";
            row601.TypeGrade = "C";
            row601.ProductForm = "Plate";
            row601.AlloyUNS = "";
            row601.ClassCondition = "3";
            row601.Notes = "";
            row601.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.3, 22.1, 21.8, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row601);

            // Row 22: SA-543, C, Plate
            var row602 = new OldStressRowData();
            row602.LineNo = 22;
            row602.MaterialId = 8848;
            row602.SpecNo = "SA-543";
            row602.TypeGrade = "C";
            row602.ProductForm = "Plate";
            row602.AlloyUNS = "";
            row602.ClassCondition = "1";
            row602.Notes = "";
            row602.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26, 25.8, 25.4, 25.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row602);

            // Row 23: SA-543, C, Plate
            var row603 = new OldStressRowData();
            row603.LineNo = 23;
            row603.MaterialId = 8849;
            row603.SpecNo = "SA-543";
            row603.TypeGrade = "C";
            row603.ProductForm = "Plate";
            row603.AlloyUNS = "";
            row603.ClassCondition = "2";
            row603.Notes = "";
            row603.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.5, 28.3, 27.9, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row603);

            // Row 24: SA-723, 2, Forgings
            var row604 = new OldStressRowData();
            row604.LineNo = 24;
            row604.MaterialId = 8850;
            row604.SpecNo = "SA-723";
            row604.TypeGrade = "2";
            row604.ProductForm = "Forgings";
            row604.AlloyUNS = "K34035";
            row604.ClassCondition = "1";
            row604.Notes = "W1";
            row604.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.3, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row604);

            // Row 25: SA-723, 2, Forgings
            var row605 = new OldStressRowData();
            row605.LineNo = 25;
            row605.MaterialId = 8851;
            row605.SpecNo = "SA-723";
            row605.TypeGrade = "2";
            row605.ProductForm = "Forgings";
            row605.AlloyUNS = "K34035";
            row605.ClassCondition = "2";
            row605.Notes = "W1";
            row605.StressValues = new double?[] { 33.8, null, 33.8, null, 33.8, 33.8, 33.8, 33.8, 33.2, 32.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row605);

            // Row 26: SA-723, 2, Forgings
            var row606 = new OldStressRowData();
            row606.LineNo = 26;
            row606.MaterialId = 8852;
            row606.SpecNo = "SA-723";
            row606.TypeGrade = "2";
            row606.ProductForm = "Forgings";
            row606.AlloyUNS = "K34035";
            row606.ClassCondition = "3";
            row606.Notes = "W1";
            row606.StressValues = new double?[] { 38.8, null, 38.8, null, 38.8, 38.8, 38.8, 38.8, 38.1, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row606);

            // Row 27: SA-723, 2, Forgings
            var row607 = new OldStressRowData();
            row607.LineNo = 27;
            row607.MaterialId = 8853;
            row607.SpecNo = "SA-723";
            row607.TypeGrade = "2";
            row607.ProductForm = "Forgings";
            row607.AlloyUNS = "K34035";
            row607.ClassCondition = "4";
            row607.Notes = "W1";
            row607.StressValues = new double?[] { 43.8, null, 43.8, null, 43.8, 43.8, 43.8, 43.8, 43.1, 41.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row607);

            // Row 28: SA-723, 2, Forgings
            var row608 = new OldStressRowData();
            row608.LineNo = 28;
            row608.MaterialId = 8854;
            row608.SpecNo = "SA-723";
            row608.TypeGrade = "2";
            row608.ProductForm = "Forgings";
            row608.AlloyUNS = "K34035";
            row608.ClassCondition = "5";
            row608.Notes = "W1";
            row608.StressValues = new double?[] { 47.5, null, 47.5, null, 47.5, 47.5, 47.5, 47.5, 46.8, 45.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row608);

            // Row 29: SA-543, B, Plate
            var row609 = new OldStressRowData();
            row609.LineNo = 29;
            row609.MaterialId = 8855;
            row609.SpecNo = "SA-543";
            row609.TypeGrade = "B";
            row609.ProductForm = "Plate";
            row609.AlloyUNS = "K42339";
            row609.ClassCondition = "3";
            row609.Notes = "";
            row609.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.3, 22.1, 21.8, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row609);

            // Row 30: SA-543, B, Plate
            var row610 = new OldStressRowData();
            row610.LineNo = 30;
            row610.MaterialId = 8856;
            row610.SpecNo = "SA-543";
            row610.TypeGrade = "B";
            row610.ProductForm = "Plate";
            row610.AlloyUNS = "K42339";
            row610.ClassCondition = "1";
            row610.Notes = "";
            row610.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26, 25.8, 25.4, 25.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row610);

            // Row 31: SA-543, B, Plate
            var row611 = new OldStressRowData();
            row611.LineNo = 31;
            row611.MaterialId = 8857;
            row611.SpecNo = "SA-543";
            row611.TypeGrade = "B";
            row611.ProductForm = "Plate";
            row611.AlloyUNS = "K42339";
            row611.ClassCondition = "2";
            row611.Notes = "";
            row611.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.5, 28.3, 27.9, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row611);

            // Row 32: SA-333, 3, Pipe
            var row612 = new OldStressRowData();
            row612.LineNo = 32;
            row612.MaterialId = 8858;
            row612.SpecNo = "SA-333";
            row612.TypeGrade = "3";
            row612.ProductForm = "Pipe";
            row612.AlloyUNS = "K31918";
            row612.ClassCondition = "";
            row612.Notes = "";
            row612.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row612);

            // Row 33: SA-333, 3, Wld. pipe
            var row613 = new OldStressRowData();
            row613.LineNo = 33;
            row613.MaterialId = 8859;
            row613.SpecNo = "SA-333";
            row613.TypeGrade = "3";
            row613.ProductForm = "Wld. pipe";
            row613.AlloyUNS = "K31918";
            row613.ClassCondition = "";
            row613.Notes = "G24";
            row613.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row613);

            // Row 34: SA-334, 3, Tube
            var row614 = new OldStressRowData();
            row614.LineNo = 34;
            row614.MaterialId = 8860;
            row614.SpecNo = "SA-334";
            row614.TypeGrade = "3";
            row614.ProductForm = "Tube";
            row614.AlloyUNS = "K31918";
            row614.ClassCondition = "";
            row614.Notes = "";
            row614.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row614);

            // Row 35: SA-334, 3, Wld. tube
            var row615 = new OldStressRowData();
            row615.LineNo = 35;
            row615.MaterialId = 8861;
            row615.SpecNo = "SA-334";
            row615.TypeGrade = "3";
            row615.ProductForm = "Wld. tube";
            row615.AlloyUNS = "K31918";
            row615.ClassCondition = "";
            row615.Notes = "G24";
            row615.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row615);

            // Row 36: SA-420, WPL3, Fittings
            var row616 = new OldStressRowData();
            row616.LineNo = 36;
            row616.MaterialId = 8862;
            row616.SpecNo = "SA-420";
            row616.TypeGrade = "WPL3";
            row616.ProductForm = "Fittings";
            row616.AlloyUNS = "";
            row616.ClassCondition = "";
            row616.Notes = "";
            row616.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row616);

            // Row 37: ..., , 
            var row617 = new OldStressRowData();
            row617.LineNo = 37;
            row617.MaterialId = 8865;
            row617.SpecNo = "...";
            row617.TypeGrade = "";
            row617.ProductForm = "";
            row617.AlloyUNS = "";
            row617.ClassCondition = "";
            row617.Notes = "";
            row617.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row617);

            // Row 38: SA-203, D, Plate
            var row618 = new OldStressRowData();
            row618.LineNo = 38;
            row618.MaterialId = 8864;
            row618.SpecNo = "SA-203";
            row618.TypeGrade = "D";
            row618.ProductForm = "Plate";
            row618.AlloyUNS = "K31718";
            row618.ClassCondition = "";
            row618.Notes = "";
            row618.StressValues = new double?[] { 16.2, null, 16.2, null, 16.2, 16.2, 16.2, 16.2, 16.2, 15.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row618);

            // Row 39: SA-203, D, Plate
            var row619 = new OldStressRowData();
            row619.LineNo = 39;
            row619.MaterialId = 8863;
            row619.SpecNo = "SA-203";
            row619.TypeGrade = "D";
            row619.ProductForm = "Plate";
            row619.AlloyUNS = "K31718";
            row619.ClassCondition = "";
            row619.Notes = "";
            row619.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 15.5, 13.9, 11.4, 9, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row619);

            // Row 40: SA-350, LF3, Forgings
            var row620 = new OldStressRowData();
            row620.LineNo = 40;
            row620.MaterialId = 8866;
            row620.SpecNo = "SA-350";
            row620.TypeGrade = "LF3";
            row620.ProductForm = "Forgings";
            row620.AlloyUNS = "K32025";
            row620.ClassCondition = "2";
            row620.Notes = "";
            row620.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row620);

            // Row 41: SA-765, III, Forgings
            var row621 = new OldStressRowData();
            row621.LineNo = 41;
            row621.MaterialId = 8867;
            row621.SpecNo = "SA-765";
            row621.TypeGrade = "III";
            row621.ProductForm = "Forgings";
            row621.AlloyUNS = "K32026";
            row621.ClassCondition = "";
            row621.Notes = "";
            row621.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row621);

            // Row 1: SA-203, E, Plate
            var row622 = new OldStressRowData();
            row622.LineNo = 1;
            row622.MaterialId = 8869;
            row622.SpecNo = "SA-203";
            row622.TypeGrade = "E";
            row622.ProductForm = "Plate";
            row622.AlloyUNS = "K32018";
            row622.ClassCondition = "";
            row622.Notes = "S1";
            row622.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row622);

            // Row 2: SA-203, E, Plate
            var row623 = new OldStressRowData();
            row623.LineNo = 2;
            row623.MaterialId = 8868;
            row623.SpecNo = "SA-203";
            row623.TypeGrade = "E";
            row623.ProductForm = "Plate";
            row623.AlloyUNS = "K32018";
            row623.ClassCondition = "";
            row623.Notes = "";
            row623.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 16.6, 14.8, 12, 9.3, 6.5, 4.5, 2.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row623);

            // Row 3: SA-352, LC3, Castings
            var row624 = new OldStressRowData();
            row624.LineNo = 3;
            row624.MaterialId = 8871;
            row624.SpecNo = "SA-352";
            row624.TypeGrade = "LC3";
            row624.ProductForm = "Castings";
            row624.AlloyUNS = "J31550";
            row624.ClassCondition = "";
            row624.Notes = "";
            row624.StressValues = new double?[] { 16.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row624);

            // Row 4: SA-352, LC3, Castings
            var row625 = new OldStressRowData();
            row625.LineNo = 4;
            row625.MaterialId = 8870;
            row625.SpecNo = "SA-352";
            row625.TypeGrade = "LC3";
            row625.ProductForm = "Castings";
            row625.AlloyUNS = "J31550";
            row625.ClassCondition = "";
            row625.Notes = "G1";
            row625.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row625);

            // Row 5: SA-203, F, Plate
            var row626 = new OldStressRowData();
            row626.LineNo = 5;
            row626.MaterialId = 8872;
            row626.SpecNo = "SA-203";
            row626.TypeGrade = "F";
            row626.ProductForm = "Plate";
            row626.AlloyUNS = "";
            row626.ClassCondition = "";
            row626.Notes = "";
            row626.StressValues = new double?[] { 18.8, 18.8, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row626);

            // Row 6: SA-203, F, Plate
            var row627 = new OldStressRowData();
            row627.LineNo = 6;
            row627.MaterialId = 8873;
            row627.SpecNo = "SA-203";
            row627.TypeGrade = "F";
            row627.ProductForm = "Plate";
            row627.AlloyUNS = "";
            row627.ClassCondition = "";
            row627.Notes = "";
            row627.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row627);

            // Row 7: SA-508, 4N, Forgings
            var row628 = new OldStressRowData();
            row628.LineNo = 7;
            row628.MaterialId = 8874;
            row628.SpecNo = "SA-508";
            row628.TypeGrade = "4N";
            row628.ProductForm = "Forgings";
            row628.AlloyUNS = "K22375";
            row628.ClassCondition = "3";
            row628.Notes = "";
            row628.StressValues = new double?[] { 22.5, null, 22.5, 22.5, 22.5, 22.3, 22.1, 21.8, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row628);

            // Row 8: SA-508, 4N, Forgings
            var row629 = new OldStressRowData();
            row629.LineNo = 8;
            row629.MaterialId = 8876;
            row629.SpecNo = "SA-508";
            row629.TypeGrade = "4N";
            row629.ProductForm = "Forgings";
            row629.AlloyUNS = "K22375";
            row629.ClassCondition = "1";
            row629.Notes = "";
            row629.StressValues = new double?[] { 26.2, null, 26.2, null, 26.2, 26, 25.8, 25.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row629);

            // Row 9: SA-508, 4N, Forgings
            var row630 = new OldStressRowData();
            row630.LineNo = 9;
            row630.MaterialId = 8875;
            row630.SpecNo = "SA-508";
            row630.TypeGrade = "4N";
            row630.ProductForm = "Forgings";
            row630.AlloyUNS = "K22375";
            row630.ClassCondition = "1";
            row630.Notes = "";
            row630.StressValues = new double?[] { 26.3, null, 26.3, 26.3, 26.3, 26, 25.8, 25.4, 25.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row630);

            // Row 10: SA-508, 4N, Forgings
            var row631 = new OldStressRowData();
            row631.LineNo = 10;
            row631.MaterialId = 8877;
            row631.SpecNo = "SA-508";
            row631.TypeGrade = "4N";
            row631.ProductForm = "Forgings";
            row631.AlloyUNS = "K22375";
            row631.ClassCondition = "2";
            row631.Notes = "";
            row631.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.5, 28.3, 27.9, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row631);

            // Row 11: SA-723, 3, Forgings
            var row632 = new OldStressRowData();
            row632.LineNo = 11;
            row632.MaterialId = 8878;
            row632.SpecNo = "SA-723";
            row632.TypeGrade = "3";
            row632.ProductForm = "Forgings";
            row632.AlloyUNS = "K44045";
            row632.ClassCondition = "1";
            row632.Notes = "W1";
            row632.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.3, 27.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row632);

            // Row 12: SA-723, 3, Forgings
            var row633 = new OldStressRowData();
            row633.LineNo = 12;
            row633.MaterialId = 8879;
            row633.SpecNo = "SA-723";
            row633.TypeGrade = "3";
            row633.ProductForm = "Forgings";
            row633.AlloyUNS = "K44045";
            row633.ClassCondition = "2";
            row633.Notes = "W1";
            row633.StressValues = new double?[] { 33.8, null, 33.8, null, 33.8, 33.8, 33.8, 33.8, 33.2, 32.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row633);

            // Row 13: SA-723, 3, Forgings
            var row634 = new OldStressRowData();
            row634.LineNo = 13;
            row634.MaterialId = 8880;
            row634.SpecNo = "SA-723";
            row634.TypeGrade = "3";
            row634.ProductForm = "Forgings";
            row634.AlloyUNS = "K44045";
            row634.ClassCondition = "3";
            row634.Notes = "W1";
            row634.StressValues = new double?[] { 38.8, null, 38.8, null, 38.8, 38.8, 38.8, 38.8, 38.1, 37.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row634);

            // Row 14: SA-723, 3, Forgings
            var row635 = new OldStressRowData();
            row635.LineNo = 14;
            row635.MaterialId = 8881;
            row635.SpecNo = "SA-723";
            row635.TypeGrade = "3";
            row635.ProductForm = "Forgings";
            row635.AlloyUNS = "K44045";
            row635.ClassCondition = "4";
            row635.Notes = "W1";
            row635.StressValues = new double?[] { 43.8, null, 43.8, null, 43.8, 43.8, 43.8, 43.8, 43.1, 41.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row635);

            // Row 15: SA-723, 3, Forgings
            var row636 = new OldStressRowData();
            row636.LineNo = 15;
            row636.MaterialId = 8882;
            row636.SpecNo = "SA-723";
            row636.TypeGrade = "3";
            row636.ProductForm = "Forgings";
            row636.AlloyUNS = "K44045";
            row636.ClassCondition = "5";
            row636.Notes = "W1";
            row636.StressValues = new double?[] { 47.5, null, 47.5, null, 47.5, 47.5, 47.5, 47.5, 46.8, 45.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row636);

            // Row 16: SA-645, , Plate
            var row637 = new OldStressRowData();
            row637.LineNo = 16;
            row637.MaterialId = 8883;
            row637.SpecNo = "SA-645";
            row637.TypeGrade = "";
            row637.ProductForm = "Plate";
            row637.AlloyUNS = "K41583";
            row637.ClassCondition = "";
            row637.Notes = "";
            row637.StressValues = new double?[] { 23.7, null, 23.7, 23.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row637);

            // Row 17: SA-522, II, Forgings
            var row638 = new OldStressRowData();
            row638.LineNo = 17;
            row638.MaterialId = 8884;
            row638.SpecNo = "SA-522";
            row638.TypeGrade = "II";
            row638.ProductForm = "Forgings";
            row638.AlloyUNS = "K71340";
            row638.ClassCondition = "";
            row638.Notes = "W5";
            row638.StressValues = new double?[] { 23.7, null, 22.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row638);

            // Row 18: SA-553, II, Plate
            var row639 = new OldStressRowData();
            row639.LineNo = 18;
            row639.MaterialId = 8885;
            row639.SpecNo = "SA-553";
            row639.TypeGrade = "II";
            row639.ProductForm = "Plate";
            row639.AlloyUNS = "K71340";
            row639.ClassCondition = "";
            row639.Notes = "G6";
            row639.StressValues = new double?[] { 25, null, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row639);

            // Row 19: SA-553, II, Plate
            var row640 = new OldStressRowData();
            row640.LineNo = 19;
            row640.MaterialId = 8887;
            row640.SpecNo = "SA-553";
            row640.TypeGrade = "II";
            row640.ProductForm = "Plate";
            row640.AlloyUNS = "K71340";
            row640.ClassCondition = "";
            row640.Notes = "G6, W5";
            row640.StressValues = new double?[] { 23.7, 23.7, 22.2, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row640);

            // Row 20: SA-553, II, Plate
            var row641 = new OldStressRowData();
            row641.LineNo = 20;
            row641.MaterialId = 8886;
            row641.SpecNo = "SA-553";
            row641.TypeGrade = "II";
            row641.ProductForm = "Plate";
            row641.AlloyUNS = "K71340";
            row641.ClassCondition = "";
            row641.Notes = "G6, W4";
            row641.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row641);

            // Row 21: SA-333, 8, Wld. & smls. pipe
            var row642 = new OldStressRowData();
            row642.LineNo = 21;
            row642.MaterialId = 8888;
            row642.SpecNo = "SA-333";
            row642.TypeGrade = "8";
            row642.ProductForm = "Wld. & smls. pipe";
            row642.AlloyUNS = "K81340";
            row642.ClassCondition = "";
            row642.Notes = "W12";
            row642.StressValues = new double?[] { 25, null, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row642);

            // Row 22: SA-333, 8, Wld. & smls. pipe
            var row643 = new OldStressRowData();
            row643.LineNo = 22;
            row643.MaterialId = 8889;
            row643.SpecNo = "SA-333";
            row643.TypeGrade = "8";
            row643.ProductForm = "Wld. & smls. pipe";
            row643.AlloyUNS = "K81340";
            row643.ClassCondition = "";
            row643.Notes = "W5, W12";
            row643.StressValues = new double?[] { 23.7, null, 22.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row643);

            // Row 23: SA-333, 8, Smls. pipe
            var row644 = new OldStressRowData();
            row644.LineNo = 23;
            row644.MaterialId = 8890;
            row644.SpecNo = "SA-333";
            row644.TypeGrade = "8";
            row644.ProductForm = "Smls. pipe";
            row644.AlloyUNS = "K81340";
            row644.ClassCondition = "";
            row644.Notes = "W4";
            row644.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row644);

            // Row 24: SA-333, 8, Smls. pipe
            var row645 = new OldStressRowData();
            row645.LineNo = 24;
            row645.MaterialId = 8891;
            row645.SpecNo = "SA-333";
            row645.TypeGrade = "8";
            row645.ProductForm = "Smls. pipe";
            row645.AlloyUNS = "K81340";
            row645.ClassCondition = "";
            row645.Notes = "W5";
            row645.StressValues = new double?[] { 23.8, 23.8, 22.3, 21.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row645);

            // Row 25: SA-333, 8, Wld. pipe
            var row646 = new OldStressRowData();
            row646.LineNo = 25;
            row646.MaterialId = 8892;
            row646.SpecNo = "SA-333";
            row646.TypeGrade = "8";
            row646.ProductForm = "Wld. pipe";
            row646.AlloyUNS = "K81340";
            row646.ClassCondition = "";
            row646.Notes = "G24, W3";
            row646.StressValues = new double?[] { 21.3, 21.3, 19.9, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row646);

            // Row 26: SA-334, 8, Wld. tube
            var row647 = new OldStressRowData();
            row647.LineNo = 26;
            row647.MaterialId = 8893;
            row647.SpecNo = "SA-334";
            row647.TypeGrade = "8";
            row647.ProductForm = "Wld. tube";
            row647.AlloyUNS = "K81340";
            row647.ClassCondition = "";
            row647.Notes = "W12";
            row647.StressValues = new double?[] { 25, null, 23.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row647);

            // Row 27: SA-334, 8, Wld. & smls. tube
            var row648 = new OldStressRowData();
            row648.LineNo = 27;
            row648.MaterialId = 8894;
            row648.SpecNo = "SA-334";
            row648.TypeGrade = "8";
            row648.ProductForm = "Wld. & smls. tube";
            row648.AlloyUNS = "K81340";
            row648.ClassCondition = "";
            row648.Notes = "W5, W12";
            row648.StressValues = new double?[] { 23.7, null, 22.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row648);

            // Row 28: SA-334, 8, Smls. tube
            var row649 = new OldStressRowData();
            row649.LineNo = 28;
            row649.MaterialId = 8895;
            row649.SpecNo = "SA-334";
            row649.TypeGrade = "8";
            row649.ProductForm = "Smls. tube";
            row649.AlloyUNS = "K81340";
            row649.ClassCondition = "";
            row649.Notes = "W4";
            row649.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row649);

            // Row 29: SA-334, 8, Smls. tube
            var row650 = new OldStressRowData();
            row650.LineNo = 29;
            row650.MaterialId = 8896;
            row650.SpecNo = "SA-334";
            row650.TypeGrade = "8";
            row650.ProductForm = "Smls. tube";
            row650.AlloyUNS = "K81340";
            row650.ClassCondition = "";
            row650.Notes = "W5";
            row650.StressValues = new double?[] { 23.8, 23.8, 22.3, 21.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row650);

            // Row 30: SA-334, 8, Wld. tube
            var row651 = new OldStressRowData();
            row651.LineNo = 30;
            row651.MaterialId = 8897;
            row651.SpecNo = "SA-334";
            row651.TypeGrade = "8";
            row651.ProductForm = "Wld. tube";
            row651.AlloyUNS = "K81340";
            row651.ClassCondition = "";
            row651.Notes = "G24, W3";
            row651.StressValues = new double?[] { 21.3, 21.3, 19.9, 19.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row651);

            // Row 31: SA-353, , Plate
            var row652 = new OldStressRowData();
            row652.LineNo = 31;
            row652.MaterialId = 8898;
            row652.SpecNo = "SA-353";
            row652.TypeGrade = "";
            row652.ProductForm = "Plate";
            row652.AlloyUNS = "K81340";
            row652.ClassCondition = "";
            row652.Notes = "W4";
            row652.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row652);

            // Row 32: SA-353, , Plate
            var row653 = new OldStressRowData();
            row653.LineNo = 32;
            row653.MaterialId = 8899;
            row653.SpecNo = "SA-353";
            row653.TypeGrade = "";
            row653.ProductForm = "Plate";
            row653.AlloyUNS = "K81340";
            row653.ClassCondition = "";
            row653.Notes = "W5";
            row653.StressValues = new double?[] { 23.8, 23.8, 22.3, 21.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row653);

            // Row 33: SA-353, , Plate
            var row654 = new OldStressRowData();
            row654.LineNo = 33;
            row654.MaterialId = 8900;
            row654.SpecNo = "SA-353";
            row654.TypeGrade = "";
            row654.ProductForm = "Plate";
            row654.AlloyUNS = "K81340";
            row654.ClassCondition = "";
            row654.Notes = "W5";
            row654.StressValues = new double?[] { 23.7, null, 22.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row654);

            // Row 34: SA-420, WPL8, Smls. & wld. fittings
            var row655 = new OldStressRowData();
            row655.LineNo = 34;
            row655.MaterialId = 8901;
            row655.SpecNo = "SA-420";
            row655.TypeGrade = "WPL8";
            row655.ProductForm = "Smls. & wld. fittings";
            row655.AlloyUNS = "K81340";
            row655.ClassCondition = "";
            row655.Notes = "W4";
            row655.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row655);

            // Row 35: SA-420, WPL8, Smls. & wld. fittings
            var row656 = new OldStressRowData();
            row656.LineNo = 35;
            row656.MaterialId = 8902;
            row656.SpecNo = "SA-420";
            row656.TypeGrade = "WPL8";
            row656.ProductForm = "Smls. & wld. fittings";
            row656.AlloyUNS = "K81340";
            row656.ClassCondition = "";
            row656.Notes = "W3";
            row656.StressValues = new double?[] { 23.7, 23.7, 22.2, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row656);

            // Row 36: SA-522, I, Forgings
            var row657 = new OldStressRowData();
            row657.LineNo = 36;
            row657.MaterialId = 8903;
            row657.SpecNo = "SA-522";
            row657.TypeGrade = "I";
            row657.ProductForm = "Forgings";
            row657.AlloyUNS = "K81340";
            row657.ClassCondition = "";
            row657.Notes = "S8, W4";
            row657.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row657);

            // Row 37: SA-522, I, Forgings
            var row658 = new OldStressRowData();
            row658.LineNo = 37;
            row658.MaterialId = 8904;
            row658.SpecNo = "SA-522";
            row658.TypeGrade = "I";
            row658.ProductForm = "Forgings";
            row658.AlloyUNS = "K81340";
            row658.ClassCondition = "";
            row658.Notes = "S8, W5";
            row658.StressValues = new double?[] { 23.7, 23.7, 22.2, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row658);

            // Row 38: SA-553, I, Plate
            var row659 = new OldStressRowData();
            row659.LineNo = 38;
            row659.MaterialId = 8905;
            row659.SpecNo = "SA-553";
            row659.TypeGrade = "I";
            row659.ProductForm = "Plate";
            row659.AlloyUNS = "K81340";
            row659.ClassCondition = "";
            row659.Notes = "W4";
            row659.StressValues = new double?[] { 25, 25, 23.4, 22.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row659);

            // Row 39: SA-553, I, Plate
            var row660 = new OldStressRowData();
            row660.LineNo = 39;
            row660.MaterialId = 8906;
            row660.SpecNo = "SA-553";
            row660.TypeGrade = "I";
            row660.ProductForm = "Plate";
            row660.AlloyUNS = "K81340";
            row660.ClassCondition = "";
            row660.Notes = "W5";
            row660.StressValues = new double?[] { 23.7, 23.7, 22.2, 21.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row660);

            // Row 40: SA-638, 660, Bar
            var row661 = new OldStressRowData();
            row661.LineNo = 40;
            row661.MaterialId = 8907;
            row661.SpecNo = "SA-638";
            row661.TypeGrade = "660";
            row661.ProductForm = "Bar";
            row661.AlloyUNS = "S66286";
            row661.ClassCondition = "";
            row661.Notes = "G9, G28, W1";
            row661.StressValues = new double?[] { 32.5, null, 32.5, null, 32.5, 32.4, 31.8, 31.2, 30.9, 30.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row661);

            // Row 41: SA-351, CN7M, Castings
            var row662 = new OldStressRowData();
            row662.LineNo = 41;
            row662.MaterialId = 8908;
            row662.SpecNo = "SA-351";
            row662.TypeGrade = "CN7M";
            row662.ProductForm = "Castings";
            row662.AlloyUNS = "J95150";
            row662.ClassCondition = "";
            row662.Notes = "";
            row662.StressValues = new double?[] { 15.6, null, 14.5, null, 13.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row662);

            // Row 1: SA-240, 204, Plate
            var row663 = new OldStressRowData();
            row663.LineNo = 1;
            row663.MaterialId = 9816;
            row663.SpecNo = "SA-240";
            row663.TypeGrade = "204";
            row663.ProductForm = "Plate";
            row663.AlloyUNS = "S20400";
            row663.ClassCondition = "";
            row663.Notes = "G5";
            row663.StressValues = new double?[] { 23.8, null, 20.6, null, 18, 16.6, 15.9, 15.7, 15.7, 15.6, 15.6, 15.5, 15.2, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row663);

            // Row 2: SA-240, 204, Plate
            var row664 = new OldStressRowData();
            row664.LineNo = 2;
            row664.MaterialId = 9817;
            row664.SpecNo = "SA-240";
            row664.TypeGrade = "204";
            row664.ProductForm = "Plate";
            row664.AlloyUNS = "S20400";
            row664.ClassCondition = "";
            row664.Notes = "";
            row664.StressValues = new double?[] { 23.8, null, 20.6, null, 18, 16.6, 15.9, 15.7, 15.6, 15.5, 15.3, 15.1, 14.8, 14.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row664);

            // Row 3: SA-182, F316L, Forgings
            var row665 = new OldStressRowData();
            row665.LineNo = 3;
            row665.MaterialId = 8910;
            row665.SpecNo = "SA-182";
            row665.TypeGrade = "F316L";
            row665.ProductForm = "Forgings";
            row665.AlloyUNS = "S31603";
            row665.ClassCondition = "";
            row665.Notes = "G5";
            row665.StressValues = new double?[] { 16.3, null, 15.7, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row665);

            // Row 4: SA-182, F316L, Forgings
            var row666 = new OldStressRowData();
            row666.LineNo = 4;
            row666.MaterialId = 8909;
            row666.SpecNo = "SA-182";
            row666.TypeGrade = "F316L";
            row666.ProductForm = "Forgings";
            row666.AlloyUNS = "S31603";
            row666.ClassCondition = "";
            row666.Notes = "";
            row666.StressValues = new double?[] { 16.3, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row666);

            // Row 5: SA-336, F316L, Forgings
            var row667 = new OldStressRowData();
            row667.LineNo = 5;
            row667.MaterialId = 8911;
            row667.SpecNo = "SA-336";
            row667.TypeGrade = "F316L";
            row667.ProductForm = "Forgings";
            row667.AlloyUNS = "S31603";
            row667.ClassCondition = "";
            row667.Notes = "G5";
            row667.StressValues = new double?[] { 16.3, null, 15.7, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row667);

            // Row 6: SA-336, F316L, Forgings
            var row668 = new OldStressRowData();
            row668.LineNo = 6;
            row668.MaterialId = 8912;
            row668.SpecNo = "SA-336";
            row668.TypeGrade = "F316L";
            row668.ProductForm = "Forgings";
            row668.AlloyUNS = "S31603";
            row668.ClassCondition = "";
            row668.Notes = "";
            row668.StressValues = new double?[] { 16.3, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row668);

            // Row 7: SA-182, F316L, Forgings
            var row669 = new OldStressRowData();
            row669.LineNo = 7;
            row669.MaterialId = 8913;
            row669.SpecNo = "SA-182";
            row669.TypeGrade = "F316L";
            row669.ProductForm = "Forgings";
            row669.AlloyUNS = "S31603";
            row669.ClassCondition = "";
            row669.Notes = "G5";
            row669.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row669);

            // Row 8: SA-182, F316L, Forgings
            var row670 = new OldStressRowData();
            row670.LineNo = 8;
            row670.MaterialId = 9840;
            row670.SpecNo = "SA-182";
            row670.TypeGrade = "F316L";
            row670.ProductForm = "Forgings";
            row670.AlloyUNS = "S31603";
            row670.ClassCondition = "";
            row670.Notes = "";
            row670.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row670);

            // Row 9: SA-213, TP316L, Smls. tube
            var row671 = new OldStressRowData();
            row671.LineNo = 9;
            row671.MaterialId = 8914;
            row671.SpecNo = "SA-213";
            row671.TypeGrade = "TP316L";
            row671.ProductForm = "Smls. tube";
            row671.AlloyUNS = "S31603";
            row671.ClassCondition = "";
            row671.Notes = "G5";
            row671.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row671);

            // Row 10: SA-213, TP316L, Smls. tube
            var row672 = new OldStressRowData();
            row672.LineNo = 10;
            row672.MaterialId = 8915;
            row672.SpecNo = "SA-213";
            row672.TypeGrade = "TP316L";
            row672.ProductForm = "Smls. tube";
            row672.AlloyUNS = "S31603";
            row672.ClassCondition = "";
            row672.Notes = "";
            row672.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row672);

            // Row 11: SA-240, 316L, Plate
            var row673 = new OldStressRowData();
            row673.LineNo = 11;
            row673.MaterialId = 8916;
            row673.SpecNo = "SA-240";
            row673.TypeGrade = "316L";
            row673.ProductForm = "Plate";
            row673.AlloyUNS = "S31603";
            row673.ClassCondition = "";
            row673.Notes = "G5";
            row673.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row673);

            // Row 12: SA-240, 316L, Plate
            var row674 = new OldStressRowData();
            row674.LineNo = 12;
            row674.MaterialId = 8917;
            row674.SpecNo = "SA-240";
            row674.TypeGrade = "316L";
            row674.ProductForm = "Plate";
            row674.AlloyUNS = "S31603";
            row674.ClassCondition = "";
            row674.Notes = "";
            row674.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row674);

            // Row 13: SA-249, TP316L, Wld. tube
            var row675 = new OldStressRowData();
            row675.LineNo = 13;
            row675.MaterialId = 8918;
            row675.SpecNo = "SA-249";
            row675.TypeGrade = "TP316L";
            row675.ProductForm = "Wld. tube";
            row675.AlloyUNS = "S31603";
            row675.ClassCondition = "";
            row675.Notes = "G5, W12";
            row675.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row675);

            // Row 14: SA-249, TP316L, Wld. tube
            var row676 = new OldStressRowData();
            row676.LineNo = 14;
            row676.MaterialId = 8919;
            row676.SpecNo = "SA-249";
            row676.TypeGrade = "TP316L";
            row676.ProductForm = "Wld. tube";
            row676.AlloyUNS = "S31603";
            row676.ClassCondition = "";
            row676.Notes = "G5, G24";
            row676.StressValues = new double?[] { 14.2, null, 14.2, null, 13.6, 13.3, 12.6, 11.9, 11.7, 11.5, 11.2, 11.1, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row676);

            // Row 15: SA-249, TP316L, Wld. tube
            var row677 = new OldStressRowData();
            row677.LineNo = 15;
            row677.MaterialId = 8920;
            row677.SpecNo = "SA-249";
            row677.TypeGrade = "TP316L";
            row677.ProductForm = "Wld. tube";
            row677.AlloyUNS = "S31603";
            row677.ClassCondition = "";
            row677.Notes = "G24";
            row677.StressValues = new double?[] { 14.2, null, 12, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.2, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row677);

            // Row 16: SA-312, TP316L, Wld. & smls. pipe
            var row678 = new OldStressRowData();
            row678.LineNo = 16;
            row678.MaterialId = 8921;
            row678.SpecNo = "SA-312";
            row678.TypeGrade = "TP316L";
            row678.ProductForm = "Wld. & smls. pipe";
            row678.AlloyUNS = "S31603";
            row678.ClassCondition = "";
            row678.Notes = "G5, W12";
            row678.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row678);

            // Row 17: SA-312, TP316L, Smls. pipe
            var row679 = new OldStressRowData();
            row679.LineNo = 17;
            row679.MaterialId = 8922;
            row679.SpecNo = "SA-312";
            row679.TypeGrade = "TP316L";
            row679.ProductForm = "Smls. pipe";
            row679.AlloyUNS = "S31603";
            row679.ClassCondition = "";
            row679.Notes = "";
            row679.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row679);

            // Row 18: SA-312, TP316L, Wld. pipe
            var row680 = new OldStressRowData();
            row680.LineNo = 18;
            row680.MaterialId = 8923;
            row680.SpecNo = "SA-312";
            row680.TypeGrade = "TP316L";
            row680.ProductForm = "Wld. pipe";
            row680.AlloyUNS = "S31603";
            row680.ClassCondition = "";
            row680.Notes = "G5, G24";
            row680.StressValues = new double?[] { 14.2, null, 14.2, null, 13.6, 13.3, 12.6, 11.9, 11.7, 11.5, 11.2, 11.1, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row680);

            // Row 19: SA-312, TP316L, Wld. pipe
            var row681 = new OldStressRowData();
            row681.LineNo = 19;
            row681.MaterialId = 8924;
            row681.SpecNo = "SA-312";
            row681.TypeGrade = "TP316L";
            row681.ProductForm = "Wld. pipe";
            row681.AlloyUNS = "S31603";
            row681.ClassCondition = "";
            row681.Notes = "G24";
            row681.StressValues = new double?[] { 14.2, null, 12, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.2, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row681);

            // Row 20: SA-358, 316L, Wld. pipe
            var row682 = new OldStressRowData();
            row682.LineNo = 20;
            row682.MaterialId = 8925;
            row682.SpecNo = "SA-358";
            row682.TypeGrade = "316L";
            row682.ProductForm = "Wld. pipe";
            row682.AlloyUNS = "S31603";
            row682.ClassCondition = "1";
            row682.Notes = "G5";
            row682.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row682);

            // Row 21: ..., , 
            var row683 = new OldStressRowData();
            row683.LineNo = 21;
            row683.MaterialId = 8927;
            row683.SpecNo = "...";
            row683.TypeGrade = "";
            row683.ProductForm = "";
            row683.AlloyUNS = "";
            row683.ClassCondition = "";
            row683.Notes = "";
            row683.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row683);

            // Row 22: ..., , 
            var row684 = new OldStressRowData();
            row684.LineNo = 22;
            row684.MaterialId = 8926;
            row684.SpecNo = "...";
            row684.TypeGrade = "";
            row684.ProductForm = "";
            row684.AlloyUNS = "";
            row684.ClassCondition = "";
            row684.Notes = "";
            row684.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row684);

            // Row 23: ..., , 
            var row685 = new OldStressRowData();
            row685.LineNo = 23;
            row685.MaterialId = 9818;
            row685.SpecNo = "...";
            row685.TypeGrade = "";
            row685.ProductForm = "";
            row685.AlloyUNS = "";
            row685.ClassCondition = "";
            row685.Notes = "";
            row685.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row685);

            // Row 24: ..., , 
            var row686 = new OldStressRowData();
            row686.LineNo = 24;
            row686.MaterialId = 9819;
            row686.SpecNo = "...";
            row686.TypeGrade = "";
            row686.ProductForm = "";
            row686.AlloyUNS = "";
            row686.ClassCondition = "";
            row686.Notes = "";
            row686.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row686);

            // Row 25: SA-403, 316L, Smls. & wld. fittings
            var row687 = new OldStressRowData();
            row687.LineNo = 25;
            row687.MaterialId = 8928;
            row687.SpecNo = "SA-403";
            row687.TypeGrade = "316L";
            row687.ProductForm = "Smls. & wld. fittings";
            row687.AlloyUNS = "S31603";
            row687.ClassCondition = "";
            row687.Notes = "G5, W12, W15";
            row687.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row687);

            // Row 26: ..., , 
            var row688 = new OldStressRowData();
            row688.LineNo = 26;
            row688.MaterialId = 8929;
            row688.SpecNo = "...";
            row688.TypeGrade = "";
            row688.ProductForm = "";
            row688.AlloyUNS = "";
            row688.ClassCondition = "";
            row688.Notes = "";
            row688.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row688);

            // Row 27: ..., , 
            var row689 = new OldStressRowData();
            row689.LineNo = 27;
            row689.MaterialId = 8930;
            row689.SpecNo = "...";
            row689.TypeGrade = "";
            row689.ProductForm = "";
            row689.AlloyUNS = "";
            row689.ClassCondition = "";
            row689.Notes = "";
            row689.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row689);

            // Row 28: SA-409, TP316L, Wld. pipe
            var row690 = new OldStressRowData();
            row690.LineNo = 28;
            row690.MaterialId = 8931;
            row690.SpecNo = "SA-409";
            row690.TypeGrade = "TP316L";
            row690.ProductForm = "Wld. pipe";
            row690.AlloyUNS = "S31603";
            row690.ClassCondition = "";
            row690.Notes = "G5";
            row690.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row690);

            // Row 29: SA-479, 316L, Bar
            var row691 = new OldStressRowData();
            row691.LineNo = 29;
            row691.MaterialId = 8932;
            row691.SpecNo = "SA-479";
            row691.TypeGrade = "316L";
            row691.ProductForm = "Bar";
            row691.AlloyUNS = "S31603";
            row691.ClassCondition = "";
            row691.Notes = "G5, G22";
            row691.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row691);

            // Row 30: SA-479, 316L, Bar
            var row692 = new OldStressRowData();
            row692.LineNo = 30;
            row692.MaterialId = 8933;
            row692.SpecNo = "SA-479";
            row692.TypeGrade = "316L";
            row692.ProductForm = "Bar";
            row692.AlloyUNS = "S31603";
            row692.ClassCondition = "";
            row692.Notes = "G22";
            row692.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row692);

            // Row 31: SA-688, TP316L, Wld. tube
            var row693 = new OldStressRowData();
            row693.LineNo = 31;
            row693.MaterialId = 8934;
            row693.SpecNo = "SA-688";
            row693.TypeGrade = "TP316L";
            row693.ProductForm = "Wld. tube";
            row693.AlloyUNS = "S31603";
            row693.ClassCondition = "";
            row693.Notes = "G5, W12";
            row693.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row693);

            // Row 32: SA-688, TP316L, Wld. tube
            var row694 = new OldStressRowData();
            row694.LineNo = 32;
            row694.MaterialId = 8935;
            row694.SpecNo = "SA-688";
            row694.TypeGrade = "TP316L";
            row694.ProductForm = "Wld. tube";
            row694.AlloyUNS = "S31603";
            row694.ClassCondition = "";
            row694.Notes = "G5, G24";
            row694.StressValues = new double?[] { 14.2, null, 14.2, null, 13.6, 13.3, 12.6, 11.9, 11.7, 11.5, 11.2, 11.1, 10.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row694);

            // Row 33: SA-688, TP316L, Wld. tube
            var row695 = new OldStressRowData();
            row695.LineNo = 33;
            row695.MaterialId = 8936;
            row695.SpecNo = "SA-688";
            row695.TypeGrade = "TP316L";
            row695.ProductForm = "Wld. tube";
            row695.AlloyUNS = "S31603";
            row695.ClassCondition = "";
            row695.Notes = "G24";
            row695.StressValues = new double?[] { 14.2, null, 12, null, 10.8, 9.9, 9.3, 8.8, 8.7, 8.5, 8.3, 8.2, 8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row695);

            // Row 34: SA-813, TP316L, Wld. pipe
            var row696 = new OldStressRowData();
            row696.LineNo = 34;
            row696.MaterialId = 8937;
            row696.SpecNo = "SA-813";
            row696.TypeGrade = "TP316L";
            row696.ProductForm = "Wld. pipe";
            row696.AlloyUNS = "S31603";
            row696.ClassCondition = "";
            row696.Notes = "G5, W12";
            row696.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row696);

            // Row 35: SA-814, TP316L, Wld. pipe
            var row697 = new OldStressRowData();
            row697.LineNo = 35;
            row697.MaterialId = 8938;
            row697.SpecNo = "SA-814";
            row697.TypeGrade = "TP316L";
            row697.ProductForm = "Wld. pipe";
            row697.AlloyUNS = "S31603";
            row697.ClassCondition = "";
            row697.Notes = "G5, W12";
            row697.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row697);

            // Row 36: SA-351, CF3M, Castings
            var row698 = new OldStressRowData();
            row698.LineNo = 36;
            row698.MaterialId = 8947;
            row698.SpecNo = "SA-351";
            row698.TypeGrade = "CF3M";
            row698.ProductForm = "Castings";
            row698.AlloyUNS = "J92800";
            row698.ClassCondition = "";
            row698.Notes = "G5, G16, G17, G32";
            row698.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row698);

            // Row 37: SA-351, CF3M, Castings
            var row699 = new OldStressRowData();
            row699.LineNo = 37;
            row699.MaterialId = 8948;
            row699.SpecNo = "SA-351";
            row699.TypeGrade = "CF3M";
            row699.ProductForm = "Castings";
            row699.AlloyUNS = "J92800";
            row699.ClassCondition = "";
            row699.Notes = "G1, G5, G32";
            row699.StressValues = new double?[] { 17.5, null, 17.5, null, 17.1, 16.8, 16.8, 16.8, 16.6, 16.3, 16, 15.8, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row699);

            // Row 38: SA-351, CF3M, Castings
            var row700 = new OldStressRowData();
            row700.LineNo = 38;
            row700.MaterialId = 8949;
            row700.SpecNo = "SA-351";
            row700.TypeGrade = "CF3M";
            row700.ProductForm = "Castings";
            row700.AlloyUNS = "J92800";
            row700.ClassCondition = "";
            row700.Notes = "G1, G32";
            row700.StressValues = new double?[] { 17.5, null, 17.5, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
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
