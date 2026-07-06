using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Orders;

public class PlaceOrderItemsDto
{
    [Required]
    public int Id_Produkt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być co najmniej 1.")]
    public int Ilosc { get; set; }
    
}