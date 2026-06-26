using SklepGrovly.DTOs.Payments;

namespace SklepGrovly.Services.Payments;

public interface IPaymentService
{
    Task<PaymentInitResultDto> InitiatePayment(int orderId, InitiatePaymentDto dto, CancellationToken ct);

    Task<PaymentStatusDto> GetPaymentStatus(int id, CancellationToken ct);
    
    Task HandlePaymentNotification(/* surowe dane z HotPay */ CancellationToken ct);

}