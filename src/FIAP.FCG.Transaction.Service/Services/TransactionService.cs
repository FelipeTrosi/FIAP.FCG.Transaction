using FIAP.FCG.Transaction.Infrastructure.Logger;
using FIAP.FCG.Transaction.Infrastructure.Repository.Interfaces;
using FIAP.FCG.Transaction.Service.Dto.Transaction;
using FIAP.FCG.Transaction.Service.Exceptions;
using FIAP.FCG.Transaction.Service.Interfaces.Clients;
using FIAP.FCG.Transaction.Service.Interfaces.Services;
using FIAP.FCG.Transaction.Service.Util;

namespace FIAP.FCG.Transaction.Service.Services;

public class TransactionService(IBaseLogger<TransactionService> logger, ITransactionRepository repository, IPaymentService paymentService,
    IUserClient userClient, IGameClient gameClient) : ITransactionService
{
    private readonly ITransactionRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IBaseLogger<TransactionService> _logger = logger;
    private readonly IUserClient _userClient = userClient;
    private readonly IGameClient _gameClient = gameClient;


    public async Task Create(TransactionCreateDto entity)
    {
        CancellationToken cancellation = CancellationToken.None;
        _logger.LogInformation("Iniciando serviço 'CREATE' de transação !");

        try
        {
            await _userClient.GetById(entity.UserId, cancellation);
            await _gameClient.GetById(entity.GameId, cancellation);

            var createdTransaction = _repository.Create(new()
            {
                CreatedAt = DateTime.Now,
                Code = entity.Code,
                UserId = entity.UserId,
                GameId = entity.GameId,
                Type = entity.Type,
                Status = entity.Status,
            });

            _logger.LogInformation("Transação cadastrado com sucesso !");

            var random = new Random();
            int amount = random.Next(57, 399);

            await _paymentService.ProcessPaymentAsync(new
            {
                transactionId = createdTransaction.Id,
                userId = entity.UserId,
                amount,
                method = "PIX"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro na criação do usuário");
            throw new Exception(ex.Message);
        }
    }


    public void DeleteById(long id)
    {
        _logger.LogInformation($"Iniciando serviço 'DELETE' por Id: {id} de transação !");

        var entity = _repository.GetById(id);

        if (entity == null)
        {
            _logger.LogWarning($"Registro não encontrado para o id: {id}");
            throw new NotFoundException($"Registro não encontrado para o id: {id}");
        }

        _repository.DeleteById(entity.Id);

        _logger.LogInformation($"Transação com id {id} removido com sucesso !");
    }

    public ICollection<TransactionOutputDto> GetAll()
    {
        _logger.LogInformation("Iniciando serviço 'GETALL' de transação !");

        return ParseModel.Map<ICollection<TransactionOutputDto>>(_repository.GetAll());
    }

    public TransactionOutputDto? GetById(long id)
    {
        _logger.LogInformation($"Iniciando serviço 'GET' por Id: {id} de transação !");

        var result = _repository.GetById(id);

        if (result != null)
            return ParseModel.Map<TransactionOutputDto>(result);
        else
        {
            _logger.LogWarning($"Transação com Id: {id} não encontrado !");
            throw new NotFoundException($"Registro não encontrado para o id: {id}");
        }
    }

    public void Update(TransactionUpdateDto entity)
    {
        _logger.LogInformation($"Iniciando serviço 'UPDATE' de transação com Id {entity.Id}!");

        _repository.Update(new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            Code = entity.Code,
            UserId = entity.UserId,
            GameId = entity.GameId,
            Type = entity.Type,
            Status = entity.Status,
        });

        _logger.LogInformation($"Transação com Id {entity.Id} atualizado com sucesso !");
    }
}
