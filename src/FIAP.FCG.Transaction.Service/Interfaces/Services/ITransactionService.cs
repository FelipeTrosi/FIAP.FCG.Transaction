using FIAP.FCG.Transaction.Service.Dto.Transaction;

namespace FIAP.FCG.Transaction.Service.Interfaces.Services;
public interface ITransactionService
{
    ICollection<TransactionOutputDto> GetAll();
    TransactionOutputDto? GetById(long id);
    Task Create(TransactionCreateDto entity);
    void Update(TransactionUpdateDto entity);
    void DeleteById(long id);
}
