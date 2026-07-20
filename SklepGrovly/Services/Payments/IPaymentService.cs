using SklepGrovly.DTOs.Payments;

namespace SklepGrovly.Services.Payments;

public interface IPaymentService
{
    Task<PaymentInitResultDto> InitiatePayment(int orderId, int klientId, CancellationToken ct);

    Task HandleWebhook(string idBramki,bool sukces, CancellationToken ct);
    
    Task<MockPaymentInfoDto> GetMockPaymentInfo(string idBramki, CancellationToken ct);

}