using FIAP.FCG.Transaction.Service.Dto.Transaction;

namespace FIAP.FCG.Transaction.Service.Interfaces;
public interface ITransactionService
{
    ICollection<TransactionOutputDto> GetAll();
    TransactionOutputDto? GetById(long id);
    void Create(TransactionCreateDto entity);
    void Update(TransactionUpdateDto entity);
    void DeleteById(long id);
}
