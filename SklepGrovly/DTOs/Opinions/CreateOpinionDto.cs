using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Opinions;

public class CreateOpinionDto
{
    [Range(1, 5)]
    public int Ocena { get; set; }
    [MaxLength(1000)]
    public string Tresc { get; set; }
    public int Id_Produkt {get; set;}
}