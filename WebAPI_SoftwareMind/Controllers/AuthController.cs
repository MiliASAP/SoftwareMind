using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI_SoftwareMind.Models.Api;
using WebAPI_SoftwareMind.Services.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService) =>
        _jwtService = jwtService;

    /// <summary>
    /// Logs in a user and returns an authentication token.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>A login response with a JWT token if authentication is successful.</returns>
    /// <response code="200">Returns the JWT token on successful authentication</response>
    /// <response code="401">Unauthorized, invalid credentials</response>
    [HttpPost("Login")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Logs in a user and returns a JWT token.", Description = "Authenticates the user with the provided credentials and returns a JWT token for future requests.")]
    [SwaggerResponse(200, "Returns the JWT token.", typeof(LoginResponse))]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _jwtService.Authentication(request);
        if (result == null)
            return Unauthorized();

        return result;
    }

}