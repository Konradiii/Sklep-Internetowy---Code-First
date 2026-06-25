namespace SklepGrovly.DTOs.ProductsDto;

public class GetProductDto
{
    public int Id_Produkt { get; set; } 
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public decimal? Znizka { get; set; }  
}