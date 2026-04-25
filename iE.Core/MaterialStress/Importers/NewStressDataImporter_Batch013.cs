using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Core.MaterialStress.Importers
{
    public static class NewStressDataImporter_Batch013
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

            // Row 26: SA-249, TPXM-19, Wld. tube
            var row1201 = new OldStressRowData();
            row1201.LineNo = 26;
            row1201.MaterialId = 9619;
            row1201.SpecNo = "SA-249";
            row1201.TypeGrade = "TPXM-19";
            row1201.ProductForm = "Wld. tube";
            row1201.AlloyUNS = "S20910";
            row1201.ClassCondition = "";
            row1201.Notes = "G5, W12";
            row1201.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1201);

            // Row 27: SA-249, TPXM-19, Wld. tube
            var row1202 = new OldStressRowData();
            row1202.LineNo = 27;
            row1202.MaterialId = 9620;
            row1202.SpecNo = "SA-249";
            row1202.TypeGrade = "TPXM-19";
            row1202.ProductForm = "Wld. tube";
            row1202.AlloyUNS = "S20910";
            row1202.ClassCondition = "";
            row1202.Notes = "G24, T8";
            row1202.StressValues = new double?[] { 24.3, null, 24.1, null, 22.9, 22.1, 21.6, 21.2, 20.9, 20.6, 20.3, 20, 19.8, 19.5, 19.3, 19.1, 18.9, 17.3, 11.1, 7.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1202);

            // Row 28: SA-312, TPXM-19, Smls. & wld. pipe
            var row1203 = new OldStressRowData();
            row1203.LineNo = 28;
            row1203.MaterialId = 9621;
            row1203.SpecNo = "SA-312";
            row1203.TypeGrade = "TPXM-19";
            row1203.ProductForm = "Smls. & wld. pipe";
            row1203.AlloyUNS = "S20910";
            row1203.ClassCondition = "";
            row1203.Notes = "G5, W12";
            row1203.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1203);

            // Row 29: SA-312, TPXM-19, Smls. pipe
            var row1204 = new OldStressRowData();
            row1204.LineNo = 29;
            row1204.MaterialId = 9623;
            row1204.SpecNo = "SA-312";
            row1204.TypeGrade = "TPXM-19";
            row1204.ProductForm = "Smls. pipe";
            row1204.AlloyUNS = "S20910";
            row1204.ClassCondition = "";
            row1204.Notes = "T8";
            row1204.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25, 24.6, 24.2, 23.9, 23.5, 23.3, 23, 22.7, 22.5, 22.2, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1204);

            // Row 30: SA-312, TPXM-19, Wld. pipe
            var row1205 = new OldStressRowData();
            row1205.LineNo = 30;
            row1205.MaterialId = 9622;
            row1205.SpecNo = "SA-312";
            row1205.TypeGrade = "TPXM-19";
            row1205.ProductForm = "Wld. pipe";
            row1205.AlloyUNS = "S20910";
            row1205.ClassCondition = "";
            row1205.Notes = "G24, T8";
            row1205.StressValues = new double?[] { 24.3, null, 24.1, null, 22.9, 22.1, 21.6, 21.2, 20.9, 20.6, 20.3, 20, 19.8, 19.5, 19.3, 19.1, 18.9, 17.3, 11.1, 7.1, null, null, null, null, null, null, null, null, null };
            batch.Add(row1205);

            // Row 31: SA-358, XM-19, Wld. pipe
            var row1206 = new OldStressRowData();
            row1206.LineNo = 31;
            row1206.MaterialId = 9624;
            row1206.SpecNo = "SA-358";
            row1206.TypeGrade = "XM-19";
            row1206.ProductForm = "Wld. pipe";
            row1206.AlloyUNS = "S20910";
            row1206.ClassCondition = "1";
            row1206.Notes = "G5, W12";
            row1206.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1206);

            // Row 32: SA-403, XM-19, Smls. & wld. fittings
            var row1207 = new OldStressRowData();
            row1207.LineNo = 32;
            row1207.MaterialId = 9626;
            row1207.SpecNo = "SA-403";
            row1207.TypeGrade = "XM-19";
            row1207.ProductForm = "Smls. & wld. fittings";
            row1207.AlloyUNS = "S20910";
            row1207.ClassCondition = "";
            row1207.Notes = "G5, T8, W12, W14";
            row1207.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, 23.9, 23.6, 23.2, 22.8, 22.3, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1207);

            // Row 33: SA-479, XM-19, Bar
            var row1208 = new OldStressRowData();
            row1208.LineNo = 33;
            row1208.MaterialId = 9630;
            row1208.SpecNo = "SA-479";
            row1208.TypeGrade = "XM-19";
            row1208.ProductForm = "Bar";
            row1208.AlloyUNS = "S20910";
            row1208.ClassCondition = "";
            row1208.Notes = "G5, G22, T8";
            row1208.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, 23.9, 23.6, 23.2, 22.8, 22.3, 20.4, 13, 8.3, null, null, null, null, null, null, null, null, null };
            batch.Add(row1208);

            // Row 34: SA-813, TPXM-19, Wld. pipe
            var row1209 = new OldStressRowData();
            row1209.LineNo = 34;
            row1209.MaterialId = 9631;
            row1209.SpecNo = "SA-813";
            row1209.TypeGrade = "TPXM-19";
            row1209.ProductForm = "Wld. pipe";
            row1209.AlloyUNS = "S20910";
            row1209.ClassCondition = "";
            row1209.Notes = "G5, W12";
            row1209.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1209);

            // Row 35: SA-814, TPXM-19, Wld. pipe
            var row1210 = new OldStressRowData();
            row1210.LineNo = 35;
            row1210.MaterialId = 9632;
            row1210.SpecNo = "SA-814";
            row1210.TypeGrade = "TPXM-19";
            row1210.ProductForm = "Wld. pipe";
            row1210.AlloyUNS = "S20910";
            row1210.ClassCondition = "";
            row1210.Notes = "G5, W12";
            row1210.StressValues = new double?[] { 28.6, null, 28.4, null, 26.9, 26, 25.5, 25.1, 24.9, 24.7, 24.5, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1210);

            // Row 36: SA-240, , Plate
            var row1211 = new OldStressRowData();
            row1211.LineNo = 36;
            row1211.MaterialId = 9633;
            row1211.SpecNo = "SA-240";
            row1211.TypeGrade = "";
            row1211.ProductForm = "Plate";
            row1211.AlloyUNS = "S32304";
            row1211.ClassCondition = "";
            row1211.Notes = "G32";
            row1211.StressValues = new double?[] { 24.9, null, 24, null, 22.5, 21.7, 21.3, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1211);

            // Row 37: SA-789, , Smls. tube
            var row1212 = new OldStressRowData();
            row1212.LineNo = 37;
            row1212.MaterialId = 9634;
            row1212.SpecNo = "SA-789";
            row1212.TypeGrade = "";
            row1212.ProductForm = "Smls. tube";
            row1212.AlloyUNS = "S32304";
            row1212.ClassCondition = "";
            row1212.Notes = "G32";
            row1212.StressValues = new double?[] { 24.9, null, 24, null, 22.5, 21.7, 21.3, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1212);

            // Row 38: SA-789, , Wld. tube
            var row1213 = new OldStressRowData();
            row1213.LineNo = 38;
            row1213.MaterialId = 9635;
            row1213.SpecNo = "SA-789";
            row1213.TypeGrade = "";
            row1213.ProductForm = "Wld. tube";
            row1213.AlloyUNS = "S32304";
            row1213.ClassCondition = "";
            row1213.Notes = "G24, G32";
            row1213.StressValues = new double?[] { 21.1, null, 20.4, null, 19.1, 18.5, 18.1, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1213);

            // Row 1: SA-790, , Smls. pipe
            var row1214 = new OldStressRowData();
            row1214.LineNo = 1;
            row1214.MaterialId = 9636;
            row1214.SpecNo = "SA-790";
            row1214.TypeGrade = "";
            row1214.ProductForm = "Smls. pipe";
            row1214.AlloyUNS = "S32304";
            row1214.ClassCondition = "";
            row1214.Notes = "G32";
            row1214.StressValues = new double?[] { 24.9, null, 24, null, 22.5, 21.7, 21.3, 21, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1214);

            // Row 2: SA-790, , Wld. pipe
            var row1215 = new OldStressRowData();
            row1215.LineNo = 2;
            row1215.MaterialId = 9637;
            row1215.SpecNo = "SA-790";
            row1215.TypeGrade = "";
            row1215.ProductForm = "Wld. pipe";
            row1215.AlloyUNS = "S32304";
            row1215.ClassCondition = "";
            row1215.Notes = "G24, G32";
            row1215.StressValues = new double?[] { 21.1, null, 20.4, null, 19.1, 18.5, 18.1, 17.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1215);

            // Row 3: SA-789, , Smls. tube
            var row1216 = new OldStressRowData();
            row1216.LineNo = 3;
            row1216.MaterialId = 16934;
            row1216.SpecNo = "SA-789";
            row1216.TypeGrade = "";
            row1216.ProductForm = "Smls. tube";
            row1216.AlloyUNS = "S32304";
            row1216.ClassCondition = "";
            row1216.Notes = "G32";
            row1216.StressValues = new double?[] { 28.6, null, 27.6, null, 25.9, 25, 24.6, 24.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1216);

            // Row 4: SA-789, , Wld. tube
            var row1217 = new OldStressRowData();
            row1217.LineNo = 4;
            row1217.MaterialId = 16935;
            row1217.SpecNo = "SA-789";
            row1217.TypeGrade = "";
            row1217.ProductForm = "Wld. tube";
            row1217.AlloyUNS = "S32304";
            row1217.ClassCondition = "";
            row1217.Notes = "G24, G32";
            row1217.StressValues = new double?[] { 24.3, null, 23.4, null, 22, 21.3, 20.9, 20.6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1217);

            // Row 5: SA-403, 309, Smls. & wld. fittings
            var row1218 = new OldStressRowData();
            row1218.LineNo = 5;
            row1218.MaterialId = 9667;
            row1218.SpecNo = "SA-403";
            row1218.TypeGrade = "309";
            row1218.ProductForm = "Smls. & wld. fittings";
            row1218.AlloyUNS = "S30900";
            row1218.ClassCondition = "";
            row1218.Notes = "G5, G12, T5, W12, W14";
            row1218.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1218);

            // Row 6: SA-213, TP309S, Smls. tube
            var row1219 = new OldStressRowData();
            row1219.LineNo = 6;
            row1219.MaterialId = 9640;
            row1219.SpecNo = "SA-213";
            row1219.TypeGrade = "TP309S";
            row1219.ProductForm = "Smls. tube";
            row1219.AlloyUNS = "S30908";
            row1219.ClassCondition = "";
            row1219.Notes = "G5, G12, G18, T5";
            row1219.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1219);

            // Row 7: SA-213, TP309S, Smls. tube
            var row1220 = new OldStressRowData();
            row1220.LineNo = 7;
            row1220.MaterialId = 9641;
            row1220.SpecNo = "SA-213";
            row1220.TypeGrade = "TP309S";
            row1220.ProductForm = "Smls. tube";
            row1220.AlloyUNS = "S30908";
            row1220.ClassCondition = "";
            row1220.Notes = "G12, G18, T6";
            row1220.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1220);

            // Row 8: SA-240, 309S, Plate
            var row1221 = new OldStressRowData();
            row1221.LineNo = 8;
            row1221.MaterialId = 9644;
            row1221.SpecNo = "SA-240";
            row1221.TypeGrade = "309S";
            row1221.ProductForm = "Plate";
            row1221.AlloyUNS = "S30908";
            row1221.ClassCondition = "";
            row1221.Notes = "G5, G12, G18, T5";
            row1221.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1221);

            // Row 9: SA-240, 309S, Plate
            var row1222 = new OldStressRowData();
            row1222.LineNo = 9;
            row1222.MaterialId = 9645;
            row1222.SpecNo = "SA-240";
            row1222.TypeGrade = "309S";
            row1222.ProductForm = "Plate";
            row1222.AlloyUNS = "S30908";
            row1222.ClassCondition = "";
            row1222.Notes = "G12, G18, T6";
            row1222.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1222);

            // Row 10: SA-249, TP309S, Wld. tube
            var row1223 = new OldStressRowData();
            row1223.LineNo = 10;
            row1223.MaterialId = 9649;
            row1223.SpecNo = "SA-249";
            row1223.TypeGrade = "TP309S";
            row1223.ProductForm = "Wld. tube";
            row1223.AlloyUNS = "S30908";
            row1223.ClassCondition = "";
            row1223.Notes = "G5, G12, G24, T5";
            row1223.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1223);

            // Row 11: SA-249, TP309S, Wld. tube
            var row1224 = new OldStressRowData();
            row1224.LineNo = 11;
            row1224.MaterialId = 9650;
            row1224.SpecNo = "SA-249";
            row1224.TypeGrade = "TP309S";
            row1224.ProductForm = "Wld. tube";
            row1224.AlloyUNS = "S30908";
            row1224.ClassCondition = "";
            row1224.Notes = "G12, G24, T6";
            row1224.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1224);

            // Row 12: SA-312, TP309S, Smls. & wld. pipe
            var row1225 = new OldStressRowData();
            row1225.LineNo = 12;
            row1225.MaterialId = 9656;
            row1225.SpecNo = "SA-312";
            row1225.TypeGrade = "TP309S";
            row1225.ProductForm = "Smls. & wld. pipe";
            row1225.AlloyUNS = "S30908";
            row1225.ClassCondition = "";
            row1225.Notes = "G5, G12, G18, T5, W12";
            row1225.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1225);

            // Row 13: SA-312, TP309S, Smls. pipe
            var row1226 = new OldStressRowData();
            row1226.LineNo = 13;
            row1226.MaterialId = 9657;
            row1226.SpecNo = "SA-312";
            row1226.TypeGrade = "TP309S";
            row1226.ProductForm = "Smls. pipe";
            row1226.AlloyUNS = "S30908";
            row1226.ClassCondition = "";
            row1226.Notes = "G12, G18, T6";
            row1226.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1226);

            // Row 14: SA-312, TP309S, Wld. pipe
            var row1227 = new OldStressRowData();
            row1227.LineNo = 14;
            row1227.MaterialId = 9658;
            row1227.SpecNo = "SA-312";
            row1227.TypeGrade = "TP309S";
            row1227.ProductForm = "Wld. pipe";
            row1227.AlloyUNS = "S30908";
            row1227.ClassCondition = "";
            row1227.Notes = "G5, G12, G18, T5, W13";
            row1227.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1227);

            // Row 15: SA-312, TP309S, Wld. pipe
            var row1228 = new OldStressRowData();
            row1228.LineNo = 15;
            row1228.MaterialId = 9659;
            row1228.SpecNo = "SA-312";
            row1228.TypeGrade = "TP309S";
            row1228.ProductForm = "Wld. pipe";
            row1228.AlloyUNS = "S30908";
            row1228.ClassCondition = "";
            row1228.Notes = "G12, G18, T6, W13";
            row1228.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1228);

            // Row 16: SA-312, TP309S, Wld. pipe
            var row1229 = new OldStressRowData();
            row1229.LineNo = 16;
            row1229.MaterialId = 9660;
            row1229.SpecNo = "SA-312";
            row1229.TypeGrade = "TP309S";
            row1229.ProductForm = "Wld. pipe";
            row1229.AlloyUNS = "S30908";
            row1229.ClassCondition = "";
            row1229.Notes = "G3, G5, G12, G18, G24, T5";
            row1229.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1229);

            // Row 17: SA-312, TP309S, Wld. pipe
            var row1230 = new OldStressRowData();
            row1230.LineNo = 17;
            row1230.MaterialId = 9655;
            row1230.SpecNo = "SA-312";
            row1230.TypeGrade = "TP309S";
            row1230.ProductForm = "Wld. pipe";
            row1230.AlloyUNS = "S30908";
            row1230.ClassCondition = "";
            row1230.Notes = "G3, G12, G18, G24, T6";
            row1230.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1230);

            // Row 18: SA-358, 309S, Wld. pipe
            var row1231 = new OldStressRowData();
            row1231.LineNo = 18;
            row1231.MaterialId = 9661;
            row1231.SpecNo = "SA-358";
            row1231.TypeGrade = "309S";
            row1231.ProductForm = "Wld. pipe";
            row1231.AlloyUNS = "S30908";
            row1231.ClassCondition = "1";
            row1231.Notes = "G5, W12";
            row1231.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1231);

            // Row 19: SA-479, 309S, Bar
            var row1232 = new OldStressRowData();
            row1232.LineNo = 19;
            row1232.MaterialId = 9670;
            row1232.SpecNo = "SA-479";
            row1232.TypeGrade = "309S";
            row1232.ProductForm = "Bar";
            row1232.AlloyUNS = "S30908";
            row1232.ClassCondition = "";
            row1232.Notes = "G5, G12, G18, G22, T5";
            row1232.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1232);

            // Row 20: SA-479, 309S, Bar
            var row1233 = new OldStressRowData();
            row1233.LineNo = 20;
            row1233.MaterialId = 9671;
            row1233.SpecNo = "SA-479";
            row1233.TypeGrade = "309S";
            row1233.ProductForm = "Bar";
            row1233.AlloyUNS = "S30908";
            row1233.ClassCondition = "";
            row1233.Notes = "G12, G18, G22, T6";
            row1233.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1233);

            // Row 21: SA-813, TP309S, Wld. pipe
            var row1234 = new OldStressRowData();
            row1234.LineNo = 21;
            row1234.MaterialId = 9672;
            row1234.SpecNo = "SA-813";
            row1234.TypeGrade = "TP309S";
            row1234.ProductForm = "Wld. pipe";
            row1234.AlloyUNS = "S30908";
            row1234.ClassCondition = "";
            row1234.Notes = "G5, G12, G24, T5";
            row1234.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1234);

            // Row 22: SA-813, TP309S, Wld. pipe
            var row1235 = new OldStressRowData();
            row1235.LineNo = 22;
            row1235.MaterialId = 9673;
            row1235.SpecNo = "SA-813";
            row1235.TypeGrade = "TP309S";
            row1235.ProductForm = "Wld. pipe";
            row1235.AlloyUNS = "S30908";
            row1235.ClassCondition = "";
            row1235.Notes = "G12, G24, T6";
            row1235.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1235);

            // Row 23: SA-814, TP309S, Wld. pipe
            var row1236 = new OldStressRowData();
            row1236.LineNo = 23;
            row1236.MaterialId = 9674;
            row1236.SpecNo = "SA-814";
            row1236.TypeGrade = "TP309S";
            row1236.ProductForm = "Wld. pipe";
            row1236.AlloyUNS = "S30908";
            row1236.ClassCondition = "";
            row1236.Notes = "G5, G12, G24, T5";
            row1236.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1236);

            // Row 24: SA-814, TP309S, Wld. pipe
            var row1237 = new OldStressRowData();
            row1237.LineNo = 24;
            row1237.MaterialId = 9675;
            row1237.SpecNo = "SA-814";
            row1237.TypeGrade = "TP309S";
            row1237.ProductForm = "Wld. pipe";
            row1237.AlloyUNS = "S30908";
            row1237.ClassCondition = "";
            row1237.Notes = "G12, G24, T6";
            row1237.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1237);

            // Row 25: SA-213, TP309H, Smls. tube
            var row1238 = new OldStressRowData();
            row1238.LineNo = 25;
            row1238.MaterialId = 9638;
            row1238.SpecNo = "SA-213";
            row1238.TypeGrade = "TP309H";
            row1238.ProductForm = "Smls. tube";
            row1238.AlloyUNS = "S30909";
            row1238.ClassCondition = "";
            row1238.Notes = "G5, G18, T6";
            row1238.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 16.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1238);

            // Row 26: SA-213, TP309H, Smls. tube
            var row1239 = new OldStressRowData();
            row1239.LineNo = 26;
            row1239.MaterialId = 9639;
            row1239.SpecNo = "SA-213";
            row1239.TypeGrade = "TP309H";
            row1239.ProductForm = "Smls. tube";
            row1239.AlloyUNS = "S30909";
            row1239.ClassCondition = "";
            row1239.Notes = "G18, T7";
            row1239.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1239);

            // Row 27: SA-240, 309H, Plate
            var row1240 = new OldStressRowData();
            row1240.LineNo = 27;
            row1240.MaterialId = 9642;
            row1240.SpecNo = "SA-240";
            row1240.TypeGrade = "309H";
            row1240.ProductForm = "Plate";
            row1240.AlloyUNS = "S30909";
            row1240.ClassCondition = "";
            row1240.Notes = "G5, G18, H1, T6";
            row1240.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 16.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1240);

            // Row 28: SA-240, 309H, Plate
            var row1241 = new OldStressRowData();
            row1241.LineNo = 28;
            row1241.MaterialId = 9643;
            row1241.SpecNo = "SA-240";
            row1241.TypeGrade = "309H";
            row1241.ProductForm = "Plate";
            row1241.AlloyUNS = "S30909";
            row1241.ClassCondition = "";
            row1241.Notes = "G18, H1, T7";
            row1241.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1241);

            // Row 29: SA-249, TP309H, Wld. tube
            var row1242 = new OldStressRowData();
            row1242.LineNo = 29;
            row1242.MaterialId = 9646;
            row1242.SpecNo = "SA-249";
            row1242.TypeGrade = "TP309H";
            row1242.ProductForm = "Wld. tube";
            row1242.AlloyUNS = "S30909";
            row1242.ClassCondition = "";
            row1242.Notes = "G5, W12";
            row1242.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1242);

            // Row 30: SA-249, TP309H, Wld. tube
            var row1243 = new OldStressRowData();
            row1243.LineNo = 30;
            row1243.MaterialId = 9647;
            row1243.SpecNo = "SA-249";
            row1243.TypeGrade = "TP309H";
            row1243.ProductForm = "Wld. tube";
            row1243.AlloyUNS = "S30909";
            row1243.ClassCondition = "";
            row1243.Notes = "G5, G24, T6";
            row1243.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 14.4, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1243);

            // Row 31: SA-249, TP309H, Wld. tube
            var row1244 = new OldStressRowData();
            row1244.LineNo = 31;
            row1244.MaterialId = 9648;
            row1244.SpecNo = "SA-249";
            row1244.TypeGrade = "TP309H";
            row1244.ProductForm = "Wld. tube";
            row1244.AlloyUNS = "S30909";
            row1244.ClassCondition = "";
            row1244.Notes = "G24, T7";
            row1244.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 10.4, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1244);

            // Row 32: SA-312, TP309H, Smls. pipe
            var row1245 = new OldStressRowData();
            row1245.LineNo = 32;
            row1245.MaterialId = 9651;
            row1245.SpecNo = "SA-312";
            row1245.TypeGrade = "TP309H";
            row1245.ProductForm = "Smls. pipe";
            row1245.AlloyUNS = "S30909";
            row1245.ClassCondition = "";
            row1245.Notes = "G5, G18, T6";
            row1245.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 16.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1245);

            // Row 33: SA-312, TP309H, Smls. pipe
            var row1246 = new OldStressRowData();
            row1246.LineNo = 33;
            row1246.MaterialId = 9652;
            row1246.SpecNo = "SA-312";
            row1246.TypeGrade = "TP309H";
            row1246.ProductForm = "Smls. pipe";
            row1246.AlloyUNS = "S30909";
            row1246.ClassCondition = "";
            row1246.Notes = "G18, T7";
            row1246.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1246);

            // Row 34: SA-312, TP309H, Wld. pipe
            var row1247 = new OldStressRowData();
            row1247.LineNo = 34;
            row1247.MaterialId = 9653;
            row1247.SpecNo = "SA-312";
            row1247.TypeGrade = "TP309H";
            row1247.ProductForm = "Wld. pipe";
            row1247.AlloyUNS = "S30909";
            row1247.ClassCondition = "";
            row1247.Notes = "G3, G5, G18, G24, T6";
            row1247.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 14.4, 11.7, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1247);

            // Row 35: SA-312, TP309H, Wld. pipe
            var row1248 = new OldStressRowData();
            row1248.LineNo = 35;
            row1248.MaterialId = 9654;
            row1248.SpecNo = "SA-312";
            row1248.TypeGrade = "TP309H";
            row1248.ProductForm = "Wld. pipe";
            row1248.AlloyUNS = "S30909";
            row1248.ClassCondition = "";
            row1248.Notes = "G3, G18, G24, T7";
            row1248.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 10.4, 8.8, 6.5, 4.7, 3.4, 2.6, 1.9, 1.4, 1.1, 0.82, 0.64, null, null, null };
            batch.Add(row1248);

            // Row 36: SA-479, 309H, Bar
            var row1249 = new OldStressRowData();
            row1249.LineNo = 36;
            row1249.MaterialId = 9668;
            row1249.SpecNo = "SA-479";
            row1249.TypeGrade = "309H";
            row1249.ProductForm = "Bar";
            row1249.AlloyUNS = "S30909";
            row1249.ClassCondition = "";
            row1249.Notes = "G5, G18, T6";
            row1249.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 16.9, 13.8, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1249);

            // Row 37: SA-479, 309H, Bar
            var row1250 = new OldStressRowData();
            row1250.LineNo = 37;
            row1250.MaterialId = 9669;
            row1250.SpecNo = "SA-479";
            row1250.TypeGrade = "309H";
            row1250.ProductForm = "Bar";
            row1250.AlloyUNS = "S30909";
            row1250.ClassCondition = "";
            row1250.Notes = "G18, T7";
            row1250.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 12.3, 10.3, 7.6, 5.5, 4, 3, 2.2, 1.7, 1.3, 1, 0.75, null, null, null };
            batch.Add(row1250);

            // Row 1: SA-213, TP309Cb, Smls. tube
            var row1251 = new OldStressRowData();
            row1251.LineNo = 1;
            row1251.MaterialId = 9676;
            row1251.SpecNo = "SA-213";
            row1251.TypeGrade = "TP309Cb";
            row1251.ProductForm = "Smls. tube";
            row1251.AlloyUNS = "S30940";
            row1251.ClassCondition = "";
            row1251.Notes = "G5, G12, T5";
            row1251.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1251);

            // Row 2: SA-213, TP309Cb, Smls. tube
            var row1252 = new OldStressRowData();
            row1252.LineNo = 2;
            row1252.MaterialId = 9677;
            row1252.SpecNo = "SA-213";
            row1252.TypeGrade = "TP309Cb";
            row1252.ProductForm = "Smls. tube";
            row1252.AlloyUNS = "S30940";
            row1252.ClassCondition = "";
            row1252.Notes = "G12, T6";
            row1252.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1252);

            // Row 3: SA-240, 309Cb, Plate
            var row1253 = new OldStressRowData();
            row1253.LineNo = 3;
            row1253.MaterialId = 9678;
            row1253.SpecNo = "SA-240";
            row1253.TypeGrade = "309Cb";
            row1253.ProductForm = "Plate";
            row1253.AlloyUNS = "S30940";
            row1253.ClassCondition = "";
            row1253.Notes = "G5, G12, T5";
            row1253.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1253);

            // Row 4: SA-240, 309Cb, Plate
            var row1254 = new OldStressRowData();
            row1254.LineNo = 4;
            row1254.MaterialId = 9679;
            row1254.SpecNo = "SA-240";
            row1254.TypeGrade = "309Cb";
            row1254.ProductForm = "Plate";
            row1254.AlloyUNS = "S30940";
            row1254.ClassCondition = "";
            row1254.Notes = "G12, T6";
            row1254.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1254);

            // Row 5: SA-249, TP309Cb, Wld. tube
            var row1255 = new OldStressRowData();
            row1255.LineNo = 5;
            row1255.MaterialId = 9680;
            row1255.SpecNo = "SA-249";
            row1255.TypeGrade = "TP309Cb";
            row1255.ProductForm = "Wld. tube";
            row1255.AlloyUNS = "S30940";
            row1255.ClassCondition = "";
            row1255.Notes = "G5, G12, G24, T5";
            row1255.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1255);

            // Row 6: SA-249, TP309Cb, Wld. tube
            var row1256 = new OldStressRowData();
            row1256.LineNo = 6;
            row1256.MaterialId = 9681;
            row1256.SpecNo = "SA-249";
            row1256.TypeGrade = "TP309Cb";
            row1256.ProductForm = "Wld. tube";
            row1256.AlloyUNS = "S30940";
            row1256.ClassCondition = "";
            row1256.Notes = "G12, G24, T6";
            row1256.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1256);

            // Row 7: SA-312, TP309Cb, Smls. & wld. pipe
            var row1257 = new OldStressRowData();
            row1257.LineNo = 7;
            row1257.MaterialId = 9682;
            row1257.SpecNo = "SA-312";
            row1257.TypeGrade = "TP309Cb";
            row1257.ProductForm = "Smls. & wld. pipe";
            row1257.AlloyUNS = "S30940";
            row1257.ClassCondition = "";
            row1257.Notes = "G5, G12, T5, W12";
            row1257.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1257);

            // Row 8: SA-312, TP309Cb, Smls. pipe
            var row1258 = new OldStressRowData();
            row1258.LineNo = 8;
            row1258.MaterialId = 9683;
            row1258.SpecNo = "SA-312";
            row1258.TypeGrade = "TP309Cb";
            row1258.ProductForm = "Smls. pipe";
            row1258.AlloyUNS = "S30940";
            row1258.ClassCondition = "";
            row1258.Notes = "G12, T6";
            row1258.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1258);

            // Row 9: SA-312, TP309Cb, Wld. pipe
            var row1259 = new OldStressRowData();
            row1259.LineNo = 9;
            row1259.MaterialId = 9684;
            row1259.SpecNo = "SA-312";
            row1259.TypeGrade = "TP309Cb";
            row1259.ProductForm = "Wld. pipe";
            row1259.AlloyUNS = "S30940";
            row1259.ClassCondition = "";
            row1259.Notes = "G5, G12, G24, T5";
            row1259.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1259);

            // Row 10: SA-312, TP309Cb, Wld. pipe
            var row1260 = new OldStressRowData();
            row1260.LineNo = 10;
            row1260.MaterialId = 9685;
            row1260.SpecNo = "SA-312";
            row1260.TypeGrade = "TP309Cb";
            row1260.ProductForm = "Wld. pipe";
            row1260.AlloyUNS = "S30940";
            row1260.ClassCondition = "";
            row1260.Notes = "G12, G24, T6";
            row1260.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1260);

            // Row 11: SA-479, 309Cb, Bar
            var row1261 = new OldStressRowData();
            row1261.LineNo = 11;
            row1261.MaterialId = 9686;
            row1261.SpecNo = "SA-479";
            row1261.TypeGrade = "309Cb";
            row1261.ProductForm = "Bar";
            row1261.AlloyUNS = "S30940";
            row1261.ClassCondition = "";
            row1261.Notes = "G5, G12, G22";
            row1261.StressValues = new double?[] { 20, null, 20, null, 20, 20, 19.4, 18.8, 18.5, 18.2, 18, 17.7, 17.5, 17.2, 15.9, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1261);

            // Row 12: SA-479, 309Cb, Bar
            var row1262 = new OldStressRowData();
            row1262.LineNo = 12;
            row1262.MaterialId = 9687;
            row1262.SpecNo = "SA-479";
            row1262.TypeGrade = "309Cb";
            row1262.ProductForm = "Bar";
            row1262.AlloyUNS = "S30940";
            row1262.ClassCondition = "";
            row1262.Notes = "G12, G22";
            row1262.StressValues = new double?[] { 20, null, 17.5, null, 16.1, 15.1, 14.4, 13.9, 13.7, 13.5, 13.3, 13.1, 12.9, 12.7, 12.5, 9.9, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1262);

            // Row 13: SA-813, TP309Cb, Wld. pipe
            var row1263 = new OldStressRowData();
            row1263.LineNo = 13;
            row1263.MaterialId = 9688;
            row1263.SpecNo = "SA-813";
            row1263.TypeGrade = "TP309Cb";
            row1263.ProductForm = "Wld. pipe";
            row1263.AlloyUNS = "S30940";
            row1263.ClassCondition = "";
            row1263.Notes = "G5, G12, G24, T5";
            row1263.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1263);

            // Row 14: SA-813, TP309Cb, Wld. pipe
            var row1264 = new OldStressRowData();
            row1264.LineNo = 14;
            row1264.MaterialId = 9689;
            row1264.SpecNo = "SA-813";
            row1264.TypeGrade = "TP309Cb";
            row1264.ProductForm = "Wld. pipe";
            row1264.AlloyUNS = "S30940";
            row1264.ClassCondition = "";
            row1264.Notes = "G12, G24, T6";
            row1264.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1264);

            // Row 15: SA-814, TP309Cb, Wld. pipe
            var row1265 = new OldStressRowData();
            row1265.LineNo = 15;
            row1265.MaterialId = 9690;
            row1265.SpecNo = "SA-814";
            row1265.TypeGrade = "TP309Cb";
            row1265.ProductForm = "Wld. pipe";
            row1265.AlloyUNS = "S30940";
            row1265.ClassCondition = "";
            row1265.Notes = "G5, G12, G13, G24, T5";
            row1265.StressValues = new double?[] { 17, null, 17, null, 17, 17, 16.5, 15.9, 15.7, 15.5, 15.3, 15.1, 14.8, 14.6, 13.5, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1265);

            // Row 16: SA-814, TP309Cb, Wld. pipe
            var row1266 = new OldStressRowData();
            row1266.LineNo = 16;
            row1266.MaterialId = 9691;
            row1266.SpecNo = "SA-814";
            row1266.TypeGrade = "TP309Cb";
            row1266.ProductForm = "Wld. pipe";
            row1266.AlloyUNS = "S30940";
            row1266.ClassCondition = "";
            row1266.Notes = "G12, G24, T6";
            row1266.StressValues = new double?[] { 17, null, 14.9, null, 13.7, 12.8, 12.2, 11.8, 11.6, 11.5, 11.3, 11.2, 11, 10.8, 10.6, 8.4, 6, 4.3, 3.1, 2.1, 1.3, 0.68, 0.43, 0.34, 0.26, 0.17, null, null, null };
            batch.Add(row1266);

            // Row 17: SA-351, CE8MN, Castings
            var row1267 = new OldStressRowData();
            row1267.LineNo = 17;
            row1267.MaterialId = 9692;
            row1267.SpecNo = "SA-351";
            row1267.TypeGrade = "CE8MN";
            row1267.ProductForm = "Castings";
            row1267.AlloyUNS = "J93345";
            row1267.ClassCondition = "";
            row1267.Notes = "G1, G32";
            row1267.StressValues = new double?[] { 27.1, null, 27.1, null, 25.1, 24.2, 24.2, 24.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1267);

            // Row 18: SA-240, , Plate
            var row1268 = new OldStressRowData();
            row1268.LineNo = 18;
            row1268.MaterialId = 9693;
            row1268.SpecNo = "SA-240";
            row1268.TypeGrade = "";
            row1268.ProductForm = "Plate";
            row1268.AlloyUNS = "S44635";
            row1268.ClassCondition = "";
            row1268.Notes = "G32";
            row1268.StressValues = new double?[] { 25.7, null, 24.9, null, 23.5, 22.5, 22, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1268);

            // Row 19: SA-268, , Wld. tube
            var row1269 = new OldStressRowData();
            row1269.LineNo = 19;
            row1269.MaterialId = 9694;
            row1269.SpecNo = "SA-268";
            row1269.TypeGrade = "";
            row1269.ProductForm = "Wld. tube";
            row1269.AlloyUNS = "S44635";
            row1269.ClassCondition = "";
            row1269.Notes = "G24, G32";
            row1269.StressValues = new double?[] { 21.9, null, 21.2, null, 19.9, 19.1, 18.7, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1269);

            // Row 20: SA-351, CD4MCu, Castings
            var row1270 = new OldStressRowData();
            row1270.LineNo = 20;
            row1270.MaterialId = 9695;
            row1270.SpecNo = "SA-351";
            row1270.TypeGrade = "CD4MCu";
            row1270.ProductForm = "Castings";
            row1270.AlloyUNS = "J93370";
            row1270.ClassCondition = "";
            row1270.Notes = "G7, G29, G32, H4";
            row1270.StressValues = new double?[] { 28.6, null, 28.6, null, 27.9, 27.5, 27.5, 27.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1270);

            // Row 21: SA-240, , Plate
            var row1271 = new OldStressRowData();
            row1271.LineNo = 21;
            row1271.MaterialId = 9696;
            row1271.SpecNo = "SA-240";
            row1271.TypeGrade = "";
            row1271.ProductForm = "Plate";
            row1271.AlloyUNS = "S32550";
            row1271.ClassCondition = "";
            row1271.Notes = "G32, G33";
            row1271.StressValues = new double?[] { 31.4, null, 31.3, null, 29.5, 28.6, 28.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1271);

            // Row 22: SA-479, , Bar
            var row1272 = new OldStressRowData();
            row1272.LineNo = 22;
            row1272.MaterialId = 9697;
            row1272.SpecNo = "SA-479";
            row1272.TypeGrade = "";
            row1272.ProductForm = "Bar";
            row1272.AlloyUNS = "S32550";
            row1272.ClassCondition = "";
            row1272.Notes = "G32, G33";
            row1272.StressValues = new double?[] { 31.4, null, 31.3, null, 29.5, 28.6, 28.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1272);

            // Row 23: SA-789, , Smls. tube
            var row1273 = new OldStressRowData();
            row1273.LineNo = 23;
            row1273.MaterialId = 9698;
            row1273.SpecNo = "SA-789";
            row1273.TypeGrade = "";
            row1273.ProductForm = "Smls. tube";
            row1273.AlloyUNS = "S32550";
            row1273.ClassCondition = "";
            row1273.Notes = "G32, G33";
            row1273.StressValues = new double?[] { 31.4, null, 31.3, null, 29.5, 28.6, 28.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1273);

            // Row 24: SA-789, , Wld. tube
            var row1274 = new OldStressRowData();
            row1274.LineNo = 24;
            row1274.MaterialId = 9699;
            row1274.SpecNo = "SA-789";
            row1274.TypeGrade = "";
            row1274.ProductForm = "Wld. tube";
            row1274.AlloyUNS = "S32550";
            row1274.ClassCondition = "";
            row1274.Notes = "G24, G32, G33";
            row1274.StressValues = new double?[] { 26.7, null, 26.6, null, 25.1, 24.3, 24, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1274);

            // Row 25: SA-790, , Smls. pipe
            var row1275 = new OldStressRowData();
            row1275.LineNo = 25;
            row1275.MaterialId = 9700;
            row1275.SpecNo = "SA-790";
            row1275.TypeGrade = "";
            row1275.ProductForm = "Smls. pipe";
            row1275.AlloyUNS = "S32550";
            row1275.ClassCondition = "";
            row1275.Notes = "G32, G33";
            row1275.StressValues = new double?[] { 31.4, null, 31.3, null, 29.5, 28.6, 28.2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1275);

            // Row 26: SA-790, , Wld. pipe
            var row1276 = new OldStressRowData();
            row1276.LineNo = 26;
            row1276.MaterialId = 9701;
            row1276.SpecNo = "SA-790";
            row1276.TypeGrade = "";
            row1276.ProductForm = "Wld. pipe";
            row1276.AlloyUNS = "S32550";
            row1276.ClassCondition = "";
            row1276.Notes = "G24, G32, G33";
            row1276.StressValues = new double?[] { 26.7, null, 26.6, null, 25.1, 24.3, 24, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1276);

            // Row 27: SA-240, , Plate
            var row1277 = new OldStressRowData();
            row1277.LineNo = 27;
            row1277.MaterialId = 9702;
            row1277.SpecNo = "SA-240";
            row1277.TypeGrade = "";
            row1277.ProductForm = "Plate";
            row1277.AlloyUNS = "S31200";
            row1277.ClassCondition = "";
            row1277.Notes = "G32";
            row1277.StressValues = new double?[] { 28.6, null, 28.6, null, 27.1, 26.3, 26.1, 26.1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1277);

            // Row 28: SA-789, , Smls. tube
            var row1278 = new OldStressRowData();
            row1278.LineNo = 28;
            row1278.MaterialId = 9808;
            row1278.SpecNo = "SA-789";
            row1278.TypeGrade = "";
            row1278.ProductForm = "Smls. tube";
            row1278.AlloyUNS = "S31260";
            row1278.ClassCondition = "";
            row1278.Notes = "G32";
            row1278.StressValues = new double?[] { 28.6, null, 28.5, null, 27.1, 26.4, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1278);

            // Row 29: SA-789, , Wld. tube
            var row1279 = new OldStressRowData();
            row1279.LineNo = 29;
            row1279.MaterialId = 9809;
            row1279.SpecNo = "SA-789";
            row1279.TypeGrade = "";
            row1279.ProductForm = "Wld. tube";
            row1279.AlloyUNS = "S31260";
            row1279.ClassCondition = "";
            row1279.Notes = "G24, G32";
            row1279.StressValues = new double?[] { 24.3, null, 24.3, null, 23, 22.5, 22.4, 22.4, 22.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1279);

            // Row 30: SA-790, , Smls. pipe
            var row1280 = new OldStressRowData();
            row1280.LineNo = 30;
            row1280.MaterialId = 9810;
            row1280.SpecNo = "SA-790";
            row1280.TypeGrade = "";
            row1280.ProductForm = "Smls. pipe";
            row1280.AlloyUNS = "S31260";
            row1280.ClassCondition = "";
            row1280.Notes = "G32";
            row1280.StressValues = new double?[] { 28.6, null, 28.5, null, 27.1, 26.4, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1280);

            // Row 31: SA-790, , Wld. pipe
            var row1281 = new OldStressRowData();
            row1281.LineNo = 31;
            row1281.MaterialId = 9811;
            row1281.SpecNo = "SA-790";
            row1281.TypeGrade = "";
            row1281.ProductForm = "Wld. pipe";
            row1281.AlloyUNS = "S31260";
            row1281.ClassCondition = "";
            row1281.Notes = "G24, G32";
            row1281.StressValues = new double?[] { 24.3, null, 24.3, null, 23, 22.5, 22.4, 22.4, 22.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1281);

            // Row 32: SA-240, , Plate
            var row1282 = new OldStressRowData();
            row1282.LineNo = 32;
            row1282.MaterialId = 9812;
            row1282.SpecNo = "SA-240";
            row1282.TypeGrade = "";
            row1282.ProductForm = "Plate";
            row1282.AlloyUNS = "S31260";
            row1282.ClassCondition = "";
            row1282.Notes = "G32";
            row1282.StressValues = new double?[] { 28.6, null, 28.5, null, 27.1, 26.4, 26.3, 26.3, 26.3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1282);

            // Row 33: SA-789, , Smls. tube
            var row1283 = new OldStressRowData();
            row1283.LineNo = 33;
            row1283.MaterialId = 9703;
            row1283.SpecNo = "SA-789";
            row1283.TypeGrade = "";
            row1283.ProductForm = "Smls. tube";
            row1283.AlloyUNS = "S32750";
            row1283.ClassCondition = "";
            row1283.Notes = "G32, G33";
            row1283.StressValues = new double?[] { 33.1, null, 33, null, 31.2, 30.1, 29.6, 29.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1283);

            // Row 34: SA-789, , Wld. tube
            var row1284 = new OldStressRowData();
            row1284.LineNo = 34;
            row1284.MaterialId = 9704;
            row1284.SpecNo = "SA-789";
            row1284.TypeGrade = "";
            row1284.ProductForm = "Wld. tube";
            row1284.AlloyUNS = "S32750";
            row1284.ClassCondition = "";
            row1284.Notes = "G24, G32, G33";
            row1284.StressValues = new double?[] { 28.2, null, 28, null, 26.5, 25.6, 25.2, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1284);

            // Row 35: SA-790, , Smls. pipe
            var row1285 = new OldStressRowData();
            row1285.LineNo = 35;
            row1285.MaterialId = 9705;
            row1285.SpecNo = "SA-790";
            row1285.TypeGrade = "";
            row1285.ProductForm = "Smls. pipe";
            row1285.AlloyUNS = "S32750";
            row1285.ClassCondition = "";
            row1285.Notes = "G32, G33";
            row1285.StressValues = new double?[] { 33.1, null, 33, null, 31.2, 30.1, 29.6, 29.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1285);

            // Row 36: SA-790, , Wld. pipe
            var row1286 = new OldStressRowData();
            row1286.LineNo = 36;
            row1286.MaterialId = 9706;
            row1286.SpecNo = "SA-790";
            row1286.TypeGrade = "";
            row1286.ProductForm = "Wld. pipe";
            row1286.AlloyUNS = "S32750";
            row1286.ClassCondition = "";
            row1286.Notes = "G24, G32, G33";
            row1286.StressValues = new double?[] { 28.2, null, 28, null, 26.5, 25.6, 25.2, 25, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1286);

            // Row 37: SA-351, CH8, Castings
            var row1287 = new OldStressRowData();
            row1287.LineNo = 37;
            row1287.MaterialId = 9708;
            row1287.SpecNo = "SA-351";
            row1287.TypeGrade = "CH8";
            row1287.ProductForm = "Castings";
            row1287.AlloyUNS = "J93400";
            row1287.ClassCondition = "";
            row1287.Notes = "G1, G5, G12, G16, G17, G32, T6";
            row1287.StressValues = new double?[] { 18.6, null, 17, null, 15.8, 15.4, 15.4, 15.4, 15.3, 15.2, 15, 14.8, 14.4, 13.9, 13.2, 11.1, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1287);

            // Row 38: SA-351, CH8, Castings
            var row1288 = new OldStressRowData();
            row1288.LineNo = 38;
            row1288.MaterialId = 9709;
            row1288.SpecNo = "SA-351";
            row1288.TypeGrade = "CH8";
            row1288.ProductForm = "Castings";
            row1288.AlloyUNS = "J93400";
            row1288.ClassCondition = "";
            row1288.Notes = "G1, G12, G32, T7";
            row1288.StressValues = new double?[] { 18.6, null, 15.3, null, 14.1, 13.5, 13.1, 12.7, 12.4, 12.1, 11.8, 11.4, 11, 10.7, 10.3, 9.9, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1288);

            // Row 39: SA-451, CPH8, Cast pipe
            var row1289 = new OldStressRowData();
            row1289.LineNo = 39;
            row1289.MaterialId = 9710;
            row1289.SpecNo = "SA-451";
            row1289.TypeGrade = "CPH8";
            row1289.ProductForm = "Cast pipe";
            row1289.AlloyUNS = "J93400";
            row1289.ClassCondition = "";
            row1289.Notes = "G5, G16, G17, G32";
            row1289.StressValues = new double?[] { 18.6, null, 17, null, 15.8, 15.4, 15.4, 15.4, 15.3, 15.2, 15, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1289);

            // Row 1: SA-351, CH20, Castings
            var row1290 = new OldStressRowData();
            row1290.LineNo = 1;
            row1290.MaterialId = 9711;
            row1290.SpecNo = "SA-351";
            row1290.TypeGrade = "CH20";
            row1290.ProductForm = "Castings";
            row1290.AlloyUNS = "J93402";
            row1290.ClassCondition = "";
            row1290.Notes = "G1, G5, G12, G16, G17, T6";
            row1290.StressValues = new double?[] { 20, null, 18.3, null, 17, 16.6, 16.6, 16.6, 16.5, 16.4, 16.2, 15.9, 15.5, 14.9, 14.3, 11.1, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1290);

            // Row 2: SA-351, CH20, Castings
            var row1291 = new OldStressRowData();
            row1291.LineNo = 2;
            row1291.MaterialId = 9712;
            row1291.SpecNo = "SA-351";
            row1291.TypeGrade = "CH20";
            row1291.ProductForm = "Castings";
            row1291.AlloyUNS = "J93402";
            row1291.ClassCondition = "";
            row1291.Notes = "G1, G12, T7";
            row1291.StressValues = new double?[] { 20, null, 16.3, null, 15.1, 14.5, 14, 13.6, 13.3, 12.9, 12.6, 12.2, 11.8, 11.4, 11, 10.6, 8.5, 6.5, 5, 3.8, 2.9, 2.3, 1.8, 1.3, 0.9, 0.8, null, null, null };
            batch.Add(row1291);

            // Row 3: SA-451, CPH20, Cast pipe
            var row1292 = new OldStressRowData();
            row1292.LineNo = 3;
            row1292.MaterialId = 9714;
            row1292.SpecNo = "SA-451";
            row1292.TypeGrade = "CPH20";
            row1292.ProductForm = "Cast pipe";
            row1292.AlloyUNS = "J93402";
            row1292.ClassCondition = "";
            row1292.Notes = "G5, G16, G17";
            row1292.StressValues = new double?[] { 20, null, 18.3, null, 17, 16.6, 16.6, 16.6, 16.5, 16.4, 16.2, 15.9, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1292);

            // Row 4: SA-351, CK20, Castings
            var row1293 = new OldStressRowData();
            row1293.LineNo = 4;
            row1293.MaterialId = 9716;
            row1293.SpecNo = "SA-351";
            row1293.TypeGrade = "CK20";
            row1293.ProductForm = "Castings";
            row1293.AlloyUNS = "J94202";
            row1293.ClassCondition = "";
            row1293.Notes = "G1, G5, G12, G16, G17, T6";
            row1293.StressValues = new double?[] { 18.6, null, 17, null, 15.8, 15.4, 15.4, 15.4, 15.3, 15.2, 15, 14.8, 14.4, 13.9, 13.2, 11.3, 9.8, 8.5, 7.3, 6, 4.8, 3.5, 2.4, 1.6, 1.1, 0.8, null, null, null };
            batch.Add(row1293);

            // Row 5: SA-351, CK20, Castings
            var row1294 = new OldStressRowData();
            row1294.LineNo = 5;
            row1294.MaterialId = 9717;
            row1294.SpecNo = "SA-351";
            row1294.TypeGrade = "CK20";
            row1294.ProductForm = "Castings";
            row1294.AlloyUNS = "J94202";
            row1294.ClassCondition = "";
            row1294.Notes = "G1, G12, T8";
            row1294.StressValues = new double?[] { 18.6, null, 15.3, null, 14.1, 13.5, 13.1, 12.7, 12.4, 12.1, 11.8, 11.4, 11, 10.7, 10.3, 9.9, 9.5, 8.5, 7.3, 6, 4.8, 3.5, 2.4, 1.6, 1.1, 0.8, null, null, null };
            batch.Add(row1294);

            // Row 6: SA-451, CPK20, Cast pipe
            var row1295 = new OldStressRowData();
            row1295.LineNo = 6;
            row1295.MaterialId = 9718;
            row1295.SpecNo = "SA-451";
            row1295.TypeGrade = "CPK20";
            row1295.ProductForm = "Cast pipe";
            row1295.AlloyUNS = "J94202";
            row1295.ClassCondition = "";
            row1295.Notes = "G5, G16, G17";
            row1295.StressValues = new double?[] { 18.6, null, 17, null, 15.8, 15.4, 15.4, 15.4, 15.3, 15.2, 15, 14.8, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1295);

            // Row 7: SA-182, F310, Forgings
            var row1296 = new OldStressRowData();
            row1296.LineNo = 7;
            row1296.MaterialId = 9719;
            row1296.SpecNo = "SA-182";
            row1296.TypeGrade = "F310";
            row1296.ProductForm = "Forgings";
            row1296.AlloyUNS = "S31000";
            row1296.ClassCondition = "";
            row1296.Notes = "G5";
            row1296.StressValues = new double?[] { 20, null, 19.8, null, 18.9, 18.6, 18.5, 18.5, 18.2, 17.9, 17.7, 17.4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
            batch.Add(row1296);

            // Row 8: SA-182, F310, Forgings
            var row1297 = new OldStressRowData();
            row1297.LineNo = 8;
            row1297.MaterialId = 9720;
            row1297.SpecNo = "SA-182";
            row1297.TypeGrade = "F310";
            row1297.ProductForm = "Forgings";
            row1297.AlloyUNS = "S31000";
            row1297.ClassCondition = "";
            row1297.Notes = "G5, G12, G14, T5";
            row1297.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1297);

            // Row 9: SA-336, F310, Forgings
            var row1298 = new OldStressRowData();
            row1298.LineNo = 9;
            row1298.MaterialId = 9747;
            row1298.SpecNo = "SA-336";
            row1298.TypeGrade = "F310";
            row1298.ProductForm = "Forgings";
            row1298.AlloyUNS = "S31000";
            row1298.ClassCondition = "";
            row1298.Notes = "G5, G12, T5";
            row1298.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1298);

            // Row 10: SA-403, 310, Smls. & wld. fittings
            var row1299 = new OldStressRowData();
            row1299.LineNo = 10;
            row1299.MaterialId = 9754;
            row1299.SpecNo = "SA-403";
            row1299.TypeGrade = "310";
            row1299.ProductForm = "Smls. & wld. fittings";
            row1299.AlloyUNS = "S31000";
            row1299.ClassCondition = "";
            row1299.Notes = "G5, G12, T5, W12, W14";
            row1299.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
            batch.Add(row1299);

            // Row 11: SA-213, TP310S, Smls. tube
            var row1300 = new OldStressRowData();
            row1300.LineNo = 11;
            row1300.MaterialId = 9724;
            row1300.SpecNo = "SA-213";
            row1300.TypeGrade = "TP310S";
            row1300.ProductForm = "Smls. tube";
            row1300.AlloyUNS = "S31008";
            row1300.ClassCondition = "";
            row1300.Notes = "G5, G12, G18, T5";
            row1300.StressValues = new double?[] { 20, null, 20, null, 20, 19.9, 19.3, 18.5, 18.2, 17.9, 17.7, 17.4, 17.2, 16.9, 15.9, 9.9, 7.1, 5, 3.6, 2.5, 1.5, 0.8, 0.5, 0.4, 0.3, 0.2, null, null, null };
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
