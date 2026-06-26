using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Payments;

public class PaymentStatusDto
{
    public int Id_Platnosc { get; set; }
    public StatusPlatnosci Status { get; set; }
    public decimal Kwota { get; set; }
    public MetodaPlatnosci MetodaPlatnosci { get; set; }
    public DateTime DataPlatnosci { get; set; }
}