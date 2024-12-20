﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReserveAqui.Entities
{
    public class Pagamento
    {
        public Pagamento() { }

       
        public Guid Id { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime DataPagamento { get; set; }

        [Required]
        public Guid ReservaId { get; set; }

        [Required]
        public MetodoPagamento MetodoPagamento { get; set; }
    }
}
