using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Categories;
using SklepGrovly.Entities;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Categories;

public class CategorieService(ShopDbContext ctx) : ICategorieService
{

    public async Task<List<GetCategoriesDto>> GetAllCategories(CancellationToken ct)
    {
        return await ctx.Kategoria
            .Select(e => new GetCategoriesDto
            {
                Id_Kategoria = e.Id_Kategoria,
                Nazwa = e.Nazwa
                
            }).ToListAsync(ct);

    }

    public async Task AddCategory(AddCategoryDto dto, CancellationToken ct)
    {

        var nowaKategoria = new Kategoria
        {
            Nazwa = dto.Nazwa,
        };
        await ctx.Kategoria.AddAsync(nowaKategoria, ct);
        await ctx.SaveChangesAsync(ct);


    }

    public async Task EditCategory(int CategoryId, EditCategoryDto dto, CancellationToken ct)
    {
        var kategoria = await ctx.Kategoria.Where(e => e.Id_Kategoria == CategoryId).FirstOrDefaultAsync(ct);

        if (kategoria == null)
        {
            throw new NotFoundException($"Nie znaleziono kategori o id: {CategoryId}");
        }
        kategoria.Nazwa = dto.Nazwa;
        await ctx.SaveChangesAsync(ct);
    }

    public async Task DeleteCategory(int CategoryId, CancellationToken ct)
    {
        var kategoria = await ctx.Kategoria.Where(e => e.Id_Kategoria == CategoryId).FirstOrDefaultAsync(ct);

        if (kategoria == null)
        {
            throw new NotFoundException($"Nie znaleziono kategori o id: {CategoryId}");
        }
        
        bool maProdukty = await ctx.Kategoria.AnyAsync(e => e.Id_Kategoria == CategoryId, ct);
        if (maProdukty)
            throw new ConflictException($"Nie można usunąć kategorii o id {CategoryId}, ma przypisane produkty.");
        
        ctx.Kategoria.Remove(kategoria);
        await ctx.SaveChangesAsync(ct);
        
    }



}