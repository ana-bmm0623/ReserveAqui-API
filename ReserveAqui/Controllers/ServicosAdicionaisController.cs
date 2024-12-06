using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;
using System.Linq;

namespace ReserveAqui.Controllers
{
    [Route("reserveAqui/servicos-adicionais")]
    [ApiController]
    public class ServicosAdicionaisController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public ServicosAdicionaisController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os serviços adicionais cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /reserveAqui/servicos-adicionais
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "nome": "string",
        ///             "descricao": "string",
        ///             "valor": 0
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de serviços adicionais cadastrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var servicos = _context.ServicosAdicionais.ToList();
            return Ok(servicos);
        }

        /// <summary>
        /// Retorna um serviço adicional específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do serviço adicional.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /reserveAqui/servicos-adicionais/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "descricao": "string",
        ///         "valor": 0
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o serviço adicional solicitado.</response>
        /// <response code="404">Se o serviço adicional não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var servico = _context.ServicosAdicionais.SingleOrDefault(s => s.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        /// <summary>
        /// Cria um novo serviço adicional.
        /// </summary>
        /// <param name="servico">Os dados do novo serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/servicos-adicionais
        ///     {
        ///         "nome": "string",
        ///         "descricao": "string",
        ///         "valor": 0
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o serviço adicional criado.</response>
        /// <response code="400">Se os dados do serviço adicional forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ServicoAdicional servico)
        {
            if (servico.Valor <= 0)
            {
                return BadRequest("O valor do serviço deve ser positivo.");
            }

            servico.Id = Guid.NewGuid();
            _context.ServicosAdicionais.Add(servico);
            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        /// <summary>
        /// Atualiza um serviço adicional existente.
        /// </summary>
        /// <param name="id">O ID do serviço adicional.</param>
        /// <param name="servico">Os novos dados do serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /reserveAqui/servicos-adicionais/{id}
        ///     {
        ///         "nome": "string",
        ///         "descricao": "string",
        ///         "valor": 0
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID do serviço adicional não corresponder.</response>
        /// <response code="404">Se o serviço adicional não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, ServicoAdicional servico)
        {
            var servicoExistente = _context.ServicosAdicionais.SingleOrDefault(s => s.Id == id);
            if (servicoExistente == null)
            {
                return NotFound();
            }

            servicoExistente.Nome = servico.Nome;
            servicoExistente.Descricao = servico.Descricao;
            servicoExistente.Valor = servico.Valor;

            return NoContent();
        }

        /// <summary>
        /// Exclui um serviço adicional existente.
        /// </summary>
        /// <param name="id">O ID do serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /reserveAqui/servicos-adicionais/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se o serviço adicional não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var servico = _context.ServicosAdicionais.SingleOrDefault(s => s.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            _context.ServicosAdicionais.Remove(servico);
            return NoContent();
        }

        /// <summary>
        /// Adiciona um serviço adicional a uma reserva.
        /// </summary>
        /// <param name="servicoId">O ID do serviço adicional.</param>
        /// <param name="reservaId">O ID da reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/servicos-adicionais/{servicoId}/add-to-reserva/{reservaId}
        /// </remarks>
        /// <response code="200">Serviço adicional adicionado com sucesso.</response>
        /// <response code="400">Se o serviço já foi adicionado a esta reserva ou se houver algum erro.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpPost("{servicoId}/add-to-reserva/{reservaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddToReserva(Guid servicoId, Guid reservaId)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == reservaId);
            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            var servicoJaAdicionado = _context.ReservaServicosAdicionais
                .Any(r => r.ReservaId == reservaId && r.ServicoAdicionalId == servicoId);

            if (servicoJaAdicionado)
            {
                return BadRequest("Este serviço já foi adicionado a esta reserva.");
            }

            var reservaServico = new ReservaServicoAdicional
            {
                ReservaId = reservaId,
                ServicoAdicionalId = servicoId
            };

            _context.ReservaServicosAdicionais.Add(reservaServico);
            return Ok("Serviço adicional adicionado com sucesso.");
        }
    }
}