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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken ct)
    {
        try
        {
            return Ok(await service.GetProduct(id, ct));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto, CancellationToken ct)
    {
        try
        {
            await service.CreateProduct(dto, ct);
            return Created();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }

}