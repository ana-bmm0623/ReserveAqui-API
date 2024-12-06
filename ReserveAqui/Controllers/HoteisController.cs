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

        /// <summary>
        /// Retorna todos os hotéis cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna uma lista de hotéis cadastrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var hotels =  _context.Hoteis.ToList();
            return Ok(hotels);
        }

        /// <summary>
        /// Retorna um hotel específico pelo ID.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="id">O ID do hotel.</param>
        /// <response code="200">Retorna o hotel solicitado.</response>
        /// <response code="404">Se o hotel não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var hotel =  _context.Hoteis.SingleOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            return Ok(hotel);
        }

        /// <summary>
        /// Cria um novo hotel.
        /// </summary>
        /// <param name="hotel">Os dados do novo hotel.</param>
        /// <remarks>
        /// Retorno:
        /// 
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o hotel criado.</response>
        /// <response code="400">Se o nome do hotel não for fornecido.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create( Hotel hotel)
        {
            if (string.IsNullOrEmpty(hotel.Nome))
            {
                return BadRequest("O nome do hotel e a capacidade máxima devem ser fornecidos e válidos.");
            }

            hotel.Id = Guid.NewGuid();
            _context.Hoteis.Add(hotel);
            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
        }

        /// <summary>
        /// Atualiza um hotel existente.
        /// </summary>
        /// <param name="id">O ID do hotel.</param>
        /// <param name="hotel">Os novos dados do hotel.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/hotels/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID do hotel não corresponder.</response>
        /// <response code="404">Se o hotel não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id,  Hotel hotel)
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

        /// <summary>
        /// Exclui um hotel existente.
        /// </summary>
        /// <param name="id">O ID do hotel.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /api/hotels/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se o hotel não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var hotel = _context.Hoteis.SingleOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel não encontrado.");
            }

            _context.Hoteis.Remove(hotel);

            return NoContent();
        }

        /// <summary>
        /// Adiciona um quarto a um hotel existente.
        /// </summary>
        /// <param name="hotelId">O ID do hotel.</param>
        /// <param name="room">Os dados do novo quarto.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/hotels/{hotelId}/add-room
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o quarto criado.</response>
        /// <response code="404">Se o hotel não for encontrado.</response>
        [HttpPost("{hotelId}/add-room")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddRoom(Guid hotelId, Quarto room)
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

        /// <summary>
        /// Atualiza um quarto de um hotel existente.
        /// </summary>
        /// <param name="hotelId">O ID do hotel.</param>
        /// <param name="roomId">O ID do quarto.</param>
        /// <param name="room">Os novos dados do quarto.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/hotels/{hotelId}/update-room/{roomId}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID do hotel não corresponder ao quarto.</response>
        /// <response code="404">Se o quarto não for encontrado.</response>
        [HttpPut("{hotelId}/update-room/{roomId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRoom(Guid hotelId, Guid roomId, [FromBody] Quarto room)
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

        /// <summary>
        /// Filtra os quartos por capacidade.
        /// </summary>
        /// <param name="capacity">A capacidade mínima dos quartos.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /api/hotels/filterByCapacity/{capacity}
        ///     [
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "string",
        ///         "endereco": "string",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "descricao": "string",
        ///         "quartos": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "numeroIdentificacao": "string",
        ///                 "capacidadeMaxima": 0,
        ///                 "disponibilidade": true,
        ///                 "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "preco": 0,
        ///                 "reservas": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "quantidadePessoas": 0,
        ///                         "dataEntrada": "2024-12-06T04:18:23.001Z",
        ///                         "dataSaida": "2024-12-06T04:18:23.001Z",
        ///                         "checkInRealizado": true,
        ///                         "checkOutRealizado": true,
        ///                         "cancelada": true,
        ///                         "reservaServicoAdicionals": [
        ///                             {
        ///                                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                                 "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                             }
        ///                         ]
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna a lista de quartos filtrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet("filterByCapacity/{capacity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FilterByCapacity(int capacity)
        {
            var filteredQuartos =  _context.Quartos
                .Where(q => q.CapacidadeMaxima >= capacity && q.Disponibilidade)
                .ToList();

            return Ok(filteredQuartos);
        }
    }
}
