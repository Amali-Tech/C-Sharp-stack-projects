namespace Application.DTOs;

/// <summary>
/// Represents a standardized success response for API requests.
/// </summary>
/// <typeparam name="T">The type of data returned.</typeparam>
/// <param name="StatusCode">The HTTP status code.</param>
/// <param name="Message">A descriptive message (default: empty).</param>
/// <param name="Data">The response data (default: null).</param>
public record ApiSuccessResponse<T>(
    int StatusCode,
    string Message = "",
    T? Data = default);