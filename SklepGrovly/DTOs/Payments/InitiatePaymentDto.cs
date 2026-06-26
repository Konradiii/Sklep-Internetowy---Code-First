using System.ComponentModel.DataAnnotations;
using SklepGrovly.Enums;

namespace SklepGrovly.DTOs.Payments;

public class InitiatePaymentDto
{
    [Required]
    public MetodaPlatnosci MetodaPlatnosci { get; set; }
}