namespace FIAP.FCG.Transaction.Infrastructure.CorrelationId;

public interface ICorrelationIdGenerator
{
    string Get();
    void Set(string correlationId);
}
