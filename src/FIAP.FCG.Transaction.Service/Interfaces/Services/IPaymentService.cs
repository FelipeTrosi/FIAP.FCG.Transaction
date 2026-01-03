using FIAP.FCG.Transaction.Service.Dto.Payment;

namespace FIAP.FCG.Transaction.Service.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentOutputDto?> ProcessPaymentAsync(object payload);
}
