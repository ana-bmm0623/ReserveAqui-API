namespace ReserveAqui.Entities
{
    public class Quarto
    {
        public int Id { get; set; }

        public string NumeroIdentificacao { get; set; }

        public int CapacidadeMaxima { get; set; }

        public bool Disponibilidade { get; set; }

        public int HotelId { get; set; }

        public double Preco { get; set; }
        public Hotel Hotel { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}