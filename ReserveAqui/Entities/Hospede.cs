using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class Hospede
    {
        public Hospede()
        {
            Reservas = new List<Reserva>();
        }

        public Guid Id { get; set; }

        [Required, StringLength(250)]
        public string NomeCompleto { get; set; }

        [Required, StringLength(11, ErrorMessage = "CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [StringLength(3)]
        public string RG { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Telefone { get; set; }

        [Required, StringLength(250)]
        public string Endereco { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}