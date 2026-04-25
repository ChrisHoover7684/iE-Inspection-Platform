using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch003
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch003(MaterialStressService service)
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

            // Row 8: SA-738, B, Plate
            var row201 = new OldStressRowData();
            row201.LineNo = 8;
            row201.MaterialId = 8460;
            row201.SpecNo = "SA-738";
            row201.TypeGrade = "B";
            row201.ProductForm = "Plate";
            row201.AlloyUNS = "K12447";
            row201.ClassCondition = "";
            row201.Notes = "G20";
            row201.StressValues = new double?[] { 21.3, 21.3, 21.3, null, 21.3, 21.3, 21.3, 21.3, 21.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row201);

            // Row 9: SA-372, C, Forgings
            var row202 = new OldStressRowData();
            row202.LineNo = 9;
            row202.MaterialId = 8461;
            row202.SpecNo = "SA-372";
            row202.TypeGrade = "C";
            row202.ProductForm = "Forgings";
            row202.AlloyUNS = "K04801";
            row202.ClassCondition = "";
            row202.Notes = "W2, W11";
            row202.StressValues = new double?[] { 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row202);

            // Row 10: SA-372, C, Forgings
            var row203 = new OldStressRowData();
            row203.LineNo = 10;
            row203.MaterialId = 9781;
            row203.SpecNo = "SA-372";
            row203.TypeGrade = "C";
            row203.ProductForm = "Forgings";
            row203.AlloyUNS = "K04801";
            row203.ClassCondition = "";
            row203.Notes = "W2, W11";
            row203.StressValues = new double?[] { 22.5, null, 22.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row203);

            // Row 11: SA-724, A, Plate
            var row204 = new OldStressRowData();
            row204.LineNo = 11;
            row204.MaterialId = 8462;
            row204.SpecNo = "SA-724";
            row204.TypeGrade = "A";
            row204.ProductForm = "Plate";
            row204.AlloyUNS = "K11831";
            row204.ClassCondition = "";
            row204.Notes = "";
            row204.StressValues = new double?[] { 22.5, 22.5, 22.5, 22.5, 22.5, 22.3, 22.3, 22.3, 22.2, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row204);

            // Row 12: SA-724, C, Plate
            var row205 = new OldStressRowData();
            row205.LineNo = 12;
            row205.MaterialId = 8463;
            row205.SpecNo = "SA-724";
            row205.TypeGrade = "C";
            row205.ProductForm = "Plate";
            row205.AlloyUNS = "K12037";
            row205.ClassCondition = "";
            row205.Notes = "";
            row205.StressValues = new double?[] { 22.5, 22.5, 22.5, 22.5, 22.5, 22.3, 22.3, 22.3, 22.2, 21.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row205);

            // Row 13: SA-724, B, Plate
            var row206 = new OldStressRowData();
            row206.LineNo = 13;
            row206.MaterialId = 8464;
            row206.SpecNo = "SA-724";
            row206.TypeGrade = "B";
            row206.ProductForm = "Plate";
            row206.AlloyUNS = "K12031";
            row206.ClassCondition = "";
            row206.Notes = "";
            row206.StressValues = new double?[] { 23.8, 23.8, 23.8, 23.8, 23.8, 23.5, 23.5, 23.5, 23.4, 23.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row206);

            // Row 14: SA-812, 65, Sheet
            var row207 = new OldStressRowData();
            row207.LineNo = 14;
            row207.MaterialId = 8465;
            row207.SpecNo = "SA-812";
            row207.TypeGrade = "65";
            row207.ProductForm = "Sheet";
            row207.AlloyUNS = "";
            row207.ClassCondition = "";
            row207.Notes = "";
            row207.StressValues = new double?[] { 21.3, 21.3, 21.3, null, 21.3, 21.3, 21.3, 21.3, 21.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row207);

            // Row 15: SA-737, B, Plate
            var row208 = new OldStressRowData();
            row208.LineNo = 15;
            row208.MaterialId = 8466;
            row208.SpecNo = "SA-737";
            row208.TypeGrade = "B";
            row208.ProductForm = "Plate";
            row208.AlloyUNS = "K12001";
            row208.ClassCondition = "";
            row208.Notes = "G19";
            row208.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row208);

            // Row 16: SA-812, 80, Sheet
            var row209 = new OldStressRowData();
            row209.LineNo = 16;
            row209.MaterialId = 8467;
            row209.SpecNo = "SA-812";
            row209.TypeGrade = "80";
            row209.ProductForm = "Sheet";
            row209.AlloyUNS = "";
            row209.ClassCondition = "";
            row209.Notes = "";
            row209.StressValues = new double?[] { 25, null, 25, null, 24.6, 24.6, 24.6, 24.6, 24.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row209);

            // Row 17: SA-737, C, Plate
            var row210 = new OldStressRowData();
            row210.LineNo = 17;
            row210.MaterialId = 8468;
            row210.SpecNo = "SA-737";
            row210.TypeGrade = "C";
            row210.ProductForm = "Plate";
            row210.AlloyUNS = "K12202";
            row210.ClassCondition = "";
            row210.Notes = "G20";
            row210.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row210);

            // Row 18: SA-562, , Plate, sheet
            var row211 = new OldStressRowData();
            row211.LineNo = 18;
            row211.MaterialId = 8469;
            row211.SpecNo = "SA-562";
            row211.TypeGrade = "";
            row211.ProductForm = "Plate, sheet";
            row211.AlloyUNS = "K11224";
            row211.ClassCondition = "";
            row211.Notes = "G30";
            row211.StressValues = new double?[] { 11.3, null, 10.9, null, 10.3, 9.5, 9.3, 9.3, 9.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row211);

            // Row 19: SA-836, , Forgings
            var row212 = new OldStressRowData();
            row212.LineNo = 19;
            row212.MaterialId = 8470;
            row212.SpecNo = "SA-836";
            row212.TypeGrade = "";
            row212.ProductForm = "Forgings";
            row212.AlloyUNS = "";
            row212.ClassCondition = "1";
            row212.Notes = "";
            row212.StressValues = new double?[] { 13.8, null, 13.5, null, 12.7, 12.1, 11.7, 11.3, 11.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row212);

            // Row 20: SA-209, T1b, Smls. tube
            var row213 = new OldStressRowData();
            row213.LineNo = 20;
            row213.MaterialId = 8472;
            row213.SpecNo = "SA-209";
            row213.TypeGrade = "T1b";
            row213.ProductForm = "Smls. tube";
            row213.AlloyUNS = "K11422";
            row213.ClassCondition = "";
            row213.Notes = "G11, S3";
            row213.StressValues = new double?[] { 13.3, null, 13.3, null, 13.3, 13.3, 13.3, 13.3, 13.3, 13.2, 12.9, 12.6, 12.3, 11.9, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row213);

            // Row 21: SA-209, T1b, Smls. tube
            var row214 = new OldStressRowData();
            row214.LineNo = 21;
            row214.MaterialId = 8471;
            row214.SpecNo = "SA-209";
            row214.TypeGrade = "T1b";
            row214.ProductForm = "Smls. tube";
            row214.AlloyUNS = "K11422";
            row214.ClassCondition = "";
            row214.Notes = "G11";
            row214.StressValues = new double?[] { 13.3, 13.3, 13.3, null, 13.3, 13.3, 13.3, 13.3, 13.3, 13.3, 12.9, 12.6, 12.3, 11.9, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row214);

            // Row 22: SA-250, T1b, Wld. tube
            var row215 = new OldStressRowData();
            row215.LineNo = 22;
            row215.MaterialId = 8473;
            row215.SpecNo = "SA-250";
            row215.TypeGrade = "T1b";
            row215.ProductForm = "Wld. tube";
            row215.AlloyUNS = "K11422";
            row215.ClassCondition = "";
            row215.Notes = "G11, S2, W14";
            row215.StressValues = new double?[] { 13.3, null, 13.3, null, 13.3, 13.3, 13.3, 13.3, 13.3, 13.2, 12.9, 12.6, 12.3, 11.9, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row215);

            // Row 23: SA-250, T1b, Wld. tube
            var row216 = new OldStressRowData();
            row216.LineNo = 23;
            row216.MaterialId = 8474;
            row216.SpecNo = "SA-250";
            row216.TypeGrade = "T1b";
            row216.ProductForm = "Wld. tube";
            row216.AlloyUNS = "K11422";
            row216.ClassCondition = "";
            row216.Notes = "G3, G11, G24, S2";
            row216.StressValues = new double?[] { 11.3, 11.3, 11.3, null, 11.3, 11.3, 11.3, 11.3, 11.3, 11.2, 11, 10.7, 10.4, 10.1, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row216);

            // Row 24: SA-209, T1, Smls. tube
            var row217 = new OldStressRowData();
            row217.LineNo = 24;
            row217.MaterialId = 8476;
            row217.SpecNo = "SA-209";
            row217.TypeGrade = "T1";
            row217.ProductForm = "Smls. tube";
            row217.AlloyUNS = "K11522";
            row217.ClassCondition = "";
            row217.Notes = "G11, S3";
            row217.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.1, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row217);

            // Row 25: SA-209, T1, Smls. tube
            var row218 = new OldStressRowData();
            row218.LineNo = 25;
            row218.MaterialId = 8475;
            row218.SpecNo = "SA-209";
            row218.TypeGrade = "T1";
            row218.ProductForm = "Smls. tube";
            row218.AlloyUNS = "K11522";
            row218.ClassCondition = "";
            row218.Notes = "G11";
            row218.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.2, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row218);

            // Row 26: ..., , 
            var row219 = new OldStressRowData();
            row219.LineNo = 26;
            row219.MaterialId = 8477;
            row219.SpecNo = "...";
            row219.TypeGrade = "";
            row219.ProductForm = "";
            row219.AlloyUNS = "";
            row219.ClassCondition = "";
            row219.Notes = "";
            row219.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row219);

            // Row 27: SA-234, WP1, Smls. & wld. fittings
            var row220 = new OldStressRowData();
            row220.LineNo = 27;
            row220.MaterialId = 8478;
            row220.SpecNo = "SA-234";
            row220.TypeGrade = "WP1";
            row220.ProductForm = "Smls. & wld. fittings";
            row220.AlloyUNS = "K12821";
            row220.ClassCondition = "";
            row220.Notes = "G11, G18";
            row220.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.2, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row220);

            // Row 28: ..., , 
            var row221 = new OldStressRowData();
            row221.LineNo = 28;
            row221.MaterialId = 8479;
            row221.SpecNo = "...";
            row221.TypeGrade = "";
            row221.ProductForm = "";
            row221.AlloyUNS = "";
            row221.ClassCondition = "";
            row221.Notes = "";
            row221.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row221);

            // Row 29: SA-250, T1, Wld. tube
            var row222 = new OldStressRowData();
            row222.LineNo = 29;
            row222.MaterialId = 8481;
            row222.SpecNo = "SA-250";
            row222.TypeGrade = "T1";
            row222.ProductForm = "Wld. tube";
            row222.AlloyUNS = "K11522";
            row222.ClassCondition = "";
            row222.Notes = "G11, S2, W14";
            row222.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.1, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row222);

            // Row 30: SA-250, T1, Wld. tube
            var row223 = new OldStressRowData();
            row223.LineNo = 30;
            row223.MaterialId = 8482;
            row223.SpecNo = "SA-250";
            row223.TypeGrade = "T1";
            row223.ProductForm = "Wld. tube";
            row223.AlloyUNS = "K11522";
            row223.ClassCondition = "";
            row223.Notes = "G3, G11, S2";
            row223.StressValues = new double?[] { 11.7, null, 11.7, null, 11.7, 11.7, 11.7, 11.7, 11.7, 11.7, 11.7, 11.5, 11.1, 10.8, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row223);

            // Row 31: SA-250, T1, Wld. tube
            var row224 = new OldStressRowData();
            row224.LineNo = 31;
            row224.MaterialId = 8480;
            row224.SpecNo = "SA-250";
            row224.TypeGrade = "T1";
            row224.ProductForm = "Wld. tube";
            row224.AlloyUNS = "K11522";
            row224.ClassCondition = "";
            row224.Notes = "G24";
            row224.StressValues = new double?[] { 11.7, 11.7, 11.7, null, 11.7, 11.7, 11.7, 11.7, 11.7, 11.7, 11.7, 11.5, 11.2, 10.8, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row224);

            // Row 32: SA-335, P1, Smls. pipe
            var row225 = new OldStressRowData();
            row225.LineNo = 32;
            row225.MaterialId = 8483;
            row225.SpecNo = "SA-335";
            row225.TypeGrade = "P1";
            row225.ProductForm = "Smls. pipe";
            row225.AlloyUNS = "K11522";
            row225.ClassCondition = "";
            row225.Notes = "G11, S2";
            row225.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.1, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row225);

            // Row 33: SA-335, P1, Smls. pipe
            var row226 = new OldStressRowData();
            row226.LineNo = 33;
            row226.MaterialId = 8484;
            row226.SpecNo = "SA-335";
            row226.TypeGrade = "P1";
            row226.ProductForm = "Smls. pipe";
            row226.AlloyUNS = "K11522";
            row226.ClassCondition = "";
            row226.Notes = "G11";
            row226.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.2, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row226);

            // Row 34: SA-369, FP1, Forged pipe
            var row227 = new OldStressRowData();
            row227.LineNo = 34;
            row227.MaterialId = 8485;
            row227.SpecNo = "SA-369";
            row227.TypeGrade = "FP1";
            row227.ProductForm = "Forged pipe";
            row227.AlloyUNS = "K11522";
            row227.ClassCondition = "";
            row227.Notes = "G11, S2";
            row227.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.1, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row227);

            // Row 35: SA-369, FP1, Forged pipe
            var row228 = new OldStressRowData();
            row228.LineNo = 35;
            row228.MaterialId = 8486;
            row228.SpecNo = "SA-369";
            row228.TypeGrade = "FP1";
            row228.ProductForm = "Forged pipe";
            row228.AlloyUNS = "K11522";
            row228.ClassCondition = "";
            row228.Notes = "G11";
            row228.StressValues = new double?[] { 13.8, 13.8, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.5, 13.2, 12.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row228);

            // Row 36: SA-209, T1a, Smls. tube
            var row229 = new OldStressRowData();
            row229.LineNo = 36;
            row229.MaterialId = 8487;
            row229.SpecNo = "SA-209";
            row229.TypeGrade = "T1a";
            row229.ProductForm = "Smls. tube";
            row229.AlloyUNS = "K12023";
            row229.ClassCondition = "";
            row229.Notes = "G11, S3";
            row229.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row229);

            // Row 37: SA-250, T1a, Wld. tube
            var row230 = new OldStressRowData();
            row230.LineNo = 37;
            row230.MaterialId = 8488;
            row230.SpecNo = "SA-250";
            row230.TypeGrade = "T1a";
            row230.ProductForm = "Wld. tube";
            row230.AlloyUNS = "K12023";
            row230.ClassCondition = "";
            row230.Notes = "G11, S2, W14";
            row230.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.6, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row230);

            // Row 38: SA-250, T1a, Wld. tube
            var row231 = new OldStressRowData();
            row231.LineNo = 38;
            row231.MaterialId = 8489;
            row231.SpecNo = "SA-250";
            row231.TypeGrade = "T1a";
            row231.ProductForm = "Wld. tube";
            row231.AlloyUNS = "K12023";
            row231.ClassCondition = "";
            row231.Notes = "G3, G11, G24, S2";
            row231.StressValues = new double?[] { 12.8, null, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.8, 12.6, 12.2, 11.9, 11.6, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row231);

            // Row 1: SA-217, WC1, Castings
            var row232 = new OldStressRowData();
            row232.LineNo = 1;
            row232.MaterialId = 8490;
            row232.SpecNo = "SA-217";
            row232.TypeGrade = "WC1";
            row232.ProductForm = "Castings";
            row232.AlloyUNS = "J12524";
            row232.ClassCondition = "";
            row232.Notes = "G1, G11, G17, G18, S2";
            row232.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 16.2, 15.8, 15.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row232);

            // Row 2: ..., , 
            var row233 = new OldStressRowData();
            row233.LineNo = 2;
            row233.MaterialId = 8491;
            row233.SpecNo = "...";
            row233.TypeGrade = "";
            row233.ProductForm = "";
            row233.AlloyUNS = "";
            row233.ClassCondition = "";
            row233.Notes = "";
            row233.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row233);

            // Row 3: SA-352, LC1, Castings
            var row234 = new OldStressRowData();
            row234.LineNo = 3;
            row234.MaterialId = 8492;
            row234.SpecNo = "SA-352";
            row234.TypeGrade = "LC1";
            row234.ProductForm = "Castings";
            row234.AlloyUNS = "J12522";
            row234.ClassCondition = "";
            row234.Notes = "G1, G17";
            row234.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row234);

            // Row 4: ..., , 
            var row235 = new OldStressRowData();
            row235.LineNo = 4;
            row235.MaterialId = 8493;
            row235.SpecNo = "...";
            row235.TypeGrade = "";
            row235.ProductForm = "";
            row235.AlloyUNS = "";
            row235.ClassCondition = "";
            row235.Notes = "";
            row235.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row235);

            // Row 5: SA-426, CP1, Cast pipe
            var row236 = new OldStressRowData();
            row236.LineNo = 5;
            row236.MaterialId = 8494;
            row236.SpecNo = "SA-426";
            row236.TypeGrade = "CP1";
            row236.ProductForm = "Cast pipe";
            row236.AlloyUNS = "J12521";
            row236.ClassCondition = "";
            row236.Notes = "G17";
            row236.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row236);

            // Row 6: SA-204, A, Plate
            var row237 = new OldStressRowData();
            row237.LineNo = 6;
            row237.MaterialId = 8495;
            row237.SpecNo = "SA-204";
            row237.TypeGrade = "A";
            row237.ProductForm = "Plate";
            row237.AlloyUNS = "K11820";
            row237.ClassCondition = "";
            row237.Notes = "G11, S2";
            row237.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 16.2, 15.8, 15.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row237);

            // Row 7: SA-204, A, Plate
            var row238 = new OldStressRowData();
            row238.LineNo = 7;
            row238.MaterialId = 8496;
            row238.SpecNo = "SA-204";
            row238.TypeGrade = "A";
            row238.ProductForm = "Plate";
            row238.AlloyUNS = "K11820";
            row238.ClassCondition = "";
            row238.Notes = "G11";
            row238.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 15.8, 15.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row238);

            // Row 8: SA-672, L65, Wld. pipe
            var row239 = new OldStressRowData();
            row239.LineNo = 8;
            row239.MaterialId = 8497;
            row239.SpecNo = "SA-672";
            row239.TypeGrade = "L65";
            row239.ProductForm = "Wld. pipe";
            row239.AlloyUNS = "K11820";
            row239.ClassCondition = "";
            row239.Notes = "G26, W10, W12";
            row239.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row239);

            // Row 9: SA-691, CM-65, Wld. pipe
            var row240 = new OldStressRowData();
            row240.LineNo = 9;
            row240.MaterialId = 8498;
            row240.SpecNo = "SA-691";
            row240.TypeGrade = "CM-65";
            row240.ProductForm = "Wld. pipe";
            row240.AlloyUNS = "K11820";
            row240.ClassCondition = "";
            row240.Notes = "G26, W10, W12";
            row240.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row240);

            // Row 10: SA-691, CM-65, Wld. pipe
            var row241 = new OldStressRowData();
            row241.LineNo = 10;
            row241.MaterialId = 8499;
            row241.SpecNo = "SA-691";
            row241.TypeGrade = "CM-65";
            row241.ProductForm = "Wld. pipe";
            row241.AlloyUNS = "K11820";
            row241.ClassCondition = "";
            row241.Notes = "G27, W10, W12";
            row241.StressValues = new double?[] { 16.2, null, 16.2, null, 16.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row241);

            // Row 11: SA-182, F1, Forgings
            var row242 = new OldStressRowData();
            row242.LineNo = 11;
            row242.MaterialId = 8500;
            row242.SpecNo = "SA-182";
            row242.TypeGrade = "F1";
            row242.ProductForm = "Forgings";
            row242.AlloyUNS = "K12822";
            row242.ClassCondition = "";
            row242.Notes = "G11, G18, S2";
            row242.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row242);

            // Row 12: SA-204, B, Plate
            var row243 = new OldStressRowData();
            row243.LineNo = 12;
            row243.MaterialId = 8501;
            row243.SpecNo = "SA-204";
            row243.TypeGrade = "B";
            row243.ProductForm = "Plate";
            row243.AlloyUNS = "K12020";
            row243.ClassCondition = "";
            row243.Notes = "G11, S2";
            row243.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row243);

            // Row 13: SA-336, F1, Forgings
            var row244 = new OldStressRowData();
            row244.LineNo = 13;
            row244.MaterialId = 8502;
            row244.SpecNo = "SA-336";
            row244.TypeGrade = "F1";
            row244.ProductForm = "Forgings";
            row244.AlloyUNS = "K12520";
            row244.ClassCondition = "";
            row244.Notes = "G11, G18, S2";
            row244.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row244);

            // Row 14: SA-672, L70, Wld. pipe
            var row245 = new OldStressRowData();
            row245.LineNo = 14;
            row245.MaterialId = 8503;
            row245.SpecNo = "SA-672";
            row245.TypeGrade = "L70";
            row245.ProductForm = "Wld. pipe";
            row245.AlloyUNS = "K12020";
            row245.ClassCondition = "";
            row245.Notes = "G26, W10, W12";
            row245.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row245);

            // Row 15: SA-691, CM-70, Wld. pipe
            var row246 = new OldStressRowData();
            row246.LineNo = 15;
            row246.MaterialId = 8504;
            row246.SpecNo = "SA-691";
            row246.TypeGrade = "CM-70";
            row246.ProductForm = "Wld. pipe";
            row246.AlloyUNS = "K12020";
            row246.ClassCondition = "";
            row246.Notes = "G26, W10, W12";
            row246.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row246);

            // Row 16: SA-691, CM-70, Wld. pipe
            var row247 = new OldStressRowData();
            row247.LineNo = 16;
            row247.MaterialId = 8505;
            row247.SpecNo = "SA-691";
            row247.TypeGrade = "CM-70";
            row247.ProductForm = "Wld. pipe";
            row247.AlloyUNS = "K12020";
            row247.ClassCondition = "";
            row247.Notes = "G27, W10, W12";
            row247.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row247);

            // Row 17: SA-204, C, Plate
            var row248 = new OldStressRowData();
            row248.LineNo = 17;
            row248.MaterialId = 8506;
            row248.SpecNo = "SA-204";
            row248.TypeGrade = "C";
            row248.ProductForm = "Plate";
            row248.AlloyUNS = "K12320";
            row248.ClassCondition = "";
            row248.Notes = "G11, S2, W12";
            row248.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, 18.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row248);

            // Row 18: SA-672, L75, Wld. pipe
            var row249 = new OldStressRowData();
            row249.LineNo = 18;
            row249.MaterialId = 8507;
            row249.SpecNo = "SA-672";
            row249.TypeGrade = "L75";
            row249.ProductForm = "Wld. pipe";
            row249.AlloyUNS = "K12320";
            row249.ClassCondition = "";
            row249.Notes = "G26, W10, W12";
            row249.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row249);

            // Row 19: SA-691, CM-75, Wld. pipe
            var row250 = new OldStressRowData();
            row250.LineNo = 19;
            row250.MaterialId = 8508;
            row250.SpecNo = "SA-691";
            row250.TypeGrade = "CM-75";
            row250.ProductForm = "Wld. pipe";
            row250.AlloyUNS = "K12320";
            row250.ClassCondition = "";
            row250.Notes = "G26, W10, W12";
            row250.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 18.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row250);

            // Row 20: SA-691, CM-75, Wld. pipe
            var row251 = new OldStressRowData();
            row251.LineNo = 20;
            row251.MaterialId = 8509;
            row251.SpecNo = "SA-691";
            row251.TypeGrade = "CM-75";
            row251.ProductForm = "Wld. pipe";
            row251.AlloyUNS = "K12320";
            row251.ClassCondition = "";
            row251.Notes = "G27, W10, W12";
            row251.StressValues = new double?[] { 18.7, null, 18.7, null, 18.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row251);

            // Row 21: SA-517, J, Plate
            var row252 = new OldStressRowData();
            row252.LineNo = 21;
            row252.MaterialId = 8510;
            row252.SpecNo = "SA-517";
            row252.TypeGrade = "J";
            row252.ProductForm = "Plate";
            row252.AlloyUNS = "K11625";
            row252.ClassCondition = "";
            row252.Notes = "";
            row252.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row252);

            // Row 22: SA-517, B, Plate
            var row253 = new OldStressRowData();
            row253.LineNo = 22;
            row253.MaterialId = 8511;
            row253.SpecNo = "SA-517";
            row253.TypeGrade = "B";
            row253.ProductForm = "Plate";
            row253.AlloyUNS = "K11630";
            row253.ClassCondition = "";
            row253.Notes = "";
            row253.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row253);

            // Row 23: SA-517, A, Plate
            var row254 = new OldStressRowData();
            row254.LineNo = 23;
            row254.MaterialId = 8513;
            row254.SpecNo = "SA-517";
            row254.TypeGrade = "A";
            row254.ProductForm = "Plate";
            row254.AlloyUNS = "K11856";
            row254.ClassCondition = "";
            row254.Notes = "";
            row254.StressValues = new double?[] { 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row254);

            // Row 24: SA-592, A, Forgings
            var row255 = new OldStressRowData();
            row255.LineNo = 24;
            row255.MaterialId = 8512;
            row255.SpecNo = "SA-592";
            row255.TypeGrade = "A";
            row255.ProductForm = "Forgings";
            row255.AlloyUNS = "K11856";
            row255.ClassCondition = "";
            row255.Notes = "";
            row255.StressValues = new double?[] { 26.3, null, 26.3, null, 26.3, 26.3, 26.3, 26.3, 26.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row255);

            // Row 25: SA-592, A, Forgings
            var row256 = new OldStressRowData();
            row256.LineNo = 25;
            row256.MaterialId = 8514;
            row256.SpecNo = "SA-592";
            row256.TypeGrade = "A";
            row256.ProductForm = "Forgings";
            row256.AlloyUNS = "K11856";
            row256.ClassCondition = "";
            row256.Notes = "";
            row256.StressValues = new double?[] { 28.8, null, 28.8, null, 28.8, 28.8, 28.8, 28.8, 28.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row256);

            // Row 26: SA-335, P2, Smls. pipe
            var row257 = new OldStressRowData();
            row257.LineNo = 26;
            row257.MaterialId = 8515;
            row257.SpecNo = "SA-335";
            row257.TypeGrade = "P2";
            row257.ProductForm = "Smls. pipe";
            row257.AlloyUNS = "K11547";
            row257.ClassCondition = "";
            row257.Notes = "";
            row257.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row257);

            // Row 27: SA-369, FP2, Forged pipe
            var row258 = new OldStressRowData();
            row258.LineNo = 27;
            row258.MaterialId = 8516;
            row258.SpecNo = "SA-369";
            row258.TypeGrade = "FP2";
            row258.ProductForm = "Forged pipe";
            row258.AlloyUNS = "K11547";
            row258.ClassCondition = "";
            row258.Notes = "";
            row258.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row258);

            // Row 28: SA-387, 2, Plate
            var row259 = new OldStressRowData();
            row259.LineNo = 28;
            row259.MaterialId = 8517;
            row259.SpecNo = "SA-387";
            row259.TypeGrade = "2";
            row259.ProductForm = "Plate";
            row259.AlloyUNS = "K12143";
            row259.ClassCondition = "1";
            row259.Notes = "";
            row259.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.3, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row259);

            // Row 29: SA-387, 2, Plate
            var row260 = new OldStressRowData();
            row260.LineNo = 29;
            row260.MaterialId = 8518;
            row260.SpecNo = "SA-387";
            row260.TypeGrade = "2";
            row260.ProductForm = "Plate";
            row260.AlloyUNS = "K12143";
            row260.ClassCondition = "1";
            row260.Notes = "";
            row260.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.4, 12.8, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row260);

            // Row 30: SA-691, 1/2 CR, Wld. pipe
            var row261 = new OldStressRowData();
            row261.LineNo = 30;
            row261.MaterialId = 8519;
            row261.SpecNo = "SA-691";
            row261.TypeGrade = "1/2 CR";
            row261.ProductForm = "Wld. pipe";
            row261.AlloyUNS = "K12143";
            row261.ClassCondition = "";
            row261.Notes = "G26, W10, W12";
            row261.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row261);

            // Row 31: SA-691, 1/2 CR, Wld. pipe
            var row262 = new OldStressRowData();
            row262.LineNo = 31;
            row262.MaterialId = 8520;
            row262.SpecNo = "SA-691";
            row262.TypeGrade = "1/2 CR";
            row262.ProductForm = "Wld. pipe";
            row262.AlloyUNS = "K12143";
            row262.ClassCondition = "";
            row262.Notes = "G27, W10, W12";
            row262.StressValues = new double?[] { 13.7, null, 13.7, null, 13.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row262);

            // Row 32: SA-213, T2, Smls. tube
            var row263 = new OldStressRowData();
            row263.LineNo = 32;
            row263.MaterialId = 8521;
            row263.SpecNo = "SA-213";
            row263.TypeGrade = "T2";
            row263.ProductForm = "Smls. tube";
            row263.AlloyUNS = "K11547";
            row263.ClassCondition = "";
            row263.Notes = "";
            row263.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 14.8, 14.4, 14, 13.7, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row263);

            // Row 33: SA-426, CP2, Cast pipe
            var row264 = new OldStressRowData();
            row264.LineNo = 33;
            row264.MaterialId = 8522;
            row264.SpecNo = "SA-426";
            row264.TypeGrade = "CP2";
            row264.ProductForm = "Cast pipe";
            row264.AlloyUNS = "J11547";
            row264.ClassCondition = "";
            row264.Notes = "G17";
            row264.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row264);

            // Row 34: SA-182, F2, Forgings
            var row265 = new OldStressRowData();
            row265.LineNo = 34;
            row265.MaterialId = 8523;
            row265.SpecNo = "SA-182";
            row265.TypeGrade = "F2";
            row265.ProductForm = "Forgings";
            row265.AlloyUNS = "K12122";
            row265.ClassCondition = "";
            row265.Notes = "G18";
            row265.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 16.9, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row265);

            // Row 35: SA-387, 2, Plate
            var row266 = new OldStressRowData();
            row266.LineNo = 35;
            row266.MaterialId = 8524;
            row266.SpecNo = "SA-387";
            row266.TypeGrade = "2";
            row266.ProductForm = "Plate";
            row266.AlloyUNS = "K12143";
            row266.ClassCondition = "2";
            row266.Notes = "";
            row266.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 16.9, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row266);

            // Row 36: SA-691, 1/2 CR, Wld. pipe
            var row267 = new OldStressRowData();
            row267.LineNo = 36;
            row267.MaterialId = 8525;
            row267.SpecNo = "SA-691";
            row267.TypeGrade = "1/2 CR";
            row267.ProductForm = "Wld. pipe";
            row267.AlloyUNS = "K12143";
            row267.ClassCondition = "";
            row267.Notes = "G26, W10, W12";
            row267.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row267);

            // Row 37: SA-202, A, Plate
            var row268 = new OldStressRowData();
            row268.LineNo = 37;
            row268.MaterialId = 8526;
            row268.SpecNo = "SA-202";
            row268.TypeGrade = "A";
            row268.ProductForm = "Plate";
            row268.AlloyUNS = "K11742";
            row268.ClassCondition = "";
            row268.Notes = "S1";
            row268.StressValues = new double?[] { 18.8, null, 18.8, null, 18.8, 18.8, 18.8, 18.8, 18.8, 17.7, 15.7, 12, 7.8, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row268);

            // Row 38: SA-202, B, Plate
            var row269 = new OldStressRowData();
            row269.LineNo = 38;
            row269.MaterialId = 8527;
            row269.SpecNo = "SA-202";
            row269.TypeGrade = "B";
            row269.ProductForm = "Plate";
            row269.AlloyUNS = "K12542";
            row269.ClassCondition = "";
            row269.Notes = "S1";
            row269.StressValues = new double?[] { 21.3, 21.3, 21.3, null, 21.3, 21.3, 21.3, 21.3, 21.3, 19.8, 17.7, 12, 7.8, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row269);

            // Row 1: SA-423, 1, Wld. tube
            var row270 = new OldStressRowData();
            row270.LineNo = 1;
            row270.MaterialId = 8529;
            row270.SpecNo = "SA-423";
            row270.TypeGrade = "1";
            row270.ProductForm = "Wld. tube";
            row270.AlloyUNS = "K11535";
            row270.ClassCondition = "";
            row270.Notes = "W14";
            row270.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row270);

            // Row 2: SA-423, 1, Wld. tube
            var row271 = new OldStressRowData();
            row271.LineNo = 2;
            row271.MaterialId = 8530;
            row271.SpecNo = "SA-423";
            row271.TypeGrade = "1";
            row271.ProductForm = "Wld. tube";
            row271.AlloyUNS = "K11535";
            row271.ClassCondition = "";
            row271.Notes = "G3, G24";
            row271.StressValues = new double?[] { 12.8, null, 12.8, null, 12.8, 12.8, 12.8, 12.8, 12.8, 12.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row271);

            // Row 3: SA-423, 1, Smls. tube
            var row272 = new OldStressRowData();
            row272.LineNo = 3;
            row272.MaterialId = 8528;
            row272.SpecNo = "SA-423";
            row272.TypeGrade = "1";
            row272.ProductForm = "Smls. tube";
            row272.AlloyUNS = "K11535";
            row272.ClassCondition = "";
            row272.Notes = "";
            row272.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row272);

            // Row 4: SA-333, 4, Pipe
            var row273 = new OldStressRowData();
            row273.LineNo = 4;
            row273.MaterialId = 8531;
            row273.SpecNo = "SA-333";
            row273.TypeGrade = "4";
            row273.ProductForm = "Pipe";
            row273.AlloyUNS = "K11267";
            row273.ClassCondition = "";
            row273.Notes = "";
            row273.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row273);

            // Row 5: SA-372, E, Forgings
            var row274 = new OldStressRowData();
            row274.LineNo = 5;
            row274.MaterialId = 8533;
            row274.SpecNo = "SA-372";
            row274.TypeGrade = "E";
            row274.ProductForm = "Forgings";
            row274.AlloyUNS = "K13047";
            row274.ClassCondition = "70";
            row274.Notes = "W2, W11";
            row274.StressValues = new double?[] { 30, 30, 30, null, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row274);

            // Row 6: SA-372, F, Forgings
            var row275 = new OldStressRowData();
            row275.LineNo = 6;
            row275.MaterialId = 8534;
            row275.SpecNo = "SA-372";
            row275.TypeGrade = "F";
            row275.ProductForm = "Forgings";
            row275.AlloyUNS = "G41350";
            row275.ClassCondition = "70";
            row275.Notes = "W2, W11";
            row275.StressValues = new double?[] { 30, null, 29.1, null, 28.5, 28.3, 28.2, 27.8, 26.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row275);

            // Row 7: SA-372, G, Forgings
            var row276 = new OldStressRowData();
            row276.LineNo = 7;
            row276.MaterialId = 8535;
            row276.SpecNo = "SA-372";
            row276.TypeGrade = "G";
            row276.ProductForm = "Forgings";
            row276.AlloyUNS = "K13049";
            row276.ClassCondition = "70";
            row276.Notes = "W2, W11";
            row276.StressValues = new double?[] { 30, null, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row276);

            // Row 8: SA-372, H, Forgings
            var row277 = new OldStressRowData();
            row277.LineNo = 8;
            row277.MaterialId = 8536;
            row277.SpecNo = "SA-372";
            row277.TypeGrade = "H";
            row277.ProductForm = "Forgings";
            row277.AlloyUNS = "K13547";
            row277.ClassCondition = "70";
            row277.Notes = "W2, W11";
            row277.StressValues = new double?[] { 30, null, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row277);

            // Row 9: SA-372, J, Forgings
            var row278 = new OldStressRowData();
            row278.LineNo = 9;
            row278.MaterialId = 8537;
            row278.SpecNo = "SA-372";
            row278.TypeGrade = "J";
            row278.ProductForm = "Forgings";
            row278.AlloyUNS = "K13548";
            row278.ClassCondition = "70";
            row278.Notes = "W2, W11";
            row278.StressValues = new double?[] { 30, null, 30, null, 30, 30, 30, 30, 29.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row278);

            // Row 10: SA-372, J, Forgings
            var row279 = new OldStressRowData();
            row279.LineNo = 10;
            row279.MaterialId = 8538;
            row279.SpecNo = "SA-372";
            row279.TypeGrade = "J";
            row279.ProductForm = "Forgings";
            row279.AlloyUNS = "G41370";
            row279.ClassCondition = "110";
            row279.Notes = "W2, W11";
            row279.StressValues = new double?[] { 33.7, null, 32.3, null, 32.1, 31.9, 31.6, 31.4, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row279);

            // Row 11: SA-387, 12, Plate
            var row280 = new OldStressRowData();
            row280.LineNo = 11;
            row280.MaterialId = 8539;
            row280.SpecNo = "SA-387";
            row280.TypeGrade = "12";
            row280.ProductForm = "Plate";
            row280.AlloyUNS = "K11757";
            row280.ClassCondition = "1";
            row280.Notes = "S4";
            row280.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, 13.4, 12.9, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row280);

            // Row 12: SA-691, 1CR, Wld. pipe
            var row281 = new OldStressRowData();
            row281.LineNo = 12;
            row281.MaterialId = 8540;
            row281.SpecNo = "SA-691";
            row281.TypeGrade = "1CR";
            row281.ProductForm = "Wld. pipe";
            row281.AlloyUNS = "K11757";
            row281.ClassCondition = "";
            row281.Notes = "G26, W10, W12";
            row281.StressValues = new double?[] { 13.8, null, 13.8, null, 13.8, 13.8, 13.8, 13.8, 13.8, 13.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row281);

            // Row 13: SA-182, F12, Forgings
            var row282 = new OldStressRowData();
            row282.LineNo = 13;
            row282.MaterialId = 8541;
            row282.SpecNo = "SA-182";
            row282.TypeGrade = "F12";
            row282.ProductForm = "Forgings";
            row282.AlloyUNS = "K11562";
            row282.ClassCondition = "1";
            row282.Notes = "";
            row282.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, 14.8, 14.6, 14.3, 14, 13.6, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row282);

            // Row 14: SA-426, CP12, Cast pipe
            var row283 = new OldStressRowData();
            row283.LineNo = 14;
            row283.MaterialId = 8542;
            row283.SpecNo = "SA-426";
            row283.TypeGrade = "CP12";
            row283.ProductForm = "Cast pipe";
            row283.AlloyUNS = "J11562";
            row283.ClassCondition = "";
            row283.Notes = "G17";
            row283.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row283);

            // Row 15: SA-213, T12, Smls. tube
            var row284 = new OldStressRowData();
            row284.LineNo = 15;
            row284.MaterialId = 8543;
            row284.SpecNo = "SA-213";
            row284.TypeGrade = "T12";
            row284.ProductForm = "Smls. tube";
            row284.AlloyUNS = "K11562";
            row284.ClassCondition = "";
            row284.Notes = "S4";
            row284.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.6, 14, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row284);

            // Row 16: SA-213, T12, Smls. tube
            var row285 = new OldStressRowData();
            row285.LineNo = 16;
            row285.MaterialId = 8544;
            row285.SpecNo = "SA-213";
            row285.TypeGrade = "T12";
            row285.ProductForm = "Smls. tube";
            row285.AlloyUNS = "K11562";
            row285.ClassCondition = "";
            row285.Notes = "";
            row285.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row285);

            // Row 17: ..., , 
            var row286 = new OldStressRowData();
            row286.LineNo = 17;
            row286.MaterialId = 8545;
            row286.SpecNo = "...";
            row286.TypeGrade = "";
            row286.ProductForm = "";
            row286.AlloyUNS = "";
            row286.ClassCondition = "";
            row286.Notes = "";
            row286.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row286);

            // Row 18: ..., , 
            var row287 = new OldStressRowData();
            row287.LineNo = 18;
            row287.MaterialId = 8546;
            row287.SpecNo = "...";
            row287.TypeGrade = "";
            row287.ProductForm = "";
            row287.AlloyUNS = "";
            row287.ClassCondition = "";
            row287.Notes = "";
            row287.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row287);

            // Row 19: SA-234, WP12, Smls. & wld. fittings
            var row288 = new OldStressRowData();
            row288.LineNo = 19;
            row288.MaterialId = 8547;
            row288.SpecNo = "SA-234";
            row288.TypeGrade = "WP12";
            row288.ProductForm = "Smls. & wld. fittings";
            row288.AlloyUNS = "K12062";
            row288.ClassCondition = "1";
            row288.Notes = "G18, S4";
            row288.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.6, 14, 11.3, 7.2, 4.5, 2.3, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row288);

            // Row 20: ..., , 
            var row289 = new OldStressRowData();
            row289.LineNo = 20;
            row289.MaterialId = 8548;
            row289.SpecNo = "...";
            row289.TypeGrade = "";
            row289.ProductForm = "";
            row289.AlloyUNS = "";
            row289.ClassCondition = "";
            row289.Notes = "";
            row289.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row289);

            // Row 21: SA-335, P12, Smls. pipe
            var row290 = new OldStressRowData();
            row290.LineNo = 21;
            row290.MaterialId = 8549;
            row290.SpecNo = "SA-335";
            row290.TypeGrade = "P12";
            row290.ProductForm = "Smls. pipe";
            row290.AlloyUNS = "K11562";
            row290.ClassCondition = "";
            row290.Notes = "S4";
            row290.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.6, 14, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row290);

            // Row 22: SA-335, P12, Smls. pipe
            var row291 = new OldStressRowData();
            row291.LineNo = 22;
            row291.MaterialId = 8550;
            row291.SpecNo = "SA-335";
            row291.TypeGrade = "P12";
            row291.ProductForm = "Smls. pipe";
            row291.AlloyUNS = "K11562";
            row291.ClassCondition = "";
            row291.Notes = "";
            row291.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row291);

            // Row 23: SA-369, FP12, Forged pipe
            var row292 = new OldStressRowData();
            row292.LineNo = 23;
            row292.MaterialId = 8551;
            row292.SpecNo = "SA-369";
            row292.TypeGrade = "FP12";
            row292.ProductForm = "Forged pipe";
            row292.AlloyUNS = "K11562";
            row292.ClassCondition = "";
            row292.Notes = "S4";
            row292.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 15, 15, 15, 15, 15, 14.6, 14, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row292);

            // Row 24: SA-369, FP12, Forged pipe
            var row293 = new OldStressRowData();
            row293.LineNo = 24;
            row293.MaterialId = 8552;
            row293.SpecNo = "SA-369";
            row293.TypeGrade = "FP12";
            row293.ProductForm = "Forged pipe";
            row293.AlloyUNS = "K11562";
            row293.ClassCondition = "";
            row293.Notes = "";
            row293.StressValues = new double?[] { 15, null, 15, null, 15, 15, 15, 14.7, 14.4, 14.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row293);

            // Row 25: SA-387, 12, Plate
            var row294 = new OldStressRowData();
            row294.LineNo = 25;
            row294.MaterialId = 8553;
            row294.SpecNo = "SA-387";
            row294.TypeGrade = "12";
            row294.ProductForm = "Plate";
            row294.AlloyUNS = "K11757";
            row294.ClassCondition = "2";
            row294.Notes = "S4";
            row294.StressValues = new double?[] { 16.3, 16.3, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, 15.8, 15.2, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row294);

            // Row 26: SA-691, 1CR, Wld. pipe
            var row295 = new OldStressRowData();
            row295.LineNo = 26;
            row295.MaterialId = 8554;
            row295.SpecNo = "SA-691";
            row295.TypeGrade = "1CR";
            row295.ProductForm = "Wld. pipe";
            row295.AlloyUNS = "K11757";
            row295.ClassCondition = "";
            row295.Notes = "G26, W10, W12";
            row295.StressValues = new double?[] { 16.3, null, 16.3, null, 16.3, 16.3, 16.3, 16.3, 16.3, 16.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row295);

            // Row 27: SA-182, F12, Forgings
            var row296 = new OldStressRowData();
            row296.LineNo = 27;
            row296.MaterialId = 8555;
            row296.SpecNo = "SA-182";
            row296.TypeGrade = "F12";
            row296.ProductForm = "Forgings";
            row296.AlloyUNS = "K11564";
            row296.ClassCondition = "2";
            row296.Notes = "G18, S4";
            row296.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17, 16.4, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row296);

            // Row 28: SA-336, F12, Forgings
            var row297 = new OldStressRowData();
            row297.LineNo = 28;
            row297.MaterialId = 8556;
            row297.SpecNo = "SA-336";
            row297.TypeGrade = "F12";
            row297.ProductForm = "Forgings";
            row297.AlloyUNS = "K11564";
            row297.ClassCondition = "";
            row297.Notes = "G18, S4";
            row297.StressValues = new double?[] { 17.5, 17.5, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17, 16.4, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row297);

            // Row 29: SA-213, T17, Smls. tube
            var row298 = new OldStressRowData();
            row298.LineNo = 29;
            row298.MaterialId = 8532;
            row298.SpecNo = "SA-213";
            row298.TypeGrade = "T17";
            row298.ProductForm = "Smls. tube";
            row298.AlloyUNS = "K12047";
            row298.ClassCondition = "";
            row298.Notes = "";
            row298.StressValues = new double?[] { 15, 15, 15, null, 15, 15, 15, 15, 15, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row298);

            // Row 30: SA-217, WC6, Castings
            var row299 = new OldStressRowData();
            row299.LineNo = 30;
            row299.MaterialId = 8557;
            row299.SpecNo = "SA-217";
            row299.TypeGrade = "WC6";
            row299.ProductForm = "Castings";
            row299.AlloyUNS = "J12072";
            row299.ClassCondition = "";
            row299.Notes = "G1, G17, G18";
            row299.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, 17.1, 13.7, 9.3, 6.3, 4.2, 2.8, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row299);

            // Row 31: SA-217, WC6, Castings
            var row300 = new OldStressRowData();
            row300.LineNo = 31;
            row300.MaterialId = 8558;
            row300.SpecNo = "SA-217";
            row300.TypeGrade = "WC6";
            row300.ProductForm = "Castings";
            row300.AlloyUNS = "J12072";
            row300.ClassCondition = "";
            row300.Notes = "";
            row300.StressValues = new double?[] { 17.5, null, 17.5, null, 17.5, 17.5, 17.5, 17.5, 17.5, 17.5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row300);

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
