using FIAP.FCG.Transaction.Domain.Enums;

namespace FIAP.FCG.Transaction.Domain.Entity
{
    public class TransactionEntity : EntityBase
    {
        public long Code { get; set; }
        public long UserId { get; set; }
        public long GameId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public TransactionsStatusEnum Status { get; set; }
    }
}
