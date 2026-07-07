using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepGrovly.DTOs.Opinions;
using SklepGrovly.Services.Opinions;

namespace SklepGrovly.Controlers;

[ApiController]
[Route("api/[controller]")]
public class OpinionController(IOpinionService service) : ControllerBase
{

    [HttpPost]
    [Authorize]
    [EndpointSummary("Wystaw opinię")]
    public async Task<IActionResult> CreateOpinion(CreateOpinionDto dto, CancellationToken ct)
    {
        var idOpinii = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await service.CreateOpinion(idOpinii, dto, ct);
        return Created();
    }
    
    [HttpPut("{id:int}")]
    [Authorize]
    [EndpointSummary("Edytuj opinię")]
    public async Task<IActionResult> EditOpinion(int idOpinii, EditOpinionDto dto, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdmin = User.IsInRole("Administrator");   // ← "Administrator", nie "Admin"
        await service.EditOpinion(idOpinii, klientId, isAdmin, dto, ct);
        return NoContent();
    }
    
    
    [HttpDelete("{id:int}")]
    [Authorize]
    [EndpointSummary("Usuń opinię")]
    public async Task<IActionResult> DeleteOpinion(int id, CancellationToken ct)
    {
        var klientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdmin = User.IsInRole("Administrator");
        await service.DeleteOpinion(id, klientId, isAdmin, ct);
        return NoContent();
    }
}