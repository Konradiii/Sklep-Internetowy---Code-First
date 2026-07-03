using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs;
using SklepGrovly.Services.Authorization;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser(LoginUserDto user, CancellationToken ct)
    {
        var token = await service.LoginUser(user, ct);
        return Ok(new { token });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(RegisterUserDto user, CancellationToken ct)
    {
        await service.RegisterUser(user, ct);
        return Ok();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetUserDetails(CancellationToken ct)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(await service.GetUserDetails(userId, ct));
    }
    
    
}