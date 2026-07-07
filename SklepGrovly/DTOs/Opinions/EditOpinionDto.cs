using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Opinions;

public class EditOpinionDto
{
    [Range(1, 5)]
    public int Ocena { get; set; }
    public string Tresc { get; set; }
}