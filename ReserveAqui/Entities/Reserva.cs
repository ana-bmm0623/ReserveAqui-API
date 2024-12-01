namespace ReserveAqui.Entities
{
    public class Reserva
    {
        public int Id { get; set; }

        public int QuartoId { get; set; }

        public int HospedeId { get; set; }

        public int QuantidadePessoas { get; set; }

        public DateTime DataEntrada { get; set; }

        public DateTime DataSaida { get; set; }

        public bool CheckInRealizado { get; set; }

        public bool CheckOutRealizado { get; set; }

        public bool Cancelada { get; set; }

        public Quarto Quarto { get; set; }

        public Hospede Hospede { get; set; }

        public List<ReservaServicoAdicional> ReservaServicoAdicionals { get; set; }

    }
}