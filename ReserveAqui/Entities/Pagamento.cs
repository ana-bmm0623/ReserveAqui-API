namespace ReserveAqui.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }

        public double Valor { get; set; }

        public DateTime DataPagamento { get; set; }

        public int ReservaId { get; set; }

        public MetodoPagamento MetodoPagamento { get; set; }

        public Reserva Reserva { get; set; }
    }
}
