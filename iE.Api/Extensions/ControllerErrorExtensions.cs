using iE.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Extensions;

public static class ControllerErrorExtensions
{
    public static BadRequestObjectResult ValidationError(this ControllerBase controller, string message, object? details = null)
        => controller.BadRequest(CreateError(controller, "validation_error", message, details));

    public static BadRequestObjectResult DomainError(this ControllerBase controller, string message, object? details = null)
        => controller.BadRequest(CreateError(controller, "domain_error", message, details));

    public static NotFoundObjectResult NotFoundError(this ControllerBase controller, string message, object? details = null)
        => controller.NotFound(CreateError(controller, "not_found", message, details));

    public static ApiErrorResponse CreateError(this ControllerBase controller, string code, string message, object? details = null)
        => new(code, message, controller.HttpContext.TraceIdentifier, details);
}
