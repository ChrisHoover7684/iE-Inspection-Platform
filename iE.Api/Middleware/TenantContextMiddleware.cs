using System.Text.Json;
using iE.Api.Contracts;
using iE.Api.Tenancy;

namespace iE.Api.Middleware;

public sealed class TenantContextMiddleware(RequestDelegate next, ILogger<TenantContextMiddleware> logger)
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public async Task InvokeAsync(HttpContext context, ITenantContextAccessor accessor, ITenantContextBuilder builder)
    {
        try
        {
            accessor.Current = builder.Build(context.User, context.TraceIdentifier);
            await next(context);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex,
                "Invalid tenant context for {Method} {Path}. TraceId={TraceId}",
                context.Request.Method,
                context.Request.Path.Value,
                context.TraceIdentifier);

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResponse(
                "tenant_context_invalid",
                "Authenticated principal is missing required tenant context.",
                context.TraceIdentifier);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, SerializerOptions));
        }
    }
}
