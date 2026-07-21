using Microsoft.EntityFrameworkCore;
using SklepGrovly.Entities;
using SklepGrovly.Enums;
using SklepGrovly.Services.Payments;

namespace SklepGrovly.Tests;

public class PaymentServiceTests
{
    [Fact]
    public async Task HandleWebhook_SukcesNaOczekujacej_UstawiaZrealizowanaIOplacone()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);


        var zamowienie = new Zamowienie 
        { 
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test",
            NazwiskoOdbiorcy = "test",
            Ulica = "test",
            NrDomu = "1",
            KodPocztowy = "00-000",
            Miejscowosc = "test",
            TelefonOdbiorcy = "123",  
        };
        var platnosc = new Platnosc
        {
            Id_Platnosc = 7777, KwotaPlatnosci = 6969,
            MetodaPlatnosci = MetodaPlatnosci.Blik,
            StatusPlatnosci = StatusPlatnosci.Oczekujaca,
            IdZBramkiPlatniczej = "test-guid-123",
            Zamowienie = zamowienie
        };
            
        ctx.Platnosc.Add(platnosc);
        await ctx.SaveChangesAsync();
        
        var service = new PaymentService(ctx);
        
        await service.HandleWebhook("test-guid-123", true, CancellationToken.None );
        
        Assert.Equal(StatusPlatnosci.Zrealizowana, platnosc.StatusPlatnosci);
        Assert.Equal(StatusZamowienia.Oplacone, zamowienie.Status);

    }

    [Fact]
    public async Task
        HandleWebhook_PlatnoscOdrzucona_NieZmieniaStatusu()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);
        
        var zamowienie = new Zamowienie 
        { 
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test",
            NazwiskoOdbiorcy = "test",
            Ulica = "test",
            NrDomu = "1",
            KodPocztowy = "00-000",
            Miejscowosc = "test",
            TelefonOdbiorcy = "123",  
        };
        var platnosc = new Platnosc
        {
            Id_Platnosc = 7777, KwotaPlatnosci = 6969,
            MetodaPlatnosci = MetodaPlatnosci.Blik,
            StatusPlatnosci = StatusPlatnosci.Odrzucona,
            IdZBramkiPlatniczej = "test-guid-123",
            Zamowienie = zamowienie
        };
        
        ctx.Platnosc.Add(platnosc);
        await ctx.SaveChangesAsync();
        
        
        var service = new PaymentService(ctx);
        
        await service.HandleWebhook("test-guid-123", true, CancellationToken.None );
        
        Assert.Equal(StatusPlatnosci.Odrzucona, platnosc.StatusPlatnosci);
        Assert.Equal(StatusZamowienia.Nowe, zamowienie.Status);
    }
}