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
            ImieOdbiorcy = dto.ImieOdbiorcy,
            NazwiskoOdbiorcy = dto.NazwiskoOdbiorcy,
            Ulica = dto.Ulica,
            NrDomu = dto.NrDomu,
            KodPocztowy = dto.KodPocztowy,
            Miejscowosc = dto.Miejscowosc,
            TelefonOdbiorcy = dto.TelefonOdbiorcy,
            PozycjaWZamowieniu = new List<PozycjaWZamowieniu>()
        };

        var pozycjeDto = new List<OrderItemConfirmationDto>();

        foreach (var pozycja in dto.Pozycje)
        {

            var produkt = await ctx.Produkt
                .FirstOrDefaultAsync(p => p.Id_Produkt == pozycja.Id_Produkt, ct);

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

            produkt.IloscNaStanie -= pozycja.Ilosc;

        }

        ctx.Zamowienie.Add(noweZamowienie);
        await ctx.SaveChangesAsync(ct);

        return new OrderConfirmationDto
        {
            Id_Zamowienie = noweZamowienie.Id_Zamowienie,
            DataZamowienia = noweZamowienie.DataZamowienia,
            Status = noweZamowienie.Status,
            Pozycje = pozycjeDto,
            SumaCalkowita = pozycjeDto.Sum(p => p.CenaPozycji)
        };
    }

    public async Task<List<OrderListItemDto>> GetAllOrdersByClient(int klientId, CancellationToken ct)
    {
        var lista = await ctx.Zamowienie
            .Where(p => p.Id_Klient == klientId)
            .Select(p => new OrderListItemDto
            {
                Id_Zamowienie = p.Id_Zamowienie,
                DataZamowienia = p.DataZamowienia,
                Status = p.Status,
                SumaCalkowita = p.PozycjaWZamowieniu.Sum(p=> p.Ilosc * p.CenaZakupu)
            }).ToListAsync(ct);
        
        return lista;
    }

    public async Task<List<OrderListItemDto>> GetAllOrders(CancellationToken ct)
    {
        
        var lista = await ctx.Zamowienie
            .Select(z => new OrderListItemDto
            {
                Id_Zamowienie = z.Id_Zamowienie,
                DataZamowienia = z.DataZamowienia,
                Status = z.Status,
                SumaCalkowita = z.PozycjaWZamowieniu.Sum(p=>p.CenaZakupu * p.Ilosc)
            }).ToListAsync(ct);
        return lista;
    }




    public async Task<OrderDetailsDto> GetOrderDetails(int id, int klientId, bool isAdmin, CancellationToken ct)
            {
                var zamowienie = await ctx.Zamowienie
                    .Where(p => p.Id_Zamowienie == id)
                    .Select(p=> new OrderDetailsDto
                    {
                        
                        Id_Klient = p.Id_Klient,
                        Id_Zamowienie = p.Id_Zamowienie,
                        DataZamowienia = p.DataZamowienia,
                        Status = p.Status,
                        Pozycje = p.PozycjaWZamowieniu.Select(poz=>new OrderItemDto
                        {
                            Id_Produkt = poz.Id_Produkt,
                            NazwaProduktu = poz.Produkt.Nazwa,
                            Ilosc = poz.Ilosc,
                            CenaJednostkowa = poz.CenaZakupu,
                            CenaPozycji = poz.CenaZakupu * poz.Ilosc
                        }).ToList(),
                        SumaCalkowita = p.PozycjaWZamowieniu.Sum(z=> z.Ilosc* z.CenaZakupu)
                        
                        
                    })
                    .FirstOrDefaultAsync(ct);

                if (zamowienie  == null)
                {
                    throw new NotFoundException("Nie ma takiego zamowienia.");
                }

                if (!isAdmin && zamowienie.Id_Klient != klientId)
                {
                    throw new NotFoundException("Nie ma takiego zamowienia.");
                }

                return zamowienie;
            }

        public async Task ChangeOrderStatus(int id, StatusZamowienia nowyStatus, CancellationToken ct)
        {
            var zamowienie = await ctx.Zamowienie
                .FirstOrDefaultAsync(p => p.Id_Zamowienie == id, ct);
            if (zamowienie == null)
            {
                throw new NotFoundException("Nie ma takiego zamowienia.");
            }

            zamowienie.Status = nowyStatus;
            await ctx.SaveChangesAsync(ct);
        }
        

        public async Task CancelOrder(int id, int klientId, bool isAdmin, CancellationToken ct)
        {
            
            var zamowienie = await ctx.Zamowienie
                .Include(p => p.PozycjaWZamowieniu)
                .FirstOrDefaultAsync(p => p.Id_Zamowienie == id, ct);
            
            if (zamowienie == null)
                throw new NotFoundException("Nie ma takiego zamowienia.");
            
            if (!isAdmin && zamowienie.Id_Klient != klientId)
                throw new NotFoundException("Nie ma takiego zamówienia.");
            
            if (zamowienie.Status == StatusZamowienia.Anulowane)
                throw new ConflictException("Zamówienie jest już anulowane.");
            
            if (zamowienie.Status == StatusZamowienia.Wyslane || 
                zamowienie.Status == StatusZamowienia.Dostarczone)
                throw new ConflictException("Nie można anulować wysłanego zamówienia.");

            foreach (var poz in zamowienie.PozycjaWZamowieniu)
            {
                var produkt = await ctx.Produkt
                    .FirstOrDefaultAsync(p => p.Id_Produkt == poz.Id_Produkt, ct);
                if (produkt != null)
                {
                    produkt.IloscNaStanie += poz.Ilosc;
                }
                
            }
            

            zamowienie.Status = StatusZamowienia.Anulowane;
            await ctx.SaveChangesAsync(ct);

        }


    
}