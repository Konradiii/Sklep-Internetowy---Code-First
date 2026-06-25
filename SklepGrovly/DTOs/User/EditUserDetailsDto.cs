using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class EditUserDetailsDto
{
    [Required]
    [MaxLength(50)]
    public string Imie { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nazwisko { get; set; }

    [Phone]
    public string NrTelefonu { get; set; }
}