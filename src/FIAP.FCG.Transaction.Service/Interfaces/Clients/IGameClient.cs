using FIAP.FCG.Transaction.Service.Dto.Game;

namespace FIAP.FCG.Transaction.Service.Interfaces.Clients;

public interface IGameClient
{
    Task<GameResponseDto?> GetById(long id, CancellationToken ct);
}
