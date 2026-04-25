using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch003
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

            // Row 18: SA-737, C, Plate
            var row201 = new OldStressRowData();
            row201.LineNo = 18;
            row201.MaterialId = 8468;
            row201.SpecNo = "SA-737";
            row201.TypeGrade = "C";
            row201.ProductForm = "Plate";
            row201.AlloyUNS = "K12202";
            row201.ClassCondition = "";
            row201.Notes = "G20";
            row201.StressValues = new double?[] { 22.9, null, 22.9, null, 22.9, 22.9, 22.9, 22.9, 22.9, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row201);

            // Row 19: SA-562, , Plate, sheet
            var row202 = new OldStressRowData();
            row202.LineNo = 19;
            row202.MaterialId = 8469;
            row202.SpecNo = "SA-562";
            row202.TypeGrade = "";
            row202.ProductForm = "Plate, sheet";
            row202.AlloyUNS = "K11224";
            row202.ClassCondition = "";
            row202.Notes = "G30";
            row202.StressValues = new double?[] { 12.9, null, 11.5, null, 10.6, 10.4, 10.4, 10.4, 10.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row202);

            // Row 20: SA-836, , Forgings
            var row203 = new OldStressRowData();
            row203.LineNo = 20;
            row203.MaterialId = 8470;
            row203.SpecNo = "SA-836";
            row203.TypeGrade = "";
            row203.ProductForm = "Forgings";
            row203.AlloyUNS = "";
            row203.ClassCondition = "1";
            row203.Notes = "";
            row203.StressValues = new double?[] { 15.7, null, 14.3, null, 13.3, 13, 13, 12.9, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row203);

            // Row 21: SA-209, T1b, Smls. tube
            var row204 = new OldStressRowData();
            row204.LineNo = 21;
            row204.MaterialId = 8472;
            row204.SpecNo = "SA-209";
            row204.TypeGrade = "T1b";
            row204.ProductForm = "Smls. tube";
            row204.AlloyUNS = "K11422";
            row204.ClassCondition = "";
            row204.Notes = "G11, S3, T5";
            row204.StressValues = new double?[] { 15.1, 15.1, 15.1, null, 15.1, 15.1, 15.1, 15.1, 15, 14.7, 14.3, 14, 13.5, 13, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row204);

            // Row 22: SA-250, T1b, Wld. tube
            var row205 = new OldStressRowData();
            row205.LineNo = 22;
            row205.MaterialId = 8473;
            row205.SpecNo = "SA-250";
            row205.TypeGrade = "T1b";
            row205.ProductForm = "Wld. tube";
            row205.AlloyUNS = "K11422";
            row205.ClassCondition = "";
            row205.Notes = "G11, S2, T5, W13";
            row205.StressValues = new double?[] { 15.1, null, 15.1, null, 15.1, 15.1, 15.1, 15.1, 15, 14.7, 14.3, 14, 13.5, 13, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row205);

            // Row 23: SA-250, T1b, Wld. tube
            var row206 = new OldStressRowData();
            row206.LineNo = 23;
            row206.MaterialId = 8474;
            row206.SpecNo = "SA-250";
            row206.TypeGrade = "T1b";
            row206.ProductForm = "Wld. tube";
            row206.AlloyUNS = "K11422";
            row206.ClassCondition = "";
            row206.Notes = "G3, G11, G24, S2, T5";
            row206.StressValues = new double?[] { 12.9, 12.9, 12.9, null, 12.9, 12.9, 12.9, 12.9, 12.7, 12.5, 12.2, 11.9, 11.5, 11.1, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row206);

            // Row 24: SA-209, T1, Smls. tube
            var row207 = new OldStressRowData();
            row207.LineNo = 24;
            row207.MaterialId = 8476;
            row207.SpecNo = "SA-209";
            row207.TypeGrade = "T1";
            row207.ProductForm = "Smls. tube";
            row207.AlloyUNS = "K11522";
            row207.ClassCondition = "";
            row207.Notes = "G11, S3, T4";
            row207.StressValues = new double?[] { 15.7, 15.7, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row207);

            // Row 25: SA-234, WP1, Smls. & wld. fittings
            var row208 = new OldStressRowData();
            row208.LineNo = 25;
            row208.MaterialId = 8478;
            row208.SpecNo = "SA-234";
            row208.TypeGrade = "WP1";
            row208.ProductForm = "Smls. & wld. fittings";
            row208.AlloyUNS = "K12821";
            row208.ClassCondition = "";
            row208.Notes = "G11, G18, T4";
            row208.StressValues = new double?[] { 15.7, 15.7, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row208);

            // Row 26: SA-250, T1, Wld. tube
            var row209 = new OldStressRowData();
            row209.LineNo = 26;
            row209.MaterialId = 8481;
            row209.SpecNo = "SA-250";
            row209.TypeGrade = "T1";
            row209.ProductForm = "Wld. tube";
            row209.AlloyUNS = "K11522";
            row209.ClassCondition = "";
            row209.Notes = "G11, S2, T4, W13";
            row209.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row209);

            // Row 27: SA-250, T1, Wld. tube
            var row210 = new OldStressRowData();
            row210.LineNo = 27;
            row210.MaterialId = 8482;
            row210.SpecNo = "SA-250";
            row210.TypeGrade = "T1";
            row210.ProductForm = "Wld. tube";
            row210.AlloyUNS = "K11522";
            row210.ClassCondition = "";
            row210.Notes = "G3, G11, G24, S2, T4";
            row210.StressValues = new double?[] { 13.4, 13.4, 13.4, null, 13.4, 13.4, 13.4, 13.4, 13.4, 13.4, 13.1, 12.7, 12.3, 11.6, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row210);

            // Row 28: SA-335, P1, Smls. pipe
            var row211 = new OldStressRowData();
            row211.LineNo = 28;
            row211.MaterialId = 8483;
            row211.SpecNo = "SA-335";
            row211.TypeGrade = "P1";
            row211.ProductForm = "Smls. pipe";
            row211.AlloyUNS = "K11522";
            row211.ClassCondition = "";
            row211.Notes = "G11, S2, T4";
            row211.StressValues = new double?[] { 15.7, 15.7, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row211);

            // Row 29: SA-369, FP1, Forged pipe
            var row212 = new OldStressRowData();
            row212.LineNo = 29;
            row212.MaterialId = 8485;
            row212.SpecNo = "SA-369";
            row212.TypeGrade = "FP1";
            row212.ProductForm = "Forged pipe";
            row212.AlloyUNS = "K11522";
            row212.ClassCondition = "";
            row212.Notes = "G11, S2, T4";
            row212.StressValues = new double?[] { 15.7, 15.7, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row212);

            // Row 30: SA-209, T1a, Smls. tube
            var row213 = new OldStressRowData();
            row213.LineNo = 30;
            row213.MaterialId = 8487;
            row213.SpecNo = "SA-209";
            row213.TypeGrade = "T1a";
            row213.ProductForm = "Smls. tube";
            row213.AlloyUNS = "K12023";
            row213.ClassCondition = "";
            row213.Notes = "G11, S3, T4";
            row213.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 16.8, 16.4, 15.9, 15.4, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row213);

            // Row 31: SA-250, T1a, Wld. tube
            var row214 = new OldStressRowData();
            row214.LineNo = 31;
            row214.MaterialId = 8488;
            row214.SpecNo = "SA-250";
            row214.TypeGrade = "T1a";
            row214.ProductForm = "Wld. tube";
            row214.AlloyUNS = "K12023";
            row214.ClassCondition = "";
            row214.Notes = "G11, S2, T4, W13";
            row214.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 16.8, 16.4, 15.9, 15.4, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row214);

            // Row 32: SA-250, T1a, Wld. tube
            var row215 = new OldStressRowData();
            row215.LineNo = 32;
            row215.MaterialId = 8489;
            row215.SpecNo = "SA-250";
            row215.TypeGrade = "T1a";
            row215.ProductForm = "Wld. tube";
            row215.AlloyUNS = "K12023";
            row215.ClassCondition = "";
            row215.Notes = "G3, G11, G24, S2, T4";
            row215.StressValues = new double?[] { 14.6, null, 14.6, null, 14.6, 14.6, 14.6, 14.6, 14.6, 14.3, 13.9, 13.6, 13.1, 11.6, 7, 4.1, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row215);

            // Row 33: SA-217, WC1, Castings
            var row216 = new OldStressRowData();
            row216.LineNo = 33;
            row216.MaterialId = 8490;
            row216.SpecNo = "SA-217";
            row216.TypeGrade = "WC1";
            row216.ProductForm = "Castings";
            row216.AlloyUNS = "J12524";
            row216.ClassCondition = "";
            row216.Notes = "G1, G11, G17, G18, S2, T4";
            row216.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.4, 17.9, 17.4, 16.9, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row216);

            // Row 34: SA-352, LC1, Castings
            var row217 = new OldStressRowData();
            row217.LineNo = 34;
            row217.MaterialId = 8492;
            row217.SpecNo = "SA-352";
            row217.TypeGrade = "LC1";
            row217.ProductForm = "Castings";
            row217.AlloyUNS = "J12522";
            row217.ClassCondition = "";
            row217.Notes = "G1, G17";
            row217.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row217);

            // Row 35: SA-426, CP1, Cast pipe
            var row218 = new OldStressRowData();
            row218.LineNo = 35;
            row218.MaterialId = 8494;
            row218.SpecNo = "SA-426";
            row218.TypeGrade = "CP1";
            row218.ProductForm = "Cast pipe";
            row218.AlloyUNS = "J12521";
            row218.ClassCondition = "";
            row218.Notes = "G17";
            row218.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row218);

            // Row 36: SA-204, A, Plate
            var row219 = new OldStressRowData();
            row219.LineNo = 36;
            row219.MaterialId = 8495;
            row219.SpecNo = "SA-204";
            row219.TypeGrade = "A";
            row219.ProductForm = "Plate";
            row219.AlloyUNS = "K11820";
            row219.ClassCondition = "";
            row219.Notes = "G11, S2, T4";
            row219.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.6, 18.6, 18.4, 17.9, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row219);

            // Row 37: SA-672, L65, Wld. pipe
            var row220 = new OldStressRowData();
            row220.LineNo = 37;
            row220.MaterialId = 8497;
            row220.SpecNo = "SA-672";
            row220.TypeGrade = "L65";
            row220.ProductForm = "Wld. pipe";
            row220.AlloyUNS = "K11820";
            row220.ClassCondition = "";
            row220.Notes = "G26, W10, W12";
            row220.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row220);

            // Row 38: SA-691, CM-65, Wld. pipe
            var row221 = new OldStressRowData();
            row221.LineNo = 38;
            row221.MaterialId = 8498;
            row221.SpecNo = "SA-691";
            row221.TypeGrade = "CM-65";
            row221.ProductForm = "Wld. pipe";
            row221.AlloyUNS = "K11820";
            row221.ClassCondition = "";
            row221.Notes = "G26, W10, W12";
            row221.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, 18.6, 18.6, 18.6, 18.6, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row221);

            // Row 39: SA-691, CM-65, Wld. pipe
            var row222 = new OldStressRowData();
            row222.LineNo = 39;
            row222.MaterialId = 8499;
            row222.SpecNo = "SA-691";
            row222.TypeGrade = "CM-65";
            row222.ProductForm = "Wld. pipe";
            row222.AlloyUNS = "K11820";
            row222.ClassCondition = "";
            row222.Notes = "G27, W10, W12";
            row222.StressValues = new double?[] { 18.6, null, 18.6, null, 18.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row222);

            // Row 1: SA-182, F1, Forgings
            var row223 = new OldStressRowData();
            row223.LineNo = 1;
            row223.MaterialId = 8500;
            row223.SpecNo = "SA-182";
            row223.TypeGrade = "F1";
            row223.ProductForm = "Forgings";
            row223.AlloyUNS = "K12822";
            row223.ClassCondition = "";
            row223.Notes = "G11, G18, S2, T4";
            row223.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.9, 19.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row223);

            // Row 2: SA-204, B, Plate
            var row224 = new OldStressRowData();
            row224.LineNo = 2;
            row224.MaterialId = 8501;
            row224.SpecNo = "SA-204";
            row224.TypeGrade = "B";
            row224.ProductForm = "Plate";
            row224.AlloyUNS = "K12020";
            row224.ClassCondition = "";
            row224.Notes = "G11, S2, T4";
            row224.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.9, 19.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row224);

            // Row 3: SA-336, F1, Forgings
            var row225 = new OldStressRowData();
            row225.LineNo = 3;
            row225.MaterialId = 8502;
            row225.SpecNo = "SA-336";
            row225.TypeGrade = "F1";
            row225.ProductForm = "Forgings";
            row225.AlloyUNS = "K12520";
            row225.ClassCondition = "";
            row225.Notes = "G11, G18, S2, T4";
            row225.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.9, 19.3, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row225);

            // Row 4: SA-672, L70, Wld. pipe
            var row226 = new OldStressRowData();
            row226.LineNo = 4;
            row226.MaterialId = 8503;
            row226.SpecNo = "SA-672";
            row226.TypeGrade = "L70";
            row226.ProductForm = "Wld. pipe";
            row226.AlloyUNS = "K12020";
            row226.ClassCondition = "";
            row226.Notes = "G26, W10, W12";
            row226.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row226);

            // Row 5: SA-691, CM-70, Wld. pipe
            var row227 = new OldStressRowData();
            row227.LineNo = 5;
            row227.MaterialId = 8504;
            row227.SpecNo = "SA-691";
            row227.TypeGrade = "CM-70";
            row227.ProductForm = "Wld. pipe";
            row227.AlloyUNS = "K12020";
            row227.ClassCondition = "";
            row227.Notes = "G26, W10, W12";
            row227.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row227);

            // Row 6: SA-691, CM-70, Wld. pipe
            var row228 = new OldStressRowData();
            row228.LineNo = 6;
            row228.MaterialId = 8505;
            row228.SpecNo = "SA-691";
            row228.TypeGrade = "CM-70";
            row228.ProductForm = "Wld. pipe";
            row228.AlloyUNS = "K12020";
            row228.ClassCondition = "";
            row228.Notes = "G27, W10, W12";
            row228.StressValues = new double?[] { 20, null, 20, null, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row228);

            // Row 7: SA-204, C, Plate
            var row229 = new OldStressRowData();
            row229.LineNo = 7;
            row229.MaterialId = 8506;
            row229.SpecNo = "SA-204";
            row229.TypeGrade = "C";
            row229.ProductForm = "Plate";
            row229.AlloyUNS = "K12320";
            row229.ClassCondition = "";
            row229.Notes = "G11, S2, T4, W12";
            row229.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 20.7, 13.7, 8.2, 4.8, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row229);

            // Row 8: SA-672, L75, Wld. pipe
            var row230 = new OldStressRowData();
            row230.LineNo = 8;
            row230.MaterialId = 8507;
            row230.SpecNo = "SA-672";
            row230.TypeGrade = "L75";
            row230.ProductForm = "Wld. pipe";
            row230.AlloyUNS = "K12320";
            row230.ClassCondition = "";
            row230.Notes = "G26, W10, W12";
            row230.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row230);

            // Row 9: SA-691, CM-75, Wld. pipe
            var row231 = new OldStressRowData();
            row231.LineNo = 9;
            row231.MaterialId = 8508;
            row231.SpecNo = "SA-691";
            row231.TypeGrade = "CM-75";
            row231.ProductForm = "Wld. pipe";
            row231.AlloyUNS = "K12320";
            row231.ClassCondition = "";
            row231.Notes = "G26, W10, W12";
            row231.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row231);

            // Row 10: SA-691, CM-75, Wld. pipe
            var row232 = new OldStressRowData();
            row232.LineNo = 10;
            row232.MaterialId = 8509;
            row232.SpecNo = "SA-691";
            row232.TypeGrade = "CM-75";
            row232.ProductForm = "Wld. pipe";
            row232.AlloyUNS = "K12320";
            row232.ClassCondition = "";
            row232.Notes = "G27, W10, W12";
            row232.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row232);

            // Row 11: SA-517, J, Plate
            var row233 = new OldStressRowData();
            row233.LineNo = 11;
            row233.MaterialId = 8510;
            row233.SpecNo = "SA-517";
            row233.TypeGrade = "J";
            row233.ProductForm = "Plate";
            row233.AlloyUNS = "K11625";
            row233.ClassCondition = "";
            row233.Notes = "";
            row233.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row233);

            // Row 12: SA-517, B, Plate
            var row234 = new OldStressRowData();
            row234.LineNo = 12;
            row234.MaterialId = 8511;
            row234.SpecNo = "SA-517";
            row234.TypeGrade = "B";
            row234.ProductForm = "Plate";
            row234.AlloyUNS = "K11630";
            row234.ClassCondition = "";
            row234.Notes = "";
            row234.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row234);

            // Row 13: SA-517, A, Plate
            var row235 = new OldStressRowData();
            row235.LineNo = 13;
            row235.MaterialId = 8513;
            row235.SpecNo = "SA-517";
            row235.TypeGrade = "A";
            row235.ProductForm = "Plate";
            row235.AlloyUNS = "K11856";
            row235.ClassCondition = "";
            row235.Notes = "";
            row235.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row235);

            // Row 14: SA-592, A, Forgings
            var row236 = new OldStressRowData();
            row236.LineNo = 14;
            row236.MaterialId = 8514;
            row236.SpecNo = "SA-592";
            row236.TypeGrade = "A";
            row236.ProductForm = "Forgings";
            row236.AlloyUNS = "K11856";
            row236.ClassCondition = "";
            row236.Notes = "";
            row236.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row236);

            // Row 15: SA-335, P2, Smls. pipe
            var row237 = new OldStressRowData();
            row237.LineNo = 15;
            row237.MaterialId = 8515;
            row237.SpecNo = "SA-335";
            row237.TypeGrade = "P2";
            row237.ProductForm = "Smls. pipe";
            row237.AlloyUNS = "K11547";
            row237.ClassCondition = "";
            row237.Notes = "T5";
            row237.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.9, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row237);

            // Row 16: SA-369, FP2, Forged pipe
            var row238 = new OldStressRowData();
            row238.LineNo = 16;
            row238.MaterialId = 8516;
            row238.SpecNo = "SA-369";
            row238.TypeGrade = "FP2";
            row238.ProductForm = "Forged pipe";
            row238.AlloyUNS = "K11547";
            row238.ClassCondition = "";
            row238.Notes = "T5";
            row238.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.4, 14.9, 14.5, 13.9, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row238);

            // Row 17: SA-387, 2, Plate
            var row239 = new OldStressRowData();
            row239.LineNo = 17;
            row239.MaterialId = 8517;
            row239.SpecNo = "SA-387";
            row239.TypeGrade = "2";
            row239.ProductForm = "Plate";
            row239.AlloyUNS = "K12143";
            row239.ClassCondition = "1";
            row239.Notes = "T5";
            row239.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, 15.3, 14.3, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row239);

            // Row 18: SA-691, 1/2 CR, Wld. pipe
            var row240 = new OldStressRowData();
            row240.LineNo = 18;
            row240.MaterialId = 8519;
            row240.SpecNo = "SA-691";
            row240.TypeGrade = "1/2 CR";
            row240.ProductForm = "Wld. pipe";
            row240.AlloyUNS = "K12143";
            row240.ClassCondition = "";
            row240.Notes = "G26, W10, W12";
            row240.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, 15.7, 15.7, 15.7, 15.7, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row240);

            // Row 19: SA-691, 1/2 CR, Wld. pipe
            var row241 = new OldStressRowData();
            row241.LineNo = 19;
            row241.MaterialId = 8520;
            row241.SpecNo = "SA-691";
            row241.TypeGrade = "1/2 CR";
            row241.ProductForm = "Wld. pipe";
            row241.AlloyUNS = "K12143";
            row241.ClassCondition = "";
            row241.Notes = "G27, W10, W12";
            row241.StressValues = new double?[] { 15.7, null, 15.7, null, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row241);

            // Row 20: SA-213, T2, Smls. tube
            var row242 = new OldStressRowData();
            row242.LineNo = 20;
            row242.MaterialId = 8521;
            row242.SpecNo = "SA-213";
            row242.TypeGrade = "T2";
            row242.ProductForm = "Smls. tube";
            row242.AlloyUNS = "K11547";
            row242.ClassCondition = "";
            row242.Notes = "T5";
            row242.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 16.9, 16.4, 16.1, 15.7, 15.4, 14.9, 14.5, 13.9, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row242);

            // Row 21: SA-426, CP2, Cast pipe
            var row243 = new OldStressRowData();
            row243.LineNo = 21;
            row243.MaterialId = 8522;
            row243.SpecNo = "SA-426";
            row243.TypeGrade = "CP2";
            row243.ProductForm = "Cast pipe";
            row243.AlloyUNS = "J11547";
            row243.ClassCondition = "";
            row243.Notes = "G17";
            row243.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 16.9, 16.4, 16.1, 15.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row243);

            // Row 22: SA-182, F2, Forgings
            var row244 = new OldStressRowData();
            row244.LineNo = 22;
            row244.MaterialId = 8523;
            row244.SpecNo = "SA-182";
            row244.TypeGrade = "F2";
            row244.ProductForm = "Forgings";
            row244.AlloyUNS = "K12122";
            row244.ClassCondition = "";
            row244.Notes = "G18, T5";
            row244.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 20, 19.9, 19.3, 18.6, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row244);

            // Row 23: SA-387, 2, Plate
            var row245 = new OldStressRowData();
            row245.LineNo = 23;
            row245.MaterialId = 8524;
            row245.SpecNo = "SA-387";
            row245.TypeGrade = "2";
            row245.ProductForm = "Plate";
            row245.AlloyUNS = "K12143";
            row245.ClassCondition = "2";
            row245.Notes = "T5";
            row245.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 20, 19.5, 18.6, 9.2, 5.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row245);

            // Row 24: SA-691, 1/2 CR, Wld. pipe
            var row246 = new OldStressRowData();
            row246.LineNo = 24;
            row246.MaterialId = 8525;
            row246.SpecNo = "SA-691";
            row246.TypeGrade = "1/2 CR";
            row246.ProductForm = "Wld. pipe";
            row246.AlloyUNS = "K12143";
            row246.ClassCondition = "";
            row246.Notes = "G26, W10, W12";
            row246.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row246);

            // Row 25: SA-202, A, Plate
            var row247 = new OldStressRowData();
            row247.LineNo = 25;
            row247.MaterialId = 8526;
            row247.SpecNo = "SA-202";
            row247.TypeGrade = "A";
            row247.ProductForm = "Plate";
            row247.AlloyUNS = "K11742";
            row247.ClassCondition = "";
            row247.Notes = "S1, T2";
            row247.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 20.2, 15.7, 12, 7.8, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row247);

            // Row 26: SA-202, B, Plate
            var row248 = new OldStressRowData();
            row248.LineNo = 26;
            row248.MaterialId = 8527;
            row248.SpecNo = "SA-202";
            row248.TypeGrade = "B";
            row248.ProductForm = "Plate";
            row248.AlloyUNS = "K12542";
            row248.ClassCondition = "";
            row248.Notes = "S1, T2";
            row248.StressValues = new double?[] { 24.3, 24.3, 24.3, null, 24.3, 24.3, 24.3, 23.5, 22.4, 21.1, 17.7, 12, 7.8, 5, 3, 1.5, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row248);

            // Row 27: SA-423, 1, Smls. & wld. tube
            var row249 = new OldStressRowData();
            row249.LineNo = 27;
            row249.MaterialId = 8529;
            row249.SpecNo = "SA-423";
            row249.TypeGrade = "1";
            row249.ProductForm = "Smls. & wld. tube";
            row249.AlloyUNS = "K11535";
            row249.ClassCondition = "";
            row249.Notes = "W13";
            row249.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row249);

            // Row 28: SA-423, 1, Wld. tube
            var row250 = new OldStressRowData();
            row250.LineNo = 28;
            row250.MaterialId = 8530;
            row250.SpecNo = "SA-423";
            row250.TypeGrade = "1";
            row250.ProductForm = "Wld. tube";
            row250.AlloyUNS = "K11535";
            row250.ClassCondition = "";
            row250.Notes = "G3, G24";
            row250.StressValues = new double?[] { 14.6, null, 14.6, null, 14.6, 14.6, 14.6, 14.6, 14.6, 14.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row250);

            // Row 29: SA-333, 4, Pipe
            var row251 = new OldStressRowData();
            row251.LineNo = 29;
            row251.MaterialId = 8531;
            row251.SpecNo = "SA-333";
            row251.TypeGrade = "4";
            row251.ProductForm = "Pipe";
            row251.AlloyUNS = "K11267";
            row251.ClassCondition = "";
            row251.Notes = "";
            row251.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row251);

            // Row 30: SA-372, E, Forgings
            var row252 = new OldStressRowData();
            row252.LineNo = 30;
            row252.MaterialId = 16833;
            row252.SpecNo = "SA-372";
            row252.TypeGrade = "E";
            row252.ProductForm = "Forgings";
            row252.AlloyUNS = "K13047";
            row252.ClassCondition = "65";
            row252.Notes = "W2, W11";
            row252.StressValues = new double?[] { 30, null, 30, null, 30, 30, 30, 30, 29.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row252);

            // Row 31: SA-372, J, Forgings
            var row253 = new OldStressRowData();
            row253.LineNo = 31;
            row253.MaterialId = 16834;
            row253.SpecNo = "SA-372";
            row253.TypeGrade = "J";
            row253.ProductForm = "Forgings";
            row253.AlloyUNS = "K13548";
            row253.ClassCondition = "65";
            row253.Notes = "W2, W11";
            row253.StressValues = new double?[] { 30, null, 30, null, 30, 30, 30, 30, 29.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row253);

            // Row 32: SA-372, E, Forgings
            var row254 = new OldStressRowData();
            row254.LineNo = 32;
            row254.MaterialId = 8533;
            row254.SpecNo = "SA-372";
            row254.TypeGrade = "E";
            row254.ProductForm = "Forgings";
            row254.AlloyUNS = "K13047";
            row254.ClassCondition = "70";
            row254.Notes = "W2, W11";
            row254.StressValues = new double?[] { 34.3, 34.3, 34.3, null, 34.3, 34.3, 34.3, 34.3, 34.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row254);

            // Row 33: SA-372, F, Forgings
            var row255 = new OldStressRowData();
            row255.LineNo = 33;
            row255.MaterialId = 8534;
            row255.SpecNo = "SA-372";
            row255.TypeGrade = "F";
            row255.ProductForm = "Forgings";
            row255.AlloyUNS = "G41350";
            row255.ClassCondition = "70";
            row255.Notes = "W2, W11";
            row255.StressValues = new double?[] { 34.3, null, 33.3, null, 32.6, 32.3, 32.2, 31.8, 30.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row255);

            // Row 34: SA-372, G, Forgings
            var row256 = new OldStressRowData();
            row256.LineNo = 34;
            row256.MaterialId = 8535;
            row256.SpecNo = "SA-372";
            row256.TypeGrade = "G";
            row256.ProductForm = "Forgings";
            row256.AlloyUNS = "K13049";
            row256.ClassCondition = "70";
            row256.Notes = "W2, W11";
            row256.StressValues = new double?[] { 34.3, null, 34.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row256);

            // Row 35: SA-372, H, Forgings
            var row257 = new OldStressRowData();
            row257.LineNo = 35;
            row257.MaterialId = 8536;
            row257.SpecNo = "SA-372";
            row257.TypeGrade = "H";
            row257.ProductForm = "Forgings";
            row257.AlloyUNS = "K13547";
            row257.ClassCondition = "70";
            row257.Notes = "W2, W11";
            row257.StressValues = new double?[] { 34.3, null, 34.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row257);

            // Row 36: SA-372, J, Forgings
            var row258 = new OldStressRowData();
            row258.LineNo = 36;
            row258.MaterialId = 8537;
            row258.SpecNo = "SA-372";
            row258.TypeGrade = "J";
            row258.ProductForm = "Forgings";
            row258.AlloyUNS = "K13548";
            row258.ClassCondition = "70";
            row258.Notes = "W2, W11";
            row258.StressValues = new double?[] { 34.3, null, 34.3, null, 34.3, 34.3, 34.3, 34.3, 34.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row258);

            // Row 37: SA-372, J, Forgings
            var row259 = new OldStressRowData();
            row259.LineNo = 37;
            row259.MaterialId = 8538;
            row259.SpecNo = "SA-372";
            row259.TypeGrade = "J";
            row259.ProductForm = "Forgings";
            row259.AlloyUNS = "G41370";
            row259.ClassCondition = "110";
            row259.Notes = "W2, W11";
            row259.StressValues = new double?[] { 38.5, null, 36.9, null, 36.7, 36.5, 36.1, 35.9, 34.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row259);

            // Row 1: SA-387, 12, Plate
            var row260 = new OldStressRowData();
            row260.LineNo = 1;
            row260.MaterialId = 8539;
            row260.SpecNo = "SA-387";
            row260.TypeGrade = "12";
            row260.ProductForm = "Plate";
            row260.AlloyUNS = "K11757";
            row260.ClassCondition = "1";
            row260.Notes = "S4, T5";
            row260.StressValues = new double?[] { 15.7, null, 15.4, null, 15.1, 15.1, 15.1, 15.1, 15.1, 15.1, 15.1, 15.1, 15.1, 14.7, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row260);

            // Row 2: SA-691, 1CR, Wld. pipe
            var row261 = new OldStressRowData();
            row261.LineNo = 2;
            row261.MaterialId = 8540;
            row261.SpecNo = "SA-691";
            row261.TypeGrade = "1CR";
            row261.ProductForm = "Wld. pipe";
            row261.AlloyUNS = "K11757";
            row261.ClassCondition = "";
            row261.Notes = "G26, W10, W12";
            row261.StressValues = new double?[] { 15.7, null, 15.4, null, 15.1, 15.1, 15.1, 15.1, 15.1, 15.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row261);

            // Row 3: SA-426, CP12, Cast pipe
            var row262 = new OldStressRowData();
            row262.LineNo = 3;
            row262.MaterialId = 8542;
            row262.SpecNo = "SA-426";
            row262.TypeGrade = "CP12";
            row262.ProductForm = "Cast pipe";
            row262.AlloyUNS = "J11562";
            row262.ClassCondition = "";
            row262.Notes = "G17";
            row262.StressValues = new double?[] { 17.1, null, 16.8, null, 16.5, 16.2, 15.7, 15.2, 15, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row262);

            // Row 4: SA-182, F12, Forgings
            var row263 = new OldStressRowData();
            row263.LineNo = 4;
            row263.MaterialId = 8541;
            row263.SpecNo = "SA-182";
            row263.TypeGrade = "F12";
            row263.ProductForm = "Forgings";
            row263.AlloyUNS = "K11562";
            row263.ClassCondition = "1";
            row263.Notes = "T5";
            row263.StressValues = new double?[] { 17.1, 17.1, 16.8, null, 16.5, 16.5, 16.5, 16.3, 16, 15.8, 15.5, 15.3, 14.9, 14.5, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row263);

            // Row 5: SA-213, T12, Smls. tube
            var row264 = new OldStressRowData();
            row264.LineNo = 5;
            row264.MaterialId = 8543;
            row264.SpecNo = "SA-213";
            row264.TypeGrade = "T12";
            row264.ProductForm = "Smls. tube";
            row264.AlloyUNS = "K11562";
            row264.ClassCondition = "";
            row264.Notes = "S4, T5";
            row264.StressValues = new double?[] { 17.1, null, 16.8, null, 16.5, 16.5, 16.5, 16.3, 16, 15.8, 15.5, 15.3, 14.9, 14.5, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row264);

            // Row 6: SA-234, WP12, Smls. & wld. fittings
            var row265 = new OldStressRowData();
            row265.LineNo = 6;
            row265.MaterialId = 8547;
            row265.SpecNo = "SA-234";
            row265.TypeGrade = "WP12";
            row265.ProductForm = "Smls. & wld. fittings";
            row265.AlloyUNS = "K12062";
            row265.ClassCondition = "1";
            row265.Notes = "G18, S4, T5";
            row265.StressValues = new double?[] { 17.1, null, 16.8, null, 16.5, 16.5, 16.5, 16.3, 16, 15.8, 15.5, 15.3, 14.9, 14.5, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row265);

            // Row 7: SA-335, P12, Smls. pipe
            var row266 = new OldStressRowData();
            row266.LineNo = 7;
            row266.MaterialId = 8549;
            row266.SpecNo = "SA-335";
            row266.TypeGrade = "P12";
            row266.ProductForm = "Smls. pipe";
            row266.AlloyUNS = "K11562";
            row266.ClassCondition = "";
            row266.Notes = "S4, T5";
            row266.StressValues = new double?[] { 17.1, null, 16.8, null, 16.5, 16.5, 16.5, 16.3, 16, 15.8, 15.5, 15.3, 14.9, 14.5, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row266);

            // Row 8: SA-369, FP12, Forged pipe
            var row267 = new OldStressRowData();
            row267.LineNo = 8;
            row267.MaterialId = 8551;
            row267.SpecNo = "SA-369";
            row267.TypeGrade = "FP12";
            row267.ProductForm = "Forged pipe";
            row267.AlloyUNS = "K11562";
            row267.ClassCondition = "";
            row267.Notes = "S4, T5";
            row267.StressValues = new double?[] { 17.1, null, 16.8, null, 16.5, 16.5, 16.5, 16.3, 16, 15.8, 15.5, 15.3, 14.9, 14.5, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row267);

            // Row 9: SA-387, 12, Plate
            var row268 = new OldStressRowData();
            row268.LineNo = 9;
            row268.MaterialId = 8553;
            row268.SpecNo = "SA-387";
            row268.TypeGrade = "12";
            row268.ProductForm = "Plate";
            row268.AlloyUNS = "K11757";
            row268.ClassCondition = "2";
            row268.Notes = "S4, T5";
            row268.StressValues = new double?[] { 18.6, 18.6, 18.2, null, 17.9, 17.9, 17.9, 17.9, 17.9, 17.9, 17.9, 17.9, 17.9, 17.4, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row268);

            // Row 10: SA-691, 1CR, Wld. pipe
            var row269 = new OldStressRowData();
            row269.LineNo = 10;
            row269.MaterialId = 8554;
            row269.SpecNo = "SA-691";
            row269.TypeGrade = "1CR";
            row269.ProductForm = "Wld. pipe";
            row269.AlloyUNS = "K11757";
            row269.ClassCondition = "";
            row269.Notes = "G26, W10, W12";
            row269.StressValues = new double?[] { 18.6, null, 18.2, null, 17.9, 17.9, 17.9, 17.9, 17.9, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row269);

            // Row 11: SA-182, F12, Forgings
            var row270 = new OldStressRowData();
            row270.LineNo = 11;
            row270.MaterialId = 8555;
            row270.SpecNo = "SA-182";
            row270.TypeGrade = "F12";
            row270.ProductForm = "Forgings";
            row270.AlloyUNS = "K11564";
            row270.ClassCondition = "2";
            row270.Notes = "G18, S4, T4";
            row270.StressValues = new double?[] { 20, null, 19.6, null, 19.2, 19.2, 19.2, 19.2, 19.2, 19.2, 19.2, 19.1, 18.6, 18, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row270);

            // Row 12: SA-336, F12, Forgings
            var row271 = new OldStressRowData();
            row271.LineNo = 12;
            row271.MaterialId = 8556;
            row271.SpecNo = "SA-336";
            row271.TypeGrade = "F12";
            row271.ProductForm = "Forgings";
            row271.AlloyUNS = "K11564";
            row271.ClassCondition = "";
            row271.Notes = "G18, S4, T4";
            row271.StressValues = new double?[] { 20, 20, 19.6, null, 19.2, 19.2, 19.2, 19.2, 19.2, 19.2, 19.2, 19.1, 18.6, 18, 11.3, 7.2, 4.5, 2.8, 1.8, 1.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row271);

            // Row 13: SA-213, T17, Smls. tube
            var row272 = new OldStressRowData();
            row272.LineNo = 13;
            row272.MaterialId = 8532;
            row272.SpecNo = "SA-213";
            row272.TypeGrade = "T17";
            row272.ProductForm = "Smls. tube";
            row272.AlloyUNS = "K12047";
            row272.ClassCondition = "";
            row272.Notes = "";
            row272.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row272);

            // Row 14: SA-217, WC6, Castings
            var row273 = new OldStressRowData();
            row273.LineNo = 14;
            row273.MaterialId = 8557;
            row273.SpecNo = "SA-217";
            row273.TypeGrade = "WC6";
            row273.ProductForm = "Castings";
            row273.AlloyUNS = "J12072";
            row273.ClassCondition = "";
            row273.Notes = "G1, G17, G18, T4";
            row273.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.7, 19.2, 18.7, 13.7, 9.3, 6.3, 4.2, 2.8, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row273);

            // Row 15: SA-426, CP11, Cast pipe
            var row274 = new OldStressRowData();
            row274.LineNo = 15;
            row274.MaterialId = 8559;
            row274.SpecNo = "SA-426";
            row274.TypeGrade = "CP11";
            row274.ProductForm = "Cast pipe";
            row274.AlloyUNS = "J12072";
            row274.ClassCondition = "";
            row274.Notes = "G17";
            row274.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row274);

            // Row 16: SA-739, B11, Bar
            var row275 = new OldStressRowData();
            row275.LineNo = 16;
            row275.MaterialId = 8560;
            row275.SpecNo = "SA-739";
            row275.TypeGrade = "B11";
            row275.ProductForm = "Bar";
            row275.AlloyUNS = "K11797";
            row275.ClassCondition = "";
            row275.Notes = "T4";
            row275.StressValues = new double?[] { 20, 20, 20, null, 20, 20, 20, 20, 20, 20, 20, 20, 19.4, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row275);

            // Row 17: SA-199, T11, Smls. tube
            var row276 = new OldStressRowData();
            row276.LineNo = 17;
            row276.MaterialId = 8561;
            row276.SpecNo = "SA-199";
            row276.TypeGrade = "T11";
            row276.ProductForm = "Smls. tube";
            row276.AlloyUNS = "K11597";
            row276.ClassCondition = "";
            row276.Notes = "T5";
            row276.StressValues = new double?[] { 16.7, null, 15.4, null, 14.6, 14, 13.5, 13.1, 12.8, 12.6, 12.3, 12, 11.7, 11.3, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row276);

            // Row 18: SA-182, F11, Forgings
            var row277 = new OldStressRowData();
            row277.LineNo = 18;
            row277.MaterialId = 8562;
            row277.SpecNo = "SA-182";
            row277.TypeGrade = "F11";
            row277.ProductForm = "Forgings";
            row277.AlloyUNS = "K11597";
            row277.ClassCondition = "1";
            row277.Notes = "G18, S4, T5";
            row277.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row277);

            // Row 19: SA-213, T11, Smls. tube
            var row278 = new OldStressRowData();
            row278.LineNo = 19;
            row278.MaterialId = 8563;
            row278.SpecNo = "SA-213";
            row278.TypeGrade = "T11";
            row278.ProductForm = "Smls. tube";
            row278.AlloyUNS = "K11597";
            row278.ClassCondition = "";
            row278.Notes = "S4, T5";
            row278.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row278);

            // Row 20: SA-234, WP11, Smls. & wld. fittings
            var row279 = new OldStressRowData();
            row279.LineNo = 20;
            row279.MaterialId = 8565;
            row279.SpecNo = "SA-234";
            row279.TypeGrade = "WP11";
            row279.ProductForm = "Smls. & wld. fittings";
            row279.AlloyUNS = "";
            row279.ClassCondition = "1";
            row279.Notes = "G18, S4, T5";
            row279.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row279);

            // Row 21: SA-335, P11, Smls. pipe
            var row280 = new OldStressRowData();
            row280.LineNo = 21;
            row280.MaterialId = 8568;
            row280.SpecNo = "SA-335";
            row280.TypeGrade = "P11";
            row280.ProductForm = "Smls. pipe";
            row280.AlloyUNS = "K11597";
            row280.ClassCondition = "";
            row280.Notes = "S4, T5";
            row280.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row280);

            // Row 22: SA-336, F11, Forgings
            var row281 = new OldStressRowData();
            row281.LineNo = 22;
            row281.MaterialId = 8570;
            row281.SpecNo = "SA-336";
            row281.TypeGrade = "F11";
            row281.ProductForm = "Forgings";
            row281.AlloyUNS = "K11597";
            row281.ClassCondition = "1";
            row281.Notes = "G18, S4, T5";
            row281.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row281);

            // Row 23: SA-369, FP11, Forged pipe
            var row282 = new OldStressRowData();
            row282.LineNo = 23;
            row282.MaterialId = 8571;
            row282.SpecNo = "SA-369";
            row282.TypeGrade = "FP11";
            row282.ProductForm = "Forged pipe";
            row282.AlloyUNS = "K11597";
            row282.ClassCondition = "";
            row282.Notes = "S4, T5";
            row282.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 16.8, 16.2, 15.7, 15.4, 15.1, 14.8, 14.4, 14, 13.6, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row282);

            // Row 24: SA-387, 11, Plate
            var row283 = new OldStressRowData();
            row283.LineNo = 24;
            row283.MaterialId = 8573;
            row283.SpecNo = "SA-387";
            row283.TypeGrade = "11";
            row283.ProductForm = "Plate";
            row283.AlloyUNS = "K11789";
            row283.ClassCondition = "1";
            row283.Notes = "S4, T4";
            row283.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 17.1, 17.1, 16.8, 16.4, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row283);

            // Row 25: SA-691, 1 1/4 CR, Wld. pipe
            var row284 = new OldStressRowData();
            row284.LineNo = 25;
            row284.MaterialId = 8574;
            row284.SpecNo = "SA-691";
            row284.TypeGrade = "1 1/4 CR";
            row284.ProductForm = "Wld. pipe";
            row284.AlloyUNS = "K11789";
            row284.ClassCondition = "";
            row284.Notes = "G27, W10, W12";
            row284.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row284);

            // Row 26: SA-691, 1 1/4 CR, Wld. pipe
            var row285 = new OldStressRowData();
            row285.LineNo = 26;
            row285.MaterialId = 8575;
            row285.SpecNo = "SA-691";
            row285.TypeGrade = "1 1/4 CR";
            row285.ProductForm = "Wld. pipe";
            row285.AlloyUNS = "K11789";
            row285.ClassCondition = "";
            row285.Notes = "G26, W10, W12";
            row285.StressValues = new double?[] { 17.1, null, 17.1, null, 17.1, 17.1, 17.1, 17.1, 17.1, 17.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row285);

            // Row 27: SA-182, F11, Forgings
            var row286 = new OldStressRowData();
            row286.LineNo = 27;
            row286.MaterialId = 8576;
            row286.SpecNo = "SA-182";
            row286.TypeGrade = "F11";
            row286.ProductForm = "Forgings";
            row286.AlloyUNS = "K11572";
            row286.ClassCondition = "2";
            row286.Notes = "G18, S4, T4";
            row286.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.7, 19.2, 18.7, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row286);

            // Row 28: SA-336, F11, Forgings
            var row287 = new OldStressRowData();
            row287.LineNo = 28;
            row287.MaterialId = 8577;
            row287.SpecNo = "SA-336";
            row287.TypeGrade = "F11";
            row287.ProductForm = "Forgings";
            row287.AlloyUNS = "K11572";
            row287.ClassCondition = "2";
            row287.Notes = "G18, S4, T4";
            row287.StressValues = new double?[] { 20, null, 20, null, 20, 20, 20, 20, 20, 20, 19.7, 19.2, 18.7, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row287);

            // Row 29: SA-336, F11, Forgings
            var row288 = new OldStressRowData();
            row288.LineNo = 29;
            row288.MaterialId = 8578;
            row288.SpecNo = "SA-336";
            row288.TypeGrade = "F11";
            row288.ProductForm = "Forgings";
            row288.AlloyUNS = "K11572";
            row288.ClassCondition = "3";
            row288.Notes = "T3";
            row288.StressValues = new double?[] { 21.4, 21.4, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 20.2, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row288);

            // Row 30: SA-387, 11, Plate
            var row289 = new OldStressRowData();
            row289.LineNo = 30;
            row289.MaterialId = 8579;
            row289.SpecNo = "SA-387";
            row289.TypeGrade = "11";
            row289.ProductForm = "Plate";
            row289.AlloyUNS = "K11789";
            row289.ClassCondition = "2";
            row289.Notes = "S4, T3";
            row289.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, 20.2, 13.7, 9.3, 6.3, 4.2, 2.8, 1.9, 1.2, null, null, null, null, null, null, null, null, null };
            batch.Add(row289);

            // Row 31: SA-691, 1 1/4 CR, Wld. pipe
            var row290 = new OldStressRowData();
            row290.LineNo = 31;
            row290.MaterialId = 8580;
            row290.SpecNo = "SA-691";
            row290.TypeGrade = "1 1/4 CR";
            row290.ProductForm = "Wld. pipe";
            row290.AlloyUNS = "K11789";
            row290.ClassCondition = "";
            row290.Notes = "G26, W10, W12";
            row290.StressValues = new double?[] { 21.4, null, 21.4, null, 21.4, 21.4, 21.4, 21.4, 21.4, 21.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row290);

            // Row 32: SA-592, E, Forgings
            var row291 = new OldStressRowData();
            row291.LineNo = 32;
            row291.MaterialId = 8581;
            row291.SpecNo = "SA-592";
            row291.TypeGrade = "E";
            row291.ProductForm = "Forgings";
            row291.AlloyUNS = "K11695";
            row291.ClassCondition = "";
            row291.Notes = "S7";
            row291.StressValues = new double?[] { 30, null, 30, null, 30, 30, 30, 30, 30, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row291);

            // Row 33: SA-592, E, Forgings
            var row292 = new OldStressRowData();
            row292.LineNo = 33;
            row292.MaterialId = 8582;
            row292.SpecNo = "SA-592";
            row292.TypeGrade = "E";
            row292.ProductForm = "Forgings";
            row292.AlloyUNS = "K11695";
            row292.ClassCondition = "";
            row292.Notes = "";
            row292.StressValues = new double?[] { 32.9, null, 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row292);

            // Row 34: SA-517, E, Plate
            var row293 = new OldStressRowData();
            row293.LineNo = 34;
            row293.MaterialId = 8583;
            row293.SpecNo = "SA-517";
            row293.TypeGrade = "E";
            row293.ProductForm = "Plate";
            row293.AlloyUNS = "K21604";
            row293.ClassCondition = "";
            row293.Notes = "";
            row293.StressValues = new double?[] { 30, null, 30, 30, 30, 30, 30, 30, 30, 29.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row293);

            // Row 35: SA-517, E, Plate
            var row294 = new OldStressRowData();
            row294.LineNo = 35;
            row294.MaterialId = 8585;
            row294.SpecNo = "SA-517";
            row294.TypeGrade = "E";
            row294.ProductForm = "Plate";
            row294.AlloyUNS = "K21604";
            row294.ClassCondition = "";
            row294.Notes = "";
            row294.StressValues = new double?[] { 32.9, null, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, 32.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row294);

            // Row 36: SA-199, T22, Tube
            var row295 = new OldStressRowData();
            row295.LineNo = 36;
            row295.MaterialId = 8586;
            row295.SpecNo = "SA-199";
            row295.TypeGrade = "T22";
            row295.ProductForm = "Tube";
            row295.AlloyUNS = "K21590";
            row295.ClassCondition = "";
            row295.Notes = "T4, W7";
            row295.StressValues = new double?[] { 16.7, 15.9, 15.6, null, 15.1, 15, 15, 15, 15, 15, 14.9, 14.8, 14.5, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row295);

            // Row 37: SA-182, F22, Forgings
            var row296 = new OldStressRowData();
            row296.LineNo = 37;
            row296.MaterialId = 8587;
            row296.SpecNo = "SA-182";
            row296.TypeGrade = "F22";
            row296.ProductForm = "Forgings";
            row296.AlloyUNS = "K21590";
            row296.ClassCondition = "1";
            row296.Notes = "G18, S4, T4, W7, W9";
            row296.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row296);

            // Row 38: SA-213, T22, Smls. tube
            var row297 = new OldStressRowData();
            row297.LineNo = 38;
            row297.MaterialId = 8588;
            row297.SpecNo = "SA-213";
            row297.TypeGrade = "T22";
            row297.ProductForm = "Smls. tube";
            row297.AlloyUNS = "K21590";
            row297.ClassCondition = "";
            row297.Notes = "S4, T4, W7, W9";
            row297.StressValues = new double?[] { 17.1, null, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row297);

            // Row 39: SA-234, WP22, Smls. & wld. fittings
            var row298 = new OldStressRowData();
            row298.LineNo = 39;
            row298.MaterialId = 8589;
            row298.SpecNo = "SA-234";
            row298.TypeGrade = "WP22";
            row298.ProductForm = "Smls. & wld. fittings";
            row298.AlloyUNS = "K21590";
            row298.ClassCondition = "1";
            row298.Notes = "G18, S4, T4, W7, W9";
            row298.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row298);

            // Row 1: SA-335, P22, Smls. pipe
            var row299 = new OldStressRowData();
            row299.LineNo = 1;
            row299.MaterialId = 8591;
            row299.SpecNo = "SA-335";
            row299.TypeGrade = "P22";
            row299.ProductForm = "Smls. pipe";
            row299.AlloyUNS = "K21590";
            row299.ClassCondition = "";
            row299.Notes = "S4, T4, W7, W9";
            row299.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
            batch.Add(row299);

            // Row 2: SA-336, F22, Forgings
            var row300 = new OldStressRowData();
            row300.LineNo = 2;
            row300.MaterialId = 8592;
            row300.SpecNo = "SA-336";
            row300.TypeGrade = "F22";
            row300.ProductForm = "Forgings";
            row300.AlloyUNS = "K21590";
            row300.ClassCondition = "1";
            row300.Notes = "G18, S4, T4, W7, W9";
            row300.StressValues = new double?[] { 17.1, 17.1, 17.1, null, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 16.6, 13.6, 10.8, 8, 5.7, 3.8, 2.4, 1.4, null, null, null, null, null, null, null, null, null };
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
