namespace FIAP.FCG.Contracts.Messaging.Events;

public interface PaymentSucceeded
{
    long TransactionId { get; }
    long UserId { get; }
    string Email { get; }
    decimal Amount { get; }
    DateTime ProcessedAt { get; }
}
