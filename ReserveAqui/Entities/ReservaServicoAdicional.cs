namespace ReserveAqui.Entities
{
    public class ReservaServicoAdicional
    {
        public ReservaServicoAdicional()
        {

        }

        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int ServicoAdicionalId { get; set; }
    }
}