namespace ReserveAqui.Entities
{
    public class ReservaServicoAdicional
    {
        public ReservaServicoAdicional()
        {

        }


        public Guid ReservaId { get; set; }

        public Reserva Reserva { get; set; }

        public Guid ServicoAdicionalId { get; set; }

        public ServicoAdicional ServicoAdicional { get; set; }


    }
}