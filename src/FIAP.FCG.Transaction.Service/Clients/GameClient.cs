using FIAP.FCG.Transaction.Infrastructure.Logger;
using FIAP.FCG.Transaction.Service.Dto.Game;
using FIAP.FCG.Transaction.Service.Exceptions;
using FIAP.FCG.Transaction.Service.Interfaces.Clients;
using System.Net;
using System.Net.Http.Json;

namespace FIAP.FCG.Transaction.Service.Clients;

public class GameClient(HttpClient http, IBaseLogger<GameClient> logger) : IGameClient
{
    private readonly IBaseLogger<GameClient> _logger = logger;

    public async Task<GameResponseDto?> GetById(long id, CancellationToken ct)
    {
        _logger.LogInformation($"Iniciando consulta do Jogo com id {id}");

        using var resp = await http.GetAsync($"/Games/GetById/{id}", ct);

        if (!resp.IsSuccessStatusCode)
        {
            string msg = string.Empty;

            switch (resp.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    msg = $"Jogo com id {id} não encontrado !";
                    _logger.LogError(msg);
                    throw new NotFoundException(msg);

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.RequestTimeout:
                    msg = $"Houve uma falha na comunicação com o Servidor";
                    _logger.LogError(msg);
                    throw new BadRequestException(msg, new Dictionary<string, string[]> { });

                case HttpStatusCode.InternalServerError:
                    msg = $"Erro interno na API de Jogo !";
                    _logger.LogError(msg);
                    throw new Exception(msg);
            }

            _logger.LogError($"Usuário com id {id} não encontrado");
        }

        return await resp.Content.ReadFromJsonAsync<GameResponseDto>(ct)!;
    }
}
