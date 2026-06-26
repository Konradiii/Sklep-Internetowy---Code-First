namespace SklepGrovly.DTOs.Orders;

public class OrderItemDto
{
    public int Id_Produkt { get; set; }
    public string NazwaProduktu { get; set; }
    public int Ilosc { get; set; }
    public decimal CenaJednostkowa { get; set; }   // zamrożona CenaZakupu
    public decimal CenaPozycji { get; set; } 
}