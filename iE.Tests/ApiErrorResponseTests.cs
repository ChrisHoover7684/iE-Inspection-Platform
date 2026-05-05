using iE.Api.Contracts;

namespace iE.Tests;

public class ApiErrorResponseTests
{
    [Fact]
    public void Constructor_AssignsAllFields()
    {
        var details = new { field = "value" };

        var response = new ApiErrorResponse("validation_error", "Invalid request", "trace-123", details);

        Assert.Equal("validation_error", response.Code);
        Assert.Equal("Invalid request", response.Message);
        Assert.Equal("trace-123", response.TraceId);
        Assert.NotNull(response.Details);
    }
}
