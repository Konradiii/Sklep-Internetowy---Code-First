using SklepGrovly.Enums;

namespace SklepGrovly.Entities;

public class Zamowienie
{
    public int Id_Zamowienie { get; set; }
    public DateTime DataZamowienia { get; set; }
    public StatusZamowienia Status { get; set; }
    
    public int Id_Klient { get; set; }   
    public Klient Klient { get; set; }
    
    //laczenie z pozycja zamowienia
    
    public List<PozycjaWZamowieniu> PozycjaWZamowieniu { get; set; } = new();
    
    public Platnosc Platnosc { get; set; }  
    
}