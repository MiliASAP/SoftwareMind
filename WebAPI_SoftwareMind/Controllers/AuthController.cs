using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SoftwareMind.Models.Api;
using WebAPI_SoftwareMind.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService) =>
        _jwtService = jwtService;

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _jwtService.Authentication(request);
        if (result == null)
            return Unauthorized();

        return result;
    }

}