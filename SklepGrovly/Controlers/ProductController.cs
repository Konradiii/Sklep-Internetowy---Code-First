using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.ProductsDto;
using SklepGrovly.Exceptions;
using SklepGrovly.Services.Products;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service) : ControllerBase
{

    [HttpGet("getAllProducts")]
    public Task<List<GetProductDto>> GetAllProducts(int? kategoriaId, CancellationToken ct)
    {
        return service.GetAllProducts(kategoriaId, ct);
    }
    
    
    [HttpGet("GetArchiveProducts")]
    public Task<List<GetProductDto>> GetArchiveProducts(int? kategoriaId, CancellationToken ct)
    {
            return service.GetArchiveProducts(kategoriaId, ct);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken ct)
    {
            return Ok(await service.GetProduct(id, ct));

    }
    
    [HttpGet("{productId:int}/OpinionsOfProduct")]
    public async Task<IActionResult> GetOpinionsOfProduct(int productId, CancellationToken ct)
    {
            return Ok(await service.GetOpinionsOfProduct(productId, ct));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken ct)
    {
            await service.CreateProduct(dto, ct);
            return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductDto dto, CancellationToken ct)
    {
            await service.EditProduct(id, dto, ct);
            return NoContent();
    }

    [HttpPatch("{productId:int}/archiwizujProdukt")]
    public async Task<IActionResult> ArchiveProduct(int productId, CancellationToken ct)
    {
            await service.ArchiveProduct(productId, ct);
            return NoContent();
        
     
    }
    

}