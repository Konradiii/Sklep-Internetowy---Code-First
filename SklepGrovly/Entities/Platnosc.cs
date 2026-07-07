using SklepGrovly.Enums;

namespace SklepGrovly.Entities;

public class Platnosc
{
    public int Id_Platnosc { get; set; }
    public decimal  KwotaPlatnosci { get; set; }
    
    public DateTime DataPlatnosci { get; set; }
    public MetodaPlatnosci  MetodaPlatnosci { get; set; }
    public StatusPlatnosci StatusPlatnosci { get; set; }
    public string? IdZBramkiPlatniczej { get; set; }
    
    public int Id_Zamowienie { get; set; }
    public Zamowienie Zamowienie { get; set; } = null!;
    
}