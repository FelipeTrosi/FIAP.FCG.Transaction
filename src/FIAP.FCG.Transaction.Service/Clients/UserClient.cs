using FIAP.FCG.Transaction.Infrastructure.Logger;
using FIAP.FCG.Transaction.Service.Dto.User;
using FIAP.FCG.Transaction.Service.Exceptions;
using FIAP.FCG.Transaction.Service.Interfaces.Clients;
using System.Net;
using System.Net.Http.Json;

namespace FIAP.FCG.Transaction.Service.Clients;

public class UserClient(HttpClient http, IBaseLogger<UserClient> logger) : IUserClient
{
    private readonly IBaseLogger<UserClient> _logger = logger;

    public async Task<UserResponseDto?> GetById(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Iniciando consulta do Usuário com id {id}");

        using var resp = await http.GetAsync($"/User/GetById/{id}", ct);

        if (!resp.IsSuccessStatusCode)
        {
            string msg = string.Empty;

            switch (resp.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    msg = $"Usuário com id {id} não encontrado !";
                    _logger.LogError(msg);
                    throw new NotFoundException(msg);

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.RequestTimeout:
                    msg = $"Houve uma falha na comunicação com o Servidor";
                    _logger.LogError(msg);
                    throw new BadRequestException(msg, new Dictionary<string, string[]> { });

                case HttpStatusCode.InternalServerError:
                    msg = $"Erro interno na API de User !";
                    _logger.LogError(msg);
                    throw new Exception(msg);
            }

            _logger.LogError($"Usuário com id {id} não encontrado");
        }

        return await resp.Content.ReadFromJsonAsync<UserResponseDto>(ct)!;
    }
}
