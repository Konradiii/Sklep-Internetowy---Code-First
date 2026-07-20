using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Payments;

public class MockPaymentInfoDto
{

    public string IdBramki { get; set; }
    public decimal Kwota { get; set; }
    public int Id_Zamowienie { get; set; }
    public StatusPlatnosci Status { get; set; }   // żeby bramka wiedziała: przyciski czy komunikat

}