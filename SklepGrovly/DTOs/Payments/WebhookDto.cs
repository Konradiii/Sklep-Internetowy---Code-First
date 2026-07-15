using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Payments;

public class WebhookDto
{
    [Required]
    public string IdBramki { get; set; }
    public bool Sukces { get; set; }
}