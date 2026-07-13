using SklepGrovly.DTOs.Orders;
using SklepGrovly.Enums;

namespace SklepGrovly.Services.Orders;

public interface IOrderService
{

    Task<OrderConfirmationDto> PlaceOrder(int klientId, PlaceOrderDto dto, CancellationToken ct);
    
    Task<OrderConfirmationDto> PlaceGuestOrder(GuestOrderDto dto, CancellationToken ct);
    
    Task<List<OrderListItemDto>> GetAllOrdersByClient(int klientId, CancellationToken ct);
    
    Task<List<OrderListItemDto>> GetAllOrders(CancellationToken ct);

    

    Task<OrderDetailsDto> GetOrderDetails(int id, int klientId, bool isAdmin, CancellationToken ct);
    
    Task ChangeOrderStatus(int id, StatusZamowienia nowyStatus, CancellationToken ct);

    Task CancelOrder(int id,int klientId, bool isAdmin, CancellationToken ct);

    
    
}