using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class EditUserDetailsDto
{
    [Phone]
    public string NrTelefonu { get; set; }
}