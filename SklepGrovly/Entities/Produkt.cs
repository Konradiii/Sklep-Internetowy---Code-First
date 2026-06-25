namespace SklepGrovly.Entities;

public class Produkt
{
    public int Id_Produkt { get; set; }
    public string Nazwa { get; set; }
    public decimal Cena { get; set; }
    public decimal? Znizka { get; set; }
    public int? IloscNaStanie { get; set; }
    
    
    public bool CzyAktywny { get; set; } = true; 
    
    //Klucz obcy
    public int? Id_Kategoria { get; set; }
    
    //nawigacja property
    public Kategoria? Kategoria { get; set; }

    public List<PozycjaWKoszyku> PozycjaWKoszyku { get; set; } = new();

    public List<PozycjaWZamowieniu> PozycjaWZamowieniu { get; set; } = new();

    public List<Opinia> Opinie { get; set; } = new();
    
}