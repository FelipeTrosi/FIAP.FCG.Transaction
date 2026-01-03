using FIAP.FCG.Transaction.Service.Dto.Transaction;
using FIAP.FCG.Transaction.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FCG.Transaction.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService service) : ControllerBase
    {
        private readonly ITransactionService _service = service;

        /// <summary>
        /// Cria um novo transação.
        /// </summary>
        /// <param name="input">Dados do transação a ser criado.</param>
        /// <response code="200">Transação criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post([FromBody] TransactionCreateDto input)
        {
            _service.Create(input);
            return Ok();
        }

        /// <summary>
        /// Atualiza um transação existente.
        /// </summary>
        /// <param name="input">Dados do transação para atualização.</param>
        /// <response code="200">Transação atualizado com sucesso.</response>
        /// <response code="404">Transação não encontrado.</response>
        [HttpPut]
        public IActionResult Put([FromBody] TransactionUpdateDto input)
        {
            _service.Update(input);
            return Ok();
        }

        /// <summary>
        /// Remove um transação pelo ID.
        /// </summary>
        /// <param name="id">ID do transação.</param>
        /// <response code="200">Transação removido com sucesso.</response>
        /// <response code="404">Transação não encontrado.</response>
        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            _service.DeleteById(id);
            return Ok();
        }

        /// <summary>
        /// Obtém um transação pelo ID.
        /// </summary>
        /// <param name="id">ID do transação.</param>
        /// <response code="200">Transação encontrado.</response>
        /// <response code="404">Transação não encontrado.</response>
        [HttpGet("GetById/{id:long}")]
        public IActionResult GetById(long id)
        {
            return Ok(_service.GetById(id));
        }

        /// <summary>
        /// Lista todos os transaçãos.
        /// </summary>
        /// <response code="200">Lista de transaçãos retornada com sucesso.</response>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

    }
}
