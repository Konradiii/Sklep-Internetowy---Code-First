namespace SklepGrovly.Entities;

public class Klient : Osoba
{
    public Koszyk Koszyk { get; set; }
    
    public List<Opinia> Opinie { get; set; } = new();

}