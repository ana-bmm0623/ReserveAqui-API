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

        [HttpGet]
        public IActionResult GetAll()
        {
            var reservasServicosAdicionais = _context.ReservaServicosAdicionais.ToList();
            return Ok(reservasServicosAdicionais);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservaServicoAdicional = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicional == null)
                return NotFound();

            return Ok(reservaServicoAdicional);
        }

        [HttpPost]
        public IActionResult Post(ReservaServicoAdicional reservaServicoAdicional)
        {
            _context.ReservaServicosAdicionais.Add(reservaServicoAdicional);

            return CreatedAtAction(nameof(GetById), new { id = reservaServicoAdicional.Id }, reservaServicoAdicional);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ReservaServicoAdicional reservaServicoAdicional)
        {
            var reservaServicoAdicionalExistente = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicionalExistente == null)
                return NotFound();

            reservaServicoAdicionalExistente.ReservaId = reservaServicoAdicional.ReservaId;
            reservaServicoAdicionalExistente.ServicoAdicionalId = reservaServicoAdicional.ServicoAdicionalId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservaServicoAdicional = _context.ReservaServicosAdicionais.SingleOrDefault(r => r.Id == id);
            if (reservaServicoAdicional == null)
                return NotFound();

            _context.ReservaServicosAdicionais.Remove(reservaServicoAdicional);

            return NoContent();
        }
    }
}