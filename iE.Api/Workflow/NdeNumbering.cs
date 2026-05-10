namespace iE.Api.Workflow;

public static class NdeNumbering
{
    public static string FormatRequestNumber(int year, int sequence)
        => $"NDE-{year % 100:00}-{sequence:000}";

    public static string FormatReportNumber(int year, string method, int sequence)
        => $"RPT-{year % 100:00}-{GetMethodAbbreviation(method)}-{sequence:000}";

    public static string GetMethodAbbreviation(string method)
        => method.Trim() switch
        {
            "UT Thickness" => "UT",
            "RT" => "RT",
            "MT" => "MT",
            "PT" => "PT",
            "PMI" => "PMI",
            "PAUT" => "PAUT",
            "VT" => "VT",
            "ET" => "ET",
            "MFL" => "MFL",
            "LRUT" => "LRUT",
            _ => method.Trim().ToUpperInvariant().Replace(" ", string.Empty)
        };
}
