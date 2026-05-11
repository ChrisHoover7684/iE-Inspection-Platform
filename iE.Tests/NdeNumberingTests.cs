using iE.Api.Workflow;

public class NdeNumberingTests
{
    [Theory]
    [InlineData(2026, 1, "NDE-26-0001")]
    [InlineData(2026, 10, "NDE-26-0010")]
    public void FormatRequestNumber_Uses_Minimum_Four_Digit_Format(int year, int sequence, string expected)
    {
        Assert.Equal(expected, NdeNumbering.FormatRequestNumber(year, sequence));
    }

    [Theory]
    [InlineData(2026, "UT Thickness", 1, "RPT-26-UT-0001")]
    [InlineData(2026, "PAUT", 6, "RPT-26-PAUT-0006")]
    public void FormatReportNumber_Uses_Minimum_Four_Digit_Format(int year, string method, int sequence, string expected)
    {
        Assert.Equal(expected, NdeNumbering.FormatReportNumber(year, method, sequence));
    }

    [Theory]
    [InlineData("UT Thickness", "UT")]
    [InlineData("RT", "RT")]
    [InlineData("MT", "MT")]
    [InlineData("PT", "PT")]
    [InlineData("PMI", "PMI")]
    [InlineData("PAUT", "PAUT")]
    [InlineData("VT", "VT")]
    [InlineData("ET", "ET")]
    [InlineData("MFL", "MFL")]
    [InlineData("LRUT", "LRUT")]
    public void GetMethodAbbreviation_Maps_Known_Methods(string method, string expected)
    {
        Assert.Equal(expected, NdeNumbering.GetMethodAbbreviation(method));
    }
}
