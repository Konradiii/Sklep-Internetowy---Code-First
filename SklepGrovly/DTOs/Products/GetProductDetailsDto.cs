using SklepGrovly.Entities;

namespace SklepGrovly.DTOs.ProductsDto;

public class GetProductDetailsDto
{
    public int Id_Produkt { get; set; }
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public decimal? Znizka { get; set; }
    public int? IloscNaStanie { get; set; }

    public int? Id_Kategoria { get; set; }
    public string? NazwaKategorii { get; set; }

}