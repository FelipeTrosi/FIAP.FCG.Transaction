using FIAP.FCG.Contracts.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FIAP.FCG.Transaction.Service.Consumers;

public class PaymentRequestedConsumer : IConsumer<PaymentRequested>
{
    private readonly ILogger<PaymentRequestedConsumer> _logger;
    private readonly IPublishEndpoint _publish;

    public PaymentRequestedConsumer(ILogger<PaymentRequestedConsumer> logger, IPublishEndpoint publish)
    {
        _logger = logger;
        _publish = publish;
    }

    public async Task Consume(ConsumeContext<PaymentRequested> context)
    {
        var msg = context.Message;

        _logger.LogInformation($"Processando pagamento Tx={msg.TransactionId} User={msg.UserId} Valor={msg.Amount} Email={msg.Email}");

        await Task.Delay(TimeSpan.FromSeconds(5));

        await _publish.Publish<PaymentSucceeded>(new
        {
            TransactionId = msg.TransactionId,
            UserId = msg.UserId,
            Amount = msg.Amount,
            ProcessedAt = DateTime.UtcNow,
            Email = msg.Email
        });

        _logger.LogInformation($"Pagamento aprovado Tx={msg.TransactionId}");
    }
}
