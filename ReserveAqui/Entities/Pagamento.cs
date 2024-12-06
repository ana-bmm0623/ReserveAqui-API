using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class Pagamento
    {
        public Pagamento() { }
        public int Id { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; }

        [Required]
        public int ReservaId { get; set; }

        [Required]
        public MetodoPagamento MetodoPagamento { get; set; }
    }
}
