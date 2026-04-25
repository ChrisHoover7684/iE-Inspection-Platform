using System.Data;
using System.Text;
using ExcelDataReader;
Console.WriteLine("STARTING...");

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string excelPath = @"C:\Users\Chris Hoover\Desktop\Excel Addin - Reference Docs\B31_3_Table_A-1C_Materials.xlsx";
string outputFolder = @"C:\TEMP\GeneratedStress";

Directory.CreateDirectory(outputFolder);

using var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
using var reader = ExcelReaderFactory.CreateReader(stream);

var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
{
    ConfigureDataTable = _ => new ExcelDataTableConfiguration
    {
        UseHeaderRow = true
    }
});


var table = dataSet.Tables[0];

var headers = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

var tempColumns = headers
    .Select((h, i) => new { h, i })
    .Where(x => double.TryParse(x.h, out _))
    .Select(x => new { Temp = double.Parse(x.h), x.i })
    .OrderBy(x => x.Temp)
    .ToList();

var sb = new StringBuilder();

sb.AppendLine("using System.Collections.Generic;");
sb.AppendLine("using iE.Core.MaterialStress.Models;");
sb.AppendLine("using iE.Core.MaterialStress.Services;");
sb.AppendLine();
sb.AppendLine("namespace iE.Core.MaterialStress.Importers");
sb.AppendLine("{");
sb.AppendLine("    public static class B313StressImporter");
sb.AppendLine("    {");
sb.AppendLine("        public static void Import(MaterialStressService service)");
sb.AppendLine("        {");

int id = 1;

foreach (DataRow row in table.Rows)
{
    string spec = row["Spec. No."]?.ToString() ?? "";
    string grade = row["Type/Grade"]?.ToString() ?? "";
    string form = row["Product Form"]?.ToString() ?? "";
    string uns = row["UNS No."]?.ToString() ?? "";
    string condition = row.Table.Columns.Contains("Class/Condition/Temper")
        ? row["Class/Condition/Temper"]?.ToString() ?? ""
        : "";
    string notes = row.Table.Columns.Contains("Notes")
        ? row["Notes"]?.ToString() ?? ""
        : "";

    var values = tempColumns
        .Select(t =>
        {
            var val = row[t.i]?.ToString();
            return double.TryParse(val, out double d) ? d.ToString("0.###") : "null";
        })
        .ToList();

    sb.AppendLine("            service.ImportRecord(");
    sb.AppendLine("                StressEra.From1999Onward,");
    sb.AppendLine("                3.5,");
    sb.AppendLine($"                \"{Esc(spec)}\",");
    sb.AppendLine($"                \"{Esc(grade)}\",");
    sb.AppendLine($"                \"{Esc(form)}\",");
    sb.AppendLine($"                \"{Esc(uns)}\",");
    sb.AppendLine($"                \"{Esc(condition)}\",");
    sb.AppendLine("                new SortedDictionary<double, double?>()");
    sb.AppendLine("                {");

    for (int i = 0; i < tempColumns.Count; i++)
    {
        sb.AppendLine($"                    {{ {tempColumns[i].Temp}, {values[i]} }},");
    }

    sb.AppendLine("                },");
    sb.AppendLine($"                \"{Esc(notes)}\",");
    sb.AppendLine($"                {id},");
    sb.AppendLine($"                {id}");
    sb.AppendLine("            );");
    sb.AppendLine();

    id++;
}

sb.AppendLine("        }");
sb.AppendLine("    }");
sb.AppendLine("}");

File.WriteAllText(Path.Combine(outputFolder, "B313StressImporter.cs"), sb.ToString());

Console.WriteLine("DONE");

static string Esc(string value)
{
    return (value ?? "")
        .Replace("\\", "\\\\")
        .Replace("\"", "\\\"")
        .Replace("\r", " ")
        .Replace("\n", " ")
        .Trim();
}
Console.WriteLine("DONE");
Console.ReadLine();