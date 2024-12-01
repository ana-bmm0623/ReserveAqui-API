using ReserveAqui.Entities;

namespace ReserveAqui.Persistence
{
    public class ReserveAquiDbContext
    {
        List<Hotel> Hoteis { get; set; }

        List<Quarto> Quartos { get; set; }

        List<Reserva> Reservas { get; set; }

        List<Hospede> Hospedes { get; set; }

        List<ServicoAdicional> ServicosAdicionais { get; set; }

        List<Pagamento> Pagamentos { get; set; }

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
