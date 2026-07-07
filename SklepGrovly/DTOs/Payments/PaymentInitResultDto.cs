using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Payments;

public class PaymentInitResultDto
{
    public int Id_Platnosc { get; set; }
    public StatusPlatnosci Status { get; set; }     
    public string? LinkDoPlatnosci  { get; set; }
}