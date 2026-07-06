using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.Orders;
using SklepGrovly.Enums;
using SklepGrovly.Services.Orders;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
[Tags("Zamowienia")]
public class OrderController(IOrderService service) : ControllerBase
{

    [HttpPost]
    [Authorize]
    [EndpointDescription("Złozenie zamowienia.")]
    public async Task<IActionResult> PlaceOrder(PlaceOrderDto dto, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var wynik = await service.PlaceOrder(klientId, dto, ct);
        
        return Ok(wynik);
    }


    [HttpGet("me")]
    [Authorize]
    [EndpointSummary("Moje zamówienia")]
    [EndpointDescription("Zwraca listę zamówień zalogowanego klienta.")]
    public async Task<IActionResult> GetMe(CancellationToken ct)
    {
        var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await service.GetAllOrdersByClient(id, ct);
        return Ok(result);
    }
    
    
    [HttpGet("klient/{klientId}")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Zamówienia klienta (admin)")]
    public async Task<IActionResult> GetOrders(int klientId, CancellationToken ct){
        var result = await service.GetAllOrdersByClient(klientId, ct);
        return Ok(result);}
    
    
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Wszystkie zamówienia (admin)")]
    public async Task<IActionResult> GetAllOrders(CancellationToken ct)
    {
        var result = await service.GetAllOrders(ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    [EndpointSummary("Szczegóły Zamówienia")]
    public async Task<IActionResult> GetOrderDetails(int id, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdmin = User.IsInRole("Administrator");
        
        return Ok(await service.GetOrderDetails(id, klientId, isAdmin, ct));
    }
    
    [HttpPatch("{id:int}/status")]
    [Authorize(Roles = "Administrator")]
    [EndpointSummary("Zmien Status zamowienia (admin)")]
    public async Task<IActionResult> ChangeOrderStatus(int id, StatusZamowienia nowyStatus, CancellationToken ct)
    {
        await service.ChangeOrderStatus(id, nowyStatus, ct);
        return NoContent();
    }
    
    [HttpPatch("{id:int}/anulowanie")]
    [Authorize]
    [EndpointSummary("Anuluj zamówienie")]
    public async Task<IActionResult> CancelOrder(int id, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdmin = User.IsInRole("Administrator");
        await service.CancelOrder(id, klientId, isAdmin, ct);
        return NoContent();
    }
}
