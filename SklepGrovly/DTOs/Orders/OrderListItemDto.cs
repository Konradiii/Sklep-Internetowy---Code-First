using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Orders;

public class OrderListItemDto
{
    public int Id_Zamowienie { get; set; }
    public DateTime DataZamowienia { get; set; }
    public StatusZamowienia Status { get; set; }
    public decimal SumaCalkowita { get; set; }
}