using FIAP.FCG.Transaction.Service.Dto.User;

namespace FIAP.FCG.Transaction.Service.Interfaces.Clients;

public interface IUserClient
{
    Task<UserResponseDto?> GetById(long id, CancellationToken ct);
}
