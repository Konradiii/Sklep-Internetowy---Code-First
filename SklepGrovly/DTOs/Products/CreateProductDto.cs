using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.ProductsDto;

public class CreateProductDto
{
    [Required]
    [MaxLength(200)]
    public string Nazwa { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być dodatnia.")]
    public decimal Cena { get; set; }

    [Range(0, 100, ErrorMessage = "Zniżka musi być w zakresie 0–100%.")]
    public decimal? Znizka { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Ilość nie może być ujemna.")]
    public int? IloscNaStanie { get; set; }

    public int? Id_Kategoria { get; set; }


}