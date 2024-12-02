using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class ServicoAdicional
    {
        public ServicoAdicional()
        {
            ReservaServicosAdicionais = new List<ReservaServicoAdicional>();
        }

        public Guid Id {get; set; }

        [Required, StringLength(250)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }

        public List<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; }

    }
}
