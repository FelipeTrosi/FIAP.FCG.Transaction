using FIAP.FCG.Transaction.Domain.Entity;
using FIAP.FCG.Transaction.Infrastructure.Repository.Interfaces;

namespace FIAP.FCG.Transaction.Infrastructure.Repository
{
    public class TransactionRepository : EFRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
