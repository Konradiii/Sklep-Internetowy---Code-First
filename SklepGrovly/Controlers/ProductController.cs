using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.ProductsDto;
using SklepGrovly.Exceptions;
using SklepGrovly.Services.Products;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
[Tags("Produkty")]
public class ProductController(IProductService service) : ControllerBase
{

    [HttpGet("getAllProducts")]
    [AllowAnonymous]
    [EndpointSummary("Lista produktów")]
    public Task<List<GetProductDto>> GetAllProducts(int? kategoriaId, CancellationToken ct)
    {
        return service.GetAllProducts(kategoriaId, ct);
    }
    
    
    [HttpGet("archiwum")] 
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Zarchiwizowane produkty (admin)")]
    public Task<List<GetProductDto>> GetArchiveProducts(int? kategoriaId, CancellationToken ct)
    {
            return service.GetArchiveProducts(kategoriaId, ct);
    }


    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [EndpointSummary("Szczegóły produktu")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken ct)
    {
            return Ok(await service.GetProduct(id, ct));

    }
    
    [HttpGet("{id:int}/opinie")]  
    [AllowAnonymous]
    [EndpointSummary("Opinie o produkcie")]
    public async Task<IActionResult> GetOpinionsOfProduct(int productId, CancellationToken ct)
    {
            return Ok(await service.GetOpinionsOfProduct(productId, ct));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Dodaj produkt (admin)")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken ct)
    {
            await service.CreateProduct(dto, ct);
            return Created();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Edytuj produkt (admin)")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductDto dto, CancellationToken ct)
    {
            await service.EditProduct(id, dto, ct);
            return NoContent();
    }

    [HttpPatch("{id:int}/archiwizacja")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Archiwizuj produkt (admin)")]
    public async Task<IActionResult> ArchiveProduct(int productId, CancellationToken ct)
    {
            await service.ArchiveProduct(productId, ct);
            return NoContent();
        
     
    }
    

}