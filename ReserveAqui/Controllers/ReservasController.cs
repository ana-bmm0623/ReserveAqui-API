using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;
using System;
using System.Linq;

namespace ReserveAqui.Controllers
{
    [ApiController]
    [Route("reserveAqui/reserva")]
    public class ReservasController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public ReservasController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as reservas cadastradas.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /reserveAqui/reserva
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "quantidadePessoas": 0,
        ///             "dataEntrada": "2024-12-06T04:44:06.733Z",
        ///             "dataSaida": "2024-12-06T04:44:06.733Z",
        ///             "checkInRealizado": true,
        ///             "checkOutRealizado": true,
        ///             "cancelada": true,
        ///             "reservaServicoAdicionals": [
        ///                 {
        ///                     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                     "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                     "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                 }
        ///             ]
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de reservas cadastradas.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var reservas = _context.Reservas
                .Select(r => new
                {
                    r.Id,
                    r.QuartoId,
                    r.HospedeId,
                    r.QuantidadePessoas,
                    r.DataEntrada,
                    r.DataSaida,
                    r.CheckInRealizado,
                    r.CheckOutRealizado,
                    r.Cancelada
                }).ToList();

            return Ok(reservas);
        }

        /// <summary>
        /// Retorna uma reserva específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da reserva.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /reserveAqui/reserva/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "quantidadePessoas": 0,
        ///         "dataEntrada": "2024-12-06T04:44:06.733Z",
        ///         "dataSaida": "2024-12-06T04:44:06.733Z",
        ///         "checkInRealizado": true,
        ///         "checkOutRealizado": true,
        ///         "cancelada": true,
        ///         "reservaServicoAdicionals": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a reserva solicitada.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            return Ok(reserva);
        }

        /// <summary>
        /// Cria uma nova reserva.
        /// </summary>
        /// <param name="reserva">Os dados da nova reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/reserva
        ///     {
        ///         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "quantidadePessoas": 0,
        ///         "dataEntrada": "2024-12-06T04:44:06.733Z",
        ///         "dataSaida": "2024-12-06T04:44:06.733Z"
        ///     }
        /// </remarks>
        /// <response code="201">Retorna a reserva criada.</response>
        /// <response code="400">Se os dados da reserva forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Reserva reserva)
        {
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == reserva.QuartoId);
            if (quarto == null || !quarto.Disponibilidade)
                return BadRequest("O quarto selecionado não está disponível.");

            quarto.Disponibilidade = false;
            reserva.CheckInRealizado = false;
            reserva.CheckOutRealizado = false;
            reserva.Cancelada = false;

            reserva.Id = Guid.NewGuid();
            _context.Reservas.Add(reserva);
            return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, reserva);
        }


        /// <summary>
        /// Atualiza uma reserva existente.
        /// </summary>
        /// <param name="id">O ID da reserva.</param>
        /// <param name="reservaAtualizada">Os novos dados da reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /reserveAqui/reserva/{id}
        ///     {
        ///         "quantidadePessoas": 0,
        ///         "dataEntrada": "2024-12-06T04:44:06.733Z",
        ///         "dataSaida": "2024-12-06T04:44:06.733Z"
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID da reserva não corresponder.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, Reserva reservaAtualizada)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            reserva.QuantidadePessoas = reservaAtualizada.QuantidadePessoas;
            reserva.DataEntrada = reservaAtualizada.DataEntrada;
            reserva.DataSaida = reservaAtualizada.DataSaida;

            return NoContent();
        }

        /// <summary>
        /// Cancela uma reserva existente.
        /// </summary>
        /// <param name="id">O ID da reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /reserveAqui/reserva/{id}
        /// </remarks>
        /// <response code="204">Cancelamento bem-sucedido.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Cancel(Guid id)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            reserva.Cancelada = true;
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == reserva.QuartoId);
            if (quarto != null)
                quarto.Disponibilidade = true;

            return NoContent();
        }

        /// <summary>
        /// Realiza o check-in de uma reserva.
        /// </summary>
        /// <param name="id">O ID da reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/reserva/{id}/check-in
        /// </remarks>
        /// <response code="204">Check-in bem-sucedido.</response>
        /// <response code="400">Se o check-in já foi realizado.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpPost("{id}/check-in")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CheckIn(Guid id)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            if (reserva.CheckInRealizado)
                return BadRequest("Check-in já foi realizado.");

            reserva.CheckInRealizado = true;
            return NoContent();
        }

        /// <summary>
        /// Realiza o check-out de uma reserva.
        /// </summary>
        /// <param name="id">O ID da reserva.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/reserva/{id}/check-out
        /// </remarks>
        /// <response code="204">Check-out bem-sucedido.</response>
        /// <response code="400">Se o check-in não foi realizado.</response>
        /// <response code="404">Se a reserva não for encontrada.</response>
        [HttpPost("{id}/check-out")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CheckOut(Guid id)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            if (!reserva.CheckInRealizado)
                return BadRequest("Check-in deve ser realizado antes do check-out.");

            reserva.CheckOutRealizado = true;
            return NoContent();
        }
    }
}