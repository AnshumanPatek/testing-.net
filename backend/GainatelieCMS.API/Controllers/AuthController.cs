using Microsoft.AspNetCore.Mvc;
using GainatelieCMS.API.Services;
using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);
        
        if (result == null)
            return Unauthorized(new { message = "Invalid credentials" });
        
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);
        
        if (result == null)
            return Unauthorized(new { message = "Invalid refresh token" });
        
        return Ok(result);
    }
}
