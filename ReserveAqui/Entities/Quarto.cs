using System;
using System.ComponentModel.DataAnnotations;

namespace ReserveAqui.Entities
{
    public class Quarto
    {
        public Quarto() {
            Reservas = new List<Reserva>();
            Disponibilidade = true;
        }
        public int Id { get; set; }

        [Required]
        public string NumeroIdentificacao { get; set; }

        [Required,MaxLength(4)]
        public int CapacidadeMaxima { get; set; }

        public bool Disponibilidade { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public double Preco { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}