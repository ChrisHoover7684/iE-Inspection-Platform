namespace iE.Api.Contracts;

public sealed record ApiErrorResponse(
    string Code,
    string Message,
    string TraceId,
    object? Details = null);
