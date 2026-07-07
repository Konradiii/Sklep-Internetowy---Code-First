using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Opinions;
using SklepGrovly.Entities;
using SklepGrovly.Enums;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Opinions;

public class OpinionService(ShopDbContext ctx) : IOpinionService
{
    public async Task CreateOpinion(int klientId, CreateOpinionDto dto, CancellationToken ct)
    {

        var kupil = await ctx.Zamowienie
            .Where(e => e.Status == StatusZamowienia.Dostarczone)
            .Where(e => e.Id_Klient == klientId)
            .Where(p => p.PozycjaWZamowieniu.Any(e => e.Id_Produkt == dto.Id_Produkt))
            .AnyAsync(ct);

        
        if (!kupil)
        {
            throw new ConflictException("Możesz ocenić tylko produkt, który kupiłeś.");
        }
        
        var juzOcenil = await ctx.Opinia
            .AnyAsync(o => o.Id_Klient == klientId && o.Id_Produkt == dto.Id_Produkt, ct);
        if (juzOcenil)
            throw new ConflictException("Już oceniłeś ten produkt.");

        ctx.Opinia.Add(new Opinia
        {
            Id_Klient = klientId,        
            Id_Produkt = dto.Id_Produkt,
            Ocena = dto.Ocena,
            Tresc = dto.Tresc,
            DataWystawienia = DateTime.UtcNow,
        });
        await ctx.SaveChangesAsync(ct);
        




    }

    public async Task EditOpinion(int opinionId, int klientId, bool isAdmin, EditOpinionDto dto, CancellationToken token)
    {

        var opinia = await ctx.Opinia.FirstOrDefaultAsync(e => e.Id_Opinia == opinionId);

        if (opinia == null)
            throw new NotFoundException("Opini nie znaleziono.");
        
        if(!isAdmin && opinia.Id_Klient != klientId)
            throw new NotFoundException("Nie ma takiej opinii.");
        
        opinia.Ocena = dto.Ocena;
        opinia.Tresc = dto.Tresc;
        
        await ctx.SaveChangesAsync(token);
        







    }

    public async Task DeleteOpinion(int opinionId, int klientId, bool isAdmin, CancellationToken ct)
    {

        var opinia = await ctx.Opinia
            .FirstOrDefaultAsync(o => o.Id_Opinia == opinionId, ct);

        if (opinia == null)
            throw new NotFoundException("Nie ma takiej opinii.");
        
        if (opinia.Id_Klient != klientId && !isAdmin)
            throw new NotFoundException("Nie ma takiej opinii.");
        
        
        ctx.Opinia.Remove(opinia);
        await ctx.SaveChangesAsync(ct);


    }
    
}