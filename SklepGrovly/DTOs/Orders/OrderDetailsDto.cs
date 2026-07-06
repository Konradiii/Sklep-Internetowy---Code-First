using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Orders;

public class OrderDetailsDto
{
    public int Id_Klient { get; set; }
    public int Id_Zamowienie { get; set; }
    public DateTime DataZamowienia { get; set; }
    public StatusZamowienia Status { get; set; }

    public List<OrderItemDto> Pozycje { get; set; } = new();

    public decimal SumaCalkowita { get; set; }

    // status płatności — nullowalny, bo płatność powstaje PO zamówieniu
    public StatusPlatnosci? StatusPlatnosci { get; set; }
    public MetodaPlatnosci? MetodaPlatnosci { get; set; }
}