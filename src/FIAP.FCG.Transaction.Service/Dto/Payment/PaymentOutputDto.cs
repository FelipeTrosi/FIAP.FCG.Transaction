namespace FIAP.FCG.Transaction.Service.Dto.Payment;
public record PaymentOutputDto(string TransactionId, string Status, string ProviderRef, DateTime ProcessedAt);

