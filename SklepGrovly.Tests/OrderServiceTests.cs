using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Orders;
using SklepGrovly.Entities;
using SklepGrovly.Enums;
using SklepGrovly.Exceptions;
using SklepGrovly.Services.Orders;

namespace SklepGrovly.Tests;

public class OrderServiceTests
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
    
    [Fact]
    public async Task GetOrderDetails_OsobaWyswietlaSwojeZamowienie_ZwróconyPowinienZostacJejZamowienie()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);
        
        var user1 = new Klient{ Id_Osoba = 1,Imie = "test1", Nazwisko = "test1", Email = "test@email.pl", Haslo = "sadasdas", NrTelefonu = "231123123"};
        var user2 = new Klient{ Id_Osoba = 2,Imie = "test2", Nazwisko = "test2",Email = "test2@email.pl" , Haslo = "sadasdas", NrTelefonu = "12312312"};
        
        ctx.Osoba.Add(user1);
        ctx.Osoba.Add(user2);
        await ctx.SaveChangesAsync();

        var zamowienie1 = new Zamowienie { 
            Id_Zamowienie = 1,
            Id_Osoba = 1,
            DataZamowienia = DateTime.UtcNow,
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test1",
            NazwiskoOdbiorcy = "test1",
            Ulica = "test1",
            NrDomu = "test1",
            KodPocztowy = "test1",
            Miejscowosc = "test",
            TelefonOdbiorcy = "222222",
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
            
        };
        var zamowienie2 = new Zamowienie
        {
            Id_Zamowienie = 2,
            Id_Osoba = 2,
            DataZamowienia = DateTime.UtcNow,
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test2",
            NazwiskoOdbiorcy = "test2",
            Ulica = "test2",
            NrDomu = "test2",
            KodPocztowy = "test2",
            Miejscowosc = "test2",
            TelefonOdbiorcy = "22222212212",
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
        };
        ctx.Zamowienie.Add(zamowienie1);
        ctx.Zamowienie.Add(zamowienie2);
        await ctx.SaveChangesAsync();
        
        var service = new OrderService(ctx);
        
        var wynik = await service.GetOrderDetails(1, 1, false,  CancellationToken.None);
        
        Assert.NotNull(wynik);
        Assert.Equal(1, wynik.Id_Zamowienie);


    }
    
    [Fact]
    public async Task GetOrderDetails_OsobaWyswietlaNIESwojeZamowienie_ZwróconyPowinienZostacNotFoundException()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);
        
        var user1 = new Klient{ Id_Osoba = 1,Imie = "test1", Nazwisko = "test1", Email = "test@email.pl", Haslo = "sadasdas", NrTelefonu = "231123123"};
        var user2 = new Klient{ Id_Osoba = 2,Imie = "test2", Nazwisko = "test2",Email = "test2@email.pl" , Haslo = "sadasdas", NrTelefonu = "12312312"};
        
        ctx.Osoba.Add(user1);
        ctx.Osoba.Add(user2);
        await ctx.SaveChangesAsync();

        var zamowienie1 = new Zamowienie { 
            Id_Zamowienie = 1,
            Id_Osoba = 1,
            DataZamowienia = DateTime.UtcNow,
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test1",
            NazwiskoOdbiorcy = "test1",
            Ulica = "test1",
            NrDomu = "test1",
            KodPocztowy = "test1",
            Miejscowosc = "test",
            TelefonOdbiorcy = "222222",
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
            
        };
        var zamowienie2 = new Zamowienie
        {
            Id_Zamowienie = 2,
            Id_Osoba = 2,
            DataZamowienia = DateTime.UtcNow,
            Status = StatusZamowienia.Nowe,
            ImieOdbiorcy = "test2",
            NazwiskoOdbiorcy = "test2",
            Ulica = "test2",
            NrDomu = "test2",
            KodPocztowy = "test2",
            Miejscowosc = "test2",
            TelefonOdbiorcy = "22222212212",
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
        };
        ctx.Zamowienie.Add(zamowienie1);
        ctx.Zamowienie.Add(zamowienie2);
        await ctx.SaveChangesAsync();
        
        var service = new OrderService(ctx);
        
        
        
       await Assert.ThrowsAsync<NotFoundException>(() => service.GetOrderDetails(2, 1, false,  CancellationToken.None));


    }

    [Fact]
    public async Task PlaceOrder_ZamowienieZamrazaCeneAktualna_ZwroconaZostajeCenaZakupu()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
            .Options;
        
        using var ctx = new ShopDbContext(options);
        
        var produkt = new Produkt 
        { 
            Id_Produkt = 1,
            Nazwa = "test",
            Cena = 100m,
            Znizka = 20,
            CzyAktywny = true,
            IloscNaStanie = 100,  
        };
        ctx.Produkt.Add(produkt);
        
        var user1 = new Klient{ Id_Osoba = 1,Imie = "test1", Nazwisko = "test1", Email = "test@email.pl", Haslo = "sadasdas", NrTelefonu = "231123123"};
        ctx.Osoba.Add(user1);
        await ctx.SaveChangesAsync();

        var order = new PlaceOrderDto
        {
            ImieOdbiorcy = "test2",
            NazwiskoOdbiorcy =  "test2",
            Ulica = "test",
            NrDomu = "12312312",
            KodPocztowy = "test2",
            Miejscowosc = "test2",
            TelefonOdbiorcy = "222222",
            Pozycje = new List<PlaceOrderItemsDto>
            {
                new PlaceOrderItemsDto{ Id_Produkt = 1, Ilosc = 1}
                
            }
        };
        var service = new OrderService(ctx);
        
        var result = await service.PlaceOrder(1,order, CancellationToken.None);

        var pozycja = ctx.Set<PozycjaWZamowieniu>().First();
        
        Assert.Equal(80m, pozycja.CenaZakupu);


    }
    
    
    
}