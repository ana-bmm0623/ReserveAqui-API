using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;

namespace ReserveAqui.Controllers
{
    [Route("api/hospedes")]
    [ApiController]
    public class HospedesController: ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public HospedesController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        // GET: api/hospedes
        [HttpGet]
        public IActionResult GetAll()
        {
            var hospedes = _context.Hospedes.ToList();
            return Ok(hospedes);
        }

      
        // GET: api/hospedes/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var hospede = _context.Hospedes.SingleOrDefault(h => h.Id == id);
            if (hospede == null)
            {
                return NotFound();
            }
            return Ok(hospede);
        }

        // POST: api/hospedes
        [HttpPost]
        public IActionResult Post(Hospede hospede)
        {
            if (_context.Hospedes.Any(h => h.CPF == hospede.CPF))
            {
                return BadRequest("Hóspede com este CPF já existe.");
            }

            if (_context.Hospedes.Any(h => h.Email == hospede.Email))
            {
                return BadRequest("Hóspede com este Email já existe.");
            }

            hospede.Id = Guid.NewGuid();
            _context.Hospedes.Add(hospede);
            return CreatedAtAction(nameof(GetById), new { id = hospede.Id }, hospede);
        }

        // PUT: api/hospedes/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Hospede hospede)
        {
            if (id != hospede.Id)
            {
                return BadRequest("ID do hóspede não corresponde.");
            }

            var existingHospede = _context.Hospedes.SingleOrDefault(h => h.Id == id);
            if (existingHospede == null)
            {
                return NotFound("Hóspede não encontrado.");
            }

            // Verificações de duplicidade para CPF e Email
            if (_context.Hospedes.Any(h => h.CPF == hospede.CPF && h.Id != id))
            {
                return BadRequest("Outro hóspede já está registrado com este CPF.");
            }

            if (_context.Hospedes.Any(h => h.Email == hospede.Email && h.Id != id))
            {
                return BadRequest("Outro hóspede já está registrado com este Email.");
            }

            existingHospede.NomeCompleto = hospede.NomeCompleto;
            existingHospede.CPF = hospede.CPF;
            existingHospede.RG = hospede.RG;
            existingHospede.Email = hospede.Email;
            existingHospede.Telefone = hospede.Telefone;
            existingHospede.Endereco = hospede.Endereco;

            return NoContent();
        }

        // DELETE: api/hospedes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var hospede = _context.Hospedes.SingleOrDefault(h => h.Id == id);
            if (hospede == null)
            {
                return NotFound("Hóspede não encontrado.");
            }

            _context.Hospedes.Remove(hospede);

            return NoContent();
        }
    }
}
