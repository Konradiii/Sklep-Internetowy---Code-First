using SklepGrovly.Enums;

namespace SklepGrovly.Entities;

public class Zamowienie
{
    public int Id_Zamowienie { get; set; }
    public DateTime DataZamowienia { get; set; }
    public StatusZamowienia Status { get; set; }
    
    public int Id_Osoba { get; set; }   
    public Osoba Osoba { get; set; }
    
    public string ImieOdbiorcy { get; set; }
    public string NazwiskoOdbiorcy { get; set; }
    
    public string Ulica { get; set; }
    public string NrDomu { get; set; }
    public string KodPocztowy { get; set; }
    public string Miejscowosc { get; set; }
    
    public string TelefonOdbiorcy { get; set; }
    
    //laczenie z pozycja zamowienia
    
    public List<PozycjaWZamowieniu> PozycjaWZamowieniu { get; set; } = new();
    
    public Platnosc Platnosc { get; set; }  
    
}