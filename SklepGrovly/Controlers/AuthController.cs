using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs;
using SklepGrovly.Services.Authorization;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
[Tags("Autoryzacja")]
public class AuthController(IAuthService service) : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    [EndpointSummary("Logowanie")]
    [EndpointDescription("Zwraca access token i refresh token.")]
    public async Task<IActionResult> LoginUser(LoginUserDto user, CancellationToken ct)
    {
        var token = await service.LoginUser(user, ct);
        return Ok(token);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [EndpointSummary("Rejestracja nowego klienta")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto user, CancellationToken ct)
    {
        await service.RegisterUser(user, ct);
        return Ok();
    }

    [HttpGet("me")]
    [Authorize]
    [EndpointSummary("Dane zalogowanego użytkownika")]
    public async Task<IActionResult> GetUserDetails(CancellationToken ct)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(await service.GetUserDetails(userId, ct));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [EndpointSummary("Odświeżenie tokenu")]
    [EndpointDescription("Wymienia ważny refresh token na nową parę tokenów (rotacja).")]
    public async Task<IActionResult> RefreshToken(RefreshReqDto dto, CancellationToken ct)
    {
        var result = await service.RefreshTokenAsync(dto.RefreshToken, ct);
        return Ok(result);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    [EndpointSummary("Wylogowanie")]
    [EndpointDescription("Unieważnia refresh token.")]
    public async Task<IActionResult> Logout(LogoutDto dto, CancellationToken ct)
    {
        await service.Logout(dto, ct);
        return Ok();
    }

    [HttpPut("me")]
    [Authorize]
    [EndpointSummary("Edytuj moje dane")]
    public async Task<IActionResult> EditUserDetails(EditUserDetailsDto dto, CancellationToken ct)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await service.EditUserDetails(userId, dto, ct);
        return Ok();
    }
    
    [HttpPut("me/haslo")]
    [Authorize]
    [EndpointSummary("Zmień hasło")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto, CancellationToken ct)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await service.ChangePassword(userId, dto, ct);
        return NoContent();
    }

}