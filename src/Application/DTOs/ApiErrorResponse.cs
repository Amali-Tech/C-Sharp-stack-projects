namespace Application.DTOs;

/// <summary>
/// Represents a standardized error response for API requests.
/// </summary>
/// <param name="Detail">Detailed error description.</param>
/// <param name="StatusCode">The HTTP status code.</param>
/// <param name="Title">Optional error title (default: null).</param>
/// <param name="Errors">Optional additional error details (default: null).</param>
public record ApiErrorResponse(
    string Detail,
    int StatusCode,
    string? Title = null,
    IEnumerable<KeyValuePair<string, string>>? Errors = null);