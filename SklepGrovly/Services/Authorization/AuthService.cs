using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SklepGrovly.DTOs;
using SklepGrovly.Entities;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Authorization;

public class AuthService(ShopDbContext ctx, IConfiguration config) : IAuthService
{
    public async Task RegisterUser(RegisterUserDto user, CancellationToken ct)
    {
        
        bool emailZajety = await ctx.Osoba
            .AnyAsync(o => o.Email == user.Email, ct);

        if (emailZajety)
            throw new ConflictException($"Konto z emailem {user.Email} już istnieje.");

        var nowyUzytkownik = new Klient
        {
            Imie = user.Imie,
            Nazwisko = user.Nazwisko,
            Email = user.Email,
            Haslo = BCrypt.Net.BCrypt.HashPassword(user.Haslo, workFactor: 12),
            NrTelefonu = user.NrTelefonu,

        };
        
        ctx.Add(nowyUzytkownik);
        await ctx.SaveChangesAsync(ct);
    }

    public async Task<LoginResponseDto> LoginUser(LoginUserDto user, CancellationToken ct)
    {
        
        var osoba = await ctx.Osoba
                .FirstOrDefaultAsync(o => o.Email == user.Email, ct);
        
        if (osoba == null || !BCrypt.Net.BCrypt.Verify(user.Haslo,osoba.Haslo))
        {
            throw new UnauthorizedException("Nieprawidłowy email lub hasło.");
        }
        
        var accessToken = GenerateAccessToken(osoba);
        var refreshToken = GenerateAndStoreRefreshToken(osoba.Id_Osoba);
        await ctx.SaveChangesAsync(ct);


        return new LoginResponseDto{
            AccessToken =  accessToken,
            RefreshToken =  refreshToken
        };
        
    }
    
    

    public async Task<RefreshResponseDto> RefreshTokenAsync(string token, CancellationToken ct)
    {
        var hash = HashToken(token);
        
        var stary = await ctx.RefreshToken
            .Include(rt => rt.Osoba)
            .FirstOrDefaultAsync(rt => rt.TokenHash == hash, ct);
        if (stary == null)
        {
            throw new UnauthorizedException("Nieprawidłowy token");
        }

        if (stary.RevokedAt != null)
        {
            throw new UnauthorizedException("Token zostal juz wykorzystany.");
        }

        if (stary.ExpiresAt <= DateTime.UtcNow)
        {
            throw new UnauthorizedException("Token wygasł.");
        }
        
        stary.RevokedAt = DateTime.UtcNow;
        
        var accessToken = GenerateAccessToken(stary.Osoba);
        var refreshToken = GenerateAndStoreRefreshToken(stary.Id_Osoba);
        
        await ctx.SaveChangesAsync(ct);
        return new RefreshResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };


    }


    private string GenerateAccessToken(Osoba osoba)
    {
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, osoba.Id_Osoba.ToString()),
            new(ClaimTypes.Email, osoba.Email),
            new(ClaimTypes.Role, osoba is Administrator ? "Administrator" : "Klient")
            
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var access_token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(access_token);


    }
    
    
    private string HashToken(string rawToken)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawToken)));
    
    
    
    
    private string GenerateAndStoreRefreshToken(int osobaId)
    {
        var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        ctx.RefreshToken.Add(new RefreshToken
        {
            TokenHash = HashToken(rawToken),  
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(15),
            Id_Osoba = osobaId,
        });
        return rawToken; 
    }
    
    
    
    
    

    public async Task<UserDetailsDto> GetUserDetails(int userId, CancellationToken ct)
    {
        return await ctx.Osoba
            .Where(o => o.Id_Osoba == userId)
            .Select(o => new UserDetailsDto
            {
                Imie = o.Imie,
                Nazwisko = o.Nazwisko,
                Email = o.Email,
                NrTelefonu = o.NrTelefonu
            })
            .FirstOrDefaultAsync(ct);
    }
    
    public Task<UserDetailsDto> EditUserDetails(EditUserDetailsDto dto,CancellationToken ct)
    {
        return null;
    }

    public Task ChangePassword(ChangePasswordDto dto, CancellationToken ct)
    {
        return null;
    }

    
}