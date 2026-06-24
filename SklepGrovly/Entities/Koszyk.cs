namespace SklepGrovly.Entities;

public class Koszyk
{
    public int Id_Koszyk { get; set; }
    
    //1:1 z Klientem
    public int Id_Klient { get; set; }
    public Klient Klient { get; set; }
    
    public List<PozycjaWKoszyku> PozycjeWKoszyku { get; set; } = new();
    
}