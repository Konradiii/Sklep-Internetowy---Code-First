using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Haslo { get; set; }
}