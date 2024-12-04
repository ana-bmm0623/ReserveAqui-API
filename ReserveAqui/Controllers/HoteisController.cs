using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;


namespace ReserveAqui.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public HotelsController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        // GET: api/hotels
        [HttpGet]
        public IActionResult GetAll()
        {
            var hotels =  _context.Hoteis.ToList();
            return Ok(hotels);
        }

        // GET: api/hotels/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel =  _context.Hoteis.SingleOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            return Ok(hotel);
        }

        // POST: api/hotels
        [HttpPost]
        public IActionResult Create( Hotel hotel)
        {
            if (string.IsNullOrEmpty(hotel.Nome))
            {
                return BadRequest("O nome do hotel e a capacidade máxima devem ser fornecidos e válidos.");
            }

            _context.Hoteis.Add(hotel);
            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
        }

        // PUT: api/hotels/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id,  Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest("ID do hotel não corresponde.");
            }

            var existingHotel =  _context.Hoteis.SingleOrDefault(h => h.Id == id);
            if (existingHotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            existingHotel.Nome = hotel.Nome;
            existingHotel.Endereco = hotel.Endereco;
            existingHotel.Email = hotel.Email;
            existingHotel.Telefone = hotel.Telefone;
            existingHotel.Descricao = hotel.Descricao;

            return NoContent();
        }

        // DELETE: api/hotels/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hoteis.SingleOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            _context.Hoteis.Remove(hotel);

            return NoContent();
        }

        // POST: api/hotels/{hotelId}/add-room
        [HttpPost("{hotelId}/add-room")]
        public IActionResult AddRoom(int hotelId, Quarto room)
        {
            var hotel = _context.Hoteis.SingleOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            room.HotelId = hotelId;
            _context.Quartos.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        // PUT: api/hotels/{hotelId}/update-room/{roomId}
        [HttpPut("{hotelId}/update-room/{roomId}")]
        public IActionResult UpdateRoom(int hotelId, int roomId, [FromBody] Quarto room)
        {
            if (hotelId != room.HotelId)
            {
                return BadRequest("Hotel ID não corresponde ao quarto.");
            }

            var existingRoom = _context.Quartos.SingleOrDefault(r => r.Id == roomId);
            if (existingRoom == null)
            {
                return NotFound("Quarto não encontrado.");
            }

            existingRoom.NumeroIdentificacao = room.NumeroIdentificacao;
            existingRoom.CapacidadeMaxima = room.CapacidadeMaxima;
            existingRoom.Preco = room.Preco;
            existingRoom.Disponibilidade = room.Disponibilidade;



            return NoContent();
        }

        // GET: api/hotels/filterByCapacity/{capacity}
        [HttpGet("filterByCapacity/{capacity}")]
        public IActionResult FilterByCapacity(int capacity)
        {
            var filteredQuartos = await _context.Quartos
                .Where(q => q.CapacidadeMaxima >= capacity && q.Disponibilidade)
                .ToListAsync();

            return Ok(filteredQuartos);
        }
    }
}
