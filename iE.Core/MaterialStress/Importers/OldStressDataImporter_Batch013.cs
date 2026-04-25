using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class OldStressDataImporter_Batch013
    {
        public static readonly double[] Temperatures = new double[]
        {
            -20, 150, 200, 250, 300, 400, 500, 600, 650, 700, 750, 800, 850, 900, 950, 1000,
            1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500, 1550, 1600, 1650
        };

        public static void ImportBatch013(MaterialStressService service)
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

            // Row 36: SA-182, F321H, Forgings
            var row1201 = new OldStressRowData();
            row1201.LineNo = 36;
            row1201.MaterialId = 9439;
            row1201.SpecNo = "SA-182";
            row1201.TypeGrade = "F321H";
            row1201.ProductForm = "Forgings";
            row1201.AlloyUNS = "S32109";
            row1201.ClassCondition = "";
            row1201.Notes = "G5, G18, H2";
            row1201.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1201);

            // Row 37: SA-182, F321H, Forgings
            var row1202 = new OldStressRowData();
            row1202.LineNo = 37;
            row1202.MaterialId = 9440;
            row1202.SpecNo = "SA-182";
            row1202.TypeGrade = "F321H";
            row1202.ProductForm = "Forgings";
            row1202.AlloyUNS = "S32109";
            row1202.ClassCondition = "";
            row1202.Notes = "H2";
            row1202.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.5, 10.1, 8.8, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1202);

            // Row 1: SA-336, F321H, Forgings
            var row1203 = new OldStressRowData();
            row1203.LineNo = 1;
            row1203.MaterialId = 9445;
            row1203.SpecNo = "SA-336";
            row1203.TypeGrade = "F321H";
            row1203.ProductForm = "Forgings";
            row1203.AlloyUNS = "S32109";
            row1203.ClassCondition = "";
            row1203.Notes = "G5, H2";
            row1203.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1203);

            // Row 2: SA-336, F321H, Forgings
            var row1204 = new OldStressRowData();
            row1204.LineNo = 2;
            row1204.MaterialId = 9446;
            row1204.SpecNo = "SA-336";
            row1204.TypeGrade = "F321H";
            row1204.ProductForm = "Forgings";
            row1204.AlloyUNS = "S32109";
            row1204.ClassCondition = "";
            row1204.Notes = "H2";
            row1204.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.5, 10.1, 8.8, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1204);

            // Row 3: SA-430, FP321H, Forged pipe
            var row1205 = new OldStressRowData();
            row1205.LineNo = 3;
            row1205.MaterialId = 9452;
            row1205.SpecNo = "SA-430";
            row1205.TypeGrade = "FP321H";
            row1205.ProductForm = "Forged pipe";
            row1205.AlloyUNS = "S32109";
            row1205.ClassCondition = "";
            row1205.Notes = "G18, H1";
            row1205.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1205);

            // Row 4: SA-430, FP321H, Forged pipe
            var row1206 = new OldStressRowData();
            row1206.LineNo = 4;
            row1206.MaterialId = 9451;
            row1206.SpecNo = "SA-430";
            row1206.TypeGrade = "FP321H";
            row1206.ProductForm = "Forged pipe";
            row1206.AlloyUNS = "S32109";
            row1206.ClassCondition = "";
            row1206.Notes = "G5, G18, H1, H2";
            row1206.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 15.3, 14.9, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1206);

            // Row 5: SA-430, FP321H, Forged pipe
            var row1207 = new OldStressRowData();
            row1207.LineNo = 5;
            row1207.MaterialId = 9450;
            row1207.SpecNo = "SA-430";
            row1207.TypeGrade = "FP321H";
            row1207.ProductForm = "Forged pipe";
            row1207.AlloyUNS = "S32109";
            row1207.ClassCondition = "";
            row1207.Notes = "H2";
            row1207.StressValues = new double?[] { 17.5, null, 16.6, null, 15.6, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 10.5, 10.1, 8.8, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1207);

            // Row 6: SA-182, F321, Forgings
            var row1208 = new OldStressRowData();
            row1208.LineNo = 6;
            row1208.MaterialId = 9455;
            row1208.SpecNo = "SA-182";
            row1208.TypeGrade = "F321";
            row1208.ProductForm = "Forgings";
            row1208.AlloyUNS = "S32100";
            row1208.ClassCondition = "";
            row1208.Notes = "G5, G12, G18";
            row1208.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1208);

            // Row 7: SA-182, F321, Forgings
            var row1209 = new OldStressRowData();
            row1209.LineNo = 7;
            row1209.MaterialId = 9454;
            row1209.SpecNo = "SA-182";
            row1209.TypeGrade = "F321";
            row1209.ProductForm = "Forgings";
            row1209.AlloyUNS = "S32100";
            row1209.ClassCondition = "";
            row1209.Notes = "G12, G18";
            row1209.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1209);

            // Row 8: SA-182, F321, Forgings
            var row1210 = new OldStressRowData();
            row1210.LineNo = 8;
            row1210.MaterialId = 9453;
            row1210.SpecNo = "SA-182";
            row1210.TypeGrade = "F321";
            row1210.ProductForm = "Forgings";
            row1210.AlloyUNS = "S32100";
            row1210.ClassCondition = "";
            row1210.Notes = "G5, G12, H2";
            row1210.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1210);

            // Row 9: SA-213, TP321, Smls. tube
            var row1211 = new OldStressRowData();
            row1211.LineNo = 9;
            row1211.MaterialId = 9460;
            row1211.SpecNo = "SA-213";
            row1211.TypeGrade = "TP321";
            row1211.ProductForm = "Smls. tube";
            row1211.AlloyUNS = "S32100";
            row1211.ClassCondition = "";
            row1211.Notes = "G5, G12, G18";
            row1211.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1211);

            // Row 10: SA-213, TP321, Smls. tube
            var row1212 = new OldStressRowData();
            row1212.LineNo = 10;
            row1212.MaterialId = 9459;
            row1212.SpecNo = "SA-213";
            row1212.TypeGrade = "TP321";
            row1212.ProductForm = "Smls. tube";
            row1212.AlloyUNS = "S32100";
            row1212.ClassCondition = "";
            row1212.Notes = "G12, G18";
            row1212.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1212);

            // Row 11: SA-213, TP321, Smls. tube
            var row1213 = new OldStressRowData();
            row1213.LineNo = 11;
            row1213.MaterialId = 9458;
            row1213.SpecNo = "SA-213";
            row1213.TypeGrade = "TP321";
            row1213.ProductForm = "Smls. tube";
            row1213.AlloyUNS = "S32100";
            row1213.ClassCondition = "";
            row1213.Notes = "G5, G12, H2";
            row1213.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1213);

            // Row 12: SA-240, 321, Plate
            var row1214 = new OldStressRowData();
            row1214.LineNo = 12;
            row1214.MaterialId = 9465;
            row1214.SpecNo = "SA-240";
            row1214.TypeGrade = "321";
            row1214.ProductForm = "Plate";
            row1214.AlloyUNS = "S32100";
            row1214.ClassCondition = "";
            row1214.Notes = "G5, G12, G18";
            row1214.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1214);

            // Row 13: SA-240, 321, Plate
            var row1215 = new OldStressRowData();
            row1215.LineNo = 13;
            row1215.MaterialId = 9464;
            row1215.SpecNo = "SA-240";
            row1215.TypeGrade = "321";
            row1215.ProductForm = "Plate";
            row1215.AlloyUNS = "S32100";
            row1215.ClassCondition = "";
            row1215.Notes = "G12, G18";
            row1215.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1215);

            // Row 14: SA-240, 321, Plate
            var row1216 = new OldStressRowData();
            row1216.LineNo = 14;
            row1216.MaterialId = 9463;
            row1216.SpecNo = "SA-240";
            row1216.TypeGrade = "321";
            row1216.ProductForm = "Plate";
            row1216.AlloyUNS = "S32100";
            row1216.ClassCondition = "";
            row1216.Notes = "G5, G12";
            row1216.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1216);

            // Row 15: SA-249, TP321, Wld. tube
            var row1217 = new OldStressRowData();
            row1217.LineNo = 15;
            row1217.MaterialId = 9470;
            row1217.SpecNo = "SA-249";
            row1217.TypeGrade = "TP321";
            row1217.ProductForm = "Wld. tube";
            row1217.AlloyUNS = "S32100";
            row1217.ClassCondition = "";
            row1217.Notes = "G3, G5, G12, G18";
            row1217.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.3, null, null, null };
            batch.Add(row1217);

            // Row 16: SA-249, TP321, Wld. tube
            var row1218 = new OldStressRowData();
            row1218.LineNo = 16;
            row1218.MaterialId = 9471;
            row1218.SpecNo = "SA-249";
            row1218.TypeGrade = "TP321";
            row1218.ProductForm = "Wld. tube";
            row1218.AlloyUNS = "S32100";
            row1218.ClassCondition = "";
            row1218.Notes = "G12, G18, W14";
            row1218.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.2, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1218);

            // Row 17: SA-249, TP321, Wld. tube
            var row1219 = new OldStressRowData();
            row1219.LineNo = 17;
            row1219.MaterialId = 9472;
            row1219.SpecNo = "SA-249";
            row1219.TypeGrade = "TP321";
            row1219.ProductForm = "Wld. tube";
            row1219.AlloyUNS = "S32100";
            row1219.ClassCondition = "";
            row1219.Notes = "G3, G12, G18";
            row1219.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.3, null, null, null };
            batch.Add(row1219);

            // Row 18: SA-249, TP321, Wld. tube
            var row1220 = new OldStressRowData();
            row1220.LineNo = 18;
            row1220.MaterialId = 9467;
            row1220.SpecNo = "SA-249";
            row1220.TypeGrade = "TP321";
            row1220.ProductForm = "Wld. tube";
            row1220.AlloyUNS = "S32100";
            row1220.ClassCondition = "";
            row1220.Notes = "G5, G12, G18, W12, W14";
            row1220.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1220);

            // Row 19: SA-249, TP321, Wld. tube
            var row1221 = new OldStressRowData();
            row1221.LineNo = 19;
            row1221.MaterialId = 9468;
            row1221.SpecNo = "SA-249";
            row1221.TypeGrade = "TP321";
            row1221.ProductForm = "Wld. tube";
            row1221.AlloyUNS = "S32100";
            row1221.ClassCondition = "";
            row1221.Notes = "G5, G12, G24";
            row1221.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.2, null, null, null };
            batch.Add(row1221);

            // Row 20: SA-249, TP321, Wld. tube
            var row1222 = new OldStressRowData();
            row1222.LineNo = 20;
            row1222.MaterialId = 9469;
            row1222.SpecNo = "SA-249";
            row1222.TypeGrade = "TP321";
            row1222.ProductForm = "Wld. tube";
            row1222.AlloyUNS = "S32100";
            row1222.ClassCondition = "";
            row1222.Notes = "G12, G24";
            row1222.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.2, null, null, null };
            batch.Add(row1222);

            // Row 21: SA-312, TP321, Smls. pipe
            var row1223 = new OldStressRowData();
            row1223.LineNo = 21;
            row1223.MaterialId = 9478;
            row1223.SpecNo = "SA-312";
            row1223.TypeGrade = "TP321";
            row1223.ProductForm = "Smls. pipe";
            row1223.AlloyUNS = "S32100";
            row1223.ClassCondition = "";
            row1223.Notes = "G5, G12, G18";
            row1223.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1223);

            // Row 22: SA-312, TP321, Smls. pipe
            var row1224 = new OldStressRowData();
            row1224.LineNo = 22;
            row1224.MaterialId = 9480;
            row1224.SpecNo = "SA-312";
            row1224.TypeGrade = "TP321";
            row1224.ProductForm = "Smls. pipe";
            row1224.AlloyUNS = "S32100";
            row1224.ClassCondition = "";
            row1224.Notes = "G12, G18";
            row1224.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1224);

            // Row 23: SA-312, TP321, Smls. pipe
            var row1225 = new OldStressRowData();
            row1225.LineNo = 23;
            row1225.MaterialId = 9479;
            row1225.SpecNo = "SA-312";
            row1225.TypeGrade = "TP321";
            row1225.ProductForm = "Smls. pipe";
            row1225.AlloyUNS = "S32100";
            row1225.ClassCondition = "";
            row1225.Notes = "G5, G12, H2, W12";
            row1225.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1225);

            // Row 24: SA-312, TP321, Wld. pipe
            var row1226 = new OldStressRowData();
            row1226.LineNo = 24;
            row1226.MaterialId = 9483;
            row1226.SpecNo = "SA-312";
            row1226.TypeGrade = "TP321";
            row1226.ProductForm = "Wld. pipe";
            row1226.AlloyUNS = "S32100";
            row1226.ClassCondition = "";
            row1226.Notes = "G5, G12, G18, W14";
            row1226.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1226);

            // Row 25: SA-312, TP321, Wld. pipe
            var row1227 = new OldStressRowData();
            row1227.LineNo = 25;
            row1227.MaterialId = 9484;
            row1227.SpecNo = "SA-312";
            row1227.TypeGrade = "TP321";
            row1227.ProductForm = "Wld. pipe";
            row1227.AlloyUNS = "S32100";
            row1227.ClassCondition = "";
            row1227.Notes = "G3, G5, G12, G18";
            row1227.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.3, null, null, null };
            batch.Add(row1227);

            // Row 26: SA-312, TP321, Wld. pipe
            var row1228 = new OldStressRowData();
            row1228.LineNo = 26;
            row1228.MaterialId = 9485;
            row1228.SpecNo = "SA-312";
            row1228.TypeGrade = "TP321";
            row1228.ProductForm = "Wld. pipe";
            row1228.AlloyUNS = "S32100";
            row1228.ClassCondition = "";
            row1228.Notes = "G12, G18, W14";
            row1228.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.2, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1228);

            // Row 27: SA-312, TP321, Wld. pipe
            var row1229 = new OldStressRowData();
            row1229.LineNo = 27;
            row1229.MaterialId = 9486;
            row1229.SpecNo = "SA-312";
            row1229.TypeGrade = "TP321";
            row1229.ProductForm = "Wld. pipe";
            row1229.AlloyUNS = "S32100";
            row1229.ClassCondition = "";
            row1229.Notes = "G3, G12, G18";
            row1229.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.3, null, null, null };
            batch.Add(row1229);

            // Row 28: SA-312, TP321, Wld. pipe
            var row1230 = new OldStressRowData();
            row1230.LineNo = 28;
            row1230.MaterialId = 9481;
            row1230.SpecNo = "SA-312";
            row1230.TypeGrade = "TP321";
            row1230.ProductForm = "Wld. pipe";
            row1230.AlloyUNS = "S32100";
            row1230.ClassCondition = "";
            row1230.Notes = "G5, G12, G24";
            row1230.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.2, null, null, null };
            batch.Add(row1230);

            // Row 29: SA-312, TP321, Wld. pipe
            var row1231 = new OldStressRowData();
            row1231.LineNo = 29;
            row1231.MaterialId = 9482;
            row1231.SpecNo = "SA-312";
            row1231.TypeGrade = "TP321";
            row1231.ProductForm = "Wld. pipe";
            row1231.AlloyUNS = "S32100";
            row1231.ClassCondition = "";
            row1231.Notes = "G12, G24";
            row1231.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 8.2, 5.9, 4.3, 3.1, 2.2, 1.5, 1, 0.7, 0.4, 0.2, null, null, null };
            batch.Add(row1231);

            // Row 30: SA-358, 321, Wld. pipe
            var row1232 = new OldStressRowData();
            row1232.LineNo = 30;
            row1232.MaterialId = 9494;
            row1232.SpecNo = "SA-358";
            row1232.TypeGrade = "321";
            row1232.ProductForm = "Wld. pipe";
            row1232.AlloyUNS = "S32100";
            row1232.ClassCondition = "1";
            row1232.Notes = "G5, W12";
            row1232.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1232);

            // Row 31: SA-376, TP321, Smls. pipe
            var row1233 = new OldStressRowData();
            row1233.LineNo = 31;
            row1233.MaterialId = 9497;
            row1233.SpecNo = "SA-376";
            row1233.TypeGrade = "TP321";
            row1233.ProductForm = "Smls. pipe";
            row1233.AlloyUNS = "S32100";
            row1233.ClassCondition = "";
            row1233.Notes = "G5, G12, G18, H1";
            row1233.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1233);

            // Row 32: SA-376, TP321, Smls. pipe
            var row1234 = new OldStressRowData();
            row1234.LineNo = 32;
            row1234.MaterialId = 9496;
            row1234.SpecNo = "SA-376";
            row1234.TypeGrade = "TP321";
            row1234.ProductForm = "Smls. pipe";
            row1234.AlloyUNS = "S32100";
            row1234.ClassCondition = "";
            row1234.Notes = "G12, G18, H1";
            row1234.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1234);

            // Row 33: SA-376, TP321, Smls. pipe
            var row1235 = new OldStressRowData();
            row1235.LineNo = 33;
            row1235.MaterialId = 9495;
            row1235.SpecNo = "SA-376";
            row1235.TypeGrade = "TP321";
            row1235.ProductForm = "Smls. pipe";
            row1235.AlloyUNS = "S32100";
            row1235.ClassCondition = "";
            row1235.Notes = "G5, G12, H2";
            row1235.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1235);

            // Row 34: ..., , 
            var row1236 = new OldStressRowData();
            row1236.LineNo = 34;
            row1236.MaterialId = 9500;
            row1236.SpecNo = "...";
            row1236.TypeGrade = "";
            row1236.ProductForm = "";
            row1236.AlloyUNS = "";
            row1236.ClassCondition = "";
            row1236.Notes = "";
            row1236.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1236);

            // Row 35: ..., , 
            var row1237 = new OldStressRowData();
            row1237.LineNo = 35;
            row1237.MaterialId = 9502;
            row1237.SpecNo = "...";
            row1237.TypeGrade = "";
            row1237.ProductForm = "";
            row1237.AlloyUNS = "";
            row1237.ClassCondition = "";
            row1237.Notes = "";
            row1237.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1237);

            // Row 36: SA-403, 321, Smls. & wld. fittings
            var row1238 = new OldStressRowData();
            row1238.LineNo = 36;
            row1238.MaterialId = 9504;
            row1238.SpecNo = "SA-403";
            row1238.TypeGrade = "321";
            row1238.ProductForm = "Smls. & wld. fittings";
            row1238.AlloyUNS = "S32100";
            row1238.ClassCondition = "";
            row1238.Notes = "G5, G12, W12, W15";
            row1238.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1238);

            // Row 37: ..., , 
            var row1239 = new OldStressRowData();
            row1239.LineNo = 37;
            row1239.MaterialId = 9501;
            row1239.SpecNo = "...";
            row1239.TypeGrade = "";
            row1239.ProductForm = "";
            row1239.AlloyUNS = "";
            row1239.ClassCondition = "";
            row1239.Notes = "";
            row1239.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1239);

            // Row 38: ..., , 
            var row1240 = new OldStressRowData();
            row1240.LineNo = 38;
            row1240.MaterialId = 9503;
            row1240.SpecNo = "...";
            row1240.TypeGrade = "";
            row1240.ProductForm = "";
            row1240.AlloyUNS = "";
            row1240.ClassCondition = "";
            row1240.Notes = "";
            row1240.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1240);

            // Row 39: ..., , 
            var row1241 = new OldStressRowData();
            row1241.LineNo = 39;
            row1241.MaterialId = 9505;
            row1241.SpecNo = "...";
            row1241.TypeGrade = "";
            row1241.ProductForm = "";
            row1241.AlloyUNS = "";
            row1241.ClassCondition = "";
            row1241.Notes = "";
            row1241.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1241);

            // Row 1: SA-409, TP321, Wld. pipe
            var row1242 = new OldStressRowData();
            row1242.LineNo = 1;
            row1242.MaterialId = 9512;
            row1242.SpecNo = "SA-409";
            row1242.TypeGrade = "TP321";
            row1242.ProductForm = "Wld. pipe";
            row1242.AlloyUNS = "S32100";
            row1242.ClassCondition = "";
            row1242.Notes = "G5, W12";
            row1242.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1242);

            // Row 2: SA-479, 321, Bar
            var row1243 = new OldStressRowData();
            row1243.LineNo = 2;
            row1243.MaterialId = 9514;
            row1243.SpecNo = "SA-479";
            row1243.TypeGrade = "321";
            row1243.ProductForm = "Bar";
            row1243.AlloyUNS = "S32100";
            row1243.ClassCondition = "";
            row1243.Notes = "G5, G12, G18";
            row1243.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1243);

            // Row 3: SA-479, 321, Bar
            var row1244 = new OldStressRowData();
            row1244.LineNo = 3;
            row1244.MaterialId = 9515;
            row1244.SpecNo = "SA-479";
            row1244.TypeGrade = "321";
            row1244.ProductForm = "Bar";
            row1244.AlloyUNS = "S32100";
            row1244.ClassCondition = "";
            row1244.Notes = "G12, G18, G22";
            row1244.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1244);

            // Row 4: SA-479, 321, Bar
            var row1245 = new OldStressRowData();
            row1245.LineNo = 4;
            row1245.MaterialId = 9513;
            row1245.SpecNo = "SA-479";
            row1245.TypeGrade = "321";
            row1245.ProductForm = "Bar";
            row1245.AlloyUNS = "S32100";
            row1245.ClassCondition = "";
            row1245.Notes = "G5, G12, G22, H2";
            row1245.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 13.8, 9.6, 6.9, 5, 3.6, 2.6, 1.7, 1.1, 0.8, 0.5, 0.3, null, null, null };
            batch.Add(row1245);

            // Row 5: SA-813, TP321, Wld. pipe
            var row1246 = new OldStressRowData();
            row1246.LineNo = 5;
            row1246.MaterialId = 9518;
            row1246.SpecNo = "SA-813";
            row1246.TypeGrade = "TP321";
            row1246.ProductForm = "Wld. pipe";
            row1246.AlloyUNS = "S32100";
            row1246.ClassCondition = "";
            row1246.Notes = "G5, W12";
            row1246.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1246);

            // Row 6: SA-814, TP321, Wld. pipe
            var row1247 = new OldStressRowData();
            row1247.LineNo = 6;
            row1247.MaterialId = 9520;
            row1247.SpecNo = "SA-814";
            row1247.TypeGrade = "TP321";
            row1247.ProductForm = "Wld. pipe";
            row1247.AlloyUNS = "S32100";
            row1247.ClassCondition = "";
            row1247.Notes = "G5, W12";
            row1247.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1247);

            // Row 7: SA-182, F321H, Forgings
            var row1248 = new OldStressRowData();
            row1248.LineNo = 7;
            row1248.MaterialId = 9456;
            row1248.SpecNo = "SA-182";
            row1248.TypeGrade = "F321H";
            row1248.ProductForm = "Forgings";
            row1248.AlloyUNS = "S32109";
            row1248.ClassCondition = "";
            row1248.Notes = "G5, G18";
            row1248.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1248);

            // Row 8: SA-182, F321H, Forgings
            var row1249 = new OldStressRowData();
            row1249.LineNo = 8;
            row1249.MaterialId = 9457;
            row1249.SpecNo = "SA-182";
            row1249.TypeGrade = "F321H";
            row1249.ProductForm = "Forgings";
            row1249.AlloyUNS = "S32109";
            row1249.ClassCondition = "";
            row1249.Notes = "G18";
            row1249.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1249);

            // Row 9: SA-213, TP321H, Smls. tube
            var row1250 = new OldStressRowData();
            row1250.LineNo = 9;
            row1250.MaterialId = 9461;
            row1250.SpecNo = "SA-213";
            row1250.TypeGrade = "TP321H";
            row1250.ProductForm = "Smls. tube";
            row1250.AlloyUNS = "S32109";
            row1250.ClassCondition = "";
            row1250.Notes = "G5, G18";
            row1250.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1250);

            // Row 10: SA-213, TP321H, Smls. tube
            var row1251 = new OldStressRowData();
            row1251.LineNo = 10;
            row1251.MaterialId = 9462;
            row1251.SpecNo = "SA-213";
            row1251.TypeGrade = "TP321H";
            row1251.ProductForm = "Smls. tube";
            row1251.AlloyUNS = "S32109";
            row1251.ClassCondition = "";
            row1251.Notes = "G18";
            row1251.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1251);

            // Row 11: SA-240, 321H, Plate
            var row1252 = new OldStressRowData();
            row1252.LineNo = 11;
            row1252.MaterialId = 9466;
            row1252.SpecNo = "SA-240";
            row1252.TypeGrade = "321H";
            row1252.ProductForm = "Plate";
            row1252.AlloyUNS = "S32109";
            row1252.ClassCondition = "";
            row1252.Notes = "G5";
            row1252.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1252);

            // Row 12: SA-249, TP321H, Wld. tube
            var row1253 = new OldStressRowData();
            row1253.LineNo = 12;
            row1253.MaterialId = 9475;
            row1253.SpecNo = "SA-249";
            row1253.TypeGrade = "TP321H";
            row1253.ProductForm = "Wld. tube";
            row1253.AlloyUNS = "S32109";
            row1253.ClassCondition = "";
            row1253.Notes = "G18, W14";
            row1253.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.2, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.8, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1253);

            // Row 13: SA-249, TP321H, Wld. tube
            var row1254 = new OldStressRowData();
            row1254.LineNo = 13;
            row1254.MaterialId = 9476;
            row1254.SpecNo = "SA-249";
            row1254.TypeGrade = "TP321H";
            row1254.ProductForm = "Wld. tube";
            row1254.AlloyUNS = "S32109";
            row1254.ClassCondition = "";
            row1254.Notes = "G3, G18";
            row1254.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 9.9, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1254);

            // Row 14: SA-249, TP321H, Wld. tube
            var row1255 = new OldStressRowData();
            row1255.LineNo = 14;
            row1255.MaterialId = 9477;
            row1255.SpecNo = "SA-249";
            row1255.TypeGrade = "TP321H";
            row1255.ProductForm = "Wld. tube";
            row1255.AlloyUNS = "S32109";
            row1255.ClassCondition = "";
            row1255.Notes = "G5, G18, W12, W14";
            row1255.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1255);

            // Row 15: SA-249, TP321H, Wld. tube
            var row1256 = new OldStressRowData();
            row1256.LineNo = 15;
            row1256.MaterialId = 9473;
            row1256.SpecNo = "SA-249";
            row1256.TypeGrade = "TP321H";
            row1256.ProductForm = "Wld. tube";
            row1256.AlloyUNS = "S32109";
            row1256.ClassCondition = "";
            row1256.Notes = "G3, G5, G18, G24";
            row1256.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 10.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1256);

            // Row 16: SA-249, TP321H, Wld. tube
            var row1257 = new OldStressRowData();
            row1257.LineNo = 16;
            row1257.MaterialId = 9474;
            row1257.SpecNo = "SA-249";
            row1257.TypeGrade = "TP321H";
            row1257.ProductForm = "Wld. tube";
            row1257.AlloyUNS = "S32109";
            row1257.ClassCondition = "";
            row1257.Notes = "G24";
            row1257.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 9.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1257);

            // Row 17: SA-312, TP321H, Smls. pipe
            var row1258 = new OldStressRowData();
            row1258.LineNo = 17;
            row1258.MaterialId = 9487;
            row1258.SpecNo = "SA-312";
            row1258.TypeGrade = "TP321H";
            row1258.ProductForm = "Smls. pipe";
            row1258.AlloyUNS = "S32109";
            row1258.ClassCondition = "";
            row1258.Notes = "G5, G18, W12";
            row1258.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1258);

            // Row 18: SA-312, TP321H, Smls. pipe
            var row1259 = new OldStressRowData();
            row1259.LineNo = 18;
            row1259.MaterialId = 9488;
            row1259.SpecNo = "SA-312";
            row1259.TypeGrade = "TP321H";
            row1259.ProductForm = "Smls. pipe";
            row1259.AlloyUNS = "S32109";
            row1259.ClassCondition = "";
            row1259.Notes = "G18";
            row1259.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1259);

            // Row 19: SA-312, TP321H, Wld. pipe
            var row1260 = new OldStressRowData();
            row1260.LineNo = 19;
            row1260.MaterialId = 9491;
            row1260.SpecNo = "SA-312";
            row1260.TypeGrade = "TP321H";
            row1260.ProductForm = "Wld. pipe";
            row1260.AlloyUNS = "S32109";
            row1260.ClassCondition = "";
            row1260.Notes = "G5, G18, W14";
            row1260.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1260);

            // Row 20: SA-312, TP321H, Wld. pipe
            var row1261 = new OldStressRowData();
            row1261.LineNo = 20;
            row1261.MaterialId = 9492;
            row1261.SpecNo = "SA-312";
            row1261.TypeGrade = "TP321H";
            row1261.ProductForm = "Wld. pipe";
            row1261.AlloyUNS = "S32109";
            row1261.ClassCondition = "";
            row1261.Notes = "G18, W14";
            row1261.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.2, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.8, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1261);

            // Row 21: SA-312, TP321H, Wld. pipe
            var row1262 = new OldStressRowData();
            row1262.LineNo = 21;
            row1262.MaterialId = 9493;
            row1262.SpecNo = "SA-312";
            row1262.TypeGrade = "TP321H";
            row1262.ProductForm = "Wld. pipe";
            row1262.AlloyUNS = "S32109";
            row1262.ClassCondition = "";
            row1262.Notes = "G3, G18";
            row1262.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 9.9, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1262);

            // Row 22: SA-312, TP321H, Wld. pipe
            var row1263 = new OldStressRowData();
            row1263.LineNo = 22;
            row1263.MaterialId = 9489;
            row1263.SpecNo = "SA-312";
            row1263.TypeGrade = "TP321H";
            row1263.ProductForm = "Wld. pipe";
            row1263.AlloyUNS = "S32109";
            row1263.ClassCondition = "";
            row1263.Notes = "G3, G5, G18, G24";
            row1263.StressValues = new double?[] { 16, null, 15.1, null, 14.2, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.9, 13.8, 13.6, 10.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1263);

            // Row 23: SA-312, TP321H, Wld. pipe
            var row1264 = new OldStressRowData();
            row1264.LineNo = 23;
            row1264.MaterialId = 9490;
            row1264.SpecNo = "SA-312";
            row1264.TypeGrade = "TP321H";
            row1264.ProductForm = "Wld. pipe";
            row1264.AlloyUNS = "S32109";
            row1264.ClassCondition = "";
            row1264.Notes = "G24";
            row1264.StressValues = new double?[] { 16, null, 15.1, null, 14, 13, 12.2, 11.5, 11.3, 11, 10.8, 10.6, 10.5, 10.4, 10.3, 10.2, 9.5, 7.7, 5.9, 4.6, 3.5, 2.7, 2.1, 1.6, 1.3, 1, null, null, null };
            batch.Add(row1264);

            // Row 24: SA-376, TP321H, Smls. pipe
            var row1265 = new OldStressRowData();
            row1265.LineNo = 24;
            row1265.MaterialId = 9498;
            row1265.SpecNo = "SA-376";
            row1265.TypeGrade = "TP321H";
            row1265.ProductForm = "Smls. pipe";
            row1265.AlloyUNS = "S32109";
            row1265.ClassCondition = "";
            row1265.Notes = "G5, G18, H1";
            row1265.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1265);

            // Row 25: SA-376, TP321H, Smls. pipe
            var row1266 = new OldStressRowData();
            row1266.LineNo = 25;
            row1266.MaterialId = 9499;
            row1266.SpecNo = "SA-376";
            row1266.TypeGrade = "TP321H";
            row1266.ProductForm = "Smls. pipe";
            row1266.AlloyUNS = "S32109";
            row1266.ClassCondition = "";
            row1266.Notes = "G18, H1";
            row1266.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1266);

            // Row 26: ..., , 
            var row1267 = new OldStressRowData();
            row1267.LineNo = 26;
            row1267.MaterialId = 9506;
            row1267.SpecNo = "...";
            row1267.TypeGrade = "";
            row1267.ProductForm = "";
            row1267.AlloyUNS = "";
            row1267.ClassCondition = "";
            row1267.Notes = "";
            row1267.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1267);

            // Row 27: ..., , 
            var row1268 = new OldStressRowData();
            row1268.LineNo = 27;
            row1268.MaterialId = 9508;
            row1268.SpecNo = "...";
            row1268.TypeGrade = "";
            row1268.ProductForm = "";
            row1268.AlloyUNS = "";
            row1268.ClassCondition = "";
            row1268.Notes = "";
            row1268.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1268);

            // Row 28: SA-403, 321H, Smls. & wld. fittings
            var row1269 = new OldStressRowData();
            row1269.LineNo = 28;
            row1269.MaterialId = 9511;
            row1269.SpecNo = "SA-403";
            row1269.TypeGrade = "321H";
            row1269.ProductForm = "Smls. & wld. fittings";
            row1269.AlloyUNS = "S32109";
            row1269.ClassCondition = "";
            row1269.Notes = "G5, W12, W15";
            row1269.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1269);

            // Row 29: ..., , 
            var row1270 = new OldStressRowData();
            row1270.LineNo = 29;
            row1270.MaterialId = 9507;
            row1270.SpecNo = "...";
            row1270.TypeGrade = "";
            row1270.ProductForm = "";
            row1270.AlloyUNS = "";
            row1270.ClassCondition = "";
            row1270.Notes = "";
            row1270.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1270);

            // Row 30: ..., , 
            var row1271 = new OldStressRowData();
            row1271.LineNo = 30;
            row1271.MaterialId = 9509;
            row1271.SpecNo = "...";
            row1271.TypeGrade = "";
            row1271.ProductForm = "";
            row1271.AlloyUNS = "";
            row1271.ClassCondition = "";
            row1271.Notes = "";
            row1271.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1271);

            // Row 31: ..., , 
            var row1272 = new OldStressRowData();
            row1272.LineNo = 31;
            row1272.MaterialId = 9510;
            row1272.SpecNo = "...";
            row1272.TypeGrade = "";
            row1272.ProductForm = "";
            row1272.AlloyUNS = "";
            row1272.ClassCondition = "";
            row1272.Notes = "";
            row1272.StressValues = new double?[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1272);

            // Row 32: SA-479, 321H, Bar
            var row1273 = new OldStressRowData();
            row1273.LineNo = 32;
            row1273.MaterialId = 9517;
            row1273.SpecNo = "SA-479";
            row1273.TypeGrade = "321H";
            row1273.ProductForm = "Bar";
            row1273.AlloyUNS = "S32109";
            row1273.ClassCondition = "";
            row1273.Notes = "G18";
            row1273.StressValues = new double?[] { 18.8, null, 17.8, null, 16.5, 15.3, 14.3, 13.5, 13.3, 12.9, 12.7, 12.5, 12.4, 12.3, 12.1, 12, 11.7, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1273);

            // Row 33: SA-479, 321H, Bar
            var row1274 = new OldStressRowData();
            row1274.LineNo = 33;
            row1274.MaterialId = 9516;
            row1274.SpecNo = "SA-479";
            row1274.TypeGrade = "321H";
            row1274.ProductForm = "Bar";
            row1274.AlloyUNS = "S32109";
            row1274.ClassCondition = "";
            row1274.Notes = "G5, G18";
            row1274.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.3, 16, 12.3, 9.1, 6.9, 5.4, 4.1, 3.2, 2.5, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1274);

            // Row 34: SA-813, TP321H, Wld. pipe
            var row1275 = new OldStressRowData();
            row1275.LineNo = 34;
            row1275.MaterialId = 9519;
            row1275.SpecNo = "SA-813";
            row1275.TypeGrade = "TP321H";
            row1275.ProductForm = "Wld. pipe";
            row1275.AlloyUNS = "S32109";
            row1275.ClassCondition = "";
            row1275.Notes = "G5, W12";
            row1275.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1275);

            // Row 35: SA-814, TP321H, Wld. pipe
            var row1276 = new OldStressRowData();
            row1276.LineNo = 35;
            row1276.MaterialId = 9521;
            row1276.SpecNo = "SA-814";
            row1276.TypeGrade = "TP321H";
            row1276.ProductForm = "Wld. pipe";
            row1276.AlloyUNS = "S32109";
            row1276.ClassCondition = "";
            row1276.Notes = "G5, W12";
            row1276.StressValues = new double?[] { 18.8, null, 17.8, null, 16.7, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, 16.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1276);

            // Row 36: SA-240, 305, Plate
            var row1277 = new OldStressRowData();
            row1277.LineNo = 36;
            row1277.MaterialId = 9522;
            row1277.SpecNo = "SA-240";
            row1277.TypeGrade = "305";
            row1277.ProductForm = "Plate";
            row1277.AlloyUNS = "S30500";
            row1277.ClassCondition = "";
            row1277.Notes = "G5";
            row1277.StressValues = new double?[] { 18.8, null, 17.8, null, 16.6, 16.2, 15.9, 15.9, 15.9, 15.9, 15.6, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1277);

            // Row 37: SA-182, F317L, Forgings
            var row1278 = new OldStressRowData();
            row1278.LineNo = 37;
            row1278.MaterialId = 9787;
            row1278.SpecNo = "SA-182";
            row1278.TypeGrade = "F317L";
            row1278.ProductForm = "Forgings";
            row1278.AlloyUNS = "S31703";
            row1278.ClassCondition = "";
            row1278.Notes = "G5";
            row1278.StressValues = new double?[] { 16.3, null, 15.7, null, 14.8, 14.5, 14.3, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1278);

            // Row 38: SA-182, F317L, Forgings
            var row1279 = new OldStressRowData();
            row1279.LineNo = 38;
            row1279.MaterialId = 9788;
            row1279.SpecNo = "SA-182";
            row1279.TypeGrade = "F317L";
            row1279.ProductForm = "Forgings";
            row1279.AlloyUNS = "S31703";
            row1279.ClassCondition = "";
            row1279.Notes = "";
            row1279.StressValues = new double?[] { 16.3, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1279);

            // Row 39: SA-182, F317L, Forgings
            var row1280 = new OldStressRowData();
            row1280.LineNo = 39;
            row1280.MaterialId = 9789;
            row1280.SpecNo = "SA-182";
            row1280.TypeGrade = "F317L";
            row1280.ProductForm = "Forgings";
            row1280.AlloyUNS = "S31703";
            row1280.ClassCondition = "";
            row1280.Notes = "G5";
            row1280.StressValues = new double?[] { 16.7, null, 16.7, null, 16, 15.6, 14.8, 14, 13.8, 13.5, 13.2, 13, 12.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1280);

            // Row 40: SA-182, F317L, Forgings
            var row1281 = new OldStressRowData();
            row1281.LineNo = 40;
            row1281.MaterialId = 9790;
            row1281.SpecNo = "SA-182";
            row1281.TypeGrade = "F317L";
            row1281.ProductForm = "Forgings";
            row1281.AlloyUNS = "S31703";
            row1281.ClassCondition = "";
            row1281.Notes = "";
            row1281.StressValues = new double?[] { 16.7, null, 14.1, null, 12.7, 11.7, 10.9, 10.4, 10.2, 10, 9.8, 9.6, 9.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1281);

            // Row 1: SA-182, F317, Forgings
            var row1282 = new OldStressRowData();
            row1282.LineNo = 1;
            row1282.MaterialId = 9525;
            row1282.SpecNo = "SA-182";
            row1282.TypeGrade = "F317";
            row1282.ProductForm = "Forgings";
            row1282.AlloyUNS = "S31700";
            row1282.ClassCondition = "";
            row1282.Notes = "G5, G12";
            row1282.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1282);

            // Row 2: SA-182, F317, Forgings
            var row1283 = new OldStressRowData();
            row1283.LineNo = 2;
            row1283.MaterialId = 9526;
            row1283.SpecNo = "SA-182";
            row1283.TypeGrade = "F317";
            row1283.ProductForm = "Forgings";
            row1283.AlloyUNS = "S31700";
            row1283.ClassCondition = "";
            row1283.Notes = "G12";
            row1283.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1283);

            // Row 3: SA-240, 317, Plate
            var row1284 = new OldStressRowData();
            row1284.LineNo = 3;
            row1284.MaterialId = 9527;
            row1284.SpecNo = "SA-240";
            row1284.TypeGrade = "317";
            row1284.ProductForm = "Plate";
            row1284.AlloyUNS = "S31700";
            row1284.ClassCondition = "";
            row1284.Notes = "G5, G12";
            row1284.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1284);

            // Row 4: SA-240, 317, Plate
            var row1285 = new OldStressRowData();
            row1285.LineNo = 4;
            row1285.MaterialId = 9528;
            row1285.SpecNo = "SA-240";
            row1285.TypeGrade = "317";
            row1285.ProductForm = "Plate";
            row1285.AlloyUNS = "S31700";
            row1285.ClassCondition = "";
            row1285.Notes = "G12";
            row1285.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1285);

            // Row 5: SA-240, 317L, Plate
            var row1286 = new OldStressRowData();
            row1286.LineNo = 5;
            row1286.MaterialId = 9529;
            row1286.SpecNo = "SA-240";
            row1286.TypeGrade = "317L";
            row1286.ProductForm = "Plate";
            row1286.AlloyUNS = "S31703";
            row1286.ClassCondition = "";
            row1286.Notes = "G5";
            row1286.StressValues = new double?[] { 18.8, null, 18.1, null, 17.1, 16.7, 16.5, 16.5, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1286);

            // Row 6: SA-240, 317L, Plate
            var row1287 = new OldStressRowData();
            row1287.LineNo = 6;
            row1287.MaterialId = 9530;
            row1287.SpecNo = "SA-240";
            row1287.TypeGrade = "317L";
            row1287.ProductForm = "Plate";
            row1287.AlloyUNS = "S31703";
            row1287.ClassCondition = "";
            row1287.Notes = "";
            row1287.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, 13.1, 12.5, 12.3, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1287);

            // Row 7: SA-249, TP317, Wld. tube
            var row1288 = new OldStressRowData();
            row1288.LineNo = 7;
            row1288.MaterialId = 9523;
            row1288.SpecNo = "SA-249";
            row1288.TypeGrade = "TP317";
            row1288.ProductForm = "Wld. tube";
            row1288.AlloyUNS = "S31700";
            row1288.ClassCondition = "";
            row1288.Notes = "G5, W12";
            row1288.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1288);

            // Row 8: SA-249, TP317, Wld. tube
            var row1289 = new OldStressRowData();
            row1289.LineNo = 8;
            row1289.MaterialId = 9531;
            row1289.SpecNo = "SA-249";
            row1289.TypeGrade = "TP317";
            row1289.ProductForm = "Wld. tube";
            row1289.AlloyUNS = "S31700";
            row1289.ClassCondition = "";
            row1289.Notes = "G5, G12, G24";
            row1289.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1289);

            // Row 9: SA-249, TP317, Wld. tube
            var row1290 = new OldStressRowData();
            row1290.LineNo = 9;
            row1290.MaterialId = 9532;
            row1290.SpecNo = "SA-249";
            row1290.TypeGrade = "TP317";
            row1290.ProductForm = "Wld. tube";
            row1290.AlloyUNS = "S31700";
            row1290.ClassCondition = "";
            row1290.Notes = "G12, G24";
            row1290.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1290);

            // Row 10: SA-249, TP317L, Wld. tube
            var row1291 = new OldStressRowData();
            row1291.LineNo = 10;
            row1291.MaterialId = 9791;
            row1291.SpecNo = "SA-249";
            row1291.TypeGrade = "TP317L";
            row1291.ProductForm = "Wld. tube";
            row1291.AlloyUNS = "S31703";
            row1291.ClassCondition = "";
            row1291.Notes = "G5, G24";
            row1291.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1291);

            // Row 11: SA-249, TP317L, Wld. tube
            var row1292 = new OldStressRowData();
            row1292.LineNo = 11;
            row1292.MaterialId = 9792;
            row1292.SpecNo = "SA-249";
            row1292.TypeGrade = "TP317L";
            row1292.ProductForm = "Wld. tube";
            row1292.AlloyUNS = "S31703";
            row1292.ClassCondition = "";
            row1292.Notes = "G24";
            row1292.StressValues = new double?[] { 15.9, null, 14.4, null, 12.9, 11.9, 11.1, 10.6, 10.5, 10.2, 9.9, 9.8, 9.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1292);

            // Row 12: SA-312, TP317, Wld. & smls. pipe
            var row1293 = new OldStressRowData();
            row1293.LineNo = 12;
            row1293.MaterialId = 9524;
            row1293.SpecNo = "SA-312";
            row1293.TypeGrade = "TP317";
            row1293.ProductForm = "Wld. & smls. pipe";
            row1293.AlloyUNS = "S31700";
            row1293.ClassCondition = "";
            row1293.Notes = "G5, W12";
            row1293.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1293);

            // Row 13: SA-312, TP317, Smls. pipe
            var row1294 = new OldStressRowData();
            row1294.LineNo = 13;
            row1294.MaterialId = 9536;
            row1294.SpecNo = "SA-312";
            row1294.TypeGrade = "TP317";
            row1294.ProductForm = "Smls. pipe";
            row1294.AlloyUNS = "S31700";
            row1294.ClassCondition = "";
            row1294.Notes = "G5, G12";
            row1294.StressValues = new double?[] { 18.8, null, 18.8, null, 18.4, 18.1, 18, 17, 16.7, 16.3, 16.1, 15.9, 15.7, 15.6, 15.4, 15.3, 15.1, 12.4, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1294);

            // Row 14: SA-312, TP317, Smls. pipe
            var row1295 = new OldStressRowData();
            row1295.LineNo = 14;
            row1295.MaterialId = 9533;
            row1295.SpecNo = "SA-312";
            row1295.TypeGrade = "TP317";
            row1295.ProductForm = "Smls. pipe";
            row1295.AlloyUNS = "S31700";
            row1295.ClassCondition = "";
            row1295.Notes = "G12";
            row1295.StressValues = new double?[] { 18.8, null, 17.7, null, 15.6, 14.3, 13.3, 12.6, 12.3, 12.1, 11.9, 11.7, 11.6, 11.5, 11.4, 11.3, 11.2, 11, 9.8, 7.4, 5.5, 4.1, 3.1, 2.3, 1.7, 1.3, null, null, null };
            batch.Add(row1295);

            // Row 15: SA-312, TP317, Wld. pipe
            var row1296 = new OldStressRowData();
            row1296.LineNo = 15;
            row1296.MaterialId = 9534;
            row1296.SpecNo = "SA-312";
            row1296.TypeGrade = "TP317";
            row1296.ProductForm = "Wld. pipe";
            row1296.AlloyUNS = "S31700";
            row1296.ClassCondition = "";
            row1296.Notes = "G5, G12, G24";
            row1296.StressValues = new double?[] { 16, null, 16, null, 15.6, 15.4, 15.3, 14.5, 14.2, 13.9, 13.7, 13.5, 13.4, 13.2, 13.1, 13, 12.8, 10.5, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1296);

            // Row 16: SA-312, TP317, Wld. pipe
            var row1297 = new OldStressRowData();
            row1297.LineNo = 16;
            row1297.MaterialId = 9535;
            row1297.SpecNo = "SA-312";
            row1297.TypeGrade = "TP317";
            row1297.ProductForm = "Wld. pipe";
            row1297.AlloyUNS = "S31700";
            row1297.ClassCondition = "";
            row1297.Notes = "G12, G24";
            row1297.StressValues = new double?[] { 16, null, 15, null, 13.3, 12.2, 11.3, 10.7, 10.5, 10.3, 10.1, 9.9, 9.9, 9.8, 9.7, 9.6, 9.5, 9.4, 8.3, 6.3, 4.7, 3.5, 2.6, 1.9, 1.5, 1.1, null, null, null };
            batch.Add(row1297);

            // Row 17: SA-312, TP317L, Smls. pipe
            var row1298 = new OldStressRowData();
            row1298.LineNo = 17;
            row1298.MaterialId = 9793;
            row1298.SpecNo = "SA-312";
            row1298.TypeGrade = "TP317L";
            row1298.ProductForm = "Smls. pipe";
            row1298.AlloyUNS = "S31703";
            row1298.ClassCondition = "";
            row1298.Notes = "G5";
            row1298.StressValues = new double?[] { 18.8, null, 18.1, null, 17.1, 16.7, 16.5, 16.5, 16.5, 16.2, 15.8, 15.5, 15.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1298);

            // Row 18: SA-312, TP317L, Smls. pipe
            var row1299 = new OldStressRowData();
            row1299.LineNo = 18;
            row1299.MaterialId = 9794;
            row1299.SpecNo = "SA-312";
            row1299.TypeGrade = "TP317L";
            row1299.ProductForm = "Smls. pipe";
            row1299.AlloyUNS = "S31703";
            row1299.ClassCondition = "";
            row1299.Notes = "";
            row1299.StressValues = new double?[] { 18.8, null, 16.9, null, 15.2, 14, 13.1, 12.5, 12.3, 12, 11.7, 11.5, 11.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1299);

            // Row 19: SA-312, TP317L, Wld. pipe
            var row1300 = new OldStressRowData();
            row1300.LineNo = 19;
            row1300.MaterialId = 9795;
            row1300.SpecNo = "SA-312";
            row1300.TypeGrade = "TP317L";
            row1300.ProductForm = "Wld. pipe";
            row1300.AlloyUNS = "S31703";
            row1300.ClassCondition = "";
            row1300.Notes = "G5, G24";
            row1300.StressValues = new double?[] { 15.9, null, 15.4, null, 14.5, 14.2, 14, 14, 14, 13.8, 13.4, 13.2, 12.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1300);

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
