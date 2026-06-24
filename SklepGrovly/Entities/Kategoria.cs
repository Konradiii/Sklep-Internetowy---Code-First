namespace SklepGrovly.Entities;

public class Kategoria
{
    public int Id_Kategoria  { get; set; }
    public string Nazwa { get; set; }
    
    //Lista produtków będąca w tej kategorii
    public List<Produkt> Produkty { get; set; } = new();
}