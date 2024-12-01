namespace ReserveAqui.Entities
{
    public class ServicoAdicional
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public List<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; };

    }
}
