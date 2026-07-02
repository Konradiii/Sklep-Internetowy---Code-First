using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    public async Task<string> LoginUser(LoginUserDto user, CancellationToken ct)
    {
        
        var osoba = await ctx.Osoba
                .FirstOrDefaultAsync(o => o.Email == user.Email, ct);
        if (osoba == null || !BCrypt.Net.BCrypt.Verify(user.Haslo,osoba.Haslo))
        {
            throw new UnauthorizedException("Nieprawidłowy email lub hasło.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, osoba.Id_Osoba.ToString()),
            new(ClaimTypes.Email, osoba.Email),
            new(ClaimTypes.Role, osoba is Administrator ? "Administrator" : "Klient")
            
        };
        
        //// klucz do podpisu (z konfiguracji / user-secrets)
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),git 
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
}