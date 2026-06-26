using System.ComponentModel.DataAnnotations;
using SklepGrovly.Entities;

namespace SklepGrovly.DTOs.Orders;

public class PlaceOrderDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Zamówienie musi zawierać co najmniej jedną pozycję.")]
    public List<PlaceOrderItemsDto> Pozycje { get; set; } = new();
}