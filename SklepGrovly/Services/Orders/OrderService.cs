using Microsoft.EntityFrameworkCore;
using SklepGrovly.DTOs.Orders;
using SklepGrovly.Entities;
using SklepGrovly.Enums;
using SklepGrovly.Exceptions;

namespace SklepGrovly.Services.Orders;

public class OrderService(ShopDbContext ctx) : IOrderService
{
    
    
    public async Task<OrderConfirmationDto> PlaceOrder(int klientId, PlaceOrderDto dto, CancellationToken ct)
    {

        var noweZamowienie = new Zamowienie
        {
            Id_Klient = klientId,
            DataZamowienia = DateTime.UtcNow,
            Status = StatusZamowienia.Nowe,
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
        };

        var pozycjeDto = new List<OrderItemConfirmationDto>();
        
        foreach (var pozycja in dto.Pozycje)
        {
            
            var produkt = await ctx.Produkt
                .FirstOrDefaultAsync(p=> p.Id_Produkt == pozycja.Id_Produkt, ct);

            if (produkt == null)
            {
                throw new NotFoundException(" Nie ma takiego produktu!");
            }

            if (!produkt.CzyAktywny)
            {
                throw new ConflictException($"Produkt {produkt.Nazwa} jest nieaktywny.");
            }

            if (produkt.IloscNaStanie < pozycja.Ilosc)
            {
                throw new ConflictException($"Brak wystarczającej ilości produktu {produkt.Nazwa} na stanie.");
            }


            noweZamowienie.PozycjaWZamowieniu.Add(new PozycjaWZamowieniu
            {
                Ilosc = pozycja.Ilosc,
                CenaZakupu = produkt.Cena,
                Id_Produkt = produkt.Id_Produkt,
            });
            
            
            pozycjeDto.Add(new OrderItemConfirmationDto
            {
                Id_Produkt = produkt.Id_Produkt,
                NazwaProduktu = produkt.Nazwa,
                Ilosc = pozycja.Ilosc,
                CenaJednostkowa = produkt.Cena,
                CenaPozycji = produkt.Cena * pozycja.Ilosc,
            });
            
            produkt.IloscNaStanie -=  pozycja.Ilosc ;
            
        }
        ctx.Zamowienie.Add(noweZamowienie);
        await ctx.SaveChangesAsync(ct);
        
        return new OrderConfirmationDto
        {
            Id_Zamowienie = noweZamowienie.Id_Zamowienie,
            DataZamowienia = noweZamowienie.DataZamowienia,
            Status = noweZamowienie.Status,
            Pozycje = pozycjeDto,
            SumaCalkowita = pozycjeDto.Sum(p=> p.CenaPozycji)
        };
    }

    public async Task<List<OrderListItemDto>> GetAllOrders(CancellationToken ct)
    {
        return null;
    }

    public async Task<OrderDetailsDto> GetOrderDetails(int id, CancellationToken ct)
    {
        return null;
    }

    public async Task ChangeOrderStatus(int id, StatusZamowienia nowyStatus, CancellationToken ct)
    {
        
    }

    public async Task CancelOrder(int id, CancellationToken ct)
    {
        
    }


    
}