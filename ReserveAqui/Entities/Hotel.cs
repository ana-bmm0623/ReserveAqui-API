using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class Hotel
    {
        public Hotel()
        {
            Quartos = new List<Quarto>();
        }
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string Nome { get; set; }

        [Required, StringLength(250)]
        public string Endereco { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Telefone { get; set; }

        [Required]
        public string Descricao { get; set; }

        public List<Quarto> Quartos { get; set; }

 
    }
}