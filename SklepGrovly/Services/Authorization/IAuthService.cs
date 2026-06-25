using SklepGrovly.DTOs;

namespace SklepGrovly.Services.Authorization;

public interface IAuthService
{
    Task RegisterUser(RegisterUserDto user, CancellationToken ct);
    Task<string> LoginUser(LoginUserDto dto, CancellationToken ct);
    Task<UserDetailsDto> GetUserDetails(CancellationToken ct);
    Task<UserDetailsDto> EditUserDetails(EditUserDetailsDto dto,CancellationToken ct);
    Task ChangePassword(ChangePasswordDto dto, CancellationToken ct);
    
}