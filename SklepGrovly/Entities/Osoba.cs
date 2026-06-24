namespace SklepGrovly.Entities;

public abstract class Osoba
{
    public int Id_Osoba { get; set; }
    public string Imie { get; set; }
    public string Nazwisko  { get; set; }
    public string Email { get; set; }
    public string Haslo { get; set; }
    public string NrTelefonu { get; set; }
    public DateTime DataUrodzenia  { get; set; }
    
    public int Wiek => DateTime.Now.Year - DataUrodzenia.Year;
    
}