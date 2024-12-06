using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveAqui.Entities
{
    public class ServicoAdicional
    {
        
        public Guid Id {get; set; }

        [Required, StringLength(250)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}
