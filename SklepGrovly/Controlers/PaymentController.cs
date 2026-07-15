using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.Payments;
using SklepGrovly.Services.Payments;

namespace SklepGrovly.Controlers;


[ApiController]
[Route("api/[controller]")]
[Tags("Płatności")]
public class PaymentController(IPaymentService service) : ControllerBase
{
    [HttpPost("zamowienie/{orderId:int}")]
    [Authorize]
    [EndpointSummary("Zainiciuj płatnosc")]
    public async Task<IActionResult> InitiatePayment(int orderId, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(await service.InitiatePayment(orderId, klientId, ct));
    }
    
    [HttpPost("webhook")]
    [AllowAnonymous]                   
    [EndpointSummary("Webhook płatności (symulacja bramki)")]
    public async Task<IActionResult> HandleWebhook([FromBody] WebhookDto dto, CancellationToken ct)
    {
        await service.HandleWebhook(dto.IdBramki , dto.Sukces, ct);
        return Ok();
    }
    
}