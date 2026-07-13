using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Payments;
using SklepGrovly.Entities;
using SklepGrovly.Enums;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Payments;

public class PaymentService(ShopDbContext ctx) : IPaymentService
{

    public async Task<PaymentInitResultDto> InitiatePayment(int orderId, int klientId, CancellationToken ct)
    {
     
        var zamowienie = await ctx.Zamowienie
            .Include(s => s.PozycjaWZamowieniu)
            .FirstOrDefaultAsync(c=>c.Id_Zamowienie == orderId, ct);

        if (zamowienie == null)
        {
            throw new NotFoundException("Takie zamowienie nie istnieje");
        }
        if (klientId != zamowienie.Id_Osoba)
        {
            throw new NotFoundException("Takie zamowienie nie istnieje");
        }
        if (zamowienie.Status == StatusZamowienia.Oplacone)
        {
            throw new ConflictException("To zamowienie jest juz opłacone");
        }
        if (zamowienie.Status == StatusZamowienia.Anulowane)
        {
            throw new ConflictException("To zamowienie jest anulowane");
        }
        
        var istniejePlatnosc = await ctx.Platnosc.AnyAsync(p=>
            p.Id_Zamowienie == orderId && (p.StatusPlatnosci == StatusPlatnosci.Oczekujaca ||
            p.StatusPlatnosci == StatusPlatnosci.Zrealizowana), ct);
        
        if (istniejePlatnosc)
            throw new ConflictException("Płatność za to zamówienie już istnieje.");



        var platnosc = new Platnosc
        {
            Id_Zamowienie = orderId,
            KwotaPlatnosci = zamowienie.PozycjaWZamowieniu.Sum(p => p.CenaZakupu * p.Ilosc),
            DataPlatnosci = DateTime.UtcNow,
            StatusPlatnosci = StatusPlatnosci.Oczekujaca,
            IdZBramkiPlatniczej = Guid.NewGuid().ToString(),
        };
        

        ctx.Platnosc.Add(platnosc);
        await ctx.SaveChangesAsync(ct);

        return new PaymentInitResultDto
        {
            Id_Platnosc = platnosc.Id_Platnosc,
            Status = platnosc.StatusPlatnosci,
            LinkDoPlatnosci = $"http://mock-payment/pay/{platnosc.IdZBramkiPlatniczej}"
        };

    }

    public async Task HandleWebhook(string idBramki, bool sukces, CancellationToken ct)
    {
     
        var platnosc = await ctx.Platnosc
            .Include(p => p.Zamowienie)
            .FirstOrDefaultAsync(p=> p.IdZBramkiPlatniczej == idBramki, ct);
        
        if (platnosc == null)
            throw new NotFoundException("Nie znaleziono płatności.");
        
        if (platnosc.StatusPlatnosci != StatusPlatnosci.Oczekujaca)
            return;

        if (sukces)
        {
            platnosc.StatusPlatnosci = StatusPlatnosci.Zrealizowana;
            platnosc.Zamowienie.Status = StatusZamowienia.Oplacone;
        }
        else
        {
            platnosc.StatusPlatnosci = StatusPlatnosci.Odrzucona;
        }
        
        await ctx.SaveChangesAsync(ct);
        
        
    }
    
    
}