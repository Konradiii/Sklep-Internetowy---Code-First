using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class ChangePasswordDto
{
    [Required]
    public string StareHaslo { get; set; }        // weryfikacja tożsamości

    [Required]
    [MinLength(8, ErrorMessage = "Hasło musi mieć min. 8 znaków.")]
    public string NoweHaslo { get; set; }

    [Required]
    [Compare(nameof(NoweHaslo), ErrorMessage = "Hasła nie są zgodne.")]
    public string PotwierdzNoweHaslo { get; set; }
}