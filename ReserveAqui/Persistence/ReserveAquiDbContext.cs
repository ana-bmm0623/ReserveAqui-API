using ReserveAqui.Entities;

namespace ReserveAqui.Persistence
{
    public class ReserveAquiDbContext
    {
        public List<Hotel> Hoteis { get; set; }
        public List<Quarto> Quartos { get; set; }
        public List<Reserva> Reservas { get; set; }
        public List<Hospede> Hospedes { get; set; }
        public List<ServicoAdicional> ServicosAdicionais { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public List<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; }

        public ReserveAquiDbContext()
        {
            Hoteis = new List<Hotel>();
            Quartos = new List<Quarto>();
            Reservas = new List<Reserva>();
            Hospedes = new List<Hospede>();
            ServicosAdicionais = new List<ServicoAdicional>();
            Pagamentos = new List<Pagamento>();
            ReservaServicosAdicionais = new List<ReservaServicoAdicional>();
        }
    }
}
