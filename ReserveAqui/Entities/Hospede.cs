namespace ReserveAqui.Entities
{
    public class Hospede
    {
        public int Id { get; set; }

        public string NomeCompleto { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }    

        public string Endereco { get; set; }

        public List<Reserva> Reservas { get; set; }
    }
}