using FIAP.FCG.Transaction.Domain.Entity;
using FIAP.FCG.Transaction.Infrastructure.Logger;
using FIAP.FCG.Transaction.Service.Dto.Payment;
using FIAP.FCG.Transaction.Service.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace FIAP.FCG.Transaction.Service.Services;

public class PaymentService(IBaseLogger<TransactionService> logger, IOptions<PaymentLambdaOptions> options) : IPaymentService
{
    private readonly IBaseLogger<TransactionService> _logger = logger;
    private readonly PaymentLambdaOptions _options = options.Value;

    public async Task<PaymentOutputDto?> ProcessPaymentAsync(object payload)
    {
        _logger.LogInformation("Iniciando chamada AWS Lambda para processo da transação !");

        using var http = new HttpClient();
        var resp = await http.PostAsJsonAsync(_options.Url, payload);

        if (resp.StatusCode != HttpStatusCode.OK)
            _logger.LogError($"Serviço AWS Lambda para processo da transação retornou com falha: {resp.StatusCode} - {resp.Content}");
        else
            _logger.LogInformation("Serviço AWS Lambda para processo da transação retornado com sucesso");

        return await resp.Content.ReadFromJsonAsync<PaymentOutputDto>();

    }
}
