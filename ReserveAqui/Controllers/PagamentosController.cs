using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;
using System.Linq;

namespace ReserveAqui.Controllers
{
    [Route("api/pagamentos")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public PagamentosController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os pagamentos cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /api/pagamentos
        ///     [
        ///         {
        ///             "id": "guid",
        ///             "valor": 100.00,
        ///             "dataPagamento": "2024-12-06T04:31:00Z",
        ///             "reservaId": "guid",
        ///             "metodoPagamento": "Cartão de Crédito"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de pagamentos cadastrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var pagamentos = _context.Pagamentos.ToList();
            return Ok(pagamentos);
        }

        /// <summary>
        /// Retorna um pagamento específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do pagamento.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /api/pagamentos/{id}
        ///     {
        ///         "id": "guid",
        ///         "valor": 100.00,
        ///         "dataPagamento": "2024-12-06T04:31:00Z",
        ///         "reservaId": "guid",
        ///         "metodoPagamento": "Cartão de Crédito"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o pagamento solicitado.</response>
        /// <response code="404">Se o pagamento não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var pagamento = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamento == null)
                return NotFound();

            return Ok(pagamento);
        }

        /// <summary>
        /// Cria um novo pagamento.
        /// </summary>
        /// <param name="pagamento">Os dados do novo pagamento.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/pagamentos
        ///     {
        ///         "valor": 100.00,
        ///         "dataPagamento": "2024-12-06T04:31:00Z",
        ///         "reservaId": "guid",
        ///         "metodoPagamento": "Cartão de Crédito"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o pagamento criado.</response>
        /// <response code="400">Se os dados do pagamento forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Pagamento pagamento)
        {
            pagamento.Id = Guid.NewGuid();
            _context.Pagamentos.Add(pagamento);

            return CreatedAtAction(nameof(GetById), new { id = pagamento.Id }, pagamento);
        }

        /// <summary>
        /// Atualiza um pagamento existente.
        /// </summary>
        /// <param name="id">O ID do pagamento.</param>
        /// <param name="pagamento">Os novos dados do pagamento.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/pagamentos/{id}
        ///     {
        ///         "valor": 100.00,
        ///         "dataPagamento": "2024-12-06T04:31:00Z",
        ///         "reservaId": "guid",
        ///         "metodoPagamento": "Cartão de Crédito"
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID do pagamento não corresponder.</response>
        /// <response code="404">Se o pagamento não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, Pagamento pagamento)
        {
            var pagamentoExistente = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamentoExistente == null)
                return NotFound();

            pagamentoExistente.Valor = pagamento.Valor;
            pagamentoExistente.DataPagamento = pagamento.DataPagamento;
            pagamentoExistente.ReservaId = pagamento.ReservaId;
            pagamentoExistente.MetodoPagamento = pagamento.MetodoPagamento;


            return NoContent();
        }

        /// <summary>
        /// Exclui um pagamento existente.
        /// </summary>
        /// <param name="id">O ID do pagamento.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /api/pagamentos/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se o pagamento não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var pagamento = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamento == null)
                return NotFound();

            _context.Pagamentos.Remove(pagamento);

            return NoContent();
        }
    }
}