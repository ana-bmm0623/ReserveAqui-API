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

        /// <summary>
        /// Retorna todos os quartos cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /reserveAqui/quartos
        /// {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroIdentificacao": "string",
        ///         "capacidadeMaxima": 0,
        ///         "disponibilidade": true,
        ///         "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "preco": 0,
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T04:38:37.078Z",
        ///                 "dataSaida": "2024-12-06T04:38:37.078Z",
        ///                 "checkInRealizado": true,
        ///                 "checkOutRealizado": true,
        ///                 "cancelada": true,
        ///                 "reservaServicoAdicionals": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna uma lista de quartos cadastrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var quartos = _context.Quartos.ToList();
            return Ok(quartos);
        }

        /// <summary>
        /// Retorna um quarto específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do quarto.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /reserveAqui/quartos/{id}
        ///      {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroIdentificacao": "string",
        ///         "capacidadeMaxima": 0,
        ///         "disponibilidade": true,
        ///         "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "preco": 0,
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T04:38:37.078Z",
        ///                 "dataSaida": "2024-12-06T04:38:37.078Z",
        ///                 "checkInRealizado": true,
        ///                 "checkOutRealizado": true,
        ///                 "cancelada": true,
        ///                 "reservaServicoAdicionals": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o quarto solicitado.</response>
        /// <response code="404">Se o quarto não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var quarto = _context.Quartos.SingleOrDefault(q => q.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return Ok(quarto);
        }

        /// <summary>
        /// Cria um novo quarto.
        /// </summary>
        /// <param name="quarto">Os dados do novo quarto.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /reserveAqui/quartos
        /// {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroIdentificacao": "string",
        ///         "capacidadeMaxima": 0,
        ///         "disponibilidade": true,
        ///         "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "preco": 0,
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T04:38:37.078Z",
        ///                 "dataSaida": "2024-12-06T04:38:37.078Z",
        ///                 "checkInRealizado": true,
        ///                 "checkOutRealizado": true,
        ///                 "cancelada": true,
        ///                 "reservaServicoAdicionals": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="201">Retorna o quarto criado.</response>
        /// <response code="400">Se os dados do quarto forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Atualiza um quarto existente.
        /// </summary>
        /// <param name="id">O ID do quarto.</param>
        /// <param name="quarto">Os novos dados do quarto.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /reserveAqui/quartos/{id}
        /// {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroIdentificacao": "string",
        ///         "capacidadeMaxima": 0,
        ///         "disponibilidade": true,
        ///         "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "preco": 0,
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T04:38:37.078Z",
        ///                 "dataSaida": "2024-12-06T04:38:37.078Z",
        ///                 "checkInRealizado": true,
        ///                 "checkOutRealizado": true,
        ///                 "cancelada": true,
        ///                 "reservaServicoAdicionals": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="204">Atualização bem-sucedida.</response>
        /// <response code="400">Se o ID do quarto não corresponder.</response>
        /// <response code="404">Se o quarto não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Exclui um quarto existente.
        /// </summary>
        /// <param name="id">O ID do quarto.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /reserveAqui/quartos/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se o quarto não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Filtra os quartos por capacidade.
        /// </summary>
        /// <param name="capacity">A capacidade mínima dos quartos.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /reserveAqui/quartos/filterByCapacity/{capacity}
        ///{
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "numeroIdentificacao": "string",
        ///         "capacidadeMaxima": 0,
        ///         "disponibilidade": true,
        ///         "hotelId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "preco": 0,
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T04:38:37.078Z",
        ///                 "dataSaida": "2024-12-06T04:38:37.078Z",
        ///                 "checkInRealizado": true,
        ///                 "checkOutRealizado": true,
        ///                 "cancelada": true,
        ///                 "reservaServicoAdicionals": [
        ///                     {
        ///                         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "reservaId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                         "servicoAdicionalId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///                     }
        ///                 ]
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">Retorna a lista de quartos filtrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        [HttpGet("filterByCapacity/{capacity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FilterByCapacity(int capacity)
        {
            var filteredQuartos = _context.Quartos.Where(q => q.CapacidadeMaxima >= capacity && q.Disponibilidade).ToList();
            return Ok(filteredQuartos);
        }
    }
}