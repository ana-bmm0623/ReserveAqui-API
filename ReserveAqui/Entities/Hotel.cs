﻿namespace ReserveAqui.Entities
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Descricao { get; set; }

        public List<Quarto> Quartos { get; set; }
    }
}