using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveAqui.Entities
{
    public class Quarto
    {
        public Quarto() {
            Reservas = new List<Reserva>();
            Disponibilidade = true;
        }

       
        public Guid Id { get; set; }

        [Required]
        public string NumeroIdentificacao { get; set; }

        [Required,MaxLength(4)]
        public int CapacidadeMaxima { get; set; }

        public bool Disponibilidade { get; set; }

        [Required]
        public Guid HotelId { get; set; }

        [Required]
        public double Preco { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}