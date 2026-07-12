using System.ComponentModel.DataAnnotations;
using SklepGrovly.Entities;

namespace SklepGrovly.DTOs.Orders;

public class PlaceOrderDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Zamówienie musi zawierać co najmniej jedną pozycję.")]
    public List<PlaceOrderItemsDto> Pozycje { get; set; } = new();
    
    [Required]
    [MaxLength(50)]
    public string ImieOdbiorcy { get; set; }
    [Required]
    [MaxLength(50)]
    public string NazwiskoOdbiorcy { get; set; }
    [Required]
    [MaxLength(100)]
    public string Ulica { get; set; }
    [Required]
    [MaxLength(100)]
    public string NrDomu { get; set; }
    [Required]
    [MaxLength(6)]
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy musi mieć format XX-XXX.")]
    public string KodPocztowy { get; set; }
    [Required]
    [MaxLength(100)]
    public string Miejscowosc { get; set; }
    [Required]
    [MaxLength(20)]
    public string TelefonOdbiorcy { get; set; }
    
}