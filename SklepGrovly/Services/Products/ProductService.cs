using Microsoft.EntityFrameworkCore;
using SklepGrovly.Configurations;
using SklepGrovly.DTOs.ProductsDto;
using SklepGrovly.Entities;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Products;

public class ProductService(ShopDbContext ctx) : IProductService
{

    public async Task<List<GetProductDto>> GetAllProducts(int? kategoriaId, CancellationToken ct)
    {

        return await ctx.Produkt
            .Where( p => p.CzyAktywny == true)
            .Where( p => kategoriaId == null || p.Id_Kategoria == kategoriaId)
            .Select( p => new GetProductDto
            {
                Id_Produkt = p.Id_Produkt, 
                Nazwa = p.Nazwa, 
                Cena = p.Cena, 
                Znizka = p.Znizka
            }
            ).ToListAsync(ct);
    }

    public async Task<GetProductDetailsDto> GetProduct(int productId, CancellationToken ct)
    {
        var produkt = await ctx.Produkt
            .Where(p => p.Id_Produkt == productId)
            .Select(p => new GetProductDetailsDto
            {
                Id_Produkt = p.Id_Produkt,
                Nazwa = p.Nazwa,
                Cena = p.Cena,
                Znizka = p.Znizka,
                IloscNaStanie = p.IloscNaStanie,
                Id_Kategoria = p.Id_Kategoria,
                NazwaKategorii = p.Kategoria.Nazwa

            }).FirstOrDefaultAsync(ct);

        if (produkt == null)
        {
            throw new NotFoundException($"Produkt o id {productId} nie istnieje.");
        }
            
        return produkt;
    }

    public async Task<int> CreateProduct(CreateProductDto dto, CancellationToken ct)
    {

        var produkt = new Produkt
        {
            Nazwa = dto.Nazwa,
            Cena = dto.Cena,
            Znizka = dto.Znizka,
            IloscNaStanie = dto.IloscNaStanie,
            Id_Kategoria = dto.Id_Kategoria
        };
        await ctx.Produkt.AddAsync(produkt, ct);
        await ctx.SaveChangesAsync(ct);
        
        return produkt.Id_Produkt;
    }

    public async Task EditProduct(int productId, EditProductDto dto, CancellationToken ct)
    {
        var produkt = await ctx.Produkt
            .FirstOrDefaultAsync(p => p.Id_Produkt == productId, ct);

        if (produkt == null)
        {
            throw new NotFoundException($"Produkt o id {productId} nie istnieje.");
        }
        
        produkt.Nazwa = dto.Nazwa;
        produkt.Cena = dto.Cena;
        produkt.Znizka = dto.Znizka;
        produkt.IloscNaStanie = dto.IloscNaStanie;
        produkt.Id_Kategoria = dto.Id_Kategoria;
        
        await ctx.SaveChangesAsync(ct);
        
        
    }

    public async Task<int> ArchiveProduct(int productId, CancellationToken ct)
    {
        
        var produkt = await ctx.Produkt
            .FirstOrDefaultAsync(p => p.Id_Produkt == productId, ct);

        if (produkt == null)
        {
            throw new NotFoundException($"Produkt o id {productId} nie istnieje.");
        }

        if (produkt.CzyAktywny == false)
        {
            throw new ConflictException($"Produkt o id {productId} jest już zarchiwizowany.");
        }
        
        produkt.CzyAktywny = false;
        await ctx.SaveChangesAsync(ct);
        return produkt.Id_Produkt;
        
        
    }

    public async Task<List<GetProductDto>> GetArchiveProducts(int? kategoriaId, CancellationToken ct)
    {

        return await ctx.Produkt
            .Where(p => kategoriaId == null || p.Id_Kategoria == kategoriaId)
            .Where(p => p.CzyAktywny == false)
            .Select(p => new GetProductDto
            {
                Id_Produkt = p.Id_Produkt, 
                Nazwa = p.Nazwa, 
                Cena = p.Cena, 
                Znizka = p.Znizka

            }).ToListAsync(ct);

    }




    public async Task<List<GetOpinionOfProductDto>> GetOpinionsOfProduct(int productId, CancellationToken ct)
    {
        
        var Opinia = ctx.Opinia.Where(p => p.Id_Produkt == productId).FirstOrDefault();

        if (Opinia == null)
        {
            throw new NotFoundException($"Produkt o id {productId} nie istnieje.");
        }

        return await ctx.Opinia
            .Where(p => p.Id_Produkt == productId)
            .Select(e => new GetOpinionOfProductDto
            {
                Id_Opinia = e.Id_Opinia,
                Ocena = e.Ocena,
                Tresc = e.Tresc,
                DataWystawienia = e.DataWystawienia


            }).ToListAsync(ct);



    }



    
    



}
