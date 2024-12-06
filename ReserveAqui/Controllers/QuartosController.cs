using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;

namespace ReserveAqui.Controllers
{
    [Route("reserveAqui/quartos")]
    [ApiController]
    public class QuartosController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public QuartosController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var quartos = _context.Quartos.ToList();
            return Ok(quartos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return Ok(quarto);
        }

        [HttpPost]
        public IActionResult Post(Quarto quarto)
        {
            // Gerar um novo Id como int
            if (quarto.CapacidadeMaxima <= 0)
            {
                return BadRequest("A capacidade máxima deve ser positiva.");
            }

            quarto.Id = Guid.NewGuid();
            _context.Quartos.Add(quarto);
            return CreatedAtAction(nameof(GetById), new { id = quarto.Id }, quarto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Quarto quarto)
        {
            var quartoExistente = _context.Quartos.SingleOrDefault(q => q.Id == id);
            if (quartoExistente == null)
            {
                return NotFound();
            }

            quartoExistente.NumeroIdentificacao = quarto.NumeroIdentificacao;
            quartoExistente.Preco = quarto.Preco;
            quartoExistente.CapacidadeMaxima = quarto.CapacidadeMaxima;
            quartoExistente.Disponibilidade = quarto.Disponibilidade;
            quartoExistente.HotelId = quarto.HotelId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            _context.Quartos.Remove(quarto);
            return NoContent();
        }

        [HttpGet("filterByCapacity/{capacity}")]
        public IActionResult FilterByCapacity(int capacity)
        {
            var filteredQuartos = _context.Quartos.Where(q => q.CapacidadeMaxima >= capacity && q.Disponibilidade).ToList();
            return Ok(filteredQuartos);
        }
    }
}