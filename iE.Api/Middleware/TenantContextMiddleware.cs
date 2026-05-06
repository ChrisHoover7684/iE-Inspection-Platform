using iE.Api.Tenancy;

namespace iE.Api.Middleware;

public sealed class TenantContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ITenantContextAccessor accessor, ITenantContextBuilder builder)
    {
        accessor.Current = builder.Build(context.User, context.TraceIdentifier);
        await next(context);
    }
}
