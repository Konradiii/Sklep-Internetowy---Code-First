using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs;

public class UserDetailsDto
{

    public string Imie { get; set; }
    
    public string Nazwisko { get; set; }
    
    public string Email { get; set; }
    
    public string NrTelefonu { get; set; }
    
    
}