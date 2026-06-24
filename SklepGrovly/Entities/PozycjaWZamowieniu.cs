namespace SklepGrovly.Entities;

public class PozycjaWZamowieniu
{
    public int Id_Pozycja_Zamowienie { get; set; }
    public int Ilosc { get; set; }
    public decimal  CenaZakupu { get; set; }
    
    public int Id_Zamowienie { get; set; }
    public Zamowienie Zamowienie { get; set; }
    
    public int Id_Produkt { get; set; }
    public Produkt Produkt { get; set; }
}