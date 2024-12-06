using Microsoft.AspNetCore.Mvc;
using ReserveAqui.Entities;
using ReserveAqui.Persistence;
using System.Linq;

namespace ReserveAqui.Controllers
{
    [Route("api/pagamentos")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly ReserveAquiDbContext _context;

        public PagamentosController(ReserveAquiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pagamentos = _context.Pagamentos.ToList();
            return Ok(pagamentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var pagamento = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamento == null)
                return NotFound();

            return Ok(pagamento);
        }

        [HttpPost]
        public IActionResult Post(Pagamento pagamento)
        {
            pagamento.Id = Guid.NewGuid();
            _context.Pagamentos.Add(pagamento);

            return CreatedAtAction(nameof(GetById), new { id = pagamento.Id }, pagamento);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Pagamento pagamento)
        {
            var pagamentoExistente = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamentoExistente == null)
                return NotFound();

            pagamentoExistente.Valor = pagamento.Valor;
            pagamentoExistente.DataPagamento = pagamento.DataPagamento;
            pagamentoExistente.ReservaId = pagamento.ReservaId;
            pagamentoExistente.MetodoPagamento = pagamento.MetodoPagamento;


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var pagamento = _context.Pagamentos.SingleOrDefault(p => p.Id == id);
            if (pagamento == null)
                return NotFound();

            _context.Pagamentos.Remove(pagamento);

            return NoContent();
        }
    }
}