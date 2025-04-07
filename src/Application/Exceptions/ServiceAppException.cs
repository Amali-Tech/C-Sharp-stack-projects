using Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

/// <summary>
/// Base class for custom application exceptions.
/// </summary>
public abstract class ServiceAppException : Exception
{
    protected ServiceAppException(string message) : base(message)
    {
    }

    public virtual int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
    protected virtual string? Title { get; init; }
    protected virtual IEnumerable<KeyValuePair<string, string>>? Errors { get; init; }

    public ApiErrorResponse FormatError() => new(
        Detail: Message,
        StatusCode: StatusCode,
        Title: Title,
        Errors: Errors);
}