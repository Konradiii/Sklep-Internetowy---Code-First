namespace SklepGrovly.DTOs.Orders;

public class OrderItemConfirmationDto
{
    public int Id_Produkt { get; set; }
    public string NazwaProduktu { get; set; }        // spłaszczone z encji
    public int Ilosc { get; set; }
    public decimal CenaJednostkowa { get; set; }     // zamrożona cena (CenaZakupu)
    public decimal CenaPozycji { get; set; }         // Ilosc * CenaJednostkowa
}