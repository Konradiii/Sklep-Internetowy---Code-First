using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Categories;

public class EditCategoryDto
{
    [Required]
    [StringLength(100)]
    public string Nazwa { get; set; }
}