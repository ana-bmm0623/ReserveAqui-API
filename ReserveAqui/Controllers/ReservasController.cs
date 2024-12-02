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

        [HttpGet]
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

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            return Ok(reserva);
        }

        [HttpPost]
        public IActionResult Create(Reserva reserva)
        {
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == reserva.QuartoId);
            if (quarto == null || !quarto.Disponibilidade)
                return BadRequest("O quarto selecionado não está disponível.");

            quarto.Disponibilidade = false;
            reserva.CheckInRealizado = false;
            reserva.CheckOutRealizado = false;
            reserva.Cancelada = false;

            _context.Reservas.Add(reserva);
            return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id,Reserva reservaAtualizada)
        {
            var reserva = _context.Reservas.SingleOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            reserva.QuantidadePessoas = reservaAtualizada.QuantidadePessoas;
            reserva.DataEntrada = reservaAtualizada.DataEntrada;
            reserva.DataSaida = reservaAtualizada.DataSaida;

            return NoContent();
        }

        [HttpDelete("{id}")]
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

        [HttpPost("{id}/check-in")]
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

        [HttpPost("{id}/check-out")]
        public IActionResult CheckOut(Guid id)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            if (!reserva.CheckInRealizado)
                return BadRequest("Check-in deve ser realizado antes do check-out.");

            reserva.CheckOutRealizado = true;
            return NoContent();
        }
    }
}
