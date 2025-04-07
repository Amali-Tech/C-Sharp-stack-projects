using Application.DTOs;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api;

/// <summary>
/// Handles exceptions globally across the API.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        ApiErrorResponse errorResponse;

        if (exception is ServiceAppException appException)
        {
            errorResponse = appException.FormatError();
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }

        _logger.LogError(exception, "Unexpected error: {Message}", exception.Message);
        errorResponse = new ApiErrorResponse("Internal Server Error", StatusCodes.Status500InternalServerError);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        return true;
    }
}