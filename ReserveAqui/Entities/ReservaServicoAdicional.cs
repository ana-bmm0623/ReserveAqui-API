using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class ReservaServicoAdicional
    {
        public ReservaServicoAdicional()
        {

        }

        public Guid Id { get; set; }
        public Guid ReservaId { get; set; }
        public Guid ServicoAdicionalId { get; set; }
    }
}