using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.Orders;
using SklepGrovly.Services.Orders;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService service) : ControllerBase
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PlaceOrder(PlaceOrderDto dto, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var wynik = await service.PlaceOrder(klientId, dto, ct);
        
        return Ok(wynik);
    }
    
    
}