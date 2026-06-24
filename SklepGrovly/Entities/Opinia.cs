namespace SklepGrovly.Entities;

public class Opinia
{
    public int Id_Opinia { get; set; }
    public int Ocena { get; set; }
    public string Tresc { get; set; }
    public DateTime DataWystawienia { get; set; }
    
    public int Id_Klient { get; set; }
    public Klient Klient { get; set; }
    
    public int Id_Produkt { get; set; }
    public Produkt Produkt { get; set; }
    
}