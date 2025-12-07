namespace FIAP.FCG.Transaction.Domain.Entity
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
