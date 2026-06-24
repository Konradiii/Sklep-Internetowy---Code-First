namespace SklepGrovly.Entities;

//Asocjacyjna encja wiele do wielu -> Proukt - Koszyk
public class PozycjaWKoszyku
{
    public int Id_Pozycja_Koszyk { get; set; }
    public int Ilosc { get; set; }
    public decimal  CenaPozycji => Ilosc * Produkt.Cena;

    public int Id_Koszyk  { get; set; }
    public Koszyk Koszyk { get; set; }
    
    public int Id_Produkt { get; set; }
    public Produkt Produkt { get; set; }
    
    
    


}