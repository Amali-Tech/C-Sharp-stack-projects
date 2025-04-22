using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
///     Represents the base controller for the API.
/// </summary>
/// <remarks>
///     This abstract controller provides common configuration and behavior for all derived API controllers.
///     It ensures consistent routing, content negotiation, and response formatting across the API.
/// </remarks>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
}