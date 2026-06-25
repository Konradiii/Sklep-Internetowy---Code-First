using SklepGrovly.DTOs.ProductsDto;

namespace SklepGrovly.Services.Products;

public interface IProductService
{
    Task<List<GetProductDto>> GetAllProducts(int? kategoriaId, CancellationToken ct );
    Task<GetProductDetailsDto> GetProduct(int productId, CancellationToken ct );
    
    // zwraca id nowo stworzonego
    Task<int> CreateProduct(CreateProductDto dto, CancellationToken ct );
    
    Task EditProduct(int productId, EditProductDto dto, CancellationToken ct);    
    
    Task<int> ArchiveProduct(int productId, CancellationToken ct);
    
    Task<List<GetOpinionOfProductDto>> GetOpinionsOfProduct(int productId, CancellationToken ct);
}