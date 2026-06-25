using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class RegisterUserDto
{
    [Required]
    [MaxLength(50)]
    public string Imie { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nazwisko { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Hasło musi mieć min. 8 znaków.")]
    public string Haslo { get; set; }

    [Required]
    [Compare(nameof(Haslo), ErrorMessage = "Hasła nie są zgodne.")]
    public string PotwierdzHaslo { get; set; }

    [Phone]
    public string NrTelefonu { get; set; }
    
}