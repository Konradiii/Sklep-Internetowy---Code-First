using SklepGrovly.DTOs;

namespace SklepGrovly.Services.Authorization;

public interface IAuthService
{
    Task RegisterUser(RegisterUserDto user, CancellationToken ct);
    Task<LoginResponseDto> LoginUser(LoginUserDto dto, CancellationToken ct);
    Task<string> RefreshToken(string token, CancellationToken ct);
    Task<UserDetailsDto> GetUserDetails(int userId, CancellationToken ct);
    Task<UserDetailsDto> EditUserDetails(EditUserDetailsDto dto,CancellationToken ct); // Na pozniej
    Task ChangePassword(ChangePasswordDto dto, CancellationToken ct); // Na pozniej
    
}