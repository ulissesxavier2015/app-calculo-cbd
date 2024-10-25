using Api.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure;

internal sealed class ErrorHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            ValidationErrorException validation => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = validation.Message
            },
            DomainExceptionValidation validation => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = validation.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = exception.Message
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
