using FIAP.FCG.Transaction.Domain.Enums;

namespace FIAP.FCG.Transaction.Service.Dto.Transaction;

/// <summary>
/// DTO utilizado para cadastrar uma nova transação no sistema.
/// </summary>
public class TransactionCreateDto
{
    /// <summary>
    /// Código interno.
    /// </summary>
    public long Code { get; set; }

    /// <summary>
    /// Id do usuário.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Id do jogo.
    /// </summary>
    public long GameId { get; set; }

    /// <summary>
    /// Tipo de transação.
    /// </summary>
    public TransactionTypeEnum Type { get; set; }

    /// <summary>
    /// Status da transação.
    /// </summary>
    public TransactionsStatusEnum Status { get; set; }
}
