using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.Categories;
using SklepGrovly.Services.Categories;

namespace SklepGrovly.Controlers;

[ApiController]
[Route("api/[controller]")]
[Tags("Kategorie")] 
public class CategoryController(ICategorieService service) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [EndpointSummary("Lista kategorii")]
    public async Task<List<GetCategoriesDto>> GetAllCategories(CancellationToken ct)
    {
        var result = await service.GetAllCategories(ct);
        return result;
    }

    [HttpPost("AddCategory")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Dodaj kategorię (admin)")]
    public async Task<IActionResult> AddCategory(AddCategoryDto dto, CancellationToken ct)
    {
        await service.AddCategory(dto, ct);
        return Created();
    }

    [HttpPut("{categoryId:int}")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Edytuj kategorię (admin)")]
    public async Task<IActionResult> UpdateCategory(int categoryId ,EditCategoryDto dto, CancellationToken ct)
    {
        await service.EditCategory(categoryId, dto, ct);
        return NoContent();
        
    }

    [HttpDelete("{categoryId:int}")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Usuń kategorię (admin)")]
    public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken ct)
    {
        await service.DeleteCategory(categoryId, ct);
        return NoContent();
        
    }

    
    
}