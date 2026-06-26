using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Orders;

public class OrderConfirmationDto
{
    public int Id_Zamowienie { get; set; }           // numer do śledzenia / płatności
    public DateTime DataZamowienia { get; set; }
    public StatusZamowienia Status { get; set; }     // np. "Nowe"

    public List<OrderItemConfirmationDto> Pozycje { get; set; } = new();

    public decimal SumaCalkowita { get; set; }       // policzona przez serwer
}