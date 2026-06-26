using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Payments;

public class PaymentInitResultDto
{
    public int Id_Platnosc { get; set; }
    public StatusPlatnosci Status { get; set; }       // "Oczekujaca" po zainicjowaniu
    public decimal Kwota { get; set; }                // policzona przez serwer
    public MetodaPlatnosci MetodaPlatnosci { get; set; }

    // przy prawdziwej bramce: URL do przekierowania klienta na stronę płatności
    // przy mocku: może być null
    public string? UrlPlatnosci { get; set; }
}