using SklepGrovly.DTOs.Categories;

namespace SklepGrovly.Services.Categories;

public interface ICategorieService
{
    Task<List<GetCategoriesDto>> GetAllCategories(CancellationToken ct);
    Task AddCategory(AddCategoryDto dto, CancellationToken ct);
    Task EditCategory(int CategoryId, EditCategoryDto dto, CancellationToken ct);
    Task DeleteCategory(int id, CancellationToken ct);
}