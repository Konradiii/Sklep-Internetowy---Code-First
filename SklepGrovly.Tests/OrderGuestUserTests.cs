using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Orders;
using SklepGrovly.Entities;
using SklepGrovly.Services.Orders;

namespace SklepGrovly.Tests;

public class OrderGuestUserTests
{
    [Fact]
    public async Task PlaceGuestOrder_ZnalezienieIstniejacegoUzytkownika_PodpiecieZamowieniaPodIstniejacy()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);
        
        var produkt = new Produkt 
        { 
            Id_Produkt = 1,
            Nazwa = "test",
            Cena = 100m,
            CzyAktywny = true,
            IloscNaStanie = 100,  
        };
        ctx.Produkt.Add(produkt);
        await ctx.SaveChangesAsync();
        
        var user2 = new GuestOrderDto
        {
            Email = "email@test.pl",
            ImieOdbiorcy =  "test2",
            NazwiskoOdbiorcy =  "test2",
            Ulica =  "test2",
            NrDomu = "23",
            KodPocztowy = "21-232",
            Miejscowosc = "test2",
            TelefonOdbiorcy =  "123123",
            Pozycje = new List<PlaceOrderItemsDto>
            {
                new PlaceOrderItemsDto { Id_Produkt = 1, Ilosc = 1 }
            }
        };
        
        var service = new OrderService(ctx);

        await service.PlaceGuestOrder(user2, CancellationToken.None);
        await service.PlaceGuestOrder(user2, CancellationToken.None);
        
        Assert.Equal(1, ctx.Set<Gosc>().Count());
        Assert.Equal(2, ctx.Zamowienie.Count());

    }
}