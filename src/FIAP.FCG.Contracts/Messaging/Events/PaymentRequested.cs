namespace FIAP.FCG.Contracts.Messaging.Events;

public interface PaymentRequested
{
    long TransactionId { get; }
    long UserId { get; }
    decimal Amount { get; }
    DateTime Timestamp { get; }
    string Email { get; }
}
