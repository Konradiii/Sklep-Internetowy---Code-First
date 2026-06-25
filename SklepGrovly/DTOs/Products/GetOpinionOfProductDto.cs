namespace SklepGrovly.DTOs.ProductsDto;

public class GetOpinionOfProductDto
{
    public int Id_Opinia { get; set; }
    public int Ocena { get; set; }
    public string Tresc { get; set; }
    public DateTime DataWystawienia { get; set; }
    
    public string NazwaKlienta  { get; set; }

}