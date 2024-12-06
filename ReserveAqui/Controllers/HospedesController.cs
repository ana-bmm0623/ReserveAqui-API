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

        /// <summary>
        /// Retorna todos os hóspedes cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorno:
        /// 
        ///     GET /api/hospedes
        ///     [
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nomeCompleto": "string",
        ///         "cpf": "string",
        ///         "rg": "str",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "endereco": "string",
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T05:02:31.584Z",
        ///                 "dataSaida": "2024-12-06T05:02:31.584Z",
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
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de hóspedes cadastrados.</response>
        /// <response code="400">Se encontrar um erro.</response>
        // GET: api/hospedes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var hospedes = _context.Hospedes.ToList();
            return Ok(hospedes);
        }

        /// <summary>
        /// Retorna um hóspede específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do hóspede.</param>
        /// <remarks>
        /// Exemplo de retorno:
        /// 
        ///     GET /api/hospedes/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nomeCompleto": "string",
        ///         "cpf": "string",
        ///         "rg": "str",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "endereco": "string",
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T05:02:31.584Z",
        ///                 "dataSaida": "2024-12-06T05:02:31.584Z",
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
        /// <response code="200">Retorna o hóspede solicitado.</response>
        /// <response code="404">Se o hóspede não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var hospede = _context.Hospedes.SingleOrDefault(h => h.Id == id);
            if (hospede == null)
            {
                return NotFound();
            }
            return Ok(hospede);
        }

        /// <summary>
        /// Cria um novo hóspede.
        /// </summary>
        /// <param name="hospede">Os dados do novo hóspede.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/hospedes
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nomeCompleto": "string",
        ///         "cpf": "string",
        ///         "rg": "str",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "endereco": "string",
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T05:02:31.584Z",
        ///                 "dataSaida": "2024-12-06T05:02:31.584Z",
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
        /// <response code="201">Retorna o hóspede criado.</response>
        /// <response code="400">Se o hóspede com o mesmo CPF ou Email já existir.</response>
        // POST: api/hospedes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Atualiza um hóspede existente.
        /// </summary>
        /// <param name="id">O ID do hóspede.</param>
        /// <param name="hospede">Os novos dados do hóspede.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT /api/hospedes/{id}
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nomeCompleto": "string",
        ///         "cpf": "string",
        ///         "rg": "str",
        ///         "email": "user@example.com",
        ///         "telefone": "string",
        ///         "endereco": "string",
        ///         "reservas": [
        ///             {
        ///                 "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quartoId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "hospedeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                 "quantidadePessoas": 0,
        ///                 "dataEntrada": "2024-12-06T05:02:31.584Z",
        ///                 "dataSaida": "2024-12-06T05:02:31.584Z",
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
        /// <response code="400">Se o ID do hóspede não corresponder ou houver duplicidade de CPF ou Email.</response>
        /// <response code="404">Se o hóspede não for encontrado.</response>
        // PUT: api/hospedes/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Exclui um hóspede existente.
        /// </summary>
        /// <param name="id">O ID do hóspede.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     DELETE /api/hospedes/{id}
        /// </remarks>
        /// <response code="204">Exclusão bem-sucedida.</response>
        /// <response code="404">Se o hóspede não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
