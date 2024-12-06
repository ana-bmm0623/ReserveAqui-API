using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;
using System.Linq;

namespace ReserveAqui.Controllers
{
    [Route("api/reservas-servicos-adicionais")]
    [ApiController]
    public class ReservaServicoAdicionalController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public ReservaServicoAdicionalController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as reservas de serviços adicionais cadastradas.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /api/reservas-servicos-adicionais
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de reservas de serviços adicionais cadastradas.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var reservasServicosAdicionais = _context.ReservaServicosAdicionais.ToList();
            return Ok(reservasServicosAdicionais);
        }

        /// <summary>
        /// Retorna uma reserva de serviço adicional específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da reserva de serviço adicional.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /api/reservas-servicos-adicionais/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a reserva de serviço adicional solicitada.</response>
        /// <response code="404">Se a reserva de serviço adicional não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var reservaServicoAdicional = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicional == null)
                return NotFound();

            return Ok(reservaServicoAdicional);
        }

        /// <summary>
        /// Cria uma nova reserva de serviço adicional.
        /// </summary>
        /// <param name="reservaServicoAdicional">Os dados da nova reserva de serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/reservas-servicos-adicionais
        ///     {
        ///         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a reserva de serviço adicional criada.</response>
        /// <response code="400">Se os dados da reserva de serviço adicional forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ReservaServicoAdicional reservaServicoAdicional)
        {
            reservaServicoAdicional.Id = Guid.NewGuid();
            _context.ReservaServicosAdicionais.Add(reservaServicoAdicional);

            return CreatedAtAction(nameof(GetById), new { id = reservaServicoAdicional.Id }, reservaServicoAdicional);
        }

        /// <summary>
        /// Atualiza uma reserva de serviço adicional existente.
        /// </summary>
        /// <param name="id">O ID da reserva de serviço adicional.</param>
        /// <param name="reservaServicoAdicional">Os novos dados da reserva de serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/reservas-servicos-adicionais/{id}
        ///     {
        ///         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID da reserva de serviço adicional não corresponder.</response>
        /// <response code="404">Se a reserva de serviço adicional não for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, ReservaServicoAdicional reservaServicoAdicional)
        {
            var reservaServicoAdicionalExistente = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicionalExistente == null)
                return NotFound();

            reservaServicoAdicionalExistente.ReservaId = reservaServicoAdicional.ReservaId;
            reservaServicoAdicionalExistente.ServicoAdicionalId = reservaServicoAdicional.ServicoAdicionalId;

            return NoContent();
        }

        /// <summary>
        /// Exclui uma reserva de serviço adicional existente.
        /// </summary>
        /// <param name="id">O ID da reserva de serviço adicional.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /api/reservas-servicos-adicionais/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se a reserva de serviço adicional não for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var reservaServicoAdicional = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicional == null)
                return NotFound();

            _context.ReservaServicosAdicionais.Remove(reservaServicoAdicional);

            return NoContent();
        }
    }
}