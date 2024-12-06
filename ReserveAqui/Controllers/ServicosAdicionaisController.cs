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

        [HttpGet]
        public IActionResult GetAll()
        {
            var servicos = _context.ServicosAdicionais.ToList();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var servico = _context.ServicosAdicionais.SingleOrDefault(s => s.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        [HttpPost]
        public IActionResult Post(ServicoAdicional servico)
        {
            if (servico.Valor <= 0)
            {
                return BadRequest("O valor do serviço deve ser positivo.");
            }

            _context.ServicosAdicionais.Add(servico);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ServicoAdicional servico)
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var servico = _context.ServicosAdicionais.SingleOrDefault(s => s.Id == id);
            if (servico == null)
            {
                return NotFound();
            }

            _context.ServicosAdicionais.Remove(servico);
            return NoContent();
        }

        [HttpPost("{servicoId}/add-to-reserva/{reservaId}")]
        public IActionResult AddToReserva(int servicoId, int reservaId)
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