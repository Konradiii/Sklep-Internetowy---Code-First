using System.ComponentModel.DataAnnotations;

namespace SklepGrovly.DTOs.Categories;

public class AddCategoryDto
{
    [Required]
    [StringLength(100)]
    public string Nazwa { get; set; }
    

}