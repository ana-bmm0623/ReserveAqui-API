using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class ServicoAdicional
    {
        public int Id {get; set; }

        [Required, StringLength(250)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }
    }
}
